namespace MyZilla.UI
{
    partial class UCResults
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCResults));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblCountResults = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSaveQuery = new System.Windows.Forms.ToolStripButton();
            this.tbnShow = new System.Windows.Forms.ToolStripButton();
            this.tsbtnRunQuery = new System.Windows.Forms.ToolStripButton();
            this.btnSelectColumns = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPreviewPane = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.btnExportToPDF = new System.Windows.Forms.ToolStripButton();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.LoadingPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.cmBugs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenBugs = new System.Windows.Forms.ToolStripMenuItem();
            this.markBugAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reopenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miResolvedFixed = new System.Windows.Forms.ToolStripMenuItem();
            this.miResolvedOther = new System.Windows.Forms.ToolStripMenuItem();
            this.verifiedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.assignToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.priorityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.severityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportToPdf = new System.Windows.Forms.ToolStripMenuItem();
            this.cSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblResultsRowCount = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lblSummary = new System.Windows.Forms.TextBox();
            this.lblMessageError = new System.Windows.Forms.Label();
            this.lblCRTSign = new System.Windows.Forms.Label();
            this.lblBugId = new System.Windows.Forms.TextBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lblAttachment = new System.Windows.Forms.Label();
            this.dgvAttachments = new System.Windows.Forms.DataGridView();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAttachment = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colCreatedOn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblURLValue = new System.Windows.Forms.TextBox();
            this.lblURL = new System.Windows.Forms.Label();
            this.lblOSValue = new System.Windows.Forms.TextBox();
            this.lblOS = new System.Windows.Forms.Label();
            this.lblHardwareValue = new System.Windows.Forms.TextBox();
            this.lblHardware = new System.Windows.Forms.Label();
            this.lblAssignedToValue = new System.Windows.Forms.TextBox();
            this.lblAssignedTo = new System.Windows.Forms.Label();
            this.lblReporterValue = new System.Windows.Forms.TextBox();
            this.lblReporter = new System.Windows.Forms.Label();
            this.lblSeverityValue = new System.Windows.Forms.TextBox();
            this.lblSeverity = new System.Windows.Forms.Label();
            this.lblPriorityValue = new System.Windows.Forms.TextBox();
            this.lblPriority = new System.Windows.Forms.Label();
            this.lblStatusValue = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblLastModifiedValue = new System.Windows.Forms.TextBox();
            this.lblLastModified = new System.Windows.Forms.Label();
            this.lblCreatedOnValue = new System.Windows.Forms.TextBox();
            this.lblCreatedOn = new System.Windows.Forms.Label();
            this.lblComponentValue = new System.Windows.Forms.TextBox();
            this.lblComponent = new System.Windows.Forms.Label();
            this.lblVersionValue = new System.Windows.Forms.TextBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblProductValue = new System.Windows.Forms.TextBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDescr = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblComments = new System.Windows.Forms.Label();
            this.ucComments1 = new MyZilla.UI.UCComments();
            this.toolStrip1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.LoadingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.cmBugs.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttachments)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Menu;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.lblCountResults,
            this.toolStripSeparator1,
            this.btnSaveQuery,
            this.tbnShow,
            this.tsbtnRunQuery,
            this.btnSelectColumns,
            this.toolStripSeparator3,
            this.btnPreviewPane,
            this.toolStripSeparator4,
            this.btnExportToExcel,
            this.btnExportToPDF});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(775, 23);
            this.toolStrip1.TabIndex = 37;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 23);
            // 
            // lblCountResults
            // 
            this.lblCountResults.AutoSize = false;
            this.lblCountResults.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblCountResults.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCountResults.Margin = new System.Windows.Forms.Padding(0, 4, 0, 2);
            this.lblCountResults.Name = "lblCountResults";
            this.lblCountResults.Size = new System.Drawing.Size(80, 13);
            this.lblCountResults.Text = "Bugs: -";
            this.lblCountResults.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // btnSaveQuery
            // 
            this.btnSaveQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveQuery.Image")));
            this.btnSaveQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveQuery.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.btnSaveQuery.Name = "btnSaveQuery";
            this.btnSaveQuery.Size = new System.Drawing.Size(82, 20);
            this.btnSaveQuery.Text = "Save query";
            this.btnSaveQuery.Click += new System.EventHandler(this.btnSaveQuery_Click);
            // 
            // tbnShow
            // 
            this.tbnShow.Image = ((System.Drawing.Image)(resources.GetObject("tbnShow.Image")));
            this.tbnShow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbnShow.Name = "tbnShow";
            this.tbnShow.Size = new System.Drawing.Size(96, 20);
            this.tbnShow.Text = "Search criteria";
            this.tbnShow.ToolTipText = "Show/Hide search criteria";
            this.tbnShow.Click += new System.EventHandler(this.tbnShow_Click);
            // 
            // tsbtnRunQuery
            // 
            this.tsbtnRunQuery.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbtnRunQuery.BackColor = System.Drawing.SystemColors.Menu;
            this.tsbtnRunQuery.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRunQuery.Image")));
            this.tsbtnRunQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRunQuery.Name = "tsbtnRunQuery";
            this.tsbtnRunQuery.Size = new System.Drawing.Size(77, 20);
            this.tsbtnRunQuery.Text = "Run query";
            this.tsbtnRunQuery.Click += new System.EventHandler(this.btnRunQuery_Click);
            // 
            // btnSelectColumns
            // 
            this.btnSelectColumns.Image = global::MyZilla.UI.Properties.Resources.columns;
            this.btnSelectColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelectColumns.Name = "btnSelectColumns";
            this.btnSelectColumns.Size = new System.Drawing.Size(105, 20);
            this.btnSelectColumns.Text = "Change columns";
            this.btnSelectColumns.Click += new System.EventHandler(this.btnSelectColumns_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 23);
            // 
            // btnPreviewPane
            // 
            this.btnPreviewPane.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnPreviewPane.Image = global::MyZilla.UI.Properties.Resources.show;
            this.btnPreviewPane.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPreviewPane.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.btnPreviewPane.Name = "btnPreviewPane";
            this.btnPreviewPane.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnPreviewPane.Size = new System.Drawing.Size(92, 20);
            this.btnPreviewPane.Text = "Preview pane";
            this.btnPreviewPane.ToolTipText = "Show/Hide preview pane";
            this.btnPreviewPane.Click += new System.EventHandler(this.btnShowHidePreviewPane_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 23);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportToExcel.Image = global::MyZilla.UI.Properties.Resources.btnExcel;
            this.btnExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportToExcel.Margin = new System.Windows.Forms.Padding(2, 1, 0, 2);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(23, 20);
            this.btnExportToExcel.ToolTipText = "Export as Excel file";
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnExportToPDF
            // 
            this.btnExportToPDF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportToPDF.Image = global::MyZilla.UI.Properties.Resources.btnPdf;
            this.btnExportToPDF.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportToPDF.Name = "btnExportToPDF";
            this.btnExportToPDF.Size = new System.Drawing.Size(23, 20);
            this.btnExportToPDF.ToolTipText = "Export as PDF file";
            this.btnExportToPDF.Click += new System.EventHandler(this.btnExportToPDF_Click);
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.IsSplitterFixed = true;
            this.splitContainer5.Location = new System.Drawing.Point(0, 23);
            this.splitContainer5.Name = "splitContainer5";
            this.splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer5.Size = new System.Drawing.Size(775, 645);
            this.splitContainer5.SplitterDistance = 385;
            this.splitContainer5.SplitterWidth = 2;
            this.splitContainer5.TabIndex = 39;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.LightGray;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer1.Panel1.Controls.Add(this.LoadingPanel);
            this.splitContainer1.Panel1.Controls.Add(this.dgvResults);
            this.splitContainer1.Panel1.Controls.Add(this.lblResultsRowCount);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Panel2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.splitContainer1.Size = new System.Drawing.Size(775, 258);
            this.splitContainer1.SplitterDistance = 140;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 39;
            // 
            // LoadingPanel
            // 
            this.LoadingPanel.BackColor = System.Drawing.Color.Transparent;
            this.LoadingPanel.Controls.Add(this.label1);
            this.LoadingPanel.Controls.Add(this.picLoading);
            this.LoadingPanel.Location = new System.Drawing.Point(341, 22);
            this.LoadingPanel.Name = "LoadingPanel";
            this.LoadingPanel.Size = new System.Drawing.Size(143, 68);
            this.LoadingPanel.TabIndex = 71;
            this.LoadingPanel.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 71;
            this.label1.Text = "Loading ...";
            // 
            // picLoading
            // 
            this.picLoading.BackColor = System.Drawing.Color.Transparent;
            this.picLoading.Image = ((System.Drawing.Image)(resources.GetObject("picLoading.Image")));
            this.picLoading.Location = new System.Drawing.Point(47, 3);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(48, 39);
            this.picLoading.TabIndex = 70;
            this.picLoading.TabStop = false;
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AllowUserToOrderColumns = true;
            this.dgvResults.AllowUserToResizeRows = false;
            this.dgvResults.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(2);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.ContextMenuStrip = this.cmBugs;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvResults.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResults.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvResults.Location = new System.Drawing.Point(0, 0);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.ReadOnly = true;
            this.dgvResults.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(2, 2, 0, 0);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResults.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvResults.RowHeadersWidth = 25;
            this.dgvResults.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.ShowCellErrors = false;
            this.dgvResults.ShowCellToolTips = false;
            this.dgvResults.ShowEditingIcon = false;
            this.dgvResults.ShowRowErrors = false;
            this.dgvResults.Size = new System.Drawing.Size(775, 140);
            this.dgvResults.TabIndex = 68;
            this.dgvResults.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvResults_Scroll);
            this.dgvResults.Sorted += new System.EventHandler(this.dgvResults_Sorted);
            this.dgvResults.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResults_RowEnter);
            this.dgvResults.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvResults_CellDoubleClick);
            this.dgvResults.ColumnDisplayIndexChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvResults_ColumnDisplayIndexChanged);
            this.dgvResults.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvResults_CellPainting);
            this.dgvResults.Paint += new System.Windows.Forms.PaintEventHandler(this.dgvResults_Paint);
            this.dgvResults.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvResults_KeyDown);
            this.dgvResults.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.dgvResults_ColumnWidthChanged);
            this.dgvResults.Click += new System.EventHandler(this.dgvResults_Click);
            // 
            // cmBugs
            // 
            this.cmBugs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadToolStripMenuItem,
            this.miOpenBugs,
            this.markBugAsToolStripMenuItem,
            this.assignToToolStripMenuItem,
            this.priorityToolStripMenuItem,
            this.severityToolStripMenuItem,
            this.exportToToolStripMenuItem});
            this.cmBugs.Name = "cmBugs";
            this.cmBugs.Size = new System.Drawing.Size(144, 158);
            this.cmBugs.Opening += new System.ComponentModel.CancelEventHandler(this.cmBugs_Opening);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.reloadToolStripMenuItem.Text = "Reload bug";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // miOpenBugs
            // 
            this.miOpenBugs.Name = "miOpenBugs";
            this.miOpenBugs.Size = new System.Drawing.Size(143, 22);
            this.miOpenBugs.Text = "Open";
            this.miOpenBugs.Click += new System.EventHandler(this.miOpenBugs_Click);
            // 
            // markBugAsToolStripMenuItem
            // 
            this.markBugAsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reopenToolStripMenuItem,
            this.miResolvedFixed,
            this.miResolvedOther,
            this.verifiedToolStripMenuItem,
            this.closedToolStripMenuItem});
            this.markBugAsToolStripMenuItem.Name = "markBugAsToolStripMenuItem";
            this.markBugAsToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.markBugAsToolStripMenuItem.Text = "Mark bug as";
            // 
            // reopenToolStripMenuItem
            // 
            this.reopenToolStripMenuItem.Name = "reopenToolStripMenuItem";
            this.reopenToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.reopenToolStripMenuItem.Text = "Reopened";
            this.reopenToolStripMenuItem.Click += new System.EventHandler(this.miReopenBugs);
            // 
            // miResolvedFixed
            // 
            this.miResolvedFixed.Name = "miResolvedFixed";
            this.miResolvedFixed.Size = new System.Drawing.Size(167, 22);
            this.miResolvedFixed.Text = "Resolved - Fixed";
            this.miResolvedFixed.Click += new System.EventHandler(this.fixedToolStripMenuItem_Click);
            // 
            // miResolvedOther
            // 
            this.miResolvedOther.Name = "miResolvedOther";
            this.miResolvedOther.Size = new System.Drawing.Size(167, 22);
            this.miResolvedOther.Text = "Resolved - Other";
            // 
            // verifiedToolStripMenuItem
            // 
            this.verifiedToolStripMenuItem.Name = "verifiedToolStripMenuItem";
            this.verifiedToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.verifiedToolStripMenuItem.Text = "Verified";
            this.verifiedToolStripMenuItem.Click += new System.EventHandler(this.verifiedToolStripMenuItem_Click);
            // 
            // closedToolStripMenuItem
            // 
            this.closedToolStripMenuItem.Name = "closedToolStripMenuItem";
            this.closedToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.closedToolStripMenuItem.Text = "Closed";
            this.closedToolStripMenuItem.Click += new System.EventHandler(this.closedToolStripMenuItem_Click);
            // 
            // assignToToolStripMenuItem
            // 
            this.assignToToolStripMenuItem.Name = "assignToToolStripMenuItem";
            this.assignToToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.assignToToolStripMenuItem.Text = "Assign To";
            // 
            // priorityToolStripMenuItem
            // 
            this.priorityToolStripMenuItem.Name = "priorityToolStripMenuItem";
            this.priorityToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.priorityToolStripMenuItem.Text = "Priority";
            // 
            // severityToolStripMenuItem
            // 
            this.severityToolStripMenuItem.Name = "severityToolStripMenuItem";
            this.severityToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.severityToolStripMenuItem.Text = "Severity";
            // 
            // exportToToolStripMenuItem
            // 
            this.exportToToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExportToExcel,
            this.mnuExportToPdf,
            this.cSVToolStripMenuItem,
            this.xMLToolStripMenuItem});
            this.exportToToolStripMenuItem.Name = "exportToToolStripMenuItem";
            this.exportToToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.exportToToolStripMenuItem.Text = "Export as";
            // 
            // mnuExportToExcel
            // 
            this.mnuExportToExcel.Image = global::MyZilla.UI.Properties.Resources.btnExcel;
            this.mnuExportToExcel.Name = "mnuExportToExcel";
            this.mnuExportToExcel.Size = new System.Drawing.Size(110, 22);
            this.mnuExportToExcel.Text = "Excel";
            this.mnuExportToExcel.Click += new System.EventHandler(this.mnuExportToExcel_Click);
            // 
            // mnuExportToPdf
            // 
            this.mnuExportToPdf.Image = global::MyZilla.UI.Properties.Resources.btnPdf;
            this.mnuExportToPdf.Name = "mnuExportToPdf";
            this.mnuExportToPdf.Size = new System.Drawing.Size(110, 22);
            this.mnuExportToPdf.Text = "PDF";
            this.mnuExportToPdf.Click += new System.EventHandler(this.mnuExportToPdf_Click);
            // 
            // cSVToolStripMenuItem
            // 
            this.cSVToolStripMenuItem.Name = "cSVToolStripMenuItem";
            this.cSVToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.cSVToolStripMenuItem.Text = "CSV";
            this.cSVToolStripMenuItem.Visible = false;
            // 
            // xMLToolStripMenuItem
            // 
            this.xMLToolStripMenuItem.Name = "xMLToolStripMenuItem";
            this.xMLToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.xMLToolStripMenuItem.Text = "XML";
            this.xMLToolStripMenuItem.Visible = false;
            // 
            // lblResultsRowCount
            // 
            this.lblResultsRowCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblResultsRowCount.AutoSize = true;
            this.lblResultsRowCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultsRowCount.Location = new System.Drawing.Point(3, 423);
            this.lblResultsRowCount.Name = "lblResultsRowCount";
            this.lblResultsRowCount.Size = new System.Drawing.Size(0, 15);
            this.lblResultsRowCount.TabIndex = 32;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.splitContainer3.Panel1.Controls.Add(this.lblSummary);
            this.splitContainer3.Panel1.Controls.Add(this.lblMessageError);
            this.splitContainer3.Panel1.Controls.Add(this.lblCRTSign);
            this.splitContainer3.Panel1.Controls.Add(this.lblBugId);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer3.Size = new System.Drawing.Size(775, 115);
            this.splitContainer3.SplitterDistance = 25;
            this.splitContainer3.SplitterWidth = 1;
            this.splitContainer3.TabIndex = 2;
            // 
            // lblSummary
            // 
            this.lblSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSummary.BackColor = System.Drawing.SystemColors.MenuBar;
            this.lblSummary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSummary.Location = new System.Drawing.Point(16, 5);
            this.lblSummary.Name = "lblSummary";
            this.lblSummary.ReadOnly = true;
            this.lblSummary.Size = new System.Drawing.Size(751, 13);
            this.lblSummary.TabIndex = 74;
            this.lblSummary.Text = "[BugSummary]";
            // 
            // lblMessageError
            // 
            this.lblMessageError.AutoSize = true;
            this.lblMessageError.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblMessageError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessageError.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMessageError.Location = new System.Drawing.Point(277, 5);
            this.lblMessageError.Name = "lblMessageError";
            this.lblMessageError.Size = new System.Drawing.Size(95, 13);
            this.lblMessageError.TabIndex = 76;
            this.lblMessageError.Text = "[Error message]";
            this.lblMessageError.Visible = false;
            // 
            // lblCRTSign
            // 
            this.lblCRTSign.AutoSize = true;
            this.lblCRTSign.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCRTSign.Location = new System.Drawing.Point(3, 3);
            this.lblCRTSign.Name = "lblCRTSign";
            this.lblCRTSign.Size = new System.Drawing.Size(15, 15);
            this.lblCRTSign.TabIndex = 75;
            this.lblCRTSign.Text = "#";
            // 
            // lblBugId
            // 
            this.lblBugId.BackColor = System.Drawing.SystemColors.MenuBar;
            this.lblBugId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblBugId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBugId.Location = new System.Drawing.Point(116, 5);
            this.lblBugId.MaxLength = 8;
            this.lblBugId.Name = "lblBugId";
            this.lblBugId.ReadOnly = true;
            this.lblBugId.Size = new System.Drawing.Size(55, 13);
            this.lblBugId.TabIndex = 73;
            this.lblBugId.Text = "[BugID]";
            this.lblBugId.Visible = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.Color.LightGray;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.AutoScroll = true;
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer2.Panel1.Controls.Add(this.lblAttachment);
            this.splitContainer2.Panel1.Controls.Add(this.dgvAttachments);
            this.splitContainer2.Panel1.Controls.Add(this.lblURLValue);
            this.splitContainer2.Panel1.Controls.Add(this.lblURL);
            this.splitContainer2.Panel1.Controls.Add(this.lblOSValue);
            this.splitContainer2.Panel1.Controls.Add(this.lblOS);
            this.splitContainer2.Panel1.Controls.Add(this.lblHardwareValue);
            this.splitContainer2.Panel1.Controls.Add(this.lblHardware);
            this.splitContainer2.Panel1.Controls.Add(this.lblAssignedToValue);
            this.splitContainer2.Panel1.Controls.Add(this.lblAssignedTo);
            this.splitContainer2.Panel1.Controls.Add(this.lblReporterValue);
            this.splitContainer2.Panel1.Controls.Add(this.lblReporter);
            this.splitContainer2.Panel1.Controls.Add(this.lblSeverityValue);
            this.splitContainer2.Panel1.Controls.Add(this.lblSeverity);
            this.splitContainer2.Panel1.Controls.Add(this.lblPriorityValue);
            this.splitContainer2.Panel1.Controls.Add(this.lblPriority);
            this.splitContainer2.Panel1.Controls.Add(this.lblStatusValue);
            this.splitContainer2.Panel1.Controls.Add(this.lblStatus);
            this.splitContainer2.Panel1.Controls.Add(this.lblLastModifiedValue);
            this.splitContainer2.Panel1.Controls.Add(this.lblLastModified);
            this.splitContainer2.Panel1.Controls.Add(this.lblCreatedOnValue);
            this.splitContainer2.Panel1.Controls.Add(this.lblCreatedOn);
            this.splitContainer2.Panel1.Controls.Add(this.lblComponentValue);
            this.splitContainer2.Panel1.Controls.Add(this.lblComponent);
            this.splitContainer2.Panel1.Controls.Add(this.lblVersionValue);
            this.splitContainer2.Panel1.Controls.Add(this.lblVersion);
            this.splitContainer2.Panel1.Controls.Add(this.lblProductValue);
            this.splitContainer2.Panel1.Controls.Add(this.lblProduct);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer2.Size = new System.Drawing.Size(775, 89);
            this.splitContainer2.SplitterDistance = 268;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 1;
            // 
            // lblAttachment
            // 
            this.lblAttachment.AutoSize = true;
            this.lblAttachment.Location = new System.Drawing.Point(7, 232);
            this.lblAttachment.Name = "lblAttachment";
            this.lblAttachment.Size = new System.Drawing.Size(69, 13);
            this.lblAttachment.TabIndex = 59;
            this.lblAttachment.Text = "Attachments:";
            // 
            // dgvAttachments
            // 
            this.dgvAttachments.AllowUserToAddRows = false;
            this.dgvAttachments.AllowUserToDeleteRows = false;
            this.dgvAttachments.AllowUserToResizeColumns = false;
            this.dgvAttachments.AllowUserToResizeRows = false;
            this.dgvAttachments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAttachments.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvAttachments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAttachments.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAttachments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAttachments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAttachments.ColumnHeadersVisible = false;
            this.dgvAttachments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colAttachment,
            this.colCreatedOn});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAttachments.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAttachments.Location = new System.Drawing.Point(10, 248);
            this.dgvAttachments.MultiSelect = false;
            this.dgvAttachments.Name = "dgvAttachments";
            this.dgvAttachments.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAttachments.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAttachments.RowHeadersVisible = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            this.dgvAttachments.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvAttachments.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.White;
            this.dgvAttachments.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvAttachments.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvAttachments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAttachments.Size = new System.Drawing.Size(145, 33);
            this.dgvAttachments.TabIndex = 58;
            this.dgvAttachments.Visible = false;
            this.dgvAttachments.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAttachments_CellDoubleClick);
            this.dgvAttachments.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAttachments_CellContentClick);
            // 
            // colID
            // 
            this.colID.DataPropertyName = "AttachmentId";
            this.colID.HeaderText = "Id";
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Visible = false;
            this.colID.Width = 10;
            // 
            // colAttachment
            // 
            this.colAttachment.ActiveLinkColor = System.Drawing.Color.Empty;
            this.colAttachment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colAttachment.DataPropertyName = "Description";
            this.colAttachment.HeaderText = "Description";
            this.colAttachment.LinkColor = System.Drawing.Color.Empty;
            this.colAttachment.Name = "colAttachment";
            this.colAttachment.ReadOnly = true;
            this.colAttachment.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.colAttachment.TrackVisitedState = false;
            this.colAttachment.VisitedLinkColor = System.Drawing.Color.Empty;
            // 
            // colCreatedOn
            // 
            this.colCreatedOn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colCreatedOn.DataPropertyName = "Created";
            this.colCreatedOn.HeaderText = "Created on";
            this.colCreatedOn.Name = "colCreatedOn";
            this.colCreatedOn.ReadOnly = true;
            this.colCreatedOn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // lblURLValue
            // 
            this.lblURLValue.BackColor = System.Drawing.SystemColors.Window;
            this.lblURLValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblURLValue.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.lblURLValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblURLValue.Location = new System.Drawing.Point(85, 209);
            this.lblURLValue.Name = "lblURLValue";
            this.lblURLValue.ReadOnly = true;
            this.lblURLValue.Size = new System.Drawing.Size(155, 13);
            this.lblURLValue.TabIndex = 56;
            this.lblURLValue.Text = "[URL]";
            this.lblURLValue.TextChanged += new System.EventHandler(this.lblURLValue_TextChanged);
            this.lblURLValue.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblURLValue_MouseClick);
            // 
            // lblURL
            // 
            this.lblURL.AutoSize = true;
            this.lblURL.Location = new System.Drawing.Point(7, 210);
            this.lblURL.Name = "lblURL";
            this.lblURL.Size = new System.Drawing.Size(32, 13);
            this.lblURL.TabIndex = 55;
            this.lblURL.Text = "URL:";
            // 
            // lblOSValue
            // 
            this.lblOSValue.BackColor = System.Drawing.SystemColors.Window;
            this.lblOSValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblOSValue.Location = new System.Drawing.Point(85, 195);
            this.lblOSValue.Name = "lblOSValue";
            this.lblOSValue.ReadOnly = true;
            this.lblOSValue.Size = new System.Drawing.Size(155, 13);
            this.lblOSValue.TabIndex = 54;
            this.lblOSValue.Text = "[OS]";
            // 
            // lblOS
            // 
            this.lblOS.AutoSize = true;
            this.lblOS.Location = new System.Drawing.Point(7, 195);
            this.lblOS.Name = "lblOS";
            this.lblOS.Size = new System.Drawing.Size(25, 13);
            this.lblOS.TabIndex = 53;
            this.lblOS.Text = "OS:";
            // 
            // lblHardwareValue
            // 
            this.lblHardwareValue.BackColor = System.Drawing.SystemColors.Window;
            this.lblHardwareValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblHardwareValue.Location = new System.Drawing.Point(85, 180);
            this.lblHardwareValue.Name = "lblHardwareValue";
            this.lblHardwareValue.ReadOnly = true;
            this.lblHardwareValue.Size = new System.Drawing.Size(155, 13);
            this.lblHardwareValue.TabIndex = 52;
            this.lblHardwareValue.Text = "[Hardware]";
            // 
            // lblHardware
            // 
            this.lblHardware.AutoSize = true;
            this.lblHardware.Location = new System.Drawing.Point(7, 180);
            this.lblHardware.Name = "lblHardware";
            this.lblHardware.Size = new System.Drawing.Size(56, 13);
            this.lblHardware.TabIndex = 51;
            this.lblHardware.Text = "Hardware:";
            // 
            // lblAssignedToValue
            // 
            this.lblAssignedToValue.BackColor = System.Drawing.SystemColors.Window;
            this.lblAssignedToValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblAssignedToValue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAssignedToValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAssignedToValue.Location = new System.Drawing.Point(85, 157);
            this.lblAssignedToValue.Name = "lblAssignedToValue";
            this.lblAssignedToValue.ReadOnly = true;
            this.lblAssignedToValue.Size = new System.Drawing.Size(155, 13);
            this.lblAssignedToValue.TabIndex = 50;
            this.lblAssignedToValue.Text = "[AssignedTo]";
            this.lblAssignedToValue.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblAssignedToValue_MouseClick);
            // 
            // lblAssignedTo
            // 
            this.lblAssignedTo.AutoSize = true;
            this.lblAssignedTo.Location = new System.Drawing.Point(7, 157);
            this.lblAssignedTo.Name = "lblAssignedTo";
            this.lblAssignedTo.Size = new System.Drawing.Size(65, 13);
            this.lblAssignedTo.TabIndex = 49;
            this.lblAssignedTo.Text = "Assigned to:";
            // 
            // lblReporterValue
            // 
            this.lblReporterValue.BackColor = System.Drawing.SystemColors.Window;
            this.lblReporterValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblReporterValue.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblReporterValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReporterValue.Location = new System.Drawing.Point(85, 143);
            this.lblReporterValue.Name = "lblReporterValue";
            this.lblReporterValue.ReadOnly = true;
            this.lblReporterValue.Size = new System.Drawing.Size(155, 13);
            this.lblReporterValue.TabIndex = 48;
            this.lblReporterValue.Text = "[Reporter]";
            this.lblReporterValue.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lblReporterValue_MouseClick);
            // 
            // lblReporter
            // 
            this.lblReporter.AutoSize = true;
            this.lblReporter.Location = new System.Drawing.Point(7, 143);
            this.lblReporter.Name = "lblReporter";
            this.lblReporter.Size = new System.Drawing.Size(51, 13);
            this.lblReporter.TabIndex = 47;
            this.lblReporter.Text = "Reporter:";
            // 
            // lblSeverityValue
            // 
            this.lblSeverityValue.BackColor = System.Drawing.SystemColors.Window;
            this.lblSeverityValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblSeverityValue.Location = new System.Drawing.Point(85, 121);
            this.lblSeverityValue.Name = "lblSeverityValue";
            this.lblSeverityValue.ReadOnly = true;
            this.lblSeverityValue.Size = new System.Drawing.Size(155, 13);
            this.lblSeverityValue.TabIndex = 46;
            this.lblSeverityValue.Text = "[Severity]";
            // 
            // lblSeverity
            // 
            this.lblSeverity.AutoSize = true;
            this.lblSeverity.Location = new System.Drawing.Point(7, 121);
            this.lblSeverity.Name = "lblSeverity";
            this.lblSeverity.Size = new System.Drawing.Size(48, 13);
            this.lblSeverity.TabIndex = 45;
            this.lblSeverity.Text = "Severity:";
            // 
            // lblPriorityValue
            // 
            this.lblPriorityValue.BackColor = System.Drawing.SystemColors.Window;
            this.lblPriorityValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblPriorityValue.Location = new System.Drawing.Point(85, 107);
            this.lblPriorityValue.Name = "lblPriorityValue";
            this.lblPriorityValue.ReadOnly = true;
            this.lblPriorityValue.Size = new System.Drawing.Size(155, 13);
            this.lblPriorityValue.TabIndex = 44;
            this.lblPriorityValue.Text = "[Priority]";
            // 
            // lblPriority
            // 
            this.lblPriority.AutoSize = true;
            this.lblPriority.Location = new System.Drawing.Point(7, 107);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(41, 13);
            this.lblPriority.TabIndex = 43;
            this.lblPriority.Text = "Priority:";
            // 
            // lblStatusValue
            // 
            this.lblStatusValue.BackColor = System.Drawing.SystemColors.Window;
            this.lblStatusValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblStatusValue.Location = new System.Drawing.Point(85, 92);
            this.lblStatusValue.Name = "lblStatusValue";
            this.lblStatusValue.ReadOnly = true;
            this.lblStatusValue.Size = new System.Drawing.Size(155, 13);
            this.lblStatusValue.TabIndex = 42;
            this.lblStatusValue.Text = "[Status]";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(7, 92);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 41;
            this.lblStatus.Text = "Status:";
            // 
            // lblLastModifiedValue
            // 
            this.lblLastModifiedValue.BackColor = System.Drawing.SystemColors.Window;
            this.lblLastModifiedValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblLastModifiedValue.Location = new System.Drawing.Point(85, 69);
            this.lblLastModifiedValue.Name = "lblLastModifiedValue";
            this.lblLastModifiedValue.ReadOnly = true;
            this.lblLastModifiedValue.Size = new System.Drawing.Size(155, 13);
            this.lblLastModifiedValue.TabIndex = 40;
            this.lblLastModifiedValue.Text = "[LastModified]";
            // 
            // lblLastModified
            // 
            this.lblLastModified.AutoSize = true;
            this.lblLastModified.Location = new System.Drawing.Point(7, 69);
            this.lblLastModified.Name = "lblLastModified";
            this.lblLastModified.Size = new System.Drawing.Size(72, 13);
            this.lblLastModified.TabIndex = 39;
            this.lblLastModified.Text = "Last modified:";
            // 
            // lblCreatedOnValue
            // 
            this.lblCreatedOnValue.BackColor = System.Drawing.SystemColors.Window;
            this.lblCreatedOnValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblCreatedOnValue.Location = new System.Drawing.Point(85, 55);
            this.lblCreatedOnValue.Name = "lblCreatedOnValue";
            this.lblCreatedOnValue.ReadOnly = true;
            this.lblCreatedOnValue.Size = new System.Drawing.Size(155, 13);
            this.lblCreatedOnValue.TabIndex = 38;
            this.lblCreatedOnValue.Text = "[CreatedOn]";
            // 
            // lblCreatedOn
            // 
            this.lblCreatedOn.AutoSize = true;
            this.lblCreatedOn.Location = new System.Drawing.Point(7, 55);
            this.lblCreatedOn.Name = "lblCreatedOn";
            this.lblCreatedOn.Size = new System.Drawing.Size(62, 13);
            this.lblCreatedOn.TabIndex = 37;
            this.lblCreatedOn.Text = "Created on:";
            // 
            // lblComponentValue
            // 
            this.lblComponentValue.BackColor = System.Drawing.SystemColors.Window;
            this.lblComponentValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblComponentValue.Location = new System.Drawing.Point(85, 34);
            this.lblComponentValue.Name = "lblComponentValue";
            this.lblComponentValue.ReadOnly = true;
            this.lblComponentValue.Size = new System.Drawing.Size(155, 13);
            this.lblComponentValue.TabIndex = 36;
            this.lblComponentValue.Text = "[Component]";
            // 
            // lblComponent
            // 
            this.lblComponent.AutoSize = true;
            this.lblComponent.Location = new System.Drawing.Point(7, 34);
            this.lblComponent.Name = "lblComponent";
            this.lblComponent.Size = new System.Drawing.Size(64, 13);
            this.lblComponent.TabIndex = 35;
            this.lblComponent.Text = "Component:";
            // 
            // lblVersionValue
            // 
            this.lblVersionValue.BackColor = System.Drawing.SystemColors.Window;
            this.lblVersionValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblVersionValue.Location = new System.Drawing.Point(85, 20);
            this.lblVersionValue.Name = "lblVersionValue";
            this.lblVersionValue.ReadOnly = true;
            this.lblVersionValue.Size = new System.Drawing.Size(155, 13);
            this.lblVersionValue.TabIndex = 34;
            this.lblVersionValue.Text = "[Version]";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(7, 20);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(45, 13);
            this.lblVersion.TabIndex = 33;
            this.lblVersion.Text = "Version:";
            // 
            // lblProductValue
            // 
            this.lblProductValue.BackColor = System.Drawing.SystemColors.Window;
            this.lblProductValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblProductValue.Location = new System.Drawing.Point(85, 5);
            this.lblProductValue.Name = "lblProductValue";
            this.lblProductValue.ReadOnly = true;
            this.lblProductValue.Size = new System.Drawing.Size(155, 13);
            this.lblProductValue.TabIndex = 32;
            this.lblProductValue.Text = "[Product]";
            // 
            // lblProduct
            // 
            this.lblProduct.AutoSize = true;
            this.lblProduct.Location = new System.Drawing.Point(7, 5);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(47, 13);
            this.lblProduct.TabIndex = 31;
            this.lblProduct.Text = "Product:";
            // 
            // splitContainer4
            // 
            this.splitContainer4.BackColor = System.Drawing.Color.LightGray;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.panel1);
            this.splitContainer4.Panel1.Controls.Add(this.lblDescription);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.AutoScroll = true;
            this.splitContainer4.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer4.Panel2.Controls.Add(this.panel2);
            this.splitContainer4.Panel2.Controls.Add(this.lblComments);
            this.splitContainer4.Size = new System.Drawing.Size(504, 89);
            this.splitContainer4.SplitterDistance = 31;
            this.splitContainer4.SplitterWidth = 3;
            this.splitContainer4.TabIndex = 39;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.lblDescr);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 16);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(504, 15);
            this.panel1.TabIndex = 37;
            // 
            // lblDescr
            // 
            this.lblDescr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDescr.BackColor = System.Drawing.SystemColors.Window;
            this.lblDescr.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lblDescr.Location = new System.Drawing.Point(3, 0);
            this.lblDescr.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.lblDescr.Multiline = true;
            this.lblDescr.Name = "lblDescr";
            this.lblDescr.ReadOnly = true;
            this.lblDescr.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.lblDescr.Size = new System.Drawing.Size(501, 15);
            this.lblDescr.TabIndex = 1;
            // 
            // lblDescription
            // 
            this.lblDescription.BackColor = System.Drawing.SystemColors.Control;
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(0, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(504, 16);
            this.lblDescription.TabIndex = 36;
            this.lblDescription.Text = "Description:";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.ucComments1);
            this.panel2.Location = new System.Drawing.Point(3, 16);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(501, 36);
            this.panel2.TabIndex = 42;
            // 
            // lblComments
            // 
            this.lblComments.BackColor = System.Drawing.SystemColors.Control;
            this.lblComments.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblComments.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComments.Location = new System.Drawing.Point(0, 0);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(504, 16);
            this.lblComments.TabIndex = 40;
            this.lblComments.Text = "Comments:";
            // 
            // ucComments1
            // 
            this.ucComments1.AutoScroll = true;
            this.ucComments1.AutoSize = true;
            this.ucComments1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucComments1.BackColor = System.Drawing.SystemColors.Menu;
            this.ucComments1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ucComments1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucComments1.Location = new System.Drawing.Point(0, 0);
            this.ucComments1.Margin = new System.Windows.Forms.Padding(0);
            this.ucComments1.Name = "ucComments1";
            this.ucComments1.Size = new System.Drawing.Size(501, 36);
            this.ucComments1.TabIndex = 42;
            // 
            // UCResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.Controls.Add(this.splitContainer5);
            this.Controls.Add(this.toolStrip1);
            this.Name = "UCResults";
            this.Size = new System.Drawing.Size(775, 668);
            this.Load += new System.EventHandler(this.ucResults_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.LoadingPanel.ResumeLayout(false);
            this.LoadingPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.cmBugs.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttachments)).EndInit();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel lblCountResults;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSaveQuery;
        private System.Windows.Forms.ToolStripButton tbnShow;
        private System.Windows.Forms.ToolStripButton tsbtnRunQuery;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblResultsRowCount;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label lblMessageError;
        private System.Windows.Forms.TextBox lblBugId;
        private System.Windows.Forms.Label lblCRTSign;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox lblURLValue;
        private System.Windows.Forms.Label lblURL;
        private System.Windows.Forms.TextBox lblOSValue;
        private System.Windows.Forms.Label lblOS;
        private System.Windows.Forms.TextBox lblHardwareValue;
        private System.Windows.Forms.Label lblHardware;
        private System.Windows.Forms.TextBox lblAssignedToValue;
        private System.Windows.Forms.Label lblAssignedTo;
        private System.Windows.Forms.TextBox lblReporterValue;
        private System.Windows.Forms.Label lblReporter;
        private System.Windows.Forms.TextBox lblSeverityValue;
        private System.Windows.Forms.Label lblSeverity;
        private System.Windows.Forms.TextBox lblPriorityValue;
        private System.Windows.Forms.Label lblPriority;
        private System.Windows.Forms.TextBox lblStatusValue;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox lblLastModifiedValue;
        private System.Windows.Forms.Label lblLastModified;
        private System.Windows.Forms.TextBox lblCreatedOnValue;
        private System.Windows.Forms.Label lblCreatedOn;
        private System.Windows.Forms.TextBox lblComponentValue;
        private System.Windows.Forms.Label lblComponent;
        private System.Windows.Forms.TextBox lblVersionValue;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.TextBox lblProductValue;
        private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblComments;
        private System.Windows.Forms.ToolStripButton btnSelectColumns;
        private System.Windows.Forms.Panel panel2;
        private UCComments ucComments1;
        private System.Windows.Forms.Panel LoadingPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picLoading;
        private System.Windows.Forms.ContextMenuStrip cmBugs;
        private System.Windows.Forms.ToolStripMenuItem miOpenBugs;
        private System.Windows.Forms.ToolStripMenuItem markBugAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miResolvedFixed;
        private System.Windows.Forms.ToolStripMenuItem verifiedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reopenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem priorityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.TextBox lblSummary;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox lblDescr;
        private System.Windows.Forms.ToolStripButton btnPreviewPane;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exportToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuExportToExcel;
        private System.Windows.Forms.ToolStripMenuItem mnuExportToPdf;
        private System.Windows.Forms.ToolStripMenuItem cSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnExportToExcel;
        private System.Windows.Forms.ToolStripButton btnExportToPDF;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        public System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.ToolStripMenuItem severityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem assignToToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvAttachments;
        private System.Windows.Forms.Label lblAttachment;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewLinkColumn colAttachment;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCreatedOn;
        private System.Windows.Forms.ToolStripMenuItem miResolvedOther;


    }
}
