using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BSI.BL.Interfaces;
using BSI.UI.Properties;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using BSI.BusinessEntities;

namespace BSI.UI
{
    public partial class UCResults : UserControl
    {
        private List<BSI.BusinessEntities.Bug> _bugs  = new List<BSI.BusinessEntities.Bug>();
        private Hashtable hBugs = new Hashtable();
        private Hashtable hCachedBugs = new Hashtable();
        private int  _selectedBugId;
        private int _connectionID;
        private bool _searchCriteriaUILoaded;
        private TreeView _tree;
        private NameValueCollection _searchParams;
        private int bugID = 0;
        private bool _inProgress;
        private bool _queryCriteriaVisible;
        UCAdvancedSearchMobile _ucAdvSearch;
        List<string> _severityShortNames = new List<string>();
        bool       isBugSeverityColVisible = false;
        bool _executeQueryAutomatically;
        BSISettings _appSettings = BSISettings.GetInstance();
        bool _columnsChangeOrderAllowed;

        private SortOrder _severitySortType = SortOrder.None;
        private string _lastSortedColumnName = String.Empty;
        private bool _keepSorting = false;
        private string _queryNodeKey = null;

        private int splitterInitialDistance;

        private AsyncOperationManager asyncOpManager = AsyncOperationManager.GetInstance();

        private bool _isClosing;

        public bool IsClosing
        {
            get { return _isClosing; }
            set { _isClosing = value; }
        }
	

        #region Events

        public delegate void ExecuteQueryCompletedHandler(object sender, OnExecuteQueryCompletedArgs e);

        public event ExecuteQueryCompletedHandler ExecuteQueryCompleted;

        public delegate void QueryNameChangedHandler(object sender, OnQueryNameChangedArgs e);

        public event QueryNameChangedHandler QueryNameChanged;

        #endregion

        #region Constructors

        public UCResults()
        {
            InitializeComponent();
        }

        public UCResults(int UserID, List<BSI.BusinessEntities.Bug> bugs, NameValueCollection SearchParams, TreeView Tree, bool DisplayQueryCriteria, bool QueryCriteriaVisible, bool ExecuteQueryAutomatically, string QueryNodeKey)
        {
            InitializeComponent();
            tableLayoutPanel1.RowStyles[0].SizeType = SizeType.AutoSize;
            Bugs = bugs;
            _searchParams = SearchParams;
            _tree = Tree;
            _connectionID = UserID;
            _queryCriteriaVisible = QueryCriteriaVisible;
            _executeQueryAutomatically = ExecuteQueryAutomatically;
            _queryNodeKey = QueryNodeKey;
            if (_searchParams["content"] != null) {
                btnSaveQuery.Enabled = false;
            }
            CreateSeverityDictionary();

            _ucAdvSearch = new UCAdvancedSearchMobile(null, _tree, null, _searchParams, _connectionID);
            splitContainer5.Panel1.Controls.Add(_ucAdvSearch);
            _ucAdvSearch.Dock = DockStyle.Fill;


            splitterInitialDistance = splitContainer1.SplitterDistance;

            if (DisplayQueryCriteria)
            {
                ShowQueryCriteria();
            }
            else {
                HideQueryCriteria();
            }

           
        }

        #endregion

        #region Properties

        public NameValueCollection SearchCriteria {
            set
            {
                _searchParams = value;
            }
        }

        public int SelectedBugId
        {
            get { return _selectedBugId; }
        }
	
        public int BugId {
            get {
                return _selectedBugId;
            }
        }

        private bool IsConnectionAlive
        {
            get
            {
                CatalogueManager catalogues = CatalogueManager.Instance();
                if (catalogues.GetCataloguesForConnection(_connectionID) == null)
                    return false;
                else return true;

            }

        }

        #endregion

        #region Public methods

        public List<BSI.BusinessEntities.Bug> Bugs
        {
            get { return _bugs; }
            set { 
                _bugs = value;
                
                IndexBugs();
            }
        }

        public void LoadBugs(List<BSI.BusinessEntities.Bug> bugs){

            if (!_inProgress)
            {
                Bugs = bugs;

                if (bugs == null)
                {
                    _keepSorting = true;
                    ExecuteQuery();
                }
                else
                    GetGridWithResults();
            }
        }

        #endregion

        #region private methods

        private void LoadEditBugWindow(int CurrentRowIndex)
        {
            if (CurrentRowIndex >= 0)
            {
                int bugId = Int32.Parse(dgvResults.Rows[CurrentRowIndex].Cells[0].Value.ToString());

                FormInstanceManager _editBugInstanceManager = FormInstanceManager.GetInstance();

                Bug cachedBug = null;

                // check for cached bug
                if (this.hCachedBugs.ContainsKey(bugId) == true)
                {
                    int indexBug = (int)this.hCachedBugs[bugId];

                    cachedBug = this._bugs[ indexBug ] as Bug ;  
                }

                FormEditBug frmEditBug =  _editBugInstanceManager.OpenEditBugFormInstance(_connectionID, bugId, cachedBug ); 

                // subscribe to the updated bug event
                frmEditBug.UpdatedBug += new FormEditBug.UpdatedBugHandler(frmEditBug_UpdatedBug); 

                Utils.ActivateLoadingForm();
            }
        }


        /// <summary>
        /// Creates a hash with bug id and index of the position in the array of bugs
        /// This helps in updating bugs with all information from the system
        /// by making a get to "show_bug.cgi"
        /// </summary>
        private void IndexBugs()
        {
            if (_bugs != null)
            {
                hBugs.Clear();
                hCachedBugs.Clear();
                for (int i = 0; i < _bugs.Count; i++)
                {
                    hBugs.Add(_bugs[i].ID, i);
                }
            }
        }

