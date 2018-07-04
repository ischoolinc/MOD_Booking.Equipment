using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using FISCA.Data;
using K12.Data;

namespace Ischool.Booking.Equipment
{
    public partial class AddUnitAdmin : BaseForm
    {
        /// <summary>
        /// 管理單位系統編號
        /// </summary>
        string _unitID;

        public AddUnitAdmin(string unitID)
        {
            InitializeComponent();

            _unitID = unitID;
            ReloadDataGridView();
        }

        public void ReloadDataGridView()
        {
            dataGridViewX1.Rows.Clear();

            DataTable dt = DAO.UnitAdminDAO.GetTeacher();

            foreach (DataRow row in dt.Rows)
            {
                DataGridViewRow dgvrow = new DataGridViewRow();
                dgvrow.CreateCells(dataGridViewX1);

                int index = 0;

                string gender = "";
                switch ("" + row["gender"])
                {
                    case "0":
                        gender = "女";
                        break;
                    case "1":
                        gender = "男";
                        break;
                    default:
                        gender = "";
                        break;
                }

                dgvrow.Cells[index++].Value = "" + row["teacher_name"];
                dgvrow.Cells[index++].Value = "" + row["nickname"];
                dgvrow.Cells[index++].Value = gender;
                dgvrow.Cells[index++].Value = "" + row["st_login_name"];
                dgvrow.Cells[index++].Value = "" + row["dept"];
                dgvrow.Cells[index++].Value = "指定";
                dgvrow.Tag = "" + row["id"]; // 老師系統編號

                dataGridViewX1.Rows.Add(dgvrow);

            }
        }

        private void searchTbx_TextChanged(object sender, EventArgs e)
        {
            if (searchTbx.Text == "")
            {
                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    row.Visible = true;
                }
            }
            else
            {
                foreach (DataGridViewRow row in dataGridViewX1.Rows)
                {
                    if (row.Cells[0].Value != null)
                    {
                        row.Visible = (row.Cells[0].Value.ToString().IndexOf(searchTbx.Text) > -1);
                    }
                }
            }
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex ==5)
            {
                string teacherName = "" + dataGridViewX1.Rows[e.RowIndex].Cells[0].Value;
                string teacherAccount = "" + dataGridViewX1.Rows[e.RowIndex].Cells[3].Value;

                if (teacherAccount == "")
                {
                    MsgBox.Show(string.Format("{0}老師沒有登入帳號，\n無法設定為單位管理員! ", teacherName));
                    return;
                }
                DialogResult result = MsgBox.Show(string.Format("確定是否將{0}老師指定為單位管理員?", teacherName),"提醒",MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    string unitID = _unitID;
                    string teacherID = "" + dataGridViewX1.Rows[e.RowIndex].Tag;
                    string loginID = Actor.GetLoginIDByAccount(teacherAccount);
                    string createTime = DateTime.Now.ToShortDateString();
                    string createdBy = Actor.Account;

                    try
                    {
                        // 新增單位管理員
                        DAO.UnitAdminDAO.AssignUnitAdmin(unitID, teacherID, teacherAccount, loginID, createTime, createdBy);

                        MsgBox.Show("儲存成功!");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch(Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }

                }
            }
        }
    }
}
