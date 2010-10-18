namespace MyZilla.UI
{
    partial class FormQueryName
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormQueryName));
            this.lblFolder = new System.Windows.Forms.Label();
            this.lblQueryName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQueryName = new System.Windows.Forms.TextBox();
            this.txtQueryDescription = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tvFolders = new System.Windows.Forms.TreeView();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtConnection = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.Location = new System.Drawing.Point(9, 43);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(39, 13);
            this.lblFolder.TabIndex = 0;
            this.lblFolder.Text = "Folder:";
            // 
            // lblQueryName
            // 
            this.lblQueryName.AutoSize = true;
            this.lblQueryName.Location = new System.Drawing.Point(9, 142);
            this.lblQueryName.Name = "lblQueryName";
            this.lblQueryName.Size = new System.Drawing.Size(67, 13);
            this.lblQueryName.TabIndex = 1;
            this.lblQueryName.Text = "Query name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 165);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Query description:";
            // 
            // txtQueryName
            // 
            this.txtQueryName.Location = new System.Drawing.Point(110, 139);
            this.txtQueryName.Name = "txtQueryName";
            this.txtQueryName.Size = new System.Drawing.Size(242, 20);
            this.txtQueryName.TabIndex = 4;
            this.txtQueryName.Validated += new System.EventHandler(this.txtQueryName_Validated);
            // 
            // txtQueryDescription
            // 
            this.txtQueryDescription.Location = new System.Drawing.Point(110, 165);
            this.txtQueryDescription.Multiline = true;
            this.txtQueryDescription.Name = "txtQueryDescription";
            this.txtQueryDescription.Size = new System.Drawing.Size(242, 63);
            this.txtQueryDescription.TabIndex = 5;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(195, 239);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(276, 239);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errProvider
            // 
            this.errProvider.ContainerControl = this;
            // 
            // tvFolders
            // 
            this.tvFolders.HideSelection = false;
            this.tvFolders.Location = new System.Drawing.Point(110, 43);
            this.tvFolders.Name = "tvFolders";
            this.tvFolders.Size = new System.Drawing.Size(242, 90);
            this.tvFolders.TabIndex = 8;
            this.tvFolders.Validated += new System.EventHandler(this.tvFolders_Validated);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(9, 19);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(64, 13);
            this.lblUser.TabIndex = 10;
            this.lblUser.Text = "Connection:";
            // 
            // txtConnection
            // 
            this.txtConnection.BackColor = System.Drawing.Color.LightGray;
            this.txtConnection.Location = new System.Drawing.Point(109, 16);
            this.txtConnection.Name = "txtConnection";
            this.txtConnection.ReadOnly = true;
            this.txtConnection.Size = new System.Drawing.Size(242, 20);
            this.txtConnection.TabIndex = 11;
            // 
            // FormQueryName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(364, 274);
            this.Controls.Add(this.txtConnection);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.tvFolders);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtQueryDescription);
            this.Controls.Add(this.txtQueryName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblQueryName);
            this.Controls.Add(this.lblFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormQueryName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Query details";
            this.Load += new System.EventHandler(this.FormQueryName_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormQueryName_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.errProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFolder;
        private System.Windows.Forms.Label lblQueryName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQueryName;
        private System.Windows.Forms.TextBox txtQueryDescription;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider errProvider;
        private System.Windows.Forms.TreeView tvFolders;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtConnection;
    }
}