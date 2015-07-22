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
    
    #line 2 "..\..\Output\Html\Helpers\PageSection.cshtml"
    using System;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Output\Html\Helpers\PageSection.cshtml"
    using System.Collections.Generic;
    
    #line default
    #line hidden
    using System.IO;
    
    #line 4 "..\..\Output\Html\Helpers\PageSection.cshtml"
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
    
    #line 5 "..\..\Output\Html\Helpers\PageSection.cshtml"
    using DocNet.Core.Models.Comments;
    
    #line default
    #line hidden
    
    #line 6 "..\..\Output\Html\Helpers\PageSection.cshtml"
    using DocNet.Core.Models.Comments.Xml;
    
    #line default
    #line hidden
    
    #line 7 "..\..\Output\Html\Helpers\PageSection.cshtml"
    using DocNet.Core.Models.CSharp;
    
    #line default
    #line hidden
    
    #line 8 "..\..\Output\Html\Helpers\PageSection.cshtml"
    using DocNet.Razor.Helpers;
    
    #line default
    #line hidden
    
    #line 9 "..\..\Output\Html\Helpers\PageSection.cshtml"
    using Microsoft.CodeAnalysis;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    public class PageSection : System.Web.WebPages.HelperPage
    {

public static System.Web.WebPages.HelperResult RenderDeclarationBlock(string declaration)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 12 "..\..\Output\Html\Helpers\PageSection.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <div class=\"code-block\">\r\n        <pre><code class=\"cs\">");



#line 14 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, declaration);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</code></pre>\r\n    </div>\r\n");



#line 16 "..\..\Output\Html\Helpers\PageSection.cshtml"

#line default
#line hidden

});

}


internal static System.Web.WebPages.HelperResult _RenderSectionTitle(string title)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 19 "..\..\Output\Html\Helpers\PageSection.cshtml"
 

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <p><h3>");



#line 20 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, title);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</h3></p>\r\n");



#line 21 "..\..\Output\Html\Helpers\PageSection.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderSummary(DocComment docComment)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 24 "..\..\Output\Html\Helpers\PageSection.cshtml"
 
    if (docComment == null || docComment.Summary == null) { return; }
    
#line default
#line hidden


#line 26 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, _RenderSectionTitle("Summary"));

#line default
#line hidden


#line 26 "..\..\Output\Html\Helpers\PageSection.cshtml"
                                   

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <p>");



#line 27 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, CommentTag.RenderSummary(docComment.Summary));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</p> \r\n");



#line 28 "..\..\Output\Html\Helpers\PageSection.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderRemarks(DocComment docComment)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 31 "..\..\Output\Html\Helpers\PageSection.cshtml"
 
    if (docComment == null || docComment.Remarks == null) { return; }
    
#line default
#line hidden


#line 33 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, _RenderSectionTitle("Remarks"));

#line default
#line hidden


#line 33 "..\..\Output\Html\Helpers\PageSection.cshtml"
                                   

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <p>");



#line 34 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, CommentTag.RenderRemarks(docComment.Remarks));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</p>\r\n");



#line 35 "..\..\Output\Html\Helpers\PageSection.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderExample(DocComment docComment)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 38 "..\..\Output\Html\Helpers\PageSection.cshtml"
 
    if (docComment == null || docComment.Example == null) { return; }
    
#line default
#line hidden


#line 40 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, _RenderSectionTitle("Example"));

#line default
#line hidden


#line 40 "..\..\Output\Html\Helpers\PageSection.cshtml"
                                   

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <p>");



#line 41 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, CommentTag.RenderExample(docComment.Example));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</p>\r\n");



#line 42 "..\..\Output\Html\Helpers\PageSection.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderSeeAlso(DocComment docComment)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 45 "..\..\Output\Html\Helpers\PageSection.cshtml"
 
    if (docComment == null || docComment.SeeAlso == null) { return; }
    
#line default
#line hidden


#line 47 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, _RenderSectionTitle("See Also"));

#line default
#line hidden


#line 47 "..\..\Output\Html\Helpers\PageSection.cshtml"
                                    

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <p>");



