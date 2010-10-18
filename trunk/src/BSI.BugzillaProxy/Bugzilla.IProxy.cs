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
  \brief Definition of the methods supported by the Bugzilla WebService module.
  
  The definitions are used by the xml-rpc.net framework. User should
  use the IServer interface in the Bugzilla namespace instead.

  The purpose of this file is to define the proxy interface for use
  by xml-rpc. The xml-rpc interface of bugzilla is in essence a flat
  interface of functions, with names suggesting some kind of
  coherence between the different functions.

  Bugzillas authentication system is based on cookies. This can be
  handled by the xml-rpc.net system. In order to make it as easy as
  possibly, all interface functions are attached to the same proxy.
  At the C# interface level, functions are named following C# sharp
  standard. The XmlRpcMethod attribute before each function tells
  which bugzilla RPC method is actually called by the interface.

  The standard of the %Bugzilla webservice is to have functions that
  takes parameters use a struct (perl:hash) and values returned, also
  return a struct (or nothing). In this interface this is organized by
  having each function (that takes a parameter) take a struct called
  param, of a type that is named after the function. Return types are
  likewise named after the method. So, e.g. "Login" uses a parameter
  type called "LoginParam", and returns a type called LoginResult.
  The meaning of these parameter and result types are described using
  C# inline docs in the actual structs.
*/

using CookComputing.XmlRpc;

namespace Bugzilla {

  namespace ProxyStructs {

    //////////////////////////////////////////////////////////////////////
    // Version and timezone

    /// <summary>Result for call to GetVersion</summary>
    public struct GetVersionResult {
      /// <summary>The version of the server, as a string</summary>
      public string version;
    }

    /// <summary>Result for call to GetTimezone</summary>
    public struct GetTimezoneResult {
      /// <summary>The timezone information as a string, in RFC 2822 format</summary>
      public string timezone;
    }

    //////////////////////////////////////////////////////////////////////
    // Login handling

    /// <summary>Parameter struct for the Login function</summary>
    public struct LoginParam {
      /// <summary>The users login name</summary>
      public string login;
      /// <summary>The users password</summary>
      public string password;
      /// <summary>If the bugzilla server allow cookie session management, 
      /// then this option determines if the cookies issued by the server 
      /// can survive between sessions</summary>
      [XmlRpcMissingMapping(MappingAction.Ignore)]
      public bool? remember;
    }
    /// <summary>Parameter struct for the result of the Login function</summary>
    public struct LoginResult {
      /// <summary>The numeric id of the user upon successful login</summary>
      public int id;
    }

    
    //////////////////////////////////////////////////////////////////////
    // Product handling
    
    /// <summary>A list of product ids. This is used in several functions, 
    /// both as a param and as a result type</summary>
    public struct ProductIds {
      /// <summary>Array of product ids</summary>
      public int[] ids;
    };
  
    /// <summary>Information about a single product.</summary>
    public struct ProductInfo {
      /// <summary>Internal information from the server about the product. 
      /// This can and will change frequently</summary>
      public XmlRpcStruct internals;
      /// <summary>Product nummeric id</summary>
      public int id;
      /// <summary>Product name</summary>
      public string name;
      /// <summary>Product description</summary>
      public string description;
    };

    /// <summary>A list of Products</summary>
    public struct GetProductsResult {
      /// <summary>Array of Products</summary>
      public ProductInfo[] products;
    };

    //////////////////////////////////////////////////////////////////////
    // Bug handling

    /// <summary>A list of bug ids. This is used in several functions, 
    /// both as a param and as a result type</summary>
    public struct BugIds {
      /// <summary>Array of bug ids</summary>
      public int[] ids;
    };
  
    /// <summary>Information about a single bug.</summary>
    public struct BugInfo {
      /// <summary>Internal information from the server about the bug. 
      /// This can and will change frequently</summary>
      public XmlRpcStruct internals;
      /// <summary>Bug nummeric id</summary>
      public int id;
      /// <summary>Bug alias (empty string if no aliases)</summary>
      public string alias;
      /// <summary>Bug summary</summary>
      public string summary;
      /// <summary>Time/date when bug was created</summary>
      [XmlRpcMember("creation_time")]
      public System.DateTime created;
      /// <summary>Time/date when bug was last changed</summary>
      [XmlRpcMember("last_change_time")]
      public System.DateTime changed;

    };

    /// <summary>A list of Bugs</summary>
    public struct GetBugsResult {
      /// <summary>Array of Bugs</summary>
      public BugInfo[] bugs;
    };

