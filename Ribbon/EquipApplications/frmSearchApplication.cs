using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using FISCA.UDT;
using Aspose.Cells;

namespace Ischool.Booking.Equipment
{
    public partial class frmSearchApplication : BaseForm
    {
        private bool _initFinish = false;
        private Dictionary<string, string> _dicUnitIDByName = new Dictionary<string, string>();

        public frmSearchApplication()
        {
            InitializeComponent();
        }

        private void frmSearchApplication_Load(object sender, EventArgs e)
        {
            // Init Stats Cbx
            cbxApplicationStats.Items.Add("全部");
            cbxApplicationStats.Items.Add("預約");
            cbxApplicationStats.Items.Add("出借中");
            cbxApplicationStats.Items.Add("已歸還");
            cbxApplicationStats.Items.Add("取消");
            cbxApplicationStats.SelectedIndex = 0;
            // Init Unit Cbx
            if (Actor.Instance.isSysAdmin())
            {
                AccessHelper access = new AccessHelper();
                List<UDT.EquipmentUnit>listData = access.Select<UDT.EquipmentUnit>();
                foreach (UDT.EquipmentUnit data in listData)
                {
                    cbxUnit.Items.Add(data.Name);
                    this._dicUnitIDByName.Add(data.Name,data.UID);
                }
                if (cbxUnit.Items.Count > 0)
                {
                    cbxUnit.SelectedIndex = 0;
                }
            }
            else
            {
                List<DAO.UnitInfo> listUnit = Actor.Instance.getUnitAdminUnits();
                foreach (DAO.UnitInfo unit in listUnit)
                {
                    cbxUnit.Items.Add(unit.unitName);
                    this._dicUnitIDByName.Add(unit.unitName, unit.unitID);
                }
                if (cbxUnit.Items.Count > 0)
                {
                    cbxUnit.SelectedIndex = 0;
                }
            }
            

            this._initFinish = true;

            ReloadDataGridView();
        }

        private void cbxUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._initFinish)
            {
                ReloadDataGridView();
            }
        }

        private void cbxApplicationStats_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._initFinish)
            {
                ReloadDataGridView();
            }
        }

        private void ReloadDataGridView()
        {
            if (cbxUnit.SelectedItem != null)
            {
                string unitID = this._dicUnitIDByName[cbxUnit.SelectedItem.ToString()];

                dataGridViewX1.Rows.Clear();

                #region condition
                string condition = string.Format("WHERE equip.ref_unit_id = {0}",unitID);
                switch (cbxApplicationStats.SelectedIndex)
                {
                    case 0:
                        condition += "";
                        break;
                    case 1:
                        condition += @"
AND equip_app.is_canceled = false 
AND app_detail.is_canceled = false
AND history.borrow_time IS NULL
AND history.return_time IS NULL
";
                        break;
                    case 2:
                        condition += @"
AND history.borrow_time IS NOT NULL
AND history.return_time IS NULL
";
                        break;
                    case 3:
                        condition += @"
AND history.return_time IS NOT NULL
";
                        break;
                    case 4:
                        condition += @"
AND equip_app.is_canceled = true
OR app_detail.is_canceled = true
";
                        break;
                }
                #endregion

                DataTable dt = DAO.EquipIOHistory.GetApplicationByCondition(condition);


                foreach (DataRow row in dt.Rows)
                {
                    DataGridViewRow dgvrow = new DataGridViewRow();
                    dgvrow.CreateCells(dataGridViewX1);

                    int col = 0;
                    dgvrow.Cells[col++].Value = "" + row["name"];
                    dgvrow.Cells[col++].Value = "" + row["property_no"];
                    dgvrow.Cells[col++].Value = "" + row["stats"];
                    dgvrow.Cells[col++].Value = "" + row["category"];
                    dgvrow.Cells[col++].Value = "" + row["company"];
                    dgvrow.Cells[col++].Value = "" + row["model_no"];
                    dgvrow.Cells[col++].Value = "" + row["applicant_name"];
                    dgvrow.Cells[col++].Value = string.Format("{0} ~ {1}", DateTime.Parse("" + row["start_time"]).ToString("yyyy/MM/dd HH:mm"), DateTime.Parse("" + row["end_time"]).ToString("yyyy/MM/dd HH:mm"));
                    dgvrow.Cells[col++].Value = "" + row["apply_reason"];

                    dataGridViewX1.Rows.Add(dgvrow);
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Workbook wb = new Workbook();
            wb.Worksheets[0].Name = "設備資料";

            //填表頭
            for (int i = 0; i < dataGridViewX1.Columns.Count; i++)
            {
                wb.Worksheets[0].Cells[0, i].PutValue(dataGridViewX1.Columns[i].HeaderText);
            }
            //填資料
            int rowIndex = 1;
            foreach (DataGridViewRow dgvrow in dataGridViewX1.Rows)
            {

                if (rowIndex < 65536)
                {
                    for (int col = 0; col < dataGridViewX1.Columns.Count; col++)
                    {
                        switch (dataGridViewX1.Columns[col].HeaderText)
                        {
                            case "設備名稱":
                                wb.Worksheets[0].Cells[rowIndex, col].PutValue("" + dgvrow.Cells[col].Value);
                                break;
                            case "財產編號":
                                wb.Worksheets[0].Cells[rowIndex, col].PutValue("" + dgvrow.Cells[col].Value);
                                break;
                            case "出借狀態":
                                wb.Worksheets[0].Cells[rowIndex, col].PutValue("" + dgvrow.Cells[col].Value);
                                break;
                            case "設備類別":
                                wb.Worksheets[0].Cells[rowIndex, col].PutValue("" + dgvrow.Cells[col].Value);
                                break;
                            case "廠牌":
                                wb.Worksheets[0].Cells[rowIndex, col].PutValue("" + dgvrow.Cells[col].Value);
                                break;
                            case "型號":
                                wb.Worksheets[0].Cells[rowIndex, col].PutValue("" + dgvrow.Cells[col].Value);
                                break;
                            case "申請人":
                                wb.Worksheets[0].Cells[rowIndex, col].PutValue("" + dgvrow.Cells[col].Value);
                                break;
                            case "申請時間":
                                wb.Worksheets[0].Cells[rowIndex, col].PutValue("" + dgvrow.Cells[col].Value);
                                break;
                            case "申請事由":
                                wb.Worksheets[0].Cells[rowIndex, col].PutValue("" + dgvrow.Cells[col].Value);
                                break;
                            default:
                                break;
                        }
                    }
                }
                rowIndex++;
            }

            #region 存檔
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "設備出借紀錄";
            saveFileDialog.FileName = "設備出借紀錄.xls";
            saveFileDialog.Filter = "Excel (*.xls)|*.xls|所有檔案 (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                DialogResult result = new DialogResult();
                try
                {
                    wb.Save(saveFileDialog.FileName);
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

                //this.Close();
            }
            #endregion
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
