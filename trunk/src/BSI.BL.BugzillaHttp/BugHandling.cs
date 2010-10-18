using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Xml;
using System.Web;
using System.Globalization;

using MyZilla.BL.Interfaces;
using MyZilla.BL.Utils;
using MyZilla.BusinessEntities;
using Tremend.Logging;
using System.Drawing ;
using System.Text.RegularExpressions;


namespace MyZilla.BL.BugzillaHttp
{
    public partial class HttpEngine : IBugBSI
    {
        private const string BUG_DETAILS_URL = "show_bug.cgi?id={0}&ctype=xml";
        private const string BUG_LAST_UPDATED_URL = "show_bug.cgi?id={0}";
        private const string BUGS_DETAILS_URL = "show_bug.cgi?{0}&ctype=xml";
        private const string SEARCH_BUGS_PAGE = "buglist.cgi?";
        private const string ATTACHMENT_PAGE = "attachment.cgi?id=";
        private const string UPDATE_BUG_PAGE = "process_bug.cgi";
        private const string UPDATE_BUG_2_0_PAGE = "post_bug.cgi";

        public int _connectionId;
        // result of the calling thread.
        List<MyZilla.BusinessEntities.Bug> bugs = new List<MyZilla.BusinessEntities.Bug>();

        #region Contructor

        public HttpEngine(object connectionId) 
        {
            _connectionId = (int)connectionId ;
        }

        #endregion

        #region IBugBSI Members

        /// <summary>
        /// Get the details of the specified bug.
        /// </summary>
        /// <param name="bugzillaURL"></param>
        /// <param name="bugId"></param>
        /// <returns></returns>
        public MyZilla.BusinessEntities.Bug GetBug( int bugId)
        {
            TDSettings.ConnectionRow connection = MyZillaSettingsDataSet.GetInstance().GetConnectionById(_connectionId);

            string urlEnding = String.Format(BUG_DETAILS_URL, bugId);

            MyZilla.BusinessEntities.Bug bugBSI = GetBugDetailsFromXml(connection.URL, urlEnding, connection.Charset);

            return bugBSI;

        }

        /// <summary>
        /// Get the LAST_UPDATED (delta_ts) of the specified bug.
        /// Used only for bugzilla 2.18
        /// </summary>
        /// <param name="bugzillaURL"></param>
        /// <param name="bugId"></param>
        /// <returns></returns>
        public string GetBugLastUpdated(int bugId)
        {
            string result = null;

            TDSettings.ConnectionRow connection = MyZillaSettingsDataSet.GetInstance().GetConnectionById(_connectionId);

            string myZillaUrl = connection.URL;

            string urlEnding = String.Format(BUG_LAST_UPDATED_URL, bugId);

            try
            {
                HttpHelper httpDialog = new HttpHelper(_connectionId, connection.Charset);

                string strContent = httpDialog.GetFromUrl(myZillaUrl + urlEnding, false);

                if (String.IsNullOrEmpty(strContent)) { } 
                else {
                    result = GetStringBetween(strContent, "value=\"", "\"", strContent.IndexOf("delta_ts"), false);
                }
            }
            catch { }

            return result;

        }

        /// <summary>
        /// Get the details of the specified bug.
        /// </summary>
        /// <param name="bugzillaURL"></param>
        /// <param name="bugId"></param>
        /// <returns></returns>
        public List<MyZilla.BusinessEntities.Bug> GetBugs(string bugIdList)
        {

            TDSettings.ConnectionRow connection = MyZillaSettingsDataSet.GetInstance().GetConnectionById(_connectionId);

            string urlEnding = String.Format(BUGS_DETAILS_URL, bugIdList);

            XmlNodeList bugsAsXMLNodes = GetBugsAsXML(connection.URL, urlEnding, connection.Charset);

            List<MyZilla.BusinessEntities.Bug> bugs = new List<MyZilla.BusinessEntities.Bug>();

            if (bugsAsXMLNodes != null)
            {
                foreach (XmlNode node in bugsAsXMLNodes)
                {
                    bugs.Add(GetBug(node));
                }
            }
            else { 
            }

            return bugs;
        }



