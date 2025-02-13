namespace Skyline.DataMiner.CICD.FileSystem
{
    /// <summary>Performs operations on <see cref="System.String" /> instances that contain file or directory path information. These operations are performed in a cross-platform manner.</summary>
    public interface IPathIO
    {
        /// <summary>Provides a platform-specific alternate character used to separate directory levels in a path string that reflects a hierarchical file system organization.</summary>
        char AltDirectorySeparatorChar { get; }

        /// <summary>Provides a platform-specific character used to separate directory levels in a path string that reflects a hierarchical file system organization.</summary>
        char DirectorySeparatorChar { get; }

        /// <summary>A platform-specific separator character used to separate path strings in environment variables.</summary>
        char PathSeparator { get; }

        /// <summary>Provides a platform-specific volume separator character.</summary>
        char VolumeSeparatorChar { get; }

        /// <summary>Changes the extension of a path string.</summary>
        /// <param name="path">The path information to modify.</param>
        /// <param name="extension">The new extension (with or without a leading period). Specify <see langword="null" /> to remove an existing extension from <paramref name="path" />.</param>
        /// <exception cref="System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="GetInvalidPathChars" />.</exception>
        /// <returns>The modified path information.
        /// 
        /// On Windows-based desktop platforms, if <paramref name="path" /> is <see langword="null" /> or an empty string (""), the path information is returned unmodified. If <paramref name="extension" /> is <see langword="null" />, the returned string contains the specified path with its extension removed. If <paramref name="path" /> has no extension, and <paramref name="extension" /> is not <see langword="null" />, the returned path string contains <paramref name="extension" /> appended to the end of <paramref name="path" />.</returns>
        string ChangeExtension(string path, string extension);

        /// <summary>Combines an array of strings into a path.</summary>
        /// <param name="paths">An array of parts of the path.</param>
        /// <exception cref="System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: One of the strings in the array contains one or more of the invalid characters defined in <see cref="GetInvalidPathChars" />.</exception>
        /// <exception cref="System.ArgumentNullException">One of the strings in the array is <see langword="null" />.</exception>
        /// <returns>The combined paths.</returns>
        string Combine(params string[] paths);

        /// <summary>Combines two strings into a path.</summary>
        /// <param name="path1">The first path to combine.</param>
        /// <param name="path2">The second path to combine.</param>
        /// <exception cref="System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path1" /> or <paramref name="path2" /> contains one or more of the invalid characters defined in <see cref="GetInvalidPathChars" />.</exception>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="path1" /> or <paramref name="path2" /> is <see langword="null" />.</exception>
        /// <returns>The combined paths. If one of the specified paths is a zero-length string, this method returns the other path. If <paramref name="path2" /> contains an absolute path, this method returns <paramref name="path2" />.</returns>
        string Combine(string path1, string path2);

        /// <summary>Combines three strings into a path.</summary>
        /// <param name="path1">The first path to combine.</param>
        /// <param name="path2">The second path to combine.</param>
        /// <param name="path3">The third path to combine.</param>
        /// <exception cref="System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path1" />, <paramref name="path2" />, or <paramref name="path3" /> contains one or more of the invalid characters defined in <see cref="GetInvalidPathChars" />.</exception>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="path1" />, <paramref name="path2" />, or <paramref name="path3" /> is <see langword="null" />.</exception>
        /// <returns>The combined paths.</returns>
        string Combine(string path1, string path2, string path3);

        /// <summary>Combines four strings into a path.</summary>
        /// <param name="path1">The first path to combine.</param>
        /// <param name="path2">The second path to combine.</param>
        /// <param name="path3">The third path to combine.</param>
        /// <param name="path4">The fourth path to combine.</param>
        /// <exception cref="System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path1" />, <paramref name="path2" />, <paramref name="path3" />, or <paramref name="path4" /> contains one or more of the invalid characters defined in <see cref="GetInvalidPathChars" />.</exception>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="path1" />, <paramref name="path2" />, <paramref name="path3" />, or <paramref name="path4" /> is <see langword="null" />.</exception>
        /// <returns>The combined paths.</returns>
        string Combine(string path1, string path2, string path3, string path4);

        /// <summary>Returns the directory information for the specified path.</summary>
        /// <param name="path">The path of a file or directory.</param>
        /// <exception cref="System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: The <paramref name="path" /> parameter contains invalid characters, is empty, or contains only whitespaces.</exception>
        /// <exception cref="System.IO.PathTooLongException">The <paramref name="path" /> parameter is longer than the system-defined maximum length.
        /// 
        /// Note: In .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="System.IO.IOException" />, instead.</exception>
        /// <returns>Directory information for <paramref name="path" />, or <see langword="null" /> if <paramref name="path" /> denotes a root directory or is null. Returns <see cref="System.String.Empty" /> if <paramref name="path" /> does not contain directory information.</returns>
        string GetDirectoryName(string path);

        /// <summary>Returns the extension (including the period ".") of the specified path string.</summary>
        /// <param name="path">The path string from which to get the extension.</param>
        /// <exception cref="System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="GetInvalidPathChars" />.</exception>
        /// <returns>The extension of the specified path (including the period "."), or <see langword="null" />, or <see cref="System.String.Empty" />. If <paramref name="path" /> is <see langword="null" />, <see cref="GetExtension(System.String)" /> returns <see langword="null" />. If <paramref name="path" /> does not have extension information, <see cref="GetExtension(System.String)" /> returns <see cref="System.String.Empty" />.</returns>
        string GetExtension(string path);

        /// <summary>Returns the file name and extension of the specified path string.</summary>
        /// <param name="path">The path string from which to obtain the file name and extension.</param>
        /// <exception cref="System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="GetInvalidPathChars" />.</exception>
        /// <returns>The characters after the last directory separator character in <paramref name="path" />. If the last character of <paramref name="path" /> is a directory or volume separator character, this method returns <see cref="System.String.Empty" />. If <paramref name="path" /> is <see langword="null" />, this method returns <see langword="null" />.</returns>
        string GetFileName(string path);

        /// <summary>Returns the file name of the specified path string without the extension.</summary>
        /// <param name="path">The path of the file.</param>
        /// <exception cref="System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="GetInvalidPathChars" />.</exception>
        /// <returns>The string returned by <see cref="GetFileName(System.String)" />, minus the last period (.) and all characters following it.</returns>
        string GetFileNameWithoutExtension(string path);

        /// <summary>Returns the absolute path for the specified path string.</summary>
        /// <param name="path">The file or directory for which to obtain absolute path information.</param>
        /// <exception cref="System.ArgumentException">
        ///         <paramref name="path" /> is a zero-length string, contains only whitespace, or contains one or more of the invalid characters defined in <see cref="GetInvalidPathChars" />.
        /// 
        /// -or-
        /// 
        ///  The system could not retrieve the absolute path.</exception>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permissions.</exception>
        /// <exception cref="System.ArgumentNullException">
        /// <paramref name="path" /> is <see langword="null" />.</exception>
        /// <exception cref="System.NotSupportedException">
        /// <paramref name="path" /> contains a colon (":") that is not part of a volume identifier (for example, "c:\").</exception>
        /// <exception cref="System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
        /// <returns>The fully qualified location of <paramref name="path" />, such as "C:\MyFile.txt".</returns>
        string GetFullPath(string path);

        /// <summary>Gets an array containing the characters that are not allowed in file names.</summary>
        /// <returns>An array containing the characters that are not allowed in file names.</returns>
        char[] GetInvalidFileNameChars();

        /// <summary>Gets an array containing the characters that are not allowed in path names.</summary>
        /// <returns>An array containing the characters that are not allowed in path names.</returns>
        char[] GetInvalidPathChars();

        /// <summary>Gets the root directory information from the path contained in the specified string.</summary>
        /// <param name="path">A string containing the path from which to obtain root directory information.</param>
        /// <exception cref="System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="GetInvalidPathChars" />.
        /// 
        /// -or-
        /// 
        /// .NET Framework only: <see cref="System.String.Empty" /> was passed to <paramref name="path" />.</exception>
        /// <returns>The root directory of <paramref name="path" /> if it is rooted.
        /// 
        ///  -or-
        /// 
        /// <see cref="System.String.Empty" /> if <paramref name="path" /> does not contain root directory information.
        /// 
        ///  -or-
        /// 
        /// <see langword="null" /> if <paramref name="path" /> is <see langword="null" /> or is effectively empty.</returns>
        string GetPathRoot(string path);

        /// <summary>Returns a random folder name or file name.</summary>
        /// <returns>A random folder name or file name.</returns>
        string GetRandomFileName();

        /// <summary>Creates a uniquely named, zero-byte temporary file on disk and returns the full path of that file.</summary>
        /// <exception cref="System.IO.IOException">An I/O error occurs, such as no unique temporary file name is available.
        /// 
        /// -or-
        /// 
        ///  This method was unable to create a temporary file.</exception>
        /// <returns>The full path of the temporary file.</returns>
        string GetTempFileName();

        /// <summary>Returns the path of the current user's temporary folder.</summary>
        /// <exception cref="System.Security.SecurityException">The caller does not have the required permissions.</exception>
        /// <returns>The path to the temporary folder, ending with a  <see cref="DirectorySeparatorChar" />.</returns>
        string GetTempPath();

        /// <summary>Determines whether a path includes a file name extension.</summary>
        /// <param name="path">The path to search for an extension.</param>
        /// <exception cref="System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="GetInvalidPathChars" />.</exception>
        /// <returns>
        /// <see langword="true" /> if the characters that follow the last directory separator (\ or /) or volume separator (:) in the path include a period (.) followed by one or more characters; otherwise, <see langword="false" />.</returns>
        bool HasExtension(string path);

        /// <summary>Returns a value indicating whether the specified path string contains a root.</summary>
        /// <param name="path">The path to test.</param>
        /// <exception cref="System.ArgumentException">.NET Framework and .NET Core versions older than 2.1: <paramref name="path" /> contains one or more of the invalid characters defined in <see cref="GetInvalidPathChars" />.</exception>
        /// <returns>
        /// <see langword="true" /> if <paramref name="path" /> contains a root; otherwise, <see langword="false" />.</returns>
        bool IsPathRooted(string path);

        /// <summary>
        /// Replaces invalid characters in a filename with a replacement character.
        /// </summary>
        /// <param name="filename">The file name with extension included</param>
        /// <param name="replacement">Replacement character.</param>
        /// <returns>Cleaned string without invalid characters</returns>
        string ReplaceInvalidCharsForFileName(string filename, string replacement = "_");
    }
}