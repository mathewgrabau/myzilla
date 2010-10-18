using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Drawing; 

using MyZilla.BusinessEntities;
using Tremend.Logging;



namespace MyZilla.BL.Utils
{
    /// <summary>
    /// Upload and download data for a specific Uniform Resource Identifier (URI). 
    /// </summary>
    /// <remarks> Use HttpWebRequest and HttpWebResponse classes from System.Net namespace.</remarks> 
    public class HttpHelper
    {
        private const string ATTACHMENT_PAGE = "attachment.cgi";
        private const string UPDATE_BUG_PAGE = "post_bug.cgi";

        // use with HttpWebRequest and HttpWebRequest
        public static CookieCollection colCookies = new CookieCollection ();

        public CookieManager cookieManager = CookieManager.Instance();

        private int _userID = -1;

        private string _charset;

        #region Constructors

        private HttpHelper() { }

        public HttpHelper(int userId, string charset) {

            this._userID = userId;
            this._charset = charset;
        }

        #endregion

        #region Public methods

        public string BugzillaCharset {
            get {
                return this._charset;
            }
            set {
                this._charset = value;
            }
        }

        /// <summary>
        /// It is a regulary POST, without a response. 
        /// Used for optimizing code.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="logIn"></param>
        public HttpWebResponse PostToUrlWhenLogOn (string url, string data, bool logIn)
        {
            HttpWebResponse response = null;

            try
            {
#if DEBUG
                System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
#endif

                string username = String.Empty;
                string password = String.Empty;

                url = HttpHelper.GetHTTPAuthenticationInfo(url, out username, out password);

                // Create the Web Request Object 
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

                // Specify that you want to POST data
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

                //httpauthentication
                byte[] credentialBuffer = new UTF8Encoding().GetBytes(
                username + ":" +
                password);
                request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(credentialBuffer);
                
                CookieContainer cc = new CookieContainer();

                cc.Add (new CookieCollection ());

                request.CookieContainer = cc;

                if (url != null)
                {
                    // write out the data to the web server
                    WriteToUrl(request, data);
                }
                else
                {
                    request.ContentLength = 0;
                }

                // Get the Web Response Object from the request
                response = (HttpWebResponse)request.GetResponse();

#if DEBUG
                watch.Stop();

                MyLogger.Write(request.RequestUri + " took " + watch.ElapsedMilliseconds.ToString(), "PostToUrlWhenLogOn", LoggingCategory.Debug);
#endif

                return response;

            }
            catch (Exception ex)
            {
                MyLogger.Write(ex.Message, "PostToUrlWhenLogOn", LoggingCategory.Exception);
                throw ex;
            }

        }

        public static string GetHTTPAuthenticationInfo(string Url, out string Username, out string Password) {
            string url = Url;

            Username = String.Empty;
            Password = String.Empty;

            string startSeparator = "://";
            char middleSeparator = ':';
            char endSeparator = '@';
            char queryStringDelimiter = '?';

            int indexQueryStringDelimiter = Url.IndexOf(queryStringDelimiter);

            int indexStartSeparator = Url.IndexOf(startSeparator, 0);
            int indexMiddleSeparator = Url.IndexOf(middleSeparator, indexStartSeparator + startSeparator.Length);

            if (indexMiddleSeparator > 0)
            {

                int indexEndSeparator = Url.IndexOf(endSeparator, indexMiddleSeparator);

                if (indexEndSeparator > indexMiddleSeparator && (indexQueryStringDelimiter>0 && indexQueryStringDelimiter>indexEndSeparator || indexQueryStringDelimiter<0 ))
                {
                    Username = Url.Substring(indexStartSeparator + startSeparator.Length, indexMiddleSeparator - indexStartSeparator - startSeparator.Length);
                    Password = Url.Substring(indexMiddleSeparator + 1, indexEndSeparator - indexMiddleSeparator - 1);

                    string httpPart = Url.Substring(indexStartSeparator + startSeparator.Length, indexEndSeparator - indexStartSeparator - startSeparator.Length + 1);

                    url = Url.Replace(httpPart, String.Empty);
                }
            }

            return url;
        }

