namespace Skyline.DataMiner.CICD.FileSystem.FileInfoWrapper
{
    using System.Runtime.Serialization;

    using Skyline.DataMiner.CICD.FileSystem.DirectoryInfoWrapper;
    using Skyline.DataMiner.CICD.FileSystem.FileSystemInfoWrapper;

    /// <inheritdoc cref="IFileInfoIO" />
    internal sealed class FileInfoIOLinux : FileSystemInfoIOLinux, IFileInfoIO
    {
        private readonly System.IO.FileInfo _fileInfo;
        
        internal FileInfoIOLinux(System.IO.FileInfo fileInfo)
        {
            _fileInfo = fileInfo;
            FileSystemInfo = _fileInfo;
        }

        internal FileInfoIOLinux(string fileName) : this(new System.IO.FileInfo(fileName))
        {
        }

        /// <inheritdoc />
        public IDirectoryInfoIO Directory
        {
            get
            {
                var result = _fileInfo.Directory;
                return result == null ? null : new DirectoryInfoIOLinux(result);
            }
        }

        /// <inheritdoc />
        public bool IsReadOnly { get => _fileInfo.IsReadOnly; set => _fileInfo.IsReadOnly = value; }

        /// <inheritdoc />
        public bool Exists => _fileInfo.Exists;

        /// <inheritdoc />
        public string DirectoryName => _fileInfo.DirectoryName;

        /// <inheritdoc />
        public long Length => _fileInfo.Length;

        /// <inheritdoc />
        public string Name => _fileInfo.Name;

        /// <inheritdoc />
        public IFileInfoIO CopyTo(string destFileName)
        {
            System.IO.FileInfo result = _fileInfo.CopyTo(destFileName);
            return new FileInfoIOLinux(result);
        }

        /// <inheritdoc />
        public IFileInfoIO CopyTo(string destFileName, bool overwrite)
        {
            System.IO.FileInfo result = _fileInfo.CopyTo(destFileName, overwrite);
            return new FileInfoIOLinux(result);
        }

        /// <inheritdoc />
        public string FullName => _fileInfo.FullName;

        /// <inheritdoc />
        public void Delete() => _fileInfo.Delete();

        /// <inheritdoc />
        public void GetObjectData(SerializationInfo info, StreamingContext context) => _fileInfo.GetObjectData(info, context);

        /// <inheritdoc />
        public System.IO.StreamWriter AppendText() => _fileInfo.AppendText();

        /// <inheritdoc />
        public System.IO.FileStream Create() => _fileInfo.Create();

        /// <inheritdoc />
        public System.IO.StreamWriter CreateText() => _fileInfo.CreateText();

        /// <inheritdoc />
        public void Decrypt() => _fileInfo.Decrypt();

        /// <inheritdoc />
        public void Encrypt() => _fileInfo.Encrypt();

        /// <inheritdoc />
        public void MoveTo(string destFileName) => _fileInfo.MoveTo(destFileName);

        /// <inheritdoc />
        public System.IO.FileStream Open(System.IO.FileMode mode, System.IO.FileAccess access, System.IO.FileShare share) => _fileInfo.Open(mode, access, share);

        /// <inheritdoc />
        public System.IO.FileStream Open(System.IO.FileMode mode, System.IO.FileAccess access) => _fileInfo.Open(mode, access);

        /// <inheritdoc />
        public System.IO.FileStream Open(System.IO.FileMode mode) => _fileInfo.Open(mode);

        /// <inheritdoc />
        public System.IO.FileStream OpenRead() => _fileInfo.OpenRead();

        /// <inheritdoc />
        public System.IO.StreamReader OpenText() => _fileInfo.OpenText();

        /// <inheritdoc />
        public System.IO.FileStream OpenWrite() => _fileInfo.OpenWrite();

        /// <inheritdoc />
        public IFileInfoIO Replace(string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
        {
            System.IO.FileInfo result = _fileInfo.Replace(destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
            return new FileInfoIOLinux(result);
        }

        /// <inheritdoc />
        public IFileInfoIO Replace(string destinationFileName, string destinationBackupFileName)
        {
            System.IO.FileInfo result = _fileInfo.Replace(destinationFileName, destinationBackupFileName);
            return new FileInfoIOLinux(result);
        }

        public override string ToString() => _fileInfo.ToString();
    }
}
