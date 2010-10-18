using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using MyZilla.BL.Interfaces;
using MyZilla.BusinessEntities;
using MyZilla.UI.Properties;
using Tremend.Logging;

namespace MyZilla.UI
{
    public partial class FormAddBug : Form
    {
        private int connectionId;

        #region Constructors 

        public FormAddBug(int connectionId)
        {

            try
            {
                InitializeComponent();

                this.connectionId = connectionId;

                ucInsertBug.UserId = this.connectionId;

                ucInsertBug.CancelEvent += new EventHandler(ucInsertBug_CancelEvent);

                bool isValid = this.CheckConditionsForLoading();

                this.DialogResult = isValid ? DialogResult.OK : DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "FormAddBug", LoggingCategory.Exception);

                throw;

            }



        }

        #endregion

        #region Form Events

        void ucInsertBug_CancelEvent(object sender, EventArgs e)
        {
            try
            {
                this.Close();

                (Utils.FormContainer as MainForm).AddBugForm = null;
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "ucInsertBug_CancelEvent", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }
        }

        private void FormAddBug_FormClosing(object sender, FormClosingEventArgs e)
        {

            ucInsertBug._formClosed = true;
 
            (Utils.FormContainer as MainForm).AddBugForm = null;
        }


        #endregion

        #region Private methods

        private bool CheckConditionsForLoading()
        {

            bool isValid = true;

            CatalogueManager _catalogues = CatalogueManager.Instance();

            Catalogues cataloguesPerUser = _catalogues.GetCataloguesForConnection(this.connectionId);

            if (cataloguesPerUser == null)
            {
                return false;
            }

            return isValid;

        }

        #endregion

        private void FormAddBug_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
            else if (e.Control && e.KeyCode == Keys.Enter) {
                e.SuppressKeyPress = true;
                ucInsertBug.Submit();
            }
        }

    }
}