        /// <summary>
        ///
        /// </summary>
        /// <param name="htmlContent"></param>
        public void ParseCsvGettingBugs(object htmlContent)
        {
            ArrayList alResult;
            string[] contentLines = ((String)htmlContent).Split('\n');
            //using (StreamReader objReader = new StreamReader(txtCSVPath.Text.Trim()))
            //{
            int lineIndex = 0;
            const int headerIndex = 0;
            ArrayList Cols = new ArrayList();
            MyZilla.BusinessEntities.Bug newBug;
            

            DateTime result;

            char csvSeparator = ',';

            if (contentLines.GetLength(0) > 0)
            {
                alResult = CsvParser.Parse(contentLines[0], csvSeparator);

                if (alResult == null || alResult.Count == 0) {
                    csvSeparator = ';';
                }

                foreach (string strLineText in contentLines)
                {
                    alResult = CsvParser.Parse(strLineText, csvSeparator);

                    if (alResult == null || alResult.Count == 0)
                    {
                        MyLogger.Write(" Parsing cvs bugs list failed.", "ParseCsvGettingBugs", LoggingCategory.Exception);
                        continue;
                    }

                    if (lineIndex == headerIndex)
                        Cols = alResult;
                    else
                    {
                        bool success;
                        newBug = new Bug();
                        for (int s = 0; s < alResult.Count; s++)
                        {
                            string currentTDValue = alResult[s].ToString();
                            #region set properties of the bug
                            switch (Cols[s].ToString())
                            {
                                case "bug_id":
                                    newBug.Id = Int32.Parse(currentTDValue);
                                    break;

                                case "alias":
                                    newBug.Alias = currentTDValue;
                                    break;

                                case "opendate":
                                    result = new DateTime();
                                    success = DateTime.TryParse(currentTDValue, out result);
                                    if (success)
                                        newBug.Created = result;
                                    else
                                        newBug.Created = new DateTime();
                                    break;

                                case "changeddate":
                                    result = new DateTime();
                                    success = DateTime.TryParse(currentTDValue, out result);
                                    if (success)
                                        newBug.Changed = result;
                                    else
                                        newBug.Changed = new DateTime();
                                    break;

                                case "bug_severity":
                                    newBug.Severity = currentTDValue;
                                    break;

                                case "priority":
                                    newBug.Priority = currentTDValue;
                                    break;

                                case "rep_platform":
                                    newBug.Hardware = currentTDValue;
                                    break;

                                case "assigned_to":
                                    newBug.AssignedTo = currentTDValue;
                                    break;

                                case "assigned_to_realname":
                                    newBug.AssignedToRealName = currentTDValue;
                                    break;

                                case "reporter":
                                    newBug.Reporter = currentTDValue;
                                    break;

                                case "reporter_realname":
                                    newBug.ReporterRealName = currentTDValue;
                                    break;

                                case "bug_status":
                                    newBug.Status = currentTDValue;
                                    break;

                                case "resolution":
                                    newBug.Resolution = currentTDValue;
                                    break;

                                case "classification":
                                    newBug.Classification = currentTDValue;
                                    break;

                                case "product":
                                    newBug.Product = currentTDValue;
                                    break;

                                case "component":
                                    newBug.Component = currentTDValue;
                                    break;

                                case "version":
                                    newBug.Version = currentTDValue;
                                    break;

                                case "op_sys":
                                    newBug.OS = currentTDValue;
                                    break;

                                case "votes":
                                    newBug.Votes = currentTDValue;
                                    break;

                                case "target_milestone":
                                    newBug.TargetMilestone = currentTDValue;
                                    break;

                                case "qa_contact":
                                    newBug.QAContact = currentTDValue;
                                    break;

                                case "qa_contact_realname":
                                    newBug.QAContact = currentTDValue;
                                    break;

                                case "status_whiteboard":
                                    newBug.StatusWhiteboard = currentTDValue;
                                    break;

                                case "keywords":
                                    newBug.Keywords = currentTDValue;
                                    break;

                                case "estimated_time":
                                    newBug.EstimatedTime = currentTDValue;
                                    break;

                                case "remaining_time":
                                    newBug.RemainingTime = currentTDValue;
                                    break;

                                case "actual_time":
                                    newBug.ActualTime = currentTDValue;
                                    break;

                                case "percentage_complete":
                                    newBug.PercentageComplete = currentTDValue;
                                    break;

                                case "deadline":
                                    result = new DateTime();
                                    success = DateTime.TryParse(currentTDValue, out result);
                                    if (success)
                                        newBug.Deadline = result;
                                    break;

                                case "short_desc":
                                    newBug.FullSummary = currentTDValue;
                                    break;

                                case "short_short_desc":
                                    newBug.Summary = currentTDValue;
                                    break;

                                case "cf_custom_field":
                                    newBug.CustomField = currentTDValue;
                                    break;

                                default:
                                    break;
                            }

                            #endregion
                        }

                        this.bugs.Add(newBug);
                    }

                    lineIndex++;
                }
            }
            //}

            #region old way (parse pure html)
            //string[] Cols = GetColumnsToBeDisplayedFromCookie();

            //WebBrowser wbHTMLDoc = new WebBrowser();

            //wbHTMLDoc.ScriptErrorsSuppressed = true;

            //wbHTMLDoc.Navigate("about:blank");

            //HtmlDocument doc = wbHTMLDoc.Document;

            //doc.Write(string.Empty);

            ////set the response HTML in the web browser control
            //wbHTMLDoc.DocumentText = htmlContent.ToString();

            ////associate complete event
            //wbHTMLDoc.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(wbHTMLDoc_DocumentCompleted);

            ////forces events to execute 
            ////(wihtout this, property Document of the web browser control is null)

            //Thread.Sleep(200);

            //Application.DoEvents();

            //HtmlDocument htmlDoc = wbHTMLDoc.Document;

            //HtmlElementCollection htmlElemCollection = htmlDoc.GetElementsByTagName("Table");

            //int nrOfBugs;

            //BSI.BusinessEntities.Bug newBug;
            //bugs = new List<BSI.BusinessEntities.Bug>();

            //foreach (HtmlElement htmlElem in htmlElemCollection)
            //{
            //    HtmlElementCollection htmlElem2 = htmlElem.GetElementsByTagName("colgroup");
            //    if (htmlElem2 != null)
            //    {
            //        HtmlElementCollection htmlTRCollection = htmlElem.GetElementsByTagName("tr");
            //        if (htmlTRCollection != null)
            //        {

            //            nrOfBugs = htmlTRCollection.Count - 1;

            //            string currentTDValue = String.Empty;



            //            //start from 1 (first tr contains the header)
            //            for (int bugIndex = 0; bugIndex < nrOfBugs; bugIndex++)
            //            {
            //                newBug = new BSI.BusinessEntities.Bug();
                            
            //                //GetBug columns collections
            //                //(each columns contains a value for a field of the bug)
            //                HtmlElementCollection htmlTDCollection = htmlTRCollection[bugIndex+1].GetElementsByTagName("td");

                            
            //                //bug id is always in the first column in "a href"
            //                currentTDValue = htmlTDCollection[0].GetElementsByTagName("a")[0].OuterText;

            //                //set the ID of the bug
            //                newBug.ID = Int32.Parse(currentTDValue);

            //                DateTime result;
            //                bool success;

            //                for (int s = 0; s < htmlTDCollection.Count - 1; s++)
            //                {
            //                    //get value from the current column
            //                    currentTDValue = htmlTDCollection[s + 1].OuterText;

            //                    #region set properties of the bug
            //                    switch (Cols[s].ToString())
            //                    {
            //                        case "opendate":
            //                            result = new DateTime();
            //                            success = DateTime.TryParse(currentTDValue, out result);
            //                            newBug.Created = result;
            //                            break;
            //                        case "changeddate":
            //                            result = new DateTime();
            //                            success = DateTime.TryParse(currentTDValue, out result);
            //                            newBug.Changed = result;
            //                            break;
            //                        case "bug_severity":
            //                            newBug.Severity = currentTDValue;
            //                            break;
            //                        case "priority":
            //                            newBug.Priority = currentTDValue;
            //                            break;
            //                        case "rep_platform":
            //                            newBug.Hardware = currentTDValue;
            //                            break;
            //                        case "assigned_to":
            //                            newBug.AssignedTo = currentTDValue;
            //                            break;
            //                        case "assigned_to_realname":
            //                            newBug.AssignedToRealName = currentTDValue;
            //                            break;
            //                        case "reporter":
            //                            newBug.Reporter = currentTDValue;
            //                            break;
            //                        case "reporter_realname":
            //                            newBug.ReporterRealName = currentTDValue;
            //                            break;
            //                        case "bug_status":
            //                            newBug.Status = currentTDValue;
            //                            break;
            //                        case "resolution":
            //                            newBug.Resolution = currentTDValue;
            //                            break;
            //                        case "product":
            //                            newBug.Product = currentTDValue;
            //                            break;
            //                        case "component":
            //                            newBug.Component = currentTDValue;
            //                            break;
            //                        case "version":
            //                            newBug.Version = currentTDValue;
            //                            break;
            //                        case "op_sys":
            //                            newBug.OS = currentTDValue;
            //                            break;
            //                        case "votes":
            //                            newBug.Votes = currentTDValue;
            //                            break;
            //                        case "status_whiteboard":
            //                            newBug.StatusWhiteboard = currentTDValue;
            //                            break;
            //                        case "short_desc":
            //                            newBug.FullSummary = currentTDValue;
            //                            break;
            //                        case "short_short_desc":
            //                            newBug.Summary = currentTDValue;
            //                            break;
            //                    }

            //                    #endregion
            //                }

            //                bugs.Add(newBug);
            //            }
                        
            //            //return bugs;
            //        }

            //    }
            //}
            
            ////return null;

            //#region old way of getting bugs from a search operation
            ////htmlContent = httpDialog.GetHTML(htmlContent, @"<table class=""bz_buglist""", @"</table>");
            ////int Pos = 0;
            ////string htmlRow = String.Empty;
            ////string[] values;

            ////string htmlHeaderRow = httpDialog.GetNextRowInTable(htmlContent, ref Pos);
            //////for each th in htmlHeaderRow

            ////string[] headerColumns = httpDialog.GetValuesFromTableRow(htmlHeaderRow, "th");


            ////while ((htmlRow = httpDialog.GetNextRowInTable(htmlContent, ref Pos)).Length>0)
            ////{
            ////    //contains the values of the bug fields
            ////    values = httpDialog.GetValuesFromTableRow(htmlRow, "td");
            ////}

            ////return new BSI.BusinessEntities.Bug[1];
            //#endregion
            #endregion

        }

