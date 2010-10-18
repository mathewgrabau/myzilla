using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using Microsoft.Reporting.WinForms;
using BSI.BusinessEntities;

namespace BSI.UI
{
    /// <summary>
    /// This class customizes the ReportViewer control.
    /// Hide the ReportViewer control. Add a new toolstrip and implement the
    /// necessary method.
    /// </summary>
    public partial class FormReportView : Form
    {
        #region Form Properties
        private string formCaption;
        /// <summary>
        /// Formu Texti
        /// </summary>
        public string FormCaption
        {
            get
            {
                return formCaption;
            }
            set
            {
                formCaption = value;
            }
        }

        private string reportDisplayName;
        /// <summary>
        /// Rapor Gösterim Adý
        /// </summary>
        public string ReportDisplayName
        {
            get
            {
                return reportDisplayName;
            }
            set
            {
                reportDisplayName = value;
            }
        }
        #endregion Form Properties

        #region Variables

        private int totalPages = 0;

        ReportPageSettings reportPageSettings = null;

        PageSetupDialog pageSetupDialog = null;

        PageSettings pageSettings = null;

        #endregion

        #region Constructor

        public FormReportView()
        {
            InitializeComponent();

        }

        #endregion

        #region Public methods

        private void FormReportView_Load(object sender, EventArgs e)
        {


            this.Text = this.FormCaption;
            LocalReport l = repView.LocalReport;
            l.DisplayName = this.ReportDisplayName;

            // hide the ReportViewer toolbar.
            this.repView.ShowToolBar = false;

            this.repView.DocumentMapCollapsed = true;
            btnDocumentMap.Checked = !this.repView.DocumentMapCollapsed;
            btnLayout.Checked = false;
            btnFind.Enabled = false;
            btnFindNext.Enabled = false;

            totalPages = this.repView.LocalReport.GetTotalPages() + 1;

            this.repView.CurrentPage = 1;

            displayCurrentPage();

            reportPageSettings = repView.LocalReport.GetDefaultPageSettings();
            pageSetupDialog = new PageSetupDialog();
            pageSetupDialog.PrinterSettings = new PrinterSettings();
            pageSettings = new PageSettings(pageSetupDialog.PrinterSettings);
            pageSetupDialog.PageSettings = new PageSettings();
            pageSetupDialog.PageSettings = pageSettings;

            pageSetupDialog.PageSettings.Margins = repView.LocalReport.GetDefaultPageSettings().Margins;
            pageSetupDialog.PageSettings.PaperSize = repView.LocalReport.GetDefaultPageSettings().PaperSize;

            setupZoomCombo();

            this.repView.RefreshReport();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text.Trim().Length > 0)
            {
                btnFind.Enabled = true;
            }
            else
            {
                btnFind.Enabled = false;
            }
            btnFindNext.Enabled = false;
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            frmCmdFindNext();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Write necessarry code to launch help file", "Help", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshReport();
        }

        private void btnDocumentMap_CheckedChanged(object sender, EventArgs e)
        {
            documentMap();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            frmCmdFind();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            firstPage();
            displayCurrentPage();
        }

        private void btnMovePrevious_Click(object sender, EventArgs e)
        {
            previousPage();

            displayCurrentPage();
        }

        private void btnMoveNext_Click(object sender, EventArgs e)
        {
            nextPage();

            displayCurrentPage();
        }

        private void btnMoveLast_Click(object sender, EventArgs e)
        {
            lastPage();
            displayCurrentPage();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmCmdPrint();
        }

        private void btnLayout_CheckedChanged(object sender, EventArgs e)
        {
            frmCmdLayout();
        }

        private void btnPageSetup_Click(object sender, EventArgs e)
        {
            frmCmdPageSetup();
        }

