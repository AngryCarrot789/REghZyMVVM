using System;
using System.IO;
using System.Xml.Serialization;

namespace REghZy.Utils {
    /// <summary>
    /// A class which uses an XML serialiser to help with saving/loading user settings
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class XmlUserSettings<T> {
        private const string REGHZY_BASE_FOLDER = "REghZy";
        private readonly XmlSerializer serializer;

        /// <summary>
        /// The path to use
        /// </summary>
        public string Destination { get; }

        public static XmlUserSettings<T> Create(string fileName) {
            if (string.IsNullOrEmpty(fileName)) {
                throw new ArgumentNullException(nameof(fileName), "The fileName cannot be null");
            }

            return new XmlUserSettings<T>(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), REGHZY_BASE_FOLDER, Path.ChangeExtension(fileName, "xml")));
        }

        public static XmlUserSettings<T> Create(string folder, string fileName) {
            if (string.IsNullOrEmpty(folder)) {
                throw new ArgumentNullException(nameof(folder), "The folder name cannot be null");
            }

            if (string.IsNullOrEmpty(fileName)) {
                throw new ArgumentNullException(nameof(fileName), "The file name cannot be null");
            }

            return new XmlUserSettings<T>(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), folder, Path.ChangeExtension(fileName, "xml")));
        }

        /// <summary>
        /// Creates a new instance of the XML user settings serialiser
        /// </summary>
        /// <param name="fileName">
        /// The name of the file, which will be placed in C:/users/_user_/MyDocuments/REghZY
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the properties instance is null, or the file name is null or empty
        /// </exception>
        public XmlUserSettings(string fileName) : this(REGHZY_BASE_FOLDER, fileName) {

        }

        /// <summary>
        /// Creates a new instance of the XML user settings serialiser
        /// </summary>
        /// <param name="folder">
        /// The name of the folder in which the file will be in (in MyDocuments)
        /// </param>
        /// <param name="fileName">
        /// The name of the file, which will be placed in C:/users/_user_/MyDocuments/REghZY
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the properties instance is null, or the file name is null or empty
        /// </exception>
        public XmlUserSettings(string folder, string fileName) {
            if (string.IsNullOrEmpty(folder)) {
                throw new ArgumentNullException(nameof(folder), "The folder name cannot be null");
            }

            if (string.IsNullOrEmpty(fileName)) {
                throw new ArgumentNullException(nameof(fileName), "The file name cannot be null");
            }

            this.serializer = new XmlSerializer(typeof(T));
            this.Destination = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), folder, Path.ChangeExtension(fileName, "xml"));
        }

        /// <summary>
        /// Saves the given instance to the file
        /// </summary>
        /// <param name="instance"></param>
        public void Save(T instance) {
            using (BufferedStream stream = new BufferedStream(File.OpenWrite(this.Destination), 512)) {
                this.serializer.Serialize(stream, instance);
            }
        }

        /// <summary>
        /// Loads an instance from the file
        /// </summary>
        /// <returns></returns>
        public T Load() {
            using (BufferedStream stream = new BufferedStream(File.OpenRead(this.Destination), 512)) {
                return (T) this.serializer.Deserialize(stream);
            }
        }
    }
}