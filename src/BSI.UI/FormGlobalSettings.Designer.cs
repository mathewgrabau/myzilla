namespace MyZilla.UI
{
    partial class FormGlobalSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGlobalSettings));
            this.chkShowLoadingForm = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkShowBugsCount = new System.Windows.Forms.CheckBox();
            this.chkEditConfirm = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkShowLoadingForm
            // 
            this.chkShowLoadingForm.AutoSize = true;
            this.chkShowLoadingForm.Location = new System.Drawing.Point(21, 19);
            this.chkShowLoadingForm.Name = "chkShowLoadingForm";
            this.chkShowLoadingForm.Size = new System.Drawing.Size(113, 17);
            this.chkShowLoadingForm.TabIndex = 0;
            this.chkShowLoadingForm.Text = "Show loading form";
            this.chkShowLoadingForm.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.txtPath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkShowBugsCount);
            this.groupBox1.Controls.Add(this.chkEditConfirm);
            this.groupBox1.Controls.Add(this.chkShowLoadingForm);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(466, 131);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preferences";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(391, 92);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(63, 23);
            this.btnBrowse.TabIndex = 8;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtPath
            // 
            this.txtPath.BackColor = System.Drawing.Color.LightGray;
            this.txtPath.Location = new System.Drawing.Point(112, 94);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(273, 20);
            this.txtPath.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Export files path:";
            // 
            // chkShowBugsCount
            // 
            this.chkShowBugsCount.AutoSize = true;
            this.chkShowBugsCount.Location = new System.Drawing.Point(21, 71);
            this.chkShowBugsCount.Name = "chkShowBugsCount";
            this.chkShowBugsCount.Size = new System.Drawing.Size(114, 17);
            this.chkShowBugsCount.TabIndex = 5;
            this.chkShowBugsCount.Text = "Load query counts";
            this.chkShowBugsCount.UseVisualStyleBackColor = true;
            // 
            // chkEditConfirm
            // 
            this.chkEditConfirm.AutoSize = true;
            this.chkEditConfirm.Location = new System.Drawing.Point(21, 45);
            this.chkEditConfirm.Name = "chkEditConfirm";
            this.chkEditConfirm.Size = new System.Drawing.Size(163, 17);
            this.chkEditConfirm.TabIndex = 4;
            this.chkEditConfirm.Text = "Show confirmation messages";
            this.chkEditConfirm.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(322, 149);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 26);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(403, 149);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 26);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FormGlobalSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(490, 187);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormGlobalSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Global settings";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormGlobalSettings_FormClosed);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormGlobalSettings_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkShowLoadingForm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkEditConfirm;
        private System.Windows.Forms.CheckBox chkShowBugsCount;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}