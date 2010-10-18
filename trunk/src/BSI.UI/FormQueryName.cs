using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Specialized;

using MyZilla.BusinessEntities;

namespace MyZilla.UI
{
    public partial class FormQueryName : Form
    {
        private int _connectionID;
        private TreeView _original;
        public TreeNode CurrentFolderNode;

        #region Properties

        public string QueryName
        {
            get { return txtQueryName.Text; }
            set
            {
                txtQueryName.Text = value;
            }
        }

        public string QueryDescription
        {
            get { return txtQueryDescription.Text; }
            set {
                txtQueryDescription.Text = value;
            }
        }

        public int FolderId
        {
            get { return ((ConfigItems.TDSQueriesTree.FoldersRow)((ConfigItems.NodeDescription)tvFolders.SelectedNode.Tag).NodeData).ID; }
        }

        public string FolderName
        {
            get { return ((ConfigItems.TDSQueriesTree.FoldersRow)((ConfigItems.NodeDescription)tvFolders.SelectedNode.Tag).NodeData).Name; }
        }

        #endregion

        #region Constructors

        public FormQueryName(int userId, TreeView treeView, TreeNode folderNode)
        {
            InitializeComponent();
            _connectionID = userId;
            _original = treeView;
            CurrentFolderNode = folderNode;

        }

        #endregion

        #region Private Methods

        private void LoadConnectionInfo()
        {
            MyZillaSettingsDataSet _appSettings = MyZillaSettingsDataSet.GetInstance();

            txtConnection.Text = _appSettings.GetConnectionInfo(_connectionID);

        }

        private void CopyTree() {
            TreeNode[] nodes = _original.Nodes.Find("User " + _connectionID.ToString(), false);
            TreeNode root;

            if (nodes.GetLength(0) != 1)
            {
                MessageBox.Show("Root Node not found for user id " + _connectionID.ToString());
                return;
            }
            else {
                root = nodes[0];
            }

            tvFolders.Nodes.Clear();
            //Add all root folders (and children) to the folders tree
            foreach (TreeNode node in root.Nodes) {
                TreeNode newNode = (TreeNode)node.Clone();
                tvFolders.Nodes.Add(newNode);
            }

            if(CurrentFolderNode!=null){
                ConfigItems.NodeDescription nodeDescription = CurrentFolderNode.Tag as ConfigItems.NodeDescription;

                TreeNode[] selected = tvFolders.Nodes.Find("folder " + ((ConfigItems.TDSQueriesTree.FoldersRow)nodeDescription.NodeData).ID, true);
                if (selected != null) {
                    selected[0].Expand();
                    tvFolders.SelectedNode = selected[0];
                    tvFolders.AutoScrollOffset = tvFolders.SelectedNode.Bounds.Location;
                }
            }
            
            //remove query nodes from the folders tree
            RemoveQueryNodes(tvFolders.Nodes);
        }

        private void RemoveQueryNodes(TreeNodeCollection nodes){
            int j = 0;
            int count = nodes.Count;
            for(int i=0; i < count; i++){
                if (((ConfigItems.NodeDescription)nodes[j].Tag).TreeNodeType == NodeType.Query)
                {
                    nodes[j].Remove();
                }
                else
                {
                    if (nodes[j].Nodes.Count>0) 
                        RemoveQueryNodes(nodes[j].Nodes);
                    j++;
                }
            }
        }



        private bool ValidateQueryName()
        {
            if (txtQueryName.Text.Length == 0)
            {
                errProvider.SetError(txtQueryName, Properties.Messages.QueryNameRequired);
                return false;
            }
            else
                return true;
        }

        private bool ValidateQueryFolder()
        {
            if (tvFolders.SelectedNode ==null)
            {
                errProvider.SetError(tvFolders, Properties.Messages.FolderRequired);
                return false;
            }
            else
                return true;
        }

        private bool ValidateQueryDescription()
        {
            if (txtQueryDescription.Text.Length == 0)
            {
                errProvider.SetError(txtQueryDescription, Properties.Messages.QueryDescriptionRequired);
                return false;
            }
            else
                return true;
        }

        #endregion

        #region Events

        private void FormQueryName_Load(object sender, EventArgs e)
        {
            LoadConnectionInfo();
            CopyTree();
        }

        private void FormQueryName_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != DialogResult.Cancel)
            {
                if (!ValidateQueryName())
                    e.Cancel = true;
                if (!ValidateQueryFolder() && tvFolders.Enabled)
                    e.Cancel = true;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtQueryName.Text.Length == 0 || (tvFolders.SelectedNode == null && tvFolders.Enabled==true))
                this.DialogResult = DialogResult.Retry;
            else
                this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.Cancel;
        }

        private void tvFolders_Validated(object sender, EventArgs e)
        {
            errProvider.Clear();
        }

        private void txtQueryName_Validated(object sender, EventArgs e)
        {
            errProvider.Clear();
        }

        #endregion

    }
}