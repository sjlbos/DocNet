﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DocNet.Razor.Helpers
{
    
    #line 2 "..\..\Output\Html\Helpers\CommentTag.cshtml"
    using System;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Output\Html\Helpers\CommentTag.cshtml"
    using System.Collections.Generic;
    
    #line default
    #line hidden
    using System.IO;
    
    #line 4 "..\..\Output\Html\Helpers\CommentTag.cshtml"
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
    
    #line 5 "..\..\Output\Html\Helpers\CommentTag.cshtml"
    using DocNet.Core.Models.Comments.Xml;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public class CommentTag : System.Web.WebPages.HelperPage
    {
        
        #line 8 "..\..\Output\Html\Helpers\CommentTag.cshtml"

    private static string CombineText(IEnumerable<string> text)
    {
        return String.Join("\n", text);
    }

        #line default
        #line hidden

public static System.Web.WebPages.HelperResult RenderSummary(SummaryTag summaryTag)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 16 "..\..\Output\Html\Helpers\CommentTag.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <div class=\"summary-comment\">\r\n        <p>\r\n            ");



#line 19 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderTopLevelContainer(summaryTag));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\r\n        </p>\r\n    </div>\r\n");



#line 22 "..\..\Output\Html\Helpers\CommentTag.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderTableSummary(SummaryTag summaryTag)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 25 "..\..\Output\Html\Helpers\CommentTag.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <div class=\"expand-table-cell summary-comment\">\r\n        ");



#line 27 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderTopLevelContainer(summaryTag));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\r\n    </div>\r\n");



#line 29 "..\..\Output\Html\Helpers\CommentTag.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderRemarks(RemarksTag remarksTag)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 32 "..\..\Output\Html\Helpers\CommentTag.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <div class=\"remarks-comment\">\r\n        <p>\r\n            ");



#line 35 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderTopLevelContainer(remarksTag));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\r\n        </p> \r\n    </div>\r\n");



#line 38 "..\..\Output\Html\Helpers\CommentTag.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderExample(ExampleTag exampleTag)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 41 "..\..\Output\Html\Helpers\CommentTag.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <div class=\"example-comment\">\r\n        <p>\r\n            ");



#line 44 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderTopLevelContainer(exampleTag));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\r\n        </p>\r\n    </div>\r\n");



#line 47 "..\..\Output\Html\Helpers\CommentTag.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderReturns(ReturnsTag returnsTag)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 50 "..\..\Output\Html\Helpers\CommentTag.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <div class=\"returns-comment\">\r\n        <p>\r\n            ");



#line 53 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderTopLevelContainer(returnsTag));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\r\n        </p>\r\n    </div>\r\n");



#line 56 "..\..\Output\Html\Helpers\CommentTag.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderValue(ValueTag valueTag)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 59 "..\..\Output\Html\Helpers\CommentTag.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <div class=\"value-comment\">\r\n        <p>\r\n            ");



#line 62 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderTopLevelContainer(valueTag));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\r\n        </p>\r\n    </div>\r\n");



#line 65 "..\..\Output\Html\Helpers\CommentTag.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderParam(ParameterTag paramTag)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 68 "..\..\Output\Html\Helpers\CommentTag.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <div class=\"parameter-comment\">\r\n        <p>\r\n            ");



#line 71 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderTopLevelContainer(paramTag));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\r\n        </p>\r\n    </div>\r\n");



#line 74 "..\..\Output\Html\Helpers\CommentTag.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderTypeParam(TypeParameterTag typeParamTag)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 77 "..\..\Output\Html\Helpers\CommentTag.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <div class=\"type-parameter-comment\">\r\n        <p>\r\n            ");



#line 80 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderTopLevelContainer(typeParamTag));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\r\n        </p>\r\n    </div>\r\n");



#line 83 "..\..\Output\Html\Helpers\CommentTag.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderException(ExceptionTag exceptionTag)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 86 "..\..\Output\Html\Helpers\CommentTag.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <div class=\"exception-comment\">\r\n        <p>\r\n            ");



#line 89 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderTopLevelContainer(exceptionTag));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "\r\n        </p>\r\n    </div>\r\n");



#line 92 "..\..\Output\Html\Helpers\CommentTag.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderSeeAlso(SeeAlsoTag seeAlsoTag)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 95 "..\..\Output\Html\Helpers\CommentTag.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <div class=\"see-also-comment\">\r\n        <p>\r\n            ");



#line 98 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, seeAlsoTag.ReferencedElementName);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "<br/>\r\n");



#line 99 "..\..\Output\Html\Helpers\CommentTag.cshtml"
             if (seeAlsoTag.Text!=null&seeAlsoTag.Text.Any())
            {

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "                <ul class=\"bulleted\">\r\n");



#line 102 "..\..\Output\Html\Helpers\CommentTag.cshtml"
                     foreach (string s in seeAlsoTag.Text)
                    {

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "                        <li class=\"bulleted\">");



#line 104 "..\..\Output\Html\Helpers\CommentTag.cshtml"
              WriteTo(@__razor_helper_writer, s);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</li>\r\n");



#line 105 "..\..\Output\Html\Helpers\CommentTag.cshtml"
                    }

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "                </ul>\r\n");