#line 48 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, CommentTag.RenderSeeAlso(docComment.SeeAlso));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</p>\r\n");



#line 49 "..\..\Output\Html\Helpers\PageSection.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderTypeParamTable(InterfaceBase interfaceModel)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 52 "..\..\Output\Html\Helpers\PageSection.cshtml"
 
    if (interfaceModel.TypeParameters == null || !interfaceModel.TypeParameters.Any()) { return; }
    
#line default
#line hidden


#line 54 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, _RenderTypeParamTable(interfaceModel.TypeParameters, interfaceModel.DocComment == null ? null : interfaceModel.DocComment.TypeParameters));

#line default
#line hidden


#line 54 "..\..\Output\Html\Helpers\PageSection.cshtml"
                                                                                                                                              

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderTypeParamTable(MethodModel methodModel)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 58 "..\..\Output\Html\Helpers\PageSection.cshtml"
 
    if (methodModel.TypeParameters == null || !methodModel.TypeParameters.Any()) { return; }
    
#line default
#line hidden


#line 60 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, _RenderTypeParamTable(methodModel.TypeParameters, methodModel.DocComment == null ? null : methodModel.DocComment.TypeParameters));

#line default
#line hidden


#line 60 "..\..\Output\Html\Helpers\PageSection.cshtml"
                                                                                                                                     

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderTypeParamTable(DelegateModel delegateModel)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 64 "..\..\Output\Html\Helpers\PageSection.cshtml"
 
    if (delegateModel.TypeParameters == null || !delegateModel.TypeParameters.Any()) { return; }
    
#line default
#line hidden


#line 66 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, _RenderTypeParamTable(delegateModel.TypeParameters, delegateModel.DocComment == null ? null : delegateModel.DocComment.TypeParameters));

#line default
#line hidden


#line 66 "..\..\Output\Html\Helpers\PageSection.cshtml"
                                                                                                                                           

#line default
#line hidden

});

}


internal static System.Web.WebPages.HelperResult _RenderTypeParamTable(IList<TypeParameterModel> typeParams, IList<TypeParameterTag> typeParamComments)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 70 "..\..\Output\Html\Helpers\PageSection.cshtml"
 
    var paramToCommentMap = new Dictionary<string, TypeParameterTag>();
    if (typeParamComments != null)
    {
        paramToCommentMap = typeParamComments.ToDictionary(tag => tag.Name, tag => tag);
    }
    
    
#line default
#line hidden


#line 77 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, _RenderSectionTitle("Type Parameters"));

#line default
#line hidden


#line 77 "..\..\Output\Html\Helpers\PageSection.cshtml"
                                           


#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <p>\r\n        <table class=\"table table-bordered table-hover\">\r\n");



#line 81 "..\..\Output\Html\Helpers\PageSection.cshtml"
             foreach (var typeParam in typeParams)
            {

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "                <tr>\r\n                    <td>");



#line 84 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, typeParam.Name + (!String.IsNullOrWhiteSpace(typeParam.Constraint) ? " : " + typeParam.Constraint : String.Empty));

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, " </td>\r\n                    <td>\r\n");



#line 86 "..\..\Output\Html\Helpers\PageSection.cshtml"
                         if (paramToCommentMap.ContainsKey(typeParam.Name))
                        {
                            
#line default
#line hidden


#line 88 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, CommentTag.RenderTypeParam(paramToCommentMap[typeParam.Name]));

#line default
#line hidden


#line 88 "..\..\Output\Html\Helpers\PageSection.cshtml"
                                                                                          
                        }

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "                </td>\r\n            </tr>\r\n");



#line 92 "..\..\Output\Html\Helpers\PageSection.cshtml"
            }

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "        </table>\r\n    </p>\r\n");



#line 95 "..\..\Output\Html\Helpers\PageSection.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderParameterTable(MethodModel methodModel)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 98 "..\..\Output\Html\Helpers\PageSection.cshtml"
 
    if (methodModel.Parameters == null || !methodModel.Parameters.Any()) { return; }
    
#line default
#line hidden


#line 100 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, _RenderParameterTable(methodModel.Parameters, methodModel.DocComment == null ? null : methodModel.DocComment.Parameters));

