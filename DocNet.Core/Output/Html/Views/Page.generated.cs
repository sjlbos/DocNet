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
    
    #line 2 "..\..\Output\Html\Views\Page.cshtml"
    using DocNet.Core.Output.Html.Views;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Output\Html\Views\Page.cshtml"
    using DocNet.Razor.Helpers;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public partial class Page : PageTemplate
    {
#line hidden
#line hidden
public System.Web.WebPages.HelperResult RenderBody()
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 8 "..\..\Output\Html\Views\Page.cshtml"
 
    Body.Execute();

#line default
#line hidden

});

}


        public override void Execute()
        {


WriteLiteral("\r\n");




WriteLiteral("\r\n\r\n");



WriteLiteral("\r\n\r\n<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n\r\n<html>\r\n");


            
            #line 15 "..\..\Output\Html\Views\Page.cshtml"
Write(PageHelper.RenderHead(CssFiles, ScriptFiles));

            
            #line default
            #line hidden
WriteLiteral(@"
<body>
    <div class=""container-fluid"">
        <div class=""page-head row"">
            <div class=""col-md-3"">
                <h1>Doc<span id=""head"">Net</span></h1></div>
            <div class=""col-md-9"">
            </div>
        </div>
        <div class=""row main"">
            <div class=""col-md-3 sidebar"">
                <h2>Sidebar</h2>
                <div id=""over"">
                    <ol>
                        ");


            
            #line 29 "..\..\Output\Html\Views\Page.cshtml"
                   Write(PageHelper.RenderSidebar());

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </ol>\r\n                </div>\r\n            </div>\r\n        " +
"    <div class=\"col-md-9 content\">\r\n                ");


            
            #line 34 "..\..\Output\Html\Views\Page.cshtml"
           Write(RenderBody());

            
            #line default
            #line hidden
WriteLiteral(@"
            </div>
        </div>
    </div>
    <footer class=""footer"">
        <div class=""container"">
            <p class=""text-muted"">DocNet was created by Stephen Bos, Scott Byrne, Chris Carr, and Keith Rollans.</p>
        </div>
    </footer>
</body>

</html>
");


        }
    }
}
#pragma warning restore 1591
