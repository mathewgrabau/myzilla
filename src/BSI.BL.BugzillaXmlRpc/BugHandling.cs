using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using MyZilla.BL.Interfaces;
using MyZilla.BusinessEntities;
using Bugzilla;
using System.Drawing;

namespace MyZilla.BL.BugzillaXmlRpc
{
    public partial class XmlRpcEngine  : IBugBSI
    {

        #region Constructor

        public int _userID;

        public XmlRpcEngine(object UserID) 
        {
            _userID = (int)UserID;
        }

        #endregion

        #region IBugBSI Members

        public MyZilla.BusinessEntities.Bug GetBug( int BugId)
        {
            MyZilla.BusinessEntities.Bug bsiBug = new MyZilla.BusinessEntities.Bug();

            try
            {
                IServer server = new Server("landfill.bugzilla.org", "bugzilla-3.0-branch");

                //returns user id if login is succesfull
                IBug bug = server.GetBug(BugId);


                bsiBug.Id = bug.Id;
                bsiBug.Alias = bug.Alias;
                bsiBug.Summary = bug.Summary;
                bsiBug.Resolution = bug.Resolution;
                bsiBug.Created = bug.Created;
                bsiBug.Changed = bug.Changed;
            }
            catch
            {
                //lblErrorMessage.Text = "User name and/or password are invalid!";
            }

            return bsiBug;
        }

        public List<MyZilla.BusinessEntities.Bug> GetBugs(string BugList)
        {
            return null;
        }

        public List<MyZilla.BusinessEntities.Bug> SearchBugs(NameValueCollection Params)
        {
            return new List<MyZilla.BusinessEntities.Bug>();
        }

        public string UpdateBug(MyZilla.BusinessEntities.Bug bug, out string errMessage)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public string  AddBug( MyZilla.BusinessEntities.Bug bug)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void PostAttachment(Attachment attachment, out string errMessage)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public string GetAttachment(int attID, bool isPicture, out Bitmap bitmap, out string errMessage)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public string UpdateBugs(List<int> bugsToUpdate, MyZilla.BusinessEntities.Bug bugPropetriesToBeChanged, out string errMessage)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public string GetAttachment(int attID, out string errorMessage)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

    }
}
