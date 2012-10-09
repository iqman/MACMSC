using System;
using System.Reflection;
using System.IO;

namespace Shared
{
    /// <summary>
    /// A general purpose file path utility class.
    /// This class is able to prepend the path for a file with the path for the assembly which called this metod.
    /// </summary>
    public static class FilePathUtility
    {
        /// <summary>
        /// Get the full path to the specified file by prepending it with the path of the calling assembly.
        /// </summary>
        /// <param name="fileName">Name of file</param>
        /// <returns>Full path of the file</returns>
        public static string GetFullPath(string fileName)
        {
            Assembly asm = Assembly.GetCallingAssembly();
            string str = asm.GetName().CodeBase;
            string fullName = Path.GetDirectoryName(str);

            // the full name can be a URI (eg file://...) but some of the
            // loader functions can't parse that type of path. Hence we get
            // a path that starts with a drive letter.
            Uri uri = new Uri(Path.Combine(fullName, fileName));
            return uri.LocalPath;
        }

        /// <summary>
        /// Gets the full path to the directory of an assembly.
        /// </summary>
        /// <param name="assembly">Assembly to get directory for</param>
        /// <returns>Returns the fulle path to the directory of the assembly</returns>
        public static string GetFullPath(Assembly assembly)
        {
            string str = assembly.GetName().CodeBase;
            string fullName = Path.GetDirectoryName(str);

            Uri uri = new Uri(fullName);
            return uri.LocalPath;
        }
    }
}
