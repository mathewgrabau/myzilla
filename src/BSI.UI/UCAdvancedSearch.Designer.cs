namespace BSI.UI
{
    partial class UCAdvancedSearch
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
            this.btnSaveQuery = new System.Windows.Forms.Button();
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbSummary = new System.Windows.Forms.ComboBox();
            this.lblSummary = new System.Windows.Forms.Label();
            this.listboxVersion = new System.Windows.Forms.ListBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.listboxComponent = new System.Windows.Forms.ListBox();
            this.lblComponent = new System.Windows.Forms.Label();
            this.listboxResolution = new System.Windows.Forms.ListBox();
            this.lblResolution = new System.Windows.Forms.Label();
            this.listboxStatus = new System.Windows.Forms.ListBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSaveQuery
            // 
            this.btnSaveQuery.Location = new System.Drawing.Point(613, 484);
            this.btnSaveQuery.Name = "btnSaveQuery";
            this.btnSaveQuery.Size = new System.Drawing.Size(75, 23);
            this.btnSaveQuery.TabIndex = 98;
            this.btnSaveQuery.Text = "Save Query";
            this.btnSaveQuery.UseVisualStyleBackColor = true;
            this.btnSaveQuery.Click += new System.EventHandler(this.btnSaveQuery_Click);
            // 
            // txtQueryFormat
            // 
            this.txtQueryFormat.Location = new System.Drawing.Point(17, 23);
            this.txtQueryFormat.Name = "txtQueryFormat";
            this.txtQueryFormat.Size = new System.Drawing.Size(100, 20);
            this.txtQueryFormat.TabIndex = 97;
            this.txtQueryFormat.Tag = "query_format";
            this.txtQueryFormat.Visible = false;
            // 
            // listboxProduct
            // 
            this.listboxProduct.FormattingEnabled = true;
            this.listboxProduct.Location = new System.Drawing.Point(17, 106);
            this.listboxProduct.Name = "listboxProduct";
            this.listboxProduct.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listboxProduct.Size = new System.Drawing.Size(120, 108);
            this.listboxProduct.TabIndex = 96;
            this.listboxProduct.Tag = "product";
            this.listboxProduct.SelectedValueChanged += new System.EventHandler(this.listboxProduct_SelectedValueChanged);
            // 
            // txtWhiteboard
            // 
            this.txtWhiteboard.Location = new System.Drawing.Point(372, 300);
            this.txtWhiteboard.Name = "txtWhiteboard";
            this.txtWhiteboard.Size = new System.Drawing.Size(267, 20);
            this.txtWhiteboard.TabIndex = 95;
            this.txtWhiteboard.Tag = "status_whiteboard";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(372, 269);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(267, 20);
            this.txtURL.TabIndex = 94;
            this.txtURL.Tag = "bug_file_loc";
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(372, 238);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(267, 20);
            this.txtComment.TabIndex = 93;
            this.txtComment.Tag = "long_desc";
            // 
            // cmbWhiteboard
            // 
            this.cmbWhiteboard.FormattingEnabled = true;
            this.cmbWhiteboard.Location = new System.Drawing.Point(102, 300);
            this.cmbWhiteboard.Name = "cmbWhiteboard";
            this.cmbWhiteboard.Size = new System.Drawing.Size(254, 21);
            this.cmbWhiteboard.TabIndex = 92;
            this.cmbWhiteboard.Tag = "status_whiteboard_type";
            // 
            // cmbURL
            // 
            this.cmbURL.FormattingEnabled = true;
            this.cmbURL.Location = new System.Drawing.Point(102, 269);
            this.cmbURL.Name = "cmbURL";
            this.cmbURL.Size = new System.Drawing.Size(254, 21);
            this.cmbURL.TabIndex = 91;
            this.cmbURL.Tag = "bug_file_loc_type";
            // 
            // cmbComment
            // 
            this.cmbComment.FormattingEnabled = true;
            this.cmbComment.Location = new System.Drawing.Point(102, 238);
            this.cmbComment.Name = "cmbComment";
            this.cmbComment.Size = new System.Drawing.Size(254, 21);
            this.cmbComment.TabIndex = 90;
            this.cmbComment.Tag = "long_desc_type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 303);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 89;
            this.label4.Text = "Whiteboard";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 272);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 88;
            this.label3.Text = "The URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 87;
            this.label2.Text = "A Comment";
            // 
            // txtSummary
            // 
            this.txtSummary.Location = new System.Drawing.Point(359, 52);
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.Size = new System.Drawing.Size(176, 20);
            this.txtSummary.TabIndex = 86;
            this.txtSummary.Tag = "short_desc";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(591, 341);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 85;
            this.label1.Text = "OS";
            // 
            // listboxOS
            // 
            this.listboxOS.FormattingEnabled = true;
            this.listboxOS.Location = new System.Drawing.Point(594, 357);
            this.listboxOS.Name = "listboxOS";
            this.listboxOS.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listboxOS.Size = new System.Drawing.Size(94, 108);
            this.listboxOS.TabIndex = 84;
            this.listboxOS.Tag = "op_sys";
            // 
            // lblHardware
            // 
            this.lblHardware.AutoSize = true;
            this.lblHardware.Location = new System.Drawing.Point(479, 341);
            this.lblHardware.Name = "lblHardware";
            this.lblHardware.Size = new System.Drawing.Size(53, 13);
            this.lblHardware.TabIndex = 83;
            this.lblHardware.Text = "Hardware";
            // 
            // listboxHardware
            // 
            this.listboxHardware.FormattingEnabled = true;
            this.listboxHardware.Location = new System.Drawing.Point(482, 357);
            this.listboxHardware.Name = "listboxHardware";
            this.listboxHardware.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listboxHardware.Size = new System.Drawing.Size(94, 108);
            this.listboxHardware.TabIndex = 82;
            this.listboxHardware.Tag = "rep_platform";
            // 
            // lblPriority
            // 
            this.lblPriority.AutoSize = true;
            this.lblPriority.Location = new System.Drawing.Point(398, 341);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(38, 13);
            this.lblPriority.TabIndex = 81;
            this.lblPriority.Text = "Priority";
            // 
            // listboxPriority
            // 
            this.listboxPriority.FormattingEnabled = true;
            this.listboxPriority.Location = new System.Drawing.Point(401, 357);
            this.listboxPriority.Name = "listboxPriority";
            this.listboxPriority.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listboxPriority.Size = new System.Drawing.Size(62, 108);
            this.listboxPriority.TabIndex = 80;
            this.listboxPriority.Tag = "priority";
            // 
            // lblSeverity
            // 
            this.lblSeverity.AutoSize = true;
            this.lblSeverity.Location = new System.Drawing.Point(270, 341);
            this.lblSeverity.Name = "lblSeverity";
            this.lblSeverity.Size = new System.Drawing.Size(45, 13);
            this.lblSeverity.TabIndex = 79;
            this.lblSeverity.Text = "Severity";
            // 
            // listboxSeverity
            // 
            this.listboxSeverity.FormattingEnabled = true;
            this.listboxSeverity.Location = new System.Drawing.Point(273, 357);
            this.listboxSeverity.Name = "listboxSeverity";
            this.listboxSeverity.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listboxSeverity.Size = new System.Drawing.Size(109, 108);
            this.listboxSeverity.TabIndex = 78;
            this.listboxSeverity.Tag = "bug_severity";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(523, 484);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 77;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cmbSummary
            // 
            this.cmbSummary.FormattingEnabled = true;
            this.cmbSummary.Location = new System.Drawing.Point(80, 52);
            this.cmbSummary.Name = "cmbSummary";
            this.cmbSummary.Size = new System.Drawing.Size(273, 21);
            this.cmbSummary.TabIndex = 76;
            this.cmbSummary.Tag = "short_desc_type";
            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.Location = new System.Drawing.Point(14, 55);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(50, 13);
            this.lblSummary.TabIndex = 75;
            this.lblSummary.Text = "Summary";
            // 
            // listboxVersion
            // 
            this.listboxVersion.FormattingEnabled = true;
            this.listboxVersion.Location = new System.Drawing.Point(339, 106);
            this.listboxVersion.Name = "listboxVersion";
            this.listboxVersion.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listboxVersion.Size = new System.Drawing.Size(120, 108);
            this.listboxVersion.TabIndex = 74;
            this.listboxVersion.Tag = "version";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(336, 90);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(42, 13);
            this.lblVersion.TabIndex = 73;
            this.lblVersion.Text = "Version";
            // 
            // listboxComponent
            // 
            this.listboxComponent.FormattingEnabled = true;
            this.listboxComponent.Location = new System.Drawing.Point(179, 106);
            this.listboxComponent.Name = "listboxComponent";
            this.listboxComponent.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listboxComponent.Size = new System.Drawing.Size(120, 108);
            this.listboxComponent.TabIndex = 72;
            this.listboxComponent.Tag = "component";
            // 
            // lblComponent
            // 
            this.lblComponent.AutoSize = true;
            this.lblComponent.Location = new System.Drawing.Point(176, 90);
            this.lblComponent.Name = "lblComponent";
            this.lblComponent.Size = new System.Drawing.Size(61, 13);
            this.lblComponent.TabIndex = 71;
            this.lblComponent.Text = "Component";
            // 
            // listboxResolution
            // 
            this.listboxResolution.FormattingEnabled = true;
            this.listboxResolution.Location = new System.Drawing.Point(145, 357);
            this.listboxResolution.Name = "listboxResolution";
            this.listboxResolution.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listboxResolution.Size = new System.Drawing.Size(109, 108);
            this.listboxResolution.TabIndex = 70;
            this.listboxResolution.Tag = "resolution";
            // 
            // lblResolution
            // 
            this.lblResolution.AutoSize = true;
            this.lblResolution.Location = new System.Drawing.Point(142, 341);
            this.lblResolution.Name = "lblResolution";
            this.lblResolution.Size = new System.Drawing.Size(57, 13);
            this.lblResolution.TabIndex = 69;
            this.lblResolution.Text = "Resolution";
            // 
            // listboxStatus
            // 
            this.listboxStatus.FormattingEnabled = true;
            this.listboxStatus.Location = new System.Drawing.Point(17, 357);
            this.listboxStatus.Name = "listboxStatus";
            this.listboxStatus.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listboxStatus.Size = new System.Drawing.Size(109, 108);
            this.listboxStatus.TabIndex = 68;
            this.listboxStatus.Tag = "bug_status";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(14, 341);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 67;
            this.lblStatus.Text = "Status";
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(14, 90);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(44, 13);
            this.lblProduct.TabIndex = 66;
            this.lblProduct.Text = "Product";
            // 
            // UCAdvancedSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.btnSaveQuery);
            this.Controls.Add(this.txtQueryFormat);
            this.Controls.Add(this.listboxProduct);
            this.Controls.Add(this.txtWhiteboard);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.cmbWhiteboard);
            this.Controls.Add(this.cmbURL);
            this.Controls.Add(this.cmbComment);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSummary);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listboxOS);
            this.Controls.Add(this.lblHardware);
            this.Controls.Add(this.listboxHardware);
            this.Controls.Add(this.lblPriority);
            this.Controls.Add(this.listboxPriority);
            this.Controls.Add(this.lblSeverity);
            this.Controls.Add(this.listboxSeverity);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cmbSummary);
            this.Controls.Add(this.lblSummary);
            this.Controls.Add(this.listboxVersion);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.listboxComponent);
            this.Controls.Add(this.lblComponent);
            this.Controls.Add(this.listboxResolution);
            this.Controls.Add(this.lblResolution);
            this.Controls.Add(this.listboxStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblProduct);
            this.Name = "UCAdvancedSearch";
            this.Size = new System.Drawing.Size(932, 678);
            this.Load += new System.EventHandler(this.ucAdvancedSearch_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveQuery;
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
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cmbSummary;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.ListBox listboxVersion;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.ListBox listboxComponent;
        private System.Windows.Forms.Label lblComponent;
        private System.Windows.Forms.ListBox listboxResolution;
        private System.Windows.Forms.Label lblResolution;
        private System.Windows.Forms.ListBox listboxStatus;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblProduct;

    }
}
