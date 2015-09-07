namespace YKToolkit.Helpers
{
    using System;
    using System.IO;
    using System.Text;
    using System.Windows.Markup;
    using System.Xml;

    /// <summary>
    /// 拡張メソッドを提供します。
    /// </summary>
    public static class Extensions
    {
        #region シリアライズ
        /// <summary>
        /// オブジェクトをシリアライズしてファイルに保存します。
        /// </summary>
        /// <param name="obj">シリアライズするオブジェクトを指定します。</param>
        /// <param name="fileName">ファイル名を指定します。</param>
        public static void Serialize(this object obj, string fileName)
        {
            string text = SerializeObject(obj);

            try
            {
                using (var writer = new StreamWriter(fileName, false, Encoding.UTF8))
                {
                    writer.WriteLine(text);
                }
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err);
            }
        }

        /// <summary>
        /// オブジェクトをシリアライズします。
        /// </summary>
        /// <param name="obj">シリアライズするオブジェクトを指定します。</param>
        /// <returns>シリアライズ結果を返します。</returns>
        private static string SerializeObject(this object obj)
        {
            var settings = new XmlWriterSettings();

            // 出力時の条件
            settings.Indent = true;
            settings.NewLineOnAttributes = false;

            // XML バージョン情報の出力を抑制する
            settings.ConformanceLevel = ConformanceLevel.Fragment;

            var sb = new StringBuilder();
            XamlDesignerSerializationManager manager = null;

            try
            {
                using (var writer = XmlWriter.Create(sb, settings))
                {
                    manager = new XamlDesignerSerializationManager(writer);
                    manager.XamlWriterMode = XamlWriterMode.Expression;

                    System.Windows.Markup.XamlWriter.Save(obj, manager);
                }
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err);
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
        public static object Deserialize(this string fileName)
        {
            object obj = null;
            string text = String.Empty;

            try
            {
                using (var reader = new StreamReader(fileName, Encoding.UTF8))
                {
                    text = reader.ReadToEnd();
                    obj = DeserializeObject(text);
                }
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err);
            }

            return obj;
        }

        /// <summary>
        /// XML ソースからデシリアライズをおこないます。
        /// </summary>
        /// <param name="xamlText">XML テキストを指定します。</param>
        /// <returns>デシリアライズの結果を返します。</returns>
        public static object DeserializeObject(this string xamlText)
        {
            var doc = new XmlDocument();

            try
            {
                doc.LoadXml(xamlText);
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err);
            }

            object obj = null;

            try
            {
                obj = XamlReader.Load(new XmlNodeReader(doc));
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err);
            }

            return obj;
        }
        #endregion デシリアライズ
    }
}
