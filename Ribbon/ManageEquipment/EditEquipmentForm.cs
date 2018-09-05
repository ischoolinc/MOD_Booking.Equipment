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
                cbxCategory.Text = EquipInfo[0].Category;
                tbxPropertyNo.Text = EquipInfo[0].PropertyNo;
                tbxCompany.Text = EquipInfo[0].Company;
                tbxModelNo.Text = EquipInfo[0].ModelNo;
                cbxStatus.Text = EquipInfo[0].Status;
                tbxDeadLine.Text = "" + EquipInfo[0].DeadLine;
                tbxPlace.Text = EquipInfo[0].Place;
            }

            // Init CbxCategory Items
            DataTable dt = DAO.Equipment.GetCategory();
            foreach (DataRow row in dt.Rows)
            {
                cbxCategory.Items.Add("" + row["category"]);
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
                        DAO.Equipment.InsertUnitEquipment(tbxName.Text.Trim(), cbxCategory.Text.Trim(),tbxPropertyNo.Text.Trim(), tbxCompany.Text.Trim(), 
                            tbxModelNo.Text.Trim(), cbxStatus.Text.Trim(), tbxDeadLine.Text.Trim(), tbxPlace.Text.Trim(), _unitID);
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
                        DAO.Equipment.UpdateUnitEquipment(tbxName.Text.Trim(), cbxCategory.Text.Trim(),tbxPropertyNo.Text.Trim(),tbxCompany.Text.Trim(),
                            tbxModelNo.Text.Trim(),cbxStatus.Text.Trim(),tbxDeadLine.Text.Trim(),tbxPlace.Text.Trim(),_unitID,_equipID);
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
            if (TbxName_Validate() && TbxPropertyNo_Validate() && TbxDeadline_Validate())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void tbxName_TextChanged(object sender, EventArgs e)
        {
            TbxName_Validate();
        }

        private void tbxPropertyNo_TextChanged(object sender, EventArgs e)
        {
            TbxPropertyNo_Validate();
        }

        private void tbxDeadLine_TextChanged(object sender, EventArgs e)
        {
            TbxDeadline_Validate();
        }

        private bool TbxName_Validate()
        {
            // 設備名稱不能空白
            if (string.IsNullOrEmpty(tbxName.Text.Trim()))
            {
                errorProvider1.SetError(tbxName, "設備名稱不能空白!");
                btnSave.Enabled = false;
                return false;
            }
            else
            {
                errorProvider1.SetError(tbxName,null);
                btnSave.Enabled = true;
                return true;
            }
        }

        private bool TbxPropertyNo_Validate()
        {
            // 財產編號不能重複、不能空白
            if (string.IsNullOrEmpty(tbxPropertyNo.Text.Trim()))
            {
                errorProvider1.SetError(tbxPropertyNo, "財產編號不能空白!");
                btnSave.Enabled = false;
                return false;
            }
            else if (listPropertyNo.Contains(tbxPropertyNo.Text))
            {
                errorProvider1.SetError(tbxPropertyNo, "財產編號重複!");
                btnSave.Enabled = false;
                return false;
            }
            else
            {
                errorProvider1.SetError(tbxPropertyNo,null);
                btnSave.Enabled = true;
                return true;
            }
        }

        private bool TbxDeadline_Validate()
        {
            // 未取用取消時間為數值
            int n = 0;
            if (!int.TryParse(tbxDeadLine.Text, out n))
            {
                errorProvider1.SetError(tbxDeadLine, "未取用解除預約時間(分)為數值!");
                btnSave.Enabled = false;
                return false;
            }
            else
            {
                errorProvider1.SetError(tbxDeadLine,null);
                btnSave.Enabled = true;
                return true;
            }
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
