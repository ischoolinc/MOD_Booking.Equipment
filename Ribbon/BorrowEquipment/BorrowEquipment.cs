using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FISCA.UDT;
using FISCA.Presentation.Controls;

namespace Ischool.Booking.Equipment.Ribbon.BorrowEquipment
{
    public partial class BorrowEquipment : UserControl, IEquipUserControl
    {
        private String equipID;

        public BorrowEquipment()
        {
            InitializeComponent();
        }

        public void SetEquipID(string equipID)
        {
            this.equipID = equipID;
            this.loadData();
        }

        public void SetVisible(bool isVisible)
        {
            this.Visible = isVisible;
        }

        private void loadData()
        {
            lbTimeNow.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            ClearTbxData();

            #region 設備基本資料

            AccessHelper access = new AccessHelper();
            List<UDT.Equipment> listEquip = access.Select<UDT.Equipment>(string.Format("uid = {0}", equipID));
            tbxEquipName.Text = listEquip[0].Name;
            tbxCategory.Text = listEquip[0].Category;
            tbxCompany.Text = listEquip[0].Company;
            tbxModel.Text = listEquip[0].ModelNo;
            tbxPlace.Text = listEquip[0].Place;
            #endregion

            #region 申請紀錄

            // 更新逾時申請紀錄
            DAO.EquipIOHistory.UpdateApplicationDetail();

            // 取得設備當天的申請紀錄
            DataTable dt = DAO.EquipIOHistory.GetApplicationByEquipID(equipID);
            if (dt.Rows.Count > 0)
            {
                tbxApplicant.Text = "" + dt.Rows[0]["applicant_name"];
                tbxApplicant.Tag = "" + dt.Rows[0]["uid"]; // 申請紀錄時段明細編號
                tbxApplyTime.Text = "" + dt.Rows[0]["apply_date"];
                tbxStarTime.Text = "" + dt.Rows[0]["start_time"];
                tbxEndTime.Text = "" + dt.Rows[0]["end_time"];
                tbxReason.Text = "" + dt.Rows[0]["apply_reason"];

                btnBorrow.Enabled = true;
            }
            else
            {
                btnBorrow.Enabled = false;
            }

            #endregion

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
            tbxEndTime.Text = "";
            tbxReason.Text = "";
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            DialogResult result = MsgBox.Show(string.Format("確認申請紀錄與{0}此設備是否正確?", tbxEquipName.Text),"提醒",MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DAO.EquipIOHistory.BorrowEquip("" + tbxApplicant.Tag);
                    MsgBox.Show("設備出借成功!");
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
