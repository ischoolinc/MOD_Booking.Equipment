using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FISCA.Data;
using K12.Data;
using FISCA.UDT;

namespace Ischool.Booking.Equipment.DAO
{
    class UnitDAO
    {
        private static Dictionary<string, UDT.EquipmentUnit> dicUnits { get; set; }

        public UnitDAO()
        {
            AccessHelper access = new AccessHelper();
            List<UDT.EquipmentUnit> listUnits = access.Select<UDT.EquipmentUnit>();
            dicUnits = new Dictionary<string, UDT.EquipmentUnit>();
            foreach (UDT.EquipmentUnit unit in listUnits)
            {
                dicUnits.Add(unit.Name, unit);
            }
        }

        /// <summary>
        /// 取得所有管理單位資料
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, UDT.EquipmentUnit> GetUnits()
        {
            return dicUnits;
        }

        /// <summary>
        /// 取得所有管理單位資料
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUnitInfo()
        {
            string sql = @"
SELECT
    unit.*
    , teacher.teacher_name
FROM
    $ischool.booking.equip_units AS unit
    LEFT OUTER JOIN teacher
        ON unit.created_by = teacher.st_login_name
";
            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);

            return dt;
        }

        /// <summary>
        /// 新增管理單位
        /// </summary>
        public static void InsertUnitInfo( string unitName,string date,string account)
        {
            string sql = string.Format(@"
INSERT INTO $ischool.booking.equip_units(
    name
    , create_time
    , created_by
)
VALUES(
    '{0}'
    , '{1}'
    , '{2}'
)
                ", unitName, date, account);
            UpdateHelper up = new UpdateHelper();
            up.Execute(sql);
            
        }

        /// <summary>
        /// 修改管理單位
        /// </summary>
        public static void UpdateUnitInfo(string unitName,string date, string account, string unitID)
        {
            string sql = string.Format(@"
UPDATE 
    $ischool.booking.equip_unit
SET
    name = '{0}'
    , create_time = '{1}'
    , created_by = '{2}'
WHERE
    uid = {3}
                ", unitName, date, account, unitID);

            UpdateHelper up = new UpdateHelper();
            up.Execute(sql);

        }

        /// <summary>
        /// 刪除管理單位
        /// </summary>
        public static void DeleteUnitInfo(string unitID)
        {
            string sql = string.Format(@"
DELETE 
FROM 
    $ischool.booking.equip_units
WHERE
    uid = {0}
                        ", unitID);

            UpdateHelper up = new UpdateHelper();
            up.Execute(sql);
        }
    }
}
