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
