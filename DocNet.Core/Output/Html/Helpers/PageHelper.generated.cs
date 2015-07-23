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

namespace DocNet.Razor.Helpers
{
    using System;
    
    #line 2 "..\..\Output\Html\Helpers\PageHelper.cshtml"
    using System.Collections.Generic;
    
    #line default
    #line hidden
    using System.IO;
    
    #line 3 "..\..\Output\Html\Helpers\PageHelper.cshtml"
    using System.Linq;
    
    #line default
    #line hidden
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using System.Web.WebPages.Html;
    
    #line 4 "..\..\Output\Html\Helpers\PageHelper.cshtml"
    using DocNet.Core.Exceptions;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Output\Html\Helpers\PageHelper.cshtml"
    using DocNet.Core.Models.CSharp;
    
    #line default
    #line hidden
    
    #line 6 "..\..\Output\Html\Helpers\PageHelper.cshtml"
    using DocNet.Core.Output.Html;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public class PageHelper : System.Web.WebPages.HelperPage
    {

public static System.Web.WebPages.HelperResult RenderElementLink(CsElement element)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 9 "..\..\Output\Html\Helpers\PageHelper.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <a href=\"");



#line 10 "..\..\Output\Html\Helpers\PageHelper.cshtml"
WriteTo(@__razor_helper_writer, HtmlDocumentationGenerator.GetFileNameForCsElement(element));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\">");



#line 10 "..\..\Output\Html\Helpers\PageHelper.cshtml"
                                            WriteTo(@__razor_helper_writer, element.DisplayName);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</a>\r\n");



#line 11 "..\..\Output\Html\Helpers\PageHelper.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderHead(IEnumerable<string> includedCss, IEnumerable<string> includedScripts)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 14 "..\..\Output\Html\Helpers\PageHelper.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <head>\r\n        <meta charset=\"utf-8\">\r\n");



#line 17 "..\..\Output\Html\Helpers\PageHelper.cshtml"
         foreach (var cssFile in includedCss)
        {

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "        <link href=\"");



#line 19 "..\..\Output\Html\Helpers\PageHelper.cshtml"
WriteTo(@__razor_helper_writer, cssFile);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\" rel=\"stylesheet\"/>\r\n");



#line 20 "..\..\Output\Html\Helpers\PageHelper.cshtml"
        }

#line default
#line hidden



#line 21 "..\..\Output\Html\Helpers\PageHelper.cshtml"
         foreach (var jsFile in includedScripts)
        {

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "        <script src=\"");



#line 23 "..\..\Output\Html\Helpers\PageHelper.cshtml"
WriteTo(@__razor_helper_writer, jsFile);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\" type=\"text/javascript\"></script>\r\n");



#line 24 "..\..\Output\Html\Helpers\PageHelper.cshtml"
        }

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "        <script>hljs.initHighlightingOnLoad();\r\n            $(document).ready(fun" +
"ction () {\r\n                hljs.configure({ languages: [] });\r\n            });\r" +
"\n        </script>\r\n        <title>DocNet Output</title>\r\n    </head>\r\n");



#line 32 "..\..\Output\Html\Helpers\PageHelper.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderSidebar(GlobalNamespaceModel globalNamespace, CsElement currentViewModel)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 35 "..\..\Output\Html\Helpers\PageHelper.cshtml"
 
    var sortedChildren = globalNamespace.OrderBy(child => child.DisplayName);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <div id=\"over\">\r\n        <h2>Hierarchy</h2>\r\n        <ul>\r\n            ");



#line 40 "..\..\Output\Html\Helpers\PageHelper.cshtml"
WriteTo(@__razor_helper_writer, _RenderChildList(sortedChildren, true, currentViewModel));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\r\n        </ul>    \r\n    </div>\r\n");



#line 43 "..\..\Output\Html\Helpers\PageHelper.cshtml"

#line default
#line hidden

});

}


internal static System.Web.WebPages.HelperResult _RenderParentElement(IParentElement parent, CsElement currentViewModel)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 46 "..\..\Output\Html\Helpers\PageHelper.cshtml"
 
    var sortedChildren = parent.OrderBy(child => child.DisplayName);
    bool hasChildren = sortedChildren.Any();    
    bool isExpanded = (parent == currentViewModel) || parent.HasDescendant(currentViewModel as INestableElement);
    bool isCurrentView = (parent == currentViewModel);

    var parentElement = parent as CsElement;
    if (parentElement == null)
    {
        throw new DocumentationGenerationException("Parent element is not a CsElement.");
    }


