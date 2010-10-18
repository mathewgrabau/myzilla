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
  \brief Declare an interface for accessing a Bugzilla server.
  
  The basic idea is to create an object implementing this interface,
  which clients can use to access a bugzilla implementation through.

  In order to use the object, some properties will have to be set,
  such as Hostname, Port, and so on.
  
  Tracing of the requests and responses, can be achieved by assigning
  a TextWriter to the TraceWriter property.

  Methods exists to get some general information about the server,
  handle authentification, and get products and bugs.

  Authentification by Bugzilla is handled using cookies. In order to
  obtain a set of cookies, you call the Login method. The cookies are
  handled automatically by the xml-rpc.net assembly. In order to store
  the cookies between sessions, methods to obtain and set the cookies
  are provided.

  All methods are synchronous, and either succeed, or throw some kind
  of exception on error.
  
*/
  
namespace Bugzilla {

  /// <summary>Interface that abstracts the operations on a %Bugzilla
  /// Server.</summary><remarks>Properties reflects settings that will
  /// not cause network traffic, while methods typically involves the
  /// server in the other end.</remarks>
  public interface IServer {

    /// <summary>Get and set the servers
    /// hostname.</summary><remarks>If you set it, while logged in, an
    /// exception will be thrown</remarks> <exception
    /// cref="System.InvalidOperationException">This exception will be
    /// thrown if trying to set the property while logged
    /// in</exception>
    string Hostname { get; set; }

    /// <summary>Get and set the port.</summary><remarks>If you set
    /// the port while logged in, an exception will be thrown. By
    /// default the port will be set to 80, which is the standard http
    /// port.</remarks> <exception
    /// cref="System.InvalidOperationException">This exception will be
    /// thrown if trying to set the property while logged
    /// in</exception>
    uint Port { get; set; }

    /// <summary>Get and set the path.</summary> <remarks>If you set
    /// the path while logged in, an exception will be thrown. By
    /// default the path will be set to the empty string.</remarks>
    /// <exception cref="System.InvalidOperationException">This
    /// exception will be thrown if trying to set the property while
    /// logged in</exception>
    string Path { get; set; }
    
    /// <summary>Get and set a TextWriter for debug
    /// traces.</summary><remarks> If set, trace messages will be
    /// written to this TextWriter</remarks>
    System.IO.TextWriter TraceWriter { get; set; }

    ///////////////////////////////////////////////////////////////////////
    // Server methods

    /*! \name General Methods 
      Methods to get information about the server, and log in/out. */
    //@{

    /*! \example ServerInfo.cs
     * This is an example on how to use the Bugzilla.IServer.GetVersion and Bugzilla.IServer.GetTimezone
     call */
    
    /// <summary>Get the version of the server.</summary> 
    /// <remarks>This requires
    /// that at least the hostname has been set. GetVersion can be
    /// called without beeing logged in to the Bugzilla
    /// server.</remarks> 
    /// <exception cref="System.InvalidOperationException">This
    /// exception will be thrown if Hostname is empty</exception>
    string GetVersion();
    
    /// <summary>Get the servers timezone.</summary>
    /// <remarks>This returns the servers timezone as a
    /// string in (+/-)XXXX (RFC 2822) format. All time/dates returned 
    /// from the server will be in this timezone.</remarks>
    /// <returns>The servers timezone as a string</returns>
    string GetTimezone();

    //@}
    
    /*! \name Authentication Handling 
      Methods to log in/out, and store/set credentials. */
    //@{
    
    /// <summary>Login to the server.</summary>
    /// <remarks>Most servers require
    /// you to log before you can retrieve information other than the
    /// version, let alone work with bugs, products or
    /// components</remarks> 
    /// <param name="username">The Bugzilla username to use</param>
    /// <param name="password">The Bugzilla password to use</param>
    /// <param name="remember">Same meaning as the remember checkbox...?</param>
    /// <exception cref="System.InvalidOperationException">This
    /// exception will be thrown if trying to login, while already
    /// logged in</exception>
    int Login( string username, string password, bool remember );
    
    /// <summary>Logout of the server.</summary> <remarks>You must be
    /// logged in to call this function. This will invalidate the
    /// cookies set by Bugzilla.</remarks> <exception
    /// cref="System.InvalidOperationException">This exception will be
    /// thrown if trying to logout, without beeing logged
    /// in</exception>
    void Logout();

    /// <summary>Get the login status.</summary> <remarks>If the user
    /// is logged in, return true. Otherwise, return false.</remarks>
    bool LoggedIn { get; }

    /// <summary>The cookies that are currently used as
    /// credentials.</summary> <remarks>By obtaining the cookies, you
    /// can store them somewhere, and use them instead of a login
    /// during a new session.</remarks> <exception
    /// cref="System.InvalidOperationException">This exception will be
    /// thrown if trying to set the cookies while logged in, or trying
    /// to get the cookies without beeing logged in.</exception>
    System.Net.CookieCollection Cookies{ get; set; } 
    