        /// <summary>
        /// Forces a POST of data to a specified URL.
        /// A HTTP POST is a combination of a write to the Web Server 
        /// and an immediate read from the Web server.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="logIn"></param>
        /// <returns></returns>
        public string PostToUrl(string url, string data, bool logIn)
        {
            string username = String.Empty;
            string password = String.Empty;

            url = HttpHelper.GetHTTPAuthenticationInfo(url, out username, out password);

            // Create the Web Request Object 
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            // Specify that you want to POST data
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8");

            //httpauthentication
            byte[] credentialBuffer = new UTF8Encoding().GetBytes(
            username + ":" +
            password);
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(credentialBuffer);

            CookieContainer cc = new CookieContainer();

            CookieCollection cCollection = cookieManager.GetCookieCollectionByUserId(_userID);

            if (cCollection != null)
            {
                cc.Add(cCollection);
            }
            else
            {
                cc.Add(new CookieCollection());
            }

            request.CookieContainer = cc;

            this.LogCookieCollection(cc.GetCookies(request.RequestUri));

            if (url != null)
            {
                // write out the data to the web server
                WriteToUrl(request, data);
            }
            else
            {
                request.ContentLength = 0;
            }

            // read the response from the Web Server
            string htmlContent = RetrieveFromUrl(request, logIn, this._charset);

            return htmlContent;

        }

        public string  PostAttachment ( Attachment objAtt, string url)
        {
           string result = string.Empty  ;

           try
           {
               string postURL = String.Concat(url, ATTACHMENT_PAGE);

               MultipartForm mPost = new MultipartForm(postURL , _userID );

               mPost.SetField("bugid", objAtt.BugId.ToString());
               mPost.SetField("action", "insert");
               mPost.SetField("description", objAtt.Description);
               mPost.SetField("contenttypemethod", "list");
               mPost.SetField("contenttypeselection", objAtt.ContentType);
               mPost.SetField("contenttypeentry", string.Empty);
               mPost.SetField("comment", objAtt.Comment);
               mPost.FileContentType = objAtt.ContentType;
               
               mPost.SendFile(objAtt.FileName);

               result = mPost.ResponseText.ToString();
           }
           catch (Exception ex)
           {
               MyLogger.WriteWithStack(ex.Message, "PostToUrlWhenLogOn", LoggingCategory.Exception);

               throw;
           }

           return result ;

        }

        public string PostMultipartWhenAddingBug(Bug addedBug, string myzillaUrl)
        {
            string result = string.Empty;

            try
            {
                string url = String.Concat(myzillaUrl, UPDATE_BUG_PAGE);

                MultipartForm mPost = new MultipartForm(url, _userID );

                mPost.SetField("product", HttpUtility.UrlEncode(addedBug.Product));
                mPost.SetField("version", HttpUtility.UrlEncode(addedBug.Version));
                mPost.SetField("component", HttpUtility.UrlEncode(addedBug.Component));
                mPost.SetField("bug_severity", HttpUtility.UrlEncode(addedBug.Severity));
                mPost.SetField("rep_platform", HttpUtility.UrlEncode(addedBug.Hardware));
                mPost.SetField("priority", HttpUtility.UrlEncode(addedBug.Priority));
                mPost.SetField("op_sys", HttpUtility.UrlEncode(addedBug.OS));

                if (!string.IsNullOrEmpty(addedBug.Milestone))
                    mPost.SetField("target_milestone", HttpUtility.UrlEncode(addedBug.Milestone));

                mPost.SetField("bug_status", HttpUtility.UrlEncode(addedBug.Status));
                mPost.SetField("assigned_to", HttpUtility.UrlEncode(addedBug.AssignedTo));

                // can be 1 cc
                if (addedBug.CC != null && addedBug.CC.Count == 1)
                {
                    mPost.SetField("cc", HttpUtility.UrlEncode(addedBug.CC[0])); 
                }
                mPost.SetField("bug_file_loc", HttpUtility.UrlEncode(addedBug.Url));
                mPost.SetField("short_desc", HttpUtility.UrlEncode(addedBug.Summary));
                if (addedBug.Comment != null && addedBug.Comment.Count >= 1)
                {
                    mPost.SetField("comment", HttpUtility.UrlEncode(addedBug.Comment[0]) ); 
                }

                mPost.SetField("dependson", HttpUtility.UrlEncode(addedBug.DependsOn));
                mPost.SetField("blocked", HttpUtility.UrlEncode(addedBug.Blocks));
                mPost.SetField("form_name", "enter_bug");

                if (addedBug.Attachments.Count > 0)
                {
                    mPost.SetField("contenttypemethod", "autodetect");
                    mPost.SetField("contenttypeselection", "text/plain"); 
                    mPost.SetField("contenttypeentry", String.Empty);
                    mPost.SetField("description", HttpUtility.UrlEncode(addedBug.Attachments[0].Description)); 
                    mPost.FileContentType = addedBug.Attachments[0].ContentType;
                    mPost.SendFile(addedBug.Attachments[0].FileName);   

                }
                else
                {
                    mPost.SendFile(String.Empty);
                }

                result = mPost.ResponseText.ToString();

            }
            catch (Exception ex)
            {
                MyLogger.WriteWithStack(ex.Message, "PostMultipartWhenAddingBug", LoggingCategory.Exception);

                throw;

            }

            return result;
        }

