using MyZilla.BusinessEntities;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System;

namespace MyZilla.UI.ConfigItems {


    partial class TDSQueriesTree
    {
        partial class QueriesDataTable
        {
        }
    
        partial class ParametersValuesDataTable
        {
        }
   
        private static string _appPath;

        private static TDSQueriesTree _instance;
        private static string _fileName = "QueriesTree.xml";
        private static string _defaultFileName = "DefaultQueriesTree.xml";

        public static TDSQueriesTree Instance() {

            if (_instance == null)
            {
                _instance = new TDSQueriesTree();
            }

            return _instance;
        } 

        public void RemoveUserItems(int userId) {
            DataRow[] folders = _instance.Folders.Select("UserID = " + userId.ToString());
            if (folders != null)
            {

                for (int i = 0; i < folders.GetLength(0); i++)
                {
                    folders[i].Delete();
                }

                _instance.Folders.AcceptChanges();
            }
            
        }

        public void LoadDefaultDataForUserId(TreeView treeView, TDSettings.ConnectionRow connectionRow)
        {
            string defaultFileName = Application.StartupPath + Path.DirectorySeparatorChar + _defaultFileName;

            try
            {
                if (File.Exists(defaultFileName))
                {
                    TDSQueriesTree treeStructure = new TDSQueriesTree();
                    treeStructure.ReadXml(defaultFileName);
                    //treeStructure.WriteXml("c:\\test.xml");
                    BuildTreeStructureForUserId(treeStructure, connectionRow );
                }
            }
            catch (IOException)
            {
                throw (new IOException("File " + defaultFileName + " does not exist."));
            }
        }

        public void LoadDefaultData(string applicationPath)
		{
            _appPath = applicationPath;
            string fileName = applicationPath + Path.DirectorySeparatorChar + _fileName;
            string defaultFileName = Application.StartupPath + Path.DirectorySeparatorChar + _defaultFileName;

            try
            {

                if (File.Exists(fileName))
                {
                    _instance.ReadXml(fileName);
                }
                //if QueriesTree does not exist in the windows user application data folder
                //load the default values from the application folder
                else if (File.Exists(defaultFileName))
                {
                    TDSQueriesTree t = new TDSQueriesTree();
                    t.ReadXml(defaultFileName);
                    _instance.QueryTypes.Merge( t.QueryTypes);
                    _instance.QueryParameters.Merge(t.QueryParameters);

                    MyZillaSettingsDataSet settings = MyZillaSettingsDataSet.GetInstance ();
                    TDSettings.ConnectionDataTable  connections  = settings.GetActiveConnections ();

                    foreach (TDSettings.ConnectionRow  connection  in connections ) 
                    {   
                        CatalogueManager catalogues = CatalogueManager.Instance();
                        if (catalogues.GetCataloguesForConnection(connection.ConnectionId)!=null)
                            BuildTreeStructureForUserId(t, connection );
                    }
                    
                }else
                    throw (new IOException("Default queries configuration [" + fileName + "] is missing!"));
            }
            catch (IOException)
            {
                throw (new IOException("File " + fileName + " not exist."));
            }

		}

        /// <summary>
        /// Walks through the default folder structure store in the DefaultQueriesTree.xml
        /// and replicates the structure for the current connection
        /// </summary>
        /// <param name="Structure"></param>
        /// <param name="Connection"></param>
        public void BuildTreeStructureForUserId(TDSQueriesTree initialQueryTree, MyZillaSettingsDataSet.ConnectionRow  connectionRow) {

            foreach (TDSQueriesTree.FoldersRow folder in initialQueryTree.Folders.Rows)
            {
                CreateFolder(connectionRow, folder.Name, folder.LevelID, folder.ParentID);
            }
            _instance.Folders.AcceptChanges();

            RefreshTreePerUser(connectionRow );
        }

        /// <summary>
        /// Returns the folder datarow object if folder exists, otherwise returns null
        /// </summary>
        /// <param name="Connection"></param>
        /// <param name="FolderName"></param>
        /// <returns></returns>
        private static FoldersRow GetFolderByName(TDSettings.ConnectionRow connectionRow, string folderName)
        {
            DataRow[] folders;
            FoldersRow result;

            folders = _instance.Folders.Select("Name = '" + folderName + "' AND UserID = " + connectionRow.ConnectionId.ToString());

            if (folders == null || folders.GetLength(0) == 0)
            {
                result = null;
            }
            else { //length should be 1
                result = (FoldersRow)folders[0];
            }

            return result;
        }

