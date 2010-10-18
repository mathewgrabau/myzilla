using System;
using System.Collections.Generic;
using System.Text;

namespace MyZilla.BusinessEntities
{
    public class Attachment
    {
        private int _ID;

        private string _fileName = string.Empty;

        private int _bugID;

        private string _description = string.Empty;

        private string _contentTypeSelection = string.Empty;

        private string _comment = string.Empty;

        private AttachmentOperation _operation = AttachmentOperation.Unchanged;

        private DateTime _created;

        private long  _size;

        private string _token;

        public Attachment(int bugId, string fileName, string description, string contentType, string comment )
        {
            _ID = -1;

            _bugID = bugId;

            _fileName = fileName;

            _description = description;

            _contentTypeSelection = contentType;

            _comment = comment;

            _operation = AttachmentOperation.Add   ;

            _created = DateTime.Now; 
        }

        public Attachment()
        {

        }

        public int AttachmentId
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        public string FileName 
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value; 
            }
        }

        public int BugId
        {
            get
            {
                return _bugID ;
            }
            set
            {
                _bugID = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        public string ContentType
        {
            get
            {
                return _contentTypeSelection ;
            }
            set
            {
                _contentTypeSelection = value;
            }
        }

        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
            }
        }

        public AttachmentOperation Operation
        {
            get
            {
                return _operation;
            }
            set
            {
                _operation = value;
            }
        }

        public DateTime Created
        {
            get
            {
                return _created;
            }
            set
            {
                _created = value;
            }

        }

        public long Size
        {
            get
            {
                return _size;
            }
            set
            {
                _size = value;
            }
        }

        public string Token
        {
            get 
            {
                return _token;
            }
            set
            {
                _token = value;
            }
        }
    }

    public enum AttachmentOperation
    {
        Add,
        Unchanged,
        Remove
    }
}
