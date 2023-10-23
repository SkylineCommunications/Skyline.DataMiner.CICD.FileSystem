namespace Skyline.DataMiner.CICD.FileSystem.FileSystemInfoWrapper
{
    using System;

    /// <summary>Provides the base class for both <see cref="T:System.IO.FileInfo" /> and <see cref="T:System.IO.DirectoryInfo" /> objects.</summary>
    internal class FileSystemInfoIOWin : IFileSystemInfoIO
    {
        private protected Alphaleonis.Win32.Filesystem.FileSystemInfo FileSystemInfo;

        /// <inheritdoc />
        public System.IO.FileAttributes Attributes { get => FileSystemInfo.Attributes; set => FileSystemInfo.Attributes = value; }

        /// <inheritdoc />
        public DateTime CreationTime { get => FileSystemInfo.CreationTime; set => FileSystemInfo.CreationTime = value; }

        /// <inheritdoc />
        public DateTime CreationTimeUtc { get => FileSystemInfo.CreationTimeUtc; set => FileSystemInfo.CreationTimeUtc = value; }

        /// <inheritdoc />
        public string Extension => FileSystemInfo.Extension;

        /// <inheritdoc />
        public DateTime LastAccessTime { get => FileSystemInfo.LastAccessTime; set => FileSystemInfo.LastAccessTime = value; }

        /// <inheritdoc />
        public DateTime LastAccessTimeUtc { get => FileSystemInfo.LastAccessTimeUtc; set => FileSystemInfo.LastAccessTimeUtc = value; }

        /// <inheritdoc />
        public DateTime LastWriteTime { get => FileSystemInfo.LastWriteTime; set => FileSystemInfo.LastWriteTime = value; }

        /// <inheritdoc />
        public DateTime LastWriteTimeUtc { get => FileSystemInfo.LastWriteTimeUtc; set => FileSystemInfo.LastWriteTimeUtc = value; }

        /// <inheritdoc />
        public void Refresh() => FileSystemInfo.Refresh();
    }
}
