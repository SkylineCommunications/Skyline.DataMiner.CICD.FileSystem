namespace Skyline.DataMiner.CICD.FileSystem
{
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
        string ReplaceInvalidCharsForFileNameForWindows(string filename, string replacement = "_");
    }
}