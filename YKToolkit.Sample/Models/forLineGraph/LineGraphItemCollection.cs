namespace YKToolkit.Models.SampleForLineGraph
{
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Windows.Media;
    using YKToolkit.Controls;

    public class LineGraphItemCollection : ObservableCollection<LineGraphItem>
    {
        /// <summary>
        /// グラフデータを追加します。
        /// </summary>
        /// <param name="name">凡例に表示する文字列を指定します。</param>
        /// <param name="x">横軸データを指定します。</param>
        /// <param name="y">縦軸データを指定します。</param>
        public void AddData(string name, IEnumerable x, IEnumerable y)
        {
            var item = new LineGraphItem();
            item.Legend = name;
            item.XAxisData = x;
            item.YAxisData = y;
            var brush = new SolidColorBrush(LineColors[this.Count % 5]);
            brush.Freeze();
            item.Stroke = brush;
            item.Fill = brush;
            item.MarkerPen = null;
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
