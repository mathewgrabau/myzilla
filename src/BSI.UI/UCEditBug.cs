using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;

using MyZilla.BL.Interfaces;
using MyZilla.BusinessEntities;
using MyZilla.UI.Properties;

using Tremend.Logging;

namespace MyZilla.UI
{
    public partial class UCEditBug : UserControl
    {
        #region Variable 

        private int bugId;

        private Bug bugToUpdate;

        public event EventHandler CancelEvent;

        private ErrorProvider ep = new ErrorProvider();

        private CatalogueManager _catalogues ;

        private AsyncOperationManagerList _asyncOpManager;

        private MyZillaSettingsDataSet _appSettings;

        private int connectionId;

        private Catalogues cataloguesPerUser;

        private UCBugStatus ucBugStatus;

        private bool _updateSuccessfully;

        private bool ccBugListCompleted;

        private bool ccCatalogListCompleted;

        private bool ccCompleted;

        private bool _formClosed;

        private bool gettingProductDependentCatalogsStarted;

        private bool allowAddingPersonsToCcList = true;

        private string _bugResolution;

        private string _bugKnob;

        private string _bugDuplicateBug;

        private string _bugReassignTo;

        private int bugLastUpdatedSeconds;

        #endregion

        #region Constants

        private const int CST_ATTACHMENT_ID = 0;

        #endregion

        #region Constructor

        public UCEditBug()
        {
            InitializeComponent();

            //if (bugToUpdate == null)
            //{
            //    bugToUpdate = new Bug();
            //}

        }

        #endregion

        #region Form events

