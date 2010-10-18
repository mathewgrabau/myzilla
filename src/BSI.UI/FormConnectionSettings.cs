using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using MyZilla.UI.Properties;
using MyZilla.BusinessEntities;
using MyZilla.BL.Interfaces ;
using Tremend.Logging;

namespace MyZilla.UI
{
    public partial class FormConnectionSettings : Form
    {
        #region Variables

        private MyZillaSettingsDataSet _connSettings;

        private ConfigItems.TDSQueriesTree queryTree;

        private bool _addConnection;

        private ErrorProvider ep = new ErrorProvider();

        private AsyncOperationManagerList _asyncOpManager;

        private string _urlPrefix = "http://";

        private string _urlSecurePrefix = "https://";

        private int _connectionID = -1;

        #endregion

        #region Constructors

        public FormConnectionSettings()
        {
            InitializeData();

        }

        public FormConnectionSettings(int connectionId)
        {
            InitializeData();

            _connectionID = connectionId;

        }

        public FormConnectionSettings(bool addNewConnection)
        {
            InitializeData();

            _addConnection = addNewConnection;

        }


        #endregion

        #region Form events

        private void FormConnectionSettings_Load(object sender, EventArgs e)
        {
            try
            {
                // populate combo box with the types of the connection.
                PopulateConnectionTypeCB();

                LoadDefinedConnections(0);

                if (lstConnections.Items.Count == 0)
                {
                    AddConnection();
                }
                else
                {
                    // check if the form was open for editing a connection
                    if (_connectionID != -1)
                    {
                        foreach (ConnectionItem cItem in lstConnections.Items)
                        {
                            if (cItem.ConnectionID == _connectionID)
                            {
                                lstConnections.SelectedItem = cItem;

                                break;
                            }
                        }

                        _connectionID = -1;
                    }

                    if (_addConnection == true)
                    {
                        AddConnection();

                        //_addConnection = false;
                    }
                }
           }

            catch (Exception ex)
            {
                MyLogger.Write(ex, "FormConnectionSettings_Load", LoggingCategory.Exception);

                MessageBox.Show(this, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

                throw;

            }

        }

        private void btnAddConn_Click(object sender, EventArgs e)
        {
            try
            {
                AddConnection();
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "btnAddConn_Click", LoggingCategory.Exception);

                MessageBox.Show(this, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnDelConn_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteConnection();

                if (lstConnections.Items.Count == 0)
                {
                    txtConnectionName.Text = string.Empty;

                    txtPassword.Text = string.Empty;

                    txtUrl.Text = string.Empty;

                    txtUserName.Text = string.Empty;

                    chkActiveUser.Checked = false;

                    //chkRemember.Checked = false;

                    AddConnection();
                }
                else {

                    this._addConnection = false;
                
                }

            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "btnDelConn_Click", LoggingCategory.Exception);

                MessageBox.Show(this, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void lstConnections_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstConnections.SelectedIndex >= 0)
                {
                    ConnectionItem ci = lstConnections.SelectedItem as ConnectionItem;

                    lblTestResult.Visible = false;  

                    if (ci != null)
                    {
                        TDSettings.ConnectionRow conn = _connSettings.GetConnectionById ( ci.ConnectionID);

                        if (conn != null)
                        {
                            txtUrl.Text = conn.URL;

                            txtConnectionName.Text = conn.ConnectionName;

                            txtUserName.Text = conn.UserName;

                            txtPassword.Text = conn.Password;

                            chkActiveUser.Checked = conn.ActiveUser;

                            //chkRemember.Checked = conn.RememberPassword;
                        }
                        else
                        {
                            if (this._addConnection == true)
                            {
                                txtConnectionName.Text = Messages.NewConnection;

                                txtUrl.Text = string.Empty;

                                txtUserName.Text = string.Empty;

                                txtPassword.Text = string.Empty;

                                chkActiveUser.Checked = false;

                                //chkRemember.Checked = false;
                            }
                        }
                    }
                        
                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "lstConnections_SelectedIndexChanged", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }

        }

        private void chkActiveUser_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActiveUser.Checked == true)
            {
                //chkRemember.Checked = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                ep.Clear();

                // save connections

                bool isValid = CheckConditionsForSave();

                if (isValid == true)
                {
                    if (this._addConnection == true)
                    {
                        // get connection information
                        int connectionID = _connSettings.AddConnection(txtConnectionName.Text.Trim(),this.FullConnectionURL (  txtUrl.Text), cmbType.Text, txtUserName.Text.Trim (), txtPassword.Text.Trim (), true , chkActiveUser.Checked  );

                        // get version of the connection (async thread)
                        this.GetVersionForConnection(connectionID, txtUrl.Text.Trim());  

                        lstConnections.Enabled = true;

                        LoadDefinedConnections( lstConnections.SelectedIndex  );

                        this._addConnection = false;

                        btnAddConn.Enabled = true; 
                    }
                    else
                    {
                        ConnectionItem ci = lstConnections.SelectedItem as ConnectionItem ;
 
                        if (ci!= null )
                        {
                            string oldURL = _connSettings.GetConnectionById(ci.ConnectionID).URL;

                            _connSettings.EditConnection (ci.ConnectionID 
                                                                , txtConnectionName.Text.Trim()
                                                                , this.FullConnectionURL(txtUrl.Text)
                                                                , cmbType.Text
                                                                , txtUserName.Text.Trim()
                                                                , txtPassword.Text.Trim()
                                                                , true
                                                                , chkActiveUser.Checked 
                                                            );

                            if (oldURL != txtUrl.Text.Trim())
                            {
                                // get version of the connection (async thread)
                                this.GetVersionForConnection(ci.ConnectionID, txtUrl.Text.Trim());  
                           }
                        }
                    }

                    MessageBox.Show(this, Messages.SaveGlSettingsOK, Messages.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadDefinedConnections(lstConnections.SelectedIndex );

                }// is valid = true
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "btnSave_Click", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            bool isValid = CheckConditionsWhenTestConnection();

            if (isValid)
            {

                this.EnableButtons(false);

                BackgroundWorker bkgTestCredentials = new BackgroundWorker();

                bkgTestCredentials.DoWork += new DoWorkEventHandler(bkgTestCredentials_DoWork);

                bkgTestCredentials.ProgressChanged += new ProgressChangedEventHandler(bkgTestCredentials_ProgressChanged);

                bkgTestCredentials.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgTestCredentials_RunWorkerCompleted);

                bkgTestCredentials.WorkerReportsProgress = true;

                bkgTestCredentials.WorkerSupportsCancellation = true;

                ArrayList al = new ArrayList();

                al.Add(this.FullConnectionURL(txtUrl.Text));

                al.Add(txtUserName.Text.Trim());

                al.Add(txtPassword.Text.Trim());

                bkgTestCredentials.RunWorkerAsync(al);
            }
            else
            {
                // no code here.
            }

        }

