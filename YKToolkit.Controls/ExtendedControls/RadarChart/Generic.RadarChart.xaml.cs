namespace YKToolkit.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using YKToolkit.Helpers;
    using Debug = System.Diagnostics.Debug;

    /// <summary>
    /// レーダーチャートを表示するためのコントロールです。
    /// </summary>
    //[TemplatePart(Name = PART_MainBorder, Type = typeof(Border))]
    public class RadarChart : Panel
    {
        #region TemplatePart
        //private const string PART_MainBorder = "PART_MainBorder";

        //private Border _mainBorder;
        ///// <summary>
        ///// メインコンテンツを含む Border コントロールを取得または設定します。
        ///// </summary>
        //private Border MainBorder
        //{
        //    get { return this._mainBorder; }
        //    set { this._mainBorder = value; }
        //}

        /// <summary>
        /// テンプレート適用時の処理
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //this.MainBorder = this.Template.FindName(PART_MainBorder, this) as Border;
        }
        #endregion TemplatePart

        #region コンストラクタ
        /// <summary>
        /// 静的なコンストラクタです。
        /// </summary>
        static RadarChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RadarChart), new FrameworkPropertyMetadata(typeof(RadarChart)));
        }
        #endregion コンストラクタ

        #region LabelOffset 添付プロパティ
        /// <summary>
        /// Point 型の LabelOffset 添付プロパティの定義を表します。
        /// </summary>
        public static readonly DependencyProperty LabelOffsetProperty = DependencyProperty.RegisterAttached("LabelOffset", typeof(Point), typeof(RadarChart), new FrameworkPropertyMetadata(default(Point), FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// LabelOffset 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <returns>取得した値を返します。</returns>
        public static Point GetLabelOffset(DependencyObject target)
        {
            return (Point)target.GetValue(LabelOffsetProperty);
        }

        /// <summary>
        /// LabelOffset 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <param name="value">設定する値を指定します。</param>
        public static void SetLabelOffset(DependencyObject target, Point value)
        {
            target.SetValue(LabelOffsetProperty, value);
        }
        #endregion LabelOffset 添付プロパティ

        #region LabelsSource 依存関係プロパティ
        /// <summary>
        /// IEnumerable&lt;object&gt; 型の LabelsSource 依存関係プロパティの定義を表します。
        /// </summary>
        public static readonly DependencyProperty LabelsSourceProperty = DependencyProperty.Register("LabelsSource", typeof(IEnumerable<object>), typeof(RadarChart), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender, (s, e) => (s as RadarChart).OnLabelsSourcePropertyChanged(e.OldValue as IEnumerable<object>, e.NewValue as IEnumerable<object>)));

        /// <summary>
        /// グラフのラベルコレクションを取得または設定します。
        /// </summary>
        public IEnumerable<object> LabelsSource
        {
            get { return (IEnumerable<object>)GetValue(LabelsSourceProperty); }
            set { SetValue(LabelsSourceProperty, value); }
        }

        /// <summary>
        /// LabelsSource プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private void OnLabelsSourcePropertyChanged(IEnumerable<object> oldValue, IEnumerable<object> newValue)
        {
            if (oldValue != null)
            {
                if (oldValue is INotifyCollectionChanged)
                    (oldValue as INotifyCollectionChanged).CollectionChanged -= LabelsSourceCollectionChanged;

                this.InternalChildren.Clear();
            }
            if (newValue != null)
            {
                if (newValue is INotifyCollectionChanged)
                    (newValue as INotifyCollectionChanged).CollectionChanged += LabelsSourceCollectionChanged;

                foreach (var obj in newValue)
                {
                    this.InternalChildren.Add(CreateInternalChild(obj));
                }
            }
        }

        private UIElement CreateInternalChild(object obj)
        {
            FrameworkElement element = null;
            if (this.ItemTemplate != null)
            {
                element = this.ItemTemplate.LoadContent() as FrameworkElement;
                element.DataContext = obj;
            }
            else
            {
                element = new ContentControl() { Content = obj };
            }

            return element;
        }

        /// <summary>
        /// LabelsSource コレクション子要素変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void LabelsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var obj in e.NewItems)
                    {
                        this.InternalChildren.Add(CreateInternalChild(obj));
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (var obj in e.OldItems)
                    {
                        if (this.ItemTemplate == null)
                        {
                            var item = this.InternalChildren.OfType<ContentControl>().FirstOrDefault(x => x.Content == obj);
                            if (item != null)
                            {
                                this.InternalChildren.Remove(item);
                            }
                        }
                        else
                        {
                            var item = this.InternalChildren.OfType<FrameworkElement>().FirstOrDefault(x => x.DataContext == obj);
                            if (item != null)
                            {
                                this.InternalChildren.Remove(item);
                            }
                        }
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    this.InternalChildren.Clear();
                    break;

                // あり得ない
                default:
                    break;
            }
        }
        #endregion LabelsSource 依存関係プロパティ

        #region ItemTemplate 依存関係プロパティ
        /// <summary>
        /// DataTemplate 型の ItemTemplate 依存関係プロパティの定義を表します。
        /// </summary>
        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register("ItemTemplate", typeof(DataTemplate), typeof(RadarChart), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// LablesSource プロパティで指定された各要素に対する DataTemplate を取得または設定します。
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
        #endregion ItemTemplate 依存関係プロパティ

        #region Values 依存関係プロパティ
        /// <summary>
        /// IEnumerable&lt;double&gt; 型の Labels 依存関係プロパティの定義を表します。
        /// </summary>
        public static readonly DependencyProperty ValuesProperty = DependencyProperty.Register("Values", typeof(IEnumerable<double>), typeof(RadarChart), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, (s, e) => OnValuesPropertyChanged(e.OldValue as IEnumerable<double>, e.NewValue as IEnumerable<double>)));

        /// <summary>
        /// グラフの値コレクションを取得または設定します。
        /// </summary>
        public IEnumerable<double> Values
        {
            get { return (IEnumerable<double>)GetValue(ValuesProperty); }
            set { SetValue(ValuesProperty, value); }
        }

        /// <summary>
        /// Values プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private static void OnValuesPropertyChanged(IEnumerable<double> oldValue, IEnumerable<double> newValue)
        {
            if (oldValue != null)
            {
                if (oldValue is INotifyCollectionChanged)
                    (oldValue as INotifyCollectionChanged).CollectionChanged -= ValuesCollectionChanged;
            }
            if (newValue != null)
            {
                if (newValue is INotifyCollectionChanged)
                    (newValue as INotifyCollectionChanged).CollectionChanged += ValuesCollectionChanged;
            }
        }

        /// <summary>
        /// Values コレクション子要素変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void ValuesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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
        }
        #endregion Values 依存関係プロパティ

        #region Maximum 依存関係プロパティ
        /// <summary>
        /// double 型の Maximum 依存関係プロパティの定義を表します。
        /// </summary>
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(double), typeof(RadarChart), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 各軸の値に対する最大値を取得または設定します。
        /// </summary>
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }
        #endregion Maximum 依存関係プロパティ

        #region GraphMargin 依存関係プロパティ
        /// <summary>
        /// double 型の GraphMargin 依存関係プロパティの定義を表します。
        /// </summary>
        public static readonly DependencyProperty GraphMarginProperty = DependencyProperty.Register("GraphMargin", typeof(double), typeof(RadarChart), new FrameworkPropertyMetadata(20.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// グラフ領域の余白を取得または設定します。
        /// </summary>
        public double GraphMargin
        {
            get { return (double)GetValue(GraphMarginProperty); }
            set { SetValue(GraphMarginProperty, value); }
        }
        #endregion GraphMargin 依存関係プロパティ

        #region BorderBrush 依存関係プロパティ
        /// <summary>
        /// Brush 型の BorderBrush 依存関係プロパティの定義を表します。
        /// </summary>
        public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(RadarChart), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// グラフ外枠線の色を取得または設定します。
        /// </summary>
        public Brush BorderBrush
        {
            get { return (Brush)GetValue(BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }
        #endregion BorderBrush 依存関係プロパティ

        #region BorderThickness 依存関係プロパティ
        /// <summary>
        /// double 型の BorderThickness 依存関係プロパティの定義を表します。
        /// </summary>
        public static readonly DependencyProperty BorderThicknessProperty = DependencyProperty.Register("BorderThickness", typeof(double), typeof(RadarChart), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// グラフ外枠線の太さを取得または設定します。
        /// </summary>
        public double BorderThickness
        {
            get { return (double)GetValue(BorderThicknessProperty); }
            set { SetValue(BorderThicknessProperty, value); }
        }
        #endregion BorderThickness 依存関係プロパティ

        #region TickBorderBrush 依存関係プロパティ
        /// <summary>
        /// Brush 型の TickBorderBrush 依存関係プロパティの定義を表します。
        /// </summary>
        public static readonly DependencyProperty TickBorderBrushProperty = DependencyProperty.Register("TickBorderBrush", typeof(Brush), typeof(RadarChart), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 軸線の色を取得または設定します。
        /// </summary>
        public Brush TickBorderBrush
        {
            get { return (Brush)GetValue(TickBorderBrushProperty); }
            set { SetValue(TickBorderBrushProperty, value); }
        }
        #endregion TickBorderBrush 依存関係プロパティ

        #region TickBorderThickness 依存関係プロパティ
        /// <summary>
        /// double 型の TickBorderThickness 依存関係プロパティの定義を表します。
        /// </summary>
        public static readonly DependencyProperty TickBorderThicknessProperty = DependencyProperty.Register("TickBorderThickness", typeof(double), typeof(RadarChart), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 軸の太さを取得または設定します。
        /// </summary>
        public double TickBorderThickness
        {
            get { return (double)GetValue(TickBorderThicknessProperty); }
            set { SetValue(TickBorderThicknessProperty, value); }
        }
        #endregion TickBorderThickness 依存関係プロパティ

        #region Ticks 依存関係プロパティ
        /// <summary>
        /// int 型の Ticks 依存関係プロパティの定義を表します。
        /// </summary>
        public static readonly DependencyProperty TicksProperty = DependencyProperty.Register("Ticks", typeof(int), typeof(RadarChart), new FrameworkPropertyMetadata(5, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 軸目盛の数を取得または設定します。
        /// </summary>
        public int Ticks
        {
            get { return (int)GetValue(TicksProperty); }
            set { SetValue(TicksProperty, value); }
        }
        #endregion Ticks 依存関係プロパティ

        #region TickWidth 依存関係プロパティ
        /// <summary>
        /// double 型の TickWidth 依存関係プロパティの定義を表します。
        /// </summary>
        public static readonly DependencyProperty TickWidthProperty = DependencyProperty.Register("TickWidth", typeof(double), typeof(RadarChart), new FrameworkPropertyMetadata(12.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 軸目盛の長さを取得または設定します。
        /// </summary>
        public double TickWidth
        {
            get { return (double)GetValue(TickWidthProperty); }
            set { SetValue(TickWidthProperty, value); }
        }
        #endregion TickWidth 依存関係プロパティ

        #region Fill 依存関係プロパティ
        /// <summary>
        /// Brush 型の Fill 依存関係プロパティの定義を表します。
        /// </summary>
        public static readonly DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(Brush), typeof(RadarChart), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// グラフ内部の塗潰し色を取得または設定します。
        /// </summary>
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }
        #endregion Fill 依存関係プロパティ

        #region Stroke 依存関係プロパティ
        /// <summary>
        /// Brush 型の Stroke 依存関係プロパティの定義を表します。
        /// </summary>
        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register("Stroke", typeof(Brush), typeof(RadarChart), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// グラフの境界線色を取得または設定します。
        /// </summary>
        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }
        #endregion Stroke 依存関係プロパティ

        #region StrokeThickness 依存関係プロパティ
        /// <summary>
        /// double 型の StrokeThickness 依存関係プロパティの定義を表します。
        /// </summary>
        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register("StrokeThickness", typeof(double), typeof(RadarChart), new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// グラフ境界線の太さを取得または設定します。
        /// </summary>
        public double StrokeThickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }
        #endregion StrokeThickness 依存関係プロパティ

        #region PointElement 依存関係プロパティ
        /// <summary>
        /// FrameworkElement 型の PointElement 依存関係プロパティの定義を表します。
        /// </summary>
        public static readonly DependencyProperty PointElementProperty = DependencyProperty.Register("PointElement", typeof(FrameworkElement), typeof(RadarChart), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// データ点を示すオブジェクトを取得または設定します。
        /// </summary>
        public FrameworkElement PointElement
        {
            get { return (FrameworkElement)GetValue(PointElementProperty); }
            set { SetValue(PointElementProperty, value); }
        }
        #endregion PointElement 依存関係プロパティ

#if DEBUG
        /// <summary>
        /// MeasureOverride メソッドが呼び出された回数をカウントします。
        /// </summary>
        private int _measureCounter;
#endif

        /// <summary>
        /// サイズ計測のオーバーライド
        /// </summary>
        /// <param name="constraint">親要素からのサイズへの制約</param>
        /// <returns>サイズ計測結果を返します。</returns>
        protected override Size MeasureOverride(Size constraint)
        {
#if DEBUG
            this._measureCounter++;
            Debug.WriteLine("[{0}] MeasureOverride[{1}]", this._measureCounter, constraint);
#endif
            // 正方形の領域を確定する
            var min = Math.Min(constraint.Width, constraint.Height);
            this._mainboardSize = new Size(min, min);

            // 描画領域のサイズを決める
            var drawingSize = new Size(double.IsInfinity(constraint.Width) ? min : constraint.Width, double.IsInfinity(constraint.Height) ? min : constraint.Height);

            var measureSize = new Size(double.PositiveInfinity, double.PositiveInfinity);
            foreach (UIElement child in this.InternalChildren)
            {
                if (child != null)
                {
                    child.Measure(measureSize);
                }
            }

            // 中心座標を算出する
            this._mainboardCenter = new Point(drawingSize.Width / 2.0, drawingSize.Height / 2.0);

            return base.MeasureOverride(drawingSize);
        }

        /// <summary>
        /// 子要素配置のオーバーライド
        /// </summary>
        /// <param name="arrangeBounds">使用可能領域</param>
        /// <returns>使用した領域のサイズを返します。</returns>
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
#if DEBUG
            Debug.WriteLine("[{0}] ArrangeOverride[{1}]", this._measureCounter, arrangeBounds);
#endif
            var angle = 360.0 / this.InternalChildren.Count;
            foreach (var item in this.InternalChildren.OfType<UIElement>().Select((x, i) => new { Element = x, Angle = i * angle }))
            {
                if (item.Element != null)
                {
                    var origin = new Point(this._mainboardCenter.X, this._mainboardCenter.Y - this._mainboardSize.Height / 2.0);
                    var pt = origin.Rotate(item.Angle, this._mainboardCenter);
                    pt.Offset(-item.Element.DesiredSize.Width / 2.0, -(pt.Y - origin.Y) / this._mainboardSize.Height * item.Element.DesiredSize.Height);
                    var offset = GetLabelOffset(item.Element);
                    pt.Offset(offset.X, offset.Y);
                    item.Element.Arrange(new Rect(pt, item.Element.DesiredSize));
                }
            }

            return arrangeBounds;
        }

        /// <summary>
        /// デザイン実行時モードかどうかを取得します。
        /// </summary>
        private bool IsDesignMode { get { return DesignerProperties.GetIsInDesignMode(this); } }

        /// <summary>
        /// 描画処理のオーバーライド
        /// </summary>
        /// <param name="drawingContext">描画先のコンテキスト</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
#if DEBUG
            Debug.WriteLine("[{0}] OnRender[{1}]", this._measureCounter, this.RenderSize);
#endif

            if (this.IsDesignMode)
            {
                // 背景色
                drawingContext.DrawRectangle(new SolidColorBrush(Colors.AntiqueWhite), null, new Rect(this.RenderSize));
            }

            var borderBrush = this.BorderBrush ?? new SolidColorBrush(this.IsDesignMode ? Colors.Black : (Color)this.FindResource("GraphBorderColor"));
            var tickBorderBrush = this.TickBorderBrush ?? borderBrush;
            var stroke = this.Stroke ?? borderBrush;

            // グラフ外枠円
            var radius = this._mainboardSize.Width / 2.0 - this.GraphMargin;
            drawingContext.DrawEllipse(this.Background, new Pen(borderBrush, this.BorderThickness), this._mainboardCenter, radius, radius);

            // 角度
            var angle = 360.0 / this.InternalChildren.Count;

            #region 基準軸の描画
            for (var i = 0; i < this.InternalChildren.Count; i++)
            {
                var pt = this._mainboardCenter;
                pt.Offset(0.0, -radius);
                drawingContext.DrawLine(new Pen(tickBorderBrush, this.TickBorderThickness), this._mainboardCenter, pt.Rotate(i * angle, this._mainboardCenter));

                // 目盛
                if (this.Ticks > 0)
                {
                    var div = radius / this.Ticks;
                    for (var j = 1; j < this.Ticks; j++)
                    {
                        var pt1 = this._mainboardCenter;
                        pt1.Offset(0.0, -j * div);
                        var pt2 = pt1;
                        pt1.Offset(-this.TickWidth / 2.0, 0.0);
                        pt2.Offset(this.TickWidth / 2.0, 0.0);
                        drawingContext.DrawLine(new Pen(tickBorderBrush, this.TickBorderThickness), pt1.Rotate(i * angle, this._mainboardCenter), pt2.Rotate(i * angle, this._mainboardCenter));
                    }
                }
            }
            #endregion 基準軸の描画

            #region データ領域の描画
            if (this.Values != null)
            {
                var values = this.InternalChildren.Count - this.Values.Count() >= 0 ? this.Values.Concat(Enumerable.Range(0, this.InternalChildren.Count - this.Values.Count()).Select(x => 0.0)) : this.Values.Take(this.InternalChildren.Count);
                var points = values.Select((x, i) =>
                {
                    var pt = this._mainboardCenter;
                    pt.Offset(0.0, -radius * x / this.Maximum);
                    return pt.Rotate(i * angle, this._mainboardCenter);
                });

                var figure = new PathFigure() { IsClosed = true };
                foreach (var item in points.Select((x, i) => new { Point = x, IsFirst = i == 0 }))
                {
                    // 線の描画の準備
                    if (item.IsFirst)
                    {
                        figure.StartPoint = item.Point;
                    }
                    else
                    {
                        figure.Segments.Add(new LineSegment(item.Point, true));
                    }
                }

                if (figure.IsFrozen)
                {
                    figure.Freeze();
                }
                var geometry = new PathGeometry();
                geometry.Figures.Add(figure);
                drawingContext.DrawGeometry(this.Fill, new Pen(stroke, this.StrokeThickness), geometry);

                if (this.PointElement != null)
                {
                    this.PointElement.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    var visualBrush = new VisualBrush(this.PointElement);
                    foreach (var pt in points)
                    {
                        var point = pt;
                        point.Offset(-this.PointElement.DesiredSize.Width / 2.0, -this.PointElement.DesiredSize.Height / 2.0);
                        // 点の描画
                        drawingContext.DrawRectangle(visualBrush, null, new Rect(point, this.PointElement.DesiredSize));
                    }
                }
            }
            #endregion データ領域の描画
        }

        /// <summary>
        /// グラフを表示する領域のサイズ
        /// </summary>
        private Size _mainboardSize;

        /// <summary>
        /// グラフを表示する領域の中心座標
        /// </summary>
        private Point _mainboardCenter;
    }
}
