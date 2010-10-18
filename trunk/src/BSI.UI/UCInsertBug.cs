using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

using MyZilla.BusinessEntities;
using MyZilla.BL.Interfaces;
using MyZilla.UI.Properties;

using Tremend.Logging;


namespace MyZilla.UI
{
    public partial class UCInsertBug : UserControl
    {
        private const int CST_ATTACHMENT_ID = 0;

        #region Variables

        private Bug addedBug;

        public event EventHandler CancelEvent;

        private ErrorProvider ep = new ErrorProvider();

        private CatalogueManager _catalogues;

        private int connectionId;

        private AsyncOperationManagerList asyncOpManager;

        private Catalogues cataloguesPerUser;

        public bool _formClosed;

        #endregion

        #region Constructor

        public UCInsertBug()
        {
            InitializeComponent();

            addedBug = new Bug ();

        }


        #endregion

        #region Form Events

        private void InsertBug_Load(object sender, EventArgs e)
        {
            try
            {
                if (!this.DesignMode)
                {

                    MyZillaSettingsDataSet _appSettings = MyZillaSettingsDataSet.GetInstance();

                    _catalogues = CatalogueManager.Instance();

                    this.txtReporter.Text = _appSettings.GetConnectionById(this.connectionId).UserName;

                    asyncOpManager = AsyncOperationManagerList.GetInstance();

                    cmbConnections.SelectedValueChanged -=new EventHandler(cmbConnections_SelectedValueChanged);

                    LoadConnectionInfo();

                    cmbConnections.Text = _appSettings.GetConnectionInfo(this.connectionId);  
 
                    cmbConnections.SelectedValueChanged += new EventHandler(cmbConnections_SelectedValueChanged);


                    // verify if all catalogues have been added and populate the controls properly

                    cataloguesPerUser = _catalogues.GetCataloguesForConnection(this.connectionId);

                    if (cataloguesPerUser.catalogueComponent == null || cataloguesPerUser.catalogueVersion == null || cataloguesPerUser.catalogueTargetMilestone == null )
                    {
                        cmbComponent.Enabled = false;

                        cmbVersion.Enabled = false;

                        cmbMilestone.Enabled = false;

                        btnInsertBug.Enabled = false; 

                        _catalogues.CompAndVersionCataloguesLoadedCompleted += new EventHandler(_catalogues_CompAndVersionCataloguesLoadedCompleted);

                        _catalogues.LoadCompAndVersionCatalogues(cataloguesPerUser);
                    }
                    else
                    {

                        _catalogues.DependentCataloguesLoadedCompleted += new EventHandler(this._catalogues_DependentCataloguesLoadedCompletedInsertBug);

                        PopulateControls();

                    }
                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "InsertBug_Load", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }

        
        }

        void cmbComponent_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbComponent.Items.Count > 0)
                {

                    cmbComponent.Enabled = false;

                    cmbAssignedTo.Enabled = false;

                    cmbMilestone.Enabled = false;

                    cmbVersion.Enabled = false; 

                    btnInsertBug.Enabled = false; 

                    lstCC.Enabled = false;


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
                        // load catalogues 
                        string product = cmbProduct.SelectedValue.ToString();

                        _catalogues.LoadAssignAndCCCollections(this.connectionId, product);

                    }
                    else
                    {

                        _catalogues_DependentCataloguesLoadedCompletedInsertBug(null, null);
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

                    #region Componet collection

                    List<string> lstComponent = Utils.GetCatalogueForDependency(cataloguesPerUser.catalogueComponent, new string[] { product });

                    this.cmbComponent.SelectedValueChanged -= new EventHandler(cmbComponent_SelectedValueChanged);

                    Utils.PopulateComboBox(cmbComponent, lstComponent);

                    cmbComponent.SelectedIndex = -1;

                    this.cmbComponent.SelectedValueChanged += new EventHandler(cmbComponent_SelectedValueChanged);


                    // set the last component - is not a Bugzilla functionality
                    //if (string.IsNullOrEmpty(Utils.lastSelectedComp))
                    //{
                    //    cmbComponent.SelectedIndex = 0;
                    //}
                    //else
                    //{
                    //    cmbComponent.SelectedValue = Utils.lastSelectedComp;

                    if (cmbComponent.SelectedIndex == -1 && cmbComponent.Items.Count>0)
                    {
                        cmbComponent.SelectedIndex = 0;
                    }

                    //}

                    #endregion

                    #region Version collection

                    // get version collection

                    List<string> lstVersion = Utils.GetCatalogueForDependency(cataloguesPerUser.catalogueVersion, new string[] { product });

                    Utils.PopulateComboBox(cmbVersion, lstVersion);


                    // set the last version in the control.
                    if (string.IsNullOrEmpty(Utils.lastSelectedVersion))
                    {
                        cmbVersion.SelectedIndex = cmbVersion.Items.Count - 1;
                    }
                    else
                    {
                        cmbVersion.SelectedValue = Utils.lastSelectedVersion;

                        if (cmbVersion.SelectedIndex == -1)
                        {
                            cmbVersion.SelectedIndex = 0; 
                        }
                    }

                    #endregion

                    #region Milestone collection

                    // get milestone collection

                    List<string> lstMilestone = Utils.GetCatalogueForDependency(cataloguesPerUser.catalogueTargetMilestone, new string[] { product });

                    Utils.PopulateComboBox(cmbMilestone, lstMilestone);

                    // set the last version in the control.
                    if (string.IsNullOrEmpty(Utils.lastSelectedMilestone ))
                    {
                        cmbMilestone.SelectedIndex = cmbMilestone.Items.Count - 1;
                    }
                    else
                    {
                        cmbMilestone.Text = Utils.lastSelectedMilestone;

                        if (cmbMilestone.SelectedIndex == -1)
                        {
                            cmbMilestone.SelectedIndex = 0;
                        }
                    }


                    #endregion


                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "cmbProduct_SelectedValueChanged", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _formClosed = true;

            if (CancelEvent != null)
            {
                CancelEvent(sender, e);
            }
        }

