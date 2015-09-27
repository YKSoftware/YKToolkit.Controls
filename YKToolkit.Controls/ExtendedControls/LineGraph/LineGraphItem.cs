namespace YKToolkit.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    /// <summary>
    /// <c>YKToolkit.Controls.LineGraph</c> コントロールで使用する折れ線グラフ用のデータを保持するクラスです。
    /// </summary>
    public class LineGraphItem : FrameworkElement
    {
        /// <summary>
        /// マーカー種別を表します。
        /// </summary>
        public enum MarkerTypes
        {
            /// <summary>
            /// 円
            /// </summary>
            Ellipse,

            /// <summary>
            /// 矩形
            /// </summary>
            Rectangle,
        };

        /// <summary>
        /// アニメーション種別を表します。
        /// </summary>
        public enum AnimationTypes
        {
            /// <summary>
            /// アニメーションなし
            /// </summary>
            None,

            /// <summary>
            /// フェードイン
            /// </summary>
            FadeIn,

            /// <summary>
            /// スライドイン
            /// </summary>
            SlideIn,

            /// <summary>
            /// 回転
            /// </summary>
            Rotate,
        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public LineGraphItem()
        {
            var group = new TransformGroup();
            group.Children.Add(new RotateTransform());
            group.Children.Add(new ScaleTransform());
            group.Children.Add(new SkewTransform());
            group.Children.Add(new TranslateTransform());
            this.RenderTransform = group;
        }

        #region XAxisData プロパティ
        /// <summary>
        /// XAxisData 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty XAxisDataProperty = DependencyProperty.Register("XAxisData", typeof(IEnumerable), typeof(LineGraphItem), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, (s, e) => (s as LineGraphItem).OnXAxisDataChanged(e.OldValue as IEnumerable, e.NewValue as IEnumerable)));

        /// <summary>
        /// 横軸データを取得または設定します。
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable XAxisData
        {
            get { return (IEnumerable)GetValue(XAxisDataProperty); }
            set { SetValue(XAxisDataProperty, value); }
        }

        /// <summary>
        /// XAxisData プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private void OnXAxisDataChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            if (oldValue != null)
            {
                if (oldValue is INotifyCollectionChanged)
                    (oldValue as INotifyCollectionChanged).CollectionChanged -= OnXAxisDataCollectionChanged;
            }
            if (newValue != null)
            {
                if (newValue is INotifyCollectionChanged)
                    (newValue as INotifyCollectionChanged).CollectionChanged += OnXAxisDataCollectionChanged;
                BeginAnimation();
            }
        }

        /// <summary>
        /// XAxisData コレクション子要素変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnXAxisDataCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    break;

                case NotifyCollectionChangedAction.Move:
                    break;

                case NotifyCollectionChangedAction.Remove:
                    break;

                case NotifyCollectionChangedAction.Replace:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    break;

                // あり得ない
                default:
                    break;
            }

            this.InvalidateVisual();
        }
        #endregion XAxisData プロパティ

        #region YAxisData プロパティ
        /// <summary>
        /// YAxisData 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty YAxisDataProperty = DependencyProperty.Register("YAxisData", typeof(IEnumerable), typeof(LineGraphItem), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, (s, e) => (s as LineGraphItem).OnYAxisDataChanged(e.OldValue as IEnumerable, e.NewValue as IEnumerable)));

        /// <summary>
        /// 縦軸データを取得または設定します。
        /// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEnumerable YAxisData
        {
            get { return (IEnumerable)GetValue(YAxisDataProperty); }
            set { SetValue(YAxisDataProperty, value); }
        }

        /// <summary>
        /// YAxisData プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private void OnYAxisDataChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            if (oldValue != null)
            {
                if (oldValue is INotifyCollectionChanged)
                    (oldValue as INotifyCollectionChanged).CollectionChanged -= OnYAxisDataCollectionChanged;
            }
            if (newValue != null)
            {
                if (newValue is INotifyCollectionChanged)
                    (newValue as INotifyCollectionChanged).CollectionChanged += OnYAxisDataCollectionChanged;
                BeginAnimation();
            }
        }

        /// <summary>
        /// YAxisData コレクション子要素変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnYAxisDataCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    break;

                case NotifyCollectionChangedAction.Move:
                    break;

                case NotifyCollectionChangedAction.Remove:
                    break;

                case NotifyCollectionChangedAction.Replace:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    break;

                // あり得ない
                default:
                    break;
            }

            this.InvalidateVisual();
        }
        #endregion YAxisData プロパティ

        #region XMin プロパティ
        /// <summary>
        /// XMin 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty XMinProperty = DependencyProperty.Register("XMin", typeof(double), typeof(LineGraphItem), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

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
        /// XMin 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty XMaxProperty = DependencyProperty.Register("XMax", typeof(double), typeof(LineGraphItem), new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 横軸の最小値を取得または設定します。
        /// </summary>
        public double XMax
        {
            get { return (double)GetValue(XMaxProperty); }
            set { SetValue(XMaxProperty, value); }
        }
        #endregion XMax プロパティ

        #region XStringFormat プロパティ
        /// <summary>
        /// XStringFormat 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty XStringFormatProperty = DependencyProperty.Register("XStringFormat", typeof(string), typeof(LineGraphItem), new FrameworkPropertyMetadata("#0"));

        /// <summary>
        /// 横軸目盛の表示形式を取得または設定します。
        /// </summary>
        public string XStringFormat
        {
            get { return (string)GetValue(XStringFormatProperty); }
            set { SetValue(XStringFormatProperty, value); }
        }
        #endregion XStringFormat プロパティ

        #region YMin プロパティ
        /// <summary>
        /// XMin 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty YMinProperty = DependencyProperty.Register("YMin", typeof(double), typeof(LineGraphItem), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 横軸の最小値を取得または設定します。
        /// </summary>
        public double YMin
        {
            get { return (double)GetValue(YMinProperty); }
            set { SetValue(YMinProperty, value); }
        }
        #endregion YMin プロパティ

        #region YMax プロパティ
        /// <summary>
        /// XMin 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty YMaxProperty = DependencyProperty.Register("YMax", typeof(double), typeof(LineGraphItem), new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 横軸の最小値を取得または設定します。
        /// </summary>
        public double YMax
        {
            get { return (double)GetValue(YMaxProperty); }
            set { SetValue(YMaxProperty, value); }
        }
        #endregion XMax プロパティ

        #region YStringFormat プロパティ
        /// <summary>
        /// YStringFormat 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty YStringFormatProperty = DependencyProperty.Register("YStringFormat", typeof(string), typeof(LineGraphItem), new FrameworkPropertyMetadata("#0"));

        /// <summary>
        /// 縦軸目盛の表示形式を取得または設定します。
        /// </summary>
        public string YStringFormat
        {
            get { return (string)GetValue(YStringFormatProperty); }
            set { SetValue(YStringFormatProperty, value); }
        }
        #endregion YStringFormat プロパティ

        #region IsSecond プロパティ
        /// <summary>
        /// IsSecond 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsSecondProperty = DependencyProperty.Register("IsSecond", typeof(bool), typeof(LineGraphItem), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.AffectsRender, OnIsSecondPropertyChanged));

        /// <summary>
        /// 第 2 主軸を使用するかどうかを取得または設定します。
        /// </summary>
        public bool IsSecond
        {
            get { return (bool)GetValue(IsSecondProperty); }
            set { SetValue(IsSecondProperty, value); }
        }

        /// <summary>
        /// IsSecond プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnIsSecondPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var element = sender as LineGraphItem;
            if (element != null)
            {
                element.RaiseIsSecondChanged();
            }
        }
        #endregion IsSecond プロパティ

        #region Fill プロパティ
        /// <summary>
        /// Fill 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(Brush), typeof(LineGraphItem), new FrameworkPropertyMetadata(Brushes.Red, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// データ点塗潰しブラシを取得または設定します。
        /// </summary>
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }
        #endregion Fill プロパティ

        #region Stroke プロパティ
        /// <summary>
        /// Stroke 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register("Stroke", typeof(Brush), typeof(LineGraphItem), new FrameworkPropertyMetadata(Brushes.Red, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 折れ線塗潰し色を取得または設定します。
        /// </summary>
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }
        #endregion Stroke プロパティ

        #region Thickness プロパティ
        /// <summary>
        /// Thickness 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register("Thickness", typeof(double), typeof(LineGraphItem), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 折れ線の太さを取得または設定します。
        /// </summary>
        public double Thickness
        {
            get { return (double)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }
        #endregion Thickness プロパティ

        #region MarkerPen プロパティ
        /// <summary>
        /// MarkerPen 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty MarkerPenProperty = DependencyProperty.Register("MarkerPen", typeof(Pen), typeof(LineGraphItem), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// データ点境界線ペンを取得または設定します。
        /// </summary>
        public Pen MarkerPen
        {
            get { return (Pen)GetValue(MarkerPenProperty); }
            set { SetValue(MarkerPenProperty, value); }
        }
        #endregion MarkerPen プロパティ

        #region MarkerSize プロパティ
        /// <summary>
        /// MarkerSize 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty MarkerSizeProperty = DependencyProperty.Register("MarkerSize", typeof(Size), typeof(LineGraphItem), new FrameworkPropertyMetadata(new Size(3.0, 3.0), FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// データ点境界線ペンを取得または設定します。
        /// </summary>
        public Size MarkerSize
        {
            get { return (Size)GetValue(MarkerSizeProperty); }
            set { SetValue(MarkerSizeProperty, value); }
        }
        #endregion Fill プロパティ

        #region MarkerType プロパティ
        /// <summary>
        /// MarkerType 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty MarkerTypeProperty = DependencyProperty.Register("MarkerType", typeof(MarkerTypes), typeof(LineGraphItem), new FrameworkPropertyMetadata(MarkerTypes.Ellipse, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// マーカー種別を取得または設定します。
        /// </summary>
        public MarkerTypes MarkerType
        {
            get { return (MarkerTypes)GetValue(MarkerTypeProperty); }
            set { SetValue(MarkerTypeProperty, value); }
        }
        #endregion MarkerType プロパティ

        #region AnimationType プロパティ
        /// <summary>
        /// AnimationType 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty AnimationTypeProperty = DependencyProperty.Register("AnimationType", typeof(AnimationTypes), typeof(LineGraphItem), new FrameworkPropertyMetadata(AnimationTypes.FadeIn));

        /// <summary>
        /// グラフ表示時のアニメーションを取得または設定します。
        /// </summary>
        public AnimationTypes AnimationType
        {
            get { return (AnimationTypes)GetValue(AnimationTypeProperty); }
            set { SetValue(AnimationTypeProperty, value); }
        }

        /// <summary>
        /// アニメーションを開始します。
        /// </summary>
        private void BeginAnimation()
        {
            switch (this.AnimationType)
            {
                case AnimationTypes.None:
                    break;

                case AnimationTypes.FadeIn:
                    StoryboardFadeIn.Begin();
                    break;

                case AnimationTypes.SlideIn:
                    StoryboardSlideIn.Begin();
                    break;

                case AnimationTypes.Rotate:
                    StoryboardRotate.Begin();
                    break;

                default:
                    break;
            }
        }

        private Storyboard _storyboardFadeIn;
        /// <summary>
        /// フェードインアニメーションを取得します。
        /// </summary>
        private Storyboard StoryboardFadeIn
        {
            get
            {
                if (_storyboardFadeIn == null)
                {
                    var animation1 = new DoubleAnimation(10, 0, new Duration(TimeSpan.FromMilliseconds(500)));
                    var animation2 = new DoubleAnimation(10, 0, new Duration(TimeSpan.FromMilliseconds(500)));
                    var animation3 = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(500)));
                    Storyboard.SetTarget(animation1, this);
                    Storyboard.SetTargetProperty(animation1, new PropertyPath("RenderTransform.Children[3].X"));
                    Storyboard.SetTarget(animation2, this);
                    Storyboard.SetTargetProperty(animation2, new PropertyPath("RenderTransform.Children[3].Y"));
                    Storyboard.SetTarget(animation3, this);
                    Storyboard.SetTargetProperty(animation3, new PropertyPath("Opacity"));

                    _storyboardFadeIn = new Storyboard();
                    _storyboardFadeIn.Children.Add(animation1);
                    _storyboardFadeIn.Children.Add(animation2);
                    _storyboardFadeIn.Children.Add(animation3);
                }
                return _storyboardFadeIn;
            }
        }

        private Storyboard _storyboardSlideIn;
        /// <summary>
        /// スライドアニメーションを取得します。
        /// </summary>
        private Storyboard StoryboardSlideIn
        {
            get
            {
                if (_storyboardSlideIn == null)
                {
                    var animation1 = new DoubleAnimation(40, 0, new Duration(TimeSpan.FromMilliseconds(500)));
                    Storyboard.SetTarget(animation1, this);
                    Storyboard.SetTargetProperty(animation1, new PropertyPath("RenderTransform.Children[3].X"));

                    _storyboardSlideIn = new Storyboard();
                    _storyboardSlideIn.Children.Add(animation1);
                }
                return _storyboardSlideIn;
            }
        }

        private Storyboard _storyboardRotate;
        /// <summary>
        /// 回転アニメーションを取得します。
        /// </summary>
        private Storyboard StoryboardRotate
        {
            get
            {
                if (_storyboardRotate == null)
                {
                    var animation1 = new DoubleAnimation(0, this.ActualWidth / 2.0, new Duration(TimeSpan.FromMilliseconds(0)));
                    var animation2 = new DoubleAnimation(0, this.ActualHeight / 2.0, new Duration(TimeSpan.FromMilliseconds(0)));
                    var animation3 = new DoubleAnimation(5 * 360, 0, new Duration(TimeSpan.FromMilliseconds(500)));
                    var animation4 = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(500)));
                    var animation5 = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromMilliseconds(500)));

                    Storyboard.SetTarget(animation1, this);
                    Storyboard.SetTargetProperty(animation1, new PropertyPath("RenderTransform.Children[0].CenterX"));
                    Storyboard.SetTarget(animation2, this);
                    Storyboard.SetTargetProperty(animation2, new PropertyPath("RenderTransform.Children[0].CenterY"));
                    Storyboard.SetTarget(animation3, this);
                    Storyboard.SetTargetProperty(animation3, new PropertyPath("RenderTransform.Children[0].Angle"));
                    Storyboard.SetTarget(animation4, this);
                    Storyboard.SetTargetProperty(animation4, new PropertyPath("RenderTransform.Children[1].ScaleX"));
                    Storyboard.SetTarget(animation5, this);
                    Storyboard.SetTargetProperty(animation5, new PropertyPath("RenderTransform.Children[1].ScaleY"));

                    _storyboardRotate = new Storyboard();
                    _storyboardRotate.Children.Add(animation1);
                    _storyboardRotate.Children.Add(animation2);
                    _storyboardRotate.Children.Add(animation3);
                    _storyboardRotate.Children.Add(animation4);
                    _storyboardRotate.Children.Add(animation5);
                }
                return _storyboardRotate;
            }
        }
        #endregion AnimationType プロパティ

        #region Legend プロパティ
        /// <summary>
        /// Legend 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty LegendProperty = DependencyProperty.Register("Legend", typeof(string), typeof(LineGraphItem), new FrameworkPropertyMetadata(null, (s, e) => (s as LineGraphItem).OnLegendChanged(e.OldValue as string, e.NewValue as string)));

        /// <summary>
        /// データ名を取得または設定します。
        /// </summary>
        public string Legend
        {
            get { return (string)GetValue(LegendProperty); }
            set { SetValue(LegendProperty, value); }
        }

        /// <summary>
        /// Legend プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private void OnLegendChanged(string oldValue, string newValue)
        {
            RaiseLegendChanged();
        }
        #endregion Legend プロパティ

        #region HighlightPoint プロパティ
        /// <summary>
        /// HighlightPoint 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty HighlightPointProperty = DependencyProperty.Register("HighlightPoint", typeof(Point?), typeof(LineGraphItem), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, OnHighlightPointPropertyChanged));

        /// <summary>
        /// 強調する点の横軸座標を取得または設定します。
        /// </summary>
        public Point? HighlightPoint
        {
            get { return (Point?)GetValue(HighlightPointProperty); }
            set { SetValue(HighlightPointProperty, value); }
        }

        /// <summary>
        /// HighlightPoint プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnHighlightPointPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var element = sender as LineGraphItem;
            if (element != null)
            {
                element.RaiseHighlightPointChanged();
            }
        }
        #endregion HighlightPoint プロパティ

        #region IsMarkerEnabled プロパティ
        /// <summary>
        /// IsMarkerEnabled 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsMarkerEnabledProperty = DependencyProperty.Register("IsMarkerEnabled", typeof(bool), typeof(LineGraphItem), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// マーカーを表示するかどうかを取得または設定します。
        /// </summary>
        public bool IsMarkerEnabled
        {
            get { return (bool)GetValue(IsMarkerEnabledProperty); }
            set { SetValue(IsMarkerEnabledProperty, value); }
        }
        #endregion IsMarkerEnabled プロパティ

        #region IsSecondChanged イベント
        /// <summary>
        /// IsSecond プロパティが変更されたときに発生します。
        /// </summary>
        public event EventHandler<EventArgs> IsSecondChanged;

        /// <summary>
        /// IsSecondChanged イベントを発行します。
        /// </summary>
        private void RaiseIsSecondChanged()
        {
            var h = this.IsSecondChanged;
            if (h != null) h(this, EventArgs.Empty);
        }
        #endregion IsSecondChanged イベント

        #region LegendChanged イベント
        /// <summary>
        /// Legend プロパティ変更時に発生します。
        /// </summary>
        public event EventHandler<EventArgs> LegendChanged;

        /// <summary>
        /// LegendChanged イベントを発行します。
        /// </summary>
        private void RaiseLegendChanged()
        {
            var h = this.LegendChanged;
            if (h != null) h(this, EventArgs.Empty);
        }
        #endregion LegendChanged イベント

        #region HighlightPointChanged イベント
        /// <summary>
        /// 強調する点が変更されたときに発生します。
        /// </summary>
        public event EventHandler<EventArgs> HighlightPointChanged;

        /// <summary>
        /// HighlightPointChanged イベントを発行します。
        /// </summary>
        private void RaiseHighlightPointChanged()
        {
            var h = this.HighlightPointChanged;
            if (h != null) h(this, EventArgs.Empty);
        }
        #endregion HighlightPointChanged イベント

        #region DataEnableChanged イベント
        /// <summary>
        /// データ表示状態変更時に発生します。
        /// </summary>
        public event EventHandler<EventArgs> DataEnableChanged;

        private bool _isDataEnabled;
        /// <summary>
        /// データ表示状態を取得または設定します。
        /// </summary>
        public bool IsDataEnabled
        {
            get { return _isDataEnabled; }
            set
            {
                if (_isDataEnabled != value)
                {
                    _isDataEnabled = value;
                    var h = DataEnableChanged;
                    if (h != null) h(this, EventArgs.Empty);
                }
            }
        }
        #endregion DataEnableChanged イベント

        #region 描画処理関連のオーバーライド
        /// <summary>
        /// 描画処理をおこないます。
        /// </summary>
        /// <param name="dc">描画するコンテキスト</param>
        protected override void OnRender(DrawingContext dc)
        {
            //base.OnRender(dc);

            if (XAxisData == null)
            {
                IsDataEnabled = false;
                return;
            }
            if (YAxisData == null)
            {
                IsDataEnabled = false;
                return;
            }

            var xArray = XAxisData.OfType<double>().ToArray();
            var yArray = YAxisData.OfType<double>().ToArray();
            var length = xArray.Length < yArray.Length ? xArray.Length : yArray.Length;
            Point? pt0 = null;
            for (var i = 0; i < length; i++)
            {
                #region 線の描画
                var pen = new Pen(this.Stroke, this.Thickness);
                pen.Freeze();
                if (pt0 != null)
                {

                    if ((pt0.Value.Y >= this.YMax) && (yArray[i] >= this.YMax))
                    {
                        // 絶対に線の描画があり得ないパターン
                    }
                    else if ((pt0.Value.Y <= this.YMin) && (yArray[i] <= this.YMin))
                    {
                        // 絶対に線の描画があり得ないパターン
                    }
                    else
                    {
                        Point? ptLine0 = null;
                        Point? ptLine1 = null;

                        // 以前の点が右端より左側にあって
                        // 今回の点が左端より右側にある場合のみ
                        // 線を描画する可能性がある
                        if ((pt0.Value.X < this.XMax) && (xArray[i] > this.XMin))
                        {
                            // 以前の点が範囲内ならこれを左端の点とする
                            if ((this.XMin <= pt0.Value.X) && (pt0.Value.X <= this.XMax) && (this.YMin <= pt0.Value.Y) && (pt0.Value.Y <= this.YMax))
                            {
                                ptLine0 = GetControlPointFromGraphPoint(pt0.Value);
                            }
                            // 今回の点が範囲内ならこれを右端の点とする
                            if ((this.XMin <= xArray[i]) && (xArray[i] <= this.XMax) && (this.YMin <= yArray[i]) && (yArray[i] <= this.YMax))
                            {
                                ptLine1 = GetControlPointFromGraphPoint(xArray[i], yArray[i]);
                            }

                            // 左端または右端の点が確定していない場合はグラフ表示範囲の境界線との交点を調べる
                            if ((ptLine0 == null) || (ptLine1 == null))
                            {
                                // y = ax + b
                                // 傾き
                                double? a = xArray[i] != pt0.Value.X ? (yArray[i] - pt0.Value.Y) / (xArray[i] - pt0.Value.X) : (double?)null;
                                if (a != null)
                                {
                                    // 切片
                                    double b = pt0.Value.Y - a.Value * pt0.Value.X;

                                    // 左端縦軸との交点
                                    var yLeft = a.Value * this.XMin + b;
                                    // 右端縦軸との交点
                                    var yRight = a.Value * this.XMax + b;
                                    // 上端横軸との交点
                                    var xTop = (this.YMax - b) / a.Value;
                                    // 下端横軸との交点
                                    var xBottom = (this.YMin - b) / a.Value;

                                    #region 左端の点を確定する
                                    if (ptLine0 == null)
                                    {
                                        // 左端縦軸交点の確認
                                        if ((this.YMin <= yLeft) && (yLeft <= this.YMax))
                                        {
                                            ptLine0 = GetControlPointFromGraphPoint(this.XMin, yLeft);
                                        }
                                        else
                                        {
                                            // 下から上への線の場合
                                            if (pt0.Value.Y < yArray[i])
                                            {
                                                // 交わり得るのは下端横軸交点
                                                if ((this.XMin <= xBottom) && (xBottom <= this.XMax))
                                                {
                                                    ptLine0 = GetControlPointFromGraphPoint(xBottom, this.YMin);
                                                }
                                            }
                                            else
                                            {
                                                // 上から下への線の場合は
                                                // 交わり得るのは上端横軸交点
                                                if ((this.XMin <= xTop) && (xTop <= this.XMax))
                                                {
                                                    ptLine0 = GetControlPointFromGraphPoint(xTop, this.YMax);
                                                }
                                            }
                                        }
                                    }
                                    #endregion 左端の点を確定する

                                    #region 右端の点を確定する
                                    if (ptLine1 == null)
                                    {
                                        // 右端縦軸交点の確認
                                        if ((this.YMin <= yRight) && (yRight <= this.YMax))
                                        {
                                            ptLine1 = GetControlPointFromGraphPoint(this.XMax, yRight);
                                        }
                                        else
                                        {
                                            // 下から上への線の場合
                                            if (pt0.Value.Y < yArray[i])
                                            {
                                                // 交わり得るのは上端横軸交点
                                                if ((this.XMin <= xTop) && (xTop <= this.XMax))
                                                {
                                                    ptLine1 = GetControlPointFromGraphPoint(xTop, this.YMax);
                                                }
                                            }
                                            else
                                            {
                                                // 上から下への線の場合は
                                                // 交わり得るのは下端横軸交点
                                                if ((this.XMin <= xBottom) && (xBottom <= this.XMax))
                                                {
                                                    ptLine1 = GetControlPointFromGraphPoint(xBottom, this.YMin);
                                                }
                                            }
                                        }
                                    }
                                    #endregion 右端の点を確定する
                                }
                            }

                            // 線の両端点が確定したら線を描画する
                            if ((ptLine0 != null) && (ptLine1 != null))
                            {
                                dc.DrawLine(pen, ptLine0.Value, ptLine1.Value);
                            }
                        }
                    }
                }
                #endregion 線の描画

                #region データ点の描画
                if (this.IsMarkerEnabled)
                {
                    if (pt0 != null)
                    {
                        // 線の上にデータ点を描画するために
                        // ひとつ前のデータ点を描画する
                        DrawingDataPoint(dc, pt0.Value);
                    }
                    if (i == length - 1)
                    {
                        // ひとつ前のデータ点を描画していたので
                        // 最後のデータ点をここで描画する
                        DrawingDataPoint(dc, new Point(xArray[i], yArray[i]));
                    }
                }
                #endregion データ点の描画

                // 以前の点として保持
                pt0 = new Point(xArray[i], yArray[i]);
            }

            // 強調表示するデータ点
            if (this.HighlightPoint != null)
            {
                DrawingDataPoint(dc, this.HighlightPoint.Value, true);
            }

            // データ表示確認
            IsDataEnabled = length > 0;
        }

        #region データ点描画ヘルパ
        /// <summary>
        /// 現在の状態でデータ点を描画します。
        /// </summary>
        /// <param name="dc">描画するコンテキスト</param>
        /// <param name="pt">コントロール座標</param>
        /// <param name="isHighLighted">強調表示する場合に true を指定します。</param>
        private void DrawingDataPoint(DrawingContext dc, Point pt, bool isHighLighted = false)
        {
            if ((this.XMin <= pt.X) && (pt.X <= this.XMax) && (this.YMin <= pt.Y) && (pt.Y <= this.YMax))
            {
                var ptData = GetControlPointFromGraphPoint(pt);
                if (ptData != null)
                {
                    var width = isHighLighted ? 2.0 * this.MarkerSize.Width : this.MarkerSize.Width;
                    var height = isHighLighted ? 2.0 * this.MarkerSize.Height : this.MarkerSize.Height;
                    switch (this.MarkerType)
                    {
                        case MarkerTypes.Ellipse:
                            dc.DrawEllipse(this.Fill, this.MarkerPen, ptData.Value, width, height);
                            break;

                        case MarkerTypes.Rectangle:
                            dc.DrawRectangle(this.Fill, this.MarkerPen, new Rect(new Point(ptData.Value.X - width / 2.0, ptData.Value.Y - height / 2.0), new Size(width, height)));
                            break;
                    }
                }
            }
        }
        #endregion データ点描画ヘルパ

        #region 座標変換ヘルパ
        /// <summary>
        /// グラフ座標をコントロール座標に変換します。
        /// </summary>
        /// <param name="pt">グラフ座標</param>
        /// <returns>コントロール座標</returns>
        private Point? GetControlPointFromGraphPoint(Point pt)
        {
            return GetControlPointFromGraphPoint(pt.X, pt.Y);
        }

        /// <summary>
        /// グラフ座標をコントロール座標に変換します。
        /// </summary>
        /// <param name="x">グラフの横軸座標</param>
        /// <param name="y">グラフの縦軸座標</param>
        /// <returns>コントロール座標</returns>
        private Point? GetControlPointFromGraphPoint(double x, double y)
        {
            if (this.XMax <= this.XMin)
                return null;
            if (this.YMax <= this.YMin)
                return null;

            var xx = this.ActualWidth * (x - this.XMin) / (this.XMax - this.XMin);
            var yy = this.ActualHeight - this.ActualHeight * (y - this.YMin) / (this.YMax - this.YMin);
            return new Point(xx, yy);
        }
        #endregion 座標変換ヘルパ

        #endregion 描画処理関連のオーバーライド
    }
}
