using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Specialized;

using BSI.BusinessEntities;
using BSI.BL.Interfaces;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using BSI.UI.Properties;


namespace BSI.UI
{
    public partial class UCAdvancedSearch : UserControl
    {
        private TabControl _tabCtrl;
        private TreeView _tree;
        private int _userID;
        private NameValueCollection _searchParam;
        private ConfigItems.NodeDescription _nodeDescription;

        private CatalogueManager _catalogues = CatalogueManager.Instance( );

        private AsyncOperationManager asyncOpManager = AsyncOperationManager.GetInstance(); 

        public UCAdvancedSearch()
        {
            InitializeComponent();
        }

        public UCAdvancedSearch(TabControl TabCtrl, TreeView Tree, ConfigItems.NodeDescription NodeDescription, NameValueCollection SearchParams, int UserID)
        {
            _tabCtrl = TabCtrl;
            _tree = Tree;
            _nodeDescription = NodeDescription;
            _searchParam = SearchParams;
            _userID = UserID;
            InitializeComponent();
        }

        private void ucAdvancedSearch_Load(object sender, EventArgs e)
        {
            Utils.PopulateListBox(listboxProduct, _catalogues.GetCataloguesByUserID(_userID).catalogueProduct);
            listboxProduct.ClearSelected();           

            // get all the catalogues dependent of the product catalogue

            Utils.PopulateListBox(listboxStatus, _catalogues.GetCataloguesByUserID(_userID).catalogueStatus);
            listboxStatus.ClearSelected();
            listboxStatus.SelectedIndex = 1;
            listboxStatus.SelectedIndex = 2;
            listboxStatus.SelectedIndex = 3;
            listboxStatus.SelectedIndex = 4;

            Utils.PopulateListBox(listboxResolution, _catalogues.GetCataloguesByUserID(_userID).catalogueResolution);
            listboxResolution.ClearSelected();

            Utils.PopulateListBox(listboxSeverity, _catalogues.GetCataloguesByUserID(_userID).catalogueSeverity);
            listboxSeverity.ClearSelected();

            Utils.PopulateListBox(listboxPriority, _catalogues.GetCataloguesByUserID(_userID).cataloguePriority);
            listboxPriority.ClearSelected();

            Utils.PopulateListBox(listboxHardware, _catalogues.GetCataloguesByUserID(_userID).catalogueHardware);
            listboxHardware.ClearSelected();

            Utils.PopulateListBox(listboxOS, _catalogues.GetCataloguesByUserID(_userID).catalogueOS);
            listboxOS.ClearSelected();

            Utils.PopulateComboBox(cmbSummary, _catalogues.GetCataloguesByUserID(_userID).catalogueStringOperators);

            Utils.PopulateComboBox(cmbComment, _catalogues.GetCataloguesByUserID(_userID).catalogueStringOperators);
            cmbComment.SelectedIndex = 2;

            Utils.PopulateComboBox(cmbURL, _catalogues.GetCataloguesByUserID(_userID).catalogueStringOperators);

            Utils.PopulateComboBox(cmbWhiteboard, _catalogues.GetCataloguesByUserID(_userID).catalogueStringOperators);

        }

        #region Async - search

        void bkgSearch_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            BackgroundWorker bkgWork = sender as BackgroundWorker;

            switch (e.ProgressPercentage)
            {
                case 0:

                    asyncOpManager.BeginOperation(bkgWork, Properties.Messages.SearchInProgress, e.ProgressPercentage);

                    break;


                case 100:

                    asyncOpManager.UpdateStatus(bkgWork, Properties.Messages.EndOperation, e.ProgressPercentage);

                    break;

                default:

                    asyncOpManager.UpdateStatus(bkgWork, Properties.Messages.SearchInProgress, e.ProgressPercentage);

                    break;

            }

        }

        void bkgSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // check the status of the async operation.

            if (e.Error != null)
            {
                Logger.Write( e.Error , LoggingCategory.Exception);

                string errMessage = Messages.ErrSearching  + Environment.NewLine + e.Error.Message ;

                MessageBox.Show( Utils.FormContainer, errMessage, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);      
            }

