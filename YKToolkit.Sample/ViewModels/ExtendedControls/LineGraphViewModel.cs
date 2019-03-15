namespace YKToolkit.Sample.ViewModels
{
    using System;
    using YKToolkit.Bindings;
    using YKToolkit.Controls;
    using YKToolkit.Helpers;
    using YKToolkit.Sample.Models;

    public class LineGraphViewModel : ViewModelBase
    {
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
                        y[i] = a * Math.Sin(2.0 * Math.PI * f * i / 1000.0 + p) + 500.0;
                    }
                    this.LineGraphItemCollection.AddData("Data" + (this.LineGraphItemCollection.Count + 1).ToString(), x, y);

                    LineGraphTrigger.ReDraw();
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

                    LineGraphTrigger.ReDraw();
                }));
            }
        }
        #endregion データクリア
    }
}
