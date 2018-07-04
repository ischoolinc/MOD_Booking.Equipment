using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FISCA.Data;
using System.Data;

namespace Ischool.Booking.Equipment
{
    class Actor
    {
        private static Actor _actor;

        public static Actor Instance
        {
            get
            {
                if (_actor == null)
                {
                    _actor = new Actor();
                }
                return _actor;
            }
        }

        /// <summary>
        /// 是否為系統管理員
        /// </summary>
        private bool _isSysAdmin = false;

        /// <summary>
        /// 是否為單位管理員
        /// </summary>
        private bool _isUnitAdmin = false;

        public bool isSysAdmin()
        {
            return this._isSysAdmin;
        }

        public bool isUnitAdmin()
        {
            return this._isUnitAdmin;
        }

        /// <summary>
        /// 取得使用者登入帳號
        /// </summary>
        public static string Account
        {
            get
            {
                return FISCA.Authentication.DSAServices.UserAccount.Replace("'", "''");
            }
        }

        private List<DAO.UnitInfo> _units;

        /// <summary>
        /// 登入帳號對應到的老師系統編號
        /// </summary>
        private string _teacherID;

        /// <summary>
        /// 透過使用者登入帳號取得_loginID
        /// </summary>
        /// <returns></returns>
        public static string GetLoginIDByAccount(string targetAccount)
        {
            string loginID;
            string sql = string.Format("SELECT * FROM _login WHERE login_name = '{0}'", targetAccount);
            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);

            if (dt.Rows.Count > 0)
            {
                loginID = "" + dt.Rows[0]["id"];
            }
            else
            {
                loginID = "";
            }

            return loginID;
        }

        public Actor()
        {
            _units = new List<DAO.UnitInfo>();
            // 1.確認使用者是否為模組管理者
            CheckSysAdmin();

            // 2. 判斷在各單位的角色
            CheckUnitAdmin();


        }

        public void CheckSysAdmin()
        {
            string sql = string.Format(@"
SELECT 
    teacher.*
FROM
    teacher
    LEFT OUTER JOIN _login
        ON teacher.st_login_name = _login.login_name
    LEFT OUTER JOIN _lr_belong
        ON _login.id = _lr_belong._login_id
WHERE
    teacher.st_login_name = '{0}'
    AND _lr_belong._role_id = {1}
                ",Actor.Account,Program._roleAdminID);
            //string sql = string.Format(@"
            //SELECT 
            //    teacher.id
            //    , _login.login_name
            //FROM
            //    _lr_belong
            //    LEFT OUTER JOIN _login
            //        ON _lr_belong._login_id = _login.id
            //    LEFT OUTER JOIN teacher
            //        ON _login.login_name = teacher.st_login_name
            //WHERE
            //    _lr_belong._role_id = {0} 
            //    AND _login.login_name='{1}'
            //", Program._roleAdminID, Actor.Account);

            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);

            this._isSysAdmin = (dt.Rows.Count > 0);
            this._teacherID = (dt.Rows.Count > 0 ? dt.Rows[0]["id"].ToString() : "");
        }

        public void CheckUnitAdmin()
        {
            QueryHelper qh = new QueryHelper();

            string sql = string.Format(@"
                                    SELECT 
                                        unit.name AS unit_name
                                        , unit_admin.*
                                    FROM
                                        $ischool.booking.equip_unit_admin AS unit_admin
                                        LEFT OUTER JOIN $ischool.booking.equip_units AS unit
                                            ON unit_admin.ref_unit_id = unit.uid
                                    WHERE
                                        unit_admin.account = '{0}'
                ", Actor.Account);

            DataTable dt = qh.Select(sql);
            foreach (DataRow row in dt.Rows)
            {
                string unitID = "" + row["ref_unit_id"];
                string unitName = "" + row["unit_name"];
                bool isBoss = ("" + row["is_boss"]) == "true" ? true : false;
                string teacherID = "" + row["ref_teacher_id"];
                DAO.UnitInfo unitRole = new DAO.UnitInfo(unitID, unitName);
                this._isUnitAdmin = true;

                this._units.Add(unitRole);
            }
        }

        public List<DAO.UnitInfo> getUnitAdminUnits()
        {
            return this._units;
        }
    }
}
