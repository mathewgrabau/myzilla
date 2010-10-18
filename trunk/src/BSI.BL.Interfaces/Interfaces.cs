using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel ;
using MyZilla.BusinessEntities ;
using System.Drawing ;
using System.IO;

namespace MyZilla.BL.Interfaces
{
    public interface IUser
    {
        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="username">The user's user name.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>On success, the id of the user that was loggin in.</returns>
        string LogOnToBugzilla(string username, string password);

        /// <summary>
        /// Log out a current user.
        /// </summary>
        void LogOutFromBugzilla( );

        /// <summary>
        /// Test credentials against a connection.
        /// </summary>
        /// <param name="myZillaUrl">Url of the bugzilla system where the user is connected.</param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool TestLogOnToBugzilla( string myZillaUrl, string username, string password);

    }

    public interface IBugBSI
    {
        /// <summary>
        /// Get details for a specified bug.
        /// </summary>
        /// <param name="BugId">The bug identifier.</param>
        /// <returns></returns>
        MyZilla.BusinessEntities.Bug GetBug( int bugId);

        List<MyZilla.BusinessEntities.Bug> GetBugs(string bugIdList);

        string GetBugLastUpdated(int bugId);

        /// <summary>
        /// This method parses the buglist.cgi response html and retrives the bugs found
        /// </summary>
        /// <param name="Params">Hash containing  the columns that must be loaded and later on, displayed</param>
        /// <returns></returns>
        List<MyZilla.BusinessEntities.Bug> SearchBugs(NameValueCollection Params);

        /// <summary>
        /// Update a bug.
        /// </summary>
        /// <param name="bug">The bug details.</param>
        string UpdateBug(MyZilla.BusinessEntities.Bug bug, out string errorMessage);

        string AddBug(MyZilla.BusinessEntities.Bug newBug);

        void PostAttachment(MyZilla.BusinessEntities.Attachment attachment, out string errorMessage);

        string GetAttachment(int attID, bool isPicture, out Bitmap bitmap, out string errorMessage);

        string GetAttachment(int attID, out string errorMessage);

        string UpdateBugs(SortedList bugsIdList, Bug bugPropetriesToBeChanged, out string errorMessage);
        
    }

    public interface IUtilities
    {
        /// <summary>
        /// Get a specified catalogue.
        /// </summary>
        /// <param name="catalogID">
        /// The ID of the catalogue.
        /// - if HTTP communication, the 'name' in the select tag
        /// <c>
        ///  <select name="product" >
        ///      <option value="Product1">Product1</option>
        ///      <option value="Product2">Product2</option>
        /// </select>
        /// </c>
        /// </param>
        /// <returns></returns>
        ArrayList GetCatalogues(string[] catalogIdList);

        /// <summary>
        /// Returns a collection of catalogues that depends on a selected product.
        /// The method is used when adding/editing a bug.
        /// </summary>
        /// <param name="url">Url of the bugzilla system where the user is connected</param>
        /// <param name="productName">The product the returned collections depend on.</param>
        /// <returns>
        /// AssignTo catalogue
        /// CC catalogue
        /// </returns>
        ArrayList GetSpecificCataloguesWhenManageBug(string productName, NameValueCollection Components);

        ArrayList GetValuesForDependentCatalogues(int classificationsCount, NameValueCollection products);

        void RemoveCookieCollectionForUser();

        /// <summary>
        /// Gets the chosen columns to be displayed in the page with the results of a search
        /// The columns are returned in a hashtable
        /// </summary>
        /// <returns>Hashtable (key is column name, value is the index in the array of columns)</returns>
        Hashtable GenerateColumnsToBeDisplayed( BackgroundWorker backgroundWorker);

        string[] GetColumnsToBeDisplayedFromCookie();

        string[] GetAllColumnsToBeDisplayedFromCookie();

        void AddNewCookie(string CookieName, string CookieValue);

        bool ContainsCookie(string CookieName);

        /// <summary>
        /// Test if the URL for the connection is valid.
        /// </summary>
        /// <param name="url">Url of the bugzilla system where the user is connected.</param>
        /// <param name="connectionType"> The type of the tested connection</param> 
        /// <returns></returns>
        bool TestConnectionToUrl(string url, string connectionType);

        string GetBugzillaVersion(string url);

        string GetBugzillaCharset(string url);

        string GetPublishedMyZillaVersion(string url, string currentVersion);
    }

        

}
