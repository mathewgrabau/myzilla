using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Specialized;
using System.Resources;
using System.Threading;
using System.IO;
using System.Diagnostics;

using MyZilla.BL.Interfaces;
using MyZilla.BusinessEntities;
using MyZilla.UI.Properties;
using MyZilla.UI.ConfigItems;

using Tremend.Logging;

namespace MyZilla.UI
{
    public partial class MainForm : Form
    {
        #region Variables

    #if DEBUG
        public WebBrowser wb = new WebBrowser();
    #endif

        public FormAddBug AddBugForm;

        public FormStatus statusForm;

        public FormGlobalSettings GlobalSettingsForm;

        public FormInstanceDictionary _editFormInstanceManager = FormInstanceDictionary.GetInstance () ;

        private UCWelcome welcomeScreen;

        private TDSQueriesTree _qTree = TDSQueriesTree.Instance();

        private TDSettings.GlobalSettingsRow settings;

        private bool _userTreePanelWidthLoaded;

        private AsyncOperationManagerList _asyncOpManager;

        private CatalogueManager _catalogManager ;

        private MyZillaSettingsDataSet _appSettings;

        private int _startupLoadingThreads;

        private bool _userNodeRightClicked;

        private SortedList<int, BackgroundWorker> queryCountsList = new SortedList<int, BackgroundWorker>();

        private bool _formClosed; 

        private int _MAX_NR_OF_TABS = 50;

        private int _indexAdvancedAndSearchTab = -1;

        private bool _maxNrOfBugsExceeded ;

        private Rectangle ScreenSize;

        private int step;

        #endregion

        #region Properties

        public int IndexAdvancedAndSearchTab {
            get {
                return _indexAdvancedAndSearchTab;
            }
            set {
                _indexAdvancedAndSearchTab = value;
            }
        }

        #endregion

        #region Constructor

        public MainForm()
        {
            try
            {

                InitializeComponent();

                _appSettings = MyZillaSettingsDataSet.CreateInstance(Utils.UserAppDataPath);

                ScreenSize = Screen.GetWorkingArea(this);

                settings = _appSettings.GetGlobalSettings(ScreenSize);

                int splasherTop;
                int splasherLeft;

                if (settings.MainFormMaximized)
                {
                    splasherTop = (ScreenSize.Height) / 2;

                    splasherLeft = (ScreenSize.Width) / 2;

                    this.WindowState = FormWindowState.Maximized;  

                }
                else 
                {
                    splasherTop = settings.MainFormTop + settings.MainFormHeight / 2;

                    splasherLeft = settings.MainFormLeft + settings.MainFormWidth / 2;

                }

                SplashManager.Show(typeof(FormSplash), splasherTop, splasherLeft);

                tBSI.OnRemoveTabEventHandler += new RemovingTabEventHandler(tBSI_OnRemoveTab);

                //if application fails to created needed folders, application will be closed
                if (String.IsNullOrEmpty(Utils.UserAppDataPath))
                    this.Close();
                
                //Subscribe to the manager of the async operations
                _asyncOpManager = AsyncOperationManagerList.GetInstance();
                _asyncOpManager.RefreshAsyncOperationListEvent += new EventHandler(asyncOpManager_RefreshAsyncOpListEvent);
                _asyncOpManager.RefreshAsyncOperationListEvent += new EventHandler(_asyncOpManager_SplashRefreshAsyncOpListEvent);
                

                //Subscribe to the catalogue manager
                _catalogManager = CatalogueManager.Instance();

                _catalogManager.CatalogueEvent += new CatalogueManager.CataloguesEventHandler(_catalogManager_CatalogueEvent);

                Utils.FormContainer = this;

//                Splasher.Show(typeof(FormSplash), splasherTop, splasherLeft);
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "MainForm", LoggingCategory.Exception);

                SplashManager.Close();

                MessageBox.Show(ex.Message);
 
                this.Close();
            }

        }

        #endregion

        #region Form Events

        private void MainForm_Load(object sender, EventArgs e)
        {

            try
            {
                //delete existing QueriesTree.xml when running in DEBUG mode
                //#if DEBUG
                //            string xmlFileForQueryTree = Utils.UserAppDataPath + "\\QueriesTree.xml";

                //            if (File.Exists(xmlFileForQueryTree))
                //            {
                //                File.Delete(xmlFileForQueryTree);
                //            }
                //#endif

                

                _qTree.LoadDefaultData(Utils.UserAppDataPath);

                SetUIParameters();

                Utils.LoadTreeWithConnectionNodes(tvQueries);

                this.AddWelcomeScreen();

                this.Text = System.Windows.Forms.Application.ProductName;

#if (DEBUG) 
                this.Text += " " + Application.ProductVersion;
#endif
                LoadTreeNodesByStartUp();
            }
            
            catch (Exception ex)
            {
                MyLogger.Write(ex, "MainForm_Load", LoggingCategory.Exception);
                SplashManager.Close();
                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }

        }

        private void tvQueries_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Utils.ConnectionId = MainForm.GetCurrentUser(e.Node);

            if (e.Button == MouseButtons.Right)
            {
                if (e.Node != null)
                {
                    tvQueries.SelectedNode = e.Node;

                    if (e.Node.Tag != null)
                    {
                        cmTree.Enabled = true;
                        ConfigItems.NodeDescription nodeDescription = (ConfigItems.NodeDescription)e.Node.Tag;
                        miDelete.Visible = true;
                        switch (nodeDescription.TreeNodeType)
                        {
                            case NodeType.Query:
                                miNewFolder.Enabled = false;
                                miConnect.Enabled = false;
                                miDelete.Enabled = true;
                                miEditQuery.Enabled = true;
                                miEditQueryInfo.Enabled = true;
                                miDisconnect.Enabled = false;
                                break;
                            case NodeType.Folder:
                                miNewFolder.Enabled = true;
                                miConnect.Enabled = false;
                                miDelete.Enabled = true;
                                miEditQuery.Enabled = false;
                                miEditQueryInfo.Enabled = false;
                                miDisconnect.Enabled = false;
                                break;
                            case NodeType.Connection:
                                miDelete.Enabled = false;
                                miEditConnection.Enabled = true;
                                miEditQuery.Enabled = false;
                                miEditQueryInfo.Enabled = false;

                                MyZillaSettingsDataSet.ConnectionRow connection = (MyZillaSettingsDataSet.ConnectionRow)nodeDescription.NodeData;
                                if (_catalogManager.AreCataloguesLoaded(connection.ConnectionId))
                                {
                                    miConnect.Enabled = false;
                                    miDisconnect.Enabled = true;
                                    miNewFolder.Enabled = true;
                                }
                                else
                                {
                                    if (e.Node.Nodes.Find("loading", false).GetLength(0) > 0)
                                        miConnect.Enabled = false;
                                    else
                                        miConnect.Enabled = true;

                                    miDisconnect.Enabled = false;
                                    miNewFolder.Enabled = false;
                                }
                                break;
                        }
                        _userNodeRightClicked = true;
                    }
                    else
                    {
                        cmTree.Enabled = false;
                    }
                }
            }

            Utils.ConnectionId = MainForm.GetCurrentUser(e.Node);
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuAddBug_Click(object sender, EventArgs e)
        {
            Utils.OpenNewBugWindow(AddBugForm);
        }

        private void btnFormStatus_Click(object sender, EventArgs e)
        {

            if (statusForm == null)
            {
                if (_asyncOpManager.Count != 0)
                {
                    statusForm = new FormStatus();

                    statusForm.Show();
                }
            }
            else
            {
                if (_asyncOpManager.Count != 0)
                {
                    statusForm.Visible = true;
 
                    statusForm.Activate();
                }
                else
                {
                    statusForm.Close(); 
                }
            }

        }

