﻿namespace Skyline.DataMiner.CICD.FileSystem
{
    using System.Text;

    /// <summary>Provides static methods for the creation, copying, deletion, moving, and opening of a single file, and aids in the creation of <see cref="System.IO.FileStream" /> objects.</summary>
    public interface IFileIO
    {
        /// <summary>
        /// Appends the specified string to the file, creating the file if it does not already exist.
        /// </summary>
        /// <param name="filePath">The file to append the specified string to.</param>
        /// <param name="fileContent">The string to append to the file.</param>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        /// <exception cref="System.IO.IOException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        /// <exception cref="System.IO.DirectoryNotFoundException"></exception>
        /// <exception cref="System.UnauthorizedAccessException"></exception>
        /// <exception cref="System.PlatformNotSupportedException"></exception>
        void AppendAllText(string filePath, string fileContent);

        /// <summary>
        /// Deletes the specified file. Won't fail if the location does not exist.
        /// </summary>
        /// <param name="path">Will delete a file.</param>
        void DeleteFile(string path);

        /// <summary>Deletes the specified file.</summary>
        /// <param name="path">The name of the file to be deleted. Wildcard characters are not supported.</param>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path" /> is <see langword="null" />.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="T:System.IO.IOException">The specified file is in use.
        /// -or-
        /// There is an open handle on the file, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
        /// <exception cref="T:System.NotSupportedException">
        /// <paramref name="path" /> is in an invalid format.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.
        /// -or-
        /// The file is an executable file that is in use.
        /// -or-
        /// <paramref name="path" /> is a directory.
        /// -or-
        /// <paramref name="path" /> specified a read-only file.</exception>
        void Delete(string path);

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

        /// <summary>Moves a specified file to a new location, providing the option to specify a new file name.</summary>
        /// <param name="sourceFileName">The name of the file to move. Can include a relative or absolute path.</param>
        /// <param name="destFileName">The new path and name for the file.</param>
        /// <exception cref="T:System.IO.IOException">The destination file already exists.
        /// -or-
        /// <paramref name="sourceFileName" /> was not found.</exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is <see langword="null" />.</exception>
        /// <exception cref="T:System.ArgumentException">
        /// <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is a zero-length string, contains only white space, or contains invalid characters as defined in <see cref="F:System.IO.Path.InvalidPathChars" />.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The path specified in <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is invalid, (for example, it is on an unmapped drive).</exception>
        /// <exception cref="T:System.NotSupportedException">
        /// <paramref name="sourceFileName" /> or <paramref name="destFileName" /> is in an invalid format.</exception>
        void Move(string sourceFileName, string destFileName);

        /// <summary>
        /// Opens a binary file, reads the contents of the file into a byte array, and then closes the file.
        /// </summary>
        /// <param name="filePath">Path to the file.</param>
        /// <returns>A byte array containing the contents of the file.</returns>
        byte[] ReadAllBytes(string filePath);

        /// <summary>
        /// Creates a new file, writes the specified byte array to the file, and then closes the file. If the target file already exists, it is overwritten.
        /// </summary>
        /// <param name="filePath">Path to the file.</param>
        /// <param name="bytes">The bytes to write to the file.</param>
        void WriteAllBytes(string filePath, byte[] bytes);

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

        /// <summary>
        /// Copies an existing file to a new file.
        /// An exception is raised if the destination file already exists.
        /// </summary>
        /// <exception cref="T:System.ArgumentException" />
        /// <exception cref="T:System.ArgumentNullException" />
        /// <exception cref="T:System.IO.DirectoryNotFoundException" />
        /// <exception cref="T:System.IO.FileNotFoundException" />
        /// <exception cref="T:System.IO.IOException" />
        /// <exception cref="T:System.NotSupportedException" />
        /// <exception cref="T:System.UnauthorizedAccessException" />
        /// <param name="sourcePath">The file to copy.</param>
        /// <param name="destinationPath">The name of the destination file. This cannot be a directory or an existing file.</param>
        void Copy(string sourcePath, string destinationPath);

        /// <summary>
        /// Copies an existing file to a new file.
        /// If <paramref name="overwrite"/> is false, an exception will be
        /// raised if the destination exists. Otherwise, it will be overwritten.
        /// </summary>
        /// <exception cref="T:System.ArgumentException" />
        /// <exception cref="T:System.ArgumentNullException" />
        /// <exception cref="T:System.IO.DirectoryNotFoundException" />
        /// <exception cref="T:System.IO.FileNotFoundException" />
        /// <exception cref="T:System.IO.IOException" />
        /// <exception cref="T:System.NotSupportedException" />
        /// <exception cref="T:System.UnauthorizedAccessException" />
        /// <param name="sourcePath">The file to copy. </param>
        /// <param name="destinationPath">The name of the destination file. This cannot be a directory.</param>
        /// <param name="overwrite"><c>true</c> if the destination file should ignore the read-only and hidden attributes and overwrite; otherwise, <c>false</c>.</param>
        void Copy(string sourcePath, string destinationPath, bool overwrite);
        
        /// <summary>Opens a <see cref="T:System.IO.FileStream" /> on the specified path with read/write access.</summary>
        /// <param name="path">The file to open.</param>
        /// <param name="mode">A <see cref="T:System.IO.FileMode" /> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten.</param>
        /// <returns>A <see cref="T:System.IO.FileStream" /> opened in the specified mode and path, with read/write access and not shared.</returns>
        System.IO.FileStream Open(string path, System.IO.FileMode mode);

        /// <summary>Opens a <see cref="T:System.IO.FileStream" /> on the specified path, with the specified mode and access.</summary>
        /// <param name="path">The file to open.</param>
        /// <param name="mode">A <see cref="T:System.IO.FileMode" /> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten.</param>
        /// <param name="access">A <see cref="T:System.IO.FileAccess" /> value that specifies the operations that can be performed on the file.</param>
        /// <returns>An unshared <see cref="T:System.IO.FileStream" /> that provides access to the specified file, with the specified mode and access.</returns>
        System.IO.FileStream Open(string path, System.IO.FileMode mode, System.IO.FileAccess access);

        /// <summary>Opens a <see cref="T:System.IO.FileStream" /> on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.</summary>
        /// <param name="path">The file to open.</param>
        /// <param name="mode">A <see cref="T:System.IO.FileMode" /> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten.</param>
        /// <param name="access">A <see cref="T:System.IO.FileAccess" /> value that specifies the operations that can be performed on the file.</param>
        /// <param name="share">A <see cref="T:System.IO.FileShare" /> value specifying the type of access other threads have to the file.</param>
        /// <returns>A <see cref="T:System.IO.FileStream" /> on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.</returns>
        System.IO.FileStream Open(string path, System.IO.FileMode mode, System.IO.FileAccess access, System.IO.FileShare share);

        /// <summary>Gets the <see cref="T:System.IO.FileAttributes" /> of the file on the path.</summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>The <see cref="T:System.IO.FileAttributes" /> of the file on the path.</returns>
        System.IO.FileAttributes GetAttributes(string path);
    }
}