        private void GetGridWithResults()
        {
            _columnsChangeOrderAllowed = false;
            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            DataTable dtBugs = BuildDataSource();

            dgvResults.DataSource = dtBugs.DefaultView;

            //if any column set as sorted
            if (_sortColumn != null)
            {
                _severitySortType = (SortOrder)(_sortColumn.Sort+1);
                dgvResults.Sort(dgvResults.Columns[_sortColumn.Label], (ListSortDirection)_sortColumn.Sort);
            }
            else
                dgvResults.Sort(dgvResults.Columns["Id"], ListSortDirection.Ascending);

            PaintGrid();
            lblCountResults.Text = String.Format("Bugs found: {0}", dtBugs.Rows.Count);

            bool hasAutofillColumns = false;
            int lastVisibleColumnIndex = dgvResults.Columns.Count-2;
            for (int i = 0; i < dgvResults.Columns.Count; i++)
            {
                DataRow[] rows = _appSettings.Columns.Select("ConnectionID = " + _connectionID + " AND Label = '" + dgvResults.Columns[i].Name + "'");
                if (rows.GetLength(0) == 1)
                {
                    int displayIndex = ((BSISettings.ColumnsRow)rows[0]).DisplayIndex;
                    if (displayIndex == -1 || (displayIndex >= dgvResults.Columns.Count))
                    {
                        dgvResults.Columns[i].DisplayIndex = dgvResults.Columns.Count - 1;
                        ((BSISettings.ColumnsRow)rows[0]).DisplayIndex = dgvResults.Columns.Count - 1;
                        if (((BSISettings.ColumnsRow)rows[0]).Width == -1)
                        {
                            dgvResults.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            hasAutofillColumns = true;
                        }
                    }
                    else
                        dgvResults.Columns[i].DisplayIndex = displayIndex;

                    if (dgvResults.Columns[i].DisplayIndex == dgvResults.Columns.Count - 1)
                        lastVisibleColumnIndex = i;
                    dgvResults.Columns[i].Width = ((BSISettings.ColumnsRow)rows[0]).Width;
                }
                else {
                    if (dgvResults.Columns[i].Name == "severity_id")
                        dgvResults.Columns[i].Visible = false;
                }
            }
            if (!hasAutofillColumns)
                dgvResults.Columns[lastVisibleColumnIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //set width for the id column
            if (dtBugs.Rows.Count > 0)
            {
                dgvResults.Columns[0].Width = 40;
                dgvResults.Rows[0].Selected = true;
                GetBugDetails(0);
            }

            if (!isBugSeverityColVisible)
                dgvResults.Columns["severity"].Visible = false;
            else
                dgvResults.Columns["severity"].Visible = true;

            _columnsChangeOrderAllowed = true;
        }

        public void ExecuteQuery(bool KeepSorting)
        {
            _keepSorting = KeepSorting;
            ExecuteQuery();
        }

        private void ExecuteQuery()
        {
            if (_executeQueryAutomatically)
            {
                if (!_inProgress)
                {
                    try
                    {
                        ClearBugDetailsArea();

                        this.EnableLoadingPanel(true);
                        //LoadingPanel.Location = new Point((splitContainer1.Panel1.Width - picLoading.Width - 50) / 2, (splitContainer1.Panel1.Height - picLoading.Height) / 2);
                        //LoadingPanel.Visible = true;
                        //LoadingPanel.BringToFront();

                        _inProgress = true;

                        Utils.ActivateLoadingForm();

                        BackgroundWorker bkgSearch = new BackgroundWorker();

                        bkgSearch.DoWork += new DoWorkEventHandler(bkgSearch_DoWork);

                        bkgSearch.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgSearch_RunWorkerCompleted);

                        bkgSearch.ProgressChanged += new ProgressChangedEventHandler(bkgSearch_ProgressChanged);

                        bkgSearch.WorkerReportsProgress = true;

                        bkgSearch.WorkerSupportsCancellation = true;


                        bkgSearch.RunWorkerAsync();
                    }
                    catch (Exception ex)
                    {
                        Logger.Write(System.Reflection.MethodInfo.GetCurrentMethod().Name + ": " + ex, LoggingCategory.Exception);

                        throw;
                    }
                }
            }
            else
            {
                _executeQueryAutomatically = true;
            }
        }



        /// <summary>
        /// Walks through the rows and, based on severity, generates a color(red) and paints each row
        /// with the selected color
        /// </summary>
        private void PaintGrid()
        {
            if (dgvResults.Columns.Contains("severity"))
            {
                int index;
                int normalSeverityIndex = _severityShortNames.IndexOf("normal");
                foreach (DataGridViewRow r in dgvResults.Rows)
                {
                    index = _severityShortNames.IndexOf(r.Cells["severity"].Value.ToString().TrimEnd(' '));
                    if (index < normalSeverityIndex)
                    {
                        r.DefaultCellStyle.ForeColor = Color.FromArgb(255, index * 50, index * 50);
                    }
                    else if (index >= normalSeverityIndex)
                        r.DefaultCellStyle.ForeColor = Color.FromArgb((index - normalSeverityIndex) * 30, (index - normalSeverityIndex) * 30, (index - normalSeverityIndex) * 30); 

                }
            }

        }

        private void CreateSeverityDictionary()
        {
            CatalogueManager catalogues = CatalogueManager.Instance();
            List<string> sev = catalogues.GetCataloguesForConnection(_connectionID).catalogueSeverity;

            foreach (string elem in sev)
                _severityShortNames.Add(elem.Split(',')[0]);

        }

        private BSISettings.ColumnsRow _sortColumn = null;

