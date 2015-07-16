using System;
using System.Diagnostics;
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

            ProcessInterfaceMembers(interfaceModel, outputDirectory);
        }

        private void ProcessClass(ClassModel classModel, string outputDirectory)
        {
            string classFilePath = Path.Combine(outputDirectory, classModel.FullName + ".html");
            WriteView<ClassDetail, ClassModel>(classFilePath, classModel);

            ProcessInterfaceMembers(classModel, outputDirectory);
            ProcessClassAndStructMembers(classModel, outputDirectory);
        }

        private void ProcessStruct(StructModel structModel, string outputDirectory)
        {
            string structFilePath = Path.Combine(outputDirectory, structModel.Name + ".html");
            WriteView<StructDetail, StructModel>(structFilePath, structModel);

            ProcessInterfaceMembers(structModel, outputDirectory);
            ProcessClassAndStructMembers(structModel, outputDirectory);
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

        private void ProcessInterfaceMembers(InterfaceBase model, string outputDirectory)
        {
            // Process Methods
            foreach (var method in model.Methods)
            {
                ProcessMethod(method, outputDirectory);
            }

            // Process Properties
            foreach (var property in model.Properties)
            {
                ProcessProperty(property, outputDirectory);
            }
        }

        private void ProcessClassAndStructMembers(ClassAndStructBase model, string outputDirectory)
        {
            // Process Constructors
            foreach (var constructor in model.Constructors)
            {
                ProcessConstructor(constructor, outputDirectory);
            }

            // Process Nested Interfaces
            foreach (var interfaceModel in model.InnerInterfaces)
            {
                ProcessInterface(interfaceModel, outputDirectory);
            }

            // Process Nested Classes
            foreach (var classModel in model.InnerClasses)
            {
                ProcessClass(classModel, outputDirectory);
            }

            // Process Nested Structs
            foreach (var structModel in model.InnerStructs)
            {
                ProcessStruct(structModel, outputDirectory);
            }

            // Process Nested Enums
            foreach (var enumModel in model.InnerEnums)
            {
                ProcessEnum(enumModel, outputDirectory);
            }

            // Process Nested Delegates
            foreach (var delegateModel in model.InnerDelegates)
            {
                ProcessDelegate(delegateModel, outputDirectory);
            }
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
