namespace Skyline.DataMiner.CICD.FileSystem.Tests
{
    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class OsSpecificHelperTests
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
        public void ReplaceInvalidCharsForFileNameForWindowsTest(string input, string expectedOutput)
        {
            // Arrange
            OsSpecificHelper path = new OsSpecificHelper();

            // Act
            var result = path.ReplaceInvalidCharsForFileNameForWindows(input);

            // Assert
            result.Should().BeEquivalentTo(expectedOutput);
        }
    }
}