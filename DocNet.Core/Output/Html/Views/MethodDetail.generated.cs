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
    
    #line 4 "..\..\Output\Html\Views\MethodDetail.cshtml"
    using DocNet.Core.Models.Comments.Xml;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Output\Html\Views\MethodDetail.cshtml"
    using DocNet.Core.Models.CSharp;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Output\Html\Views\MethodDetail.cshtml"
    using DocNet.Core.Output.Html.Views;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Output\Html\Views\MethodDetail.cshtml"
    using DocNet.Razor.Helpers;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public partial class MethodDetail : BaseTemplate<MethodModel>
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");






WriteLiteral("\r\n<p><h1>");


            
            #line 8 "..\..\Output\Html\Views\MethodDetail.cshtml"
  Write(Model.Name);

            
            #line default
            #line hidden

            
            #line 8 "..\..\Output\Html\Views\MethodDetail.cshtml"
                  WriteLiteral("</h1></p>\r\n<!--Summary\r\n    Type Parameters\r\n    Returns\r\n    Parameters\r\n    Exc" +
"eptions\r\n    Remarks\r\n    Example\r\n    See Also-->\r\n\r\n<!--Summary-->\r\n<p>");

            
            #line default
            #line hidden
            
            #line 19 "..\..\Output\Html\Views\MethodDetail.cshtml"
     CommentTag.RenderSummary(Model.DocComment.Summary);
            
            #line default
            #line hidden
WriteLiteral("</p>\r\n\r\n<!--Type Parameters-->\r\n<p>\r\n    <h3>Type Parameters</h3>\r\n    <table cel" +
"lpadding=\"10\">\r\n");


            
            #line 25 "..\..\Output\Html\Views\MethodDetail.cshtml"
         foreach(TypeParameterTag t in Model.DocComment.TypeParameters){

            
            #line default
            #line hidden

            
            #line 26 "..\..\Output\Html\Views\MethodDetail.cshtml"
WriteLiteral("            <tr>");

            
            #line default
            #line hidden
            
            #line 26 "..\..\Output\Html\Views\MethodDetail.cshtml"
                  CommentTag.RenderTypeParam(t);
            
            #line default
            #line hidden
WriteLiteral("</tr>\r\n");


            
            #line 27 "..\..\Output\Html\Views\MethodDetail.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </table>\r\n</p>\r\n\r\n<!--Returns-->\r\n<p>\r\n    <h3>Returns</h3>\r\n");


            
            #line 34 "..\..\Output\Html\Views\MethodDetail.cshtml"
      CommentTag.RenderReturns(Model.DocComment.Returns);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n\r\n<!--Parameters-->\r\n<p>\r\n    <h3>Parameters</h3>\r\n    <table cellpadding=\"" +
"10\">\r\n");


            
            #line 41 "..\..\Output\Html\Views\MethodDetail.cshtml"
         foreach(ParameterTag p in Model.DocComment.Parameters){

            
            #line default
            #line hidden

            
            #line 42 "..\..\Output\Html\Views\MethodDetail.cshtml"
WriteLiteral("            <tr>");

            
            #line default
            #line hidden
            
            #line 42 "..\..\Output\Html\Views\MethodDetail.cshtml"
                  CommentTag.RenderParam(p);
            
            #line default
            #line hidden
WriteLiteral("</tr>\r\n");


            
            #line 43 "..\..\Output\Html\Views\MethodDetail.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </table>\r\n</p>\r\n\r\n<!--Exceptions-->\r\n<p>\r\n    <h3>Exceptions</h3>\r\n    <table" +
" cellpadding=\"10\">\r\n");


            
            #line 51 "..\..\Output\Html\Views\MethodDetail.cshtml"
         foreach(ExceptionTag e in Model.DocComment.Exceptions){

            
            #line default
            #line hidden

            
            #line 52 "..\..\Output\Html\Views\MethodDetail.cshtml"
WriteLiteral("            <tr>");

            
            #line default
            #line hidden
            
            #line 52 "..\..\Output\Html\Views\MethodDetail.cshtml"
                  CommentTag.RenderException(e);
            
            #line default
            #line hidden
WriteLiteral("</tr>\r\n");


            
            #line 53 "..\..\Output\Html\Views\MethodDetail.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </table>\r\n</p>\r\n\r\n<!--Remarks-->\r\n<p>\r\n    <h3>Remarks</h3>\r\n");


            
            #line 60 "..\..\Output\Html\Views\MethodDetail.cshtml"
      CommentTag.RenderRemarks(Model.DocComment.Remarks);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n\r\n<!--Example-->\r\n<p>\r\n    <h3>Example</h3>\r\n");


            
            #line 66 "..\..\Output\Html\Views\MethodDetail.cshtml"
      CommentTag.RenderExample(Model.DocComment.Example);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n\r\n<!--See Also-->\r\n<p>\r\n    <h3>See Also</h3>\r\n");


            
            #line 72 "..\..\Output\Html\Views\MethodDetail.cshtml"
      CommentTag.RenderSeeAlso(Model.DocComment.SeeAlso);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n");


        }
    }
}
#pragma warning restore 1591
