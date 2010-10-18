using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;

using MyZilla.BusinessEntities;
using MyZilla.UI.Properties;

using Tremend.Logging;

namespace MyZilla.UI
{
    public partial class UCBugStatus : UserControl
    {
        #region Variables

        private List<string> _catResolution;
        private MyZilla.BusinessEntities.Bug _bugToUpd;
        private Size ucSize = new Size(337, 145); 
        private List<string> assignedTo;
        private UCEditBug _ucEditBug;

        #endregion

        #region Constructors

        private UCBugStatus() { 
        }

        public UCBugStatus(List<string> resolutionList, MyZilla.BusinessEntities.Bug bugToUpdate, List<string >  assignedToList , UCEditBug ucEditBug   )
        {
            InitializeComponent();

            _catResolution = resolutionList.GetRange(0, resolutionList.Count);

            _bugToUpd = bugToUpdate;

            this.Size = ucSize;

            this.assignedTo = assignedToList;

            _ucEditBug = ucEditBug;
        }

        #endregion

        #region Form events

        private void UCBugStatus_Load(object sender, EventArgs e)
        {

            try
            {
                txtStatusResolution.Text = _bugToUpd.Status + " " + _bugToUpd.Resolution;

                _ucEditBug.BugResolution = _bugToUpd.Resolution;

                _ucEditBug.BugKnob = _bugToUpd.Knob;

                _ucEditBug.DuplicateBug = _bugToUpd.DuplicateBug;

                _ucEditBug.ReassignTo = _bugToUpd.ReassignTo; 

                switch (_bugToUpd.Resolution)
                {
                    case "":
                        switch (_bugToUpd.Status)
                        {
                            case "ASSIGNED":
                                pnlResolution.Location = rbTemp1.Location;
                                pnlDuplicate.Location = rbTemp2.Location;
                                pnlReassign.Location = rbTemp3.Location;
                                rb6.Location = rbTemp4.Location;
                                break;
                            default:
                                rb2.Location = rbTemp1.Location;
                                pnlResolution.Location = rbTemp2.Location;
                                pnlDuplicate.Location = rbTemp3.Location;
                                pnlReassign.Location = rbTemp4.Location;
                                rb6.Location = rbTemp5.Location;
                                break;
                        }
                        break;

                    default:
                        switch (_bugToUpd.Status)
                        {
                            case "VERIFIED":
                                rb7.Location = rbTemp1.Location;
                                rb9.Location = rbTemp2.Location;
                                break;
                            case "CLOSED":
                                rb7.Location = rbTemp1.Location;
                                break;
                            default:
                                rb7.Location = rbTemp1.Location;
                                rb8.Location = rbTemp2.Location;
                                rb9.Location = rbTemp3.Location;
                                break;

                        }
                        break;
                }

                this.Controls.Remove(rbTemp1);
                this.Controls.Remove(rbTemp2);
                this.Controls.Remove(rbTemp3);
                this.Controls.Remove(rbTemp4);
                this.Controls.Remove(rbTemp5);

                cmbResolution.SelectedIndexChanged -= new EventHandler(cmbResolution_SelectedIndexChanged);

                string bugStatus = _bugToUpd.Status.ToUpper();

                if (bugStatus == "NEW" || bugStatus == "REOPENED" || bugStatus == "ASSIGNED")
                {
                    for (int i = 0; i < _catResolution.Count;i++)
                    {
                        if (_catResolution[i].StartsWith("DUPLICATE,"))
                        {
                            _catResolution.RemoveAt(i--);
                        }
                        else if (_catResolution[i].StartsWith("MOVED,"))
                        {
                            _catResolution.RemoveAt(i--);
                        }
                        else if (_catResolution[i].StartsWith("-"))
                        {
                            _catResolution.RemoveAt(i--);
                        }
                    }
                }
                Utils.PopulateComboBox(cmbResolution, _catResolution);
                

                cmbResolution.SelectedIndexChanged += new EventHandler(cmbResolution_SelectedIndexChanged);

                cmbReasignTo.TextChanged -= new EventHandler(cmbReasignTo_TextChanged);
                Utils.PopulateComboBox(cmbReasignTo, assignedTo);
                cmbReasignTo.TextChanged += new EventHandler(cmbReasignTo_TextChanged);
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "UCBugStatus_Load", LoggingCategory.Exception); 

                MessageBox.Show(this, ex.Message, Messages.Error , MessageBoxButtons.OK, MessageBoxIcon.Error     ); 
            }


        }

