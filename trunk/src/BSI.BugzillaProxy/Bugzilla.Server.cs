/* Bugzilla C# Proxy Library
   Copyright (C) 2006, Dansk BiblioteksCenter A/S
   Mads Bondo Dydensborg, <mbd@dbc.dk>
   
   This library is free software; you can redistribute it and/or
   modify it under the terms of the GNU Lesser General Public
   License as published by the Free Software Foundation; either
   version 2.1 of the License, or (at your option) any later version.
   
   This library is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
   Lesser General Public License for more details.
   
   You should have received a copy of the GNU Lesser General Public
   License along with this library; if not, write to the Free Software
   Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
*/   


/*! \file 
  \brief Implementation of the IServer interface.

  This file implements a the IServer interface.

*/

using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using CookComputing.XmlRpc;
using Bugzilla.ProxyStructs;

namespace Bugzilla {

  /// <summary>This class encapsulates a %Bugzilla server.</summary>
  /// <remarks>Access is done through http/xmlrpc. You should consult
  /// the documentation for Bugzilla.IServer if you wish to know how
  /// the public part, besides the constructors, which are documented
  /// here, works.</remarks>
  public class Server : IServer {
    
    ////////////////////////////////////////////////////////////////////////////////
    // Internal tracer class
    /// <summary>Trace data sent and received using the XMLRPC framework</summary>
    private class TextWriterTracer : XmlRpcLogger
    {
      /// Stream to dump to
        public TextWriter writer ;

      /// <summary>Called when a request is made</summary>
      /// <param name="sender">The sending object</param>
      /// <param name="e">The Event arguments</param>
      protected override void OnRequest(object sender, 
					XmlRpcRequestEventArgs e) {
	if ( writer != null ) writer.WriteLine( "Sending =====>" );
	DumpStream(e.RequestStream);
	if ( writer != null ) { 
	  writer.WriteLine( "=====" );
	  writer.Flush();
	}
      }

      /// <summary>Called when a response is received</summary>
      /// <param name="sender">The sending object??</param>
      /// <param name="e">The Event arguments</param>
      protected override void OnResponse(object sender, 
					 XmlRpcResponseEventArgs e) {
	if ( writer != null ) writer.WriteLine( "Receiving <=====" );
	DumpStream(e.ResponseStream);
	if ( writer != null ) {
	  writer.WriteLine( "=====" );
	  writer.Flush();
	}
      }

      private void DumpStream(Stream stm) {
	if ( writer != null ) {
	  stm.Position = 0;
	  TextReader trdr = new StreamReader(stm);
	  String s = trdr.ReadLine();
	  while (s != null)
	  {
	    writer.WriteLine(s);
	    s = trdr.ReadLine();
	  }
	}
      } 
    } // class MyTracer
    //////////////////////////////////////////////////////////////////////

    
    /// <summary>This is were we direct our xmlrpc request. This could
    /// be an option, but for now it is hardcoded, as I do not expect
    /// the Bugzilla server to change this.</summary>
    private const string rpcname = "xmlrpc.cgi";
    
    /// <summary>Our xml-rpc.net proxy instance</summary>
    IProxy bugzillaProxy;

    /// <summary>Assembly members can access the bugzillaProxy</summary>
    internal IProxy Proxy {
      get {
	return bugzillaProxy;
      }
    }

    /// <summary>PreAuthenticate</summary>
    public bool PreAuthenticate {
      get {
	return Proxy.PreAuthenticate;
      }
      set {
	Proxy.PreAuthenticate = value;
      }
    }

    public ICredentials Credentials {
      get {
	return Proxy.Credentials;
      }
      set {
	Proxy.Credentials = value;
      }
    }

    /// <summary>Our Tracer.</summary>
    TextWriterTracer tracer;

    /// <summary>The hostname.</summary>
    private string hostname;

    /// <summary>The port to use on the server.</summary>
    private uint port;

    /// <summary>The path to use on the server.</summary>
    private string path;

    /// <summary>Whether or not to connect via SSL.</summary>
    private bool ssl;

