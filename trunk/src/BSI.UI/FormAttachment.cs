using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using MyZilla.BusinessEntities;
using MyZilla.UI.Properties;
using MyZilla.BL.Interfaces;
using Tremend.Logging;


namespace MyZilla.UI
{
    public struct FileContentType
    {
        public const string TEXT_CONTENT_TYPE = "plain text(text/plain)";
        public const string GIF_CONTENT_TYPE = "GIF image(image/gif)";
        public const string JPG_CONTENT_TYPE = "JPEG image(image/jpg)";
    }

    public partial class FormAttachment : Form
    {
        #region Variables

        private int _bugID;

        private int connectionId;

        public Attachment NewAttachment;

        private ErrorProvider ep = new ErrorProvider();

        private bool _postWhenAdding;


        #endregion

        #region Constructors

        private FormAttachment() { }

        public FormAttachment(int connectionId, int bugId, bool postWhenAdding)
        {
            InitializeComponent();

            this.connectionId = connectionId;

            this._bugID = bugId;

            if (bugId != -1)
            {

                lblInfo.Text += " for bug " + bugId.ToString();
            }
            else
            {
                lblInfo.Text += "."; 
            }

            _postWhenAdding = postWhenAdding;

            this.cmbContentType.Items.AddRange(new object[] {
            FileContentType.TEXT_CONTENT_TYPE,
            FileContentType.GIF_CONTENT_TYPE,
            FileContentType.JPG_CONTENT_TYPE});

        }

        #endregion

        #region Form Events

        private void FormAttachment_Load(object sender, EventArgs e)
        {
            cmbContentType.SelectedIndex = 0;
            openFileDialog1.Filter = "Images (*.jpg,*.gif)|*.jpg;*.gif|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.InitialDirectory = @"C:\";
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            DialogResult dr =  openFileDialog1.ShowDialog();

            if (dr == DialogResult.OK)
            {
                txtFile.Text = openFileDialog1.FileName; 
            }
        }

        private void btnAddAttachment_Click(object sender, EventArgs e)
        {
            try
            {
                bool isValid = CheckConditions();

                if (isValid == true)
                {
                    this.Cursor = Cursors.WaitCursor;

                    string contentType = Utils.GetFileContentType(txtFile.Text);

                    this.NewAttachment = new Attachment(_bugID, txtFile.Text, txtDescription.Text, contentType, txtComment.Text);

                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(txtFile.Text);

                    this.NewAttachment.Size = fileInfo.Length;

                    if (_postWhenAdding == true)
                    {

                        string errorMessage = string.Empty;
                        // post attachment
                        IBugBSI bugProvider = (IBugBSI)BLControllerFactory.GetRegisteredConcreteFactory(this.connectionId);

                        bugProvider.PostAttachment(this.NewAttachment, out errorMessage);

                        if (!String.IsNullOrEmpty(errorMessage))
                        {
                            string strMessage = string.Format(Messages.ErrPostFile, txtFile.Text);

                            MessageBox.Show(this, strMessage + Environment.NewLine + errorMessage, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

                            this.NewAttachment = null;
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        // no code here.
                        this.Close();
                    }

                }
            }

            catch (Exception ex)
            {
                MyLogger.Write(ex, "btnAddAttachment_Click", LoggingCategory.Exception);

                this.NewAttachment = null;

                MessageBox.Show(this, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (txtFile.Text.Trim().Length>0 || txtDescription.Text.Trim().Length>0 || txtComment.Text.Trim ().Length>0)
            {
                DialogResult dr = MessageBox.Show(this, Messages.MsgExistModifications, Messages.Info, MessageBoxButtons.YesNo , MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        private void txtFile_TextChanged(object sender, EventArgs e)
        {
            string extension = Path.GetExtension(txtFile.Text);

            switch (extension.ToLower())
            {
                case ".txt":
                    cmbContentType.SelectedIndex = 0;
                    break;
                case ".gif":
                    cmbContentType.SelectedIndex = 1;
                    break;
                case ".jpg":
                    cmbContentType.SelectedIndex = 2;
                    break;
            }
        }

        #endregion

        #region Private methods

        private bool CheckConditions()
        {
            bool isValid = true;

            if ( txtFile.Text.Trim().Length==0 )
            {
                ep.SetError(txtFile, Messages.NotEmptyField);

                isValid = false;
            }


            if (txtDescription.Text.Trim().Length==0)
            {
                ep.SetError(txtDescription, Messages.NotEmptyField);

                isValid = false;
            }


            return isValid;

        }

        #endregion

        private void FormAttachment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

    }
}

//plain text(text/plain)
//HTML source (text/html)
//XML source(application/xml)
//GIF image(image/gif)
//JPEG image(image/jpg)
//PNG image(image/png)
//binary file(application/octet-stream)
