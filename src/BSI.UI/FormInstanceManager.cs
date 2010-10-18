using System;
using System.Collections.Generic;
using System.Text;
using System.Collections; 
using System.Windows.Forms ;

using MyZilla.UI.Properties;
using MyZilla.BusinessEntities;

using Tremend.Logging;

namespace MyZilla.UI
{
    /// <summary>
    /// Manage form instances.
    /// Key = userID, bugID
    /// </summary>
    public class FormInstanceDictionary : SortedList < string , FormEditBug  >
    {
        private static FormInstanceDictionary _instance;

        #region Private/Static Constructor

        public static FormInstanceDictionary GetInstance()
        {
            if (_instance == null)
            {
                _instance = new FormInstanceDictionary();
            }

            return _instance;
        }

        private FormInstanceDictionary()
        {

        }

        #endregion

        #region Public methods

        private FormEditBug GetEditBugFormInstance( int connectionId,  int bugId, Bug cachedBug, out bool existInstance)
        {
            FormEditBug result = null;

            string key = connectionId.ToString() + "," + bugId.ToString();

            existInstance = false;
  
            if (this.ContainsKey(key))
            {

                int keyIndex = this.IndexOfKey(key);

                result = this.Values[keyIndex];

                existInstance = true;

            }
            else
            {
                MyLogger.Write(String.Format("Generate new edit window for {0}-{1}", bugId, connectionId), "GetEditBugFormInstance", LoggingCategory.General);


                FormEditBug newInstance = FormInstanceDictionary.LoadForm(bugId, connectionId, cachedBug);

                if (newInstance != null)
                {
                    this.Add(key, newInstance);

                    result = newInstance;

                    existInstance = false;

                }
                else
                {
                    result = null;

                    existInstance = false;
                }
            }

            return result;
        }

        public void RemoveEditBugFormInstance ( int userId, int bugId)
        {
            string key = userId.ToString() + "," + bugId.ToString();

            int keyIndex = this.IndexOfKey(key);

            this.RemoveAt(keyIndex);
        }

        public FormEditBug OpenEditBugFormInstance(int userId, int bugId, Bug cachedBug)
        {
            bool existInstance;

            FormEditBug frmEditBug = this.GetEditBugFormInstance(userId, bugId, cachedBug, out existInstance);

            if (existInstance == true)
            {
                frmEditBug.Visible = true;

                frmEditBug.Activate();

                Utils.ActivateLoadingForm(); 

            }
            else
            {
                if (frmEditBug != null)
                {
                    frmEditBug.Show();

                    Utils.ActivateLoadingForm(); 

                }

            }

            return frmEditBug;


        }

        #endregion

        #region Private methods

        private static FormEditBug LoadForm(int bugId, int connectionId, Bug cachedBug)
        {
            // load form
            FormEditBug frmEditBug = null;

            try
            {
                frmEditBug = new FormEditBug(connectionId, bugId, cachedBug);

                if (frmEditBug.DialogResult == DialogResult.OK)
                {
                    //frmEditBug.Show();
                }

                if (frmEditBug.DialogResult == DialogResult.Cancel)
                {
                    MyZillaSettingsDataSet _appSettings = MyZillaSettingsDataSet.GetInstance ();

                    string connInfo = _appSettings.GetConnectionInfo(connectionId); 

                    string strMessage = string.Format(Messages.NoActiveConnection , connInfo  );

                    MessageBox.Show(Utils.FormContainer , strMessage, Messages.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    frmEditBug = null;
                }
            }

            catch (Exception ex)
            {
                MyLogger.Write(ex, "LoadForm", LoggingCategory.Exception);

                MessageBox.Show( Utils.FormContainer  , ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (frmEditBug != null)
                {
                    frmEditBug = null;

                    frmEditBug.Dispose();

                }

            }

            return frmEditBug;
        }

        #endregion
    }
}
