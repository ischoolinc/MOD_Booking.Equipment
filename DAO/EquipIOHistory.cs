using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Data;
using System.Data;
using K12.Data;

namespace Ischool.Booking.Equipment.DAO
{
    class EquipIOHistory
    {
        private bool isBorrow { get; set; }

        public EquipIOHistory(string equipID)
        {
            // 判斷設備狀態 : 是否出借中
            DataTable dt = GetBorrowData(equipID);
            this.isBorrow = dt.Rows.Count > 0;
        }

        // 如果設備尚未歸還 IsBorrow = true
        public bool IsBorrow()
        {
            return this.isBorrow;
        }

        /// <summary>
        /// 取得出借紀錄
        /// </summary>
        /// <param name="equipID"></param>
        /// <returns></returns>
        public static DataTable GetBorrowData(string equipID)
        {
            string sql = string.Format(@"
SELECT
	app.applicant_name
	, app.apply_date
	, app.apply_reason
	, app_detail.start_time
	, app_detail.end_time
	, history.*
FROM
    $ischool.booking.equip_applications AS app
    LEFT OUTER JOIN $ischool.booking.equip_application_detail AS app_detail
        ON app.uid = app_detail.ref_application_id
    LEFT OUTER JOIN $ischool.booking.equip_io_history AS history
        ON app_detail.uid = history.ref_app_detail_id
WHERE
    app.ref_equip_id = {0}
	AND history.uid IS NOT NULL
	AND history.return_time IS NULL
                ", equipID);

            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);

            return dt;
        }

        /// <summary>
        /// 設備歸還
        /// </summary>
        /// <param name="time"></param>
        /// <param name="history"></param>
        public static void ReturnEquip(string history)
        {
            string sql = string.Format(@"
UPDATE $ischool.booking.equip_io_history 
SET 
    return_time = '{0}'
WHERE
    uid = {1}
            ", DateTime.Now.ToString("yyyy/MM/dd HH:mm"), history);

            UpdateHelper up = new UpdateHelper();
            up.Execute(sql);

        }

        /// <summary>
        /// 設備借出
        /// </summary>
        /// <param name="appDetailID"></param>
        public static void BorrowEquip(string appDetailID)
        {
            string sql = string.Format(@"
INSERT INTO $ischool.booking.equip_io_history(
    ref_app_detail_id
    , borrow_time
)
VALUES(
    {0}
    , '{1}'
)
                ",appDetailID,DateTime.Now.ToString("yyyy/MM/dd HH:mm"));

            UpdateHelper up = new UpdateHelper();
            up.Execute(sql);
        }

        /// <summary>
        /// 取得設備當天申請紀錄
        /// </summary>
        /// <param name="equipID"></param>
        /// <returns></returns>
        public static DataTable GetApplicationByEquipID(string equipID)
        {
            string sql = string.Format(@"
SELECT 
    app.applicant_name
    , app.apply_date
    , app.apply_reason
    , app_detail.*
FROM
    $ischool.booking.equip_applications AS app
    LEFT OUTER JOIN $ischool.booking.equip_application_detail AS app_detail
        ON app.uid = app_detail.ref_application_id
    LEFT OUTER JOIN $ischool.booking.equip_io_history AS history
        ON app_detail.uid = history.ref_app_detail_id
WHERE
    app.ref_equip_id = {0}
    AND app.is_canceled = false
    AND app_detail.is_canceled = false
    AND date_trunc('day',app_detail.start_time) =  CURRENT_DATE
    AND history.uid IS NULL
ORDER BY
    start_time
            ", equipID);

            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);

            return dt;
        }

        /// <summary>
        /// 更新申請紀錄 : 如逾時更新為取消
        /// </summary>
        public static void UpdateApplicationDetail()
        {
            string sql = @"
update $ischool.booking.equip_application_detail detail
set is_canceled = true,
    canceled_time = now(),
    canceled_name = '系統自動',
    canceled_by = null,
    cancel_reason = '逾時',
    last_update = now()
where is_canceled is not true
    and start_time + (deadline || ' minute')::interval < now()
    and ref_application_id in (select uid from $ischool.booking.equip_applications where is_canceled is not true)
    and (select count(*) from $ischool.booking.equip_io_history where ref_app_detail_id = detail.uid) <= 0 
";
            UpdateHelper up = new UpdateHelper();
            up.Execute(sql);
        }
    }
}
