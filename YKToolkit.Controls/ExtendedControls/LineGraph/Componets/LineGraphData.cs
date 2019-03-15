namespace YKToolkit.Controls
{
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// 折れ線グラフのデータを表します。
    /// </summary>
    public class LineGraphData : FrameworkElement
    {
        #region XData プロパティ

        /// <summary>
        /// グラフの横軸データを double[] 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty XDataProperty = DependencyProperty.Register("XData", typeof(double[]), typeof(LineGraphData), new UIPropertyMetadata(new double[0]));

        /// <summary>
        /// グラフの横軸データを取得または設定します。
        /// </summary>
        public double[] XData
        {
            get { return (double[])GetValue(XDataProperty); }
            set { SetValue(XDataProperty, value); }
        }

        #endregion XData プロパティ

        #region YData プロパティ

        /// <summary>
        /// グラフの縦軸データを double[] 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty YDataProperty = DependencyProperty.Register("YData", typeof(double[]), typeof(LineGraphData), new UIPropertyMetadata(new double[0]));

        /// <summary>
        /// グラフの縦軸データを取得または設定します。
        /// </summary>
        public double[] YData
        {
            get { return (double[])GetValue(YDataProperty); }
            set { SetValue(YDataProperty, value); }
        }

        #endregion YData プロパティ

        #region IsY2 プロパティ

        /// <summary>
        /// 第 2 主軸を使用するかどうかを bool 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty IsY2Property = DependencyProperty.Register("IsY2", typeof(bool), typeof(LineGraphData), new UIPropertyMetadata(false));

        /// <summary>
        /// 第 2 主軸を使用するかどうかを取得または設定します。
        /// </summary>
        public bool IsY2
        {
            get { return (bool)GetValue(IsY2Property); }
            set { SetValue(IsY2Property, value); }
        }

        #endregion IsY2 プロパティ

        #region IsMarkerEnabled プロパティ

        /// <summary>
        /// マーカーを表示するかどうかを bool 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty IsMarkerEnabledProperty = DependencyProperty.Register("IsMarkerEnabled", typeof(bool), typeof(LineGraphData), new UIPropertyMetadata(false));

        /// <summary>
        /// マーカーを表示するかどうかを取得または設定します。
        /// </summary>
        public bool IsMarkerEnabled
        {
            get { return (bool)GetValue(IsMarkerEnabledProperty); }
            set { SetValue(IsMarkerEnabledProperty, value); }
        }

        #endregion IsMarkerEnabled プロパティ

        #region Legend プロパティ

        /// <summary>
        /// グラフの凡例を string 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty LegendProperty = DependencyProperty.Register("Legend", typeof(string), typeof(LineGraphData), new UIPropertyMetadata(null));

        /// <summary>
        /// グラフの凡例を取得または設定します。
        /// </summary>
        public string Legend
        {
            get { return (string)GetValue(LegendProperty); }
            set { SetValue(LegendProperty, value); }
        }

        #endregion Legend プロパティ

        #region AutoStroke プロパティ

        /// <summary>
        /// グラフの自動線色を bool 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty AutoStrokeProperty = DependencyProperty.Register("AutoStroke", typeof(bool), typeof(LineGraphData), new UIPropertyMetadata(true));

        /// <summary>
        /// グラフの自動線色を取得または設定します。
        /// </summary>
        public bool AutoStroke
        {
            get { return (bool)GetValue(AutoStrokeProperty); }
            set { SetValue(AutoStrokeProperty, value); }
        }

        #endregion AutoStroke プロパティ

        #region Stroke プロパティ

        /// <summary>
        /// グラフの線色を Color 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register("Stroke", typeof(Color), typeof(LineGraphData), new UIPropertyMetadata(Colors.Orange, OnStrokePropertyChanged));

        /// <summary>
        /// グラフの線色を取得または設定します。
        /// </summary>
        public Color Stroke
        {
            get { return (Color)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        /// <summary>
        /// Stroke プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnStrokePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var data = d as LineGraphData;
            data.AutoStroke = data.Stroke == null;
        }

        #endregion Stroke プロパティ

        #region StrokeThickness プロパティ

        /// <summary>
        /// グラフの線色を Color? 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register("StrokeThickness", typeof(int), typeof(LineGraphData), new UIPropertyMetadata(1));

        /// <summary>
        /// グラフの線色を取得または設定します。
        /// </summary>
        public int StrokeThickness
        {
            get { return (int)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        #endregion StrokeThickness プロパティ

        /// <summary>
        /// グラフに描画されるデータの開始インデックスを取得または設定します。
        /// </summary>
        internal int StartIndex { get; set; }

        /// <summary>
        /// グラフに描画されるデータの終了インデックスを取得または設定します。
        /// </summary>
        internal int EndIndex { get; set; }
    }
}
