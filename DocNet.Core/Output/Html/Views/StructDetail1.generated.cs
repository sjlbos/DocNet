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

namespace DocNet.Razor.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    #line 2 "..\..\Output\Html\Views\StructDetail.cshtml"
    using DocNet.Core.Models.CSharp;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Output\Html\Views\StructDetail.cshtml"
    using DocNet.Core.Output.Html.Views;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Output\Html\Views\StructDetail.cshtml"
    using DocNet.Razor.Helpers;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public partial class StructDetail : BodyTemplate<StructModel>
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");





WriteLiteral("\r\n<p>Struct: ");


            
            #line 7 "..\..\Output\Html\Views\StructDetail.cshtml"
      Write(Model.FullName);

            
            #line default
            #line hidden

            
            #line 7 "..\..\Output\Html\Views\StructDetail.cshtml"
                          WriteLiteral("</p>\r\n\r\n<!--Summary\r\n    Constructors\r\n    Methods\r\n    Remarks\r\n    See Also-->\r" +
"\n\r\n<!--Summary-->\r\n<p>");

            
            #line default
            #line hidden
            
            #line 16 "..\..\Output\Html\Views\StructDetail.cshtml"
     CommentTag.RenderSummary(Model.DocComment.Summary);
            
            #line default
            #line hidden
WriteLiteral("</p>\r\n\r\n<!--Constructors-->\r\n<p>\r\n    <h3>Constructors</h3>\r\n    <table class=\"ta" +
"ble table-bordered table-hover\">\r\n");


            
            #line 22 "..\..\Output\Html\Views\StructDetail.cshtml"
         foreach (ConstructorModel c in Model.Constructors){

            
            #line default
            #line hidden
WriteLiteral("            <tr>\r\n                <td>");


            
            #line 24 "..\..\Output\Html\Views\StructDetail.cshtml"
               Write(c.Name);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                <td>");


            
            #line 25 "..\..\Output\Html\Views\StructDetail.cshtml"
               Write(c.DocComment.Summary);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n            </tr>\r\n");


            
            #line 27 "..\..\Output\Html\Views\StructDetail.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </table>\r\n</p>\r\n\r\n<!--Methods-->\r\n<p>\r\n    <h3>Methods</h3>\r\n    <table class" +
"=\"table table-bordered table-hover\">\r\n");


            
            #line 35 "..\..\Output\Html\Views\StructDetail.cshtml"
         foreach (MethodModel m in Model.Methods){

            
            #line default
            #line hidden
WriteLiteral("            <tr>\r\n                <td>");


            
            #line 37 "..\..\Output\Html\Views\StructDetail.cshtml"
               Write(m.Name);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                <td>");


            
            #line 38 "..\..\Output\Html\Views\StructDetail.cshtml"
               Write(m.DocComment.Summary);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n            </tr>\r\n");


            
            #line 40 "..\..\Output\Html\Views\StructDetail.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </table>\r\n</p>\r\n\r\n<!--Remarks-->\r\n<p>\r\n    <h3>Remarks</h3>\r\n");


            
            #line 47 "..\..\Output\Html\Views\StructDetail.cshtml"
      CommentTag.RenderRemarks(Model.DocComment.Remarks);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n\r\n<!--See Also-->\r\n<p>\r\n    <h3>See Also</h3>\r\n");


            
            #line 53 "..\..\Output\Html\Views\StructDetail.cshtml"
      CommentTag.RenderSeeAlso(Model.DocComment.SeeAlso);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n");


        }
    }
}
#pragma warning restore 1591
