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
    
    #line 2 "..\..\Output\Html\Views\ConstructorDetail.cshtml"
    using DocNet.Core.Models.CSharp;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Output\Html\Views\ConstructorDetail.cshtml"
    using DocNet.Core.Output.Html.Helpers;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Output\Html\Views\ConstructorDetail.cshtml"
    using DocNet.Core.Output.Html.Views;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Output\Html\Views\ConstructorDetail.cshtml"
    using DocNet.Razor.Helpers;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public partial class ConstructorDetail : BodyTemplate<ConstructorModel>
    {
#line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");






WriteLiteral("\r\n<p><h1>Constructor: ");


            
            #line 8 "..\..\Output\Html\Views\ConstructorDetail.cshtml"
               Write(Model.FullDisplayName);

            
            #line default
            #line hidden
WriteLiteral("</h1></p>\r\n\r\n");


            
            #line 10 "..\..\Output\Html\Views\ConstructorDetail.cshtml"
Write(PageSection.RenderDeclarationBlock(GetDeclaration.OfConstructor(Model)));

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 12 "..\..\Output\Html\Views\ConstructorDetail.cshtml"
 if (Model.DocComment != null){

            
            #line default
            #line hidden
WriteLiteral("    <!--Summary-->\r\n");




            
            #line 14 "..\..\Output\Html\Views\ConstructorDetail.cshtml"
WriteLiteral("    <p>");

            
            #line default
            #line hidden
            
            #line 14 "..\..\Output\Html\Views\ConstructorDetail.cshtml"
          CommentTag.RenderSummary(Model.DocComment.Summary); 
            
            #line default
            #line hidden
WriteLiteral("</p>\r\n");


            
            #line 15 "..\..\Output\Html\Views\ConstructorDetail.cshtml"


            
            #line default
            #line hidden
WriteLiteral("    <!--Remarks-->\r\n");



WriteLiteral("    <p>\r\n        <h3>Remarks</h3>\r\n");


            
            #line 19 "..\..\Output\Html\Views\ConstructorDetail.cshtml"
           CommentTag.RenderRemarks(Model.DocComment.Remarks); 

            
            #line default
            #line hidden
WriteLiteral("    </p>\r\n");


            
            #line 21 "..\..\Output\Html\Views\ConstructorDetail.cshtml"


            
            #line default
            #line hidden
WriteLiteral("    <!--See Also-->\r\n");



WriteLiteral("    <p>\r\n        <h3>See Also</h3>\r\n");


            
            #line 25 "..\..\Output\Html\Views\ConstructorDetail.cshtml"
           CommentTag.RenderSeeAlso(Model.DocComment.SeeAlso); 

            
            #line default
            #line hidden
WriteLiteral("    </p>\r\n");


            
            #line 27 "..\..\Output\Html\Views\ConstructorDetail.cshtml"
}
            
            #line default
            #line hidden

        }
    }
}
#pragma warning restore 1591
