using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using MyZilla.UI.Properties;
using MyZilla.BusinessEntities;

using Tremend.Logging;  

namespace MyZilla.UI
{
    public partial class FormStatus : Form
    {
        #region Variables

        private Size formSizeMin = new Size(451, 171);

        private Size formSizeMax = new Size(451, 386);  

        private AsyncOperationManagerList asyncOpManager = AsyncOperationManagerList.GetInstance () ;

        private Point locPnlDetails;

        private Point locPnlCommand;

        private Color borderColor;

        #endregion

        #region Constructor


        public FormStatus()
        {
            InitializeComponent();

            this.Size = formSizeMin;

            locPnlCommand = pnlCommands.Location;

            locPnlDetails = pnlDetails.Location;  

            btnDetails.Text = Resources.Text_Details;

            asyncOpManager.RefreshAsyncOperationListEvent += new EventHandler(asyncOpManager_RefreshAsyncOpListEvent);

            (Utils.FormContainer as MainForm).statusForm = this;

            pictureBox1.Image  = System.Drawing.SystemIcons.Information.ToBitmap ();

        }

        void asyncOpManager_RefreshAsyncOpListEvent(object sender, EventArgs e)
        {
            RefreshStatusList();
        }

        #endregion

        #region Form events

        private void FormStatus_Load(object sender, EventArgs e)
        {
            try
            {
                if (asyncOpManager.Count == 0)
                {
                    lblCurrent.Text = Messages.NoOperation;

                    pbCurrent.Value = 0;

                }
                else
                {
                    // show the newer thread.

                    AsyncOperationStatus operation = asyncOpManager.GetLastAsyncOperation();

                    lblCurrent.Text = operation.Message;

                    pbCurrent.Value = operation.Percentage;

                }

                this.Location = new Point(Utils.FormContainer.Left + (Utils.FormContainer.Width - this.Width) / 2,
                                Utils.FormContainer.Top + (Utils.FormContainer.Height - this.Height) / 2);


                this.borderColor = Color.DeepSkyBlue;

                FormStatus.SetBorderColorForPanel(pnlDetails, borderColor);
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "FormStatus_Load", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }

        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnDetails.Text == Resources.Text_Details)
                {
                    // details

                    this.Size = formSizeMax;

                    pnlDetails.Location = locPnlCommand;

                    pnlCommands.Location = new Point(locPnlDetails.X, locPnlDetails.Y + pnlDetails.Size.Height - pnlCommands.Size.Height);

                    btnDetails.Text = Resources.Text_Hide;

                    RefreshStatusList();

                    FormStatus.SetBorderColorForPanel(pnlDetails, borderColor);

                }
                else
                {
                    // hide

                    this.Size = formSizeMin;

                    pnlCommands.Location = locPnlCommand;

                    pnlDetails.Location = locPnlDetails;

                    btnDetails.Text = Resources.Text_Details;
                }
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "btnDetails_Click", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }

       }

        private void btnRunBackground_Click(object sender, EventArgs e)
        {
             this.Hide();
        }

        public void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void FormStatus_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((Utils.FormContainer) as MainForm).statusForm = null;
        }


        #endregion

        #region Private methods

        private void RefreshStatusList()
        {

            if (asyncOpManager.Count == 0)
            {
                this.Close();
            }
            else
            {
                // show the newer thread.

                AsyncOperationStatus operation = asyncOpManager.GetLastAsyncOperation ();

                lblCurrent.Text = operation.Message;

                pbCurrent.Value = operation.Percentage;

            }
                // the list of details is shown.

                pnlDetails.Controls.Clear();

                foreach (AsyncOperationStatus op in asyncOpManager)
                {
                    UCProgressStatus pStatus = new UCProgressStatus(  );

                    pStatus.lblMessage.Text = op.Message;

                    pStatus.pbStatus.Value = op.Percentage;

                    pStatus.BackColor = SystemColors.ControlLightLight;  

                    if (asyncOpManager.Count  > 0)
                    {
                        pStatus.Location = new Point(2, pStatus.Size.Height * pnlDetails.Controls.Count + 2);
                    }

                    pnlDetails.Controls.Add(pStatus);

                }

                if (pnlDetails.Controls.Count > 0)
                {
                    pnlDetails.Controls[0].BackColor = Color.GhostWhite;   
                }





        }

        private static void SetBorderColorForPanel(Panel pnl, Color borderColor)
        {
            Graphics g = pnl.CreateGraphics();
            ControlPaint.DrawBorder(g, pnl.DisplayRectangle, borderColor, ButtonBorderStyle.Solid);
            g.Dispose();

        }

        #endregion

        private void FormStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Hide();
            }
        }




    }
}