#line 107 "..\..\Output\Html\Helpers\CommentTag.cshtml"
            }

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "        </p>\r\n    </div>\r\n");



#line 110 "..\..\Output\Html\Helpers\CommentTag.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderPermission(PermissionTag permissionTag)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 113 "..\..\Output\Html\Helpers\CommentTag.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <p>Fix me, I don\'t know what I am!</p>\r\n");



#line 115 "..\..\Output\Html\Helpers\CommentTag.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderTopLevelContainer(TopLevelContainer container)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 118 "..\..\Output\Html\Helpers\CommentTag.cshtml"
 
    if (container.Items == null) { return; }
    foreach (var element in container.Items)
     {
        
#line default
#line hidden


#line 122 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderContainerElement(element));

#line default
#line hidden


#line 122 "..\..\Output\Html\Helpers\CommentTag.cshtml"
                                        
     }

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderContainer(Container container)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 127 "..\..\Output\Html\Helpers\CommentTag.cshtml"
   
    if (container.Items == null) { return; }
    foreach (var element in container.Items)
    {
        
#line default
#line hidden


#line 131 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderContainerElement(element));

#line default
#line hidden


#line 131 "..\..\Output\Html\Helpers\CommentTag.cshtml"
                                        
    }

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderContainerElement(object element)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 136 "..\..\Output\Html\Helpers\CommentTag.cshtml"
 
    if(element is string)
    {
        
#line default
#line hidden


#line 139 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, element);

#line default
#line hidden


#line 139 "..\..\Output\Html\Helpers\CommentTag.cshtml"
                
    }
    if (element is ParagraphTag)
    {
        
#line default
#line hidden


#line 143 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderPara(element as ParagraphTag));

#line default
#line hidden


#line 143 "..\..\Output\Html\Helpers\CommentTag.cshtml"
                                            
        return;
    }
    if (element is CTag)
    {
        
#line default
#line hidden


#line 148 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderC(element as CTag));

#line default
#line hidden


#line 148 "..\..\Output\Html\Helpers\CommentTag.cshtml"
                                 
        return;
    }
    if (element is CodeTag)
    {
        
#line default
#line hidden


#line 153 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderCode(element as CodeTag));

#line default
#line hidden


#line 153 "..\..\Output\Html\Helpers\CommentTag.cshtml"
                                       
        return;
    }
    if (element is ListTag)
    {
        
#line default
#line hidden


#line 158 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderList(element as ListTag));

#line default
#line hidden


#line 158 "..\..\Output\Html\Helpers\CommentTag.cshtml"
                                       ;
        return;
    }
    if (element is ParameterReferenceTag)
    {
        
#line default
#line hidden


#line 163 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderParamRef(element as ParameterReferenceTag));

#line default
#line hidden


#line 163 "..\..\Output\Html\Helpers\CommentTag.cshtml"
                                                         ;
        return;
    }
    if (element is SeeTag)
    {
        
#line default
#line hidden


#line 168 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderSee(element as SeeTag));

#line default
#line hidden


#line 168 "..\..\Output\Html\Helpers\CommentTag.cshtml"
                                     ;
        return;
    }
    if (element is TypeParameterReferenceTag)
    {
        
#line default
#line hidden


#line 173 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderTypeParamRef(element as TypeParameterReferenceTag));

#line default
#line hidden


#line 173 "..\..\Output\Html\Helpers\CommentTag.cshtml"
                                                                 ;
        return;
    }

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderC(CTag cTag)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 179 "..\..\Output\Html\Helpers\CommentTag.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <span class=\"inline-code\">");



