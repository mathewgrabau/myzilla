namespace MyZilla.UI
{
    partial class FormAbout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.lnkCompanyName = new System.Windows.Forms.LinkLabel();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.tdsQueriesTree1 = new MyZilla.UI.ConfigItems.TDSQueriesTree();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbMyZilla = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.tdsQueriesTree1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMyZilla)).BeginInit();
            this.SuspendLayout();
            // 
            // labelCopyright
            // 
            this.labelCopyright.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCopyright.BackColor = System.Drawing.Color.Transparent;
            this.labelCopyright.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelCopyright.Location = new System.Drawing.Point(57, 178);
            this.labelCopyright.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelCopyright.MaximumSize = new System.Drawing.Size(270, 17);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(270, 17);
            this.labelCopyright.TabIndex = 37;
            this.labelCopyright.Text = "Copyright";
            this.labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelVersion
            // 
            this.labelVersion.BackColor = System.Drawing.Color.Transparent;
            this.labelVersion.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.labelVersion.Location = new System.Drawing.Point(60, 136);
            this.labelVersion.Margin = new System.Windows.Forms.Padding(6, 0, 3, 0);
            this.labelVersion.MaximumSize = new System.Drawing.Size(270, 17);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(262, 13);
            this.labelVersion.TabIndex = 35;
            this.labelVersion.Text = "Version: ";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lnkCompanyName
            // 
            this.lnkCompanyName.AutoSize = true;
            this.lnkCompanyName.BackColor = System.Drawing.Color.Transparent;
            this.lnkCompanyName.Location = new System.Drawing.Point(208, 157);
            this.lnkCompanyName.Name = "lnkCompanyName";
            this.lnkCompanyName.Size = new System.Drawing.Size(88, 13);
            this.lnkCompanyName.TabIndex = 38;
            this.lnkCompanyName.TabStop = true;
            this.lnkCompanyName.Tag = "http://www.tremend.ro/";
            this.lnkCompanyName.Text = "[Company Name]";
            this.lnkCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkCompanyName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labelCompanyName_LinkClicked);
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.BackColor = System.Drawing.Color.Transparent;
            this.lblCompanyName.Location = new System.Drawing.Point(119, 157);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(93, 13);
            this.lblCompanyName.TabIndex = 40;
            this.lblCompanyName.Text = "Brought to you by ";
            // 
            // tdsQueriesTree1
            // 
            this.tdsQueriesTree1.DataSetName = "QueryTreeDataSet";
            this.tdsQueriesTree1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(150, 203);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(82, 23);
            this.btnClose.TabIndex = 42;
            this.btnClose.Text = "OK";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::MyZilla.UI.Properties.Resources.myzilla_bg;
            this.panel1.Controls.Add(this.pbMyZilla);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(382, 151);
            this.panel1.TabIndex = 43;
            // 
            // pbMyZilla
            // 
            this.pbMyZilla.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbMyZilla.Image = global::MyZilla.UI.Properties.Resources.myzilla;
            this.pbMyZilla.Location = new System.Drawing.Point(60, 0);
            this.pbMyZilla.Name = "pbMyZilla";
            this.pbMyZilla.Size = new System.Drawing.Size(262, 151);
            this.pbMyZilla.TabIndex = 40;
            this.pbMyZilla.TabStop = false;
            this.pbMyZilla.Tag = "http://www.myzilla.ro/";
            this.pbMyZilla.Click += new System.EventHandler(this.pbMyZilla_Click);
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(213)))), ((int)(((byte)(255)))));
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(382, 239);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblCompanyName);
            this.Controls.Add(this.lnkCompanyName);
            this.Controls.Add(this.labelCopyright);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormAbout";
            this.Load += new System.EventHandler(this.FormAbout_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormAbout_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.tdsQueriesTree1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMyZilla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.LinkLabel lnkCompanyName;
        private System.Windows.Forms.Label lblCompanyName;
        private MyZilla.UI.ConfigItems.TDSQueriesTree tdsQueriesTree1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbMyZilla;
    }
}