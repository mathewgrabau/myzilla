using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Specialized ;
using System.Windows.Forms;
using System.Threading;
using System.Net;

using MyZilla.BL.Interfaces;
using MyZilla.BL.Utils;
using MyZilla.BusinessEntities;
using Tremend.Logging;
//using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Xml;


namespace MyZilla.BL.BugzillaHttp
{
    public partial class HttpEngine : IUtilities 
    {
        private const string ADVANCED_SEARCH_BUGS_PAGE = "query.cgi?format=advanced";
        private const string DISPLAYED_COLUMNS_PAGE = "colchange.cgi";
        private const string ADD_BUG_PAGE = "enter_bug.cgi?product={0}";

        ArrayList resultSpecificCatalogues = new ArrayList();

        NameValueCollection resAssignedTo ;

        NameValueCollection resCC;

        NameValueCollection resPriority;

        // result of the calling thread.
        Hashtable cols = new Hashtable(50);

        private string bugzillaCharset;

        public string GetBugzillaCharset()
        {
             return this.bugzillaCharset;
        }

        #region IUtilities Members

        public ArrayList GetCatalogues( string[] catalogIdList)
        {

            int pos = 0;
            int posBegin = 0;
            int posEnd = 0;
            
            List<string> lstOptions = null;

            string strSelect = string.Empty; 
            
            ArrayList lstCatalogues = new ArrayList ();

            TDSettings.ConnectionRow connection = MyZillaSettingsDataSet.GetInstance().GetConnectionById(_connectionId);

            HttpHelper httpDialog = new HttpHelper(_connectionId, connection.Charset);

            string myZillaUrl = connection.URL;

            // get the content of the page
            myZillaUrl += ADVANCED_SEARCH_BUGS_PAGE;

            MyLogger.Write("Start getting main catalogs! Url = " + myZillaUrl, "GetCatalogues", LoggingCategory.General);

            string htmlContent = httpDialog.GetFromUrl(myZillaUrl, false);

#if DEBUG
            MyZilla.BL.Interfaces.Utils.htmlContents = htmlContent;
#endif

            // find the select TAG in html content.

            for (int catalogNumber = 0; catalogNumber < catalogIdList.Length; catalogNumber ++ )
            {

                pos = 0;

                lstOptions = new List<string>();

                while (pos < htmlContent.Length)
                {
                    posBegin = htmlContent.IndexOf("<select", pos);

                    if (posBegin == -1)
                    {
                        break;
                    }
                    else
                    {
                        posEnd = htmlContent.IndexOf("</select>", posBegin);

                        strSelect = htmlContent.Substring(posBegin, posEnd - posBegin);

                        if (strSelect.IndexOf("name=\"" + catalogIdList [catalogNumber ]) >= 0)
                        {
                            // the catalogue was found

                            lstOptions = HttpHelper.GetOptionsFormSelection(strSelect);  

                            break;

                        }
                        else
                        {
                            pos = posEnd + 1;
                        }

                    }
                }

                lstCatalogues.Add(lstOptions);

            }

            MyLogger.Write("Complete getting main catalogs!", "GetCatalogues", LoggingCategory.General);  

            return lstCatalogues;

        }

        private static Dictionary <string, string > GetSubstringForCriteria(string baseString, string startString, string endString, string baseCatalogItemName)
        {
            int startPos = 0;
            int endPos = 0;
            string container = string.Empty;

            Dictionary<string, string> result = new Dictionary<string, string>();

            startPos = baseString.IndexOf(startString);

            if (startPos >= 0)
            {
                endPos = baseString.IndexOf(endString, startPos ); 

                if (endPos >= 0)
                {
                    container  = baseString.Substring(startPos + startString.Length +1, endPos - (startPos+startString.Length + 1)  ); 
                }
            }

            if (container.Length != 0)
            {
                string[] temp = container.Split(',');

                foreach (string t in temp)
                {
                    string tt = t.Trim().TrimStart('\'').TrimEnd('\'');

                    //result.Add(tt.Replace("\\x40", "@"), baseCatalogItemName); 
                    result.Add(UnEscapeString(tt), baseCatalogItemName); 
                }
            }
            return result ;

        }

