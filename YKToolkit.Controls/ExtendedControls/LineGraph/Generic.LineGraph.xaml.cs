namespace YKToolkit.Controls
{
    using System.Collections;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// 折れ線グラフを表示するためのコントロールです。
    /// </summary>
    [TemplatePart(Name = PART_GraphPanel, Type = typeof(LineGraphPanel))]
    [TemplatePart(Name = PART_LegendPanel, Type = typeof(LineGraphLegendPanel))]
    public class LineGraph : Control
    {
        #region TemplatePart
        private const string PART_GraphPanel = "PART_GraphPanel";
        private const string PART_LegendPanel = "PART_LegendPanel";

        private LineGraphPanel _graphPanel;
        /// <summary>
        /// 折れ線グラフ表示用パネル
        /// </summary>
        private LineGraphPanel GraphPanel
        {
            get { return _graphPanel; }
            set { _graphPanel = value; }
        }

        private LineGraphLegendPanel _legendPanel;
        /// <summary>
        /// 凡例表示用パネル
        /// </summary>
        private LineGraphLegendPanel LegendPanel
        {
            get { return _legendPanel; }
            set { _legendPanel = value; }
        }

        /// <summary>
        /// テンプレート適用後の処理をおこないます。
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            GraphPanel = this.Template.FindName(PART_GraphPanel, this) as LineGraphPanel;
            LegendPanel = this.Template.FindName(PART_LegendPanel, this) as LineGraphLegendPanel;

            if (GraphPanel != null)
            {
                ResetGraphItem();
                if (this.ItemsSource != null)
                {
                    foreach (LineGraphItem item in this.ItemsSource)
                    {
                        AddGraphItem(item);
                    }
                }
            }
        }
        #endregion TemplatePart

        #region コンストラクタ
        /// <summary>
        /// 静的なコンストラクタです。
        /// </summary>
        static LineGraph()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LineGraph), new FrameworkPropertyMetadata(typeof(LineGraph)));
        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public LineGraph()
        {
            this.SizeChanged += OnSizeChanged;

            _xAxisMoveAreaBrush = new LinearGradientBrush();
            _xAxisMoveAreaBrush.StartPoint = new Point(0, 0.5);
            _xAxisMoveAreaBrush.EndPoint = new Point(1, 0.5);
            _xAxisMoveAreaBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 0.0));
            _xAxisMoveAreaBrush.GradientStops.Add(new GradientStop(Color.FromArgb(0x60, 0xDD, 0xA0, 0xDD), 0.5));
            _xAxisMoveAreaBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 1.0));
            _xAxisMoveAreaBrush.Freeze();

            _yAxisMoveAreaBrush = new LinearGradientBrush();
            _yAxisMoveAreaBrush.StartPoint = new Point(0.5, 0);
            _yAxisMoveAreaBrush.EndPoint = new Point(0.5, 1);
            _yAxisMoveAreaBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 0.0));
            _yAxisMoveAreaBrush.GradientStops.Add(new GradientStop(Color.FromArgb(0x60, 0xDD, 0xA0, 0xDD), 0.5));
            _yAxisMoveAreaBrush.GradientStops.Add(new GradientStop(Colors.Transparent, 1.0));
            _yAxisMoveAreaBrush.Freeze();
        }
        #endregion コンストラクタ

        #region XMin プロパティ
        /// <summary>
        /// XMin 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty XMinProperty = DependencyProperty.Register("XMin", typeof(double), typeof(LineGraph), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 横軸の最小値を取得または設定します。
        /// </summary>
        public double XMin
        {
            get { return (double)GetValue(XMinProperty); }
            set { SetValue(XMinProperty, value); }
        }
        #endregion XMin プロパティ

        #region XMax プロパティ
        /// <summary>
        /// XMax 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty XMaxProperty = DependencyProperty.Register("XMax", typeof(double), typeof(LineGraph), new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 横軸の最大値を取得または設定します。
        /// </summary>
        public double XMax
        {
            get { return (double)GetValue(XMaxProperty); }
            set { SetValue(XMaxProperty, value); }
        }
        #endregion XMax プロパティ

        #region XStep プロパティ
        /// <summary>
        /// XStep 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty XStepProperty = DependencyProperty.Register("XStep", typeof(double), typeof(LineGraph), new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 横軸目盛の間隔を取得または設定します。
        /// </summary>
        public double XStep
        {
            get { return (double)GetValue(XStepProperty); }
            set { SetValue(XStepProperty, value); }
        }
        #endregion XStep プロパティ

        #region XStringFormat プロパティ
        /// <summary>
        /// XStringFormat 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty XStringFormatProperty = DependencyProperty.Register("XStringFormat", typeof(string), typeof(LineGraph), new FrameworkPropertyMetadata("#0", OnXStringFormatPropertyChanged));

        /// <summary>
        /// 横軸目盛の表示形式を取得または設定します。
        /// </summary>
        public string XStringFormat
        {
            get { return (string)GetValue(XStringFormatProperty); }
            set { SetValue(XStringFormatProperty, value); }
        }

        /// <summary>
        /// XStringFormat プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnXStringFormatPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as LineGraph;
            if (control != null)
            {
                if (control.ItemsSource != null)
                {
                    foreach (var obj in control.ItemsSource)
                    {
                        if (obj is LineGraphItem)
                        {
                            (obj as LineGraphItem).XStringFormat = control.XStringFormat;
                        }
                    }
                }
            }
        }
        #endregion XStringFormat プロパティ

        #region XFontSize プロパティ
        /// <summary>
        /// XFontSize 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty XFontSizeProperty = DependencyProperty.Register("XFontSize", typeof(double), typeof(LineGraph), new FrameworkPropertyMetadata(16.0));

        /// <summary>
        /// 横軸目盛のフォントサイズを取得または設定します。
        /// </summary>
        public double XFontSize
        {
            get { return (double)GetValue(XFontSizeProperty); }
            set { SetValue(XFontSizeProperty, value); }
        }
        #endregion XFontSize プロパティ

        #region XGridPen プロパティ
        /// <summary>
        /// XGridPen 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty XGridPenProperty = DependencyProperty.Register("XGridPen", typeof(Pen), typeof(LineGraph), new FrameworkPropertyMetadata(new Pen(Brushes.LightGray, 1.0) { DashStyle = DashStyles.Dash }));

        /// <summary>
        /// 横軸目盛の線種を取得または設定します。
        /// </summary>
        public Pen XGridPen
        {
            get { return (Pen)GetValue(XGridPenProperty); }
            set { SetValue(XGridPenProperty, value); }
        }
        #endregion XGridPen プロパティ

        #region YMin プロパティ
        /// <summary>
        /// YMin 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty YMinProperty = DependencyProperty.Register("YMin", typeof(double), typeof(LineGraph), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 縦軸の最小値を取得または設定します。
        /// </summary>
        public double YMin
        {
            get { return (double)GetValue(YMinProperty); }
            set { SetValue(YMinProperty, value); }
        }
        #endregion YMin プロパティ

        #region YMax プロパティ
        /// <summary>
        /// YMax 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty YMaxProperty = DependencyProperty.Register("YMax", typeof(double), typeof(LineGraph), new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 横軸の最大値を取得または設定します。
        /// </summary>
        public double YMax
        {
            get { return (double)GetValue(YMaxProperty); }
            set { SetValue(YMaxProperty, value); }
        }
        #endregion YMax プロパティ

        #region YStep プロパティ
        /// <summary>
        /// YStep 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty YStepProperty = DependencyProperty.Register("YStep", typeof(double), typeof(LineGraph), new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 縦軸目盛の間隔を取得または設定します。
        /// </summary>
        public double YStep
        {
            get { return (double)GetValue(YStepProperty); }
            set { SetValue(YStepProperty, value); }
        }
        #endregion YStep プロパティ

        #region YStringFormat プロパティ
        /// <summary>
        /// YStringFormat 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty YStringFormatProperty = DependencyProperty.Register("YStringFormat", typeof(string), typeof(LineGraph), new FrameworkPropertyMetadata("#0", OnYStringFormatPropertyChanged));

        /// <summary>
        /// 縦軸目盛の表示形式を取得または設定します。
        /// </summary>
        public string YStringFormat
        {
            get { return (string)GetValue(YStringFormatProperty); }
            set { SetValue(YStringFormatProperty, value); }
        }

        /// <summary>
        /// YStringFormat プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnYStringFormatPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as LineGraph;
            if (control != null)
            {
                if (control.ItemsSource != null)
                {
                    foreach (var obj in control.ItemsSource)
                    {
                        if (obj is LineGraphItem)
                        {
                            var item = obj as LineGraphItem;
                            if (!item.IsSecond)
                                item.YStringFormat = control.YStringFormat;
                        }
                    }
                }
            }
        }
        #endregion YStringFormat プロパティ

        #region YFontSize プロパティ
        /// <summary>
        /// YFontSize 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty YFontSizeProperty = DependencyProperty.Register("YFontSize", typeof(double), typeof(LineGraph), new FrameworkPropertyMetadata(16.0));

        /// <summary>
        /// 縦軸目盛のフォントサイズを取得または設定します。
        /// </summary>
        public double YFontSize
        {
            get { return (double)GetValue(YFontSizeProperty); }
            set { SetValue(YFontSizeProperty, value); }
        }
        #endregion YFontSize プロパティ

        #region YGridPen プロパティ
        /// <summary>
        /// YGridPen 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty YGridPenProperty = DependencyProperty.Register("YGridPen", typeof(Pen), typeof(LineGraph), new FrameworkPropertyMetadata(new Pen(Brushes.LightGray, 1.0) { DashStyle = DashStyles.Dash }));

        /// <summary>
        /// 縦軸目盛の線種を取得または設定します。
        /// </summary>
        public Pen YGridPen
        {
            get { return (Pen)GetValue(YGridPenProperty); }
            set { SetValue(YGridPenProperty, value); }
        }
        #endregion YGridPen プロパティ

        #region IsY2Enabled プロパティ
        /// <summary>
        /// IsY2Enabled 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsY2EnabledProperty = DependencyProperty.Register("IsY2Enabled", typeof(bool), typeof(LineGraph), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 第 2 主軸の有効性を取得または設定します。
        /// </summary>
        public bool IsY2Enabled
        {
            get { return (bool)GetValue(IsY2EnabledProperty); }
            set { SetValue(IsY2EnabledProperty, value); }
        }
        #endregion IsY2Enabled プロパティ

        #region Y2Min プロパティ
        /// <summary>
        /// Y2Min 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty Y2MinProperty = DependencyProperty.Register("Y2Min", typeof(double), typeof(LineGraph), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 第 2 主軸の最小値を取得または設定します。
        /// </summary>
        public double Y2Min
        {
            get { return (double)GetValue(Y2MinProperty); }
            set { SetValue(Y2MinProperty, value); }
        }
        #endregion Y2Min プロパティ

        #region Y2Max プロパティ
        /// <summary>
        /// Y2Max 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty Y2MaxProperty = DependencyProperty.Register("Y2Max", typeof(double), typeof(LineGraph), new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 横軸の最大値を取得または設定します。
        /// </summary>
        public double Y2Max
        {
            get { return (double)GetValue(Y2MaxProperty); }
            set { SetValue(Y2MaxProperty, value); }
        }
        #endregion Y2Max プロパティ

        #region Y2Step プロパティ
        /// <summary>
        /// Y2Step 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty Y2StepProperty = DependencyProperty.Register("Y2Step", typeof(double), typeof(LineGraph), new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 第 2 主軸目盛の間隔を取得または設定します。
        /// </summary>
        public double Y2Step
        {
            get { return (double)GetValue(Y2StepProperty); }
            set { SetValue(Y2StepProperty, value); }
        }
        #endregion Y2Step プロパティ

        #region Y2StringFormat プロパティ
        /// <summary>
        /// Y2StringFormat 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty Y2StringFormatProperty = DependencyProperty.Register("Y2StringFormat", typeof(string), typeof(LineGraph), new FrameworkPropertyMetadata("#0", OnY2StringFormatPropertyChanged));

        /// <summary>
        /// 第 2 主軸目盛の表示形式を取得または設定します。
        /// </summary>
        public string Y2StringFormat
        {
            get { return (string)GetValue(Y2StringFormatProperty); }
            set { SetValue(Y2StringFormatProperty, value); }
        }

        /// <summary>
        /// Y2StringFormat プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnY2StringFormatPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as LineGraph;
            if (control != null)
            {
                if (control.ItemsSource != null)
                {
                    foreach (var obj in control.ItemsSource)
                    {
                        if (obj is LineGraphItem)
                        {
                            var item = obj as LineGraphItem;
                            if (item.IsSecond)
                                item.YStringFormat = control.Y2StringFormat;
                        }
                    }
                }
            }
        }
        #endregion Y2StringFormat プロパティ

        #region Y2FontSize プロパティ
        /// <summary>
        /// Y2FontSize 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty Y2FontSizeProperty = DependencyProperty.Register("Y2FontSize", typeof(double), typeof(LineGraph), new FrameworkPropertyMetadata(16.0));

        /// <summary>
        /// 第 2 主軸目盛のフォントサイズを取得または設定します。
        /// </summary>
        public double Y2FontSize
        {
            get { return (double)GetValue(Y2FontSizeProperty); }
            set { SetValue(Y2FontSizeProperty, value); }
        }
        #endregion Y2FontSize プロパティ

        #region Y2GridPen プロパティ
        /// <summary>
        /// YGridPen 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty Y2GridPenProperty = DependencyProperty.Register("Y2GridPen", typeof(Pen), typeof(LineGraph), new FrameworkPropertyMetadata(new Pen(Brushes.LightGray, 1.0) { DashStyle = DashStyles.Dash }));

        /// <summary>
        /// 第 2 主軸目盛の線種を取得または設定します。
        /// </summary>
        public Pen Y2GridPen
        {
            get { return (Pen)GetValue(Y2GridPenProperty); }
            set { SetValue(Y2GridPenProperty, value); }
        }
        #endregion Y2GridPen プロパティ

        #region ItemsSource プロパティ
        /// <summary>
        /// ItemsSource 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(LineGraph), new FrameworkPropertyMetadata(null, (s, e) => (s as LineGraph).OnItemsSourceChanged(e.OldValue as IEnumerable, e.NewValue as IEnumerable)));

        /// <summary>
        /// グラフデータコレクションを取得または設定します。
        /// コレクションの子要素は <c>YKToolkit.Controls.LineGraphItems</c> を使用します。
        /// </summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        /// <summary>
        /// ItemsSource プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            if (oldValue != null)
            {
                if (oldValue is INotifyCollectionChanged)
                    (oldValue as INotifyCollectionChanged).CollectionChanged -= ItemsSourceCollectionChanged;
                ResetGraphItem();
            }
            if (newValue != null)
            {
                if (newValue is INotifyCollectionChanged)
                    (newValue as INotifyCollectionChanged).CollectionChanged += ItemsSourceCollectionChanged;
                foreach (LineGraphItem item in newValue)
                    AddGraphItem(item);
            }
        }

        /// <summary>
        /// ItemsSource コレクション子要素変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void ItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (LineGraphItem item in e.NewItems)
                        AddGraphItem(item);
                    break;

                case NotifyCollectionChangedAction.Move:
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (LineGraphItem item in e.OldItems)
                        RemoveGraphItem(item);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    ResetGraphItem();
                    break;

                // あり得ない
                default:
                    break;
            }
        }

        /// <summary>
        /// グラフデータを追加します。
        /// </summary>
        /// <param name="item">追加するグラフデータ</param>
        private void AddGraphItem(LineGraphItem item)
        {
            if (GraphPanel != null)
            {
                item.XMin = this.XMin;
                item.XMax = this.XMax;
                item.YMin = item.IsSecond ? this.Y2Min : this.YMin;
                item.YMax = item.IsSecond ? this.Y2Max : this.YMax;
                item.XStringFormat = this.XStringFormat;
                item.YStringFormat = item.IsSecond ? this.Y2StringFormat : this.YStringFormat;
                item.IsSecondChanged += item_IsSecondChanged;
                GraphPanel.Children.Add(item);
            }
        }

        /// <summary>
        /// 子要素の IsSecond プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void item_IsSecondChanged(object sender, System.EventArgs e)
        {
            var item = sender as LineGraphItem;
            if (item != null)
            {
                item.YMax = double.MaxValue;
                item.YMin = item.IsSecond ? this.Y2Min : this.YMin;
                item.YMax = item.IsSecond ? this.Y2Max : this.YMax;
                item.YStringFormat = item.IsSecond ? this.Y2StringFormat : this.YStringFormat;
            }
        }

        /// <summary>
        /// グラフデータを削除します。
        /// </summary>
        /// <param name="item">削除するグラフデータ</param>
        private void RemoveGraphItem(LineGraphItem item)
        {
            item.IsSecondChanged -= item_IsSecondChanged;
            if (GraphPanel != null)
            {
                GraphPanel.Children.Remove(item);
            }
        }

        /// <summary>
        /// グラフデータをすべて削除します。
        /// </summary>
        private void ResetGraphItem()
        {
            if (GraphPanel != null)
            {
                foreach (var obj in GraphPanel.Children)
                {
                    var item = obj as LineGraphItem;
                    if (item != null)
                        item.IsSecondChanged -= item_IsSecondChanged;
                }
                GraphPanel.Children.Clear();
            }
        }
        #endregion ItemsSource プロパティ

        #region GraphAreaMargin プロパティ
        /// <summary>
        /// GraphAreaMargin 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty GraphAreaMarginProperty = DependencyProperty.Register("GraphAreaMargin", typeof(Thickness), typeof(LineGraph), new FrameworkPropertyMetadata(new Thickness(80, 40, 20, 60), FrameworkPropertyMetadataOptions.AffectsRender, (s, e) => (s as LineGraph).OnGraphAreaMarginChanged((Thickness)e.OldValue, (Thickness)e.NewValue)));

        /// <summary>
        /// グラフ表示領域の余白を取得または設定します。
        /// </summary>
        public Thickness GraphAreaMargin
        {
            get { return (Thickness)GetValue(GraphAreaMarginProperty); }
            set { SetValue(GraphAreaMarginProperty, value); }
        }

        /// <summary>
        /// GraphAreaMargin プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private void OnGraphAreaMarginChanged(Thickness oldValue, Thickness newValue)
        {
            UpdateMoveArea();
        }
        #endregion GraphAreaMargin プロパティ

        #region Title プロパティ
        /// <summary>
        /// Title 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(LineGraph), new FrameworkPropertyMetadata("Graph Title", FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// グラフタイトルを取得または設定します。
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        #endregion Title プロパティ

        #region TitleFontSize プロパティ
        /// <summary>
        /// TitleFontSize 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty TitleFontSizeProperty = DependencyProperty.Register("TitleFontSize", typeof(double), typeof(LineGraph), new FrameworkPropertyMetadata(16.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// グラフタイトルのフォントサイズを取得または設定します。
        /// </summary>
        public double TitleFontSize
        {
            get { return (double)GetValue(TitleFontSizeProperty); }
            set { SetValue(TitleFontSizeProperty, value); }
        }
        #endregion TitleFontSize プロパティ

        #region XLabel プロパティ
        /// <summary>
        /// XLabel 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty XLabelProperty = DependencyProperty.Register("XLabel", typeof(string), typeof(LineGraph), new FrameworkPropertyMetadata("X-Axis Label", FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 横軸ラベルを取得または設定します。
        /// </summary>
        public string XLabel
        {
            get { return (string)GetValue(XLabelProperty); }
            set { SetValue(XLabelProperty, value); }
        }
        #endregion XLabel プロパティ

        #region XLabelFontSize プロパティ
        /// <summary>
        /// XLabelFontSize 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty XLabelFontSizeProperty = DependencyProperty.Register("XLabelFontSize", typeof(double), typeof(LineGraph), new FrameworkPropertyMetadata(16.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 横軸ラベルのフォントサイズを取得または設定します。
        /// </summary>
        public double XLabelFontSize
        {
            get { return (double)GetValue(XLabelFontSizeProperty); }
            set { SetValue(XLabelFontSizeProperty, value); }
        }
        #endregion XLabelFontSize プロパティ

        #region YLabel プロパティ
        /// <summary>
        /// YLabel 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty YLabelProperty = DependencyProperty.Register("YLabel", typeof(string), typeof(LineGraph), new FrameworkPropertyMetadata("Y-Axis Label", FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 縦軸ラベルを取得または設定します。
        /// </summary>
        public string YLabel
        {
            get { return (string)GetValue(YLabelProperty); }
            set { SetValue(YLabelProperty, value); }
        }
        #endregion YLabel プロパティ

        #region YLabelFontSize プロパティ
        /// <summary>
        /// YLabelFontSize 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty YLabelFontSizeProperty = DependencyProperty.Register("YLabelFontSize", typeof(double), typeof(LineGraph), new FrameworkPropertyMetadata(16.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 縦軸ラベルのフォントサイズを取得または設定します。
        /// </summary>
        public double YLabelFontSize
        {
            get { return (double)GetValue(YLabelFontSizeProperty); }
            set { SetValue(YLabelFontSizeProperty, value); }
        }
        #endregion YLabelFontSize プロパティ

        #region Y2Label プロパティ
        /// <summary>
        /// Y2Label 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty Y2LabelProperty = DependencyProperty.Register("Y2Label", typeof(string), typeof(LineGraph), new FrameworkPropertyMetadata("Y2-Axis Label", FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 第 2 主軸ラベルを取得または設定します。
        /// </summary>
        public string Y2Label
        {
            get { return (string)GetValue(Y2LabelProperty); }
            set { SetValue(Y2LabelProperty, value); }
        }
        #endregion Y2Label プロパティ

        #region Y2LabelFontSize プロパティ
        /// <summary>
        /// YLabelFontSize 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty Y2LabelFontSizeProperty = DependencyProperty.Register("Y2LabelFontSize", typeof(double), typeof(LineGraph), new FrameworkPropertyMetadata(16.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 第 2 主軸ラベルのフォントサイズを取得または設定します。
        /// </summary>
        public double Y2LabelFontSize
        {
            get { return (double)GetValue(Y2LabelFontSizeProperty); }
            set { SetValue(Y2LabelFontSizeProperty, value); }
        }
        #endregion Y2LabelFontSize プロパティ

        #region CornerRadius プロパティ
        /// <summary>
        /// CornerRadius 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(LineGraph), new FrameworkPropertyMetadata(new CornerRadius(6)));

        /// <summary>
        /// 境界線の丸みを取得または設定します。
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        #endregion CornerRadius プロパティ

        #region IsLegendEnabled プロパティ
        /// <summary>
        /// IsLegendEnabled 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsLegendEnabledProperty = DependencyProperty.Register("IsLegendEnabled", typeof(bool), typeof(LineGraph), new FrameworkPropertyMetadata(true));

        /// <summary>
        /// 凡例の有効性を取得または設定します。
        /// </summary>
        public bool IsLegendEnabled
        {
            get { return (bool)GetValue(IsLegendEnabledProperty); }
            set { SetValue(IsLegendEnabledProperty, value); }
        }
        #endregion IsLegendEnabled プロパティ

        #region LegendFontSize プロパティ
        /// <summary>
        /// LegendFontSize 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty LegendFontSizeProperty = DependencyProperty.Register("LegendFontSize", typeof(double), typeof(LineGraph), new FrameworkPropertyMetadata(16.0));

        /// <summary>
        /// 凡例のフォントサイズを取得または設定します。
        /// </summary>
        public double LegendFontSize
        {
            get { return (double)GetValue(LegendFontSizeProperty); }
            set { SetValue(LegendFontSizeProperty, value); }
        }
        #endregion LegendFontSize プロパティ

        #region LegendPositionLeft プロパティ
        /// <summary>
        /// LegendPositionLeft 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty LegendPositionLeftProperty = DependencyProperty.Register("LegendPositionLeft", typeof(double), typeof(LineGraph), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 凡例の左位置を取得または設定します。
        /// </summary>
        public double LegendPositionLeft
        {
            get { return (double)GetValue(LegendPositionLeftProperty); }
            set { SetValue(LegendPositionLeftProperty, value); }
        }
        #endregion LegendPositionLeft プロパティ

        #region LegendPositionTop プロパティ
        /// <summary>
        /// LegendPositionTop 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty LegendPositionTopProperty = DependencyProperty.Register("LegendPositionTop", typeof(double), typeof(LineGraph), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 凡例の上位置を取得または設定します。
        /// </summary>
        public double LegendPositionTop
        {
            get { return (double)GetValue(LegendPositionTopProperty); }
            set { SetValue(LegendPositionTopProperty, value); }
        }
        #endregion LegendPositionTop プロパティ

        #region IsMouseOverInformationEnabled プロパティ
        /// <summary>
        /// IsMouseOverInformationEnabled 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsMouseOverInformationEnabledProperty = DependencyProperty.Register("IsMouseOverInformationEnabled", typeof(bool), typeof(LineGraph), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// マウスオーバー時の情報表示の有効性を取得または設定します。
        /// </summary>
        public bool IsMouseOverInformationEnabled
        {
            get { return (bool)GetValue(IsMouseOverInformationEnabledProperty); }
            set { SetValue(IsMouseOverInformationEnabledProperty, value); }
        }
        #endregion IsMouseOverInformationEnabled プロパティ

        #region 描画関連オーバーライド
        /// <summary>
        /// 描画処理をおこないます。
        /// </summary>
        /// <param name="dc">描画するコンテキスト</param>
        protected override void OnRender(DrawingContext dc)
        {
            //base.OnRender(dc);

            var typeface = new Typeface(this.FontFamily.FamilyNames.Select(x => x.Value).First());

            #region 移動軸強調描画
            if (this.IsXMoveEnabled && this._xMoveArea.HasValue)
            {
                dc.DrawRectangle(this._xAxisMoveAreaBrush, null, this._xMoveArea.Value);
            }
            if (this.IsYMoveEnabled && this._yMoveArea.HasValue)
            {
                dc.DrawRectangle(this._yAxisMoveAreaBrush, null, this._yMoveArea.Value);
            }
            if (this.IsY2Enabled && this.IsY2MoveEnabled && this._y2MoveArea.HasValue)
            {
                dc.DrawRectangle(this._yAxisMoveAreaBrush, null, this._y2MoveArea.Value);
            }
            #endregion 移動軸強調描画

            #region タイトル/ラベルの描画
            // グラフタイトルの描画
            if (this.Title != null)
            {
                var text = new FormattedText(this.Title, CultureInfo.CurrentUICulture, this.FlowDirection, typeface, this.TitleFontSize, this.Foreground);
                this._titleSize = new Size(text.Width, text.Height);
                dc.DrawText(text, new Point(this.ActualWidth / 2.0 - text.Width / 2.0, this.GraphAreaMargin.Top - text.Height - 4));
            }
            else
            {
                this._titleSize = new Size();
            }

            // 横軸ラベルの描画
            if (this.XLabel != null)
            {
                var text = new FormattedText(this.XLabel, CultureInfo.CurrentUICulture, this.FlowDirection, typeface, this.XLabelFontSize, this.Foreground);
                this._xLabelSize = new Size(text.Width, text.Height);
                dc.DrawText(text, new Point(this.GraphAreaMargin.Left + (this.ActualWidth - this.GraphAreaMargin.Left - this.GraphAreaMargin.Right) / 2.0 - text.Width / 2.0, this.ActualHeight - text.Height));
            }
            else
            {
                this._xLabelSize = new Size();
            }

            // 縦軸ラベルの描画
            if (this.YLabel != null)
            {
                var text = new FormattedText(this.YLabel, CultureInfo.CurrentUICulture, this.FlowDirection, typeface, this.YLabelFontSize, this.Foreground);
                this._yLabelSize = new Size(text.Width, text.Height);
                dc.PushTransform(new RotateTransform(-90));
                dc.DrawText(text, new Point(-this.ActualHeight / 2.0 - text.Width / 2.0, 0.0));
                dc.Pop();
            }
            else
            {
                this._yLabelSize = new Size();
            }

            // 第 2 主軸ラベルの描画
            if (IsY2Enabled && (this.Y2Label != null))
            {
                var text = new FormattedText(this.Y2Label, CultureInfo.CurrentUICulture, this.FlowDirection, typeface, this.Y2LabelFontSize, this.Foreground);
                this._y2LabelSize = new Size(text.Width, text.Height);
                dc.PushTransform(new RotateTransform(-90));
                dc.DrawText(text, new Point(-this.ActualHeight / 2.0 - text.Width / 2.0, this.ActualWidth - text.Height));
                dc.Pop();
            }
            else
            {
                this._y2LabelSize = new Size();
            }
            #endregion タイトル/ラベルの描画

            UpdateMoveArea();
        }

        private Size _titleSize;

        private Size _xLabelSize;

        private Size _yLabelSize;

        private Size _y2LabelSize;

        /// <summary>
        /// 横軸移動領域塗潰しブラシ
        /// </summary>
        private LinearGradientBrush _xAxisMoveAreaBrush;

        /// <summary>
        /// 縦軸/第 2 2主軸移動領域塗潰しブラシ
        /// </summary>
        private LinearGradientBrush _yAxisMoveAreaBrush;

        #endregion 描画関連オーバーライド

        #region イベントハンドラ
        /// <summary>
        /// サイズ変更時イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateMoveArea();
        }
        #endregion イベントハンドラ

        #region 軸移動/拡大
        /// <summary>
        /// 横軸移動可能領域
        /// </summary>
        private Rect? _xMoveArea;

        /// <summary>
        /// 縦軸移動可能領域
        /// </summary>
        private Rect? _yMoveArea;

        /// <summary>
        /// 第 2 主軸移動可能領域
        /// </summary>
        private Rect? _y2MoveArea;

        private bool _isXMoveEnabled;
        /// <summary>
        /// 横軸移動の有効性
        /// </summary>
        private bool IsXMoveEnabled
        {
            get { return _isXMoveEnabled; }
            set
            {
                if (_isXMoveEnabled != value)
                {
                    _isXMoveEnabled = value;
                    this.InvalidateVisual();
                }
            }
        }

        private bool _isYMoveEnabled;
        /// <summary>
        /// 縦軸移動の有効性
        /// </summary>
        private bool IsYMoveEnabled
        {
            get { return _isYMoveEnabled; }
            set
            {
                if (_isYMoveEnabled != value)
                {
                    _isYMoveEnabled = value;
                    this.InvalidateVisual();
                }
            }
        }

        private bool _isY2MoveEnabled;
        /// <summary>
        /// 第 2 主軸移動の有効性
        /// </summary>
        private bool IsY2MoveEnabled
        {
            get { return _isY2MoveEnabled; }
            set
            {
                if (_isY2MoveEnabled != value)
                {
                    _isY2MoveEnabled = value;
                    this.InvalidateVisual();
                }
            }
        }

        /// <summary>
        /// マウス左ボタン押下時のコントロール座標
        /// </summary>
        private Point? _moveStartPoint;

        /// <summary>
        /// 移動モード処理用バッファ
        /// </summary>
        private Point _moveTempPoint;

        /// <summary>
        /// マウスホイール操作イベントハンドラ
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            if (this.IsXMoveEnabled)
            {
                e.Handled = true;

                var d = GetGraphDistanceFromControlDistance(e.Delta, 0);

                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                {
                    // 拡大モード
                    if (d.X > 0)
                    {
                        if (this.XMax - this.XMin > 2.0 * this.XStep)
                        {
                            this.XMin += this.XStep;
                            this.XMax -= this.XStep;
                        }
                    }
                    else
                    {
                        this.XMin -= this.XStep;
                        this.XMax += this.XStep;
                    }

                    this.XStep = (this.XMax - this.XMin) / 10.0;
                }
                else
                {
                    // 移動モード
                    // 一応変更する順番を気にしておく
                    if (d.X > 0)
                    {
                        this.XMin -= this.XStep;
                        this.XMax -= this.XStep;
                    }
                    else
                    {
                        this.XMax += this.XStep;
                        this.XMin += this.XStep;
                    }
                }
            }

            if (this.IsYMoveEnabled)
            {
                e.Handled = true;

                var d = GetGraphDistanceFromControlDistance(0, e.Delta);

                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                {
                    // 拡大モード
                    if (d.Y > 0)
                    {
                        if (this.YMax - this.YMin > 2.0 * this.YStep)
                        {
                            this.YMin += this.YStep;
                            this.YMax -= this.YStep;
                        }
                    }
                    else
                    {
                        this.YMin -= this.YStep;
                        this.YMax += this.YStep;
                    }

                    this.YStep = (this.YMax - this.YMin) / 10.0;
                }
                else
                {
                    // 移動モード
                    // 一応変更する順番を気にしておく
                    if (d.Y > 0)
                    {
                        this.YMin -= this.YStep;
                        this.YMax -= this.YStep;
                    }
                    else
                    {
                        this.YMax += this.YStep;
                        this.YMin += this.YStep;
                    }
                }
            }

            if (this.IsY2MoveEnabled)
            {
                e.Handled = true;

                var d = GetGraphDistanceFromControlDistance(0, e.Delta, true);

                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                {
                    // 拡大モード
                    if (d.Y > 0)
                    {
                        if (this.Y2Max - this.Y2Min > 2.0 * this.Y2Step)
                        {
                            this.Y2Min += this.Y2Step;
                            this.Y2Max -= this.Y2Step;
                        }
                    }
                    else
                    {
                        this.Y2Min -= this.Y2Step;
                        this.Y2Max += this.Y2Step;
                    }

                    this.Y2Step = (this.Y2Max - this.Y2Min) / 10.0;
                }
                else
                {
                    // 移動モード
                    // 一応変更する順番を気にしておく
                    if (d.Y > 0)
                    {
                        this.Y2Min -= this.Y2Step;
                        this.Y2Max -= this.Y2Step;
                    }
                    else
                    {
                        this.Y2Max += this.Y2Step;
                        this.Y2Min += this.Y2Step;
                    }
                }
            }

            if (!e.Handled)
                base.OnPreviewMouseWheel(e);
        }

        /// <summary>
        /// マウス左ボタン押下イベントハンドラ
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            var pt = e.GetPosition(this);
            var legendPos = new Rect(this.LegendPositionLeft, this.LegendPositionTop, this.LegendPanel.ActualWidth, this.LegendPanel.ActualHeight);
            if (!legendPos.Contains(pt))
            {
                _moveStartPoint = pt;

                if (this.IsXMoveEnabled || this.IsYMoveEnabled || this.IsY2MoveEnabled)
                {
                    _moveTempPoint = e.GetPosition(this);
                    if (this.CaptureMouse())
                        e.Handled = true;
                }

                if (!e.Handled)
                    base.OnPreviewMouseLeftButtonDown(e);
            }
        }

        /// <summary>
        /// マウス左ボタンリリースイベントハンドラ
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (this.IsMouseCaptured)
            {
                _moveStartPoint = null;
                this.ReleaseMouseCapture();
            }

            base.OnPreviewMouseLeftButtonUp(e);
        }

        /// <summary>
        /// マウス移動時イベントハンドラ
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            var pt = e.GetPosition(this);

            if (this.IsMouseCaptured)
            {
                #region 移動処理
                var dx = pt.X - _moveTempPoint.X;
                var dy = pt.Y - _moveTempPoint.Y;

                if (this.IsXMoveEnabled)
                {
                    var d = GetGraphDistanceFromControlDistance(dx, dy);
                    // 一応変更する順番を気にしておく
                    if (d.X > 0)
                    {
                        this.XMin -= d.X;
                        this.XMax -= d.X;
                    }
                    else
                    {
                        this.XMax -= d.X;
                        this.XMin -= d.X;
                    }
                }

                if (this.IsYMoveEnabled)
                {
                    var d = GetGraphDistanceFromControlDistance(dx, dy);
                    // 一応変更する順番を気にしておく
                    if (d.Y > 0)
                    {
                        this.YMax += d.Y;
                        this.YMin += d.Y;
                    }
                    else
                    {
                        this.YMin += d.Y;
                        this.YMax += d.Y;
                    }
                }

                if (this.IsY2MoveEnabled)
                {
                    var d = GetGraphDistanceFromControlDistance(dx, dy, true);
                    // 一応変更する順番を気にしておく
                    if (d.Y > 0)
                    {
                        this.Y2Max += d.Y;
                        this.Y2Min += d.Y;
                    }
                    else
                    {
                        this.Y2Min += d.Y;
                        this.Y2Max += d.Y;
                    }
                }
                _moveTempPoint = pt;
                #endregion 移動処理

                e.Handled = true;
            }
            else
            {
                #region 移動/拡大可能判別
                var graphPanelArea = new Rect(this.GraphAreaMargin.Left, this.GraphAreaMargin.Top, this.GraphPanel.ActualWidth, this.GraphPanel.ActualHeight);
                var legendPos = new Rect(this.LegendPositionLeft, this.LegendPositionTop, this.LegendPanel.ActualWidth, this.LegendPanel.ActualHeight);
                var isLegendArea = legendPos.Contains(pt);
                if (graphPanelArea.Contains(pt) && !isLegendArea)
                {
                    var isControl = Keyboard.Modifiers.HasFlag(ModifierKeys.Control);
                    var isShift = Keyboard.Modifiers.HasFlag(ModifierKeys.Shift);
                    var isAlt = Keyboard.Modifiers.HasFlag(ModifierKeys.Alt);

                    this.IsXMoveEnabled = isControl || (this.IsY2Enabled && isShift);
                    this.IsYMoveEnabled = isControl;
                    this.IsY2MoveEnabled = this.IsY2Enabled && (isShift || (isControl && isAlt));
                }
                else
                {
                    this.IsXMoveEnabled = !isLegendArea && this._xMoveArea != null ? this._xMoveArea.Value.Contains(pt) : false;
                    this.IsYMoveEnabled = !isLegendArea && this._yMoveArea != null ? this._yMoveArea.Value.Contains(pt) : false;
                    this.IsY2MoveEnabled = !isLegendArea && this._y2MoveArea != null && this.IsY2Enabled ? this._y2MoveArea.Value.Contains(pt) : false;
                }
                #endregion 移動/拡大可能判別
            }

            if (!e.Handled)
                base.OnMouseMove(e);
        }
        #endregion 軸移動/拡大

        #region 座標変換ヘルパ
        /// <summary>
        /// コントロール座標の距離をグラフ座標の距離に変換します。
        /// </summary>
        /// <param name="pt">コントロール座標での距離を指定します。</param>
        /// <param name="isSecond">第 2 主軸を使用する場合に true を指定します。</param>
        /// <returns>グラフ座標での距離</returns>
        private Point GetGraphDistanceFromControlDistance(Point pt, bool isSecond = false)
        {
            return GetGraphDistanceFromControlDistance(pt.X, pt.Y, isSecond);
        }

        /// <summary>
        /// コントロール座標の距離をグラフ座標の距離に変換します。
        /// </summary>
        /// <param name="x">コントロールの横軸座標での距離</param>
        /// <param name="y">コントロールの縦軸座標での距離</param>
        /// <param name="isSecond">第 2 主軸を使用する場合に true を指定します。</param>
        /// <returns>グラフ座標での距離</returns>
        private Point GetGraphDistanceFromControlDistance(double x, double y, bool isSecond = false)
        {
            var ymin = isSecond ? this.Y2Min : this.YMin;
            var ymax = isSecond ? this.Y2Max : this.YMax;
            var xx = (this.XMax - this.XMin) * x / GraphPanel.ActualWidth;
            var yy = (ymax - ymin) * y / GraphPanel.ActualHeight;
            return new Point(xx, yy);
        }
        #endregion 座標変換ヘルパ

        #region ヘルパ
        /// <summary>
        /// 軸移動/拡大可能を示す矩形領域の更新
        /// </summary>
        private void UpdateMoveArea()
        {
            this._xMoveArea = this.ActualWidth - this.GraphAreaMargin.Left - this.GraphAreaMargin.Right <= 0.0 ? null as Rect? :
                new Rect(
                    this.GraphAreaMargin.Left,
                    this.ActualHeight - this.GraphAreaMargin.Bottom + 4.0,
                    this.ActualWidth - this.GraphAreaMargin.Left - this.GraphAreaMargin.Right,
                    this.GraphAreaMargin.Bottom - this._xLabelSize.Height > 0.0 ? this.GraphAreaMargin.Bottom - this._xLabelSize.Height : this.GraphAreaMargin.Bottom);

            this._yMoveArea = this.ActualHeight - this.GraphAreaMargin.Top - this.GraphAreaMargin.Bottom <= 0.0 ? null as Rect? :
                new Rect(
                    this._yLabelSize.Height,
                    this.GraphAreaMargin.Top,
                    this.GraphAreaMargin.Left - this._yLabelSize.Height - 4.0 > 0.0 ? this.GraphAreaMargin.Left - this._yLabelSize.Height - 4.0 : this.GraphAreaMargin.Left,
                    this.ActualHeight - this.GraphAreaMargin.Top - this.GraphAreaMargin.Bottom);

            this._y2MoveArea = this.ActualHeight - this.GraphAreaMargin.Top - this.GraphAreaMargin.Bottom <= 0.0 ? null as Rect? :
                new Rect(
                    this.ActualWidth - this.GraphAreaMargin.Right + 4.0,
                    this.GraphAreaMargin.Top,
                    this.GraphAreaMargin.Right - this._y2LabelSize.Height - 4.0 > 0.0 ? this.GraphAreaMargin.Right - this._y2LabelSize.Height - 4.0 : this.GraphAreaMargin.Right,
                    this.ActualHeight - this.GraphAreaMargin.Top - this.GraphAreaMargin.Bottom);
        }
        #endregion ヘルパ
    }
}
