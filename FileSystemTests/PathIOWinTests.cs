namespace Skyline.DataMiner.CICD.FileSystem.Tests
{
    using System.Runtime.InteropServices;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PathIOWinTests
    {
        [DataRow("NormalFile.ext", "NormalFile.ext")]
        [DataRow("File with spaces.ext", "File with spaces.ext")]
        [DataRow("File with spaces and a dot. ext", "File with spaces and a dot. ext")]
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
        [TestMethod]
        public void ReplaceInvalidCharsForFileNameTest(string input, string expectedOutput)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Assert.Inconclusive("Can't give accurate results on non-windows systems.");
                return;
            }

            // Arrange
            PathIOWin path = new PathIOWin();

            // Act
            var result = path.ReplaceInvalidCharsForFileName(input);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }
    }
}