        /// <summary>
        /// This method parses the buglist.cgi response html and retrives the bugs found
        /// </summary>
        /// <param name="BugzillaUrl">Url of the bugzilla system where the user is connected</param>
        /// <param name="Params">Hash containing  the columns that must be loaded and later on, displayed</param>
        /// <returns></returns>
        public List<MyZilla.BusinessEntities.Bug> SearchBugs(NameValueCollection Params)
        {
            TDSettings.ConnectionRow connection = MyZillaSettingsDataSet.GetInstance().GetConnectionById(_connectionId);

            string myZillaUrl = connection.URL;

            HttpHelper httpDialog = new HttpHelper(_connectionId, connection.Charset);

            myZillaUrl += SEARCH_BUGS_PAGE;// "buglist.cgi?";

            StringBuilder criterias = new StringBuilder();

            string paramFormat = "{0}={1}&";

            for (int i = 0; i < Params.Count; i++)
            {
                //compose QueryString based on the criteria selected in the interface
                for (int j = 0; j < Params.GetValues(i).Length; j++)
                {
                    criterias.Append(String.Format(paramFormat, Params.GetKey(i), Params.GetValues(i)[j]));
                }
            }

            myZillaUrl += criterias.ToString();

            string htmlContent = httpDialog.GetFromUrl(myZillaUrl + "ctype=csv", false);

            this.ParseCsvGettingBugs(htmlContent);

            return bugs;

        }


