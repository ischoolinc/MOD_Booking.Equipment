using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.UDT;
using FISCA.Presentation.Controls;

namespace Ischool.Booking.Equipment.Ribbon.BorrowEquipment
{
    public partial class ReturnEquipment : UserControl, IEquipUserControl
    {
        private String equipID;

        public ReturnEquipment()
        {
            InitializeComponent();
        }

        public void SetEquipID(string equipID)
        {
            this.equipID = equipID;

            LoadData();
        }

        public void SetVisible(bool isVisible)
        {
            this.Visible = isVisible;
        }

        private void LoadData()
        {
            lbTimeNow.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            ClearTbxData();
            AccessHelper access = new AccessHelper();
            List<UDT.Equipment> listEquipData = access.Select<UDT.Equipment>(string.Format("uid = {0}", equipID));
            // 設備基本資料
            tbxEquipName.Text = listEquipData[0].Name;
            tbxCategory.Text = listEquipData[0].Category;
            tbxCompany.Text = listEquipData[0].Company;
            tbxModel.Text = listEquipData[0].ModelNo;
            // 出借紀錄
            DataTable dt = DAO.EquipIOHistory.GetBorrowData(equipID);
            if (dt.Rows.Count > 0)
            {
                tbxApplicant.Text = "" + dt.Rows[0]["applicant_name"];
                tbxApplicant.Tag = "" + dt.Rows[0]["uid"]; // 設備借出歷程編號
                tbxApplyTime.Text = "" + dt.Rows[0]["apply_date"];
                tbxStarTime.Text = "" + dt.Rows[0]["start_time"];
                tbxendTime.Text = "" + dt.Rows[0]["end_time"];
                tbxBorrowTime.Text = "" + dt.Rows[0]["borrow_time"];
                tbxReason.Text = "" + dt.Rows[0]["apply_reason"];

                btnReturn.Enabled = true;
            }
            else
            {
                btnReturn.Enabled = false;
            }
        }

        private void ClearTbxData()
        {
            tbxEquipName.Text = "";
            tbxCategory.Text = "";
            tbxCompany.Text = "";
            tbxModel.Text = "";
            tbxApplicant.Text = "";
            tbxApplyTime.Text = "";
            tbxStarTime.Text = "";
            tbxendTime.Text = "";
            tbxBorrowTime.Text = "";
            tbxReason.Text = "";
        }


        private void btnReturn_Click(object sender, EventArgs e)
        {
            DialogResult result = MsgBox.Show(string.Format("確認出借紀錄與{0}此設備是否正確?", tbxEquipName.Text),"提醒",MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DAO.EquipIOHistory.ReturnEquip("" + tbxApplicant.Tag);
                    MsgBox.Show("設備歸還成功!");
                    SetVisible(false);
                }
                catch(Exception ex)
                {
                    MsgBox.Show(ex.Message);
                }
            }
        }
    }
}