        private DataTable BuildDataSource()
        {
            DataTable dtBugs = new DataTable();

            if (_bugs != null)
            {
                _sortColumn = null;

                dtBugs.Columns.Add(Properties.Resources.ResourceManager.GetString("bug_id"));

                DataRow[] rows = _appSettings.Columns.Select("ConnectionID = " + _connectionID.ToString());
                BSISettings.ColumnsRow rowCol;

                isBugSeverityColVisible = false;
                foreach (DataRow row in rows)
                {
                    rowCol = (BSISettings.ColumnsRow)row;
                    dtBugs.Columns.Add(rowCol.Label);
                    if ((rowCol.Sort != 2) && (_sortColumn == null)) //2 = no sorting
                        _sortColumn = rowCol;
                    if (rowCol.Name == "bug_severity" && rowCol.Visible)
                        isBugSeverityColVisible = true;
                }
                dtBugs.Columns.Add("severity_id", System.Type.GetType("System.Int32"));

                //for (int i = 0; i < _columnNames.GetLength(0); i++)
                //{
                //    dtBugs.Columns.Add(_colNames[_columnNames[i]].ToString());
                //    //dtBugs.Columns.Add(Properties.Resources.ResourceManager.GetString(_columnNames[i]));
                //}

                for (int iBug = 0; iBug < _bugs.Count; iBug++)
                {
                    DataRow dr = dtBugs.NewRow();
                    dr[0] = _bugs[iBug].ID;
                    #region column value set
                    int i = 1;
                    foreach (DataRow row in rows)
                    {
                        rowCol = ((BSISettings.ColumnsRow)row);
                        switch (rowCol.Name)
                        {
                            case "bug_id":
                                dr[i++] = Bugs[iBug].ID;
                                break;
                            case "opendate":
                                dr[i++] = Bugs[iBug].Created;
                                break;
                            case "changeddate":
                                dr[i++] = Bugs[iBug].Changed;
                                break;
                            case "bug_severity":
                                dr[i++] = Bugs[iBug].Severity;
                                break;
                            case "severity_id":
                                dr[i++] = _severityShortNames.IndexOf(Bugs[iBug].Severity.TrimEnd(' '));
                                break;
                            case "priority":
                                dr[i++] = Bugs[iBug].Priority;
                                break;
                            case "rep_platform":
                                dr[i++] = Bugs[iBug].Hardware;
                                break;
                            case "assigned_to":
                                dr[i++] = Bugs[iBug].AssignedTo;
                                break;
                            case "assigned_to_realname":
                                dr[i++] = Bugs[iBug].AssignedToRealName;
                                break;
                            case "reporter":
                                dr[i++] = Bugs[iBug].Reporter;
                                break;
                            case "reporter_realname":
                                dr[i++] = Bugs[iBug].ReporterRealName;
                                break;
                            case "bug_status":
                                dr[i++] = Bugs[iBug].Status;
                                break;
                            case "resolution":
                                dr[i++] = Bugs[iBug].Resolution;
                                break;
                            case "product":
                                dr[i++] = Bugs[iBug].Product;
                                break;
                            case "component":
                                dr[i++] = Bugs[iBug].Component;
                                break;
                            case "version":
                                dr[i++] = Bugs[iBug].Version;
                                break;
                            case "op_sys":
                                dr[i++] = Bugs[iBug].OS;
                                break;
                            case "votes":
                                dr[i++] = Bugs[iBug].Votes;
                                break;
                            case "status_whiteboard":
                                dr[i++] = Bugs[iBug].StatusWhiteboard;
                                break;
                            case "short_desc":
                                dr[i++] = Bugs[iBug].FullSummary;
                                break;
                            case "short_short_desc":
                                dr[i++] = Bugs[iBug].Summary;
                                break;
                        }
                    }
                    #endregion

                    dr[i++] = _severityShortNames.IndexOf(Bugs[iBug].Severity.TrimEnd(' '));
                    dtBugs.Rows.Add(dr);
                }
            }
            else
                MessageBox.Show("_bugs is null");

            return dtBugs;

        }

