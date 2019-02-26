namespace YKToolkit.Sample.ViewModels
{
    using System;
    using YKToolkit.Bindings;
    using YKToolkit.Models.SampleForLineGraph;

    public class LineGraphSubViewModel : ViewModelBase
    {
        #region Title プロパティ
        private LineGraphTextItem _title = new LineGraphTextItem("グラフタイトル", 16.0);
        /// <summary>
        /// グラフタイトル設定を取得または設定します。
        /// </summary>
        public LineGraphTextItem Title
        {
            get { return this._title; }
            set { SetProperty(ref this._title, value); }
        }
        #endregion Title プロパティ

        #region XLabel プロパティ
        private LineGraphTextItem _xLabel = new LineGraphTextItem("横軸ラベル");
        /// <summary>
        /// 横軸ラベル設定を取得または設定します。
        /// </summary>
        public LineGraphTextItem XLabel
        {
            get { return this._xLabel; }
            set { SetProperty(ref this._xLabel, value); }
        }
        #endregion XLabel プロパティ

        #region YLabel プロパティ
        private LineGraphTextItem _yLabel = new LineGraphTextItem("縦軸ラベル");
        /// <summary>
        /// 縦軸ラベル設定を取得または設定します。
        /// </summary>
        public LineGraphTextItem YLabel
        {
            get { return this._yLabel; }
            set { SetProperty(ref this._yLabel, value); }
        }
        #endregion YLabel プロパティ

        #region Y2Label プロパティ
        private LineGraphTextItem _y2Label = new LineGraphTextItem("第 2 主軸ラベル");
        /// <summary>
        /// 第 2 主軸ラベル設定を取得または設定します。
        /// </summary>
        public LineGraphTextItem Y2Label
        {
            get { return this._y2Label; }
            set { SetProperty(ref this._y2Label, value); }
        }
        #endregion Y2Label プロパティ

        #region 軸設定
        private LineGraphAxisItem _xAxis = new LineGraphAxisItem(0.0, 100.0, 10.0);
        /// <summary>
        /// 横軸設定を取得または設定します。
        /// </summary>
        public LineGraphAxisItem XAxis
        {
            get { return this._xAxis; }
            set { SetProperty(ref this._xAxis, value); }
        }

        private LineGraphAxisItem _yAxis = new LineGraphAxisItem(-100, 100.0, 10.0);
        /// <summary>
        /// 縦軸設定を取得または設定します。
        /// </summary>
        public LineGraphAxisItem YAxis
        {
            get { return this._yAxis; }
            set { SetProperty(ref this._yAxis, value); }
        }

        private LineGraphAxisItem _y2Axis = new LineGraphAxisItem(0.0, 100.0, 10.0);
        /// <summary>
        /// 第 2 主軸設定を取得または設定します。
        /// </summary>
        public LineGraphAxisItem Y2Axis
        {
            get { return this._y2Axis; }
            set { SetProperty(ref this._y2Axis, value); }
        }
        #endregion 軸設定

        #region IsY2Enabled プロパティ
        private bool _isY2Enabled;
        /// <summary>
        /// 第 2 主軸の有効性を取得または設定します。
        /// </summary>
        public bool IsY2Enabled
        {
            get { return this._isY2Enabled; }
            set { SetProperty(ref this._isY2Enabled, value); }
        }
        #endregion IsY2Enabled プロパティ

        #region IsLegendEnabled プロパティ
        private bool _isLegendEnabled;
        /// <summary>
        /// 凡例の有効性を取得または設定します。
        /// </summary>
        public bool IsLegendEnabled
        {
            get { return this._isLegendEnabled; }
            set { SetProperty(ref this._isLegendEnabled, value); }
        }
        #endregion IsLegendEnabled プロパティ

        #region LegendPositionLeft プロパティ
        private double _legendPositionLeft;
        /// <summary>
        /// 凡例の左端の位置を取得または設定します。
        /// </summary>
        public double LegendPositionLeft
        {
            get { return this._legendPositionLeft; }
            set { SetProperty(ref this._legendPositionLeft, value); }
        }

        private double _legendPositionTop;
        /// <summary>
        /// 凡例の上端の位置を取得または設定します。
        /// </summary>
        public double LegendPositionTop
        {
            get { return this._legendPositionTop; }
            set { SetProperty(ref this._legendPositionTop, value); }
        }
        #endregion LegendPositionLeft プロパティ

        #region LineGraphItemCollection プロパティ
        private LineGraphItemCollection _lineGraphItemCollection = new LineGraphItemCollection();
        /// <summary>
        /// グラフデータコレクションを取得または設定します。
        /// </summary>
        public LineGraphItemCollection LineGraphItemCollection
        {
            get { return this._lineGraphItemCollection; }
            set { SetProperty(ref this._lineGraphItemCollection, value); }
        }
        #endregion LineGraphItemCollection プロパティ

        #region IsMouseOverInformationEnabled プロパティ
        private bool _isMouseOverInformationEnabled;
        /// <summary>
        /// マウスオーバー時の情報表示が有効かどうかを取得または設定します。
        /// </summary>
        public bool IsMouseOverInformationEnabled
        {
            get { return this._isMouseOverInformationEnabled; }
            set { SetProperty(ref this._isMouseOverInformationEnabled, value); }
        }
        #endregion IsMouseOverInformationEnabled プロパティ

        #region データ追加
        private DelegateCommand _addDataCommand;
        /// <summary>
        /// データ追加コマンドを取得します。
        /// </summary>
        public DelegateCommand AddDataCommand
        {
            get
            {
                return this._addDataCommand ?? (this._addDataCommand = new DelegateCommand(_ =>
                {
                    var n = 1000;
                    var rnd = new Random();
                    var x = new double[n];
                    var y = new double[n];
                    var a = rnd.Next(0, 100);
                    var f = rnd.Next(0, 100);
                    var p = rnd.NextDouble() * Math.PI / 2.0;
                    for (var i = 0; i < n; i++)
                    {
                        x[i] = (double)i;
                        y[i] = a * Math.Sin(2.0 * Math.PI * f * i / 1000.0 + p);
                    }
                    this.LineGraphItemCollection.AddData("Data" + (this.LineGraphItemCollection.Count + 1).ToString(), x, y);
                }));
            }
        }
        #endregion データ追加

        #region データクリア
        private DelegateCommand _clearDataCommand;
        /// <summary>
        /// データ追加コマンドを取得します。
        /// </summary>
        public DelegateCommand ClearDataCommand
        {
            get
            {
                return this._clearDataCommand ?? (this._clearDataCommand = new DelegateCommand(_ =>
                {
                    this.LineGraphItemCollection.Clear();
                }));
            }
        }
        #endregion データクリア
    }
}
