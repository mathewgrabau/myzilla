namespace MyZilla.UI
{
    partial class FormEditBug
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditBug));
            this.ucEditBug = new MyZilla.UI.UCEditBug();
            this.SuspendLayout();
            // 
            // ucEditBug
            // 
            this.ucEditBug.BackColor = System.Drawing.SystemColors.Window;
            this.ucEditBug.BugToUpdate = null;
            this.ucEditBug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucEditBug.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ucEditBug.Location = new System.Drawing.Point(0, 0);
            this.ucEditBug.Name = "ucEditBug";
            this.ucEditBug.Size = new System.Drawing.Size(806, 702);
            this.ucEditBug.TabIndex = 1;
            // 
            // FormEditBug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 702);
            this.Controls.Add(this.ucEditBug);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FormEditBug";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit bug";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEditBug_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormEditBug_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private UCEditBug ucEditBug;
    }
}