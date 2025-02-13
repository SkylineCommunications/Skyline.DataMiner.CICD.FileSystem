namespace Skyline.DataMiner.CICD.FileSystem
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <inheritdoc />
    internal sealed class PathIOLinux : IPathIO
    {
        private static readonly HashSet<char> InvalidChars = new HashSet<char>(Path.GetInvalidPathChars());
        private const string WindowsSeparator = "\\";
        private const string LinuxSeparator = "/";

        /// <inheritdoc />
        public char AltDirectorySeparatorChar => Path.AltDirectorySeparatorChar;

        /// <inheritdoc />
        public char DirectorySeparatorChar => Path.DirectorySeparatorChar;

        /// <inheritdoc />
        public char PathSeparator => Path.PathSeparator;

        /// <inheritdoc />
        public char VolumeSeparatorChar => Path.VolumeSeparatorChar;

        /// <inheritdoc />
        public string ChangeExtension(string path, string extension) => ChangeExtensionWrapper(Path.ChangeExtension, path, extension);

        /// <inheritdoc />
        public string Combine(string path1, string path2) => PathSplitCombined(path1, path2);

        /// <inheritdoc />
        public string Combine(string path1, string path2, string path3) => PathSplitCombined(path1, path2, path3);

        /// <inheritdoc />
        public string Combine(string path1, string path2, string path3, string path4) => PathSplitCombined(path1, path2, path3, path4);

        /// <inheritdoc />
        public string Combine(params string[] paths) => PathSplitCombined(paths);

        /// <inheritdoc />
        public string GetDirectoryName(string path) => SeparatorWrapper(Path.GetDirectoryName, path, true);

        /// <inheritdoc />
        public string GetExtension(string path) => SeparatorWrapper(Path.GetExtension, path, false);

        /// <inheritdoc />
        public string GetFileName(string path) => SeparatorWrapper(Path.GetFileName, path, false);

        /// <inheritdoc />
        public string GetFileNameWithoutExtension(string path) => SeparatorWrapper(Path.GetFileNameWithoutExtension, path, false);

        /// <inheritdoc />
        public string GetFullPath(string path) => SeparatorWrapper(Path.GetFullPath, path, true);

        /// <inheritdoc />
        public char[] GetInvalidFileNameChars() => Path.GetInvalidFileNameChars();

        /// <inheritdoc />
        public char[] GetInvalidPathChars() => Path.GetInvalidPathChars();

        /// <inheritdoc />
        public string GetPathRoot(string path) => SeparatorWrapper(Path.GetPathRoot, path, true);

        /// <inheritdoc />
        public string GetRandomFileName() => Path.GetRandomFileName();

        /// <inheritdoc />
        public string GetTempFileName() => Path.GetTempFileName();

        /// <inheritdoc />
        public string GetTempPath() => Path.GetTempPath();

        /// <inheritdoc />
        public bool HasExtension(string path) => SeparatorWrapper(Path.HasExtension, path);

        /// <inheritdoc />
        public bool IsPathRooted(string path) => SeparatorWrapper(Path.IsPathRooted, path);

        /// <inheritdoc />
        public string ReplaceInvalidCharsForFileName(string filename, string replacement = "_")
        {
            string cleaned = String.Join(replacement, filename.Split(Path.GetInvalidFileNameChars()));

            // Trim trailing dots and spaces, as they are also invalid in filenames
            cleaned = cleaned.TrimEnd('.', ' ');

            return cleaned;
        }

        private static string ChangeExtensionWrapper(Func<string, string, string> changeExtensionCall, string path, string extension)
        {
            if (changeExtensionCall == null)
            {
                throw new ArgumentNullException(nameof(changeExtensionCall));
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (extension == null)
            {
                throw new ArgumentNullException(nameof(extension));
            }

            string finalResult;
            var removedWindowsRoot = path.Split(':');
            if (removedWindowsRoot.Length > 1)
            {
                var convertedToUnix = removedWindowsRoot[1].Replace(WindowsSeparator, LinuxSeparator);

                if (!convertedToUnix.StartsWith(LinuxSeparator))
                {
                    convertedToUnix = LinuxSeparator + convertedToUnix;
                }

                var unixResult = changeExtensionCall(convertedToUnix, extension);
                finalResult = removedWindowsRoot[0] + ":" + unixResult.Replace(LinuxSeparator, WindowsSeparator);
            }
            else
            {
                if (path.Contains(WindowsSeparator))
                {
                    var convertedToUnix = path.Replace(WindowsSeparator, LinuxSeparator);
                    var unixResult = changeExtensionCall(convertedToUnix, extension);
                    finalResult = unixResult.Replace(LinuxSeparator, WindowsSeparator);
                }
                else
                {
                    finalResult = changeExtensionCall(path, extension);
                    finalResult = finalResult.Replace(WindowsSeparator, LinuxSeparator);
                }
            }

            return finalResult;
        }

        private static string PathSplitCombined(params string[] paths)
        {
            return PathCombine.UniversalCombine(paths, InvalidChars);
        }

        private static string SeparatorWrapper(Func<string, string> pathMethod, string path, bool returnWithRoot)
        {
            if (pathMethod == null)
            {
                throw new ArgumentNullException(nameof(pathMethod));
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            string finalResult;
            var removedWindowsRoot = path.Split(':');
            if (removedWindowsRoot.Length > 1)
            {
                var convertedToUnix = removedWindowsRoot[1].Replace(WindowsSeparator, LinuxSeparator);
                if (!convertedToUnix.StartsWith(LinuxSeparator))
                {
                    convertedToUnix = LinuxSeparator + convertedToUnix;
                }

                var unixResult = pathMethod(convertedToUnix);

                if (returnWithRoot)
                {
                    finalResult = removedWindowsRoot[0] + ":" + unixResult.Replace(LinuxSeparator, WindowsSeparator);
                }
                else
                {
                    finalResult = unixResult.Replace(LinuxSeparator, WindowsSeparator);
                }
            }
            else
            {
                if (path.Contains(WindowsSeparator))
                {
                    var convertedToUnix = path.Replace(WindowsSeparator, LinuxSeparator);
                    var unixResult = pathMethod(convertedToUnix);
                    finalResult = unixResult.Replace(LinuxSeparator, WindowsSeparator);
                }
                else
                {
                    finalResult = pathMethod(path);
                    finalResult = finalResult.Replace(WindowsSeparator, LinuxSeparator);
                }
            }

            return finalResult;
        }

        private static bool SeparatorWrapper(Func<string, bool> pathMethod, string path)
        {
            if (pathMethod == null)
            {
                throw new ArgumentNullException(nameof(pathMethod));
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            bool finalResult;
            var removedWindowsRoot = path.Split(':');
            if (removedWindowsRoot.Length > 1)
            {
                var convertedToUnix = removedWindowsRoot[1].Replace(WindowsSeparator, LinuxSeparator);

                if (!convertedToUnix.StartsWith(LinuxSeparator))
                {
                    convertedToUnix = LinuxSeparator + convertedToUnix;
                }

                finalResult = pathMethod(convertedToUnix);
            }
            else
            {
                if (path.Contains(WindowsSeparator))
                {
                    var convertedToUnix = path.Replace(WindowsSeparator, LinuxSeparator);
                    finalResult = pathMethod(convertedToUnix);
                }
                else
                {
                    finalResult = pathMethod(path);
                }
            }

            return finalResult;
        }
    }
}