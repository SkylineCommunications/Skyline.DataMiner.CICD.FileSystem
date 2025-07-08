namespace Skyline.DataMiner.CICD.FileSystem
{
    using System;
    using System.Diagnostics;
    using System.IO;

    /// <inheritdoc />
    internal sealed class FileIOLinux : IFileIO
    {
        /// <inheritdoc />
        public void AppendAllText(string filePath, string fileContent)
        {
            TryAllowWritesOnFile(filePath);
            File.AppendAllText(filePath, fileContent);
        }

        /// <inheritdoc />
        public void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                Delete(path);
            }
        }

        /// <inheritdoc />
        public void Delete(string path)
        {
            TryAllowWritesOnFile(path);
            File.Delete(path);
        }

        /// <inheritdoc />
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        /// <inheritdoc />
        public long GetFileLength(string fileName)
        {
            return new FileInfo(fileName).Length;
        }

        /// <inheritdoc />
        public string GetFileProductVersion(string filePath)
        {
            if (!Exists(filePath))
                return String.Empty;

            var fileVersionInfo = FileVersionInfo.GetVersionInfo(filePath);
            return fileVersionInfo.ProductVersion;
        }

        /// <inheritdoc />
        public string GetFileVersion(string filePath)
        {
            if (!Exists(filePath))
                return String.Empty;

            var fileVersionInfo = FileVersionInfo.GetVersionInfo(filePath);
            return fileVersionInfo.FileVersion;
        }

        /// <inheritdoc />
        public string GetFullPath(string relativePath)
        {
            return FileSystem.Instance.Path.GetFullPath(relativePath);
        }

        /// <inheritdoc />
        public string GetParentDirectory(string relativePath)
        {
            return FileSystem.Instance.Path.GetDirectoryName(relativePath);
        }

        /// <inheritdoc />
        public void MoveFile(string filePath, string sourceFolder, string targetFolder, bool forceReplace)
        {
            string targetFile = Path.Combine(targetFolder, Path.GetFileName(filePath));
            TryAllowWritesOnFile(sourceFolder);

            if (!File.Exists(targetFile))
            {
                File.Move(filePath, targetFile);
            }
            else
            {
                if (forceReplace)
                {
                    DeleteFile(targetFile);
                    File.Move(filePath, targetFile);
                }
                else
                {
                    throw new InvalidOperationException(
                        "Trying to move a file that already exists: " + targetFile);
                }
            }
        }

        /// <inheritdoc />
        public void Move(string sourceFileName, string destFileName)
        {
            File.Move(sourceFileName, destFileName);
        }

        /// <inheritdoc />
        public byte[] ReadAllBytes(string filePath)
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllBytes(filePath);
            }
            else
            {
                return new byte[0];
            }
        }

        /// <inheritdoc />
        public void WriteAllBytes(string filePath, byte[] bytes)
        {
            File.WriteAllBytes(filePath, bytes);
        }

        /// <inheritdoc />
        public string ReadAllText(string filePath)
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <inheritdoc />
        public string ReadAllText(string filePath, System.Text.Encoding encoding)
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath, encoding);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <inheritdoc />
        public void WriteAllText(string filePath, string fileContent)
        {
            TryAllowWritesOnFile(filePath);
            File.WriteAllText(filePath, fileContent);
        }

        public FileStream Create(string path)
        {
            return File.Create(path);
        }

        public FileStream Create(string path, int bufferSize)
        {
            return File.Create(path, bufferSize);
        }

        public FileStream Create(string path, int bufferSize, FileOptions options)
        {
            return File.Create(path, bufferSize, options);
        }

        public void Copy(string sourcePath, string destinationPath)
        {
            Copy(sourcePath, destinationPath, false);
        }

        public void Copy(string sourcePath, string destinationPath, bool overwrite)
        {
            File.Copy(sourcePath, destinationPath, overwrite);
        }

        public FileStream Open(string path, FileMode mode)
        {
            return File.Open(path, mode);
        }

        public FileStream Open(string path, FileMode mode, FileAccess access)
        {
            return File.Open(path, mode, access);
        }

        /// <summary>Opens a <see cref="T:System.IO.FileStream" /> on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.</summary>
        /// <param name="path">The file to open.</param>
        /// <param name="mode">A <see cref="T:System.IO.FileMode" /> value that specifies whether a file is created if one does not exist, and determines whether the contents of existing files are retained or overwritten.</param>
        /// <param name="access">A <see cref="T:System.IO.FileAccess" /> value that specifies the operations that can be performed on the file.</param>
        /// <param name="share">A <see cref="T:System.IO.FileShare" /> value specifying the type of access other threads have to the file.</param>
        /// <returns>A <see cref="T:System.IO.FileStream" /> on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.</returns>
        public FileStream Open(string path, FileMode mode, FileAccess access, FileShare share)
        {
            return File.Open(path, mode, access, share);
        }

        public FileAttributes GetAttributes(string path)
        {
            return File.GetAttributes(path);
        }

        public DateTime GetCreationTime(string path)
        {
            return File.GetCreationTime(path);
        }

        public DateTime GetCreationTimeUtc(string path)
        {
            return File.GetCreationTimeUtc(path);
        }

        public DateTime GetLastWriteTime(string path)
        {
            return File.GetLastWriteTime(path);
        }

        public DateTime GetLastWriteTimeUtc(string path)
        {
            return File.GetLastWriteTimeUtc(path);
        }

        public DateTime GetLastAccessTime(string path)
        {
            return File.GetLastAccessTime(path);
        }

        public DateTime GetLastAccessTimeUtc(string path)
        {
            return File.GetLastAccessTimeUtc(path);
        }

        private static bool TryAllowWritesOnFile(string path)
        {
            try
            {
                var file = new FileInfo(path);
                if (file.Exists)
                {
                    file.Attributes = FileAttributes.Normal;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
