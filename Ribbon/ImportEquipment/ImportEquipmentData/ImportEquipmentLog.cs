using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Ischool.Booking.Equipment
{
    class ImportEquipmentLog
    {
        private Dictionary<string, string> dicUnits;

        public ImportEquipmentLog()
        {
            dicUnits = new Dictionary<string, string>();

            foreach (DataRow row in DAO.UnitDAO.GetUnitInfo().Rows)
            {
                dicUnits.Add("" + row["uid"],"" + row["name"]);
            }
        }

        public string GetLogString(UDT.Equipment equip)
        {
            StringBuilder log = new StringBuilder();

            try
            {
                log.AppendLine(string.Format("設備名稱「{0}」", equip.Name));

                if (!string.IsNullOrEmpty(equip.Category))
                {
                    log.AppendLine(string.Format("類別「{0}」", equip.Category));
                }

                log.AppendLine(string.Format("財產編號「{0}」", equip.PropertyNo));

                if (!string.IsNullOrEmpty(equip.Company))
                {
                    log.AppendLine(string.Format("廠牌「{0}」", equip.Company));
                }

                if (!string.IsNullOrEmpty("" + equip.ModelNo))
                {
                    log.AppendLine(string.Format("型號「{0}」", equip.ModelNo));
                }

                if (!string.IsNullOrEmpty(equip.Status))
                {
                    log.AppendLine(string.Format("設備狀態「{0}」", equip.Status));
                }

                if (!string.IsNullOrEmpty("" + equip.DeadLine))
                {
                    log.AppendLine(string.Format("未取用解除預約時間(分)「{0}」", equip.DeadLine));
                }

                if (!string.IsNullOrEmpty("" + equip.Place))
                {
                    log.AppendLine(string.Format("放置位置「{0}」", equip.Place));
                }

                if (!string.IsNullOrEmpty("" + equip.RefUnitID))
                {
                    if (dicUnits.ContainsKey("" + equip.RefUnitID))
                    {
                        log.AppendLine(string.Format("管理單位名稱「{0}」", dicUnits["" + equip.RefUnitID]));
                    }
                }

                log.AppendLine(string.Format("建立日期「{0}」", DateTime.Now.ToShortDateString()));

                log.AppendLine(string.Format("建立者「{0}」", Actor.Account));
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }


            return log.ToString();
        }
    }
}
