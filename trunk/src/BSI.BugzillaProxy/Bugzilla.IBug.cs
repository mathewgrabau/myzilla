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
  \brief Declare an interface for accessing a Bugzilla bug.

  Bugs are, of course, what Bugzilla tracks. This is the interface of
  the bug abstraction. In order to do anything with bugs, you need bug
  objects. 

*/
namespace Bugzilla {
  /// <summary>Abstracts the operations on a bug from a %Bugzilla
  /// server.</summary><remarks>One should assume that all operations
  /// on a Bug will create network traffic, unless specifically
  /// indicated they will not.</remarks>
  public interface IBug {
    
    /*! \name General methods */
    //@{ 
    
    /// <summary>Update bug from server</summary> <remarks>Get any
    /// changes of the bug from the server. This updates information
    /// such as the time/date of the last change to the bug,
    /// etc.</remarks>
    void Update();

    //@}

    /*! \name Experimental

    Experimental methods require patches. */
    //@{

    /*! \example AppendComment.cs
     * This is an example on how to use the Bugzilla.IBug.AppendComment function */

    /// <summary>Append a comment to the bug.</summary>
    /// <remarks>If isPrivate is null, the comment is assumed public</remarks>
    /// <param name="comment">The comment to append</param>
    /// <param name="isPrivate">Wheter or not this is a private comment</param>
    /// <param name="worktime">The worktime of this comment? Can be null or 0</param>
    void AppendComment( string comment, bool? isPrivate, int? worktime );

    /// <summary>The resolution of this bug</summary>
    string Resolution {
      get;
      set;
    }
    
    //@}
    
    
    /// <summary>The summary of this bug</summary>
    string Summary {
      get;
      set; 
    }

    /// <summary>The id of this bug. Readonly</summary>
    int Id {
      get;
    }
    
    /// <summary>The alias of this bug. Readonly</summary>
    string Alias {
      get;
    }

    /// <summary>The creation time/date of this bug. Readonly</summary>
    /// <remarks>Wil
    System.DateTime Created {
      get;
    }

    /// <summary>When this bug was last changed. Readonly</summary>
    System.DateTime Changed {
      get;
    }
    
  }
  

}; // namespace

