﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRMConnectivity.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CRMConnectivity.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;Queries&gt;
        ///  &lt;Query Id=&quot;GetTestCompany&quot;&gt;
        ///    &lt;![CDATA[
        ///    &lt;fetch mapping=&quot;logical&quot; version=&quot;1.0&quot; output-format=&quot;xml-platform&quot; distinct=&quot;false&quot;&gt;
        ///      &lt;entity name=&quot;account&quot;&gt;
        ///        &lt;attribute name=&quot;name&quot; /&gt;
        ///        &lt;attribute name=&quot;primarycontactid&quot; /&gt;
        ///        &lt;attribute name=&quot;telephone1&quot; /&gt;
        ///        &lt;attribute name=&quot;accountid&quot; /&gt;
        ///        &lt;order descending=&quot;false&quot; attribute=&quot;name&quot; /&gt;
        ///        &lt;filter type=&quot;and&quot;&gt;
        ///          &lt;condition value=&quot;10&quot; attribute=&quot;na [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Fetch {
            get {
                return ResourceManager.GetString("Fetch", resourceCulture);
            }
        }
    }
}