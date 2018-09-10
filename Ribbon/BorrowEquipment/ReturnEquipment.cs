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
using K12.Data;

namespace Ischool.Booking.Equipment.Ribbon.BorrowEquipment
{
    public partial class ReturnEquipment : UserControl, IEquipUserControl
    {
        private String equipID;

        private string _identity;

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

            #region 設備基本資料
            tbxEquipName.Text = listEquipData[0].Name;
            tbxCategory.Text = listEquipData[0].Category;
            tbxCompany.Text = listEquipData[0].Company;
            tbxModel.Text = listEquipData[0].ModelNo;
            tbxPlace.Text = listEquipData[0].Place;
            #endregion

            #region 申請資料
            DataTable dt = DAO.EquipIOHistory.GetBorrowData(equipID);
            if (dt.Rows.Count > 0)
            {
                tbxApplicant.Text = "" + dt.Rows[0]["applicant_name"];
                tbxApplicant.Tag = "" + dt.Rows[0]["uid"]; // 設備借出歷程編號
                tbxApplyTime.Text = "" + dt.Rows[0]["apply_date"];
                tbxStarTime.Text = "" + dt.Rows[0]["start_time"];
                tbxEndTime.Text = "" + dt.Rows[0]["end_time"];
                tbxBorrowTime.Text = "" + dt.Rows[0]["borrow_time"];
                tbxReason.Text = "" + dt.Rows[0]["apply_reason"];

                btnReturn.Enabled = true;
            }
            else
            {
                btnReturn.Enabled = false;
            }
            #endregion

            if (Actor.Instance.isSysAdmin())
            {
                this._identity = GetDescription.Get(typeof(EnumIdentity),EnumIdentity.ModuleAdmin.ToString());
            }
            else if (Actor.Instance.isUnitAdmin())
            {
                this._identity = GetDescription.Get(typeof(EnumIdentity), EnumIdentity.UnitAdmin.ToString());
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
            tbxEndTime.Text = "";
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
                    string logs = GetLogs().ToString();
                    FISCA.LogAgent.ApplicationLog.Log("設備出借/歸還", "設備歸還", logs.ToString());
                    MsgBox.Show("設備歸還成功!");
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
申請人「{2}」申請設備「{3}」歸還。
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
歸還時間「{13}」
            ", this._identity, tr.Name, tbxApplicant.Text, tbxEquipName.Text, tbxCategory.Text, tbxCompany.Text, tbxModel.Text, tbxPlace.Text, tbxApplyTime.Text, tbxStarTime.Text, tbxEndTime.Text, tbxReason.Text, DateTime.Parse(tbxBorrowTime.Text).ToString("yyyy/MM/dd HH:mm"),lbTimeNow.Text));


            return logs;
        }
    }
}
