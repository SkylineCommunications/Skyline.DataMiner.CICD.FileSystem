﻿namespace Skyline.DataMiner.CICD.FileSystem.DirectoryInfoWrapper
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    using Skyline.DataMiner.CICD.FileSystem.FileInfoWrapper;
    using Skyline.DataMiner.CICD.FileSystem.FileSystemInfoWrapper;

    /// <inheritdoc cref="IDirectoryInfoIO" />
    internal sealed class DirectoryInfoIOWin : FileSystemInfoIOWin, IDirectoryInfoIO
    {
        private readonly Alphaleonis.Win32.Filesystem.DirectoryInfo _directoryInfo;

        internal DirectoryInfoIOWin(Alphaleonis.Win32.Filesystem.DirectoryInfo directoryInfo)
        {
            _directoryInfo = directoryInfo;
            FileSystemInfo = _directoryInfo;
        }

        internal DirectoryInfoIOWin(string path) : this(new Alphaleonis.Win32.Filesystem.DirectoryInfo(path))
        {
        }

        internal DirectoryInfoIOWin(System.IO.DirectoryInfo directoryInfo) : this(new Alphaleonis.Win32.Filesystem.DirectoryInfo(directoryInfo.FullName))
        {
        }

        /// <inheritdoc />
        public string Name => _directoryInfo.Name;

        /// <inheritdoc />
        public bool Exists => _directoryInfo.Exists;

        /// <inheritdoc />
        public IDirectoryInfoIO Parent => _directoryInfo.Parent == null ? null : new DirectoryInfoIOWin(_directoryInfo.Parent);

        /// <inheritdoc />
        public IDirectoryInfoIO Root => new DirectoryInfoIOWin(_directoryInfo.Root);

        /// <inheritdoc />
        public void Create() => _directoryInfo.Create();

        /// <inheritdoc />
        public IDirectoryInfoIO CreateSubdirectory(string path) => new DirectoryInfoIOWin(_directoryInfo.CreateSubdirectory(path));

        /// <inheritdoc />
        public void Delete() => _directoryInfo.Delete();

        /// <inheritdoc />
        public void Delete(bool recursive) => _directoryInfo.Delete(recursive);

        /// <inheritdoc />
        public IEnumerable<IDirectoryInfoIO> EnumerateDirectories() => _directoryInfo.EnumerateDirectories().Select(p => new DirectoryInfoIOWin(p));

        /// <inheritdoc />
        public IEnumerable<IDirectoryInfoIO> EnumerateDirectories(string searchPattern) => _directoryInfo.EnumerateDirectories(searchPattern).Select(p => new DirectoryInfoIOWin(p));

        /// <inheritdoc />
        public IEnumerable<IDirectoryInfoIO> EnumerateDirectories(string searchPattern, System.IO.SearchOption searchOption) => _directoryInfo.EnumerateDirectories(searchPattern, searchOption).Select(p => new DirectoryInfoIOWin(p));

        /// <inheritdoc />
        public IEnumerable<IFileInfoIO> EnumerateFiles() => _directoryInfo.EnumerateFiles().Select(p => new FileInfoIOWin(p));

        /// <inheritdoc />
        public IEnumerable<IFileInfoIO> EnumerateFiles(string searchPattern) => _directoryInfo.EnumerateFiles(searchPattern).Select(p => new FileInfoIOWin(p));

        /// <inheritdoc />
        public IEnumerable<IFileInfoIO> EnumerateFiles(string searchPattern, System.IO.SearchOption searchOption) => _directoryInfo.EnumerateFiles(searchPattern, searchOption).Select(p => new FileInfoIOWin(p));

        /// <inheritdoc />
        public IDirectoryInfoIO[] GetDirectories(string searchPattern, System.IO.SearchOption searchOption) => _directoryInfo.GetDirectories(searchPattern, searchOption).Select(p => new DirectoryInfoIOWin(p)).ToArray();

        /// <inheritdoc />
        public IDirectoryInfoIO[] GetDirectories(string searchPattern) => _directoryInfo.GetDirectories(searchPattern).Select(p => new DirectoryInfoIOWin(p)).ToArray();

        /// <inheritdoc />
        public IDirectoryInfoIO[] GetDirectories() => _directoryInfo.GetDirectories().Select(p => new DirectoryInfoIOWin(p)).ToArray();

        /// <inheritdoc />
        public IFileInfoIO[] GetFiles(string searchPattern, System.IO.SearchOption searchOption) => _directoryInfo.GetFiles(searchPattern, searchOption).Select(p => new FileInfoIOWin(p)).ToArray();

        /// <inheritdoc />
        public IFileInfoIO[] GetFiles() => _directoryInfo.GetFiles().Select(p => new FileInfoIOWin(p)).ToArray();

        /// <inheritdoc />
        public IFileInfoIO[] GetFiles(string searchPattern) => _directoryInfo.GetFiles(searchPattern).Select(p => new FileInfoIOWin(p)).ToArray();

        /// <inheritdoc />
        public void MoveTo(string destDirName) => _directoryInfo.MoveTo(destDirName);

        public override string ToString() => _directoryInfo.ToString();

        /// <inheritdoc />
        public string FullName => _directoryInfo.FullName;

        /// <inheritdoc />
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            var tempDirInfo = new System.IO.DirectoryInfo(_directoryInfo.FullName);
            tempDirInfo.GetObjectData(info, context);
        }
    }
}
