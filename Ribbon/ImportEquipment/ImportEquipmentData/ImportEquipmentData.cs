using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Campus.Import;
using FISCA.UDT;
using Campus.DocumentValidator;

namespace Ischool.Booking.Equipment
{
    class ImportEquipmentData : ImportWizard
    {
        private ImportOption mOption;

        private ImportEquipmentLog log = new ImportEquipmentLog();

        DAO.UnitDAO dao = new DAO.UnitDAO();

        // 第一步 : 取得XML設備匯入設定檔
        public override string GetValidateRule()
        {
            return Properties.Resources.EquipmentValidate;
        }

        // 第二步 : 設定匯入模式
        public override ImportAction GetSupportActions()
        {
            // 新增或更新
            return ImportAction.InsertOrUpdate;
        }

        // 第三步
        public override void Prepare(ImportOption Option)
        {
            mOption = Option;
        }

        // 第四步
        public override string Import(List<Campus.DocumentValidator.IRowStream> Rows)
        {
            if (mOption.Action == ImportAction.InsertOrUpdate)
            {
                List<UDT.Equipment> listEquipmentInsert = new List<UDT.Equipment>();
                List<UDT.Equipment> listEquipmentUpdate = new List<UDT.Equipment>();

                // 1. 資料整理
                ParseData(Rows, listEquipmentInsert, listEquipmentUpdate);

                // 2. 儲存
                Save(listEquipmentInsert, listEquipmentUpdate);
            }

            return "";
        }

        /// <summary>
        /// 資料整理
        /// </summary>
        /// <param name="Rows"></param>
        /// <param name="listEquipmentInsert"></param>
        /// <param name="listEquipmentUpdate"></param>
        private void ParseData(List<Campus.DocumentValidator.IRowStream> Rows, List<UDT.Equipment> listEquipmentInsert, List<UDT.Equipment> listEquipmentUpdate)
        {
            // 對於每一筆場地的設備資料
            foreach (IRowStream Row in Rows)
            {
                //a. 確認場地是新增或修改
                string propertyNo = Row.GetValue("財產編號");
                string key = string.Format("{0}", propertyNo); // 設備key

                Dictionary<string, UDT.Equipment> dicEquips = DAO.Equipment.GetEquipments();
                
                //更新
                if (dicEquips.ContainsKey(key)) 
                {
                    UDT.Equipment equipData = dicEquips[key];

                    // 將Row資料填入UDT.Equipment
                    FillData(Row, equipData);
                    listEquipmentUpdate.Add(equipData);
                }
                //新增
                else
                {
                    UDT.Equipment equipData = new UDT.Equipment();

                    FillData(Row, equipData);
                    listEquipmentInsert.Add(equipData);
                }
            }
        }

        private void FillData(IRowStream Row, UDT.Equipment equip)
        {
            equip.Name = Row.GetValue("設備名稱");
            equip.Category = Row.GetValue("類別");
            equip.PropertyNo = Row.GetValue("財產編號");
            equip.Company = Row.GetValue("廠牌");
            equip.ModelNo = Row.GetValue("型號");
            equip.Status = Row.GetValue("設備狀態");
            equip.DeadLine = Row.GetValue("未取用解除預約時間(分)") == "" ? 0 : int.Parse(Row.GetValue("未取用解除預約時間(分)"));
            equip.Place = Row.GetValue("放置位置");
            equip.RefUnitID = int.Parse(CheckUnitName(Row.GetValue("管理單位名稱")));
            equip.CreateTime = DateTime.Now;
            equip.CreatedBy = Actor.UserAccount;
        }

        public string CheckUnitName(string unitName)
        {            
            Dictionary<string,UDT.EquipmentUnit> dicUnits = DAO.UnitDAO.GetUnits();

            if (dicUnits.ContainsKey(unitName))
            {
                return dicUnits[unitName].UID;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 資料寫入系統
        /// </summary>
        /// <param name="listEquipmentInsert"></param>
        /// <param name="listEquipmentUpdate"></param>
        private void Save(List<UDT.Equipment> listEquipmentInsert, List<UDT.Equipment> listEquipmentUpdate)
        {
            AccessHelper access = new AccessHelper();

            if (listEquipmentInsert.Count > 0)
            {
                try
                {
                    StringBuilder mstrLog1 = new StringBuilder();
                    mstrLog1.AppendLine("新增匯入設備：");
                    foreach (UDT.Equipment each in listEquipmentInsert)
                    {
                        mstrLog1.AppendLine(log.GetLogString(each));
                    }
                    access.InsertValues(listEquipmentInsert);
                    FISCA.LogAgent.ApplicationLog.Log("設備", "新增匯入", mstrLog1.ToString());
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }

            if (listEquipmentUpdate.Count > 0)
            {
                try
                {
                    StringBuilder mstrLog2 = new StringBuilder();
                    mstrLog2.AppendLine("更新匯入設備：");
                    foreach (UDT.Equipment each in listEquipmentUpdate)
                    {
                        mstrLog2.AppendLine(log.GetLogString(each));

                    }
                    access.UpdateValues(listEquipmentUpdate);
                    FISCA.LogAgent.ApplicationLog.Log(Actor.UserAccount, "更新匯入", mstrLog2.ToString());
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
        }

        
    }
}