        private void EditBug_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.DesignMode)
                {

                    _appSettings = MyZillaSettingsDataSet.GetInstance();

                    _asyncOpManager = AsyncOperationManagerList.GetInstance();

                    // disable the control until de bug details are loaded and 
                    // all the controls are populated accordingly.

                    ShowConnectionInfo();

                    this.Enabled = false;

                    _catalogues = CatalogueManager.Instance();

                    //if (_catalogues.DependentCataloguesLoadedCompleted!=null)
                        _catalogues.DependentCataloguesLoadedCompleted += new EventHandler(this._catalogues_DependentCataloguesLoadedCompleted);

                    cataloguesPerUser = _catalogues.GetCataloguesForConnection(this.connectionId);

                    if (cataloguesPerUser.catalogueComponent == null || cataloguesPerUser.catalogueVersion == null || cataloguesPerUser.catalogueTargetMilestone == null )
                    {
                        cmbComponent.Enabled = false;

                        cmbVersion.Enabled = false;

                        cmbMilestone.Enabled = false; 

                        _catalogues.CompAndVersionCataloguesLoadedCompleted += new EventHandler(_catalogues_CompAndVersionCataloguesLoadedCompleted);

                        _catalogues.LoadCompAndVersionCatalogues(cataloguesPerUser);
                    }
                    else
                    {

                        PopulateControls();

                        // asyn op 
                        GetBugDetailsAndSetControls(this.bugId, true);
                    }

                    if (_appSettings.GetConnectionById(connectionId).Version.StartsWith("2.18"))
                        GetLastUpdated();
                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "EditBug_Load", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this._formClosed = true;

            if (CancelEvent != null)
            {
                CancelEvent(sender, e);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Submit();
        }

        /// <summary>
        /// This event forces to reaload some data in the user control because of the dependency on Components
        /// </summary>
        private void cmbComponent_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbComponent.Items.Count > 0)
                {

                    cmbComponent.Enabled = false;

                    txtAssignedTo.Enabled = false;

                    cmbVersion.Enabled = false; 

                    lstCC.Enabled = false;

                    btnSave.Enabled = false; 

                    // check if AssignTo and CC are loaded for the selected product
                    bool isLoaded = false;

                    if (cataloguesPerUser.catalogueAssignedTo != null)
                    {
                        foreach (string key in cataloguesPerUser.catalogueAssignedTo.AllKeys)
                        {
                            if (key == cmbComponent.SelectedValue.ToString())
                            {
                                isLoaded = true;
                            }
                        }
                    }

                    if (isLoaded == false)
                    {
                        if (!this.gettingProductDependentCatalogsStarted)
                        {
                            this.gettingProductDependentCatalogsStarted = true;

                            string product = cmbProduct.SelectedValue.ToString();

                            _catalogues.LoadAssignAndCCCollections(this.connectionId, product);
                        }

                    }
                    else
                    {
                        this.ccCompleted = false;
                        _catalogues_DependentCataloguesLoadedCompleted(null, null);
                    }

                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "cmbComponent_SelectedValueChanged", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }

        }

        private void cmbProduct_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbProduct.Items.Count > 0)
                {

                    string product = cmbProduct.SelectedValue.ToString();


                    //_catalogues.DependentCataloguesLoadedCompleted -= new EventHandler(this._catalogues_DependentCataloguesLoadedCompleted);
                    //_catalogues.DependentCataloguesLoadedCompleted += new EventHandler(this._catalogues_DependentCataloguesLoadedCompleted);

                    List<string> lstComponent = Utils.GetCatalogueForDependency(cataloguesPerUser.catalogueComponent, new string[] { product });

                    this.cmbComponent.SelectedValueChanged -= new EventHandler(cmbComponent_SelectedValueChanged);

                    Utils.PopulateComboBox(cmbComponent, lstComponent);

                    cmbComponent.SelectedIndex = -1;

                    this.cmbComponent.SelectedValueChanged += new EventHandler(cmbComponent_SelectedValueChanged);

                    List<string> lstVersion = Utils.GetCatalogueForDependency(cataloguesPerUser.catalogueVersion, new string[] { product });

                    Utils.PopulateComboBox(cmbVersion, lstVersion);

                    if (cmbComponent.Items.Count > 0)
                    {
                        cmbComponent.SelectedIndex = 0;
                    }

                    List<string> lstMilestone = Utils.GetCatalogueForDependency(cataloguesPerUser.catalogueTargetMilestone , new string[] { product });

                    Utils.PopulateComboBox(cmbMilestone, lstMilestone);

                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "cmbProduct_SelectedValueChanged", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }



        }

        private void btnAddAttachment_Click(object sender, EventArgs e)
        {
            try
            {
                FormAttachment frm = new FormAttachment(this.connectionId, this.bugId, true);

                frm.ShowDialog();

                Attachment newAtt = frm.NewAttachment;

                if (newAtt != null)
                {
                    bugToUpdate.Attachments.Add(newAtt);

                    GetBugDetailsAndSetControls(this.bugId, false);

                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "btnAddAttachment_Click", LoggingCategory.Exception);

                MessageBox.Show(this, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void txtComment_KeyDown(object sender, KeyEventArgs e)
        {
            //remove entering of a new line when pressing CTRL+ENTER
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void btnAddCC_Click(object sender, EventArgs e)
        {
            if (txtCC.Text.Length > 0)
            {
                lstCC.Items.Add(txtCC.Text, true);
            }
        }

        private void txtAssignedTo_MouseClick(object sender, MouseEventArgs e)
        {
            TextBox assignee = sender as TextBox;

            Utils.SendMailTo(assignee);
        }

        private void txtReporter_MouseClick(object sender, MouseEventArgs e)
        {
            TextBox reporter = sender as TextBox;

            Utils.SendMailTo(reporter);
        }

        #region Attachment grid events

        private void dgvAttachments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 1)
                {
                    //StartAsync_OpenAttachment((int)lstAttachments.Rows[e.RowIndex].Cells[CST_ATTACHMENT_ID].Value, _connectionID);
                    Utils.OpenAttachmentAsync((Control)sender, (int)lstAttachments.Rows[e.RowIndex].Cells[CST_ATTACHMENT_ID].Value, this.connectionId);
                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "dgvAttachments_CellContentClick", LoggingCategory.Exception);

                MessageBox.Show(this, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvAttachments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    Utils.OpenAttachmentAsync((Control)sender, (int)lstAttachments.Rows[e.RowIndex].Cells[CST_ATTACHMENT_ID].Value, this.connectionId);
                }

            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "dgvAttachments_CellDoubleClick", LoggingCategory.Exception);

                MessageBox.Show(this, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #endregion

        #region Private methods

        BackgroundWorker bkgBugLastUpdated = null;

        private void GetLastUpdated() {
            bkgBugLastUpdated = new BackgroundWorker();

            bkgBugLastUpdated.DoWork += new DoWorkEventHandler(bkgBugLastUpdated_DoWork);

            bkgBugLastUpdated.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgBugLastUpdated_RunWorkerCompleted);

            bkgBugLastUpdated.ProgressChanged += new ProgressChangedEventHandler(bkgBugLastUpdated_ProgressChanged);

            bkgBugLastUpdated.WorkerReportsProgress = true;

            bkgBugLastUpdated.WorkerSupportsCancellation = false;

            // start run async thread.
            ArrayList al = new ArrayList();
            al.Add(this.bugId);

            bkgBugLastUpdated.RunWorkerAsync(al);
        }

        void bkgBugLastUpdated_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
        }

        void bkgBugLastUpdated_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool result = false;

            if (e.Result != null)
            {
                string bugLastUpdated = e.Result.ToString();
                result = int.TryParse(bugLastUpdated.Substring(bugLastUpdated.Length - 2), out bugLastUpdatedSeconds);
            }

            if (!result)
                MyLogger.Write("Parsing seconds failed", "bkgBugLastUpdated_RunWorkerCompleted", LoggingCategory.Exception);
        }

        void bkgBugLastUpdated_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            string bugLastUpdated = null;

            ArrayList al = e.Argument as ArrayList;

            int bugNo = int.Parse(al[0].ToString());

            try
            {

                worker.ReportProgress(0); // start thread.

                worker.ReportProgress(10);

                IBugBSI bugInterface = (IBugBSI)BLControllerFactory.GetRegisteredConcreteFactory(this.connectionId);

                bugLastUpdated = bugInterface.GetBugLastUpdated(bugNo);

                worker.ReportProgress(60);

                e.Result = bugLastUpdated;

                worker.ReportProgress(100);  //completed
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "bkgBugDetails_DoWork", LoggingCategory.Exception);

                worker.ReportProgress(100);  //completed

                throw;

            }

        }

        private void GetBugDetailsAndSetControls(int bugNo, bool useCachedBugIfExist)
        {

            btnSave.Enabled = false;

            BackgroundWorker bkgBugDetails = new BackgroundWorker();

            bkgBugDetails.DoWork += new DoWorkEventHandler(bkgBugDetails_DoWork);

            bkgBugDetails.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgBugDetails_RunWorkerCompleted);

            bkgBugDetails.ProgressChanged += new ProgressChangedEventHandler(bkgBugDetails_ProgressChanged);

            bkgBugDetails.WorkerReportsProgress = true;

            bkgBugDetails.WorkerSupportsCancellation = true;

            // start run async thread.
            ArrayList al = new ArrayList();
            al.Add(bugNo);
            al.Add(useCachedBugIfExist);
            bkgBugDetails.RunWorkerAsync( al );

        }

        private void PopulateControls()
        {
            try
            {
                // populate controls with catalogues.

                Utils.PopulateComboBox(cmbSeverity, cataloguesPerUser.catalogueSeverity);

                Utils.PopulateComboBox(cmbPriority, cataloguesPerUser.cataloguePriority);

                Utils.PopulateComboBox(cmbHardware, cataloguesPerUser.catalogueHardware);

                Utils.PopulateComboBox(cmbOS, cataloguesPerUser.catalogueOS);

                cmbProduct.SelectedValueChanged -= new EventHandler(cmbProduct_SelectedValueChanged);

                Utils.PopulateComboBox(cmbProduct, cataloguesPerUser.catalogueProduct);

                cmbProduct.SelectedIndex = -1;

                cmbProduct.SelectedValueChanged += new EventHandler(cmbProduct_SelectedValueChanged);
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "PopulateControls", LoggingCategory.Exception);

                throw;

            }



        }

        private void DisplayBugDetails( MyZilla.BusinessEntities.Bug bugDetail)
        {
            try
            {
                #region Simple properties

                txtSummary.Text = bugDetail.FullSummary ;

                txtOpenedOn.Text = bugDetail.Created.ToString();

                txtLastModified.Text = bugDetail.Changed.ToString();

                txtBugNo.Text = bugDetail.Id.ToString();

                txtReporter.Text = bugDetail.Reporter;

                txtAssignedTo.Text  = bugDetail.AssignedTo;

                cmbProduct.SelectedValue = bugDetail.Product;

                cmbComponent.SelectedValue = bugDetail.Component;

                cmbVersion.SelectedValue = bugDetail.Version;

                cmbMilestone.SelectedValue = bugDetail.Milestone;  

                cmbPriority.SelectedValue = bugDetail.Priority;

                txtStatus.Text = bugDetail.Status;

                cmbOS.SelectedValue = bugDetail.OS;

                cmbSeverity.SelectedValue = bugDetail.Severity;

                cmbHardware.SelectedValue = bugDetail.Hardware;

                txtDependsOn.Text = bugDetail.DependsOn;

                txtBlock.Text = bugDetail.Blocks;

                txtURL.Text = bugDetail.Url;

                txtStWhiteboard.Text = bugDetail.StatusWhiteboard;

                txtResolution.Text = bugDetail.Resolution;

                _bugKnob = bugDetail.Knob;

                _bugReassignTo = bugDetail.ReassignTo;

                _bugDuplicateBug = bugDetail.DuplicateBug;

                _bugResolution = bugDetail.Resolution;

                #endregion

                #region CC

                if (this.ccCatalogListCompleted && !this.ccCompleted)
                {
                    this.ccCompleted = true;
                    //complete cc list with ccBugList

                    this.CompleteCcList(bugDetail);

                    this.ccCatalogListCompleted = false;
                }

                #endregion

                #region Description & Comments

                //load Comments
                ucComments1.LoadComments(bugDetail.Comment);

                //load description
                if (bugDetail.Comment != null && bugDetail.Comment.Count > 0)
                {
                    

                    // who, bug_when, thetext

                    StringBuilder sb = new StringBuilder();


                    string strComment = bugDetail.Comment[0].Replace("\n", Environment.NewLine  );

                    string[] elements = strComment.Split(',');

                    if (elements.GetLength(0) >= 3)
                    {
                        string Date = elements[1].ToString();

                        sb.Append(strComment.Substring(strComment.IndexOf(Date) + Date.Length + 2));
                    }

                    txtDescriptionValue.Text = sb.ToString();
                }


                #endregion

                if (txtResolution.Text  == "DUPLICATE")
                {

                    // search in comment text the bug to duplicate
                    Regex  duplicateBug = new Regex (@"This bug has been marked as a duplicate of (?<bug_number>[(0-9)]+)", RegexOptions.IgnoreCase  );

                    foreach (string strComment in bugDetail.Comment)
                    {
                        Match match = duplicateBug.Match(strComment.ToString());

                        if (match.Success == true)
                        {
                            string bugNo = match.Groups["bug_number"].ToString();

                            txtResolution.Text = "DUPLICATE of bug " + bugNo;

                            break;
                        }

                    }

                }


                #region Attachments

                this.PopulateAttachmentList();

                #endregion

            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "DisplayBugDetails", LoggingCategory.Exception);

                throw ;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bugDetail"></param>
        private void CompleteCcList(MyZilla.BusinessEntities.Bug bug)
        {
            if (allowAddingPersonsToCcList && !txtCC.Visible)
            {
                lstCC.Top += txtCC.Height + 5;
                lstCC.Height -= (txtCC.Height + 5);
                txtCC.Visible = true;
                btnAddCC.Visible = true;
            }

            if (bug.CC != null && bug.CC.Count > 0)
            {
                if (lstCC.Items.Count > 1)
                {
                    // check the proper items in chklistbox
                    for (int i = 0; i < lstCC.Items.Count; i++)
                    {
                        string item = lstCC.Items[i] as string; // name <email>

                        if (CcListContainsItem(item))
                        {
                            lstCC.SetItemChecked(i, true);
                        }
                    }
                }
                else
                {
                    //cc catalog contains only default value
                    if (lstCC.Items.Count == 1)
                    {
                        lstCC.Items.Clear();
                    }

                    int prevCount = lstCC.Items.Count;

                    for (int i = 0; i < bug.CC.Count; i++)
                    {
                        lstCC.Items.Add(bug.CC[i]);
                        lstCC.SetItemChecked(i + prevCount, true);
                    }
                }

            }
        }

        private bool CcListContainsItem(string value)
        {
            foreach (string item in bugToUpdate.CC)
            {
                if (item == value)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Populate bug object with the values from controls.
        /// </summary>
        /// <returns></returns>
        public MyZilla.BusinessEntities.Bug GetUpdatedBug()
        {

            try
            {
                // resolution, knob... values form ucBugStatus
                bugToUpdate.Resolution = _bugResolution;
                bugToUpdate.Knob = _bugKnob ;
                bugToUpdate.DuplicateBug = _bugDuplicateBug  ;
                bugToUpdate.ReassignTo = _bugReassignTo  ;


                bugToUpdate.Summary = txtSummary.Text;
                bugToUpdate.Changed = DateTime.Parse(txtLastModified.Text);
                bugToUpdate.Id = int.Parse(txtBugNo.Text);
                bugToUpdate.Reporter = txtReporter.Text;

                if (string.IsNullOrEmpty(bugToUpdate.ReassignTo))
                {
                    bugToUpdate.AssignedTo = txtAssignedTo.Text  ;
                }
                else
                {
                    bugToUpdate.AssignedTo = bugToUpdate.ReassignTo ; 
                }

                bugToUpdate.Product = cmbProduct.SelectedValue.ToString();
                bugToUpdate.Component = cmbComponent.SelectedValue.ToString();
                bugToUpdate.Version = cmbVersion.SelectedValue.ToString();
                if (cmbMilestone.SelectedValue != null)
                {
                    bugToUpdate.Milestone = cmbMilestone.SelectedValue.ToString();
                }
                if (cmbPriority.SelectedValue != null)
                {
                    bugToUpdate.Priority = cmbPriority.SelectedValue.ToString();
                }

                bugToUpdate.Status = txtStatus.Text;

                if (cmbOS.SelectedValue != null){
                    bugToUpdate.OS = cmbOS.SelectedValue.ToString();
                }

                bugToUpdate.Severity = cmbSeverity.SelectedValue.ToString();
                bugToUpdate.Hardware = cmbHardware.SelectedValue.ToString();
                bugToUpdate.DependsOn = txtDependsOn.Text;
                bugToUpdate.Blocks = txtBlock.Text;
                bugToUpdate.Url = txtURL.Text;
                bugToUpdate.StatusWhiteboard = txtStWhiteboard.Text;


                #region CC

                List<string> oldCC = new List<string>();
                foreach (string strCC in bugToUpdate.CC)
                {
                    oldCC.Add(strCC); 
                }

                List<string> newCC = new List<string>();

                if (lstCC.CheckedItems.Count > 0)
                {

                    foreach (string item in lstCC.CheckedItems)
                    {
                        // item = name <email>

                        newCC.Add(item);  
                    }
                }

                // build the CC list
                // the items will have the pattern: item,attribute ( newcc, removecc)

                // check if any item has been added
                foreach (string item in newCC )
                {
                    bool isContained = oldCC.Contains(item);

                    if (isContained)
                    {
                        // no code here
                    }
                    else
                    {
                        string ccItem = item + ",newcc";
                        bugToUpdate.AddCC ( ccItem );  
                    }
                }

                // check if any item has been removed.
                foreach (string item in oldCC)
                {
                    bool isContained = newCC.Contains(item);

                    if (isContained)
                    {
                        // no code here
                    }
                    else
                    {
                        string ccItem = item + ",removecc";
                        bugToUpdate.CC.Add(ccItem);
                    }
                }


                #endregion

                #region Comment
//                if (txtComment.Text != string.Empty)
//                {
                    bugToUpdate.Comment.Add (txtComment.Text);
//                }
                #endregion

            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "GetUpdatedBug", LoggingCategory.Exception);

                throw;
            }
            

            return bugToUpdate;

        }

        /// <summary>
        /// Check the conditions before updating a new bug.
        /// </summary>
        private bool CheckConditionsForSaving()
        {
            bool isValid = true;

            if (String.IsNullOrEmpty(txtSummary.Text.Trim()))
            {
                ep.SetError(txtSummary, Messages.NotEmptyField);

                isValid = false;

            }

            return isValid;
        }

        private void ShowConnectionInfo ()
        {
            MyZillaSettingsDataSet _appSettings = MyZillaSettingsDataSet.GetInstance();

            txtConnInfo.Text = _appSettings.GetConnectionInfo(this.connectionId);  
 
        }

        private void PopulateAttachmentList()
        {
            try
            {
                if (bugToUpdate.Attachments.Count > 0)
                {

                    dsAttachments.Tables[0].Rows.Clear();

                    foreach (Attachment att in bugToUpdate.Attachments)
                    {
                        DataRow row = dsAttachments.Tables[0].NewRow();

                        row[0] = att.AttachmentId;

                        row[1] = att.Description;

                        row[2] = att.ContentType;

                        row[3] = att.Created;

                        //row[4] = att.Size; TO DO

                        dsAttachments.Tables[0].Rows.Add(row);
                    }
                    dsAttachments.AcceptChanges();
                    lstAttachments.ClearSelection();
                }
                else
                {
                    //lstAttachments.DataSource = null; 
                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "PopulateAttachmentList", LoggingCategory.Exception);

                throw;

            }


        }

        #endregion

        #region Properties

        public string BugResolution
        {
            get { return _bugResolution; }
            set { _bugResolution = value; }
        }

        
        public string BugKnob
        {
            get { return _bugKnob; }
            set { _bugKnob = value; }
        }

        public string DuplicateBug
        {
            get { return _bugDuplicateBug; }
            set { _bugDuplicateBug = value; }
        }

        public string ReassignTo
        {
            get { return _bugReassignTo; }
            set { _bugReassignTo = value; }
        }

        public int BugNumber
        {
            set
            {
                this.bugId = value;
            }
        }

        public int ConnectionId
        {
            set
            {
                this.connectionId = value;
            }
        }

        public MyZilla.BusinessEntities.Bug BugToUpdate
        {
            get
            {
                return bugToUpdate;
            }
            set
            {
                bugToUpdate = value;
            }
        }

        public bool UpdateSuccessfully
        {
            get
            {
                return _updateSuccessfully;
            }
        }

        public bool IsFormClosed
        {
            get { return _formClosed; }
            set { _formClosed = value; }
        }

        #endregion

        #region Public Methods

        public void Submit()
        {
            try
            {
                bool isValid = this.CheckConditionsForSaving();

                if (isValid == true)
                {
                    bugToUpdate = GetUpdatedBug();

                    if (bugToUpdate == null)
                    {
                        return;
                    }

                    if (bugLastUpdatedSeconds != null)
                        bugToUpdate.Changed = bugToUpdate.Changed.AddSeconds(bugLastUpdatedSeconds);

                    //// check for modifications
                    //if (bugToUpdate.isDirty == false)
                    //{
                    //    Messages.UpdBugOK  

                    //}
                    BackgroundWorker bkgBugUpdate = new BackgroundWorker();

                    bkgBugUpdate.DoWork += new DoWorkEventHandler(bkgBugUpdate_DoWork);

                    bkgBugUpdate.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgBugUpdate_RunWorkerCompleted);

                    bkgBugUpdate.ProgressChanged += new ProgressChangedEventHandler(bkgBugUpdate_ProgressChanged);

                    bkgBugUpdate.WorkerReportsProgress = true;

                    bkgBugUpdate.WorkerSupportsCancellation = true;

                    // start run async thread.
                    bkgBugUpdate.RunWorkerAsync();

                    btnSave.Enabled = false;
                }
                else
                {
                    // no code here
                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "Submit", LoggingCategory.Exception);

                MessageBox.Show("Update bug failed" + Environment.NewLine + ex.Message);
            }
        }

        #endregion

        #region Async - load bug details

        private void bkgBugDetails_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker bkgWork = sender as BackgroundWorker;

            string strMessage = string.Format(Messages.LoadBugDetails, this.bugId); 

            switch (e.ProgressPercentage)
            {
                case 0:

                    _asyncOpManager.BeginOperation(bkgWork, strMessage , e.ProgressPercentage);

                    break;


                case 100:

                    _asyncOpManager.UpdateStatus(bkgWork, Messages.EndOperation, e.ProgressPercentage);

                    break;

                default:

                    _asyncOpManager.UpdateStatus(bkgWork, strMessage  , e.ProgressPercentage);

                    break;

            }
        }

        private void bkgBugDetails_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.ccBugListCompleted = true;
            // check the status of the async operation.

            try
            {
                if (e.Error != null)
                {

                    string errorMessage = Messages.ErrUpdBug + Environment.NewLine + e.Error.Message;

                    MessageBox.Show(Utils.FormContainer, errorMessage, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }

                // status OK
                if (!e.Cancelled && e.Error == null)
                {
                    bugToUpdate = e.Result as MyZilla.BusinessEntities.Bug;

                    if (!String.IsNullOrEmpty(bugToUpdate.ErrorMessage))
                    {
                        this.Enabled = false;

                        if ((bugToUpdate.ErrorMessage == GettingBugErrorMessages.InvalidBugId) || (bugToUpdate.ErrorMessage == GettingBugErrorMessages.BugIdNotFound))
                            bugToUpdate.ErrorMessage = String.Format(Properties.Messages.BugIdDoesNotExists, bugToUpdate.Id);

                        DialogResult dr = MessageBox.Show(bugToUpdate.ErrorMessage);

                        if (dr == DialogResult.OK)
                        {
                            btnCancel_Click(null, null);
                        }
                    }
                    else
                    {
                        DisplayBugDetails(bugToUpdate);

                        bugToUpdate.IsDirty = false;

                        btnSave.Enabled = true;

                        Utils.LoadingFormInBackground();

                        this.Enabled = true;
                    }
                }

#if DEBUG
                (Utils.FormContainer as MainForm).wb.DocumentText = MyZilla.BL.Interfaces.Utils.htmlContents;
#endif

            }

            catch (Exception ex)
            {
                // The thread could continue to execute after the form was closed.
                // In this case, an exception is generated. It is no need to be logged or be shown those type of exceptions.
                if (!_formClosed)
                {

                    MyLogger.Write(ex, "bkgBugDetails_RunWorkerCompleted", LoggingCategory.Exception);

                    MessageBox.Show(this, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bkgBugDetails_DoWork(object sender, DoWorkEventArgs e)
        {
 
            BackgroundWorker worker = sender as BackgroundWorker;
            Bug bugDetail = null;

            ArrayList al = e.Argument as ArrayList;

            int bugNo = int.Parse(al[0].ToString());

            bool useCachedBugIfExits = bool.Parse(al[1].ToString()); 

            try
            {

                worker.ReportProgress(0); // start thread.
                worker.ReportProgress(10);

                // check if bug was previously loaded
                if (bugToUpdate == null || !useCachedBugIfExits )
                {
                    IBugBSI bugInterface = (IBugBSI)BLControllerFactory.GetRegisteredConcreteFactory(this.connectionId);

                    bugDetail = bugInterface.GetBug(bugNo);

                    bugDetail.Id = bugNo;

                }
                else
                {
                    bugDetail = bugToUpdate;
                }

                worker.ReportProgress(60);

                e.Result = bugDetail;

                worker.ReportProgress(100);  //completed
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "bkgBugDetails_DoWork", LoggingCategory.Exception);

                worker.ReportProgress(100);  //completed

                throw;

            }

        }

        #endregion

        #region Async - Save bug

        private void bkgBugUpdate_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker bkgWork = sender as BackgroundWorker;

            string message = string.Format(Messages.UpdateBugInProgress, this.bugId);

            switch (e.ProgressPercentage)
            {
                case 0:

                    _asyncOpManager.BeginOperation(bkgWork, message , e.ProgressPercentage);

                    break;

                case 100:

                    _asyncOpManager.UpdateStatus(bkgWork, Messages.EndOperation, e.ProgressPercentage);

                    break;

                default:

                    _asyncOpManager.UpdateStatus(bkgWork, message , e.ProgressPercentage);

                    break;

            }
        }

        private void bkgBugUpdate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                // check the status of the async operation.

                if (e.Error != null)
                {
                    string errorMessage = Messages.ErrUpdBug + Environment.NewLine + e.Error.Message;

                    MessageBox.Show(Utils.FormContainer, errorMessage, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    
                }

                // status OK
                if (!e.Cancelled && e.Error == null)
                {
                    ArrayList al = e.Result as ArrayList;

                    string errorMessage = al[0].ToString();

                    string strResult = al[1].ToString();

                    btnSave.Enabled = true;

                    if (string.IsNullOrEmpty(errorMessage))
                    {
                        // confirmation message
                        TDSettings.GlobalSettingsRow globalSettings = _appSettings.GetGlobalSettings();

                        if (globalSettings.ConfirmSuccessfullyEditBug)
                        {
                            MessageBox.Show(this, strResult, Messages.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        _updateSuccessfully = true;

                        btnCancel_Click(null, null);


                    }
                    else
                    {
                        MessageBox.Show(this, strResult, Messages.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        GetBugDetailsAndSetControls(bugId, false);
                    }


                }


#if DEBUG
                (Utils.FormContainer as MainForm).wb.DocumentText = MyZilla.BL.Interfaces.Utils.htmlContents;
#endif
            }
            catch (Exception ex)
            {
                // The thread could continue to execute after the form was closed.
                // In this case, an exception is generated. It is no need to be logged or be shown those type of exceptions.
                if (!_formClosed)
                {

                    MyLogger.Write(ex, "bkgBugUpdate_RunWorkerCompleted", LoggingCategory.Exception);

                    MessageBox.Show(this, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }




        }

        private void bkgBugUpdate_DoWork(object sender, DoWorkEventArgs e)
        {

            BackgroundWorker worker = sender as BackgroundWorker;
            try
            {

                worker.ReportProgress(0); // start thread.
                worker.ReportProgress(10);

                IBugBSI bugInterface = (IBugBSI)BLControllerFactory.GetRegisteredConcreteFactory(this.connectionId);

                worker.ReportProgress(60);  //intermediate state

                string errorMessage = string.Empty;

                string result = bugInterface.UpdateBug(bugToUpdate, out errorMessage);

                ArrayList al = new ArrayList();

                al.Add(errorMessage);

                al.Add(result);

                e.Result = al;

                worker.ReportProgress(100);  //completed
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "bkgBugUpdate_DoWork", LoggingCategory.Exception);

                worker.ReportProgress(100);  //completed

                throw;
            }


        }

        #endregion

        #region Load dependent catalogues - completed

        /// <summary>
        /// Event raised when dependent catalogs are loaded (CC, AssignedTo)
        /// </summary>
        private void _catalogues_DependentCataloguesLoadedCompleted(object sender, EventArgs e)
        {
            MyLogger.Write("Start!", "_catalogues_DependentCataloguesLoadedCompleted", LoggingCategory.General);

            this.ccCatalogListCompleted = true;

            try
            {
                //wait until component are filled
                if (cmbComponent.SelectedValue != null)
                {
                    string component = cmbComponent.SelectedValue.ToString();

                    Utils.PopulateContactListForComponent(component, lstCC, cataloguesPerUser.catalogueCC);

                    #region build a list of assignedTo persons for the bug status user control

                    List<string> assignedToList = new List<string>();

                    foreach (string key in cataloguesPerUser.catalogueAssignedTo.Keys) {
                        if (key == BusinessEntities.Dependencies.NoComponentDependency) {
                            foreach (string item in cataloguesPerUser.catalogueAssignedTo.GetValues(key))
                            {
                                assignedToList.Add(item);
                            }
                            allowAddingPersonsToCcList = false;
                            break;
                        }
                    }

                    bool isFirst = true;

                    if (assignedToList.Count == 0) {
                        foreach (string key in cataloguesPerUser.catalogueAssignedTo.Keys)
                        {
                            if (key == component)
                            {
                                allowAddingPersonsToCcList = true;

                                if (cataloguesPerUser.catalogueAssignedTo.GetValues(key) != null)
                                {

                                    foreach (string item in cataloguesPerUser.catalogueAssignedTo.GetValues(key))
                                    {
                                        if (!isFirst)
                                        {
                                            allowAddingPersonsToCcList = false;
                                            assignedToList.Add(item);
                                        }
                                        else
                                            isFirst = false;
                                    }
                                }
                                
                                break;
                            }
                        }
                    }

                    //for (int i = 0; i < cataloguesPerUser.catalogueAssignedTo.Count; i++)
                    //{
                    //    assignedToList.Add(cataloguesPerUser.catalogueAssignedTo[i].ToString());
                    //}

                    if (assignedToList.Count == 0 && bugToUpdate.AssignedTo != null)
                        assignedToList.Add(bugToUpdate.AssignedTo);

                    //for (int i = 0; i < lstCC.Items.Count ; i++)
                    //{
                    //    listCC.Add(lstCC.Items[i].ToString ());
                    //}

                    #endregion

                    if (this.ccBugListCompleted && !this.ccCompleted)
                    {
                        this.ccCompleted = true;
                        //complete cc list with ccBugList
                        this.CompleteCcList(bugToUpdate);
                    }

                    if (ucBugStatus == null)
                    {
                        ucBugStatus = new UCBugStatus(cataloguesPerUser.catalogueResolution, bugToUpdate, assignedToList, this);
                        ucBugStatus.Location = new Point(5, 10);
                        grpSel.Controls.Add(ucBugStatus);
                    }

                    cmbComponent.Enabled = true;

                    txtAssignedTo.Enabled = true;

                    cmbVersion.Enabled = true;

                    lstCC.Enabled = true;

                    btnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "EditBug._catalogues_DependentCataloguesLoadedCompleted", LoggingCategory.Exception);
            }
            finally {
                this.gettingProductDependentCatalogsStarted = false;

                _catalogues.DependentCataloguesLoadedCompleted -= new EventHandler(this._catalogues_DependentCataloguesLoadedCompleted);
            }
        }

        private void _catalogues_CompAndVersionCataloguesLoadedCompleted(object sender, EventArgs e)
        {
            cmbComponent.Enabled = true;

            cmbVersion.Enabled = true;

            cmbMilestone.Enabled = true; 

            PopulateControls();

            // asyn op 
            GetBugDetailsAndSetControls(this.bugId, true);
        }

        #endregion
    }

    public class UpdatedBugEventArgs : EventArgs
    {
        private Bug updatedBug;

        public UpdatedBugEventArgs( Bug updatedBug)
        {
            this.updatedBug = updatedBug;
        }

        public Bug UpdatedBug
        {
            get
            {
                return this.updatedBug ;
            }
        }
    }
}