        /// <summary>
        /// Retrieves the contents of a specified URL in response to a request.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="logIn"></param>
        /// <returns></returns>
        public string GetFromUrl(string url, bool doLogOn, int timeout)
        {
            string username = String.Empty;
            string password = String.Empty;

            HttpHelper.GetHTTPAuthenticationInfo(url, out username, out password);

            // 1. Create the Web Request Object          
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            request.Timeout = timeout;

            request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8");

            //httpauthentication
            byte[] credentialBuffer = new UTF8Encoding().GetBytes(
            username + ":" +
            password);
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(credentialBuffer);

            CookieContainer cc = new CookieContainer();

            string htmlContent = string.Empty;

            try
            {

                CookieCollection cCollection = cookieManager.GetCookieCollectionByUserId(_userID);

                if (cCollection != null)
                {
                    cc.Add(cCollection);
                }
                else
                {
                    cc.Add(new CookieCollection());
                }

                this.LogCookieCollection(cc.GetCookies(request.RequestUri));

                request.CookieContainer = cc;

                htmlContent = RetrieveFromUrl(request, doLogOn, this._charset);

            }
            catch { }

            return htmlContent;

        }

        /// <summary>
        /// Retrieves the contents of a specified URL in response to a request.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="logIn"></param>
        /// <returns></returns>
        public string GetFromUrl(string url, bool logIn)
        {
            string username = String.Empty;
            string password = String.Empty;

            url = HttpHelper.GetHTTPAuthenticationInfo(url, out username, out password);

#if DEBUG
            MyLogger.Write(String.Format("Start HttpGet from {0}", url), "GetFromUrl", LoggingCategory.General);
#endif

            // 1. Create the Web Request Object          
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            request.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8");

            //httpauthentication
            byte[] credentialBuffer = new UTF8Encoding().GetBytes(
            username + ":" +
            password);
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(credentialBuffer);

            CookieContainer cc = new CookieContainer();

            string htmlContent = string.Empty;

            try
            {

                CookieCollection cCollection = cookieManager.GetCookieCollectionByUserId(_userID);

                if (cCollection != null)
                {
                    cc.Add(cCollection);
                }
                else
                {
                    cc.Add(new CookieCollection());
                }

                this.LogCookieCollection(cc.GetCookies(request.RequestUri));

                request.CookieContainer = cc;

                htmlContent = RetrieveFromUrl(request, logIn, this._charset);

            }
            catch(Exception ex)
            {
                MyLogger.Write(ex, "GetFromUrl", LoggingCategory.Exception);
                throw ex;
            }
            finally {
#if DEBUG
                MyLogger.Write(String.Format("Complete HttpGet from {0}", url), "GetFromUrl", LoggingCategory.General);
#endif
            }

            return htmlContent;

        }

        /// <summary>
        /// Retrieves the contents of a specified URL in response to a request.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="logIn"></param>
        /// <returns></returns>
        public Bitmap GetPictureFromUrl(string url, bool logIn)
        {

            // 1. Create the Web Request Object          
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            CookieContainer cc = new CookieContainer();

            CookieCollection cCollection = cookieManager.GetCookieCollectionByUserId(_userID);

            if (cCollection != null)
            {
                cc.Add(cCollection);
            }
            else
            {
                cc.Add(new CookieCollection());
            }

            request.CookieContainer = cc;

            Bitmap bmp = RetrievePictureFromUrl (request, logIn);

            return bmp ;

        }

        public string  GetStreamFormUrl(string url, out string errorMessage )
        {
            string strFullPath = string.Empty; 
 
            // 1. Create the Web Request Object          
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            CookieContainer cc = new CookieContainer();

            CookieCollection cCollection = cookieManager.GetCookieCollectionByUserId(_userID);

            if (cCollection != null)
            {
                cc.Add(cCollection);
            }
            else
            {
                cc.Add(new CookieCollection());
            }

            request.CookieContainer = cc;

            strFullPath = RetrieveStreamFromUrl(request, out errorMessage);

            return strFullPath;

        }

        /// <summary>
        /// Strip HTML tags from a specified string.
        /// </summary>
        /// <param name="htmlContent">String whose HTML tags are to be stripped</param>
        /// <returns></returns>
        public string StripHtml(string htmlContent)
        {
            string result;

            Regex objRegExp = new Regex ("<(.|\n)+?>", RegexOptions.IgnoreCase)  ;

            // Replace all HTML tag matches with the empty string
            result =  objRegExp.Replace(htmlContent, String.Empty);

            // Replace all < and > with &lt; and &gt;
            result = result.Replace("<", "&lt;");

            result = result.Replace(">", "&gt;");

            return result;

        }

