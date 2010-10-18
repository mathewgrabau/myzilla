namespace MyZilla.UI
{
    partial class UCBugStatus
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
            this.rb9 = new System.Windows.Forms.RadioButton();
            this.rb8 = new System.Windows.Forms.RadioButton();
            this.rb7 = new System.Windows.Forms.RadioButton();
            this.txtStatusResolution = new System.Windows.Forms.TextBox();
            this.rb6 = new System.Windows.Forms.RadioButton();
            this.cmbReasignTo = new System.Windows.Forms.ComboBox();
            this.rb5 = new System.Windows.Forms.RadioButton();
            this.txtDuplicateBug = new System.Windows.Forms.TextBox();
            this.rb4 = new System.Windows.Forms.RadioButton();
            this.cmbResolution = new System.Windows.Forms.ComboBox();
            this.rb3 = new System.Windows.Forms.RadioButton();
            this.rb2 = new System.Windows.Forms.RadioButton();
            this.rb1 = new System.Windows.Forms.RadioButton();
            this.rbTemp1 = new System.Windows.Forms.RadioButton();
            this.rbTemp2 = new System.Windows.Forms.RadioButton();
            this.rbTemp3 = new System.Windows.Forms.RadioButton();
            this.rbTemp4 = new System.Windows.Forms.RadioButton();
            this.pnlResolution = new System.Windows.Forms.Panel();
            this.pnlReassign = new System.Windows.Forms.Panel();
            this.pnlDuplicate = new System.Windows.Forms.Panel();
            this.rbTemp5 = new System.Windows.Forms.RadioButton();
            this.pnlResolution.SuspendLayout();
            this.pnlReassign.SuspendLayout();
            this.pnlDuplicate.SuspendLayout();
            this.SuspendLayout();
            // 
            // rb9
            // 
            this.rb9.AutoSize = true;
            this.rb9.Location = new System.Drawing.Point(8, 404);
            this.rb9.Name = "rb9";
            this.rb9.Size = new System.Drawing.Size(130, 17);
            this.rb9.TabIndex = 14;
            this.rb9.TabStop = true;
            this.rb9.Text = "Mark bug as CLOSED";
            this.rb9.UseVisualStyleBackColor = true;
            this.rb9.CheckedChanged += new System.EventHandler(this.rb9_CheckedChanged);
            // 
            // rb8
            // 
            this.rb8.AutoSize = true;
            this.rb8.Location = new System.Drawing.Point(7, 378);
            this.rb8.Name = "rb8";
            this.rb8.Size = new System.Drawing.Size(136, 17);
            this.rb8.TabIndex = 13;
            this.rb8.TabStop = true;
            this.rb8.Text = "Mark bug as VERIFIED";
            this.rb8.UseVisualStyleBackColor = true;
            this.rb8.CheckedChanged += new System.EventHandler(this.rb8_CheckedChanged);
            // 
            // rb7
            // 
            this.rb7.AutoSize = true;
            this.rb7.Location = new System.Drawing.Point(7, 352);
            this.rb7.Name = "rb7";
            this.rb7.Size = new System.Drawing.Size(84, 17);
            this.rb7.TabIndex = 12;
            this.rb7.TabStop = true;
            this.rb7.Text = "Reopen bug";
            this.rb7.UseVisualStyleBackColor = true;
            this.rb7.CheckedChanged += new System.EventHandler(this.rb7_CheckedChanged);
            // 
            // txtStatusResolution
            // 
            this.txtStatusResolution.BackColor = System.Drawing.Color.LightGray; 
            this.txtStatusResolution.Location = new System.Drawing.Point(86, 4);
            this.txtStatusResolution.Name = "txtStatusResolution";
            this.txtStatusResolution.ReadOnly = true;
            this.txtStatusResolution.Size = new System.Drawing.Size(146, 20);
            this.txtStatusResolution.TabIndex = 1;
            // 
            // rb6
            // 
            this.rb6.AutoSize = true;
            this.rb6.Location = new System.Drawing.Point(7, 326);
            this.rb6.Name = "rb6";
            this.rb6.Size = new System.Drawing.Size(314, 17);
            this.rb6.TabIndex = 11;
            this.rb6.TabStop = true;
            this.rb6.Tag = "reassignbycomponent";
            this.rb6.Text = "Reassign bug to the default assignee of selected  component";
            this.rb6.UseVisualStyleBackColor = true;
            this.rb6.CheckedChanged += new System.EventHandler(this.rb6_CheckedChanged);
            // 
            // cmbReasignTo
            // 
            this.cmbReasignTo.FormattingEnabled = true;
            this.cmbReasignTo.Location = new System.Drawing.Point(112, 0);
            this.cmbReasignTo.Name = "cmbReasignTo";
            this.cmbReasignTo.Size = new System.Drawing.Size(206, 21);
            this.cmbReasignTo.TabIndex = 1;
            this.cmbReasignTo.TextChanged += new System.EventHandler(this.cmbReasignTo_TextChanged);
            // 
            // rb5
            // 
            this.rb5.AutoSize = true;
            this.rb5.Location = new System.Drawing.Point(0, 0);
            this.rb5.Name = "rb5";
            this.rb5.Size = new System.Drawing.Size(102, 17);
            this.rb5.TabIndex = 0;
            this.rb5.TabStop = true;
            this.rb5.Tag = "reassign";
            this.rb5.Text = "Reassign bug to";
            this.rb5.UseVisualStyleBackColor = true;
            this.rb5.CheckedChanged += new System.EventHandler(this.rb5_CheckedChanged);
            // 
            // txtDuplicateBug
            // 
            this.txtDuplicateBug.Location = new System.Drawing.Point(235, 0);
            this.txtDuplicateBug.Name = "txtDuplicateBug";
            this.txtDuplicateBug.Size = new System.Drawing.Size(83, 20);
            this.txtDuplicateBug.TabIndex = 1;
            this.txtDuplicateBug.TextChanged += new System.EventHandler(this.txtDuplicateBug_TextChanged);
            // 
            // rb4
            // 
            this.rb4.AutoSize = true;
            this.rb4.Location = new System.Drawing.Point(0, 0);
            this.rb4.Name = "rb4";
            this.rb4.Size = new System.Drawing.Size(225, 17);
            this.rb4.TabIndex = 0;
            this.rb4.TabStop = true;
            this.rb4.Text = "Resolve bug, mark it as duplicate of bug #";
            this.rb4.UseVisualStyleBackColor = true;
            this.rb4.CheckedChanged += new System.EventHandler(this.rb4_CheckedChanged);
            // 
            // cmbResolution
            // 
            this.cmbResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResolution.FormattingEnabled = true;
            this.cmbResolution.Location = new System.Drawing.Point(195, 0);
            this.cmbResolution.Name = "cmbResolution";
            this.cmbResolution.Size = new System.Drawing.Size(123, 21);
            this.cmbResolution.TabIndex = 1;
            this.cmbResolution.SelectedIndexChanged += new System.EventHandler(this.cmbResolution_SelectedIndexChanged);
            // 
            // rb3
            // 
            this.rb3.AutoSize = true;
            this.rb3.Location = new System.Drawing.Point(0, 0);
            this.rb3.Name = "rb3";
            this.rb3.Size = new System.Drawing.Size(195, 17);
            this.rb3.TabIndex = 0;
            this.rb3.TabStop = true;
            this.rb3.Text = "Resolve bug, changing resolution to";
            this.rb3.UseVisualStyleBackColor = true;
            this.rb3.CheckedChanged += new System.EventHandler(this.rb3_CheckedChanged);
            // 
            // rb2
            // 
            this.rb2.AutoSize = true;
            this.rb2.Location = new System.Drawing.Point(9, 183);
            this.rb2.Name = "rb2";
            this.rb2.Size = new System.Drawing.Size(232, 17);
            this.rb2.TabIndex = 7;
            this.rb2.TabStop = true;
            this.rb2.Tag = "accept";
            this.rb2.Text = "Accept bug ( change status to ASSIGNED )";
            this.rb2.UseVisualStyleBackColor = true;
            this.rb2.CheckedChanged += new System.EventHandler(this.rb2_CheckedChanged);
            // 
            // rb1
            // 
            this.rb1.AutoSize = true;
            this.rb1.Checked = true;
            this.rb1.Location = new System.Drawing.Point(6, 6);
            this.rb1.Name = "rb1";
            this.rb1.Size = new System.Drawing.Size(72, 17);
            this.rb1.TabIndex = 0;
            this.rb1.TabStop = true;
            this.rb1.Tag = "none";
            this.rb1.Text = "Leave as ";
            this.rb1.UseVisualStyleBackColor = true;
            this.rb1.CheckedChanged += new System.EventHandler(this.rb1_CheckedChanged);
            // 
            // rbTemp1
            // 
            this.rbTemp1.AutoSize = true;
            this.rbTemp1.Location = new System.Drawing.Point(6, 28);
            this.rbTemp1.Name = "rbTemp1";
            this.rbTemp1.Size = new System.Drawing.Size(85, 17);
            this.rbTemp1.TabIndex = 2;
            this.rbTemp1.TabStop = true;
            this.rbTemp1.Text = "radioButton1";
            this.rbTemp1.UseVisualStyleBackColor = true;
            // 
            // rbTemp2
            // 
            this.rbTemp2.AutoSize = true;
            this.rbTemp2.Location = new System.Drawing.Point(6, 51);
            this.rbTemp2.Name = "rbTemp2";
            this.rbTemp2.Size = new System.Drawing.Size(85, 17);
            this.rbTemp2.TabIndex = 3;
            this.rbTemp2.TabStop = true;
            this.rbTemp2.Text = "radioButton2";
            this.rbTemp2.UseVisualStyleBackColor = true;
            // 
            // rbTemp3
            // 
            this.rbTemp3.AutoSize = true;
            this.rbTemp3.Location = new System.Drawing.Point(6, 74);
            this.rbTemp3.Name = "rbTemp3";
            this.rbTemp3.Size = new System.Drawing.Size(85, 17);
            this.rbTemp3.TabIndex = 4;
            this.rbTemp3.TabStop = true;
            this.rbTemp3.Text = "radioButton3";
            this.rbTemp3.UseVisualStyleBackColor = true;
            // 
            // rbTemp4
            // 
            this.rbTemp4.AutoSize = true;
            this.rbTemp4.Location = new System.Drawing.Point(6, 98);
            this.rbTemp4.Name = "rbTemp4";
            this.rbTemp4.Size = new System.Drawing.Size(85, 17);
            this.rbTemp4.TabIndex = 5;
            this.rbTemp4.TabStop = true;
            this.rbTemp4.Text = "radioButton4";
            this.rbTemp4.UseVisualStyleBackColor = true;
            // 
            // pnlResolution
            // 
            this.pnlResolution.Controls.Add(this.rb3);
            this.pnlResolution.Controls.Add(this.cmbResolution);
            this.pnlResolution.Location = new System.Drawing.Point(7, 209);
            this.pnlResolution.Name = "pnlResolution";
            this.pnlResolution.Size = new System.Drawing.Size(321, 23);
            this.pnlResolution.TabIndex = 8;
            // 
            // pnlReassign
            // 
            this.pnlReassign.Controls.Add(this.cmbReasignTo);
            this.pnlReassign.Controls.Add(this.rb5);
            this.pnlReassign.Location = new System.Drawing.Point(7, 285);
            this.pnlReassign.Name = "pnlReassign";
            this.pnlReassign.Size = new System.Drawing.Size(321, 22);
            this.pnlReassign.TabIndex = 10;
            // 
            // pnlDuplicate
            // 
            this.pnlDuplicate.Controls.Add(this.rb4);
            this.pnlDuplicate.Controls.Add(this.txtDuplicateBug);
            this.pnlDuplicate.Location = new System.Drawing.Point(7, 248);
            this.pnlDuplicate.Name = "pnlDuplicate";
            this.pnlDuplicate.Size = new System.Drawing.Size(321, 21);
            this.pnlDuplicate.TabIndex = 9;
            // 
            // rbTemp5
            // 
            this.rbTemp5.AutoSize = true;
            this.rbTemp5.Location = new System.Drawing.Point(6, 121);
            this.rbTemp5.Name = "rbTemp5";
            this.rbTemp5.Size = new System.Drawing.Size(85, 17);
            this.rbTemp5.TabIndex = 6;
            this.rbTemp5.TabStop = true;
            this.rbTemp5.Text = "radioButton5";
            this.rbTemp5.UseVisualStyleBackColor = true;
            // 
            // UCBugStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.rbTemp5);
            this.Controls.Add(this.pnlDuplicate);
            this.Controls.Add(this.pnlReassign);
            this.Controls.Add(this.pnlResolution);
            this.Controls.Add(this.rbTemp4);
            this.Controls.Add(this.rbTemp3);
            this.Controls.Add(this.rbTemp2);
            this.Controls.Add(this.rbTemp1);
            this.Controls.Add(this.rb9);
            this.Controls.Add(this.txtStatusResolution);
            this.Controls.Add(this.rb8);
            this.Controls.Add(this.rb7);
            this.Controls.Add(this.rb6);
            this.Controls.Add(this.rb1);
            this.Controls.Add(this.rb2);
            this.Name = "UCBugStatus";
            this.Size = new System.Drawing.Size(337, 451);
            this.Load += new System.EventHandler(this.UCBugStatus_Load);
            this.pnlResolution.ResumeLayout(false);
            this.pnlResolution.PerformLayout();
            this.pnlReassign.ResumeLayout(false);
            this.pnlReassign.PerformLayout();
            this.pnlDuplicate.ResumeLayout(false);
            this.pnlDuplicate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rb9;
        private System.Windows.Forms.RadioButton rb8;
        private System.Windows.Forms.RadioButton rb7;
        private System.Windows.Forms.TextBox txtStatusResolution;
        private System.Windows.Forms.RadioButton rb6;
        private System.Windows.Forms.RadioButton rb1;
        private System.Windows.Forms.RadioButton rbTemp1;
        private System.Windows.Forms.RadioButton rbTemp2;
        private System.Windows.Forms.RadioButton rbTemp3;
        private System.Windows.Forms.RadioButton rbTemp4;
        private System.Windows.Forms.Panel pnlResolution;
        private System.Windows.Forms.Panel pnlReassign;
        private System.Windows.Forms.Panel pnlDuplicate;
        public System.Windows.Forms.ComboBox cmbResolution;
        public System.Windows.Forms.RadioButton rb3;
        public System.Windows.Forms.RadioButton rb2;
        public System.Windows.Forms.ComboBox cmbReasignTo;
        public System.Windows.Forms.RadioButton rb5;
        public System.Windows.Forms.TextBox txtDuplicateBug;
        public System.Windows.Forms.RadioButton rb4;
        private System.Windows.Forms.RadioButton rbTemp5;
    }
}
