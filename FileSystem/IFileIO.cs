namespace Skyline.DataMiner.CICD.FileSystem
{
    using System.Text;

    /// <summary>Provides static methods for the creation, copying, deletion, moving, and opening of a single file, and aids in the creation of <see cref="System.IO.FileStream" /> objects.</summary>
    public interface IFileIO
    {
        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        /// <param name="path">Will delete a file.</param>
        void DeleteFile(string path);

        /// <summary>Determines whether the specified file exists.</summary>
        /// <param name="path">The file to check.</param>
        /// <returns>
        /// <see langword="true" /> if the caller has the required permissions and <paramref name="path" /> contains the name of an existing file; otherwise, <see langword="false" />. This method also returns <see langword="false" /> if <paramref name="path" /> is <see langword="null" />, an invalid path, or a zero-length string. If the caller does not have sufficient permissions to read the specified file, no exception is thrown and the method returns <see langword="false" /> regardless of the existence of <paramref name="path" />.</returns>
        bool Exists(string path);

        /// <summary>
        /// Gets the length of the file.
        /// </summary>
        /// <param name="fileName">The path to the file.</param>
        /// <returns>The length of the file content.</returns>
        long GetFileLength(string fileName);

        /// <summary>
        /// Finds the file product version.
        /// </summary>
        /// <param name="filePath">The file to check.</param>
        /// <returns>The product version of the file.</returns>
        string GetFileProductVersion(string filePath);

        /// <summary>
        /// Finds the file version.
        /// </summary>
        /// <param name="filePath">The file to check.</param>
        /// <returns>The version of the file.</returns>
        string GetFileVersion(string filePath);

        /// <summary>
        /// Gets the full path of the file.
        /// </summary>
        /// <param name="relativePath">A possibly relative path to a file.</param>
        /// <returns>The full path of the file.</returns>
        string GetFullPath(string relativePath);

        /// <summary>
        /// Gets the directory name of the file.
        /// </summary>
        /// <param name="relativePath">The path to the file.</param>
        /// <returns>The directory name/</returns>
        string GetParentDirectory(string relativePath);

        /// <summary>
        /// Moves a file from a source location to a target location.
        /// </summary>
        /// <param name="filePath">The full path of the file.</param>
        /// <param name="sourceFolder">The parent folder of the file.</param>
        /// <param name="targetFolder">The target folder.</param>
        /// <param name="forceReplace">Forces a replacement in case the file exists in the target folder.</param>
        void MoveFile(string filePath, string sourceFolder, string targetFolder, bool forceReplace);

        /// <summary>
        /// Opens a binary file, reads the contents of the file into a byte array, and then closes the file.
        /// </summary>
        /// <param name="filePath">Path to the file.</param>
        /// <returns>A byte array containing the contents of the file.</returns>
        byte[] ReadAllBytes(string filePath);

        /// <summary>
        /// Reads all text in a file. If file does not exist it will return an empty string.
        /// </summary>
        /// <param name="filePath">Path to the file.</param>
        /// <returns>All Read text.</returns>
        string ReadAllText(string filePath);

        /// <summary>
        /// Reads all text in a file. If file does not exist it will return an empty string.
        /// </summary>
        /// <param name="filePath">Path to the file.</param>
        /// <param name="encoding">Encoding to use during reading.</param>
        /// <returns></returns>
        string ReadAllText(string filePath, Encoding encoding);

        /// <summary>
        /// Creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.
        /// </summary>
        /// <param name="filePath">Path to the file.</param>
        /// <param name="fileContent">Content for the file.</param>
        void WriteAllText(string filePath, string fileContent);

        /// <summary>Creates or overwrites a file in the specified path.</summary>
        /// <param name="path">The path and name of the file to create.</param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.
        /// 
        /// -or-
        /// 
        /// <paramref name="path" /> specified a file that is read-only.
        /// 
        /// -or-
        /// 
        /// <paramref name="path" /> specified a file that is hidden.</exception>
        /// <exception cref="T:System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path" /> is <see langword="null" />.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while creating the file.</exception>
        /// <exception cref="T:System.NotSupportedException">
        /// <paramref name="path" /> is in an invalid format.</exception>
        /// <returns>A <see cref="T:System.IO.FileStream" /> that provides read/write access to the file specified in <paramref name="path" />.</returns>
        System.IO.FileStream Create(string path);

        /// <summary>Creates or overwrites a file in the specified path, specifying a buffer size.</summary>
        /// <param name="path">The path and name of the file to create.</param>
        /// <param name="bufferSize">The number of bytes buffered for reads and writes to the file.</param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.
        /// 
        /// -or-
        /// 
        /// <paramref name="path" /> specified a file that is read-only.
        /// 
        /// -or-
        /// 
        /// <paramref name="path" /> specified a file that is hidden.</exception>
        /// <exception cref="T:System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path" /> is <see langword="null" />.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while creating the file.</exception>
        /// <exception cref="T:System.NotSupportedException">
        /// <paramref name="path" /> is in an invalid format.</exception>
        /// <returns>A <see cref="T:System.IO.FileStream" /> with the specified buffer size that provides read/write access to the file specified in <paramref name="path" />.</returns>
        System.IO.FileStream Create(string path, int bufferSize);

        /// <summary>Creates or overwrites a file in the specified path, specifying a buffer size and options that describe how to create or overwrite the file.</summary>
        /// <param name="path">The path and name of the file to create.</param>
        /// <param name="bufferSize">The number of bytes buffered for reads and writes to the file.</param>
        /// <param name="options">One of the <see cref="T:System.IO.FileOptions" /> values that describes how to create or overwrite the file.</param>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.
        /// 
        /// -or-
        /// 
        /// <paramref name="path" /> specified a file that is read-only.
        /// 
        /// -or-
        /// 
        /// <paramref name="path" /> specified a file that is hidden.</exception>
        /// <exception cref="T:System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path" /> is <see langword="null" />.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive.</exception>
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while creating the file.</exception>
        /// <exception cref="T:System.NotSupportedException">
        /// <paramref name="path" /> is in an invalid format.</exception>
        /// <returns>A new file with the specified buffer size.</returns>
        System.IO.FileStream Create(string path, int bufferSize, System.IO.FileOptions options);
    }
}