#line default
#line hidden


#line 100 "..\..\Output\Html\Helpers\PageSection.cshtml"
                                                                                                                             

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderParameterTable(ConstructorModel constructorModel)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 104 "..\..\Output\Html\Helpers\PageSection.cshtml"
 
    if (constructorModel.Parameters == null || !constructorModel.Parameters.Any()) { return; }
    
#line default
#line hidden


#line 106 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, _RenderParameterTable(constructorModel.Parameters, constructorModel.DocComment == null ? null : constructorModel.DocComment.Parameters));

#line default
#line hidden


#line 106 "..\..\Output\Html\Helpers\PageSection.cshtml"
                                                                                                                                            

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderParameterTable(DelegateModel delegateModel)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 110 "..\..\Output\Html\Helpers\PageSection.cshtml"
 
    if (delegateModel.Parameters == null || !delegateModel.Parameters.Any()) { return; }
    
#line default
#line hidden


#line 112 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, _RenderParameterTable(delegateModel.Parameters, delegateModel.DocComment == null ? null : delegateModel.DocComment.Parameters));

#line default
#line hidden


#line 112 "..\..\Output\Html\Helpers\PageSection.cshtml"
                                                                                                                                   

#line default
#line hidden

});

}


internal static System.Web.WebPages.HelperResult _RenderParameterTable(IList<ParameterModel> parameters, IList<ParameterTag> parameterComments)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 116 "..\..\Output\Html\Helpers\PageSection.cshtml"
 
    var paramToCommentMap = new Dictionary<string, ParameterTag>();
    if (parameterComments != null)
    {
        paramToCommentMap = parameterComments.ToDictionary(tag => tag.Name, tag => tag);
    }

    
#line default
#line hidden


#line 123 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, _RenderSectionTitle("Parameters"));

#line default
#line hidden


#line 123 "..\..\Output\Html\Helpers\PageSection.cshtml"
                                      

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <p>\r\n        <table class=\"table table-bordered table-hover\">\r\n");



#line 126 "..\..\Output\Html\Helpers\PageSection.cshtml"
             foreach (var param in parameters)
            {

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "                <tr>\r\n                    <td>");



#line 129 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, param.TypeName);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</td>\r\n                    <td>");



#line 130 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, param.Name);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</td>\r\n                    <td>\r\n");



#line 132 "..\..\Output\Html\Helpers\PageSection.cshtml"
                         if (paramToCommentMap.ContainsKey(param.Name))
                        {
                            
#line default
#line hidden


#line 134 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, CommentTag.RenderParam(paramToCommentMap[param.Name]));

#line default
#line hidden


#line 134 "..\..\Output\Html\Helpers\PageSection.cshtml"
                                                                                  
                        }

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "                    </td>\r\n                </tr>\r\n");



#line 138 "..\..\Output\Html\Helpers\PageSection.cshtml"
            }

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "        </table>\r\n    </p>\r\n");



#line 141 "..\..\Output\Html\Helpers\PageSection.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderConstructorTable(ClassAndStructBase classAndStructBase)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 144 "..\..\Output\Html\Helpers\PageSection.cshtml"
 
    if (classAndStructBase.Constructors == null || !classAndStructBase.Constructors.Any()) { return; }
    
    
#line default
#line hidden


#line 147 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, _RenderSectionTitle("Constructors"));

#line default
#line hidden


#line 147 "..\..\Output\Html\Helpers\PageSection.cshtml"
                                        


#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <p>\r\n        <table class=\"table table-bordered table-hover\">\r\n            <t" +
"r>\r\n                <td><b>Constructor Name</b></td>\r\n                <td><b>Des" +
"cription</b></td>\r\n            </tr>\r\n");



#line 155 "..\..\Output\Html\Helpers\PageSection.cshtml"
             foreach (var constructor in classAndStructBase.Constructors)
            {

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "                <tr>\r\n                    <td>");



#line 158 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, constructor.DisplayName);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</td>\r\n                    <td>\r\n");



