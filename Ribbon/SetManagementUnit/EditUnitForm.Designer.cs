namespace Ischool.Booking.Equipment
{
    partial class EditUnitForm
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
            this.unitNameTbx = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.saveBtn = new DevComponents.DotNetBar.ButtonX();
            this.leaveBtn = new DevComponents.DotNetBar.ButtonX();
            this.errorLb = new DevComponents.DotNetBar.LabelX();
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
            this.labelX1.Location = new System.Drawing.Point(12, 49);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(88, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "管理單位名稱";
            // 
            // unitNameTbx
            // 
            // 
            // 
            // 
            this.unitNameTbx.Border.Class = "TextBoxBorder";
            this.unitNameTbx.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.unitNameTbx.Location = new System.Drawing.Point(125, 48);
            this.unitNameTbx.Name = "unitNameTbx";
            this.unitNameTbx.Size = new System.Drawing.Size(191, 25);
            this.unitNameTbx.TabIndex = 1;
            this.unitNameTbx.TextChanged += new System.EventHandler(this.unitNameTbx_TextChanged);
            // 
            // saveBtn
            // 
            this.saveBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.saveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveBtn.BackColor = System.Drawing.Color.Transparent;
            this.saveBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.saveBtn.Location = new System.Drawing.Point(160, 119);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(75, 23);
            this.saveBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.saveBtn.TabIndex = 2;
            this.saveBtn.Text = "儲存";
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // leaveBtn
            // 
            this.leaveBtn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.leaveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.leaveBtn.BackColor = System.Drawing.Color.Transparent;
            this.leaveBtn.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.leaveBtn.Location = new System.Drawing.Point(241, 119);
            this.leaveBtn.Name = "leaveBtn";
            this.leaveBtn.Size = new System.Drawing.Size(75, 23);
            this.leaveBtn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.leaveBtn.TabIndex = 3;
            this.leaveBtn.Text = "離開";
            this.leaveBtn.Click += new System.EventHandler(this.leaveBtn_Click);
            // 
            // errorLb
            // 
            this.errorLb.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.errorLb.BackgroundStyle.Class = "";
            this.errorLb.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.errorLb.ForeColor = System.Drawing.Color.Red;
            this.errorLb.Location = new System.Drawing.Point(125, 79);
            this.errorLb.Name = "errorLb";
            this.errorLb.Size = new System.Drawing.Size(191, 23);
            this.errorLb.TabIndex = 4;
            this.errorLb.Text = "errorText";
            this.errorLb.Visible = false;
            // 
            // EditUnitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 154);
            this.Controls.Add(this.errorLb);
            this.Controls.Add(this.leaveBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.unitNameTbx);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.Name = "EditUnitForm";
            this.Text = "EditUnitForm";
            this.Load += new System.EventHandler(this.EditUnitForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX unitNameTbx;
        private DevComponents.DotNetBar.ButtonX saveBtn;
        private DevComponents.DotNetBar.ButtonX leaveBtn;
        private DevComponents.DotNetBar.LabelX errorLb;
    }
}