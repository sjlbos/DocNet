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
    
    #line 2 "..\..\Output\Html\Views\StructDetail.cshtml"
    using System.Linq;
    
    #line default
    #line hidden
    using System.Text;
    
    #line 3 "..\..\Output\Html\Views\StructDetail.cshtml"
    using DocNet.Core.Models.CSharp;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Output\Html\Views\StructDetail.cshtml"
    using DocNet.Core.Output.Html.Helpers;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Output\Html\Views\StructDetail.cshtml"
    using DocNet.Core.Output.Html.Views;
    
    #line default
    #line hidden
    
    #line 6 "..\..\Output\Html\Views\StructDetail.cshtml"
    using DocNet.Razor.Helpers;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public partial class StructDetail : BodyTemplate<StructModel>
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");







WriteLiteral("\r\n<!--Name-->\r\n<p>\r\n    <h3>Interface</h3>\r\n    <h1>");


            
            #line 12 "..\..\Output\Html\Views\StructDetail.cshtml"
   Write(Model.DisplayName);

            
            #line default
            #line hidden
WriteLiteral("</h1>\r\n    <h3>Namespace: ");


            
            #line 13 "..\..\Output\Html\Views\StructDetail.cshtml"
              Write(Model.FullNameQualifier);

            
            #line default
            #line hidden
WriteLiteral("</h3>\r\n</p>\r\n\r\n");


            
            #line 16 "..\..\Output\Html\Views\StructDetail.cshtml"
Write(PageSection.RenderDeclarationBlock(GetDeclaration.OfStruct(Model)));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 18 "..\..\Output\Html\Views\StructDetail.cshtml"
 if (Model.DocComment != null)
{

            
            #line default
            #line hidden
WriteLiteral("    <!--Summary-->\r\n");




            
            #line 21 "..\..\Output\Html\Views\StructDetail.cshtml"
WriteLiteral("    <p>");

            
            #line default
            #line hidden
            
            #line 21 "..\..\Output\Html\Views\StructDetail.cshtml"
          CommentTag.RenderSummary(Model.DocComment.Summary); 
            
            #line default
            #line hidden
WriteLiteral("</p>\r\n");


            
            #line 22 "..\..\Output\Html\Views\StructDetail.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n");


            
            #line 24 "..\..\Output\Html\Views\StructDetail.cshtml"
 if (Model.Constructors.Any()){

            
            #line default
            #line hidden
WriteLiteral("    <!--Constructors-->\r\n");



WriteLiteral("    <p>\r\n        <h3>Constructors</h3>\r\n        <table class=\"table table-bordere" +
"d table-hover\">\r\n            <tr>\r\n                <td><b>Constructor Name</b></" +
"td>\r\n                <td><b>Description</b></td>\r\n            </tr>\r\n");


            
            #line 33 "..\..\Output\Html\Views\StructDetail.cshtml"
             foreach (ConstructorModel c in Model.Constructors){

            
            #line default
            #line hidden
WriteLiteral("                <tr>\r\n                    <td>");


            
            #line 35 "..\..\Output\Html\Views\StructDetail.cshtml"
                   Write(c.Identifier);

            
            #line default
            #line hidden

            
            #line 35 "..\..\Output\Html\Views\StructDetail.cshtml"
                                     WriteLiteral("</td>\r\n                    <td>");

            
            #line default
            #line hidden
            
            #line 36 "..\..\Output\Html\Views\StructDetail.cshtml"
                         if(c.DocComment!=null){
                            
            
            #line default
            #line hidden
            
            #line 37 "..\..\Output\Html\Views\StructDetail.cshtml"
                       Write(c.DocComment.Summary);

            
            #line default
            #line hidden
            
            #line 37 "..\..\Output\Html\Views\StructDetail.cshtml"
                                                 
                    }
            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                </tr>\r\n");


            
            #line 40 "..\..\Output\Html\Views\StructDetail.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </table>\r\n    </p>\r\n");


            
            #line 43 "..\..\Output\Html\Views\StructDetail.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n");


            
            #line 45 "..\..\Output\Html\Views\StructDetail.cshtml"
 if (Model.Methods.Any()){

            
            #line default
            #line hidden
WriteLiteral("    <!--Methods-->\r\n");



WriteLiteral("    <p>\r\n        <h3>Methods</h3>\r\n        <table class=\"table table-bordered tab" +
"le-hover\">\r\n            <tr>\r\n                <td><b>Method Name</b></td>\r\n     " +
"           <td><b>Description</b></td>\r\n            </tr>\r\n");


            
            #line 54 "..\..\Output\Html\Views\StructDetail.cshtml"
             foreach (MethodModel m in Model.Methods){

            
            #line default
            #line hidden
WriteLiteral("                <tr>\r\n                    <td>");


            
            #line 56 "..\..\Output\Html\Views\StructDetail.cshtml"
                   Write(m.Identifier);

            
            #line default
            #line hidden

            
            #line 56 "..\..\Output\Html\Views\StructDetail.cshtml"
                                     WriteLiteral("</td>\r\n                    <td>");

            
            #line default
            #line hidden
            
            #line 57 "..\..\Output\Html\Views\StructDetail.cshtml"
                         if (m.DocComment != null){
                            
            
            #line default
            #line hidden
            
            #line 58 "..\..\Output\Html\Views\StructDetail.cshtml"
                       Write(m.DocComment.Summary);

            
            #line default
            #line hidden
            
            #line 58 "..\..\Output\Html\Views\StructDetail.cshtml"
                                                 
                        }
            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                </tr>\r\n");


            
            #line 61 "..\..\Output\Html\Views\StructDetail.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </table>\r\n    </p>\r\n");


            
            #line 64 "..\..\Output\Html\Views\StructDetail.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n");


            
            #line 66 "..\..\Output\Html\Views\StructDetail.cshtml"
 if (Model.DocComment != null){

            
            #line default
            #line hidden
WriteLiteral("    <!--Remarks-->\r\n");



WriteLiteral("    <p>\r\n        <h3>Remarks</h3>\r\n");


            
            #line 70 "..\..\Output\Html\Views\StructDetail.cshtml"
           CommentTag.RenderRemarks(Model.DocComment.Remarks); 

            
            #line default
            #line hidden
WriteLiteral("    </p>\r\n");


            
            #line 72 "..\..\Output\Html\Views\StructDetail.cshtml"


            
            #line default
            #line hidden
WriteLiteral("    <!--See Also-->\r\n");



WriteLiteral("    <p>\r\n        <h3>See Also</h3>\r\n");


            
            #line 76 "..\..\Output\Html\Views\StructDetail.cshtml"
           CommentTag.RenderSeeAlso(Model.DocComment.SeeAlso); 

            
            #line default
            #line hidden
WriteLiteral("    </p>\r\n");


            
            #line 78 "..\..\Output\Html\Views\StructDetail.cshtml"
}
            
            #line default
            #line hidden

        }
    }
}
#pragma warning restore 1591