    /// <summary>Wheter we are logged in or not.</summary>
    private bool loggedIn;


    /* You can construct a Server instance, supplying optional
      information about hostname, port, path, ssl support, and
      tracer. */


    /*! \name Constructors  */
    //@{

    //////////////////////////////////////////////////////////////////////
    /// <summary>Constructor</summary>
    /// <param name="hostname">The name of the server to use</param>
    /// <param name="port">The port to use</param>
    /// <param name="path">The path to use</param>
    /// <param name="ssl">If true, use https for connections, otherwise use http</param>
    /// <param name="traceWriter">A TextWriter instance to trace to</param>
    public Server( string hostname, uint port, string path, bool ssl,
		   TextWriter traceWriter ) {
      // Create the bugzillaproxy instance, associate it with our tracer
      bugzillaProxy = XmlRpcProxyGen.Create<IProxy>();
      this.tracer        = new TextWriterTracer();
      tracer.Attach( bugzillaProxy );
      this.hostname      = hostname;
      this.port          = port;
      this.path          = path;
      this.ssl           = ssl;
      this.tracer.writer = traceWriter;
      loggedIn           = false;
      UpdateUrl();
    }
    
    /*! \overload */
    /// <remarks>This constructs with a null traceWriter, empty path, and http as protocol (scheme)</remarks>
    /// <param name="hostname">The name of the server to use</param>
    /// <param name="port">The port to use</param>
    /// <param name="path">The path to use</param>
    public Server( string hostname, uint port, string path ) 
      : this( hostname, port, path, false, null ) {}

    /*! \overload */
    /// <remarks>This constructs with a null traceWriter and empty path</remarks>
    /// <param name="hostname">The name of the server to use</param>
    /// <param name="port">The port to use</param>
    /// <param name="ssl">If true, use https for connections, otherwise use http</param>
    public Server( string hostname, uint port, bool ssl ) 
      : this( hostname, port, "", ssl, null ) {}
    
    /*! \overload */
    /// <remarks>This constructs with a null traceWriter, empty path, and http as protocol (scheme)</remarks>
    /// <param name="hostname">The name of the server to use</param>
    /// <param name="port">The port to use</param>
    public Server( string hostname, uint port ) 
      : this( hostname, port, "", false, null ) {}

    /*! \overload */
    /// <remarks>This constructs with a null traceWriter at port 80, with empty path and http as protocol (scheme)</remarks>
    /// <param name="hostname">The name of the server to use</param>
    /// <param name="path">The path to use</param>
    public Server( string hostname, string path ) 
      : this( hostname, 80, path, false, null ) {}

    /*! \overload */
    /// <remarks>This constructs with a null traceWriter, and empty path. Depending on the setting of ssl, the port will be either 80 (false) or 443 (true).</remarks>
    /// <param name="hostname">The name of the server to use</param>
    /// <param name="ssl">If true, use https for connections at port 443, otherwise use http at port 80.</param>
    public Server( string hostname, bool ssl )
      : this( hostname, (ssl ? (uint) 443 : (uint) 80), "", ssl, null ) {}

    /*! \overload */
    /// <remarks>This constructs using port 80, with an empty path, with a null traceWriter, and using http.</remarks>
    /// <param name="hostname">The name of the server to use</param>
    public Server( string hostname ) 
      : this( hostname, 80, "", false, null ) {}



    /*! \overload */
    /// <remarks>This constructs using an empty hostname, an empty path, port 80, using http, and with a null traceWriter.</remarks>
    public Server() 
      : this( "", 80, "", false, null ) {}
    
    //@}

    //////////////////////////////////////////////////////////////////////
    /// <summary>Update the Url on the proxy object from hostname and
    /// port</summary>
    private void UpdateUrl() {
      if ( path != "" ) {
    //bugzillaProxy.Url 
    //  = (ssl ? "https://" : "http://") + hostname + ":" + port + "/" + path + "/" + rpcname;
    //  } else {
    //bugzillaProxy.Url 
    //  = (ssl ? "https://" : "http://") + hostname + ":" + port + "/" + rpcname;

          bugzillaProxy.Url
            = (ssl ? "https://" : "http://") + hostname +  "/" + path + "/" + rpcname;
      }
      else
      {
          bugzillaProxy.Url
            = (ssl ? "https://" : "http://") + hostname +  "/" + rpcname;

      }
    }

