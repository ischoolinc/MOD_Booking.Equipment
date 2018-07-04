using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FISCA.Data;

namespace Ischool.Booking.Equipment.DAO
{
    class StatisticalReportDAO
    {

        public static DataTable GetReportData(string unitID, string starDate, string endDate)
        {
            string sql = "";
            if (unitID == "--全部--")
            {
                sql = string.Format(@"
WITH data_row AS(
SELECT 
    unit.name AS unit_name
    , equip.name AS equip_name
    , history.*
FROM
    $ischool.booking.equip_applications AS app
    LEFT OUTER JOIN $ischool.booking.equip_application_detail AS app_detail
        ON app.uid = app_detail.ref_application_id
    LEFT OUTER JOIN $ischool.booking.equip_io_history AS history
        ON app_detail.uid = history.ref_app_detail_id
    LEFT OUTER JOIN $ischool.booking.equipment AS equip
        ON app.ref_equip_id = equip.uid
    LEFT OUTER JOIN $ischool.booking.equip_units AS unit
        ON equip.ref_unit_id = unit.uid
WHERE
    app.apply_start_date >= '{0}'
    AND app.apply_start_date <= '{1}'
)
SELECT
    unit_name
    , equip_name
    , count(*) AS 使用次數
    , SUM(return_time - borrow_time) AS 使用時數
FROM
    data_row
WHERE
    return_time IS NOT NULL
GROUP BY
    unit_name
    , equip_name
                ", starDate, endDate);
            }
            else
            {
                sql = string.Format(@"
WITH data_row AS(
SELECT 
    unit.name AS unit_name
    , equip.name AS equip_name
    , history.*
FROM
    $ischool.booking.equip_applications AS app
    LEFT OUTER JOIN $ischool.booking.equip_application_detail AS app_detail
        ON app.uid = app_detail.ref_application_id
    LEFT OUTER JOIN $ischool.booking.equip_io_history AS history
        ON app_detail.uid = history.ref_app_detail_id
    LEFT OUTER JOIN $ischool.booking.equipment AS equip
        ON app.ref_equip_id = equip.uid
    LEFT OUTER JOIN $ischool.booking.equip_units AS unit
        ON equip.ref_unit_id = unit.uid
WHERE
    unit.uid = {2}
    AND app.apply_start_date >= '{0}'
    AND app.apply_start_date <= '{1}'
)
SELECT
    unit_name
    , equip_name
    , count(*) AS 使用次數
    , SUM(borrow_time - return_time) AS 使用時數
FROM
    data_row
WHERE
    return_time IS NOT NULL
GROUP BY
    unit_name
    , equip_name
                ", starDate, endDate,unitID);
            }

            QueryHelper qh = new QueryHelper();
            DataTable dt = qh.Select(sql);

            return dt;
        }
    }
}
