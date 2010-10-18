using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace BSI.UI
{
    public partial class UCComment : UserControl
    {
        public UCComment(string UserName, string Date, string Comment)
        {
            InitializeComponent();

            lblUserName.Text = UserName;
            lblDate.Text = Date;
            lblComment.Text = Utils.ReplaceNewLines(Comment);

            tableLayoutPanel1.Controls.Add(lblUserName);
            tableLayoutPanel1.Controls.Add(lblDate);
            tableLayoutPanel1.Controls.Add(lblComment);

            lblComment.Dock = DockStyle.Fill;
            lblComment.AutoSize = true;
            //lblComment.Height = lblComment.Lines.GetLength(0) * 19;
            //lblComment.ReadOnly = true;
            //lblComment.BackColor = Color.White;
            //find control in row 2 column 1 (in tableLayoutPanel1)
            Control c = this.tableLayoutPanel1.GetControlFromPosition(0, 1);

            //set colspan = 2
            tableLayoutPanel1.SetColumnSpan(c, 2);
            
            lblComment.Text = Utils.ReplaceNewLines(Comment);
        }

        public UCComment()
        {
            InitializeComponent();
        }
    }
}