        /// <summary>
        /// Strip HTML tags from a specified string.
        /// </summary>
        /// <param name="htmlContent">String whose HTML tags are to be stripped</param>
        /// <param name="StartString">Start string to search for in the HTML</param>
        /// <param name="EndString">End string to search for in the HTML</param>
        /// <param name="StartPosInHTML">Start index to search for in the HTML</param>
        /// <returns></returns>
        private string GetHtml(string htmlContent, string StartString, string EndString, int StartPosInHTML, out int Pos)
        {
            string result = String.Empty;
            Pos = 0;

            int startPos = htmlContent.IndexOf(StartString, StartPosInHTML);
            if (startPos>0){
                int endPos = htmlContent.IndexOf(EndString, startPos);

                Pos = endPos + EndString.Length;

                result = htmlContent.Substring(startPos, Pos - startPos );
            }
            return result;
        }

        public string GetHtml(string htmlContent, string startString, string endString)
        {
            int index = 0;
            return GetHtml(htmlContent, startString, endString, 0, out index);
        }

        public string GetNextRowInTable(string htmlTable, ref int index) {
            int endIndex = 0;
            //int _pos = Pos;
            string htmlRow = GetHtml(htmlTable, "<tr", "</tr>", index, out endIndex);
            
            index = endIndex;

            return htmlRow;
        }

        private string GetNextColumnInTableRow(string htmlRow, string columnType, ref int index)
        {
            int endIndex = 0;
            //int _pos = Pos;
            string htmlColumn = GetHtml(htmlRow, "<" + columnType, "</" + columnType + ">", index, out endIndex);

            index = endIndex;

            return htmlColumn;
        }

        public string[] GetValuesFromTableRow(string htmlRow, string rowType) {
            string[] values = new string[50];
            int Pos = 0;
            int i = 0;
            string col = String.Empty;
            while ((col = GetNextColumnInTableRow(htmlRow, rowType, ref Pos)).Length > 0) {
                values.SetValue(StripHtml(col).Replace("\n", String.Empty).Trim(), i);
                i++;
            }

            return values;
        }

        /// <summary>
        /// Parse the Selection HTML tag.
        /// </summary>
        /// <param name="strSelection">The string to be parsed.</param>
        /// <returns>Returns a collection of pairs (value, text) for each option in selection TAG </returns>
        public static List<string> GetOptionsFormSelection(string data)
        {
            // <option value="Administration Interface">Administration Interface</option>
            // value = "Administration Interface"; text = "Administration Interface"
            const string BEGIN_OPTION_VALUE = "option value=";
            const string END_OPTION_VALUE = ">";
            const string END_OPTION_TEXT = "</option>";
            const string DELIMITATOR = "\"";

            int pos = 0;
            int pos1 = 0;
            int pos2 = 0;
            int pos3 = 0;
            int p1 = 0;
            int p2 = 0;
            string optionValue = string.Empty;
            string optionText = string.Empty;

            List<string> result = new List<string>();

            while (pos < data.Length)
            {
                pos1 = data.IndexOf(BEGIN_OPTION_VALUE, pos);

                if (pos1 != -1)
                {

                    pos2 = data.IndexOf(END_OPTION_VALUE, pos1);

                    optionValue = data.Substring(pos1 + BEGIN_OPTION_VALUE.Length, pos2 - (pos1 + BEGIN_OPTION_VALUE.Length));

                    p1 = optionValue.IndexOf(DELIMITATOR);
                    p2 = optionValue.IndexOf(DELIMITATOR, p1 + DELIMITATOR.Length);

                    optionValue = optionValue.Substring(p1 + DELIMITATOR.Length, p2 - (p1 + DELIMITATOR.Length));

                    pos3 = data.IndexOf(END_OPTION_TEXT, pos2);

                    optionText = data.Substring(pos2 + END_OPTION_VALUE.Length, pos3 - (pos2 + END_OPTION_VALUE.Length));
                    optionText = optionText.TrimEnd(new char[] { ' ', '\n'});
                    pos = pos3 + END_OPTION_TEXT.Length;

                    result.Add(optionValue + "," + optionText);
                }
                else
                {

                    // end of string
                    break;
                }
            }

            return result;

        }

        #endregion
        
        #region Private methods

