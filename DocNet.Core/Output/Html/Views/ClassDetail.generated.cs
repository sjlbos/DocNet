﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
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
    
    #line 2 "..\..\Output\Html\Views\ClassDetail.cshtml"
    using DocNet.Core.Models.CSharp;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Output\Html\Views\ClassDetail.cshtml"
    using DocNet.Core.Output.Html.Helpers;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Output\Html\Views\ClassDetail.cshtml"
    using DocNet.Core.Output.Html.Views;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Output\Html\Views\ClassDetail.cshtml"
    using DocNet.Razor.Helpers;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public partial class ClassDetail : BodyTemplate<ClassModel>
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");






WriteLiteral("\r\n<!--Name-->\r\n<p><h1>Class: ");


            
            #line 9 "..\..\Output\Html\Views\ClassDetail.cshtml"
         Write(Model.FullInternalName);

            
            #line default
            #line hidden
WriteLiteral("</h1></p>\r\n\r\n");


            
            #line 11 "..\..\Output\Html\Views\ClassDetail.cshtml"
Write(PageSection.RenderDeclarationBlock(GetDeclaration.OfClass(Model)));

            
            #line default
            #line hidden

            
            #line 11 "..\..\Output\Html\Views\ClassDetail.cshtml"
                                                                  WriteLiteral("\r\n\r\n<!--Summary-->\r\n<p>");

            
            #line default
            #line hidden
            
            #line 14 "..\..\Output\Html\Views\ClassDetail.cshtml"
     CommentTag.RenderSummary(Model.DocComment.Summary);
            
            #line default
            #line hidden
WriteLiteral("</p>\r\n\r\n<!--Constructors-->\r\n<p>\r\n    <h3>Constructors</h3>\r\n    <table class=\"ta" +
"ble table-bordered table-hover\">\r\n");


            
            #line 20 "..\..\Output\Html\Views\ClassDetail.cshtml"
         foreach(ConstructorModel c in Model.Constructors){

            
            #line default
            #line hidden
WriteLiteral("            <tr>\r\n                <td>");


            
            #line 22 "..\..\Output\Html\Views\ClassDetail.cshtml"
               Write(c.Identifier);

            
            #line default
            #line hidden

            
            #line 22 "..\..\Output\Html\Views\ClassDetail.cshtml"
                                 WriteLiteral("</td>\r\n                <td>");

            
            #line default
            #line hidden
            
            #line 23 "..\..\Output\Html\Views\ClassDetail.cshtml"
                      CommentTag.RenderSummary(c.DocComment.Summary);
            
            #line default
            #line hidden
WriteLiteral("</td>\r\n            </tr>\r\n");


            
            #line 25 "..\..\Output\Html\Views\ClassDetail.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </table>\r\n</p>\r\n\r\n<!--Properties-->\r\n<p>\r\n    <h3>Properties</h3>\r\n    <table" +
" class=\"table table-bordered table-hover\">\r\n");


            
            #line 33 "..\..\Output\Html\Views\ClassDetail.cshtml"
         foreach (PropertyModel p in Model.Properties){

            
            #line default
            #line hidden
WriteLiteral("            <tr>\r\n                <td>");


            
            #line 35 "..\..\Output\Html\Views\ClassDetail.cshtml"
               Write(p.Identifier);

            
            #line default
            #line hidden

            
            #line 35 "..\..\Output\Html\Views\ClassDetail.cshtml"
                                 WriteLiteral("</td>\r\n                <td>");

            
            #line default
            #line hidden
            
            #line 36 "..\..\Output\Html\Views\ClassDetail.cshtml"
                      CommentTag.RenderSummary(p.DocComment.Summary);
            
            #line default
            #line hidden
WriteLiteral("</td>\r\n            </tr>\r\n");


            
            #line 38 "..\..\Output\Html\Views\ClassDetail.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </table>\r\n</p>\r\n\r\n<!--Methods-->\r\n<p>\r\n    <h3>Methods</h3>\r\n    <table class" +
"=\"table table-bordered table-hover\">\r\n");


            
            #line 46 "..\..\Output\Html\Views\ClassDetail.cshtml"
         foreach (MethodModel m in Model.Methods){

            
            #line default
            #line hidden
WriteLiteral("            <tr>\r\n                <td>");


            
            #line 48 "..\..\Output\Html\Views\ClassDetail.cshtml"
               Write(m.Identifier);

            
            #line default
            #line hidden

            
            #line 48 "..\..\Output\Html\Views\ClassDetail.cshtml"
                                 WriteLiteral("</td>\r\n                <td>");

            
            #line default
            #line hidden
            
            #line 49 "..\..\Output\Html\Views\ClassDetail.cshtml"
                      CommentTag.RenderSummary(m.DocComment.Summary);
            
            #line default
            #line hidden
WriteLiteral("</td>\r\n            </tr>\r\n");


            
            #line 51 "..\..\Output\Html\Views\ClassDetail.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </table>\r\n</p>\r\n\r\n<!--Explicit Interface Implementations-->\r\n\r\n<!--Remarks-->" +
"\r\n<p>\r\n    <h3>Remarks</h3>\r\n");


            
            #line 60 "..\..\Output\Html\Views\ClassDetail.cshtml"
      CommentTag.RenderRemarks(Model.DocComment.Remarks);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n\r\n<!--Example-->\r\n<p>\r\n    <h3>Example</h3>\r\n");


            
            #line 66 "..\..\Output\Html\Views\ClassDetail.cshtml"
      CommentTag.RenderExample(Model.DocComment.Example);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n\r\n<!--See Also-->\r\n<p>\r\n    <h3>See Also</h3>\r\n");


            
            #line 72 "..\..\Output\Html\Views\ClassDetail.cshtml"
      CommentTag.RenderSeeAlso(Model.DocComment.SeeAlso);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n");


        }
    }
}
#pragma warning restore 1591
