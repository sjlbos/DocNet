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
    
    #line 2 "..\..\Output\Html\Views\EnumDetail.cshtml"
    using System.Linq;
    
    #line default
    #line hidden
    using System.Text;
    
    #line 3 "..\..\Output\Html\Views\EnumDetail.cshtml"
    using DocNet.Core.Models.CSharp;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Output\Html\Views\EnumDetail.cshtml"
    using DocNet.Core.Output.Html.Helpers;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Output\Html\Views\EnumDetail.cshtml"
    using DocNet.Core.Output.Html.Views;
    
    #line default
    #line hidden
    
    #line 6 "..\..\Output\Html\Views\EnumDetail.cshtml"
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







WriteLiteral("\r\n");


            
            #line 9 "..\..\Output\Html\Views\EnumDetail.cshtml"
Write(PageSection.RenderElementTitle(Model, "Enum"));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 11 "..\..\Output\Html\Views\EnumDetail.cshtml"
Write(PageSection.RenderDeclarationBlock(GetDeclaration.OfEnum(Model)));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 13 "..\..\Output\Html\Views\EnumDetail.cshtml"
Write(PageSection.RenderSummary(Model.DocComment));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 15 "..\..\Output\Html\Views\EnumDetail.cshtml"
 if (Model.Fields.Any()){

            
            #line default
            #line hidden
WriteLiteral("    <!--Members-->\r\n");



WriteLiteral("    <p>\r\n        <h3>Members</h3>\r\n        <table class=\"table table-bordered tab" +
"le-hover\">\r\n");


            
            #line 20 "..\..\Output\Html\Views\EnumDetail.cshtml"
             foreach (string s in Model.Fields){

            
            #line default
            #line hidden
WriteLiteral("                <tr><td>");


            
            #line 21 "..\..\Output\Html\Views\EnumDetail.cshtml"
                   Write(s);

            
            #line default
            #line hidden
WriteLiteral("</td></tr>\r\n");


            
            #line 22 "..\..\Output\Html\Views\EnumDetail.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </table>\r\n    </p>\r\n");


            
            #line 25 "..\..\Output\Html\Views\EnumDetail.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n");


            
            #line 27 "..\..\Output\Html\Views\EnumDetail.cshtml"
Write(PageSection.RenderRemarks(Model.DocComment));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 29 "..\..\Output\Html\Views\EnumDetail.cshtml"
Write(PageSection.RenderExample(Model.DocComment));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 31 "..\..\Output\Html\Views\EnumDetail.cshtml"
Write(PageSection.RenderSeeAlso(Model.DocComment));

            
            #line default
            #line hidden
WriteLiteral("\r\n");


        }
    }
}
#pragma warning restore 1591
