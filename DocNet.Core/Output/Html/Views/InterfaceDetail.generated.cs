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
    
    #line 2 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
    using System.Linq;
    
    #line default
    #line hidden
    using System.Text;
    
    #line 3 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
    using DocNet.Core.Models.CSharp;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
    using DocNet.Core.Output.Html.Helpers;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
    using DocNet.Core.Output.Html.Views;
    
    #line default
    #line hidden
    
    #line 6 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
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


            
            #line 12 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
   Write(Model.DisplayName);

            
            #line default
            #line hidden
WriteLiteral("</h1>\r\n    <h3>Namespace: ");


            
            #line 13 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
              Write(Model.FullNameQualifier);

            
            #line default
            #line hidden
WriteLiteral("</h3>\r\n</p>\r\n\r\n");


            
            #line 16 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
Write(PageSection.RenderDeclarationBlock(GetDeclaration.OfInterface(Model)));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 18 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
Write(PageSection.RenderSummary(Model.DocComment));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 20 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
Write(PageSection.RenderTypeParamTable(Model));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 22 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
 if (Model.Methods.Any()){

            
            #line default
            #line hidden
WriteLiteral("    <!--Methods-->\r\n");



WriteLiteral("    <p>\r\n        <table class=\"table table-bordered table-hover\">\r\n");


            
            #line 26 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
             foreach (MethodModel m in Model.Methods){

            
            #line default
            #line hidden
WriteLiteral("                <tr>\r\n                    <td>");


            
            #line 28 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
                   Write(m.Identifier);

            
            #line default
            #line hidden

            
            #line 28 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
                                     WriteLiteral("</td>\r\n                    <td>");

            
            #line default
            #line hidden
            
            #line 29 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
                         if (m.DocComment != null){
                            
            
            #line default
            #line hidden
            
            #line 30 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
                       Write(CommentTag.RenderSummary(m.DocComment.Summary));

            
            #line default
            #line hidden
            
            #line 30 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
                                                                           
                        }
            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                </tr>\r\n");


            
            #line 33 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </table>\r\n    </p>\r\n");


            
            #line 36 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n");


            
            #line 38 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
Write(PageSection.RenderRemarks(Model.DocComment));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 40 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
Write(PageSection.RenderExample(Model.DocComment));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 42 "..\..\Output\Html\Views\InterfaceDetail.cshtml"
Write(PageSection.RenderSeeAlso(Model.DocComment));

            
            #line default
            #line hidden
WriteLiteral("\r\n");


        }
    }
}
#pragma warning restore 1591