        public static string UnEscapeString(string FieldValue)
        {
            string result = FieldValue;

            if (FieldValue.IndexOf('\\') >= 0)
            {
                result = result.Replace("\\\\", "\\").Replace("\\x40", "@").Replace("\\/", "/");
            }
            return result;
        }

        private static string[] GetStringFromArray(string baseString, string ArrayName, int index)
        {
            int startPos = 0;
            int endPos = 0;
            string stringToFind = ArrayName + "[" + index.ToString() + "] = ";
            string endString = "\";";
            string container = string.Empty;

            string[] result = new string[1];

            startPos = baseString.IndexOf(stringToFind);

            if (startPos >= 0)
            {
                endPos = baseString.IndexOf(endString, startPos);

                if (endPos >= 0)
                {
                    container = baseString.Substring(startPos + stringToFind.Length + 1, endPos - (startPos + stringToFind.Length + 1));
                }
            }

            if (container.Length != 0)
            {
                string[] temp = container.Split(',');

                for(int i=0;i<temp.GetLength(0);i++)
                {
                    string tt = temp[i].Trim().TrimStart('\'').TrimEnd('\'');

                    result[i]= UnEscapeString(tt);
                    //result[i] = HttpUtility.HtmlDecode(tt);
                }
            }

            return result;

        }

        public ArrayList  GetValuesForDependentCatalogues( int classificationsCount, NameValueCollection products   )
        {
            
            ArrayList result = new ArrayList();

            NameValueCollection resCPTS = new NameValueCollection();

            NameValueCollection resVERS = new NameValueCollection();

            NameValueCollection resTMS = new NameValueCollection();

            // get the content of the page
            TDSettings.ConnectionRow connection = MyZillaSettingsDataSet.GetInstance().GetConnectionById(_connectionId);

            string myZillaUrl = connection.URL;

            HttpHelper httpDialog = new HttpHelper(_connectionId, connection.Charset);

            myZillaUrl += ADVANCED_SEARCH_BUGS_PAGE;

            string htmlContent = httpDialog.GetFromUrl(myZillaUrl, false);

            if (classificationsCount == 0)
            {
                for (int i = 0; i < products.Count ; i++)
                {
                    string catalogItemName =  products.GetKey (i);  

                    Dictionary< string, string > res = GetSubstringForCriteria(htmlContent, "cpts[" + i.ToString() + "] = ", "];", catalogItemName   );
                    
                    foreach (KeyValuePair <string , string > entry in res )
                    {
                        resCPTS.Add (entry.Value, entry.Key);   
                     }


                    res = GetSubstringForCriteria(htmlContent, "vers[" + i.ToString() + "] = ", "];", catalogItemName);

                    foreach (KeyValuePair<string, string> entry in res)
                    {
                        resVERS.Add(entry.Value , entry.Key);
                    }

                    res = GetSubstringForCriteria(htmlContent, "tms[" + i.ToString() + "]  = ", "];", catalogItemName);

                    foreach (KeyValuePair<string, string> entry in res)
                    {
                        resTMS.Add(entry.Value, entry.Key);
                    }

 
                }

                result.Add(resCPTS);

                result.Add(resVERS);

                result.Add(resTMS); 
            }

            return result;

        }

