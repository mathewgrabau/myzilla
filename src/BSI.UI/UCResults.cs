using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MyZilla.BL.Interfaces;
using MyZilla.UI.Properties;
using MyZilla.BusinessEntities;

//using Microsoft.Reporting.WinForms; 
using Tremend.Logging;


namespace MyZilla.UI
{
    public partial class UCResults : UserControl
    {


        #region Variables

        private List<MyZilla.BusinessEntities.Bug> _bugs  = new List<MyZilla.BusinessEntities.Bug>();
        private Hashtable hBugs = new Hashtable();
        private Hashtable hCachedBugs = new Hashtable();
        private int  _selectedBugId = -1;
        private int _connectionId;
        private bool _searchCriteriaUILoaded;
        private TreeView _tree;
        private NameValueCollection _searchParams;
        private bool _gettingBug;
        private bool gettingBugsDetails;
        private bool _queryCriteriaVisible;
        private bool _showCriteriaVisible;
        UCAdvancedSearchMobile _ucAdvSearch;
        List<string> _severityShortNames = new List<string>();
        bool        _isBugSeverityColVisible;
        bool        _executeQueryAutomatically;
        bool        _isSearchingBugs;
        MyZillaSettingsDataSet _appSettings = MyZillaSettingsDataSet.GetInstance();
        bool _columnsChangeOrderAllowed;
        private ArrayList _sortedColumns = new ArrayList();
        private SortOrder _realSortOrder = SortOrder.None;
        private string SEVERITY_COL_NAME = "bug_severity";
        private string SEVERITY_ID_COL_NAME = "severity_id";
        private string BUG_ID_COL_NAME = "bug_id";

        private string _queryNodeKey;
        private int splitterInitialDistance;
        private AsyncOperationManagerList asyncOpManager = AsyncOperationManagerList.GetInstance();
        private int scrollOffset;
        private bool _blockReadingScrollPosition;
        private bool _blockRowSelectionFromRowEnter;
        private int _defaultNewColummnWidth = 70;
        private int _maxDisplayIndex = -1;

        #endregion

        #region Properties

        public bool IsGettingBugs {
            get {
                return this._gettingBug;
            }
        }

        public bool IsSearchingBugs {
            get {
                return _isSearchingBugs;
            }
        }

        private bool _isClosing;

        /// <summary>
        /// True if user has pressed the closing tab button
        /// </summary>
        public bool IsClosing
        {
            get { return _isClosing; }
            set { _isClosing = value; }
        }

        public bool DisplayQueryCriteria {
            get {
                return this._showCriteriaVisible;
            }
            set {
                this._showCriteriaVisible = value;
            }
        }

        #endregion

        #region Events

        public delegate void ExecuteQueryCompletedEventHandler(object sender, OnExecuteQueryCompletedEventArgs e);

        public event ExecuteQueryCompletedEventHandler ExecuteQueryCompleted;

        public delegate void QueryNameChangedEventHandler(object sender, OnQueryNameChangedEventArgs e);

        public event QueryNameChangedEventHandler QueryNameChanged;

        #endregion

        #region Constructors

        public UCResults()
        {
            InitializeComponent();
        }

        public UCResults(int userId, List<MyZilla.BusinessEntities.Bug> bugList, NameValueCollection searchConfiguration, TreeView treeView, bool showQueryCriteria, bool queryCriteriaVisible, bool executeQueryAutomatically, string queryNodeKey)
        {
            InitializeComponent();
            
            Bugs = bugList;
            _searchParams = searchConfiguration;
            _tree = treeView;
            _connectionId = userId;
            _queryCriteriaVisible = queryCriteriaVisible;
            _showCriteriaVisible = showQueryCriteria;
            _executeQueryAutomatically = executeQueryAutomatically;
            _queryNodeKey = queryNodeKey;

            if (IsGeneralQuery)
                tbnShow.Enabled = false;

            CreateSeverityDictionary();

            _ucAdvSearch = new UCAdvancedSearchMobile(_searchParams, _connectionId);
            _ucAdvSearch.PressKeyEnterEvent += new EventHandler(_ucAdvSearch_PressKeyEnterEvent); 
            splitContainer5.Panel1.Controls.Add(_ucAdvSearch);
            _ucAdvSearch.Dock = DockStyle.Fill;

            splitterInitialDistance = splitContainer1.SplitterDistance;

            SetQueryCriteriaVisibility();
        }

        /// <summary>
        /// Capture the Enter press event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _ucAdvSearch_PressKeyEnterEvent(object sender, EventArgs e)
        {
            btnRunQuery_Click(this, new EventArgs());
        }


        #endregion

        #region Properties

        bool IsGeneralQuery
        {
            get
            {
                return (_searchParams["content"] != null && _searchParams["content"].Length > 0);
            }
        }

        public NameValueCollection SearchCriteria {
            get {
                return _searchParams;
            }
            set
            {
                _searchParams = value;
            }
        }

        public int SelectedBugId
        {
            get { 
                return _selectedBugId; 
            }
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
                if (catalogues.GetCataloguesForConnection(_connectionId) == null)
                    return false;
                else return true;

            }

        }

        #endregion

        #region Public methods

        public List<MyZilla.BusinessEntities.Bug> Bugs
        {
            get { return this._bugs; }
            set { 
                this._bugs = value;
                
                IndexBugs();
            }
        }

        /// <summary>
        /// Returns the dataview currently displayed in the grid
        /// </summary>
        public DataView BugsView
        {
            get {
                return (DataView)dgvResults.DataSource; 
            }
        }

        public List<DataRow> MultipleSelectedRows = new List<DataRow>();

        /// <summary>
        /// Load bugs according to the specified search criteria.
        /// </summary>
        /// <param name="bugs"></param>
        /// <param name="sp"> 
        /// // added by mionescu 2008-Jan-17
        /// Search param collection.
        /// If null, the prevoius search parameters (in <c> _searchParams</c>) are used.
        /// </param>
        public void LoadBugs(List<MyZilla.BusinessEntities.Bug> bugs, NameValueCollection sp)
        {
            if (sp != null)
            {
                _searchParams = sp;

                _ucAdvSearch.SetUISearchCriteriaFromUI();
            }

            if (!_gettingBug)
            {
                Bugs = bugs;

                if (bugs == null)
                {
                    ExecuteQuery();
                }
                else
                    GetGridWithResults();
            }

            //GetBugsDetailsForVisibleBugs();
        }

        #endregion

        #region Private methods

