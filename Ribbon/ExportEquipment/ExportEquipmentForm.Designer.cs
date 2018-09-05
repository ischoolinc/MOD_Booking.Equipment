namespace Ischool.Booking.Equipment
{
    partial class ExportEquipmentForm
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("設備系統編號");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("設備名稱");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("類別");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("財產編號");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("廠牌");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("型號");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("設備狀態");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("未取用解除預約時間(分)");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("放置位置");
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("管理單位系統編號");
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("管理單位名稱");
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("建立日期");
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem("建立者帳號");
            this.wizard1 = new DevComponents.DotNetBar.Wizard();
            this.wizardPage1 = new DevComponents.DotNetBar.WizardPage();
            this.cbxAll = new System.Windows.Forms.CheckBox();
            this.listViewEx1 = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.wizard1.SuspendLayout();
            this.wizardPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizard1
            // 
            this.wizard1.BackColor = System.Drawing.Color.Transparent;
            this.wizard1.BackgroundImage = global::Ischool.Booking.Equipment.Properties.Resources.wizard1_BackgroundImage;
            this.wizard1.ButtonStyle = DevComponents.DotNetBar.eWizardStyle.Office2007;
            this.wizard1.CancelButtonText = "關閉";
            this.wizard1.Cursor = System.Windows.Forms.Cursors.Default;
            this.wizard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizard1.FinishButtonTabIndex = 3;
            this.wizard1.FinishButtonText = "匯出";
            // 
            // 
            // 
            this.wizard1.FooterStyle.BackColor = System.Drawing.Color.Transparent;
            this.wizard1.FooterStyle.BackColorGradientAngle = 90;
            this.wizard1.FooterStyle.BorderBottomWidth = 1;
            this.wizard1.FooterStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.wizard1.FooterStyle.BorderLeftWidth = 1;
            this.wizard1.FooterStyle.BorderRightWidth = 1;
            this.wizard1.FooterStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Etched;
            this.wizard1.FooterStyle.BorderTopColor = System.Drawing.SystemColors.Control;
            this.wizard1.FooterStyle.BorderTopWidth = 1;
            this.wizard1.FooterStyle.Class = "";
            this.wizard1.FooterStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.wizard1.FooterStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.wizard1.FooterStyle.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.wizard1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(57)))), ((int)(((byte)(129)))));
            this.wizard1.HeaderCaptionFont = new System.Drawing.Font("微軟正黑體", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizard1.HeaderDescriptionFont = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.wizard1.HeaderDescriptionIndent = 16;
            // 
            // 
            // 
            this.wizard1.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.wizard1.HeaderStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(241)))), ((int)(((byte)(254)))));
            this.wizard1.HeaderStyle.BackColorGradientAngle = 90;
            this.wizard1.HeaderStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.wizard1.HeaderStyle.BorderBottomColor = System.Drawing.Color.FromArgb(((int)(((byte)(121)))), ((int)(((byte)(157)))), ((int)(((byte)(182)))));
            this.wizard1.HeaderStyle.BorderBottomWidth = 1;
            this.wizard1.HeaderStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.wizard1.HeaderStyle.BorderLeftWidth = 1;
            this.wizard1.HeaderStyle.BorderRightWidth = 1;
            this.wizard1.HeaderStyle.BorderTopWidth = 1;
            this.wizard1.HeaderStyle.Class = "";
            this.wizard1.HeaderStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.wizard1.HeaderStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.wizard1.HeaderStyle.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.wizard1.HelpButtonVisible = false;
            this.wizard1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.wizard1.Location = new System.Drawing.Point(0, 0);
            this.wizard1.Name = "wizard1";
            this.wizard1.Size = new System.Drawing.Size(500, 403);
            this.wizard1.TabIndex = 0;
            this.wizard1.WizardPages.AddRange(new DevComponents.DotNetBar.WizardPage[] {
            this.wizardPage1});
            this.wizard1.FinishButtonClick += new System.ComponentModel.CancelEventHandler(this.wizard1_FinishButtonClick);
            this.wizard1.CancelButtonClick += new System.ComponentModel.CancelEventHandler(this.wizard1_CancelButtonClick);
            // 
            // wizardPage1
            // 
            this.wizardPage1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wizardPage1.AntiAlias = false;
            this.wizardPage1.BackButtonVisible = DevComponents.DotNetBar.eWizardButtonState.False;
            this.wizardPage1.BackColor = System.Drawing.Color.Transparent;
            this.wizardPage1.Controls.Add(this.cbxAll);
            this.wizardPage1.Controls.Add(this.listViewEx1);
            this.wizardPage1.Location = new System.Drawing.Point(7, 72);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.PageDescription = "請選擇匯出欄位";
            this.wizardPage1.PageHeaderImage = global::Ischool.Booking.Equipment.Properties.Resources.Export_Image;
            this.wizardPage1.PageTitle = "匯出設備清單";
            this.wizardPage1.Size = new System.Drawing.Size(486, 273);
            // 
            // 
            // 
            this.wizardPage1.Style.Class = "";
            this.wizardPage1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.wizardPage1.StyleMouseDown.Class = "";
            this.wizardPage1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.wizardPage1.StyleMouseOver.Class = "";
            this.wizardPage1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.wizardPage1.TabIndex = 7;
            // 
            // cbxAll
            // 
            this.cbxAll.AutoSize = true;
            this.cbxAll.Checked = true;
            this.cbxAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxAll.Location = new System.Drawing.Point(5, 3);
            this.cbxAll.Name = "cbxAll";
            this.cbxAll.Size = new System.Drawing.Size(79, 21);
            this.cbxAll.TabIndex = 1;
            this.cbxAll.Text = "選擇全部";
            this.cbxAll.UseVisualStyleBackColor = true;
            this.cbxAll.CheckedChanged += new System.EventHandler(this.cbxAll_CheckedChanged);
            // 
            // listViewEx1
            // 
            // 
            // 
            // 
            this.listViewEx1.Border.Class = "ListViewBorder";
            this.listViewEx1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listViewEx1.CheckBoxes = true;
            listViewItem1.Checked = true;
            listViewItem1.StateImageIndex = 1;
            listViewItem2.Checked = true;
            listViewItem2.StateImageIndex = 1;
            listViewItem3.Checked = true;
            listViewItem3.StateImageIndex = 1;
            listViewItem4.Checked = true;
            listViewItem4.StateImageIndex = 1;
            listViewItem5.Checked = true;
            listViewItem5.StateImageIndex = 1;
            listViewItem6.Checked = true;
            listViewItem6.StateImageIndex = 1;
            listViewItem7.Checked = true;
            listViewItem7.StateImageIndex = 1;
            listViewItem8.Checked = true;
            listViewItem8.StateImageIndex = 1;
            listViewItem9.Checked = true;
            listViewItem9.StateImageIndex = 1;
            listViewItem10.Checked = true;
            listViewItem10.StateImageIndex = 1;
            listViewItem11.Checked = true;
            listViewItem11.StateImageIndex = 1;
            listViewItem12.Checked = true;
            listViewItem12.StateImageIndex = 1;
            listViewItem13.Checked = true;
            listViewItem13.StateImageIndex = 1;
            this.listViewEx1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13});
            this.listViewEx1.Location = new System.Drawing.Point(5, 30);
            this.listViewEx1.Name = "listViewEx1";
            this.listViewEx1.Size = new System.Drawing.Size(476, 243);
            this.listViewEx1.TabIndex = 0;
            this.listViewEx1.UseCompatibleStateImageBehavior = false;
            this.listViewEx1.View = System.Windows.Forms.View.List;
            // 
            // ExportEquipmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 403);
            this.Controls.Add(this.wizard1);
            this.DoubleBuffered = true;
            this.MaximumSize = new System.Drawing.Size(516, 442);
            this.MinimumSize = new System.Drawing.Size(516, 442);
            this.Name = "ExportEquipmentForm";
            this.Text = "匯出設備清單";
            this.wizard1.ResumeLayout(false);
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Wizard wizard1;
        private DevComponents.DotNetBar.WizardPage wizardPage1;
        private DevComponents.DotNetBar.Controls.ListViewEx listViewEx1;
        private System.Windows.Forms.CheckBox cbxAll;
    }
}