#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <li>\r\n        <div class=\"expand link\" onclick=\"ToggleSidebarNode(this)\">\r\n");



#line 60 "..\..\Output\Html\Helpers\PageHelper.cshtml"
             if(hasChildren)
            {

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "                <span class=\"glyphicon ");



#line 62 "..\..\Output\Html\Helpers\PageHelper.cshtml"
         WriteTo(@__razor_helper_writer, isExpanded ? "glyphicon-menu-down" : "glyphicon-menu-right");

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\"></span>\r\n");



#line 63 "..\..\Output\Html\Helpers\PageHelper.cshtml"
            }
            else
            {

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "                <span class=\"expand\"></span>\r\n");



#line 67 "..\..\Output\Html\Helpers\PageHelper.cshtml"
            }            

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "        </div>\r\n        <a class=\"side-link");



#line 69 "..\..\Output\Html\Helpers\PageHelper.cshtml"
WriteTo(@__razor_helper_writer, isCurrentView ? " current" : "");

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\" href=\"");



#line 69 "..\..\Output\Html\Helpers\PageHelper.cshtml"
                                      WriteTo(@__razor_helper_writer, HtmlDocumentationGenerator.GetFileNameForCsElement(parentElement));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\">");



#line 69 "..\..\Output\Html\Helpers\PageHelper.cshtml"
                                                                                                          WriteTo(@__razor_helper_writer, parentElement.DisplayName);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</a>\r\n");



#line 70 "..\..\Output\Html\Helpers\PageHelper.cshtml"
         if(hasChildren)
        {
            
#line default
#line hidden


#line 72 "..\..\Output\Html\Helpers\PageHelper.cshtml"
WriteTo(@__razor_helper_writer, _RenderChildList(sortedChildren, isExpanded, currentViewModel));

#line default
#line hidden


#line 72 "..\..\Output\Html\Helpers\PageHelper.cshtml"
                                                                           
        }     

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    </li>\r\n");



#line 75 "..\..\Output\Html\Helpers\PageHelper.cshtml"

#line default
#line hidden

});

}


internal static System.Web.WebPages.HelperResult _RenderChildList(IEnumerable<INestableElement> children, bool isExpanded, CsElement currentViewModel)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 78 "..\..\Output\Html\Helpers\PageHelper.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <ul class=\"child-list collapse ");



#line 79 "..\..\Output\Html\Helpers\PageHelper.cshtml"
     WriteTo(@__razor_helper_writer, isExpanded ? "in" : "");

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\">\r\n\r\n");



#line 81 "..\..\Output\Html\Helpers\PageHelper.cshtml"
         foreach (var child in children)
        {
            bool isCurrentView = (child == currentViewModel);
            if (child is IParentElement)
            {
                
#line default
#line hidden


#line 86 "..\..\Output\Html\Helpers\PageHelper.cshtml"
WriteTo(@__razor_helper_writer, _RenderParentElement(child as IParentElement, currentViewModel));

#line default
#line hidden


#line 86 "..\..\Output\Html\Helpers\PageHelper.cshtml"
                                                                                
            }
            else
            {

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "                <li><span class=\"expand\"></span><a class=\"side-link");



#line 90 "..\..\Output\Html\Helpers\PageHelper.cshtml"
                                     WriteTo(@__razor_helper_writer, isCurrentView ? " current" : "");

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\" href=\"");



#line 90 "..\..\Output\Html\Helpers\PageHelper.cshtml"
                                                                              WriteTo(@__razor_helper_writer, HtmlDocumentationGenerator.GetFileNameForCsElement(child as CsElement));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\">");



#line 90 "..\..\Output\Html\Helpers\PageHelper.cshtml"
                                                                                                                                                       WriteTo(@__razor_helper_writer, child.DisplayName);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</a></li>\r\n");



#line 91 "..\..\Output\Html\Helpers\PageHelper.cshtml"
            }
        }

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    </ul>\r\n");



#line 94 "..\..\Output\Html\Helpers\PageHelper.cshtml"

#line default
#line hidden

});

}


        public PageHelper()
        {
        }
    }
}
#pragma warning restore 1591
