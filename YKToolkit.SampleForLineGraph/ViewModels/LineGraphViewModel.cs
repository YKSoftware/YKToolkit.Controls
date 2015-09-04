namespace YKToolkit.SampleForLineGraph.ViewModels
{
    using YKToolkit.SampleForLineGraph.Models;

    public class LineGraphViewModel : ViewModelBase
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

        private LineGraphAxisItem _yAxis = new LineGraphAxisItem(0.0, 100.0, 10.0);
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
    }
}
