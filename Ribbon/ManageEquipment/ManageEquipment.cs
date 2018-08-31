using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using FISCA.UDT;

namespace Ischool.Booking.Equipment
{
    public partial class ManageEquipment : BaseForm
    {
        Actor actor = Actor.Instance;

        Dictionary<string, string> dicUnit = new Dictionary<string, string>();

        public ManageEquipment()
        {
            InitializeComponent();
        }

        private void ManageEquipment_Load(object sender, EventArgs e)
        {
            // 1.確認使用者身分
            if (actor.isSysAdmin())
            {
                lbIdentity.Text = "設備預約模組管理者";

                // 2.載入管理的單位
                AccessHelper access = new AccessHelper();
                List<UDT.EquipmentUnit> listUnit = access.Select<UDT.EquipmentUnit>();
                foreach (UDT.EquipmentUnit unit in listUnit)
                {
                    cbxUnit.Items.Add(unit.Name);
                    dicUnit.Add(unit.Name,unit.UID);
                }
            }
            else if (actor.isUnitAdmin())
            {
                lbIdentity.Text = "單位管理員";

                // 2.載入管理的單位
                List<DAO.UnitInfo> listUnit = actor.getUnitAdminUnits();
                foreach (DAO.UnitInfo unit in listUnit)
                {
                    cbxUnit.Items.Add(unit.unitName);
                    dicUnit.Add(unit.unitName,unit.unitID);
                }
            }
            if (cbxUnit.Items.Count > 0)
            {
                cbxUnit.SelectedIndex = 0;
            }
            
        }

        private void ReloadDataGridView(string unitID)
        {
            dataGridViewX1.Rows.Clear();

            DataTable dt = DAO.EquipmentDAO.GetUnitEquipment(unitID);

            foreach (DataRow row in dt.Rows)
            {
                DataGridViewRow dgvrow = new DataGridViewRow();
                dgvrow.CreateCells(dataGridViewX1);

                int index = 0;
                dgvrow.Cells[index++].Value = "" + row["name"];
                dgvrow.Cells[index++].Value = "" + row["category"];
                dgvrow.Cells[index++].Value = "" + row["property_no"];
                dgvrow.Cells[index++].Value = "" + row["company"];
                dgvrow.Cells[index++].Value = "" + row["model_no"];
                dgvrow.Cells[index++].Value = "" + row["status"];
                dgvrow.Cells[index++].Value = "" + row["deadline"];
                dgvrow.Cells[index++].Value = "" + row["place"];
                dgvrow.Cells[index++].Value = "" + row["created_by"];
                dgvrow.Cells[index++].Value = DateTime.Parse("" + row["create_time"]).ToShortDateString();

                dgvrow.Tag = "" + row["uid"]; //設備系統編號

                dataGridViewX1.Rows.Add(dgvrow);
            }

        }

        private void cbxUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadDataGridView(dicUnit[cbxUnit.SelectedItem.ToString()]);
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            EditEquipmentForm form = new EditEquipmentForm("新增", dicUnit[cbxUnit.Text],"");
            form.Text = "新增設備";
            form.FormClosed += delegate 
            {
                ReloadDataGridView(dicUnit[cbxUnit.Text]);
            };
            form.ShowDialog();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            string equipID = "" + dataGridViewX1.SelectedRows[0].Tag;
            EditEquipmentForm form = new EditEquipmentForm("修改", dicUnit[cbxUnit.Text], equipID);
            form.Text = "修改設備";
            form.FormClosed += delegate
            {
                ReloadDataGridView(dicUnit[cbxUnit.Text]);
            };
            form.ShowDialog();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            int index = dataGridViewX1.SelectedRows[0].Index;
            
            if (index > -1)
            {
                string equipName = "" + dataGridViewX1.Rows[index].Cells[0].Value;
                string equipID = "" + dataGridViewX1.Rows[index].Tag;
                DialogResult result = MsgBox.Show(string.Format("確定是否刪除{0}此設備資料", equipName),"警告",MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        DAO.EquipmentDAO.DELETEUnitEquipment(equipID);
                        MsgBox.Show("刪除成功!");
                        ReloadDataGridView(dicUnit[cbxUnit.Text]);
                    }
                    catch(Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }
                }
            }
            string sql = string.Format(@"
");
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
