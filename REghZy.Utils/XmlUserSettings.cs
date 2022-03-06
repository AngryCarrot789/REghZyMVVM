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
        private readonly string path;
        private readonly XmlSerializer serializer;

        /// <summary>
        /// Creates a new instance of the XML user settings serialiser
        /// </summary>
        /// <param name="fileName">
        /// The name of the file, which will be placed in C:/users/_user_/MyDocuments/REghZY
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the properties instance is null, or the file name is null or empty
        /// </exception>
        public XmlUserSettings(string fileName) {
            if (string.IsNullOrEmpty(fileName)) {
                throw new ArgumentNullException("fileName", "The file name cannot be null");
            }

            this.serializer = new XmlSerializer(typeof(T));
            this.path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), REGHZY_BASE_FOLDER, Path.ChangeExtension(fileName, "xml"));
        }

        public void Save(T instance) {
            using (BufferedStream stream = new BufferedStream(File.OpenWrite(this.path), 512)) {
                this.serializer.Serialize(stream, instance);
            }
        }

        public T Load() {
            using (BufferedStream stream = new BufferedStream(File.OpenRead(this.path), 512)) {
                return (T) this.serializer.Deserialize(stream);
            }
        }
    }
}