namespace Skyline.DataMiner.CICD.FileSystem.FileSystemInfoWrapper
{
    using System;
    using System.IO;

    /// <summary>Provides the base interface for both <see cref="FileInfoWrapper.IFileInfoIO" /> and <see cref="DirectoryInfoWrapper.IDirectoryInfoIO" /> objects.</summary>
    public interface IFileSystemInfoIO
    {
        /// <summary>Gets or sets the attributes for the current file or directory.</summary>
        /// <exception cref="FileNotFoundException">The specified file doesn't exist. Only thrown when setting the property value.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid. For example, it's on an unmapped drive. Only thrown when setting the property value.</exception>
        /// <exception cref="System.Security.SecurityException">The caller doesn't have the required permission.</exception>
        /// <exception cref="UnauthorizedAccessException">.NET Core and .NET 5+ only: The user attempts to set an attribute value but doesn't have write permission.</exception>
        /// <exception cref="PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <exception cref="ArgumentException">The caller attempts to set an invalid file attribute.
        /// 
        /// -or-
        /// 
        /// .NET Framework only: The user attempts to set an attribute value but doesn't have write permission.</exception>
        /// <exception cref="IOException">
        /// <see cref="Refresh" /> cannot initialize the data.</exception>
        /// <returns>
        /// <see cref="FileAttributes" /> of the current <see cref="IFileSystemInfoIO" />.</returns>
        FileAttributes Attributes { get; set; }

        /// <summary>Gets or sets the creation time of the current file or directory.</summary>
        /// <exception cref="IOException">
        /// <see cref="Refresh" /> cannot initialize the data.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The caller attempts to set an invalid creation time.</exception>
        /// <returns>The creation date and time of the current <see cref="IFileSystemInfoIO" /> object.</returns>
        DateTime CreationTime { get; set; }

        /// <summary>Gets or sets the creation time, in coordinated universal time (UTC), of the current file or directory.</summary>
        /// <exception cref="IOException">
        /// <see cref="Refresh" /> cannot initialize the data.</exception>
        /// <exception cref="DirectoryNotFoundException">The specified path is invalid; for example, it is on an unmapped drive.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The caller attempts to set an invalid access time.</exception>
        /// <returns>The creation date and time in UTC format of the current <see cref="IFileSystemInfoIO" /> object.</returns>
        DateTime CreationTimeUtc { get; set; }

        /// <summary>Gets the extension part of the file name, including the leading dot <c>.</c> even if it is the entire file name, or an empty string if no extension is present.</summary>
        /// <returns>A string containing the <see cref="IFileSystemInfoIO" /> extension.</returns>
        string Extension { get; }

        /// <summary>Gets or sets the time the current file or directory was last accessed.</summary>
        /// <exception cref="IOException">
        /// <see cref="Refresh" /> cannot initialize the data.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The caller attempts to set an invalid access time</exception>
        /// <returns>The time that the current file or directory was last accessed.</returns>
        DateTime LastAccessTime { get; set; }

        /// <summary>Gets or sets the time, in coordinated universal time (UTC), that the current file or directory was last accessed.</summary>
        /// <exception cref="IOException">
        /// <see cref="Refresh" /> cannot initialize the data.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The caller attempts to set an invalid access time.</exception>
        /// <returns>The UTC time that the current file or directory was last accessed.</returns>
        DateTime LastAccessTimeUtc { get; set; }

        /// <summary>Gets or sets the time when the current file or directory was last written to.</summary>
        /// <exception cref="IOException">
        /// <see cref="Refresh" /> cannot initialize the data.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The caller attempts to set an invalid write time.</exception>
        /// <returns>The time the current file was last written.</returns>
        DateTime LastWriteTime { get; set; }

        /// <summary>Gets or sets the time, in coordinated universal time (UTC), when the current file or directory was last written to.</summary>
        /// <exception cref="IOException">
        /// <see cref="Refresh" /> cannot initialize the data.</exception>
        /// <exception cref="PlatformNotSupportedException">The current operating system is not Windows NT or later.</exception>
        /// <exception cref="ArgumentOutOfRangeException">The caller attempts to set an invalid write time.</exception>
        /// <returns>The UTC time when the current file was last written to.</returns>
        DateTime LastWriteTimeUtc { get; set; }

        /// <summary>Refreshes the state of the object.</summary>
        /// <exception cref="IOException">A device such as a disk drive is not ready.</exception>
        void Refresh();
    }
}