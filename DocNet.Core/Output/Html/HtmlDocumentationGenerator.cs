using System;
using System.IO;
using DocNet.Core.Models.CSharp;
using DocNet.Core.Output.Html.Views;
using DocNet.RazorGenerator.Views;

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
            ProcessNamespace(globalNamespace, _rootOutputDirectory);
        }

        #region Helper Methods

        private void ProcessNamespace(NamespaceModel currentNamespace, string outputDirectory)
        {
            // Create namespace file
            string namespaceDetailFilePath = Path.Combine(outputDirectory, NamespaceFileName);
            var namespaceViewModel = new ViewModel<NamespaceModel>()
            {
                Model = currentNamespace,
                RootDirectory = _rootOutputDirectory,
                ViewPath = namespaceDetailFilePath
            };
            WriteView<NamespaceDetail, ViewModel<NamespaceModel>>(namespaceDetailFilePath, namespaceViewModel);

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

            }

            // Process Child Classes
            foreach (var childClass in currentNamespace.Classes)
            {
                
            }

            // Process Child Enums
            foreach (var childEnum in currentNamespace.Enums)
            {
                
            }

            // Process Child Delegates
            foreach (var childDelegate in currentNamespace.Delegates)
            {
                
            }
        }

        private static void WriteView<TView, TModel>(string filePath, TModel model) where TView:BaseTemplate<TModel>, new()
        {
            using (var writer = new StreamWriter(filePath))
            {
                var view = new TView
                {
                    Writer = writer,
                    ViewData = model
                };
                view.Execute();
            }
        }

        #endregion
    }
}
