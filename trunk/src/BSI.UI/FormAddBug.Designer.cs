namespace MyZilla.UI
{
    partial class FormAddBug
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddBug));
            this.ucInsertBug = new MyZilla.UI.UCInsertBug();
            this.SuspendLayout();
            // 
            // ucInsertBug
            // 
            this.ucInsertBug.BackColor = System.Drawing.SystemColors.Window;
            this.ucInsertBug.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucInsertBug.Location = new System.Drawing.Point(0, 0);
            this.ucInsertBug.Name = "ucInsertBug";
            this.ucInsertBug.Size = new System.Drawing.Size(760, 611);
            this.ucInsertBug.TabIndex = 1;
            // 
            // FormAddBug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 611);
            this.Controls.Add(this.ucInsertBug);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAddBug";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New bug";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormAddBug_FormClosing);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormAddBug_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private UCInsertBug ucInsertBug;
    }
}