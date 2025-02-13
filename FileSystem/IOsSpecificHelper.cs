namespace Skyline.DataMiner.CICD.FileSystem
{
    using System;

    /// <summary>
    /// Represents a way to have platform-specific functionality.
    /// </summary>
    public interface IOsSpecificHelper
    {
        /// <summary>
        /// Replaces invalid characters in a filename with a replacement character.
        /// </summary>
        /// <param name="filename">The file name with extension included</param>
        /// <param name="replacement">Replacement character.</param>
        /// <returns>Cleaned string without invalid characters</returns>
        /// <exception cref="ArgumentNullException"><paramref name="filename" /> or <paramref name="replacement" /> is <see langword="null" />.</exception>
        /// <exception cref="ArgumentException">Value cannot be null or whitespace.</exception>
        string ReplaceInvalidCharsForFileNameForWindows(string filename, string replacement = "_");
    }
}