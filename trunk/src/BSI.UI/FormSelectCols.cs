using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Collections;

using Tremend.Logging;

using MyZilla.BusinessEntities;

namespace MyZilla.UI
{
    public partial class FormSelectCols : Form
    {
        string _columnList;
        string _columnListAll;
        //bool _hasUnsavedData;
        int connectionId;

        MyZillaSettingsDataSet _appSettings = MyZillaSettingsDataSet.GetInstance();


        FormSelectCols()
        {
            InitializeComponent();
        }

        public FormSelectCols(int connectionId)
        {
            InitializeComponent();
            this.connectionId = connectionId;
        }

        private void FormSelectCols_Load(object sender, EventArgs e)
        {
            string[] allCols = null;
            CheckBox chk;
            string[] chkProperties;

            try
            {
                allCols = Utils.GetAllColumnsListFromCookie(this.connectionId);

                if (allCols == null)
                {
                    MessageBox.Show(Properties.Messages.BugzillaListCookieFailedToGenerate);
                    this.Close();
                }

                foreach (string column in allCols)
                {
                    chkProperties = column.Split('&');
                    chk = new CheckBox();
                    chk.AutoSize = true;
                    chk.Text = chkProperties[0];
                    chk.Name = chkProperties[1];
                    chk.Tag = (chkProperties[2] == "1" ? true : false);
                    DataRow[] rows = _appSettings.Columns.Select("ConnectionID = " + this.connectionId + " AND Name = '" + chk.Name + "'");

                    //read value from settings (checked or not)
                    //chk.Checked = (chkProperties[2] == "1" ? true : false);
                    if (rows.Length == 1)
                    {
                        chk.Checked = ((MyZillaSettingsDataSet.ColumnsRow)rows[0]).Visible;
                    }

                    tablePanel.Controls.Add(chk);
                }

                //groupColumns.Height = tablePanel.Height + btnSave.Height * 2;
                this.Height = tablePanel.Height + (int)(btnSave.Height * 4.5);
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "FormSelectCols_Load", LoggingCategory.Exception);
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            //CheckForUnsavedData();
            //if (_hasUnsavedData)
            //{
            //    DialogResult res = MessageBox.Show(Properties.Messages.ContinueWithoutSavingData, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //    if (res == DialogResult.Yes)
            //        this.Close();
            //}
            //else

            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BuildNewCookieValue();

            Utils.AddCookie("COLUMNLIST", _columnList, this.connectionId);
            //Utils.AddCookie("COLUMNLISTALL", _columnListAll, _connectionID);

            //_hasUnsavedData = false;

            this.DialogResult = DialogResult.OK;

            this.Close();
        }

        private void BuildNewCookieValue()
        {
            _columnList = String.Empty;
            _columnListAll = String.Empty;
            char ctrlValue;

            //_appSettings.Columns.Clear();

            foreach (Control ctrl in tablePanel.Controls)
            {
                CheckBox chk = (CheckBox)ctrl;
                bool visible = false;

                if (ctrl.Name == "bug_severity")
                {
                    if (chk.Checked)
                    {
                        ctrlValue = '1';
                        visible = true;
                    }
                    else
                    {
                        ctrlValue = '2';
                    }
                }
                else
                {
                    if (chk.Checked)
                        visible = true;


                    ctrlValue = (chk.Checked ? '1' : '0');
                }
                DataRow[] rows = _appSettings.Columns.Select("ConnectionID = " + this.connectionId + " AND Name = '" + chk.Name + "'");

                if (rows.GetLength(0) == 1)
                {
                    MyZillaSettingsDataSet.ColumnsRow r = (MyZillaSettingsDataSet.ColumnsRow)rows[0];
                    if (ctrl.Name == "bug_severity")
                    {
                        r.Visible = visible;
                        r.DisplayIndex = -1;
                    }
                    else
                        if (!chk.Checked)
                        {
                            rows[0].Delete();
                        }
                }
                else if (chk.Checked)
                    _appSettings.Columns.AddColumnsRow(this.connectionId, chk.Name, chk.Text, -1, 0, 0, -1, visible);

                _columnListAll += ctrl.Text + "&" + ctrl.Name.ToString() + "&" + ctrlValue + "@";

                if (ctrlValue != '0')
                    _columnList += ctrl.Name.ToString() + "%20";

                //reset initial value
                chk.Tag = chk.Checked;

            }

        }
    }
}