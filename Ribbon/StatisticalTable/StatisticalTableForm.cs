using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using Aspose.Cells;
using System.IO;
using System.Diagnostics;

namespace Ischool.Booking.Equipment
{
    public partial class StatisticalTableForm : BaseForm
    {
        Actor actor = new Actor();

        Dictionary<string, string> dicUnitNameID = new Dictionary<string, string>();

        public StatisticalTableForm()
        {
            InitializeComponent();
        }

        private void StatisticalTableForm_Load(object sender, EventArgs e)
        {
            DataTable dt = DAO.UnitDAO.GetUnitInfo();
            foreach (DataRow row in dt.Rows)
            {
                dicUnitNameID.Add("" + row["name"],"" + row["uid"]);
            }

            if (actor.isSysAdmin())
            {
                lbIdentity.Text = "設備預約模組管理者";

                foreach (DataRow row in dt.Rows)
                {
                    cbxUnit.Items.Add("" + row["name"]);
                }
                cbxUnit.Items.Insert(0,"--全部--");
            }
            else if (actor.isUnitAdmin())
            {
                lbIdentity.Text = "單位管理員";

                List<DAO.UnitInfo> listUnitInfo = actor.getUnitAdminUnits();
                foreach (DAO.UnitInfo unit in listUnitInfo)
                {
                    cbxUnit.Items.Add(unit.unitName);
                }
            }
            if (cbxUnit.Items.Count > 0)
            {
                cbxUnit.SelectedIndex = 0;
            }

            dtStar.Text = DateTime.Now.AddDays(-7).ToShortDateString();
            dtEnd.Text = DateTime.Now.ToShortDateString();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dt;
            if (cbxUnit.Text == "--全部--")
            {
                dt = DAO.StatisticalReportDAO.GetReportData("--全部--",dtStar.Value.ToShortDateString(),dtEnd.Value.ToShortDateString());
            }
            else
            {
                string unitID = dicUnitNameID[cbxUnit.Text];
                dt = DAO.StatisticalReportDAO.GetReportData(unitID, dtStar.Value.ToShortDateString(), dtEnd.Value.ToShortDateString());
            }

            // 列印樣板設定
            Workbook book = new Workbook();
            Workbook template = new Workbook();
            template.Open(new MemoryStream(Properties.Resources.統計設備報表樣板), FileFormatType.Excel2003);

            book.Copy(template);
            Worksheet sheet = book.Worksheets[0];
            Style style = sheet.Cells[2, 0].Style;

            #region 寫入資料
            int rowIndex = 3;
            sheet.Cells[0, 1].PutValue(dtStar.Value.ToShortDateString() + " ~ " + dtEnd.Value.ToShortDateString());

            foreach (DataRow row in dt.Rows)
            {
                int colIndex = 0;

                sheet.Cells[rowIndex, colIndex].PutValue("" + row["unit_name"]);
                sheet.Cells[rowIndex, colIndex++].Style = style;

                sheet.Cells[rowIndex, colIndex].PutValue("" + row["equip_name"]);
                sheet.Cells[rowIndex, colIndex++].Style = style;

                sheet.Cells[rowIndex, colIndex].PutValue("" + row["使用次數"]);
                sheet.Cells[rowIndex, colIndex++].Style = style;

                sheet.Cells[rowIndex, colIndex].PutValue(DateTime.Parse("" + row["使用時數"]).ToString("hh"));
                sheet.Cells[rowIndex, colIndex++].Style = style;
            }
            #endregion

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Excel (*.xls)|*.xls|所有檔案 (*.*)|*.*";
            string fileName = "設備使用狀況統計報表";
            saveFile.FileName = fileName;

            try
            {
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    book.Save(saveFile.FileName);
                    MsgBox.Show("設備使用狀況統計報表列印完成!");
                    this.Close();
                    Process.Start(saveFile.FileName);
                }
                else
                {
                    FISCA.Presentation.Controls.MsgBox.Show("檔案未儲存");
                    return;
                }
            }
            catch
            {
                MsgBox.Show("檔案儲存錯誤,請檢查檔案是否開啟中!!");
            }

        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
