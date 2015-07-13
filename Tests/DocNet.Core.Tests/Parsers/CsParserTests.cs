using System.IO;
using System.Linq;
using DocNet.Core.Parsers.CSharp;
using DocNet.Core.Models.CSharp;
using NUnit.Framework;

namespace DocNet.Core.Tests.Parsers
{
    [TestFixture]
    public class CsParserTests
    {
        private const string TestFileDirectory = @"..\..\..\TestData";
        private static readonly string SimpleMultiNamespaceFile = Path.Combine(TestFileDirectory, "SimpleMultiNamespaceFile.cs");

        private static readonly string SimpleNestedElementFile = Path.Combine(TestFileDirectory, "SimpleNestedElementFile.cs");

        [Test]
        public void TestGetNamespaceTreesReturnsAllNamepsaces()
        {
            // Arrange
            string inputCode = File.ReadAllText(SimpleMultiNamespaceFile);
            var parser = new CsTextParser();

            // Act
            var globalNamespace = parser.GetGlobalNamespace(inputCode);

            // Assert
            Assert.That(globalNamespace.ChildNamespaces.Count(), Is.EqualTo(2));
        }

        [Test]
        public void TestNestedTypesAreFoundAndLinkedTogether()
        {
            // Arrange
            string inputCode = File.ReadAllText(SimpleNestedElementFile);
            var parser = new CsTextParser();
            var expectedGlobalNamespace = new NamespaceModel();
            var topLevelNamespace = new NamespaceModel { Name = "N1" };
            var nestedNamespace = new NamespaceModel { Name = "N2" };
            var classN1L1 = new ClassModel {Name = "ClassN1L1", Namespace = topLevelNamespace};
            var classC1L2 = new ClassModel {Name = "ClassC1L2"};
            var classS1L2 = new ClassModel {Name = "ClassS1L2"};
            var classN2L1 = new ClassModel {Name = "ClassN2L1", Namespace = nestedNamespace};
            var structN1L1 = new StructModel {Name = "StructN1L1", Namespace = topLevelNamespace};
            var structC1L2 = new StructModel {Name = "StructC1L2"};
            var structS1L2 = new StructModel {Name = "StructS1L2"};
            var structN2L1 = new StructModel {Name = "StructN2L1", Namespace = nestedNamespace};

            classN1L1.NestedTypes.Add(classC1L2);
            classN1L1.NestedTypes.Add(structC1L2);

            structN1L1.NestedTypes.Add(classS1L2);
            structC1L2.NestedTypes.Add(structS1L2);

            topLevelNamespace.Classes.Add(classN1L1);
            topLevelNamespace.Structs.Add(structN1L1);

            nestedNamespace.Classes.Add(classN2L1);
            nestedNamespace.Structs.Add(structN2L1);
            topLevelNamespace.ChildNamespaces.Add(nestedNamespace);

            expectedGlobalNamespace.ChildNamespaces.Add(topLevelNamespace);

            // Act
            var returnedGlobalNamespace = parser.GetGlobalNamespace(inputCode);

            // Assert
            Assert.That(returnedGlobalNamespace, Is.EqualTo(expectedGlobalNamespace));              
        }
    }
}
