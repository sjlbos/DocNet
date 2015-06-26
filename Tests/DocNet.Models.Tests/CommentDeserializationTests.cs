using System;
using System.Collections.Generic;
using DocNet.Models.Comments;
using DocNet.Models.Comments.Xml;
using NUnit.Framework;

namespace DocNet.Models.Tests
{
    [TestFixture]
    public class CommentDeserializationTests
    {
        [Test]
        public void TestFromXmlThrowsNullArgumentExceptionForNullInput()
        {
            Assert.Throws<ArgumentNullException>(() => DocComment.FromXml<DocComment>(null));
        }

        public IEnumerable<TestCaseData> MissingTagData
        {
            get
            {
                yield return new TestCaseData("<summary>Dummy Summary</summary>", new DocComment { Summary = new SummaryTag { Items = new List<object> { "Dummy Summary" } } });
                yield return new TestCaseData("<remarks>Dummy Remarks</remarks>", new DocComment { Remarks = new RemarksTag { Items = new List<object> { "Dummy Remarks" } } });
                yield return new TestCaseData("<example>Dummy Example</example>", new DocComment { Example = new ExampleTag { Items = new List<object> { "Dummy Example" } } });
                yield return new TestCaseData(@"<permission cref=""DummyPermission""></permission>", new DocComment { Permission = new PermissionTag { ReferencedElementName = "Dummy Permission" } });
                yield return new TestCaseData(@"<seealso cref=""DummyClass""/>", new DocComment { SeeAlso = new SeeAlsoTag { ReferencedElementName = "Dummy Class" } });
            }
        }

        [Test, TestCaseSource("MissingTagData")]
        public void TestFromXmlHandlesMissingTags(string xmlComment, DocComment expectedComment)
        {
            // Act
            DocComment deserializedComment = DocComment.FromXml<DocComment>(xmlComment);

            // Assert
            Assert.That(expectedComment, Is.EqualTo(deserializedComment));
        }

        [Test]
        public void TestFromXmlDeserializesDocCommentCorrectly()
        {
            // Arrange
            const string xmlComment = @"
             <summary>
                 Dummy Summary
                 <para>Dummy Paragraph</para>
                 <code>Dummy Code</code>
                 <list type=""bullet"">
                     <item>
                         <description>Dummy Description</description>
                     </item>
                 </list>
                 <see cref=""DummyClass""/>
             </summary>
             <remarks>
                Dummy Remarks
             </remarks>
             <example>
                 Dummy Example
             </example>
             <permission cref=""DummyPermission""></permission>
             <seealso cref=""DummyClass""/>
            ";

            DocComment expectedComment = new DocComment
            {
                Summary = new SummaryTag
                {
                    Items = new List<object>
                    {
                        "Dummy Summary",
                        new ParagraphTag
                        {
                            Items = new List<object> { "Dummy Paragraph" }
                        },
                        new CodeTag
                        {
                            Text = new List<string> { "Dummy Code" }
                        },
                        new ListTag
                        {
                            ListType = ListType.Bullet,
                            Elements = new List<ListItem>
                            {
                                new ListItem
                                {
                                    Descriptions = new List<Description>
                                    {
                                        new Description { Text = new List<string>{ "Dummy Description" } } 
                                    }
                                } 
                            }
                        },
                        new SeeTag { ReferencedElementName = "DummyClass" }
                    }
                },
                Remarks = new RemarksTag { Items = new List<object> { "Dummy Remarks" } },
                Example = new ExampleTag { Items = new List<object> { "Dummy Example" } },
                Permission = new PermissionTag { ReferencedElementName = "Dummy Permission" },
                SeeAlso = new SeeAlsoTag { ReferencedElementName = "Dummy Class" } 
            };

            // Act
            DocComment deserializedComment = DocComment.FromXml<DocComment>(xmlComment);

            // Assert
            Assert.That(expectedComment, Is.EqualTo(deserializedComment));
        }