        /// <summary>
        /// Writes the content of a specified URL to the web
        /// </summary>
        /// <param name="request"></param>
        /// <param name="data"></param>
        private void WriteToUrl(WebRequest request, string data)
        {
            Stream outputStream = null;

            try
            {
#if DEBUG
                System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
#endif
                //Accept SSL certificate automatically
                System.Net.ServicePointManager.ServerCertificateValidationCallback =
                    delegate(Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
                    {
                        // Replace this line with code to validate server certificate.
                        return true;
                    };

                byte[] bytes = null;
                // Get the data that is being posted (or sent) to the server
                bytes = System.Text.Encoding.GetEncoding(this._charset).GetBytes(data);

                request.ContentLength = bytes.Length;

                // 1. Get an output stream from the request object
                outputStream = request.GetRequestStream(); //long time
                
#if DEBUG
                watch.Stop();
                MyLogger.Write("GetRequestStream for " + request.RequestUri + " took " + watch.ElapsedMilliseconds.ToString(), "WriteToUrl", LoggingCategory.Debug);
#endif
                
                // 2. Post the data out to the stream
                outputStream.Write(bytes, 0, bytes.Length);

#if DEBUG
                watch.Stop();
                MyLogger.Write(request.RequestUri + " took " + watch.ElapsedMilliseconds.ToString(), "WriteToUrl", LoggingCategory.Debug);
#endif



            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "WriteToUrl", LoggingCategory.Exception); 

                throw;
            }
            finally
            {
                // 3. Close the output stream and send the data out to the web server

                if (outputStream != null)
                {
                    outputStream.Close();
                }
            }
        }

        /// <summary>
        /// Retrieves the content of a specified URL in a response to a request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="logIn"></param>
        /// <returns></returns>
        private string RetrieveFromUrl(HttpWebRequest request, bool logIn, string charset)
        {

            StreamReader reader = null;

            string strContent = string.Empty;

            HttpWebResponse response = null;

            Stream responseStream = null;

 
            try
            {
                #if DEBUG
                    System.Diagnostics.Stopwatch watch = System.Diagnostics.Stopwatch.StartNew();
                #endif

                //Accept SSL certificate automatically
                System.Net.ServicePointManager.ServerCertificateValidationCallback =
                    delegate(Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
                    {
                        // Replace this line with code to validate server certificate.
                        return true;
                    };

                // 1. Get the Web Response Object from the request
                response = (HttpWebResponse)request.GetResponse();

                // 2. Get the Stream Object from the response
                responseStream = response.GetResponseStream();

                CookieManager.AddNewCookieContainer(_userID, response.Cookies);

                // 3. Create a stream reader and associate it with the stream object
                //reader = new StreamReader(responseStream, Encoding.GetEncoding(string.IsNullOrEmpty(response.CharacterSet) ? "utf-8" : response.CharacterSet));
                reader = new StreamReader(responseStream, Encoding.GetEncoding(string.IsNullOrEmpty(charset) ? "utf-8" : charset));

                if (!string.IsNullOrEmpty(response.CharacterSet))
                    this._charset = response.CharacterSet;

                // 4. read the entire stream 
                strContent = reader.ReadToEnd();

#if DEBUG
                watch.Stop();
                MyLogger.Write("END " + request.RequestUri + " " + watch.ElapsedMilliseconds.ToString(), "RetriveFromUrl", LoggingCategory.Debug);
#endif
                charset = response.CharacterSet;

                return strContent;

            }
            catch (Exception ex)
            {
                MyLogger.WriteWithStack(ex + "; " + request.RequestUri.OriginalString, "RetriveFromUrl",  LoggingCategory.Exception); 

                throw;
            }
            finally 
            {

                if (response != null)
                {
                    if (request.RequestUri.OriginalString.Contains("id=-1&ctype=xml"))
                        MyLogger.Write(response.Server, "RetriveFromUrl", LoggingCategory.Debug);
 
                    response.Close(); 
                }

                if (responseStream != null)
                {
                    responseStream.Close(); 
                }

                if (reader != null)
                {
                    reader.Close();

                }


            }
        }

        private Bitmap  RetrievePictureFromUrl(HttpWebRequest request, bool logIn)
        {

            Bitmap  BmpContent = null;

            HttpWebResponse response = null;

            Stream responseStream = null;


            try
            {
                // 1. Get the Web Response Object from the request
                response = (HttpWebResponse)request.GetResponse();

                // 2. Get the Stream Object from the response
                responseStream = response.GetResponseStream();

                BmpContent = new Bitmap(responseStream);

                return BmpContent;

            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "RetrivePictureFromUrl", LoggingCategory.Exception);

                throw;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }

                if (responseStream != null)
                {
                    responseStream.Close();
                }
            }
        }