        private void LoadEditBugWindow(int CurrentRowIndex)
        {
            if (CurrentRowIndex >= 0)
            {
                int bugId = Int32.Parse(dgvResults.Rows[CurrentRowIndex].Cells[BUG_ID_COL_NAME].Value.ToString());

                FormInstanceDictionary _editBugInstanceManager = FormInstanceDictionary.GetInstance();

                Bug cachedBug = null;

                // check for cached bug
                if (this.hCachedBugs.ContainsKey(bugId) == true)
                {
                    int indexBug = (int)this.hCachedBugs[bugId];

                    cachedBug = this._bugs[ indexBug ] as Bug ;  
                }

                FormEditBug frmEditBug =  _editBugInstanceManager.OpenEditBugFormInstance(_connectionId, bugId, cachedBug ); 

                // subscribe to the updated bug event
                frmEditBug.UpdatedBug += new FormEditBug.UpdatedBugEventHandler(frmEditBug_UpdatedBug); 

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
                    hBugs.Add(_bugs[i].Id, i);
                }
            }
        }

        /// <summary>
        /// Builds the gridview will all properties set according to the user preferences
        /// </summary>
        private void GetGridWithResults()
        {

            _columnsChangeOrderAllowed = false;

            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            DataTable dtBugs = BuildDataSource();

            DataView dv = dtBugs.DefaultView;

            dv.Sort = GetSortString();

            dgvResults.DataSource = dv;

            #region add attachment column
            if (dgvResults.Columns.Contains("attach"))
                dgvResults.Columns.Remove("attach");

            DataGridViewColumn attachCol = new DataGridViewImageColumn();
            attachCol.Name = "attach";
            attachCol.HeaderText = String.Empty;
            //attachCol.HeaderCell.Value = Resources.attach;
            //attachCol.DisplayIndex = 0;

            attachCol.Width = 18;
            attachCol.DisplayIndex = 0;
            attachCol.Resizable = DataGridViewTriState.False;

            dgvResults.Columns.Add(attachCol);
            #endregion

            PaintGrid();

            SetGridSortingOrder();

            lblCountResults.Text = String.Format("Bugs: {0}", dtBugs.Rows.Count);

            int lastVisibleColumnIndex = dgvResults.Columns.Count - 2;

            int totalVisibleColumnsWidth = 0;

            int nrOfNewColumns = 0;

            _maxDisplayIndex = -1;

            for (int i = 0; i < dgvResults.Columns.Count; i++)
                dgvResults.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            DataRow[] rows = _appSettings.Columns.Select("ConnectionID = " + _connectionId + " AND DisplayIndex<>-1");

            bool noColumnConfiguration = (rows.GetLength(0) == 1);

            rows = _appSettings.Columns.Select("ConnectionID = " + _connectionId);

            for (int i = 0; i < dgvResults.Columns.Count; i++)
            {
                //get the settings for the current column
                rows = _appSettings.Columns.Select("ConnectionID = " + _connectionId + " AND Name = '" + dgvResults.Columns[i].Name + "'");

                if (rows != null && rows.GetLength(0) == 1)
                {
                    MyZillaSettingsDataSet.ColumnsRow currentColumnSettings = (MyZillaSettingsDataSet.ColumnsRow)rows[0];

                    int displayIndex = currentColumnSettings.DisplayIndex;

                    //new column added by user to be displayed
                    if (displayIndex == -1)
                    {
                        dgvResults.Columns[i].DisplayIndex = dgvResults.Columns.Count - 1;

                        currentColumnSettings.DisplayIndex = dgvResults.Columns.Count - 1;

                        nrOfNewColumns++;
                    }
                    else
                        dgvResults.Columns[i].DisplayIndex = Math.Min(displayIndex, dgvResults.Columns.Count - 1);

                    if (dgvResults.Columns[i].DisplayIndex >= _maxDisplayIndex)
                    {
                        lastVisibleColumnIndex = i;
                        _maxDisplayIndex = dgvResults.Columns[i].DisplayIndex;
                    }

                    dgvResults.Columns[i].Width = currentColumnSettings.Width;

                }
                else
                {
                    if (dgvResults.Columns[i].Name == SEVERITY_ID_COL_NAME)
                        dgvResults.Columns[i].Visible = false;
                }

                if (dgvResults.Columns[i].Visible)
                    totalVisibleColumnsWidth += dgvResults.Columns[i].Width;
            }

            //new grid columns will have by default 5 px width which must be substract from total
            //totalVisibleColumnsWidth -= nrOfNewColumns * 5;

            for (int i = 0; i < dgvResults.Columns.Count; i++)
            {
                if (noColumnConfiguration)
                {
                    if (dgvResults.Columns[i].Name != "attach")
                        dgvResults.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                    else
                    {
                        dgvResults.Columns["attach"].DisplayIndex = 0;
                    }
                }
                else if (dgvResults.Columns[i].Visible)
                {
                    if (dgvResults.Columns[i].Width == 5)
                    {
                        dgvResults.Columns[i].Width = _defaultNewColummnWidth;
                    }
                    else
                    {
                        if ((totalVisibleColumnsWidth) != 0 && (nrOfNewColumns > 0) && (!noColumnConfiguration) && dgvResults.Columns[i].Name != "attach")
                            dgvResults.Columns[i].Width -= (int)((nrOfNewColumns * _defaultNewColummnWidth * dgvResults.Columns[i].Width) / (double)totalVisibleColumnsWidth);
                    }
                }
            }

            //if (noColumnConfiguration)
            //    dgvResults.Columns["attach"].DisplayIndex = 0;

            dgvResults.Columns[lastVisibleColumnIndex].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //set width for the id column
            if (dtBugs.Rows.Count > 0)
            {
                KeepBugSelection();
            }

            if (!_isBugSeverityColVisible)
                dgvResults.Columns[SEVERITY_COL_NAME].Visible = false;
            else
                dgvResults.Columns[SEVERITY_COL_NAME].Visible = true;

            _columnsChangeOrderAllowed = true;
        }

        private List<Bug> GetBugsDetailsForVisibleBugs() {
            List<Bug> dBugs = new List<Bug>();

            if (!IsClosing)
            {
                GetBugDetails();
                
                if (!this.gettingBugsDetails)
                {
                    this.gettingBugsDetails = true;

                    StringBuilder sb = new StringBuilder();
                    string pattern = "id={0}&";
                    try
                    {
                        int nrOfBugsToCache = 0;

                        int wide = 50;

                        int lowerLimit = dgvResults.FirstDisplayedScrollingRowIndex - wide;
                        if (lowerLimit < 0)
                            lowerLimit = 0;

                        int upperLimit = 0;

                        if (lowerLimit == 0)
                            upperLimit = dgvResults.FirstDisplayedScrollingRowIndex + 2 * wide;
                        else
                            upperLimit = dgvResults.FirstDisplayedScrollingRowIndex + wide;

                        if (upperLimit > ((DataView)dgvResults.DataSource).Count)
                            upperLimit = ((DataView)dgvResults.DataSource).Count;

                        for (int i = lowerLimit; i < upperLimit; i++)
                        {
                            int bugId = Convert.ToInt32(((DataView)dgvResults.DataSource)[i][BUG_ID_COL_NAME]);

                            if (!hCachedBugs.ContainsKey(bugId))
                            {
                                sb.Append(String.Format(pattern, bugId));
                                nrOfBugsToCache++;
                            }
                        }

                        if (sb.Length > 0)
                        {
                            #region start secondary thread for getting bugs details.

                            BackgroundWorker bkgBugsDetails = new BackgroundWorker();

                            bkgBugsDetails.DoWork += new DoWorkEventHandler(bkgBugsDetails_DoWork);

                            bkgBugsDetails.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgBugsDetails_RunWorkerCompleted);

                            bkgBugsDetails.ProgressChanged += new ProgressChangedEventHandler(bkgBugsDetails_ProgressChanged);

                            bkgBugsDetails.WorkerReportsProgress = true;

                            bkgBugsDetails.WorkerSupportsCancellation = true;

                            ArrayList al = new ArrayList();

                            al.Add(sb.ToString());

                            // start run async thread.
                            bkgBugsDetails.RunWorkerAsync(al);

                            #endregion
                        }
                        else
                        {
                            this.gettingBugsDetails = false;
                        }
                    }
                    catch(Exception ex)
                    {
                        this.gettingBugsDetails = false;

                        MyLogger.Write(ex, "GetBugDetailsForVisibleBugs", LoggingCategory.Exception);
                    }
                }
            }
            return dBugs;
        }

        private void KeepBugSelection()
        {
            int indexOfTheSelBug = 0;

            if (_selectedBugId != -1)
            {

                // get the index of the previous selected bug

                foreach (DataGridViewRow row in dgvResults.Rows)
                {

                    int currentBugId = Convert.ToInt32(row.Cells[BUG_ID_COL_NAME].Value);

                    if (currentBugId == _selectedBugId)
                    {
                        indexOfTheSelBug = row.Index;

                        break;
                    }

                }

            }
            else {
                //NO CODE
            }

            if (dgvResults.Rows.Count > 0)
            {
                _blockReadingScrollPosition = true;

                _selectedBugId = Convert.ToInt32(dgvResults.Rows[indexOfTheSelBug].Cells[BUG_ID_COL_NAME].Value);

                dgvResults.CurrentCell = dgvResults.Rows[indexOfTheSelBug].Cells[BUG_ID_COL_NAME];

                if (this.scrollOffset < dgvResults.Rows.Count)
                    dgvResults.FirstDisplayedScrollingRowIndex = this.scrollOffset;

                _blockReadingScrollPosition = false;

                GetBugDetails();
            }
        }

        private void SetQueryCriteriaVisibility(){
            if (_showCriteriaVisible){
                ShowQueryCriteria();
            }else{
                HideQueryCriteria();
            }
        }

        public void ExecuteQuery()
        {
            SetQueryCriteriaVisibility();

            if (_executeQueryAutomatically)
            {
                if (!_gettingBug)
                {
                    try
                    {
                        if (_queryCriteriaVisible && _searchCriteriaUILoaded)
                            _searchParams = _ucAdvSearch.GetSearchCriteria();

                        this.EnableLoadingPanel(true);

                        if (!IsGeneralQuery)
                            _searchParams.Remove("content");

                        ClearBugDetailsArea();

                        _gettingBug = true;

                        _isSearchingBugs = true;

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
                        MyLogger.Write(ex, "ExecuteQuery", LoggingCategory.Exception);

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
            this.dgvResults.Paint -= new System.Windows.Forms.PaintEventHandler(this.dgvResults_Paint);

            if (dgvResults.Columns.Contains(SEVERITY_COL_NAME))
            {
                int index;
                int normalSeverityIndex = _severityShortNames.IndexOf("normal");
                int multiplierForRedRange;
                int multiplierForGrayRange;
                foreach (DataGridViewRow r in dgvResults.Rows)
                {
                    //if (((int)r.Cells[BUG_ID_COL_NAME].Value)%2==0)
                    //    r.HeaderCell.Value = "A";
                    //else
                    //    r.HeaderCell.Value = "B";
                    if (hCachedBugs.ContainsKey(r.Cells[BUG_ID_COL_NAME].Value) && _bugs != null)
                    {
                        Bug bug = _bugs[(int)hCachedBugs[r.Cells[BUG_ID_COL_NAME].Value]];
                        if (bug.Attachments.Count > 0)
                        {
                            ((DataGridViewImageCell)r.Cells["attach"]).Value = Resources.attach;
                        }
                        else
                        {
                            ((DataGridViewImageCell)r.Cells["attach"]).Value = Resources.no_attach;
                        }
                    }
                    else {
                        ((DataGridViewImageCell)r.Cells["attach"]).Value = Resources.no_attach;
                    }

                    index = _severityShortNames.IndexOf(r.Cells[SEVERITY_COL_NAME].Value.ToString().TrimEnd(' '));
                    
                    if (index > normalSeverityIndex)
                    {
                        multiplierForRedRange = _severityShortNames.Count - index - 1;
                        r.DefaultCellStyle.ForeColor = Color.FromArgb(255, multiplierForRedRange * 50, multiplierForRedRange * 50);
                    }
                    else if (index <= normalSeverityIndex)
                    {
                        multiplierForGrayRange = normalSeverityIndex - index;
                        r.DefaultCellStyle.ForeColor = Color.FromArgb(multiplierForGrayRange * 30, multiplierForGrayRange * 30, multiplierForGrayRange * 30);
                    }
                }
            }
            for (int i = 0; i < dgvResults.Columns.Count; i++) {
                dgvResults.Columns[i].SortMode = DataGridViewColumnSortMode.Automatic;
                if (i < ((DataView)dgvResults.DataSource).Table.Columns.Count)
                    dgvResults.Columns[i].HeaderText = ((DataView)dgvResults.DataSource).Table.Columns[i].Caption;
            }

            //_isGridPainting = false;
            this.dgvResults.Paint += new System.Windows.Forms.PaintEventHandler(this.dgvResults_Paint);
        }

        private void CreateSeverityDictionary()
        {
            CatalogueManager catalogues = CatalogueManager.Instance();
            List<string> sev = catalogues.GetCataloguesForConnection(_connectionId).catalogueSeverity;

            foreach (string elem in sev)
                _severityShortNames.Insert(0, elem.Split(',')[0]);

        }

        private DataTable BuildDataSource()
        {
            DataTable dtBugs = new DataTable();

            if (!IsClosing && _bugs != null)
            {
                if (_appSettings.Columns.Select("ConnectionID = " + _connectionId.ToString() + " AND Name = 'bug_id'" ).GetLength(0)==0)

                    _appSettings.Columns.AddColumnsRow(_connectionId, BUG_ID_COL_NAME, "Id", 0, 0, 0, 1, true);

                _appSettings.Columns.AcceptChanges();

                DataRow[] rows = _appSettings.Columns.Select("ConnectionID = " + _connectionId.ToString());

                MyZillaSettingsDataSet.ColumnsRow rowCol;

                _isBugSeverityColVisible = false;
                
                _sortedColumns.Clear();

                foreach (DataRow row in rows)
                {
                    rowCol = (MyZillaSettingsDataSet.ColumnsRow)row;

                    DataColumn dc =  new DataColumn(rowCol.Name);
                    
                    dc.Caption =  rowCol.Label;
                    
                    dtBugs.Columns.Add(dc);
                    
                    if (rowCol.Label == "Id")
                        dtBugs.Columns[BUG_ID_COL_NAME].DataType = Type.GetType("System.Int32");
                    if (rowCol.Label == "Changed")
                        dtBugs.Columns["changeddate"].DataType = Type.GetType("System.DateTime");
                    if (rowCol.Label == "Opened")
                        dtBugs.Columns["opendate"].DataType = Type.GetType("System.DateTime");
                    if (rowCol.Label == "Deadline")
                        dtBugs.Columns["deadline"].DataType = Type.GetType("System.DateTime");
                    else if (rowCol.Name == SEVERITY_COL_NAME && rowCol.Visible)
                        _isBugSeverityColVisible = true;

                    if (rowCol.Sort>0){
                        if (rowCol.SortIndex == 1)
                        {
                            _sortedColumns.Insert(0, new SortedColumn(rowCol.Name, GetSortType((SortOrder)rowCol.Sort)));
                            _realSortOrder = (SortOrder)rowCol.Sort;
                        }
                        else
                        {
                            _sortedColumns.Add(new SortedColumn(rowCol.Name, GetSortType((SortOrder)rowCol.Sort)));
                        }

                    }
                }

                //this region is nedded for preventing errors 
                //generated by renaming fields from one version of MyZilla to the next one
                #region prevention code
                if (_sortedColumns.Count > 2) {
                    _sortedColumns.RemoveRange(2, _sortedColumns.Count - 2);
                }
                else if (_sortedColumns.Count == 0) {
                    _sortedColumns.Add(new SortedColumn(BUG_ID_COL_NAME, "ASC"));
                }
                #endregion

                dtBugs.Columns.Add(SEVERITY_ID_COL_NAME, Type.GetType("System.Int32"));

                for (int iBug = 0; iBug < this._bugs.Count; iBug++)
                {
                    DataRow dr = dtBugs.NewRow();

                    #region column value set
                    int i = 0;
                    foreach (DataRow row in rows)
                    {
                        rowCol = ((MyZillaSettingsDataSet.ColumnsRow)row);
                        switch (rowCol.Name)
                        {
                            case "":
                                dr[i++] = "A";
                                break;

                            case "severity_id":
                                dr[i++] = _severityShortNames.IndexOf(Bugs[iBug].Severity.TrimEnd(' '));
                                break;

                            case "bug_id":
                                dr[i++] = Bugs[iBug].Id;
                                break;

                            case "alias":
                                dr[i++] = Bugs[iBug].Alias;
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

                            case "classification":
                                dr[i++] = Bugs[iBug].Classification;
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

                            case "target_milestone":
                                dr[i++] = Bugs[iBug].TargetMilestone;
                                break;

                            case "qa_contact":
                                dr[i++] = Bugs[iBug].QAContact;
                                break;
                            case "qa_contact_realname":
                                dr[i++] = Bugs[iBug].QAContactRealName;
                                break;

                            case "status_whiteboard":
                                dr[i++] = Bugs[iBug].StatusWhiteboard;
                                break;

                            case "keywords":
                                dr[i++] = Bugs[iBug].Keywords;
                                break;

                            case "estimated_time":
                                dr[i++] = Bugs[iBug].EstimatedTime;
                                break;

                            case "remaining_time":
                                dr[i++] = Bugs[iBug].RemainingTime;
                                break;

                            case "actual_time":
                                dr[i++] = Bugs[iBug].ActualTime;
                                break;

                            case "percentage_complete":
                                dr[i++] = Bugs[iBug].PercentageComplete;
                                break;

                            case "deadline":
                                dr[i++] = Bugs[iBug].Deadline;
                                break;
                            
                            case "short_desc":
                                dr[i++] = Bugs[iBug].FullSummary;
                                break;

                            case "short_short_desc":
                                dr[i++] = Bugs[iBug].Summary;
                                break;

                            case "cf_custom_field":
                                dr[i++] = Bugs[iBug].CustomField;
                                break;
                            
                            default:
                                i++;
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

        private void ShowBugDetails(MyZilla.BusinessEntities.Bug currentBug)
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

                    lblBugId.Text = currentBug.Id.ToString();

                    lblSummary.Text = String.Format("{0}  {1}", currentBug.Id, currentBug.Summary);

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

                    lblURLValue.Text = currentBug.Url;

                    lblDescr.Text = String.Empty;

                    lblAttachment.Text = String.Format("Attachments:    {0}", currentBug.Attachments.Count);

                    if (currentBug.Attachments.Count > 0)
                    {
                        dgvAttachments.AutoGenerateColumns = false;
                        dgvAttachments.DataSource = currentBug.Attachments;
                        dgvAttachments.Height = currentBug.Attachments.Count * dgvAttachments.Rows[0].Height;
                        dgvAttachments.Visible = true;
                        //btnAtach.Visible = true;
                    }
                    else {
                        dgvAttachments.Visible = false;
                        //btnAtach.Visible = false;
                    }
                    

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
                MyLogger.Write(ex, "ShowBugDetails", LoggingCategory.Exception);

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
            if (!splitContainer5.Panel1Collapsed)
            {
                splitContainer5.Panel1Collapsed = true;
                splitContainer1.SplitterDistance = (int)(splitContainer1.SplitterDistance * 0.75);
                _ucAdvSearch.Visible = false;
                tbnShow.Image = Properties.Resources.hide;
            }
        }

        private void EnableLoadingPanel(bool enabled)
        {
            if (LoadingPanel.InvokeRequired)
            {
                LoadingPanel.BeginInvoke(new MethodInvoker(delegate()
                {
                    LoadingPanel.Location = new Point((splitContainer1.Panel1.Width - LoadingPanel.Width - 50) / 2, (splitContainer1.Panel1.Height - LoadingPanel.Height) / 2);
                    LoadingPanel.Visible = enabled;
                }));
            }
            else {
                LoadingPanel.Location = new Point((splitContainer1.Panel1.Width - LoadingPanel.Width - 50) / 2, (splitContainer1.Panel1.Height - LoadingPanel.Height) / 2);
                LoadingPanel.Visible = enabled;
            }
        }


        #endregion

        #region Form events

        private void ucResults_Load(object sender, EventArgs e)
        {
            ClearBugDetailsArea();

            this.LoadPriorityContextMenu();

            this.LoadSeverityContextMenu();

            this.LoadResolvedOtherContextMenu();
            
        }

        /// <summary>
        /// Empties the preview pane fields
        /// </summary>
        private void ClearBugDetailsArea(){
            string nothing = String.Empty;

            lblBugId.Text = String.Empty;
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
            lblDescr.Text = String.Empty;
            ucComments1.ClearControl();
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
                ConfigItems.TDSQueriesTree tree = ConfigItems.TDSQueriesTree.Instance();
                //update params collection with values from UI
                try
                {
                    //if search criteria was not opened, _ucAdvSearch.GetSearchCriteria will crash
                    //but saving must continue
                    if (!IsGeneralQuery)
                    {
                        _searchParams = _ucAdvSearch.GetSearchCriteria();

                    }

                }catch{}

                //if (_queryNodeKey == null)
                if (Int32.Parse(_queryNodeKey.Split(' ')[1])<0)
                {
                    #region show dialog for choosing a query name
                    FormQueryName frm = new FormQueryName(_connectionId, _tree, null);

                    DialogResult result = frm.ShowDialog(this);

                    //check respose of the user from the dialog form
                    switch (result)
                    {
                        case DialogResult.OK:
                            queryName = frm.QueryName;
                            folderID = frm.FolderId;
                            queryDescription = frm.QueryDescription;

                            int bugsCount = -1;
                            if (_bugs != null)
                                bugsCount = _bugs.Count;

                            ConfigItems.TDSQueriesTree.QueriesRow query = tree.AddNewQuery(queryName, queryDescription, folderID, (int)QueryType.UserCustom, bugsCount, _searchParams);

                            tree.AddQueryToTree(_tree, query);

                            QueryNameChanged(this, new OnQueryNameChangedEventArgs(query.ID, query.Name, frm.FolderName));

                            _queryNodeKey = "query " + query.ID.ToString();
                            break;

                    }
                    #endregion
                }
                else {
                    TreeNode[] nodes = _tree.Nodes.Find(_queryNodeKey, true);
                    if (nodes.GetLength(0) == 1) {
                        ConfigItems.NodeDescription nodeDescr = (ConfigItems.NodeDescription)nodes[0].Tag;
                        ConfigItems.TDSQueriesTree.QueriesRow query = (ConfigItems.TDSQueriesTree.QueriesRow)nodeDescr.NodeData;
                        if (!tree.UpdateQuery(query.ID, _searchParams))
                            MessageBox.Show("Updating query failed!");
                        else {
                            MessageBox.Show(String.Format(Properties.Messages.QuerySuccessfulSaved, query.Name), "Query saved", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                        }
                    }
                }
            }
        }

        private void dgvResults_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!_isSearchingBugs)
                _selectedBugId = Convert.ToInt32(dgvResults.Rows[e.RowIndex].Cells[BUG_ID_COL_NAME].Value);

            if (!this.gettingBugsDetails & !_blockRowSelectionFromRowEnter)
            {
                GetBugsDetailsForVisibleBugs();
            }
            else {
                GetBugDetails();
            }

        }

        /// <summary>
        /// Gets the details of a bug (from cash or from the server)
        /// and displayes them in the preview pane
        /// </summary>
        private void GetBugDetails()
        {
            //preview pane is visible
            if (!splitContainer1.Panel2Collapsed)
            {
                try
                {
                    if (this.ParentForm != null)
                    {
                        //set url for current selected bug on top of the main form
                        ((MainForm)this.ParentForm).txtBugURL.Text = Utils.GetBugURL(this._connectionId, _selectedBugId.ToString());
                    }
                    if ((lblBugId.Text != _selectedBugId.ToString()) && (_selectedBugId != -1))
                    {

                        MyZilla.BusinessEntities.Bug currentBug = GetBugFromCache(_selectedBugId);

                        if (currentBug != null)
                        {
                            ShowBugDetails(currentBug);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MyLogger.Write(ex, String.Format("UCResults.GetBugDetails for {0}", _selectedBugId), LoggingCategory.Exception);

                }
            }
        }

        private Bug GetBugFromCache(int bugId)
        {
            Bug bug = null;

            if (hCachedBugs.ContainsKey(bugId))
            {
                bug = _bugs[(int)hBugs[bugId]];
            }

            return bug;
        }

        private void dgvResults_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && (dgvResults.SelectedRows.Count == 1))
            {
                e.SuppressKeyPress = true;

                LoadEditBugWindow(dgvResults.SelectedRows[0].Index);
            }
        }

        private void btnRunQuery_Click(object sender, EventArgs e)
        {
            if (!IsConnectionAlive)
            {
                MessageBox.Show("The current query connection does not exist anymore! This tab should be closed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                //if (_queryCriteriaVisible && _searchCriteriaUILoaded)
                //    _searchParams = _ucAdvSearch.GetSearchCriteria();
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

        private void btnSelectColumns_Click(object sender, EventArgs e)
        {
            if (!IsConnectionAlive)
            {
                MessageBox.Show("The current query connection does not exist anymore! This tab should be closed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Form selectColumns = new FormSelectCols(_connectionId);
                DialogResult dialogResult = selectColumns.ShowDialog();

                //if select columns form was changed and saved, query is reexecuted
                if (dialogResult == DialogResult.OK)
                {
                    ExecuteQuery();
                }
            }
        }

        private void dgvResults_Paint(object sender, PaintEventArgs e)
        {
            //if (!_isGridPainting)
            //    PaintGrid();
        }

        private void dgvResults_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (_columnsChangeOrderAllowed)
            {
                DataRow[] rows = _appSettings.Columns.Select("ConnectionID = " + _connectionId + " AND Name = '" + e.Column.Name + "'");
                if (rows.GetLength(0) == 1)
                    ((MyZillaSettingsDataSet.ColumnsRow)rows[0]).Width = e.Column.Width;
            }
        }

        private void dgvResults_ColumnDisplayIndexChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (_columnsChangeOrderAllowed && !IsClosing)
            {
                //if (e.Column.Name == "attach") {
                //    _columnsChangeOrderAllowed = false;
                //    dgvResults.Columns["attach"].DisplayIndex = 1;
                //    _columnsChangeOrderAllowed = true;
                //}

                DataRow[] rows = _appSettings.Columns.Select("ConnectionID = " + _connectionId + " AND Name = '" + e.Column.Name + "'");

                if (rows.GetLength(0) == 1)
                {

                    ((MyZillaSettingsDataSet.ColumnsRow)rows[0]).DisplayIndex = e.Column.DisplayIndex;
                    if (e.Column.DisplayIndex == _maxDisplayIndex)
                        e.Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    else
                        e.Column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                }

            }
        }

        struct SortedColumn {
            public string Name;
            public string SortType;

            public SortedColumn(string Name, string SortType) {
                this.Name = Name;
                this.SortType = SortType;
            }
        }

        private static string GetSortType(SortOrder SortType) {
            string result = String.Empty;

            switch (SortType)
            {
                case SortOrder.Ascending:
                    result = "ASC";
                    break;
                case SortOrder.Descending:
                    result = "DESC";
                    break;
                case SortOrder.None:
                    break;
                default:
                    break;
            }

            return result;
           
        }

        private void RefreshSortingConfiguration(DataGridViewColumn SortedColumn, SortOrder SortType) {
            SortedColumn prevColumnSorted = new SortedColumn();

            if (_sortedColumns == null) {
                _sortedColumns = new ArrayList();
            }

            if (_sortedColumns.Count > 0) {
                prevColumnSorted = (SortedColumn)_sortedColumns[0];

                if (prevColumnSorted.Name == SortedColumn.Name)
                {
                    //if ((prevColumnSorted.Name == SEVERITY_ID_COL_NAME) && (SortedColumn.Name == SEVERITY_COL_NAME))
                    if ((SortedColumn.Name == SEVERITY_COL_NAME))
                    {
                        //change sort type
                        if (_realSortOrder == SortOrder.Ascending)
                        {
                            SortType = SortOrder.Descending;
                        }
                        else
                        {
                            SortType = SortOrder.Ascending;
                        }
                    }

                    _sortedColumns.RemoveAt(0);
                }
                else
                {
                    if (_sortedColumns.Count==2)
                        _sortedColumns.RemoveAt(1);
                }
            }


            _sortedColumns.Insert(0, new SortedColumn(SortedColumn.Name, GetSortType(SortType)));
            //if (SortedColumn.Name == SEVERITY_COL_NAME)
            //{
            //    _sortedColumns.Insert(0, new SortedColumn(SEVERITY_ID_COL_NAME, GetSortType(SortType)));
            //}
            //else if ((SortedColumn.Name == SEVERITY_ID_COL_NAME))
            //{
            //    //no code here   
            //}
            //else
            //{
            //    _sortedColumns.Insert(0, new SortedColumn(SortedColumn.Name, GetSortType(SortType)));
            //}

            _realSortOrder = SortType;

            #region synchonize user configuration with user UI modifications
            DataRow[] rows = _appSettings.Columns.Select("ConnectionID = " + _connectionId);

            if (rows != null)
            {
                //walks through the list of columns and refresh sorting parameters
                //  according to the last user actions
                for (int i=0; i<rows.GetLength(0); i++)//(DataRow row in rows)
                {
                    MyZillaSettingsDataSet.ColumnsRow col = (MyZillaSettingsDataSet.ColumnsRow)rows[i];

                    if (col.Name == SortedColumn.Name) {
                        col.SortIndex = 1;
                        col.Sort = (byte)SortType;
                    }
                    else if (col.Name == prevColumnSorted.Name)
                    {
                        col.SortIndex = 2;
                        col.Sort = (byte)(prevColumnSorted.SortType == "ASC" ? 1 : 2);
                    }
                    else
                    {
                        col.SortIndex = 0;
                        col.Sort = (byte)SortOrder.None;
                    }
                }
            }
            #endregion
        }

        private string GetSortString() {
            string result = String.Empty;

            for (int i = 0; i < _sortedColumns.Count; i++) { 

                SortedColumn sortedColumn = (SortedColumn)_sortedColumns[i];

                string columnName = sortedColumn.Name;

                if (columnName == SEVERITY_COL_NAME)
                    columnName = SEVERITY_ID_COL_NAME;

                //prevent null after renaming datatable column names
                if (columnName == "Id")
                    columnName = BUG_ID_COL_NAME;

                result += (i>0?",":"") + columnName + " " + sortedColumn.SortType;
            }

            return result;
        }

        private void SetGridSortingOrder() {
            if ((_sortedColumns!=null && _sortedColumns.Count>0 &&
                ((((SortedColumn)_sortedColumns[0]).Name == SEVERITY_ID_COL_NAME) 
                    || ((SortedColumn)_sortedColumns[0]).Name == SEVERITY_COL_NAME)))
            {
                    dgvResults.Columns[SEVERITY_COL_NAME].HeaderCell.SortGlyphDirection = _realSortOrder;
            }
        }

        

        private void dgvResults_Sorted(object sender, EventArgs e)
        {
            _blockRowSelectionFromRowEnter = true;

            _blockReadingScrollPosition = true;

            RefreshSortingConfiguration(dgvResults.SortedColumn, dgvResults.SortOrder);

            DataView dv = (DataView)dgvResults.DataSource;

            dv.Sort = GetSortString();

            SetGridSortingOrder();

            KeepBugSelection();

            PaintGrid();

            _blockRowSelectionFromRowEnter = false;
            _blockReadingScrollPosition = false;
        }

        void frmEditBug_UpdatedBug(object sender, UpdatedBugEventArgs e)
        {

            btnRunQuery_Click(null, null);

        }


        #endregion

        #region Async - load bug details

        void bkgBugDetails_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker bkgWork = sender as BackgroundWorker;

            int bugId = -1;
            try
            {
                if (e.UserState != null)
                    bugId = (int)e.UserState;

                string strMessage = string.Format(Messages.LoadBugDetails, bugId);

                switch (e.ProgressPercentage)
                {
                    case 0:

                        asyncOpManager.BeginOperation(bkgWork, strMessage, e.ProgressPercentage);

                        break;


                    case 100:

                        asyncOpManager.UpdateStatus(bkgWork, Messages.EndOperation, e.ProgressPercentage);

                        break;

                    default:

                        asyncOpManager.UpdateStatus(bkgWork, strMessage, e.ProgressPercentage);

                        break;

                }
            }
            catch {
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
                        string errorMessage = Messages.ErrGetBugDetails + Environment.NewLine + e.Error.Message;

                        MessageBox.Show(Utils.FormContainer, errorMessage, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }

                    // status OK
                    if (!IsClosing && !e.Cancelled && e.Error == null)
                    {
                        ArrayList alResult = e.Result as ArrayList;

                        MyZilla.BusinessEntities.Bug bugResult = alResult[0] as Bug;

                        int indexHT = int.Parse(alResult[1].ToString());

                        if (_bugs != null && indexHT < _bugs.Count)
                        {
                            _bugs[indexHT] = bugResult;

                            if (!hCachedBugs.ContainsKey(bugResult.Id))
                                hCachedBugs.Add(bugResult.Id, indexHT);
                            if (dgvResults.SelectedRows != null && dgvResults.SelectedRows[0].Cells[BUG_ID_COL_NAME].Value.ToString() == bugResult.Id.ToString())
                            {
                                ShowBugDetails(bugResult);
                            }
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    MyLogger.Write(ex, "bkgBugDetails_RunWorkerCompleted", LoggingCategory.Exception);
                }
                finally
                {
                    this.EnableLoadingPanel(false);

                    _gettingBug = false;
                }
            }
        }

        void bkgBugDetails_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            int bugID = -1;

            try
            {

                _gettingBug = true;

                ArrayList al = e.Argument  as ArrayList;

                bugID = int.Parse(al[0].ToString ());

                if ((dgvResults.SelectedRows.Count == 0) || (bugID != int.Parse(dgvResults.SelectedRows[0].Cells[BUG_ID_COL_NAME].Value.ToString())))
                {
                    e.Cancel = true;

                    return;

                }

                worker.ReportProgress(0, bugID); // start thread.

                worker.ReportProgress(10, bugID);


                IBugBSI bugInterface = (IBugBSI)BLControllerFactory.GetRegisteredConcreteFactory(_connectionId);

                worker.ReportProgress(60, bugID);  //intermediate state

                MyZilla.BusinessEntities.Bug bugDetails = bugInterface.GetBug(bugID);

                bugDetails.Id = bugID;

                worker.ReportProgress(100, bugID);  //completed

#if DEBUG

                (Utils.FormContainer as MainForm).wb.DocumentText = MyZilla.BL.Interfaces.Utils.htmlContents;
#endif

                ArrayList alResult = new ArrayList();

                alResult.Add(bugDetails);

                alResult.Add(al[1].ToString ());

                e.Result = alResult ;

            }
            catch(Exception ex )
            {
                //PROBLEM
                MyLogger.Write(ex, "bkgBugDetails_DoWork", LoggingCategory.Exception);

                worker.ReportProgress(100, bugID);  //completed

                throw ex;
            }
        }

        #endregion

        #region Async - load bugs details

        void bkgBugsDetails_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker bkgWork = sender as BackgroundWorker;

            switch (e.ProgressPercentage)
            {
                case 0:

                    asyncOpManager.BeginOperation(bkgWork, Messages.GettingBulkBugsDetails_Start, e.ProgressPercentage);

                    break;


                case 100:

                    asyncOpManager.UpdateStatus(bkgWork, Messages.GettingBulkBugsDetails_Completed, e.ProgressPercentage);

                    break;

                default:

                    asyncOpManager.UpdateStatus(bkgWork, Messages.GettingBulkBugsDetails_Progress, e.ProgressPercentage);

                    break;

            }
        }

        void bkgBugsDetails_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //if tab(container) is not closing
            if (!_isClosing)
            {
                try
                {

                    // check the status of the async operation.

                    if (e.Error != null)
                    {
                        string errorMessage = Messages.ErrGetBugDetails + Environment.NewLine + e.Error.Message;

                        MessageBox.Show(Utils.FormContainer, errorMessage, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }

                    // status OK
                    if (!e.Cancelled && e.Error == null)
                    {
                        ArrayList alResult = e.Result as ArrayList;

                        List<Bug> setOfBugs = alResult[0] as List<Bug>;

                        int indexOfSelectedBug = (int)hBugs[(int)dgvResults.SelectedRows[0].Cells[BUG_ID_COL_NAME].Value];

                        bool bugLoaded = false;

                        foreach (Bug bug in setOfBugs)
                        {
                            if (!hCachedBugs.ContainsKey(bug.Id))
                            {
                                int index = (int)hBugs[bug.Id];

                                hCachedBugs.Add(bug.Id, index);

                                _bugs[index] = bug;

                                if (index == indexOfSelectedBug)
                                {
                                    bugLoaded = true;
                                    ShowBugDetails(_bugs[index]);
                                }
                            }
                        }

                        if (!bugLoaded) {
                            this.gettingBugsDetails = false;

                            GetBugsDetailsForVisibleBugs();
                        }

                        PaintGrid();

                    }
                    
                }
                catch (Exception ex)
                {
                    MyLogger.Write(ex, "bkgBugsDetails_RunWorkerCompleted", LoggingCategory.Exception);
                }
                finally
                {
                    this.EnableLoadingPanel(false);

                    this.gettingBugsDetails = false;

                }
            }
            
        }

        void bkgBugsDetails_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            string bugs = string.Empty;

            try
            {
                this.gettingBugsDetails = true;

                ArrayList al = e.Argument as ArrayList;

                bugs = (string)al[0];

                if (String.IsNullOrEmpty(bugs))
                {
                    this.gettingBugsDetails = false;

                    e.Cancel = true;

                    return;

                }

                worker.ReportProgress(0, null); // start thread.

                worker.ReportProgress(10, null);


                IBugBSI bugInterface = (IBugBSI)BLControllerFactory.GetRegisteredConcreteFactory(_connectionId);

                worker.ReportProgress(60, null);  //intermediate state

                List<MyZilla.BusinessEntities.Bug> setOfBugs = bugInterface.GetBugs(bugs);

                //build result
                ArrayList alResult = new ArrayList();

                alResult.Add(setOfBugs);

                e.Result = alResult;

                worker.ReportProgress(80, null);  //completed

            }
            finally {
                worker.ReportProgress(100, null);  
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
            MyLogger.Write("Search ended", "bkgSearch_RunWorkerCompleted", LoggingCategory.General);
            //if tab closing was not started
            if (!_isClosing)
            {
                MyLogger.Write("Search ended and is not closing.", "bkgSearch_RunWorkerCompleted", LoggingCategory.General);
                // check the status of the async operation.
                try
                {
                    if (e.Error != null)
                    {

                        string errorMessage = Messages.ErrSearching + Environment.NewLine + e.Error.Message;

                        MessageBox.Show(Utils.FormContainer, errorMessage, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }

                    // status OK
                    if (!e.Cancelled && e.Error == null)
                    {
                        Bugs = e.Result as List<MyZilla.BusinessEntities.Bug>;

                        if (ExecuteQueryCompleted != null)
                        {
                            this.ExecuteQueryCompleted(this, new OnExecuteQueryCompletedEventArgs(_tree, _queryNodeKey, _bugs.Count));
                        }

                        if (!IsClosing)
                            this.GetGridWithResults();
                    }



                    this.EnableLoadingPanel(false);

                    GetBugsDetailsForVisibleBugs();
                }
                catch (Exception ex)
                {
                    MyLogger.Write(ex, "bkgSearch_RunWorkerCompleted", LoggingCategory.Exception);
                }
                finally {
                    _gettingBug = false;
                }
            }
            _isSearchingBugs = false;
        }

        void bkgSearch_DoWork(object sender, DoWorkEventArgs e)
        {

            BackgroundWorker worker = sender as BackgroundWorker;
            try
            {

                worker.ReportProgress(0); // start thread.
                worker.ReportProgress(10);

                IBugBSI bugProvider = (IBugBSI)BLControllerFactory.GetRegisteredConcreteFactory(_connectionId);

                worker.ReportProgress(60);  //intermediate state

                List<MyZilla.BusinessEntities.Bug> bugsFound = bugProvider.SearchBugs(_searchParams);

                worker.ReportProgress(100);  //completed

                e.Result = bugsFound;
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "bkgSearch_DoWork", LoggingCategory.Exception);

                worker.ReportProgress(100);  //completed

                throw;
            }


        }

        #endregion

        #region Context menu 

        private void miReopenBugs(object sender, EventArgs e)
        {
            Bug bugsPropToBeChanged = new Bug();
            bugsPropToBeChanged.Knob = BugKnob.REOPEN;
            bugsPropToBeChanged.Resolution = String.Empty;

            this.StartAsyncBulkUpdateBugs(bugsPropToBeChanged);
        }

        private void fixedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bug bugsPropToBeChanged = new Bug();
            bugsPropToBeChanged.Knob = BugKnob.RESOLVE;
            bugsPropToBeChanged.Resolution = BugResolution.FIXED;

            StartAsyncBulkUpdateBugs(bugsPropToBeChanged);
        }

        private void verifiedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bug bugsPropToBeChanged = new Bug();
            bugsPropToBeChanged.Knob = BugKnob.VERIFY;
            //keep bug resolution the same

            this.StartAsyncBulkUpdateBugs(bugsPropToBeChanged);

        }

        private void closedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bug bugsPropToBeChanged = new Bug();
            bugsPropToBeChanged.Knob = BugKnob.CLOSE;
            //keep bug resolution the same

            this.StartAsyncBulkUpdateBugs(bugsPropToBeChanged);

        }

        private void StartAsyncBulkUpdateBugs(Bug bugValuesToBeChanged) {
            dgvResults.UseWaitCursor = true;
            
            BackgroundWorker bw = new BackgroundWorker();

            bw.DoWork += new DoWorkEventHandler(BulkUpdateBugs_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BulkUpdateBugs_RunWorkerCompleted);
            bw.ProgressChanged += new ProgressChangedEventHandler(BulkUpdateBugs_ProgressChanged);

            bw.RunWorkerAsync(bugValuesToBeChanged);
        }

        void BulkUpdateBugs_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            switch (e.ProgressPercentage)
            {
                case 0:


                    if (asyncOpManager != null)
                    {
                        asyncOpManager.BeginOperation(bw, "Bulk updating bugs started...", e.ProgressPercentage);
                    }

                    break;


                case 100:

                    if (asyncOpManager != null)
                    {

                        asyncOpManager.UpdateStatus(bw, "Bulk updating bugs completed!", e.ProgressPercentage);
                    }

                    break;

                default:

                    if (asyncOpManager != null)
                    {

                        asyncOpManager.UpdateStatus(bw, "Bulk updating bugs...", e.ProgressPercentage);
                    }

                    break;

            }

        }

        void BulkUpdateBugs_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(this, "Bulk update failed!" + Environment.NewLine + e.Error.Message, "Bulk update", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                BulkUpdateResult result = e.Result as BulkUpdateResult;

                if (result.CountBugs>0) 
                    btnRunQuery_Click(null, null);

                MessageBox.Show(this, result.Message, Messages.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            dgvResults.UseWaitCursor = false;

            dgvResults.Cursor = Cursors.Default;
        }

        void BulkUpdateBugs_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            bw.WorkerReportsProgress = true;

            bw.ReportProgress(5);

            Bug bugProperties = e.Argument as Bug;

            bw.ReportProgress(30);

            try
            {
                BulkUpdateResult updateResult = this.BulkUpdateBugs(bugProperties);

                bw.ReportProgress(80);

                e.Result = updateResult;
            }
            finally
            {
                bw.ReportProgress(100);
            }
        }

        
        private BulkUpdateResult BulkUpdateBugs(Bug bugValuesToBeChanged)
        {
            BulkUpdateResult result = new BulkUpdateResult();

            // get the selected bugs
            SortedList bugs = new SortedList(dgvResults.SelectedRows.Count);
            //string result = string.Empty;

            DataGridViewSelectedRowCollection selRows = dgvResults.SelectedRows;

            int countSelectedBugs = 0;
            
            if (selRows != null)
            {
                countSelectedBugs = selRows.Count;

                for (int i = 0; i < selRows.Count; i++)
                {
                    DataGridViewRow row = selRows[i];

                    int bugID = int.Parse(row.Cells[BUG_ID_COL_NAME].Value.ToString());

                    Bug bug = GetBugFromCache(bugID);

                    if (bug != null)
                    {
                        if (Utils.ValidateBugStateTransition(bug, bugValuesToBeChanged))
                            bugs.Add(bugID, bugID);
                    }
                    else {
                        //MessageBox.Show("Bug(s) details not loaded yet!");
                        result.CountBugs = 0;
                        result.Message = "Bug(s) details not loaded yet!";

                        return result;
                    }
                }
            }

            result.CountBugs = bugs.Count;
            result.BugsList = (SortedList)bugs.Clone();

            if (bugs.Count > 0)
            {

                IBugBSI bugInterface = (IBugBSI)BLControllerFactory.GetRegisteredConcreteFactory(_connectionId);
                string strError = null;

                result.Message = bugInterface.UpdateBugs(bugs, bugValuesToBeChanged, out strError);

                if (!string.IsNullOrEmpty(strError))
                {
                    throw new Exception(strError);
                }
                else { 
                    if(countSelectedBugs > bugs.Count){
                        result.Message = "Only bugs that have a valid bug status transition have been updated!" + Environment.NewLine + "Bugs updated: " + result.ListOfBugs;
                    }
                }
            }
            else
            {
                result.Message = Messages.NoBugWithValidStatusTransition;
            }

#if DEBUG
            (Utils.FormContainer as MainForm).wb.DocumentText = MyZilla.BL.Interfaces.Utils.htmlContents;
#endif

            return result;
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

                FormInstanceDictionary _editBugInstanceManager = FormInstanceDictionary.GetInstance();

                for (int i = 0; i < selRows.Count; i++)
                {
                    DataGridViewRow row = selRows[i];

                    int selBugID = int.Parse(row.Cells[BUG_ID_COL_NAME].Value.ToString());

                    Bug cachedBug = null;

                    // check for cached bug
                    if (this.hCachedBugs.ContainsKey(selBugID) == true)
                    {
                        int indexBug = (int)this.hCachedBugs[selBugID];

                        cachedBug = this._bugs[indexBug] as Bug;
                    }
                    if (_editBugInstanceManager.Count < 50)
                    {
                        _editBugInstanceManager.OpenEditBugFormInstance(_connectionId, selBugID, cachedBug);
                    }
                    else {
                        MessageBox.Show(this, "You have too many edit bug windows opened!" + Environment.NewLine + "Please close those you do not need anymore!", "Edit bugs", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "miOpenBugs_Click", LoggingCategory.Exception);

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
                    this.EnableLoadingPanel(true);

                    int indexHT = (int)hBugs[_selectedBugId];

                    // start secondary thread when getting bug details.

                    BackgroundWorker bkgBugDetails = new BackgroundWorker();

                    bkgBugDetails.DoWork += new DoWorkEventHandler(bkgBugDetails_DoWork);

                    bkgBugDetails.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgBugDetails_RunWorkerCompleted);

                    bkgBugDetails.ProgressChanged += new ProgressChangedEventHandler(bkgBugDetails_ProgressChanged);

                    bkgBugDetails.WorkerReportsProgress = true;

                    bkgBugDetails.WorkerSupportsCancellation = true;

                    ArrayList al = new ArrayList();

                    al.Add(_selectedBugId);

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
                MyLogger.Write(ex, "reloadToolStripMenuItem_Click", LoggingCategory.Exception);

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
                    //reset all possible selections
                    for (int i = rowsSelected.Count - 1; i >= 0; i--)
                    {
                        rowsSelected[i].Selected = false;
                    }

                    //select the right clicked row
                    SelectBugInGrid(rowIndex);

                    GetBugsDetailsForVisibleBugs();

                    //GetBugDetails();
                }
            }

        }

        private void SelectBugInGrid(int RowIndex) {
            dgvResults.CurrentCell = dgvResults.Rows[RowIndex].Cells[0];
            //dgvResults.Rows[RowIndex].Selected = true;

            _selectedBugId = Convert.ToInt32(dgvResults.Rows[RowIndex].Cells[BUG_ID_COL_NAME].Value);
        }

        #endregion

        #region Priority Context Menu

        private void LoadPriorityContextMenu()
        {
            try
            {
                // load priority context 
                CatalogueManager _catalogueManager = CatalogueManager.Instance();

                Catalogues cataloguesPerConnection = _catalogueManager.GetCataloguesForConnection(_connectionId);

                if (cataloguesPerConnection.cataloguePriority.Count > 0)
                {
                    ToolStripItem[] priorityItems = new ToolStripItem[cataloguesPerConnection.cataloguePriority.Count];

                    for (int i = 0; i < cataloguesPerConnection.cataloguePriority.Count; i++)
                    {
                        priorityItems[i] = new System.Windows.Forms.ToolStripMenuItem();

                        string[] elem = cataloguesPerConnection.cataloguePriority[i].Split(',');
                        priorityItems[i].Text = elem[0];

                        priorityItems[i].Click += new EventHandler(PriorityItem_Click);

                        priorityItems[i].Name = "priorityToolStrip" + cataloguesPerConnection.cataloguePriority[i];

                        priorityItems[i].Size = new System.Drawing.Size(143, 22);


                    }

                    this.priorityToolStripMenuItem.DropDownItems.AddRange(priorityItems);

                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "LoadPriorityContextMenu", LoggingCategory.Exception);

                throw;

            }


        }

        void PriorityItem_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                string priority = sender.ToString();

                Bug bugsPropToBeChanged = new Bug();
                bugsPropToBeChanged.Knob = BugKnob.NONE;
                bugsPropToBeChanged.Priority = priority;

                this.StartAsyncBulkUpdateBugs(bugsPropToBeChanged);

            }
        }

        #endregion

        #region Resolved/Other Context Menu

        private void LoadResolvedOtherContextMenu()
        {
            try
            {
                List<string> catalogueResolution = null;

                // load priority context 
                CatalogueManager _catalogueManager = CatalogueManager.Instance();

                Catalogues cataloguesPerConnection = _catalogueManager.GetCataloguesForConnection(_connectionId);

                catalogueResolution = cataloguesPerConnection.catalogueResolution;

                if (catalogueResolution!=null && catalogueResolution.Count > 0)
                {
                    ToolStripItem[] resolutions = new ToolStripItem[catalogueResolution.Count-4];
                    int j = 0;
                    for (int i = 0; i < catalogueResolution.Count; i++)
                    {
                        if (j < resolutions.GetLength(0))
                        {
                            string[] elem = catalogueResolution[i].Split(',');

                            if (elem.Length > 0 && !(elem[0].Equals(BugResolution.FIXED) || elem[0].Equals(BugResolution.DUPLICATE) || elem[0].Equals(BugResolution.MOVED) || elem[0].Equals(BugResolution.NONE)))
                            {
                                resolutions[j] = new System.Windows.Forms.ToolStripMenuItem();

                                resolutions[j].Text = elem[0];

                                resolutions[j].Click += new EventHandler(ResolvedOtherItem_Click);

                                resolutions[j].Name = "miResolution" + catalogueResolution[i];

                                resolutions[j].Size = new System.Drawing.Size(143, 22);

                                j++;
                            }
                            
                        }
                    }

                    this.miResolvedOther.DropDownItems.AddRange(resolutions);

                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "LoadResolvedOtherContextMenu", LoggingCategory.Exception);

                throw;

            }
        }

        void ResolvedOtherItem_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                string resolution = sender.ToString();

                Bug bugsPropToBeChanged = new Bug();
                bugsPropToBeChanged.Knob = BugKnob.RESOLVE;
                bugsPropToBeChanged.Resolution = resolution;

                this.StartAsyncBulkUpdateBugs(bugsPropToBeChanged);

            }
        }

        #endregion

        #region Severity Context Menu

        private void LoadSeverityContextMenu()
        {
            try
            {
                // load priority context 
                CatalogueManager _catalogueManager = CatalogueManager.Instance();

                Catalogues cataloguesPerConnection = _catalogueManager.GetCataloguesForConnection(_connectionId);

                if (cataloguesPerConnection.catalogueSeverity.Count > 0)
                {
                    ToolStripItem[] severityItems = new ToolStripItem[cataloguesPerConnection.catalogueSeverity.Count];

                    for (int i = 0; i < cataloguesPerConnection.catalogueSeverity.Count; i++)
                    {
                        severityItems[i] = new System.Windows.Forms.ToolStripMenuItem();

                        string[] elem = cataloguesPerConnection.catalogueSeverity[i].Split(',');

                        severityItems[i].Text = elem[0];

                        severityItems[i].Click += new EventHandler(SeverityItem_Click);

                        severityItems[i].Name = "severityToolStrip" + cataloguesPerConnection.catalogueSeverity[i];

                        severityItems[i].Size = new System.Drawing.Size(143, 22);


                    }

                    this.severityToolStripMenuItem.DropDownItems.AddRange(severityItems);

                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "LoadSeverityContextMenu", LoggingCategory.Exception);

                throw;

            }



        }

        void SeverityItem_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                string newSeverityValue = sender.ToString();

                Bug bugsPropToBeChanged = new Bug();
                bugsPropToBeChanged.Knob = BugKnob.NONE;
                bugsPropToBeChanged.Severity  = newSeverityValue ;

                this.StartAsyncBulkUpdateBugs(bugsPropToBeChanged);

            }
        }

        #endregion

        #region AssignTo Context Menu

        private void StartAsync_GetAssignToList(string Product, NameValueCollection CatalogCollection) {
            BackgroundWorker bwAssignTo = new BackgroundWorker();

            bwAssignTo.DoWork += new DoWorkEventHandler(bwAssignTo_DoWork);
            bwAssignTo.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwAssignTo_RunWorkerCompleted);

            AssignToParam param = new AssignToParam(Product, CatalogCollection);

            bwAssignTo.RunWorkerAsync(param);
        }

        void bwAssignTo_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                ArrayList result = e.Result as ArrayList;

                ToolStripItem[] assignToItems = new ToolStripItem[1];

                NameValueCollection assignToCatalogue = new NameValueCollection();

                if (result.Count > 0)
                {
                    assignToCatalogue = result[0] as NameValueCollection;
                }

                if (assignToCatalogue.Count > 0)
                {
                    string[] assignToColl = assignToCatalogue.GetValues(0);

                    if (assignToColl.Length > 1)
                    {
                        assignToItems = new ToolStripItem[assignToColl.Length - 1];

                        // 0 = default assignee
                        for (int i = 0; i < assignToColl.Length - 1; i++)
                        {
                            assignToItems[i] = new System.Windows.Forms.ToolStripMenuItem();

                            assignToItems[i].Text = assignToColl[i + 1];

                            assignToItems[i].Click += new EventHandler(AssignToItem_Click);

                            assignToItems[i].Name = "assignToToolStrip" + i.ToString();

                            assignToItems[i].Size = new System.Drawing.Size(143, 22);


                        }
                    }

                    if (assignToColl.Length == 1)
                    {
                        assignToItems = new ToolStripItem[1];

                        assignToItems[0] = new System.Windows.Forms.ToolStripMenuItem();

                        assignToItems[0].Text = assignToColl[0];

                        assignToItems[0].Click += new EventHandler(AssignToItem_Click);

                        assignToItems[0].Name = "assignToToolStrip0";

                        assignToItems[0].Size = new System.Drawing.Size(143, 22);

                    }
                }
                else
                {

                    assignToItems = new ToolStripItem[1];

                    assignToItems[0] = new System.Windows.Forms.ToolStripMenuItem();

                    assignToItems[0].Text = "Error. No assignTo collection. ";

                    assignToItems[0].Size = new System.Drawing.Size(143, 22);

                }

                if (assignToItems.Length > 0)
                {
                    this.assignToToolStripMenuItem.DropDownItems.AddRange(assignToItems);
                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "bwAssignTo_RunWorkerCompleted", LoggingCategory.Exception);
            }
        }

        void bwAssignTo_DoWork(object sender, DoWorkEventArgs e)
        {
            AssignToParam arg = e.Argument as AssignToParam;

            if (arg != null) {
                string product = arg.Product;

                NameValueCollection catalogueComponent = arg.CatalogueComponent;

                IUtilities catalogue = (IUtilities)BLControllerFactory.GetRegisteredConcreteFactory(_connectionId);

                ArrayList alSpecificCatalogues = catalogue.GetSpecificCataloguesWhenManageBug(product, catalogueComponent);

                e.Result = alSpecificCatalogues;

            }
        }

        private class AssignToParam {
            public string Product = String.Empty;
            public NameValueCollection CatalogueComponent = null;

            public AssignToParam(string Product, NameValueCollection CatalogueComponent)
            {
                this.Product = Product;
                this.CatalogueComponent = CatalogueComponent;
            }
        }

        public void LoadAssignToContextMenu()
        {
            EnableLoadingPanel(_executeQueryAutomatically);

            try
            {
                CatalogueManager _catalogueManager = CatalogueManager.Instance();

                Catalogues cataloguesPerConnection = _catalogueManager.GetCataloguesForConnection(_connectionId);

                if (cataloguesPerConnection.catalogueProduct.Count > 0)
                {
                    string productStr = cataloguesPerConnection.catalogueProduct.GetKey(0).Split(',')[0];

                    // al[0] = AssignTo; al[1] = CC
                    // request

                    StartAsync_GetAssignToList(productStr, cataloguesPerConnection.catalogueComponent);

                    return;

                }

            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "LoadAssignToContextMenu", LoggingCategory.Exception);

                throw;

            }

        }

        void AssignToItem_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                string assignTo = sender.ToString();

                Bug bugsPropToBeChanged = new Bug();
                bugsPropToBeChanged.Knob = BugKnob.REASSIGN;
                bugsPropToBeChanged.ReassignTo = assignTo ;

                this.StartAsyncBulkUpdateBugs(bugsPropToBeChanged);

            }
        }


        #endregion

        #region Report

        private void btnShowHidePreviewPane_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;

            if (!splitContainer1.Panel2Collapsed)
            {
                btnPreviewPane.Image = Properties.Resources.show;
                if (dgvResults.SelectedRows.Count > 0)
                {
                    GetBugsDetailsForVisibleBugs();

                    //GetBugDetails();
                }
            }
            else {
                btnPreviewPane.Image = Properties.Resources.hide;
            }
        }

        private void mnuExportToExcel_Click(object sender, EventArgs e)
        {
            this.GetSelectedRows();

            this.DoExport(".xls", mnuExportToExcel , false ); 
        }

        private void mnuExportToPdf_Click(object sender, EventArgs e)
        {
            this.GetSelectedRows();

            this.DoExport(".pdf", mnuExportToPdf, false ); 
        }

        private void DoExport(string extension, object ctrl, bool value){

            if (!this.gettingBugsDetails)
            {
                Utils.ExportTo(extension, ctrl, value);
            }
            else
            {
                MessageBox.Show("Export can not be done during query execution!");
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            this.DoExport(".xls", btnExportToExcel, true); 
        }

        private void btnExportToPDF_Click(object sender, EventArgs e)
        {
            this.DoExport(".pdf", btnExportToPDF, true);
            //gios pdf codeproject
        }

        private void dgvResults_Scroll(object sender, ScrollEventArgs e)
        {
            if (!_blockReadingScrollPosition)
                this.scrollOffset = dgvResults.FirstDisplayedScrollingRowIndex;

            
        }

        /// <summary>
        /// Get selected rows and populate the public variable <c>MultipleSelectedRows</c>
        /// </summary>
        private void GetSelectedRows()
        {

            MultipleSelectedRows.Clear();

            // get the selected bugs
            List<int> bugs = new List<int>();

            DataGridViewSelectedRowCollection selRows = dgvResults.SelectedRows;

            if (selRows != null)
            {
                for (int i = 0; i < selRows.Count; i++)
                {
                    DataGridViewRow row = selRows[i];

                    int bugID = int.Parse(row.Cells[BUG_ID_COL_NAME].Value.ToString());

                    bugs.Add(bugID);
                }
            }

            foreach (int bugId in bugs)
            {
                foreach (DataRow row in (dgvResults.DataSource as DataView).Table.Rows)
                {
                    if (row[BUG_ID_COL_NAME].ToString() == bugId.ToString())
                    {
                        MultipleSelectedRows.Add(row);

                        break;
                    }
                }
            }

        }

        #endregion

        private void dgvResults_Click(object sender, EventArgs e)
        {
            MouseEventArgs args = (MouseEventArgs)e;
            int rowIndex = dgvResults.HitTest(args.X, args.Y).RowIndex;
            if (rowIndex==-1)
                _blockRowSelectionFromRowEnter = true;
            else
                _blockRowSelectionFromRowEnter = false;
        }

        private void lblURLValue_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (lblURLValue.Text.Length > 4)
                    System.Diagnostics.Process.Start(lblURLValue.Text);
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "lblURLValue_MouseClick", LoggingCategory.Exception);

                MessageBox.Show(this, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblURLValue_TextChanged(object sender, EventArgs e)
        {
            if (lblURLValue.Text.Length > 4)
            {
                lblURLValue.Cursor = Cursors.Hand;
            }
            else
            {
                lblURLValue.Cursor = Cursors.IBeam;
            }
        }

        private void dgvResults_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && (e.ColumnIndex>=0) && ((DataGridView)sender).Columns[e.ColumnIndex].Name == "attach")
            {
                e.PaintBackground(e.ClipBounds, true);
                e.Graphics.DrawImageUnscaledAndClipped(Resources.attach, new Rectangle(e.CellBounds.X + 1, (e.CellBounds.Height-Resources.attach.Height)/2, Resources.attach.Width, Resources.attach.Height));
                e.Handled = true;
            }
        }

        private void dgvAttachments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Utils.OpenAttachmentAsync((Control)sender, (int)dgvAttachments.Rows[e.RowIndex].Cells[0].Value, _connectionId);
        }

        private void dgvAttachments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex>=0) {
                DataGridView dgv = (DataGridView)sender;
                Utils.OpenAttachmentAsync(dgv, (int)dgvAttachments.Rows[e.RowIndex].Cells[0].Value, _connectionId);
            }
        }

        private void lblReporterValue_MouseClick(object sender, MouseEventArgs e)
        {
            TextBox reporter = sender as TextBox;
            
            Utils.SendMailTo(reporter);
        }

        private void lblAssignedToValue_MouseClick(object sender, EventArgs e)
        {
            TextBox assignee = sender as TextBox;

            Utils.SendMailTo(assignee);
        }

        class BulkUpdateResult
        {
            public int CountBugs;
            public string Message;
            public SortedList BugsList;

            public string ListOfBugs
            {
                get
                {
                    StringBuilder sb = new StringBuilder();
                    char delimiter = ',';

                    if (BugsList != null)
                    {

                        if (BugsList.Count > 0)
                        {
                            for (int i = 0; i < BugsList.Count; i++)
                            {
                                if (i > 0)
                                    sb.Append(delimiter);

                                sb.Append(BugsList.GetValueList()[i]);
                            }
                        }
                    }

                    return sb.ToString();
                }
            }
        }

    }

    public class OnExecuteQueryCompletedEventArgs : EventArgs
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

        public OnExecuteQueryCompletedEventArgs(TreeView treeView, string queryNodeKey, int bugsCount)
        {
            _tv = treeView;
            _queryNodeKey = queryNodeKey;
            _bugsCount = bugsCount;
        }
    }

    public class OnQueryNameChangedEventArgs : EventArgs
    {
        private string _queryName;

        private string _folderName;

        private int _queryID;

        public string QueryName
        {
            get { return _queryName; }
        }

        public string FolderName
        {
            get { return _folderName; }
        }

        public int QueryId
        {
            get { return _queryID; }
        }

        public OnQueryNameChangedEventArgs(int queryId, string queryName, string folderName)
        {
            _queryName = queryName;
            _queryID = queryId;
            _folderName = folderName;
        }
    }

}