#line 180 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, CombineText(cTag.Text));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</span>\r\n");



#line 181 "..\..\Output\Html\Helpers\CommentTag.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderCode(CodeTag codeTag)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 184 "..\..\Output\Html\Helpers\CommentTag.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <div class=\"code-block\">\r\n        <pre><code>");



#line 186 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, CombineText(codeTag.Text));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</code></pre> \r\n    </div>  \r\n");



#line 188 "..\..\Output\Html\Helpers\CommentTag.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderList(ListTag listTag)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 191 "..\..\Output\Html\Helpers\CommentTag.cshtml"
 
    if (listTag.Elements != null&&listTag.Elements.Any()){
        if (listTag.ListType.Equals(ListType.Bullet))
        {

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "            <ul>\r\n");



#line 196 "..\..\Output\Html\Helpers\CommentTag.cshtml"
                 foreach (var element in listTag.Elements)
                {

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "                    <li>");



#line 198 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderContainerElement(element));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</li>\r\n");



#line 199 "..\..\Output\Html\Helpers\CommentTag.cshtml"
                }

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "            </ul>\r\n");



#line 201 "..\..\Output\Html\Helpers\CommentTag.cshtml"
        }else if (listTag.ListType.Equals(ListType.Number))
        {

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "            <ol>\r\n");



#line 204 "..\..\Output\Html\Helpers\CommentTag.cshtml"
                 foreach (var element in listTag.Elements)
                {

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "                    <li>");



#line 206 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderContainerElement(element));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</li>\r\n");



#line 207 "..\..\Output\Html\Helpers\CommentTag.cshtml"
                }

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "            </ol>\r\n");



#line 209 "..\..\Output\Html\Helpers\CommentTag.cshtml"
        }else{

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "            <table>\r\n");



#line 211 "..\..\Output\Html\Helpers\CommentTag.cshtml"
                 foreach (var element in listTag.Elements)
                {

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "                    <tr>\r\n                        <td>");



#line 214 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderContainerElement(element));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</td>\r\n                    </tr>\r\n");



#line 216 "..\..\Output\Html\Helpers\CommentTag.cshtml"
                }

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "            </table>\r\n");



#line 218 "..\..\Output\Html\Helpers\CommentTag.cshtml"
        }
    }else{

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "        <!--Add a debug message maybe?-->\r\n");



#line 221 "..\..\Output\Html\Helpers\CommentTag.cshtml"
    }

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderParamRef(ParameterReferenceTag paramRefTag)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 225 "..\..\Output\Html\Helpers\CommentTag.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <b>");



#line 226 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, paramRefTag.Name);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</b>\r\n");



#line 227 "..\..\Output\Html\Helpers\CommentTag.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderSee(SeeTag seeTag)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 230 "..\..\Output\Html\Helpers\CommentTag.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <b>");



#line 231 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, seeTag.ReferencedElementName);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</b>   \r\n");



#line 232 "..\..\Output\Html\Helpers\CommentTag.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderTypeParamRef(TypeParameterReferenceTag typeParamRefTag)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 235 "..\..\Output\Html\Helpers\CommentTag.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <b>");



#line 236 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, typeParamRefTag.Name);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</b>\r\n");



#line 237 "..\..\Output\Html\Helpers\CommentTag.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult  RenderPara(ParagraphTag paragraphTag)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 240 "..\..\Output\Html\Helpers\CommentTag.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <p>");



#line 241 "..\..\Output\Html\Helpers\CommentTag.cshtml"
WriteTo(@__razor_helper_writer, RenderContainer(paragraphTag));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</p>\r\n");



#line 242 "..\..\Output\Html\Helpers\CommentTag.cshtml"

#line default
#line hidden

});

}


        public CommentTag()
        {
        }
    }
}
#pragma warning restore 1591
