namespace YKToolkit.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
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
                this._xArray = this.XAxisData.OfType<double>().ToArray();

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

            this._xArray = this.XAxisData.OfType<double>().ToArray();
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
                this._yArray = this.YAxisData.OfType<double>().ToArray();
                
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

            this._yArray = this.YAxisData.OfType<double>().ToArray();
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

        #region Color プロパティ
        /// <summary>
        /// Color 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(Color?), typeof(LineGraphItem), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, OnColorPropertyChanged));

        /// <summary>
        /// 折れ線グラフの色を取得または設定します。
        /// </summary>
        public Color? Color
        {
            get { return (Color?)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        /// <summary>
        /// Color 依存関係プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LineGraphItem).UpdatePensAndBrushes();
        }
        #endregion Color プロパティ

        #region Fill プロパティ
        /// <summary>
        /// Fill 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(Brush), typeof(LineGraphItem), new FrameworkPropertyMetadata(Brushes.Red, FrameworkPropertyMetadataOptions.AffectsRender, OnFillPropertyChanged));

        /// <summary>
        /// データ点塗潰しブラシを取得または設定します。
        /// </summary>
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        /// <summary>
        /// Fill 依存関係プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnFillPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LineGraphItem).UpdatePensAndBrushes();
        }
        #endregion Fill プロパティ

        #region Stroke プロパティ
        /// <summary>
        /// Stroke 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register("Stroke", typeof(Brush), typeof(LineGraphItem), new FrameworkPropertyMetadata(Brushes.Red, FrameworkPropertyMetadataOptions.AffectsRender, OnStrokePropertyChanged));

        /// <summary>
        /// 折れ線塗潰し色を取得または設定します。
        /// </summary>
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        /// <summary>
        /// Stroke 依存関係プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnStrokePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LineGraphItem).UpdatePensAndBrushes();
        }
        #endregion Stroke プロパティ

        #region Thickness プロパティ
        /// <summary>
        /// Thickness 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.Register("Thickness", typeof(double), typeof(LineGraphItem), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender, OnThicknessPropertyChanged));

        /// <summary>
        /// 折れ線の太さを取得または設定します。
        /// </summary>
        public double Thickness
        {
            get { return (double)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        /// <summary>
        /// Thickness 依存関係プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnThicknessPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LineGraphItem).UpdatePensAndBrushes();
        }
        #endregion Thickness プロパティ

        #region MarkerPen プロパティ
        /// <summary>
        /// MarkerPen 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty MarkerPenProperty = DependencyProperty.Register("MarkerPen", typeof(Pen), typeof(LineGraphItem), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, OnMarkerPenPropertyChanged));

        /// <summary>
        /// データ点境界線ペンを取得または設定します。
        /// </summary>
        public Pen MarkerPen
        {
            get { return (Pen)GetValue(MarkerPenProperty); }
            set { SetValue(MarkerPenProperty, value); }
        }

        /// <summary>
        /// MarkerPen 依存関係プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnMarkerPenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as LineGraphItem).UpdatePensAndBrushes();
        }
        #endregion MarkerPen プロパティ

        #region MarkerSize プロパティ
        /// <summary>
        /// MarkerSize 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty MarkerSizeProperty = DependencyProperty.Register("MarkerSize", typeof(Size), typeof(LineGraphItem), new FrameworkPropertyMetadata(new Size(3.0, 3.0), FrameworkPropertyMetadataOptions.AffectsRender, OnMarkerSizePropertyChanged));

        /// <summary>
        /// マーカーサイズを取得または設定します。
        /// </summary>
        public Size MarkerSize
        {
            get { return (Size)GetValue(MarkerSizeProperty); }
            set { SetValue(MarkerSizeProperty, value); }
        }

        /// <summary>
        /// MarkerSize プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnMarkerSizePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as LineGraphItem;
            if (control != null)
            {
                if (control._isMarkerSizeChangedFromUI)
                {
                    control._isMarkerSizeChangedFromUI = false;
                    control.MarkerWidth = control.MarkerSize.Width;
                    control._isMarkerSizeChangedFromUI = true;
                }
            }
        }

        /// <summary>
        /// MarkerSize プロパティが UI から変更されたかどうか
        /// </summary>
        private bool _isMarkerSizeChangedFromUI = true;
        #endregion MarkerSize プロパティ

        #region MarkerWidth プロパティ
        /// <summary>
        /// MarkerWidth 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty MarkerWidthProperty = DependencyProperty.Register("MarkerWidth", typeof(double), typeof(LineGraphItem), new FrameworkPropertyMetadata(3.0, OnMarkerWidthPropertyChanged));

        /// <summary>
        /// マーカーの幅を取得または設定します。
        /// </summary>
        public double MarkerWidth
        {
            get { return (double)GetValue(MarkerWidthProperty); }
            set { SetValue(MarkerWidthProperty, value); }
        }

        /// <summary>
        /// MarkerWidth プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnMarkerWidthPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as LineGraphItem;
            if (control != null)
            {
                if (control._isMarkerSizeChangedFromUI)
                {
                    control._isMarkerSizeChangedFromUI = false;
                    control.MarkerSize = new Size(control.MarkerWidth, control.MarkerHeight);
                    control._isMarkerSizeChangedFromUI = true;
                }
            }
        }
        #endregion MarkerWidth プロパティ

        #region MarkerHeight プロパティ
        /// <summary>
        /// MarkerHeight 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty MarkerHeightProperty = DependencyProperty.Register("MarkerHeight", typeof(double), typeof(LineGraphItem), new FrameworkPropertyMetadata(3.0, OnMarkerHeightPropertyChanged));

        /// <summary>
        /// マーカーの高さを取得または設定します。
        /// </summary>
        public double MarkerHeight
        {
            get { return (double)GetValue(MarkerHeightProperty); }
            set { SetValue(MarkerHeightProperty, value); }
        }

        /// <summary>
        /// MarkerHeight プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnMarkerHeightPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as LineGraphItem;
            if (control != null)
            {
                if (control._isMarkerSizeChangedFromUI)
                {
                    control._isMarkerSizeChangedFromUI = false;
                    control.MarkerSize = new Size(control.MarkerWidth, control.MarkerHeight);
                    control._isMarkerSizeChangedFromUI = true;
                }
            }
        }
        #endregion MarkerHeight プロパティ

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

        #region IsStrokeEnabled プロパティ
        /// <summary>
        /// IsStrokeEnabled 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsStrokeEnabledProperty = DependencyProperty.Register("IsStrokeEnabled", typeof(bool), typeof(LineGraphItem), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 線を表示するかどうかを取得または設定します。
        /// </summary>
        public bool IsStrokeEnabled
        {
            get { return (bool)GetValue(IsStrokeEnabledProperty); }
            set { SetValue(IsStrokeEnabledProperty, value); }
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

            if (this._xArray == null)
            {
                IsDataEnabled = false;
                return;
            }
            if (this._yArray == null)
            {
                IsDataEnabled = false;
                return;
            }

            var length = Math.Min(this._xArray.Length, this._yArray.Length);
            var pt0 = new Point();
            var ptLine0 = new Point();
            var ptLine1 = new Point();
            var isPtLine0 = false;
            var isPtLine1 = false;
            var isFirstPoint = true;
            var isLineFirst = true;
            var pathfigure = new PathFigure();
            for (var i = 0; i < length; i++)
            {
                if (IsStrokeEnabled)
                {
                    #region 線の描画
                    if (!isFirstPoint)
                    {

                        if ((pt0.Y > this.YMax) && (this._yArray[i] > this.YMax))
                        {
                            // 絶対に線の描画があり得ないパターン
                        }
                        else if ((pt0.Y < this.YMin) && (this._yArray[i] < this.YMin))
                        {
                            // 絶対に線の描画があり得ないパターン
                        }
                        else
                        {

                            // 以前の点が右端より左側にあって
                            // 今回の点が左端より右側にある場合のみ
                            // 線を描画する可能性がある
                            if ((pt0.X < this.XMax) && (this._xArray[i] > this.XMin))
                            {
                                // 以前の点が範囲内ならこれを左端の点とする
                                if ((this.XMin <= pt0.X) && (pt0.X <= this.XMax) && (this.YMin <= pt0.Y) && (pt0.Y <= this.YMax))
                                {
                                    isPtLine0 = GetControlPointFromGraphPoint(pt0, out ptLine0);
                                }
                                // 今回の点が範囲内ならこれを右端の点とする
                                if ((this.XMin <= this._xArray[i]) && (this._xArray[i] <= this.XMax) && (this.YMin <= this._yArray[i]) && (this._yArray[i] <= this.YMax))
                                {
                                    isPtLine1 = GetControlPointFromGraphPoint(this._xArray[i], this._yArray[i], out ptLine1);
                                }

                                // 左端または右端の点が確定していない場合はグラフ表示範囲の境界線との交点を調べる
                                if (!isPtLine0 || !isPtLine1)
                                {
                                    if (this._xArray[i] != pt0.X)
                                    {
                                        // y = ax + b
                                        // 傾き
                                        double a = (this._yArray[i] - pt0.Y) / (this._xArray[i] - pt0.X);
                                        // 切片
                                        double b = pt0.Y - a * pt0.X;

                                        // 左端縦軸との交点
                                        var yLeft = a * this.XMin + b;
                                        // 右端縦軸との交点
                                        var yRight = a * this.XMax + b;
                                        // 上端横軸との交点
                                        var xTop = (this.YMax - b) / a;
                                        // 下端横軸との交点
                                        var xBottom = (this.YMin - b) / a;

                                        #region 左端の点を確定する
                                        if (!isPtLine0)
                                        {
                                            // 左端縦軸交点の確認
                                            if ((this.YMin <= yLeft) && (yLeft <= this.YMax))
                                            {
                                                isPtLine0 = GetControlPointFromGraphPoint(this.XMin, yLeft, out ptLine0);
                                            }
                                            else if (yLeft < this.YMin)
                                            {
                                                // 交わり得るのは下端横軸交点
                                                if ((this.XMin <= xBottom) && (xBottom <= this.XMax))
                                                {
                                                    isPtLine0 = GetControlPointFromGraphPoint(xBottom, this.YMin, out ptLine0);
                                                }
                                            }
                                            else // if (yLeft > this.YMax)
                                            {
                                                // 交わり得るのは上端横軸交点
                                                if ((this.XMin <= xTop) && (xTop <= this.XMax))
                                                {
                                                    isPtLine0 = GetControlPointFromGraphPoint(xTop, this.YMax, out ptLine0);
                                                }
                                            }

                                            if (isPtLine0)
                                            {
                                                pathfigure.Segments.Add(new LineSegment(ptLine0, false));
                                            }
                                        }
                                        #endregion 左端の点を確定する

                                        #region 右端の点を確定する
                                        if (!isPtLine1)
                                        {
                                            // 右端縦軸交点の確認
                                            if ((this.YMin <= yRight) && (yRight <= this.YMax))
                                            {
                                                isPtLine1 = GetControlPointFromGraphPoint(this.XMax, yRight, out ptLine1);
                                            }
                                            else
                                            {
                                                // 下から上への線の場合
                                                if (pt0.Y < this._yArray[i])
                                                {
                                                    // 交わり得るのは上端横軸交点
                                                    if ((this.XMin <= xTop) && (xTop <= this.XMax))
                                                    {
                                                        isPtLine1 = GetControlPointFromGraphPoint(xTop, this.YMax, out ptLine1);
                                                    }
                                                }
                                                else
                                                {
                                                    // 上から下への線の場合は
                                                    // 交わり得るのは下端横軸交点
                                                    if ((this.XMin <= xBottom) && (xBottom <= this.XMax))
                                                    {
                                                        isPtLine1 = GetControlPointFromGraphPoint(xBottom, this.YMin, out ptLine1);
                                                    }
                                                }
                                            }
                                        }
                                        #endregion 右端の点を確定する
                                    }
                                }

                                // 線の開始点を確定する
                                if (isLineFirst && isPtLine0)
                                {
                                    pathfigure.StartPoint = ptLine0;
                                    isLineFirst = false;
                                }

                                // 右端の点が確定したら線を結ぶ
                                if (isPtLine0 && isPtLine1)
                                {
                                    pathfigure.Segments.Add(new LineSegment(ptLine1, true));
                                }
                            }
                        }
                        isPtLine0 = false;
                        isPtLine1 = false;
                    }
                    #endregion 線の描画
                }

                if (this.IsMarkerEnabled)
                {
                    #region データ点の描画
                    if (!isFirstPoint)
                    {
                        // 線の上にデータ点を描画するために
                        // ひとつ前のデータ点を描画する
                        DrawingDataPoint(dc, this._markerFill, this._markerPen, pt0);
                    }
                    if (i == length - 1)
                    {
                        // ひとつ前のデータ点を描画していたので
                        // 最後のデータ点をここで描画する
                        DrawingDataPoint(dc, this._markerFill, this._markerPen, new Point(this._xArray[i], this._yArray[i]));
                    }
                    #endregion データ点の描画
                }

                // 以前の点として保持
                pt0.X = this._xArray[i];
                pt0.Y = this._yArray[i];
                isFirstPoint = false;
            }

            if (pathfigure.CanFreeze)
                pathfigure.Freeze();
            var geometry = new PathGeometry();
            geometry.Figures.Add(pathfigure);
            geometry.Freeze();
            dc.DrawGeometry(null, this._pen, geometry);

            // 強調表示するデータ点
            if (this.HighlightPoint != null)
            {
                DrawingDataPoint(dc, this._markerFill, this._markerPen, this.HighlightPoint.Value, true);
            }

            // データ表示確認
            IsDataEnabled = length > 0;
        }

        #region データ点描画ヘルパ
        /// <summary>
        /// 現在の状態でデータ点を描画します。
        /// </summary>
        /// <param name="dc">描画するコンテキストを指定します。</param>
        /// <param name="fill">データ点塗潰しブラシを指定します。</param>
        /// <param name="pen">データ点境界線ペンを指定します。</param>
        /// <param name="pt">コントロール座標を指定します。</param>
        /// <param name="isHighLighted">強調表示する場合に true を指定します。</param>
        private void DrawingDataPoint(DrawingContext dc, Brush fill, Pen pen, Point pt, bool isHighLighted = false)
        {
            if ((this.XMin <= pt.X) && (pt.X <= this.XMax) && (this.YMin <= pt.Y) && (pt.Y <= this.YMax))
            {
                Point ptData;
                if (GetControlPointFromGraphPoint(pt, out ptData))
                {
                    var width = isHighLighted ? 2.0 * this.MarkerSize.Width : this.MarkerSize.Width;
                    var height = isHighLighted ? 2.0 * this.MarkerSize.Height : this.MarkerSize.Height;
                    switch (this.MarkerType)
                    {
                        case MarkerTypes.Ellipse:
                            dc.DrawEllipse(fill, pen, ptData, width, height);
                            break;

                        case MarkerTypes.Rectangle:
                            dc.DrawRectangle(fill, pen, new Rect(new Point(ptData.X - width / 2.0, ptData.Y - height / 2.0), new Size(width, height)));
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
        /// <param name="pt0">グラフ座標</param>
        /// <param name="pt1">コントロール座標</param>
        /// <returns>変換できなかった場合に false を返します。</returns>
        private bool GetControlPointFromGraphPoint(Point pt0, out Point pt1)
        {
            return GetControlPointFromGraphPoint(pt0.X, pt0.Y, out pt1);
        }

        /// <summary>
        /// グラフ座標をコントロール座標に変換します。
        /// </summary>
        /// <param name="x">グラフの横軸座標</param>
        /// <param name="y">グラフの縦軸座標</param>
        /// <param name="pt">コントロール座標</param>
        /// <returns>変換できなかった場合に false を返します。</returns>
        private bool GetControlPointFromGraphPoint(double x, double y, out Point pt)
        {
            if (this.XMax <= this.XMin)
            {
                pt = new Point();
                return false;
            }
            if (this.YMax <= this.YMin)
            {
                pt = new Point();
                return false;
            }

            var xx = this.ActualWidth * (x - this.XMin) / (this.XMax - this.XMin);
            var yy = this.ActualHeight - this.ActualHeight * (y - this.YMin) / (this.YMax - this.YMin);
            pt = new Point(xx, yy);
            return true;
        }
        #endregion 座標変換ヘルパ

        #endregion 描画処理関連のオーバーライド

        /// <summary>
        /// ペンとブラシを更新します。
        /// </summary>
        private void UpdatePensAndBrushes()
        {
            this._pen = new Pen(this.Color != null ? new SolidColorBrush(this.Color.Value) : this.Stroke, this.Thickness);
            if ((this._pen != null) && this._pen.CanFreeze) this._pen.Freeze();

            this._markerPen = this.Color != null ? new Pen(new SolidColorBrush(this.Color.Value), (this.MarkerPen != null) ? this.MarkerPen.Thickness : 1.0) : this.MarkerPen;
            if ((this._markerPen != null) && this._markerPen.CanFreeze) this._markerPen.Freeze();

            this._markerFill = this.Color != null ? new SolidColorBrush(this.Color.Value) : this.Fill;
            if ((this._markerFill != null) && this._markerFill.CanFreeze) this._markerFill.Freeze();
        }

        private Pen _pen;
        private Pen _markerPen;
        private Brush _markerFill;

        private double[] _xArray;
        private double[] _yArray;
    }
}
