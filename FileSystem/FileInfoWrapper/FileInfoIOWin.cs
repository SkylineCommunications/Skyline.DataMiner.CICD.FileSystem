namespace Skyline.DataMiner.CICD.FileSystem.FileInfoWrapper
{
    using System.Runtime.Serialization;

    using Skyline.DataMiner.CICD.FileSystem.DirectoryInfoWrapper;
    using Skyline.DataMiner.CICD.FileSystem.FileSystemInfoWrapper;

    /// <inheritdoc cref="IFileInfoIO" />
    internal sealed class FileInfoIOWin : FileSystemInfoIOWin, IFileInfoIO
    {
        private readonly Alphaleonis.Win32.Filesystem.FileInfo _fileInfo;
        
        internal FileInfoIOWin(Alphaleonis.Win32.Filesystem.FileInfo fileInfo)
        {
            _fileInfo = fileInfo;
            FileSystemInfo = _fileInfo;
        }

        internal FileInfoIOWin(string fileName) : this(new Alphaleonis.Win32.Filesystem.FileInfo(fileName))
        {
        }

        internal FileInfoIOWin(System.IO.FileInfo fileInfo) : this(new Alphaleonis.Win32.Filesystem.FileInfo(fileInfo.FullName))
        {
        }

        /// <inheritdoc />
        public IDirectoryInfoIO Directory
        {
            get
            {
                var result = _fileInfo.Directory;
                return result == null ? null : new DirectoryInfoIOWin(result);
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
            /*
             * https://github.com/alphaleonis/AlphaFS/issues/526
             * AlphaFS mutates the original FileInfo to the new destination. This deviates from the behavior of System.IO.FileInfo.CopyTo.
             * As such, we will create a new FileInfo object to do the copy to not change the _fileInfo.
             */
            Alphaleonis.Win32.Filesystem.FileInfo temp = new Alphaleonis.Win32.Filesystem.FileInfo(_fileInfo.FullName);
            temp.CopyTo(destFileName);
            
            return this;
        }

        /// <inheritdoc />
        public IFileInfoIO CopyTo(string destFileName, bool overwrite)
        {
            /*
             * https://github.com/alphaleonis/AlphaFS/issues/526
             * AlphaFS mutates the original FileInfo to the new destination. This deviates from the behavior of System.IO.FileInfo.CopyTo.
             * As such, we will create a new FileInfo object to do the copy to not change the _fileInfo.
             */
            Alphaleonis.Win32.Filesystem.FileInfo temp = new Alphaleonis.Win32.Filesystem.FileInfo(_fileInfo.FullName);
            temp.CopyTo(destFileName, overwrite);

            return this;
        }

        /// <inheritdoc />
        public string FullName => _fileInfo.FullName;

        /// <inheritdoc />
        public void Delete() => _fileInfo.Delete();

        /// <inheritdoc />
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            System.IO.FileInfo oldFileInfo = new System.IO.FileInfo(_fileInfo.FullName);
            oldFileInfo.GetObjectData(info, context);
        }

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
            Alphaleonis.Win32.Filesystem.FileInfo result = _fileInfo.Replace(destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
            return new FileInfoIOWin(result);
        }

        /// <inheritdoc />
        public IFileInfoIO Replace(string destinationFileName, string destinationBackupFileName)
        {
            Alphaleonis.Win32.Filesystem.FileInfo result = _fileInfo.Replace(destinationFileName, destinationBackupFileName);
            return new FileInfoIOWin(result);
        }

        public override string ToString() => _fileInfo.ToString();
    }
}
