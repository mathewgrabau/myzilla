using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms ;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.InteropServices;

using MyZilla.BL.Interfaces;
using MyZilla.UI.Properties;  
using MyZilla.BusinessEntities;
using MyZilla.UI.ConfigItems ;

using Tremend.Logging;  

namespace MyZilla.UI
{

    internal class Utils
    {
        #region Variables

        private const string SHOW_BUG_PAGE = "show_bug.cgi?id={0}";
        private const string ERROR_TOKEN = "associated";

        // the identifier for the current connection/user
        public static int ConnectionId = -1;
        
        //define the container for all forms in the application
        public static Form FormContainer;

        private static AsyncOperationManagerList _asyncOpManager;

        private static CatalogueManager _catManager;

        private static MyZillaSettingsDataSet _appSettings;

        public static string lastSelectedProduct;

        public static string lastSelectedVersion;

        public static string lastSelectedOS;

        public static string lastSelectedHardware;

        public static string lastSelectedMilestone;


        #endregion

        #region Public methods

        public delegate Control AsyncDelegate(Control ctrl, int AttachmentID, int ConnectionID);

        public static bool ValidateBugStateTransition(Bug bug, Bug bugValuesToBeChanged)
        {
            bool result = false;

            string bugStatus = bug.Status.ToUpper();

            string knob = bugValuesToBeChanged.Knob.ToLower();

            result = ((bugStatus == BugStatus.UNCONFIRMED && (knob == BugKnob.CONFIRM
                                                                || knob == BugKnob.ACCEPT
                                                                || knob == BugKnob.RESOLVE
                                                                || knob == BugKnob.DUPLICATE
                                                                || knob == BugKnob.REASSIGN))

                    || (bugStatus == BugStatus.NEW && (knob == BugKnob.ACCEPT
                                                        || knob == BugKnob.RESOLVE
                                                        || knob == BugKnob.DUPLICATE
                                                        || knob == BugKnob.REASSIGN))

                    || (bugStatus == BugStatus.ASSIGNED && (knob == BugKnob.RESOLVE
                                                            || knob == BugKnob.DUPLICATE
                                                            || knob == BugKnob.REASSIGN))

                    || (bugStatus == BugStatus.RESOLVED && (knob == BugKnob.CHANGE_RESOLUTION
                                                            || knob == BugKnob.REOPEN
                                                            || knob == BugKnob.DUPLICATE
                                                            || knob == BugKnob.VERIFY
                                                            || knob == BugKnob.REASSIGN  //?
                                                            || knob == BugKnob.CLOSE))

                    || (bugStatus == BugStatus.REOPENED && (knob == BugKnob.RESOLVE
                                                        || knob == BugKnob.DUPLICATE
                                                        || knob == BugKnob.REASSIGN))

                    || (bugStatus == BugStatus.VERIFIED && (knob == BugKnob.CHANGE_RESOLUTION
                                                            || knob == BugKnob.REOPEN
                                                            || knob == BugKnob.DUPLICATE
                                                            || knob == BugKnob.REASSIGN //?
                                                            || knob == BugKnob.CLOSE))

                    || (bugStatus == BugStatus.CLOSED && (knob == BugKnob.CHANGE_RESOLUTION
                                                            || knob == BugKnob.REOPEN
                                                            || knob == BugKnob.DUPLICATE))
                    || (knob == BugKnob.NONE && (!String.IsNullOrEmpty(bugValuesToBeChanged.Severity) || !String.IsNullOrEmpty(bugValuesToBeChanged.Priority)))
                    );
            //|| (true));

            return result;
        }


        public static void OpenAttachmentAsync(Control Ctrl, int AttachmentID, int ConnectionID)
        {
            Ctrl.UseWaitCursor = true;

            // Create the delegate.
            AsyncDelegate dlgt = new AsyncDelegate(Utils.OpenAttachment);

            // Initiate the asychronous call.  Include an AsyncCallback
            // delegate representing the callback method, and the data
            // needed to call EndInvoke.
            IAsyncResult ar = dlgt.BeginInvoke(Ctrl, AttachmentID, ConnectionID, new AsyncCallback(CallbackMethod), dlgt);
        }

        [Serializable]
        public struct ShellExecuteInfo
        {
            public int Size;
            public uint Mask;
            public IntPtr hwnd;
            public string Verb;
            public string File;
            public string Parameters;
            public string Directory;
            public uint Show;
            public IntPtr InstApp;
            public IntPtr IDList;
            public string Class;
            public IntPtr hkeyClass;
            public uint HotKey;
            public IntPtr Icon;
            public IntPtr Monitor;
        }

        [DllImport("shell32.dll", SetLastError = true)]
        extern public static bool
               ShellExecuteEx(ref ShellExecuteInfo lpExecInfo);

        public const uint SW_NORMAL = 1;

        static void OpenAs(string file)
        {
            ShellExecuteInfo sei = new ShellExecuteInfo();
            sei.Size = Marshal.SizeOf(sei);
            sei.Verb = "openas";
            sei.File = file;
            sei.Show = SW_NORMAL;
            if (!ShellExecuteEx(ref sei))
                throw new System.ComponentModel.Win32Exception();
        }

        private static Control OpenAttachment(Control Ctrl, int AttachmentID, int ConnectionID)
        {

            // post attachment
            IBugBSI bugProvider = (IBugBSI)BLControllerFactory.GetRegisteredConcreteFactory(ConnectionID);

            string errorMessage = string.Empty;

            string strFullPath = bugProvider.GetAttachment(AttachmentID, out errorMessage);

            try
            {
                System.Diagnostics.Process.Start(strFullPath);
            }
            catch (Exception ex)
            {
                //if no application is associated with the file type....
                if (ex.Message.Contains(ERROR_TOKEN))
                    OpenAs(strFullPath);
                else
                {
                    MyLogger.Write(ex, "OpenAttachment", LoggingCategory.Exception);
                }
            }

            return Ctrl;
        }

