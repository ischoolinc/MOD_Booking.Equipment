namespace Ischool.Booking.Equipment
{
    partial class BorrowEquipmentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.lbIdentity = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.tbxPropertyNo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lberror = new DevComponents.DotNetBar.LabelX();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
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
            // BorrowEquipmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 578);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lberror);
            this.Controls.Add(this.tbxPropertyNo);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.lbIdentity);
            this.Controls.Add(this.labelX1);
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
    }
}