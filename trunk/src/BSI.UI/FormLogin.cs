using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BSI.BL.Interfaces;
using System.IO;
using BSI.UI.Properties;


namespace BSI.UI
{
    public partial class FormLogin : Form
    {

        #region Variable

        private string lastUserLogged = string.Empty;

        #endregion

        #region Constructor

        public FormLogin()
        {
            InitializeComponent();

            lblErrorMessage.Text = string.Empty;

        }

        #endregion

        #region Form Events

        private void btnLogin_Click(object sender, EventArgs e)
        {

            #region Old code
            //string bugzillaUrl = Utils.GetBugzillaUrl();

            //try
            //{
            //    // show the splash window
            //    Splasher.Show(typeof(frmSplash));

            //    // start an asynchrounous call
            //    BackgroundWorker bkgLogin = new BackgroundWorker();

            //    bkgLogin.DoWork += new DoWorkEventHandler(bkgLogin_DoWork);

            //    bkgLogin.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgLogin_RunWorkerCompleted);

            //    bkgLogin.ProgressChanged += new ProgressChangedEventHandler(bkgLogin_ProgressChanged);

            //    bkgLogin.WorkerReportsProgress = true;

            //    bkgLogin.WorkerSupportsCancellation = true;

            //    bkgLogin.RunWorkerAsync();


            //}
            //catch
            //{
            //    lblErrorMessage.Text = Messages.InvalidLogin ;
            //}
            #endregion
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel  ; 
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            #region Old code

            // check for the last user logged.
            // a temporary file is made in startup application directory

            //tempFile = Application.StartupPath + "\\UserFile.txt";


            //if (System.IO.File.Exists(tempFile))
            //{
            //    // read the file. The file contains the last logged user.

            //    StreamReader sr = new StreamReader(tempFile);

            //    lastUserLogged = sr.ReadLine();

            //    txtUserName.Text = lastUserLogged;

            //    string decryptedPassword = EncryptDecrypt.DecryptString(sr.ReadLine(), Utils.encryptionKey);

            //    txtPassword.Text = decryptedPassword;

            //    sr.Close();
            //}


            #endregion
        }


        /// <summary>
        /// Set focus on form.
        /// NO try to set Focus in the Form_Load event. 
        /// This is not possible because no Controls are visible at this time. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormLogin_Activated(object sender, EventArgs e)
        {
            if (lastUserLogged != string.Empty)
            {
                this.btnLogin.Focus();
            }
            else
            {
                this.txtUserName.Focus();
            }

        }



        #endregion

        #region Asyncronous thread - Login

        //void bkgLogin_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{

        //    BackgroundWorker bkgWork = sender as BackgroundWorker;


        //    switch (e.ProgressPercentage)
        //    {
        //        case 0:

        //            Splasher.Status = Messages.EstablishingConnection;  

        //            break;


        //        case 100:

        //            Splasher.Status = string.Format(Messages.ConnectionEstablished, Utils.GetBugzillaUrl()); 

        //            break;

        //        default:

        //            Splasher.Status = Messages.EstablishingConnection;  

        //            break;

        //    }
        //}

        //void bkgLogin_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{

        //    // check the status of the async operation.
        //    if (e.Error != null)
        //    {
        //        MessageBox.Show(this, e.Error.Message, Messages.ErrorAsyncOperation, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //    // status OK
        //    if (!e.Cancelled && e.Error == null)
        //    {

        //        int userID = (int)e.Result;

        //        if (userID > 0)
        //        {
        //            // successfully logged
        //            this.DialogResult = DialogResult.OK;

        //            Utils.userName = txtUserName.Text;

        //            if (chkRememberMe.Checked)
        //            {
        //                if (lastUserLogged != txtUserName.Text)
        //                {
        //                    // write the file

        //                    StreamWriter sw = new StreamWriter(tempFile);

        //                    sw.WriteLine(txtUserName.Text);

        //                    string encryptedPassword = EncryptDecrypt.EncryptString(txtPassword.Text, Utils.encryptionKey);

        //                    sw.WriteLine(encryptedPassword);

        //                    sw.Close();

        //                }

        //            }
        //            else
        //            {
        //                // delete the temporary file if exists.
        //                if (System.IO.File.Exists(tempFile))
        //                {
        //                    System.IO.File.Delete(tempFile);
        //                }
        //            }

        //            //show splash screen

        //            Utils.CachedGlobalData();

        //            Splasher.Close();


        //        }
        //        else
        //        {
        //            lblErrorMessage.Text = Messages.InvalidLogin  ;

        //        }

        //    }

        //}

        //void bkgLogin_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    string bugzillaUrl = Utils.GetBugzillaUrl();

        //    BackgroundWorker worker = sender as BackgroundWorker;

        //    worker.ReportProgress(70);

        //    IUser user = (IUser)BLControllerFactory.GetRegisteredConcreteFactory(bugzillaUrl);

        //    //returns user id if login is succesfull
        //    int userID = user.Login(bugzillaUrl, txtUserName.Text, txtPassword.Text, chkRememberMe.Checked);

        //    e.Result = userID;

        //    worker.ReportProgress(100);


        //}

        #endregion

    }
}
