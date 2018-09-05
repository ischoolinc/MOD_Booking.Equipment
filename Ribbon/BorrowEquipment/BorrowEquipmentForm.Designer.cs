namespace Ischool.Booking.Equipment
{
    partial class BorrowEquipmentForm
    {

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.lbIdentity = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.tbxPropertyNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lberror = new DevComponents.DotNetBar.LabelX();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.returnEquipment1 = new Ischool.Booking.Equipment.Ribbon.BorrowEquipment.ReturnEquipment();
            this.borrowEquipment1 = new Ischool.Booking.Equipment.Ribbon.BorrowEquipment.BorrowEquipment();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.Class = "";
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(12, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(60, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "管理身分";
            // 
            // lbIdentity
            // 
            this.lbIdentity.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lbIdentity.BackgroundStyle.Class = "";
            this.lbIdentity.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbIdentity.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lbIdentity.Location = new System.Drawing.Point(76, 12);
            this.lbIdentity.Name = "lbIdentity";
            this.lbIdentity.Size = new System.Drawing.Size(147, 23);
            this.lbIdentity.TabIndex = 1;
            this.lbIdentity.Text = "labelX2";
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.Class = "";
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("微軟正黑體", 11F, System.Drawing.FontStyle.Bold);
            this.labelX4.Location = new System.Drawing.Point(35, 88);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(75, 25);
            this.labelX4.TabIndex = 4;
            this.labelX4.Text = "財產編號";
            // 
            // tbxPropertyNo
            // 
            // 
            // 
            // 
            this.tbxPropertyNo.Border.Class = "TextBoxBorder";
            this.tbxPropertyNo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbxPropertyNo.Font = new System.Drawing.Font("微軟正黑體", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbxPropertyNo.Location = new System.Drawing.Point(116, 85);
            this.tbxPropertyNo.Name = "tbxPropertyNo";
            this.tbxPropertyNo.Size = new System.Drawing.Size(251, 27);
            this.tbxPropertyNo.TabIndex = 5;
            this.tbxPropertyNo.TextChanged += new System.EventHandler(this.tbxPropertyNo_TextChanged);
            this.tbxPropertyNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxPropertyNo_KeyDown);
            // 
            // lberror
            // 
            this.lberror.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.lberror.BackgroundStyle.Class = "";
            this.lberror.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lberror.ForeColor = System.Drawing.Color.Red;
            this.lberror.Location = new System.Drawing.Point(116, 121);
            this.lberror.Name = "lberror";
            this.lberror.Size = new System.Drawing.Size(251, 23);
            this.lberror.TabIndex = 6;
            this.lberror.Text = "errorText";
            this.lberror.Visible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearch.Enabled = false;
            this.btnSearch.Location = new System.Drawing.Point(373, 85);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(41, 30);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "搜尋";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.Class = "";
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(12, 149);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(470, 23);
            this.labelX2.TabIndex = 9;
            this.labelX2.Text = "-----------------------------------搜尋結果-----------------------------------";
            this.labelX2.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // returnEquipment1
            // 
            this.returnEquipment1.BackColor = System.Drawing.Color.Transparent;
            this.returnEquipment1.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.returnEquipment1.Location = new System.Drawing.Point(12, 170);
            this.returnEquipment1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.returnEquipment1.Name = "returnEquipment1";
            this.returnEquipment1.Size = new System.Drawing.Size(471, 508);
            this.returnEquipment1.TabIndex = 11;
            this.returnEquipment1.Visible = false;
            // 
            // borrowEquipment1
            // 
            this.borrowEquipment1.BackColor = System.Drawing.Color.Transparent;
            this.borrowEquipment1.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.borrowEquipment1.Location = new System.Drawing.Point(12, 170);
            this.borrowEquipment1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.borrowEquipment1.Name = "borrowEquipment1";
            this.borrowEquipment1.Size = new System.Drawing.Size(471, 501);
            this.borrowEquipment1.TabIndex = 10;
            this.borrowEquipment1.Visible = false;
            // 
            // BorrowEquipmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 693);
            this.Controls.Add(this.borrowEquipment1);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lberror);
            this.Controls.Add(this.tbxPropertyNo);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.lbIdentity);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.returnEquipment1);
            this.DoubleBuffered = true;
            this.Name = "BorrowEquipmentForm";
            this.Text = "設備出借/歸還";
            this.Load += new System.EventHandler(this.BorrowEquipmentForm_Load);
            this.ResumeLayout(false);

        }
        #endregion
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX lbIdentity;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX tbxPropertyNo;
        private DevComponents.DotNetBar.LabelX lberror;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private DevComponents.DotNetBar.LabelX labelX2;
        private Ribbon.BorrowEquipment.BorrowEquipment borrowEquipment1;
        private Ribbon.BorrowEquipment.ReturnEquipment returnEquipment1;
    }
}