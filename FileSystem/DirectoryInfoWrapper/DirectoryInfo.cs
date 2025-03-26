namespace Skyline.DataMiner.CICD.FileSystem.DirectoryInfoWrapper
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;

    using Skyline.DataMiner.CICD.FileSystem.FileInfoWrapper;

    /// <summary>Exposes instance methods for creating, moving, and enumerating through directories and subdirectories.</summary>
    public sealed class DirectoryInfo : IDirectoryInfoIO
    {
        private readonly IDirectoryInfoIO _directoryInfo;

        /// <summary>Initializes a new instance of the <see cref="DirectoryInfo" /> class on the specified path.</summary>
        /// <param name="path">A string specifying the path on which to create the <see cref="DirectoryInfo" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path" /> is <see langword="null" />.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path" /> contains invalid characters such as ", &lt;, &gt;, or |.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        public DirectoryInfo(string path)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _directoryInfo = new DirectoryInfoIOWin(path);
            }
            else
            {
                _directoryInfo = new DirectoryInfoIOLinux(path);
            }
        }

        /// <summary>Initializes a new instance of the <see cref="DirectoryInfo" /> class on the specified path.</summary>
        /// <param name="directoryInfo">A <see cref="System.IO.DirectoryInfo"/> to convert to the <see cref="IDirectoryInfoIO"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="directoryInfo" /> is <see langword="null" />.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="directoryInfo" /> contains invalid characters such as ", &lt;, &gt;, or |.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        public DirectoryInfo(System.IO.DirectoryInfo directoryInfo)
        {
            if (directoryInfo is null)
            {
                throw new ArgumentNullException(nameof(directoryInfo));
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _directoryInfo = new DirectoryInfoIOWin(directoryInfo);
            }
            else
            {
                _directoryInfo = new DirectoryInfoIOLinux(directoryInfo);
            }
        }

        /// <summary>Gets the name of this <see cref="DirectoryInfo" /> instance.</summary>
        /// <returns>The directory name.</returns>
        public string Name => _directoryInfo.Name;

        /// <summary>Gets a value indicating whether the directory exists.</summary>
        /// <returns>
        /// <see langword="true" /> if the directory exists; otherwise, <see langword="false" />.</returns>
        public bool Exists => _directoryInfo.Exists;

        /// <summary>Gets the parent directory of a specified subdirectory.</summary>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>The parent directory, or <see langword="null" /> if the path is null or if the file path denotes a root (such as \, C:\, or \\server\share).</returns>
        public IDirectoryInfoIO Parent => _directoryInfo.Parent;

        /// <summary>Gets the root portion of the directory.</summary>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>An object that represents the root of the directory.</returns>
        public IDirectoryInfoIO Root => _directoryInfo.Root;

        /// <summary>Creates a directory.</summary>
        /// <exception cref="System.IO.IOException">The directory cannot be created.</exception>
        public void Create() => _directoryInfo.Create();

        /// <summary>Creates a subdirectory or subdirectories on the specified path. The specified path can be relative to this instance of the <see cref="DirectoryInfo" /> class.</summary>
        /// <param name="path">The specified path. This cannot be a different disk volume or Universal Naming Convention (UNC) name.</param>
        /// <exception cref="ArgumentException">
        /// <paramref name="path" /> does not specify a valid file path or contains invalid <see cref="DirectoryInfo" /> characters.</exception>
        /// <exception cref="ArgumentNullException">
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
        /// The caller does not have code access permission to read the directory described by the returned <see cref="DirectoryInfo" /> object.  This can occur when the <paramref name="path" /> parameter describes an existing directory.</exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="path" /> contains a colon character (:) that is not part of a drive label ("C:\").</exception>
        /// <returns>The last directory specified in <paramref name="path" />.</returns>
        public IDirectoryInfoIO CreateSubdirectory(string path) => _directoryInfo.CreateSubdirectory(path);

        /// <summary>Deletes this <see cref="DirectoryInfo" /> if it is empty.</summary>
        /// <exception cref="UnauthorizedAccessException">The directory contains a read-only file.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The directory described by this <see cref="DirectoryInfo" /> object does not exist or could not be found.</exception>
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
        public void Delete() => _directoryInfo.Delete();

        /// <summary>Deletes this instance of a <see cref="DirectoryInfo" />, specifying whether to delete subdirectories and files.</summary>
        /// <param name="recursive">
        /// <see langword="true" /> to delete this directory, its subdirectories, and all files; otherwise, <see langword="false" />.</param>
        /// <exception cref="UnauthorizedAccessException">The directory contains a read-only file.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The directory described by this <see cref="DirectoryInfo" /> object does not exist or could not be found.</exception>
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
        public void Delete(bool recursive) => _directoryInfo.Delete(recursive);

        /// <summary>Returns an enumerable collection of directory information in the current directory.</summary>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>An enumerable collection of directories in the current directory.</returns>
        public IEnumerable<IDirectoryInfoIO> EnumerateDirectories() => _directoryInfo.EnumerateDirectories();

        /// <summary>Returns an enumerable collection of directory information that matches a specified search pattern.</summary>
        /// <param name="searchPattern">The search string to match against the names of directories.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>An enumerable collection of directories that matches <paramref name="searchPattern" />.</returns>
        public IEnumerable<IDirectoryInfoIO> EnumerateDirectories(string searchPattern) => _directoryInfo.EnumerateDirectories(searchPattern);

        /// <summary>Returns an enumerable collection of directory information that matches a specified search pattern and search subdirectory option.</summary>
        /// <param name="searchPattern">The search string to match against the names of directories.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories. The default value is <see cref="System.IO.SearchOption.TopDirectoryOnly" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="searchOption" /> is not a valid <see cref="System.IO.SearchOption" /> value.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>An enumerable collection of directories that matches <paramref name="searchPattern" /> and <paramref name="searchOption" />.</returns>
        public IEnumerable<IDirectoryInfoIO> EnumerateDirectories(string searchPattern, System.IO.SearchOption searchOption) => _directoryInfo.EnumerateDirectories(searchPattern, searchOption);

        /// <summary>Returns an enumerable collection of file information in the current directory.</summary>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>An enumerable collection of the files in the current directory.</returns>
        public IEnumerable<IFileInfoIO> EnumerateFiles() => _directoryInfo.EnumerateFiles();

        /// <summary>Returns an enumerable collection of file information that matches a search pattern.</summary>
        /// <param name="searchPattern">The search string to match against the names of files.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="DirectoryInfo" /> object is invalid, (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>An enumerable collection of files that matches <paramref name="searchPattern" />.</returns>
        public IEnumerable<IFileInfoIO> EnumerateFiles(string searchPattern) => _directoryInfo.EnumerateFiles(searchPattern);

        /// <summary>Returns an enumerable collection of file information that matches a specified search pattern and search subdirectory option.</summary>
        /// <param name="searchPattern">The search string to match against the names of files.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories. The default value is <see cref="System.IO.SearchOption.TopDirectoryOnly" />.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="searchOption" /> is not a valid <see cref="System.IO.SearchOption" /> value.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>An enumerable collection of files that matches <paramref name="searchPattern" /> and <paramref name="searchOption" />.</returns>
        public IEnumerable<IFileInfoIO> EnumerateFiles(string searchPattern, System.IO.SearchOption searchOption) => _directoryInfo.EnumerateFiles(searchPattern, searchOption);

        /// <summary>Returns an array of directories in the current <see cref="DirectoryInfo" /> matching the given search criteria and using a value to determine whether to search subdirectories.</summary>
        /// <param name="searchPattern">The search string to match against the names of directories.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories.</param>
        /// <exception cref="ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="searchPattern" /> contains one or more invalid characters defined by the <see cref="IPathIO.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="searchOption" /> is not a valid <see cref="System.IO.SearchOption" /> value.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <returns>An array of type <see cref="IDirectoryInfoIO" /> matching <paramref name="searchPattern" />.</returns>
        public IDirectoryInfoIO[] GetDirectories(string searchPattern, System.IO.SearchOption searchOption) => _directoryInfo.GetDirectories(searchPattern, searchOption);

        /// <summary>Returns an array of directories in the current <see cref="DirectoryInfo" /> matching the given search criteria.</summary>
        /// <param name="searchPattern">The search string to match against the names of directories.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
        /// <exception cref="ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="searchPattern" /> contains one or more invalid characters defined by the <see cref="IPathIO.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="DirectoryInfo" /> object is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <returns>An array of type <see cref="IDirectoryInfoIO" /> matching <paramref name="searchPattern" />.</returns>
        public IDirectoryInfoIO[] GetDirectories(string searchPattern) => _directoryInfo.GetDirectories(searchPattern);

        /// <summary>Returns the subdirectories of the current directory.</summary>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path encapsulated in the <see cref="DirectoryInfo" /> object is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <returns>An array of <see cref="IDirectoryInfoIO" /> objects.</returns>
        public IDirectoryInfoIO[] GetDirectories() => _directoryInfo.GetDirectories();

        /// <summary>Returns a file list from the current directory matching the given search pattern and using a value to determine whether to search subdirectories.</summary>
        /// <param name="searchPattern">The search string to match against the names of files.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
        /// <param name="searchOption">One of the enumeration values that specifies whether the search operation should include only the current directory or all subdirectories.</param>
        /// <exception cref="ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="searchPattern" /> contains one or more invalid characters defined by the <see cref="IPathIO.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="searchOption" /> is not a valid <see cref="System.IO.SearchOption" /> value.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>An array of type <see cref="IFileInfoIO" />.</returns>
        public IFileInfoIO[] GetFiles(string searchPattern, System.IO.SearchOption searchOption) => _directoryInfo.GetFiles(searchPattern, searchOption);

        /// <summary>Returns a file list from the current directory.</summary>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path is invalid, such as being on an unmapped drive.</exception>
        /// <returns>An array of type <see cref="IFileInfoIO" />.</returns>
        public IFileInfoIO[] GetFiles() => _directoryInfo.GetFiles();

        /// <summary>Returns a file list from the current directory matching the given search pattern.</summary>
        /// <param name="searchPattern">The search string to match against the names of files.  This parameter can contain a combination of valid literal path and wildcard (* and ?) characters, but it doesn't support regular expressions.</param>
        /// <exception cref="ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="searchPattern" /> contains one or more invalid characters defined by the <see cref="IPathIO.GetInvalidPathChars" /> method.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="searchPattern" /> is <see langword="null" />.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path is invalid (for example, it is on an unmapped drive).</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>An array of type <see cref="IFileInfoIO" />.</returns>
        public IFileInfoIO[] GetFiles(string searchPattern) => _directoryInfo.GetFiles(searchPattern);

        /// <summary>Moves a <see cref="DirectoryInfo" /> instance and its contents to a new path.</summary>
        /// <param name="destDirName">The name and path to which to move this directory. The destination cannot be another disk volume or a directory with the identical name. It can be an existing directory to which you want to add this directory as a subdirectory.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="destDirName" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentException">
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
        public void MoveTo(string destDirName) => _directoryInfo.MoveTo(destDirName);

        /// <summary>Returns the original path that was passed to the <see cref="DirectoryInfo" /> constructor. Use the <see cref="FullName" /> or <see cref="Name" /> properties for the full path or file/directory name instead of this method.</summary>
        /// <returns>The original path that was passed by the user.</returns>
        public override string ToString() => _directoryInfo.ToString();

        /// <summary>Gets the full path of the directory</summary>
        /// <exception cref="System.IO.PathTooLongException">The fully qualified path and file name exceed the system-defined maximum length.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>A string containing the full path.</returns>
        public string FullName => _directoryInfo.FullName;

        /// <summary>Sets the <see cref="SerializationInfo" /> object with the file name and additional exception information.</summary>
        /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context) => _directoryInfo.GetObjectData(info, context);

        #region IFileSystemInfoIO

        /// <summary>Gets or sets the time when the current file or directory was last written to.</summary>
        /// <exception cref="System.IO.IOException">
        /// <see cref="FileSystemInfoWrapper.IFileSystemInfoIO.Refresh" /> cannot initialize the data.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The caller attempts to set an invalid write time.</exception>
        /// <returns>The time the current file was last written.</returns>
        public DateTime LastWriteTime { get => _directoryInfo.LastWriteTime; set => _directoryInfo.LastWriteTime = value; }

        /// <summary>Gets or sets the time, in coordinated universal time (UTC), that the current file or directory was last accessed.</summary>
        /// <exception cref="System.IO.IOException">
        /// <see cref="FileSystemInfoWrapper.IFileSystemInfoIO.Refresh" /> cannot initialize the data.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The caller attempts to set an invalid access time.</exception>
        /// <returns>The UTC time that the current file or directory was last accessed.</returns>
        public DateTime LastAccessTimeUtc { get => _directoryInfo.LastAccessTimeUtc; set => _directoryInfo.LastAccessTimeUtc = value; }

        /// <summary>Gets or sets the time the current file or directory was last accessed.</summary>
        /// <exception cref="System.IO.IOException">
        /// <see cref="FileSystemInfoWrapper.IFileSystemInfoIO.Refresh" /> cannot initialize the data.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The caller attempts to set an invalid access time</exception>
        /// <returns>The time that the current file or directory was last accessed.</returns>
        public DateTime LastAccessTime { get => _directoryInfo.LastAccessTime; set => _directoryInfo.LastAccessTime = value; }

        /// <summary>Gets the extension part of the file name, including the leading dot <c>.</c> even if it is the entire file name, or an empty string if no extension is present.</summary>
        /// <returns>A string containing the <see cref="FileSystemInfoWrapper.IFileSystemInfoIO" /> extension.</returns>
        public string Extension => _directoryInfo.Extension;

        /// <summary>Gets or sets the creation time, in coordinated universal time (UTC), of the current file or directory.</summary>
        /// <exception cref="System.IO.IOException">
        /// <see cref="FileSystemInfoWrapper.IFileSystemInfoIO.Refresh" /> cannot initialize the data.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The caller attempts to set an invalid access time.</exception>
        /// <returns>The creation date and time in UTC format of the current <see cref="FileSystemInfoWrapper.IFileSystemInfoIO" /> object.</returns>
        public DateTime CreationTime { get => _directoryInfo.CreationTime; set => _directoryInfo.CreationTime = value; }

        /// <summary>Gets or sets the time, in coordinated universal time (UTC), when the current file or directory was last written to.</summary>
        /// <exception cref="System.IO.IOException">
        /// <see cref="FileSystemInfoWrapper.IFileSystemInfoIO.Refresh" /> cannot initialize the data.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The caller attempts to set an invalid write time.</exception>
        /// <returns>The UTC time when the current file was last written to.</returns>
        public DateTime LastWriteTimeUtc { get => _directoryInfo.LastWriteTimeUtc; set => _directoryInfo.LastWriteTimeUtc = value; }

        /// <summary>Gets or sets the attributes for the current file or directory.</summary>
        /// <exception cref="System.IO.FileNotFoundException">The specified file doesn't exist. Only thrown when setting the property value.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid. For example, it's on an unmapped drive. Only thrown when setting the property value.</exception>
        /// <exception cref="System.Security.SecurityException">The caller doesn't have the required permission.</exception>
        /// <exception cref="UnauthorizedAccessException">.NET Core and .NET 5+ only: The user attempts to set an attribute value but doesn't have write permission.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="ArgumentException">The caller attempts to set an invalid file attribute.
        /// 
        /// -or-
        /// 
        /// .NET Framework only: The user attempts to set an attribute value but doesn't have write permission.</exception>
        /// <exception cref="System.IO.IOException">
        /// <see cref="FileSystemInfoWrapper.IFileSystemInfoIO.Refresh" /> cannot initialize the data.</exception>
        /// <returns>
        /// <see cref="System.IO.FileAttributes" /> of the current <see cref="FileSystemInfoWrapper.IFileSystemInfoIO" />.</returns>
        public System.IO.FileAttributes Attributes { get => _directoryInfo.Attributes; set => _directoryInfo.Attributes = value; }

        /// <summary>Gets or sets the creation time, in coordinated universal time (UTC), of the current file or directory.</summary>
        /// <exception cref="System.IO.IOException">
        /// <see cref="FileSystemInfoWrapper.IFileSystemInfoIO.Refresh" /> cannot initialize the data.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The caller attempts to set an invalid access time.</exception>
        /// <returns>The creation date and time in UTC format of the current <see cref="FileSystemInfoWrapper.IFileSystemInfoIO" /> object.</returns>
        public DateTime CreationTimeUtc { get => _directoryInfo.CreationTimeUtc; set => _directoryInfo.CreationTimeUtc = value; }

        /// <summary>Refreshes the state of the object.</summary>
        /// <exception cref="System.IO.IOException">A device such as a disk drive is not ready.</exception>
        public void Refresh() => _directoryInfo.Refresh();

        #endregion
    }
}
