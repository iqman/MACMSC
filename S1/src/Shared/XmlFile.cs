using System.IO;
using System.Xml.Serialization;

namespace Shared
{
    /// <summary>
    /// Encapsulation of reading from/writing to an xml file.
    /// </summary>
    public static class XmlFile
    {
        /// <summary>
        /// Loads an object from a file.
        /// If the specified file does not exist, null will be returned.
        /// </summary>
        public static T ReadFile<T>(string path) where T : class
        {
            T settings = null;
            if (File.Exists(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    settings = (T)serializer.Deserialize(fs);
                }
            }

            return settings;
        }

        /// <summary>
        /// Saves an object to a new xml file.
        /// If the file exists it will be overwritten.
        /// </summary>
        public static void WriteFile<T>(T obj, string path)
        {
            string foldersOnly = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(foldersOnly) && !Directory.Exists(foldersOnly))
            {
                Directory.CreateDirectory(foldersOnly);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(fs, obj);
            }
        }
    }
}