        private static FoldersRow CreateFolder(TDSettings.ConnectionRow connectionRow, string forlderName, int levelId, int parentId){

            FoldersRow newFolder = _instance.Folders.NewFoldersRow();

            newFolder.Name = forlderName;
            newFolder.UserID = connectionRow.ConnectionId;
            newFolder.LevelID = levelId;
            newFolder.ParentID = parentId;
            newFolder.ReadOnly = true;
            newFolder.Expanded = false;
            newFolder.Deleted = false;

            _instance.Folders.AddFoldersRow(newFolder);

            _instance.AcceptChanges();

            return newFolder;
        }

        /// <summary>
        /// Checks for the predefined folder structure and build it together with the predefined queries
        /// </summary>
        /// <param name="Connection"></param>
        public static void RefreshTreePerUser(TDSettings.ConnectionRow  connectionRow)
        {
            int parentID;

            DataRow[] folders;
            FoldersRow folder, productFolder;
            
            //Check for the default structure of folders and queries
            //Check for folder "Product Queries"
            folder = GetFolderByName(connectionRow, "Product Queries");
            if (folder == null)
            {
                //create product queries folder
                folder = CreateFolder(connectionRow, "Product Queries", 0, -1);

                if (folder == null)
                    throw new Exception("Queries folder structure is corrupted!");
            }
            
            parentID = folder.ID;

            DataRow[] Queries;
            string productName = String.Empty;
            QueriesRow query;

            #region add each product as a folder in the tree

            CatalogueManager catalogues = CatalogueManager.Instance();
            NameValueCollection products = catalogues.GetCataloguesForConnection (connectionRow.ConnectionId ).catalogueProduct;

            folders = _instance.Folders.Select("ParentID = " + parentID.ToString() + " AND UserID = " + connectionRow.ConnectionId.ToString());

            for (int i = folders.GetLength(0)-1; i >=0; i--)
            {
                productFolder = (FoldersRow)folders[i];

                //check if product belongs to the current user
                bool belongsToCurrentUser = false;
                
                for (int index = 0; index <  products.Count; index++)
                {
                    if (folder.Name == products.GetKey(index).Split(',')[1])
                    {
                        belongsToCurrentUser = true;
                        break;
                    }
                }

                if (!belongsToCurrentUser)
                {

                    productFolder.Delete();
                    //folder.Delete();
                }

            }
            

            //generate new product folders
            for (int i = 0; i < products.Count; i++)
            {
                productName = products.GetKey(i).Split(',')[1];
                CreateProductFolderDefaultQueries(connectionRow, parentID, productName);
            }

            #endregion

            //Check for the default folder "My Queries"

            folder = GetFolderByName(connectionRow, "My Queries");

            if (folder == null)
            {
                //think about throuwing error
                folder = CreateFolder(connectionRow, "My Queries", 0, -1);

                if (folder == null) {
                    throw new Exception("Queries folder structure is corrupted!");
                }
            }

            #region Check if exists predefined query My Opened BUGS
            Queries = _instance.Queries.Select("Name = 'My opened bugs' AND FolderID = " + folder.ID);

            if (Queries.GetLength(0) == 0)
            {
                query = _instance.Queries.NewQueriesRow();

                query.Name = "My opened bugs";
                query.Description = "All my opened bugs for any product";
                query.FolderID = folder.ID;
                query.TypeID = Convert.ToByte(_queryTypes.Predefined);
                _instance.Queries.AddQueriesRow(query);
                _instance.AddParameterValuesForQuery(query, GetParamsForOpenedBugsQuery(String.Empty, true, connectionRow.UserName));
            }
            #endregion

            #region Check if exists predefined query My Closed BUGS
            Queries = _instance.Queries.Select("Name = 'My closed bugs' AND FolderID = " + folder.ID);

            if (Queries.GetLength(0) == 0)
            {
                query = _instance.Queries.NewQueriesRow();
                query.Name = "My closed bugs";
                query.Description = "All my closed bugs for any product";
                query.FolderID = folder.ID;
                query.TypeID = Convert.ToByte(_queryTypes.Predefined);
                _instance.Queries.AddQueriesRow(query);
                _instance.AddParameterValuesForQuery(query, GetParamsForClosedBugsQuery(String.Empty, true, connectionRow.UserName));
            }
            #endregion

            #region Check if exists predefined query My Fixed BUGS
            Queries = _instance.Queries.Select("Name = 'My fixed bugs' AND FolderID = " + folder.ID);

            if (Queries.GetLength(0) == 0)
            {
                query = _instance.Queries.NewQueriesRow();
                query.Name = "My fixed bugs";
                query.Description = "All my fixed bugs for any product";
                query.FolderID = folder.ID;
                query.TypeID = Convert.ToByte(_queryTypes.Predefined);
                _instance.Queries.AddQueriesRow(query);
                _instance.AddParameterValuesForQuery(query, GetParamsForFixedBugsQuery(String.Empty, true, connectionRow.UserName));
            }
            #endregion

            //Update Tree with the default structure
            _instance.Folders.AcceptChanges();

            _instance.Queries.AcceptChanges();

            _instance.ParametersValues.AcceptChanges();

        }