        private void chkRemember_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActiveUser.Checked == true)
            {
                //chkRemember.Checked = true;
            }
        }

        private void txtUrl_Leave(object sender, EventArgs e)
        { 
            if (txtUrl.Text.Trim().Length > 0)
                txtUrl.Text = this.FullConnectionURL(txtUrl.Text.Trim());
        }

        private void FormConnectionSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConnectionItem selConnection = lstConnections.SelectedItem as ConnectionItem;

            if (selConnection != null)
            {
                bool isModified = this.CheckForModifications(selConnection.ConnectionID);

                if (isModified)
                {
                    DialogResult dr = MessageBox.Show(this, Messages.MsgExistModifications, Messages.Info, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dr == DialogResult.Yes)
                    {
                        e.Cancel = false;
                    }
                    else
                    {
                        e.Cancel = true;
                    }

                }
                else
                {
                    e.Cancel = false;
                }

            }


        }

        private void FormConnectionSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        #endregion

        #region Private methods

        private void InitializeData()
        {
            InitializeComponent();

            _connSettings = MyZillaSettingsDataSet.GetInstance();

            this.queryTree = ConfigItems.TDSQueriesTree.Instance();

            _asyncOpManager = AsyncOperationManagerList.GetInstance();
        }

        private void PopulateConnectionTypeCB()
        {

            // load connection type
            cmbType.DataSource = _connSettings.ConnectionType;

            cmbType.DisplayMember = "Name";

            cmbType.ValueMember = "Name";

        }

        private void AddConnection()
        {
            this._addConnection = true;

            lstConnections.Enabled = false;

            ConnectionItem ci = new ConnectionItem(-1, Messages.NewConnection );

            lstConnections.Items.Add(ci);

            lstConnections.SelectedIndex = lstConnections.Items.Count - 1;

            btnAddConn.Enabled = false; 



        }

        private void LoadDefinedConnections( int selectedIndex)
        {
            try
            {
                lstConnections.Items.Clear();

                foreach (MyZillaSettingsDataSet.ConnectionRow cConnection in _connSettings.Connection.Rows)
                {
                    ConnectionItem ci = new ConnectionItem(cConnection.ConnectionId , cConnection.ConnectionName  );

                    lstConnections.Items.Add(ci);

                }

                lstConnections.DisplayMember =  "ConnectionInfo";

                lstConnections.ValueMember = "ConnectionID";

                if (lstConnections.Items.Count >= selectedIndex+ 1 )
                {
                    lstConnections.SelectedIndex = selectedIndex ;
                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "LoadDefinedConnections", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }


        }

        private void DeleteConnection()
        {
            if (lstConnections.SelectedIndex >= 0)
            {
                ConnectionItem connectionDetails = lstConnections.SelectedItem as ConnectionItem;

                if (connectionDetails.ConnectionID  == -1)
                {
                    // user was adding a new connection

                    this._addConnection = false;
                }
                else
                {
                    this._connSettings.DeleteConnection(connectionDetails.ConnectionID);
                    this.queryTree.RemoveUserItems(connectionDetails.ConnectionID);
                }


                lstConnections.Items.RemoveAt(lstConnections.SelectedIndex);

                if (lstConnections.Items.Count > 0)
                {
                    lstConnections.SelectedIndex = 0;
                }

            }

            lstConnections.Enabled = true;

            btnAddConn.Enabled = true; 
        }

        private bool CheckConditionsWhenTestConnection()
        {
            bool isValid = true;

            ep.Clear();

            if (txtConnectionName.Text.Trim().Length==0)
            {
                ep.SetError(txtConnectionName, Messages.NotEmptyField);

                isValid = false;

            }
            else
            {
                ep.SetError(txtConnectionName, string.Empty);
            }


            if (txtUrl.Text.Trim().Length == 0)
            {

                ep.SetError(txtUrl, Messages.NotEmptyField);

                isValid = false;

            }
            else
            {
                ep.SetError(txtUrl, string.Empty);

            }


            if (txtUserName.Text.Trim().Length == 0)
            {
                ep.SetError(txtUserName, Messages.NotEmptyField);

                isValid = false;
            }


            if (txtPassword.Text.Trim().Length == 0)
            {
                ep.SetError(txtPassword, Messages.NotEmptyField);

                isValid = false;
            }

            return isValid;
        }

        private bool CheckConditionsForSave()
        {
            bool isValid = true;

            ep.Clear();

            if (txtConnectionName.Text.Trim().Length == 0)
            {
                ep.SetError(txtConnectionName, Messages.NotEmptyField);

                isValid = false;

            }
            //else if ((((ConnectionItem)lstConnections.SelectedItem).ConnectionInfo != txtConnectionName.Text) && _connSettings.Connection.Select(String.Format("ConnectionName='{0}'", txtConnectionName.Text)).GetLength(0)>0)
            else if (ConnectionNameAlreadyInUse(txtConnectionName.Text))
            {
                ep.SetError(txtConnectionName, Messages.ConnectionNameAlreadyExists);

                isValid = false;
            }
            else
            {
                ep.SetError(txtConnectionName, string.Empty);
            }


            if (txtUrl.Text.Trim().Length == 0)
            {

                ep.SetError(txtUrl, Messages.NotEmptyField);

                isValid = false;

            }
            else
            {
                ep.SetError(txtUrl, string.Empty);

            }


            if (txtUserName.Text.Trim().Length == 0)
            {
                ep.SetError(txtUserName, Messages.NotEmptyField);

                isValid = false;
            }


            if (txtPassword.Text.Trim().Length == 0)
            {
                ep.SetError(txtPassword , Messages.NotEmptyField);

                isValid = false;
            }

            if (chkRemember.Checked == true && chkActiveUser.Checked == true)
            {
                if (txtUserName.Text.Trim().Length == 0)
                {
                    ep.SetError(txtUserName, Messages.NotEmptyField);

                    isValid = false;
                }

                if (txtPassword.Text.Trim().Length == 0)
                {
                    ep.SetError(txtPassword, Messages.NotEmptyField);

                    isValid = false;
                }
            }

            if (isValid)
            {
                // check if url has protocol in front:
                int pos = txtUrl.Text.Trim().IndexOf("//");

                if (pos == -1)
                {
                    txtUrl.Text = "http://" + txtUrl.Text.Trim();   
                }

            }


            return isValid;

        }

        private bool ConnectionNameAlreadyInUse(string ConnName) {
            bool result = false;

            for (int i = 0; i < lstConnections.Items.Count; i++) {
                if (i != lstConnections.SelectedIndex) {
                    ConnectionItem item = (ConnectionItem)lstConnections.Items[i];
                    if (item != null && item.ConnectionInfo == ConnName)
                    {
                        result = true; 
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Disables all buttons when enable is 'false' and set their status back
        /// when enable is send with the value 'true'
        /// </summary>
        /// <param name="enable"></param>
        private void EnableButtons(bool enable)
        {
            if (!enable)
            {
                #region remember prev status of the buttons

                btnAddConn.Tag = btnAddConn.Enabled;

                btnDelConn.Tag = btnDelConn.Enabled;

                btnTest.Tag = btnTest.Enabled;

                btnSave.Tag = btnSave.Enabled;

                btnClose.Tag = btnClose.Enabled;

                #endregion

                btnAddConn.Enabled = enable;

                btnDelConn.Enabled = enable;

                btnTest.Enabled = enable;

                btnSave.Enabled = enable;

                btnClose.Enabled = enable;
            }
            else {
                #region restore buttons state

                btnAddConn.Enabled = btnAddConn.Tag==null?enable:(bool)btnAddConn.Tag;

                btnDelConn.Enabled = btnDelConn.Tag == null ? enable : (bool)btnDelConn.Tag;

                btnTest.Enabled = btnTest.Tag == null ? enable : (bool)btnTest.Tag;

                btnSave.Enabled = btnSave.Tag == null ? enable : (bool)btnSave.Tag;

                btnClose.Enabled = btnClose.Tag == null ? enable : (bool)btnClose.Tag;

                #endregion
            }

            
        }

        private string FullConnectionURL(string Url)
        {
            string result;

            result = Url.Trim();
            if (!Url.Trim().StartsWith(_urlPrefix) && (!Url.Trim().StartsWith(_urlSecurePrefix)))
                result = _urlPrefix + result;

            if (result.Length > 0) {
                if (result[result.Length - 1] != '/')
                    result += '/';
            }

            return result;
        }

        private bool CheckForModifications(int connectionID)
        {
            bool isModified = false;

            // get the connection info and compare with the values in the controls
            TDSettings.ConnectionRow connection = _connSettings.GetConnectionById(connectionID);

            if (connection != null)
            {

                isModified = (connection.ConnectionName != txtConnectionName.Text.Trim());

                isModified |= (connection.URL != txtUrl.Text.Trim());

                isModified |= (connection.UserName != txtUserName.Text.Trim());

                isModified |= (connection.Password != txtPassword.Text.Trim());

                isModified |= (connection.Type != cmbType.Text);

                isModified |= (connection.RememberPassword != chkRemember.Checked);

                isModified |= (connection.ActiveUser != chkActiveUser.Checked);

            }

            return isModified;

        } 


        #endregion

        #region Async - test credentials

        private void bkgTestCredentials_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // check the status of the async operation.
            if (e.Error != null)
            {
                string errorMessage = string.Format ( Messages.ErrTestConnection , txtUrl.Text ) + Environment.NewLine + e.Error.Message;

                MessageBox.Show(Utils.FormContainer, errorMessage, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                lblTestResult.Visible = true;

                lblTestResult.Text =  Messages.Test_Failed;

            }

            // status OK
            if (!e.Cancelled && e.Error == null)
            {
                bool result = e.Result.ToString() == "True" ? true : false;

                lblTestResult.Visible = true;

                lblTestResult.Text = result == true ? Messages.Test_OK : Messages.Test_Failed;

            }

            EnableButtons(true); 

        }

        private void bkgTestCredentials_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker bkgWork = sender as BackgroundWorker;


            switch (e.ProgressPercentage)
            {
                case 0:

                    //statusStrip1.Visible = true;
                    pbStatus.Visible = true;

                    lblStatusInfo.Visible = true;
                    _asyncOpManager.BeginOperation(bkgWork, Messages.TestCredentials, e.ProgressPercentage);

                    break;


                case 100:

                    //statusStrip1.Visible = false; 
                    pbStatus.Visible = false;
                    lblStatusInfo.Visible = false;
                    _asyncOpManager.UpdateStatus(bkgWork, Messages.EndOperation, e.ProgressPercentage);

                    break;

                default:

                    _asyncOpManager.UpdateStatus(bkgWork, Messages.TestCredentials, e.ProgressPercentage);

                    lblStatusInfo.Text = Messages.TestCredentials;

                    pbStatus.ProgressBar.Value = e.ProgressPercentage;  

                    break;

            }
        }

        private void bkgTestCredentials_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {

                worker.ReportProgress(0);
                worker.ReportProgress(10);

                IUser user = (IUser)BLControllerFactory.GetRegisteredConcreteFactory(Utils.ConnectionId);

                worker.ReportProgress(50);

                ArrayList al = e.Argument as ArrayList;

                bool result = user.TestLogOnToBugzilla(al[0].ToString(), al[1].ToString(), al[2].ToString());

                worker.ReportProgress(100);

                e.Result = result;
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "bkgTestCredentials_DoWork", LoggingCategory.Exception);

                worker.ReportProgress(100);

                throw;

            }

        }

        #endregion

        #region Get version

        private void GetVersionForConnection(int connectionID, string url)
        {

            try
            {
                BackgroundWorker bkgVersion = new BackgroundWorker();
                bkgVersion.DoWork += new DoWorkEventHandler(bkgVersion_DoWork);
                bkgVersion.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgVersion_RunWorkerCompleted);
                ArrayList al = new ArrayList();
                al.Add(connectionID);
                al.Add(url);
                bkgVersion.RunWorkerAsync(al);
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "GetVersionForConnection", LoggingCategory.Exception);
            }

        }

        void bkgVersion_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // status OK
            if (!e.Cancelled && e.Error == null)
            {
                ArrayList al = e.Result as ArrayList;

                string version = al[0].ToString();

                int connectionID = int.Parse(al[1].ToString());

                _connSettings.SetVersionForConnection(connectionID, version); 

            }
        }

        void bkgVersion_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ArrayList al = e.Argument as ArrayList;

                int connectionID = int.Parse(al[0].ToString());

                string url = al[1].ToString();

                IUtilities utilities = (IUtilities)BLControllerFactory.GetRegisteredConcreteFactory(connectionID);

                string version =  utilities.GetBugzillaVersion(url);

                ArrayList alResult = new ArrayList();

                alResult.Add(version);

                alResult.Add(connectionID);

                e.Result = alResult;

            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "bkgVersion_DoWork", LoggingCategory.Exception);

                throw;
            }

        }

        #endregion

    }

    [System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
    internal class ConnectionItem
    {
        private int _connID;

        private string _connectionInfo;

        public int ConnectionID
        {
            get { return _connID; }
        }

        // ConnectionName - UserName
        public string ConnectionInfo
        {
            get { return _connectionInfo; }
        }

        public ConnectionItem(int connID, string connName)
        {
            _connID  = connID ;

            _connectionInfo = connName;

        }

    }

}