        private void cmbResolution_SelectedIndexChanged(object sender, EventArgs e)
        {

            rb3.Checked = true;

            _ucEditBug.BugKnob = "resolve";
            _ucEditBug.BugResolution = cmbResolution.Text; 

        }

        private void rb7_CheckedChanged(object sender, EventArgs e)
        {
            if (rb7.Checked == true)
            {
                UnCheckControls(rb7);

                _ucEditBug.BugKnob = "reopen";
                _ucEditBug.BugResolution = ""; 
            }
        }

        private void rb1_CheckedChanged(object sender, EventArgs e)
        {

            if (rb1.Checked == true)
            {
                UnCheckControls(rb1);

                _ucEditBug.BugKnob = "none";
                _ucEditBug.BugResolution = ""; 
            }
        }

        private void rb3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb3.Checked == true)
            {
                UnCheckControls(rb3);

                cmbResolution_SelectedIndexChanged(null, null);
            }


        }

        private void rb4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb4.Checked == true)
            {
                UnCheckControls(rb4);

                _ucEditBug.BugKnob = "duplicate";
                _ucEditBug.DuplicateBug = txtDuplicateBug.Text; 
            }
        }

        private void rb5_CheckedChanged(object sender, EventArgs e)
        {
            if (rb5.Checked == true)
            {
                UnCheckControls(rb5);

                cmbReasignTo_TextChanged(null, null);
            }
       }

        private void rb2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2.Checked == true)
            {
                UnCheckControls(rb2);
            }


            _ucEditBug.BugKnob = "accept";
            _ucEditBug.BugResolution = "FIXED"; 
        }

        private void rb6_CheckedChanged(object sender, EventArgs e)
        {
            if (rb6.Checked == true)
            {
                UnCheckControls(rb6);
            }

            _ucEditBug.BugKnob = "reassignbycomponent";

            _ucEditBug.BugResolution = "FIXED"; 

        }

        private void rb8_CheckedChanged(object sender, EventArgs e)
        {
            if (rb8.Checked == true)
            {
                UnCheckControls(rb8);

                _ucEditBug.BugKnob = "verify"; 
            }

        }

        private void rb9_CheckedChanged(object sender, EventArgs e)
        {
            if (rb9.Checked == true)
            {
                UnCheckControls(rb9);
            }

            _ucEditBug.BugKnob = "close";

        }

        private void cmbReasignTo_TextChanged(object sender, EventArgs e)
        {
            rb5.Checked = true;

            _ucEditBug.BugKnob = "reassign";
            _ucEditBug.ReassignTo = cmbReasignTo.Text;  

        }

        private void txtDuplicateBug_TextChanged(object sender, EventArgs e)
        {
            _ucEditBug.DuplicateBug = txtDuplicateBug.Text; 
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Simulate option group functionality.
        /// </summary>
        /// <param name="chkBox"></param>
        private void UnCheckControls(RadioButton rb)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is RadioButton && ctrl != rb)
                {
                    (ctrl as RadioButton).Checked = false;
                }

                if (ctrl is Panel)
                {
                    foreach (Control c in ctrl.Controls)
                    {
                        if (c is RadioButton && c != rb)
                        {
                            (c as RadioButton).Checked = false;
                        }

                    }
                }
            }

            if (rb != rb5)
            {
                _ucEditBug.ReassignTo = string.Empty;
            }
            if (rb != rb4)
            {
                _ucEditBug.DuplicateBug = string.Empty; 
            }

        }

        #endregion

    }

    public struct BugStatusTransition {
        public string Resolution;

        public string Knob;

        public string DuplicateBug;

        public string ReassignTo;
    }
}