        public static void OpenNewAdvancedSearchTab(MainForm Main)
        {
            if (Main.ValidNROfTabs(String.Empty))
            {
                // check for valid users
                string validConnection = Utils.CheckIfValidConnection();

                if (!string.IsNullOrEmpty(validConnection))
                {
                    MessageBox.Show(validConnection, Messages.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                Main.RemoveWelcomeScreen();

                Main.IndexAdvancedAndSearchTab--;

                Utils.CreateNewResultsTab(null, Utils.GetDefaultSearchParametersConfiguration(), Main.tBSI, Main.tvQueries, null, true, true, "query " + Main.IndexAdvancedAndSearchTab.ToString());
            }
        }

        static void CallbackMethod(IAsyncResult ar)
        {
            // Retrieve the delegate.
            AsyncDelegate dlgt = (AsyncDelegate)ar.AsyncState;

            // Call EndInvoke to retrieve the results.
            Control result = dlgt.EndInvoke(ar);

            if (result.InvokeRequired)
            {
                result.BeginInvoke(new MethodInvoker(delegate()
                {
                    result.UseWaitCursor = false;
                    result.Cursor = Cursors.Default;
                }));
            }
            else
            {
                result.Cursor = Cursors.Default;
                result.UseWaitCursor = false;
            }
        }

        public static void OpenNewConnectionWindow()
        {
            try
            {
                bool addNewConnection = true;

                FormConnectionSettings frm = new FormConnectionSettings(addNewConnection);

                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Properties.Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Opens the window for inserting a new bug    
        /// </summary>
        public static void OpenNewBugWindow(FormAddBug frmAddBug)
        {
            // check for valid users
            string validConnection = Utils.CheckIfValidConnection();

            if (!string.IsNullOrEmpty(validConnection))
            {
                MessageBox.Show(validConnection, Messages.Info, MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            try
            {
                // get the current user
                int connectionId = Utils.ConnectionId;

                if (connectionId == -1)
                {
                    MessageBox.Show(Messages.NoCurrentUser);

                }
                else
                {
                    if (frmAddBug == null)
                    {

                        // load form

                        try
                        {
                            frmAddBug = new FormAddBug(connectionId);

                            if (frmAddBug.DialogResult == DialogResult.OK)
                            {
                                frmAddBug.Show();
                            }

                            if (frmAddBug.DialogResult == DialogResult.Cancel)
                            {
                                string strMessage = string.Format(Messages.NoLoadedCatalogues, Environment.NewLine);

                                MessageBox.Show(strMessage, Messages.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                frmAddBug = null;
                            }
                        }

                        catch (Exception exception)
                        {
                            MyLogger.Write(exception, "OpenNewBugWindow", LoggingCategory.Exception);

                            MessageBox.Show(exception.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

                            if (frmAddBug != null)
                            {
                                frmAddBug = null;

                                frmAddBug.Dispose();

                            }

                        }
                    }
                    else
                    {
                        frmAddBug.Visible = true;

                        frmAddBug.Activate();
                    }
                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "OpenNewBugWindow", LoggingCategory.Exception);

                MessageBox.Show(ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Returns an url stripped of http authentication info
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static string GetURLStripped(string Url)
        {
            string url = Url;

            string startSeparator = "://";
            string middleSeparator = ":";
            string endSeparator = "@";

            int indexStartSeparator = Url.IndexOf(startSeparator);
            int indexMiddleSeparator = 0;

            if (indexStartSeparator > 0)
                indexMiddleSeparator = Url.IndexOf(middleSeparator, indexStartSeparator + startSeparator.Length);

            if (indexMiddleSeparator >= 0)
            {

                int indexEndSeparator = Url.IndexOf(endSeparator, indexMiddleSeparator);

                if (indexEndSeparator > 0)
                {
                    string httpPart = Url.Substring(indexStartSeparator + startSeparator.Length, indexEndSeparator - indexStartSeparator - startSeparator.Length + 1);

                    url = Url.Replace(httpPart, String.Empty);
                }
            }

            return url;
        }

        public static void LoadTreeWithConnectionNodes(TreeView TVQueries)
        {
            TVQueries.Nodes.Clear();

            MyZillaSettingsDataSet appSettings = MyZillaSettingsDataSet.GetInstance();

            foreach (DataRow dr in appSettings.Connection.Rows)
            {
                TDSettings.ConnectionRow connRow = (TDSettings.ConnectionRow)dr;

                TreeNode userNode = new TreeNode(appSettings.GetConnectionInfo(connRow.ConnectionId));
                userNode.Name = "User " + connRow.ConnectionId;
                userNode.Tag = new NodeDescription(NodeType.Connection, connRow);
                userNode.ImageIndex = 2;
                userNode.SelectedImageIndex = 2;
                userNode.ToolTipText = null;

                TVQueries.Nodes.Add(userNode);

            }

        }

        /// <summary>
        /// Populate the specified list box.
        /// </summary>
        /// <param name="lbControl">The control to be populated.</param>
        /// <param name="lst">
        /// The list of items. 
        /// An item in the list has the following format: 'value, text'
        /// </param>
        public static void PopulateListBox(ListBox lbControl, List<string> lst)
        {
            lbControl.DataSource = null;

            ArrayList al = new ArrayList();

            foreach (string item in lst)
            {
                string[] itemValueText = item.Split(',');

                CatalogueItem cItem = null;
                if (itemValueText.Length == 2)
                {

                    cItem = new CatalogueItem(itemValueText[0], itemValueText[1]);
                }
                else
                {
                    cItem = new CatalogueItem(itemValueText[0], itemValueText[0]);
                }

                al.Add(cItem);
            }

            lbControl.DisplayMember = "CatText";
            lbControl.ValueMember = "CatValue";
            lbControl.DataSource = al;

        }

        public static void PopulateListBox(ListBox lbControl, NameValueCollection lst)
        {
            lbControl.DataSource = null;

            ArrayList al = new ArrayList();

            for (int i = 0; i < lst.Count; i++)
            {
                string[] element = lst.GetKey(i).Split(',');

                if (element.Length == 2)
                {
                    // items in collection are stored as <element, parent>,and the element is (value, text)

                    CatalogueItem cItem = new CatalogueItem(element[0], element[1]);

                    al.Add(cItem);

                }
                else
                {
                    // items in collection are stored as <element, parent>
                    // I assume that the dependent collections are value = key  // mmi
                    CatalogueItem cItem = new CatalogueItem(lst.GetKey(i), lst.GetKey(i));

                    al.Add(cItem);


                }

            }

            lbControl.DisplayMember = "CatText";
            lbControl.ValueMember = "CatValue";
            lbControl.DataSource = al;



        }

        /// <summary>
        /// Populate the specified combo box.
        /// </summary>
        /// <param name="cbControl">The control to be populated.</param>
        /// <param name="lst">
        /// The list of items. 
        /// An item in the list has the following format: 'value, text'
        /// </param>
        public static void PopulateComboBox(ComboBox cbControl, List<string> lst)
        {
            cbControl.DataSource = null;

            if (lst != null && lst.Count > 0)
            {
                ArrayList al = new ArrayList();

                foreach (string item in lst)
                {
                    string[] itemValueText = item.Split(',');

                    CatalogueItem cItem = null;

                    if (itemValueText.Length == 2)
                    {
                        cItem = new CatalogueItem(itemValueText[0], itemValueText[1]);
                    }
                    else
                    {
                        cItem = new CatalogueItem(itemValueText[0], itemValueText[0]);

                    }

                    al.Add(cItem);
                }

                cbControl.DisplayMember = "CatText";
                cbControl.ValueMember = "CatValue";
                cbControl.DataSource = al;
            }

        }

        /// <summary>
        /// Populate the specified combo box.
        /// </summary>
        /// <param name="cbControl">The control to be populated.</param>
        /// <param name="lst">
        /// The list of items. 
        /// An item in the list has the following format: 'value, text'
        /// </param>
        public static void PopulateComboBox(ComboBox cbControl, NameValueCollection lst)
        {
            cbControl.DataSource = null;

            if (lst != null && lst.Count > 0)
            {

                ArrayList al = new ArrayList();

                for (int i = 0; i < lst.Count; i++)
                {
                    string[] element = lst.GetKey(i).Split(',');

                    if (element.Length == 2)
                    {
                        // items in collection are stored as <element, parent>,and the element is (value, text)

                        CatalogueItem cItem = new CatalogueItem(element[0], element[1]);

                        al.Add(cItem);

                    }
                    else
                    {
                        string[] values = lst.GetValues(i);
                        foreach (string strValue in values)
                        {
                            CatalogueItem cItem = new CatalogueItem(strValue, strValue);
                            al.Add(cItem);

                        }
                    }

                }

                cbControl.DisplayMember = "CatText";
                cbControl.ValueMember = "CatValue";
                cbControl.DataSource = al;
            }
        }


        /// <summary>
        /// Get the collection of values for a specified dependency.
        /// Ex: a Component catalogue depends on the selected product.
        /// </summary>
        /// <param name="catalogue"></param>
        /// <param name="parent"></param>
        /// <returns>A collection of value, text pairs.</returns>
        public static List<string> GetCatalogueForDependency(NameValueCollection catalogue, string[] parent)
        {
            List<string> result = new List<string>();

            for (int i = 0; i < catalogue.Keys.Count; i++)
            {

                if (catalogue.Keys[i].IndexOf(parent[0]) >= 0)
                {
                    foreach (string item in catalogue.GetValues(i))
                    {
                        result.Add(item);
                    }
                }
            }

            return result;
        }

        public static void AddCookie(string CookieName, string CookieValue, int connectionID)
        {
            IUtilities utilProvider = (IUtilities)BLControllerFactory.GetRegisteredConcreteFactory(connectionID);
            utilProvider.AddNewCookie(CookieName, CookieValue);
        }

        public static bool ContainsCookie(string CookieName, int connectionID)
        {
            IUtilities utilProvider = (IUtilities)BLControllerFactory.GetRegisteredConcreteFactory(connectionID);
            return utilProvider.ContainsCookie(CookieName);
        }

        public static void LoadColumnListCookieFromLocalCache(int connectionId) {
            #region gets all the columns to be displayed from the saved local user xml file ()

            MyZillaSettingsDataSet appSettings = MyZillaSettingsDataSet.GetInstance();
            DataRow[] userColumnsList = appSettings.Columns.Select("ConnectionID = " + connectionId.ToString());

            #endregion

            //build COLUMNLIST cookie based on the user settings (columns he choosed to be displayed)
            if (userColumnsList.GetLength(0) > 0)
            {
                StringBuilder colCookieValue = new StringBuilder(String.Empty);
                foreach (DataRow row in userColumnsList)
                {
                    colCookieValue.Append(((MyZillaSettingsDataSet.ColumnsRow)row).Name + "%20");
                }
                AddCookie("COLUMNLIST", colCookieValue.ToString(), connectionId);
            }
        }

        public static void GenerateColumnListCookie(int connectionId)
        {

            #region gets all the columns to be displayed from the saved local user xml file ()

            MyZillaSettingsDataSet appSettings = MyZillaSettingsDataSet.GetInstance();
            DataRow[] userColumnsList = appSettings.Columns.Select("ConnectionID = " + connectionId.ToString());

            #endregion

            //build COLUMNLIST cookie based on the user settings (columns he choosed to be displayed)
            if (userColumnsList.GetLength(0) > 0)
            {
                StringBuilder colCookieValue = new StringBuilder(String.Empty);
                foreach (DataRow row in userColumnsList)
                {
                    colCookieValue.Append(((MyZillaSettingsDataSet.ColumnsRow)row).Name + "%20");
                }
                AddCookie("COLUMNLIST", colCookieValue.ToString(), connectionId);
            }

            #region get default 'columns to be displayed' list from Bugzilla system

            IUtilities utilProvider = (IUtilities)BLControllerFactory.GetRegisteredConcreteFactory(connectionId);

            string[] bugzillaColumnsList = null;
            bugzillaColumnsList = GetAllColumnsListFromCookie(connectionId);

            if (!Utils.ContainsCookie("COLUMNLIST", connectionId))
            {
                utilProvider.GenerateColumnsToBeDisplayed(null);
            }

            #endregion

            
            //when user has yet no local settings use the default one from Bugzilla
            if (userColumnsList.GetLength(0) == 0)
            {
                //
                string[] chkProperties = new string[3];
                foreach (string column in bugzillaColumnsList)
                {
                    chkProperties = column.Split('&');
                    //add displayed columns to the settings dataset
                    if (chkProperties[2] == "1")
                        appSettings.Columns.AddColumnsRow(connectionId, chkProperties[1], chkProperties[0], 100, 0, 0, -1, true);
                }
            }
        }

        public static string[] GetColumnsListFromCookie(int CustomUserID)
        {
            IUtilities utilProvider = (IUtilities)BLControllerFactory.GetRegisteredConcreteFactory(CustomUserID);
            return utilProvider.GetColumnsToBeDisplayedFromCookie();
        }

        public static string[] GetAllColumnsListFromCookie(int CustomUserID)
        {
            IUtilities utilProvider = (IUtilities)BLControllerFactory.GetRegisteredConcreteFactory(CustomUserID);
            return utilProvider.GetAllColumnsToBeDisplayedFromCookie();
        }


        public static void RemoveCookieCollectionForCurrentUser()
        {
            IUtilities utilProvider = (IUtilities)BLControllerFactory.GetRegisteredConcreteFactory(ConnectionId);
            utilProvider.RemoveCookieCollectionForUser();
        }


        public static void FillAssignToCombo(string component, ComboBox cmbAssignedTo, NameValueCollection catAssignedTo)
        {
            // populate the AssignTo and CC accordingly.

            List<string> alAssignedTo = new List<string>();

            for (int i = 0; i < catAssignedTo.Count; i++)
            {
                if (catAssignedTo.GetKey(i) == component)
                {
                    if (catAssignedTo.GetValues(i) != null)
                    {
                        foreach (string item in catAssignedTo.GetValues(i))
                        {
                            alAssignedTo.Add(item);
                        }
                    }
                }
            }

            if (alAssignedTo.Count > 0)
            {

                ArrayList al = new ArrayList();


                if (alAssignedTo.Count == 1)
                {
                    al.Add(new CatalogueItem(alAssignedTo[0], alAssignedTo[0]));
                }
                else
                {
                    // alAssignedTo[0] is the default assignee
                    for (int i = 1; i < alAssignedTo.Count; i++)
                    {
                        CatalogueItem item = new CatalogueItem(alAssignedTo[i], alAssignedTo[i]);

                        al.Add(item);
                    }

                }

                cmbAssignedTo.DisplayMember = "CatText";
                cmbAssignedTo.ValueMember = "CatValue";
                cmbAssignedTo.DataSource = al;

                cmbAssignedTo.SelectedValue = alAssignedTo[0];

            }
            else
            {
                // no code here.
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="component">Name of the component on which cc depends on</param>
        /// <param name="lstCC">CheckedListBox to be filled</param>
        /// <param name="catCC"></param>
        public static void PopulateContactListForComponent(string component, CheckedListBox ccCheckedListBox, NameValueCollection ccCatalog)
        {
            try
            {
                List<string> ccList = new List<string>();

                // verify if it is not dependent (bugzilla 2.0)
                for (int i = 0; i < ccCatalog.Count; i++)
                {
                    if (ccCatalog.GetKey(i) == MyZilla.BusinessEntities.Dependencies.NoComponentDependency)
                    {
                        foreach (string item in ccCatalog.GetValues(i))
                        {
                            ccList.Add(item);
                        }
                        break;
                    }
                }

                //if dependent
                if (ccList.Count == 0)
                {
                    for (int i = 0; i < ccCatalog.Count; i++)
                    {
                        if (ccCatalog.GetKey(i) == component)
                        {
                            foreach (string item in ccCatalog.GetValues(i))
                            {
                                ccList.Add(item);
                            }
                            break;
                        }
                    }
                }


                // populate check list box (It doesn't work with the "populate list box method ... ")
                ccCheckedListBox.Items.Clear();

                foreach (string item in ccList)
                {
                    string[] element = item.Split(','); // value, text

                    if (element.Length == 2)
                    {
                        ccCheckedListBox.Items.Add(element[1]);
                    }
                    else
                    {
                        ccCheckedListBox.Items.Add(element[0]);
                    }
                }

                ccCheckedListBox.Enabled = true;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static void CreateNewResultsTab(List<MyZilla.BusinessEntities.Bug> bg, NameValueCollection SearchParams, TabControl _tabCtrl, TreeView TreeViewQueries, TDSQueriesTree.QueriesRow Query, bool ShowQueryCriteria, bool QueryCriteriaVisible, bool ExecuteQuery, string QueryNodeKey)
        {
            try
            {

                string tabKey = QueryNodeKey;
                string tabHeaderText = "New Query";

                if (Query != null)
                {
                    //the key of the tabs is Query ID
                    tabKey = "query " + Query.ID.ToString();
                    tabHeaderText = Query.Name + "-" + TreeViewQueries.SelectedNode.Parent.Text;
                }
                else
                {
                    if (bg != null)
                    {
                        //tabKey = "query -2";
                        tabHeaderText = "General search";
                    }
                }

                TabPage tabResults = _tabCtrl.TabPages[tabKey];

                UCResults bugsResults;
                if (tabResults == null)
                {

                    tabResults = new TabPage(tabHeaderText + "*****");
                    tabResults.Name = tabKey;

                    bugsResults = new UCResults(ConnectionId, bg, SearchParams, TreeViewQueries, ShowQueryCriteria, QueryCriteriaVisible, ExecuteQuery, QueryNodeKey);
                    bugsResults.Dock = DockStyle.Fill;
                    tabResults.Controls.Add(bugsResults);
                    bugsResults.ExecuteQueryCompleted += new UCResults.ExecuteQueryCompletedEventHandler(bugsResults_ExecuteQueryCompleted);
                    bugsResults.Name = "ucResults";


                    _tabCtrl.TabPages.Add(tabResults);

                    _tabCtrl.SelectedTab = tabResults;

                    Application.DoEvents();

                    bugsResults.LoadAssignToContextMenu();
                }
                else
                {
                    Control[] resultCtrl = tabResults.Controls.Find("ucResults", true);

                    if (resultCtrl != null && resultCtrl.GetLength(0) == 1)
                    {
                        bugsResults = (UCResults)(resultCtrl[0]);
                        bugsResults.DisplayQueryCriteria = ShowQueryCriteria;
                        bugsResults.LoadBugs(bg, SearchParams);
                    }
                }

                tabResults.ToolTipText = GetTabResultsTooltip(TreeViewQueries.SelectedNode);
                tabResults.Tag = ConnectionId;
                _tabCtrl.SelectedTab = tabResults;

            }
            catch (Exception ex)
            {
                throw ex;

            }

        }


        public static void CreateNewResultsTab(List<MyZilla.BusinessEntities.Bug> bg, NameValueCollection SearchParams, TabControl _tabCtrl, TreeView TreeViewQueries, TDSQueriesTree.QueriesRow Query, bool ShowQueryCriteria, bool QueryCriteriaVisible, string QueryNodeKey)
        {
            try
            {
                string tabKey = QueryNodeKey;
                string tabHeaderText = "New Query";
                bool _executeQuery = true;

                if (Query != null)
                {
                    //the key of the tabs is Query ID
                    tabKey = "query " + Query.ID.ToString();
                    tabHeaderText = Query.Name + "-" + TreeViewQueries.SelectedNode.Parent.Text;
                }
                else
                {
                    if (bg != null)
                    {
                        //tabKey = "query -2";
                        tabHeaderText = "General search";
                    }
                    else
                        _executeQuery = false;
                }

                TabPage tabResults = _tabCtrl.TabPages[tabKey];

                UCResults bugsResults;
                if (tabResults == null)
                {
                    //_tabCtrl.TabPages.Add(tabKey, tabHeaderText + "*****");
                    //tabResults = _tabCtrl.TabPages[tabKey];
                    tabResults = new TabPage(tabHeaderText + "*****");
                    tabResults.Name = tabKey;
                    //tabResults.BackColor = System.Drawing.SystemColors.Window;

                    bugsResults = new UCResults(ConnectionId, bg, SearchParams, TreeViewQueries, ShowQueryCriteria, QueryCriteriaVisible, _executeQuery, QueryNodeKey);
                    tabResults.Controls.Add(bugsResults);

                    bugsResults.Dock = DockStyle.Fill;

                    bugsResults.ExecuteQueryCompleted += new UCResults.ExecuteQueryCompletedEventHandler(bugsResults_ExecuteQueryCompleted);
                    bugsResults.QueryNameChanged += new UCResults.QueryNameChangedEventHandler(bugsResults_QueryNameChanged);
                    bugsResults.Name = "ucResults";
                    _tabCtrl.TabPages.Add(tabResults);
                    Application.DoEvents();
                    _tabCtrl.SelectedTab = tabResults;
                    Application.DoEvents();


                    bugsResults.LoadAssignToContextMenu();

                    bugsResults.LoadBugs(null, null);
                }
                else
                {
                    Control[] resultCtrl = tabResults.Controls.Find("ucResults", true);

                    if (resultCtrl != null && resultCtrl.GetLength(0) == 1)
                    {
                        bugsResults = (UCResults)(resultCtrl[0]);

                        bugsResults.LoadBugs(bg, SearchParams);
                    }
                }

                tabResults.ToolTipText = GetTabResultsTooltip(TreeViewQueries.SelectedNode);
                tabResults.Tag = ConnectionId;
                _tabCtrl.SelectedTab = tabResults;
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        static void bugsResults_QueryNameChanged(object sender, OnQueryNameChangedEventArgs e)
        {
            TabPage tab = (TabPage)((UCResults)sender).Parent;
            tab.Name = "query " + e.QueryId.ToString();
            tab.Text = String.Concat(e.QueryName, "-", e.FolderName, "    ");
        }

        static void bugsResults_ExecuteQueryCompleted(object sender, OnExecuteQueryCompletedEventArgs e)
        {
            if (e.QueryNodeKey != null)
            {
                TreeNode[] nodes = e.TreeView.Nodes.Find(e.QueryNodeKey, true);

                if (nodes.GetLength(0) == 1)
                {
                    NodeDescription nodeDescr = (NodeDescription)nodes[0].Tag;
                    TDSQueriesTree.QueriesRow query = (TDSQueriesTree.QueriesRow)nodeDescr.NodeData;
                    query.BugsCount = e.BugsCount;
                    nodes[0].Text = String.Format(nodeDescr.Format, e.BugsCount.ToString());
                }
            }
        }

        /// <summary>
        /// Walks from a child node to the root and build the nodes path to be displayed as tooltip
        /// </summary>
        /// <param name="CurrentNode">Selected query node</param>
        /// <returns>string</returns>
        private static string GetTabResultsTooltip(TreeNode CurrentNode)
        {
            StringBuilder result = new StringBuilder();

            while (CurrentNode != null)
            {
                if (result.Length > 0)
                    result.Insert(0, '\\');
                result.Insert(0, CurrentNode.Text);
                CurrentNode = CurrentNode.Parent;
            }

            return result.ToString();
        }

        public static NameValueCollection GetParamsForFreeTextQuery(string Text)
        {
            NameValueCollection searchParams = new NameValueCollection();

            searchParams.Add("query_format", "specific");

            searchParams.Add("order", "relevance+desc");

            searchParams.Add("bug_status", "__all__");

            searchParams.Add("product", String.Empty);

            searchParams.Add("content", Text);

            return searchParams;
        }

        public static NameValueCollection GetDefaultSearchParametersConfiguration()
        {
            NameValueCollection searchParams = new NameValueCollection();

            searchParams.Add("query_format", "advanced");

            searchParams.Add("bug_status", "new");
            searchParams.Add("bug_status", "assigned");
            searchParams.Add("bug_status", "reopened");

            return searchParams;
        }

        /// <summary>
        /// Gets the Url associated with the connection
        /// ,for a specified bug id
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="bugId"></param>
        /// <returns>string containing the url for retriving the bug</returns>
        public static string GetBugURL(int connectionId, string bugId)
        {
            string myZillaUrl = MyZillaSettingsDataSet.GetInstance().GetUrlForConnection(connectionId);

            myZillaUrl = GetURLStripped(myZillaUrl) + SHOW_BUG_PAGE;

            myZillaUrl = String.Format(myZillaUrl, bugId);

            return myZillaUrl;
        }

        public static string CheckIfValidConnection()
        {

            string result = string.Empty;

            if (_catManager == null)
            {
                _catManager = CatalogueManager.Instance();
            }

            Catalogues catalogues = _catManager.GetCataloguesForConnection(Utils.ConnectionId);

            if (catalogues == null)
            {
                // get the connection info
                if (_appSettings == null)
                {
                    _appSettings = MyZillaSettingsDataSet.GetInstance();
                }

                string connInfo = _appSettings.GetConnectionInfo(Utils.ConnectionId);

                result = string.Format(Messages.NoActiveConnection, connInfo);

            }

            return result;
        }

        public static string EscapeString(string FieldValue)
        {
            return FieldValue.Replace("&", "%26").Replace(";", "%3B");
        }




        /// <summary>
        /// Get the mime type of a file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>mime type of the file</returns>
        public static string GetFileContentType(string fileName)
        {

            string mime = "application/octetstream";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey rk = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (rk != null && rk.GetValue("Content Type") != null)
                mime = rk.GetValue("Content Type").ToString();
            return mime;

        }

        public static void SendMailTo(Control email)
        {
            email.Cursor = Cursors.WaitCursor;

            string mailTo = String.Format("mailto:{0}?subject=", email.Text);

            try
            {
                System.Diagnostics.Process.Start(mailTo);
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "lblReporterValue_MouseClick", LoggingCategory.Exception);

                MessageBox.Show(null, ex.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                email.Cursor = Cursors.Hand;
            }
        }

        #endregion

        #region Private Variables and Methods

        private static string _userAppDataPath;

        /// <summary>
        /// If "C:\Documents and Settings\username\Application Data\remend\Taskzilla" does not exit, it will be created
        /// </summary>
        public static string UserAppDataPath
        {
            get
            {
                if (String.IsNullOrEmpty(_userAppDataPath))
                {
                    try
                    {
                        _userAppDataPath = System.Windows.Forms.Application.UserAppDataPath;
                        Directory.Delete(_userAppDataPath);
                        string[] pathParts = _userAppDataPath.Split(System.IO.Path.DirectorySeparatorChar);
                        string pathSep = System.Char.ToString(System.IO.Path.DirectorySeparatorChar);
                        _userAppDataPath = System.String.Join(pathSep, pathParts, 0, pathParts.Length - 1);
                    }
                    catch (IOException ex)
                    {
                        MyLogger.Write(ex, "UserAppDataPath", LoggingCategory.Exception);
                        MessageBox.Show("An error occured when creating the default folders for the application!", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        _userAppDataPath = null;
                    }
                }
                return _userAppDataPath;
            }
        }

        /// <summary>
        /// Used to populate a list box control
        /// </summary>
        [System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
        public class CatalogueItem
        {
            private string catValue;

            private string catText;

            public CatalogueItem(string value, string text)
            {
                catValue = value;

                catText = text;
            }

            public string CatValue
            {
                get { return catValue; }
            }

            public string CatText
            {
                get { return catText; }
            }
        }

        #endregion

        #region Loading form

        public static void ActivateLoadingForm()
        {

            // verify ShowLoadingForm in app global settings.
            _appSettings = MyZillaSettingsDataSet.GetInstance();

            TDSettings.GlobalSettingsRow appSettings = _appSettings.GetGlobalSettings();

            if (appSettings.ShowLoadingForm)
            {

                // open form status 
                if ((FormContainer as MainForm).statusForm == null)
                {
                    FormStatus frm = new FormStatus();

                    frm.Show();

                }
                else
                {
                    (FormContainer as MainForm).statusForm.Show();
                }
            }
            else
            {
                // no code here.
            }

        }

        public static void LoadingFormInBackground()
        {
            if ((FormContainer as MainForm).statusForm != null)
            {
                (FormContainer as MainForm).statusForm.btnCancel_Click(null, new EventArgs());
            }
        }


        #endregion

        #region Cached data

        public static string ReplaceNewLines(string InString)
        {
            return InString.Replace("\n", " " + Environment.NewLine);
        }

        #endregion

        #region Export


        /// <summary>
        /// Export the file with the specified extension.
        /// </summary>
        /// <param name="ext">
        /// .pdf, .xls
        /// </param>
        /// <param name="allRows">
        /// true = all rows will be exported
        /// false = only the selected rows will be exported
        /// </param> 
        public static bool ExportTo(string extFile, object actionButton, bool allRows)
        {
            bool exportDone = true;
            try
            {
                // get the data source

                Control[] results = (Utils.FormContainer as MainForm).tBSI.SelectedTab.Controls.Find("ucResults", true);

                if (results.GetLength(0) == 1)
                {

                    UCResults ucResults = (UCResults)results[0];

                    if (!ucResults.IsSearchingBugs)
                    {

                        DataView dw = new DataView();

                        if (allRows == true)
                        {
                            dw = ucResults.BugsView;
                        }
                        else
                        {

                            DataTable dt = ucResults.BugsView.Table.Clone();


                            foreach (DataRow row in ucResults.MultipleSelectedRows)
                            {
                                dt.Rows.Add(row.ItemArray);
                            }

                            dw = dt.DefaultView;

                            dw.Sort = ucResults.BugsView.Sort;
                        }

                        #region Prepare field names, captions, widths for the columns of the dinamic generated report

                        List<string> fields = new List<string>(ucResults.dgvResults.Columns.Count);
                        List<string> captionFields = new List<string>(ucResults.dgvResults.Columns.Count);
                        List<int> widthFields = new List<int>(ucResults.dgvResults.Columns.Count);

                        int count = 0;
                        for (int i = 0; i < ucResults.dgvResults.Columns.Count; i++)
                        {
                            count++;
                            fields.Add(String.Empty);
                            captionFields.Add(String.Empty);
                            widthFields.Add(100);
                        }

                        for (int i = 0; i < dw.Table.Columns.Count; i++)
                        {

                            if (ucResults.dgvResults.Columns[i].Visible)
                            {
                                fields[ucResults.dgvResults.Columns[i].DisplayIndex] = dw.Table.Columns[i].ColumnName;
                                captionFields[ucResults.dgvResults.Columns[i].DisplayIndex] = dw.Table.Columns[i].Caption;
                                widthFields[ucResults.dgvResults.Columns[i].DisplayIndex] = ucResults.dgvResults.Columns[i].Width;
                            }
                        }

                        //remove hidden items (columns that are not displayed in the grid)
                        for (int i = fields.Count - 1; i >= 0; i--)
                        {
                            if (fields[i].Length == 0)
                            {
                                fields.RemoveAt(i);
                                captionFields.RemoveAt(i);
                                widthFields.RemoveAt(i);
                            }
                        }

                        #endregion


                        BackgroundWorker bkgExport = new BackgroundWorker();

                        bkgExport.DoWork += new DoWorkEventHandler(bkgExport_DoWork);

                        bkgExport.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgExport_RunWorkerCompleted);

                        bkgExport.ProgressChanged += new ProgressChangedEventHandler(bkgExport_ProgressChanged);

                        bkgExport.WorkerReportsProgress = true;

                        ArrayList al = new ArrayList();

                        al.Add(dw);

                        al.Add(extFile);

                        al.Add(actionButton);

                        al.Add(fields);
                        al.Add(captionFields);
                        al.Add(widthFields);

                        ToolStripButton btn = actionButton as ToolStripButton;

                        if (btn != null)
                        {
                            btn.Enabled = false;
                        }

                        ToolStripMenuItem menu = actionButton as ToolStripMenuItem;

                        if (menu != null)
                        {
                            menu.Enabled = false;
                        }

                        Utils.ActivateLoadingForm();

                        bkgExport.RunWorkerAsync(al);
                    }
                    else {
                        MessageBox.Show(Properties.Messages.ExportNotPossible, "Export", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else {
                    exportDone = false;
                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "ExportTo", LoggingCategory.Exception);

                throw ex;

            }

            return exportDone;

        }

        static void bkgExport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BackgroundWorker bkgWork = sender as BackgroundWorker;

            if (_asyncOpManager == null)
            {
                _asyncOpManager = AsyncOperationManagerList.GetInstance();
            }
            switch (e.ProgressPercentage)
            {
                case 0:

                    _asyncOpManager.BeginOperation(bkgWork, Messages.GenerateReport , e.ProgressPercentage);

                    break;


                case 100:

                    _asyncOpManager.UpdateStatus(bkgWork, Messages.GenerateReport , e.ProgressPercentage);

                    break;

                default:

                    _asyncOpManager.UpdateStatus(bkgWork, Messages.GenerateReport , e.ProgressPercentage);

                    break;

            }

        }

        static void bkgExport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // check the status of the async operation.
            if (e.Error != null)
            {
                MessageBox.Show(Utils.FormContainer, e.Error.Message, Messages.Error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                // no code here.
            }

            if (e.Result as ToolStripButton != null)
            {
                (e.Result as ToolStripButton).Enabled = true;

            }

            if (e.Result as ToolStripMenuItem != null)
            {
                (e.Result as ToolStripMenuItem).Enabled = true;

            }

        }

        static void bkgExport_DoWork(object sender, DoWorkEventArgs e)
        {
            MemoryStream m_rdl;

            // get the new thread argument.
            ArrayList al = e.Argument as ArrayList;

            TDSettings.GlobalSettingsRow savedGlobalSettings = _appSettings.GetGlobalSettings();

            #region check file system settings

            string fileName = "BugList";

            string fileExtention = al[1].ToString();

            string outputFile = savedGlobalSettings.ReportFilesPath + "\\BugList" + al[1].ToString();

            if (!Directory.Exists(savedGlobalSettings.ReportFilesPath))
            {
                MessageBox.Show(String.Format(Messages.ExportFolderDoesNotExist));
                return;
            }

            
            string checkedOutFileName = outputFile;
            int i = 0;
            while (File.Exists(checkedOutFileName))
            {

                checkedOutFileName = String.Concat(Path.Combine(savedGlobalSettings.ReportFilesPath, fileName), String.Format("({0})", i++), fileExtention);    
            }

            #endregion

            BackgroundWorker bkgWork = sender as BackgroundWorker;

            try
            {

                bkgWork.ReportProgress(0); 

                DataView dw = al[0] as DataView;

                if (dw != null)
                {
                    m_rdl = GenerateRdl((List<string>)al[3], (List<string>)al[4], (List<int>)al[5]);

                    Microsoft.Reporting.WinForms.ReportViewer _reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();

                    //BindingSource bindingSource1 = new BindingSource();

                    //Microsoft.Reporting.WinForms.ReportDataSource rptSource2 = new Microsoft.Reporting.WinForms.ReportDataSource("BSI_BusinessEntities_DummyBug", bindingSource1);

                    //_reportViewer.LocalReport.DataSources.Add(rptSource2);

                    //_reportViewer.LocalReport.ReportEmbeddedResource = "BSI.UI.Reports.Report_Bugs.rdlc";

                    //bindingSource1.DataSource = dw;

                    //_reportViewer.RefreshReport();

                    BindingSource bindingSource1 = new BindingSource();

                    Microsoft.Reporting.WinForms.ReportDataSource rptSource2 = new Microsoft.Reporting.WinForms.ReportDataSource("MyData", bindingSource1);

                    _reportViewer.LocalReport.DataSources.Add(rptSource2);

                    _reportViewer.LocalReport.LoadReportDefinition(m_rdl);

                    bindingSource1.DataSource = dw;

                    _reportViewer.RefreshReport();

                    bkgWork.ReportProgress(65); 

                    Microsoft.Reporting.WinForms.LocalReport lr = _reportViewer.LocalReport;

                    // get the path where the files are saved.

                    Microsoft.Reporting.WinForms.Warning[] warnings;
                    string[] streamids;
                    string mimeType = String.Empty;
                    string encoding = String.Empty;
                    string extension = String.Empty;
                    string deviceInfo = String.Empty;
                    byte[] bytes = null;

                    switch (al[1].ToString())
                    {
                        case ".pdf":
                            bytes = lr.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);
                            break;
                        case ".xls":
                            bytes = lr.Render("Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                            break;
                    }

                    FileStream fs = new FileStream(checkedOutFileName, FileMode.Create);

                    fs.Write(bytes, 0, bytes.Length);

                    fs.Close();

                    bkgWork.ReportProgress(80); 

                    System.Diagnostics.Process process = new System.Diagnostics.Process();

                    process = new System.Diagnostics.Process();

                    process.StartInfo.FileName = checkedOutFileName;

                    process.Start();
                }
            }
            catch (Exception ex)
            {
                //if no application is associated with the file type....
                if (ex.Message.Contains(ERROR_TOKEN))
                    OpenAs(checkedOutFileName);
                else
                {
                    MyLogger.Write(ex, "bkgExport_DoWork", LoggingCategory.Exception);
                }
            }
            finally
            {
                e.Result = al[2];

                if (bkgWork != null)
                {
                    bkgWork.ReportProgress(100); 
                }
            }
        }

        private static MemoryStream GenerateRdl(List<string> selectedFields, List<string> captionFields, List<int> widthFields)
        {
            MemoryStream ms = new MemoryStream();
            RdlGenerator gen = new RdlGenerator();
            gen.SelectedFields = selectedFields;
            gen.CaptionFields = captionFields;
            gen.WidthFields = widthFields;
            gen.WriteXml(ms);
            ms.Position = 0;
            return ms;
        }

        public static object[] GetPublishedMyZillaVersion(Form owner, string CurrentAppVersion, bool forceCheck) {
            object[] result = new object[3];
            //check newer version at http://www.myzilla.ro/download_link.php

            IUtilities utilProvider = (IUtilities)BLControllerFactory.GetRegisteredConcreteFactory(1);

            string myZillaVersionUrl = ConfigurationManager.AppSettings["MyZillaVersionWebPage"];

            string version = null;

            if (!String.IsNullOrEmpty(myZillaVersionUrl))
                version = utilProvider.GetPublishedMyZillaVersion(myZillaVersionUrl, CurrentAppVersion);
            else{
                MyLogger.Write("MyZillaVersionWebPage configuration item is missing or empty!", "GetPublishedMyZillaVersion", LoggingCategory.Exception);
                version = null;
            }

            result[0] = version;
            result[1] = forceCheck;
            result[2] = owner;

            return result;
        }

        public static void OpenDownloadPage()
        {
            string myZillaWebSiteUrl = System.Configuration.ConfigurationManager.AppSettings["MyZillaWebSite"];

            if (!String.IsNullOrEmpty(myZillaWebSiteUrl))
                System.Diagnostics.Process.Start(myZillaWebSiteUrl);
            else
            {
                MyLogger.Write("MyZillaWebSite configuration item is missing or empty!", "OpenDownloadPage", LoggingCategory.Exception);
            }
        }

        #endregion

    }

    #region Enums

    public enum QueryType
    {
        UserCustom,
        Predefined,
        Temporary,
        Remote
    }

    public enum NodeType
    {
        Connection,
        Query,
        Folder
    }

    public struct GettingBugErrorMessages
    {
        public const string InvalidBugId = "InvalidBugId";
        public const string BugIdNotFound = "NotFound";
        public const string BugIdNotPermitted = "NotPermited";
    }


    #endregion

    public class BugStatus
    {
        private BugStatus() { }

        public const string NONE = "NONE";
        public const string UNCONFIRMED = "UNCONFIRMED";
        public const string NEW = "NEW";
        public const string ASSIGNED = "ASSIGNED";
        public const string RESOLVED = "RESOLVED";
        public const string VERIFIED = "VERIFIED";
        public const string REOPENED = "REOPENED";
        public const string CLOSED = "CLOSED";
    }

    public class BugResolution
    {
        private BugResolution() { }

        public const string FIXED = "FIXED";
        public const string DUPLICATE = "DUPLICATE";
        public const string WONTFIX = "WONTFIX";
        public const string WORKSFORME = "WORKSFORME";
        public const string INVALID = "INVALID";
        public const string REMIND = "REMIND";
        public const string LATER = "LATER";
        public const string MOVED = "MOVED";
        public const string NONE = "---";
    }

    public class BugKnob
    {
        private BugKnob() { }

        public const string NONE = "none";
        public const string CONFIRM = "confirm";
        public const string RESOLVE = "resolve";
        public const string CHANGE_RESOLUTION = "change_resolution";
        public const string DUPLICATE = "duplicate";
        public const string ACCEPT = "accept";
        public const string REASSIGN = "reassign";
        public const string REOPEN = "reopen";
        public const string VERIFY = "verify";
        public const string CLOSE = "close";
    }
}
