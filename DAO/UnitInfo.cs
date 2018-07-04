using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ischool.Booking.Equipment.DAO
{
    class UnitInfo
    {
        public string unitID { get; set; }

        public string unitName { get; set; }

        public UnitInfo(string id, string name)
        {
            this.unitID = id;
            this.unitName = name;
        }

    }
}
