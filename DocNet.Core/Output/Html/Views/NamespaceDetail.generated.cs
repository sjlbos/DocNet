﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
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
    
    #line 4 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
    using DocNet.Core.Models.Comments.Xml;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
    using DocNet.Core.Models.CSharp;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
    using DocNet.Core.Output.Html.Views;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
    using DocNet.Razor.Helpers;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public partial class NamespaceDetail : BaseTemplate<NamespaceModel>
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");






WriteLiteral("\r\n<!--Name-->\r\n<p><h1>");


            
            #line 9 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
  Write(Model.FullName);

            
            #line default
            #line hidden
WriteLiteral("</h1></p>\r\n\r\n<!--Classes-->\r\n<p>\r\n    <h3>Classes</h3>\r\n    <table cellpadding=\"1" +
"0\">\r\n");


            
            #line 15 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
         foreach (ClassModel c in Model.Classes){

            
            #line default
            #line hidden
WriteLiteral("            <tr>\r\n                <td>");


            
            #line 17 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
               Write(c.Name);

            
            #line default
            #line hidden

            
            #line 17 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
                           WriteLiteral("</td>\r\n                <td>");

            
            #line default
            #line hidden
            
            #line 18 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
                      CommentTag.RenderSummary(c.DocComment.Summary);
            
            #line default
            #line hidden
WriteLiteral("</td>\r\n            </tr>\r\n");


            
            #line 20 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </table>\r\n</p>\r\n\r\n<!--Structures-->\r\n<p>\r\n    <h3>Structures</h3>\r\n    <table" +
" cellpadding=\"10\">\r\n");


            
            #line 28 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
         foreach (StructModel s in Model.Structs){

            
            #line default
            #line hidden
WriteLiteral("            <tr>\r\n                <td>");


            
            #line 30 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
               Write(s.Name);

            
            #line default
            #line hidden

            
            #line 30 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
                           WriteLiteral("</td>\r\n                <td>");

            
            #line default
            #line hidden
            
            #line 31 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
                      CommentTag.RenderSummary(s.DocComment.Summary);
            
            #line default
            #line hidden
WriteLiteral("</td>\r\n            </tr>\r\n");


            
            #line 33 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </table>\r\n</p>\r\n\r\n<!--Delegates-->\r\n<p>\r\n    <h3>Delegates</h3>\r\n    <table c" +
"ellpadding=\"10\">\r\n");


            
            #line 41 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
         foreach (DelegateModel d in Model.Delegates){

            
            #line default
            #line hidden
WriteLiteral("            <tr>\r\n                <td>");


            
            #line 43 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
               Write(d.Name);

            
            #line default
            #line hidden

            
            #line 43 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
                           WriteLiteral("</td>\r\n                <td>");

            
            #line default
            #line hidden
            
            #line 44 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
                      CommentTag.RenderSummary(d.DocComment.Summary);
            
            #line default
            #line hidden
WriteLiteral("</td>\r\n            </tr>\r\n");


            
            #line 46 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </table>\r\n</p>\r\n\r\n<!--Enumerations-->\r\n<p>\r\n    <h3>Enumerations</h3>\r\n    <t" +
"able cellpadding=\"10\">\r\n");


            
            #line 54 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
         foreach (EnumModel e in Model.Enums){

            
            #line default
            #line hidden
WriteLiteral("            <tr>\r\n                <td>");


            
            #line 56 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
               Write(e.Name);

            
            #line default
            #line hidden

            
            #line 56 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
                           WriteLiteral("</td>\r\n                <td>");

            
            #line default
            #line hidden
            
            #line 57 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
                      CommentTag.RenderSummary(e.DocComment.Summary);
            
            #line default
            #line hidden
WriteLiteral("</td>\r\n            </tr>\r\n");


            
            #line 59 "..\..\Output\Html\Views\NamespaceDetail.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </table>\r\n</p>");


        }
    }
}
#pragma warning restore 1591
