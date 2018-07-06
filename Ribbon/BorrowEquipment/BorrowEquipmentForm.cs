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

namespace Ischool.Booking.Equipment
{
    public partial class BorrowEquipmentForm : BaseForm
    {
        Actor actor = new Actor();
        // Init dao
        DAO.UnitDAO dao = new DAO.UnitDAO();

        /// <summary>
        ///  使用者可管理的設備清單，Key : 財產編號 
        /// </summary>
        Dictionary<string, UDT.Equipment> dicEquipments;

        /// <summary>
        /// 所有設備，Key:財產編號
        /// </summary>
        Dictionary<string, UDT.Equipment> dicAllEquipments;

        public BorrowEquipmentForm()
        {
            InitializeComponent();
        }

        private void BorrowEquipmentForm_Load(object sender, EventArgs e)
        {
            dicAllEquipments = DAO.EquipmentDAO.GetEquipments();

            if (actor.isSysAdmin())
            {
                lbIdentity.Text = "設備預約模組管理者";

                //foreach (string unitName in DAO.UnitDAO.GetUnits().Keys)
                //{
                //    cbxUnit.Items.Add(unitName);
                //}
                //if (cbxUnit.Items.Count > 0)
                //{
                //    cbxUnit.SelectedIndex = 0;
                //}

                dicEquipments = DAO.EquipmentDAO.GetEquipments();
            }
            else if (actor.isUnitAdmin())
            {
                lbIdentity.Text = "單位管理員";
                List<string> unitIDs = new List<string>();
                foreach (DAO.UnitInfo unit in actor.getUnitAdminUnits())
                {
                    //cbxUnit.Items.Add(unit.unitName);
                    unitIDs.Add(unit.unitID);
                }
                //if (cbxUnit.Items.Count > 0)
                //{
                //    cbxUnit.SelectedIndex = 0;
                //}

                dicEquipments = DAO.EquipmentDAO.GetEquipmentsByUnitIDs(unitIDs);
            }
        }

        private void tbxPropertyNo_TextChanged(object sender, EventArgs e)
        {
            
            if (tbxPropertyNo.Text == "")
            {
                lberror.Visible = false;
            }
            else
            {
                if (!dicEquipments.ContainsKey(tbxPropertyNo.Text) && dicAllEquipments.ContainsKey(tbxPropertyNo.Text))
                {
                    lberror.Visible = true;
                    lberror.Text = "您無法出借此設備";
                    btnSearch.Enabled = false;
                }
                else if (!dicAllEquipments.ContainsKey(tbxPropertyNo.Text))
                {
                    lberror.Visible = true;
                    lberror.Text = "此財產編號不存在";
                    btnSearch.Enabled = false;
                }
                else
                {
                    lberror.Visible = false;
                    btnSearch.Enabled = true;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 1.  取得設備ID
            string equipID = dicEquipments[tbxPropertyNo.Text].UID;
            string equipName = dicEquipments[tbxPropertyNo.Text].Name;

            // 2. 判斷設備是否出借中
            DAO.EquipIOHistory dao = new DAO.EquipIOHistory(equipID);
            // b. 設備已出借 : 讀取出借紀錄 
            if (dao.IsBorrow())
            {
                EditEquipIOForm form = new EditEquipIOForm(equipID);
                form.Text = "設備歸還";
                form.ShowDialog();
            }
            // a. 設備已歸還 : 讀取當天設備預約紀錄
            else
            {
                ApplicationForm form = new ApplicationForm(equipID);
                form.Text = string.Format("{1}設備  {0} 申請紀錄", DateTime.Now.ToShortDateString(), equipName);
                form.ShowDialog();
            }
            
        }
    }
}
