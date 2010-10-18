namespace MyZilla.UI
{
    partial class UCInsertBug
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
            this.lblReporter = new System.Windows.Forms.Label();
            this.lblComponent = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblPriority = new System.Windows.Forms.Label();
            this.lblAssignedTo = new System.Windows.Forms.Label();
            this.lblSeverity = new System.Windows.Forms.Label();
            this.cmbComponent = new System.Windows.Forms.ComboBox();
            this.cmbVersion = new System.Windows.Forms.ComboBox();
            this.cmbPriority = new System.Windows.Forms.ComboBox();
            this.cmbAssignedTo = new System.Windows.Forms.ComboBox();
            this.txtDependsOn = new System.Windows.Forms.TextBox();
            this.txtBlock = new System.Windows.Forms.TextBox();
            this.cmbOS = new System.Windows.Forms.ComboBox();
            this.cmbSeverity = new System.Windows.Forms.ComboBox();
            this.cmbHardware = new System.Windows.Forms.ComboBox();
            this.lblSummary = new System.Windows.Forms.Label();
            this.txtSummary = new System.Windows.Forms.TextBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.txtReporter = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lstCC = new System.Windows.Forms.CheckedListBox();
            this.btnInsertBug = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbProduct = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.groupStatus = new System.Windows.Forms.GroupBox();
            this.btnAddCC = new System.Windows.Forms.Button();
            this.txtCC = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbMilestone = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblURL = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbConnections = new System.Windows.Forms.ComboBox();
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
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupStatus.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstAttachments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAttachments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblReporter
            // 
            this.lblReporter.AutoSize = true;
            this.lblReporter.Location = new System.Drawing.Point(29, 22);
            this.lblReporter.Name = "lblReporter";
            this.lblReporter.Size = new System.Drawing.Size(48, 13);
            this.lblReporter.TabIndex = 0;
            this.lblReporter.Text = "Reporter";
            // 
            // lblComponent
            // 
            this.lblComponent.AutoSize = true;
            this.lblComponent.Location = new System.Drawing.Point(256, 24);
            this.lblComponent.Name = "lblComponent";
            this.lblComponent.Size = new System.Drawing.Size(61, 13);
            this.lblComponent.TabIndex = 2;
            this.lblComponent.Text = "Component";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(504, 24);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(42, 13);
            this.lblVersion.TabIndex = 4;
            this.lblVersion.Text = "Version";
            // 
            // lblPriority
            // 
            this.lblPriority.AutoSize = true;
            this.lblPriority.Location = new System.Drawing.Point(40, 74);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(38, 13);
            this.lblPriority.TabIndex = 4;
            this.lblPriority.Text = "Priority";
            // 
            // lblAssignedTo
            // 
            this.lblAssignedTo.AutoSize = true;
            this.lblAssignedTo.Location = new System.Drawing.Point(11, 48);
            this.lblAssignedTo.Name = "lblAssignedTo";
            this.lblAssignedTo.Size = new System.Drawing.Size(66, 13);
            this.lblAssignedTo.TabIndex = 2;
            this.lblAssignedTo.Text = "Assigned To";
            // 
            // lblSeverity
            // 
            this.lblSeverity.AutoSize = true;
            this.lblSeverity.Location = new System.Drawing.Point(33, 47);
            this.lblSeverity.Name = "lblSeverity";
            this.lblSeverity.Size = new System.Drawing.Size(45, 13);
            this.lblSeverity.TabIndex = 2;
            this.lblSeverity.Text = "Severity";
            // 
            // cmbComponent
            // 
            this.cmbComponent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComponent.DropDownWidth = 190;
            this.cmbComponent.FormattingEnabled = true;
            this.cmbComponent.Location = new System.Drawing.Point(323, 21);
            this.cmbComponent.Name = "cmbComponent";
            this.cmbComponent.Size = new System.Drawing.Size(175, 21);
            this.cmbComponent.TabIndex = 3;
            this.cmbComponent.SelectedValueChanged += new System.EventHandler(this.cmbComponent_SelectedValueChanged);
            // 
            // cmbVersion
            // 
            this.cmbVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVersion.DropDownWidth = 160;
            this.cmbVersion.FormattingEnabled = true;
            this.cmbVersion.Location = new System.Drawing.Point(552, 21);
            this.cmbVersion.Name = "cmbVersion";
            this.cmbVersion.Size = new System.Drawing.Size(160, 21);
            this.cmbVersion.TabIndex = 5;
            // 
            // cmbPriority
            // 
            this.cmbPriority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPriority.FormattingEnabled = true;
            this.cmbPriority.Location = new System.Drawing.Point(84, 70);
            this.cmbPriority.Name = "cmbPriority";
            this.cmbPriority.Size = new System.Drawing.Size(143, 21);
            this.cmbPriority.TabIndex = 5;
            // 
            // cmbAssignedTo
            // 
            this.cmbAssignedTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAssignedTo.FormattingEnabled = true;
            this.cmbAssignedTo.Location = new System.Drawing.Point(84, 45);
            this.cmbAssignedTo.Name = "cmbAssignedTo";
            this.cmbAssignedTo.Size = new System.Drawing.Size(203, 21);
            this.cmbAssignedTo.TabIndex = 3;
            // 
            // txtDependsOn
            // 
            this.txtDependsOn.Location = new System.Drawing.Point(310, 20);
            this.txtDependsOn.Name = "txtDependsOn";
            this.txtDependsOn.Size = new System.Drawing.Size(143, 20);
            this.txtDependsOn.TabIndex = 3;
            // 
            // txtBlock
            // 
            this.txtBlock.Location = new System.Drawing.Point(310, 46);
            this.txtBlock.Name = "txtBlock";
            this.txtBlock.Size = new System.Drawing.Size(143, 20);
            this.txtBlock.TabIndex = 9;
            // 
            // cmbOS
            // 
            this.cmbOS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOS.FormattingEnabled = true;
            this.cmbOS.Location = new System.Drawing.Point(79, 20);
            this.cmbOS.Name = "cmbOS";
            this.cmbOS.Size = new System.Drawing.Size(143, 21);
            this.cmbOS.TabIndex = 1;
            // 
            // cmbSeverity
            // 
            this.cmbSeverity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSeverity.FormattingEnabled = true;
            this.cmbSeverity.Location = new System.Drawing.Point(84, 43);
            this.cmbSeverity.Name = "cmbSeverity";
            this.cmbSeverity.Size = new System.Drawing.Size(143, 21);
            this.cmbSeverity.TabIndex = 3;
            // 
            // cmbHardware
            // 
            this.cmbHardware.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHardware.FormattingEnabled = true;
            this.cmbHardware.Location = new System.Drawing.Point(79, 47);
            this.cmbHardware.Name = "cmbHardware";
            this.cmbHardware.Size = new System.Drawing.Size(143, 21);
            this.cmbHardware.TabIndex = 7;
            // 
            // lblSummary
            // 
            this.lblSummary.AutoSize = true;
            this.lblSummary.Location = new System.Drawing.Point(10, 51);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.Size = new System.Drawing.Size(50, 13);
            this.lblSummary.TabIndex = 6;
            this.lblSummary.Text = "Summary";
            // 
            // txtSummary
            // 
            this.txtSummary.Location = new System.Drawing.Point(65, 48);
            this.txtSummary.Name = "txtSummary";
            this.txtSummary.Size = new System.Drawing.Size(648, 20);
            this.txtSummary.TabIndex = 7;
            // 
            // txtComment
            // 
            this.txtComment.AcceptsReturn = true;
            this.txtComment.Location = new System.Drawing.Point(13, 19);
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComment.Size = new System.Drawing.Size(377, 179);
            this.txtComment.TabIndex = 0;
            this.txtComment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtComment_KeyDown);
            // 
            // txtReporter
            // 
            this.txtReporter.BackColor = System.Drawing.Color.LightGray;
            this.txtReporter.Location = new System.Drawing.Point(84, 19);
            this.txtReporter.Name = "txtReporter";
            this.txtReporter.ReadOnly = true;
            this.txtReporter.Size = new System.Drawing.Size(203, 20);
            this.txtReporter.TabIndex = 1;
            this.txtReporter.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(41, 20);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(37, 13);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Status";
            // 
            // cmbStatus
            // 
            this.cmbStatus.BackColor = System.Drawing.Color.LightGray;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Enabled = false;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(84, 16);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(202, 21);
            this.cmbStatus.TabIndex = 1;
            // 
            // lstCC
            // 
            this.lstCC.CheckOnClick = true;
            this.lstCC.FormattingEnabled = true;
            this.lstCC.Location = new System.Drawing.Point(84, 74);
            this.lstCC.Name = "lstCC";
            this.lstCC.ScrollAlwaysVisible = true;
            this.lstCC.Size = new System.Drawing.Size(203, 124);
            this.lstCC.TabIndex = 5;
            // 
            // btnInsertBug
            // 
            this.btnInsertBug.Location = new System.Drawing.Point(593, 579);
            this.btnInsertBug.Name = "btnInsertBug";
            this.btnInsertBug.Size = new System.Drawing.Size(68, 23);
            this.btnInsertBug.TabIndex = 7;
            this.btnInsertBug.Text = "Add bug";
            this.btnInsertBug.UseVisualStyleBackColor = true;
            this.btnInsertBug.Click += new System.EventHandler(this.btnInsertBug_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(678, 579);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSummary);
            this.groupBox2.Controls.Add(this.lblSummary);
            this.groupBox2.Controls.Add(this.cmbProduct);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.lblComponent);
            this.groupBox2.Controls.Add(this.cmbComponent);
            this.groupBox2.Controls.Add(this.lblVersion);
            this.groupBox2.Controls.Add(this.cmbVersion);
            this.groupBox2.Location = new System.Drawing.Point(14, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(732, 84);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bug details";
            // 
            // cmbProduct
            // 
            this.cmbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProduct.DropDownWidth = 175;
            this.cmbProduct.FormattingEnabled = true;
            this.cmbProduct.Location = new System.Drawing.Point(65, 21);
            this.cmbProduct.Name = "cmbProduct";
            this.cmbProduct.Size = new System.Drawing.Size(175, 21);
            this.cmbProduct.TabIndex = 1;
            this.cmbProduct.SelectedValueChanged += new System.EventHandler(this.cmbProduct_SelectedValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Product";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtComment);
            this.groupBox1.Location = new System.Drawing.Point(14, 126);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(412, 209);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Description";
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(510, 20);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(202, 20);
            this.txtURL.TabIndex = 5;
            this.txtURL.Text = "http://";
            // 
            // groupStatus
            // 
            this.groupStatus.Controls.Add(this.btnAddCC);
            this.groupStatus.Controls.Add(this.txtCC);
            this.groupStatus.Controls.Add(this.label7);
            this.groupStatus.Controls.Add(this.txtReporter);
            this.groupStatus.Controls.Add(this.lstCC);
            this.groupStatus.Controls.Add(this.cmbAssignedTo);
            this.groupStatus.Controls.Add(this.lblReporter);
            this.groupStatus.Controls.Add(this.lblAssignedTo);
            this.groupStatus.Location = new System.Drawing.Point(440, 126);
            this.groupStatus.Name = "groupStatus";
            this.groupStatus.Size = new System.Drawing.Size(306, 209);
            this.groupStatus.TabIndex = 3;
            this.groupStatus.TabStop = false;
            this.groupStatus.Text = "People";
            // 
            // btnAddCC
            // 
            this.btnAddCC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddCC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.btnAddCC.Location = new System.Drawing.Point(267, 74);
            this.btnAddCC.Name = "btnAddCC";
            this.btnAddCC.Size = new System.Drawing.Size(19, 20);
            this.btnAddCC.TabIndex = 9;
            this.btnAddCC.Text = "+";
            this.btnAddCC.UseVisualStyleBackColor = true;
            this.btnAddCC.Visible = false;
            this.btnAddCC.Click += new System.EventHandler(this.btnAddCC_Click);
            // 
            // txtCC
            // 
            this.txtCC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCC.BackColor = System.Drawing.SystemColors.Window;
            this.txtCC.Location = new System.Drawing.Point(84, 74);
            this.txtCC.Name = "txtCC";
            this.txtCC.Size = new System.Drawing.Size(177, 20);
            this.txtCC.TabIndex = 8;
            this.txtCC.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(56, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "CC";
            // 
            // cmbMilestone
            // 
            this.cmbMilestone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMilestone.FormattingEnabled = true;
            this.cmbMilestone.Location = new System.Drawing.Point(84, 96);
            this.cmbMilestone.Name = "cmbMilestone";
            this.cmbMilestone.Size = new System.Drawing.Size(143, 21);
            this.cmbMilestone.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Milestone";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblURL);
            this.groupBox3.Controls.Add(this.txtURL);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cmbHardware);
            this.groupBox3.Controls.Add(this.cmbOS);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.txtBlock);
            this.groupBox3.Controls.Add(this.txtDependsOn);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(14, 489);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(732, 78);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Other details";
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Location = new System.Drawing.Point(472, 23);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(29, 13);
            this.lblURL.TabIndex = 4;
            this.lblURL.Text = "URL";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Hardware";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "OS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(270, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Block";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(237, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Depends On";
            // 
            // cmbConnections
            // 
            this.cmbConnections.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConnections.FormattingEnabled = true;
            this.cmbConnections.Location = new System.Drawing.Point(510, 9);
            this.cmbConnections.Name = "cmbConnections";
            this.cmbConnections.Size = new System.Drawing.Size(236, 21);
            this.cmbConnections.TabIndex = 0;
            this.cmbConnections.SelectedValueChanged += new System.EventHandler(this.cmbConnections_SelectedValueChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lstAttachments);
            this.groupBox4.Controls.Add(this.btnAddAttachment);
            this.groupBox4.Location = new System.Drawing.Point(14, 341);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(412, 142);
            this.groupBox4.TabIndex = 4;
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
            this.lstAttachments.DataMember = "tblAttachment";
            this.lstAttachments.DataSource = this.dsAttachments;
            this.lstAttachments.EnableHeadersVisualStyles = false;
            this.lstAttachments.Location = new System.Drawing.Point(13, 17);
            this.lstAttachments.MultiSelect = false;
            this.lstAttachments.Name = "lstAttachments";
            this.lstAttachments.ReadOnly = true;
            this.lstAttachments.RowHeadersVisible = false;
            this.lstAttachments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lstAttachments.Size = new System.Drawing.Size(377, 85);
            this.lstAttachments.TabIndex = 0;
            this.lstAttachments.TabStop = false;
            this.lstAttachments.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.lstAttachments_CellDoubleClick);
            this.lstAttachments.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.lstAttachments_CellContentClick);
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
            this.fileNameDataGridViewTextBoxColumn.HeaderText = "FileName";
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
            this.btnAddAttachment.Location = new System.Drawing.Point(282, 112);
            this.btnAddAttachment.Name = "btnAddAttachment";
            this.btnAddAttachment.Size = new System.Drawing.Size(108, 23);
            this.btnAddAttachment.TabIndex = 1;
            this.btnAddAttachment.Text = "Add attachment";
            this.btnAddAttachment.UseVisualStyleBackColor = true;
            this.btnAddAttachment.Click += new System.EventHandler(this.btnAddAttachment_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblStatus);
            this.groupBox5.Controls.Add(this.cmbMilestone);
            this.groupBox5.Controls.Add(this.cmbSeverity);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.cmbPriority);
            this.groupBox5.Controls.Add(this.cmbStatus);
            this.groupBox5.Controls.Add(this.lblPriority);
            this.groupBox5.Controls.Add(this.lblSeverity);
            this.groupBox5.Location = new System.Drawing.Point(440, 341);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(306, 142);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Status";
            // 
            // UCInsertBug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.cmbConnections);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupStatus);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnInsertBug);
            this.Name = "UCInsertBug";
            this.Size = new System.Drawing.Size(761, 613);
            this.Load += new System.EventHandler(this.InsertBug_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupStatus.ResumeLayout(false);
            this.groupStatus.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstAttachments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsAttachments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblReporter;
        private System.Windows.Forms.Label lblComponent;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.Label lblAssignedTo;
        private System.Windows.Forms.Label lblSeverity;
        private System.Windows.Forms.ComboBox cmbComponent;
        private System.Windows.Forms.ComboBox cmbVersion;
        private System.Windows.Forms.ComboBox cmbPriority;
        private System.Windows.Forms.ComboBox cmbAssignedTo;
        private System.Windows.Forms.TextBox txtDependsOn;
        private System.Windows.Forms.TextBox txtBlock;
        private System.Windows.Forms.ComboBox cmbOS;
        private System.Windows.Forms.ComboBox cmbSeverity;
        private System.Windows.Forms.ComboBox cmbHardware;
        private System.Windows.Forms.Label lblSummary;
        private System.Windows.Forms.TextBox txtSummary;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.TextBox txtReporter;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.CheckedListBox lstCC;
        private System.Windows.Forms.Button btnInsertBug;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbConnections;
        private System.Windows.Forms.ComboBox cmbProduct;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.ComboBox cmbMilestone;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView lstAttachments;
        private System.Windows.Forms.Button btnAddAttachment;
        private System.Data.DataSet dsAttachments;
        private System.Data.DataTable dataTable1;
        private System.Data.DataColumn dataColumn1;
        private System.Data.DataColumn dataColumn2;
        private System.Data.DataColumn dataColumn3;
        private System.Data.DataColumn dataColumn4;
        private System.Data.DataColumn dataColumn5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewLinkColumn fileNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnAddCC;
        private System.Windows.Forms.TextBox txtCC;

    }
}
