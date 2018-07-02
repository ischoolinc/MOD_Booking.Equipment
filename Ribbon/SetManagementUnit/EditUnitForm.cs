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

            AccessHelper access = new AccessHelper();
            _mode = mode; 

            if (mode == "新增")
            {

            }
            else
            {
                _unitID = mode;
                string condition = string.Format("uid = {0}", _unitID);
                unitNameTbx.Text =  access.Select<UDT.EquipmentUnit>(condition)[0].Name;
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
                string sql = string.Format(@"
INSERT INTO $ischool.booking.equip_unit(
    name
    , create_time
    , created_by
)
VALUES(
    '{0}'
    , '{1}'
    , '{2}'
)
                ", unitNameTbx.Text,DateTime.Now.ToShortDateString(),Actor.Account);

                try
                {
                    up.Execute(sql);
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
                string sql = string.Format(@"
UPDATE 
    $ischool.booking.equip_unit
SET
    name = '{0}'
    , create_time = '{1}'
    , created_by = '{2}'
WHERE
    uid = {3}
                ",unitNameTbx.Text,DateTime.Now.ToShortDateString(),Actor.Account,_unitID);

                try
                {
                    up.Execute(sql);
                    MsgBox.Show("儲存成功!");

                    this.Close();
                }
                catch(Exception ex)
                {
                    MsgBox.Show(ex.Message);
                }
            }
        }

        private void leaveBtn_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
