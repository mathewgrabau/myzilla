namespace MyZilla.UI
{
    partial class UCProgressStatus
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblMessage = new System.Windows.Forms.Label();
            this.pbStatus = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(10, 31);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(35, 13);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "label1";
            // 
            // pbStatus
            // 
            this.pbStatus.Location = new System.Drawing.Point(10, 12);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(391, 12);
            this.pbStatus.TabIndex = 1;
            // 
            // UCProgressStatus
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.pbStatus);
            this.Controls.Add(this.lblMessage);
            this.Name = "UCProgressStatus";
            this.Size = new System.Drawing.Size(411, 57);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblMessage;
        public System.Windows.Forms.ProgressBar pbStatus;

    }
}
