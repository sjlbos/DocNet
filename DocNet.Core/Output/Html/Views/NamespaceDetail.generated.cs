﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DocNet.RazorGenerator.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    #line 2 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
    using DocNet.Core.Models.CSharp;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
    using DocNet.Core.Output.Html.Views;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public partial class NamespaceDetail : BaseTemplate<NamespaceModel>
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");




WriteLiteral("\r\n<p>Namespace: ");


            
            #line 6 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
         Write(Model.FullName);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n");


        }
    }
}
#pragma warning restore 1591
