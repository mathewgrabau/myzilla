using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using MyZilla.BusinessEntities;
using MyZilla.BL.Interfaces;
using MyZilla.UI.Properties;
using Tremend.Logging;

namespace MyZilla.UI
{
    public partial class FormEditBug : Form
    {
        #region Variables

        private int _bugNo;

        private int _connectionId;

        #endregion

        #region Defined events

        public delegate void UpdatedBugEventHandler(object sender, UpdatedBugEventArgs e);

        public event UpdatedBugEventHandler UpdatedBug;

        #endregion

        #region Constructors

        public FormEditBug(int connectionId, int bugNo, Bug cachedBug)
        {
            InitializeComponent();

            _connectionId = connectionId; 

            bool isValid = CheckConditionsForLoading();

            this.DialogResult = isValid ? DialogResult.OK : DialogResult.Cancel;

            if (this.DialogResult == DialogResult.OK)
            {

                _bugNo = bugNo;

                ucEditBug.BugNumber = bugNo;

                ucEditBug.ConnectionId = _connectionId;

                if (cachedBug != null)
                {
                    ucEditBug.BugToUpdate = cachedBug; 
                }

                ucEditBug.CancelEvent += new EventHandler(ucEditBug_CancelEvent);

                this.KeyPreview = true;

                this.Text = string.Format(Messages.EditBugTitle, _bugNo);

            }

        }

        #endregion

        #region Form events

        private void FormEditBug_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Escape)
                {
                    Bug bugToUpdate = ucEditBug.GetUpdatedBug();

                    if (bugToUpdate != null && bugToUpdate.IsDirty == false)
                    {
                        this.Close();

                    }
                    else
                    {
                        DialogResult dr = MessageBox.Show(this, Messages.MessModifiedBug, Messages.Info, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dr == DialogResult.Yes)
                        {
                            ucEditBug.Submit();
                        }
                        else
                        {
                            this.Close();
                        }
                    }

                }
                else if (e.Control && e.KeyCode == Keys.Enter){
                    
                    Bug bugToUpdate = ucEditBug.GetUpdatedBug();

                    if (bugToUpdate != null)
                    {
                        ucEditBug.Submit();
                    }
                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "FormEditBug_KeyDown", LoggingCategory.Exception);

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }


        }

        private void FormEditBug_FormClosing(object sender, FormClosingEventArgs e)
        {

            ucEditBug.IsFormClosed = true;
 
            FormInstanceDictionary formManager = FormInstanceDictionary.GetInstance();

            formManager.RemoveEditBugFormInstance(_connectionId, _bugNo);

            if (ucEditBug.UpdateSuccessfully == true)
            {
                if (UpdatedBug != null)
                {
                   //IBugBSI bugInterface = (IBugBSI)BLControllerFactory.GetRegisteredConcreteFactory(_userID);

                   //ucEditBug.BugToUpdate =  bugInterface.GetBug(ucEditBug.BugToUpdate.Id);   

                    UpdatedBug ( this, new UpdatedBugEventArgs ( ucEditBug.BugToUpdate )); 
                }
            }

        }

        private void ucEditBug_CancelEvent(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Private methods

        private bool CheckConditionsForLoading()
        {

            bool isValid = true;

            CatalogueManager _catalogues = CatalogueManager.Instance();

            Catalogues cataloguesPerUser = _catalogues.GetCataloguesForConnection(_connectionId);

            if (cataloguesPerUser == null)
            {
                return false;
            }

            return isValid;

        }

        #endregion

    }
}
