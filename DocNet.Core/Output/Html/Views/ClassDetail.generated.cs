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
    
    #line 2 "..\..\Output\Html\Views\ClassDetail.cshtml"
    using System.Linq;
    
    #line default
    #line hidden
    using System.Text;
    
    #line 3 "..\..\Output\Html\Views\ClassDetail.cshtml"
    using DocNet.Core.Models.CSharp;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Output\Html\Views\ClassDetail.cshtml"
    using DocNet.Core.Output.Html.Helpers;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Output\Html\Views\ClassDetail.cshtml"
    using DocNet.Core.Output.Html.Views;
    
    #line default
    #line hidden
    
    #line 6 "..\..\Output\Html\Views\ClassDetail.cshtml"
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







WriteLiteral("\r\n<!--Name-->\r\n<p>\r\n    <h3>Class</h3>\r\n    <h1>");


            
            #line 12 "..\..\Output\Html\Views\ClassDetail.cshtml"
   Write(Model.DisplayName);

            
            #line default
            #line hidden
WriteLiteral("</h1>\r\n    <h3>Namespace: ");


            
            #line 13 "..\..\Output\Html\Views\ClassDetail.cshtml"
              Write(Model.FullNameQualifier);

            
            #line default
            #line hidden
WriteLiteral("</h3>\r\n</p>\r\n\r\n");


            
            #line 16 "..\..\Output\Html\Views\ClassDetail.cshtml"
Write(PageSection.RenderDeclarationBlock(GetDeclaration.OfClass(Model)));

            
            #line default
            #line hidden
