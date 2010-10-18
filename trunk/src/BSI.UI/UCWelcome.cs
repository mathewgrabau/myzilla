using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MyZilla.BusinessEntities ;

namespace MyZilla.UI
{
    public partial class UCWelcome : UserControl
    {
        #region Variables

        private const string MYZILLA_HOME_PAGE = "www.myzilla.ro";

        #endregion

        #region Constructors

        public UCWelcome()
        {
            if (!this.DesignMode)
            {
                InitializeComponent();
            }
            //tableLayoutPanel1.RowStyles[0].SizeType = SizeType.AutoSize;
            //tableLayoutPanel1.RowStyles[1].SizeType = SizeType.AutoSize;
            //tableLayoutPanel1.RowStyles[2].SizeType = SizeType.AutoSize;

            //string strMessage2 = "You can browse your queries in the left panel. You can customize your tree by creating or deleting folders (right click" 
            //            + "on a query to inspect all actions). Query details can be changed, as well as the query criteria. "
            //            + "Use the buttons on the toolbar for quick search by either ID or custom string. Use the Advanced Search button from the toolbar or from the menu to create more complex queries."
            //            + Environment.NewLine + Environment.NewLine 
            //            + "In the search results you can reorder the columns by simply dragging them. Your settings will automatically be saved." 
            //            +  Environment.NewLine + 
            //            "You can also customize your columns using the Change Columns button on top."
            //            + Environment.NewLine + Environment.NewLine 
            //            + "You can edit several bugs at one time. Simply select bugs using Shift or Ctrl and right click on the group of bugs."
            //            + Environment.NewLine + Environment.NewLine 
            //            + "Double clicking one bug will open up the edit bug window. You can also preview bug details by moving through the grid using your mouse or your keyboard. ";

             //label5.Text = strMessage2;


        }

        #endregion

        #region Form Events


        private void lnkAddConnection_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormConnectionSettings frm = new FormConnectionSettings();

            frm.ShowDialog();
        }

        #endregion


        private void pbMyZilla_Click(object sender, EventArgs e)
        {
            //open myZilla home page MYZILLA_HOME_PAGE
            System.Diagnostics.Process.Start(MYZILLA_HOME_PAGE);
        }

        private void lnkNewConnection_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Utils.OpenNewConnectionWindow();
        }

        private void lnkNewBug_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.ParentForm.GetType() == typeof(MainForm))
                Utils.OpenNewBugWindow(((MainForm)this.ParentForm).AddBugForm);
        }

        private void lnkSearchBug_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.ParentForm.GetType() == typeof(MainForm))
                Utils.OpenNewAdvancedSearchTab(((MainForm)this.ParentForm));
        }
    }
}
