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

namespace DocNet.Razor.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using System.Web.WebPages.Html;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public class PageSection : System.Web.WebPages.HelperPage
    {

public static System.Web.WebPages.HelperResult RenderDeclarationBlock(string declaration)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 5 "..\..\Output\Html\Helpers\PageSection.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <div class=\"code-block\">\r\n        <pre><code class=\"cs\">");



#line 7 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, declaration);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</code></pre>\r\n    </div>\r\n");



#line 9 "..\..\Output\Html\Helpers\PageSection.cshtml"


#line default
#line hidden

});

}


        public PageSection()
        {
        }
    }
}
#pragma warning restore 1591