        private void btnNewAdvancedSearchTab_Click(object sender, EventArgs e)
        {
            Utils.OpenNewAdvancedSearchTab(this);
        }

        private void tvQueries_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (step == 0)
                {
                    ConfigItems.NodeDescription nodeDescr = (ConfigItems.NodeDescription)e.Node.Tag;

                    switch (nodeDescr.TreeNodeType)
                    {
                        case NodeType.Folder:
                            ConfigItems.TDSQueriesTree.FoldersRow folder = (ConfigItems.TDSQueriesTree.FoldersRow)nodeDescr.NodeData;
                            folder.Name = e.Label;
                            break;
                        case NodeType.Query:
                            ConfigItems.TDSQueriesTree.QueriesRow query = (ConfigItems.TDSQueriesTree.QueriesRow)nodeDescr.NodeData;
                            query.Name = e.Label;

                            e.Node.EndEdit(false);

                            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                            timer.Tick += new EventHandler(timer_Tick2);
                            timer.Interval = 10;
                            timer.Tag = e.Node;
                            timer.Start();

                            break;
                    }
                    _qTree.Save();
                }
                else
                {
                    e.Node.EndEdit(false);
                    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                    timer.Tick += new EventHandler(timer_Tick);
                    timer.Interval = 10;
                    timer.Tag = e.Node;
                    timer.Start();
                }

