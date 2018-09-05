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
using Ischool.Booking.Equipment.Ribbon.BorrowEquipment;

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

        /// <summary>
        /// UserControl List
        /// </summary>
        List<IEquipUserControl> ucEquips;

        public BorrowEquipmentForm()
        {
            InitializeComponent();
        }

        private void BorrowEquipmentForm_Load(object sender, EventArgs e)
        {
            ucEquips = new List<IEquipUserControl>();
            ucEquips.Add(this.borrowEquipment1);
            ucEquips.Add(this.returnEquipment1);

            dicAllEquipments = DAO.Equipment.GetEquipments();

            if (actor.isSysAdmin())
            {
                lbIdentity.Text = "設備預約模組管理者";

                dicEquipments = DAO.Equipment.GetEquipments();
            }
            else if (actor.isUnitAdmin())
            {
                lbIdentity.Text = "單位管理員";
                List<string> unitIDs = new List<string>();
                foreach (DAO.UnitInfo unit in actor.getUnitAdminUnits())
                {
                    unitIDs.Add(unit.unitID);
                }

                dicEquipments = DAO.Equipment.GetEquipmentsByUnitIDs(unitIDs);
            }
        }

        private void tbxPropertyNo_TextChanged(object sender, EventArgs e)
        {
            this.hideAllUserControl();
            if (tbxPropertyNo.Text == "")
            {
                lberror.Visible = false;
                btnSearch.Enabled = false;
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
            //string equipName = dicEquipments[tbxPropertyNo.Text].Name;

            // 2. 判斷設備是否出借中
            DAO.EquipIOHistory dao = new DAO.EquipIOHistory(equipID);

            this.hideAllUserControl();
            IEquipUserControl uc = this.getUserCotnrol(dao.IsBorrow());
            uc.SetVisible(true);
            uc.SetEquipID(equipID);
        }

        private IEquipUserControl getUserCotnrol(bool isBorrowed)
        {
            if (isBorrowed)
            {
                return this.returnEquipment1;
            }
            else
            {
                return this.borrowEquipment1;
            }
        }

        private void hideAllUserControl()
        {
            foreach(IEquipUserControl uc in this.ucEquips)
            {
                uc.SetVisible(false);
            }
        }

        private void tbxPropertyNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (btnSearch.Enabled)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    btnSearch_Click(sender,e);
                }
            }
        }
    }
}
