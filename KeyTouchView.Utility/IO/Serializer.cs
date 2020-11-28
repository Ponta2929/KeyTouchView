using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace KeyTouchView.Utility.IO
{
    /// <summary>
    /// クラスのシリアライズ・デシリアライズを行います。
    /// </summary>
    public static class Serializer
    {
        /// <summary>
        /// Xmlデータをファイルに書き込みます。
        /// </summary>
        /// <param name="fileName">対象のファイル名。</param>
        public static void FileSerialize<T>(string fileName, object @object)
        {
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                (new XmlSerializer(typeof(T))).Serialize(stream, @object);
        }

        /// <summary>
        /// Xmlデータをファイルから読み込みます。
        /// </summary>
        /// <param name="fileName">対象のファイル名。</param>
        public static T FileDeserialize<T>(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))            
                return (T)(new XmlSerializer(typeof(T))).Deserialize(stream);
        }

        /// <summary>
        /// Xmlデータを文字列として取得します。
        /// </summary>
        /// <returns>Xmlデータ</returns>
        public static string XmlSerialize<T>(object @object)
        {
            using (var stream = new MemoryStream())
            {
                (new XmlSerializer(typeof(T))).Serialize(stream, @object);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// Xmlデータを文字列から読み込みます。
        /// </summary>
        public static T XmlDeserialize<T>(string xml)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
                return (T)(new XmlSerializer(typeof(T))).Deserialize(stream);
        }
    }

    /// <summary>
    /// クラスのシリアライズ・デシリアライズを行います。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Serializer<T>
    {
        /// <summary>
        /// Xmlデータをファイルに書き込みます。
        /// </summary>
        /// <param name="fileName">対象のファイル名。</param>
        public void FileSerialize(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                (new XmlSerializer(typeof(T))).Serialize(stream, this);
        }

        /// <summary>
        /// Xmlデータをファイルから読み込みます。
        /// </summary>
        /// <param name="fileName">対象のファイル名。</param>
        public void FileDeserialize(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                var t = (T)(new XmlSerializer(typeof(T))).Deserialize(stream);

                foreach (var info in t.GetType().GetFields())
                    info.SetValue(this, info.GetValue(t));

                foreach (var info in t.GetType().GetProperties())
                    info.SetValue(this, info.GetValue(t));
            }
        }

        /// <summary>
        /// Xmlデータを文字列として取得します。
        /// </summary>
        /// <returns>Xmlデータ</returns>
        public string XmlSerialize()
        {
            using (var stream = new MemoryStream())
            {
                (new XmlSerializer(typeof(T))).Serialize(stream, this);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// Xmlデータを文字列から読み込みます。
        /// </summary>
        public void XmlDeserialize(string xml)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                var t = (T)(new XmlSerializer(typeof(T))).Deserialize(stream);
                               
                foreach (var info in t.GetType().GetFields())
                    info.SetValue(this, info.GetValue(t));

                foreach (var info in t.GetType().GetProperties())
                    info.SetValue(this, info.GetValue(t));
            }
        }
    }
}