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
    
    #line 4 "..\..\Output\Html\Views\DelegateDetail.cshtml"
    using DocNet.Core.Models.Comments.Xml;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Output\Html\Views\DelegateDetail.cshtml"
    using DocNet.Core.Models.CSharp;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Output\Html\Views\DelegateDetail.cshtml"
    using DocNet.Core.Output.Html.Helpers;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Output\Html\Views\DelegateDetail.cshtml"
    using DocNet.Core.Output.Html.Views;
    
    #line default
    #line hidden
    
    #line 6 "..\..\Output\Html\Views\DelegateDetail.cshtml"
    using DocNet.Razor.Helpers;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public partial class DelegateDetail : BodyTemplate<DelegateModel>
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");







WriteLiteral("\r\n<!--Name-->\r\n<p>\r\n    <h3>Delegate</h3>\r\n    <h1>");


            
            #line 12 "..\..\Output\Html\Views\DelegateDetail.cshtml"
   Write(Model.DisplayName);

            
            #line default
            #line hidden
WriteLiteral("</h1>\r\n    <h3>Namespace: ");


            
            #line 13 "..\..\Output\Html\Views\DelegateDetail.cshtml"
              Write(Model.FullNameQualifier);

            
            #line default
            #line hidden
WriteLiteral("</h3>\r\n</p>\r\n\r\n");


            
            #line 16 "..\..\Output\Html\Views\DelegateDetail.cshtml"
Write(PageSection.RenderDeclarationBlock(GetDeclaration.OfDelegate(Model)));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 18 "..\..\Output\Html\Views\DelegateDetail.cshtml"
 if (Model.DocComment != null){

            
            #line default
            #line hidden
WriteLiteral("    <!--Summary-->\r\n");



WriteLiteral("    <p>");


            
            #line 20 "..\..\Output\Html\Views\DelegateDetail.cshtml"
  Write(CommentTag.RenderSummary(Model.DocComment.Summary));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n");


            
            #line 21 "..\..\Output\Html\Views\DelegateDetail.cshtml"


            
            #line default
            #line hidden
WriteLiteral("    <!--Type Parameters-->\r\n");



WriteLiteral("    <p>\r\n        <h3>Type Parameters</h3>\r\n        <table class=\"table table-bord" +
"ered table-hover\">\r\n");


            
            #line 26 "..\..\Output\Html\Views\DelegateDetail.cshtml"
             foreach (TypeParameterTag t in Model.DocComment.TypeParameters){

            
            #line default
            #line hidden
WriteLiteral("                <tr><td>");


            
            #line 27 "..\..\Output\Html\Views\DelegateDetail.cshtml"
                   Write(CommentTag.RenderTypeParam(t));

            
            #line default
            #line hidden
WriteLiteral("</td></tr>\r\n");


            
            #line 28 "..\..\Output\Html\Views\DelegateDetail.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </table>\r\n    </p>\r\n");


            
            #line 31 "..\..\Output\Html\Views\DelegateDetail.cshtml"


            
            #line default
            #line hidden
WriteLiteral("    <!--Returns-->\r\n");



WriteLiteral("    <p>\r\n        <h3>Returns</h3>\r\n        ");


            
            #line 35 "..\..\Output\Html\Views\DelegateDetail.cshtml"
   Write(CommentTag.RenderReturns(Model.DocComment.Returns));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </p>\r\n");


            
            #line 37 "..\..\Output\Html\Views\DelegateDetail.cshtml"


            
            #line default
            #line hidden
WriteLiteral("    <!--Parameters-->\r\n");



WriteLiteral("    <p>\r\n        <h3>Parameters</h3>\r\n        <table class=\"table table-bordered " +
"table-hover\">\r\n");


            
            #line 42 "..\..\Output\Html\Views\DelegateDetail.cshtml"
             foreach (ParameterTag p in Model.DocComment.Parameters){

            
            #line default
            #line hidden
WriteLiteral("                <tr><td>");


            
            #line 43 "..\..\Output\Html\Views\DelegateDetail.cshtml"
                   Write(CommentTag.RenderParam(p));

            
            #line default
            #line hidden
WriteLiteral("</td></tr>\r\n");


            
            #line 44 "..\..\Output\Html\Views\DelegateDetail.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </table>\r\n    </p>\r\n");


            
            #line 47 "..\..\Output\Html\Views\DelegateDetail.cshtml"


            
            #line default
            #line hidden
WriteLiteral("    <!--Exceptions-->\r\n");



WriteLiteral("    <p>\r\n        <h3>Exceptions</h3>\r\n        <table class=\"table table-bordered " +
"table-hover\">\r\n");


            
            #line 52 "..\..\Output\Html\Views\DelegateDetail.cshtml"
             foreach (ExceptionTag e in Model.DocComment.Exceptions){

            
            #line default
            #line hidden
WriteLiteral("                <tr><td>");


            
            #line 53 "..\..\Output\Html\Views\DelegateDetail.cshtml"
                   Write(CommentTag.RenderException(e));

            
            #line default
            #line hidden
WriteLiteral("</td></tr>\r\n");


            
            #line 54 "..\..\Output\Html\Views\DelegateDetail.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </table>\r\n    </p>\r\n");


            
            #line 57 "..\..\Output\Html\Views\DelegateDetail.cshtml"


            
            #line default
            #line hidden
WriteLiteral("    <!--Remarks-->\r\n");



WriteLiteral("    <p>\r\n        <h3>Remarks</h3>\r\n        ");


            
            #line 61 "..\..\Output\Html\Views\DelegateDetail.cshtml"
   Write(CommentTag.RenderRemarks(Model.DocComment.Remarks));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </p>\r\n");


            
            #line 63 "..\..\Output\Html\Views\DelegateDetail.cshtml"


            
            #line default
            #line hidden
WriteLiteral("    <!--Example-->\r\n");



WriteLiteral("    <p>\r\n        <h3>Example</h3>\r\n        ");


            
            #line 67 "..\..\Output\Html\Views\DelegateDetail.cshtml"
   Write(CommentTag.RenderExample(Model.DocComment.Example));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </p>\r\n");


            
            #line 69 "..\..\Output\Html\Views\DelegateDetail.cshtml"


            
            #line default
            #line hidden
WriteLiteral("    <!--See Also-->\r\n");



WriteLiteral("    <p>\r\n        <h3>See Also</h3>\r\n        ");


            
            #line 73 "..\..\Output\Html\Views\DelegateDetail.cshtml"
   Write(CommentTag.RenderSeeAlso(Model.DocComment.SeeAlso));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </p>\r\n");


            
            #line 75 "..\..\Output\Html\Views\DelegateDetail.cshtml"
}
            
            #line default
            #line hidden

        }
    }
}
#pragma warning restore 1591