        private string RetrieveStreamFromUrl(HttpWebRequest request, out string errorMessage)
        {

            HttpWebResponse response = null;

            Stream localStream = null;

            errorMessage = string.Empty;

            string strFullPath = string.Empty; 


            try
            {
                // 1. Get the Web Response Object from the request
                response = (HttpWebResponse)request.GetResponse();

                // get from the response the name of the file
                int pos1 = response.ContentType.IndexOf("\"");

                int pos2 = response.ContentType.IndexOf("\"", pos1+1);

                string fileName = response.ContentType.Substring(pos1+1, pos2 - pos1 - 1);  

                // 2. Get the Stream Object from the response
                Stream responseStream = response.GetResponseStream();

                strFullPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +"\\" +  fileName ;

                localStream = File.Create(strFullPath);

                byte[] buffer = new byte[1024];

                int bytesRead;

                int bytesProcessed = 0;


                // Simple do/while loop to read from stream until
                // no bytes are returned
                do
                {
                    // Read data (up to 1k) from the stream
                    bytesRead = responseStream.Read(buffer, 0, buffer.Length);

                    // Write the data to the local file
                    localStream.Write(buffer, 0, bytesRead);

                    // Increment total bytes processed
                    bytesProcessed += bytesRead;
                }
                while (bytesRead > 0);

            }
            catch (Exception ex)
            {
                MyLogger.Write(ex, "RetriveStreamFromUrl", LoggingCategory.Exception);

                errorMessage = ex.Message;

                strFullPath = string.Empty; 

            }
            finally
            {

                if (response != null)
                {
                    response.Close();
                }

                if (localStream != null)
                {
                    localStream.Close(); 
                }
                    

            }

            return strFullPath;
        }

        private void LogCookieCollection(CookieCollection cookies)
        {

#if DEBUG
            foreach (Cookie cookie in cookies)
            {
                MyLogger.Write(cookie.Name + ": " + cookie.Value, "LogCookieCollection", LoggingCategory.Debug);
            }
#endif
        }

        #endregion

        #region Build data for POST request.

        public string BuildBugzillaLogOnData(string userName, string password)
        {
            //string data = string.Format("Bugzilla_login={0}&Bugzilla_password={1}", userName, password);
            string data = string.Format("Bugzilla_login={0}&Bugzilla_password={1}&GoAheadAndLogIn=Login&GoAheadAndLogIn=1", userName, password);

            return data;
        }

