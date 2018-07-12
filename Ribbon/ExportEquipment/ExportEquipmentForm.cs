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

namespace Ischool.Booking.Equipment
{
    public partial class ExportEquipmentForm : BaseForm
    {
        public ExportEquipmentForm()
        {
            InitializeComponent();
        }

        private void wizard1_FinishButtonClick(object sender, CancelEventArgs e)
        {
            List<string> exportFieldList = new List<string>();

            #region 取得選取欄位
            foreach (ListViewItem item in listViewEx1.Items)
            {
                if (item.Checked)
                {
                    exportFieldList.Add(item.Text.Trim());
                }
            }
            #endregion

            // 取得資料
            DataTable dt = DAO.EquipmentDAO.GetAllEquipment();

            // 寫入資料
            Workbook report = new Workbook();
            report.Worksheets[0].Name = "設備資料";

            //填表頭
            for (int i = 0; i < exportFieldList.Count; i++)
            {
                report.Worksheets[0].Cells[0, i].PutValue(exportFieldList[i]);
            }
            //填資料
            int rowIndex = 1;
            foreach (DataRow row in dt.Rows)
            {

                if (rowIndex < 65536)
                {
                    for (int col = 0; col < exportFieldList.Count(); col++)
                    {
                        switch (exportFieldList[col])
                        {
                            case "設備系統編號":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["uid"]);
                                break;
                            case "設備名稱":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["name"]);
                                break;
                            case "類別":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["category"]);
                                break;
                            case "財產編號":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["property_no"]);
                                break;
                            case "廠牌":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["company"]);
                                break;
                            case "型號":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["model_no"]);
                                break;
                            case "設備狀態":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["status"]);
                                break;
                            case "未取用解除預約時間(分)":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["deadline"]);
                                break;
                            case "放置位置":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["place"]);
                                break;
                            case "管理單位編號":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["ref_unit_id"]);
                                break;
                            case "管理單位名稱":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["unit_name"]);
                                break;
                            case "建立日期":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["create_time"]);
                                break;
                            case "建立者帳號":
                                report.Worksheets[0].Cells[rowIndex, col].PutValue("" + row["created_by"]);
                                break;
                            default:
                                break;
                        }
                    }
                }
                rowIndex++;
            }
            // 存檔
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "匯出設備清單";
            saveFileDialog.FileName = "匯出設備清單.xls";
            saveFileDialog.Filter = "Excel (*.xls)|*.xls|所有檔案 (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                DialogResult result = new DialogResult();
                try
                {
                    report.Save(saveFileDialog.FileName);
                    result = MsgBox.Show("檔案儲存完成，是否開啟檔案?", "是否開啟", MessageBoxButtons.YesNo);
                }
                catch (Exception ex)
                {
                    MsgBox.Show(ex.Message);
                    return;
                }

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        System.Diagnostics.Process.Start(saveFileDialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show("開啟檔案發生失敗:" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                this.Close();
            }
        }

        private void wizard1_CancelButtonClick(object sender, CancelEventArgs e)
        {
            this.Close();
        }

        private void cbxAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxAll.Checked)
            {
                for (int i = 0; i < listViewEx1.Items.Count; i++)
                {
                    listViewEx1.Items[i].Checked = true;
                }
            }
        }
    }
}
