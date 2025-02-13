namespace Skyline.DataMiner.CICD.FileSystem.Tests
{
    using System.Runtime.InteropServices;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PathIOLinuxTests
    {
        [TestMethod]
        public void ChangeExtensionTest_Linux()
        {
            var pathToTest = "/github/workspace/FolderA/MyFile.xml";

            PathIOLinux linux = new PathIOLinux();
            var result = linux.ChangeExtension(pathToTest, ".txt");
            Assert.AreEqual("/github/workspace/FolderA/MyFile.txt", result);
        }

        [TestMethod]
        public void ChangeExtensionTest_Windows()
        {
            var pathToTest = "C:\\Skyline DataMiner\\ProtocolScripts\\DllImport\\newtonsoft.json\\13.0.2-beta3\\lib\\net45\\Newtonsoft.Json.dll";

            PathIOLinux linux = new PathIOLinux();
            var result = linux.ChangeExtension(pathToTest, ".txt");
            Assert.AreEqual("C:\\Skyline DataMiner\\ProtocolScripts\\DllImport\\newtonsoft.json\\13.0.2-beta3\\lib\\net45\\Newtonsoft.Json.txt", result);
        }

        [TestMethod]
        public void GetDirectoryNameTest_Linux()
        {
            var pathToTest = "/github/workspace/FolderA/MyFile.xml";

            PathIOLinux linux = new PathIOLinux();
            var result = linux.GetDirectoryName(pathToTest);
            Assert.AreEqual("/github/workspace/FolderA", result);
        }

        [TestMethod]
        public void GetDirectoryNameTest_Windows()
        {
            var pathToTest = "C:\\Skyline DataMiner\\ProtocolScripts\\DllImport\\newtonsoft.json\\13.0.2-beta3\\lib\\net45\\Newtonsoft.Json.dll";

            PathIOLinux linux = new PathIOLinux();
            var result = linux.GetDirectoryName(pathToTest);
            Assert.AreEqual("C:\\Skyline DataMiner\\ProtocolScripts\\DllImport\\newtonsoft.json\\13.0.2-beta3\\lib\\net45", result);
        }

        [TestMethod]
        public void GetFileNameNameTest_Linux()
        {
            var pathToTest = "/github/workspace/FolderA/MyFile.xml";

            PathIOLinux linux = new PathIOLinux();
            var result = linux.GetFileName(pathToTest);
            Assert.AreEqual("MyFile.xml", result);
        }

        [TestMethod]
        public void GetFileNameTest_Windows()
        {
            var pathToTest = "C:\\Skyline DataMiner\\ProtocolScripts\\DllImport\\newtonsoft.json\\13.0.2-beta3\\lib\\net45\\Newtonsoft.Json.dll";

            PathIOLinux linux = new PathIOLinux();
            var result = linux.GetFileName(pathToTest);
            Assert.AreEqual("Newtonsoft.Json.dll", result);
        }

        [TestMethod]
        public void GetFileNameWithoutExtensionTest_Linux()
        {
            var pathToTest = "/github/workspace/FolderA/MyFile.xml";

            PathIOLinux linux = new PathIOLinux();
            var result = linux.GetFileNameWithoutExtension(pathToTest);
            Assert.AreEqual("MyFile", result);
        }

        [TestMethod]
        public void GetFileNameWithoutExtensionTest_Windows()
        {
            var pathToTest = "C:\\Skyline DataMiner\\ProtocolScripts\\DllImport\\newtonsoft.json\\13.0.2-beta3\\lib\\net45\\Newtonsoft.Json.dll";

            PathIOLinux linux = new PathIOLinux();
            var result = linux.GetFileNameWithoutExtension(pathToTest);
            Assert.AreEqual("Newtonsoft.Json", result);
        }

        [TestMethod]
        public void GetFullPathTest_NoSubFolder_Windows()
        {
            var pathToTest = "Skyline.DataMiner.CICD.FileSystem.dll";

            PathIOLinux linux = new PathIOLinux();
            var result = linux.GetFullPath(pathToTest);

            Assert.IsTrue(result.Contains(":"), "Does not have the drive letter root");
            bool forNet472 = result.EndsWith("FileSystemTests/bin/Debug/net472/Skyline.DataMiner.CICD.FileSystem.dll");
            bool for6 = result.EndsWith("FileSystemTests/bin/Debug/net6.0/Skyline.DataMiner.CICD.FileSystem.dll");
            Assert.IsTrue(for6 || forNet472, "Does not have the full path: " + result);
        }

        [TestMethod]
        public void GetFullPathTest_WithSubFolder_Linux()
        {
            var pathToTest = "SubFolderTest/TestFile.xml";

            PathIOLinux linux = new PathIOLinux();
            var result = linux.GetFullPath(pathToTest);

            Assert.IsTrue(result.Contains(":"), "Does not have the drive letter root");

            bool forNet472 = result.EndsWith("FileSystemTests/bin/Debug/net472/SubFolderTest/TestFile.xml");
            bool for6 = result.EndsWith("FileSystemTests/bin/Debug/net6.0/SubFolderTest/TestFile.xml");
            Assert.IsTrue(for6 || forNet472, "Does not have the full path." + result);
        }

        [TestMethod]
        public void GetFullPathTest_WithSubFolder_Windows()
        {
            var pathToTest = "SubFolderTest\\TestFile.xml";

            PathIOLinux linux = new PathIOLinux();
            var result = linux.GetFullPath(pathToTest);

            Assert.IsTrue(result.Contains(":"), "Does not have the drive letter root");
            bool forNet472 = result.EndsWith("FileSystemTests\\bin\\Debug\\net472\\SubFolderTest\\TestFile.xml");
            bool for6 = result.EndsWith("FileSystemTests\\bin\\Debug\\net6.0\\SubFolderTest\\TestFile.xml");
            Assert.IsTrue(for6 || forNet472, "Does not have the full path." + result);
        }

        [TestMethod]
        public void HasExtensionTest_False_WithFullPath_Linux()
        {
            var pathToTest = "/github/workspace/FolderA/MyFile";

            PathIOLinux linux = new PathIOLinux();
            var result = linux.HasExtension(pathToTest);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HasExtensionTest_False_WithFullPath_Windows()
        {
            var pathToTest = "C:\\Skyline DataMiner\\ProtocolScripts\\DllImport\\newtonsoft.json\\13.0.2-beta3\\lib\\net45\\Newtonsoft";

            PathIOLinux linux = new PathIOLinux();
            var result = linux.HasExtension(pathToTest);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HasExtensionTest_True_WithFullPath_Linux()
        {
            var pathToTest = "/github/workspace/FolderA/MyFile.xml";

            PathIOLinux linux = new PathIOLinux();
            var result = linux.HasExtension(pathToTest);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HasExtensionTest_True_WithFullPath_Windows()
        {
            var pathToTest = "C:\\Skyline DataMiner\\ProtocolScripts\\DllImport\\newtonsoft.json\\13.0.2-beta3\\lib\\net45\\Newtonsoft.Json.dll";

            PathIOLinux linux = new PathIOLinux();
            var result = linux.HasExtension(pathToTest);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HasExtensionTest_True_WithRelativePath_Windows()
        {
            var pathToTest = "SubFolderTest\\TestFile.xml";

            PathIOLinux linux = new PathIOLinux();
            var result = linux.HasExtension(pathToTest);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPathRootedTest_False_WithFullPath_Windows()
        {
            var pathToTest = "ProtocolScripts\\DllImport\\newtonsoft.json\\13.0.2-beta3\\lib\\net45\\Newtonsoft";

            PathIOLinux linux = new PathIOLinux();
            var result = linux.IsPathRooted(pathToTest);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsPathRootedTest_True_WithFullPath_Linux()
        {
            var pathToTest = "/github/workspace/FolderA/MyFile";

            PathIOLinux linux = new PathIOLinux();
            var result = linux.IsPathRooted(pathToTest);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsPathRootedTest_True_WithFullPath_Windows()
        {
            var pathToTest = "C:\\Skyline DataMiner\\ProtocolScripts\\DllImport\\newtonsoft.json\\13.0.2-beta3\\lib\\net45\\Newtonsoft";

            PathIOLinux linux = new PathIOLinux();
            var result = linux.IsPathRooted(pathToTest);

            Assert.IsTrue(result);
        }

        // Valid
        [DataRow("NormalFile.ext", "NormalFile.ext")]
        [DataRow("File with spaces.ext", "File with spaces.ext")]
        [DataRow("File with spaces and a dot. ext", "File with spaces and a dot. ext")]
        [DataRow("File with ending dot.ext.", "File with ending dot.ext.")]
        [DataRow("File with ending space.ext ", "File with ending space.ext ")]
        [DataRow("File with ending space and dot.ext. ", "File with ending space and dot.ext. ")]
        // Invalid
        [DataRow("File with other invalid / chars.ext", "File with other invalid _ chars.ext")]
        [DataRow("File with other invalid \x00 chars.ext", "File with other invalid _ chars.ext")]
        [TestMethod]
        public void ReplaceInvalidCharsForFileNameTest(string input, string expectedOutput)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Assert.Inconclusive("Can't give accurate results on non-linux systems.");
                return;
            }

            // Arrange
            PathIOLinux path = new PathIOLinux();

            // Act
            var result = path.ReplaceInvalidCharsForFileName(input);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }

        // Valid
        [DataRow("NormalFile.ext", "NormalFile.ext")]
        [DataRow("File with spaces.ext", "File with spaces.ext")]
        [DataRow("File with spaces and a dot. ext", "File with spaces and a dot. ext")]
        // Invalid
        [DataRow("File with ending dot.ext.", "File with ending dot.ext")]
        [DataRow("File with ending space.ext ", "File with ending space.ext")]
        [DataRow("File with ending space and dot.ext. ", "File with ending space and dot.ext")]
        [DataRow("File with other invalid / chars.ext", "File with other invalid _ chars.ext")]
        [DataRow("File with other invalid \\ chars.ext", "File with other invalid _ chars.ext")]
        [DataRow("File with other invalid : chars.ext", "File with other invalid _ chars.ext")]
        [DataRow("File with other invalid | chars.ext", "File with other invalid _ chars.ext")]
        [DataRow("File with other invalid ? chars.ext", "File with other invalid _ chars.ext")]
        [DataRow("File with other invalid * chars.ext", "File with other invalid _ chars.ext")]
        [DataRow("File with other invalid \" chars.ext", "File with other invalid _ chars.ext")]
        [DataRow("File with other invalid < chars.ext", "File with other invalid _ chars.ext")]
        [DataRow("File with other invalid > chars.ext", "File with other invalid _ chars.ext")]
        [DataRow("File with other invalid \x00 chars.ext", "File with other invalid _ chars.ext")]
        [DataRow("File with other invalid \x1F chars.ext", "File with other invalid _ chars.ext")]
        [TestMethod]
        public void ReplaceInvalidCharsForFileNameTest_Windows(string input, string expectedOutput)
        {
            // Arrange
            PathIOWin path = new PathIOWin();

            // Act
            var result = path.ReplaceInvalidCharsForFileName(input, OSPlatform.Windows);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }
    }
}