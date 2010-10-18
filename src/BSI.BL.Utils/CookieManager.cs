using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized ;
using System.Net;
using System.Collections;

namespace MyZilla.BL.Utils
{
    /// <summary>
    /// Manages catalogues for the active users in the application.
    /// </summary>
    /// <remarks >
    /// 1. Implemented as a singleton.
    /// 2. Splasher class is used.
    /// </remarks>
    public class CookieManager
    {
        #region Variables

        private static CookieManager instance;

        private Dictionary<int, CookieCollection> globalCookieCollection = new Dictionary<int, CookieCollection>();

        #endregion

        #region Private constructor

        private CookieManager()
        {
        }

        #endregion

        #region Get Instance

        public static CookieManager Instance()
        {
            if (instance == null)
            {
                instance = new CookieManager();
            }

            return instance;
        }

        #endregion

        #region Public methods

        public CookieCollection GetCookieCollectionByUserId(int userId)
        {
            CookieCollection result = null;

            if (instance.globalCookieCollection.ContainsKey(userId))
                result = instance.globalCookieCollection[userId];

            return result;
        }

        public bool CookieCollectionContainsCookieByUserId(string cookieName, int userId)
        {
            CookieCollection cc = null;
            bool result = false;

            if (instance.globalCookieCollection.ContainsKey(userId))
                cc = instance.globalCookieCollection[userId];

            if (cc!=null)
            {
                result = (cc[cookieName] == null ? false : true);
            }

            return result;
        }

        public void AddNewCookieToCookieCollection(int userId, Cookie newCookie) 
        {
            if (!instance.globalCookieCollection.ContainsKey(userId))
            {
                CookieCollection cc = new CookieCollection();
                cc.Add(newCookie);

                instance.globalCookieCollection.Add(userId, cc);
            }
            //{
                System.Threading.Monitor.Enter(this);
                try
                {
                    instance.globalCookieCollection[userId].Add(newCookie);
                }
                finally {
                    System.Threading.Monitor.Exit(this);
                }

            //}
        }

        public static void AddNewCookieContainer(int userId, CookieCollection cookieCollection)
        {
            if (instance.globalCookieCollection.ContainsKey(userId))
            {
                //System.Threading.Monitor.Enter(this);
                //try
                //{
                    instance.globalCookieCollection[userId].Add(cookieCollection);
                //}
                //finally
                //{
                //    System.Threading.Monitor.Exit(this);
                //}
                
            }
            else
                //System.Threading.Monitor.Enter(this);
                //try
                //{
                    instance.globalCookieCollection.Add(userId, cookieCollection);
                //}
                //finally
                //{
                //    System.Threading.Monitor.Exit(this);
                //}
            

            return;
        }

        public static void DeleteCookiesForUser(int userId)
        {
            if (instance.globalCookieCollection.ContainsKey(userId))
            {
                //System.Threading.Monitor.Enter(this);
                //try
                //{
                    instance.globalCookieCollection.Remove(userId);
                //}
                //finally
                //{
                //    System.Threading.Monitor.Exit(this);
                //}
            }

            return;
        }

        #endregion

    }
}