    //////////////////////////////////////////////////////////////////////
    // Properties
    //////////////////////////////////////////////////////////////////////
    public string Hostname {
      get { return hostname; }
      set { 
	if ( loggedIn ) {
	  throw new System.
	    InvalidOperationException( "Bugzilla.Hostname: Tried to change the hostname while logged in" );
	}
	hostname = value;
	UpdateUrl();
      }
    }

    public uint Port {
      get { return port; }
      set {
	if ( loggedIn ) {
	  throw new System.
	    InvalidOperationException( "Bugzilla.Port: Tried to change the port while logged in" );
	}
	port = value;
	UpdateUrl();
      }
    }
    
    public string Path {
      get { return path; }
      set { 
	if ( loggedIn ) {
	  throw new System.
	    InvalidOperationException( "Bugzilla.Path: Tried to change the path while logged in" );
	}
	path = value;
	UpdateUrl();
      }
    }

    public bool LoggedIn {
      get {
        return loggedIn;
      }
    }

    public TextWriter TraceWriter {
      get { return tracer.writer; }
      set { tracer.writer = value; }
    }
    
    //////////////////////////////////////////////////////////////////////
    // Server Methods
    //////////////////////////////////////////////////////////////////////

    /*! \name General Methods */
    //@{
    public string GetVersion() {
      if ( hostname == "" ) {
	throw new System.InvalidOperationException( "GetVersion: Hostname is not set" );
      }
      return bugzillaProxy.GetVersion().version;
    }

    public string GetTimezone() {
      if ( hostname == "" ) {
	throw new System.InvalidOperationException( "GetTimezone: Hostname is not set" );
      }
      return bugzillaProxy.GetTimezone().timezone;
    }
    //@}
    //////////////////////////////////////////////////////////////////////
    // Methods to login and out, get/set cookies
    //////////////////////////////////////////////////////////////////////

    /*! \name Authentication Handling */
    //@{
    public int Login( string username, string password, bool remember ) {
      if ( loggedIn ) {
	throw new System.InvalidOperationException( "Login: Already logged in" );
      }
      LoginParam param = new LoginParam();
      param.login    = username;
      param.password = password;
      // param.remember = remember;
      int res = bugzillaProxy.Login( param ).id;
      loggedIn = true; // prev statement will throw if failed.
      return res; // I have no idea what the user want to do with that.
    }

    public void Logout() {
      if ( !loggedIn ) {
	throw new System.InvalidOperationException( "Logout: Not logged in" );
      }
      bugzillaProxy.Logout();
    }

    public System.Net.CookieCollection Cookies {

      get {
	if ( !loggedIn ) {
	  throw new System.InvalidOperationException( "cookies.get: Not logged in" );
	}
	return bugzillaProxy.CookieContainer.GetCookies( new Uri( (ssl ? "https://" : "http://") + hostname ) );
      }
      
      set {
	if ( loggedIn ) {
	  throw new System.InvalidOperationException( "cookies.set: Already logged in" );
	}
	foreach ( Cookie c in value ) {
	  Console.WriteLine( "Adding a cookie" );
	  bugzillaProxy.CookieContainer.Add( c );
	}
      }
    }
    
    public void WriteCookies( System.IO.Stream stream ) {
      CookieCollection cc = Cookies;
      BinaryFormatter b = new BinaryFormatter();
      b.Serialize( stream, cc );
    }

    public void ReadCookies( System.IO.Stream stream ) {
      BinaryFormatter b = new BinaryFormatter();
      CookieCollection cc = (CookieCollection) b.Deserialize( stream );
      Cookies = cc;
    }

    //@}
    //////////////////////////////////////////////////////////////////////
    // Product related methods
    //////////////////////////////////////////////////////////////////////
    
    /*! \name Product Access */
    //@{
    public int[] GetSelectableProductIds() {
      return bugzillaProxy.GetSelectableProducts().ids;
    }