                MyLogger.Write("Step: " + step.ToString() + " " + e.Label.ToString(), "tvQueries_AfterLabelEdit", LoggingCategory.Debug);
            }
            else
            {
                MyLogger.Write(" Step: " + step.ToString() + ": Label is null ", "tvQueries_AfterLabelEdit", LoggingCategory.Debug);
                NodeDescription nodeDescr = (NodeDescription)e.Node.Tag;
                switch (nodeDescr.TreeNodeType)
                {
                    case NodeType.Folder:
                        break;
                    case NodeType.Query:
                        TDSQueriesTree.QueriesRow query = (TDSQueriesTree.QueriesRow)nodeDescr.NodeData;
                        nodeDescr.Format = query.Name + " ({0})";
                        e.Node.Text = String.Format(nodeDescr.Format, query.BugsCount);
                        break;
                }
            }

        }

        void timer_Tick(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = (System.Windows.Forms.Timer)(sender);
            TreeNode node = (TreeNode)timer.Tag;
            timer.Stop();
            node.BeginEdit();
        }

        void timer_Tick2(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = (System.Windows.Forms.Timer)(sender);
            timer.Stop();
            TreeNode node = (TreeNode)timer.Tag;
            NodeDescription nodeDescr = (NodeDescription)node.Tag;
            TDSQueriesTree.QueriesRow query = (TDSQueriesTree.QueriesRow)nodeDescr.NodeData;
            nodeDescr.Format = query.Name + " ({0})";
            node.Text = String.Format(nodeDescr.Format, query.BugsCount);
        }

        private void tvQueries_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            OpenTreeQuery(e.Node);
        }

        private void OpenTreeQuery(TreeNode e)
        {
            if (e.Tag != null)
            {
                if (ValidNROfTabs(e.Name))
                {
                    ConfigItems.NodeDescription nodeDescription = (ConfigItems.NodeDescription)e.Tag;

                    switch (nodeDescription.TreeNodeType)
                    {
                        case NodeType.Query:
                            //to do 
                            if (Utils.GetColumnsListFromCookie(Utils.ConnectionId) == null)
                            {
                                MessageBox.Show(Properties.Messages.GeneratingBugzillaListCookie);
                                return;
                            }

                            NameValueCollection paramSearch;

                            ConfigItems.TDSQueriesTree.QueriesRow query = (ConfigItems.TDSQueriesTree.QueriesRow)nodeDescription.NodeData;

                            paramSearch = _qTree.GetQueryParameters(query.ID);

                            Utils.CreateNewResultsTab(null, paramSearch, tBSI, tvQueries, query, false, true, e.Name);

                            RemoveWelcomeScreen();

                            break;
                    }
                }
            }
        }

        private void miDelete_Click(object sender, EventArgs e)
        {
            DeleteNodeFromTree(tvQueries.SelectedNode);
        }

        private void DeleteNodeFromTree(TreeNode SelectedNode)
        {
            if (SelectedNode != null)
            {
                ConfigItems.NodeDescription nodeDescr = (ConfigItems.NodeDescription)SelectedNode.Tag;
                DialogResult dialogResult;
                switch (nodeDescr.TreeNodeType)
                {
                    case NodeType.Folder:
                        ConfigItems.TDSQueriesTree.FoldersRow folder = (ConfigItems.TDSQueriesTree.FoldersRow)nodeDescr.NodeData;

                        dialogResult = MessageBox.Show(String.Format(Properties.Messages.ConfirmDeleteFolder, SelectedNode.Text), "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {

                            //_qTree.Folders.RemoveFoldersRow(folder);
                            _qTree.MarkFolderAsDeleted(folder);
                            SelectedNode.Remove();
                        }

                        break;
                    case NodeType.Query:

                        dialogResult = MessageBox.Show(String.Format(Properties.Messages.ConfirmDeleteQuery, SelectedNode.Text), "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.Yes)
                        {
                            ConfigItems.TDSQueriesTree.QueriesRow query = (ConfigItems.TDSQueriesTree.QueriesRow)nodeDescr.NodeData;
                            _qTree.Queries.RemoveQueriesRow(query);
                            SelectedNode.Remove();
                        }

                        break;
                }
            }
        }

        private void btnSearchById_Click(object sender, EventArgs e)
        {
            int bugId ;

            try
            {

                // check for valid users
                string validConnection = Utils.CheckIfValidConnection();

                if (!string.IsNullOrEmpty(validConnection))
                {
                    MessageBox.Show(this, validConnection, Messages.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }


                bool result = int.TryParse(txtSearchById.Text, out bugId);

                if (result)
                {
                    _editFormInstanceManager.OpenEditBugFormInstance(Utils.ConnectionId, bugId, null);
                }
                else
                {
                    MessageBox.Show(Properties.Messages.BugIDIsNotValid, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "btnSearchById", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }


        }

        /// <summary>
        /// Key down event for the search bug by id text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchById_KeyDown(object sender, KeyEventArgs e)
        {
            //if ENTER is pressed, start searching
            if (e.KeyCode == Keys.Enter)
                btnSearchById_Click(sender, e);
            HandleNewBugKeyPress(e);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _formClosed = true;

                _qTree.Save();

                TDSettings.GlobalSettingsRow savedGlobalSettings =  _appSettings.GetGlobalSettings();

                settings.ShowLoadingForm = savedGlobalSettings.ShowLoadingForm;

                settings.ConfirmSuccessfullyEditBug  = savedGlobalSettings.ConfirmSuccessfullyEditBug ;

                settings.ShowBugsCount = savedGlobalSettings.ShowBugsCount;

                settings.ReportFilesPath = savedGlobalSettings.ReportFilesPath;  
 
                _appSettings.SaveGlobalSettings(settings);

            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "MainForm_FormClosing", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }

        }

        private void ValidateMaxNROfTabsAllowed() {
            _maxNrOfBugsExceeded = (tBSI.TabPages.Count >= _MAX_NR_OF_TABS);
        }

        private void mnuConnection_Click(object sender, EventArgs e)
        {
            try
            {

                FormConnectionSettings frm = new FormConnectionSettings();

                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "mnuCOnnection_Click", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }


        }

        private void miNewFolder_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode newFolderNode;


                ConfigItems.TDSQueriesTree.FoldersRow folder = _qTree.Folders.NewFoldersRow();
                folder.Name = "New Folder";
                folder.ReadOnly = false;
                folder.UserID = Utils.ConnectionId;
                folder.Expanded = false;

                if (tvQueries.SelectedNode != null)
                {
                    folder.LevelID = tvQueries.SelectedNode.Level;
                    NodeDescription nodeDescr = (NodeDescription)tvQueries.SelectedNode.Tag;

                    if (nodeDescr.TreeNodeType == NodeType.Folder)
                        folder.ParentID = ((ConfigItems.TDSQueriesTree.FoldersRow)(nodeDescr).NodeData).ID;
                    else
                        folder.ParentID = -1;

                    newFolderNode = tvQueries.SelectedNode.Nodes.Add("folder " + folder.ID.ToString(), folder.Name);
                    newFolderNode.Tag = new ConfigItems.NodeDescription(NodeType.Folder, folder);

                }
                else
                {
                    newFolderNode = tvQueries.Nodes.Add("folder " + folder.ID.ToString(), folder.Name);
                    newFolderNode.Tag = new ConfigItems.NodeDescription(NodeType.Folder, folder);
                }

                _qTree.Folders.Rows.Add(folder);
                _qTree.Folders.AcceptChanges();

                //expand selected node
                tvQueries.SelectedNode.Expand();
                tvQueries.SelectedNode = newFolderNode;
                _qTree.Save();
                newFolderNode.BeginEdit();
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "miNewFolder_Click", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }

        }
        
        private void tvQueries_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (step == 0)
            {
                try
                {
                    ConfigItems.NodeDescription nodeDescription = (ConfigItems.NodeDescription)e.Node.Tag;

                    if (nodeDescription != null)
                    {
                        switch (nodeDescription.TreeNodeType)
                        {
                            case NodeType.Folder:
                            //    ConfigItems.QueryTreeDataSet.FoldersRow folder = (ConfigItems.QueryTreeDataSet.FoldersRow)nodeDescription.NodeData;
                            //    if (folder.ReadOnly)
                            //        e.CancelEdit = true;
                                break;
                            case NodeType.Query:
                                e.Node.Text = ((TDSQueriesTree.QueriesRow)nodeDescription.NodeData).Name;
                                e.CancelEdit = true;
                                step = 1;
                                //e.Node.BeginEdit();
                                //return;
                                //e.Node.EndEdit(false);
                                tvQueries_AfterLabelEdit(sender, new NodeLabelEditEventArgs(e.Node, e.Node.Text));
                                break;
                            case NodeType.Connection:
                                e.CancelEdit = true;
                                break;
                            default:
                                e.CancelEdit = true;
                                break;
                        }
                    }
                    else {
                        e.CancelEdit = true;
                    }
                }
                catch (Exception ex)
                {
                    MyLogger.Write(ex, "tvQueries_BeforeLabelEdit", LoggingCategory.Exception);

                    MessageBox.Show(this, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else {
                step = 0;
            }

            MyLogger.Write("Step = " + step.ToString() + ": " + e.Node.ToString(), "tvQueries_BeforeLabelEdit", LoggingCategory.Debug);
        }

        public bool ValidNROfTabs(string tabKey) {
            if (_maxNrOfBugsExceeded && (!tBSI.TabPages.ContainsKey(tabKey)))
            {
                MessageBox.Show(Properties.Messages.MaxNrOfTabsExceeded, "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return true;
            }
            else
                return true;
        }

        private void btnGeneralSearch_Click(object sender, EventArgs e)
        {
            if (ValidNROfTabs("query"))
            {
                // check for valid users
                string validConnection = Utils.CheckIfValidConnection();

                if (!string.IsNullOrEmpty(validConnection))
                {
                    MessageBox.Show(this, validConnection, Messages.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                BackgroundWorker bkgGeneralSearch = new BackgroundWorker();

                bkgGeneralSearch.DoWork += new DoWorkEventHandler(bkgGeneralSearch_DoWork);

                bkgGeneralSearch.ProgressChanged += new ProgressChangedEventHandler(bkgGeneralSearch_ProgressChanged);

                bkgGeneralSearch.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgGeneralSearch_RunWorkerCompleted);

                bkgGeneralSearch.WorkerReportsProgress = true;

                bkgGeneralSearch.WorkerSupportsCancellation = true;

                bkgGeneralSearch.RunWorkerAsync(txtGeneralSearch.Text);

            }
        }

        private void numRefreshCatalogues_Click(object sender, EventArgs e)
        {
            try
            {

                if (_catalogManager.Count() > 0)
                {
                    // show loading form.
                    Utils.ActivateLoadingForm();

                    _catalogManager = CatalogueManager.Instance();

                    _catalogManager.RefreshCatalogues();

                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "numRefreshCatalogues_Click", LoggingCategory.Exception);

                MessageBox.Show(this, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void mnuGlobalSettings_Click(object sender, EventArgs e)
        {
            try
            {
                if (GlobalSettingsForm == null)
                {
                    FormGlobalSettings frm = new FormGlobalSettings();

                    frm.Show();

                    GlobalSettingsForm = frm;
                }
                else
                {
                    GlobalSettingsForm.Activate(); 
                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "mnuGlobalSettings_Click", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }

        }

        private void mnuNewConnection_Click(object sender, EventArgs e)
        {
            Utils.OpenNewConnectionWindow();
        }

        private void btnOpenLink_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBugURL.Text.Length>7)
                    System.Diagnostics.Process.Start(txtBugURL.Text);
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "btnOpenLink_Click", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }

        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            try
            {
                if (_userTreePanelWidthLoaded)
                    settings.TreePanelWidth = this.splitContainer1.SplitterDistance;

                btnShowHide.Left = splitContainer1.SplitterDistance - btnShowHide.Width;
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "splitContainer1_SplitterMoved", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }

        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            if (_userTreePanelWidthLoaded && settings != null)
            {
                settings.MainFormWidth = this.Width;
                settings.MainFormHeight = this.Height;
            }
        }

        private void tvQueries_AfterExpand(object sender, TreeViewEventArgs e)
        {
            try
            {
                MainForm.ModifyFolderExpandedState(e.Node, e.Action);
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "tvQueries_AfterExpand", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }

        }

        private void LoadTreeNodesByStartUp()
        {

            Utils.ConnectionId = MainForm.GetCurrentUser(tvQueries.SelectedNode);

            bool activeUsers = false;

            foreach (TreeNode connNode in tvQueries.Nodes)
            {
                NodeDescription descr = (NodeDescription)connNode.Tag;
                if (descr != null)
                {
                    MyZillaSettingsDataSet.ConnectionRow conn = (MyZillaSettingsDataSet.ConnectionRow)descr.NodeData;
                    if (conn.ActiveUser)
                    {
                        activeUsers = true;
                        Interlocked.Increment(ref _startupLoadingThreads);
                        StartConnectingUser(conn.ConnectionId, connNode);

                        //test
                        BuildAsyncProcessForGenerateColumnListCookie(conn.ConnectionId);
                    }
                }
            }

            if (!activeUsers) {
                CheckForNewMyZillaVersion(false);
            }


            if (_startupLoadingThreads == 0)
            {
                SplashManager.Close();
            }

        }

        private void tvQueries_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            try
            {
                MainForm.ModifyFolderExpandedState(e.Node, e.Action);
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "tvQueries_AfterCollapse", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }

        }

        private void miNewConnection_Click(object sender, EventArgs e)
        {
            Utils.OpenNewConnectionWindow();            
        }

        private void miConnect_Click(object sender, EventArgs e)
        {
            try
            {
                StartConnectingUser(Utils.ConnectionId, tvQueries.SelectedNode);
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "miConnect_Click", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }

        }

        private void miDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                // cancel the background worker that load query counts
                if (this.queryCountsList.ContainsKey(Utils.ConnectionId))
                {
                    int indexKey = this.queryCountsList.IndexOfKey(Utils.ConnectionId);

                    BackgroundWorker bkgWorkerCount = this.queryCountsList.Values[indexKey];

                    if (bkgWorkerCount != null)
                    {
                        bkgWorkerCount.CancelAsync(); 
                    }

                }

                DisconnectUser(Utils.ConnectionId);
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "miDisconnect_Click", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }

        }

        private void newBugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.OpenNewBugWindow(AddBugForm);
        }

        private void searchToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            btnNewAdvancedSearchTab_Click(null, null);
        }

        private void cmTree_Opening(object sender, CancelEventArgs e)
        {
            if (tvQueries.Nodes.Count == 0)
            {
                miNewFolder.Enabled = false;
                miConnect.Enabled = false;
                miDisconnect.Enabled = false;
                miEditQuery.Enabled = false;
                miEditQueryInfo.Enabled = false;
                miDelete.Enabled = false;
                miNewConnection.Enabled = true;
            }
            else
            {
                if (!_userNodeRightClicked)
                {
                    //leave only "New Connection" item enabled
                    for (int i = 0; i < cmTree.Items.Count; i++)
                        cmTree.Items[i].Enabled = false;

                    cmTree.Items[0].Enabled = true;
                }
                _userNodeRightClicked = false;
            }
        }

        private void closeThisTab_Click(object sender, EventArgs e)
        {
            tBSI.SelectedTab.Dispose();
        }

        private void closeAll_Click(object sender, EventArgs e)
        {
            foreach (TabPage tp in tBSI.TabPages)
            {
                //if (tp.Name!="Welcome")
                tp.Dispose();
            }
            ValidateMaxNROfTabsAllowed();
        }

        private void closeAllButThis_Click(object sender, EventArgs e)
        {
            foreach (TabPage tp in tBSI.TabPages)
            {
                if (tp != tBSI.SelectedTab)// && (tp.Name != "Welcome"))
                    tp.Dispose();
            }
            ValidateMaxNROfTabsAllowed();
        }

        private void tvQueries_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Utils.ConnectionId = MainForm.GetCurrentUser(e.Node);
        }

        private void miEditQuery_Click(object sender, EventArgs e)
        {
            if (tvQueries.SelectedNode != null)
            {
                if (ValidNROfTabs(tvQueries.SelectedNode.Name))
                {
                    TreeNode queryNodeToBeEdited = tvQueries.SelectedNode;

                    ConfigItems.NodeDescription nodeDescription = (ConfigItems.NodeDescription)queryNodeToBeEdited.Tag;

                    if (Utils.GetColumnsListFromCookie(Utils.ConnectionId) == null)
                    {
                        MessageBox.Show(Properties.Messages.GeneratingBugzillaListCookie);
                        return;
                    }

                    NameValueCollection paramSearch;

                    ConfigItems.TDSQueriesTree.QueriesRow query = (ConfigItems.TDSQueriesTree.QueriesRow)nodeDescription.NodeData;

                    paramSearch = _qTree.GetQueryParameters(query.ID);

                    Utils.CreateNewResultsTab(null, paramSearch, tBSI, tvQueries, query, true, true, false, queryNodeToBeEdited.Name);

                    RemoveWelcomeScreen();
                }
            }
        }

        private void miEditQueryInfo_Click(object sender, EventArgs e)
        {
            TreeNode queryNode = tvQueries.SelectedNode;

            if (queryNode != null)
            {
                ConfigItems.NodeDescription nodeDescription = (ConfigItems.NodeDescription)queryNode.Tag;

                TreeNode folderNode = queryNode.Parent;

                FormQueryName frm = new FormQueryName(Utils.ConnectionId, tvQueries, folderNode);

                TDSQueriesTree.QueriesRow query = (TDSQueriesTree.QueriesRow)nodeDescription.NodeData;

                frm.QueryName = query.Name;

                frm.QueryDescription = query.Description;

                DialogResult result = frm.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    query.Name = frm.QueryName;

                    query.Description = frm.QueryDescription;

                    queryNode.ToolTipText = frm.QueryDescription;

                    tvQueries_AfterLabelEdit(this, new NodeLabelEditEventArgs(queryNode, query.Name));
                }
            }
        }

        private void btnShowHide_Click(object sender, EventArgs e)
        {

            splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            if (splitContainer1.Panel1Collapsed)
            {
                btnShowHide.Text = ">";
                btnShowHide.Left = 0;
                tBSI.Width -= btnShowHide.Width;
                tBSI.Left = btnShowHide.Width;
            }
            else
            {
                tBSI.Left = 0;
                btnShowHide.Text = "<";
                tBSI.Width += btnShowHide.Width;
                btnShowHide.Left = splitContainer1.SplitterDistance - btnShowHide.Width;
            }
        }

        private void editConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FormConnectionSettings frm = new FormConnectionSettings(Utils.ConnectionId);

                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout frm = new FormAbout();

            frm.ShowDialog();
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            HandleNewBugKeyPress(e);
        }

        private void HandleNewBugKeyPress(KeyEventArgs Key)
        {
            if (Key.Control & Key.KeyCode == Keys.N)
                Utils.OpenNewBugWindow(AddBugForm);
        }

        private void tvQueries_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                DeleteNodeFromTree(tvQueries.SelectedNode);
            else if (e.KeyCode == Keys.Enter) {
                if (tvQueries.SelectedNode!=null)
                    OpenTreeQuery(tvQueries.SelectedNode);
            }
            else if (e.KeyCode == Keys.F2 && (tvQueries.SelectedNode != null)) {
                TreeNode queryNode = tvQueries.SelectedNode;
                ConfigItems.NodeDescription nodeDescription = (ConfigItems.NodeDescription)queryNode.Tag;
                if (nodeDescription.TreeNodeType == NodeType.Folder) {
                    queryNode.BeginEdit();
                }
            }
            else
            {
                HandleNewBugKeyPress(e);
            }
        }

        private void toolStrip1_KeyUp(object sender, KeyEventArgs e)
        {
            HandleNewBugKeyPress(e);
        }

        private void txtBugURL_KeyUp(object sender, KeyEventArgs e)
        {
            HandleNewBugKeyPress(e);
        }

        private void MainForm_LocationChanged(object sender, EventArgs e)
        {
            if (_userTreePanelWidthLoaded && settings != null)
            {
                if (this.Top > 0)
                {
                    settings.MainFormTop = this.Top;
                }
                if (this.Left > 0)
                {
                    settings.MainFormLeft = this.Left;
                }
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (_userTreePanelWidthLoaded && settings != null)
            {
                if (this.WindowState == FormWindowState.Maximized)
                    settings.MainFormMaximized = true;
                else
                    settings.MainFormMaximized = false;
            }
        }

        private void tBSI_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Control) && ((e.KeyCode == Keys.F4) || (e.KeyCode == Keys.W)))
            {
                if (tBSI.SelectedTab != null)
                    tBSI.SelectedTab.Dispose();
            }
            else if (!e.Control && e.KeyCode == Keys.F5)
            {
                if (tBSI.SelectedTab != null && tBSI.SelectedTab.Controls.ContainsKey("UCResults"))
                {
                    UCResults ucResults = (UCResults)tBSI.SelectedTab.Controls["UCResults"];
                    ucResults.ExecuteQuery();
                }
            }
            HandleNewBugKeyPress(e);
        }

        private void tBSI_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidateMaxNROfTabsAllowed();

            TabPage selectedTab = tBSI.SelectedTab;
            if (selectedTab != null)
            {
                TreeNode[] nodes = tvQueries.Nodes.Find("query " + selectedTab.Name, true);
                if (nodes.GetLength(0) == 1)
                    tvQueries.SelectedNode = nodes[0];

                Control[] results = selectedTab.Controls.Find("ucResults", false);
                if ((results != null) && (results.GetLength(0) > 0))
                {
                    UCResults resultsCtrl = (UCResults)results[0];
                    txtBugURL.Text = Utils.GetBugURL(Utils.ConnectionId, resultsCtrl.SelectedBugId.ToString());
                }
            }
        }

        private void tBSI_OnRemoveTab(object sender, int indx)
        {
            SelectNextTab();

            Control[] results = tBSI.TabPages[indx].Controls.Find("ucResults", true);
            if (results.GetLength(0) == 1)
            {
                UCResults ucResults = (UCResults)results[0];
                ucResults.IsClosing = true;
            }
        }

        private void mnuFile_Click(object sender, EventArgs e)
        {
            if (tBSI.SelectedTab != null)
            {
                //enable export from the file->menu when any grid with bugs is opened and focus
                //disable export from the file->menu when no grid with bugs is opened and focus
                Control[] results = tBSI.SelectedTab.Controls.Find("ucResults", true);

                if (results != null)
                    mnuExportTo.Enabled = results.GetLength(0) == 1;
            }
        }

        #endregion

        #region Private Methods




        /// <summary>
        /// Sets the automatically saved values for:
        /// 1.Form width and height
        /// 2.Tree view width
        /// </summary>
        private void SetUIParameters()
        {

            _appSettings = MyZillaSettingsDataSet.GetInstance();

            settings = _appSettings.GetGlobalSettings(ScreenSize);

            this.Height = settings.MainFormHeight;

            this.Width = settings.MainFormWidth;

            this.Left = settings.MainFormLeft;

            this.Top = settings.MainFormTop;

            this.splitContainer1.SplitterDistance = settings.TreePanelWidth;

            //this.StartPosition = FormStartPosition.CenterScreen;

            _userTreePanelWidthLoaded = true;
        }

        private static int GetCurrentUser(TreeNode node) {
            int connectionID = -1;

            if (node != null)
            {
                for (int i = node.Level; i >= 1; i--)
                {
                    node = node.Parent;
                }

                connectionID = ((TDSettings.ConnectionRow)((NodeDescription)node.Tag).NodeData).ConnectionId;
            }

            return connectionID;
        }

        /// <summary>
        /// Add UCWelcome screen in the control collection of <c>this.splitContainer1.Panel2</c>
        /// Focus on the welcome screen.
        /// Keep a reference to the new added control, that will be used later.
        /// The reference are stored in the global variable <c>this.welcomeScreen</c>
        /// </summary>
        private void AddWelcomeScreen()
        {
            TabPage welcomeTab = tBSI.TabPages["Welcome"];
#if DEBUG

            if (welcomeTab != null)
            {
                welcomeTab.Controls.Add(wb);
                wb.Visible = true;
                wb.ScriptErrorsSuppressed = true;
                wb.Dock = DockStyle.Fill;
                wb.BringToFront();
            }
#else
            this.welcomeScreen = new UCWelcome();

            if (welcomeTab != null) {

                //webBrowser1.Visible = false;
                //panel1.Controls.Add(this.welcomeScreen);
                
                //welcomeScreen.AutoSize = true;
                //welcomeScreen.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                //welcomeScreen.Dock = DockStyle.Top;
                //panel1.AutoScroll = true;
                //welcomeScreen.BringToFront();
            }

            // focus on welcome screen, sending back the tab control.
            this.splitContainer1.Panel2.Controls[0].SendToBack();
#endif
            //TabControlEx ntc = new TabControlEx();
            //ntc.Width = 200;
            //ntc.Height = 300;
            //ntc.TabPages.Add("asdasd");
            //ntc.TabPages.Add("23 45 56");

            //tBSI.TabPages[0].Controls.Add(ntc);
            //ntc.BringToFront();
        }

        /// <summary>
        /// Remove the welcome screen from the control collection of <c>this.splitContainer1.Panel2</c>
        /// </summary>
        public void RemoveWelcomeScreen()
        {
            if (this.welcomeScreen != null)
            {
                this.splitContainer1.Panel2.Controls.Remove(this.welcomeScreen);
            }
            this.welcomeScreen = null;
        }

        private void StartConnectingUser(int UserID, TreeNode UserNode)
        {
            try
            {
                AddLoadingNode(UserNode);

                if (!_catalogManager.AreCataloguesLoaded(UserID))
                {
                    _catalogManager.LoadCataloguesForUser(UserID);
                    //BuildAsyncProcessForGenerateColumnListCookie(UserID);
                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "StartConnectingUser", LoggingCategory.Exception); 

                MessageBox.Show(ex.Message); 
            }

        }

        private void AddLoadingNode(TreeNode UserNode)
        {
            if ((UserNode != null) && (UserNode.Level == 0))
            {
                tvQueries.SelectedNode = UserNode;
                UserNode.Nodes.Clear();  
                UserNode.Nodes.Add("loading", "Loading ...");
                UserNode.Expand();
            }
        }

        private void RemoveConnectingNode(int UserID)
        {
            //add condition on User !!!!!!!!
            TreeNode[] nodes = tvQueries.Nodes.Find("User " + UserID.ToString(), false);
            if (nodes.GetLength(0) == 1)
            {
                nodes = nodes[0].Nodes.Find("loading", false);
                if (nodes.GetLength(0)==1)
                    nodes[0].Remove();
            }
        }

        private void LoginFailedMessage(int connectionID, string Message)
        {
            TreeNode[] nodes = tvQueries.Nodes.Find("User " + connectionID.ToString(), false);
            if (nodes.GetLength(0) == 1)
            {
                nodes = nodes[0].Nodes.Find("loading", false);

                if (nodes != null && nodes.GetLength(0) > 0)
                {
                    nodes[0].Name = "error";
                    if (!string.IsNullOrEmpty(Message))
                        nodes[0].Text = Message;
                    else
                        nodes[0].Text = Messages.InvalidLogin;
                }
            }

            #region menu option refresh

            miEditConnection.Enabled = true;
            miConnect.Enabled = true;

            #endregion
        }


        private bool IsTreeConnectionNodeLoaded(int connectionID)
        {
            TreeNode[] nodes = tvQueries.Nodes.Find("User " + connectionID.ToString(), false);
            if (nodes.Length == 1)
            {
                return (nodes[0].Nodes.Count > 0);
            }
            else
            {
                return true;
            }
        }

        private void DisconnectUser(int UserID)
        {
            TreeNode[] nodes = tvQueries.Nodes.Find("User " + UserID.ToString(), false);

            if (nodes.GetLength(0)==1){

                TreeNode userNode = nodes[0];

                userNode.Nodes.Clear();

                _catalogManager.RemoveCataloguesForConnection(UserID);

                Utils.RemoveCookieCollectionForCurrentUser();

                //QueriesTree.Instance().RemoveUserItems(UserID);

                for (int i = tBSI.TabPages.Count - 1; i >= 0; i--)
                {
                    int connID;
                    if (tBSI.TabPages[i].Tag != null)
                    {
                        bool result = Int32.TryParse(tBSI.TabPages[i].Tag.ToString(), out connID);
                        if (result && (connID == UserID))
                        {
                            tBSI.TabPages[i].Dispose();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Start new secondary threads for loading depandent catalogues, columns for search...
        /// </summary>
        [Obsolete]
        private void LoadAndCacheData()
        {
            TDSQueriesTree qTree = TDSQueriesTree.Instance();

            //qTree.LoadDefaultData(Utils.UserAppDataPath);

            for (int iCatalogue = 0; iCatalogue < _catalogManager.Count(); iCatalogue++)
            {
                Catalogues cataloguesPerUser = _catalogManager.GetCataloguesByIndex(iCatalogue);

                //_catalogManager.LoadCompAndVersionCatalogues (cataloguesPerUser);

                //GenerateColumnListCookie(cataloguesPerUser.ConnectionID );

            }

        }

        private void SetTreeNodesExpandState(TreeNodeCollection Nodes)
        {
            NodeDescription nodeDescr;

            foreach (TreeNode node in Nodes)
            {
                nodeDescr = (NodeDescription)node.Tag;

                switch (nodeDescr.TreeNodeType)
                {
                    case NodeType.Folder:
                        TDSQueriesTree.FoldersRow folder = (TDSQueriesTree.FoldersRow)nodeDescr.NodeData;
                        if (folder.Expanded) node.Expand();

                        break;
                    case NodeType.Connection:
                        node.Expand();
                        break;
                }
                if (node.Nodes.Count > 0)
                    SetTreeNodesExpandState(node.Nodes);
            }

        }

        private static void ModifyFolderExpandedState(TreeNode Node, TreeViewAction Action)
        {
            ConfigItems.NodeDescription nodeDescr = (ConfigItems.NodeDescription)Node.Tag;

            switch (nodeDescr.TreeNodeType)
            {
                case NodeType.Folder:
                    ConfigItems.TDSQueriesTree.FoldersRow folder = (ConfigItems.TDSQueriesTree.FoldersRow)nodeDescr.NodeData;

                    folder.Expanded = (Action == TreeViewAction.Expand ? true : false);

                    break;
            }
        }

        private int GetBugsCountForQuery(int connectionId, int queryId)
        {

            NameValueCollection searchParams = _qTree.GetQueryParameters(queryId);

            IBugBSI bugProvider = (IBugBSI)BLControllerFactory.GetRegisteredConcreteFactory(connectionId);

            List<MyZilla.BusinessEntities.Bug> bugsFound = bugProvider.SearchBugs(searchParams);

            return bugsFound.Count;
        }





        #endregion

        #region Asynchronious General Search

        void bkgGeneralSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // check the status of the async operation.

            if (e.Error != null)
            {

                string errorMessage = Messages.ErrSearching  + Environment.NewLine + e.Error.Message ;

                MessageBox.Show( this, errorMessage, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);      

            }

            // status OK
            if (!e.Cancelled && e.Error == null)
            {
                ArrayList al = e.Result as ArrayList;

                NameValueCollection searchParams = al[0] as NameValueCollection ;

                List<MyZilla.BusinessEntities.Bug> bugsFound = al[1] as List<MyZilla.BusinessEntities.Bug>;

                _indexAdvancedAndSearchTab--;

                Utils.CreateNewResultsTab(bugsFound, searchParams, tBSI, tvQueries, null, false, false, "query " + _indexAdvancedAndSearchTab.ToString());

                this.RemoveWelcomeScreen();

            }

        }

        void bkgGeneralSearch_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker bkgWork = sender as BackgroundWorker;

            string strMessage = string.Format(Messages.SearchInProgress , String.Empty);

            switch (e.ProgressPercentage)
            {
                case 0:


                    _asyncOpManager.BeginOperation(bkgWork, strMessage, e.ProgressPercentage);

                    break;


                case 100:

                    _asyncOpManager.UpdateStatus(bkgWork, Messages.EndOperation, e.ProgressPercentage);

                    break;

                default:

                    _asyncOpManager.UpdateStatus(bkgWork, strMessage, e.ProgressPercentage);

                    break;
            }
        }

        void bkgGeneralSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {
                worker.ReportProgress(0);
                worker.ReportProgress(10);

                string genSearch = e.Argument as string;

                NameValueCollection searchParams = Utils.GetParamsForFreeTextQuery(genSearch);

                IBugBSI bugProvider = (IBugBSI)BLControllerFactory.GetRegisteredConcreteFactory(Utils.ConnectionId);

                worker.ReportProgress(60);

                List<MyZilla.BusinessEntities.Bug> bugsFound = bugProvider.SearchBugs(searchParams);

                worker.ReportProgress(100);

                ArrayList al = new ArrayList();

                al.Add(searchParams);

                al.Add(bugsFound);

                e.Result = al;
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "bkgGeneralSearch_DoWork", LoggingCategory.Exception);

                worker.ReportProgress(100);

                throw;
           }


        }

        private void txtGeneralSearch_KeyDown(object sender, KeyEventArgs e)
        {
            //if ENTER is pressed, start searching
            if (e.KeyCode == Keys.Enter)
                btnGeneralSearch_Click(sender, e);
            HandleNewBugKeyPress(e);
        }

        #endregion

        #region Refresh Async Operation EVENT

        void asyncOpManager_RefreshAsyncOpListEvent(object sender, EventArgs e)
        {
            AsyncOperationEventArgs operation = e as AsyncOperationEventArgs;

            lblStatusInfo.Visible = (operation != null && operation.Status != null && operation.Status.Percentage != 100);

            pbStatus.Visible = (operation != null && operation.Status != null && operation.Status.Percentage != 100);

            if (operation != null && operation.Status != null)
            {
                lblStatusInfo.Text = operation.Status.Message;

                pbStatus.Value = operation.Status.Percentage;
            }
            else
            {
                //no code here.
            }

        }

        private void _asyncOpManager_SplashRefreshAsyncOpListEvent(object sender, EventArgs e)
        {
            AsyncOperationEventArgs opStatusParam = e as AsyncOperationEventArgs;

            if (opStatusParam != null && opStatusParam.Status != null)
            {
                SplashManager.Status = opStatusParam.Status.Message;
            }

        }

        #endregion

        #region Generate column list from cookies

        private void BuildAsyncProcessForGenerateColumnListCookie( int userID)
        {
            if (!Utils.ContainsCookie("COLUMNLIST", userID))
            {
                BackgroundWorker bkgColListCookie = new BackgroundWorker();
                bkgColListCookie.DoWork += new DoWorkEventHandler(bkgColListCookie_DoWork);
                bkgColListCookie.ProgressChanged += new ProgressChangedEventHandler(bkgColListCookie_ProgressChanged);
                bkgColListCookie.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgColListCookie_RunWorkerCompleted);
                bkgColListCookie.WorkerReportsProgress = true;
                bkgColListCookie.WorkerSupportsCancellation = true;
                bkgColListCookie.RunWorkerAsync(userID);
            }
        }

        void bkgColListCookie_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {

                    string errorMessage = e.Error.Message;

                    MessageBox.Show(this, errorMessage, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    Interlocked.Decrement(ref _catalogManager.activeThreads);
                }
            }
            catch (Exception ex)
            {
                // The thread could continue to execute after the form was closed.
                // In this case, an exception is generated. It is no need to be logged or be shown those type of exceptions.
                if (!_formClosed)
                {

                    MyLogger.Write(ex, "bkgColListCookie_RunWorkerCompleted", LoggingCategory.Exception);

                    MessageBox.Show(this, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        void bkgColListCookie_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker bkgWork = sender as BackgroundWorker;

            switch (e.ProgressPercentage)
            {
                case 0:

                    if (_asyncOpManager != null)
                    {
                        _asyncOpManager.BeginOperation(bkgWork, Messages.LoadDefaultSettings , e.ProgressPercentage);
                    }

                    break;


                case 100:

                    if (_asyncOpManager != null)
                    {
                        _asyncOpManager.UpdateStatus(bkgWork, Messages.LoadDefaultSettings, e.ProgressPercentage);
                    }

                    break;

                default:

                    if (_asyncOpManager != null)
                    {

                        _asyncOpManager.UpdateStatus(bkgWork, Messages.LoadDefaultSettings, e.ProgressPercentage);
                    }

                    break;

            }

        }

        void bkgColListCookie_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bkgWork = sender as BackgroundWorker;

            try
            {
                Interlocked.Increment(ref _catalogManager.activeThreads);

                int userId = int.Parse(e.Argument.ToString());

                bkgWork.ReportProgress(0);
                bkgWork.ReportProgress(10);

                bkgWork.ReportProgress(60);

                Utils.GenerateColumnListCookie(userId);

            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "bkgColListCookie_DoWork", LoggingCategory.Exception);

                //throw;
            }
            finally {
                bkgWork.ReportProgress(100);
            }

        }

        #endregion

        #region Catalogue Update event

        void _catalogManager_CatalogueEvent(object sender, MyZillaSettingsEventArgs e)
        {
            try
            {
                int connID = e.SaveParameter.ConnectionRow.ConnectionId ;

                TDSettings.ConnectionRow  connection = _appSettings.GetConnectionById ( connID);
                TreeNode userNode = null;
                TreeNode[] nodes = tvQueries.Nodes.Find("User " + connID.ToString(), false);
                if (nodes.GetLength(0) == 1)
                {
                    userNode = nodes[0];
                }
                
                NodeDescription nodeDescr;

                switch (e.SaveParameter.Operation)
                {

                    #region Exceptions
                    case OperationType.AddConnectionThrowsError:
                        RemoveConnectingNode(connID);
                        break;
                    case OperationType.LogOnFailed :
                        LoginFailedMessage( connID, e.SaveParameter.ErrorMessage );
                        break;
                    #endregion

                    #region Add Connection event
                    case OperationType.AddConnection :

                        if (_catalogManager.AreCataloguesLoaded( connID ))
                        {
                            nodeDescr = (NodeDescription)nodes[0].Tag;

                            TDSQueriesTree.Instance().LoadDefaultDataForUserId(tvQueries, connection );

                            TDSQueriesTree.Instance().AddUserSubtree(tvQueries, (TDSettings.ConnectionRow )nodeDescr.NodeData);

                            _qTree.Save();

                            RemoveConnectingNode(connID);

                            BuildAsyncProcessForGenerateColumnListCookie(connID);

                            this.StartCountingBugsPerQuery(connID);

                        }
                        else
                        {
                            nodeDescr = new NodeDescription(NodeType.Connection, connection );

                            userNode = new TreeNode(_appSettings.GetConnectionInfo(connID), 2, 2);
                            userNode.Name = "User " + connID .ToString();
                            userNode.ToolTipText = String.Empty;
                            userNode.Tag = nodeDescr;

                            tvQueries.Nodes.Add(userNode);
                            if (connection.ActiveUser )
                                AddLoadingNode(userNode);
                        }
                        break;
                    #endregion

                    #region Delete Connection event
                    case OperationType.DeleteConnection :

                        DisconnectUser(connID );
                        _qTree.RemoveUserItems(connID);
                        userNode.Remove();

                        break;
                    #endregion

                    #region Edit User event
                    case OperationType.EditConnection :
                        
                            if (_catalogManager.AreCataloguesLoaded( connID ))
                            {
                                Interlocked.Decrement(ref _startupLoadingThreads);

                                nodeDescr = (NodeDescription)nodes[0].Tag;

                                if (_qTree.Folders.Select("UserId = " + connID.ToString()).GetLength(0)==0)
                                    TDSQueriesTree.Instance().LoadDefaultDataForUserId(tvQueries, connection );

                                RemoveConnectingNode(connID);

                                BuildAsyncProcessForGenerateColumnListCookie(connID);

                                if (this.IsTreeConnectionNodeLoaded( connID ))
                                {
                                    // no code here
                                }
                                else
                                {
                                    TDSQueriesTree.Instance().AddUserSubtree(tvQueries, (TDSettings.ConnectionRow)nodeDescr.NodeData);

                                    this.StartCountingBugsPerQuery(connID);

                                }

                                nodes[0].Text = e.SaveParameter.ConnectionRow.ConnectionName;

                                this.SetTreeNodesExpandState(userNode.Nodes);

                                if (_startupLoadingThreads == 0)
                                {
                                    this.Visible = true;

                                    SplashManager.Close();

                                    this.CheckForNewMyZillaVersion(false);
                                }

                            }
                            else {
                                if (connection.ActiveUser)
                                {
                                    this.AddLoadingNode(userNode);
                                }
                            }
                        break;
                    #endregion
                } // end switch
            }
            catch (Exception ex)
            {
                // The thread could continue to execute after the form was closed.
                // In this case, an exception is generated. It is no need to be logged or be shown those type of exceptions.
                if (!_formClosed)
                {

                    MyLogger.Write(ex, "_catalogManager_CatalogueEvent", LoggingCategory.Exception);

                    MessageBox.Show(this, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                //just in case
                SplashManager.Close();
            }
        }

        #endregion

        #region Async thread for counting bugs

        private void StartCountingBugsPerQuery( int connectionID)
        {

            TDSettings.GlobalSettingsRow globalSettings = _appSettings.GetGlobalSettings();

            if (globalSettings.ShowBugsCount == true)
            {
 
                BackgroundWorker bkgCountBug = new BackgroundWorker();
                bkgCountBug.DoWork += new DoWorkEventHandler(bkgCountBug_DoWork);
                bkgCountBug.ProgressChanged += new ProgressChangedEventHandler(bkgCountBug_ProgressChanged);
                bkgCountBug.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgCountBug_RunWorkerCompleted);
                bkgCountBug.WorkerReportsProgress = true;
                bkgCountBug.WorkerSupportsCancellation = true;
                bkgCountBug.RunWorkerAsync( connectionID );
            }
        }

        void bkgCountBug_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    MyLogger.Write("Counting bugs per query raised and error: " + e.Error.Message, "bkgCountBug_RunWorkerCompleted", LoggingCategory.Exception);

                    return;

                }

                // cancel or status OK
                if (!e.Cancelled && e.Error == null)
                {
                    if (e.Result != null)
                    {
                        int connectionID;
                        bool success = int.TryParse(e.Result.ToString(), out connectionID);

                        if (success)
                        {

                            // delete the reference in background workers array
                            int indexKey = this.queryCountsList.IndexOfKey(connectionID);

                            this.queryCountsList.RemoveAt(indexKey);        
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                // The thread could continue to execute after the form was closed.
                // In this case, an exception is generated. It is no need to be logged or be shown those type of exceptions.
                if (!_formClosed)
                {

                    MyLogger.Write(ex, "bkgCountBug_RunWorkerCompleted", LoggingCategory.Exception);

                    MessageBox.Show(this, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }

        void bkgCountBug_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker bkgWork = sender as BackgroundWorker;

            string connInfo = e.UserState as string;

            string strMessage = string.Format (Messages.MsgCountBugs, connInfo ); 

            switch (e.ProgressPercentage)
            {
                case 0:

                    if (_asyncOpManager != null)
                    {

                        _asyncOpManager.BeginOperation(bkgWork, strMessage, e.ProgressPercentage);
                    }

                    break;


                case 100:

                    if (_asyncOpManager != null)
                    {
                        _asyncOpManager.UpdateStatus(bkgWork, strMessage , e.ProgressPercentage);
                    }

                    break;

                default:

                    if (_asyncOpManager != null)
                    {

                        _asyncOpManager.UpdateStatus(bkgWork, strMessage , e.ProgressPercentage);
                    }

                    break;

            }
        }

        void bkgCountBug_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bkgWork = sender as BackgroundWorker;

            int connectionId = int.Parse(e.Argument.ToString());

            Thread currentThread = System.Threading.Thread.CurrentThread;

            // set thread priority 
            currentThread.Priority = ThreadPriority.Lowest ;  

            // add in the array that keeps the references to the active background workers for this task (query counts)
            if (!this.queryCountsList.ContainsKey(connectionId))
                this.queryCountsList.Add(connectionId, bkgWork); 

            string connInfo = _appSettings.GetConnectionInfo(connectionId);

            try
            {
                bkgWork.ReportProgress(0, connInfo);

                //pause to allow other request to end
                Thread.Sleep(1000);

                TDSQueriesTree.FoldersRow[] connectionFolders = (TDSQueriesTree.FoldersRow[])_qTree.Folders.Select(String.Format("UserID = {0}", connectionId));

                int queryCount = 0;

                int totalQueryCount = _qTree.GetQueryCountForConnectionId(connectionId);

                foreach (TDSQueriesTree.FoldersRow folder in connectionFolders)
                {
                    //MyLogger.Write(String.Format("Counting bugs in folder:{0}", folder.Name), "bkgCountBug_DoWork", LoggingCategory.General);

                    TDSQueriesTree.QueriesRow[] folderQueries = (TDSQueriesTree.QueriesRow[])_qTree.Queries.Select(String.Format("FolderID = {0}", folder.ID));

                    foreach (TDSQueriesTree.QueriesRow query in folderQueries)
                    {
                        queryCount++;

                        //MyLogger.Write(String.Format("\tCounting bugs for query:{0}", query.Name), "bkgCountBug_DoWork", LoggingCategory.General);

                        if (bkgWork.CancellationPending == false)
                        {

                            TreeNode[] nodes = tvQueries.Nodes.Find("query " + query.ID, true);

                            if (nodes != null && nodes.Length == 1)
                            {
                                int bugCount = this.GetBugsCountForQuery(connectionId, query.ID);

                                MyLogger.Write(String.Format("For:[{0}, {1}, {2}] Found :{3}", connectionId, folder.Name, query.Name, bugCount), "bkgCountBug_DoWork", LoggingCategory.General);

                                query.BugsCount = bugCount;

                                NodeDescription nodeInfo = nodes[0].Tag as NodeDescription;

                                // update node
                                this.SetText(string.Format(nodeInfo.Format, bugCount), nodes[0]);

                                //insert a pause between requests in order to make the UI more responsive
                                //too many requests were killing the responsiveness
                                Thread.Sleep(500);
                            }
                            else { 
                            }

                            bkgWork.ReportProgress((queryCount * 100) / totalQueryCount, connInfo);
                        }
                        else {
                            MyLogger.Write("Counting bugs canceled!", "bkgCountBug_DoWork", LoggingCategory.General);
                        }
                    }

                }

                // after successfully completed, the array that keeps the references to the active background workers
                // must be refreshed
                e.Result = connectionId;

            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "bkgCountBug_DoWork", LoggingCategory.Exception);
            }
            finally {
                bkgWork.ReportProgress(100);
            }
        }

        //public delegate void SetTextNode(string text, TreeNode node);

        public void SetText(string nodeText, TreeNode node)
        {
            if (tvQueries.InvokeRequired)
            {
                //SetTextNode del = new SetTextNode(SetText);
                //this.Invoke ( del, new object [] {nodeText, node});

                tvQueries.BeginInvoke(new MethodInvoker(delegate()
                {
                    node.Text = nodeText;
                }));
            }
            else
            {
                node.Text = nodeText;
            }
        }

        #endregion
        
        #region Export

        /// <summary>
        /// Export the current bug selection to a excel file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuExportToExcel_Click(object sender, EventArgs e)
        {
            if (!Utils.ExportTo(".xls", mnuExportToExcel, true))
                MessageBox.Show("Export can not be done during query execution operation!");
        }

        /// <summary>
        /// Export the current bug selection to a pdf file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuExportToPDF_Click(object sender, EventArgs e)
        {
            if (!Utils.ExportTo(".pdf", mnuExportToPDF, true ))
                MessageBox.Show("Export can not be done during query execution operation!");
        }


        #endregion

        private void miCheckForUpdates_Click(object sender, EventArgs e)
        {
            //perform manual check for a new MyZilla version
            this.CheckForNewMyZillaVersion(true);
        }

        // The delegate must have the same signature as the method
        // you want to call asynchronously.
        private delegate object[] AsyncCheckForNewMyZillaVersion(Form owner, string CurrentVersion, bool forceCheck);

        /// <summary>
        /// Opens an alert message if a new version of Myzilla is available on the server
        /// </summary>
        private void CheckForNewMyZillaVersion(bool forceCheck)
        {
            // Create the delegate.
            AsyncCheckForNewMyZillaVersion dlgt = new AsyncCheckForNewMyZillaVersion(Utils.GetPublishedMyZillaVersion);

            // Initiate the asychronous call.  Include an AsyncCallback
            // delegate representing the callback method, and the data
            // needed to call EndInvoke.
            dlgt.BeginInvoke(this, Application.ProductVersion, forceCheck,
                new AsyncCallback(CallbackCheckForNewMyZillaVersion),
                dlgt);
        }

        // Callback method must have the same signature as the
        // AsyncCallback delegate.
        private void CallbackCheckForNewMyZillaVersion(IAsyncResult ar)
        {
            // Retrieve the delegate.
            AsyncCheckForNewMyZillaVersion dlgt = (AsyncCheckForNewMyZillaVersion)ar.AsyncState;

            // Call EndInvoke to retrieve the results.
            object[] result = dlgt.EndInvoke(ar);

            string publishedMyZillaVersion = result[0] as string;
            bool forceCheck = (result[1] == null ? false : (bool)result[1]);
            Form owner = result[2] as Form;

            if (!String.IsNullOrEmpty(publishedMyZillaVersion))
            {
                ApplicationVersion publishedVersion = new ApplicationVersion(publishedMyZillaVersion);
                ApplicationVersion applicationInstanceVersion = new ApplicationVersion(Application.ProductVersion);

                MyZillaSettingsDataSet _appSettings = MyZillaSettingsDataSet.CreateInstance(Utils.UserAppDataPath);

                MyZillaSettingsDataSet.GlobalSettingsRow settings = _appSettings.GetGlobalSettings();

                if (forceCheck || (publishedVersion > (new ApplicationVersion(settings.LastMyZillaVersion))))
                {

                    if (publishedVersion > applicationInstanceVersion)
                    {
                        FormNewVersion newVersion = new FormNewVersion(publishedVersion.ToString());

                        if (this.InvokeRequired)
                        {
                            this.BeginInvoke(new MethodInvoker(delegate()
                            {
                                newVersion.ShowDialog(this);
                            }));
                        }
                    }
                    else
                    {
                        if (forceCheck)
                        {
                            if (owner.InvokeRequired)
                            {
                                this.BeginInvoke(new MethodInvoker(delegate()
                                {
                                    MessageBox.Show(owner, "No update is available!", "MyZilla", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }));
                                
                            }
                        }
                    }
                }
            }

        }

        private void SelectNextTab()
        {
            if (tBSI.SelectedTab != null)
            {
                if (tBSI.SelectedIndex + 1 < tBSI.TabCount)
                {
                    tBSI.SelectedIndex++;
                }
                else
                {
                    if (tBSI.SelectedIndex > 1)
                        tBSI.SelectedIndex--;
                }
            }
        }

        private void tvQueries_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ') { 
            }
        }
    }
}
