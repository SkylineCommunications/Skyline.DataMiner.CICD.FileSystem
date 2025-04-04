﻿namespace Skyline.DataMiner.CICD.FileSystem
{
    using System;
    using System.Diagnostics;
    using File = Alphaleonis.Win32.Filesystem.File;
    using FileInfo = Alphaleonis.Win32.Filesystem.FileInfo;
    using Path = Alphaleonis.Win32.Filesystem.Path;

    /// <inheritdoc />
    internal sealed class FileIOWin : IFileIO
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

            if (!File.Exists(targetFile))
            {
                TryAllowWritesOnFile(sourceFolder);
                File.Move(filePath, targetFile);
            }
            else
            {
                if (forceReplace)
                {
                    TryAllowWritesOnFile(sourceFolder);
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
            TryAllowWritesOnFile(filePath);
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
            TryAllowWritesOnFile(filePath);
            File.WriteAllBytes(filePath, bytes);
        }

        /// <inheritdoc />
        public string ReadAllText(string filePath)
        {
            TryAllowWritesOnFile(filePath);
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
            TryAllowWritesOnFile(filePath);
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

        public System.IO.FileStream Create(string path)
        {
            return File.Create(path);
        }

        public System.IO.FileStream Create(string path, int bufferSize)
        {
            return File.Create(path, bufferSize);
        }

        public System.IO.FileStream Create(string path, int bufferSize, System.IO.FileOptions options)
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

        public System.IO.FileStream Open(string path, System.IO.FileMode mode)
        {
            return File.Open(path, mode);
        }

        public System.IO.FileStream Open(string path, System.IO.FileMode mode, System.IO.FileAccess access)
        {
            return File.Open(path, mode, access);
        }

        public System.IO.FileStream Open(string path, System.IO.FileMode mode, System.IO.FileAccess access, System.IO.FileShare share)
        {
            return File.Open(path, mode, access, share);
        }

        public System.IO.FileAttributes GetAttributes(string path)
        {
            return File.GetAttributes(path);
        }

        private static bool TryAllowWritesOnFile(string path)
        {
            try
            {
                // DirectoryInfo can be created from a file path
                //FileSecurity fileSec = new FileSecurity(path,AccessControlSections.All);
                //if (AccessPermissions.HasWritePermissionOnDir(fileSec)) return;
                var file = new FileInfo(path);

                if (file.Exists)
                {
                    file.Attributes = System.IO.FileAttributes.Normal;
                }

                //if (!AccessPermissions.WaitOnWritePermission(fileSec, new TimeSpan(0, 0, 10)))
                //{
                //    throw new TimeoutException("Could not get write access on: " + path);
                //}

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}