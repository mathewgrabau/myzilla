using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Specialized;

using MyZilla.BusinessEntities;
using MyZilla.BL.Interfaces;
using MyZilla.UI.Properties;

using Tremend.Logging;

namespace MyZilla.UI
{
    public partial class UCAdvancedSearchMobile : UserControl
    {


        #region Variables

        private int _userId;

        private NameValueCollection _searchParam;

        private CatalogueManager _catalogues = CatalogueManager.Instance( );

        #endregion

        #region Events

        public event EventHandler PressKeyEnterEvent;

        #endregion

        #region Constructors

        public UCAdvancedSearchMobile()
        {
            if (!this.DesignMode)
            {
                InitializeComponent();
            }
        }

        public UCAdvancedSearchMobile(NameValueCollection searchConfiguration, int userId)
        {
            _searchParam = searchConfiguration;
            _userId = userId;
            InitializeComponent();
        }

        #endregion

        #region Form events

        private void ucAdvancedSearch_Load(object sender, EventArgs e)
        {
            Utils.PopulateListBox(listboxProduct, _catalogues.GetCataloguesForConnection (_userId).catalogueProduct);
            listboxProduct.ClearSelected();           

            // get all the catalogues dependent of the product catalogue

            Utils.PopulateListBox(listboxStatus, _catalogues.GetCataloguesForConnection(_userId).catalogueStatus);
            listboxStatus.ClearSelected();
            //listboxStatus.SelectedIndex = 1;
            //listboxStatus.SelectedIndex = 2;
            //listboxStatus.SelectedIndex = 3;
            //listboxStatus.SelectedIndex = 4;

            Utils.PopulateListBox(listboxResolution, _catalogues.GetCataloguesForConnection(_userId).catalogueResolution);
            listboxResolution.ClearSelected();

            Utils.PopulateListBox(listboxSeverity, _catalogues.GetCataloguesForConnection(_userId).catalogueSeverity);
            listboxSeverity.ClearSelected();

            Utils.PopulateListBox(listboxPriority, _catalogues.GetCataloguesForConnection(_userId).cataloguePriority);
            listboxPriority.ClearSelected();

            Utils.PopulateListBox(listboxHardware, _catalogues.GetCataloguesForConnection(_userId).catalogueHardware);
            listboxHardware.ClearSelected();

            Utils.PopulateListBox(listboxOS, _catalogues.GetCataloguesForConnection(_userId).catalogueOS);
            listboxOS.ClearSelected();

            Utils.PopulateComboBox(cmbSummary, _catalogues.GetCataloguesForConnection(_userId).catalogueStringOperators);

            Utils.PopulateComboBox(cmbComment, _catalogues.GetCataloguesForConnection(_userId).catalogueStringOperators);
            //cmbComment.SelectedIndex = 2;

            Utils.PopulateComboBox(cmbURL, _catalogues.GetCataloguesForConnection(_userId).catalogueStringOperators);

            Utils.PopulateComboBox(cmbWhiteboard, _catalogues.GetCataloguesForConnection(_userId).catalogueStringOperators);

            Utils.PopulateComboBox(cmbGeneralField, _catalogues.GetCataloguesForConnection(_userId).catalogueFields);

            Utils.PopulateComboBox(cmbGeneralOperator, _catalogues.GetCataloguesForConnection(_userId).catalogueOperators);


            SetUISearchCriteriaFromUI();
        }
        
        /// <summary>
        /// Populate the dependent listboxes (Components, Version, Targets)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listboxProduct_SelectedValueChanged(object sender, EventArgs e)
        {
            List<string> lstComponent = new List<string>();

            List<string> lstVersion = new List<string>();

            List<string> products = new List<string>();

            if ((listboxProduct.Items.Count > 0) && (listboxProduct.SelectedValue != null))
            {
                foreach (Utils.CatalogueItem objProduct in listboxProduct.SelectedItems)
                {
                    products.Add(objProduct.CatValue);
                }

                bool noComponentItemSelected = (listboxComponent.SelectedItems.Count == 0);
                bool noVersionItemSelected = (listboxVersion.SelectedItems.Count == 0);


                lstComponent = Utils.GetCatalogueForDependency(_catalogues.GetCataloguesForConnection(_userId).catalogueComponent, products.ToArray());

                lstVersion = Utils.GetCatalogueForDependency(_catalogues.GetCataloguesForConnection(_userId).catalogueVersion, products.ToArray());

                Utils.PopulateListBox(listboxComponent, lstComponent);

                Utils.PopulateListBox(listboxVersion, lstVersion);

                if (noComponentItemSelected)
                    listboxComponent.ClearSelected();

                if (noVersionItemSelected)
                    listboxVersion.ClearSelected();

            }
            else
            {
                // no product selected
                listboxComponent.DataSource = null;

                listboxVersion.DataSource = null;
            }
        }

