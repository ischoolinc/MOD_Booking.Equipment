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
    public partial class EditEquipIOForm : BaseForm
    {
        private string _equipID;

        private string _historyID;

        public EditEquipIOForm(string equipID)
        {
            InitializeComponent();

            _equipID = equipID;
        }

        private void EditEquipIOForm_Load(object sender, EventArgs e)
        {
            lbReturnTime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

            AccessHelper access = new AccessHelper();
            List<UDT.Equipment> equip = access.Select<UDT.Equipment>(string.Format("uid = {0}",_equipID));
            tbxEquipName.Text = equip[0].Name;
            tbxCategory.Text = equip[0].Category;
            tbxCompany.Text = equip[0].Company;
            tbxModel.Text = equip[0].ModelNo;

            DAO.EquipIOHistory dao = new DAO.EquipIOHistory(_equipID);
            DataTable dt = dao.GetBorrowData(_equipID);
            tbxApplicant.Text = "" + dt.Rows[0]["applicant_name"];
            tbxApplyTime.Text = DateTime.Parse("" + dt.Rows[0]["apply_date"]).ToShortDateString();
            tbxStarTime.Text = DateTime.Parse("" + dt.Rows[0]["start_time"]).ToString("yyyy/MM/dd HH:mm");
            tbxEndTime.Text = DateTime.Parse("" + dt.Rows[0]["end_time"]).ToString("yyyy/MM/dd HH:mm");
            tbxBorrowTime.Text = DateTime.Parse("" + dt.Rows[0]["borrow_time"]).ToString("yyyy/MM/dd HH:mm");
            tbxReason.Text = "" + dt.Rows[0]["apply_reason"];

            _historyID = "" + dt.Rows[0]["uid"];
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                // 設備歸還
                DAO.EquipIOHistory.ReturnEquip(lbReturnTime.Text,_historyID);
                MsgBox.Show("設備歸還成功!");
                this.Close();
            }
            catch(Exception ex)
            {
                MsgBox.Show(ex.Message);
            }
        }

        private void BtnLeave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
