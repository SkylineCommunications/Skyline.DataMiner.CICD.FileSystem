namespace Skyline.DataMiner.CICD.FileSystem.DirectoryInfoWrapper
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    using Skyline.DataMiner.CICD.FileSystem.FileInfoWrapper;
    using Skyline.DataMiner.CICD.FileSystem.FileSystemInfoWrapper;

    /// <inheritdoc cref="IDirectoryInfoIO" />
    internal sealed class DirectoryInfoIOLinux : FileSystemInfoIOLinux, IDirectoryInfoIO
    {
        private readonly System.IO.DirectoryInfo _directoryInfo;

        internal DirectoryInfoIOLinux(System.IO.DirectoryInfo directoryInfo)
        {
            _directoryInfo = directoryInfo;
            FileSystemInfo = _directoryInfo;
        }

        internal DirectoryInfoIOLinux(string path) : this(new System.IO.DirectoryInfo(path))
        {
        }

        /// <inheritdoc />
        public string Name => _directoryInfo.Name;

        /// <inheritdoc />
        public bool Exists => _directoryInfo.Exists;

        /// <inheritdoc />
        public IDirectoryInfoIO Parent => _directoryInfo.Parent == null ? null : new DirectoryInfoIOLinux(_directoryInfo.Parent);

        /// <inheritdoc />
        public IDirectoryInfoIO Root => new DirectoryInfoIOLinux(_directoryInfo.Root);

        /// <inheritdoc />
        public void Create() => _directoryInfo.Create();

        /// <inheritdoc />
        public IDirectoryInfoIO CreateSubdirectory(string path) => new DirectoryInfoIOLinux(_directoryInfo.CreateSubdirectory(path));

        /// <inheritdoc />
        public void Delete() => _directoryInfo.Delete();

        /// <inheritdoc />
        public void Delete(bool recursive) => _directoryInfo.Delete(recursive);

        /// <inheritdoc />
        public IEnumerable<IDirectoryInfoIO> EnumerateDirectories() => _directoryInfo.EnumerateDirectories().Select(p => new DirectoryInfoIOLinux(p));

        /// <inheritdoc />
        public IEnumerable<IDirectoryInfoIO> EnumerateDirectories(string searchPattern) => _directoryInfo.EnumerateDirectories(searchPattern).Select(p => new DirectoryInfoIOLinux(p));

        /// <inheritdoc />
        public IEnumerable<IDirectoryInfoIO> EnumerateDirectories(string searchPattern, System.IO.SearchOption searchOption) => _directoryInfo.EnumerateDirectories(searchPattern, searchOption).Select(p => new DirectoryInfoIOLinux(p));

        /// <inheritdoc />
        public IEnumerable<IFileInfoIO> EnumerateFiles() => _directoryInfo.EnumerateFiles().Select(p => new FileInfoIOLinux(p));

        /// <inheritdoc />
        public IEnumerable<IFileInfoIO> EnumerateFiles(string searchPattern) => _directoryInfo.EnumerateFiles(searchPattern).Select(p => new FileInfoIOLinux(p));

        /// <inheritdoc />
        public IEnumerable<IFileInfoIO> EnumerateFiles(string searchPattern, System.IO.SearchOption searchOption) => _directoryInfo.EnumerateFiles(searchPattern, searchOption).Select(p => new FileInfoIOLinux(p));

        /// <inheritdoc />
        public IDirectoryInfoIO[] GetDirectories(string searchPattern, System.IO.SearchOption searchOption) => _directoryInfo.GetDirectories(searchPattern, searchOption).Select(p => new DirectoryInfoIOLinux(p)).ToArray();

        /// <inheritdoc />
        public IDirectoryInfoIO[] GetDirectories(string searchPattern) => _directoryInfo.GetDirectories(searchPattern).Select(p => new DirectoryInfoIOLinux(p)).ToArray();

        /// <inheritdoc />
        public IDirectoryInfoIO[] GetDirectories() => _directoryInfo.GetDirectories().Select(p => new DirectoryInfoIOLinux(p)).ToArray();

        /// <inheritdoc />
        public IFileInfoIO[] GetFiles(string searchPattern, System.IO.SearchOption searchOption) => _directoryInfo.GetFiles(searchPattern, searchOption).Select(p => new FileInfoIOLinux(p)).ToArray();

        /// <inheritdoc />
        public IFileInfoIO[] GetFiles() => _directoryInfo.GetFiles().Select(p => new FileInfoIOLinux(p)).ToArray();

        /// <inheritdoc />
        public IFileInfoIO[] GetFiles(string searchPattern) => _directoryInfo.GetFiles(searchPattern).Select(p => new FileInfoIOLinux(p)).ToArray();

        /// <inheritdoc />
        public void MoveTo(string destDirName) => _directoryInfo.MoveTo(destDirName);

        public override string ToString() => _directoryInfo.ToString();

        /// <inheritdoc />
        public string FullName => _directoryInfo.FullName;

        /// <inheritdoc />
        public void GetObjectData(SerializationInfo info, StreamingContext context) => _directoryInfo.GetObjectData(info, context);
    }
}
