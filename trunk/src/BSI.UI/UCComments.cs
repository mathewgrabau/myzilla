using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MyZilla.UI
{
    public partial class UCComments : UserControl
    {
        List<string> _comments;

        [Browsable(true), Bindable(true), Description("Dummy text control"), DefaultValue("")]
        public Color BackgroundColor
        {
            get { return txtComments.BackColor; }
            set { txtComments.BackColor = value; }
        }
	
        public UCComments()
        {
            InitializeComponent();
        }

        public UCComments(List<string> commentList)
        {
            InitializeComponent();

            _comments = commentList;

        }

        private void UCComments_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode) {
                LoadComments(null);
            }

        }

        public void LoadComments(List<string> commentList)
        {
            _comments = commentList;
            if (_comments != null)
            {
                string UserName;
                string Date;
                string Comment;
                string[] res;

                String[] lines = new String[_comments.Count*4];

                //set comments
                for (int i = 1; i < _comments.Count; i++)
                {
                    res = _comments[i].Split(',');

                    if (res.GetLength(0) >= 3)
                    {
                        UserName = res[0];
                        Date = res[1];
                        Comment = _comments[i].Substring(_comments[i].IndexOf(Date) + Date.Length + 1);
                        lines[4 * (i - 1)] =  UserName;

                        lines[4 * (i - 1)] += "\t\t" + Date;

                        lines[4 * (i - 1) +1 ] += "\t\t";

                        lines[4 * (i - 1) + 2] = Utils.ReplaceNewLines(Comment);

                        lines[4 * (i - 1) + 3] += "\t\t";

                    }
                }
                txtComments.Lines = lines;
            }
        }

        public void ClearControl() {
            txtComments.Lines = null;
        }

    }
}
