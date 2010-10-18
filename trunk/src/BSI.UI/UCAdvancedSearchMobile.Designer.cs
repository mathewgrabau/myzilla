namespace MyZilla.UI
{
    partial class UCAdvancedSearchMobile
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtQueryFormat = new System.Windows.Forms.TextBox();
            this.listboxProduct = new System.Windows.Forms.ListBox();
            this.txtWhiteboard = new System.Windows.Forms.TextBox();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.cmbWhiteboard = new System.Windows.Forms.ComboBox();
            this.cmbURL = new System.Windows.Forms.ComboBox();
            this.cmbComment = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listboxOS = new System.Windows.Forms.ListBox();
            this.lblHardware = new System.Windows.Forms.Label();
            this.listboxHardware = new System.Windows.Forms.ListBox();
            this.lblPriority = new System.Windows.Forms.Label();
            this.listboxPriority = new System.Windows.Forms.ListBox();
            this.lblSeverity = new System.Windows.Forms.Label();
            this.listboxSeverity = new System.Windows.Forms.ListBox();
            this.cmbSummary = new System.Windows.Forms.ComboBox();
            this.listboxVersion = new System.Windows.Forms.ListBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.listboxComponent = new System.Windows.Forms.ListBox();
            this.lblComponent = new System.Windows.Forms.Label();
            this.listboxResolution = new System.Windows.Forms.ListBox();
            this.lblResolution = new System.Windows.Forms.Label();
            this.listboxStatus = new System.Windows.Forms.ListBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtGeneralValue = new System.Windows.Forms.TextBox();
            this.cmbGeneralOperator = new System.Windows.Forms.ComboBox();
            this.cmbGeneralField = new System.Windows.Forms.ComboBox();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lblChangedFrom = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtQueryFormat
            // 
            this.txtQueryFormat.Location = new System.Drawing.Point(538, 4);
            this.txtQueryFormat.Name = "txtQueryFormat";
            this.txtQueryFormat.Size = new System.Drawing.Size(100, 20);
            this.txtQueryFormat.TabIndex = 97;
            this.txtQueryFormat.Tag = "query_format";
            this.txtQueryFormat.Text = "query_format";
            this.txtQueryFormat.Visible = false;
            // 
            // listboxProduct
            // 
            this.listboxProduct.FormattingEnabled = true;
            this.listboxProduct.Location = new System.Drawing.Point(8, 34);
            this.listboxProduct.Name = "listboxProduct";
            this.listboxProduct.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listboxProduct.Size = new System.Drawing.Size(120, 95);
            this.listboxProduct.TabIndex = 96;
            this.listboxProduct.Tag = "product";
            this.listboxProduct.SelectedValueChanged += new System.EventHandler(this.listboxProduct_SelectedValueChanged);
            // 
            // txtWhiteboard
            // 
            this.txtWhiteboard.Location = new System.Drawing.Point(356, 69);
            this.txtWhiteboard.Name = "txtWhiteboard";
            this.txtWhiteboard.Size = new System.Drawing.Size(196, 20);
            this.txtWhiteboard.TabIndex = 95;
            this.txtWhiteboard.Tag = "status_whiteboard";
            this.txtWhiteboard.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UCAdvancedSearchMobile_KeyDown);
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(356, 42);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(196, 20);
            this.txtURL.TabIndex = 94;
            this.txtURL.Tag = "bug_file_loc";
            this.txtURL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UCAdvancedSearchMobile_KeyDown);
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(356, 15);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(196, 20);
            this.txtComment.TabIndex = 93;
            this.txtComment.Tag = "long_desc";
            this.txtComment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UCAdvancedSearchMobile_KeyDown);
            // 
            // cmbWhiteboard
            // 
            this.cmbWhiteboard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWhiteboard.FormattingEnabled = true;
            this.cmbWhiteboard.Location = new System.Drawing.Point(105, 69);
            this.cmbWhiteboard.Name = "cmbWhiteboard";
            this.cmbWhiteboard.Size = new System.Drawing.Size(245, 21);
            this.cmbWhiteboard.TabIndex = 92;
            this.cmbWhiteboard.Tag = "status_whiteboard_type";
            // 
            // cmbURL
            // 
            this.cmbURL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbURL.FormattingEnabled = true;
            this.cmbURL.Location = new System.Drawing.Point(105, 42);
            this.cmbURL.Name = "cmbURL";
            this.cmbURL.Size = new System.Drawing.Size(245, 21);
            this.cmbURL.TabIndex = 91;
            this.cmbURL.Tag = "bug_file_loc_type";
            // 
            // cmbComment
            // 
            this.cmbComment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComment.FormattingEnabled = true;
            this.cmbComment.Location = new System.Drawing.Point(105, 15);
            this.cmbComment.Name = "cmbComment";
            this.cmbComment.Size = new System.Drawing.Size(245, 21);
            this.cmbComment.TabIndex = 90;
            this.cmbComment.Tag = "long_desc_type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 89;
            this.label4.Text = "Whiteboard";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 88;
            this.label3.Text = "The URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 87;
            this.label2.Text = "A Comment";
            // 
            // txtSummary
            // 
            this.txtSummary.Location = new System.Drawing.Point(398, 15);
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.Size = new System.Drawing.Size(352, 20);
            this.txtSummary.TabIndex = 86;
            this.txtSummary.Tag = "short_desc";
            this.txtSummary.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UCAdvancedSearchMobile_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(94, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 85;
            this.label1.Text = "OS";
            // 
            // listboxOS
            // 
            this.listboxOS.FormattingEnabled = true;
            this.listboxOS.Location = new System.Drawing.Point(97, 34);
            this.listboxOS.Name = "listboxOS";
            this.listboxOS.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listboxOS.Size = new System.Drawing.Size(81, 108);
            this.listboxOS.TabIndex = 84;
            this.listboxOS.Tag = "op_sys";
            // 
            // lblHardware
            // 
            this.lblHardware.AutoSize = true;
            this.lblHardware.Location = new System.Drawing.Point(7, 16);
            this.lblHardware.Name = "lblHardware";
            this.lblHardware.Size = new System.Drawing.Size(53, 13);
            this.lblHardware.TabIndex = 83;
            this.lblHardware.Text = "Hardware";
            // 
            // listboxHardware
            // 
            this.listboxHardware.FormattingEnabled = true;
            this.listboxHardware.Location = new System.Drawing.Point(10, 34);
            this.listboxHardware.Name = "listboxHardware";
            this.listboxHardware.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listboxHardware.Size = new System.Drawing.Size(77, 108);
            this.listboxHardware.TabIndex = 82;
            this.listboxHardware.Tag = "rep_platform";
            // 
            // lblPriority
            // 
            this.lblPriority.AutoSize = true;
            this.lblPriority.Location = new System.Drawing.Point(287, 18);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(38, 13);
            this.lblPriority.TabIndex = 81;
            this.lblPriority.Text = "Priority";
            // 
            // listboxPriority
            // 
            this.listboxPriority.FormattingEnabled = true;
            this.listboxPriority.Location = new System.Drawing.Point(290, 34);
            this.listboxPriority.Name = "listboxPriority";
            this.listboxPriority.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listboxPriority.Size = new System.Drawing.Size(62, 95);
            this.listboxPriority.TabIndex = 80;
            this.listboxPriority.Tag = "priority";
            // 
            // lblSeverity
            // 
            this.lblSeverity.AutoSize = true;
            this.lblSeverity.Location = new System.Drawing.Point(194, 18);
            this.lblSeverity.Name = "lblSeverity";
            this.lblSeverity.Size = new System.Drawing.Size(45, 13);
            this.lblSeverity.TabIndex = 79;
            this.lblSeverity.Text = "Severity";
            // 
            // listboxSeverity
            // 
            this.listboxSeverity.FormattingEnabled = true;
            this.listboxSeverity.Location = new System.Drawing.Point(197, 34);
            this.listboxSeverity.Name = "listboxSeverity";
            this.listboxSeverity.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listboxSeverity.Size = new System.Drawing.Size(87, 95);
            this.listboxSeverity.TabIndex = 78;
            this.listboxSeverity.Tag = "bug_severity";
            // 
            // cmbSummary
            // 
            this.cmbSummary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSummary.FormattingEnabled = true;
            this.cmbSummary.Location = new System.Drawing.Point(7, 15);
            this.cmbSummary.Name = "cmbSummary";
            this.cmbSummary.Size = new System.Drawing.Size(382, 21);
            this.cmbSummary.TabIndex = 76;
            this.cmbSummary.Tag = "short_desc_type";
            // 
            // listboxVersion
            // 
            this.listboxVersion.FormattingEnabled = true;
            this.listboxVersion.Location = new System.Drawing.Point(260, 34);
            this.listboxVersion.Name = "listboxVersion";
            this.listboxVersion.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listboxVersion.Size = new System.Drawing.Size(118, 95);
            this.listboxVersion.TabIndex = 74;
            this.listboxVersion.Tag = "version";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(257, 18);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(42, 13);
            this.lblVersion.TabIndex = 73;
            this.lblVersion.Text = "Version";
            // 
            // listboxComponent
            // 
            this.listboxComponent.FormattingEnabled = true;
            this.listboxComponent.Location = new System.Drawing.Point(134, 34);
            this.listboxComponent.Name = "listboxComponent";
            this.listboxComponent.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listboxComponent.Size = new System.Drawing.Size(120, 95);
            this.listboxComponent.TabIndex = 72;
            this.listboxComponent.Tag = "component";
            // 
            // lblComponent
            // 
            this.lblComponent.AutoSize = true;
            this.lblComponent.Location = new System.Drawing.Point(131, 18);
            this.lblComponent.Name = "lblComponent";
            this.lblComponent.Size = new System.Drawing.Size(61, 13);
            this.lblComponent.TabIndex = 71;
            this.lblComponent.Text = "Component";
            // 
            // listboxResolution
            // 
            this.listboxResolution.FormattingEnabled = true;
            this.listboxResolution.Location = new System.Drawing.Point(104, 34);
            this.listboxResolution.Name = "listboxResolution";
            this.listboxResolution.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listboxResolution.Size = new System.Drawing.Size(87, 95);
            this.listboxResolution.TabIndex = 70;
            this.listboxResolution.Tag = "resolution";
            // 
            // lblResolution
            // 
            this.lblResolution.AutoSize = true;
            this.lblResolution.Location = new System.Drawing.Point(101, 18);
            this.lblResolution.Name = "lblResolution";
            this.lblResolution.Size = new System.Drawing.Size(57, 13);
            this.lblResolution.TabIndex = 69;
            this.lblResolution.Text = "Resolution";
            // 
            // listboxStatus
            // 
            this.listboxStatus.FormattingEnabled = true;
            this.listboxStatus.Location = new System.Drawing.Point(11, 34);
            this.listboxStatus.Name = "listboxStatus";
            this.listboxStatus.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listboxStatus.Size = new System.Drawing.Size(87, 95);
            this.listboxStatus.TabIndex = 68;
            this.listboxStatus.Tag = "bug_status";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(8, 18);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 67;
            this.lblStatus.Text = "Status";
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(5, 16);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(44, 13);
            this.lblProduct.TabIndex = 66;
            this.lblProduct.Text = "Product";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listboxProduct);
            this.groupBox1.Controls.Add(this.listboxVersion);
            this.groupBox1.Controls.Add(this.lblVersion);
            this.groupBox1.Controls.Add(this.listboxComponent);
            this.groupBox1.Controls.Add(this.lblComponent);
            this.groupBox1.Controls.Add(this.lblProduct);
            this.groupBox1.Location = new System.Drawing.Point(9, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 138);
            this.groupBox1.TabIndex = 103;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Product";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.cmbSummary);
            this.groupBox2.Controls.Add(this.txtSummary);
            this.groupBox2.Location = new System.Drawing.Point(9, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(760, 44);
            this.groupBox2.TabIndex = 104;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Summary";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(635, 1);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 108;
            this.textBox2.Tag = "content";
            this.textBox2.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblPriority);
            this.groupBox3.Controls.Add(this.listboxPriority);
            this.groupBox3.Controls.Add(this.lblSeverity);
            this.groupBox3.Controls.Add(this.listboxSeverity);
            this.groupBox3.Controls.Add(this.listboxResolution);
            this.groupBox3.Controls.Add(this.lblResolution);
            this.groupBox3.Controls.Add(this.listboxStatus);
            this.groupBox3.Controls.Add(this.lblStatus);
            this.groupBox3.Location = new System.Drawing.Point(407, 52);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(362, 138);
            this.groupBox3.TabIndex = 105;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Status";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtGeneralValue);
            this.groupBox4.Controls.Add(this.cmbGeneralOperator);
            this.groupBox4.Controls.Add(this.cmbGeneralField);
            this.groupBox4.Controls.Add(this.dateTimePicker2);
            this.groupBox4.Controls.Add(this.dateTimePicker1);
            this.groupBox4.Controls.Add(this.lblChangedFrom);
            this.groupBox4.Controls.Add(this.cmbWhiteboard);
            this.groupBox4.Controls.Add(this.cmbURL);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.txtURL);
            this.groupBox4.Controls.Add(this.txtWhiteboard);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.cmbComment);
            this.groupBox4.Controls.Add(this.txtComment);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(9, 195);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(564, 149);
            this.groupBox4.TabIndex = 106;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Misc";
            // 
            // txtGeneralValue
            // 
            this.txtGeneralValue.AcceptsReturn = true;
            this.txtGeneralValue.Location = new System.Drawing.Point(356, 122);
            this.txtGeneralValue.Name = "txtGeneralValue";
            this.txtGeneralValue.Size = new System.Drawing.Size(196, 20);
            this.txtGeneralValue.TabIndex = 112;
            this.txtGeneralValue.Tag = "value0-0-0";
            // 
            // cmbGeneralOperator
            // 
            this.cmbGeneralOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGeneralOperator.FormattingEnabled = true;
            this.cmbGeneralOperator.Location = new System.Drawing.Point(188, 122);
            this.cmbGeneralOperator.Name = "cmbGeneralOperator";
            this.cmbGeneralOperator.Size = new System.Drawing.Size(162, 21);
            this.cmbGeneralOperator.TabIndex = 111;
            this.cmbGeneralOperator.Tag = "type0-0-0";
            // 
            // cmbGeneralField
            // 
            this.cmbGeneralField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGeneralField.FormattingEnabled = true;
            this.cmbGeneralField.Location = new System.Drawing.Point(9, 121);
            this.cmbGeneralField.Name = "cmbGeneralField";
            this.cmbGeneralField.Size = new System.Drawing.Size(173, 21);
            this.cmbGeneralField.TabIndex = 110;
            this.cmbGeneralField.Tag = "field0-0-0";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Checked = false;
            this.dateTimePicker2.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(248, 96);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.ShowCheckBox = true;
            this.dateTimePicker2.Size = new System.Drawing.Size(102, 20);
            this.dateTimePicker2.TabIndex = 109;
            this.dateTimePicker2.Tag = "chfieldto";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Checked = false;
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(105, 96);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowCheckBox = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(102, 20);
            this.dateTimePicker1.TabIndex = 108;
            this.dateTimePicker1.Tag = "chfieldfrom";
            // 
            // lblChangedFrom
            // 
            this.lblChangedFrom.AutoSize = true;
            this.lblChangedFrom.Location = new System.Drawing.Point(6, 99);
            this.lblChangedFrom.Name = "lblChangedFrom";
            this.lblChangedFrom.Size = new System.Drawing.Size(73, 13);
            this.lblChangedFrom.TabIndex = 107;
            this.lblChangedFrom.Text = "Changed from";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(218, 99);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(16, 13);
            this.label5.TabIndex = 108;
            this.label5.Text = "to";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.lblHardware);
            this.groupBox5.Controls.Add(this.listboxHardware);
            this.groupBox5.Controls.Add(this.listboxOS);
            this.groupBox5.Location = new System.Drawing.Point(581, 195);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(188, 149);
            this.groupBox5.TabIndex = 107;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Platform";
            // 
            // UCAdvancedSearchMobile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtQueryFormat);
            this.Controls.Add(this.groupBox2);
            this.Name = "UCAdvancedSearchMobile";
            this.Size = new System.Drawing.Size(818, 473);
            this.Load += new System.EventHandler(this.ucAdvancedSearch_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UCAdvancedSearchMobile_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtQueryFormat;
        private System.Windows.Forms.ListBox listboxProduct;
        private System.Windows.Forms.TextBox txtWhiteboard;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.ComboBox cmbWhiteboard;
        private System.Windows.Forms.ComboBox cmbURL;
        private System.Windows.Forms.ComboBox cmbComment;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listboxOS;
        private System.Windows.Forms.Label lblHardware;
        private System.Windows.Forms.ListBox listboxHardware;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.ListBox listboxPriority;
        private System.Windows.Forms.Label lblSeverity;
        private System.Windows.Forms.ListBox listboxSeverity;
        private System.Windows.Forms.ComboBox cmbSummary;
        private System.Windows.Forms.ListBox listboxVersion;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.ListBox listboxComponent;
        private System.Windows.Forms.Label lblComponent;
        private System.Windows.Forms.ListBox listboxResolution;
        private System.Windows.Forms.Label lblResolution;
        private System.Windows.Forms.ListBox listboxStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblChangedFrom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.TextBox txtGeneralValue;
        private System.Windows.Forms.ComboBox cmbGeneralOperator;
        private System.Windows.Forms.ComboBox cmbGeneralField;

    }
}
