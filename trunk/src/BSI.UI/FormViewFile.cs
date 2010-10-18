using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BSI.BusinessEntities;
using System.IO;

namespace BSI.UI
{
    public partial class FormViewFile : Form
    {
        string filePath = string.Empty;

        public FormViewFile( Attachment fileInfo, Bitmap bAtt, string strFile)
        {
            InitializeComponent();

            grInfo.Text += fileInfo.BugID;  

            txtDescription.Text = fileInfo.Description;

            txtContentType.Text = fileInfo.ContentType;

            txtComments.Text = fileInfo.Comment;

            if (bAtt == null)
            {
                strFile =  strFile.Replace(Environment.NewLine, "<br>");  

                wbFile.DocumentText = strFile ;
            }
            else
            {
                string fileExtension = fileInfo.ContentType.Substring(fileInfo.ContentType.IndexOf("/") + 1);

                switch (fileExtension)
                {
                    case "gif":
                        filePath = Application.StartupPath + "\\att.gif";
                        break;
                    case "jpg":
                        filePath = Application.StartupPath + "\\att.jpg";
                        break;
                    case "jpeg":
                        filePath = Application.StartupPath + "\\att.jpeg";
                        break;

                }

                bAtt.Save(filePath );

                string path = "file:///" + filePath ;

                string str  = string.Format("<img src='" + path + "'></img>");

                wbFile.DocumentText = str; 
            }
        }

        public FormViewFile( Attachment fileInfo)
        {
            InitializeComponent();

            if (fileInfo.BugID != -1)
            {
                grInfo.Text += fileInfo.BugID;
            }


            txtDescription.Text = fileInfo.Description;

            txtContentType.Text = fileInfo.ContentType;

            txtComments.Text = fileInfo.Comment;

            string fileExtension = Path.GetExtension ( fileInfo.FileName );
  
            switch (fileExtension )
            {
                case ".txt":
                    StreamReader re = File.OpenText(fileInfo.FileName );

                    string input = null;

                    StringBuilder docText = new StringBuilder();
 
                    while ((input = re.ReadLine()) != null)
                    {
                        docText.Append(input);
                        docText.Append("<br>");
                    }

                    re.Close ();

                    wbFile.DocumentText = docText.ToString(); 

                    break;
                case ".gif":
                case ".jpg":

                    string path = "file:///" + fileInfo.FileName;

                    string str = string.Format("<img src='" + path + "'></img>");

                    wbFile.DocumentText = str; 

                    break;
            }


        }

        #region Form events

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormViewFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath); 
            }
        }

        #endregion

    }
}