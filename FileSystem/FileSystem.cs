namespace Skyline.DataMiner.CICD.FileSystem
{
    using System.Runtime.InteropServices;

    /// <summary>
    /// Represents a way to have cross-platform functionality regarding <see cref="System.IO.Directory"/>,
    /// <see cref="System.IO.File"/> and <see cref="System.IO.Path"/>.
    /// </summary>
    public class FileSystem : IFileSystem
    {
        private static IFileSystem _instance;

        /// <summary>
        /// Creates a new FileSystem instance.
        /// </summary>
        public FileSystem()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                File = new FileIOWin();
                Path = new PathIOWin();
                Directory = new DirectoryIOWin(File);
            }
            else
            {
                File = new FileIOLinux();
                Path = new PathIOLinux();
                Directory = new DirectoryIOLinux(File);
            }
        }

        /// <summary>
        /// Gets the singleton instance of <see cref="IFileSystem"/> created using the current Runtime Operating System.
        /// </summary>
        public static IFileSystem Instance => _instance ?? (_instance = new FileSystem());

        /// <summary>Exposes static methods for creating, moving, and enumerating through directories and subdirectories.</summary>
        public IDirectoryIO Directory { get; }

        /// <summary>Provides static methods for the creation, copying, deletion, moving, and opening of a single file, and aids in the creation of <see cref="System.IO.FileStream" /> objects.</summary>
        public IFileIO File { get; }

        /// <summary>Performs operations on <see cref="System.String" /> instances that contain file or directory path information. These operations are performed in a cross-platform manner.</summary>
        public IPathIO Path { get; }

        /// <inheritdoc />
        public IOsSpecificHelper OsSpecificHelper { get; } = new OsSpecificHelper();
    }
}