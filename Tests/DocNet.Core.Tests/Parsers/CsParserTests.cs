using System.IO;
using System.Linq;
using DocNet.Core.Parsers.CSharp;
using NUnit.Framework;

namespace DocNet.Core.Tests.Parsers
{
    [TestFixture]
    public class CsParserTests
    {
        private static readonly string TestFileDirectory = @"..\..\..\TestData";
        private static readonly string SimpleMultiNamespaceFile = Path.Combine(TestFileDirectory, "SimpleMultiNamespaceFile.cs");

        [Test]
        public void TestGetNamespaceTreesReturnsAllNamepsaces()
        {
            string sampleInput = File.ReadAllText(SimpleMultiNamespaceFile);
            var parser = new CsTextParser(sampleInput);

            var namespaces = parser.GetNamespaceTrees();

            Assert.That(namespaces.Count(), Is.EqualTo(2));
        }
    }
}
