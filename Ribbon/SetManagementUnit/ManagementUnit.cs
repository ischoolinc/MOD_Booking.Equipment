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
    public partial class ManagementUnit : BaseForm
    {
        public ManagementUnit()
        {
            InitializeComponent();
        }

        private void ManagementUnit_Load(object sender, EventArgs e)
        {
            ReloadDataGridview();
        }

        public void ReloadDataGridview()
        {
            dataGridViewX1.Rows.Clear();

            DataTable dt = DAO.UnitDAO.GetUnitInfo();

            foreach (DataRow row in dt.Rows)
            {
                DataGridViewRow dgvrow = new DataGridViewRow();
                dgvrow.CreateCells(dataGridViewX1);

                int index = 0;
                dgvrow.Cells[index++].Value = "" + row["name"];
                dgvrow.Cells[index++].Value = "" + row["teacher_name"];
                dgvrow.Cells[index++].Value = DateTime.Parse("" + row["create_time"]).ToShortDateString();
                dgvrow.Tag = "" + row["uid"]; // 管理單位系統編號

                dataGridViewX1.Rows.Add(dgvrow);
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            EditUnitForm form = new EditUnitForm("新增");
            form.Text = "新增設備管理單位";
            form.FormClosed += delegate 
            {
                ReloadDataGridview();
            };
            form.ShowDialog();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.SelectedRows[0].Index > -1)
            {
                string unitID = "" + dataGridViewX1.SelectedRows[0].Tag;

                EditUnitForm form = new EditUnitForm(unitID);
                form.Text = "修改設備管理單位";
                form.FormClosed += delegate
                {
                    ReloadDataGridview();
                };
                form.ShowDialog();
            }

        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            string unitID = "";
            int row = dataGridViewX1.SelectedRows[0].Index;
            if (row > -1)
            {
                string unitName = "" + dataGridViewX1.Rows[row].Cells[0].Value;
                DialogResult result = MsgBox.Show(string.Format("是否確認刪除{0}此管理單位! \n 該管理單位設備將一併刪除。", unitName),"警告",MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    unitID = "" + dataGridViewX1.SelectedRows[0].Tag;
                    try
                    {
                        DAO.UnitDAO.DeleteUnitInfo(unitID);
                        MsgBox.Show("資料刪除成功!");
                        ReloadDataGridview();
                    }
                    catch(Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }
                }
                
            }
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
