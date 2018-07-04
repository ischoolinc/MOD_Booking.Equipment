using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.UDT;

namespace Ischool.Booking.Equipment.UDT
{
    /// <summary>
    /// 設備預約申請時段明細
    /// </summary>
    [TableName("ischool.booking.equip_application_detail")]
    class EquipmentApplicationDetail : ActiveRecord
    {
        /// <summary>
        /// 申請紀錄系統編號
        /// </summary>
        [Field(Field = "ref_application_id", Indexed = false)]
        public int RefApplicationID { get; set; }

        /// <summary>
        /// 預約開始時間
        /// </summary>
        [Field(Field = "start_time", Indexed = false)]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 預約結束時間
        /// </summary>
        [Field(Field = "end_time", Indexed = false)]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 未取用取消時間
        /// </summary>
        [Field(Field = "deadline", Indexed = false)]
        public int DeadLine { get; set; }

        /// <summary>
        /// 是否取消
        /// </summary>
        [Field(Field = "is_canceled", Indexed = false)]
        public bool IsCancel { get; set; }

        /// <summary>
        /// 取消時間
        /// </summary>
        [Field(Field = "canceled_time", Indexed = false)]
        public DateTime CanceledTime { get; set; }

        /// <summary>
        /// 取消者姓名
        /// </summary>
        [Field(Field = "canceled_name", Indexed = false)]
        public string CanceledName { get; set; }

        /// <summary>
        /// 取消者教師編號
        /// </summary>
        [Field(Field = "canceled_by", Indexed = false)]
        public int CanceledBy { get; set; }

        /// <summary>
        /// 取消原因
        /// </summary>
        [Field(Field = "cancel_reason", Indexed = false)]
        public string CancelReason { get; set; }
    }
}
