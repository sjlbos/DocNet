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
    
    #line 2 "..\..\Output\Html\Views\EnumDetail.cshtml"
    using DocNet.Core.Models.CSharp;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Output\Html\Views\EnumDetail.cshtml"
    using DocNet.Core.Output.Html.Views;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Output\Html\Views\EnumDetail.cshtml"
    using DocNet.Razor.Helpers;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public partial class EnumDetail : BaseTemplate<EnumModel>
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");





WriteLiteral("\r\n<p>Enum: ");


            
            #line 7 "..\..\Output\Html\Views\EnumDetail.cshtml"
    Write(Model.FullName);

            
            #line default
            #line hidden

            
            #line 7 "..\..\Output\Html\Views\EnumDetail.cshtml"
                        WriteLiteral("</p>\r\n\r\n<!--Summary\r\n    Members\r\n    Remarks\r\n    Examples\r\n    See Also-->\r\n\r\n<" +
"!--Summary-->\r\n<p>");

            
            #line default
            #line hidden
            
            #line 16 "..\..\Output\Html\Views\EnumDetail.cshtml"
     CommentTag.RenderSummary(Model.DocComment.Summary);
            
            #line default
            #line hidden
WriteLiteral("</p>\r\n\r\n<!--Members-->\r\n<p>\r\n    <h3>Members</h3>\r\n    <table cellpadding=\"10\">\r\n" +
"");


            
            #line 22 "..\..\Output\Html\Views\EnumDetail.cshtml"
         foreach(string s in Model.Fields){

            
            #line default
            #line hidden
WriteLiteral("            <tr><td>");


            
            #line 23 "..\..\Output\Html\Views\EnumDetail.cshtml"
               Write(s);

            
            #line default
            #line hidden
WriteLiteral("</td></tr>\r\n");


            
            #line 24 "..\..\Output\Html\Views\EnumDetail.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </table>\r\n</p>\r\n\r\n<!--Remarks-->\r\n<p>\r\n    <h3>Remarks</h3>\r\n");


            
            #line 31 "..\..\Output\Html\Views\EnumDetail.cshtml"
      CommentTag.RenderRemarks(Model.DocComment.Remarks);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n\r\n<!--Examples-->\r\n<p>\r\n    <h3>Examples</h3>\r\n");


            
            #line 37 "..\..\Output\Html\Views\EnumDetail.cshtml"
      CommentTag.RenderExample(Model.DocComment.Example);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n\r\n<!--See Also-->\r\n<p>\r\n    <h3>See Also</h3>\r\n");


            
            #line 43 "..\..\Output\Html\Views\EnumDetail.cshtml"
      CommentTag.RenderSeeAlso(Model.DocComment.SeeAlso);

            
            #line default
            #line hidden
WriteLiteral("</p>");


        }
    }
}
#pragma warning restore 1591