        [Test]
        public void TestFromXmlDeserializesClassDocCommentCorrectly()
        {
            // Arrange
            const string xmlComment = @"
             <summary>Dummy Summary</summary>
             <remarks>Dummy Remarks</remarks>
             <example>Dummy Example</example>
             <permission cref=""DummyPermission""></permission>
             <seealso cref=""DummyClass""/>
             <typeparam name=""T"">TypeParmT</typeparam>
             <typeparam name=""F"">TypeParmF</typeparam>
             <exception cref=""ExceptionA"">Exception A Description</exception>
             <exception cref=""ExceptionB"">Exception B Description</exception>
            ";

            InterfaceDocComment expectedComment = new InterfaceDocComment
            {
                Summary = new SummaryTag { Items = new List<object> { "Dummy Summary" } },
                Remarks = new RemarksTag { Items = new List<object> { "Dummy Remarks" } },
                Example = new ExampleTag { Items = new List<object> { "Dummy Example" } },
                Permission = new PermissionTag { ReferencedElementName = "Dummy Permission" },
                SeeAlso = new SeeAlsoTag { ReferencedElementName = "Dummy Class" },
                TypeParameters = new List<TypeParameterTag>
                {
                    new TypeParameterTag { Name = "T", Items = new List<object> { "TypeParamT" }},
                    new TypeParameterTag { Name = "F", Items = new List<object> { "TypeParamF" }}
                },
                Exceptions = new List<ExceptionTag>
                {
                    new ExceptionTag { ExceptionType = "ExceptionA", Items = new List<object> { "Exception A Description" }},
                    new ExceptionTag { ExceptionType = "ExceptionB", Items = new List<object> { "Exception B Description" }}
                }
            };

            // Act
            InterfaceDocComment deserializedComment = DocComment.FromXml<InterfaceDocComment>(xmlComment);

            // Assert
            Assert.That(expectedComment, Is.EqualTo(deserializedComment));
        }

        [Test]
        public void TestFromXmlDeserializesMethodDocCommentCorrectly()
        {
            // Arrange
            const string xmlComment = @"
             <summary>Dummy Summary</summary>
             <remarks>Dummy Remarks</remarks>
             <example>Dummy Example</example>
             <permission cref=""DummyPermission""></permission>
             <seealso cref=""DummyClass""/>
             <typeparam name=""T"">TypeParamT</typeparam>
             <typeparam name=""F"">TypeParamF</typeparam>
             <exception cref=""ExceptionA"">Exception A Description</exception>
             <exception cref=""ExceptionB"">Exception B Description</exception>
             <param name=""ParameterA"">Parameter A Description</param>
             <param name=""ParameterB"">Parameter B Description</param>
             <returns>Dummy Returns</returns>
            ";

            MethodDocComment expectedComment = new MethodDocComment
            {
                Summary = new SummaryTag { Items = new List<object> { "Dummy Summary" } },
                Remarks = new RemarksTag { Items = new List<object> { "Dummy Remarks" } },
                Example = new ExampleTag { Items = new List<object> { "Dummy Example" } },
                Permission = new PermissionTag { ReferencedElementName = "Dummy Permission" },
                SeeAlso = new SeeAlsoTag { ReferencedElementName = "Dummy Class" },
                TypeParameters = new List<TypeParameterTag>
                {
                    new TypeParameterTag { Name = "T", Items = new List<object> { "TypeParamT" }},
                    new TypeParameterTag { Name = "F", Items = new List<object> { "TypeParamF" }}
                },
                Exceptions = new List<ExceptionTag>
                {
                    new ExceptionTag { ExceptionType = "ExceptionA", Items = new List<object> { "Exception A Description" }},
                    new ExceptionTag { ExceptionType = "ExceptionB", Items = new List<object> { "Exception B Description" }}
                },
                Parameters = new List<ParameterTag>
                {
                    new ParameterTag{ Name = "ParameterA", Items = new List<object>{ "Parameter A Description" }},
                    new ParameterTag{ Name = "ParameterB", Items = new List<object>{ "Parameter B Description" }}
                },
                Returns = new ReturnsTag { Items = new List<object> { "Dummy Returns" } }
            };

            // Act
            MethodDocComment deserializedComment = DocComment.FromXml<MethodDocComment>(xmlComment);

            // Assert
            Assert.That(expectedComment, Is.EqualTo(deserializedComment));
        }

        [Test]
        public void TestFromXmlDeserializesPropertyDocCommentCorrectly()
        {
            // Arrange
            const string xmlComment = @"
             <summary>Dummy Summary</summary>
             <remarks>Dummy Remarks</remarks>
             <example>Dummy Example</example>
             <permission cref=""DummyPermission""></permission>
             <seealso cref=""DummyClass""/>
             <value>Dummy Value</value>
            ";

            PropertyDocComment expectedComment = new PropertyDocComment
            {
                Summary = new SummaryTag { Items = new List<object> { "Dummy Summary" } },
                Remarks = new RemarksTag { Items = new List<object> { "Dummy Remarks" } },
                Example = new ExampleTag { Items = new List<object> { "Dummy Example" } },
                Permission = new PermissionTag { ReferencedElementName = "Dummy Permission" },
                SeeAlso = new SeeAlsoTag { ReferencedElementName = "Dummy Class" },
                Value = new ValueTag { Items = new List<object> { "Dummy Value" } }
            };

            // Act
            PropertyDocComment deserializedComment = DocComment.FromXml<PropertyDocComment>(xmlComment);

            // Assert
            Assert.That(expectedComment, Is.EqualTo(deserializedComment));
        }
    }
}
