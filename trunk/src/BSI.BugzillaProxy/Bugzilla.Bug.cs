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
  \brief Implementation of the IBug interface.

  Instances of this class can be used to manipulate a Bug in a
  Bugzilla server.

  In order to actually instantiate a Bug, a Server instance must be
  supplied.

*/

using CookComputing.XmlRpc;
using System;
using Bugzilla.ProxyStructs;

namespace Bugzilla {
  /// <summary>This class implements an Bugzilla.IBug interface</summary>
  public class Bug : IBug {
    private Server server;
    private BugInfo bi; // Must always be valid

    /*! \name Constructors */
    
    //@{

    // This assumes the bug allready exists on the server side.
    internal Bug( Server server, BugInfo bi ) {
      this.server = server;
      this.bi = bi;
    }
    //@}

    public void Update() {
      int[] ids = new int[1];
      ids[0]    = bi.id;
      BugIds param;
      param.ids = ids;
      checked {
	        this.bi   = server.Proxy.GetBugs( param ).bugs[0];
      }
    }


    /*! \name Experimental */
    
    //@{



    public void AppendComment( string comment, bool? isPrivate, int? worktime ) {
      AppendCommentParam param = new AppendCommentParam();
      param.id        = bi.id;
      param.comment   = comment;
      param.isPrivate = isPrivate;
      param.worktime  = worktime;
      server.Proxy.AppendComment( param );
      /*! \todo, call update? */
    }


    //@}
    
    public int Id {
      get { return bi.id; }
    }

    public System.DateTime Created {
      get { return bi.created; }
    }

    public System.DateTime Changed {
      get { return bi.changed; }
    }

    public string Alias {
      get { return bi.alias; }
    }

    public string Resolution {
      get {
	throw new Exception( "Unimplemented" );
      }
      set {
	SetBugResolutionParam parameters;
	parameters.bug_id     = bi.id;
	parameters.resolution = value;
	server.Proxy.SetBugResolution( parameters );
      }
    }
    
    public string Summary {
      get {
	return bi.summary;
      }
      set {
	throw new Exception( "Unimplemented" );
      }
    }

  }; // class Bug

}; // namespace Bugzilla
