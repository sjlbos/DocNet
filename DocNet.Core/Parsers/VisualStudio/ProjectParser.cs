using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using DocNet.Models.VisualStudio;

namespace DocNet.Core.Parsers.VisualStudio
{
    /// <summary>
    /// A class responsible for parsing Visual Studio project files and converting the project into a DocNet 
    /// ProjectModel object.
    /// </summary>
    /// <remarks>
    /// To find the files included in the input project, this class uses LINQ to query the project file for a list of 
    /// &lt;Compile/&gt; tags from which the included files' file paths are extracted.
    /// </remarks>
    public class ProjectParser : IProjectParser
    {
        /// <summary>
        /// Parses the Visual Studio project file at the specified file path and returns a ProjectModel object representation of the project.
        /// </summary>
        /// <param name="projectFilePath">Full or local file path to the project file.</param>
        /// <returns>A ProjectModel object representation of the parsed project file.</returns>
        /// <exception cref="ArgumentNullException">Thrown when "projectFilePath" is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the input path points to a directory, non-project file, or an unparsable project file.</exception>
        /// <exception cref="FileNotFoundException">Thrown when the input path points to a non-existent file.</exception>
        public ProjectModel ParseProjectFile(string projectFilePath)
        {
            if(projectFilePath == null)
                throw new ArgumentNullException("projectFilePath");
            if(Directory.Exists(projectFilePath))
                throw new InvalidOperationException("Project file path points to a directory: " + projectFilePath);
            if(!File.Exists(projectFilePath))
                throw new FileNotFoundException(projectFilePath);

            try
            {
                var root = XElement.Load(projectFilePath);
                var compileNodes = root.Descendants(root.GetDefaultNamespace() + "Compile");

                string projectDirectoryPath = Path.GetDirectoryName(projectFilePath) ?? String.Empty;

                var projectModel = new ProjectModel
                {
                    Name = Path.GetFileNameWithoutExtension(projectFilePath),
                    FilePath = projectFilePath,
                    IncludedFilePaths = compileNodes.Select(sourceFile =>
                            Path.Combine(projectDirectoryPath, sourceFile.Attribute("Include").Value)
                        ).ToList()
                };

                return projectModel;
            }
            catch (XmlException ex)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.InvariantCulture,
                    "Unable to parse the project file at \"{0}\". The file is either not a valid project file or has corrupt XML.", projectFilePath),
                    ex);
            }
        }
    }
}
