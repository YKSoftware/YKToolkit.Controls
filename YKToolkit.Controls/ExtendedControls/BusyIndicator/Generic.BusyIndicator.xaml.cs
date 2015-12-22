namespace YKToolkit.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;

    /// <summary>
    /// ビジーインジケーターを表します。
    /// </summary>
    [TemplatePart(Name = PART_Ellipse1, Type = typeof(Shape))]
    [TemplatePart(Name = PART_Ellipse2, Type = typeof(Shape))]
    [TemplatePart(Name = PART_Ellipse3, Type = typeof(Shape))]
    [TemplatePart(Name = PART_Ellipse4, Type = typeof(Shape))]
    [TemplatePart(Name = PART_Ellipse5, Type = typeof(Shape))]
    public class BusyIndicator : Control
    {
        #region TemplatePart
        private const string PART_Ellipse1 = "PART_Ellipse1";
        private const string PART_Ellipse2 = "PART_Ellipse2";
        private const string PART_Ellipse3 = "PART_Ellipse3";
        private const string PART_Ellipse4 = "PART_Ellipse4";
        private const string PART_Ellipse5 = "PART_Ellipse5";

        private Shape _ellipse1;
        private Shape Ellipse1
        {
            get { return _ellipse1; }
            set { _ellipse1 = value; }
        }

        private Shape _ellipse2;
        private Shape Ellipse2
        {
            get { return _ellipse2; }
            set { _ellipse2 = value; }
        }

        private Shape _ellipse3;
        private Shape Ellipse3
        {
            get { return _ellipse3; }
            set { _ellipse3 = value; }
        }

        private Shape _ellipse4;
        private Shape Ellipse4
        {
            get { return _ellipse4; }
            set { _ellipse4 = value; }
        }

        private Shape _ellipse5;
        private Shape Ellipse5
        {
            get { return _ellipse5; }
            set { _ellipse5 = value; }
        }

        /// <summary>
        /// テンプレート適用時の処理
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.Ellipse1 = this.Template.FindName(PART_Ellipse1, this) as Shape;
            this.Ellipse2 = this.Template.FindName(PART_Ellipse2, this) as Shape;
            this.Ellipse3 = this.Template.FindName(PART_Ellipse3, this) as Shape;
            this.Ellipse4 = this.Template.FindName(PART_Ellipse4, this) as Shape;
            this.Ellipse5 = this.Template.FindName(PART_Ellipse5, this) as Shape;

            Storyboard.SetTarget(_animation0_1, this.Ellipse1);
            Storyboard.SetTarget(_animation0_2, this.Ellipse2);
            Storyboard.SetTarget(_animation0_3, this.Ellipse3);
            Storyboard.SetTarget(_animation0_4, this.Ellipse4);
            Storyboard.SetTarget(_animation0_5, this.Ellipse5);

            Storyboard.SetTarget(_animation1, this.Ellipse1);
            Storyboard.SetTarget(_animation2, this.Ellipse2);
            Storyboard.SetTarget(_animation3, this.Ellipse3);
            Storyboard.SetTarget(_animation4, this.Ellipse4);
            Storyboard.SetTarget(_animation5, this.Ellipse5);
        }
        #endregion TemplatePart

        #region コンストラクタ
        /// <summary>
        /// 静的なコンストラクタです。
        /// </summary>
        static BusyIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyIndicator), new FrameworkPropertyMetadata(typeof(BusyIndicator)));
        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public BusyIndicator()
        {
            #region private フィールドの初期化
            _animation0_1 = new DoubleAnimationUsingKeyFrames();
            _animation0_2 = new DoubleAnimationUsingKeyFrames();
            _animation0_3 = new DoubleAnimationUsingKeyFrames();
            _animation0_4 = new DoubleAnimationUsingKeyFrames();
            _animation0_5 = new DoubleAnimationUsingKeyFrames();

            Storyboard.SetTargetProperty(_animation0_1, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTargetProperty(_animation0_2, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTargetProperty(_animation0_3, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTargetProperty(_animation0_4, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTargetProperty(_animation0_5, new PropertyPath("(Canvas.Left)"));

            _animation1 = new DoubleAnimationUsingKeyFrames();
            _animation2 = new DoubleAnimationUsingKeyFrames();
            _animation3 = new DoubleAnimationUsingKeyFrames();
            _animation4 = new DoubleAnimationUsingKeyFrames();
            _animation5 = new DoubleAnimationUsingKeyFrames();

            _animation1.BeginTime = TimeSpan.FromMilliseconds(200);
            _animation2.BeginTime = TimeSpan.FromMilliseconds(400);
            _animation3.BeginTime = TimeSpan.FromMilliseconds(600);
            _animation4.BeginTime = TimeSpan.FromMilliseconds(800);
            _animation5.BeginTime = TimeSpan.FromMilliseconds(1000);

            Storyboard.SetTargetProperty(_animation1, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTargetProperty(_animation2, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTargetProperty(_animation3, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTargetProperty(_animation4, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTargetProperty(_animation5, new PropertyPath("(Canvas.Left)"));

            _storyboard = new Storyboard();
            _storyboard.Children.Add(_animation0_1);
            _storyboard.Children.Add(_animation0_2);
            _storyboard.Children.Add(_animation0_3);
            _storyboard.Children.Add(_animation0_4);
            _storyboard.Children.Add(_animation0_5);
            _storyboard.Children.Add(_animation1);
            _storyboard.Children.Add(_animation2);
            _storyboard.Children.Add(_animation3);
            _storyboard.Children.Add(_animation4);
            _storyboard.Children.Add(_animation5);
            _storyboard.RepeatBehavior = RepeatBehavior.Forever;
            #endregion private フィールドの初期化

            this.Loaded += OnLoaded;
            this.SizeChanged += OnSizeChanged;
            this.IsVisibleChanged += OnIsVisibleChanged;
        }
        #endregion コンストラクタ

        #region IndicatorSize 依存関係プロパティ
        /// <summary>
        /// IndicatorSize 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IndicatorSizeProperty = DependencyProperty.Register("IndicatorSize", typeof(double), typeof(BusyIndicator), new PropertyMetadata(4.0));

        /// <summary>
        /// インジケータのサイズを取得または設定します。
        /// </summary>
        public double IndicatorSize
        {
            get { return (double)GetValue(IndicatorSizeProperty); }
            set { SetValue(IndicatorSizeProperty, value); }
        }
        #endregion IndicatorSize 依存関係プロパティ

        #region IndicatorBrush 依存関係プロパティ
        /// <summary>
        /// IndicatorBrush 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IndicatorBrushProperty = DependencyProperty.Register("IndicatorBrush", typeof(Brush), typeof(BusyIndicator), new PropertyMetadata(Brushes.Red));

        /// <summary>
        /// インジケータのブラシを取得または設定します。
        /// </summary>
        public Brush IndicatorBrush
        {
            get { return (Brush)GetValue(IndicatorBrushProperty); }
            set { SetValue(IndicatorBrushProperty, value); }
        }
        #endregion IndicatorBrush 依存関係プロパティ

        #region イベントハンドラ
        /// <summary>
        /// Loaded イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdateAnimation();
        }

        /// <summary>
        /// SizeChanged イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateAnimation();
        }

        /// <summary>
        /// IsVisibleChanged イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateAnimation();
        }
        #endregion イベントハンドラ

        #region ヘルパ
        /// <summary>
        /// アニメーションを更新します。
        /// </summary>
        private void UpdateAnimation()
        {
            _storyboard.Stop();

            if (this.Ellipse1 == null)
                return;
            if (double.IsNaN(this.ActualWidth))
                return;

            _initialKeyFrame = new DiscreteDoubleKeyFrame(-this.Ellipse1.ActualWidth, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0)));
            var splineDoubleKeyFrame = new SplineDoubleKeyFrame(this.ActualWidth, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(3000)), new KeySpline(0.3, 0.85, 0.7, 0.15));

            _animation0_1.KeyFrames.Clear();
            _animation0_2.KeyFrames.Clear();
            _animation0_3.KeyFrames.Clear();
            _animation0_4.KeyFrames.Clear();
            _animation0_5.KeyFrames.Clear();

            _animation0_1.KeyFrames.Add(_initialKeyFrame);
            _animation0_2.KeyFrames.Add(_initialKeyFrame);
            _animation0_3.KeyFrames.Add(_initialKeyFrame);
            _animation0_4.KeyFrames.Add(_initialKeyFrame);
            _animation0_5.KeyFrames.Add(_initialKeyFrame);

            _animation1.KeyFrames.Clear();
            _animation2.KeyFrames.Clear();
            _animation3.KeyFrames.Clear();
            _animation4.KeyFrames.Clear();
            _animation5.KeyFrames.Clear();

            _animation1.KeyFrames.Add(splineDoubleKeyFrame);
            _animation2.KeyFrames.Add(splineDoubleKeyFrame);
            _animation3.KeyFrames.Add(splineDoubleKeyFrame);
            _animation4.KeyFrames.Add(splineDoubleKeyFrame);
            _animation5.KeyFrames.Add(splineDoubleKeyFrame);

            _storyboard.Begin();
        }
        #endregion ヘルパ

        #region private フィールド
        private Storyboard _storyboard;
        private DiscreteDoubleKeyFrame _initialKeyFrame;
        private DoubleAnimationUsingKeyFrames _animation0_1;
        private DoubleAnimationUsingKeyFrames _animation0_2;
        private DoubleAnimationUsingKeyFrames _animation0_3;
        private DoubleAnimationUsingKeyFrames _animation0_4;
        private DoubleAnimationUsingKeyFrames _animation0_5;
        private DoubleAnimationUsingKeyFrames _animation1;
        private DoubleAnimationUsingKeyFrames _animation2;
        private DoubleAnimationUsingKeyFrames _animation3;
        private DoubleAnimationUsingKeyFrames _animation4;
        private DoubleAnimationUsingKeyFrames _animation5;
        #endregion private フィールド
    }
}
