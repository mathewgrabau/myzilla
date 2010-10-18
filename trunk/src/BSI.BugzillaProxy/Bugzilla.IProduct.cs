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
  \brief Declare an interface for accessing a Bugzilla product. 

  Bugs are associated with products and components. This is an
  interface that describes the methods and properties of an
  abstraction of a product.
*/


using CookComputing.XmlRpc;

namespace Bugzilla {
  
  /// <summary>Interface that abstracts the operations on a
  /// Product.</summary>
  public interface IProduct {
    
    /// <summary>The id of the product.</summary>
    int Id { get; }
    
    /// <summary>The name of the product.</summary>
    string Name { get; set; }

    /// <summary>The description of the product</summary>
    string Description { get; set; }

    /// <summary>Get list of legal values for a bug field</summary>
    /// <remarks>This can be used to retrieve a list of legal values
    /// for both product specific fields, as well as non-product
    /// specific fields of a bug, such as status, severity, component,
    /// and so on. This method understands both the bugzilla original
    /// naming (such as op_sys) and the naming used here (such as
    /// operatingSystem).</remarks> <returns>A list of legal values for
    /// the field</returns> <param name="fieldName">The name of a
    /// field.</param>
    string[] GetLegalFieldValues( string fieldName );

    /// <summary>Get a list of the components of this product.</summary>
    /// <remarks>This just calls GetLegalFieldValues( "component" ).</remarks>
    /// <returns>A list of the components of the product.</returns>
    string[] GetComponents();

    /*! \example CreateBug.cs
     * This is an example on how to use the Bugzilla.IProduct.CreateBug call */

    /// <summary>Create a new bug on this product</summary>
    /// <remarks>The parameters for this call can be marked "optional"
    /// or "defaulted". Optional parameters can be left out in all
    /// %Bugzilla installations, and a default value from the server
    /// will be substituted. Defaulted parameters can be left out of
    /// some installations, while other installations may require
    /// these parameters to be present. This is decided in the
    /// %Bugzilla preferences. If you wish to make sure that the call
    /// works with all %Bugzilla installations, you should supply
    /// values for all "defaulted" parameters. Use GetLegalFieldValues
    /// to get a list of the legal values for a given
    /// field. Parameters not marked optional or defaulted are
    /// required.</remarks> 
    /// <param name="alias">If aliases are enabled for the %Bugzilla
    /// server, you can supply an unique identifier (no spaces or
    /// weird characters) to identify the bug with, in
    /// addition to the id.</param>
    /// <param name="component">The name of the component that the bug
    /// will be created under.</param>
    /// <param name="version">The version of the product, that the bug
    /// was found in.</param>
    /// <param name="operatingSystem">(Defaulted) The operating system
    /// the bug was discovered on.</param>
    /// <param name="platform">(Defaulted) What type of hardware the
    /// bug was experienced on.</param>
    /// <param name="summary">Summary of the bug.</param>
    /// <param name="description">(Defaulted) Description of the
    /// bug.</param>
    /// <param name="priority">(Defaulted) What order the bug will be
    /// fixed in by the developer, compared to the developers other
    /// bugs.</param>
    /// <param name="severity">(Defaulted) How severe the bug
    /// is.</param>
    /// <param name="status">(Optional) The status that this bug
    /// should start out as. Note that only certain statuses can be
    /// set on bug creation.</param>
    /// <param name="targetMilestone">(Optional) A valid target
    /// milestone for this product.</param>
    /// <param name="assignedTo">(Optional) A user to assign this bug
    /// to, if you dont want it to be assigned to the component
    /// owner.</param>
    /// <param name="cc">(Optional) An array of usernames to CC on
    /// this bug.</param>
    /// <param name="qaContact">(Optional) If this installation has QA
    /// Contacts enabled, you can set the QA Contact here if you dont
    /// want to use the components default QA Contact. </param>
    IBug CreateBug( 
		   string alias, 
		   string component, 
		   string version, 
		   string operatingSystem, 
		   string platform,
		   string summary, 
		   string description,
		   string priority, 
		   string severity, 
		   string status,
		   string targetMilestone, 
		   string assignedTo, 
		   string[] cc,
		   string qaContact );
    
  }

}
