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
    
    #line 2 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
    using DocNet.Core.Models.CSharp;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
    using DocNet.Core.Output.Html.Helpers;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
    using DocNet.Core.Output.Html.Views;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
    using DocNet.Razor.Helpers;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public partial class InterfaceDetail : BodyTemplate<InterfaceModel>
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");






WriteLiteral("\r\n<!--Name-->\r\n<p>\r\n    <h3>Interface</h3>\r\n    <h1>");


            
            #line 11 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
   Write(Model.DisplayName);

            
            #line default
            #line hidden
WriteLiteral("</h1>\r\n    <h3>Namespace: ");


            
            #line 12 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
              Write(Model.FullNameQualifier);

            
            #line default
            #line hidden
WriteLiteral("</h3>\r\n</p>\r\n\r\n");


            
            #line 15 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
Write(PageSection.RenderDeclarationBlock(GetDeclaration.OfInterface(Model)));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 17 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
Write(PageSection.RenderSummary(Model.DocComment));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 19 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
Write(PageSection.RenderTypeParamTable(Model));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 21 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
Write(PageSection.RenderExceptionTable(Model.DocComment));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 23 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
Write(PageSection.RenderPropertyTable(Model));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 25 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
Write(PageSection.RenderMethodTable(Model));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 27 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
Write(PageSection.RenderRemarks(Model.DocComment));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 29 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
Write(PageSection.RenderExample(Model.DocComment));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 31 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
Write(PageSection.RenderSeeAlso(Model.DocComment));

            
            #line default
            #line hidden
WriteLiteral("\r\n");


        }
    }
}
#pragma warning restore 1591
