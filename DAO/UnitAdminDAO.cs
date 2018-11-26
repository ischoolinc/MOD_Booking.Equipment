using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FISCA.Data;
using K12.Data;

namespace Ischool.Booking.Equipment.DAO
{
    class UnitAdminDAO
    {
        private static QueryHelper _qh = new QueryHelper();
        private static UpdateHelper _up = new UpdateHelper();

        /// <summary>
        /// 取得尚未指定模組管理員與在該單位尚未指定管理員身分的老師清單
        /// </summary>
        /// <returns></returns>
        public static DataTable GetTeacher(string unitID)
        {
            string sql = string.Format(@"
SELECT
    teacher.*
FROM
    teacher
    LEFT OUTER JOIN $ischool.booking.equip_unit_admin AS unit_admin
        ON unit_admin.ref_teacher_id = teacher.id
        AND unit_admin.ref_unit_id = {0}
WHERE
    unit_admin.uid IS NULL
    AND teacher.status = 1
            ",unitID);

            #region old sql
//            string sql = string.Format(@"
//SELECT
//    teacher.*
//FROM    
//    teacher
//    LEFT OUTER JOIN $ischool.booking.equip_unit_admin AS unit_admin
//        ON unit_admin.ref_teacher_id = teacher.id
//        AND unit_admin.ref_unit_id = {1}
//    LEFT OUTER JOIN(
//        SELECT
//            _login.login_name
//        FROM
//            _login
//            LEFT OUTER JOIN _lr_belong
//                ON _login.id = _lr_belong._login_id
//        WHERE
//            _lr_belong._role_id = {0}
//    ) target_login
//        ON teacher.st_login_name = target_login.login_name
//WHERE
//    unit_admin.uid IS NULL
//    AND target_login.login_name IS NULL
//    AND teacher.status = 1
//                ", Program._roleAdminID, unitID); 
            #endregion

            //QueryHelper qh = new QueryHelper();
            DataTable dt = UnitAdminDAO._qh.Select(sql);
            return dt;
        }

        /// <summary>
        /// 新增單位管理員
        /// </summary>
        /// <param name="unitID"></param>
        /// <param name="teacherID"></param>
        /// <param name="teacherAccount"></param>
        /// <param name="loginID"></param>
        /// <param name="createTime"></param>
        /// <param name="createdBy"></param>
        public static void AssignUnitAdmin(string unitID,string teacherID,string teacherAccount,string loginID,string createTime,string createdBy)
        {
            string sql = "";
            // 判斷教師登入帳號是否存在系統登入帳號
            if (loginID == "")
            {
                sql = string.Format(@"
WITN insert_unit_admin AS(
    INSERT INTO $ischool.booking.equip_unit_admin(
        account
        , ref_unit_id
        , ref_teacher_id
        , is_boss
        , create_time
        , created_by
    )
    VALUES(
        '{0}'
        , {1}
        , {2}
        , 'false'
        , '{4}'
        , '{5}'
    )
) ,insert_login AS(
    INSERT INTO _login(
        login_name
        , password
        , sys_admin
        , account_type
    )
    VALUES(
        '{0}'
        , '1234'
        , '0'
        , 'greening'
    )
    RETURNING _login.id
)
INSERT INTO _lr_belong(
    _login_id
    , _role_id
)
SELECT
    insert_login.id
    , {6}
FROM
    insert_login
                    ", unitID, teacherID, teacherAccount, loginID, createTime, createdBy,Program._roleUnitID);
            }
            else
            {
                sql = string.Format(@"
WITH insert_unit_admin AS(
    INSERT INTO $ischool.booking.equip_unit_admin(
        account
        , ref_unit_id
        , ref_teacher_id
        , is_boss
        , create_time
        , created_by
    )
    VALUES(
        '{0}'
        , {1}
        , {2}
        , 'false'
        , '{4}'
        , '{5}'
    )
) 
INSERT INTO _lr_belong(
    _login_id
    , _role_id
)
SELECT
    {3}
    , {6}
                    ", teacherAccount, unitID, teacherID, loginID, createTime, createdBy,Program._roleUnitID);
            }
            //UpdateHelper up = new UpdateHelper();
            UnitAdminDAO._up.Execute(sql);
            
        }

        /// <summary>
        /// 取得單位管理員
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        public static DataTable GetUnitAdmins(string unitID)
        {
            string sql = string.Format(@"
SELECT
    unit_admin.*
    , teacher.teacher_name
    , teacher2.teacher_name AS created_name
FROM
    $ischool.booking.equip_unit_admin AS unit_admin
    LEFT OUTER JOIN teacher
        ON unit_admin.ref_teacher_id = teacher.id
    LEFT OUTER JOIN teacher AS teacher2
        ON unit_admin.created_by = teacher2.st_login_name
WHERE
    unit_admin.ref_unit_id = {0}
            ",unitID);

            //QueryHelper qh = new QueryHelper();
            DataTable dt = UnitAdminDAO._qh.Select(sql);

            return dt;
        }

        /// <summary>
        /// 刪除單位管理員
        /// </summary>
        /// <param name="unitAdminID"></param>
        public static void DeleteUnitAdmin(string unitAdminID)
        {
            string sql = string.Format(@"
DELETE
FROM
    $ischool.booking.equip_unit_admin
WHERE
    uid = {0}
                    ", unitAdminID);

            //UpdateHelper up = new UpdateHelper();
            UnitAdminDAO._up.Execute(sql);
        }
    }
}
