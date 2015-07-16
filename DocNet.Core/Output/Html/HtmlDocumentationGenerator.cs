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

            // Process Methods
            foreach (var method in interfaceModel.Methods)
            {
                ProcessMethod(method, outputDirectory);
            }

            // Process Properties
            foreach (var property in interfaceModel.Properties)
            {
                ProcessProperty(property, outputDirectory);
            }
        }

        private void ProcessClass(ClassModel classModel, string outputDirectory)
        {
            string classFilePath = Path.Combine(outputDirectory, classModel.FullName + ".html");
            WriteView<ClassDetail, ClassModel>(classFilePath, classModel);

            // Process Nested Types
            foreach(var nestedType in classModel.NestedTypes)
            {
                ProcessCsType(nestedType, outputDirectory);
            }

            // Process Constructors
            foreach(var constructor in classModel.Constructors)
            {
                ProcessConstructor(constructor, outputDirectory);
            }

            // Process Methods
            foreach(var method in classModel.Methods)
            {
                ProcessMethod(method, outputDirectory);
            }

            // Process Properties
            foreach(var property in classModel.Properties)
            {
                ProcessProperty(property, outputDirectory);
            }
        }

        private void ProcessStruct(StructModel structModel, string outputDirectory)
        {
            string structFilePath = Path.Combine(outputDirectory, structModel.Name + ".html");
            WriteView<StructDetail, StructModel>(structFilePath, structModel);

            // Process Nested Types
            foreach (var nestedType in structModel.NestedTypes)
            {
                ProcessCsType(nestedType, outputDirectory);
            }

            // Process Constructors
            foreach (var constructor in structModel.Constructors)
            {
                ProcessConstructor(constructor, outputDirectory);
            }

            // Process Methods
            foreach (var method in structModel.Methods)
            {
                ProcessMethod(method, outputDirectory);
            }

            // Process Properties
            foreach (var property in structModel.Properties)
            {
                ProcessProperty(property, outputDirectory);
            }
        }

        private void ProcessEnum(EnumModel enumModel, string outputDirectory)
        {
            string enumFilePath = Path.Combine(outputDirectory, enumModel.Name + ".html");
            WriteView<EnumDetail, EnumModel>(enumFilePath, enumModel);
        }

        private void ProcessDelegate(DelegateModel delegateModel, string outputDirectory)
        {
            string delegateFilePath = Path.Combine(outputDirectory, delegateModel.Name + ".html");
            WriteView<DelegateDetail, DelegateModel>(delegateFilePath, delegateModel);
        }

        private void ProcessConstructor(ConstructorModel constructorModel, string outputDirectory)
        {
            string constructorFilePath = Path.Combine(outputDirectory, constructorModel.Name + ".html");
            WriteView<ConstructorDetail, ConstructorModel>(constructorFilePath, constructorModel);
        }

        private void ProcessMethod(MethodModel methodModel, string outputDirectory)
        {
            string methodFilePath = Path.Combine(outputDirectory, methodModel.Name + ".html");
            WriteView<MethodDetail, MethodModel>(methodFilePath, methodModel);
        }

        private void ProcessProperty(PropertyModel propertyModel, string outputDirectory)
        {
            string propertyFilePath = Path.Combine(outputDirectory, propertyModel.Name + ".html");
            WriteView<PropertyDetail, PropertyModel>(propertyFilePath, propertyModel);
        }

        private void ProcessCsType(CsTypeModel csType, string outputDirectory)
        {
            if (csType is ClassModel)
                ProcessClass(csType as ClassModel, outputDirectory);
            else if (csType is StructModel)
                ProcessStruct(csType as StructModel, outputDirectory);
            else if (csType is InterfaceModel)
                ProcessInterface(csType as InterfaceModel, outputDirectory);
            else if (csType is EnumModel)
                ProcessEnum(csType as EnumModel, outputDirectory);
            else if (csType is DelegateModel)
                ProcessDelegate(csType as DelegateModel, outputDirectory);
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