    public int[] GetEnterableProductIds() {
      return bugzillaProxy.GetEnterableProducts().ids;
    }

    public int[] GetAccessibleProductIds() {
      return bugzillaProxy.GetAccessibleProducts().ids;
    }

    public IProduct[] GetProducts( int[] ids ) {
      ProductIds param;
      param.ids = ids;
      ProductInfo[] pis = bugzillaProxy.GetProducts( param ).products;
      IProduct[] res = new Product[ pis.Length ];
      for ( int i = 0; i < pis.Length; ++i ) {
	res[i] = new Product( this, pis[i] );
      }
      return res;
    }

    public IProduct GetProduct( int id ) {
      int[] ids = new int[1];
      ids[0] = id;
      IProduct[] ps = GetProducts( ids );
      if ( ps.Length != 1 ) {
	throw new ArgumentOutOfRangeException( "id", id, 
					       "No product was returned from the server. You probably specified an id of a non-existing product, or a product you can not access" );
      }
      return ps[0];
    }
    //@}

    //////////////////////////////////////////////////////////////////////
    // Bug handling methods
    //////////////////////////////////////////////////////////////////////
    /*! \name Bug Access */
    //@{
    public IBug[] GetBugs( int[] ids ) {
      BugIds param;
      param.ids = ids;
      BugInfo[] bis = bugzillaProxy.GetBugs( param ).bugs;
      IBug[] res = new Bug[ bis.Length ];
      for ( int i = 0; i < bis.Length; ++i ) {
	res[i] = new Bug( this, bis[i] );
      }
      return res;
    }

    public IBug GetBug( int id ) {
      int[] ids = new int[1];
      ids[0] = id;
      IBug[] bs = GetBugs( ids );
      if ( bs.Length != 1 ) {
	throw new ArgumentOutOfRangeException( "id", id, 
					       "No bug was returned from the server. You probably specified an id of a non-existing bug, or a bug you can not access" );
      }
      return bs[0];
    }

    public string[] GetLegalFieldValues( string fieldName ) {
      return GetLegalFieldValues( fieldName, -1 );
    }

    //@}
    // Private method, used internally, also by Product, etc.
    internal string[] GetLegalFieldValues( string field, int product_id ) {
      GetLegalValuesForBugFieldParam param;
      // Translate names used by us to bugzilla names
      switch( field ) {
          case "operatingSystem": field = "op_sys"; break;
          case "assignedTo": field = "assigned_to"; break; 
          case "qaContact": field = "qa_contact"; break;
          case "targetMilestone": field = "target_milestone"; break;
          case "version": field = "version"; break;
          case "component": field = "component"; break;
          case "bugStatus": field = "bug_status"; break;
          case "platform": field = "platform"; break;
          case "priority": field = "priority"; break;
          case "severity": field = "severity"; break;
      }
      // Setup parameters
      param.field      = field;
      param.product_id = product_id; // Ignored by server if not needed.
      return bugzillaProxy.GetLegalValuesForBugField( param ).values;
    }


    /*! \name Experimental 

    * These methods are experimental, and will change/move, as the API
    * stabilizes. */

    //@{
    //////////////////////////////////////////////////////////////////////
    /// <summary>Create a bug - experimental</summary>
    public int CreateBug( string product, string component, 
			  string summary, string description ) {
      CreateBugParam param = new CreateBugParam();
      param.product         = product;
      param.component       = component;
      param.summary         = summary;
      param.description     = description;
      param.version         = "unspecified";
      param.severity        = "normal";
      param.operatingSystem = "All";
      param.priority        = "P5";
      param.platform        = "All";

      return bugzillaProxy.CreateBug( param ).id;
    }
    
    
    public string SetBugResolution( int bugId, string resolution ) {
      SetBugResolutionParam parameters;
      parameters.bug_id     = bugId;
      parameters.resolution = resolution;
      return bugzillaProxy.SetBugResolution( parameters );
    }
    
    //@}
    
  } // class Server
  
} // namespace Bugzilla
