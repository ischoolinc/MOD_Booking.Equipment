using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FISCA.Data;
using System.Data;
using K12.Data;

namespace Ischool.Booking.Equipment.DAO
{
    class Role
    {
        /// <summary>
        /// 建立角色
        /// </summary>
        /// <param name="_roleAdminName"></param>
        /// <param name="description"></param>
        /// <param name="_permission"></param>
        /// <returns></returns>
        public static string InsertRole(string _roleAdminName,string description,string _permission)
        {
            string sqlInsert = string.Format(@"
WITH insert_role AS(
    INSERT INTO _role(
        role_name 
        , description
        , permission
    ) 
    VALUES (
        '{0}'
        ,'{1}'
        ,'{2}' 
    )
    RETURNING _role.id
)
SELECT * FROM insert_role

                    ", _roleAdminName, description, _permission);

            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sqlInsert);

            return "" + dt.Rows[0]["id"];
        }

        /// <summary>
        /// 檢查角色是否存在
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static bool CheckIsRoleExist(string roleName)
        {
            string sql = string.Format("SELECT * FROM _role WHERE role_name = '{0}' ", roleName);
            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);

            if (dt.Rows.Count > 0) // 角色存在
            {
                if (roleName == Program._roleAdminName)
                {
                    Program._roleAdminID = "" + dt.Rows[0]["id"];
                }
                if (roleName == Program._roleUnitAdminName)
                {
                    Program._roleUnitAdminID = "" + dt.Rows[0]["id"];
                }
                return true;
            }
            else // 角色不存在
            {
                return false;
            }
        }

        /// <summary>
        /// 更新角色權限
        /// </summary>
        /// <param name="roleID"></param>
        public static void UpdateRole(string roleID,string permission)
        {
            string sql = string.Format(@"
UPDATE 
    _role
SET
    permission = '{0}'
WHERE
    id = {1}
            ", permission, roleID);

            UpdateHelper up = new UpdateHelper();
            up.Execute(sql);
        }
    }
}
