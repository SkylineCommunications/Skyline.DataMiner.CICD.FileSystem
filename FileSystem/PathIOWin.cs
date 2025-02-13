namespace Skyline.DataMiner.CICD.FileSystem
{
    using System;
    using System.Collections.Generic;

    using Alphaleonis.Win32.Filesystem;

    /// <inheritdoc />
    internal sealed class PathIOWin : IPathIO
    {
        private static readonly HashSet<char> InvalidChars = new HashSet<char>(Path.GetInvalidPathChars());

        /// <inheritdoc />
        public char AltDirectorySeparatorChar => Path.AltDirectorySeparatorChar;

        /// <inheritdoc />
        public char DirectorySeparatorChar => Path.DirectorySeparatorChar;

        /// <inheritdoc />
        public char PathSeparator => Path.PathSeparator;

        /// <inheritdoc />
        public char VolumeSeparatorChar => Path.VolumeSeparatorChar;

        /// <inheritdoc />
        public string ChangeExtension(string path, string extension) => Path.ChangeExtension(path, extension);

        /// <inheritdoc />
        public string Combine(string path1, string path2) => PathSplitCombined(path1, path2);

        /// <inheritdoc />
        public string Combine(string path1, string path2, string path3) => PathSplitCombined(path1, path2, path3);

        /// <inheritdoc />
        public string Combine(string path1, string path2, string path3, string path4) => PathSplitCombined(path1, path2, path3, path4);

        /// <inheritdoc />
        public string Combine(params string[] paths) => PathSplitCombined(paths);

        /// <inheritdoc />
        public string GetDirectoryName(string path) => Path.GetDirectoryName(path);

        /// <inheritdoc />
        public string GetExtension(string path) => Path.GetExtension(path);

        /// <inheritdoc />
        public string GetFileName(string path) => Path.GetFileName(path);

        /// <inheritdoc />
        public string GetFileNameWithoutExtension(string path) => Path.GetFileNameWithoutExtension(path);

        /// <inheritdoc />
        public string GetFullPath(string path) => Path.GetFullPath(path);

        /// <inheritdoc />
        public char[] GetInvalidFileNameChars() => Path.GetInvalidFileNameChars();

        /// <inheritdoc />
        public char[] GetInvalidPathChars() => Path.GetInvalidPathChars();

        /// <inheritdoc />
        public string GetPathRoot(string path) => Path.GetPathRoot(path);

        /// <inheritdoc />
        public string GetRandomFileName() => Path.GetRandomFileName();

        /// <inheritdoc />
        public string GetTempFileName() => Path.GetTempFileName();

        /// <inheritdoc />
        public string GetTempPath() => Path.GetTempPath();

        /// <inheritdoc />
        public bool HasExtension(string path) => Path.HasExtension(path);

        /// <inheritdoc />
        public bool IsPathRooted(string path) => Path.IsPathRooted(path);

        /// <inheritdoc />
        public string ReplaceInvalidCharsForFileName(string filename, string replacement = "_")
        {
            string cleaned = String.Join(replacement, filename.Split(Path.GetInvalidFileNameChars()));

            // Trim trailing dots and spaces, as they are also invalid in filenames
            cleaned = cleaned.TrimEnd('.', ' ');

            return cleaned;
        }

        private static string PathSplitCombined(params string[] paths)
        {
            return PathCombine.UniversalCombine(paths, InvalidChars);
        }
    }
}