        /// <summary>
        /// Update a bug.
        /// </summary>
        /// <param name="bug"></param>
        public string UpdateBug( MyZilla.BusinessEntities.Bug bug, out string errorMessage)
        {
            errorMessage = string.Empty;

            // get version for connection
            MyZillaSettingsDataSet _appSettings = MyZillaSettingsDataSet.GetInstance();

            TDSettings.ConnectionRow connection = _appSettings.GetConnectionById(_connectionId);

            string myZillaUrl = connection.URL;

            string bugzillaVersion = _appSettings.GetConnectionById(_connectionId).Version;

            string strResult = string.Empty;

            string url = String.Concat(myZillaUrl, UPDATE_BUG_PAGE);

            MyZilla.BL.Utils.HttpHelper httpDialog = new HttpHelper(_connectionId, connection.Charset);
            
            string dataToPost = httpDialog.PostHttpRequest_UpdateBug(bug, bugzillaVersion);

            string htmlContent = httpDialog.PostToUrl(url, dataToPost, false);

            // verify if confirmation string exits in response
            int pos = htmlContent.IndexOf("Changes submitted");

            if (pos >= 0)
            {
                strResult = string.Format(Resource.MsgUpdBugSuccessfully, bug.Id);
            }
            else
            {

                int pos1 = htmlContent.IndexOf("<title>");
                int pos2 = htmlContent.IndexOf("</title>");


                string strTitle = errorMessage = htmlContent.Substring(pos1 + "<title>".Length, pos2 - (pos1 - 1 + "</title>".Length));

                strResult = string.Format(Resource.MsgUpdBugFailed, bug.Id) + Environment.NewLine + strTitle;

                if (strTitle.Contains("collision"))
                    strResult += Environment.NewLine + "Bug details will be reloaded!";
            }

#if DEBUG
            MyZilla.BL.Interfaces.Utils.htmlContents = htmlContent;
#endif

            if (strResult.IndexOf("failed") >= 0)
            {
                // search in htmlContent the error
                // check if assignee was properly assigned
                //bool errIdentified = false;

                if (htmlContent.ToLower().IndexOf("assignee") >= 0)
                {
                    errorMessage = string.Format ( Resource.ErrAssigneeNotMatch, bug.AssignedTo );

                    //errIdentified = true;
                }
                if (htmlContent.ToLower().IndexOf("invalid bug id") >= 0)
                {
                    errorMessage = Resource.ErrInvalidBugID;

                   // errIdentified = true;
                }
                //if (!errIdentified)
                //{
                //    errorMessage = Resource.ErrNotIdentifiedFail;  
                //}

            }

            return strResult;

        }

        public string AddBug( MyZilla.BusinessEntities.Bug newBug)
        {
            string result = string.Empty  ;

            int versionINT = -1;

            TDSettings.ConnectionRow connection = MyZillaSettingsDataSet.GetInstance().GetConnectionById(_connectionId);

            // get version for connection
            MyZillaSettingsDataSet _appSettings = MyZillaSettingsDataSet.GetInstance();

            string version =  _appSettings.GetConnectionById(_connectionId).Version;

            try
            {
                versionINT = int.Parse(version.Substring(0, version.IndexOf(".")));
            }
            catch (FormatException ex)
            {
                result = ex.Message;

                return result;
            }

            switch (versionINT)
            {
                case 2:
                    result = this.AddBugForVersion_2_0(newBug, connection.URL, connection.Charset);
                    break;
                case 3:
                    result = this.AddBugForVersion_3_0(newBug, connection.URL, connection.Charset);
                    break;
            }

            return result;

        }

