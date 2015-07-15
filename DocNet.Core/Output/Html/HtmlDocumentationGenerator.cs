using System;
using System.IO;
using DocNet.Core.Models.CSharp;
using DocNet.Core.Output.Html.Views;
using DocNet.Razor.Views;

namespace DocNet.Core.Output.Html
{
    public class HtmlDocumentationGenerator : IDocumentationGenerator
    {
        private const string GlobalNamespaceDirectoryName = "html";
        private const string NamespaceFileName = "index.html";

        private string _rootOutputDirectory;

        public void GenerateDocumentation(NamespaceModel globalNamespace, string outputDirectory)
        {
            if(globalNamespace == null)
                throw new ArgumentNullException("globalNamespace");
            if(!Directory.Exists(outputDirectory))
                throw new DirectoryNotFoundException(outputDirectory);

            _rootOutputDirectory = Path.Combine(outputDirectory, GlobalNamespaceDirectoryName);
            Directory.CreateDirectory(_rootOutputDirectory);
            ProcessNamespace(globalNamespace, _rootOutputDirectory);
        }

        #region Helper Methods

        private void ProcessNamespace(NamespaceModel currentNamespace, string outputDirectory)
        {
            // Create namespace file
            string namespaceDetailFilePath = Path.Combine(outputDirectory, NamespaceFileName);
            WriteView<NamespaceDetail, NamespaceModel>(namespaceDetailFilePath, currentNamespace);

            // Process Child Namespaces
            foreach (var childNamespace in currentNamespace.ChildNamespaces)
            {
                // Make namespace directory
                string namespaceDirectoryPath = Path.Combine(outputDirectory, childNamespace.Name);
                Directory.CreateDirectory(namespaceDirectoryPath);

                ProcessNamespace(childNamespace, namespaceDirectoryPath);
            }

            // Process Child Interfaces
            foreach (var childInterface in currentNamespace.Interfaces)
            {
                ProcessInterface(childInterface, outputDirectory);
            }

            // Process Child Classes
            foreach (var childClass in currentNamespace.Classes)
            {
                ProcessClass(childClass, outputDirectory);   
            }

            // Process Child Structs
            foreach (var childStruct in currentNamespace.Structs)
            {
                ProcessStruct(childStruct, outputDirectory);
            }

            // Process Child Enums
            foreach (var childEnum in currentNamespace.Enums)
            {
                ProcessEnum(childEnum, outputDirectory);
            }

            // Process Child Delegates
            foreach (var childDelegate in currentNamespace.Delegates)
            {
                ProcessDelegate(childDelegate, outputDirectory);
            }
        }

        private void ProcessInterface(InterfaceModel interfaceModel, string outputDirectory)
        {
            string interfaceFilePath = Path.Combine(outputDirectory, interfaceModel.FullName + ".html");
            WriteView<InterfaceDetail, InterfaceModel>(interfaceFilePath, interfaceModel);
        }

        private void ProcessClass(ClassModel classModel, string outputDirectory)
        {
            string classFilePath = Path.Combine(outputDirectory, classModel.FullName + ".html");
            WriteView<ClassDetail, ClassModel>(classFilePath, classModel);
        }

        private void ProcessStruct(StructModel structModel, string outputDirectory)
        {
            string structFilePath = Path.Combine(outputDirectory, structModel.FullName + ".html");
            WriteView<StructDetail, StructModel>(structFilePath, structModel);
        }

        private void ProcessEnum(EnumModel enumModel, string outputDirectory)
        {
            string enumFilePath = Path.Combine(outputDirectory, enumModel.FullName + ".html");
            WriteView<EnumDetail, EnumModel>(enumFilePath, enumModel);
        }

        private void ProcessDelegate(DelegateModel delegateModel, string outputDirectory)
        {
            string delegateFilePath = Path.Combine(outputDirectory, delegateModel.FullName + ".html");
            WriteView<DelegateDetail, DelegateModel>(delegateFilePath, delegateModel);
        }

        private void WriteView<TView, TModel>(string filePath, TModel model) where TView:BaseTemplate<TModel>, new()
        {
            using (var writer = new StreamWriter(filePath))
            {
                var view = new TView
                {
                    Writer = writer,
                    RootDirectoryAbsolutePath = _rootOutputDirectory,
                    ViewAbsolutePath = filePath,
                    Model = model
                };
                view.Execute();
            }
        }

        #endregion
    }
}
