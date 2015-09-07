namespace YKToolkit.SampleForLineGraph.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class GraphDataParser
    {
        /// <summary>
        /// CSV ファイルを読み込みます。
        /// </summary>
        /// <param name="fullPath">CSV ファイルのフルパス</param>
        /// <param name="progress">進捗状況を報告するための <c>IProgress&lt;int&gt;</c></param>
        public static List<List<string>> ReadFile(string fullPath, IProgress<int> progress)
        {
            if (!File.Exists(fullPath))
            {
                throw new ArgumentException(fullPath + " が存在しません。");
            }

            var totalBytes = new FileInfo(fullPath).Length;
            long bytes = 0;
            var sjisEnc = Encoding.GetEncoding("UTF-8");
            var list = new List<List<string>>();
            using (var sr = new StreamReader(@fullPath))
            {
                while (sr.Peek() >= 0)
                {
                    var lineList = new List<string>();
                    var line = sr.ReadLine();
                    bytes += sjisEnc.GetByteCount(line) + 2;    // \r\n の分を余計に足す

                    var cols = line.Split(',');
                    foreach (var col in cols)
                    {
                        lineList.Add(col);
                    }
                    list.Add(lineList);

                    if (progress != null)
                    {
                        var completion = (int)(100 * bytes / totalBytes);
                        progress.Report(completion > 100 ? 100 : completion);
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// csv ファイルを書き込みます。
        /// </summary>
        /// <param name="fullPath">CSV ファイルのフルパス</param>
        /// <param name="progress">進捗状況を報告するための <c>IProgress&lt;int&gt;</c></param>
        public static void WriteFile(string fullPath, List<List<string>> writeData, IProgress<int> progress)
        {
            if (writeData == null)
            {
                throw new InvalidOperationException("WriteFileData プロパティが設定されていません。");
            }

            var rows = writeData.Count;
            long row = 0;
            using (var sw = new StreamWriter(@fullPath, false))
            {
                sw.NewLine = "\r\n";
                foreach (var cols in writeData)
                {
                    var str = string.Empty;
                    foreach (var col in cols as List<string>)
                    {
                        str += col as string + ',';
                    }
                    str = str.Remove(str.Length - 1);
                    sw.WriteLine(str);
                    row++;

                    if (progress != null)
                    {
                        var completion = (int)(100 * row / rows);
                        progress.Report(completion);
                    }
                }
            }
        }
    }
}
