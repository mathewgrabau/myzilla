using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using MyZilla.BusinessEntities;

namespace MyZilla.UI
{
    public partial class FormAbout : Form
    {
        private string _assemblyTitle = string.Empty;
        private string _assemblyVersion = string.Empty;
        private string _assemblyCopyright = string.Empty;
        private string _assemblyCompany = string.Empty;

        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            this.GetAssemblyAttributes ();

            this.Text = _assemblyTitle;

            ApplicationVersion appVersion = new ApplicationVersion(_assemblyVersion);

            labelVersion.Text += appVersion.ToString();

            lnkCompanyName.Text  = _assemblyCompany;

            labelCopyright.Text = _assemblyCopyright;

        }

        private void GetAssemblyAttributes ()
        {
               // assembly title
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

                if (attributes.Length > 0)
                {
                    _assemblyTitle = ((AssemblyTitleAttribute)attributes[0]).Title;
                }
                else
                {
                    // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                    _assemblyTitle = System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);

                }

                // assembly copyright
                attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

                if (attributes.Length > 0)
                {
                    _assemblyCopyright   = ((AssemblyCopyrightAttribute)attributes[0]).Copyright ;
                }

                // assembly company
                attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);

                if (attributes.Length > 0)
                {
                    _assemblyCompany  = ((AssemblyCompanyAttribute)attributes[0]).Company ;
                }

                string appVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

                _assemblyVersion = appVersion.Remove(appVersion.Length-2);


            }

        private void labelCompanyName_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(lnkCompanyName.Tag.ToString () );

        }

        private void FormAbout_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
                
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pbMyZilla_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(System.Configuration.ConfigurationManager.AppSettings["MyZillaWebSite"]);
        }

        }

    }