    /// <summary>Information needed to create a bug</summary>
    public struct CreateBugParam {
      /// <summary>Name of product to create bug against</summary>
      public string product;
      /// <summary>Name of component to create bug against</summary>
      public string component;
      /// <summary>Summary of bug</summary>
      public string summary;
      /// <summary>Version of product above, the version the bug was found in</summary>
      public string version;
      /// <summary>The initial description for this bug. Some %Bugzilla
      /// installations require this to not be blank.</summary>
      [XmlRpcMissingMapping(MappingAction.Ignore)]
      public string description;
      /// <summary>The operating system the bug was discovered
      /// on. Some %Bugzilla installations require this to not be
      /// blank.</summary>
      [XmlRpcMissingMapping(MappingAction.Ignore)]
      [XmlRpcMember("op_sys")]
      public string operatingSystem;
      /// <summary>What type of hardware the bug was experienced
      /// on. Some %Bugzilla installations require this to not be
      /// blank.</summary>
      [XmlRpcMissingMapping(MappingAction.Ignore)]
      public string platform;
      /// <summary>What order the bug will be fixed in by the
      /// developer, compared to the developers other bugs. Some
      /// %Bugzilla installations require this to not be
      /// blank. </summary>
      [XmlRpcMissingMapping(MappingAction.Ignore)]
      public string priority;
      /// <summary>How severe the bug is. Some %Bugzilla installations
      /// require this to not be blank. </summary>
      [XmlRpcMissingMapping(MappingAction.Ignore)]
      public string severity;
      /// <summary>A brief alias for the bug that can be used instead
      /// of a bug number when accessing this bug. Must be unique in
      /// all of this %Bugzilla.</summary>
      [XmlRpcMissingMapping(MappingAction.Ignore)]
      public string alias;
      /// <summary>A user to assign this bug to, if you dont want it
      /// to be assigned to the component owner.</summary>
      [XmlRpcMissingMapping(MappingAction.Ignore)]
      [XmlRpcMember("assigned_to")]
      public string assignedTo;
      /// <summary>An array of usernames to CC on this bug.</summary>
      [XmlRpcMissingMapping(MappingAction.Ignore)]
      public string[] cc;
      /// <summary>If this installation has QA Contacts enabled, you
      /// can set the QA Contact here if you dont want to use the
      /// components default QA Contact. </summary>
      [XmlRpcMissingMapping(MappingAction.Ignore)]
      [XmlRpcMember("qa_contact")]
      public string qaContact;
      /// <summary>The status that this bug should start out as. Note
      /// that only certain statuses can be set on bug creation.</summary>
      [XmlRpcMissingMapping(MappingAction.Ignore)]
      public string status;
      /// <summary>A valid target milestone for this product.</summary>
      [XmlRpcMissingMapping(MappingAction.Ignore)]
      [XmlRpcMember("target_milestone")]
      public string targetMilestone;

    }
    
    /// <summary>Result of call to CreateBug</summary>
    public struct CreateBugResult {
      /// <summary>The id of the newly created bug</summary>
      public int id;
    }
    
    // Legal values for fields of a bug
    /// <summary>Parameter for GetLegalValues call</summary>
    public struct GetLegalValuesForBugFieldParam {
      /// <summary>Which field to query for</summary>
      public string field;
      /// <summary>If the field is product specific, which product then</summary>
      public int product_id;
    };

    /// <summary>Result of call to GetLegalValues</summary>
    public struct GetLegalValuesForBugFieldResult {
      /// <summary>Array of values</summary>
      public string[] values;
    };

    /// <summary>Parameter for AppendComment call</summary>
    public struct AppendCommentParam {
      /// <summary>The id of the bug</summary>
      public int id;
      /// <summary>The actual comment to append</summary>
      public string comment;
      /// <summary>Wheter or not this is a private comment?</summary>
      // Note, must use renaming, as "private" is reserved in C#.
      [XmlRpcMissingMapping(MappingAction.Ignore)]
      [XmlRpcMember("private")]
      public bool? isPrivate;
      /// <summary>The worktime of this comment. Should probably not
      /// be a string</summary>
      [XmlRpcMissingMapping(MappingAction.Ignore)]
      public int? worktime;
    };

    /// <summary>Parameter for call to SetBugResolution</summary>
    public struct SetBugResolutionParam {
      /// <summary>The id of the bug</summary>
      public int    bug_id;
      /// <summary>The actual resolution to set. Should be legal.</summary>
      public string resolution;
    };

  } // namespace ProxyStructs

    /// <summary>The interface for use by the xml-rpc.net
    /// framework.</summary> <remarks>The interface methods translates
    /// into other method names at the server end, as we are using
    /// .NET name styling. It also puts all methods in the same
    /// interface. This may change. Users are not expected to use this
    /// interface directly.</remarks>
    public interface IProxy : IXmlRpcProxy {
    
    //////////////////////////////////////////////////////////////////////
    // Server related / general methods
    //////////////////////////////////////////////////////////////////////

    /*! \name General Methods */
    //@{

    //////////////////////////////////////////////////////////////////////
    /// <summary>Get the version of the server</summary>
    /// <returns>The version of the server as a string</returns>
    [XmlRpcMethod( "Bugzilla.version" )] 
      ProxyStructs.GetVersionResult GetVersion();
    
