using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ischool.Booking.Equipment.DAO
{
    /// <summary>
    /// 管理 UnitInfo 的集合物件
    /// </summary>
    class Units
    {
        private static Units _instance;
        public static Units getInstance()
        {
            if (_instance == null)
            {
                _instance = new Units();
            }
            return _instance;
        }

        private List<UnitInfo> units;
        private Dictionary<string, UnitInfo> dicUnits;

        private Units()
        {
            this.units = new List<UnitInfo>();
            this.dicUnits = new Dictionary<string, UnitInfo>();
        }

        public void add(UnitInfo unit)
        {
            if (unit == null)
            {
                return;
            }

            if (!this.dicUnits.ContainsKey(unit.unitID))
            {
                this.dicUnits.Add(unit.unitID, unit);
                this.units.Add(unit);
            }
        }

        public Dictionary<string, UnitInfo> getAllUnits()
        {
            return this.dicUnits;
        }

        public UnitInfo getUnitByID(string unitID)
        {
            UnitInfo result = null;
            if (this.dicUnits.ContainsKey(unitID))
            {
                result = this.dicUnits[unitID];
            }
            return result;
        }
    }
}
