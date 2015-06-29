
using System;
using System.Collections.Generic;
using System.IO;
using DocNet.Core.Parsers.VisualStudio;
using DocNet.Models.VisualStudio;
using FakeItEasy;
using NUnit.Framework;

namespace DocNet.Core.Tests.Parsers
{
    [TestFixture]
    public class OnionSolutionParserWrapperTests
    {
        private const string TestFileDirectory = @"..\..\..\TestData";
        private static readonly string EmptySolutionPath = Path.Combine(TestFileDirectory, @"EmptySolution\EmptySolution.sln");
        private static readonly string BasicSolutionDirectory = Path.Combine(TestFileDirectory, @"BasicSolution");
        private static readonly string BasicSolutionPath = Path.Combine(BasicSolutionDirectory, @"BasicSolution.sln");

        [Test]
        public void TestParseSolutionFileThrowsExceptionForNullInput()
        {
            var parser = new OnionSolutionParserWrapper(A.Fake<IProjectParser>());
            Assert.Throws<ArgumentNullException>(() => parser.ParseSolutionFile(null));
        }

        [Test]
        public void TestParseSolutionFileThrowsExceptionForInvalidFilePath()
        {
            var parser = new OnionSolutionParserWrapper(A.Fake<IProjectParser>());
            Assert.Throws<FileNotFoundException>(() => parser.ParseSolutionFile("C:\\nonExistentSolutionFile.sln"));
        }

        [Test]
        public void TestParseSolutionFileThrowsExceptionWhenInputIsNotSolutionFile()
        {
            var notASolutionFile = Path.Combine(BasicSolutionDirectory, "ProjectWithNoFiles", "ProjectWithNoFiles.csproj");
            var parser = new OnionSolutionParserWrapper(A.Fake<IProjectParser>());
            Assert.Throws<IOException>(() => parser.ParseSolutionFile(notASolutionFile));
            Assert.Throws<IOException>(() => parser.ParseSolutionFile(BasicSolutionDirectory));
        }

        [Test]
        public void TestParseSolutionFileReturnsCorrectModelForValidInputSolution()
        {
            // Arrange
            string projectWithNoFilesPath = Path.Combine(BasicSolutionDirectory, "ProjectWithNoFiles", "ProjectWithNoFiles.csproj");
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

            var projectWithNoFiles = new ProjectModel
            {
                Name = "ProjectWithNoFiles",
                FilePath = projectWithNoFilesPath,
                IncludedFilePaths = new List<string>()
            };

            var expectedSolutionModel = new SolutionModel
            {
                Name = "BasicSolution",
                FilePath = BasicSolutionPath,
                Projects = new []{projectWithNoFiles, projectWith5Files}
            };

            var fakeProjectParser = A.Fake<IProjectParser>();
            A.CallTo(() => fakeProjectParser.ParseProjectFile(projectWith5FilesPath)).Returns(projectWith5Files);
            A.CallTo(() => fakeProjectParser.ParseProjectFile(projectWithNoFilesPath)).Returns(projectWithNoFiles);

            var parser = new OnionSolutionParserWrapper(fakeProjectParser);

            // Act
            var model = parser.ParseSolutionFile(BasicSolutionPath);

            // Assert
            Assert.NotNull(model);
            Assert.That(model, Is.EqualTo(expectedSolutionModel));          
        }

        [Test]
        public void TestParserSolutionFileReturnsModelWithEmptyListOfProjectFilesForEmptySolution()
        {
            // Arrange
            var fakeProjectParser = A.Fake<IProjectParser>();
            A.CallTo(() => fakeProjectParser.ParseProjectFile(A<string>.Ignored))
                .Returns(new ProjectModel {Name = "DummyProject"});
            var parser = new OnionSolutionParserWrapper(fakeProjectParser);

            // Act
            var model = parser.ParseSolutionFile(EmptySolutionPath);

            // Assert
            Assert.NotNull(model);
            Assert.That(model.Projects, Is.Empty);
        }
    }
}
