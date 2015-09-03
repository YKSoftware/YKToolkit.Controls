namespace YKToolkit.Sample.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using YKToolkit.Bindings;

    public class LineGraphViewModel : ViewModelBase
    {
        #region グラフタイトル/ラベル
        private string _graphTitle = "グラフタイトル";
        /// <summary>
        /// グラフタイトルを取得または設定します。
        /// </summary>
        public string GraphTitle
        {
            get { return _graphTitle; }
            set { SetProperty(ref _graphTitle, value); }
        }

        private string _xLabel = "横軸ラベル";
        /// <summary>
        /// 横軸ラベルを取得または設定します。
        /// </summary>
        public string XLabel
        {
            get { return _xLabel; }
            set { SetProperty(ref _xLabel, value); }
        }

        private string _yLabel = "縦軸ラベル";
        /// <summary>
        /// 縦軸ラベルを取得または設定します。
        /// </summary>
        public string YLabel
        {
            get { return _yLabel; }
            set { SetProperty(ref _yLabel, value); }
        }

        private string _y2Label = "第 2 主軸ラベル";
        /// <summary>
        /// 第 2 主軸ラベルを取得または設定します。
        /// </summary>
        public string Y2Label
        {
            get { return _y2Label; }
            set { SetProperty(ref _y2Label, value); }
        }
        #endregion グラフタイトル/ラベル

        #region グラフデータ 1
        private ObservableCollection<double> _xAxisData1;
        /// <summary>
        /// 横軸データ 1 を取得または設定します。
        /// </summary>
        public ObservableCollection<double> XAxisData1
        {
            get { return _xAxisData1; }
            set { SetProperty(ref _xAxisData1, value); }
        }

        private ObservableCollection<double> _yAxisData1;
        /// <summary>
        /// 縦軸データ 1 を取得または設定します。
        /// </summary>
        public ObservableCollection<double> YAxisData1
        {
            get { return _yAxisData1; }
            set { SetProperty(ref _yAxisData1, value); }
        }

        private string _legend1 = "Data1";
        /// <summary>
        /// データ 1 名を取得または設定します。
        /// </summary>
        public string Legend1
        {
            get { return _legend1; }
            set { SetProperty(ref _legend1, value); }
        }
        #endregion グラフデータ 1

        #region グラフデータ 2
        private ObservableCollection<double> _xAxisData2;
        /// <summary>
        /// 横軸データ 2 を取得または設定します。
        /// </summary>
        public ObservableCollection<double> XAxisData2
        {
            get { return _xAxisData2; }
            set { SetProperty(ref _xAxisData2, value); }
        }

        private ObservableCollection<double> _yAxisData2;
        /// <summary>
        /// 縦軸データ 2 を取得または設定します。
        /// </summary>
        public ObservableCollection<double> YAxisData2
        {
            get { return _yAxisData2; }
            set { SetProperty(ref _yAxisData2, value); }
        }

        private string _legend2 = "Data2";
        /// <summary>
        /// データ 2 名を取得または設定します。
        /// </summary>
        public string Legend2
        {
            get { return _legend2; }
            set { SetProperty(ref _legend2, value); }
        }
        #endregion グラフデータ 2

        #region グラフ表示範囲
        private double _xMin = 0.0;
        /// <summary>
        /// 横軸の最小値を取得または設定します。
        /// </summary>
        public double XMin
        {
            get { return _xMin; }
            set { SetProperty(ref _xMin, value); }
        }

        private double _xMax = 100.0;
        /// <summary>
        /// 横軸の最大値を取得または設定します。
        /// </summary>
        public double XMax
        {
            get { return _xMax; }
            set { SetProperty(ref _xMax, value); }
        }

        private double _xStep = 10.0;
        /// <summary>
        /// 横軸目盛の間隔を取得または設定します。
        /// </summary>
        public double XStep
        {
            get { return _xStep; }
            set { SetProperty(ref _xStep, value); }
        }

        private string _xStringFormat = "#";
        /// <summary>
        /// 横軸目盛の表示形式取得または設定します。
        /// </summary>
        public string XStringFormat
        {
            get { return _xStringFormat; }
            set { SetProperty(ref _xStringFormat, value); }
        }

        private double _yMin = -5.0;
        /// <summary>
        /// 縦軸の最小値を取得または設定します。
        /// </summary>
        public double YMin
        {
            get { return _yMin; }
            set { SetProperty(ref _yMin, value); }
        }

        private double _yMax = 5.0;
        /// <summary>
        /// 縦軸の最大値を取得または設定します。
        /// </summary>
        public double YMax
        {
            get { return _yMax; }
            set { SetProperty(ref _yMax, value); }
        }

        private double _yStep = 1;
        /// <summary>
        /// 縦軸目盛の間隔を取得または設定します。
        /// </summary>
        public double YStep
        {
            get { return _yStep; }
            set { SetProperty(ref _yStep, value); }
        }

        private string _yStringFormat = "#";
        /// <summary>
        /// 縦軸目盛の表示形式取得または設定します。
        /// </summary>
        public string YStringFormat
        {
            get { return _yStringFormat; }
            set { SetProperty(ref _yStringFormat, value); }
        }

        private double _y2Min = -10.0;
        /// <summary>
        /// 第 2 主軸の最小値を取得または設定します。
        /// </summary>
        public double Y2Min
        {
            get { return _y2Min; }
            set { SetProperty(ref _y2Min, value); }
        }

        private double _y2Max = 10.0;
        /// <summary>
        /// 第 2 主軸の最大値を取得または設定します。
        /// </summary>
        public double Y2Max
        {
            get { return _y2Max; }
            set { SetProperty(ref _y2Max, value); }
        }

        private double _y2Step = 2.0;
        /// <summary>
        /// 第 2 主軸目盛の間隔を取得または設定します。
        /// </summary>
        public double Y2Step
        {
            get { return _y2Step; }
            set { SetProperty(ref _y2Step, value); }
        }

        private string _y2StringFormat = "#";
        /// <summary>
        /// 第 2 主軸目盛の表示形式取得または設定します。
        /// </summary>
        public string Y2StringFormat
        {
            get { return _y2StringFormat; }
            set { SetProperty(ref _y2StringFormat, value); }
        }

        private bool _isY2Enabled;
        /// <summary>
        /// 第 2 主軸の有効性を取得または設定します。
        /// </summary>
        public bool IsY2Enabled
        {
            get { return _isY2Enabled; }
            set { SetProperty(ref _isY2Enabled, value); }
        }
        #endregion グラフ表示範囲

        #region グラフ領域の余白
        /// <summary>
        /// グラフ領域に対する左余白を取得または設定します。
        /// </summary>
        public double GraphAreaMarginLeft
        {
            get { return GraphAreaMargin.Left; }
            set
            {
                if (GraphAreaMargin.Left != value)
                {
                    GraphAreaMargin = new Thickness(value, GraphAreaMargin.Top, GraphAreaMargin.Right, GraphAreaMargin.Bottom);
                    RaisePropertyChanged("GraphAreaMarginLeft");
                    RaisePropertyChanged("GraphAreaMargin");
                }
            }
        }

        /// <summary>
        /// グラフ領域に対する上余白を取得または設定します。
        /// </summary>
        public double GraphAreaMarginTop
        {
            get { return GraphAreaMargin.Top; }
            set
            {
                if (GraphAreaMargin.Top != value)
                {
                    GraphAreaMargin = new Thickness(GraphAreaMargin.Left, value, GraphAreaMargin.Right, GraphAreaMargin.Bottom);
                    RaisePropertyChanged("GraphAreaMarginTop");
                    RaisePropertyChanged("GraphAreaMargin");
                }
            }
        }

        /// <summary>
        /// グラフ領域に対する右余白を取得または設定します。
        /// </summary>
        public double GraphAreaMarginRight
        {
            get { return GraphAreaMargin.Right; }
            set
            {
                if (GraphAreaMargin.Right != value)
                {
                    GraphAreaMargin = new Thickness(GraphAreaMargin.Left, GraphAreaMargin.Top, value, GraphAreaMargin.Bottom);
                    RaisePropertyChanged("GraphAreaMarginRight");
                    RaisePropertyChanged("GraphAreaMargin");
                }
            }
        }

        /// <summary>
        /// グラフ領域に対する下余白を取得または設定します。
        /// </summary>
        public double GraphAreaMarginBottom
        {
            get { return GraphAreaMargin.Bottom; }
            set
            {
                if (GraphAreaMargin.Bottom != value)
                {
                    GraphAreaMargin = new Thickness(GraphAreaMargin.Left, GraphAreaMargin.Top, GraphAreaMargin.Right, value);
                    RaisePropertyChanged("GraphAreaMarginBottom");
                    RaisePropertyChanged("GraphAreaMargin");
                }
            }
        }

        private Thickness _graphAreaMargin = new Thickness(100, 40, 60, 50);
        /// <summary>
        /// グラフ領域に対する余白を取得または設定します。
        /// </summary>
        public Thickness GraphAreaMargin
        {
            get { return _graphAreaMargin; }
            set { SetProperty(ref _graphAreaMargin, value); }
        }
        #endregion グラフ領域の余白

        #region 凡例位置
        private double _legendPositionLeft;
        /// <summary>
        /// 凡例の左位置を取得または設定します。
        /// </summary>
        public double LegendPositionLeft
        {
            get { return _legendPositionLeft; }
            set { SetProperty(ref _legendPositionLeft, value); }
        }

        private double _legendPositionTop;
        /// <summary>
        /// 凡例の上位置を取得または設定します。
        /// </summary>
        public double LegendPositionTop
        {
            get { return _legendPositionTop; }
            set { SetProperty(ref _legendPositionTop, value); }
        }
        #endregion 凡例位置

        #region 凡例の有効性
        private bool _isLegendEnabled;
        /// <summary>
        /// 凡例の有効性を取得または設定します。
        /// </summary>
        public bool IsLegendEnabled
        {
            get { return _isLegendEnabled; }
            set { SetProperty(ref _isLegendEnabled, value); }
        }
        #endregion 凡例の有効性

        #region Commands
        private DelegateCommand _createDataCommand;
        /// <summary>
        /// グラフデータ生成コマンドを取得します。
        /// </summary>
        public DelegateCommand CreateDataCommand
        {
            get
            {
                return _createDataCommand ?? (_createDataCommand = new DelegateCommand(_ =>
                {
                    XAxisData1 = null;
                    YAxisData1 = null;

                    XAxisData1 = new ObservableCollection<double>(Enumerable.Range(0, 10).Select(i => (double)i));
                    YAxisData1 = new ObservableCollection<double>(XAxisData1.Select(x => 2.0 * Math.Sin(2.0 * Math.PI * 5.0 * x / 1000.0)));

                    XAxisData2 = null;
                    YAxisData2 = null;

                    XAxisData2 = new ObservableCollection<double>(Enumerable.Range(0, 10).Select(i => (double)i));
                    YAxisData2 = new ObservableCollection<double>(XAxisData2.Select(x => 3.0 * Math.Sin(2.0 * Math.PI * 15.0 * x / 1000.0)));
                }));
            }
        }

        private DelegateCommand _addDataCommand;
        /// <summary>
        /// グラフデータ削除コマンドを取得します。
        /// </summary>
        public DelegateCommand AddDataCommand
        {
            get
            {
                return _addDataCommand ?? (_addDataCommand = new DelegateCommand(
                _ =>
                {
                    var x = XAxisData1.Count == 0 ? 0.0 : XAxisData1.Last() + 1.0;
                    XAxisData1.Add(x);
                    YAxisData1.Add(2.0 * Math.Sin(2.0 * Math.PI * 5.0 * x / 1000.0));
                    x = XAxisData2.Count == 0 ? 0.0 : XAxisData2.Last() + 1.0;
                    XAxisData2.Add(x);
                    YAxisData2.Add(3.0 * Math.Sin(2.0 * Math.PI * 15.0 * x / 1000.0));
                },
                _ => (XAxisData1 != null) && (YAxisData1 != null) && (XAxisData2 != null) && (YAxisData2 != null)));
            }
        }

        private DelegateCommand _deleteDataCommand;
        /// <summary>
        /// グラフデータ削除コマンドを取得します。
        /// </summary>
        public DelegateCommand DeleteDataCommand
        {
            get
            {
                return _deleteDataCommand ?? (_deleteDataCommand = new DelegateCommand(
                _ =>
                {
                    XAxisData1.RemoveAt(0);
                    YAxisData1.RemoveAt(0);
                    XAxisData2.RemoveAt(0);
                    YAxisData2.RemoveAt(0);
                },
                _ => (XAxisData1 != null && XAxisData1.Count > 0) && (YAxisData1 != null && YAxisData1.Count > 0) && (XAxisData2 != null && XAxisData2.Count > 0) && (YAxisData2 != null && YAxisData2.Count > 0)));
            }
        }

        private DelegateCommand _clearDataCommand;
        /// <summary>
        /// グラフデータクリアコマンドを取得します。
        /// </summary>
        public DelegateCommand ClearDataCommand
        {
            get
            {
                return _clearDataCommand ?? (_clearDataCommand = new DelegateCommand(
                _ =>
                {
                    XAxisData1 = null;
                    YAxisData1 = null;
                    XAxisData2 = null;
                    YAxisData2 = null;
                },
                _ => (XAxisData1 != null) && (YAxisData1 != null) && (XAxisData2 != null) && (YAxisData2 != null)));
            }
        }
        #endregion Commands
    }
}
