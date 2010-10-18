using System;
using System.Collections.Generic;
using System.Text;
using MyZilla.BL.Interfaces;
using Bugzilla;

namespace MyZilla.BL.BugzillaXmlRpc
{
    public partial class XmlRpcEngine :IUser 
    {

        #region IUser Members

        public string Login( string username, string password)
        {

            IServer server = new Server("landfill.bugzilla.org", "bugzilla-3.0-branch");

            int uID = server.Login(username , password , true);

            return uID.ToString();
        }

        public void Logout()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool TestUserCredentials(string taskzillaURL, string username, string password)
        {
            throw new Exception("The method or operation is not implemented.");
        }


        #endregion




    }
}