        public ArrayList GetValuesForProductDependentCatalogues(int classificationsCount, NameValueCollection products)
        {
            ArrayList result = new ArrayList();

            NameValueCollection resCPTS = new NameValueCollection();

            NameValueCollection resVERS = new NameValueCollection();

            // get the content of the page
            TDSettings.ConnectionRow connection = MyZillaSettingsDataSet.GetInstance().GetConnectionById(_connectionId);

            string myZillaUrl = connection.URL;

            HttpHelper httpDialog = new HttpHelper(_connectionId, connection.Charset);

            myZillaUrl += ADVANCED_SEARCH_BUGS_PAGE;

            string htmlContent = httpDialog.GetFromUrl(myZillaUrl, false);

            if (classificationsCount == 0)
            {
                for (int i = 0; i < products.Count; i++)
                {
                    string catalogItemName = products.GetKey(i);

                    Dictionary<string, string> res = GetSubstringForCriteria(htmlContent, "cpts[" + i.ToString() + "] = ", "];", catalogItemName);

                    foreach (KeyValuePair<string, string> entry in res)
                    {
                        resCPTS.Add(entry.Key, entry.Value);
                    }


                    res = GetSubstringForCriteria(htmlContent, "vers[" + i.ToString() + "] = ", "];", catalogItemName);

                    foreach (KeyValuePair<string, string> entry in res)
                    {
                        resVERS.Add(entry.Key, entry.Value);
                    }

                }

                result.Add(resCPTS);

                result.Add(resVERS);
            }

            return result;


        }

