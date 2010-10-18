namespace MyZilla.UI
{
    partial class UCEditBug
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtReporter = new System.Windows.Forms.TextBox();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.lblURL = new System.Windows.Forms.Label();
            this.lblComment = new System.Windows.Forms.Label();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.lblSummary = new System.Windows.Forms.Label();
            this.cmbHardware = new System.Windows.Forms.ComboBox();
            this.cmbSeverity = new System.Windows.Forms.ComboBox();
            this.cmbOS = new System.Windows.Forms.ComboBox();
            this.txtBlock = new System.Windows.Forms.TextBox();
            this.txtDependsOn = new System.Windows.Forms.TextBox();
            this.cmbPriority = new System.Windows.Forms.ComboBox();
            this.cmbVersion = new System.Windows.Forms.ComboBox();
            this.cmbComponent = new System.Windows.Forms.ComboBox();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.lblHardware = new System.Windows.Forms.Label();
            this.lblSeverity = new System.Windows.Forms.Label();
            this.lblOS = new System.Windows.Forms.Label();
            this.lblBlock = new System.Windows.Forms.Label();
            this.lblDependsOn = new System.Windows.Forms.Label();
            this.lblAssignedTo = new System.Windows.Forms.Label();
            this.lblPriority = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblComponent = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblReporter = new System.Windows.Forms.Label();
            this.lblLastModified = new System.Windows.Forms.Label();
            this.txtLastModified = new System.Windows.Forms.TextBox();
            this.lblBugNo = new System.Windows.Forms.Label();
            this.txtBugNo = new System.Windows.Forms.TextBox();
            this.lblStatusWh = new System.Windows.Forms.Label();
            this.txtStWhiteboard = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.grpSel = new System.Windows.Forms.GroupBox();
            this.lblResolution = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupStatus = new System.Windows.Forms.GroupBox();
            this.btnAddCC = new System.Windows.Forms.Button();
            this.txtCC = new System.Windows.Forms.TextBox();
            this.txtAssignedTo = new System.Windows.Forms.TextBox();
            this.lstCC = new System.Windows.Forms.CheckedListBox();
            this.lblCC = new System.Windows.Forms.Label();
            this.cmbMilestone = new System.Windows.Forms.ComboBox();
            this.lblMilestone = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtDescriptionValue = new System.Windows.Forms.TextBox();
            this.lblPreviousComments = new System.Windows.Forms.Label();
            this.pnlComments = new System.Windows.Forms.Panel();
            this.ucComments1 = new MyZilla.UI.UCComments();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtResolution = new System.Windows.Forms.TextBox();
            this.txtConnInfo = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lstAttachments = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewLinkColumn();
            this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dsAttachments = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.dataColumn1 = new System.Data.DataColumn();
            this.dataColumn2 = new System.Data.DataColumn();
            this.dataColumn3 = new System.Data.DataColumn();
            this.dataColumn4 = new System.Data.DataColumn();
            this.dataColumn5 = new System.Data.DataColumn();
            this.btnAddAttachment = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtOpenedOn = new System.Windows.Forms.TextBox();
            this.lblOpenedOn = new System.Windows.Forms.Label();
            this.groupStatus.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlComments.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstAttachments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAttachments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(14, 22);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Status";
            // 
            // txtReporter
            // 
            this.txtReporter.BackColor = System.Drawing.Color.LightGray;
            this.txtReporter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtReporter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReporter.Location = new System.Drawing.Point(78, 18);
            this.txtReporter.Name = "txtReporter";
            this.txtReporter.ReadOnly = true;
            this.txtReporter.Size = new System.Drawing.Size(242, 20);
            this.txtReporter.TabIndex = 1;
            this.txtReporter.TabStop = false;
            this.txtReporter.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtReporter_MouseClick);
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(78, 73);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(368, 20);
            this.txtURL.TabIndex = 9;
            this.txtURL.Text = "http://";
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Location = new System.Drawing.Point(43, 76);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(29, 13);
            this.lblURL.TabIndex = 8;
            this.lblURL.Text = "URL";
            // 
            // lblComment
            // 
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new System.Drawing.Point(15, 209);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(51, 13);
            this.lblComment.TabIndex = 4;
            this.lblComment.Text = "Comment";
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(73, 208);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComment.Size = new System.Drawing.Size(352, 82);
            this.txtComment.TabIndex = 5;
            this.txtComment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtComment_KeyDown);
            // 
            // txtSummary
            // 
            this.txtSummary.Location = new System.Drawing.Point(79, 45);
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.Size = new System.Drawing.Size(690, 20);
            this.txtSummary.TabIndex = 7;
            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.Location = new System.Drawing.Point(23, 49);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(50, 13);
            this.lblSummary.TabIndex = 6;
            this.lblSummary.Text = "Summary";
            // 
            // cmbHardware
            // 
            this.cmbHardware.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHardware.FormattingEnabled = true;
            this.cmbHardware.Location = new System.Drawing.Point(78, 46);
            this.cmbHardware.Name = "cmbHardware";
            this.cmbHardware.Size = new System.Drawing.Size(143, 21);
            this.cmbHardware.TabIndex = 5;
            // 
            // cmbSeverity
            // 
            this.cmbSeverity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeverity.FormattingEnabled = true;
            this.cmbSeverity.Location = new System.Drawing.Point(56, 44);
            this.cmbSeverity.Name = "cmbSeverity";
            this.cmbSeverity.Size = new System.Drawing.Size(96, 21);
            this.cmbSeverity.TabIndex = 3;
            // 
            // cmbOS
            // 
            this.cmbOS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOS.FormattingEnabled = true;
            this.cmbOS.Location = new System.Drawing.Point(78, 19);
            this.cmbOS.Name = "cmbOS";
            this.cmbOS.Size = new System.Drawing.Size(143, 21);
            this.cmbOS.TabIndex = 1;
            // 
            // txtBlock
            // 
            this.txtBlock.Location = new System.Drawing.Point(318, 46);
            this.txtBlock.Name = "txtBlock";
            this.txtBlock.Size = new System.Drawing.Size(128, 20);
            this.txtBlock.TabIndex = 7;
            // 
            // txtDependsOn
            // 
            this.txtDependsOn.Location = new System.Drawing.Point(318, 19);
            this.txtDependsOn.Name = "txtDependsOn";
            this.txtDependsOn.Size = new System.Drawing.Size(128, 20);
            this.txtDependsOn.TabIndex = 3;
            // 
            // cmbPriority
            // 
            this.cmbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPriority.FormattingEnabled = true;
            this.cmbPriority.Location = new System.Drawing.Point(224, 17);
            this.cmbPriority.Name = "cmbPriority";
            this.cmbPriority.Size = new System.Drawing.Size(96, 21);
            this.cmbPriority.TabIndex = 5;
            // 
            // cmbVersion
            // 
            this.cmbVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVersion.FormattingEnabled = true;
            this.cmbVersion.Location = new System.Drawing.Point(582, 19);
            this.cmbVersion.Name = "cmbVersion";
            this.cmbVersion.Size = new System.Drawing.Size(180, 21);
            this.cmbVersion.TabIndex = 5;
            // 
            // cmbComponent
            // 
            this.cmbComponent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComponent.FormattingEnabled = true;
            this.cmbComponent.Location = new System.Drawing.Point(342, 19);
            this.cmbComponent.Name = "cmbComponent";
            this.cmbComponent.Size = new System.Drawing.Size(180, 21);
            this.cmbComponent.TabIndex = 3;
            this.cmbComponent.SelectedValueChanged += new System.EventHandler(this.cmbComponent_SelectedValueChanged);
            // 
            // cmbProduct
            // 
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.Location = new System.Drawing.Point(79, 19);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(180, 21);
            this.cmbProduct.TabIndex = 1;
            this.cmbProduct.SelectedValueChanged += new System.EventHandler(this.cmbProduct_SelectedValueChanged);
            // 
            // lblHardware
            // 
            this.lblHardware.AutoSize = true;
            this.lblHardware.Location = new System.Drawing.Point(20, 49);
            this.lblHardware.Name = "lblHardware";
            this.lblHardware.Size = new System.Drawing.Size(53, 13);
            this.lblHardware.TabIndex = 4;
            this.lblHardware.Text = "Hardware";
            // 
            // lblSeverity
            // 
            this.lblSeverity.AutoSize = true;
            this.lblSeverity.Location = new System.Drawing.Point(6, 48);
            this.lblSeverity.Name = "lblSeverity";
            this.lblSeverity.Size = new System.Drawing.Size(45, 13);
            this.lblSeverity.TabIndex = 2;
            this.lblSeverity.Text = "Severity";
            // 
            // lblOS
            // 
            this.lblOS.AutoSize = true;
            this.lblOS.Location = new System.Drawing.Point(51, 23);
            this.lblOS.Name = "lblOS";
            this.lblOS.Size = new System.Drawing.Size(22, 13);
            this.lblOS.TabIndex = 0;
            this.lblOS.Text = "OS";
            // 
            // lblBlock
            // 
            this.lblBlock.AutoSize = true;
            this.lblBlock.Location = new System.Drawing.Point(278, 49);
            this.lblBlock.Name = "lblBlock";
            this.lblBlock.Size = new System.Drawing.Size(34, 13);
            this.lblBlock.TabIndex = 6;
            this.lblBlock.Text = "Block";
            // 
            // lblDependsOn
            // 
            this.lblDependsOn.AutoSize = true;
            this.lblDependsOn.Location = new System.Drawing.Point(245, 23);
            this.lblDependsOn.Name = "lblDependsOn";
            this.lblDependsOn.Size = new System.Drawing.Size(67, 13);
            this.lblDependsOn.TabIndex = 2;
            this.lblDependsOn.Text = "Depends On";
            // 
            // lblAssignedTo
            // 
            this.lblAssignedTo.AutoSize = true;
            this.lblAssignedTo.Location = new System.Drawing.Point(6, 46);
            this.lblAssignedTo.Name = "lblAssignedTo";
            this.lblAssignedTo.Size = new System.Drawing.Size(66, 13);
            this.lblAssignedTo.TabIndex = 2;
            this.lblAssignedTo.Text = "Assigned To";
            // 
            // lblPriority
            // 
            this.lblPriority.AutoSize = true;
            this.lblPriority.Location = new System.Drawing.Point(181, 21);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(38, 13);
            this.lblPriority.TabIndex = 4;
            this.lblPriority.Text = "Priority";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(534, 23);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(42, 13);
            this.lblVersion.TabIndex = 4;
            this.lblVersion.Text = "Version";
            // 
            // lblComponent
            // 
            this.lblComponent.AutoSize = true;
            this.lblComponent.Location = new System.Drawing.Point(275, 23);
            this.lblComponent.Name = "lblComponent";
            this.lblComponent.Size = new System.Drawing.Size(61, 13);
            this.lblComponent.TabIndex = 2;
            this.lblComponent.Text = "Component";
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(29, 23);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(44, 13);
            this.lblProduct.TabIndex = 0;
            this.lblProduct.Text = "Product";
            // 
            // lblReporter
            // 
            this.lblReporter.AutoSize = true;
            this.lblReporter.Location = new System.Drawing.Point(24, 20);
            this.lblReporter.Name = "lblReporter";
            this.lblReporter.Size = new System.Drawing.Size(48, 13);
            this.lblReporter.TabIndex = 0;
            this.lblReporter.Text = "Reporter";
            // 
            // lblLastModified
            // 
            this.lblLastModified.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLastModified.AutoSize = true;
            this.lblLastModified.Location = new System.Drawing.Point(47, 77);
            this.lblLastModified.Name = "lblLastModified";
            this.lblLastModified.Size = new System.Drawing.Size(70, 13);
            this.lblLastModified.TabIndex = 4;
            this.lblLastModified.Text = "Last Modified";
            // 
            // txtLastModified
            // 
            this.txtLastModified.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLastModified.BackColor = System.Drawing.Color.LightGray;
            this.txtLastModified.Location = new System.Drawing.Point(128, 73);
            this.txtLastModified.Name = "txtLastModified";
            this.txtLastModified.ReadOnly = true;
            this.txtLastModified.Size = new System.Drawing.Size(144, 20);
            this.txtLastModified.TabIndex = 5;
            this.txtLastModified.TabStop = false;
            // 
            // lblBugNo
            // 
            this.lblBugNo.AutoSize = true;
            this.lblBugNo.Location = new System.Drawing.Point(53, 8);
            this.lblBugNo.Name = "lblBugNo";
            this.lblBugNo.Size = new System.Drawing.Size(33, 13);
            this.lblBugNo.TabIndex = 0;
            this.lblBugNo.Text = "Bug#";
            // 
            // txtBugNo
            // 
            this.txtBugNo.BackColor = System.Drawing.Color.LightGray;
            this.txtBugNo.Location = new System.Drawing.Point(92, 5);
            this.txtBugNo.Name = "txtBugNo";
            this.txtBugNo.ReadOnly = true;
            this.txtBugNo.Size = new System.Drawing.Size(73, 20);
            this.txtBugNo.TabIndex = 1;
            this.txtBugNo.TabStop = false;
            // 
            // lblStatusWh
            // 
            this.lblStatusWh.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatusWh.AutoSize = true;
            this.lblStatusWh.Location = new System.Drawing.Point(22, 23);
            this.lblStatusWh.Name = "lblStatusWh";
            this.lblStatusWh.Size = new System.Drawing.Size(95, 13);
            this.lblStatusWh.TabIndex = 0;
            this.lblStatusWh.Text = "Status Whiteboard";
            // 
            // txtStWhiteboard
            // 
            this.txtStWhiteboard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStWhiteboard.Location = new System.Drawing.Point(128, 19);
            this.txtStWhiteboard.Name = "txtStWhiteboard";
            this.txtStWhiteboard.Size = new System.Drawing.Size(144, 20);
            this.txtStWhiteboard.TabIndex = 1;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(6, 21);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "Description";
            // 
            // grpSel
            // 
            this.grpSel.Location = new System.Drawing.Point(12, 399);
            this.grpSel.Name = "grpSel";
            this.grpSel.Size = new System.Drawing.Size(345, 161);
            this.grpSel.TabIndex = 7;
            this.grpSel.TabStop = false;
            // 
            // lblResolution
            // 
            this.lblResolution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResolution.AutoSize = true;
            this.lblResolution.Location = new System.Drawing.Point(60, 50);
            this.lblResolution.Name = "lblResolution";
            this.lblResolution.Size = new System.Drawing.Size(57, 13);
            this.lblResolution.TabIndex = 2;
            this.lblResolution.Text = "Resolution";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(638, 675);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(68, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(718, 675);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupStatus
            // 
            this.groupStatus.Controls.Add(this.btnAddCC);
            this.groupStatus.Controls.Add(this.txtCC);
            this.groupStatus.Controls.Add(this.txtAssignedTo);
            this.groupStatus.Controls.Add(this.lblReporter);
            this.groupStatus.Controls.Add(this.lblAssignedTo);
            this.groupStatus.Controls.Add(this.txtReporter);
            this.groupStatus.Controls.Add(this.lstCC);
            this.groupStatus.Controls.Add(this.lblCC);
            this.groupStatus.Location = new System.Drawing.Point(460, 100);
            this.groupStatus.Name = "groupStatus";
            this.groupStatus.Size = new System.Drawing.Size(333, 219);
            this.groupStatus.TabIndex = 5;
            this.groupStatus.TabStop = false;
            this.groupStatus.Text = "People";
            // 
            // btnAddCC
            // 
            this.btnAddCC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddCC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnAddCC.Location = new System.Drawing.Point(301, 69);
            this.btnAddCC.Name = "btnAddCC";
            this.btnAddCC.Size = new System.Drawing.Size(19, 20);
            this.btnAddCC.TabIndex = 7;
            this.btnAddCC.Text = "+";
            this.btnAddCC.UseVisualStyleBackColor = true;
            this.btnAddCC.Visible = false;
            this.btnAddCC.Click += new System.EventHandler(this.btnAddCC_Click);
            // 
            // txtCC
            // 
            this.txtCC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCC.BackColor = System.Drawing.SystemColors.Window;
            this.txtCC.Location = new System.Drawing.Point(78, 70);
            this.txtCC.Name = "txtCC";
            this.txtCC.Size = new System.Drawing.Size(215, 20);
            this.txtCC.TabIndex = 6;
            this.txtCC.Visible = false;
            // 
            // txtAssignedTo
            // 
            this.txtAssignedTo.BackColor = System.Drawing.Color.LightGray;
            this.txtAssignedTo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtAssignedTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAssignedTo.Location = new System.Drawing.Point(78, 44);
            this.txtAssignedTo.Name = "txtAssignedTo";
            this.txtAssignedTo.ReadOnly = true;
            this.txtAssignedTo.Size = new System.Drawing.Size(242, 20);
            this.txtAssignedTo.TabIndex = 3;
            this.txtAssignedTo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtAssignedTo_MouseClick);
            // 
            // lstCC
            // 
            this.lstCC.CheckOnClick = true;
            this.lstCC.FormattingEnabled = true;
            this.lstCC.Location = new System.Drawing.Point(78, 70);
            this.lstCC.Name = "lstCC";
            this.lstCC.Size = new System.Drawing.Size(242, 139);
            this.lstCC.TabIndex = 5;
            // 
            // lblCC
            // 
            this.lblCC.AutoSize = true;
            this.lblCC.Location = new System.Drawing.Point(51, 70);
            this.lblCC.Name = "lblCC";
            this.lblCC.Size = new System.Drawing.Size(21, 13);
            this.lblCC.TabIndex = 4;
            this.lblCC.Text = "CC";
            // 
            // cmbMilestone
            // 
            this.cmbMilestone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMilestone.FormattingEnabled = true;
            this.cmbMilestone.Location = new System.Drawing.Point(224, 44);
            this.cmbMilestone.Name = "cmbMilestone";
            this.cmbMilestone.Size = new System.Drawing.Size(96, 21);
            this.cmbMilestone.TabIndex = 7;
            // 
            // lblMilestone
            // 
            this.lblMilestone.AutoSize = true;
            this.lblMilestone.Location = new System.Drawing.Point(167, 48);
            this.lblMilestone.Name = "lblMilestone";
            this.lblMilestone.Size = new System.Drawing.Size(52, 13);
            this.lblMilestone.TabIndex = 6;
            this.lblMilestone.Text = "Milestone";
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.Color.LightGray;
            this.txtStatus.Location = new System.Drawing.Point(56, 18);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(96, 20);
            this.txtStatus.TabIndex = 1;
            this.txtStatus.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDescriptionValue);
            this.groupBox1.Controls.Add(this.lblPreviousComments);
            this.groupBox1.Controls.Add(this.pnlComments);
            this.groupBox1.Controls.Add(this.lblDescription);
            this.groupBox1.Controls.Add(this.lblComment);
            this.groupBox1.Controls.Add(this.txtComment);
            this.groupBox1.Location = new System.Drawing.Point(12, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(437, 297);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Description and comments";
            // 
            // txtDescriptionValue
            // 
            this.txtDescriptionValue.BackColor = System.Drawing.Color.LightGray;
            this.txtDescriptionValue.Location = new System.Drawing.Point(74, 20);
            this.txtDescriptionValue.Multiline = true;
            this.txtDescriptionValue.Name = "txtDescriptionValue";
            this.txtDescriptionValue.ReadOnly = true;
            this.txtDescriptionValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescriptionValue.Size = new System.Drawing.Size(352, 76);
            this.txtDescriptionValue.TabIndex = 1;
            this.txtDescriptionValue.TabStop = false;
            // 
            // lblPreviousComments
            // 
            this.lblPreviousComments.Location = new System.Drawing.Point(9, 103);
            this.lblPreviousComments.Name = "lblPreviousComments";
            this.lblPreviousComments.Size = new System.Drawing.Size(57, 29);
            this.lblPreviousComments.TabIndex = 2;
            this.lblPreviousComments.Text = "Previous Comments";
            this.lblPreviousComments.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pnlComments
            // 
            this.pnlComments.AutoScroll = true;
            this.pnlComments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlComments.Controls.Add(this.ucComments1);
            this.pnlComments.Location = new System.Drawing.Point(74, 102);
            this.pnlComments.Name = "pnlComments";
            this.pnlComments.Size = new System.Drawing.Size(352, 100);
            this.pnlComments.TabIndex = 82;
            // 
            // ucComments1
            // 
            this.ucComments1.AutoScroll = true;
            this.ucComments1.AutoSize = true;
            this.ucComments1.BackColor = System.Drawing.SystemColors.Menu;
            this.ucComments1.BackgroundColor = System.Drawing.Color.LightGray;
            this.ucComments1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucComments1.Location = new System.Drawing.Point(0, 0);
            this.ucComments1.Name = "ucComments1";
            this.ucComments1.Size = new System.Drawing.Size(350, 98);
            this.ucComments1.TabIndex = 3;
            this.ucComments1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSummary);
            this.groupBox2.Controls.Add(this.lblSummary);
            this.groupBox2.Controls.Add(this.cmbProduct);
            this.groupBox2.Controls.Add(this.lblProduct);
            this.groupBox2.Controls.Add(this.cmbComponent);
            this.groupBox2.Controls.Add(this.lblComponent);
            this.groupBox2.Controls.Add(this.lblVersion);
            this.groupBox2.Controls.Add(this.cmbVersion);
            this.groupBox2.Location = new System.Drawing.Point(12, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(781, 71);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Summary";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbHardware);
            this.groupBox3.Controls.Add(this.cmbOS);
            this.groupBox3.Controls.Add(this.txtBlock);
            this.groupBox3.Controls.Add(this.lblURL);
            this.groupBox3.Controls.Add(this.txtURL);
            this.groupBox3.Controls.Add(this.txtDependsOn);
            this.groupBox3.Controls.Add(this.lblHardware);
            this.groupBox3.Controls.Add(this.lblOS);
            this.groupBox3.Controls.Add(this.lblBlock);
            this.groupBox3.Controls.Add(this.lblDependsOn);
            this.groupBox3.Location = new System.Drawing.Point(12, 562);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(468, 105);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Bug details";
            // 
            // txtResolution
            // 
            this.txtResolution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResolution.BackColor = System.Drawing.Color.LightGray;
            this.txtResolution.Location = new System.Drawing.Point(128, 46);
            this.txtResolution.Name = "txtResolution";
            this.txtResolution.ReadOnly = true;
            this.txtResolution.Size = new System.Drawing.Size(143, 20);
            this.txtResolution.TabIndex = 3;
            this.txtResolution.TabStop = false;
            // 
            // txtConnInfo
            // 
            this.txtConnInfo.BackColor = System.Drawing.Color.LightGray;
            this.txtConnInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConnInfo.Location = new System.Drawing.Point(594, 5);
            this.txtConnInfo.Name = "txtConnInfo";
            this.txtConnInfo.ReadOnly = true;
            this.txtConnInfo.Size = new System.Drawing.Size(199, 20);
            this.txtConnInfo.TabIndex = 2;
            this.txtConnInfo.TabStop = false;
            this.txtConnInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lstAttachments);
            this.groupBox4.Controls.Add(this.btnAddAttachment);
            this.groupBox4.Location = new System.Drawing.Point(376, 399);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(417, 161);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Attachment";
            // 
            // lstAttachments
            // 
            this.lstAttachments.AllowUserToAddRows = false;
            this.lstAttachments.AllowUserToDeleteRows = false;
            this.lstAttachments.AllowUserToOrderColumns = true;
            this.lstAttachments.AutoGenerateColumns = false;
            this.lstAttachments.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.lstAttachments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.lstAttachments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lstAttachments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.fileNameDataGridViewTextBoxColumn,
            this.typeDataGridViewTextBoxColumn,
            this.createdDataGridViewTextBoxColumn});
            this.lstAttachments.Cursor = System.Windows.Forms.Cursors.Default;
            this.lstAttachments.DataMember = "tblAttachment";
            this.lstAttachments.DataSource = this.dsAttachments;
            this.lstAttachments.EnableHeadersVisualStyles = false;
            this.lstAttachments.Location = new System.Drawing.Point(15, 19);
            this.lstAttachments.MultiSelect = false;
            this.lstAttachments.Name = "lstAttachments";
            this.lstAttachments.ReadOnly = true;
            this.lstAttachments.RowHeadersVisible = false;
            this.lstAttachments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lstAttachments.Size = new System.Drawing.Size(389, 105);
            this.lstAttachments.TabIndex = 0;
            this.lstAttachments.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAttachments_CellDoubleClick);
            this.lstAttachments.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAttachments_CellContentClick);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // fileNameDataGridViewTextBoxColumn
            // 
            this.fileNameDataGridViewTextBoxColumn.ActiveLinkColor = System.Drawing.Color.Empty;
            this.fileNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fileNameDataGridViewTextBoxColumn.DataPropertyName = "FileName";
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.fileNameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.fileNameDataGridViewTextBoxColumn.HeaderText = "File description";
            this.fileNameDataGridViewTextBoxColumn.LinkColor = System.Drawing.Color.Empty;
            this.fileNameDataGridViewTextBoxColumn.Name = "fileNameDataGridViewTextBoxColumn";
            this.fileNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.fileNameDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.fileNameDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.fileNameDataGridViewTextBoxColumn.VisitedLinkColor = System.Drawing.Color.Empty;
            // 
            // typeDataGridViewTextBoxColumn
            // 
            this.typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
            this.typeDataGridViewTextBoxColumn.HeaderText = "Type";
            this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            this.typeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // createdDataGridViewTextBoxColumn
            // 
            this.createdDataGridViewTextBoxColumn.DataPropertyName = "Created";
            this.createdDataGridViewTextBoxColumn.HeaderText = "Created";
            this.createdDataGridViewTextBoxColumn.Name = "createdDataGridViewTextBoxColumn";
            this.createdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dsAttachments
            // 
            this.dsAttachments.DataSetName = "NewDataSet";
            this.dsAttachments.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1});
            // 
            // dataTable1
            // 
            this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5});
            this.dataTable1.TableName = "tblAttachment";
            // 
            // dataColumn1
            // 
            this.dataColumn1.ColumnName = "Id";
            this.dataColumn1.DataType = typeof(int);
            // 
            // dataColumn2
            // 
            this.dataColumn2.ColumnName = "FileName";
            // 
            // dataColumn3
            // 
            this.dataColumn3.ColumnName = "Type";
            // 
            // dataColumn4
            // 
            this.dataColumn4.ColumnName = "Created";
            // 
            // dataColumn5
            // 
            this.dataColumn5.ColumnName = "Size";
            // 
            // btnAddAttachment
            // 
            this.btnAddAttachment.Location = new System.Drawing.Point(297, 130);
            this.btnAddAttachment.Name = "btnAddAttachment";
            this.btnAddAttachment.Size = new System.Drawing.Size(108, 23);
            this.btnAddAttachment.TabIndex = 1;
            this.btnAddAttachment.Text = "Add attachment";
            this.btnAddAttachment.UseVisualStyleBackColor = true;
            this.btnAddAttachment.Click += new System.EventHandler(this.btnAddAttachment_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cmbMilestone);
            this.groupBox5.Controls.Add(this.txtStatus);
            this.groupBox5.Controls.Add(this.cmbPriority);
            this.groupBox5.Controls.Add(this.cmbSeverity);
            this.groupBox5.Controls.Add(this.lblMilestone);
            this.groupBox5.Controls.Add(this.lblPriority);
            this.groupBox5.Controls.Add(this.lblStatus);
            this.groupBox5.Controls.Add(this.lblSeverity);
            this.groupBox5.Location = new System.Drawing.Point(460, 322);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(333, 75);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Status";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtResolution);
            this.groupBox6.Controls.Add(this.txtStWhiteboard);
            this.groupBox6.Controls.Add(this.txtLastModified);
            this.groupBox6.Controls.Add(this.lblLastModified);
            this.groupBox6.Controls.Add(this.lblStatusWh);
            this.groupBox6.Controls.Add(this.lblResolution);
            this.groupBox6.Location = new System.Drawing.Point(498, 562);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(295, 105);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Other";
            // 
            // txtOpenedOn
            // 
            this.txtOpenedOn.BackColor = System.Drawing.Color.LightGray;
            this.txtOpenedOn.Location = new System.Drawing.Point(354, 3);
            this.txtOpenedOn.Name = "txtOpenedOn";
            this.txtOpenedOn.ReadOnly = true;
            this.txtOpenedOn.Size = new System.Drawing.Size(142, 20);
            this.txtOpenedOn.TabIndex = 14;
            this.txtOpenedOn.TabStop = false;
            // 
            // lblOpenedOn
            // 
            this.lblOpenedOn.AutoSize = true;
            this.lblOpenedOn.Location = new System.Drawing.Point(287, 8);
            this.lblOpenedOn.Name = "lblOpenedOn";
            this.lblOpenedOn.Size = new System.Drawing.Size(60, 13);
            this.lblOpenedOn.TabIndex = 13;
            this.lblOpenedOn.Text = "Opened on";
            // 
            // UCEditBug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.txtOpenedOn);
            this.Controls.Add(this.lblOpenedOn);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.txtBugNo);
            this.Controls.Add(this.lblBugNo);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.txtConnInfo);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.grpSel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupStatus);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox6);
            this.Name = "UCEditBug";
            this.Size = new System.Drawing.Size(805, 704);
            this.Load += new System.EventHandler(this.EditBug_Load);
            this.groupStatus.ResumeLayout(false);
            this.groupStatus.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlComments.ResumeLayout(false);
            this.pnlComments.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstAttachments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAttachments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtReporter;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.ComboBox cmbHardware;
        private System.Windows.Forms.ComboBox cmbSeverity;
        private System.Windows.Forms.ComboBox cmbOS;
        private System.Windows.Forms.TextBox txtBlock;
        private System.Windows.Forms.TextBox txtDependsOn;
        private System.Windows.Forms.ComboBox cmbPriority;
        private System.Windows.Forms.ComboBox cmbVersion;
        private System.Windows.Forms.ComboBox cmbComponent;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.Label lblHardware;
        private System.Windows.Forms.Label lblSeverity;
        private System.Windows.Forms.Label lblOS;
        private System.Windows.Forms.Label lblBlock;
        private System.Windows.Forms.Label lblDependsOn;
        private System.Windows.Forms.Label lblAssignedTo;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblComponent;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Label lblReporter;
        private System.Windows.Forms.Label lblLastModified;
        private System.Windows.Forms.TextBox txtLastModified;
        private System.Windows.Forms.Label lblBugNo;
        private System.Windows.Forms.TextBox txtBugNo;
        private System.Windows.Forms.Label lblStatusWh;
        private System.Windows.Forms.TextBox txtStWhiteboard;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.GroupBox grpSel;
        private System.Windows.Forms.Label lblResolution;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel pnlComments;
        private UCComments ucComments1;
        private System.Windows.Forms.Label lblPreviousComments;
        private System.Windows.Forms.TextBox txtConnInfo;
        private System.Windows.Forms.TextBox txtResolution;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.TextBox txtAssignedTo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnAddAttachment;
        private System.Data.DataSet dsAttachments;
        private System.Windows.Forms.DataGridView lstAttachments;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private System.Windows.Forms.ComboBox cmbMilestone;
        private System.Windows.Forms.Label lblMilestone;
        private System.Windows.Forms.CheckedListBox lstCC;
        private System.Windows.Forms.Label lblCC;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtDescriptionValue;
        private System.Windows.Forms.TextBox txtCC;
        private System.Windows.Forms.Button btnAddCC;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewLinkColumn fileNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox txtOpenedOn;
        private System.Windows.Forms.Label lblOpenedOn;
    }
}