        private static string CreateProductFolderDefaultQueries(TDSettings.ConnectionRow connectionRow, int parentId, string productName)
        {
            FoldersRow productFolder = GetFolderByName(connectionRow, productName);

            QueriesRow query;

            if (productFolder == null)
                productFolder = CreateFolder(connectionRow, productName, 1, parentId);

            if (productFolder != null)
            {
                //*********************************************************************
                query = _instance.Queries.NewQueriesRow();
                query.Name = "Opened bugs";
                query.Description = "Opened bugs for the product [" + productName + "]";
                query.FolderID = productFolder.ID;
                query.TypeID = Convert.ToByte(_queryTypes.Predefined);
                _instance.Queries.AddQueriesRow(query);
                _instance.AddParameterValuesForQuery(query, GetParamsForOpenedBugsQuery(productName, false, String.Empty));

                //*********************************************************************
                query = _instance.Queries.NewQueriesRow();
                query.Name = "Closed bugs";
                query.Description = "Closed bugs for the product [" + productName + "]";
                query.FolderID = productFolder.ID;
                query.TypeID = Convert.ToByte(_queryTypes.Predefined);
                _instance.Queries.AddQueriesRow(query);
                _instance.AddParameterValuesForQuery(query, GetParamsForClosedBugsQuery(productName, false, String.Empty));

                //*********************************************************************
                query = _instance.Queries.NewQueriesRow();
                query.Name = "Fixed bugs";
                query.Description = "Fixed bugs for the product [" + productName + "]";
                query.FolderID = productFolder.ID;
                query.TypeID = Convert.ToByte(_queryTypes.Predefined);
                _instance.Queries.AddQueriesRow(query);
                _instance.AddParameterValuesForQuery(query, GetParamsForFixedBugsQuery(productName, false, String.Empty));

                //*********************************************************************
                query = _instance.Queries.NewQueriesRow();
                query.Name = "All bugs";
                query.Description = "All bugs for the product [" + productName + "]";
                query.FolderID = productFolder.ID;
                query.TypeID = Convert.ToByte(_queryTypes.Predefined);
                _instance.Queries.AddQueriesRow(query);
                _instance.AddParameterValuesForQuery(query, GetParamsForAllBugsQuery(productName, true, String.Empty));


                //*********************************************************************
                query = _instance.Queries.NewQueriesRow();
                query.Name = "My bugs";
                query.Description = "All my bugs for the product [" + productName + "]";
                query.FolderID = productFolder.ID;
                query.TypeID = Convert.ToByte(_queryTypes.Predefined);
                _instance.Queries.AddQueriesRow(query);
                _instance.AddParameterValuesForQuery(query, GetParamsForAllBugsQuery(productName, true, connectionRow.UserName));
            }
            return productName;
        }

        private static NameValueCollection GetParamsForOpenedBugsQuery(string productName, bool myBugs, string userName)
        {
            NameValueCollection searchParams = new NameValueCollection();

            searchParams.Add("query_format", "advanced");

            searchParams.Add("short_desc_type", "allwordssubstr");
            searchParams.Add("short_desc", String.Empty);

            searchParams.Add("bug_status", "new");
            searchParams.Add("bug_status", "assigned");
            searchParams.Add("bug_status", "reopened");

            //if (productName.Length>0)
            searchParams.Add("product", productName);

            //field0-0-0=assigned_to&type0-0-0=equals&value0-0-0=mzavoi%40tremend.ro
            if (myBugs)
            {
                searchParams.Add("field0-0-0", "assigned_to");
                searchParams.Add("type0-0-0", "equals");
                searchParams.Add("value0-0-0", userName);
            }

            return searchParams;
        }

