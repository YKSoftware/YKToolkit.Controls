namespace YKToolkit.Sample.Models
{
    using System.Collections.ObjectModel;
    using System.Windows.Media;
    using YKToolkit.Controls;

    public class LineGraphItemCollection : ObservableCollection<LineGraphData>
    {
        /// <summary>
        /// グラフデータを追加します。
        /// </summary>
        /// <param name="name">凡例に表示する文字列を指定します。</param>
        /// <param name="x">横軸データを指定します。</param>
        /// <param name="y">縦軸データを指定します。</param>
        public void AddData(string name, double[] x, double[] y, bool isAnimation = false)
        {
            var item = new LineGraphData();
            item.Tag = isAnimation;
            item.Legend = name;
            item.XData = x;
            item.YData = y;
            item.Stroke = LineColors[this.Count % 5];
            this.Add(item);
        }

        /// <summary>
        /// グラフデータの色
        /// </summary>
        private static Color[] LineColors = new Color[5]
        {
            Colors.Orange,
            Colors.MediumSeaGreen,
            Colors.LightSteelBlue,
            Colors.Plum,
            Colors.LightCoral,
        };
    }
}
