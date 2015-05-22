using System.Collections.Generic;
using DocNet.Models.Comments;
using DocNet.Models.Comments.Xml;
using NUnit.Framework;

namespace DocNet.Models.Tests
{
    [TestFixture]
    public class SerializationTests
    {
        [Test]
        public void TestDocCommentDeserializesCorrectly()
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
            Assert.AreEqual(expectedComment, deserializedComment);
        }
    }
}