        /// <summary>
        /// Gets first substring after a specified index, substring identified by a start string and an end string
        /// Method can include start and end string in the result depending on input parameter
        /// </summary>
        /// <param name="Container"></param>
        /// <param name="StartString"></param>
        /// <param name="EndString"></param>
        /// <param name="FromIndex"></param>
        /// <param name="IncludeStartEnd"></param>
        /// <returns></returns>
        private static string GetStringBetween(string Container, string StartString, string EndString, int FromIndex, bool IncludeStartEnd) {
            string result = string.Empty;

            int startIndex = Container.IndexOf(StartString, FromIndex);

            if (startIndex >= 0)
            {

                int endIndex = Container.IndexOf(EndString, startIndex + StartString.Length);

                if (!IncludeStartEnd)
                    startIndex += StartString.Length;

                if (endIndex > startIndex)
                {
                    if (IncludeStartEnd)
                        endIndex += EndString.Length;

                    result = Container.Substring(startIndex, endIndex - startIndex);
                }
            }

            return(result.Trim());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlContent"></param>
        public void ParseHtmlForColumnListValues(object htmlContent)
        {
            string content = htmlContent.ToString();

            int startFormIndex = content.IndexOf("<form action=\"colchange.cgi\"");
            if (startFormIndex >= 0)
            {
                int endFormIndex = content.IndexOf("</form>", startFormIndex);

                string formXML = content.Substring(startFormIndex, endFormIndex + "</form>".Length - startFormIndex - 1);

                Hashtable labels = new Hashtable();

                string cookieValue = String.Empty;
                string cookieValueAll = String.Empty;
                string delim = String.Empty;

                int index = formXML.IndexOf("<input type=\"checkbox\"");

                while (index > 0)
                {
                    int endIndex = formXML.IndexOf(">", index);
                    string input = GetStringBetween(formXML, "<input type=\"checkbox\"", ">", index, true);//formXML.Substring(index, endIndex - index + 1);
                    string nameAttributeValue = GetStringBetween(input, "name=\"", "\"", 0, false);
                    string isChecked = GetStringBetween(input, "checked=\'", "\'", 0, false);
                    nameAttributeValue = nameAttributeValue.Replace("column_", String.Empty);

                    if (isChecked != null && isChecked.Length > 0)
                    {

                        //isChecked can have values: 0-not used, 1-used and visible, 2-used but invisible

                        isChecked = "1";
                        cookieValue += delim + nameAttributeValue;

                        if (cookieValue.Length > 0)
                            delim = "%20";
                    }
                    else
                    {

                        if (nameAttributeValue == "bug_severity")
                        {
                            cookieValue += delim + nameAttributeValue;
                            isChecked = "2";
                        }
                        else
                            isChecked = "0";
                    }

                    index = formXML.IndexOf("<label for=", endIndex);
                    string forAttributeValue = GetStringBetween(formXML, "for=\"", "\"", index, false);
                    string forText = GetStringBetween(formXML, "\">", "</label>", index, false);
                    labels.Add(forAttributeValue, forText.TrimEnd(new char[] { ' ', '\n' }));

                    cookieValueAll += labels[nameAttributeValue] + "&" + nameAttributeValue;

                    cookieValueAll += "&" + isChecked + "@";

                    index = formXML.IndexOf("<input type=\"checkbox\"", index);
                }

                bool bugSeverityIsVisible = (cookieValue.IndexOf("bug_severity") >= 0);

                if (!bugSeverityIsVisible)
                    cookieValue += "bug_severity";

                string myZillaUrl = MyZillaSettingsDataSet.GetInstance().GetUrlForConnection(_connectionId);
                string domain = myZillaUrl.Substring(myZillaUrl.IndexOf("://") + 3).TrimEnd('/');

                if (!CookieManager.Instance().CookieCollectionContainsCookieByUserId("COLUMNLIST", _connectionId))
                    CookieManager.Instance().AddNewCookieToCookieCollection(_connectionId, new Cookie("COLUMNLIST", cookieValue, "", domain));
                CookieManager.Instance().AddNewCookieToCookieCollection(_connectionId, new Cookie("COLUMNLISTALL", cookieValueAll, "", domain));
            }

            return;
        }

        public void AddNewCookie(string CookieName, string CookieValue) {
            string myZillaUrl = MyZillaSettingsDataSet.GetInstance().GetUrlForConnection(_connectionId);
            string domain = myZillaUrl.Substring(myZillaUrl.IndexOf("://")+3).TrimEnd('/');
            CookieManager.Instance().AddNewCookieToCookieCollection(_connectionId, new Cookie(CookieName, CookieValue, "", domain));
        }

        public bool ContainsCookie(string CookieName)
        {
            return CookieManager.Instance().CookieCollectionContainsCookieByUserId(CookieName, _connectionId);
        }

        /// <summary>
        /// Gets the columns to be displayed from Bugzilla - colchange.cgi
        /// 
        /// </summary>
        /// <param name="BugzillaUrl">Url of the bugzilla system</param>
        /// <returns>Hashtable (key is column name, value is the index in the array of columns)</returns>
        public Hashtable GenerateColumnsToBeDisplayed( System.ComponentModel.BackgroundWorker backgroundWorker  )
        {

            TDSettings.ConnectionRow connection = MyZillaSettingsDataSet.GetInstance().GetConnectionById(_connectionId);

            HttpHelper httpRequest = new HttpHelper(_connectionId, connection.Charset);

            //GET HTML content for the page provided
            string htmlContent = httpRequest.GetFromUrl(String.Concat(connection.URL, DISPLAYED_COLUMNS_PAGE), false);

            ParseHtmlForColumnListValues(htmlContent);

            if (backgroundWorker != null)
            {
                backgroundWorker.ReportProgress(100);
            }

            return cols ;

        }

        public void RemoveCookieCollectionForUser() {
            CookieManager.DeleteCookiesForUser(_connectionId);
        }

        /// <summary>
        /// Gets the columns to be displayed in the bug search operation
        /// from the already generated cookie
        /// </summary>
        /// <returns>null is cookie is not yet generated</returns>
        public string[] GetColumnsToBeDisplayedFromCookie() {
            return GetColumnsFromCookie("COLUMNLIST", "%20");
        }

        /// <summary>
        /// Gets all possible columns to be displayed in the bug search operation
        /// from the already generated cookie
        /// </summary>
        /// <returns>null is cookie is not yet generated</returns>
        public string[] GetAllColumnsToBeDisplayedFromCookie()
        {
            return GetColumnsFromCookie("COLUMNLISTALL", "@");
        }

        public string[] GetColumnsFromCookie(string cookieName, string columnsDelimiter)
        {
            string[] cols = null;

            CookieManager cookieManager = CookieManager.Instance();

            CookieCollection cookieCollection = cookieManager.GetCookieCollectionByUserId(_connectionId);

            if (cookieCollection == null)
            {
                GenerateColumnsToBeDisplayed(null);
                cookieCollection = cookieManager.GetCookieCollectionByUserId(_connectionId);
            }
            
            Cookie cookieColumnNames;

            if (cookieCollection[cookieName] == null)
                GenerateColumnsToBeDisplayed(null);

            cookieColumnNames = cookieCollection[cookieName];

            if (cookieColumnNames != null)
            {
                cols = cookieColumnNames.Value.Split(new string[] { columnsDelimiter }, StringSplitOptions.RemoveEmptyEntries);
            }
            else {
                MyLogger.Write("GetColumnsFromCookie - Cookie does not exist: " + cookieName, "GetCatalogues", LoggingCategory.Warning);
            }

            return cols;
        }

        private static List<string> GetNameAndEmailFormString(string htmlContent)
        {
            List<string> result = new List<string>();

            htmlContent = htmlContent.Replace("&#64;", "@");

            int startIndex = htmlContent.IndexOf("<option");

            while (startIndex >= 0) {
                string optionTag = GetStringBetween(htmlContent, "<option", "</option>", startIndex, false);

                string valueOfOption = GetStringBetween(optionTag, "value=\"", "\"", 0, false);

                if (valueOfOption.Length>0)
                    result.Add(valueOfOption);

                startIndex = htmlContent.IndexOf("<option", startIndex + 1);
            }

            return result;
        }
        
        /// <summary>
        /// Parses bugzilla html response for values associated with
        /// assignedTo and cc catalogues
        /// </summary>
        /// <param name="dataContainer"></param>
        public void ParseHtmlForDependentCatalogues(object dataContainer)
        {
            DataContainer data = (DataContainer)dataContainer;

            string htmlContent = data.HtmlContent;
            NameValueCollection Components = data.Components;
            string product = data.Product;

            List<string> collCC = new List<string>();

            List<string> colAssignedTo = new List<string>();

            resAssignedTo = new NameValueCollection();
            resCC = new NameValueCollection();
            resPriority = new NameValueCollection();

            string[] keyComponent = new string[] { };

            string htmlElement = string.Empty;

            #region Getting AssignedTo info

            int count = 0;

            string keyProduct = String.Concat(product, ",", product);

            keyComponent = Components[keyProduct].Split(',');

            count = keyComponent.GetLength(0);

            for (int i = 0; i < count ; i++)
            {
                //process only components belonging to the current product
                //if (Components.Keys[i].StartsWith(product + ","))
                {
                    keyComponent = GetStringFromArray(htmlContent, "components", i);

                    if (keyComponent[0] != null)
                    {
                        //value from initialowners is used to get the default assignee (dependend on component)
                        string[] depAssignedTo = GetStringFromArray(htmlContent, "owners", i);

                        resAssignedTo.Add(keyComponent[0], depAssignedTo[0]);
                    }
                    else {
                        keyComponent[0] = MyZilla.BusinessEntities.Dependencies.NoComponentDependency;
                    }

                    //all possible assignee might be contained in a combo box
                    htmlElement = GetStringBetween(htmlContent, "<select name=\"assigned_to", "</select>", 0, false);

                    if (!String.IsNullOrEmpty(htmlElement))
                    {
                        colAssignedTo = GetNameAndEmailFormString(htmlElement);

                        foreach (string name in colAssignedTo)
                        {
                            resAssignedTo.Add(keyComponent[0], name);
                        }
                    }

                    //count++;
                }

            }//end for

            #endregion

            #region Getting default Priority
            htmlElement = GetStringBetween(htmlContent, "<select name=\"priority", "</select>", 0, false);

            if (!String.IsNullOrEmpty(htmlElement))
            {
                string priority = GetStringBetween(htmlElement, "\"selected\">", "</option>", 0, false);
                if (!string.IsNullOrEmpty(priority)) {
                    resPriority.Add(product, priority);
                }
            }

            #endregion

            #region Getting CC info

            //bugzilla 2.x might support only listbox containing possible cc values
            //no dependency on component
            htmlElement = GetStringBetween(htmlContent, "<select name=\"cc", "</select>", 0, true);

            if (!String.IsNullOrEmpty(htmlElement))
            {

                collCC = GetNameAndEmailFormString(htmlElement);

                foreach (string name in collCC) {
                    resCC.Add(MyZilla.BusinessEntities.Dependencies.NoComponentDependency, name);
                }
            }
            //bugzilla 3.x contains cc values as a dependent catalog on component
            else //tag name = input
            {
                //if (htmlContent.IndexOf("initialccs") == -1)
                //{
                //    htmlElement = GetStringBetween(htmlContent, "<select name=\"cc", "</select>", 0, true);

                //    string value = GetStringBetween(htmlElement, "value=\"", "\"", 0, false);
                    
                //    resCC.Add(MyZilla.BusinessEntities.Dependencies.NoComponentDependency, value);
                //}
                //else
                //{
                    //initialccs
                    for (int i = 0; i < Components.Count; i++)
                    {
                        if (Components.Keys[i].StartsWith(product + ","))
                        {
                            keyComponent = GetStringFromArray(htmlContent, "components", i);

                            if (keyComponent != null)
                            {
                                string[] depCC = GetStringFromArray(htmlContent, "initialccs", i);

                                for (int j = 0; j < depCC.GetLength(0); j++)
                                {
                                    if (!String.IsNullOrEmpty(depCC[j]))
                                        resCC.Add(keyComponent[j], depCC[j]);
                                }
                            }

                            break;
                        }
                        
                    }
                //}
            }

            #endregion



            resultSpecificCatalogues.Add(resAssignedTo);

            resultSpecificCatalogues.Add(resCC);

            resultSpecificCatalogues.Add(resPriority);

            // TO DO:
            //remarks: AssignTo catalogue do not depend of the components/products. The default value ( default asignee ) depends of the component.

        }

        public ArrayList GetSpecificCataloguesWhenManageBug(string productName, NameValueCollection Components)
        {
            MyLogger.Write("Start getting bug specific catalogs!", "GetSpecificCataloguesWhenManageBug", LoggingCategory.General);

            TDSettings.ConnectionRow connection = MyZillaSettingsDataSet.GetInstance().GetConnectionById(_connectionId);

            string myZillaUrl = connection.URL;

            HttpHelper httpHelper = new HttpHelper(_connectionId, connection.Charset);

            myZillaUrl = myZillaUrl + String.Format(ADD_BUG_PAGE, productName);
  
            //get html content
            string htmlContent = httpHelper.GetFromUrl(myZillaUrl, false);

            ParseHtmlForDependentCatalogues(new DataContainer(productName, htmlContent, Components));

            MyLogger.Write("Complete getting bug specific catalogs!", "GetSpecificCataloguesWhenManageBug", LoggingCategory.General);  

            return resultSpecificCatalogues;

        }

        public class DataContainer
        {
            private string product;
            private string htmlContent;
            private NameValueCollection components;

            public DataContainer(string product, string htmlContent, NameValueCollection componentsCollection)
            {
                this.product = product;
                this.htmlContent = htmlContent;
                this.components = componentsCollection;
            }

            public string Product
            {
                get { return this.product; }
                set { this.product = value; }
            }

            public string HtmlContent
            {
                get { return this.htmlContent; }
                set { this.htmlContent = value; }
            }

            public NameValueCollection Components
            {
                get { return this.components; }
                set { this.components = value; }
            }

        }

        public bool TestConnectionToUrl(string url, string connectionType)
        {
            System.Net.CookieCollection cookies = HttpHelper.colCookies;

            bool result = true;

            try
            {
                // because the cookie collection is modified during the request, save it.

                HttpHelper httpRequest = new HttpHelper(_connectionId, this.bugzillaCharset);

                //GET HTML content for the page provided
                string htmlContent = httpRequest.GetFromUrl(url, true);

                // search in the htmlContent the string that represent the connection type
                if (htmlContent.IndexOf(connectionType) == -1)
                {
                    result = false;
                }

            }
            catch (Exception ex )
            {
                MyLogger.Write(ex, "TestConnectionToUrl", LoggingCategory.Exception); 

                result = false;
            }
            finally
            {
                HttpHelper.colCookies = cookies;

            }

            return result ;

        }

        public string GetBugzillaCharset(string url)
        {
            string version = String.Empty;

            TDSettings.ConnectionRow connection = MyZillaSettingsDataSet.GetInstance().GetConnectionById(_connectionId);

            string urlBase = connection.URL;

            HttpHelper httpDialog = new HttpHelper(_connectionId, connection.Charset);

            string httpContent = httpDialog.GetFromUrl(urlBase, false);

            this.bugzillaCharset = httpDialog.BugzillaCharset;

            return this.bugzillaCharset;
        }

        public string GetBugzillaVersion(string url)
        {
            string version = String.Empty;

            TDSettings.ConnectionRow connection = MyZillaSettingsDataSet.GetInstance().GetConnectionById(_connectionId);

            string urlBase = connection.URL;

            HttpHelper httpDialog = new HttpHelper(_connectionId, connection.Charset);

            string httpContent = httpDialog.GetFromUrl(String.Concat(urlBase, String.Format(BUG_DETAILS_URL, -1)), false);

            this.bugzillaCharset = httpDialog.BugzillaCharset;

            XmlDocument doc = new XmlDocument();
            
            string toFind = "urlbase=\"";

            int startIndex = httpContent.IndexOf(toFind);

            if (startIndex >= 0)
            {
                int endIndex = httpContent.IndexOf('"', startIndex + toFind.Length);

                if (endIndex > startIndex)
                {
                    string[] list = httpContent.Substring(startIndex + toFind.Length, endIndex - startIndex - toFind.Length).Split(new char[] { '"' }, StringSplitOptions.RemoveEmptyEntries);

                    httpContent = httpContent.Replace(list[0], urlBase);
                }
            }

            //WARNING
            //if login cookies are not generated
            //, xml is not returned and getting bugzilla version fails
            try
            {
                doc.LoadXml(httpContent);

                XmlNodeList bugVersion = doc.GetElementsByTagName("bugzilla");
                XmlNode versionNode = bugVersion[0];
                XmlElement versionElement = (XmlElement)versionNode;
                if (versionElement.HasAttributes)
                {
                    version = versionElement.Attributes["version"].InnerText;
                }
            }
            catch {
                //generic version number for cases when getting bugzilla version fails
                version = "2.0";
            }

            return version;
       }

        public string GetPublishedMyZillaVersion(string url, string currentVersion) {
            string result = null;

            try
            {
                HttpHelper httpRequest = new HttpHelper(_connectionId, this.bugzillaCharset);

                //GET HTML content for the page provided
                string htmlContent = httpRequest.GetFromUrl(url, true);

                string[] versionGroups = htmlContent.Split('-');

                if ((versionGroups != null) && (versionGroups.GetLength(0))>=2) {

                    if (versionGroups != null && versionGroups.GetLength(0) >= 2)
                    {
                        result = versionGroups[1];
                    }
                }

            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "GetPublishedMyZillaVersion", LoggingCategory.Exception);

                result = null;
            }
            finally
            {

            }

            return result;
        }

        #endregion

    }
}