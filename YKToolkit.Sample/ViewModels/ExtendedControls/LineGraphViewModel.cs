namespace YKToolkit.Sample.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Threading;
    using YKToolkit.Bindings;
    using YKToolkit.Controls;
    using YKToolkit.Sample.Models;

    public class LineGraphViewModel : ViewModelBase
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public LineGraphViewModel()
        {
            this._timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(20) };
            this._timer.Tick += OnTick;
            this._timer.Start();
        }

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
                    var a = rnd.Next(60, 400);
                    var f = rnd.Next(1, 10);
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

        private DelegateCommand _addAnimationDataCommand;
        /// <summary>
        /// 動くデータ追加コマンドを取得します。
        /// </summary>
        public DelegateCommand AddAnimationDataCommand
        {
            get
            {
                return this._addAnimationDataCommand ?? (this._addAnimationDataCommand = new DelegateCommand(_ =>
                {
                    var a = this._rand.Next(60, 400);
                    var f = this._rand.Next(1, 10);
                    this._amps.Add(a);
                    this._freqs.Add(f);
                    this.LineGraphItemCollection.AddData("Data" + (this.LineGraphItemCollection.Count + 1).ToString(), Enumerable.Range(0, 1000).Select(x => (double)x).ToArray(), new double[0], true);
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
                    this._amps.Clear();
                    this._freqs.Clear();
                    this._count = 0;

                    LineGraphTrigger.ReDraw();
                }));
            }
        }
        #endregion データクリア

        /// <summary>
        /// Elapsed イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnTick(object sender, EventArgs e)
        {
            if (this.LineGraphItemCollection.Count > 0)
            {
                foreach (var item in this.LineGraphItemCollection.Where(x => (bool)x.Tag).Select((x, i) => new { a = this._amps[i], f = this._freqs[i], Graph = x }))
                {
                    item.Graph.YData = item.Graph.XData.Select(x => item.a * Math.Sin(2 * Math.PI * item.f * (x + _count) / 1000.0) + 500.0 + 200.0 * (this._rand.NextDouble() - 0.5)).ToArray();
                }

                LineGraphTrigger.ReDraw();
                this._count += 10;
            }
        }

        /// <summary>
        /// グラフ更新用タイマー
        /// </summary>
        private DispatcherTimer _timer;

        private int _count;

        /// <summary>
        /// グラフノイズ用乱数発生器
        /// </summary>
        private Random _rand = new Random();

        private List<double> _amps = new List<double>();
        private List<double> _freqs = new List<double>();
    }
}
