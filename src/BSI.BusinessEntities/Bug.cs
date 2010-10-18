using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MyZilla.BusinessEntities
{
    public class Bug 
    {

        #region private fields
        private static string None = "none";

        // bug id
        private int         _id;
        private string      _summary = string.Empty  ;
        private string      _fullSummary = string.Empty;
        private string      _product = string.Empty;
        private string      _alias = string.Empty;
        private string      _component = string.Empty;
        private string      _status = string.Empty;
        private string      _resolution = string.Empty;
        private string      _assignedTo = string.Empty;
        private string      _assignedToRealName = string.Empty;
        private string      _qaContact = string.Empty;
        private string      _qaContactRealName = string.Empty;
        private string      _hardware = string.Empty;
        private string      _os = string.Empty;
        private string      _version= string.Empty ;
        private string      _priority = string.Empty;
        private string      _severity = string.Empty;
        private string      _milestone = string.Empty;
        private string      _reporter = string.Empty;
        private string      _reporterRealName = string.Empty;
        private string      _votes = string.Empty;
        private List<string> _cc = new List<string> ();
        private string      _url = string.Empty;
        private string      _statusWhiteboard = string.Empty;
        private string      _keywords = string.Empty;
        private string      _dependsOn = string.Empty;
        private string         _estimatedTime;
        private string         _remainingTime;
        private string         _actualTime;
        private string _percentageCOmplete;

        private string      _blocks = string.Empty;

        private List<string> _comment = new List<string> ();
        private string      _classification = string.Empty;
        private string      _targetMilestone = string.Empty;
        

        private DateTime    _created;
        private DateTime    _changed;
        private DateTime    _deadline;

        private string _customField;

        private string      _knob =  None;
        private string      _error = string.Empty;

        private bool        _isDirty;
        private string      _reassignTo = string.Empty;
        private string      _duplicateBug = string.Empty;
        private string      _token = string.Empty;
        private List<Attachment> _lstAttachment = new List<Attachment>(); 


        #endregion

        #region properties
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Summary
        {
            get { return _summary; }
            set 
            {
                if (_summary != value)
                {
                    _summary = value;
                    _isDirty = true;
                }
            }
        }

        public string Product
        {
            get { return _product; }
            set 
            {
                if (_product != value)
                {
                    _product = value;
                    _isDirty = true;
                }

            }
        }

        public string Alias
        {
            get { return _alias; }
            set 
            {
                if (_alias != value)
                {
                    _alias = value;
                    _isDirty = true;
                }

            }
        }

        public string Component
        {
            get { return _component; }
            set 
            {
                if (_component != value)
                {
                    _component = value;
                    _isDirty = true;
                }
            }
        }

        public string Status
        {
            get { return _status; }
            set 
            {
                if (_status != value)
                {
                    _status = value;
                    _isDirty = true;
                }

            }
        }

        public string Resolution
        {
            get { return _resolution; }
            set 
            {
                if (_resolution != value)
                {
                    _resolution = value;
                    _isDirty = true;
                }

            }
        }

        public string AssignedTo
        {
            get { return _assignedTo; }
            set 
            {
                if (_assignedTo != value)
                {
                    _assignedTo = value;
                    _isDirty = true;
                }

            }
        }

        public string AssignedToRealName
        {
            get { return _assignedToRealName; }
            set
            {
                if (_assignedToRealName != value)
                {
                    _assignedToRealName = value;
                    _isDirty = true;
                }
            }

        }

        public string QAContact
        {
            get { return _qaContact; }
            set
            {
                if (_qaContact != value)
                {
                    _qaContact = value;
                    _isDirty = true;
                }
            }
        }

        public string QAContactRealName
        {
            get { return _qaContactRealName; }
            set
            {
                if (_qaContactRealName != value)
                {
                    _qaContactRealName = value;
                    _isDirty = true;
                }
            }
        }

        public string TargetMilestone
        {
            get { return _targetMilestone; }
            set
            {
                if (_targetMilestone != value)
                {
                    _targetMilestone = value;
                    _isDirty = true;
                }
            }
        }

        public string Hardware
        {
            get { return _hardware; }
            set 
            {
                if (_hardware != value)
                {
                    _hardware = value;
                    _isDirty = true;
                }

           }
        }

        public string OS
        {
            get { return _os; }
            set 
            {
                if (_os != value)
                {
                    _os = value;
                    _isDirty = true;
                }

            }
        }

        public string Version
        {
            get { return _version; }
            set 
            {
                if (_version != value)
                {
                    _version = value;
                    _isDirty = true;
                }

           }
        }

        public string Priority
        {
            get { return _priority; }
            set 
            {
                if (_priority != value)
                {
                    _priority = value;
                    _isDirty = true;
                }

            }
        }

        public string Severity
        {
            get { return _severity; }
            set 
            {
                if (_severity != value)
                {
                    _severity = value;
                    _isDirty = true;
                }

            }
        }

        public string Milestone
        {
            get { return _milestone; }
            set 
            {
                if (_milestone != value)
                {
                    _milestone = value;
                    _isDirty = true;
                }

            }
        }

        public string Reporter
        {
            get { return _reporter; }
            set 
            { 
                if (_reporter != value)
                {
                    _reporter = value;
                    _isDirty = true;

                }

            }
        }

        public string ReporterRealName
        {
            get { return _reporterRealName; }
            set 
            {
                if (_reporterRealName != value)
                {
                    _reporterRealName = value;
                    _isDirty = true;
                }
            }
        }

        public string Votes
        {
            get { return _votes; }
            set {
                if (_votes != value)
                {
                    _votes = value;
                    _isDirty = true;
                }

            }
        }

        public string FullSummary
        {
            get { return _fullSummary; }
            set
            {
                if (_fullSummary != value)
                {
                    _fullSummary = value;
                    _isDirty = true;
                }
                
            }
        }

        public string PercentageComplete
        {
            get { return _percentageCOmplete; }
            set
            {
                if (_percentageCOmplete != value)
                {
                    _percentageCOmplete = value;
                    _isDirty = true;
                }
            }
        }

        public string CustomField
        {
            get { return _customField; }
            set { 
                if (_customField != value) { 
                    _customField = value;
                    _isDirty = true;
                } 
            }
        }

        public void AddCC(string ccItem)
        {
            _cc.Add(ccItem);

            if (ccItem.Length > 0)
            {
                _isDirty = true;
            }
        }

        public List<string> CC
        {
            get { return _cc; }
            set { _cc = value ;}
        }

        public string Url
        {
            get { return _url; }
            set 
            {
                if (_url != value)
                {

                    _url = value;
                    
                    _isDirty = true;
                }

            }
        }

        public string StatusWhiteboard
        {
            get { return _statusWhiteboard; }
            set  
            {
                if (_statusWhiteboard != value)
                {
                    _statusWhiteboard = value;
                    _isDirty = true;
                }

            }
        }

        public string Keywords
        {
            get { return _keywords; }
            set 
            {
                if (_keywords != value)
                {
                    _keywords = value;
                    _isDirty = true;
                }

            }
        }

        public string DependsOn
        {
            get { return _dependsOn; }
            set 
            {
                if (_dependsOn != value)
                {
                    _dependsOn = value;
                    _isDirty = true;
                }

            }
        }

        public string EstimatedTime
        {
            get { return _estimatedTime; }
            set 
            {
                if (_estimatedTime != value)
                {
                    _estimatedTime = value;
                    _isDirty = true;
                }
            }
        }

        public string ActualTime
        {
            get { return _actualTime; }
            set { _actualTime = value; }
        }


        public string RemainingTime
        {
            get { return _remainingTime; }
            set { _remainingTime = value; }
        }

        public string Blocks
        {
            get { return _blocks; }
            set 
            {
                if (_blocks != value)
                {
                    _blocks = value;
                    _isDirty = true;
                }

            }
        }

        public DateTime Deadline
        {
            get { return _deadline; }
            set 
            {
                if (_deadline != value)
                {
                    _deadline = value;
                    _isDirty = true;
                }
            }
        }

        public void AddComment(string comment)
        {
            _comment.Add(comment); 

            if (comment.Length > 0)
            {
                _isDirty = true;
            }
        }

        /// <summary>
        /// Contains collection of strings with the following format:
        /// who, bug_when, thetext
        /// </summary>
        public List<string> Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }


        public DateTime Created
        {
            get { return _created; }
            set 
            { 
                if (_created != value)
                {
                    _created = value;
                    _isDirty = true;
                }
            }
        }

        public DateTime Changed
        {
            get { return _changed; }
            set 
            {
                if (_changed != value)
                {
                    _changed = value;
                    _isDirty = true;
                }
            }
        }

        public string Classification
        {
            get { return _classification; }
            set 
            {
                if (_classification != value)
                {
                    _classification = value;
                    _isDirty = true;
                }

            }

        }

        public string Knob
        {
            get { return _knob; }
            set 
            {
                if (_knob != value)
                {
                    _knob = value;
                    _isDirty = true;
                }

            }
        }

        public string ErrorMessage
        {
            get { return _error; }
            set 
            {
                if (_error != value)
                {
                    _error = value;
                    _isDirty = true;
                }
            }
        }

        public string ReassignTo
        {
            get
            {
                return _reassignTo;
            }
            set
            {
                if (_reassignTo != value)
                {
                    _reassignTo = value;
                    _isDirty = true;
                }
            }
        }

        public string  DuplicateBug
        {
            get
            {
                return _duplicateBug;
            }
            set
            {
                if (_duplicateBug != value)
                {
                    _duplicateBug = value;
                    _isDirty = true;
                }
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
                if (_token != value)
                {
                    _token = value;
                    //_isDirty = true;
                }
            }
        }

        public bool IsDirty
        {
            get
            {
                return _isDirty;
            }
            set
            {
                _isDirty = value;
            }
    

        }

        public List<Attachment> Attachments
        {
            get
            {
                return _lstAttachment;
            }
            set
            {
                _lstAttachment = value; 
            }

        }


        #endregion

    }
}
