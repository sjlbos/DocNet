using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using DocNet.Core.Models.CSharp;
using DocNet.Core.Output.Html.Views;
using DocNet.Razor.Views;
using log4net;

namespace DocNet.Core.Output.Html
{
    public class HtmlDocumentationGenerator : IDocumentationGenerator
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(HtmlDocumentationGenerator));

        private const string RootFileName = "index.html";


        private static readonly string MarkupFileDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Export");
        
        private readonly IEnumerable<string> _cssFiles;
        private readonly IEnumerable<string> _scriptFiles; 

        public HtmlDocumentationGenerator(IEnumerable<string> markupFilesToExport)
        {
            if(markupFilesToExport == null)
                throw new ArgumentNullException("markupFilesToExport");

            _cssFiles = markupFilesToExport.Where(name => name.EndsWith(".css", StringComparison.Ordinal));
            _scriptFiles = markupFilesToExport.Where(name => name.EndsWith(".js", StringComparison.Ordinal));
        }

        public void GenerateDocumentation(NamespaceModel globalNamespace, string outputDirectory)
        {
            if(globalNamespace == null)
                throw new ArgumentNullException("globalNamespace");
            if(!Directory.Exists(outputDirectory))
                throw new DirectoryNotFoundException(outputDirectory);

            CopyExportFilesToDirectory(outputDirectory);
            ProcessNamespace(globalNamespace, outputDirectory);
        }

        #region Helper Methods

        private void CopyExportFilesToDirectory(string outputDirectory)
        {
            Log.Debug("Copying .css and .js files to output directory.");
            foreach(var fileName in _cssFiles)
            {
                CopyExportFileToDirectory(fileName, outputDirectory);
            }
            foreach (var fileName in _scriptFiles)
            {
                CopyExportFileToDirectory(fileName, outputDirectory);
            }
        }

        private void CopyExportFileToDirectory(string fileName, string outputDirectory)
        {
            string sourcePath = Path.Combine(MarkupFileDirectoryPath, fileName);
            string destPath = Path.Combine(outputDirectory, fileName);
            Log.DebugFormat(CultureInfo.CurrentCulture,
                "Copying \"{0}\" to \"{1}\".", sourcePath, destPath);
            File.Copy(sourcePath, destPath, true);
        }

        private void ProcessNamespace(NamespaceModel currentNamespace, string outputDirectory)
        {
            // Create namespace file
            string namespaceFileName = currentNamespace.FullName != null ? currentNamespace.FullName + ".html" : RootFileName;
            WriteView<NamespaceDetail, NamespaceModel>(namespaceFileName, outputDirectory, currentNamespace);

            // Process Child Namespaces
            foreach (var childNamespace in currentNamespace.ChildNamespaces)
            {
                ProcessNamespace(childNamespace, outputDirectory);
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
            string interfaceFileName = interfaceModel.FullName + ".html";
            WriteView<InterfaceDetail, InterfaceModel>(interfaceFileName, outputDirectory, interfaceModel);

            ProcessInterfaceMembers(interfaceModel, outputDirectory);
        }

        private void ProcessClass(ClassModel classModel, string outputDirectory)
        {
            string classFileName = classModel.FullName + ".html";
            WriteView<ClassDetail, ClassModel>(classFileName, outputDirectory, classModel);

            ProcessInterfaceMembers(classModel, outputDirectory);
            ProcessClassAndStructMembers(classModel, outputDirectory);
        }

        private void ProcessStruct(StructModel structModel, string outputDirectory)
        {
            string structFileName = structModel.FullName + ".html";
            WriteView<StructDetail, StructModel>(structFileName, outputDirectory, structModel);

            ProcessInterfaceMembers(structModel, outputDirectory);
            ProcessClassAndStructMembers(structModel, outputDirectory);
        }

        private void ProcessEnum(EnumModel enumModel, string outputDirectory)
        {
            string enumFileName = enumModel.FullName + ".html";
            WriteView<EnumDetail, EnumModel>(enumFileName, outputDirectory, enumModel);
        }

        private void ProcessDelegate(DelegateModel delegateModel, string outputDirectory)
        {
            string delegateFileName = delegateModel.FullName + ".html";
            WriteView<DelegateDetail, DelegateModel>(delegateFileName, outputDirectory, delegateModel);
        }

        private void ProcessConstructor(ConstructorModel constructorModel, string outputDirectory)
        {
            string constructorFileName = constructorModel.FullName + ".html";
            WriteView<ConstructorDetail, ConstructorModel>(constructorFileName, outputDirectory, constructorModel);
        }

        private void ProcessMethod(MethodModel methodModel, string outputDirectory)
        {
            string methodFileName = methodModel.FullName + ".html";
            WriteView<MethodDetail, MethodModel>(methodFileName, outputDirectory, methodModel);
        }

        private void ProcessProperty(PropertyModel propertyModel, string outputDirectory)
        {
            string propertyFileName = propertyModel.FullName + ".html";
            WriteView<PropertyDetail, PropertyModel>(propertyFileName, outputDirectory, propertyModel);
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private void WriteView<TView, TModel>(string fileName, string outputDirectory, TModel model) where TView:BodyTemplate<TModel>, new()
        {
            string filePath = SanitizeOutputFileName(Path.Combine(outputDirectory, fileName));
            Log.InfoFormat(CultureInfo.CurrentCulture,
                "Writing \"{0}\".", filePath);
            using (var writer = new StreamWriter(filePath))
            {
                var page = new Page
                {
                    Writer = writer,
                    GlobalNamespace = null,
                    OutputDirectoryAbsolutePath = outputDirectory,
                    ScriptFiles = _scriptFiles,
                    CssFiles = _cssFiles,
                    Body = new TView
                    {
                        Model = model,
                        GlobalNamespace = null,
                        OutputDirectoryAbsolutePath = outputDirectory
                    }
                };

                page.Execute();
            }
        }

        private static string SanitizeOutputFileName(string fileName)
        {
            return fileName
                    .Replace('<', '{')
                    .Replace('>', '}');
        }

        #endregion
    }
}
