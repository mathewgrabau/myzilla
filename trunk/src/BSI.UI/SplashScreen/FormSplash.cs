using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyZilla.UI
{
    public partial class FormSplash : Form,ISplashForm
    {

        public FormSplash()
        {
            InitializeComponent();
#if DEBUG
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow; 
#else
            this.FormBorderStyle = FormBorderStyle.None;
#endif
            
        }

        #region ISplashForm

        void ISplashForm.SetStatusInfo(string newStatusInfo)
        {
            lbStatusInfo.Text = newStatusInfo;
        }

        #endregion

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}