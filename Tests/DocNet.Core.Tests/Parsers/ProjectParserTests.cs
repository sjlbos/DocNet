using System;
using System.Collections.Generic;
using System.IO;
using DocNet.Core.Parsers.VisualStudio;
using DocNet.Models.VisualStudio;
using NUnit.Framework;

namespace DocNet.Core.Tests.Parsers
{
    [TestFixture]
    public class ProjectParserTests
    {
        private const string TestFileDirectory = @"..\..\..\TestData";
        private static readonly string BasicSolutionDirectory = Path.Combine(TestFileDirectory, @"BasicSolution");

        [Test]
        public void TestParseProjectFileThrowsExceptionForNullInput()
        {
            var parser = new ProjectParser();
            Assert.Throws<ArgumentNullException>(() => parser.ParseProjectFile(null));
        }

        [Test]
        public void TestParseProjectFileThrowsExceptionForInvalidFilePath()
        {
            var parser = new ProjectParser();
            Assert.Throws<FileNotFoundException>(() => parser.ParseProjectFile("C:\\nonExistentProjectFile.csproj"));
        }

        [Test]
        public void TestParserProjectFileThrowsExceptionWhenInputIsNotProjectFile()
        {
            var parser = new ProjectParser();
            Assert.Throws<InvalidOperationException>(() => parser.ParseProjectFile(BasicSolutionDirectory));
            Assert.Throws<InvalidOperationException>(() => parser.ParseProjectFile(Path.Combine(BasicSolutionDirectory, "BasicSolution.sln")));
        }

        [Test]
        public void TestParseProjectFileReturnsModelWithCorrectListOfCSharpFilesForValidInputProject()
        {
            // Arrange
            string projectWith5FilesDirectory = Path.Combine(BasicSolutionDirectory, "ProjectWith5Files");
            string projectWith5FilesPath = Path.Combine(projectWith5FilesDirectory, "ProjectWith5Files.csproj");
            var projectWith5Files = new ProjectModel
            {
                Name = "ProjectWith5Files",
                FilePath = projectWith5FilesPath,
                IncludedFilePaths = new List<string>
                {
                    Path.Combine(projectWith5FilesDirectory, "Class1.cs"),
                    Path.Combine(projectWith5FilesDirectory, "Class2.cs"),
                    Path.Combine(projectWith5FilesDirectory, "Class3.cs"),
                    Path.Combine(projectWith5FilesDirectory, "Class4.cs"),
                    Path.Combine(projectWith5FilesDirectory, "Class5.cs"),
                    Path.Combine(projectWith5FilesDirectory, "Properties", "AssemblyInfo.cs")
                }
            };

            var parser = new ProjectParser();

            // Act
            var returnedProject = parser.ParseProjectFile(projectWith5FilesPath);

            // Assert
            Assert.NotNull(returnedProject);
            Assert.That(returnedProject, Is.EqualTo(projectWith5Files));
        }

        [Test]
        public void TestParseProjectFileReturnsModelWithEmptySourceFileListWhenProjectContainsNoFiles()
        {
            // Arrange
            string projectWithNoFilesPath = Path.Combine(BasicSolutionDirectory, "ProjectWithNoFiles", "ProjectWithNoFiles.csproj");
            var projectWithNoFiles = new ProjectModel
            {
                Name = "ProjectWithNoFiles",
                FilePath = projectWithNoFilesPath,
                IncludedFilePaths = new List<string>()
            };

            var parser = new ProjectParser();

            // Act
            var returnedProject = parser.ParseProjectFile(projectWithNoFilesPath);

            // Assert
            Assert.NotNull(returnedProject);
            Assert.That(returnedProject, Is.EqualTo(projectWithNoFiles));
        }
    }
}
