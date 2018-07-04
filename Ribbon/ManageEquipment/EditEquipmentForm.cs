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
    public partial class EditEquipmentForm : BaseForm
    {
        private string _mode;

        private string _unitID;

        private string _equipID;

        /// <summary>
        /// 財產編號清單
        /// </summary>
        private List<string> listPropertyNo;

        public EditEquipmentForm(string mode,string unitID,string equipID)
        {
            InitializeComponent();

            _mode = mode;

            _unitID = unitID;

            _equipID = equipID;
        }

        private void EditEquipmentForm_Load(object sender, EventArgs e)
        {
            lbTime.Text = DateTime.Now.ToShortDateString();
            listPropertyNo = new List<string>();
            cbxStatus.SelectedIndex = 0;

            AccessHelper access = new AccessHelper();
            List<UDT.Equipment> listEquip = access.Select<UDT.Equipment>();
            foreach (UDT.Equipment equip in listEquip)
            {
                if (_equipID != equip.UID) // 在修改的時候避免判斷該財產編號已存在
                {
                    listPropertyNo.Add(equip.PropertyNo);
                }
            }


            if (_mode == "新增")
            {
                
            }
            else if (_mode == "修改")
            {
                
                List<UDT.Equipment>EquipInfo = access.Select<UDT.Equipment>(string.Format("uid = {0}",_equipID));

                tbxName.Text = EquipInfo[0].Name;
                tbxCategory.Text = EquipInfo[0].Category;
                tbxPropertyNo.Text = EquipInfo[0].PropertyNo;
                tbxCompany.Text = EquipInfo[0].Company;
                tbxModelNo.Text = EquipInfo[0].ModelNo;
                cbxStatus.Text = EquipInfo[0].Status;
                tbxDeadLine.Text = "" + EquipInfo[0].DeadLine;
                tbxPlace.Text = EquipInfo[0].Place;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 資料驗證
            if (DataVerify())
            {
                if (_mode == "新增")
                {
                    try
                    {
                        DAO.EquipmentDAO.InsertUnitEquipment(tbxName.Text, tbxCategory.Text,tbxPropertyNo.Text, tbxCompany.Text, tbxModelNo.Text, cbxStatus.Text, tbxDeadLine.Text, tbxPlace.Text, _unitID);
                        MsgBox.Show("儲存成功!");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }

                }
                else if (_mode == "修改")
                {
                    try
                    {
                        DAO.EquipmentDAO.UpdateUnitEquipment(tbxName.Text,tbxCategory.Text,tbxPropertyNo.Text,tbxCompany.Text,tbxModelNo.Text,cbxStatus.Text,tbxDeadLine.Text,tbxPlace.Text,_unitID,_equipID);
                        MsgBox.Show("儲存成功!");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MsgBox.Show(ex.Message);
                    }
                }
            }
        }

        private bool DataVerify()
        {
            if (!CheckTbxName())
            {
                return false;
            }
            else if (!CheckTbxPropertyNo())
            {
                return false;
            }
            else if (!CheckTbxDeadline())
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }


        private void btnLeave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbxName_TextChanged(object sender, EventArgs e)
        {
            CheckTbxName();
        }

        private void tbxPropertyNo_TextChanged(object sender, EventArgs e)
        {
            CheckTbxPropertyNo();
        }

        private void tbxDeadLine_TextChanged(object sender, EventArgs e)
        {
            CheckTbxDeadline();
        }

        public bool CheckTbxName()
        {
            // 設備名稱不能空白
            if (tbxName.Text.Trim() == "")
            {
                lbErrorText.Text = "設備名稱不能空白!";
                lbErrorText.Visible = true;
                btnSave.Enabled = false;
                return false;
            }
            else
            {
                btnSave.Enabled = true;
                lbErrorText.Visible = false;
                return true;
            }
        }

        public bool CheckTbxPropertyNo()
        {
            // 財產編號不能重複、不能空白
            if (tbxPropertyNo.Text.Trim() == "")
            {
                lbErrorText.Text = "財產編號不能空白!";
                lbErrorText.Visible = true;
                btnSave.Enabled = false;
                return false;
            }
            else if (listPropertyNo.Contains(tbxPropertyNo.Text))
            {
                lbErrorText.Text = "財產編號重複!";
                lbErrorText.Visible = true;
                btnSave.Enabled = false;
                return false;
            }
            else
            {
                btnSave.Enabled = true;
                lbErrorText.Visible = false;
                return true;
            }
        }

        public bool CheckTbxDeadline()
        {
            // 未取用取消時間為數值
            int n = 0;
            if (!int.TryParse(tbxDeadLine.Text, out n))
            {
                lbErrorText.Text = "未取用取消時間為數值!";
                lbErrorText.Visible = true;
                btnSave.Enabled = false;
                return false;
            }
            else
            {
                btnSave.Enabled = true;
                lbErrorText.Visible = false;
                return true;
            }
        }
    }
}