    /// <summary>Write the currently used cookies to a
    /// stream</summary> <remarks>By obtaining the cookies, you can
    /// store them somewhere, and use them instead of a login during a
    /// new session.</remarks> <exception
    /// cref="System.InvalidOperationException">This exception will be
    /// thrown if trying to set the cookies, without beeing logged
    /// in</exception><param name="stream">The stream to write the
    /// cookies to.</param>
    void WriteCookies( System.IO.Stream stream );
    
    /// <summary>Read cookies from a stream.</summary> <remarks>By
    /// calling this method with a stored set of cookies, you do not
    /// need to perform a login.</remarks> <exception
    /// cref="System.InvalidOperationException">This exception will be
    /// thrown if trying to get the Cookies, without beeing logged
    /// in</exception> <param name="stream">The stream to read the
    /// cookies from.</param>
    void ReadCookies( System.IO.Stream stream );

    //@}

    // <summary>Get a Bug from the Bugzilla server. This is not a
    // stable interface, so this is more of a test</summary>
    // <param name="bugId">The numeric id of a bug</param>
    // <param name="readOnly">If true, you can not modify the bug</param>
    // <returns>A bug</returns>
    // IBug GetBug( int bugId, bool readOnly );

    /*! \name Product Access 
      Methods to get information about the products that the server know
      
      A user may not have access to all products. A user may have
      access to search and view bugs from some products, and
      additionally, to enter bugs against some other products. Methods
      for this are exposed here. Additionally, general methods to
      retrieve one or more bugs are made avaiable as well.
    */
    //@{

    /// <summary>Get a list of the products an user can search</summary>
    /// <returns>List of product ids that the user can search against</returns>
    int[] GetSelectableProductIds();

    /// <summary>Get a list of the products an user can file bugs against</summary>
    /// <returns>List of product ids that the user can file bugs against</returns>
    int[] GetEnterableProductIds();

    /// <summary>Get a list of the products an user can search or file bugs against</summary>
    /// <remarks>This is effectively an union of GetSelectableProductIds and GetEnterableProductIds.</remarks>
    /// <returns>List of product ids that the user can search or file bugs against</returns>
    int[] GetAccessibleProductIds();

    /*! \example ListProducts.cs
     * This is an example on how to use the Bugzilla.IServer.GetProducts call */
    
    /// <summary>Get a list of existing products</summary>
    /// <remarks>This returns an array of products, matching the ids
    /// supplied as argument. Note, however, that if the user have
    /// specified an id for a product that the user for some reason
    /// can not access, this is silently ignored.</remarks>
    /// <param name="ids">List of product ids</param>
    /// <returns>List of products</returns>
    IProduct[] GetProducts( int[] ids );

    /// <summary>Get a single existing product</summary>
    /// <remarks>This return a single existing product from the id</remarks>
    /// <param name="id">The id of the product to get</param>
    /// <returns>A product</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">This exception will
    /// be thrown, if the id does not exists, or can not be accessed</exception>
    IProduct GetProduct( int id );

    //@}

    /*! \name Bug Access 
      Methods to get information about the bugs that the server know.

      Note: There are currently no search support in the %Bugzilla
      WebService. When this is implemented, the search facilities
      should complement these services nicely.
      
      As for products, a user may not have read/write access to all bugs.
    */
    //@{

    /*! \example ListBug.cs
     * This is an example on how to use the Bugzilla.IServer.GetBug call */

    /// <summary>Get a list of bugs</summary>
    /// <remarks>This returns an array of bugs, matching the ids
    /// supplied as arguments. Note, that if the user have specified
    /// an id for a bug that does not exist, or that the user can not
    /// access (read), an exception will be thrown. This is different
    /// from GetProducts.</remarks>
    /// <param name="ids">List of bug ids</param>
    /// <returns>List of bug</returns>
    /*! \todo Document exception, when known. */
    IBug[] GetBugs( int[] ids );

    /*! \example ListBugs.cs
     * This is an example on how to use the Bugzilla.IServer.GetBugs call */

    /// <summary>Get a bug</summary>
    /// <remarks>This return a single existing bug, from the id</remarks>
    /// <param name="id">The id of a bug</param>
    /// <returns>A bug</returns>
    /// <exception cref="System.ArgumentOutOfRangeException">This exception will
    /// be thrown, if the id does not exists, or can not be accessed</exception>
    IBug GetBug( int id );

    /// <summary>Get a list of legal values for a non-product specific
    /// bug field.</summary> <remarks>This can be used to retrieve a
    /// list of legal values for non-product specific fields of a bug,
    /// such as status, severity, and so on. This method understands
    /// both the bugzilla original naming (such as op_sys) and the
    /// naming used here (such as operatingSystem). Note, that in
    /// order to retrieve values for product specific fields (such as
    /// component), you must use the
    /// IProduct.GetLegalFieldValues</remarks> <returns>A list of
    /// legal values for the field</returns> <param
    /// name="fieldName">The name of a field.</param>
    string[] GetLegalFieldValues( string fieldName );
    
    //@}

  }

} // namespace
