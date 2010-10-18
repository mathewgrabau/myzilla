namespace MyZilla.UI
{
    partial class FormStatus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStatus));
            this.btnRunBackground = new System.Windows.Forms.Button();
            this.btnDetails = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pbCurrent = new System.Windows.Forms.ProgressBar();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.pnlCommands = new System.Windows.Forms.Panel();
            this.pnlDetails = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlCommands.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRunBackground
            // 
            this.btnRunBackground.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRunBackground.Location = new System.Drawing.Point(190, 3);
            this.btnRunBackground.Name = "btnRunBackground";
            this.btnRunBackground.Size = new System.Drawing.Size(135, 23);
            this.btnRunBackground.TabIndex = 1;
            this.btnRunBackground.Text = "Run in background";
            this.btnRunBackground.UseVisualStyleBackColor = true;
            this.btnRunBackground.Click += new System.EventHandler(this.btnRunBackground_Click);
            // 
            // btnDetails
            // 
            this.btnDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetails.Location = new System.Drawing.Point(340, 3);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(75, 23);
            this.btnDetails.TabIndex = 4;
            this.btnDetails.Text = "[btnDetails]";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(13, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 44);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Operation in progress ...";
            // 
            // pbCurrent
            // 
            this.pbCurrent.Location = new System.Drawing.Point(13, 62);
            this.pbCurrent.Name = "pbCurrent";
            this.pbCurrent.Size = new System.Drawing.Size(418, 17);
            this.pbCurrent.TabIndex = 8;
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.Location = new System.Drawing.Point(13, 86);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(43, 13);
            this.lblCurrent.TabIndex = 9;
            this.lblCurrent.Text = "[Status]";
            // 
            // pnlCommands
            // 
            this.pnlCommands.Controls.Add(this.btnRunBackground);
            this.pnlCommands.Controls.Add(this.btnDetails);
            this.pnlCommands.Location = new System.Drawing.Point(16, 110);
            this.pnlCommands.Name = "pnlCommands";
            this.pnlCommands.Size = new System.Drawing.Size(415, 27);
            this.pnlCommands.TabIndex = 10;
            // 
            // pnlDetails
            // 
            this.pnlDetails.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnlDetails.Location = new System.Drawing.Point(16, 149);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(415, 198);
            this.pnlDetails.TabIndex = 11;
            // 
            // FormStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(445, 365);
            this.ControlBox = false;
            this.Controls.Add(this.pnlDetails);
            this.Controls.Add(this.pnlCommands);
            this.Controls.Add(this.lblCurrent);
            this.Controls.Add(this.pbCurrent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormStatus";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Status ";
            this.Load += new System.EventHandler(this.FormStatus_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormStatus_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormStatus_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlCommands.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRunBackground;
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar pbCurrent;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.Panel pnlCommands;
        private System.Windows.Forms.Panel pnlDetails;
    }
}