        private void UCAdvancedSearchMobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.PressKeyEnterEvent != null)
                {
                    this.PressKeyEnterEvent(null, new EventArgs());
                }
            }

        }

        #endregion

        #region Private methods

        public NameValueCollection GetSearchCriteria()
        {
            NameValueCollection searchParams = new NameValueCollection();

            searchParams.Add("query_format", "advanced");

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.GetType().ToString()=="System.Windows.Forms.GroupBox")
                    foreach (Control innerCtrl in ctrl.Controls) {
                        ExtractCriteriaFromUIControl(searchParams, innerCtrl);
                    }
                else
                    ExtractCriteriaFromUIControl(searchParams, ctrl);
            }

            return searchParams;
        }

        private static void ExtractCriteriaFromUIControl(NameValueCollection searchConfiguration, Control control)
        {
            if (control.Tag != null && control.Tag.ToString().Length > 0)
                switch (control.GetType().ToString())
                {
                    case "System.Windows.Forms.Label":
                        //searchParams.Add(ctrl.Tag.ToString(), ctrl.Text);
                        //break;
                    //case typeof(System.Windows.Forms.TextBox).ToString():
                    case "System.Windows.Forms.TextBox":
                        searchConfiguration.Add(control.Tag.ToString(), control.Text);
                        break;
                    case "System.Windows.Forms.DateTimePicker":
                        DateTimePicker dtpCtrl = (DateTimePicker)control;
                        if (dtpCtrl!=null && dtpCtrl.Checked)
                            searchConfiguration.Add(dtpCtrl.Tag.ToString(), dtpCtrl.Value.ToString("yyyy-MM-dd"));
                        break;
                    //case typeof(System.Windows.Forms.ComboBox).ToString():
                    case "System.Windows.Forms.ComboBox":
                        searchConfiguration.Add(control.Tag.ToString(), ((ComboBox)control).SelectedValue.ToString());
                        break;
                    //case typeof(System.Windows.Forms.ListBox).ToString():
                    case "System.Windows.Forms.ListBox":
                        ListBox lb = (System.Windows.Forms.ListBox)(control);
                        for (int i = 0; i < lb.SelectedItems.Count; i++)
                            searchConfiguration.Add(control.Tag.ToString(), ((MyZilla.UI.Utils.CatalogueItem)lb.SelectedItems[i]).CatText);
                        break;
                }
        }

        public void SetUISearchCriteriaFromUI()
        {
            Hashtable htControls = new Hashtable();

            #region create a hash of controls and tag(parameter name)
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.Tag != null)
                {
                    htControls.Add(ctrl.Tag.ToString(), ctrl.Name);
                    switch (ctrl.GetType().ToString())
                    {
                        case "System.Windows.Forms.ListBox":
                            ((ListBox)ctrl).ClearSelected();
                            break;
                    }
                }
                else {
                    switch (ctrl.GetType().ToString())
                    {
                        case "System.Windows.Forms.GroupBox":
                            foreach (Control innerCtrl in ctrl.Controls)
                                if (innerCtrl.Tag != null)
                                {
                                    htControls.Add(innerCtrl.Tag.ToString(), innerCtrl.Name);

                                    if (innerCtrl.GetType().ToString() == "System.Windows.Forms.ListBox")
                                        ((ListBox)innerCtrl).ClearSelected();
                                }
                            break;
                        
                    }
                }
            }
            #endregion

            for (int i = 0; i < _searchParam.Count; i++)
            {
                if (htControls[_searchParam.GetKey(i)] != null)
                {
                    Control[] ctrl = this.Controls.Find(htControls[_searchParam.GetKey(i)].ToString(), true);
                    if (ctrl[0].GetType() == typeof(Label))
                    {
                        ((Label)ctrl[0]).Text = _searchParam.GetValues(i)[0];
                    }else if (ctrl[0].GetType() == typeof(TextBox))
                    {
                        ((TextBox)ctrl[0]).Text = _searchParam.GetValues(i)[0];
                    }
                    else if (ctrl[0].GetType() == typeof(ComboBox))
                    {
                        ((ComboBox)ctrl[0]).SelectedValue = _searchParam.GetValues(i)[0];
                    }
                    else if (ctrl[0].GetType() == typeof(DateTimePicker))
                    {
                        ((DateTimePicker)ctrl[0]).Value = DateTime.Parse(_searchParam.GetValues(i)[0]);
                    }
                    else if (ctrl[0].GetType() == typeof(ListBox))
                    {
                        ListBox lb = (ListBox)ctrl[0];

                        for (int j = 0; j < _searchParam.GetValues(i).GetLength(0); j++)
                        {
                            int pos = lb.FindStringExact(_searchParam.GetValues(i)[j]);
                            //if string was found in the list box
                            if (pos >= 0)
                                lb.SelectedItem = lb.Items[pos];
                        }

                    }
                }
                else
                {
                    TextBox txt;
                    if (!this.Controls.ContainsKey(_searchParam.GetKey(i)))
                    {
                        txt = new TextBox();
                        txt.Visible = false;
                        txt.Name = _searchParam.GetKey(i);
                        txt.Tag = _searchParam.GetKey(i);
                        
                        this.Controls.Add(txt);
                    }
                    else
                    {
                        txt = (TextBox)this.Controls.Find(_searchParam.GetKey(i), false)[0];
                    }
                    txt.Text = _searchParam.GetValues(i)[0];
                }
            }
        }

        #endregion

    }

}
