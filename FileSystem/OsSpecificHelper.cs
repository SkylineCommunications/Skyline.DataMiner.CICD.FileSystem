namespace Skyline.DataMiner.CICD.FileSystem
{
    using System;
    using System.Text.RegularExpressions;

    internal sealed class OsSpecificHelper : IOsSpecificHelper
    {
        public string ReplaceInvalidCharsForFileNameForWindows(string filename, string replacement = "_")
        {
            if (filename is null)
            {
                throw new ArgumentNullException(nameof(filename));
            }

            if (String.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(filename));
            }

            if (replacement is null)
            {
                throw new ArgumentNullException(nameof(replacement));
            }

            // !! Can't use ReplaceInvalidCharsForFileName as that uses the Path.GetInvalidFileNameChars() which is different if run on linux.
            // Does mean that not all characters are covered (unicode, line endings, ...)

            // Regex to remove invalid characters for Windows filenames
            string cleaned = Regex.Replace(filename, @"[<>:""/\\|?*]", replacement);

            // Trim trailing dots and spaces, as they are also invalid in filenames
            cleaned = cleaned.TrimEnd('.', ' ');

            return cleaned;
        }
    }
}