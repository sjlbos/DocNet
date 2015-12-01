using System.IO;
using System.Reflection;
using DocNet.Core.Parsers.CSharp;
using NUnit.Framework;

namespace DocNet.Core.Tests.Parsers
{
    [TestFixture]
    public class CsAssemblyParserTests
    {
        private const string AssemblyBuildPath = @"..\..\..\DummyInputProject\bin\Debug\";
        private static readonly string AssemblyPath = Path.Combine(AssemblyBuildPath, "DummyInputProject.dll");
        private static readonly string XmlFilePath = Path.Combine(AssemblyBuildPath, "DummyInputProject.XML");

        [Test]
        public void TestHarness()
        {
            Assembly assembly = Assembly.LoadFrom(AssemblyPath);
            var xmlFile = new FileStream(XmlFilePath, FileMode.Open, FileAccess.Read);
            var parser = new CsAssemblyParser();
            var globalNamespace = parser.GetGlobalNamespace(assembly, xmlFile, OutputMode.AllElements);
        }
    }
}