WriteLiteral("\r\n<!--Name\r\nSummary\r\nConstructors\r\nProperties\r\nMethods\r\nExceptions\r\nRemarks\r\nExam" +
"ple\r\n    See Also-->\r\n\r\n");


            
            #line 27 "..\..\Output\Html\Views\ClassDetail.cshtml"
 if (Model.DocComment != null) { 

            
            #line default
            #line hidden
WriteLiteral("    <!--Summary-->\r\n");



WriteLiteral("    <p>");


            
            #line 29 "..\..\Output\Html\Views\ClassDetail.cshtml"
  Write(CommentTag.RenderSummary(Model.DocComment.Summary));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n");


            
            #line 30 "..\..\Output\Html\Views\ClassDetail.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n");


            
            #line 32 "..\..\Output\Html\Views\ClassDetail.cshtml"
 if (Model.Constructors.Any()){

            
            #line default
            #line hidden
WriteLiteral("    <!--Constructors-->\r\n");



WriteLiteral("    <p>\r\n        <h3>Constructors</h3>\r\n        <table class=\"table table-bordere" +
"d table-hover\">\r\n            <tr>\r\n                <td><b>Constructor Name</b></" +
"td>\r\n                <td><b>Description</b></td>\r\n            </tr>\r\n");


            
            #line 41 "..\..\Output\Html\Views\ClassDetail.cshtml"
             foreach (ConstructorModel c in Model.Constructors){

            
            #line default
            #line hidden
WriteLiteral("                <tr>\r\n                    <td>");


            
            #line 43 "..\..\Output\Html\Views\ClassDetail.cshtml"
                   Write(c.Identifier);

            
            #line default
            #line hidden

            
            #line 43 "..\..\Output\Html\Views\ClassDetail.cshtml"
                                     WriteLiteral("</td>\r\n                    <td>");

            
            #line default
            #line hidden
            
            #line 44 "..\..\Output\Html\Views\ClassDetail.cshtml"
                         if (c.DocComment != null){
                            
            
            #line default
            #line hidden
            
            #line 45 "..\..\Output\Html\Views\ClassDetail.cshtml"
                       Write(CommentTag.RenderSummary(c.DocComment.Summary));

            
            #line default
            #line hidden
            
            #line 45 "..\..\Output\Html\Views\ClassDetail.cshtml"
                                                                           
                        }
            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                </tr>\r\n");


            
            #line 48 "..\..\Output\Html\Views\ClassDetail.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </table>\r\n    </p>\r\n");


            
            #line 51 "..\..\Output\Html\Views\ClassDetail.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n");


            
            #line 53 "..\..\Output\Html\Views\ClassDetail.cshtml"
 if (Model.Properties.Any()){

            
            #line default
            #line hidden
WriteLiteral("    <!--Properties-->\r\n");



WriteLiteral("    <p>\r\n        <h3>Properties</h3>\r\n        <table class=\"table table-bordered " +
"table-hover\">\r\n            <tr>\r\n                <td><b>Property Name</b></td>\r\n" +
"                <td><b>Description</b></td>\r\n            </tr>\r\n");


            
            #line 62 "..\..\Output\Html\Views\ClassDetail.cshtml"
             foreach (PropertyModel p in Model.Properties){

            
            #line default
            #line hidden
WriteLiteral("                <tr>\r\n                    <td>");


            
            #line 64 "..\..\Output\Html\Views\ClassDetail.cshtml"
                   Write(p.Identifier);

            
            #line default
            #line hidden

            
            #line 64 "..\..\Output\Html\Views\ClassDetail.cshtml"
                                     WriteLiteral("</td>\r\n                    <td>");

            
            #line default
            #line hidden
            
            #line 65 "..\..\Output\Html\Views\ClassDetail.cshtml"
                         if (p.DocComment != null){
                            
            
            #line default
            #line hidden
            
            #line 66 "..\..\Output\Html\Views\ClassDetail.cshtml"
                       Write(CommentTag.RenderSummary(p.DocComment.Summary));

            
            #line default
            #line hidden
            
            #line 66 "..\..\Output\Html\Views\ClassDetail.cshtml"
                                                                           
                        }
            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                </tr>\r\n");


            
            #line 69 "..\..\Output\Html\Views\ClassDetail.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </table>\r\n    </p>\r\n");


            
            #line 72 "..\..\Output\Html\Views\ClassDetail.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n");


            
            #line 74 "..\..\Output\Html\Views\ClassDetail.cshtml"
 if (Model.Methods.Any()){

            
            #line default
            #line hidden
WriteLiteral("    <!--Methods-->\r\n");



WriteLiteral("    <p>\r\n        <h3>Methods</h3>\r\n        <table class=\"table table-bordered tab" +
"le-hover\">\r\n            <tr>\r\n                <td><b>Method Name</b></td>\r\n     " +
"           <td><b>Description</b></td>\r\n            </tr>\r\n");


            
            #line 83 "..\..\Output\Html\Views\ClassDetail.cshtml"
             foreach (MethodModel m in Model.Methods){

            
            #line default
            #line hidden
WriteLiteral("                <tr>\r\n                    <td>");


            
            #line 85 "..\..\Output\Html\Views\ClassDetail.cshtml"
                   Write(m.Identifier);

            
            #line default
            #line hidden

            
            #line 85 "..\..\Output\Html\Views\ClassDetail.cshtml"
                                     WriteLiteral("</td>\r\n                    <td>");

            
            #line default
            #line hidden
            
            #line 86 "..\..\Output\Html\Views\ClassDetail.cshtml"
                         if (m.DocComment != null){
                            
            
            #line default
            #line hidden
            
            #line 87 "..\..\Output\Html\Views\ClassDetail.cshtml"
                       Write(CommentTag.RenderSummary(m.DocComment.Summary));

            
            #line default
            #line hidden
            
            #line 87 "..\..\Output\Html\Views\ClassDetail.cshtml"
                                                                           
                        }
            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                </tr>\r\n");


            
            #line 90 "..\..\Output\Html\Views\ClassDetail.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </table>\r\n    </p>\r\n");


            
            #line 93 "..\..\Output\Html\Views\ClassDetail.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n<!--Exceptions-->\r\n\r\n<!--Explicit Interface Implementations-->\r\n");


            
            #line 98 "..\..\Output\Html\Views\ClassDetail.cshtml"
 if (Model.DocComment != null){

            
            #line default
            #line hidden
WriteLiteral("    <!--Remarks-->\r\n");



WriteLiteral("    <p>\r\n        <h3>Remarks</h3>\r\n");


            
            #line 102 "..\..\Output\Html\Views\ClassDetail.cshtml"
           CommentTag.RenderRemarks(Model.DocComment.Remarks); 

            
            #line default
            #line hidden
WriteLiteral("    </p>\r\n");


            
            #line 104 "..\..\Output\Html\Views\ClassDetail.cshtml"


            
            #line default
            #line hidden
WriteLiteral("    <!--Example-->\r\n");



WriteLiteral("    <p>\r\n        <h3>Example</h3>\r\n");


            
            #line 108 "..\..\Output\Html\Views\ClassDetail.cshtml"
           CommentTag.RenderExample(Model.DocComment.Example); 

            
            #line default
            #line hidden
WriteLiteral("    </p>\r\n");


            
            #line 110 "..\..\Output\Html\Views\ClassDetail.cshtml"


            
            #line default
            #line hidden
WriteLiteral("    <!--See Also-->\r\n");



WriteLiteral("    <p>\r\n        <h3>See Also</h3>\r\n        ");


            
            #line 114 "..\..\Output\Html\Views\ClassDetail.cshtml"
   Write(CommentTag.RenderSeeAlso(Model.DocComment.SeeAlso));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </p>\r\n");


            
            #line 116 "..\..\Output\Html\Views\ClassDetail.cshtml"
}

            
            #line default
            #line hidden

        }
    }
}
#pragma warning restore 1591
