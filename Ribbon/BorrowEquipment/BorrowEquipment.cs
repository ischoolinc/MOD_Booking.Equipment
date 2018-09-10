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
using K12.Data;

namespace Ischool.Booking.Equipment.Ribbon.BorrowEquipment
{
    public partial class BorrowEquipment : UserControl, IEquipUserControl
    {
        private String equipID;
        private string _identity;

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

            if (Actor.Instance.isSysAdmin())
            {
                this._identity = GetDescription.Get(typeof(EnumIdentity),EnumIdentity.ModuleAdmin.ToString());
            }
            else if (Actor.Instance.isUnitAdmin())
            {
                this._identity = GetDescription.Get(typeof(EnumIdentity), EnumIdentity.UnitAdmin.ToString());
            }

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
                    string logs = GetLogs().ToString();
                    FISCA.LogAgent.ApplicationLog.Log("設備出借/歸還", "設備出借", logs.ToString());
                    MsgBox.Show("設備出借成功!");
                    SetVisible(false);
                }
                catch(Exception ex)
                {
                    MsgBox.Show(ex.Message);
                }
            }
        }

        private StringBuilder GetLogs()
        {
            TeacherRecord tr = Teacher.SelectByID(Actor.Instance.GetTeacherID());

            StringBuilder logs = new StringBuilder();
            logs.AppendLine(string.Format(@"
{0}「{1}」確認: 
申請人「{2}」申請設備「{3}」出借。　
設備基本資料:
設備名稱「{3}」 
設備類別「{4}」
廠牌「{5}」 
型號「{6}」 
放置位置「{7}」 
申請資料:
申請人「{2}」 
申請時間「{8}」
預約使用時間「{9}」~「{10}」
申請事由「{11}」
借出時間「{12}」
            ", this._identity, tr.Name, tbxApplicant.Text, tbxEquipName.Text,tbxCategory.Text,tbxCompany.Text,tbxModel.Text,tbxPlace.Text,tbxApplyTime.Text,tbxStarTime.Text,tbxEndTime.Text,tbxReason.Text, lbTimeNow.Text));


            return logs;
        }
    }
}
