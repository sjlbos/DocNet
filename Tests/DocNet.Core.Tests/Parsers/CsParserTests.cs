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
            expectedGlobalNamespace.AddChild(topLevelNamespace);

            var classN1L1 = new ClassModel
            {
                Name = "ClassN1L1",
                AccessModifier = AccessModifier.Public,
                Constructors = new []{ new ConstructorModel{ Name = "ClassN1L1", AccessModifier = AccessModifier.Public} },
                Methods = new[] { new MethodModel { Name = "Method_ClassN1L1", ReturnType = "void", AccessModifier =  AccessModifier.Public} }
            };
            topLevelNamespace.AddChild(classN1L1);

            var classC1L2 = new ClassModel
            {
                Name = "ClassC1L2",
                AccessModifier = AccessModifier.Public,
                Constructors = new[] { new ConstructorModel { Name = "ClassC1L2", AccessModifier = AccessModifier.Public } },
                Methods = new[] { new MethodModel { Name = "Method_ClassC1L2", ReturnType = "void", AccessModifier = AccessModifier.Public } }
            };
            var structC1L2 = new StructModel
            {
                Name = "StructC1L2",
                AccessModifier = AccessModifier.Public,
                Methods = new[] { new MethodModel { Name = "Method_StructC1L2", ReturnType = "void", AccessModifier = AccessModifier.Public } }
            };
            classN1L1.AddChild(classC1L2);
            classN1L1.AddChild(structC1L2);


            var structN1L1 = new StructModel
            {
                Name = "StructN1L1",
                AccessModifier = AccessModifier.Public,
                Methods = new []{ new MethodModel{ Name = "Method_StructN1L1", ReturnType = "void", AccessModifier = AccessModifier.Public } }
            };
            topLevelNamespace.AddChild(structN1L1);

            var classS1L2 = new ClassModel
            {
                Name = "ClassS1L2",
                AccessModifier = AccessModifier.Public,
                Constructors = new []{ new ConstructorModel{ Name = "ClassS1L2", AccessModifier = AccessModifier.Public } },
                Methods = new []{ new MethodModel{ Name = "Method_ClassS1L2", ReturnType = "void", AccessModifier = AccessModifier.Public }}
            };
            var structS1L2 = new StructModel
            {
                Name = "StructS1L2",
                AccessModifier = AccessModifier.Public,
                Methods = new []{ new MethodModel{ Name = "Method_StructS1L2", ReturnType = "void", AccessModifier = AccessModifier.Public } }
            };
            structN1L1.AddChild(classS1L2);
            structN1L1.AddChild(structS1L2);

            var classN2L1 = new ClassModel
            {
                Name = "ClassN2L1",
                AccessModifier = AccessModifier.Public
            };
            var structN2L1 = new StructModel
            {
                Name = "StructN2L1",
                AccessModifier = AccessModifier.Public
            };

            var nestedNamespace = new NamespaceModel { Name = "N2" };
            topLevelNamespace.AddChild(nestedNamespace);

            nestedNamespace.AddChild(classN2L1);
            nestedNamespace.AddChild(structN2L1);

            // Act
            var returnedGlobalNamespace = parser.GetGlobalNamespace(inputCode);

            // Assert
            Assert.True(expectedGlobalNamespace.Equals(returnedGlobalNamespace));
        }
    }
}
