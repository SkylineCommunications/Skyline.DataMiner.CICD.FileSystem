namespace Skyline.DataMiner.CICD.FileSystem
{
    using System;
    using System.Collections.Generic;

    using Skyline.DataMiner.CICD.FileSystem.DirectoryInfoWrapper;

    /// <summary>Exposes static methods for creating, moving, and enumerating through directories and subdirectories.</summary>
    public interface IDirectoryIO
    {
        /// <summary>
        /// Recursively copies all content of a folder to a different folder.
        /// </summary>
        /// <param name="sourceDirectory">The source directory to copy.</param>
        /// <param name="targetDirectory">The destination directory to copy</param>
        /// <param name="ignoreNamesWith">A list with filenames you want to ignore during the copy.</param>
        void CopyRecursive(string sourceDirectory, string targetDirectory, string[] ignoreNamesWith = null);

        /// <summary>
        /// Recursively copies all content of a folder located on a Share to a different folder.
        /// </summary>
        /// <param name="sourceDirectory">The source directory to copy.</param>
        /// <param name="targetDirectory">The destination directory to copy</param>
        /// <param name="ignoreNamesWith">A list with filenames you want to ignore during the copy.</param>
        void CopyRecursiveFromShare(string sourceDirectory, string targetDirectory, string[] ignoreNamesWith = null);

        /// <summary>
        /// Creates a new folder in the OS Temporary Files location.
        /// </summary>
        /// <returns>The created temporary directory.</returns>
        string CreateTemporaryDirectory();

        /// <summary>
        /// Deletes a directory if it exists. Won't do anything if the directory does not exist.
        /// <para>Deletes the recursive directories as well.</para>
        /// </summary>
        /// <param name="path">The path to the directory.</param>
        void DeleteDirectory(string path);

        /// <summary>Deletes an empty directory from a specified path.</summary>
        /// <exception cref="T:System.ArgumentException" />
        /// <exception cref="T:System.ArgumentNullException" />
        /// <exception cref="T:System.IO.DirectoryNotFoundException" />
        /// <exception cref="T:System.IO.IOException" />
        /// <exception cref="T:System.NotSupportedException" />
        /// <exception cref="T:System.UnauthorizedAccessException" />
        /// <param name="path">The name of the empty directory to remove. This directory must be writable and empty.</param>
        void Delete(string path);

        /// <summary>Deletes the specified directory and, if indicated, any subdirectories in the directory.</summary>
        /// <exception cref="T:System.ArgumentException" />
        /// <exception cref="T:System.ArgumentNullException" />
        /// <exception cref="T:System.IO.DirectoryNotFoundException" />
        /// <exception cref="T:System.IO.IOException" />
        /// <exception cref="T:System.NotSupportedException" />
        /// <exception cref="T:System.UnauthorizedAccessException" />
        /// <param name="path">The name of the directory to remove.</param>
        /// <param name="recursive"><c>true</c> to remove directories, subdirectories, and files in <paramref name="path" />. <c>false</c> otherwise.</param>
        void Delete(string path, bool recursive);

        /// <summary>Returns an enumerable collection of full file names in a specified path.</summary>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <exception cref="ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="IPathIO.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path" /> is <see langword="null" />.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">
        /// <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
        /// <exception cref="System.IO.IOException">
        /// <paramref name="path" /> is a file name.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path" />.</returns>
        IEnumerable<string> EnumerateFiles(string path);

        /// <summary>Returns an enumerable collection of full file names that match a search pattern in a specified path.</summary>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
        /// <exception cref="ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="IPathIO.GetInvalidPathChars" /> method.
        /// 
        /// -or-
        /// 
        /// <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
        /// <exception cref="ArgumentNullException">
        ///        <paramref name="path" /> is <see langword="null" />.
        /// 
        /// -or-
        /// 
        /// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">
        /// <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
        /// <exception cref="System.IO.IOException">
        /// <paramref name="path" /> is a file name.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path" /> and that match the specified search pattern.</returns>
        IEnumerable<string> EnumerateFiles(string path, string searchPattern);

        /// <summary>Returns an enumerable collection of full file names that match a search pattern in a specified path, and optionally searches subdirectories.</summary>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or should include all subdirectories. The default value is <see cref="System.IO.SearchOption.TopDirectoryOnly" />.</param>
        /// <exception cref="ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path" /> is a zero-length string, contains only white space, or contains invalid characters. You can query for invalid characters by using the <see cref="IPathIO.GetInvalidPathChars" /> method.
        /// 
        /// -or-
        /// 
        /// <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
        /// <exception cref="ArgumentNullException">
        ///        <paramref name="path" /> is <see langword="null" />.
        /// 
        /// -or-
        /// 
        /// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="searchOption" /> is not a valid <see cref="System.IO.SearchOption" /> value.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">
        /// <paramref name="path" /> is invalid, such as referring to an unmapped drive.</exception>
        /// <exception cref="System.IO.IOException">
        /// <paramref name="path" /> is a file name.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or combined exceed the system-defined maximum length.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path" /> and that match the specified search pattern and search option.</returns>
        IEnumerable<string> EnumerateFiles(string path, string searchPattern, System.IO.SearchOption searchOption);

        /// <summary>Determines whether the given path refers to an existing directory on disk.</summary>
        /// <param name="path">The path to test.</param>
        /// <returns>
        /// <see langword="true" /> if <paramref name="path" /> refers to an existing directory; <see langword="false" /> if the directory does not exist or an error occurs when trying to determine if the specified directory exists.</returns>
        bool Exists(string path);

        /// <summary>
        /// Enumerates all directories located inside of the provided directory.
        /// </summary>
        /// <param name="path">Path to the directory.</param>
        /// <returns>Enumeration of all directory paths inside the provided directory.</returns>
        IEnumerable<string> EnumerateDirectories(string path);

        /// <summary>
        /// Gets the parent directory of the provided directory.
        /// </summary>
        /// <param name="path">path to the directory.</param>
        /// <returns>The parent directory of the provided directory.</returns>
        string GetParentDirectory(string path);

        /// <summary>Returns the names of files (including their paths) in the specified directory.</summary>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <exception cref="System.IO.IOException">
        ///        <paramref name="path" /> is a file name.
        /// 
        /// -or-
        /// 
        /// A network error has occurred.</exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <exception cref="ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="IPathIO.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path" /> is <see langword="null" />.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is not found or is invalid (for example, it is on an unmapped drive).</exception>
        /// <returns>An array of the full names (including paths) for the files in the specified directory, or an empty array if no files are found.</returns>
        string[] GetFiles(string path);

        /// <summary>Returns the names of files (including their paths) that match the specified search pattern in the specified directory.</summary>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
        /// <exception cref="System.IO.IOException">
        ///        <paramref name="path" /> is a file name.
        /// 
        /// -or-
        /// 
        /// A network error has occurred.</exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <exception cref="ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using <see cref="IPathIO.GetInvalidPathChars" />.
        /// 
        /// -or-
        /// 
        /// <paramref name="searchPattern" /> doesn't contain a valid pattern.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path" /> or <paramref name="searchPattern" /> is <see langword="null" />.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is not found or is invalid (for example, it is on an unmapped drive).</exception>
        /// <returns>An array of the full names (including paths) for the files in the specified directory that match the specified search pattern, or an empty array if no files are found.</returns>
        string[] GetFiles(string path, string searchPattern);

        /// <summary>Returns the names of files (including their paths) that match the specified search pattern in the specified directory, using a value to determine whether to search subdirectories.</summary>
        /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
        /// <param name="searchPattern">The search string to match against the names of files in <paramref name="path" />.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include all subdirectories or only the current directory.</param>
        /// <exception cref="ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters with the <see cref="IPathIO.GetInvalidPathChars" /> method.
        /// 
        /// -or-
        /// 
        /// <paramref name="searchPattern" /> does not contain a valid pattern.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path" /> or <paramref name="searchPattern" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="searchOption" /> is not a valid <see cref="System.IO.SearchOption" /> value.</exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is not found or is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="System.IO.IOException">
        ///        <paramref name="path" /> is a file name.
        /// 
        /// -or-
        /// 
        /// A network error has occurred.</exception>
        /// <returns>An array of the full names (including paths) for the files in the specified directory that match the specified search pattern and option, or an empty array if no files are found.</returns>
        string[] GetFiles(string path, string searchPattern, System.IO.SearchOption searchOption);

        // TODO: MOVE THIS TO SYSDEV SOLUTION
        // string LocalSvnPathToSvnServer(string localFolderPath);

        /// <summary>
        /// Move the content of a directory to a different directory.
        /// </summary>
        /// <param name="source">The source directory to move its content from.</param>
        /// <param name="target">The target directory to place the content in.</param>
        /// <param name="move">Whether to move or copy.</param>
        /// <param name="forceReplace">Forces replacing existing files.</param>
        void MoveDirectory(string source, string target, bool move = true, bool forceReplace = false);

        /// <summary>
        /// Unzips a compressed directory.
        /// </summary>
        /// <param name="zipPath">Path to a compressed folder.</param>
        /// <param name="destinationDir">Path to a destination for unzipping the contents into.</param>
        void Unzip(string zipPath, string destinationDir);

        /// <summary>
        /// Check if the provided path is a directory.
        /// </summary>
        /// <param name="path">The path to check.</param>
        /// <returns>True if the provided path is a directory.</returns>
        bool IsDirectory(string path);

        /// <summary>
        /// Allow writes to be performed on the directory and its contents.
        /// </summary>
        /// <param name="path">Path to the directory</param>
        void AllowWritesOnDirectory(string path);

        /// <summary>
        /// Allow writes to be performed on the directory and its contents.
        /// </summary>
        /// <param name="path">Path to the directory</param>
        bool TryAllowWritesOnDirectory(string path);

        /// <summary>Creates all directories and subdirectories in the specified path unless they already exist.</summary>
        /// <param name="path">The directory to create.</param>
        /// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path" /> is a file.
        /// 
        /// -or-
        /// 
        /// The network name is not known.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <exception cref="T:System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path" /> is a zero-length string, contains only white space, or contains one or more invalid characters. You can query for invalid characters by using the <see cref="M:System.IO.Path.GetInvalidPathChars" /> method.
        /// 
        /// -or-
        /// 
        /// <paramref name="path" /> is prefixed with, or contains, only a colon character (:).</exception>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="path" /> is <see langword="null" />.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="T:System.NotSupportedException">
        /// <paramref name="path" /> contains a colon character (:) that is not part of a drive label ("C:\").</exception>
        /// <returns>An object that represents the directory at the specified path. This object is returned regardless of whether a directory at the specified path already exists.</returns>
        IDirectoryInfoIO CreateDirectory(string path);
    }
}