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
  \brief Implement the IProduct interface.

  This is an implementation of the IProduct interface. It adds some
  methods to test/access the Bugzilla server in a "non-standard" way.

  In order to actually instantiate a Product, a Server instance must
  be supplied.
*/

using System;
using CookComputing.XmlRpc;
using Bugzilla.ProxyStructs;

namespace Bugzilla {

  /// <summary>Implementation of the IProduct interface - abstracts a
  /// product on the server.</summary>
  /// <remarks>Currently there are no public constructors.</remarks>
  public class Product : IProduct {
    private Server server;

    private ProductInfo pi;
    
    /*! \name Constructors 

    Currently there are no way to create an entirely new product. Only
    product instances that reflect an already existing product on the
    server can be created. */
    //@{

    /// <summary>Constructor</summary> <param name="server">A Server
    /// instance that is associated with this product.</param> <param
    /// name="pi">Information about the product, as retrieved from the
    /// server</param>
    
    internal Product( Server server, ProductInfo pi ) {
      this.server = server;
      this.pi     = pi;
    }
    //@}
    
    public int Id {
      get {
	return pi.id;
      }
    }

    public string Name {
      get {
	return pi.name;
      }
      set { throw new Exception( "Unimplemented" ); }
    }

    public string Description {
      get {
	return pi.description;
      }
      set { throw new Exception( "Unimplemented" ); }
    }

    public string[] GetLegalFieldValues( string fieldName ) {
      return server.GetLegalFieldValues( fieldName, Id );
    }

    public string[] GetComponents() {
      return GetLegalFieldValues( "component" );
    }
    
    public IBug CreateBug( string alias, string component, 
			   string version, string operatingSystem, string platform,
			   string summary, string description,
			   string priority, string severity, string status,
			   string targetMilestone, string assignedTo, string[] cc,
			   string qaContact ) {
      CreateBugParam param;
      param.product         = pi.name;
      param.alias           = alias;
      param.component       = component;
      param.version         = version;
      param.operatingSystem = operatingSystem;
      param.platform        = platform;
      param.summary         = summary;
      param.description     = description;
      param.priority        = priority;
      param.severity        = severity;
      param.status          = status;
      param.targetMilestone = targetMilestone;
      param.assignedTo      = assignedTo;
      param.cc              = cc;
      param.qaContact       = qaContact;
      return server.GetBug( server.Proxy.CreateBug( param ).id );
    }
    
    
  }

}
