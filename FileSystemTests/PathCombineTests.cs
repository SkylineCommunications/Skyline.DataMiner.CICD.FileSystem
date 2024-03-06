namespace Skyline.DataMiner.CICD.FileSystem.Tests
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Skyline.DataMiner.CICD.FileSystem;

    [TestClass]
    public class PathCombineTests
    {
        private readonly HashSet<char> _invalidCharacters = new HashSet<char>(System.IO.Path.GetInvalidPathChars());

        [TestMethod]
        [DataRow(new[] { "C:\\FolderA\\folderB", "FolderC\\FolderD", "File.xml" }, "C:\\FolderA\\folderB\\FolderC\\FolderD\\File.xml")]
        [DataRow(new[] { "C:\\FolderA\\folderB", "File.xml" }, "C:\\FolderA\\folderB\\File.xml")]
        [DataRow(new[] { "C:\\FolderA", "File.xml" }, "C:\\FolderA\\File.xml")]
        [DataRow(new[] { "C:\\", "File.xml" }, "C:\\File.xml")]
        [DataRow(new[] { "D:\\", "File.xml" }, "D:\\File.xml")]
        [DataRow(new[] { "C:\\FolderA\\folderB", "FolderC/FolderD", "File.xml" }, "C:\\FolderA\\folderB\\FolderC\\FolderD\\File.xml")]
        [DataRow(new[] { "C:\\FolderA\\folderB", "FolderC\\FolderD\\", "File.xml" }, "C:\\FolderA\\folderB\\FolderC\\FolderD\\File.xml")]
        [DataRow(new[] { "C:\\FolderA\\folderB", "FolderC\\FolderD\\", "/File.xml" }, "C:\\FolderA\\folderB\\FolderC\\FolderD\\File.xml")]
        [DataRow(new[] { "/github/workspace/FolderA/folderB", "FolderC\\FolderD", "File.xml" }, "/github/workspace/FolderA/folderB/FolderC/FolderD/File.xml")]
        [DataRow(new[] { "/github/workspace/FolderA/folderB", "FolderC\\FolderD\\", "File.xml" }, "/github/workspace/FolderA/folderB/FolderC/FolderD/File.xml")]
        [DataRow(new[] { "/github/", "FolderC/FolderD", "File.xml" }, "/github/FolderC/FolderD/File.xml")]
        [DataRow(new[] { "/github", "FolderC/FolderD" }, "/github/FolderC/FolderD")]
        [DataRow(new[] { "/github\\what?", "FolderC/FolderD" }, "/github/what?/FolderC/FolderD")]
        public void UniversalCombineTests(string[] paths, string expectedResult)
        {
            var result = PathCombine.UniversalCombine(paths, _invalidCharacters);
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void UniversalCombine_NullPaths_ExpectedArgumentNullException()
        {
            // Arrange

            // Act
            Action act = () => PathCombine.UniversalCombine(null, _invalidCharacters);

            // Assert
            act.Should().ThrowExactly<ArgumentNullException>();
        }

        [TestMethod]
        public void UniversalCombine_NullInvalidChars_ExpectedArgumentNullException()
        {
            // Arrange

            // Act
            Action act = () => PathCombine.UniversalCombine(new[] { "C:\\", "File.xml" }, null);

            // Assert
            act.Should().ThrowExactly<ArgumentNullException>();
        }
    }
}