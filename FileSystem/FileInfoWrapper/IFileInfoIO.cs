namespace Skyline.DataMiner.CICD.FileSystem.FileInfoWrapper
{
    using System.Runtime.Serialization;

    using Skyline.DataMiner.CICD.FileSystem.DirectoryInfoWrapper;
    using Skyline.DataMiner.CICD.FileSystem.FileSystemInfoWrapper;

    /// <summary>Provides properties and instance methods for the creation, copying, deletion, moving, and opening of files, and aids in the creation of <see cref="System.IO.FileStream" /> objects.</summary>
    public interface IFileInfoIO : IFileSystemInfoIO
    {
        /// <summary>Gets an instance of the parent directory.</summary>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>A <see cref="IDirectoryInfoIO" /> object representing the parent directory of this file.</returns>
        IDirectoryInfoIO Directory { get; }

        /// <summary>Gets a string representing the directory's full path.</summary>
        /// <exception cref="System.ArgumentNullException">
        /// <see langword="null" /> was passed in for the directory name.</exception>
        /// <exception cref="System.IO.PathTooLongException">The fully qualified path name exceeds the system-defined maximum length.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>A string representing the directory's full path.</returns>
        string DirectoryName { get; }

        /// <summary>Gets a value indicating whether a file exists.</summary>
        /// <returns>
        /// <see langword="true" /> if the file exists; <see langword="false" /> if the file does not exist or if the file is a directory.</returns>
        bool Exists { get; }

        /// <summary>Gets the full path of the file</summary>
        /// <exception cref="System.IO.PathTooLongException">The fully qualified path and file name exceed the system-defined maximum length.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>A string containing the full path.</returns>
        string FullName { get; }

        /// <summary>Gets or sets a value that determines if the current file is read only.</summary>
        /// <exception cref="System.IO.FileNotFoundException">The file described by the current <see cref="IFileInfoIO" /> object could not be found.</exception>
        /// <exception cref="System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="System.UnauthorizedAccessException">This operation is not supported on the current platform.
        /// 
        /// -or-
        /// 
        /// The caller does not have the required permission.</exception>
        /// <exception cref="System.ArgumentException">The user does not have write permission, but attempted to set this property to <see langword="false" />.</exception>
        /// <returns>
        /// <see langword="true" /> if the current file is read only; otherwise, <see langword="false" />.</returns>
        bool IsReadOnly { get; set; }

        /// <summary>Gets the size, in bytes, of the current file.</summary>
        /// <exception cref="System.IO.IOException">
        /// <see cref="System.IO.FileSystemInfo.Refresh" /> cannot update the state of the file or directory.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file does not exist.
        /// 
        /// -or-
        /// 
        /// The <see cref="Length" /> property is called for a directory.</exception>
        /// <returns>The size of the current file in bytes.</returns>
        long Length { get; }

        /// <summary>Gets the name of the file.</summary>
        /// <returns>The name of the file.</returns>
        string Name { get; }

        /// <summary>Creates a <see cref="System.IO.StreamWriter" /> that appends text to the file represented by this instance of the <see cref="IFileInfoIO" />.</summary>
        /// <returns>A new <see cref="System.IO.StreamWriter" />.</returns>
        System.IO.StreamWriter AppendText();

        /// <summary>Copies an existing file to a new file, disallowing the overwriting of an existing file.</summary>
        /// <param name="destFileName">The name of the new file to copy to.</param>
        /// <exception cref="System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="destFileName" /> is empty, contains only white spaces, or contains invalid characters.</exception>
        /// <exception cref="System.IO.IOException">An error occurs, or the destination file already exists.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="destFileName" /> is <see langword="null" />.</exception>
        /// <exception cref="System.UnauthorizedAccessException">A directory path is passed in, or the file is being moved to a different drive.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The directory specified in <paramref name="destFileName" /> does not exist.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="System.NotSupportedException">
        /// <paramref name="destFileName" /> contains a colon (:) within the string but does not specify the volume.</exception>
        /// <returns>A new file with a fully qualified path.</returns>
        IFileInfoIO CopyTo(string destFileName);

        /// <summary>Copies an existing file to a new file, allowing the overwriting of an existing file.</summary>
        /// <param name="destFileName">The name of the new file to copy to.</param>
        /// <param name="overwrite">
        /// <see langword="true" /> to allow an existing file to be overwritten; otherwise, <see langword="false" />.</param>
        /// <exception cref="System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="destFileName" /> is empty, contains only white spaces, or contains invalid characters.</exception>
        /// <exception cref="System.IO.IOException">An error occurs, or the destination file already exists and <paramref name="overwrite" /> is <see langword="false" />.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="destFileName" /> is <see langword="null" />.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The directory specified in <paramref name="destFileName" /> does not exist.</exception>
        /// <exception cref="System.UnauthorizedAccessException">A directory path is passed in, or the file is being moved to a different drive.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="System.NotSupportedException">
        /// <paramref name="destFileName" /> contains a colon (:) in the middle of the string.</exception>
        /// <returns>A new file, or an overwrite of an existing file if <paramref name="overwrite" /> is <see langword="true" />. If the file exists and <paramref name="overwrite" /> is <see langword="false" />, an <see cref="System.IO.IOException" /> is thrown.</returns>
        IFileInfoIO CopyTo(string destFileName, bool overwrite);

        /// <summary>Creates a file.</summary>
        /// <returns>A new file.</returns>
        System.IO.FileStream Create();

        /// <summary>Creates a <see cref="System.IO.StreamWriter" /> that writes a new text file.</summary>
        /// <exception cref="System.UnauthorizedAccessException">The file name is a directory.</exception>
        /// <exception cref="System.IO.IOException">The disk is read-only.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <returns>A new <see cref="System.IO.StreamWriter" />.</returns>
        System.IO.StreamWriter CreateText();

        /// <summary>Decrypts a file that was encrypted by the current account using the <see cref="Encrypt" /> method.</summary>
        /// <exception cref="System.IO.DriveNotFoundException">An invalid drive was specified.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file described by the current <see cref="IFileInfoIO" /> object could not be found.</exception>
        /// <exception cref="System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="System.NotSupportedException">The file system is not NTFS.</exception>
        /// <exception cref="System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        /// <exception cref="System.UnauthorizedAccessException">The file described by the current <see cref="IFileInfoIO" /> object is read-only.
        /// 
        /// -or-
        /// 
        /// This operation is not supported on the current platform.
        /// 
        /// -or-
        /// 
        /// The caller does not have the required permission.</exception>
        void Decrypt();

        /// <summary>Permanently deletes a file.</summary>
        /// <exception cref="System.IO.IOException">The target file is open or memory-mapped on a computer running Microsoft Windows NT.
        /// 
        /// -or-
        /// 
        /// There is an open handle on the file, and the operating system is Windows XP or earlier. This open handle can result from enumerating directories and files. For more information, see How to: Enumerate Directories and Files.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="System.UnauthorizedAccessException">The path is a directory.</exception>
        void Delete();

        /// <summary>Encrypts a file so that only the account used to encrypt the file can decrypt it.</summary>
        /// <exception cref="System.IO.DriveNotFoundException">An invalid drive was specified.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file described by the current <see cref="IFileInfoIO" /> object could not be found.</exception>
        /// <exception cref="System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// <exception cref="System.NotSupportedException">The file system is not NTFS.</exception>
        /// <exception cref="System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        /// <exception cref="System.UnauthorizedAccessException">The file described by the current <see cref="IFileInfoIO" /> object is read-only.
        /// 
        /// -or-
        /// 
        /// This operation is not supported on the current platform.
        /// 
        /// -or-
        /// 
        /// The caller does not have the required permission.</exception>
        void Encrypt();

        /// <summary>Sets the <see cref="SerializationInfo" /> object with the file name and additional exception information.</summary>
        /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
        void GetObjectData(SerializationInfo info, StreamingContext context);

        /// <summary>Moves a specified file to a new location, providing the option to specify a new file name.</summary>
        /// <param name="destFileName">The path to move the file to, which can specify a different file name.</param>
        /// <exception cref="System.IO.IOException">An I/O error occurs, such as the destination file already exists or the destination device is not ready.</exception>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="destFileName" /> is <see langword="null" />.</exception>
        /// <exception cref="System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="destFileName" /> is empty, contains only white spaces, or contains invalid characters.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="System.UnauthorizedAccessException">
        /// <paramref name="destFileName" /> is read-only or is a directory.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file is not found.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="System.NotSupportedException">
        /// <paramref name="destFileName" /> contains a colon (:) in the middle of the string.</exception>
        void MoveTo(string destFileName);

        /// <summary>Opens a file in the specified mode.</summary>
        /// <param name="mode">A <see cref="System.IO.FileMode" /> constant specifying the mode (for example, <see cref="System.IO.FileMode.Open" /> or <see cref="System.IO.FileMode.Append" />) in which to open the file.</param>
        /// <exception cref="System.IO.FileNotFoundException">The file is not found.</exception>
        /// <exception cref="System.UnauthorizedAccessException">The file is read-only or is a directory.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="System.IO.IOException">The file is already open.</exception>
        /// <returns>A file opened in the specified mode, with read/write access and unshared.</returns>
        System.IO.FileStream Open(System.IO.FileMode mode);

        /// <summary>Opens a file in the specified mode with read, write, or read/write access.</summary>
        /// <param name="mode">A <see cref="System.IO.FileMode" /> constant specifying the mode (for example, <see cref="System.IO.FileMode.Open" /> or <see cref="System.IO.FileMode.Append" />) in which to open the file.</param>
        /// <param name="access">A <see cref="System.IO.FileAccess" /> constant specifying whether to open the file with <see cref="System.IO.FileAccess.Read" />, <see cref="System.IO.FileAccess.Write" />, or <see cref="System.IO.FileAccess.ReadWrite" /> file access.</param>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file is not found.</exception>
        /// <exception cref="System.UnauthorizedAccessException">
        /// <see cref="Name" /> is read-only or is a directory.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="System.IO.IOException">The file is already open.</exception>
        /// <exception cref="System.ArgumentException">
        /// <see cref="Name" /> is empty or contains only white spaces.</exception>
        /// <exception cref="System.ArgumentNullException">One or more arguments is null.</exception>
        /// <returns>A <see cref="System.IO.FileStream" /> object opened in the specified mode and access, and unshared.</returns>
        System.IO.FileStream Open(System.IO.FileMode mode, System.IO.FileAccess access);

        /// <summary>Opens a file in the specified mode with read, write, or read/write access and the specified sharing option.</summary>
        /// <param name="mode">A <see cref="System.IO.FileMode" /> constant specifying the mode (for example, <see cref="System.IO.FileMode.Open" /> or <see cref="System.IO.FileMode.Append" />) in which to open the file.</param>
        /// <param name="access">A <see cref="System.IO.FileAccess" /> constant specifying whether to open the file with <see cref="System.IO.FileAccess.Read" />, <see cref="System.IO.FileAccess.Write" />, or <see cref="System.IO.FileAccess.ReadWrite" /> file access.</param>
        /// <param name="share">A <see cref="System.IO.FileShare" /> constant specifying the type of access other <see cref="System.IO.FileStream" /> objects have to this file.</param>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file is not found.</exception>
        /// <exception cref="System.UnauthorizedAccessException">
        /// <see cref="Name" /> is read-only or is a directory.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="System.IO.IOException">The file is already open.</exception>
        /// <exception cref="System.ArgumentException">
        /// <see cref="Name" /> is empty or contains only white spaces.</exception>
        /// <exception cref="System.ArgumentNullException">One or more arguments is null.</exception>
        /// <returns>A <see cref="System.IO.FileStream" /> object opened with the specified mode, access, and sharing options.</returns>
        System.IO.FileStream Open(System.IO.FileMode mode, System.IO.FileAccess access, System.IO.FileShare share);

        /// <summary>Creates a read-only <see cref="System.IO.FileStream" />.</summary>
        /// <exception cref="System.UnauthorizedAccessException">
        /// <see cref="Name" /> is read-only or is a directory.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <exception cref="System.IO.IOException">The file is already open.</exception>
        /// <returns>A new read-only <see cref="System.IO.FileStream" /> object.</returns>
        System.IO.FileStream OpenRead();

        /// <summary>Creates a <see cref="System.IO.StreamReader" /> with UTF8 encoding that reads from an existing text file.</summary>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file is not found.</exception>
        /// <exception cref="System.UnauthorizedAccessException">
        /// <see cref="Name" /> is read-only or is a directory.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
        /// <returns>A new <see cref="System.IO.StreamReader" /> with UTF8 encoding.</returns>
        System.IO.StreamReader OpenText();

        /// <summary>Creates a write-only <see cref="System.IO.FileStream" />.</summary>
        /// <exception cref="System.UnauthorizedAccessException">The path specified when creating an instance of the <see cref="IFileInfoIO" /> object is read-only or is a directory.</exception>
        /// <exception cref="System.IO.DirectoryNotFoundException">The path specified when creating an instance of the <see cref="IFileInfoIO" /> object is invalid, such as being on an unmapped drive.</exception>
        /// <returns>A write-only unshared <see cref="System.IO.FileStream" /> object for a new or existing file.</returns>
        System.IO.FileStream OpenWrite();

        /// <summary>Replaces the contents of a specified file with the file described by the current <see cref="IFileInfoIO" /> object, deleting the original file, and creating a backup of the replaced file.</summary>
        /// <param name="destinationFileName">The name of a file to replace with the current file.</param>
        /// <param name="destinationBackupFileName">The name of a file with which to create a backup of the file described by the <paramref name="destinationFileName" /> parameter.</param>
        /// <exception cref="System.ArgumentException">The path described by the <paramref name="destinationFileName" /> parameter was not of a legal form.
        /// 
        /// -or-
        /// 
        /// The path described by the <paramref name="destinationBackupFileName" /> parameter was not of a legal form.</exception>
        /// <exception cref="System.ArgumentNullException">The <paramref name="destinationFileName" /> parameter is <see langword="null" />.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file described by the current <see cref="IFileInfoIO" /> object could not be found.
        /// 
        /// -or-
        /// 
        /// The file described by the <paramref name="destinationFileName" /> parameter could not be found.</exception>
        /// <exception cref="System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        /// <returns>A <see cref="IFileInfoIO" /> object that encapsulates information about the file described by the <paramref name="destinationFileName" /> parameter.</returns>
        IFileInfoIO Replace(string destinationFileName, string destinationBackupFileName);

        /// <summary>Replaces the contents of a specified file with the file described by the current <see cref="IFileInfoIO" /> object, deleting the original file, and creating a backup of the replaced file.  Also specifies whether to ignore merge errors.</summary>
        /// <param name="destinationFileName">The name of a file to replace with the current file.</param>
        /// <param name="destinationBackupFileName">The name of a file with which to create a backup of the file described by the <paramref name="destinationFileName" /> parameter.</param>
        /// <param name="ignoreMetadataErrors">
        /// <see langword="true" /> to ignore merge errors (such as attributes and ACLs) from the replaced file to the replacement file; otherwise <see langword="false" />.</param>
        /// <exception cref="System.ArgumentException">The path described by the <paramref name="destinationFileName" /> parameter was not of a legal form.
        /// 
        /// -or-
        /// 
        /// The path described by the <paramref name="destinationBackupFileName" /> parameter was not of a legal form.</exception>
        /// <exception cref="System.ArgumentNullException">The <paramref name="destinationFileName" /> parameter is <see langword="null" />.</exception>
        /// <exception cref="System.IO.FileNotFoundException">The file described by the current <see cref="IFileInfoIO" /> object could not be found.
        /// 
        /// -or-
        /// 
        /// The file described by the <paramref name="destinationFileName" /> parameter could not be found.</exception>
        /// <exception cref="System.PlatformNotSupportedException">The current operating system is not Microsoft Windows NT or later.</exception>
        /// <returns>A <see cref="IFileInfoIO" /> object that encapsulates information about the file described by the <paramref name="destinationFileName" /> parameter.</returns>
        IFileInfoIO Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors);

        /// <summary>Returns the path as a string. Use the <see cref="Name" /> property for the full path.</summary>
        /// <returns>A string representing the path.</returns>
        string ToString();
    }
}