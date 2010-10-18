namespace BSI.UI
{
    partial class FormViewFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormViewFile));
            this.btnClose = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblContentType = new System.Windows.Forms.Label();
            this.txtContentType = new System.Windows.Forms.TextBox();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.grInfo = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.wbFile = new System.Windows.Forms.WebBrowser();
            this.grInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(786, 423);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(20, 23);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(100, 19);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(519, 20);
            this.txtDescription.TabIndex = 4;
            // 
            // lblContentType
            // 
            this.lblContentType.AutoSize = true;
            this.lblContentType.Location = new System.Drawing.Point(20, 49);
            this.lblContentType.Name = "lblContentType";
            this.lblContentType.Size = new System.Drawing.Size(71, 13);
            this.lblContentType.TabIndex = 5;
            this.lblContentType.Text = "Content Type";
            // 
            // txtContentType
            // 
            this.txtContentType.Location = new System.Drawing.Point(100, 45);
            this.txtContentType.Name = "txtContentType";
            this.txtContentType.ReadOnly = true;
            this.txtContentType.Size = new System.Drawing.Size(249, 20);
            this.txtContentType.TabIndex = 6;
            // 
            // txtComments
            // 
            this.txtComments.Location = new System.Drawing.Point(100, 71);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.ReadOnly = true;
            this.txtComments.Size = new System.Drawing.Size(519, 57);
            this.txtComments.TabIndex = 8;
            // 
            // grInfo
            // 
            this.grInfo.Controls.Add(this.label1);
            this.grInfo.Controls.Add(this.txtDescription);
            this.grInfo.Controls.Add(this.txtComments);
            this.grInfo.Controls.Add(this.lblDescription);
            this.grInfo.Controls.Add(this.txtContentType);
            this.grInfo.Controls.Add(this.lblContentType);
            this.grInfo.Location = new System.Drawing.Point(13, 12);
            this.grInfo.Name = "grInfo";
            this.grInfo.Size = new System.Drawing.Size(637, 140);
            this.grInfo.TabIndex = 9;
            this.grInfo.TabStop = false;
            this.grInfo.Text = "Attachment of bug ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Comments";
            // 
            // wbFile
            // 
            this.wbFile.AllowNavigation = false;
            this.wbFile.AllowWebBrowserDrop = false;
            this.wbFile.Location = new System.Drawing.Point(13, 158);
            this.wbFile.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbFile.Name = "wbFile";
            this.wbFile.Size = new System.Drawing.Size(848, 250);
            this.wbFile.TabIndex = 10;
            // 
            // FormViewFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(885, 464);
            this.Controls.Add(this.wbFile);
            this.Controls.Add(this.grInfo);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormViewFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Attachment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormViewFile_FormClosing);
            this.grInfo.ResumeLayout(false);
            this.grInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblContentType;
        private System.Windows.Forms.TextBox txtContentType;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.GroupBox grInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.WebBrowser wbFile;
    }
}