        private void cboZoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            setZoom(cboZoom.SelectedIndex);
        }

        private void repView_ZoomChange(object sender, ZoomChangeEventArgs e)
        {

            if (e.ZoomMode == ZoomMode.Percent)
            {
                int cboZoomPercentIndex = getZoomPercentIndex(e.ZoomPercent);
                if (cboZoom.SelectedIndex != cboZoomPercentIndex)
                {
                    setZoom(cboZoomPercentIndex);
                }
            }
            if (e.ZoomMode == ZoomMode.PageWidth)
            {
                if (cboZoom.SelectedIndex != 8)
                {
                    setZoom(8);
                }
            }
            if (e.ZoomMode == ZoomMode.FullPage)
            {
                if (cboZoom.SelectedIndex != 7)
                {
                    setZoom(7);
                }
            }
        }

        private void repView_PageNavigation(object sender, PageNavigationEventArgs e)
        {
            txtCurrentPage.Text = e.NewPage.ToString() + "/" + totalPages.ToString().Trim();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            frmCmdExcel();
        }

        private void btnPdf_Click(object sender, EventArgs e)
        {
            frmCmdPdf();
        }


        #endregion

        #region Private methods

        private bool setupZoomCombo()
        {
            cboZoom.Items.AddRange(new object[] {
            "25%",
            "50%",
            "75%",
            "100%",
            "150%",
            "200%",
            "500%",
            "Whole Page%",
            "Page Width"}
            );
            cboZoom.Text = "100%";
            cboZoom.SelectedIndex = 3;
            return true;
        }

        private bool displayCurrentPage()
        {
            txtCurrentPage.Text = this.repView.CurrentPage.ToString().Trim() + "/" + totalPages.ToString().Trim();

            return true;
        }

        private bool frmCmdFind()
        {
            int found = this.repView.Find(txtSearch.Text, 1);
            if (found > 0)
            {
                btnFindNext.Enabled = true;
            }
            else
            {
                MessageBox.Show("Searched string not found", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnFindNext.Enabled = false;
            }
            return true;
        }

        private bool frmCmdFindNext()
        {
            int found = this.repView.FindNext();
            if (found < 1)
            {
                MessageBox.Show("Searched string not found", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return true;
        }

        private bool refreshReport()
        {
            this.repView.RefreshReport();
            return true;
        }

        private bool documentMap()
        {
            this.repView.DocumentMapCollapsed = !btnDocumentMap.Checked;
            return true;
        }

        private bool firstPage()
        {
            this.repView.CurrentPage = 1;

            return true;
        }

        private bool nextPage()
        {

            if (this.repView.CurrentPage + 1 <= this.totalPages)
            {
                this.repView.CurrentPage++;
            }

            return true;
        }

        private bool lastPage()
        {
            this.repView.CurrentPage = totalPages;

            return true;
        }

        private bool previousPage()
        {
            if (this.repView.CurrentPage > 1)
            {
                this.repView.CurrentPage--;
            }
            return true;
        }

        private bool frmCmdPrint()
        {
            this.repView.PrintDialog();
            return true;
        }

        private bool frmCmdExcel()
        {
            LocalReport lr = this.repView.LocalReport;
            string outputFile = this.ReportDisplayName + ".xls";
            File.Delete(outputFile);

            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension = "";

            byte[] bytes = lr.Render("Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            FileStream fs = new FileStream(outputFile, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p = new System.Diagnostics.Process();

            //this.p.Exited += new System.EventHandler(this.p_Exited);
            //p.EnableRaisingEvents = true;
            //p.SynchronizingObject = this;
            p.StartInfo.FileName = outputFile;
            p.Start();
            return true;
        }

        private bool frmCmdPdf()
        {
            LocalReport lr = this.repView.LocalReport;
            lr.ReportPath = this.repView.LocalReport.ReportPath;
            string outputFile = this.ReportDisplayName + ".pdf";
            File.Delete(outputFile);
            Warning[] warnings;
            string[] streamids;
            string mimeType = "";
            string encoding = "";
            string extension = "";

            string deviceInfo = "";

            byte[] bytes = lr.Render("PDF", deviceInfo, out mimeType, out encoding, out extension, out streamids, out warnings);

            FileStream fs = new FileStream(outputFile, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p = new System.Diagnostics.Process();

            //this.p.Exited += new System.EventHandler(this.p_Exited);
            //p.EnableRaisingEvents = true;
            //p.SynchronizingObject = this;
            p.StartInfo.FileName = outputFile;
            p.Start();
            return true;
        }

        private bool frmCmdLayout()
        {
            if (btnLayout.Checked)
            {
                this.repView.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                this.repView.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.FullPage;
                Application.DoEvents();
                setZoom(7);
                Application.DoEvents();
            }
            else
            {
                this.repView.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.Normal);
                this.repView.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                this.repView.ZoomPercent = 100;
                Application.DoEvents();
                setZoom(8);
                Application.DoEvents();
            }
            return true;
        }

        private bool setZoom(int index)
        {
            switch (cboZoom.SelectedIndex)
            {
                case 0:
                    this.repView.ZoomMode = ZoomMode.Percent;
                    this.repView.ZoomPercent = 25;
                    break;
                case 1:
                    this.repView.ZoomMode = ZoomMode.Percent;
                    this.repView.ZoomPercent = 50;
                    break;
                case 2:
                    this.repView.ZoomMode = ZoomMode.Percent;
                    this.repView.ZoomPercent = 75;
                    break;
                case 3:
                    this.repView.ZoomMode = ZoomMode.Percent;
                    this.repView.ZoomPercent = 100;
                    break;
                case 4:
                    this.repView.ZoomMode = ZoomMode.Percent;
                    this.repView.ZoomPercent = 150;
                    break;
                case 5:
                    this.repView.ZoomMode = ZoomMode.Percent;
                    this.repView.ZoomPercent = 200;
                    break;
                case 6:
                    this.repView.ZoomMode = ZoomMode.Percent;
                    this.repView.ZoomPercent = 500;
                    break;
                case 7:
                    this.repView.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.FullPage;
                    break;
                case 8:
                    this.repView.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
                    this.repView.ZoomPercent = 100;
                    break;
                default:
                    break;
            }
            return true;
        }

        private int getZoomPercentIndex(int percent)
        {
            int ret = 0;
            switch (percent)
            {
                case 25:
                    ret = 0;
                    break;
                case 50:
                    ret = 1;
                    break;
                case 75:
                    ret = 2;
                    break;
                case 100:
                    ret = 3;
                    break;
                case 150:
                    ret = 4;
                    break;
                case 200:
                    ret = 5;
                    break;
                case 500:
                    ret = 6;
                    break;
                default:
                    break;
            }
            return ret;
        }

        private bool frmCmdPageSetup()
        {
            string xmlFile = getProjectSourcePath() + @"\Report1.rdlc";
            int leftMargin = 0;
            int rightMargin = 0;
            int topMargin = 0;
            int bottomMargin = 0;

            bool ret = getMargin(xmlFile, ref leftMargin, ref rightMargin, ref topMargin, ref bottomMargin);
            pageSetupDialog.PageSettings.Margins.Left = leftMargin ;
            pageSetupDialog.PageSettings.Margins.Right = rightMargin ;
            pageSetupDialog.PageSettings.Margins.Top = topMargin ;
            pageSetupDialog.PageSettings.Margins.Bottom = bottomMargin ;

            DialogResult dres = pageSetupDialog.ShowDialog();
            if (dres == DialogResult.OK)
            {
                ret = setMargin(xmlFile,
                    pageSetupDialog.PageSettings.Margins.Left,
                    pageSetupDialog.PageSettings.Margins.Right,
                    pageSetupDialog.PageSettings.Margins.Top,
                    pageSetupDialog.PageSettings.Margins.Bottom);
            }
            return true;
        }

        public bool getMargin(string Path, ref int leftMargin, ref int rightMargin, ref int topMargin, ref int bottomMargin)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load(Path);
                XmlNode xn = xml.DocumentElement;
                string tmp = "";
                tmp = xml.GetElementsByTagName("LeftMargin").Item(0).InnerText;
                tmp = tmp.Replace("mm", "");
                leftMargin = int.Parse(tmp) * 10;

                tmp = xml.GetElementsByTagName("RightMargin").Item(0).InnerText;
                tmp = tmp.Replace("mm", "");
                rightMargin = int.Parse(tmp) * 10;

                tmp = xml.GetElementsByTagName("TopMargin").Item(0).InnerText;
                tmp = tmp.Replace("mm", "");
                topMargin = int.Parse(tmp) * 10;

                tmp = xml.GetElementsByTagName("BottomMargin").Item(0).InnerText;
                tmp = tmp.Replace("mm", "");
                bottomMargin = int.Parse(tmp) * 10;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool setMargin(string Path, int leftMargin, int rightMargin, int topMargin, int bottomMargin)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                xml.Load(Path);
                XmlNode xn = xml.DocumentElement;
                //25,4    100(X) * 0.254
                //              
                double tmp = 0;
                tmp = leftMargin * 0.254;
                leftMargin = (int)tmp;
                xml.GetElementsByTagName("LeftMargin").Item(0).InnerText = leftMargin.ToString() + "mm";

                tmp = rightMargin * 0.254;
                rightMargin = (int)tmp;
                xml.GetElementsByTagName("RightMargin").Item(0).InnerText = rightMargin.ToString() + "mm";

                tmp = topMargin * 0.254;
                topMargin = (int)tmp;
                xml.GetElementsByTagName("TopMargin").Item(0).InnerText = topMargin.ToString() + "mm";


                tmp = bottomMargin * 0.254;
                bottomMargin = (int)tmp;
                xml.GetElementsByTagName("BottomMargin").Item(0).InnerText = bottomMargin.ToString() + "mm";
                xml.Save(Path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private string getProjectSourcePath()
        {
            string tmp = Application.StartupPath;
            tmp = tmp.Replace(@"\bin\Debug", "");
            tmp = tmp.Replace(@"\bin\Release", "");
            return tmp;
        }

        #endregion

    }
}