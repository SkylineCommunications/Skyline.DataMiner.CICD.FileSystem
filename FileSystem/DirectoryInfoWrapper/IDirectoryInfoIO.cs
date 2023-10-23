namespace Skyline.DataMiner.CICD.FileSystem.DirectoryInfoWrapper
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using Skyline.DataMiner.CICD.FileSystem.FileInfoWrapper;
    using Skyline.DataMiner.CICD.FileSystem.FileSystemInfoWrapper;

    /// <summary>Exposes instance methods for creating, moving, and enumerating through directories and subdirectories.</summary>
    public interface IDirectoryInfoIO : IFileSystemInfoIO
    {
        /// <summary>Gets a value indicating whether the directory exists.</summary>
        /// <returns>
        /// <see langword="true" /> if the directory exists; otherwise, <see langword="false" />.</returns>
        bool Exists { get; }

        /// <summary>Gets the name of this <see cref="IDirectoryInfoIO" /> instance.</summary>
        /// <returns>The directory name.</returns>
        string Name { get; }

        /// <summary>Gets the full path of the directory</summary>
        /// <exception cref="System.IO.PathTooLongException">The fully qualified path and file name exceed the system-defined maximum length.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>A string containing the full path.</returns>
        string FullName { get; }

        /// <summary>Gets the parent directory of a specified subdirectory.</summary>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>The parent directory, or <see langword="null" /> if the path is null or if the file path denotes a root (such as \, C:\, or \\server\share).</returns>
        IDirectoryInfoIO Parent { get; }

        /// <summary>Gets the root portion of the directory.</summary>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>An object that represents the root of the directory.</returns>
        IDirectoryInfoIO Root { get; }

        /// <summary>Creates a directory.</summary>
        /// <exception cref="System.IO.IOException">The directory cannot be created.</exception>
        void Create();

        /// <summary>Creates a subdirectory or subdirectories on the specified path. The specified path can be relative to this instance of the <see cref="IDirectoryInfoIO" /> class.</summary>
        /// <param name="path">The specified path. This cannot be a different disk volume or Universal Naming Convention (UNC) name.</param>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="path" /> does not specify a valid file path or contains invalid <see cref="IDirectoryInfoIO" /> characters.</exception>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="path" /> is <see langword="null" />.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="System.IO.IOException">The subdirectory cannot be created.
        /// 
        /// -or-
        /// 
        /// A file or directory already has the name specified by <paramref name="path" />.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have code access permission to create the directory.
        /// 
        /// -or-
        /// 
        /// The caller does not have code access permission to read the directory described by the returned <see cref="IDirectoryInfoIO" /> object.  This can occur when the <paramref name="path" /> parameter describes an existing directory.</exception>
        /// <exception cref="System.NotSupportedException">
        /// <paramref name="path" /> contains a colon character (:) that is not part of a drive label ("C:\").</exception>
        /// <returns>The last directory specified in <paramref name="path" />.</returns>
        IDirectoryInfoIO CreateSubdirectory(string path);

        /// <summary>Deletes this <see cref="IDirectoryInfoIO" /> if it is empty.</summary>
        /// <exception cref="System.UnauthorizedAccessException">The directory contains a read-only file.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The directory described by this <see cref="IDirectoryInfoIO" /> object does not exist or could not be found.</exception>
        /// <exception cref="System.IO.IOException">The directory is not empty.
        /// 
        /// -or-
        /// 
        /// The directory is the application's current working directory.
        /// 
        /// -or-
        /// 
        /// There is an open handle on the directory, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories. For more information, see How to: Enumerate Directories and Files.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        void Delete();

        /// <summary>Deletes this instance of a <see cref="IDirectoryInfoIO" />, specifying whether to delete subdirectories and files.</summary>
        /// <param name="recursive">
        /// <see langword="true" /> to delete this directory, its subdirectories, and all files; otherwise, <see langword="false" />.</param>
        /// <exception cref="System.UnauthorizedAccessException">The directory contains a read-only file.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The directory described by this <see cref="IDirectoryInfoIO" /> object does not exist or could not be found.</exception>
        /// <exception cref="System.IO.IOException">The directory is read-only.
        /// 
        /// -or-
        /// 
        /// The directory contains one or more files or subdirectories and <paramref name="recursive" /> is <see langword="false" />.
        /// 
        /// -or-
        /// 
        /// The directory is the application's current working directory.
        /// 
        /// -or-
        /// 
        /// There is an open handle on the directory or on one of its files, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        void Delete(bool recursive);

        /// <summary>Returns an enumerable collection of directory information in the current directory.</summary>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="IDirectoryInfoIO" /> object is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>An enumerable collection of directories in the current directory.</returns>
        IEnumerable<IDirectoryInfoIO> EnumerateDirectories();

        /// <summary>Returns an enumerable collection of directory information that matches a specified search pattern.</summary>
        /// <param name="searchPattern">The search string to match against the names of directories.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="IDirectoryInfoIO" /> object is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>An enumerable collection of directories that matches <paramref name="searchPattern" />.</returns>
        IEnumerable<IDirectoryInfoIO> EnumerateDirectories(string searchPattern);

        /// <summary>Returns an enumerable collection of directory information that matches a specified search pattern and search subdirectory option.</summary>
        /// <param name="searchPattern">The search string to match against the names of directories.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories. The default value is <see cref="System.IO.SearchOption.TopDirectoryOnly" />.</param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="searchOption" /> is not a valid <see cref="System.IO.SearchOption" /> value.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="IDirectoryInfoIO" /> object is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>An enumerable collection of directories that matches <paramref name="searchPattern" /> and <paramref name="searchOption" />.</returns>
        IEnumerable<IDirectoryInfoIO> EnumerateDirectories(string searchPattern, System.IO.SearchOption searchOption);

        /// <summary>Returns an enumerable collection of file information in the current directory.</summary>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="IDirectoryInfoIO" /> object is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>An enumerable collection of the files in the current directory.</returns>
        IEnumerable<IFileInfoIO> EnumerateFiles();

        /// <summary>Returns an enumerable collection of file information that matches a search pattern.</summary>
        /// <param name="searchPattern">The search string to match against the names of files.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="IDirectoryInfoIO" /> object is invalid, (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>An enumerable collection of files that matches <paramref name="searchPattern" />.</returns>
        IEnumerable<IFileInfoIO> EnumerateFiles(string searchPattern);

        /// <summary>Returns an enumerable collection of file information that matches a specified search pattern and search subdirectory option.</summary>
        /// <param name="searchPattern">The search string to match against the names of files.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories. The default value is <see cref="System.IO.SearchOption.TopDirectoryOnly" />.</param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="searchOption" /> is not a valid <see cref="System.IO.SearchOption" /> value.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="IDirectoryInfoIO" /> object is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>An enumerable collection of files that matches <paramref name="searchPattern" /> and <paramref name="searchOption" />.</returns>
        IEnumerable<IFileInfoIO> EnumerateFiles(string searchPattern, System.IO.SearchOption searchOption);

        /// <summary>Returns the subdirectories of the current directory.</summary>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="IDirectoryInfoIO" /> object is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <returns>An array of <see cref="IDirectoryInfoIO" /> objects.</returns>
        IDirectoryInfoIO[] GetDirectories();

        /// <summary>Returns an array of directories in the current <see cref="IDirectoryInfoIO" /> matching the given search criteria.</summary>
        /// <param name="searchPattern">The search string to match against the names of directories.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
        /// <exception cref="System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="searchPattern" /> contains one or more invalid characters defined by the <see cref="IPathIO.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="IDirectoryInfoIO" /> object is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <returns>An array of type <see cref="IDirectoryInfoIO" /> matching <paramref name="searchPattern" />.</returns>
        IDirectoryInfoIO[] GetDirectories(string searchPattern);

        /// <summary>Returns an array of directories in the current <see cref="IDirectoryInfoIO" /> matching the given search criteria and using a value to determine whether to search subdirectories.</summary>
        /// <param name="searchPattern">The search string to match against the names of directories.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories.</param>
        /// <exception cref="System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="searchPattern" /> contains one or more invalid characters defined by the <see cref="IPathIO.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="searchOption" /> is not a valid <see cref="System.IO.SearchOption" /> value.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="IDirectoryInfoIO" /> object is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <returns>An array of type <see cref="IDirectoryInfoIO" /> matching <paramref name="searchPattern" />.</returns>
        IDirectoryInfoIO[] GetDirectories(string searchPattern, System.IO.SearchOption searchOption);

        /// <summary>Returns a file list from the current directory.</summary>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path is invalid, such as being on an unmapped drive.</exception>
        /// <returns>An array of type <see cref="IFileInfoIO" />.</returns>
        IFileInfoIO[] GetFiles();

        /// <summary>Returns a file list from the current directory matching the given search pattern.</summary>
        /// <param name="searchPattern">The search string to match against the names of files.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
        /// <exception cref="System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="searchPattern" /> contains one or more invalid characters defined by the <see cref="IPathIO.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>An array of type <see cref="IFileInfoIO" />.</returns>
        IFileInfoIO[] GetFiles(string searchPattern);

        /// <summary>Returns a file list from the current directory matching the given search pattern and using a value to determine whether to search subdirectories.</summary>
        /// <param name="searchPattern">The search string to match against the names of files.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories.</param>
        /// <exception cref="System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="searchPattern" /> contains one or more invalid characters defined by the <see cref="IPathIO.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <paramref name="searchOption" /> is not a valid <see cref="System.IO.SearchOption" /> value.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>An array of type <see cref="IFileInfoIO" />.</returns>
        IFileInfoIO[] GetFiles(string searchPattern, System.IO.SearchOption searchOption);

        /// <summary>Sets the <see cref="SerializationInfo" /> object with the file name and additional exception information.</summary>
        /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
        void GetObjectData(SerializationInfo info, StreamingContext context);

        /// <summary>Moves a <see cref="IDirectoryInfoIO" /> instance and its contents to a new path.</summary>
        /// <param name="destDirName">The name and path to which to move this directory. The destination cannot be another disk volume or a directory with the identical name. It can be an existing directory to which you want to add this directory as a subdirectory.</param>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="destDirName" /> is <see langword="null" />.</exception>
        /// <exception cref="System.ArgumentException">
        /// <paramref name="destDirName" /> is an empty string (''").</exception>
        /// <exception cref="System.IO.IOException">An attempt was made to move a directory to a different volume.
        /// 
        /// -or-
        /// 
        /// <paramref name="destDirName" /> already exists.
        /// 
        /// -or-
        /// 
        /// You are not authorized to access this path.
        /// 
        /// -or-
        /// 
        /// The directory being moved and the destination directory have the same name.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The destination directory cannot be found.</exception>
        void MoveTo(string destDirName);

        /// <summary>Returns the original path that was passed to the <see cref="IDirectoryInfoIO" /> constructor. Use the <see cref="FullName" /> or <see cref="Name" /> properties for the full path or file/directory name instead of this method.</summary>
        /// <returns>The original path that was passed by the user.</returns>
        string ToString();
    }
}