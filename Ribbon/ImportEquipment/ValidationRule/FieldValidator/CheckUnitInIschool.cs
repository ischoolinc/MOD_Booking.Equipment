using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Campus.DocumentValidator;
using FISCA.Data;
using System.Data;

namespace Ischool.Booking.Equipment
{
    class CheckUnitInIschool : IFieldValidator
    {
        private List<string> unitNames;

        public CheckUnitInIschool()
        {
            unitNames = new List<string>();

            QueryHelper qh = new QueryHelper();

            string sql = @"SELECT name FROM $ischool.booking.equip_units";

            DataTable dt = qh.Select(sql);

            foreach (DataRow row in dt.Rows)
            {
                string unitName = row.Field<string>("name");

                if (!unitNames.Contains(unitName))
                    unitNames.Add(unitName);
            }
        }

        public string Correct(string Value)
        {
            return string.Empty;
        }

        public string ToString(string template)
        {
            return template;
        }

        public bool Validate(string Value)
        {
            return unitNames.Contains(Value);
        }
    }
}
