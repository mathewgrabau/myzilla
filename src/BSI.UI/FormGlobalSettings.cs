using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MyZilla.BusinessEntities;
using MyZilla.UI.Properties; 

namespace MyZilla.UI
{
    public partial class FormGlobalSettings : Form
    {
        #region Variables

        MyZillaSettingsDataSet _appSettings = null;

        TDSettings.GlobalSettingsRow settings;

        #endregion

        #region Constructor

        public FormGlobalSettings()
        {
            InitializeComponent();

            _appSettings = MyZillaSettingsDataSet.GetInstance();

            LoadGlobalSettings();

        }

        #endregion

        #region Form events

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveGlobalSettings();

            MessageBox.Show(Messages.SaveGlSettingsOK);

            this.Close();


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.GetSettingsFromControls();

            if (settings.RowState == DataRowState.Modified)
            {
                DialogResult dr = MessageBox.Show(this, Messages.MsgExistModifications , Messages.Info, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    settings.RejectChanges();
 
                    this.Close();
                }
                else
                {
                    // no code here.
                }

            }
            else
            {
                this.Close();

            }
        }

        private void FormGlobalSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            (Utils.FormContainer as MainForm).GlobalSettingsForm = null;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyDocuments;  

            DialogResult dr = folderBrowserDialog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                txtPath.Text = folderBrowserDialog.SelectedPath; 
            }
        }



        #endregion

        #region Private methods

        private void LoadGlobalSettings()
        {
            settings = _appSettings.GetGlobalSettings();

            chkShowLoadingForm.Checked = settings.ShowLoadingForm;

            chkEditConfirm.Checked = settings.ConfirmSuccessfullyEditBug;

            chkShowBugsCount.Checked = settings.ShowBugsCount;

            txtPath.Text = settings.ReportFilesPath;  
 

        }

        private void GetSettingsFromControls()
        {
            if (settings.ShowLoadingForm != chkShowLoadingForm.Checked)
            {
                settings.ShowLoadingForm = chkShowLoadingForm.Checked;
            }

            if (settings.ConfirmSuccessfullyEditBug != chkEditConfirm.Checked)
            {
                settings.ConfirmSuccessfullyEditBug = chkEditConfirm.Checked;
            }

            if (settings.ShowBugsCount != chkShowBugsCount.Checked)
            {
                settings.ShowBugsCount = chkShowBugsCount.Checked;
            }

            if (settings.ReportFilesPath != txtPath.Text)
            {
                settings.ReportFilesPath = txtPath.Text;
            }

        }

        private void SaveGlobalSettings()
        {

            this.GetSettingsFromControls();

            if (settings.ShowLoadingForm == false)
            {
                // close the form if loaded.
                if ((Utils.FormContainer as MainForm).statusForm != null)
                {
                    (Utils.FormContainer as MainForm).statusForm.Close();
                }
            }

            _appSettings.SaveGlobalSettings(settings);
        }

        #endregion

        private void FormGlobalSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

    }
}