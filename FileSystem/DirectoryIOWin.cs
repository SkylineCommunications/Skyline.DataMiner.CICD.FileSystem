namespace Skyline.DataMiner.CICD.FileSystem
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Security.AccessControl;

    using Skyline.DataMiner.CICD.FileSystem.DirectoryInfoWrapper;

    using Directory = Alphaleonis.Win32.Filesystem.Directory;
    using DirectoryInfo = Alphaleonis.Win32.Filesystem.DirectoryInfo;
    using FileInfo = Alphaleonis.Win32.Filesystem.FileInfo;
    using Path = Alphaleonis.Win32.Filesystem.Path;

    /// <inheritdoc />
    internal sealed class DirectoryIOWin : IDirectoryIO
    {
        private readonly IFileIO _fileIo;

        internal DirectoryIOWin(IFileIO fileIo)
        {
            _fileIo = fileIo ?? throw new ArgumentNullException(nameof(fileIo));
        }

        /// <inheritdoc />
        public void CopyRecursive(string sourceDirectory, string targetDirectory, string[] ignoreNamesWith = null)
        {
            DirectoryInfo diSource;
            DirectoryInfo diTarget;
            if (Path.IsPathRooted(sourceDirectory)
                && !Path.GetPathRoot(sourceDirectory).Equals(
                    Path.DirectorySeparatorChar.ToString(),
                    StringComparison.Ordinal))
            {
                diSource = new DirectoryInfo(@"\\?\" + sourceDirectory);
            }
            else
            {
                diSource = new DirectoryInfo(sourceDirectory);
            }

            if (Path.IsPathRooted(targetDirectory)
                && !Path.GetPathRoot(targetDirectory).Equals(
                    Path.DirectorySeparatorChar.ToString(),
                    StringComparison.Ordinal))
            {
                diTarget = new DirectoryInfo(@"\\?\" + targetDirectory);
            }
            else
            {
                diTarget = new DirectoryInfo(targetDirectory);
            }

            CopyAll(diSource, diTarget, ignoreNamesWith);
        }

        /// <inheritdoc />
        public void CopyRecursiveFromShare(string sourceDirectory, string targetDirectory, string[] ignoreNamesWith = null)
        {
            DirectoryInfo diSource;
            DirectoryInfo diTarget;

            diSource = new DirectoryInfo(sourceDirectory);

            if (Path.IsPathRooted(targetDirectory) && !Path.GetPathRoot(targetDirectory).Equals(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
            {
                diTarget = new DirectoryInfo(@"\\?\" + targetDirectory);
            }
            else
            {
                diTarget = new DirectoryInfo(targetDirectory);
            }

            CopyAll(diSource, diTarget, ignoreNamesWith);
        }

        /// <inheritdoc />
        public string CreateTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }

        /// <inheritdoc />
        public void DeleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                TryAllowWritesOnDirectory(path);
                Directory.Delete(path, true);
            }
        }

        /// <inheritdoc />
        public IEnumerable<string> EnumerateFiles(string path)
        {
            return Directory.EnumerateFiles(path);
        }

        /// <inheritdoc />
        public IEnumerable<string> EnumerateFiles(string path, string searchPattern)
        {
            return Directory.EnumerateFiles(path, searchPattern);
        }

        /// <inheritdoc />
        public IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
        {
            return Directory.EnumerateFiles(path, searchPattern, searchOption);
        }

        /// <inheritdoc />
        public bool Exists(string path)
        {
            return Directory.Exists(path);
        }

        /// <inheritdoc />
        public IEnumerable<string> EnumerateDirectories(string path)
        {
            return Directory.EnumerateDirectories(path);
        }

        /// <inheritdoc />
        [Obsolete("Replace with IPathIO.GetDirectoryName")]
        public string GetParentDirectory(string pathToRootDirectory)
        {
            return FileSystem.Instance.Path.GetDirectoryName(pathToRootDirectory);
        }

        /// <inheritdoc />
        public string[] GetFiles(string path)
        {
            path = Path.GetFullPath(path);
            return Directory.GetFiles(path);
        }

        /// <inheritdoc />
        public string[] GetFiles(string path, string searchPattern)
        {
            path = Path.GetFullPath(path);
            return Directory.GetFiles(path, searchPattern);
        }

        /// <inheritdoc />
        public string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            path = Path.GetFullPath(path);
            return Directory.GetFiles(path, searchPattern, searchOption);
        }

        /// <inheritdoc />
        public string LocalSvnPathToSvnServer(string localFolderPath)
        {
            if (!String.IsNullOrWhiteSpace(localFolderPath))
            {
                return "svn:" + localFolderPath.Replace("D:\\SVN\\SystemEngineering\\", "https:\\\\svn.skyline.be\\svn\\SystemEngineering\\").Trim();
            }
            else
            {
                return String.Empty;
            }
        }

        /// <inheritdoc />
        public void MoveDirectory(string source, string target, bool move = true, bool forceReplace = false)
        {
            string sourcePath = source.TrimEnd('\\', ' ');
            string targetPath = target.TrimEnd('\\', ' ');
            TryAllowWritesOnDirectory(sourcePath);
            TryAllowWritesOnDirectory(targetPath);
            var files = Directory.EnumerateFiles(sourcePath, "*", SearchOption.AllDirectories)
                .GroupBy(s => Path.GetDirectoryName(s));

            foreach (var folder in files)
            {
                string targetFolder = folder.Key.Replace(sourcePath, targetPath);
                if (targetFolder.Contains(".svn") || targetFolder.Contains(".git"))
                    continue;
                Directory.CreateDirectory(targetFolder);

                foreach (string file in folder)
                {
                    if (file.EndsWith("Jenkinsfile"))
                        continue;
                    _fileIo.MoveFile(file, sourcePath, targetFolder, forceReplace);
                }
            }

            if (move)
            {
                DeleteDirectory(source);
            }
        }

        /// <inheritdoc />
        public void Unzip(string zipPath, string destinationDir)
        {
            TryAllowWritesOnDirectory(zipPath);
            TryAllowWritesOnDirectory(destinationDir);
            ZipFile.ExtractToDirectory(zipPath, destinationDir);
        }

        /// <inheritdoc />
        public bool IsDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                FileAttributes attr = File.GetAttributes(path);

                if (!attr.HasFlag(FileAttributes.Directory))
                {
                    return false;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc />
        public void AllowWritesOnDirectory(string path)
        {
            FileSecurity fileSec = new FileSecurity(path, AccessControlSections.All);
            if (AccessPermissions.HasWritePermissionOnDir(fileSec))
                return;

            if (String.IsNullOrWhiteSpace(path))
                return;
            var directory = new DirectoryInfo(path) { Attributes = System.IO.FileAttributes.Normal };
            foreach (var info in directory.GetFileSystemInfos("*", System.IO.SearchOption.AllDirectories))
            {
                info.Attributes = System.IO.FileAttributes.Normal;
            }

            if (!AccessPermissions.WaitOnWritePermission(fileSec, new TimeSpan(0, 0, 10)))
            {
                throw new TimeoutException("Could not get write access on: " + path);
            }
        }

        /// <inheritdoc />
        public bool TryAllowWritesOnDirectory(string path)
        {
            try
            {
                AllowWritesOnDirectory(path);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IDirectoryInfoIO CreateDirectory(string path)
        {
            var dir = Directory.CreateDirectory(path);
            return new DirectoryInfoIOWin(dir);
        }

        private void CopyAll(DirectoryInfo source, DirectoryInfo target, string[] ignoreNamesWith = null)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                if (ignoreNamesWith != null && ShouldBeIgnored(fi.Name, ignoreNamesWith))
                    continue;
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                if (ignoreNamesWith != null && ShouldBeIgnored(diSourceSubDir.Name, ignoreNamesWith))
                    continue;
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir, ignoreNamesWith);
            }
        }

        private bool ShouldBeIgnored(string name, string[] ignoreNamesWith)
        {
            if (ignoreNamesWith != null)
            {
                for (var i = 0; i < ignoreNamesWith.Length; i++)
                {
                    string nameToIgnore = ignoreNamesWith[i];
                    if (name.Contains(nameToIgnore))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}