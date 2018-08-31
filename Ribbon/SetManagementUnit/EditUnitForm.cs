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
using K12.Data;

namespace Ischool.Booking.Equipment
{
    public partial class EditUnitForm : BaseForm
    {
        List<string> _unitNameList = new List<string>();
        string _mode;
        string _unitID;

        public EditUnitForm(string mode)
        {
            InitializeComponent();
            _mode = mode; 
        }

        private void EditUnitForm_Load(object sender, EventArgs e)
        {
            AccessHelper access = new AccessHelper();

            if (_mode == "新增")
            {
                //畫面不用初始化
            }
            else
            {
                _unitID = _mode;
                string condition = string.Format("uid = {0}", _unitID);
                unitNameTbx.Text = access.Select<UDT.EquipmentUnit>(condition)[0].Name;
                unitNameTbx.Tag = _unitID;
            }

            // 取得所有管理單位資料
            List<UDT.EquipmentUnit> unitList = access.Select<UDT.EquipmentUnit>();
            foreach (UDT.EquipmentUnit unit in unitList)
            {
                _unitNameList.Add(unit.Name);
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            UpdateHelper up = new UpdateHelper();

            if (_mode == "新增")
            {
                try
                {
                    DAO.UnitDAO.InsertUnitInfo(unitNameTbx.Text,DateTime.Now.ToShortDateString(),Actor.UserAccount);
                    MsgBox.Show("儲存成功!");
                    this.Close();
                }
                catch(Exception ex)
                {
                    MsgBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    DAO.UnitDAO.UpdateUnitInfo(unitNameTbx.Text,DateTime.Now.ToShortDateString(),Actor.UserAccount, _unitID);       
                    MsgBox.Show("儲存成功!");
                    this.Close();
                }
                catch(Exception ex)
                {
                    MsgBox.Show(ex.Message);
                }
            }
        }

        private void unitNameTbx_TextChanged(object sender, EventArgs e)
        {
            if (_unitNameList.Contains(unitNameTbx.Text))
            {
                errorLb.Visible = true;
                errorLb.Text = "管理單位名稱重複!";
                saveBtn.Enabled = false;
            }
            else if (unitNameTbx.Text.Trim() == "")
            {
                errorLb.Visible = true;
                errorLb.Text = "管理單位名稱不能空白!";
                saveBtn.Enabled = false;
            }
            else
            {
                errorLb.Visible = false;
                saveBtn.Enabled = true;
            }
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
