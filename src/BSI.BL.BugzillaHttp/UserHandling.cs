using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Diagnostics;

//using Microsoft.Practices.EnterpriseLibrary.Logging;
using Tremend.Logging;

using MyZilla.BL.Interfaces;
using MyZilla.BL.Utils;
using MyZilla.BusinessEntities;

namespace MyZilla.BL.BugzillaHttp
{
    public partial class HttpEngine : IUser
    {
        private const string INDEX_PAGE = "index.cgi";

        #region IUser Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>empty string when ok, err message when an error was raised</returns>
        public string LogOnToBugzilla( string username, string password)
        {
            HttpWebResponse respLogin = null;
            HttpHelper httpDialog = null;

#if DEBUG
            Stopwatch watch = Stopwatch.StartNew();
#endif
            MyLogger.Write("Starting LogOn!", "LogOnToBugzilla", LoggingCategory.General);

            try
            {
                TDSettings.ConnectionRow connection = MyZillaSettingsDataSet.GetInstance().GetConnectionById(_connectionId);

                string myZillaUrl = connection.URL;

                string url = String.Concat(myZillaUrl, INDEX_PAGE);

                httpDialog = new HttpHelper(_connectionId, connection.Charset);

                httpDialog.BugzillaCharset = connection.Charset;

                string dataToPost = httpDialog.BuildBugzillaLogOnData(username, password);

                // POST
                respLogin = httpDialog.PostToUrlWhenLogOn(url, dataToPost, false);

#if DEBUG
                watch.Stop();
                MyLogger.Write("LogOn took: " + watch.ElapsedMilliseconds, "LogOnToBugzilla", LoggingCategory.Debug);
#endif

                if (respLogin != null && respLogin.Cookies != null && respLogin.Cookies.Count == 2)
                {
                    // user is logged.

                    CookieManager.AddNewCookieContainer(_connectionId, respLogin.Cookies);
                    
                    return String.Empty;
                }
                else
                {
                    // log failed.
                    if (respLogin != null && respLogin.Cookies != null)
                    {
                        MyLogger.Write(String.Format("LogOn failed! Cookies.Count: {0}", respLogin.Cookies.Count), "LogOnToBugzilla", LoggingCategory.Warning);
                    }
                    else if (respLogin.Cookies == null) {
                        MyLogger.Write("LogOn failed! Cookie collection is NULL", "LogOnToBugzilla", LoggingCategory.Warning);
                    }


                    CookieManager.DeleteCookiesForUser(_connectionId);

                    return "Invalid user or password";
                }
            }
            catch (Exception ex)
            {
                if (httpDialog != null)
                {
                    CookieManager.DeleteCookiesForUser(_connectionId);
                }

                return ex.Message;
            }
            finally
            {
                if (respLogin != null)
                {
                    respLogin.Close(); 
                }

                MyLogger.Write("LogOn completed!", "LogOnToBugzilla", LoggingCategory.General);
            }

        }

        public void LogOutFromBugzilla()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID">Identifies the connection and user credentials.</param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool TestLogOnToBugzilla( string myZillaUrl, string username, string password)
        {

#if DEBUG
            Stopwatch watch = Stopwatch.StartNew();
#endif
            MyLogger.Write("Starting Test LogOn!", "TestLogOnToBugzilla", LoggingCategory.General);

            bool result = false;

            MyZilla.BL.Utils.HttpHelper httpDialog = null;

            if (string.IsNullOrEmpty(this.bugzillaCharset))
            {
                httpDialog = new HttpHelper(-1, "UTF-8");
            }
            else
            {
                httpDialog = new HttpHelper(-1, this.bugzillaCharset);
            }

            HttpWebResponse respLogin = null;

            try
            {

                string url = String.Concat(myZillaUrl, INDEX_PAGE);// "//index.cgi";

                string dataToPost = httpDialog.BuildBugzillaLogOnData(username, password);

                respLogin = httpDialog.PostToUrlWhenLogOn (url, dataToPost, false);

#if DEBUG
                watch.Stop();
                MyLogger.Write("Test LogOn took: " + watch.ElapsedMilliseconds, "TestLogOnToBugzilla", LoggingCategory.Debug);
#endif

                // verify if user is logged. Check if cookie collection has 2 elements.
                if (respLogin != null && respLogin.Cookies != null && respLogin.Cookies.Count == 2)
                {
                    result = true;
                }
                else
                {
                    // connection/credentials not valid 
                    result = false;
                }
            }
            catch (Exception ex )
            {
                MyLogger.Write(ex, "TestLogOnToBugzilla", LoggingCategory.Exception);

                throw ex;
            }
            finally
            {
                CookieManager.DeleteCookiesForUser(-1);

                if (respLogin != null)
                {
                    respLogin.Close(); 
                }

                MyLogger.Write("Test LogOn completed!", "TestLogOnToBugzilla", LoggingCategory.General);
            }

            return result;

        }


        #endregion



    }
}
