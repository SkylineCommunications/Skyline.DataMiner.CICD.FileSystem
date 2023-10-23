namespace Skyline.DataMiner.CICD.FileSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class PathCombine
    {
        private const string WindowsSeparator = "\\";
        private const string LinuxSeparator = "/";

        public static string UniversalCombine(string[] paths, HashSet<char> invalidChars)
        {
            // Do not use System.IO.Combine. It will default use the directory separator of the running system.

            if (paths == null)
            {
                throw new ArgumentNullException(nameof(paths));
            }

            if (invalidChars == null)
            {
                throw new ArgumentNullException(nameof(invalidChars));
            }

            List<string> nicePaths = new List<string>();
            foreach (var path in paths)
            {
                var splitPath = path.Split(new[] { WindowsSeparator, LinuxSeparator }, StringSplitOptions.RemoveEmptyEntries);
                nicePaths.AddRange(splitPath.Where(p => !String.IsNullOrWhiteSpace(p)));
            }

            string separator;

            if (paths[0].StartsWith(LinuxSeparator))
            {
                separator = LinuxSeparator;
            }
            else if (nicePaths[0].EndsWith(":"))
            {
                separator = WindowsSeparator;
            }
            else
            {
                separator = System.IO.Path.DirectorySeparatorChar.ToString();
            }

            var result = String.Join(separator, nicePaths);

            if (separator == LinuxSeparator && paths[0].StartsWith(LinuxSeparator))
            {
                // Linux paths need to start with a / only if we already had a /.
                if (!result.StartsWith(LinuxSeparator))
                {
                    result = LinuxSeparator + result;
                }
            }

            foreach (var pathChar in result)
            {
                if (invalidChars.Contains(pathChar))
                {
                    throw new ArgumentException($"Invalid Character Detected in Path {result}", nameof(paths));
                }
            }

            return result;
        }
    }
}