#line 160 "..\..\Output\Html\Helpers\PageSection.cshtml"
                         if (constructor.DocComment != null && constructor.DocComment.Summary != null)
                        {
                            
#line default
#line hidden


#line 162 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, CommentTag.RenderSummary(constructor.DocComment.Summary));

#line default
#line hidden


#line 162 "..\..\Output\Html\Helpers\PageSection.cshtml"
                                                                                     
                        }

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "                </td>\r\n            </tr>\r\n");



#line 166 "..\..\Output\Html\Helpers\PageSection.cshtml"
            }

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "        </table>\r\n    </p>\r\n");



#line 169 "..\..\Output\Html\Helpers\PageSection.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderPropertyTable(InterfaceBase interfaceModel)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 172 "..\..\Output\Html\Helpers\PageSection.cshtml"
 
    if (interfaceModel.Properties == null || !interfaceModel.Properties.Any()) { return; }
    
    
#line default
#line hidden


#line 175 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, _RenderSectionTitle("Properties"));

#line default
#line hidden


#line 175 "..\..\Output\Html\Helpers\PageSection.cshtml"
                                      


#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <p>\r\n        <table class=\"table table-bordered table-hover\">\r\n            <t" +
"r>\r\n                <td><b>Property Name</b></td>\r\n                <td><b>Descri" +
"ption</b></td>\r\n            </tr>\r\n");



#line 183 "..\..\Output\Html\Helpers\PageSection.cshtml"
             foreach (var property in interfaceModel.Properties)
            {

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "                <tr>\r\n                    <td>");



#line 186 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, property.DisplayName);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</td>\r\n                    <td>\r\n");



#line 188 "..\..\Output\Html\Helpers\PageSection.cshtml"
                         if (property.DocComment != null && property.DocComment.Summary != null)
                        {
                            
#line default
#line hidden


#line 190 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, CommentTag.RenderSummary(property.DocComment.Summary));

#line default
#line hidden


#line 190 "..\..\Output\Html\Helpers\PageSection.cshtml"
                                                                                  
                        }

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "                </td>\r\n            </tr>\r\n");



#line 194 "..\..\Output\Html\Helpers\PageSection.cshtml"
            }

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "        </table>\r\n    </p>\r\n");



#line 197 "..\..\Output\Html\Helpers\PageSection.cshtml"

#line default
#line hidden

});

}


public static System.Web.WebPages.HelperResult RenderMethodTable(InterfaceBase interfaceModel)
{
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {



#line 200 "..\..\Output\Html\Helpers\PageSection.cshtml"
 
    if (interfaceModel.Methods == null || !interfaceModel.Methods.Any()) { return; }
    
    
#line default
#line hidden


#line 203 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, _RenderSectionTitle("Methods"));

#line default
#line hidden


#line 203 "..\..\Output\Html\Helpers\PageSection.cshtml"
                                   

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "    <p>\r\n        <table class=\"table table-bordered table-hover\">\r\n            <t" +
"r>\r\n                <td><b>Method Name</b></td>\r\n                <td><b>Descript" +
"ion</b></td>\r\n            </tr>\r\n");



#line 210 "..\..\Output\Html\Helpers\PageSection.cshtml"
             foreach (var method in interfaceModel.Methods)
            {

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "                <tr>\r\n                    <td>");



#line 213 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, method.DisplayName);

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "</td>\r\n                    <td>\r\n");



#line 215 "..\..\Output\Html\Helpers\PageSection.cshtml"
                         if (method.DocComment != null && method.DocComment.Summary != null)
                        {
                            
#line default
#line hidden


#line 217 "..\..\Output\Html\Helpers\PageSection.cshtml"
WriteTo(@__razor_helper_writer, CommentTag.RenderSummary(method.DocComment.Summary));

#line default
#line hidden


#line 217 "..\..\Output\Html\Helpers\PageSection.cshtml"
                                                                                
                        }

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "                </td>\r\n            </tr>\r\n");



#line 221 "..\..\Output\Html\Helpers\PageSection.cshtml"
            }

#line default
#line hidden

WriteLiteralTo(@__razor_helper_writer, "        </table>\r\n    </p>\r\n");



#line 224 "..\..\Output\Html\Helpers\PageSection.cshtml"

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