    //////////////////////////////////////////////////////////////////////
    /// <summary>Get the timezone of the server</summary>
    /// <returns>The timezone information as a string, in RFC 2822 format</returns>
    [XmlRpcMethod( "Bugzilla.timezone" )] 
      ProxyStructs.GetTimezoneResult GetTimezone();
    //@}
    
    /*! \name Login/out */
    //@{
    //////////////////////////////////////////////////////////////////////
    /// <summary>Login to the server</summary> 
    /// <param name="param">Login, password and optional remember value</param>
    /// <returns>If successful, the users numeric id</returns>
    [XmlRpcMethod( "User.login" )] 
      ProxyStructs.LoginResult Login( ProxyStructs.LoginParam param );
    
    //////////////////////////////////////////////////////////////////////
    /// <summary>Logout of the server</summary>
    [XmlRpcMethod( "User.logout" )] 
      void Logout();
    //@}

    //////////////////////////////////////////////////////////////////////
    // Product related methods
    //////////////////////////////////////////////////////////////////////
    /*! \name Product Related Methods */
    //@{
    //////////////////////////////////////////////////////////////////////
    /// <summary>Get a list of the products (ids) that the user can
    /// search against.</summary>
    /// <returns>A list of product ids</returns>
    [XmlRpcMethod( "Product.get_selectable_products" )]
      ProxyStructs.ProductIds GetSelectableProducts();
    
    //////////////////////////////////////////////////////////////////////
    /// <summary>Get a list of the products (ids) that the user can
    /// post bugs against.</summary>
    /// <returns>A list of product ids</returns>
    [XmlRpcMethod( "Product.get_enterable_products" )]
      ProxyStructs.ProductIds GetEnterableProducts();
    
    //////////////////////////////////////////////////////////////////////
    /// <summary>Get a list of the products (ids) that the user can
    /// search or enter bug against.</summary>
    /// <returns>A list of product ids</returns>
    [XmlRpcMethod( "Product.get_accessible_products" )]
      ProxyStructs.ProductIds GetAccessibleProducts();
    
    //////////////////////////////////////////////////////////////////////
    /// <summary>Get a list of products from a list of ids</summary>
    /// <param name="param">A list of product ids</param>
    /// <returns>A list of products</returns>
    [XmlRpcMethod( "Product.get_products" )]
      ProxyStructs.GetProductsResult 
      GetProducts( ProxyStructs.ProductIds param );
    //@}
   
    //////////////////////////////////////////////////////////////////////
    // Bug related methods
    //////////////////////////////////////////////////////////////////////
    /*! \name Bug Related Methods */
    //@{
    
    //////////////////////////////////////////////////////////////////////
    /// <summary>Create a new bug</summary>
    /// <param name="param">Various information about the new bug</param>
    /// <returns>The id of the newly created bug</returns>
    [XmlRpcMethod( "Bug.create" )]
      ProxyStructs.CreateBugResult CreateBug( ProxyStructs.CreateBugParam param );
    
    //////////////////////////////////////////////////////////////////////
    /// <summary>Get a list of bugs from a list of ids</summary>
    /// <param name="param">A list of bug ids</param>
    /// <returns>A list of bugs</returns>
    [XmlRpcMethod( "Bug.get_bugs" )] 
      ProxyStructs.GetBugsResult
      GetBugs( ProxyStructs.BugIds param );
    
    //////////////////////////////////////////////////////////////////////
    /// <summary>Get a list of legal values for a field in a bug</summary>
    /// <param name="param">Which field, possibly product</param>
    /// <returns>The legal values for the field</returns>
    [XmlRpcMethod( "Bug.legal_values" )]
      ProxyStructs.GetLegalValuesForBugFieldResult 
      GetLegalValuesForBugField( ProxyStructs.GetLegalValuesForBugFieldParam param );
    
    //@}

    /*! \name Experimental Methods
      These are here as part of the development of patches, etc. */
    //@{

    //////////////////////////////////////////////////////////////////////
    /// <summary>Append a comment</summary>
    /// <remarks>This requires a patch: https://bugzilla.mozilla.org/show_bug.cgi?id=355847</remarks>
    /// <param name="param">Bug id, comment, etc</param>
    [XmlRpcMethod( "Bug.append_comment" )]
      void AppendComment( ProxyStructs.AppendCommentParam param );

    //////////////////////////////////////////////////////////////////////
    /// <summary>Change the resolution of a bug</summary>
    /// <remarks>This requires an unpublished patch</remarks>
    /// <param name="param">Bug id and resolution</param>
    [XmlRpcMethod( "Bug.set_resolution" )]
      string SetBugResolution( ProxyStructs.SetBugResolutionParam param );

    //@}
    


    } 
  } // namespace









