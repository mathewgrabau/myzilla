using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using MyZilla.BusinessEntities;

namespace MyZilla.UI
{
    public partial class FormNewVersion : Form
    {
        private string publishedMyZillaVersion;

        MyZillaSettingsDataSet _appSettings;

        MyZillaSettingsDataSet.GlobalSettingsRow settings;

        private FormNewVersion()
        {
            InitializeComponent();
        }

        public FormNewVersion(string publishedVersion)
        {
            InitializeComponent();
            this.publishedMyZillaVersion = publishedVersion;

            this._appSettings = MyZillaSettingsDataSet.CreateInstance(Utils.UserAppDataPath);

            this.settings = _appSettings.GetGlobalSettings();

            chkDoNotRemindLater.Checked = !settings.CheckForUpdate;
        }

        private void lnkNewVersion_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenDownloadPageAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            

            this.settings.CheckForUpdate = !chkDoNotRemindLater.Checked;

            if (chkDoNotRemindLater.Checked)
            {
                this.settings.LastMyZillaVersion = this.publishedMyZillaVersion;
            }
            else {
                this.settings.LastMyZillaVersion = "1.0.0";
            }

            this._appSettings.SaveGlobalSettings(this.settings);

            this.Close();
        }

        public delegate void AsyncOpenMyZillaDownloadPage();

        private void OpenDownloadPageAsync()
        {
            this.UseWaitCursor = true;
            
            // Create the delegate.
            AsyncOpenMyZillaDownloadPage dlgt = new AsyncOpenMyZillaDownloadPage(Utils.OpenDownloadPage);

            // Initiate the asychronous call.  Include an AsyncCallback
            // delegate representing the callback method, and the data
            // needed to call EndInvoke.
            dlgt.BeginInvoke(new AsyncCallback(CallbackOpenMyZillaDownloadPage), dlgt);
        
        }

        // Callback method must have the same signature as the
        // AsyncCallback delegate.
        private void CallbackOpenMyZillaDownloadPage(IAsyncResult ar)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(delegate()
                {
                    this.UseWaitCursor = false;
                }));
            }
        }
    }
}