        public string PostHttpRequest_UpdateBug(MyZilla.BusinessEntities.Bug bug, string bugzillaVersion)
        {
            StringBuilder sb = new StringBuilder();

            //Encoding bugzillaEncoding = Encoding.GetEncoding("utf-8");
            Encoding bugzillaEncoding = Encoding.GetEncoding(this._charset);

            #region Build data to be posted
            
            if (bugzillaVersion.StartsWith("2.18"))
                sb.AppendFormat("&delta_ts={0}", bug.Changed.ToString("yyyyMMddHHmmss"));
            else
                sb.AppendFormat("&delta_ts={0}", HttpUtility.UrlEncode(bug.Changed.ToString("yyyy-MM-dd HH:mm:ss"), bugzillaEncoding));

            sb.AppendFormat("&longdesclength={0}", 1); // mmi
            sb.AppendFormat("&id={0}", bug.Id);
            sb.AppendFormat("&token={0}", bug.Token);
            sb.AppendFormat("&alias={0}", "");
            sb.AppendFormat("&short_desc={0}", HttpUtility.UrlEncode(bug.Summary, bugzillaEncoding));
            sb.AppendFormat("&product={0}", HttpUtility.UrlEncode(bug.Product, bugzillaEncoding));
            sb.AppendFormat("&component={0}", HttpUtility.UrlEncode(bug.Component, bugzillaEncoding));
            sb.AppendFormat("&rep_platform={0}", HttpUtility.UrlEncode(bug.Hardware, bugzillaEncoding));
            sb.AppendFormat("&op_sys={0}", HttpUtility.UrlEncode(bug.OS, bugzillaEncoding));
            sb.AppendFormat("&version={0}", HttpUtility.UrlEncode(bug.Version, bugzillaEncoding));
            sb.AppendFormat("&priority={0}", HttpUtility.UrlEncode(bug.Priority, bugzillaEncoding));
            sb.AppendFormat("&bug_severity={0}", HttpUtility.UrlEncode(bug.Severity, bugzillaEncoding));
            sb.AppendFormat("&assigned_to={0}", HttpUtility.UrlEncode(bug.AssignedTo, bugzillaEncoding));
            sb.AppendFormat("&qa_contact={0}", HttpUtility.UrlEncode("", bugzillaEncoding));
            sb.AppendFormat("&target_milestone={0}", HttpUtility.UrlEncode(bug.Milestone, bugzillaEncoding));
            sb.AppendFormat("&bug_file_loc={0}", HttpUtility.UrlEncode(bug.Url, bugzillaEncoding));
            sb.AppendFormat("&status_whiteboard={0}", HttpUtility.UrlEncode(bug.StatusWhiteboard, bugzillaEncoding));
            sb.AppendFormat("&keywords={0}", HttpUtility.UrlEncode("", bugzillaEncoding));
            sb.AppendFormat("&dependson={0}", bug.DependsOn );
            sb.AppendFormat("&blocked={0}", bug.Blocks );
            sb.AppendFormat("&newcc={0}", HttpUtility.UrlEncode("", bugzillaEncoding));
            sb.AppendFormat("&cf_custom_field={0}", HttpUtility.UrlEncode("", bugzillaEncoding));
            sb.AppendFormat("&defined_cf_colo={0}", HttpUtility.UrlEncode("", bugzillaEncoding));
            sb.AppendFormat("&cf_color={0}", HttpUtility.UrlEncode("---", bugzillaEncoding));
            sb.AppendFormat("&cf_datetime={0}", HttpUtility.UrlEncode("", bugzillaEncoding));

            sb.AppendFormat("&flag_type-8={0}", HttpUtility.UrlEncode("X", bugzillaEncoding));
            sb.AppendFormat("&flag_type-9={0}", HttpUtility.UrlEncode("X", bugzillaEncoding));
            sb.AppendFormat("&flag_type-11={0}", HttpUtility.UrlEncode("X", bugzillaEncoding));
            sb.AppendFormat("&flag_type-10={0}", HttpUtility.UrlEncode("X", bugzillaEncoding));
            sb.AppendFormat("&flag_type-6={0}", HttpUtility.UrlEncode("X", bugzillaEncoding));

            sb.AppendFormat("&estimated_time={0}", HttpUtility.UrlEncode("0.0", bugzillaEncoding));
            sb.AppendFormat("&work_time={0}", HttpUtility.UrlEncode("0", bugzillaEncoding));
            sb.AppendFormat("&remaining_time={0}", HttpUtility.UrlEncode("0.0", bugzillaEncoding));

            sb.AppendFormat("&deadline={0}", HttpUtility.UrlEncode("", bugzillaEncoding));

            sb.AppendFormat("&comment={0}", HttpUtility.UrlEncode(bug.Comment[bug.Comment.Count - 1], bugzillaEncoding));

            sb.AppendFormat("&knob={0}", bug.Knob ); // mmi
            sb.AppendFormat("&resolution={0}", HttpUtility.UrlEncode(bug.Resolution, bugzillaEncoding));

            sb.AppendFormat("&bug_status={0}", HttpUtility.UrlEncode(bug.Status, bugzillaEncoding));

            sb.AppendFormat("&addtonewgroup={0}", "yesifinold");

            //&estimated_time=0.0
            //&work_time=0
            //&remaining_time=0.0
            //&deadline=
            //&comment=ddd
            //&bug_status=NEW
            
            //&dup_id=
            //&defined_isprivate_13662=1
            //&defined_isprivate_13724=1

            if (bug.DuplicateBug != null)
            {
                sb.AppendFormat("&dup_id={0}", bug.DuplicateBug);
            }

            #region CC
            if (bug.CC != null &&  bug.CC.Count > 0)
            {
                for (int i = 0; i < bug.CC.Count; i++ )
                {
                    string itemCC = bug.CC[i];

                    string[] elements = itemCC.Split(',');

                    if (elements.Length == 2)
                    {
                        switch (elements[1])
                        {
                            case "removecc":
                                // cc=mzavoi%40tremend.ro&removecc=on
                                sb.AppendFormat("&cc={0}&removecc=on", HttpUtility.UrlEncode(elements[0], bugzillaEncoding));
                                break;
                            case "newcc":
                                sb.AppendFormat("&newcc={0}", HttpUtility.UrlEncode(elements[0], bugzillaEncoding));
                                break;

                        }
                    }
                    else
                    {
                        // no code here.
                    }
                }
            }
            #endregion

            
            
            #endregion

            // spaces have to be replaced bu "+"
            string dataToPost = sb.Replace(" ", "+").ToString();

            return dataToPost ;

        }

