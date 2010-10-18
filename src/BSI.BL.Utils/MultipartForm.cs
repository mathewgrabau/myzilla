using System;
using System.Net;
using System.Text;
using System.IO;
using System.Collections;

namespace MyZilla.BL.Utils
{
/// <summary>
/// Allow the transfer of data files using the W3C's specification
/// for HTTP multipart form data. Microsoft's version has a bug
/// where it does not format the ending boundary correctly.
/// Written by: gregoryp@norvanco.com
/// </summary>
public class MultipartForm
{
    /// <summary>
    /// Holds any form fields and values that you
    /// wish to transfer with your data.
    /// </summary>
    private Hashtable coFormFields;
    /// <summary>
    /// Used mainly to avoid passing parameters to other routines.
    /// Could have been local to sendFile().
    /// </summary>
    protected HttpWebRequest coRequest;
    /// <summary>
    /// Used if we are testing and want to output the raw
    /// request, minus http headers, out to a file.
    /// </summary>
    System.IO.Stream coFileStream;
    /// <summary>
    /// Difined to build the form field data that is being
    /// passed along with the request.
    /// </summary>
    static string CONTENT_DISP = "Content-Disposition: form-data; name=";
    /// <summary>
    /// Allows you to specify the specific version of HTTP to use for
    ///uploads.
    /// The dot NET stuff currently does not allow you to remove the
    ///continue-100 header
    /// from 1.1 and 1.0 currently has a bug in it where it adds the
    ///continue-100. MS
    /// has sent a patch to remove the continue-100 in HTTP 1.0.
    /// </summary>
    public Version TransferHttpVersion
    {get{return coHttpVersion;}set{coHttpVersion=value;}}
    Version coHttpVersion;

    /// <summary>
    /// Used to change the content type of the file being sent.
    /// Currently defaults to: text/xml. Other options are
    /// text/plain or binary
    /// </summary>
    public string FileContentType
    {get{return coFileContentType;}set{coFileContentType=value;}}
    string coFileContentType;
    int _connectionID;

    /// <summary>
    /// Initialize our class for use to send data files.
    /// </summary>
    /// <param name="url">The web address of the recipient of the data
    ///transfer.</param>
    public MultipartForm(string url, int connectionId)
    {
        Url = url;
        coFormFields = new Hashtable();
        ResponseText = new StringBuilder();
        BufferSize = 1024 * 10;
        BeginBoundary = "---------------------------103641785115111";

        TransferHttpVersion = HttpVersion.Version11;
        FileContentType = "text/xml";
        _connectionID = connectionId;
    }
    //---------- BEGIN PROPERTIES SECTION ----------
    string _BeginBoundary;
    /// <summary>
    /// The string that defines the begining boundary of
    /// our multipart transfer as defined in the w3c specs.
    /// This method also sets the Content and Ending
    /// boundaries as defined by the w3c specs.
    /// </summary>
    public string BeginBoundary
    {
    get{return _BeginBoundary;}
    set
    {
    _BeginBoundary =value;
    ContentBoundary = "--" + BeginBoundary;
    EndingBoundary = ContentBoundary + "--" + "\r\n";
    }
    }
    /// <summary>
    /// The string that defines the content boundary of
    /// our multipart transfer as defined in the w3c specs.
    /// </summary>
    protected string ContentBoundary
    {get{return _ContentBoundary;}set{_ContentBoundary=value;}}
    string _ContentBoundary;
    /// <summary>
    /// The string that defines the ending boundary of
    /// our multipart transfer as defined in the w3c specs.
    /// </summary>
    protected string EndingBoundary
    {get{return _EndingBoundary;}set{_EndingBoundary=value;}}
    string _EndingBoundary;
    /// <summary>
    /// The data returned to us after the transfer is completed.
    /// </summary>
    public StringBuilder ResponseText
    {get{return _ResponseText;}set{_ResponseText=value;}}
    StringBuilder _ResponseText;
    /// <summary>
    /// The web address of the recipient of the transfer.
    /// </summary>
    public string Url
    {get{return _URL;}set{_URL = value;}}
    string _URL;
    /// <summary>
    /// Allows us to determine the size of the buffer used
    /// to send a piece of the file at a time out the IO
    /// stream. Defaults to 1024 * 10.
    /// </summary>
    public int BufferSize
    {get{return _BufferSize;}set{_BufferSize = value;}}
    int _BufferSize;
    //---------- END PROPERTIES SECTION ----------
    /// <summary>
    /// Used to signal we want the output to go to a
    /// text file verses being transfered to a URL.
    /// </summary>
    /// <param name="path"></param>
    public void SetFileName(string path)
    {
    coFileStream = new
    System.IO.FileStream(path,FileMode.Create,FileAccess.Write);
    }
    /// <summary>
    /// Allows you to add some additional field data to be
    /// sent along with the transfer. This is usually used
    /// for things like userid and password to validate the
    /// transfer.
    /// </summary>
    /// <param name="key">The form field name</param>
    /// <param name="str">The form field value</param>
    public void SetField(string key, string data)
    {
    coFormFields[key] = data;
    }
    /// <summary>
    /// Determines if we have a file stream set, and returns either
    /// the HttpWebRequest stream of the file.
    /// </summary>
    /// <returns></returns>
    public virtual System.IO.Stream GetStream( )
    {
    System.IO.Stream io;
    if (null == coFileStream)
    {
        CookieContainer cc = new CookieContainer();

        CookieManager cookieManager = CookieManager.Instance();

        CookieCollection cCollection = cookieManager.GetCookieCollectionByUserId(this._connectionID );

        if (cCollection != null)
        {
            cc.Add(cCollection);
        }
        else
        {
            cc.Add(new CookieCollection());
        }


        coRequest.CookieContainer = cc;

        io = coRequest.GetRequestStream();
    }
    else
        io = coFileStream;
    return io;
    }
    /// <summary>
    /// Here we actually make the request to the web server and
    /// retrieve it's response into a text buffer.
    /// </summary>
    public virtual void GetResponse()
    {
    if( null == coFileStream )
    {
    System.IO.Stream io;
    WebResponse oResponse;
    try
    {
    oResponse = coRequest.GetResponse();
    }
    catch(WebException web )
    {
    oResponse = web.Response;
    }
    if( null != oResponse )
    {
    io = oResponse.GetResponseStream();
    StreamReader sr = new StreamReader(io);
    string str;
    ResponseText.Length = 0;
    while( (str = sr.ReadLine()) != null )
    ResponseText.Append(str);
    oResponse.Close();
    }
    else
    throw new Exception("MultipartForm: Error retrieving server response");
    }
    }
    /// <summary>
    /// Transmits a file to the web server stated in the
    /// URL property. You may call this several times and it
    /// will use the values previously set for fields and URL.
    /// </summary>
    /// <param name="aFilename">The full path of file being
    ///transfered.</param>
    public void SendFile(string fileName)
    {
        System.IO.Stream io = null;

        try
        {
            // The live of this object is only good during
            // this function. Used mainly to avoid passing
            // around parameters to other functions.

            string username = String.Empty;
            string password = String.Empty;

            Url = HttpHelper.GetHTTPAuthenticationInfo(Url, out username, out password);

            coRequest = (HttpWebRequest)WebRequest.Create(Url);

            //httpauthentication
            byte[] credentialBuffer = new UTF8Encoding().GetBytes(
            username + ":" +
            password);
            coRequest.Headers["Authorization"] = "Basic " + Convert.ToBase64String(credentialBuffer);

            // Set use HTTP 1.0 or 1.1.
            coRequest.ProtocolVersion = TransferHttpVersion;

            coRequest.Method = "POST";

            coRequest.ContentType = "multipart/form-data; boundary=" + BeginBoundary;

            coRequest.Headers.Add("Cache-Control", "no-cache");

            coRequest.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8");

            coRequest.Accept = "text/xml,application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";

            coRequest.KeepAlive = true;

            string strFields = GetFormFields();

            string strFileHdr = GetFileHeader(fileName);

            string strFileTlr = GetFileTrailer();

            if (!String.IsNullOrEmpty(fileName))
            {
                FileInfo info = new FileInfo(fileName);

                coRequest.ContentLength = strFields.Length + strFileHdr.Length + strFileTlr.Length + info.Length;

            }
            else
            {
                coRequest.ContentLength = strFields.Length + strFileHdr.Length + strFileTlr.Length;
            }

            

            io = GetStream();

            WriteString(io, strFields);

            WriteString(io, strFileHdr);

            if (!String.IsNullOrEmpty(fileName))
            {
                this.WriteFile(io, fileName);
            }

            WriteString(io, strFileTlr);

            GetResponse();



        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            if (io != null)
            {
                io.Close();
            }

            // End the life time of this request object.
            coRequest = null;


        }
    }