        private void btnInsertBug_Click(object sender, EventArgs e)
        {
            Submit();


        }

        private void cmbConnections_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbConnections.SelectedValue != null)
                {
                    int selUser = int.Parse(cmbConnections.SelectedValue.ToString());

                    if (selUser != this.connectionId)
                    {
                        this.connectionId = selUser;

                        this.Enabled = false;

                        cataloguesPerUser = _catalogues.GetCataloguesForConnection(this.connectionId);

                        // load the catalogues from the new selected user
                        // and populate the controls accordingly
                        PopulateControls();

                        this.Enabled = true;

                    }
                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "cmbConnections_SelectedValueChanged", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }

        }

        private void btnAddAttachment_Click(object sender, EventArgs e)
        {

            // not allowed many attachments for Bugzilla 3.0
            // get the version of this connection
            MyZillaSettingsDataSet _appSettings = MyZillaSettingsDataSet.GetInstance();

            string version = _appSettings.GetConnectionById(this.connectionId).Version;

            int versionINT = int.Parse(version.Substring(0, version.IndexOf(".")));

            if (versionINT >= 3 && addedBug.Attachments.Count == 1)
            {
                MessageBox.Show(this, string.Format(Messages.MsgAttNotAllowed, Environment.NewLine), Messages.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            else
            {


                FormAttachment frm = new FormAttachment(this.connectionId, -1, false);

                frm.ShowDialog();

                Attachment newAtt = frm.NewAttachment;

                if (newAtt != null)
                {
                    addedBug.Attachments.Add(newAtt);

                    PopulateAttachmentList();

                }
            }

        }

        private void lstAttachments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 1)
                {
                    Utils.OpenAttachmentAsync((Control)sender, (int)lstAttachments.Rows[e.RowIndex].Cells[CST_ATTACHMENT_ID].Value, this.connectionId);
                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "lstAttachments_CellContentClick", LoggingCategory.Exception);

                MessageBox.Show(this, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void lstAttachments_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 )
                {
                    Utils.OpenAttachmentAsync((Control)sender, (int)lstAttachments.Rows[e.RowIndex].Cells[CST_ATTACHMENT_ID].Value, this.connectionId);
                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "lstAttachments_CellDoubleClick", LoggingCategory.Exception);

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

        #endregion

        #region Public methods

        public int UserId 
        {
            set
            {
                this.connectionId = value;
            }
        }

        public MyZilla.BusinessEntities.Bug GetBugDetails()
        {
            ep.Clear();

            MyZilla.BusinessEntities.Bug result = new Bug();

            try
            {

                result.Reporter = txtReporter.Text;

                result.Product = cmbProduct.SelectedValue.ToString();

                result.Component = cmbComponent.SelectedValue.ToString();

                result.Version = cmbVersion.SelectedValue.ToString();

                if (cmbMilestone.SelectedValue != null)
                {
                    result.Milestone = cmbMilestone.SelectedValue.ToString();
                }

                result.Priority = cmbPriority.SelectedValue.ToString();

                if (cmbAssignedTo.SelectedValue != null)
                    result.AssignedTo = cmbAssignedTo.SelectedValue.ToString().TrimStart('<').TrimEnd('>');
                else
                    result.AssignedTo = cmbAssignedTo.Text;

                result.Status = cmbStatus.SelectedValue.ToString();

                result.OS = cmbOS.SelectedValue.ToString();

                result.Severity = cmbSeverity.SelectedValue.ToString();

                result.Hardware = cmbHardware.SelectedValue.ToString();

                result.DependsOn = txtDependsOn.Text;

                result.Blocks = txtBlock.Text;

                if (lstCC.CheckedItems.Count > 0)
                {
                    foreach (string item in lstCC.CheckedItems)
                    {
                        // item = name <email>

                        result.CC.Add(item);

                    }
                }

                result.Url = txtURL.Text;

                result.Summary = txtSummary.Text;

                result.Comment.Add(txtComment.Text);

                // attachment

                result.Attachments = addedBug.Attachments;  


            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "GetBugDetails", LoggingCategory.Exception);

                throw;

            }

            return result;

        }


        public void Submit()
        {
            try
            {
                bool isValid = this.CheckConditionsForSaving();

                if (isValid == true)
                {

                    addedBug = GetBugDetails();

                    if (addedBug != null)
                    {
                        btnInsertBug.Enabled = false;

                        BackgroundWorker bkgAddBug = new BackgroundWorker();

                        bkgAddBug.DoWork += new DoWorkEventHandler(bkgAddBug_DoWork);

                        bkgAddBug.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgAddBug_RunWorkerCompleted);

                        bkgAddBug.ProgressChanged += new ProgressChangedEventHandler(bkgAddBug_ProgressChanged);

                        bkgAddBug.WorkerReportsProgress = true;

                        bkgAddBug.WorkerSupportsCancellation = true;

                        bkgAddBug.RunWorkerAsync();
                    }
                }
                else
                {
                    // no code here.
                }
            }

            catch (Exception ex)
            {
                MyLogger.Write(ex, "UCInsertBug.Submit", LoggingCategory.Exception);

                MessageBox.Show(this, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Private Methods

        private bool CheckConditionsForSaving()
        {
            bool isValid = true;

            if (String.IsNullOrEmpty(txtSummary.Text.Trim()))
            {
                ep.SetError(txtSummary, Messages.NotEmptyField  );

                isValid = false;

            }

            return isValid;
        }

        /// <summary>
        /// Populate the controls for the current user.
        /// </summary>
        private void PopulateControls( )
        {

            // populate controls with catalogues.
            Utils.PopulateComboBox(cmbStatus, cataloguesPerUser.catalogueStatus);

            cmbStatus.SelectedValue = "NEW";

            Utils.PopulateComboBox(cmbSeverity, cataloguesPerUser.catalogueSeverity);

            cmbSeverity.Text = "normal"; 

            Utils.PopulateComboBox(cmbPriority, cataloguesPerUser.cataloguePriority);

            Utils.PopulateComboBox(cmbHardware, cataloguesPerUser.catalogueHardware);

            if (string.IsNullOrEmpty(Utils.lastSelectedHardware))
            {
                cmbHardware.SelectedIndex = 0;
            }
            else
            {
                cmbHardware.Text = Utils.lastSelectedHardware ;

                if (cmbHardware.SelectedIndex == -1)
                {
                    cmbHardware.SelectedIndex = 0;
                }
            }


            Utils.PopulateComboBox(cmbOS, cataloguesPerUser.catalogueOS);

            if (string.IsNullOrEmpty (Utils.lastSelectedOS   ) )
            {
                cmbOS.SelectedIndex = 0; 
            }
            else
            {
                cmbOS.Text = Utils.lastSelectedOS;

                if (cmbOS.SelectedIndex == -1)
                {
                    cmbOS.SelectedIndex = 0; 
                }
            }

            // populate product combobox

            cmbProduct.SelectedValueChanged -= new EventHandler(cmbProduct_SelectedValueChanged);

            Utils.PopulateComboBox(cmbProduct, cataloguesPerUser.catalogueProduct);

            cmbProduct.SelectedIndex = -1;

            cmbProduct.SelectedValueChanged += new EventHandler(cmbProduct_SelectedValueChanged);

            if (string.IsNullOrEmpty(Utils.lastSelectedProduct))
            {
                cmbProduct.SelectedIndex = 0;
            }
            else
            {
                cmbProduct.Text = Utils.lastSelectedProduct;

                if (cmbProduct.SelectedIndex == -1 && cmbProduct.Items.Count>0)
                {
                    cmbProduct.SelectedIndex = 0;
                }
            }


        }

        private void LoadConnectionInfo()
        {
            // get active connections

            NameValueCollection lst = _catalogues.GetActiveConnections(); 

            ArrayList al = new ArrayList();

            for (int i = 0; i < lst.Count; i++)
            {
                int connID = int.Parse (lst.GetKey(i));

                string connInfo = lst.GetValues(i)[0];

                al.Add(new ItemInCombo(connID, connInfo));

            }

            cmbConnections.DisplayMember = ItemInCombo.CONNECTION_DISPLAY_MEMBER;

            cmbConnections.ValueMember = ItemInCombo.CONNECTION_VALUE_MEMBER;

            cmbConnections.DataSource = al;

            cmbConnections.SelectedValue = this.connectionId; 
 

        }

        private void PopulateAttachmentList()
        {

            if (addedBug.Attachments.Count > 0)
            {

                dsAttachments.Tables[0].Rows.Clear();


                foreach (Attachment att in addedBug.Attachments)
                {
                    DataRow row = dsAttachments.Tables[0].NewRow();

                    row[0] = att.AttachmentId;

                    row[1] = att.FileName ;

                    row[2] = att.ContentType;

                    row[3] = att.Created;

                    //row[4] = att.Size; TO DO

                    dsAttachments.Tables[0].Rows.Add(row);
                }

            }
            else
            {
            }

        }


        #endregion

        #region Async - add bug

        void bkgAddBug_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker bkgWork = sender as BackgroundWorker;

            switch (e.ProgressPercentage)
            {
                case 0:

                    asyncOpManager.BeginOperation(bkgWork, Messages.AddBugInProgress , e.ProgressPercentage);

                    break;


                case 100:

                    asyncOpManager.UpdateStatus(bkgWork, Messages.EndOperation, e.ProgressPercentage);

                    break;

                default:

                    asyncOpManager.UpdateStatus(bkgWork, Messages.AddBugInProgress , e.ProgressPercentage);

                    break;

            }
        }

        void bkgAddBug_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool continueBugEdit = false;
            try
            {
                // check the status of the async operation.

                if (e.Error != null)
                {
                    //display error message from bugzilla
                    string errorMessage = e.Error.Message;

                    DialogResult dr = MessageBox.Show(Utils.FormContainer, errorMessage, Messages.Error, MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);

                    continueBugEdit = (dr == DialogResult.Retry);

#if DEBUG
                    (Utils.FormContainer as MainForm).wb.DocumentText = MyZilla.BL.Interfaces.Utils.htmlContents;
#endif

                }
                else
                {

                    // status OK
                    if (!e.Cancelled && e.Error == null)
                    {

                        string strResult = e.Result.ToString();

#if DEBUG
                        (Utils.FormContainer as MainForm).wb.DocumentText = MyZilla.BL.Interfaces.Utils.htmlContents;
#endif

                        // set the last selected item for some properties

                        Utils.lastSelectedHardware = cmbHardware.Text;

                        Utils.lastSelectedOS = cmbOS.Text;

                        Utils.lastSelectedProduct = cmbProduct.Text;

                        Utils.lastSelectedVersion = cmbVersion.Text;

                        Utils.lastSelectedMilestone = cmbMilestone.Text;

                        // confirmation message
                        MyZillaSettingsDataSet _appSettings = MyZillaSettingsDataSet.GetInstance();

                        TDSettings.GlobalSettingsRow globalSettings = _appSettings.GetGlobalSettings();

                        if (globalSettings.ConfirmSuccessfullyEditBug)
                        {
                            DialogResult dr = MessageBox.Show(this, strResult, Messages.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

                if (!continueBugEdit)
                    btnCancel_Click(this, null);

                btnInsertBug.Enabled = true;
            }
            
            catch (Exception ex)
            {
                // The thread could continue to execute after the form was closed.
                // In this case, an exception is generated. It is no need to be logged or be shown those type of exceptions.
                if (!_formClosed)
                {

                    MyLogger.Write(ex, "bkgAddBug_RunWorkerCompleted", LoggingCategory.Exception);

                    MessageBox.Show(this, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        void bkgAddBug_DoWork(object sender, DoWorkEventArgs e)
        {

            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {
 
                worker.ReportProgress(0); // start thread.
                worker.ReportProgress(10);

                IBugBSI bugInterface = (IBugBSI)BLControllerFactory.GetRegisteredConcreteFactory(this.connectionId);

                worker.ReportProgress(60);  //intermediate state

                string strResult = bugInterface.AddBug(addedBug);

                if (addedBug.Attachments.Count > 0)
                {
                    // get bug ID

                    Regex addBug = new Regex(@"(?<bug_number>[(0-9)]+) was added to the database", RegexOptions.IgnoreCase);

                    Match match = addBug.Match(strResult);

                    int bugNo = 0;

                    if (match.Success == true)
                    {
                        bugNo = int.Parse ( match.Groups["bug_number"].ToString());
                    }


                    string strAtt = string.Empty;

                    string errorMessage = string.Empty;

                    // get version for current connection
                    MyZillaSettingsDataSet _appSettings = MyZillaSettingsDataSet.GetInstance();

                    string version = _appSettings.GetConnectionById(this.connectionId).Version;

                    int versionINT = int.Parse(version.Substring(0, version.IndexOf(".")));

                    switch (versionINT)
                    {
                        case 2:
                            foreach (Attachment att in addedBug.Attachments)
                            {
                                att.BugId = bugNo;

                                bugInterface.PostAttachment(att, out errorMessage);

                                if (!String.IsNullOrEmpty(errorMessage))
                                {
                                    strAtt = string.Format(Messages.ErrPostFile, att.FileName);

                                    strResult += Environment.NewLine + strAtt + " [" + errorMessage + "]";
                                }

                            }

                            break;
                        case 3:
                            break;
                    }
 


                }

                   
                e.Result = strResult;

                worker.ReportProgress(100);  //completed
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "bkgAddBug_DoWork", LoggingCategory.Exception);

                worker.ReportProgress(100);  //completed

                throw;
            }


        }

        #endregion

        #region LoadDependentCatalogues completed

        void _catalogues_DependentCataloguesLoadedCompletedInsertBug(object sender, EventArgs e)
        {
            string component = cmbComponent.SelectedValue.ToString();

            // populate the AssignTo and CC accordingly.

            Utils.FillAssignToCombo(component, cmbAssignedTo, cataloguesPerUser.catalogueAssignedTo);

            Utils.PopulateContactListForComponent(component, lstCC, cataloguesPerUser.catalogueCC);

            if (cataloguesPerUser.catalogueDefaultPriority.Count > 0)
            {
                //cmbPriority.SelectedValue = cataloguesPerUser.catalogueDefaultPriority[cmbProduct.SelectedValue as string];
                cmbPriority.SelectedValue = cataloguesPerUser.catalogueDefaultPriority[0];
            }

            if (lstCC.Items.Count==0 && !btnAddCC.Visible)
            {
                lstCC.Top += txtCC.Height + 5;
                lstCC.Height -= (txtCC.Height + 5);
                txtCC.Visible = true;
                btnAddCC.Visible = true;
            }

            cmbComponent.Enabled = true;

            cmbAssignedTo.Enabled = true;

            cmbMilestone.Enabled = true;

            cmbVersion.Enabled = true; 

            btnInsertBug.Enabled = true; 

            lstCC.Enabled = true;

            if (cmbAssignedTo.Items.Count <= 1) {
                cmbAssignedTo.DropDownStyle = ComboBoxStyle.DropDown;
            }
        }

        void _catalogues_CompAndVersionCataloguesLoadedCompleted(object sender, EventArgs e)
        {
            cmbComponent.Enabled = true;

            cmbVersion.Enabled = true;

            cmbMilestone.Enabled = true ;

            btnInsertBug.Enabled = true; 
         
            _catalogues.DependentCataloguesLoadedCompleted += new EventHandler(this._catalogues_DependentCataloguesLoadedCompletedInsertBug);

            PopulateControls();
        }


        #endregion

    }

    [System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
    internal class ItemInCombo
    {
        private int _userID;

        private string _connection;

        public static string CONNECTION_DISPLAY_MEMBER = "ConnectionInfo";

        public static string CONNECTION_VALUE_MEMBER = "UserID";

        public int UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        public string ConnectionInfo
        {
            get { return _connection; }
            set { _connection = value; }
        }


        public ItemInCombo(int userID, string conn)
        {
            this._userID = userID;

            this._connection = conn;

        }

    }
}
