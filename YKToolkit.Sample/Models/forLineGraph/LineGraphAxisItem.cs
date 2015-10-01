namespace YKToolkit.Models.SampleForLineGraph
{
    using System;
    using System.Windows.Media;
    using YKToolkit.Bindings;

    /// <summary>
    /// 折れ線グラフの 1 つの軸設定を表します。
    /// </summary>
    public class LineGraphAxisItem : NotificationObject
    {
        #region コンストラクタ
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public LineGraphAxisItem()
        {
        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="minimum">最小値を指定します。</param>
        /// <param name="maximum">最大値を指定します。</param>
        public LineGraphAxisItem(double minimum, double maximum)
            : this(minimum, maximum, 1.0)
        {
            this.Step = (maximum - minimum) / 10.0;
        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="minimum">最小値を指定します。</param>
        /// <param name="maximum">最大値を指定します。</param>
        /// <param name="step">間隔を指定します。</param>
        public LineGraphAxisItem(double minimum, double maximum, double step)
            : this(minimum, maximum, step, "#0", 12.0, new Pen(Brushes.LightGray, 1.0) { DashStyle = new DashStyle(new double[] { 2.0, 4.0 }, 0.0) })
        {
        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="minimum">最小値を指定します。</param>
        /// <param name="maximum">最大値を指定します。</param>
        /// <param name="step">間隔を指定します。</param>
        /// <param name="stringFormat">表示形式を指定します。</param>
        /// <param name="fontSize">フォントサイズを指定します。</param>
        /// <param name="pen">目盛線の線種を指定します。</param>
        public LineGraphAxisItem(double minimum, double maximum, double step, string stringFormat, double fontSize, Pen pen)
        {
            if (minimum > maximum)
                throw new ArgumentException("最小値と最大値が逆転しています。");
            if (step < 0.0)
                throw new ArgumentException("間隔は正の値である必要があります。");
            if (fontSize < 0.0)
                throw new ArgumentException("フォントサイズは正の値である必要があります。");

            this.Minimum = minimum;
            this.Maximum = maximum;
            this.Step = step;
            this.StringFormat = stringFormat;
            this.GridPen = pen;
        }
        #endregion コンストラクタ

        private double _minimum = double.MinValue;
        /// <summary>
        /// 軸の最小値を取得または設定します。
        /// </summary>
        public double Minimum
        {
            get { return this._minimum; }
            set
            {
                var minimum = value;
                if (minimum < this.Maximum)
                {
                    SetProperty(ref this._minimum, minimum);
                }
            }
        }

        private double _maximum = double.MaxValue;
        /// <summary>
        /// 軸の最大値を取得または設定します。
        /// </summary>
        public double Maximum
        {
            get { return this._maximum; }
            set
            {
                var maximum = value;
                if (maximum > this.Minimum)
                {
                    SetProperty(ref this._maximum, maximum);
                }
            }
        }

        private double _step = 10.0;
        /// <summary>
        /// 軸目盛の間隔を取得または設定します。
        /// </summary>
        public double Step
        {
            get { return this._step; }
            set
            {
                var step = value;
                if (step > 0.0)
                {
                    SetProperty(ref this._step, step);
                }
            }
        }

        private string _stringFormat = "#0";
        /// <summary>
        /// 軸目盛の表示形式を取得または設定します。
        /// </summary>
        public string StringFormat
        {
            get { return this._stringFormat; }
            set { SetProperty(ref this._stringFormat, value); }
        }

        private double _fontSize = 12.0;
        /// <summary>
        /// 軸目盛のフォントサイズを取得または設定します。
        /// </summary>
        public double FontSize
        {
            get { return this._fontSize; }
            set { SetProperty(ref this._fontSize, value); }
        }

        private Pen _gridPen;
        /// <summary>
        /// 目盛線の線種を取得または設定します。
        /// </summary>
        public Pen GridPen
        {
            get { return this._gridPen; }
            set { SetProperty(ref this._gridPen, value); }
        }
    }
}
