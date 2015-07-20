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

        private const string OutputFileExtension = ".html";
        private const string RootFileName = "index" + OutputFileExtension;
        
        private static readonly string MarkupFileDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Export");

        private readonly IEnumerable<string> _allExportedFiles; 
        private readonly IEnumerable<string> _cssFiles;
        private readonly IEnumerable<string> _scriptFiles;

        private GlobalNamespaceModel _globalNamespace;
        private string _outputDirectory;

        public HtmlDocumentationGenerator(IEnumerable<string> markupFilesToExport)
        {
            if(markupFilesToExport == null)
                throw new ArgumentNullException("markupFilesToExport");

            _allExportedFiles = markupFilesToExport;
            _cssFiles = markupFilesToExport.Where(name => name.EndsWith(".css", StringComparison.Ordinal));
            _scriptFiles = markupFilesToExport.Where(name => name.EndsWith(".js", StringComparison.Ordinal));
        }

        #region Public Interface

        public void GenerateDocumentation(GlobalNamespaceModel globalNamespace, string outputDirectory)
        {
            if(globalNamespace == null)
                throw new ArgumentNullException("globalNamespace");
            if(!Directory.Exists(outputDirectory))
                throw new DirectoryNotFoundException(outputDirectory);

            _globalNamespace = globalNamespace;
            _outputDirectory = outputDirectory;

            CopyExportFilesToDirectory(_outputDirectory);
            ProcessNamespace(globalNamespace);
        }

        public static string GetFileNameForCsElement(CsElement element)
        {
            if (element == null)
                throw new ArgumentNullException("element");
            if (element.FullName == null) 
                throw new ArgumentException("CsElement has null name property.");
            if (element is GlobalNamespaceModel)
                return RootFileName;
            return SanitizeOutputFileName(element.FullName) + OutputFileExtension;
        }

        #endregion

        #region Helper Methods

        private void CopyExportFilesToDirectory(string outputDirectory)
        {
            Log.Debug("Copying exported files to output directory.");
            foreach (var file in _allExportedFiles)
            {
                CopyExportFileToDirectory(file, outputDirectory);
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

        private void ProcessNamespace(NamespaceBase currentNamespace)
        {
            // Create namespace file
            string namespaceFileName = GetFileNameForCsElement(currentNamespace);
            WriteView<NamespaceDetail, NamespaceBase>(namespaceFileName, currentNamespace);

            // Process Child Namespaces
            foreach (var childNamespace in currentNamespace.ChildNamespaces)
            {
                ProcessNamespace(childNamespace);
            }

            // Process Child Interfaces
            foreach (var childInterface in currentNamespace.Interfaces)
            {
                ProcessInterface(childInterface);
            }

            // Process Child Classes
            foreach (var childClass in currentNamespace.Classes)
            {
                ProcessClass(childClass);
            }

            // Process Child Structs
            foreach (var childStruct in currentNamespace.Structs)
            {
                ProcessStruct(childStruct);
            }

            // Process Child Enums
            foreach (var childEnum in currentNamespace.Enums)
            {
                ProcessEnum(childEnum);
            }

            // Process Child Delegates
            foreach (var childDelegate in currentNamespace.Delegates)
            {
                ProcessDelegate(childDelegate);
            }
        }

        private void ProcessInterface(InterfaceModel interfaceModel)
        {
            string interfaceFileName = GetFileNameForCsElement(interfaceModel);
            WriteView<InterfaceDetail, InterfaceModel>(interfaceFileName, interfaceModel);

            ProcessInterfaceMembers(interfaceModel);
        }

        private void ProcessClass(ClassModel classModel)
        {
            string classFileName = GetFileNameForCsElement(classModel);
            WriteView<ClassDetail, ClassModel>(classFileName, classModel);

            ProcessInterfaceMembers(classModel);
            ProcessClassAndStructMembers(classModel);
        }

        private void ProcessStruct(StructModel structModel)
        {
            string structFileName = GetFileNameForCsElement(structModel);
            WriteView<StructDetail, StructModel>(structFileName, structModel);

            ProcessInterfaceMembers(structModel);
            ProcessClassAndStructMembers(structModel);
        }

        private void ProcessEnum(EnumModel enumModel)
        {
            string enumFileName = GetFileNameForCsElement(enumModel);
            WriteView<EnumDetail, EnumModel>(enumFileName, enumModel);
        }

        private void ProcessDelegate(DelegateModel delegateModel)
        {
            string delegateFileName = GetFileNameForCsElement(delegateModel);
            WriteView<DelegateDetail, DelegateModel>(delegateFileName, delegateModel);
        }

        private void ProcessConstructor(ConstructorModel constructorModel)
        {
            string constructorFileName = GetFileNameForCsElement(constructorModel);
            WriteView<ConstructorDetail, ConstructorModel>(constructorFileName, constructorModel);
        }

        private void ProcessMethod(MethodModel methodModel)
        {
            string methodFileName = GetFileNameForCsElement(methodModel);
            WriteView<MethodDetail, MethodModel>(methodFileName, methodModel);
        }

        private void ProcessProperty(PropertyModel propertyModel)
        {
            string propertyFileName = GetFileNameForCsElement(propertyModel);
            WriteView<PropertyDetail, PropertyModel>(propertyFileName, propertyModel);
        }

        private void ProcessInterfaceMembers(InterfaceBase model)
        {
            // Process Methods
            foreach (var method in model.Methods)
            {
                ProcessMethod(method);
            }

            // Process Properties
            foreach (var property in model.Properties)
            {
                ProcessProperty(property);
            }
        }

        private void ProcessClassAndStructMembers(ClassAndStructBase model)
        {
            // Process Constructors
            foreach (var constructor in model.Constructors)
            {
                ProcessConstructor(constructor);
            }

            // Process Nested Interfaces
            foreach (var interfaceModel in model.InnerInterfaces)
            {
                ProcessInterface(interfaceModel);
            }

            // Process Nested Classes
            foreach (var classModel in model.InnerClasses)
            {
                ProcessClass(classModel);
            }

            // Process Nested Structs
            foreach (var structModel in model.InnerStructs)
            {
                ProcessStruct(structModel);
            }

            // Process Nested Enums
            foreach (var enumModel in model.InnerEnums)
            {
                ProcessEnum(enumModel);
            }

            // Process Nested Delegates
            foreach (var delegateModel in model.InnerDelegates)
            {
                ProcessDelegate(delegateModel);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private void WriteView<TView, TModel>(string fileName, TModel model) 
            where TView:BodyTemplate<TModel>, new()
            where TModel:CsElement
        {
            string filePath = Path.Combine(_outputDirectory, fileName);
            Log.InfoFormat(CultureInfo.CurrentCulture,
                "Writing \"{0}\".", filePath);
            using (var writer = new StreamWriter(filePath))
            {
                var page = new Page
                {
                    Writer = writer,
                    GlobalNamespace = _globalNamespace,
                    OutputDirectoryAbsolutePath = _outputDirectory,
                    ScriptFiles = _scriptFiles,
                    CssFiles = _cssFiles,
                    Body = new TView
                    {
                        Model = model,
                        GlobalNamespace = _globalNamespace,
                        OutputDirectoryAbsolutePath = _outputDirectory
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