        private void ShowBugDetails(BSI.BusinessEntities.Bug currentBug)
        {
            try
            {
                if (currentBug != null)
                {
                    if (!string.IsNullOrEmpty(currentBug.ErrorMessage))
                    {
                        lblMessageError.Visible = true;

                        lblMessageError.Text = currentBug.ErrorMessage;

                    }
                    else
                    {
                        lblMessageError.Visible = false;

                    }

                    lblBugId.Text = currentBug.ID.ToString();

                    lblSummary.Text = currentBug.Summary;

                    lblProductValue.Text = currentBug.Product;

                    lblVersionValue.Text = currentBug.Version;

                    lblComponentValue.Text = currentBug.Component;

                    lblCreatedOnValue.Text = currentBug.Created.ToString("dd/MM/yyyy HH:mm");

                    lblLastModifiedValue.Text = currentBug.Changed.ToString("dd/MM/yyyy HH:mm");

                    lblStatusValue.Text = currentBug.Status;

                    lblPriorityValue.Text = currentBug.Priority;

                    lblSeverityValue.Text = currentBug.Severity;

                    lblReporterValue.Text = currentBug.Reporter;

                    lblAssignedToValue.Text = currentBug.AssignedTo;

                    lblHardwareValue.Text = currentBug.Hardware;

                    lblOSValue.Text = currentBug.OS;

                    lblURLValue.Text = currentBug.URL;

                    lblDescr.Text = String.Empty;

                    if (currentBug.Comment != null)
                    {
                        if (currentBug.Comment.Count > 0)
                        {
                            string[] res = currentBug.Comment[0].Split(',');
                            if (res.GetLength(0) >= 3)
                            {
                                string Date = res[1];
                                //avoid problems of spliting by comma (comment might contain comma)
                                lblDescr.Text = Utils.ReplaceNewLines(currentBug.Comment[0].Substring(currentBug.Comment[0].IndexOf(Date) + Date.Length + 1));
                            }

                            ucComments1.LoadComments(currentBug.Comment);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(System.Reflection.MethodInfo.GetCurrentMethod().Name + ": " + ex, LoggingCategory.Exception);

                throw;

            }
        }

        private void ShowQueryCriteria()
        {
            if (_queryCriteriaVisible)
            {
                splitContainer5.Panel1Collapsed = false;
                splitContainer1.SplitterDistance = splitterInitialDistance;
                _ucAdvSearch.Visible = true;
                _searchCriteriaUILoaded = true;
                tbnShow.Image = Properties.Resources.show;
            }
        }

        private void HideQueryCriteria()
        {
            splitContainer5.Panel1Collapsed = true;
            splitContainer1.SplitterDistance = (int)(splitContainer1.SplitterDistance * 0.75);
            _ucAdvSearch.Visible = false;
            tbnShow.Image = Properties.Resources.hide;
        }

        private void EnableLoadingPanel(bool enabled)
        {
            LoadingPanel.Location = new Point((splitContainer1.Panel1.Width - picLoading.Width - 50) / 2, (splitContainer1.Panel1.Height - picLoading.Height) / 2);
            LoadingPanel.Visible = enabled;
            LoadingPanel.BringToFront();

        }


        #endregion

        #region Form events

        private void ucResults_Load(object sender, EventArgs e)
        {
            LoadBugs(null);

            ClearBugDetailsArea();

            this.LoadPriorityContextMenu();
          
        }

        private void ClearBugDetailsArea(){
            string nothing = "-";
            lblBugId.Text = "Bug details";
            lblSummary.Text = String.Empty;
            lblProductValue.Text = nothing;
            lblVersionValue.Text = nothing;
            lblURLValue.Text = nothing;
            lblOSValue.Text = nothing;
            lblStatusValue.Text = nothing;
            lblSeverityValue.Text = nothing;
            lblPriorityValue.Text = nothing;
            lblHardwareValue.Text = nothing;
            lblCreatedOnValue.Text = nothing;
            lblComponentValue.Text = nothing;
            lblAssignedToValue.Text = nothing;
            lblReporterValue.Text = nothing;
            lblLastModifiedValue.Text = nothing;
        }

        private void dgvResults_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            LoadEditBugWindow(e.RowIndex);

        }

        private void btnSaveQuery_Click(object sender, EventArgs e)
        {
            if (!IsConnectionAlive)
            {
                MessageBox.Show("The current query connection does not exist anymore! This tab should be closed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                string queryName = String.Empty;
                string queryDescription = String.Empty;
                int folderID = -1;
                ConfigItems.QueriesTree tree = ConfigItems.QueriesTree.Instance();
                //update params collection with values from UI
                try
                {
                    //if search criteria was not opened, _ucAdvSearch.GetSearchCriteria will crash
                    //but saving must continue
                    _searchParams = _ucAdvSearch.GetSearchCriteria();

                }catch{}

                //if (_queryNodeKey == null)
                if (Int32.Parse(_queryNodeKey.Split(' ')[1])<0)
                {
                    #region show dialog for choosing a query name
                    FormQueryName frm = new FormQueryName(_connectionID, _tree);

                    DialogResult result = frm.ShowDialog(this);

                    //check respose of the user from the dialog form
                    switch (result)
                    {
                        case DialogResult.OK:
                            queryName = frm.QueryName;
                            folderID = frm.FolderID;
                            queryDescription = frm.QueryDescription;

                            int bugsCount = -1;
                            if (_bugs != null)
                                bugsCount = _bugs.Count;

                            ConfigItems.TDSQueriesTree.QueriesRow query = tree.AddNewQuery(queryName, queryDescription, folderID, (int)QueryTypes.UserCustom, bugsCount, _searchParams);

                            tree.AddQueryToTree(_tree, query);

                            QueryNameChanged(this, new OnQueryNameChangedArgs(query.ID, query.Name, frm.FolderName));

                            _queryNodeKey = "query " + query.ID.ToString();
                            break;

                    }
                    #endregion
                }
                else {
                    TreeNode[] nodes = _tree.Nodes.Find(_queryNodeKey, true);
                    if (nodes.GetLength(0) == 1) {
                        ConfigItems.NodeDescription nodeDescr = (ConfigItems.NodeDescription)nodes[0].Tag;
                        ConfigItems.QueriesTree.QueriesRow query = (ConfigItems.QueriesTree.QueriesRow)nodeDescr.NodeData;
                        if (!tree.UpdateQuery(query.ID, _searchParams))
                            MessageBox.Show("Updating query failed!");
                        else {
                            MessageBox.Show(String.Format(Properties.Messages.QuerySuccesfullySaved, query.Name), "Query saved", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                        }
                    }
                }
            }
        }

        private void dgvResults_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!_inProgress)
                GetBugDetails(e.RowIndex);
        }

        private void GetBugDetails(int bugIndex)
        {
            try
            {
                if (dgvResults.SelectedRows.Count > 0 && this.ParentForm != null)
                {
                    if (dgvResults.SelectedRows.Count > 0)
                    {
                        _selectedBugId = -1;

                        _selectedBugId = Convert.ToInt32(dgvResults.Rows[bugIndex].Cells[0].Value);

                        ((MainForm)this.ParentForm).txtBugURL.Text = Utils.GetBugURL(_selectedBugId.ToString());

                        if ((lblBugId.Text != _selectedBugId.ToString()) && (_selectedBugId != -1))
                        {

                            bool isInCache = false;

                            BSI.BusinessEntities.Bug currentBug = null;
                            if (hCachedBugs.ContainsKey(_selectedBugId))
                            {
                                currentBug = _bugs[(int)hBugs[_selectedBugId]];

                                if (currentBug != null)
                                {
                                    isInCache = true;
                                }
                            }

                            if (isInCache)
                            {
                                ShowBugDetails(currentBug);

                            }
                            else
                            {

                                bugID = Convert.ToInt32(dgvResults.Rows[bugIndex].Cells[0].Value);

                                int indexHT = (int)hBugs[bugID];

                                // start secondary thread when getting bug details.

                                BackgroundWorker bkgBugDetails = new BackgroundWorker();

                                bkgBugDetails.DoWork += new DoWorkEventHandler(bkgBugDetails_DoWork);

                                bkgBugDetails.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgBugDetails_RunWorkerCompleted);

                                bkgBugDetails.ProgressChanged += new ProgressChangedEventHandler(bkgBugDetails_ProgressChanged);

                                bkgBugDetails.WorkerReportsProgress = true;

                                bkgBugDetails.WorkerSupportsCancellation = true;

                                ArrayList al = new ArrayList();

                                al.Add(bugID);

                                al.Add(indexHT);

                                // start run async thread.
                                bkgBugDetails.RunWorkerAsync(al);


                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write(System.Reflection.MethodInfo.GetCurrentMethod().Name + ": " + ex.Message, LoggingCategory.Exception);

                //MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }

        }

        private void dgvResults_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (dgvResults.SelectedRows.Count == 1))
            {
                e.SuppressKeyPress = true;

                LoadEditBugWindow(dgvResults.SelectedRows[0].Index);
            }
        }

        private void tsbtnRunQuery_Click(object sender, EventArgs e)
        {
            if (!IsConnectionAlive)
            {
                MessageBox.Show("The current query connection does not exist anymore! This tab should be closed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                _keepSorting = true;
                if (_queryCriteriaVisible && _searchCriteriaUILoaded)
                    _searchParams = _ucAdvSearch.GetSearchCriteria();
                ExecuteQuery();
            }
        }

        private void tbnShow_Click(object sender, EventArgs e)
        {
            if (!IsConnectionAlive)
            {
                MessageBox.Show("The current query connection does not exist anymore! This tab should be closed!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (_ucAdvSearch.Visible)
                {
                    HideQueryCriteria();
                }
                else
                {
                    ShowQueryCriteria();
                }
            }
        }

        private void tsbSelectColumns_Click(object sender, EventArgs e)
        {
            if (!IsConnectionAlive)
            {
                MessageBox.Show("The current query connection does not exist anymore! This tab should be closed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Form selectColumns = new FormSelectCols(_connectionID);
                DialogResult dialogResult = selectColumns.ShowDialog();

                //if select columns form was changed and saved, query is reexecuted
                if (dialogResult == DialogResult.OK)
                {
                    _keepSorting = true;
                    ExecuteQuery();
                }
            }
        }

        private void dgvResults_Paint(object sender, PaintEventArgs e)
        {
            PaintGrid();
            //Logger.Write("Grid repainted", LoggingCategory.Debug);
        }

        private void dgvResults_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (_columnsChangeOrderAllowed)
            {
                for (int i = 0; i < dgvResults.Columns.Count; i++)
                {
                    DataRow[] rows = _appSettings.Columns.Select("ConnectionID = " + _connectionID + " AND Label = '" + dgvResults.Columns[i].Name + "'");
                    if (rows.GetLength(0) == 1)
                        ((BSISettings.ColumnsRow)rows[0]).Width = dgvResults.Columns[i].Width;
                }
            }
        }

        private void dgvResults_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (_columnsChangeOrderAllowed)
            {
                for (int i = 0; i < dgvResults.Columns.Count; i++)
                {
                    DataRow[] rows = _appSettings.Columns.Select("ConnectionID = " + _connectionID + " AND Label = '" + dgvResults.Columns[i].Name + "'");
                    if (rows.GetLength(0) == 1)
                    {
                        //((BSISettings.ColumnsRow)rows[0]).Width = dgvResults.Columns[i].Width;
                        ((BSISettings.ColumnsRow)rows[0]).DisplayIndex = dgvResults.Columns[i].DisplayIndex;
                    }
                }
            }
        }

        private void dgvResults_Sorted(object sender, EventArgs e)
        {
            DataRow[] rows = _appSettings.Columns.Select("ConnectionID = " + _connectionID);

            if (rows != null)
            {
                bool columnSortedFound = false;
                BSISettings.ColumnsRow severityColRow = null;
                foreach (DataRow row in rows)
                {
                    BSISettings.ColumnsRow col = (BSISettings.ColumnsRow)row;

                    if (col.Label == "Severity")
                    {
                        severityColRow = col;
                        if (dgvResults.SortedColumn.Name == "severity_id")
                        {
                            col.Sort = (byte)((byte)dgvResults.SortOrder - 1);
                            columnSortedFound = true;
                        }
                        else
                        {
                            col.Sort = 2;
                        }
                    }
                    else
                        if (col.Label == dgvResults.SortedColumn.Name)
                        {
                            col.Sort = (byte)((byte)dgvResults.SortOrder - 1);
                            columnSortedFound = true;
                        }
                        else
                        {

                            //no sorting
                            col.Sort = 2;
                        }
                }
                if ((!columnSortedFound) && (dgvResults.SortedColumn.Name == "severity_id") && (severityColRow != null))
                {
                    severityColRow.Sort = (byte)((byte)dgvResults.SortOrder - 1);
                }
            }

            if (dgvResults.SortedColumn.Name != _lastSortedColumnName)
            {


                if (dgvResults.SortedColumn.Name == "Severity")
                {
                    if (_lastSortedColumnName.Length > 0)
                    {
                        dgvResults.SortedColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                        _severitySortType = SortOrder.Ascending;
                        dgvResults.Sort(dgvResults.Columns["severity_id"], ListSortDirection.Ascending);
                    }
                    else
                    {
                        dgvResults.SortedColumn.HeaderCell.SortGlyphDirection = _severitySortType;
                        _severitySortType = _severitySortType;
                        dgvResults.Sort(dgvResults.Columns["severity_id"], (ListSortDirection)(_severitySortType - 1));
                    }
                }
                else if (dgvResults.SortedColumn.Name == "severity_id")
                {
                    dgvResults.Columns["Severity"].HeaderCell.SortGlyphDirection = _severitySortType;
                    _lastSortedColumnName = dgvResults.Columns["Severity"].Name;
                }

                _lastSortedColumnName = dgvResults.SortedColumn.Name;
            }
            else
            {
                _lastSortedColumnName = dgvResults.SortedColumn.Name;

                if (dgvResults.SortedColumn.Name == "Severity")
                {
                    switch (_severitySortType)
                    {
                        case SortOrder.None:
                            dgvResults.SortedColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                            _severitySortType = SortOrder.Ascending;
                            dgvResults.Sort(dgvResults.Columns["severity_id"], ListSortDirection.Ascending);
                            break;
                        case SortOrder.Ascending:
                            dgvResults.SortedColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending;
                            _severitySortType = SortOrder.Descending;
                            dgvResults.Sort(dgvResults.Columns["severity_id"], ListSortDirection.Descending);
                            break;
                        case SortOrder.Descending:
                            dgvResults.SortedColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                            _severitySortType = SortOrder.Ascending;
                            dgvResults.Sort(dgvResults.Columns["severity_id"], ListSortDirection.Ascending);
                            break;
                    }
                }
                else if (dgvResults.SortedColumn.Name == "severity_id")
                {
                    dgvResults.Columns["Severity"].HeaderCell.SortGlyphDirection = _severitySortType;
                    _lastSortedColumnName = dgvResults.Columns["Severity"].Name;
                }
                else
                    _severitySortType = SortOrder.None;
            }


        }

        void frmEditBug_UpdatedBug(object sender, UpdatedBugParam e)
        {

            Bug updatedBug = e.UpdatedBug as Bug;

            tsbtnRunQuery_Click(null, null);

        }


        #endregion

        #region Async - load bug details

        void bkgBugDetails_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker bkgWork = sender as BackgroundWorker;

            string strMessage = string.Format(Messages.LoadBugDetails, bugID ); 

            switch (e.ProgressPercentage)
            {
                case 0:

                    asyncOpManager.BeginOperation(bkgWork, strMessage , e.ProgressPercentage);

                    break;


                case 100:

                    asyncOpManager.UpdateStatus(bkgWork,  Messages.EndOperation, e.ProgressPercentage);

                    break;

                default:

                    asyncOpManager.UpdateStatus(bkgWork,  strMessage , e.ProgressPercentage);

                    break;

            }
        }

        void bkgBugDetails_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if tab(container) is not closing
            if (!_isClosing)
            {
                try
                {

                    // check the status of the async operation.

                    if (e.Error != null)
                    {
                        string errMessage = Messages.ErrGetBugDetails + Environment.NewLine + e.Error.Message;

                        MessageBox.Show(Utils.FormContainer, errMessage, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


                    }

                    // status OK
                    if (!e.Cancelled && e.Error == null)
                    {
                        ArrayList alResult = e.Result as ArrayList;

                        BSI.BusinessEntities.Bug bugResult = alResult[0] as Bug;

                        int indexHT = int.Parse(alResult[1].ToString());

                        _bugs[indexHT] = bugResult;

                        if (!hCachedBugs.ContainsKey(bugResult.ID))
                            hCachedBugs.Add(bugResult.ID, indexHT);
                        if (dgvResults.SelectedRows != null && dgvResults.SelectedRows[0].Cells[0].Value.ToString() == bugResult.ID.ToString())
                        {
                            ShowBugDetails(bugResult);
                        }

                    }
                    _inProgress = false;
                }
                catch (Exception ex)
                {
                    Logger.Write(System.Reflection.MethodInfo.GetCurrentMethod().Name + ": " + ex.Message, LoggingCategory.Exception);

                    //    MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
                }
                finally
                {
                    this.EnableLoadingPanel(false);
                }
            }
        }

        void bkgBugDetails_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            try
            {

                _inProgress = true;

                ArrayList al = e.Argument  as ArrayList;

                int bugNumber = int.Parse(al[0].ToString ());

                if ((dgvResults.SelectedRows.Count == 0) || (bugNumber != int.Parse(dgvResults.SelectedRows[0].Cells[0].Value.ToString())))
                {
                    e.Cancel = true;

                    return;

                }

                worker.ReportProgress(0); // start thread.
                worker.ReportProgress(10);


                IBugBSI bugInterface = (IBugBSI)BLControllerFactory.GetRegisteredConcreteFactory(_connectionID);

                worker.ReportProgress(60);  //intermediate state

                BSI.BusinessEntities.Bug bugDetails = bugInterface.GetBug(bugNumber);

                bugDetails.ID = bugNumber;

                worker.ReportProgress(100);  //completed

#if DEBUG

                (Utils.FormContainer as MainForm).webBrowser1.DocumentText = BSI.BL.Interfaces.Utils.htmlContents;
#endif

                ArrayList alResult = new ArrayList();

                alResult.Add(bugDetails);

                alResult.Add(al[1].ToString ());

                e.Result = alResult ;

            }
            catch (Exception ex)
            {
                //PROBLEM
                //Logger.Write(System.Reflection.MethodInfo.GetCurrentMethod().Name + ": " + ex.Message, LoggingCategory.Exception);

                worker.ReportProgress(100);  //completed

                throw;
            }
        }

        #endregion

        #region Async - search

        void bkgSearch_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            BackgroundWorker bkgWork = sender as BackgroundWorker;

            switch (e.ProgressPercentage)
            {
                case 0:

                    asyncOpManager.BeginOperation(bkgWork, Properties.Messages.SearchInProgress, e.ProgressPercentage);

                    break;


                case 100:

                    asyncOpManager.UpdateStatus(bkgWork, Properties.Messages.EndOperation, e.ProgressPercentage);

                    break;

                default:

                    asyncOpManager.UpdateStatus(bkgWork, Properties.Messages.SearchInProgress, e.ProgressPercentage);

                    break;

            }

        }

        void bkgSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if tab closing was not started
            if (!_isClosing)
            {
                // check the status of the async operation.

                if (e.Error != null)
                {

                    string errMessage = Messages.ErrSearching  + Environment.NewLine + e.Error.Message ;

                    MessageBox.Show( Utils.FormContainer, errMessage, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);      

                }

                // status OK
                if (!e.Cancelled && e.Error == null)
                {
                    Bugs = e.Result as List<BSI.BusinessEntities.Bug>;

                    if (ExecuteQueryCompleted != null)
                    {
                        this.ExecuteQueryCompleted(this, new OnExecuteQueryCompletedArgs(_tree, _queryNodeKey, _bugs.Count));
                    }

                    GetGridWithResults();
                }

                _inProgress = false;

                this.EnableLoadingPanel(false);
    //            LoadingPanel.Visible = false;
            }
        }

        void bkgSearch_DoWork(object sender, DoWorkEventArgs e)
        {

            BackgroundWorker worker = sender as BackgroundWorker;
            try
            {

                worker.ReportProgress(0); // start thread.
                worker.ReportProgress(10);


                IBugBSI bugProvider = (IBugBSI)BLControllerFactory.GetRegisteredConcreteFactory(_connectionID);

                worker.ReportProgress(60);  //intermediate state

                List<BSI.BusinessEntities.Bug> bugsFound = bugProvider.SearchBugs(_searchParams);

                worker.ReportProgress(100);  //completed

                e.Result = bugsFound;
            }
            catch (Exception ex)
            {
                Logger.Write(System.Reflection.MethodInfo.GetCurrentMethod().Name + ": " + ex.Message, LoggingCategory.Exception);

                worker.ReportProgress(100);  //completed

                throw;
            }


        }

        #endregion

        #region Context menu 

        private void reopenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bug bugsPropToBeChanged = new Bug();
            bugsPropToBeChanged.Knob = "reopen";
            bugsPropToBeChanged.Resolution = "";

            this.UpdateBunchBugs(bugsPropToBeChanged);

        }

