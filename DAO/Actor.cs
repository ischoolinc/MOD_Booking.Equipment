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
        private bool _isSysAdmin = false;
        private bool _isUnitAdmin = false;
        // 登入帳號對應到的老師系統編號
        private string _teacherID;
        private QueryHelper _qh = new QueryHelper();
        private List<DAO.UnitInfo> _units;
        private static Actor _actor;

        public static string UserAccount
        {
            get
            {
                return FISCA.Authentication.DSAServices.UserAccount.Replace("'", "''");
            }
        }

        public Actor()
        {
            _units = new List<DAO.UnitInfo>();
            // 1.確認使用者是否為模組管理者
            CheckSysAdmin();

            // 2. 判斷在各單位的角色
            CheckUnitAdmin();

            GetTeacherDataByAccount();
        }

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

        public bool isSysAdmin()
        {
            return this._isSysAdmin;
        }

        public bool isUnitAdmin()
        {
            return this._isUnitAdmin;
        }

        public string GetTeacherID()
        {
            return this._teacherID;
        }

        private void CheckSysAdmin()
        {
            string sql = string.Format(@"
SELECT 
    _login.*
FROM
     _login
    LEFT OUTER JOIN _lr_belong
        ON _login.id = _lr_belong._login_id
WHERE
    _login.login_name = '{0}'
    AND _lr_belong._role_id = {1}
                ", Actor.UserAccount,Program._roleAdminID);

            DataTable dt = this._qh.Select(sql);

            this._isSysAdmin = (dt.Rows.Count > 0);
        }

        private void GetTeacherDataByAccount()
        {
            string sql = string.Format(@"
SELECT 
    *
FROM
    teacher
WHERE
    teacher.st_login_name = '{0}'
            ",Actor.UserAccount);

            DataTable dt = this._qh.Select(sql);
            this._teacherID = (dt.Rows.Count > 0 ? dt.Rows[0]["id"].ToString() : "");
        }

        private void CheckUnitAdmin()
        {
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
                ", Actor.UserAccount);

            DataTable dt = this._qh.Select(sql);


            foreach (DataRow row in dt.Rows)
            {
                string unitID = "" + row["ref_unit_id"];
                string unitName = "" + row["unit_name"];
                DAO.UnitInfo unitRole = new DAO.UnitInfo(unitID, unitName);
                this._isUnitAdmin = true;
                this._units.Add(unitRole);
            }
        }

        /// <summary>
        /// 取得使用者管理的單位
        /// </summary>
        /// <returns></returns>
        public List<DAO.UnitInfo> getUnitAdminUnits()
        {
            return this._units;
        }
    }
}
