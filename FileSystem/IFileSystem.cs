namespace Skyline.DataMiner.CICD.FileSystem
{
    /// <summary>
    /// Represents a way to have cross-platform functionality regarding <see cref="System.IO.Directory"/>,
    /// <see cref="System.IO.File"/> and <see cref="System.IO.Path"/>.
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>Exposes static methods for creating, moving, and enumerating through directories and subdirectories.</summary>
        IDirectoryIO Directory { get; }

        /// <summary>Provides static methods for the creation, copying, deletion, moving, and opening of a single file, and aids in the creation of <see cref="System.IO.FileStream" /> objects.</summary>
        IFileIO File { get; }

        /// <summary>Performs operations on <see cref="System.String" /> instances that contain file or directory path information. These operations are performed in a cross-platform manner.</summary>
        IPathIO Path { get; }

        /// <summary>
        /// Represents a way to have platform-specific functionality.
        /// </summary>
        IOsSpecificHelper OsSpecificHelper { get; }
    }
}