        private static NameValueCollection GetParamsForAllBugsQuery(string productName, bool myBugs, string userName)
        {
            NameValueCollection searchConfiguration = new NameValueCollection();

            searchConfiguration.Add("query_format", "advanced");

            searchConfiguration.Add("short_desc_type", "allwordssubstr");
            searchConfiguration.Add("short_desc", String.Empty);

            //if (productName.Length>0)
            searchConfiguration.Add("product", productName);

            //field0-0-0=assigned_to&type0-0-0=equals&value0-0-0=mzavoi%40tremend.ro
            if (myBugs)
            {
                searchConfiguration.Add("field0-0-0", "assigned_to");
                searchConfiguration.Add("type0-0-0", "equals");
                searchConfiguration.Add("value0-0-0", userName);
            }

            return searchConfiguration;
        }

        private static NameValueCollection GetParamsForClosedBugsQuery(string productName, bool myBugs, string userName)
        {
            NameValueCollection searchParams = new NameValueCollection();

            searchParams.Add("query_format", "advanced");

            searchParams.Add("short_desc_type", "allwordssubstr");
            searchParams.Add("short_desc", String.Empty);

            searchParams.Add("bug_status", "closed");

            searchParams.Add("product", productName);

            if (myBugs)
            {
                searchParams.Add("field0-0-0", "assigned_to");
                searchParams.Add("type0-0-0", "equals");
                searchParams.Add("value0-0-0", userName);
            }

            return searchParams;
        }

