namespace Skyline.DataMiner.CICD.FileSystem
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

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
        public string ReplaceInvalidCharsForFileName(string filename, OSPlatform platform, string replacement = "_")
        {
            if (filename is null)
            {
                throw new ArgumentNullException(nameof(filename));
            }

            if (replacement is null)
            {
                throw new ArgumentNullException(nameof(replacement));
            }

            if (String.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(filename));
            }

            if (platform == OSPlatform.Windows)
            {
                // !! Can't use Path.GetInvalidFileNameChars() which is different if run on linux.
                // Does mean that not all characters are covered though (unicode, line endings, ...)

                // Regex to remove invalid characters for Windows filenames
                string cleaned = Regex.Replace(filename, @"[<>:""/\\|?*\x00\x01\x02\x03\x04\x05\x06\x07\x08\x09\x0A\x0B\x0C\x0D\x0E\x0F\x10\x11\x12\x13\x14\x15\x16\x17\x18\x19\x1A\x1B\x1C\x1D\x1E\x1F]", replacement);

                // Trim trailing dots and spaces, as they are also invalid in filenames
                cleaned = cleaned.TrimEnd('.', ' ');

                return cleaned;
            }
            else
            {
                return String.Join(replacement, filename.Split(Path.GetInvalidFileNameChars()));
            }
        }

        /// <inheritdoc />
        public string ReplaceInvalidCharsForFileName(string filename, string replacement = "_")
        {
            return ReplaceInvalidCharsForFileName(filename, OSPlatform.Linux, replacement);
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