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
    
    #line 2 "..\..\Output\Html\Views\EnumDetail.cshtml"
    using DocNet.Core.Models.CSharp;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Output\Html\Views\EnumDetail.cshtml"
    using DocNet.Core.Output.Html.Helpers;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Output\Html\Views\EnumDetail.cshtml"
    using DocNet.Core.Output.Html.Views;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Output\Html\Views\EnumDetail.cshtml"
    using DocNet.Razor.Helpers;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public partial class EnumDetail : BodyTemplate<EnumModel>
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");






WriteLiteral("\r\n<p>Enum: ");


            
            #line 8 "..\..\Output\Html\Views\EnumDetail.cshtml"
    Write(Model.FullInternalName);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n\r\n");


            
            #line 10 "..\..\Output\Html\Views\EnumDetail.cshtml"
Write(PageSection.RenderDeclarationBlock(GetDeclaration.OfEnum(Model)));

            
            #line default
            #line hidden

            
            #line 10 "..\..\Output\Html\Views\EnumDetail.cshtml"
                                                                 WriteLiteral("\r\n\r\n<!--Summary-->\r\n<p>");

            
            #line default
            #line hidden
            
            #line 13 "..\..\Output\Html\Views\EnumDetail.cshtml"
     CommentTag.RenderSummary(Model.DocComment.Summary);
            
            #line default
            #line hidden
WriteLiteral("</p>\r\n\r\n<!--Members-->\r\n<p>\r\n    <h3>Members</h3>\r\n    <table class=\"table table-" +
"bordered table-hover\">\r\n");


            
            #line 19 "..\..\Output\Html\Views\EnumDetail.cshtml"
         foreach(string s in Model.Fields){

            
            #line default
            #line hidden
WriteLiteral("            <tr><td>");


            
            #line 20 "..\..\Output\Html\Views\EnumDetail.cshtml"
               Write(s);

            
            #line default
            #line hidden
WriteLiteral("</td></tr>\r\n");


            
            #line 21 "..\..\Output\Html\Views\EnumDetail.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </table>\r\n</p>\r\n\r\n<!--Remarks-->\r\n<p>\r\n    <h3>Remarks</h3>\r\n");


            
            #line 28 "..\..\Output\Html\Views\EnumDetail.cshtml"
      CommentTag.RenderRemarks(Model.DocComment.Remarks);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n\r\n<!--Examples-->\r\n<p>\r\n    <h3>Examples</h3>\r\n");


            
            #line 34 "..\..\Output\Html\Views\EnumDetail.cshtml"
      CommentTag.RenderExample(Model.DocComment.Example);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n\r\n<!--See Also-->\r\n<p>\r\n    <h3>See Also</h3>\r\n");


            
            #line 40 "..\..\Output\Html\Views\EnumDetail.cshtml"
      CommentTag.RenderSeeAlso(Model.DocComment.SeeAlso);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n");


        }
    }
}
#pragma warning restore 1591
