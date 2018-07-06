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
using FISCA.UDT;

namespace Ischool.Booking.Equipment
{
    public partial class SetUnitAdmin : BaseForm
    {
        /// <summary>
        /// key : 單位名稱，供ComboBox事件使用
        /// </summary>
        Dictionary<string, UDT.EquipmentUnit> _unitDataDic = new Dictionary<string, UDT.EquipmentUnit>();

        public SetUnitAdmin()
        {
            InitializeComponent();
        }

        private void SetUnitAdmin_Load(object sender, EventArgs e)
        {
            // Init unitCbx
            AccessHelper access = new AccessHelper();
            // 取得所有設備管理單位資料
            List<UDT.EquipmentUnit> unitList = access.Select<UDT.EquipmentUnit>();

            foreach (UDT.EquipmentUnit unit in unitList)
            {
                unitCbx.Items.Add(unit.Name);

                _unitDataDic.Add(unit.Name, unit);
            }
            if (unitCbx.Items.Count > 0)
            {
                unitCbx.SelectedIndex = 0;
            }
            
        }

        public void ReloadDataGridView()
        {
            dataGridViewX1.Rows.Clear();
            string unitID = _unitDataDic[unitCbx.Text].UID;

            DataTable dt = DAO.UnitAdminDAO.GetUnitAdmins(unitID);

            foreach (DataRow row in dt.Rows)
            {
                DataGridViewRow dgvrow = new DataGridViewRow();
                dgvrow.CreateCells(dataGridViewX1);

                int index = 0;
                dgvrow.Cells[index++].Value = "" + row["teacher_name"];
                dgvrow.Cells[index++].Value = "" + row["account"];
                dgvrow.Cells[index++].Value = ("" + row["is_boss"]) == "true" ? "單位主管" : "單位管理員";
                dgvrow.Cells[index++].Value = "" + row["created_name"];
                dgvrow.Cells[index++].Value = DateTime.Parse("" + row["create_time"]).ToShortDateString();
                dgvrow.Tag = "" + row["uid"]; // 單位管理員編號

                dataGridViewX1.Rows.Add(dgvrow);
            }
            
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            string unitID = _unitDataDic[unitCbx.Text].UID;
            AddUnitAdmin form = new AddUnitAdmin(unitID);
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                ReloadDataGridView();
            }
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridViewX1.SelectedRows[0].Index;

            if (rowIndex > -1)
            {
                string teacherName = "" + dataGridViewX1.Rows[rowIndex].Cells[0].Value;

                DialogResult result = MsgBox.Show(string.Format("確定是否刪除{0}老師單位管理員身分?", teacherName), "警告", MessageBoxButtons.YesNo);

                string unitAdminID = "" + dataGridViewX1.SelectedRows[0].Tag;

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        DAO.UnitAdminDAO.DeleteUnitAdmin(unitAdminID);
                        MsgBox.Show("刪除成功!");
                        ReloadDataGridView();
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

        private void unitCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadDataGridView();
        }
    }
}