        public void PostAttachment(Attachment attachment, out string errorMessage)
        {
            errorMessage = string.Empty;

            TDSettings.ConnectionRow connection = MyZillaSettingsDataSet.GetInstance().GetConnectionById(_connectionId);

            string myZillaUrl = connection.URL;

            MyZilla.BL.Utils.HttpHelper httpDialog = new HttpHelper(_connectionId, connection.Charset);

            try
            {
                string result = httpDialog.PostAttachment(attachment , myZillaUrl );

                string token = "";
                int startToken = result.IndexOf("token");
                if (startToken > 0)
                {
                    int stopTokenEntry = result.Substring(startToken).IndexOf(">");
                    if (stopTokenEntry > 0)
                    {
                        string res = result.Substring(startToken, stopTokenEntry);
                        if (!string.IsNullOrEmpty(res))
                        {
                            if (res.Contains("value="))
                            {
                                token = res.Substring(res.IndexOf("value=") + "value=".Length);
                                token = token.Replace("\"", "");
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(token))
                    attachment.Token = token;

                if (result.IndexOf("Changes Submitted") >= 0)
                {
                    errorMessage = string.Empty;
                }
                else
                {
                    int pos1 = result.IndexOf("<title>");
                    int pos2 = result.IndexOf("</title>");


                    errorMessage = result.Substring(pos1 +"<title>".Length, pos2 -( pos1 -1  + "</title>".Length ));
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public string GetAttachment(int attID, bool isPicture, out Bitmap bitmap, out string errorMessage)
        {
            bitmap = null;

            string result = string.Empty  ;

            errorMessage = string.Empty;

            TDSettings.ConnectionRow connection = MyZillaSettingsDataSet.GetInstance().GetConnectionById(_connectionId);

            string url = String.Concat(connection.URL, ATTACHMENT_PAGE, attID);

            MyZilla.BL.Utils.HttpHelper httpDialog = new HttpHelper(_connectionId, connection.Charset);

            if (isPicture)
            {
                bitmap = httpDialog.GetPictureFromUrl(url, false);
            }
            else
            {

                string getResult = httpDialog.GetFromUrl(url, false);

                if (result.ToLower().IndexOf("error") >= 0)
                {
                    errorMessage = getResult;
                }
                else
                {
                    result = getResult;
                }

            }


            return result;
           
        }


        public string GetAttachment(int attID , out string errorMessage)
        {
            TDSettings.ConnectionRow connection = MyZillaSettingsDataSet.GetInstance().GetConnectionById(_connectionId);

            string url = String.Concat(connection.URL, ATTACHMENT_PAGE, attID);

            MyZilla.BL.Utils.HttpHelper httpDialog = new HttpHelper(_connectionId, connection.Charset);

            string strFullPath = httpDialog.GetStreamFormUrl(url,out errorMessage  );

            return strFullPath;

        }


        public string UpdateBugs(SortedList bugsIdList, Bug bugPropetriesToBeChanged, out string errorMessage)
        {
            errorMessage = string.Empty;

            TDSettings.ConnectionRow connection = MyZillaSettingsDataSet.GetInstance().GetConnectionById(_connectionId);

            string strResult = string.Empty;

            string url = String.Concat(connection.URL, UPDATE_BUG_PAGE);

            MyZilla.BL.Utils.HttpHelper httpDialog = new HttpHelper(_connectionId, connection.Charset);

            string dataToPost = httpDialog.PostHttpRequest_UpdateBugsBulk ( bugsIdList, bugPropetriesToBeChanged  );

            string htmlContent = httpDialog.PostToUrl(url, dataToPost, false);

            errorMessage = HttpEngine.GetStringBetween(htmlContent, "<title>", "</title>", 0, false);
            
            if (errorMessage.Contains("processed")) {
                //message is not from an error
                errorMessage = String.Empty;

                string updBugs = String.Empty;

                foreach (object key in bugsIdList.Keys)
                {
                    if (String.IsNullOrEmpty(updBugs))
                    {
                        updBugs = key.ToString();
                    }
                    else
                    {
                        updBugs += "," + key.ToString();
                    }
                }

                if (updBugs.Length > 0)
                {
                    strResult = string.Format(Resource.MsgUpdBunchBugsSuccessfully, updBugs);
                }

            }
#if DEBUG
            MyZilla.BL.Interfaces.Utils.htmlContents = htmlContent;
#endif

            return strResult;
        }

        #endregion

        #region Private methods

        private XmlNodeList GetBugsAsXML(string urlBase, string urlEnding, string charset) {
            XmlNodeList nodes = null;

            try
            {
                HttpHelper httpDialog = new HttpHelper(_connectionId, charset);

                string url = urlBase + urlEnding;

                httpDialog.BugzillaCharset = charset;

                string strContent = httpDialog.GetFromUrl(url, false);

                #region prevent error in loading the bugs xml

                string toFind = "urlbase=\"";

                int startIndex = strContent.IndexOf(toFind);

                int endIndex = strContent.IndexOf('"', startIndex + toFind.Length);

                string[] list = strContent.Substring(startIndex + toFind.Length, endIndex - startIndex - toFind.Length).Split(new char[] { '"' }, StringSplitOptions.RemoveEmptyEntries);

                if (list != null && list.GetLength(0) > 0)
                    strContent = strContent.Replace(list[0], urlBase);
                else
                    strContent = strContent.Replace(toFind + "\"", toFind + urlBase + "\"");

                startIndex = strContent.IndexOf("<!DOCTYPE");
                if (startIndex>=0){
                    endIndex = strContent.IndexOf(">", startIndex);

                    string xmlType = strContent.Substring(startIndex, endIndex-startIndex + 1);

                    strContent = strContent.Replace(xmlType, string.Empty);
                }

                #endregion

                XmlDocument doc = new XmlDocument();

                doc.LoadXml(CleanInvalidXmlChars(strContent));

                nodes = doc.GetElementsByTagName("bug");
            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "GetBugsAsXML", LoggingCategory.Exception);

                //result.ErrorMessage = ex.Message;
            }

            return nodes;
        }

        /// <summary>
        /// Get the details of a bug, reading a xml structure.
        /// </summary>
        /// <param name="htmlContent"></param>
        /// <returns></returns>
        private MyZilla.BusinessEntities.Bug GetBugDetailsFromXml(string urlBase,  string urlEnding, string charset)
        {
            MyZilla.BusinessEntities.Bug result = new MyZilla.BusinessEntities.Bug();

            try
            {

                HttpHelper httpDialog = new HttpHelper(_connectionId, charset);

                string strContent =  httpDialog.GetFromUrl(urlBase + urlEnding, false);

#if DEBUG
                //string bugDetailsFile = @"C:\Documents and Settings\Marius Zavoi\Desktop\LogFiles\show_bug.xml";
                //if (File.Exists(bugDetailsFile)){
                //    TextReader reader = new StreamReader(bugDetailsFile);
                //    using (reader) {
                //        strContent = reader.ReadToEnd();
                //    }
                //}
#endif

                XmlDocument doc = new XmlDocument();
                
                string toFind = "urlbase=\"";

                int startIndex = strContent.IndexOf(toFind);

                int endIndex = strContent.IndexOf('"', startIndex + toFind.Length);

                string[] list = strContent.Substring(startIndex + toFind.Length, endIndex - startIndex - toFind.Length).Split(new char[] { '"' }, StringSplitOptions.RemoveEmptyEntries);

                if (list != null && list.GetLength(0)>0)
                    strContent = strContent.Replace(list[0], urlBase);
                else
                    strContent = strContent.Replace(toFind + "\"", toFind + urlBase + "\"");

                startIndex = strContent.IndexOf("<!DOCTYPE");
                if (startIndex >= 0)
                {
                    endIndex = strContent.IndexOf(">", startIndex);

                    string xmlType = strContent.Substring(startIndex, endIndex - startIndex + 1);

                    strContent = strContent.Replace(xmlType, string.Empty);
                }

                doc.LoadXml(CleanInvalidXmlChars(strContent));

                XmlNodeList bugDetail = doc.GetElementsByTagName("bug");

                result = GetBug(bugDetail[0]);

            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "GetBugDetailsFromXml", LoggingCategory.Exception); 
                
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// This removes characters that are invalid for xml encoding
        /// </summary>
        /// <param name="text">Text to be encoded.</param>
        /// <returns>Text with invalid xml characters removed.</returns>
        public static string CleanInvalidXmlChars(string text)
        {
            // From xml spec valid chars:
            // #x9 | #xA | #xD | [#x20-#xD7FF] | [#xE000-#xFFFD] | [#x10000-#x10FFFF]    
            // any Unicode character, excluding the surrogate blocks, FFFE, and FFFF.
            //string regExpr = @"[^\x09\x0A\x0D\x20-\xD7FF\xE000-\xFFFD\x10000-x10FFFF]";
            // \x02 was added by my based on errors encountered
            string regExpr = @"^([\\x09\\x0A\\x0D\\x20-\\x7E]|" //# ASCII
                                + "[\\xC2-\\xDF][\\x80-\\xBF]|" //# non-overlong 2-byte
                                + "\\xE0[\\xA0-\\xBF][\\x80-\\xBF]|" //# excluding overlongs
                                + "[\\xE1-\\xEC\\xEE\\xEF][\\x80-\\xBF]{2}|" //# straight 3-byte
                                + "\\xED[\\x80-\\x9F][\\x80-\\xBF]|" //# excluding surrogates
                                + "\\xF0[\\x90-\\xBF][\\x80-\\xBF]{2}|" //# planes 1-3
                                + "[\\xF1-\\xF3][\\x80-\\xBF]{3}|" //# planes 4-15
                                + "\\xF4[\\x80-\\x8F][\\x80-\\xBF]{2})*$";
            return Regex.Replace(text, regExpr, "?");
        }

        /// <summary>
        /// Walks through the node element and fills a bug object
        /// </summary>
        /// <param name="BugNode"></param>
        /// <returns></returns>
        private static Bug GetBug(XmlNode BugNode) {

            Bug bugResult = new Bug();

            XmlElement bugElement = (XmlElement)BugNode;

            try{

                if (bugElement.HasAttributes)
                {
                    string errorMessage = bugElement.Attributes["error"].InnerText;

                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        bugResult.ErrorMessage = errorMessage;

                        return bugResult;
                    }
                }

                string comment = string.Empty;

                #region Get object properties

                if (bugElement.GetElementsByTagName("alias").Count == 1)
                {
                    bugResult.Alias = bugElement.GetElementsByTagName("alias")[0].InnerText;
                }

                bugResult.Id = int.Parse(bugElement.GetElementsByTagName("bug_id")[0].InnerText);

                bugResult.Product = bugElement.GetElementsByTagName("product")[0].InnerText;

                bugResult.Component = bugElement.GetElementsByTagName("component")[0].InnerText;

                bugResult.Status = bugElement.GetElementsByTagName("bug_status")[0].InnerText;

                bugResult.AssignedTo = bugElement.GetElementsByTagName("assigned_to")[0].InnerText;

                if (bugElement.GetElementsByTagName("bug_file_loc").Count == 1)
                {

                    bugResult.Url = bugElement.GetElementsByTagName("bug_file_loc")[0].InnerText;
                }

                if (bugElement.GetElementsByTagName("short_desc").Count == 1)
                {

                    bugResult.FullSummary = bugElement.GetElementsByTagName("short_desc")[0].InnerText;


                    bugResult.Summary = bugResult.FullSummary.Substring(0, Math.Min(bugResult.FullSummary.Length, 60));
                }


                if (bugElement.GetElementsByTagName("rep_platform").Count == 1)
                {

                    bugResult.Hardware = bugElement.GetElementsByTagName("rep_platform")[0].InnerText;
                }


                if (bugElement.GetElementsByTagName("op_sys").Count == 1)
                {

                    bugResult.OS = bugElement.GetElementsByTagName("op_sys")[0].InnerText;
                }


                if (bugElement.GetElementsByTagName("version").Count == 1)
                {

                    bugResult.Version = bugElement.GetElementsByTagName("version")[0].InnerText;
                }

                if (bugElement.GetElementsByTagName("priority").Count == 1)
                {

                    bugResult.Priority = bugElement.GetElementsByTagName("priority")[0].InnerText;
                }

                if (bugElement.GetElementsByTagName("bug_severity").Count == 1)
                {

                    bugResult.Severity = bugElement.GetElementsByTagName("bug_severity")[0].InnerText;
                }

                if (bugElement.GetElementsByTagName("reporter").Count == 1)
                {

                    bugResult.Reporter = bugElement.GetElementsByTagName("reporter")[0].InnerText;
                }

                if (bugElement.GetElementsByTagName("clasification").Count == 1)
                {

                    bugResult.Classification = bugElement.GetElementsByTagName("clasification")[0].InnerText;
                }


                //result.Created
                string dateAsString = bugElement.GetElementsByTagName("creation_ts")[0].InnerText;
                DateTime value;

                //prevent wrond date parsing by removing PST, PDT, CET ...
                //(some database datetime values particularity) with  empty string
                Regex dateFieldEnding = new Regex(@" [A-Z]+", RegexOptions.IgnoreCase);

                dateAsString = dateFieldEnding.Replace(dateAsString, string.Empty);

                bool success = DateTime.TryParse(dateAsString, out value);

                if (success)
                    bugResult.Created = value;

                //read bug CHANGED date
                dateAsString = bugElement.GetElementsByTagName("delta_ts")[0].InnerText;

                dateAsString = dateFieldEnding.Replace(dateAsString, string.Empty);

                success = DateTime.TryParse(dateAsString, out value);

                if (success)
                {
                    bugResult.Changed = value;
                }

                if (bugElement.GetElementsByTagName("resolution").Count == 1)
                {

                    bugResult.Resolution = bugElement.GetElementsByTagName("resolution")[0].InnerText;
                }

                if (bugElement.GetElementsByTagName("target_milestone").Count == 1)
                {

                    bugResult.Milestone = bugElement.GetElementsByTagName("target_milestone")[0].InnerText;
                }

                if (bugElement.GetElementsByTagName("status_whiteboard").Count == 1)
                {

                    bugResult.StatusWhiteboard = bugElement.GetElementsByTagName("status_whiteboard")[0].InnerText;
                }

                if (bugElement.GetElementsByTagName("dependson").Count == 1)
                {

                    bugResult.DependsOn = bugElement.GetElementsByTagName("dependson")[0].InnerText;
                }

                if (bugElement.GetElementsByTagName("token").Count >= 1)
                {

                    bugResult.Token = bugElement.GetElementsByTagName("token")[0].InnerText;
                }

                if (bugElement.GetElementsByTagName("blocked").Count == 1)
                {

                    bugResult.Blocks = bugElement.GetElementsByTagName("blocked")[0].InnerText;
                }

                if (bugElement.GetElementsByTagName("cc").Count >= 1)
                {
                    for (int i = 0; i < bugElement.GetElementsByTagName("cc").Count; i++)
                    {
                        string strCC = bugElement.GetElementsByTagName("cc")[i].InnerText;

                        if (!string.IsNullOrEmpty(strCC))
                        {
                            bugResult.CC.Add(strCC);

                        }
                    }

                }

                //getting comments (first comment is the long description): "Description" is the label from the web interface
                int nrOfComments = bugElement.GetElementsByTagName("who").Count;
                for (int i = 0; i < nrOfComments; i++)
                {
                    comment = String.Empty;
                    comment = bugElement.GetElementsByTagName("who")[i].InnerText;
                    comment += " , " + bugElement.GetElementsByTagName("bug_when")[i].InnerText;
                    comment += " , " + bugElement.GetElementsByTagName("thetext")[i].InnerText.Replace("System.Collections.Generic.List`1[System.String]", String.Empty);

                    bugResult.Comment.Add(comment);
                }


                //getting comments (first comment is the long description): "Description" is the label from the web interface
                int nrOfAttachments = bugElement.GetElementsByTagName("attachment").Count;
                for (int i = 0; i < nrOfAttachments; i++)
                {
                    Attachment newAttachment = new Attachment();

                    newAttachment.AttachmentId = int.Parse(bugElement.GetElementsByTagName("attachid")[i].InnerText);

                    //replace PST(some database datetime values particularity) with  empty string
                    string attachmentCreatedOn = bugElement.GetElementsByTagName("date")[i].InnerText;

                    attachmentCreatedOn = dateFieldEnding.Replace(attachmentCreatedOn, string.Empty);

                    success = DateTime.TryParse(attachmentCreatedOn, out value);

                    if (success)
                        newAttachment.Created = DateTime.Parse(attachmentCreatedOn);

                    newAttachment.Description = bugElement.GetElementsByTagName("desc")[i].InnerText;
                    if (bugElement.GetElementsByTagName("ctype")[i] != null)
                    {
                        newAttachment.ContentType = bugElement.GetElementsByTagName("ctype")[i].InnerText;
                    }
                    if (bugElement.GetElementsByTagName("type")[i] != null)
                    {
                        newAttachment.ContentType = bugElement.GetElementsByTagName("type")[i].InnerText;
                    }
                    if (bugElement.GetElementsByTagName("size")[i] != null)
                    {
                        newAttachment.Size = long.Parse(bugElement.GetElementsByTagName("size")[i].InnerText);
                    }
                    if (bugElement.GetElementsByTagName("filename")[i] != null)
                    {
                        newAttachment.FileName = bugElement.GetElementsByTagName("filename")[i].InnerText;
                    }

                    newAttachment.Operation = AttachmentOperation.Unchanged;

                    bugResult.Attachments.Add(newAttachment);
                }

                #endregion
            }catch(Exception ex){
                bugResult.ErrorMessage = ex.Message;

                MyLogger.WriteWithStack(ex.Message, "GetBug", LoggingCategory.Exception);
            }

            return bugResult;
        }

        private string AddBugForVersion_2_0( MyZilla.BusinessEntities.Bug bug, string myZillaUrl, string charset)
        {
            string result = string.Empty;

            string url = myZillaUrl + UPDATE_BUG_2_0_PAGE;

            MyZilla.BL.Utils.HttpHelper httpDialog = new HttpHelper(_connectionId, charset);

            string dataToPost = httpDialog.PostHttpRequest_AddBug(bug);

            string htmlContent = httpDialog.PostToUrl(url, dataToPost, false);

#if DEBUG
            MyZilla.BL.Interfaces.Utils.htmlContents = htmlContent;
#endif

            result = ValidateAddBug(htmlContent);

            return result;

        }

        private string AddBugForVersion_3_0(MyZilla.BusinessEntities.Bug bug, string myZillaUrl, string charset)
        {

            string result = string.Empty;
 
            MyZilla.BL.Utils.HttpHelper httpDialog = new HttpHelper(_connectionId, charset);

            string htmlContent =  httpDialog.PostMultipartWhenAddingBug(bug, myZillaUrl );

#if DEBUG
            MyZilla.BL.Interfaces.Utils.htmlContents = htmlContent;
#endif

            result = ValidateAddBug(htmlContent);

            return result;
      
 
        }

        private static string ValidateAddBug(string htmlContent)
        {
            string result = string.Empty;

            int pos1 = htmlContent.IndexOf("<title>");
            int pos2 = htmlContent.IndexOf("</title>");

            string strTitle = htmlContent.Substring(pos1 + "<title>".Length, pos2 - (pos1 - 1 + "</title>".Length));
            
            Regex addBug = new Regex(@"(?<bug_number>[(0-9)]+) submitted", RegexOptions.IgnoreCase);

            Match match = addBug.Match(strTitle);

            if (match.Success == true)
            {

                string bugNo = match.Groups["bug_number"].ToString();

                result = string.Format(Resource.MsgInsBugSuccessfully, bugNo);
            }
            else
            {
                //signal the bugzilla error
                throw new CustomException(null, Resource.MsgInsBugFailed + Environment.NewLine + strTitle + ".");
            }

            return result;

        }


        #endregion

    }
}