        private static NameValueCollection GetParamsForFixedBugsQuery(string productName, bool myBugs, string userName)
        {
            NameValueCollection searchParams = new NameValueCollection();

            searchParams.Add("query_format", "advanced");

            searchParams.Add("short_desc_type", "allwordssubstr");
            searchParams.Add("short_desc", String.Empty);

            searchParams.Add("bug_status", "resolved");
            searchParams.Add("bug_status", "verified");

            //if (productName.Length>0)
            searchParams.Add("product", productName);

            if (myBugs)
            {
                searchParams.Add("field0-0-0", "assigned_to");
                searchParams.Add("type0-0-0", "equals");
                searchParams.Add("value0-0-0", userName);
            }

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

        public static NameValueCollection GetSearchConfigurationFromUserInterface(Control container)
        {
            NameValueCollection _searchConfiguration = new NameValueCollection();

            _searchConfiguration.Add("query_format", "advanced");

            foreach (Control ctrl in container.Controls)
            {
                switch (ctrl.GetType().ToString())
                {
                    //case typeof(System.Windows.Forms.TextBox).ToString():
                    case "System.Windows.Forms.TextBox":
                        _searchConfiguration.Add(ctrl.Tag.ToString(), ctrl.Text);
                        break;
                    //case typeof(System.Windows.Forms.ComboBox).ToString():
                    case "System.Windows.Forms.ComboBox":
                        _searchConfiguration.Add(ctrl.Tag.ToString(), ((ComboBox)ctrl).SelectedValue.ToString());
                        break;
                    //case typeof(System.Windows.Forms.ListBox).ToString():
                    case "System.Windows.Forms.ListBox":
                        ListBox lb = (System.Windows.Forms.ListBox)(ctrl);
                        for (int i = 0; i < lb.SelectedItems.Count; i++)
                            _searchConfiguration.Add(ctrl.Tag.ToString(), ((MyZilla.UI.Utils.CatalogueItem)lb.SelectedItems[i]).CatText);
                        break;
                }
            }

            return _searchConfiguration;
        }

        public void AddParameterValuesForQuery(TDSQueriesTree.QueriesRow queryRow, NameValueCollection searchConfiguration)
        {
            DataRow[] parameters  = ParametersValues.Select("QueryID = " + queryRow.ID);
            for (int i = parameters.GetLength(0) - 1; i >= 0; i--)
                parameters[i].Delete();
            
            ParametersValues.AcceptChanges();

            for (int i = 0; i < searchConfiguration.Count; i++)
            {
                //compose QueryString based on the criteria selected in the interface
                for (int j = 0; j < searchConfiguration.GetValues(i).Length; j++)
                {
                    ParametersValuesRow qpValue = _instance.ParametersValues.NewParametersValuesRow();
                    qpValue.QueryID = queryRow.ID;
                    DataRow[] qpRows = _instance.QueryParameters.Select("Name = '" + searchConfiguration.GetKey(i) + "'");
                    if (qpRows.GetLength(0) == 1)
                    {
                        qpValue.ParameterID = Byte.Parse(qpRows[0][0].ToString());
                        qpValue.Value = searchConfiguration.GetValues(i)[j];

                        _instance.ParametersValues.Rows.Add(qpValue);
                    }
                }
            }
        }

        public QueriesRow AddNewQuery(string queryName, string queryDescription, int folderId, byte typeId, int bugsCount, NameValueCollection searchConfiguration)
        {

            int queryID = GetQueryIdByQueryName(queryName);
            QueriesRow qRow = null;

            if (queryID == -1)
            {
                qRow = _instance.Queries.NewQueriesRow();
                qRow.Name = queryName;
                qRow.Description = queryDescription;
                qRow.TypeID = typeId;
                qRow.FolderID = folderId;
                qRow.BugsCount = bugsCount;
                _instance.Queries.Rows.Add(qRow);
            }
            else 
                qRow = _instance.Queries.FindByID(queryID); 


            AddParameterValuesForQuery(qRow, searchConfiguration);


            return qRow;
        }

        public bool UpdateQuery(int queryId, NameValueCollection searchConfiguration)
        {


            QueriesRow qRow = _instance.Queries.FindByID(queryId);

            if (qRow == null)
            {
                return false;
            }

            AddParameterValuesForQuery(qRow, searchConfiguration);


            return true;
        }

        public void Save() {
            _instance.AcceptChanges();

            _instance.WriteXml(_appPath + Path.DirectorySeparatorChar + _fileName);
        }

        private int GetQueryIdByQueryName(string queryName) { 
            int QueryID = -1;

            DataRow[] drQueries = Queries.Select("Name = '" + queryName + "'");
            if (drQueries.GetLength(0) > 0)
                QueryID = ((QueriesRow)drQueries[0]).ID;

            return QueryID;
        }

        public NameValueCollection GetQueryParameters(int queryId) {

            NameValueCollection searchParams = new NameValueCollection();

            if (queryId>=0){
                DataRow[] qpValues = this.ParametersValues.Select("QueryID = " + queryId.ToString());
                searchParams = new NameValueCollection();
                for (int i = 0; i < qpValues.GetLength(0); i++) {

                    searchParams.Add(this.QueryParameters.FindByID(((ParametersValuesRow)qpValues[i]).ParameterID).Name,((ParametersValuesRow)qpValues[i]).Value);
                }
            }

            return searchParams;
        }

        private enum _queryTypes
        {
            User = 0,
            Predefined,
            Temporary,
            Remote
        }

        public void MarkFolderAsDeleted(FoldersRow folderRow) {
            folderRow.Deleted = true;
            _instance.Folders.AcceptChanges();
        }

        public void AddUserSubtree(TreeView treeView, TDSettings.ConnectionRow  connectionRow)
        {
            DataRow[] rows;
            TreeNode nodeUser;

            //find the node in the tree corresponding to the user-connection
            TreeNode[] nodesUser = treeView.Nodes.Find("User " + connectionRow.ConnectionId.ToString(), true);

            if (nodesUser.GetLength(0) == 1)
            {
                nodeUser = nodesUser[0];

                //find all folders of the user and sort them on LevelID
                rows = _instance.Folders.Select("UserID = " + Int16.Parse(nodeUser.Name.Replace("User", String.Empty)).ToString(), "LevelID, Name, ID, ParentID");
                TreeNode node = new TreeNode();

                if (rows.GetLength(0) == 0)
                    nodeUser.ForeColor = System.Drawing.Color.Gray;

                //check if product list changed on the server
                CatalogueManager catalogues = CatalogueManager.Instance();
                NameValueCollection products = catalogues.GetCataloguesForConnection(connectionRow.ConnectionId).catalogueProduct;
              


                string productName = String.Empty;
                FoldersRow productsRootFolder = GetFolderByName(connectionRow, "Product Queries");

                if (productsRootFolder == null) {
                    productsRootFolder = CreateFolder(connectionRow, "Product Queries", 0, -1);
                }

                //check if local user xml still contains deleted products
                // if contains one, delete it
                for (int i = rows.GetLength(0) - 1; i >= 0; i--)
                {
                    FoldersRow folder = (FoldersRow)rows[i];
                    if ((folder.ParentID == productsRootFolder.ID) && (products[folder.Name + "," + folder.Name] == null))
                        folder.Delete();
                }

                //check if new product is missing from the local user structure
                for (int i = 0; i < products.Count; i++)
                {
                    productName = products.GetKey(i).Split(',')[1];
                    FoldersRow productFolder = GetFolderByName(connectionRow, productName);

                    if (productFolder == null)
                    {
                        CreateProductFolderDefaultQueries(connectionRow, productsRootFolder.ID, productName);
                    }
                }

                rows = _instance.Folders.Select("UserID = " + Int16.Parse(nodeUser.Name.Replace("User", String.Empty)).ToString(), "LevelID, Name, ID, ParentID");

                //add each folder(except deleted ones) as a TreeNode in the TreeView
                foreach (DataRow dr in rows)
                {
                    FoldersRow folder = (FoldersRow)dr;
                    
                    //protect agains folders that do not have the Deleted tag (protect agains null value)
                    try
                    {
                        folder.Deleted = folder.Deleted;
                    }
                    catch {
                        folder.Deleted = false;
                    }

                    
                    if (!folder.Deleted)
                    {
                        if (folder.ParentID == -1)
                        {
                            node = nodeUser.Nodes.Add("folder " + folder.ID.ToString(), folder.Name, "Folder");
                        }
                        else
                        {
                            FoldersRow parentFolder = _instance.Folders.FindByID(folder.ParentID);

                            if (parentFolder.Deleted) {
                                folder.Deleted = true;
                            }

                            TreeNode[] nodes = nodeUser.Nodes.Find("folder " + folder.ParentID.ToString(), true);

                            if (nodes.GetLength(0) == 1)
                            {
                                node = nodes[0].Nodes.Add("folder " + folder.ID.ToString(), folder.Name, "Folder");
                            }
                        }

                        if (!folder.Deleted)
                        {
                            node.SelectedImageKey = "Folder";
                            node.Tag = new NodeDescription(NodeType.Folder, folder);

                            //add queries associated with the folder as tree nodes
                            DataRow[] queries = _instance.Queries.Select("FolderId = " + folder.ID);
                            foreach (DataRow query in queries)
                            {
                                QueriesRow queryRow = (QueriesRow)query;

                                AddQueryToTreeNode(node, queryRow);
                            }
                        }
                    }
                }

            }
        }

        private void AddQueryToTreeNode(TreeNode folderNode, TDSQueriesTree.QueriesRow queryRow)
        {

            TreeNode queryNode = new TreeNode();

            string queryName = String.Empty;

            string queryNodeLabelFormat = String.Concat(queryRow.Name, " ({0})");

            if (queryRow.BugsCount==-1)
                queryName = String.Format(queryNodeLabelFormat, '?');
            else
                queryName = String.Format(queryNodeLabelFormat, queryRow.BugsCount);

            queryNode = folderNode.Nodes.Add("query " + queryRow.ID.ToString(), queryName, "Query");
            queryNode.ToolTipText = queryRow.Description;

            queryNode.Tag = new NodeDescription(NodeType.Query, queryRow, queryNodeLabelFormat);
            queryNode.ImageIndex = 1;
            queryNode.SelectedImageIndex = 1;

        }

        public void AddQueryToTree(TreeView treeView, TDSQueriesTree.QueriesRow queryRow)
        {
            TreeNode[] folderNodes = treeView.Nodes.Find("folder " + queryRow.FolderID, true);
            if (folderNodes.GetLength(0) == 1)
            {
                AddQueryToTreeNode(folderNodes[0], queryRow);
                folderNodes[0].Expand();
            }
        }

        public int GetConnectionForFolder (int folderId)
        {
            int connectionID = -1;
           FoldersRow[] folders = _instance.Folders.Select("ID = " + folderId) as FoldersRow [];

           if (folders.Length == 1)
           {
               connectionID = folders[0].UserID;
           }

           return connectionID;
             
        }

        public int GetQueryCountForConnectionId(int connectionId)
        {
            int count = 0;

            FoldersRow[] folders = _instance.Folders.Select("UserId = " + connectionId) as FoldersRow[];

            foreach(FoldersRow folder in folders){
                count += _instance.Queries.Select(String.Format("FolderId = {0}", folder.ID)).Length;
            }

            return count;

        }

    }
}