        private void fixedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bug bugsPropToBeChanged = new Bug();
            bugsPropToBeChanged.Knob = "resolve";
            bugsPropToBeChanged.Resolution = "FIXED";

            this.UpdateBunchBugs(bugsPropToBeChanged);
        }

        private void verifiedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bug bugsPropToBeChanged = new Bug();
            bugsPropToBeChanged.Knob = "verify";
            bugsPropToBeChanged.Resolution = "";

            this.UpdateBunchBugs(bugsPropToBeChanged);

        }

        private void closedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bug bugsPropToBeChanged = new Bug();
            bugsPropToBeChanged.Knob = "close";
            bugsPropToBeChanged.Resolution = "";

            this.UpdateBunchBugs(bugsPropToBeChanged);

        }

        private void UpdateBunchBugs(Bug bugsPropToBeChanged)
        {
            // get the selected bugs
            List<int> bugs = new List<int>();

            DataGridViewSelectedRowCollection selRows = dgvResults.SelectedRows;

            if (selRows != null)
            {
                for (int i = 0; i < selRows.Count; i++)
                {
                    DataGridViewRow row = selRows[i];

                    int bugID = int.Parse(row.Cells["id"].Value.ToString());

                    bugs.Add(bugID);
                }
            }

            if (bugs.Count > 0)
            {

                IBugBSI bugInterface = (IBugBSI)BLControllerFactory.GetRegisteredConcreteFactory(_connectionID);
                string strError = string.Empty;
                string strResult = bugInterface.UpdateBugs(bugs, bugsPropToBeChanged, out strError);

                if (strError != string.Empty)
                {
                    MessageBox.Show(this, strError, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // successfully updated
                    MessageBox.Show(this, strResult, Messages.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    tsbtnRunQuery_Click(null, null);
                }
            }
            else
            {
                // no code here.
            }

#if DEBUG
            (Utils.FormContainer as MainForm).webBrowser1.DocumentText = BSI.BL.Interfaces.Utils.htmlContents;
#endif


        }

        private void miOpenBugs_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection selRows = dgvResults.SelectedRows;

                if (selRows != null && selRows.Count >= 5)
                {
                    string strMessage = string.Format(Messages.MsgOpenBugs, selRows.Count);

                    DialogResult dr = MessageBox.Show(this, strMessage, Messages.Info, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr == DialogResult.No)
                    {
                        return;
                    }
                    else
                    {
                        // no code here.
                    }
                }

                FormInstanceManager _editBugInstanceManager = FormInstanceManager.GetInstance();

                for (int i = 0; i < selRows.Count; i++)
                {
                    DataGridViewRow row = selRows[i];

                    int selBugID = int.Parse(row.Cells["id"].Value.ToString());

                    Bug cachedBug = null;

                    // check for cached bug
                    if (this.hCachedBugs.ContainsKey(selBugID) == true)
                    {
                        int indexBug = (int)this.hCachedBugs[selBugID];

                        cachedBug = this._bugs[indexBug] as Bug;
                    }

                    FormEditBug frmEditBug = _editBugInstanceManager.OpenEditBugFormInstance(_connectionID, selBugID, cachedBug);
                }



            }
            catch (Exception ex)
            {
                Logger.Write(System.Reflection.MethodInfo.GetCurrentMethod().Name + ": " + ex.Message, LoggingCategory.Exception);

                MessageBox.Show(this, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // option enabled if only a bug has been selected

                DataGridViewSelectedRowCollection selRows = dgvResults.SelectedRows;

                if (selRows.Count == 1)
                {
                    DataGridViewRow row = selRows[0];

                    int selBugID = int.Parse(row.Cells["id"].Value.ToString());

                    this.EnableLoadingPanel(true);

                    int indexHT = (int)hBugs[selBugID];

                    // start secondary thread when getting bug details.

                    BackgroundWorker bkgBugDetails = new BackgroundWorker();

                    bkgBugDetails.DoWork += new DoWorkEventHandler(bkgBugDetails_DoWork);

                    bkgBugDetails.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgBugDetails_RunWorkerCompleted);

                    bkgBugDetails.ProgressChanged += new ProgressChangedEventHandler(bkgBugDetails_ProgressChanged);

                    bkgBugDetails.WorkerReportsProgress = true;

                    bkgBugDetails.WorkerSupportsCancellation = true;

                    ArrayList al = new ArrayList();

                    al.Add(bugID);

                    al.Add(indexHT);

                    // start run async thread.
                    bkgBugDetails.RunWorkerAsync(al);

                }
                else
                {
                    // no code here
                }


            }
            catch (Exception ex)
            {
                Logger.Write(System.Reflection.MethodInfo.GetCurrentMethod().Name + ": " + ex.Message, LoggingCategory.Exception);

                MessageBox.Show(this, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void cmBugs_Opening(object sender, CancelEventArgs e)
        {
            reloadToolStripMenuItem.Enabled = dgvResults.SelectedRows.Count == 1;

            Point LocalMousePosition = dgvResults.PointToClient(Cursor.Position);

            DataGridViewSelectedRowCollection rowsSelected = dgvResults.SelectedRows;

            
            int rowIndex = dgvResults.HitTest(LocalMousePosition.X, LocalMousePosition.Y).RowIndex;

            if (rowIndex >= 0)
            {
                if (!dgvResults.Rows[rowIndex].Selected)
                {
                    for (int i = rowsSelected.Count - 1; i >= 0; i--)
                    {
                        rowsSelected[i].Selected = false;
                    }

                    dgvResults.Rows[rowIndex].Selected = true;
                    GetBugDetails(rowIndex);
                }
            }

        }

        #endregion

        #region Priority Context Menu

        private void LoadPriorityContextMenu()
        {
            // load priority context 
            CatalogueManager _catalogueManager = CatalogueManager.Instance();

            CataloguesNew cataloguesPerConnection = _catalogueManager.GetCataloguesForConnection(_connectionID);

            if (cataloguesPerConnection.cataloguePriority.Count > 0)
            {
                ToolStripItem[] priorityItems = new ToolStripItem[cataloguesPerConnection.cataloguePriority.Count];



                for (int i = 0; i < cataloguesPerConnection.cataloguePriority.Count; i++)
                {
                    priorityItems[i] = new System.Windows.Forms.ToolStripMenuItem();

                    string[] elem = cataloguesPerConnection.cataloguePriority[i].Split(',');
                    priorityItems[i].Text = elem[0];

                    priorityItems[i].Click += new EventHandler(UCResults_Click);

                    priorityItems[i].Name = "priorityToolStrip" + cataloguesPerConnection.cataloguePriority[i];

                    priorityItems[i].Size = new System.Drawing.Size(143, 22);


                }

                this.priorityToolStripMenuItem.DropDownItems.AddRange(priorityItems);

            }


        }

        void UCResults_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                string priority = sender.ToString();

                Bug bugsPropToBeChanged = new Bug();
                bugsPropToBeChanged.Knob = "none";
                bugsPropToBeChanged.Resolution = "FIXED";
                bugsPropToBeChanged.Priority = priority; 

                this.UpdateBunchBugs(bugsPropToBeChanged);

            }
        }


        #endregion

        private void dgvResults_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }

    public class OnExecuteQueryCompletedArgs : EventArgs
    {
        private string _queryNodeKey;
        private int _bugsCount;

        private TreeView _tv;

        public TreeView TreeView
        {
            get { return _tv; }
            set { _tv = value; }
        }
	

        public string QueryNodeKey
        {
            get { return _queryNodeKey; }
        }

        public int BugsCount
        {
            get { return _bugsCount; }
            set { _bugsCount = value; }
        }

        public OnExecuteQueryCompletedArgs(TreeView TV, string QueryNodeKey, int bugsCount)
        {
            _tv = TV;
            _queryNodeKey = QueryNodeKey;
            _bugsCount = bugsCount;
        }
    }

    public class OnQueryNameChangedArgs : EventArgs
    {
        private string _queryName;

        public string QueryName
        {
            get { return _queryName; }
        }

        private string _folderName;

        public string FolderName
        {
            get { return _folderName; }
        }


        private int _queryID;

        public int QueryID
        {
            get { return _queryID; }
        }


        public OnQueryNameChangedArgs(int QueryID, string QueryName, string FolderName)
        {
            _queryName = QueryName;
            _queryID = QueryID;
            _folderName = FolderName;
        }
    }

}