        /// <summary>
        /// Build POST data for updating priority, severity or Reassign to
        /// </summary>
        /// <param name="lstBugs"></param>
        /// <param name="propBugsToUpdate"></param>
        /// <returns></returns>
        public string PostHttpRequest_UpdateBugsBulk(SortedList bugsIdList, Bug bugValuesToUpdate)
        {
            StringBuilder sb = new StringBuilder();
            string strUpdate = string.Empty;
            string strToPost = string.Empty;

            foreach (object key in bugsIdList.Keys) {
                sb.Append("id_" + key.ToString() + "=on&");
            }

            //for (int i = 0; i < bugsIdList.Count;i++ )
            //{
            //    //sb.Append("id_" + bugsIdList[i].ToString() + "=on&");
            //}


            strUpdate = "dontchange=--do_not_change--&product=--do_not_change--&version=--do_not_change--&rep_platform=--do_not_change--&priority={0}&component=--do_not_change--&bug_severity={3}&masscc=&ccaction=add&comment=&bit-14=-1&bit-17=-1&bit-16=-1&bit-18=-1&bit-15=-1&bit-12=-1&bit-13=-1&bit-19=-1&knob={1}&resolution={2}&assigned_to={4}";

            if (String.IsNullOrEmpty(bugValuesToUpdate.Priority))
            {
                bugValuesToUpdate.Priority = "--do_not_change--";
            }


            if (String.IsNullOrEmpty(bugValuesToUpdate.Severity))
            {
                bugValuesToUpdate.Severity = "--do_not_change--";
            }

            if (String.IsNullOrEmpty(bugValuesToUpdate.ReassignTo))
            {
                bugValuesToUpdate.ReassignTo  = "--do_not_change--"; 
            }

            strToPost = string.Format(strUpdate, bugValuesToUpdate.Priority, bugValuesToUpdate.Knob, bugValuesToUpdate.Resolution, bugValuesToUpdate.Severity, bugValuesToUpdate.ReassignTo  );

            string dataToPost = sb.Append(strToPost).ToString() ;

            return dataToPost;

        }

        public string PostHttpRequest_AddBug(MyZilla.BusinessEntities.Bug bug)
        {
            StringBuilder sb = new StringBuilder();

            //Encoding bugzillaEncoding = Encoding.GetEncoding("utf-8");
            Encoding bugzillaEncoding = Encoding.GetEncoding(this._charset);

            #region Build data to be posted

            sb.AppendFormat("product={0}", HttpUtility.UrlEncode(bug.Product, bugzillaEncoding));
            sb.AppendFormat("&version={0}", HttpUtility.UrlEncode(bug.Version, bugzillaEncoding));
            sb.AppendFormat("&component={0}", HttpUtility.UrlEncode(bug.Component, bugzillaEncoding));
            sb.AppendFormat("&rep_platform={0}", HttpUtility.UrlEncode(bug.Hardware, bugzillaEncoding));
            sb.AppendFormat("&op_sys={0}", HttpUtility.UrlEncode(bug.OS, bugzillaEncoding));
            sb.AppendFormat("&priority={0}", HttpUtility.UrlEncode(bug.Priority, bugzillaEncoding));
            sb.AppendFormat("&bug_severity={0}", HttpUtility.UrlEncode(bug.Severity, bugzillaEncoding));
            if (String.IsNullOrEmpty(bug.Milestone))
            {
                sb.AppendFormat("&target_milestone={0}", "---");
            }
            else
            {
                sb.AppendFormat("&target_milestone={0}", HttpUtility.UrlEncode(bug.Milestone, bugzillaEncoding));
            }
            sb.AppendFormat("&bug_status={0}", HttpUtility.UrlEncode(bug.Status, bugzillaEncoding));
            sb.AppendFormat("&assigned_to={0}", HttpUtility.UrlEncode(bug.AssignedTo, bugzillaEncoding));
            if (bug.CC != null &&  bug.CC.Count != 0 ) 
            {
                foreach (string cc in bug.CC  )
                {
                    sb.AppendFormat ("&cc={0}", HttpUtility.UrlEncode(cc, bugzillaEncoding));
                }
            }
            sb.AppendFormat("&bug_file_loc={0}", HttpUtility.UrlEncode(bug.Url, bugzillaEncoding));
            sb.AppendFormat("&short_desc={0}", HttpUtility.UrlEncode(bug.Summary, bugzillaEncoding));
            if ( bug.Comment != null && bug.Comment.Count  > 0) 
            {
                sb.AppendFormat ("&comment={0}", HttpUtility.UrlEncode(bug.Comment[0], bugzillaEncoding) );
            }

            sb.AppendFormat("&dependson={0}", bug.DependsOn);
            sb.AppendFormat("&blocked={0}", bug.Blocks);

            #endregion

            // blanks have to be replaced with "+"
            return sb.Replace(' ', '+').ToString(); ;


        }

        #endregion


    }
}