    public void WriteString(System.IO.Stream io, string data)
    {
        byte[] PostData = System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(data);
        io.Write(PostData,0,PostData.Length);
    }
    /// <summary>
    /// Builds the proper format of the multipart data that
    /// contains the form fields and their respective values.
    /// </summary>
    /// <returns>The data to send in the multipart upload.</returns>
    public string GetFormFields()
    {
        string str = String.Empty;
        IDictionaryEnumerator myEnumerator = coFormFields.GetEnumerator();
        while ( myEnumerator.MoveNext() )
        {
            str += ContentBoundary + "\r\n" +
            CONTENT_DISP + '"' + myEnumerator.Key + "\"\r\n\r\n" +
            myEnumerator.Value + "\r\n";
        }
        return str;
    }
    /// <summary>
    /// Returns the proper content information for the
    /// file we are sending.
    /// </summary>
    /// <remarks>
    /// Hits Patel reported a bug when used with ActiveFile.
    /// Added semicolon after sendfile to resolve that issue.
    /// Tested for compatibility with IIS 5.0 and Java.
    /// </remarks>
    /// <param name="aFilename"></param>
    /// <returns></returns>
    public string GetFileHeader(string fileName)
    {
        return ContentBoundary + "\r\n" +
        CONTENT_DISP +
        "\"data\"; filename=\"" +
        Path.GetFileName(fileName) + "\"\r\n" +
        "Content-Type: " + FileContentType + "\r\n\r\n";
    }
    /// <summary>
    /// Creates the proper ending boundary for the multipart upload.
    /// </summary>
    /// <returns>The ending boundary.</returns>
    public string GetFileTrailer()
    {
    return "\r\n" + EndingBoundary;
    }
    /// <summary>
    /// Reads in the file a chunck at a time then sends it to the
    /// output stream.
    /// </summary>
    /// <param name="io">The io stream to write the file to.</param>
    /// <param name="aFilename">The name of the file to transfer.</param>
    public void WriteFile(System.IO.Stream io, string fileName)
    {
    FileStream readIn = new FileStream(fileName, FileMode.Open,
    FileAccess.Read);
    readIn.Seek(0, SeekOrigin.Begin); // move to the start of the file
    byte[] fileData = new byte[BufferSize];
    int bytes;
    while( (bytes = readIn.Read(fileData,0, BufferSize)) > 0 )
    {
    // read the file data and send a chunk at a time
    io.Write(fileData,0,bytes);
    }
    readIn.Close();
    }
}
}