            // status OK
            if (!e.Cancelled && e.Error == null)
            {
                List<BSI.BusinessEntities.Bug> bugsFound = e.Result as List<BSI.BusinessEntities.Bug>;

                CreateNewResultsTab(bugsFound);
            }

        }

        void bkgSearch_DoWork(object sender, DoWorkEventArgs e)
        {


            BackgroundWorker worker = sender as BackgroundWorker;

            worker.ReportProgress(0); // start thread.

            IBugBSI bugProvider = (IBugBSI)BLControllerFactory.GetRegisteredConcreteFactory( _userID);

            worker.ReportProgress(60);  //intermediate state

            List<BSI.BusinessEntities.Bug> bugsFound = bugProvider.SearchBugs(_searchParam);

            worker.ReportProgress(100);  //completed

            e.Result = bugsFound;


        }

        #endregion


        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                //Utils.ActivateLoadingForm(); 

                _searchParam = Utils.GetSearchCriteriaFromUI(this);

                BackgroundWorker bkgSearch = new BackgroundWorker();


                bkgSearch.DoWork += new DoWorkEventHandler(bkgSearch_DoWork);

                bkgSearch.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgSearch_RunWorkerCompleted);

                bkgSearch.ProgressChanged += new ProgressChangedEventHandler(bkgSearch_ProgressChanged);

                bkgSearch.WorkerReportsProgress = true;

                bkgSearch.WorkerSupportsCancellation = true;


                bkgSearch.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// Populate the dependent listboxes (Components, Version, Targets)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listboxProduct_SelectedValueChanged(object sender, EventArgs e)
        {
            List<string> lstComponent = new List<string>();

            List<string> lstVersion = new List<string>();

            List<string> products = new List<string>();

            if ((listboxProduct.Items.Count > 0) && (listboxProduct.SelectedValue != null))
            {
                foreach (Utils.CatalogueItem objProduct in listboxProduct.SelectedItems)
                {
                    products.Add(objProduct.CatValue);
                }

                bool noComponentItemSelected = (listboxComponent.SelectedItems.Count==0);
                bool noVersionItemSelected = (listboxVersion.SelectedItems.Count == 0);


                lstComponent = Utils.GetCatalogueForDependency(_catalogues.GetCataloguesByUserID(_userID).catalogueComponent, products.ToArray ());

                lstVersion = Utils.GetCatalogueForDependency(_catalogues.GetCataloguesByUserID(_userID).catalogueVersion, products.ToArray ());

                Utils.PopulateListBox(listboxComponent, lstComponent);

                Utils.PopulateListBox(listboxVersion, lstVersion);

                if (noComponentItemSelected)
                    listboxComponent.ClearSelected();

                if (noVersionItemSelected)
                    listboxVersion.ClearSelected();

            }
            else
            {
                // no product selected
                listboxComponent.DataSource = null;

                listboxVersion.DataSource = null;
            }
       }

        private void btnSaveQuery_Click(object sender, EventArgs e)
        {
            string queryName = String.Empty;
            string queryDescription = String.Empty;
            ConfigItems.TDSQueriesTree.QueriesRow query;
            ConfigItems.QueriesTree queriesTree = ConfigItems.QueriesTree.Instance();
            int folderID = -1;

            _searchParam = Utils.GetSearchCriteriaFromUI(this);

            if (this.Tag!=null)
            {
                ConfigItems.NodeDescription nodeDescription = (ConfigItems.NodeDescription)this.Tag;
                query = (ConfigItems.TDSQueriesTree.QueriesRow)nodeDescription.NodeData;
                queriesTree.AddParameterValuesForQuery(query, _searchParam);
            }
            else
            {
                #region show dialog for choosing a query name
                FormQueryName frm = new FormQueryName(_userID, _tree);

                DialogResult result = frm.ShowDialog(this);

                //check respose of the user from the dialog form
                switch (result)
                {
                    case DialogResult.OK:
                        queryName = frm.QueryName;
                        folderID = frm.FolderID;
                        queryDescription = frm.QueryDescription;

                        query = queriesTree.AddNewQuery(queryName, queryDescription, folderID, (int)QueryTypes.UserCustom, _searchParam);
                        queriesTree.AddQueryToTree(_tree, query);

                        _tabCtrl.SelectedTab.Name = query.ID.ToString();
                        _tabCtrl.SelectedTab.Text = query.Name;
                        this.Tag = new ConfigItems.NodeDescription(NodeType.Query, query);
                        break;
                }
                #endregion
                
            }
            
        }


        //private void CreateNewResultsTab(List<BSI.BusinessEntities.Bug> bg)
        //{

        //    string tabKeyName = "Results" + _tabCtrl.SelectedIndex.ToString();
        //    string tabHeaderText = "Results for " + _tabCtrl.SelectedTab.Text;

        //    TabPage tabResults = _tabCtrl.TabPages[tabKeyName];
        //    UCResults bugsResults;
        //    if (tabResults == null)
        //    {
        //        _tabCtrl.TabPages.Insert(_tabCtrl.SelectedIndex + 1, tabKeyName, tabHeaderText);
        //        tabResults = _tabCtrl.TabPages[tabKeyName];

        //        bugsResults = new UCResults(_userID, bg, _searchParam, _tree);
        //        bugsResults.Name = "ucResults";
        //        bugsResults.Dock = DockStyle.Fill;
        //        tabResults.Controls.Add(bugsResults);
        //        tabResults.BackColor = Color.White;
        //    }
        //    else
        //    {
        //        bugsResults = (UCResults)(tabResults.Controls.Find("ucResults", true)[0]);
        //        bugsResults.LoadBugs(bg);
        //    }

        //    _tabCtrl.SelectedTab = tabResults;

        //    SetUISearchCriteriaFromUI();
        //}

        public void SetUISearchCriteriaFromUI()
        {
            Hashtable htControls = new Hashtable();

            #region create a hash of controls and tag(parameter name)
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.Tag != null)
                {
                    htControls.Add(ctrl.Tag.ToString(), ctrl.Name);
                    //clear any kinf of selection on the 
                    if (ctrl.GetType() == typeof(ListBox))
                    {
                        ((ListBox)ctrl).ClearSelected();
                    }
                }
            }
            #endregion

            for (int i = 0; i < _searchParam.Count; i++)
            {
                if (htControls[_searchParam.GetKey(i)] != null)
                {
                    Control[] ctrl = this.Controls.Find(htControls[_searchParam.GetKey(i)].ToString(), true);

                    if (ctrl[0].GetType() == typeof(TextBox))
                    {
                        ((TextBox)ctrl[0]).Text = _searchParam.GetValues(i)[0];
                    }
                    else if (ctrl[0].GetType() == typeof(ComboBox))
                    {
                        ((ComboBox)ctrl[0]).SelectedValue = _searchParam.GetValues(i)[0];
                    }
                    else if (ctrl[0].GetType() == typeof(ListBox))
                    {
                        ListBox lb = (ListBox)ctrl[0];

                        for (int j = 0; j < _searchParam.GetValues(i).GetLength(0); j++)
                        {
                            int pos = lb.FindStringExact(_searchParam.GetValues(i)[j]);
                            //if string was found in the list box
                            if (pos >= 0)
                                lb.SelectedItem = lb.Items[pos];
                        }

                    }
                }
                else
                {
                    TextBox txt;
                    if (!this.Controls.ContainsKey(_searchParam.GetKey(i)))
                    {
                        txt = new TextBox();
                        txt.Visible = false;
                        txt.Name = _searchParam.GetKey(i);
                        txt.Tag = _searchParam.GetKey(i);
                        
                        this.Controls.Add(txt);
                    }
                    else {
                        txt = (TextBox)this.Controls.Find(_searchParam.GetKey(i), false)[0];
                    }
                    txt.Text = _searchParam.GetValues(i)[0];
                }
            }
        }

    }

}
