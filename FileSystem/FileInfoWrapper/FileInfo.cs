namespace Skyline.DataMiner.CICD.FileSystem.FileInfoWrapper
{
    using System;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;

    using Skyline.DataMiner.CICD.FileSystem.DirectoryInfoWrapper;

    /// <summary>Provides properties and instance methods for the creation, copying, deletion, moving, and opening of files, and aids in the creation of <see cref="System.IO.FileStream" /> objects.</summary>
    public sealed class FileInfo : IFileInfoIO
    {
        private readonly IFileInfoIO _fileInfo;

        /// <summary>Initializes a new instance of the <see cref="FileInfo" /> class, which acts as a wrapper for a file path.</summary>
        /// <param name="fileName">The fully qualified name of the new file, or the relative file name. Do not end the path with the directory separator character.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fileName" /> is <see langword="null" />.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="ArgumentException">.NET Framework and .NET Core versions older than 2.1: The file name is empty, contains only white spaces, or contains invalid characters.</exception>
        /// <exception cref="UnauthorizedAccessException">Access to <paramref name="fileName" /> is denied.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="fileName" /> contains a colon (:) in the middle of the string.</exception>
        public FileInfo(string fileName)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _fileInfo = new FileInfoIOWin(fileName);
            }
            else
            {
                _fileInfo = new FileInfoIOLinux(fileName);
            }
        }

        /// <summary>Initializes a new instance of the <see cref="FileInfo" /> class, which acts as a wrapper for a file path.</summary>
        /// <param name="fileInfo">The <see cref="System.IO.FileInfo"/> to convert to the <see cref="IFileInfoIO"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="fileInfo" /> is <see langword="null" />.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="ArgumentException">.NET Framework and .NET Core versions older than 2.1: The file name is empty, contains only white spaces, or contains invalid characters.</exception>
        /// <exception cref="UnauthorizedAccessException">Access to <paramref name="fileInfo" /> is denied.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="fileInfo" /> contains a colon (:) in the middle of the string.</exception>
        public FileInfo(System.IO.FileInfo fileInfo)
        {
            if (fileInfo is null)
            {
                throw new ArgumentNullException(nameof(fileInfo));
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _fileInfo = new FileInfoIOWin(fileInfo);
            }
            else
            {
                _fileInfo = new FileInfoIOLinux(fileInfo);
            }
        }

        /// <summary>Gets an instance of the parent directory.</summary>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>A <see cref="IDirectoryInfoIO" /> object representing the parent directory of this file.</returns>
        public IDirectoryInfoIO Directory => _fileInfo.Directory;

        /// <summary>Gets a string representing the directory's full path.</summary>
        /// <exception cref="ArgumentNullException">
        /// <see langword="null" /> was passed in for the directory name.</exception>
        /// <exception cref="System.IO.PathTooLongException">The fully qualified path name exceeds the system-defined maximum length.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>A string representing the directory's full path.</returns>
        public string DirectoryName => _fileInfo.DirectoryName;

        /// <summary>Gets a value indicating whether a file exists.</summary>
        /// <returns>
        /// <see langword="true" /> if the file exists; <see langword="false" /> if the file does not exist or if the file is a directory.</returns>
        public bool Exists => _fileInfo.Exists;

        /// <summary>Gets or sets a value that determines if the current file is read only.</summary>
        /// <exception cref="System.IO.FileNotFoundException">The file described by the current <see cref="FileInfo" /> object could not be found.</exception>
        /// <exception cref="System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="UnauthorizedAccessException">This operation is not supported on the current platform.
        /// 
        /// -or-
        /// 
        /// The caller does not have the required permission.</exception>
        /// <exception cref="ArgumentException">The user does not have write permission, but attempted to set this property to <see langword="false" />.</exception>
        /// <returns>
        /// <see langword="true" /> if the current file is read only; otherwise, <see langword="false" />.</returns>
        public bool IsReadOnly { get => _fileInfo.IsReadOnly; set => _fileInfo.IsReadOnly = value; }

        /// <summary>Gets the size, in bytes, of the current file.</summary>
        /// <exception cref="System.IO.IOException">
        /// <see cref="FileSystemInfoWrapper.IFileSystemInfoIO.Refresh" /> cannot update the state of the file or directory.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file does not exist.
        /// 
        /// -or-
        /// 
        /// The <see cref="Length" /> property is called for a directory.</exception>
        /// <returns>The size of the current file in bytes.</returns>
        public long Length => _fileInfo.Length;

        /// <summary>Gets the name of the file.</summary>
        /// <returns>The name of the file.</returns>
        public string Name => _fileInfo.Name;

        /// <summary>Copies an existing file to a new file, disallowing the overwriting of an existing file.</summary>
        /// <param name="destFileName">The name of the new file to copy to.</param>
        /// <exception cref="ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="destFileName" /> is empty, contains only white spaces, or contains invalid characters.</exception>
        /// <exception cref="System.IO.IOException">An error occurs, or the destination file already exists.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="destFileName" /> is <see langword="null" />.</exception>
        /// <exception cref="UnauthorizedAccessException">A directory path is passed in, or the file is being moved to a different drive.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The directory specified in <paramref name="destFileName" /> does not exist.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="destFileName" /> contains a colon (:) within the string but does not specify the volume.</exception>
        /// <returns>A new file with a fully qualified path.</returns>
        public IFileInfoIO CopyTo(string destFileName) => _fileInfo.CopyTo(destFileName);

        /// <summary>Copies an existing file to a new file, allowing the overwriting of an existing file.</summary>
        /// <param name="destFileName">The name of the new file to copy to.</param>
        /// <param name="overwrite">
        /// <see langword="true" /> to allow an existing file to be overwritten; otherwise, <see langword="false" />.</param>
        /// <exception cref="ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="destFileName" /> is empty, contains only white spaces, or contains invalid characters.</exception>
        /// <exception cref="System.IO.IOException">An error occurs, or the destination file already exists and <paramref name="overwrite" /> is <see langword="false" />.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="destFileName" /> is <see langword="null" />.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The directory specified in <paramref name="destFileName" /> does not exist.</exception>
        /// <exception cref="UnauthorizedAccessException">A directory path is passed in, or the file is being moved to a different drive.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="destFileName" /> contains a colon (:) in the middle of the string.</exception>
        /// <returns>A new file, or an overwrite of an existing file if <paramref name="overwrite" /> is <see langword="true" />. If the file exists and <paramref name="overwrite" /> is <see langword="false" />, an <see cref="System.IO.IOException" /> is thrown.</returns>
        public IFileInfoIO CopyTo(string destFileName, bool overwrite) => _fileInfo.CopyTo(destFileName, overwrite);

        /// <summary>Gets the full path of the file</summary>
        /// <exception cref="System.IO.PathTooLongException">The fully qualified path and file name exceed the system-defined maximum length.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>A string containing the full path.</returns>
        public string FullName => _fileInfo.FullName;

        /// <summary>Permanently deletes a file.</summary>
        /// <exception cref="System.IO.IOException">The target file is open or memory-mapped on a computer running Microsoft Windows NT.
        /// 
        /// -or-
        /// 
        /// There is an open handle on the file, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="UnauthorizedAccessException">The path is a directory.</exception>
        public void Delete() => _fileInfo.Delete();

        /// <summary>Sets the <see cref="SerializationInfo" /> object with the file name and additional exception information.</summary>
        /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context) => _fileInfo.GetObjectData(info, context);

        /// <summary>Creates a <see cref="System.IO.StreamWriter" /> that appends text to the file represented by this instance of the <see cref="FileInfo" />.</summary>
        /// <returns>A new <see langword="StreamWriter" />.</returns>
        public System.IO.StreamWriter AppendText() => _fileInfo.AppendText();

        /// <summary>Creates a file.</summary>
        /// <returns>A new file.</returns>
        public System.IO.FileStream Create() => _fileInfo.Create();

        /// <summary>Creates a <see cref="System.IO.StreamWriter" /> that writes a new text file.</summary>
        /// <exception cref="UnauthorizedAccessException">The file name is a directory.</exception>
        /// <exception cref="System.IO.IOException">The disk is read-only.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>A new <see cref="System.IO.StreamWriter" />.</returns>
        public System.IO.StreamWriter CreateText() => _fileInfo.CreateText();

        /// <summary>Decrypts a file that was encrypted by the current account using the <see cref="FileInfo.Encrypt" /> method.</summary>
        /// <exception cref="System.IO.DriveNotFoundException">An invalid drive was specified.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file described by the current <see cref="FileInfo" /> object could not be found.</exception>
        /// <exception cref="System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="NotSupportedException">The file system is not NTFS.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        /// <exception cref="UnauthorizedAccessException">The file described by the current <see cref="FileInfo" /> object is read-only.
        /// 
        /// -or-
        /// 
        /// This operation is not supported on the current platform.
        /// 
        /// -or-
        /// 
        /// The caller does not have the required permission.</exception>
        public void Decrypt() => _fileInfo.Decrypt();

        /// <summary>Encrypts a file so that only the account used to encrypt the file can decrypt it.</summary>
        /// <exception cref="System.IO.DriveNotFoundException">An invalid drive was specified.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file described by the current <see cref="FileInfo" /> object could not be found.</exception>
        /// <exception cref="System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="NotSupportedException">The file system is not NTFS.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        /// <exception cref="UnauthorizedAccessException">The file described by the current <see cref="FileInfo" /> object is read-only.
        /// 
        /// -or-
        /// 
        /// This operation is not supported on the current platform.
        /// 
        /// -or-
        /// 
        /// The caller does not have the required permission.</exception>
        public void Encrypt() => _fileInfo.Encrypt();

        /// <summary>Moves a specified file to a new location, providing the option to specify a new file name.</summary>
        /// <param name="destFileName">The path to move the file to, which can specify a different file name.</param>
        /// <exception cref="System.IO.IOException">An I/O error occurs, such as the destination file already exists or the destination device is not ready.</exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="destFileName" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="destFileName" /> is empty, contains only white spaces, or contains invalid characters.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="UnauthorizedAccessException">
        /// <paramref name="destFileName" /> is read-only or is a directory.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file is not found.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="destFileName" /> contains a colon (:) in the middle of the string.</exception>
        public void MoveTo(string destFileName) => _fileInfo.MoveTo(destFileName);

        /// <summary>Opens a file in the specified mode with read, write, or read/write access and the specified sharing option.</summary>
        /// <param name="mode">A <see cref="System.IO.FileMode" /> constant specifying the mode (for example, <see cref="System.IO.FileMode.Open" /> or <see cref="System.IO.FileMode.Append" />) in which to open the file.</param>
        /// <param name="access">A <see cref="System.IO.FileAccess" /> constant specifying whether to open the file with <see cref="System.IO.FileAccess.Read" />, <see cref="System.IO.FileAccess.Write" />, or <see cref="System.IO.FileAccess.ReadWrite" /> file access.</param>
        /// <param name="share">A <see cref="System.IO.FileShare" /> constant specifying the type of access other <see cref="System.IO.FileStream" /> objects have to this file.</param>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file is not found.</exception>
        /// <exception cref="UnauthorizedAccessException">
        /// <see cref="Name" /> is read-only or is a directory.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="System.IO.IOException">The file is already open.</exception>
        /// <exception cref="ArgumentException">
        /// <see cref="Name" /> is empty or contains only white spaces.</exception>
        /// <exception cref="ArgumentNullException">One or more arguments is null.</exception>
        /// <returns>A <see cref="System.IO.FileStream" /> object opened with the specified mode, access, and sharing options.</returns>
        public System.IO.FileStream Open(System.IO.FileMode mode, System.IO.FileAccess access, System.IO.FileShare share) => _fileInfo.Open(mode, access, share);

        /// <summary>Opens a file in the specified mode with read, write, or read/write access.</summary>
        /// <param name="mode">A <see cref="System.IO.FileMode" /> constant specifying the mode (for example, <see cref="System.IO.FileMode.Open" /> or <see cref="System.IO.FileMode.Append" />) in which to open the file.</param>
        /// <param name="access">A <see cref="System.IO.FileAccess" /> constant specifying whether to open the file with <see cref="System.IO.FileAccess.Read" />, <see cref="System.IO.FileAccess.Write" />, or <see cref="System.IO.FileAccess.ReadWrite" /> file access.</param>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file is not found.</exception>
        /// <exception cref="UnauthorizedAccessException">
        /// <see cref="Name" /> is read-only or is a directory.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="System.IO.IOException">The file is already open.</exception>
        /// <exception cref="ArgumentException">
        /// <see cref="Name" /> is empty or contains only white spaces.</exception>
        /// <exception cref="ArgumentNullException">One or more arguments is null.</exception>
        /// <returns>A <see cref="System.IO.FileStream" /> object opened in the specified mode and access, and unshared.</returns>
        public System.IO.FileStream Open(System.IO.FileMode mode, System.IO.FileAccess access) => _fileInfo.Open(mode, access);

        /// <summary>Opens a file in the specified mode.</summary>
        /// <param name="mode">A <see cref="System.IO.FileMode" /> constant specifying the mode (for example, <see cref="System.IO.FileMode.Open" /> or <see cref="System.IO.FileMode.Append" />) in which to open the file.</param>
        /// <exception cref="System.IO.FileNotFoundException">The file is not found.</exception>
        /// <exception cref="UnauthorizedAccessException">The file is read-only or is a directory.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="System.IO.IOException">The file is already open.</exception>
        /// <returns>A file opened in the specified mode, with read/write access and unshared.</returns>
        public System.IO.FileStream Open(System.IO.FileMode mode) => _fileInfo.Open(mode);

        /// <summary>Creates a read-only <see cref="System.IO.FileStream" />.</summary>
        /// <exception cref="UnauthorizedAccessException">
        /// <see cref="Name" /> is read-only or is a directory.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="System.IO.IOException">The file is already open.</exception>
        /// <returns>A new read-only <see cref="System.IO.FileStream" /> object.</returns>
        public System.IO.FileStream OpenRead() => _fileInfo.OpenRead();

        /// <summary>Creates a <see cref="System.IO.StreamReader" /> with UTF8 encoding that reads from an existing text file.</summary>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file is not found.</exception>
        /// <exception cref="UnauthorizedAccessException">
        /// <see cref="Name" /> is read-only or is a directory.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <returns>A new <see cref="System.IO.StreamReader" /> with UTF8 encoding.</returns>
        public System.IO.StreamReader OpenText() => _fileInfo.OpenText();

        /// <summary>Creates a write-only <see cref="System.IO.FileStream" />.</summary>
        /// <exception cref="UnauthorizedAccessException">The path specified when creating an instance of the <see cref="FileInfo" /> object is read-only or is a directory.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path specified when creating an instance of the <see cref="FileInfo" /> object is invalid, such as being on an unmapped drive.</exception>
        /// <returns>A write-only unshared <see cref="System.IO.FileStream" /> object for a new or existing file.</returns>
        public System.IO.FileStream OpenWrite() => _fileInfo.OpenWrite();

        /// <summary>Replaces the contents of a specified file with the file described by the current <see cref="FileInfo" /> object, deleting the original file, and creating a backup of the replaced file. Also specifies whether to ignore merge errors.</summary>
        /// <param name="destinationFileName">The name of a file to replace with the current file.</param>
        /// <param name="destinationBackupFileName">The name of a file with which to create a backup of the file described by the <paramref name="destinationFileName" /> parameter.</param>
        /// <param name="ignoreMetadataErrors">
        /// <see langword="true" /> to ignore merge errors (such as attributes and ACLs) from the replaced file to the replacement file; otherwise <see langword="false" />.</param>
        /// <exception cref="ArgumentException">The path described by the <paramref name="destinationFileName" /> parameter was not of a legal form.
        /// 
        /// -or-
        /// 
        /// The path described by the <paramref name="destinationBackupFileName" /> parameter was not of a legal form.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="destinationFileName" /> parameter is <see langword="null" />.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file described by the current <see cref="FileInfo" /> object could not be found.
        /// 
        /// -or-
        /// 
        /// The file described by the <paramref name="destinationFileName" /> parameter could not be found.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        /// <returns>A <see cref="IFileInfoIO" /> object that encapsulates information about the file described by the <paramref name="destinationFileName" /> parameter.</returns>
        public IFileInfoIO Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors) => _fileInfo.Replace(destinationFileName, destinationBackupFileName, ignoreMetadataErrors);

        /// <summary>Replaces the contents of a specified file with the file described by the current <see cref="FileInfo" /> object, deleting the original file, and creating a backup of the replaced file.</summary>
        /// <param name="destinationFileName">The name of a file to replace with the current file.</param>
        /// <param name="destinationBackupFileName">The name of a file with which to create a backup of the file described by the <paramref name="destinationFileName" /> parameter.</param>
        /// <exception cref="ArgumentException">The path described by the <paramref name="destinationFileName" /> parameter was not of a legal form.
        /// 
        /// -or-
        /// 
        /// The path described by the <paramref name="destinationBackupFileName" /> parameter was not of a legal form.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="destinationFileName" /> parameter is <see langword="null" />.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file described by the current <see cref="FileInfo" /> object could not be found.
        /// 
        /// -or-
        /// 
        /// The file described by the <paramref name="destinationFileName" /> parameter could not be found.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        /// <returns>A <see cref="IFileInfoIO" /> object that encapsulates information about the file described by the <paramref name="destinationFileName" /> parameter.</returns>
        public IFileInfoIO Replace(string destinationFileName, string destinationBackupFileName) => _fileInfo.Replace(destinationFileName, destinationBackupFileName);

        /// <summary>Returns the path as a string. Use the <see cref="Name" /> property for the full path.</summary>
        /// <returns>A string representing the path.</returns>
        public override string ToString() => _fileInfo.ToString();

        #region IFileSystemInfoIO

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
        public System.IO.FileAttributes Attributes { get => _fileInfo.Attributes; set => _fileInfo.Attributes = value; }

        /// <summary>Gets or sets the creation time of the current file or directory.</summary>
        /// <exception cref="System.IO.IOException">
        /// <see cref="FileSystemInfoWrapper.IFileSystemInfoIO.Refresh" /> cannot initialize the data.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The caller attempts to set an invalid creation time.</exception>
        /// <returns>The creation date and time of the current <see cref="FileSystemInfoWrapper.IFileSystemInfoIO" /> object.</returns>
        public DateTime CreationTime { get => _fileInfo.CreationTime; set => _fileInfo.CreationTime = value; }

        /// <summary>Gets or sets the creation time, in coordinated universal time (UTC), of the current file or directory.</summary>
        /// <exception cref="System.IO.IOException">
        /// <see cref="FileSystemInfoWrapper.IFileSystemInfoIO.Refresh" /> cannot initialize the data.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The caller attempts to set an invalid access time.</exception>
        /// <returns>The creation date and time in UTC format of the current <see cref="FileSystemInfoWrapper.IFileSystemInfoIO" /> object.</returns>
        public DateTime CreationTimeUtc { get => _fileInfo.CreationTimeUtc; set => _fileInfo.CreationTimeUtc = value; }

        /// <summary>Gets the extension part of the file name, including the leading dot <c>.</c> even if it is the entire file name, or an empty string if no extension is present.</summary>
        /// <returns>A string containing the <see cref="FileSystemInfoWrapper.IFileSystemInfoIO" /> extension.</returns>
        public string Extension => _fileInfo.Extension;

        /// <summary>Gets or sets the time the current file or directory was last accessed.</summary>
        /// <exception cref="System.IO.IOException">
        /// <see cref="FileSystemInfoWrapper.IFileSystemInfoIO.Refresh" /> cannot initialize the data.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The caller attempts to set an invalid access time</exception>
        /// <returns>The time that the current file or directory was last accessed.</returns>
        public DateTime LastAccessTime { get => _fileInfo.LastAccessTime; set => _fileInfo.LastAccessTime = value; }

        /// <summary>Gets or sets the time, in coordinated universal time (UTC), that the current file or directory was last accessed.</summary>
        /// <exception cref="System.IO.IOException">
        /// <see cref="FileSystemInfoWrapper.IFileSystemInfoIO.Refresh" /> cannot initialize the data.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The caller attempts to set an invalid access time.</exception>
        /// <returns>The UTC time that the current file or directory was last accessed.</returns>
        public DateTime LastAccessTimeUtc { get => _fileInfo.LastAccessTimeUtc; set => _fileInfo.LastAccessTimeUtc = value; }

        /// <summary>Gets or sets the time when the current file or directory was last written to.</summary>
        /// <exception cref="System.IO.IOException">
        /// <see cref="FileSystemInfoWrapper.IFileSystemInfoIO.Refresh" /> cannot initialize the data.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The caller attempts to set an invalid write time.</exception>
        /// <returns>The time the current file was last written.</returns>
        public DateTime LastWriteTime { get => _fileInfo.LastWriteTime; set => _fileInfo.LastWriteTime = value; }

        /// <summary>Gets or sets the time, in coordinated universal time (UTC), when the current file or directory was last written to.</summary>
        /// <exception cref="System.IO.IOException">
        /// <see cref="FileSystemInfoWrapper.IFileSystemInfoIO.Refresh" /> cannot initialize the data.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The caller attempts to set an invalid write time.</exception>
        /// <returns>The UTC time when the current file was last written to.</returns>
        public DateTime LastWriteTimeUtc { get => _fileInfo.LastWriteTimeUtc; set => _fileInfo.LastWriteTimeUtc = value; }

        /// <summary>Refreshes the state of the object.</summary>
        /// <exception cref="System.IO.IOException">A device such as a disk drive is not ready.</exception>
        public void Refresh() => _fileInfo.Refresh();

        #endregion
    }
}