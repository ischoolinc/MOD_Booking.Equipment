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
    class EquipmentDAO
    {
        private static Dictionary<string, UDT.Equipment> dicEquipments;

        /// <summary>
        /// 取得所有設備資料，Key:財產編號
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, UDT.Equipment> GetEquipments()
        {
            dicEquipments = new Dictionary<string, UDT.Equipment>();

            AccessHelper access = new AccessHelper();
            List<UDT.Equipment> listEquips = access.Select<UDT.Equipment>();
            foreach (UDT.Equipment equip in listEquips)
            {
                dicEquipments.Add(equip.PropertyNo,equip);
            }

            return dicEquipments;
        }

        /// <summary>
        /// 取得所有設備資料
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllEquipment()
        {
            string sql = @"
SELECT
    equip.*
    , unit.name AS unit_name
FROM
    $ischool.booking.equipment AS equip
    LEFT OUTER JOIN $ischool.booking.equip_units AS unit
        ON equip.ref_unit_id = unit.uid
";
            QueryHelper qh = new QueryHelper();

            return qh.Select(sql);
        }

        /// <summary>
        /// 透過UnitIDs取得設備資料
        /// </summary>
        /// <param name="unitIDs"></param>
        /// <returns></returns>
        public static Dictionary<string, UDT.Equipment> GetEquipmentsByUnitIDs(List<string>unitIDs)
        {
            dicEquipments = new Dictionary<string, UDT.Equipment>();

            AccessHelper access = new AccessHelper();
            List<UDT.Equipment> listEquips = access.Select<UDT.Equipment>(string.Format("ref_unit_id IN({0})",string.Join(",",unitIDs)));
            foreach (UDT.Equipment equip in listEquips)
            {
                dicEquipments.Add(equip.PropertyNo, equip);
            }

            return dicEquipments;
        }

        /// <summary>
        /// 取得單位設備資料
        /// </summary>
        /// <returns></returns>
        public static DataTable GetUnitEquipment(string unitID)
        {
            string sql = string.Format(@"
SELECT
    *
FROM
    $ischool.booking.equipment
WHERE
    ref_unit_id = {0}
ORDER BY
	category
	--, property_no
    , company
    , model_no
            ", unitID);

            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);

            return dt;
        }

        /// <summary>
        /// 新增設備資料
        /// </summary>
        /// <param name="name"></param>
        /// <param name="property"></param>
        /// <param name="company"></param>
        /// <param name="model"></param>
        /// <param name="status"></param>
        /// <param name="deadline"></param>
        /// <param name="place"></param>
        /// <param name="unitID"></param>
        public static void InsertUnitEquipment(string name,string category,string property,string company,string model,string status,string deadline,string place,string unitID)
        {
            string sql = string.Format(@"
INSERT INTO $ischool.booking.equipment(
    name
    , category
    , property_no
    , company
    , model_no
    , status
    , deadline
    , place
    , create_time
    , created_by
    , ref_unit_id
)
VALUES(
    '{0}'
    , '{1}' 
    , '{2}'
    , '{3}'
    , '{4}'
    , '{5}'
    , {6}
    , '{7}'
    , '{8}'
    , '{9}'
    , {10}
)
            ",name, category,property, company, model, status, deadline, place,DateTime.Now.ToShortDateString(),Actor.Account,unitID);

            UpdateHelper up = new UpdateHelper();
            up.Execute(sql);
        }

        /// <summary>
        /// 更新設備資料
        /// </summary>
        /// <param name="name"></param>
        /// <param name="propertyNo"></param>
        /// <param name="company"></param>
        /// <param name="model"></param>
        /// <param name="status"></param>
        /// <param name="deadline"></param>
        /// <param name="place"></param>
        /// <param name="unitID"></param>
        /// <param name="equipID"></param>
        public static void UpdateUnitEquipment(string name,string category,string propertyNo,string company,string model,string status,string deadline,string place,string unitID,string equipID)
        {
            string sql = string.Format(@"
UPDATE $ischool.booking.equipment
SET
     name = '{0}'
    , category = '{1}'
    , property_no = '{2}'
    , company = '{3}'
    , model_no = '{4}'
    , status = '{5}'
    , deadline = '{6}'
    , place = '{7}'
    , create_time = '{8}'
    , created_by = '{9}'
    , ref_unit_id = {10}
WHERE
    uid ={11}
            ",name,category,propertyNo,company,model,status,deadline,place,DateTime.Now.ToShortDateString(),Actor.Account,unitID, equipID);

            UpdateHelper up = new UpdateHelper();
            up.Execute(sql);
        }

        /// <summary>
        /// 刪除設備資料
        /// </summary>
        /// <param name="equipID"></param>
        public static void DELETEUnitEquipment(string equipID)
        {
            string sql = string.Format(@"
DELETE
FROM
    $ischool.booking.equipment
WHERE
    uid = {0}
            ",equipID);

            UpdateHelper up = new UpdateHelper();
            up.Execute(sql);
        }
    }
}
