using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;

namespace Ischool.Booking.Equipment
{
    public partial class ApplicationForm : BaseForm
    {
        private string _equipID;

        public ApplicationForm(string equip)
        {
            InitializeComponent();

            _equipID = equip;
        }

        private void ApplicationForm_Load(object sender, EventArgs e)
        {
            // 1.更新申請紀錄 : 如逾時更新為取消
            try
            {
                DAO.EquipIOHistory.UpdateApplicationDetail();
            }
            catch(Exception ex)
            {
                MsgBox.Show(ex.Message);
            }

            // 2.載入設備當天申請紀錄
            DataTable dt = DAO.EquipIOHistory.GetApplicationByEquipID(_equipID);

            int n = 0;
            foreach (DataRow row in dt.Rows)
            {
                DataGridViewRow dgvrow = new DataGridViewRow();
                dgvrow.CreateCells(dataGridViewX1);

                

                int index = 0;
                dgvrow.Cells[index++].Value = "" + row["applicant_name"];
                dgvrow.Cells[index++].Value = DateTime.Parse("" + row["apply_date"]).ToShortDateString();
                dgvrow.Cells[index++].Value = "" + row["apply_reason"];
                dgvrow.Cells[index++].Value = DateTime.Parse("" + row["start_time"]).ToString("yyyy/MM/dd HH:mm");
                dgvrow.Cells[index++].Value = DateTime.Parse("" + row["end_time"]).ToString("yyyy/MM/dd HH:mm");
                #region 判斷是否取消
                string status = "";
                // 判斷是否取消
                if ("" + row["is_canceled"] == "true")
                {
                    status = "取消";
                    dgvrow.Cells[index].Style.BackColor = Color.LightPink;
                }
                else if ("" + row["is_canceled"] == "false" && n == 0)
                {
                    status = "可以領取";
                    dgvrow.Cells[index].Style.BackColor = Color.LightGreen;
                    n++;
                }
                #endregion
                dgvrow.Cells[index].Value = status;
                dgvrow.Tag = "" + row["uid"]; // 設備預約時段明細系統編號

                dataGridViewX1.Rows.Add(dgvrow);
                
            }
        }

        private void dataGridViewX1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {

            }
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                string status = "" + dataGridViewX1.Rows[e.RowIndex].Cells[5].Value;

                if (status == "可以領取")
                {
                    BtnBorrow.Enabled = true;
                }
                else
                {
                    BtnBorrow.Enabled = false;
                }
            }
        }

        private void BtnBorrow_Click(object sender, EventArgs e)
        {

        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
