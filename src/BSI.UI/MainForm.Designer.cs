namespace MyZilla.UI
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatusInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.btnFormStatus = new System.Windows.Forms.ToolStripSplitButton();
            this.mnuBSI = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.newBugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExportTo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportToPDF = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportToCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportToXML = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.numRefreshCatalogues = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGlobalSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCheckForUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mnuAddBug = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblQuickSearch = new System.Windows.Forms.ToolStripLabel();
            this.txtSearchById = new System.Windows.Forms.ToolStripTextBox();
            this.btnSearchById = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSearch = new System.Windows.Forms.ToolStripLabel();
            this.txtGeneralSearch = new System.Windows.Forms.ToolStripTextBox();
            this.btnGeneralSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNewAdvancedSearchTab = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.lblBugUrl = new System.Windows.Forms.ToolStripLabel();
            this.txtBugURL = new System.Windows.Forms.ToolStripTextBox();
            this.btnOpenLink = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvQueries = new System.Windows.Forms.TreeView();
            this.cmTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miNewConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.miEditQueryInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditQuery = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.miNewFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.miConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.miDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.lblHeader = new System.Windows.Forms.Label();
            this.tBSI = new MyZilla.UI.TabControlExtended();
            this.cmTabs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeThisTab = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAll = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllButThis = new System.Windows.Forms.ToolStripMenuItem();
            this.Welcome = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ucWelcome1 = new MyZilla.UI.UCWelcome();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.btnShowHide = new System.Windows.Forms.Label();
            this.statusBar.SuspendLayout();
            this.mnuBSI.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cmTree.SuspendLayout();
            this.tBSI.SuspendLayout();
            this.cmTabs.SuspendLayout();
            this.Welcome.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblStatusInfo,
            this.pbStatus,
            this.btnFormStatus});
            this.statusBar.Location = new System.Drawing.Point(0, 710);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(1014, 22);
            this.statusBar.TabIndex = 1;
            this.statusBar.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(978, 17);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // lblStatusInfo
            // 
            this.lblStatusInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblStatusInfo.Name = "lblStatusInfo";
            this.lblStatusInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblStatusInfo.Size = new System.Drawing.Size(19, 17);
            this.lblStatusInfo.Text = "...";
            this.lblStatusInfo.Visible = false;
            // 
            // pbStatus
            // 
            this.pbStatus.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pbStatus.Size = new System.Drawing.Size(200, 16);
            this.pbStatus.Visible = false;
            // 
            // btnFormStatus
            // 
            this.btnFormStatus.BackColor = System.Drawing.Color.Transparent;
            this.btnFormStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFormStatus.DropDownButtonWidth = 0;
            this.btnFormStatus.Image = ((System.Drawing.Image)(resources.GetObject("btnFormStatus.Image")));
            this.btnFormStatus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFormStatus.Margin = new System.Windows.Forms.Padding(0);
            this.btnFormStatus.Name = "btnFormStatus";
            this.btnFormStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnFormStatus.Size = new System.Drawing.Size(21, 22);
            this.btnFormStatus.Text = "toolStripSplitButton1";
            this.btnFormStatus.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.btnFormStatus.Click += new System.EventHandler(this.btnFormStatus_Click);
            // 
            // mnuBSI
            // 
            this.mnuBSI.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.searchToolStripMenuItem,
            this.mnuTools,
            this.helpToolStripMenuItem});
            this.mnuBSI.Location = new System.Drawing.Point(0, 0);
            this.mnuBSI.Name = "mnuBSI";
            this.mnuBSI.Size = new System.Drawing.Size(1014, 24);
            this.mnuBSI.TabIndex = 2;
            this.mnuBSI.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newBugToolStripMenuItem,
            this.mnuNewConnection,
            this.toolStripSeparator5,
            this.mnuExportTo,
            this.toolStripMenuItem2,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(35, 20);
            this.mnuFile.Text = "File";
            this.mnuFile.Click += new System.EventHandler(this.mnuFile_Click);
            // 
            // newBugToolStripMenuItem
            // 
            this.newBugToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newBugToolStripMenuItem.Image")));
            this.newBugToolStripMenuItem.Name = "newBugToolStripMenuItem";
            this.newBugToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.newBugToolStripMenuItem.Text = "&New bug";
            this.newBugToolStripMenuItem.Click += new System.EventHandler(this.newBugToolStripMenuItem_Click);
            // 
            // mnuNewConnection
            // 
            this.mnuNewConnection.Image = global::MyZilla.UI.Properties.Resources.connection;
            this.mnuNewConnection.Name = "mnuNewConnection";
            this.mnuNewConnection.Size = new System.Drawing.Size(161, 22);
            this.mnuNewConnection.Text = "New connection";
            this.mnuNewConnection.Click += new System.EventHandler(this.mnuNewConnection_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(158, 6);
            // 
            // mnuExportTo
            // 
            this.mnuExportTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuExportToExcel,
            this.mnuExportToPDF,
            this.mnuExportToCSV,
            this.mnuExportToXML});
            this.mnuExportTo.Name = "mnuExportTo";
            this.mnuExportTo.Size = new System.Drawing.Size(161, 22);
            this.mnuExportTo.Text = "Export as";
            // 
            // mnuExportToExcel
            // 
            this.mnuExportToExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.mnuExportToExcel.Image = global::MyZilla.UI.Properties.Resources.btnExcel;
            this.mnuExportToExcel.Name = "mnuExportToExcel";
            this.mnuExportToExcel.Size = new System.Drawing.Size(110, 22);
            this.mnuExportToExcel.Text = "Excel";
            this.mnuExportToExcel.Click += new System.EventHandler(this.mnuExportToExcel_Click);
            // 
            // mnuExportToPDF
            // 
            this.mnuExportToPDF.Image = global::MyZilla.UI.Properties.Resources.btnPdf;
            this.mnuExportToPDF.Name = "mnuExportToPDF";
            this.mnuExportToPDF.Size = new System.Drawing.Size(110, 22);
            this.mnuExportToPDF.Text = "PDF";
            this.mnuExportToPDF.Click += new System.EventHandler(this.mnuExportToPDF_Click);
            // 
            // mnuExportToCSV
            // 
            this.mnuExportToCSV.Name = "mnuExportToCSV";
            this.mnuExportToCSV.Size = new System.Drawing.Size(110, 22);
            this.mnuExportToCSV.Text = "CSV";
            this.mnuExportToCSV.Visible = false;
            // 
            // mnuExportToXML
            // 
            this.mnuExportToXML.Name = "mnuExportToXML";
            this.mnuExportToXML.Size = new System.Drawing.Size(110, 22);
            this.mnuExportToXML.Text = "XML";
            this.mnuExportToXML.Visible = false;
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(158, 6);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(161, 22);
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem1});
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.searchToolStripMenuItem.Text = "Search";
            // 
            // searchToolStripMenuItem1
            // 
            this.searchToolStripMenuItem1.Image = global::MyZilla.UI.Properties.Resources.advanced_search;
            this.searchToolStripMenuItem1.Name = "searchToolStripMenuItem1";
            this.searchToolStripMenuItem1.Size = new System.Drawing.Size(168, 22);
            this.searchToolStripMenuItem1.Text = "Advanced &search";
            this.searchToolStripMenuItem1.Click += new System.EventHandler(this.searchToolStripMenuItem1_Click);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuConnection,
            this.numRefreshCatalogues,
            this.mnuGlobalSettings});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(44, 20);
            this.mnuTools.Text = "Tools";
            // 
            // mnuConnection
            // 
            this.mnuConnection.Image = ((System.Drawing.Image)(resources.GetObject("mnuConnection.Image")));
            this.mnuConnection.Name = "mnuConnection";
            this.mnuConnection.Size = new System.Drawing.Size(181, 22);
            this.mnuConnection.Text = "Connection Settings";
            this.mnuConnection.Click += new System.EventHandler(this.mnuConnection_Click);
            // 
            // numRefreshCatalogues
            // 
            this.numRefreshCatalogues.Image = ((System.Drawing.Image)(resources.GetObject("numRefreshCatalogues.Image")));
            this.numRefreshCatalogues.Name = "numRefreshCatalogues";
            this.numRefreshCatalogues.Size = new System.Drawing.Size(181, 22);
            this.numRefreshCatalogues.Text = "Refresh catalogues";
            this.numRefreshCatalogues.Click += new System.EventHandler(this.numRefreshCatalogues_Click);
            // 
            // mnuGlobalSettings
            // 
            this.mnuGlobalSettings.Image = ((System.Drawing.Image)(resources.GetObject("mnuGlobalSettings.Image")));
            this.mnuGlobalSettings.Name = "mnuGlobalSettings";
            this.mnuGlobalSettings.Size = new System.Drawing.Size(181, 22);
            this.mnuGlobalSettings.Text = "Global settings";
            this.mnuGlobalSettings.Click += new System.EventHandler(this.mnuGlobalSettings_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCheckForUpdates,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // miCheckForUpdates
            // 
            this.miCheckForUpdates.Name = "miCheckForUpdates";
            this.miCheckForUpdates.Size = new System.Drawing.Size(173, 22);
            this.miCheckForUpdates.Text = "Check for updates";
            this.miCheckForUpdates.Click += new System.EventHandler(this.miCheckForUpdates_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddBug,
            this.toolStripSeparator1,
            this.lblQuickSearch,
            this.txtSearchById,
            this.btnSearchById,
            this.toolStripSeparator4,
            this.btnSearch,
            this.txtGeneralSearch,
            this.btnGeneralSearch,
            this.toolStripSeparator2,
            this.btnNewAdvancedSearchTab,
            this.toolStripSeparator3,
            this.lblBugUrl,
            this.txtBugURL,
            this.btnOpenLink});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1014, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.toolStrip1_KeyUp);
            // 
            // mnuAddBug
            // 
            this.mnuAddBug.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.mnuAddBug.Image = ((System.Drawing.Image)(resources.GetObject("mnuAddBug.Image")));
            this.mnuAddBug.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuAddBug.Name = "mnuAddBug";
            this.mnuAddBug.Size = new System.Drawing.Size(69, 22);
            this.mnuAddBug.Text = "New bug";
            this.mnuAddBug.Click += new System.EventHandler(this.mnuAddBug_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // lblQuickSearch
            // 
            this.lblQuickSearch.Name = "lblQuickSearch";
            this.lblQuickSearch.Size = new System.Drawing.Size(70, 22);
            this.lblQuickSearch.Text = "Search by id:";
            // 
            // txtSearchById
            // 
            this.txtSearchById.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearchById.Name = "txtSearchById";
            this.txtSearchById.Size = new System.Drawing.Size(70, 25);
            this.txtSearchById.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchById_KeyDown);
            // 
            // btnSearchById
            // 
            this.btnSearchById.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSearchById.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchById.Image")));
            this.btnSearchById.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearchById.Name = "btnSearchById";
            this.btnSearchById.Size = new System.Drawing.Size(23, 22);
            this.btnSearchById.ToolTipText = "Seach bug by ID";
            this.btnSearchById.Click += new System.EventHandler(this.btnSearchById_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSearch
            // 
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(44, 22);
            this.btnSearch.Text = "Search:";
            // 
            // txtGeneralSearch
            // 
            this.txtGeneralSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGeneralSearch.Name = "txtGeneralSearch";
            this.txtGeneralSearch.Size = new System.Drawing.Size(100, 25);
            this.txtGeneralSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtGeneralSearch_KeyDown);
            // 
            // btnGeneralSearch
            // 
            this.btnGeneralSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGeneralSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnGeneralSearch.Image")));
            this.btnGeneralSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGeneralSearch.Name = "btnGeneralSearch";
            this.btnGeneralSearch.Size = new System.Drawing.Size(23, 22);
            this.btnGeneralSearch.Text = "Search by keyword";
            this.btnGeneralSearch.Click += new System.EventHandler(this.btnGeneralSearch_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnNewAdvancedSearchTab
            // 
            this.btnNewAdvancedSearchTab.Image = global::MyZilla.UI.Properties.Resources.advanced_search;
            this.btnNewAdvancedSearchTab.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewAdvancedSearchTab.Name = "btnNewAdvancedSearchTab";
            this.btnNewAdvancedSearchTab.Size = new System.Drawing.Size(113, 22);
            this.btnNewAdvancedSearchTab.Text = "Advanced search ";
            this.btnNewAdvancedSearchTab.Click += new System.EventHandler(this.btnNewAdvancedSearchTab_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Margin = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // lblBugUrl
            // 
            this.lblBugUrl.Name = "lblBugUrl";
            this.lblBugUrl.Size = new System.Drawing.Size(29, 22);
            this.lblBugUrl.Text = "Bug:";
            // 
            // txtBugURL
            // 
            this.txtBugURL.AcceptsReturn = true;
            this.txtBugURL.BackColor = System.Drawing.Color.LightGray;
            this.txtBugURL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBugURL.Name = "txtBugURL";
            this.txtBugURL.ReadOnly = true;
            this.txtBugURL.Size = new System.Drawing.Size(265, 25);
            this.txtBugURL.Text = "http://";
            this.txtBugURL.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBugURL_KeyUp);
            // 
            // btnOpenLink
            // 
            this.btnOpenLink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpenLink.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenLink.Image")));
            this.btnOpenLink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenLink.Name = "btnOpenLink";
            this.btnOpenLink.Size = new System.Drawing.Size(23, 22);
            this.btnOpenLink.Text = "Open bug in browser";
            this.btnOpenLink.ToolTipText = "Open bug in browser";
            this.btnOpenLink.Click += new System.EventHandler(this.btnOpenLink_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.LightGray;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.splitContainer1.Panel1.Controls.Add(this.tvQueries);
            this.splitContainer1.Panel1.Controls.Add(this.lblHeader);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.Controls.Add(this.tBSI);
            this.splitContainer1.Size = new System.Drawing.Size(1014, 661);
            this.splitContainer1.SplitterDistance = 197;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 4;
            this.splitContainer1.TabStop = false;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // tvQueries
            // 
            this.tvQueries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvQueries.BackColor = System.Drawing.SystemColors.Window;
            this.tvQueries.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvQueries.ContextMenuStrip = this.cmTree;
            this.tvQueries.HideSelection = false;
            this.tvQueries.ImageIndex = 0;
            this.tvQueries.ImageList = this.imgList;
            this.tvQueries.LabelEdit = true;
            this.tvQueries.Location = new System.Drawing.Point(0, 20);
            this.tvQueries.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.tvQueries.Name = "tvQueries";
            this.tvQueries.SelectedImageIndex = 0;
            this.tvQueries.ShowNodeToolTips = true;
            this.tvQueries.Size = new System.Drawing.Size(197, 641);
            this.tvQueries.TabIndex = 0;
            this.tvQueries.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvQueries_NodeMouseDoubleClick);
            this.tvQueries.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvQueries_AfterCollapse);
            this.tvQueries.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvQueries_AfterLabelEdit);
            this.tvQueries.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvQueries_AfterSelect);
            this.tvQueries.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tvQueries_KeyPress);
            this.tvQueries.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tvQueries_KeyUp);
            this.tvQueries.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvQueries_NodeMouseClick);
            this.tvQueries.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.tvQueries_BeforeLabelEdit);
            this.tvQueries.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvQueries_AfterExpand);
            // 
            // cmTree
            // 
            this.cmTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNewConnection,
            this.miEditConnection,
            this.toolStripSeparator6,
            this.miEditQueryInfo,
            this.miEditQuery,
            this.toolStripSeparator8,
            this.miNewFolder,
            this.miDelete,
            this.toolStripSeparator7,
            this.miConnect,
            this.miDisconnect});
            this.cmTree.Name = "cmTree";
            this.cmTree.Size = new System.Drawing.Size(164, 198);
            this.cmTree.Opening += new System.ComponentModel.CancelEventHandler(this.cmTree_Opening);
            // 
            // miNewConnection
            // 
            this.miNewConnection.Name = "miNewConnection";
            this.miNewConnection.Size = new System.Drawing.Size(163, 22);
            this.miNewConnection.Text = "New Connection";
            this.miNewConnection.Click += new System.EventHandler(this.miNewConnection_Click);
            // 
            // miEditConnection
            // 
            this.miEditConnection.Name = "miEditConnection";
            this.miEditConnection.Size = new System.Drawing.Size(163, 22);
            this.miEditConnection.Text = "Edit Connection";
            this.miEditConnection.Click += new System.EventHandler(this.editConnectionToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(160, 6);
            // 
            // miEditQueryInfo
            // 
            this.miEditQueryInfo.Name = "miEditQueryInfo";
            this.miEditQueryInfo.Size = new System.Drawing.Size(163, 22);
            this.miEditQueryInfo.Text = "Edit query info";
            this.miEditQueryInfo.Click += new System.EventHandler(this.miEditQueryInfo_Click);
            // 
            // miEditQuery
            // 
            this.miEditQuery.Name = "miEditQuery";
            this.miEditQuery.Size = new System.Drawing.Size(163, 22);
            this.miEditQuery.Text = "Edit query";
            this.miEditQuery.Click += new System.EventHandler(this.miEditQuery_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(160, 6);
            // 
            // miNewFolder
            // 
            this.miNewFolder.Name = "miNewFolder";
            this.miNewFolder.Size = new System.Drawing.Size(163, 22);
            this.miNewFolder.Text = "New folder";
            this.miNewFolder.Click += new System.EventHandler(this.miNewFolder_Click);
            // 
            // miDelete
            // 
            this.miDelete.Name = "miDelete";
            this.miDelete.Size = new System.Drawing.Size(163, 22);
            this.miDelete.Text = "Delete";
            this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(160, 6);
            // 
            // miConnect
            // 
            this.miConnect.Name = "miConnect";
            this.miConnect.Size = new System.Drawing.Size(163, 22);
            this.miConnect.Text = "Connect";
            this.miConnect.Click += new System.EventHandler(this.miConnect_Click);
            // 
            // miDisconnect
            // 
            this.miDisconnect.Name = "miDisconnect";
            this.miDisconnect.Size = new System.Drawing.Size(163, 22);
            this.miDisconnect.Text = "Disconnect";
            this.miDisconnect.Click += new System.EventHandler(this.miDisconnect_Click);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "");
            this.imgList.Images.SetKeyName(1, "magnifier.png");
            this.imgList.Images.SetKeyName(2, "battery_connection.png");
            this.imgList.Images.SetKeyName(3, "connection.png");
            this.imgList.Images.SetKeyName(4, "advanced_search.png");
            this.imgList.Images.SetKeyName(5, "connection_settings.png");
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.Location = new System.Drawing.Point(3, 0);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(168, 19);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "Connections";
            // 
            // tBSI
            // 
            this.tBSI.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tBSI.ContextMenuStrip = this.cmTabs;
            this.tBSI.Controls.Add(this.Welcome);
            this.tBSI.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tBSI.Location = new System.Drawing.Point(0, 0);
            this.tBSI.Name = "tBSI";
            this.tBSI.SelectedIndex = 0;
            this.tBSI.ShowToolTips = true;
            this.tBSI.Size = new System.Drawing.Size(814, 661);
            this.tBSI.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tBSI.TabIndex = 0;
            this.tBSI.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tBSI_KeyUp);
            this.tBSI.SelectedIndexChanged += new System.EventHandler(this.tBSI_SelectedIndexChanged);
            // 
            // cmTabs
            // 
            this.cmTabs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeThisTab,
            this.closeAll,
            this.closeAllButThis});
            this.cmTabs.Name = "cmTabs";
            this.cmTabs.Size = new System.Drawing.Size(188, 70);
            // 
            // closeThisTab
            // 
            this.closeThisTab.Name = "closeThisTab";
            this.closeThisTab.Size = new System.Drawing.Size(187, 22);
            this.closeThisTab.Text = "Close this tab";
            this.closeThisTab.Click += new System.EventHandler(this.closeThisTab_Click);
            // 
            // closeAll
            // 
            this.closeAll.Name = "closeAll";
            this.closeAll.Size = new System.Drawing.Size(187, 22);
            this.closeAll.Text = "Close all tabs";
            this.closeAll.Click += new System.EventHandler(this.closeAll_Click);
            // 
            // closeAllButThis
            // 
            this.closeAllButThis.Name = "closeAllButThis";
            this.closeAllButThis.Size = new System.Drawing.Size(187, 22);
            this.closeAllButThis.Text = "Close all tabs but this";
            this.closeAllButThis.Click += new System.EventHandler(this.closeAllButThis_Click);
            // 
            // Welcome
            // 
            this.Welcome.AutoScroll = true;
            this.Welcome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(213)))), ((int)(((byte)(255)))));
            this.Welcome.Controls.Add(this.panel1);
            this.Welcome.Location = new System.Drawing.Point(4, 22);
            this.Welcome.Margin = new System.Windows.Forms.Padding(0);
            this.Welcome.Name = "Welcome";
            this.Welcome.Padding = new System.Windows.Forms.Padding(3);
            this.Welcome.Size = new System.Drawing.Size(806, 635);
            this.Welcome.TabIndex = 0;
            this.Welcome.Text = "Welcome*****";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.ucWelcome1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 629);
            this.panel1.TabIndex = 1;
            // 
            // ucWelcome1
            // 
            this.ucWelcome1.AutoSize = true;
            this.ucWelcome1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ucWelcome1.BackColor = System.Drawing.Color.White;
            this.ucWelcome1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucWelcome1.Location = new System.Drawing.Point(0, 0);
            this.ucWelcome1.Margin = new System.Windows.Forms.Padding(0);
            this.ucWelcome1.Name = "ucWelcome1";
            this.ucWelcome1.Size = new System.Drawing.Size(800, 629);
            this.ucWelcome1.TabIndex = 0;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(552, 25);
            // 
            // btnShowHide
            // 
            this.btnShowHide.BackColor = System.Drawing.SystemColors.MenuBar;
            this.btnShowHide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowHide.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowHide.ImageIndex = 3;
            this.btnShowHide.Location = new System.Drawing.Point(188, 50);
            this.btnShowHide.Margin = new System.Windows.Forms.Padding(0);
            this.btnShowHide.Name = "btnShowHide";
            this.btnShowHide.Size = new System.Drawing.Size(10, 19);
            this.btnShowHide.TabIndex = 2;
            this.btnShowHide.Text = "<";
            this.btnShowHide.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnShowHide.Click += new System.EventHandler(this.btnShowHide_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1014, 732);
            this.Controls.Add(this.btnShowHide);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.mnuBSI);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuBSI;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyZilla";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.LocationChanged += new System.EventHandler(this.MainForm_LocationChanged);
            this.ResizeEnd += new System.EventHandler(this.MainForm_ResizeEnd);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.mnuBSI.ResumeLayout(false);
            this.mnuBSI.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.cmTree.ResumeLayout(false);
            this.tBSI.ResumeLayout(false);
            this.cmTabs.ResumeLayout(false);
            this.Welcome.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.MenuStrip mnuBSI;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuTools;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton mnuAddBug;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.TreeView tvQueries;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel lblQuickSearch;
        private System.Windows.Forms.ToolStripButton btnSearchById;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnNewAdvancedSearchTab;
        private System.Windows.Forms.ContextMenuStrip cmTabs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel lblBugUrl;
        public System.Windows.Forms.ToolStripTextBox txtBugURL;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ContextMenuStrip cmTree;
        private System.Windows.Forms.ToolStripMenuItem miNewFolder;
        private System.Windows.Forms.ToolStripMenuItem miDelete;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusInfo;
        private System.Windows.Forms.ToolStripProgressBar pbStatus;
        private System.Windows.Forms.ToolStripSplitButton btnFormStatus;
        private System.Windows.Forms.ToolStripMenuItem mnuConnection;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel btnSearch;
        private System.Windows.Forms.ToolStripTextBox txtGeneralSearch;
        private System.Windows.Forms.ToolStripButton btnGeneralSearch;
        private System.Windows.Forms.ToolStripMenuItem numRefreshCatalogues;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newBugToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem1;
        private System.Windows.Forms.ToolStripTextBox txtSearchById;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.TabPage Welcome;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.ToolStripMenuItem mnuGlobalSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuNewConnection;
        private System.Windows.Forms.ToolStripMenuItem miNewConnection;
        private System.Windows.Forms.ToolStripMenuItem miConnect;
        private System.Windows.Forms.ToolStripMenuItem miDisconnect;
        private System.Windows.Forms.ToolStripButton btnOpenLink;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem closeThisTab;
        private System.Windows.Forms.ToolStripMenuItem closeAll;
        private System.Windows.Forms.ToolStripMenuItem closeAllButThis;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem miEditQuery;
        private System.Windows.Forms.ToolStripMenuItem miEditQueryInfo;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.Label btnShowHide;
        private System.Windows.Forms.ToolStripMenuItem miEditConnection;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem mnuExportTo;
        private System.Windows.Forms.ToolStripMenuItem mnuExportToExcel;
        private System.Windows.Forms.ToolStripMenuItem mnuExportToPDF;
        private System.Windows.Forms.ToolStripMenuItem mnuExportToCSV;
        private System.Windows.Forms.ToolStripMenuItem mnuExportToXML;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        internal TabControlExtended tBSI;
        private System.Windows.Forms.Panel panel1;
        private UCWelcome ucWelcome1;
        private System.Windows.Forms.ToolStripMenuItem miCheckForUpdates;
    }
}
