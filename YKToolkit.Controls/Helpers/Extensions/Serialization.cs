namespace YKToolkit.Helpers
{
    using System;
    using System.IO;
    using System.Text;
    using System.Windows.Markup;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// 拡張メソッドを提供します。
    /// </summary>
    public static partial class Extensions
    {
        #region シリアライズ
        /// <summary>
        /// XmlSerializer を使用してオブジェクトをシリアライズしてファイルに保存します。
        /// シリアライズしたくないプロパティには <c>[XmlIgnore]</c> などの
        /// 属性を付加することでシリアライズを回避することができます。
        /// </summary>
        /// <param name="obj">シリアライズするオブジェクトを指定します。</param>
        /// <param name="type">対象とするオブジェクトの型情報を指定します。</param>
        /// <param name="fileName">ファイル名を指定します。</param>
        public static void Serialize(this object obj, Type type, string fileName)
        {
            var serializer = new XmlSerializer(type);

            // 書き込む書式の設定
            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = false;
            settings.Encoding = Encoding.UTF8;

            using (var writer = XmlWriter.Create(fileName, settings))
            {
                serializer.Serialize(writer, obj);
            }
        }

        /// <summary>
        /// XamlWriter を使用してオブジェクトをシリアライズしてファイルに保存します。
        /// シリアライズしたくないプロパティには <c>[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]</c> などの
        /// 属性を付加することでシリアライズを回避することができます。
        /// </summary>
        /// <param name="obj">シリアライズするオブジェクトを指定します。</param>
        /// <param name="fileName">ファイル名を指定します。</param>
        public static void SerializeByXamlWriter(this object obj, string fileName)
        {
            string text = obj.SerializeByXamlWriter();

            using (var writer = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                writer.WriteLine(text);
            }
        }

        /// <summary>
        /// オブジェクトをシリアライズします。
        /// </summary>
        /// <param name="obj">シリアライズするオブジェクトを指定します。</param>
        /// <returns>シリアライズ結果を返します。</returns>
        public static string SerializeByXamlWriter(this object obj)
        {
            var settings = new XmlWriterSettings();

            // 出力時の条件
            settings.Indent = true;
            settings.NewLineOnAttributes = false;

            // XML バージョン情報の出力を抑制する
            settings.ConformanceLevel = ConformanceLevel.Fragment;

            var sb = new StringBuilder();
            XamlDesignerSerializationManager manager = null;

            using (var writer = XmlWriter.Create(sb, settings))
            {
                manager = new XamlDesignerSerializationManager(writer);
                manager.XamlWriterMode = XamlWriterMode.Expression;

                System.Windows.Markup.XamlWriter.Save(obj, manager);
            }

            return sb.ToString();
        }
        #endregion シリアライズ

        #region デシリアライズ
        /// <summary>
        /// オブジェクトをデシリアライズします。
        /// </summary>
        /// <param name="fileName">ファイル名を指定します。</param>
        /// <returns>デシリアライズの結果を返します。</returns>
        public static object DeserializeByXamlReaderFromFile(this string fileName)
        {
            object obj = null;
            string text = String.Empty;

            using (var reader = new StreamReader(fileName, Encoding.UTF8))
            {
                text = reader.ReadToEnd();
                obj = DeserializeByXamlReader(text);
            }

            return obj;
        }

        /// <summary>
        /// XML ソースからデシリアライズをおこないます。
        /// </summary>
        /// <param name="xamlText">XML テキストを指定します。</param>
        /// <returns>デシリアライズの結果を返します。</returns>
        public static object DeserializeByXamlReader(this string xamlText)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xamlText);

            return XamlReader.Load(new XmlNodeReader(doc));
        }
        #endregion デシリアライズ
    }
}
