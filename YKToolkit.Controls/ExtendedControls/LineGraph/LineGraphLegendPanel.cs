namespace YKToolkit.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    internal class LineGraphLegendPanel : Control
    {
        #region ItemsSource 依存関係プロパティ
        /// <summary>
        /// ItemsSource 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(LineGraphLegendPanel), new FrameworkPropertyMetadata(null, (s, e) => (s as LineGraphLegendPanel).OnItemsSourceChanged(e.OldValue as IEnumerable, e.NewValue as IEnumerable)));

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
                foreach (LineGraphItem item in oldValue)
                {
                    item.DataEnableChanged -= item_LegendChanged;
                    item.LegendChanged -= item_LegendChanged;
                    item.HighlightPointChanged -= item_HighlightPointChanged;
                }
            }
            if (newValue != null)
            {
                if (newValue is INotifyCollectionChanged)
                    (newValue as INotifyCollectionChanged).CollectionChanged += ItemsSourceCollectionChanged;
                foreach (LineGraphItem item in newValue)
                {
                    item.DataEnableChanged += item_LegendChanged;
                    item.LegendChanged += item_LegendChanged;
                    item.HighlightPointChanged += item_HighlightPointChanged;
                }
            }

            this.InvalidateMeasure();
            this.InvalidateVisual();
        }

        /// <summary>
        /// 子要素の Legend プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        void item_LegendChanged(object sender, EventArgs e)
        {
            this.InvalidateMeasure();
            this.InvalidateVisual();
        }

        /// <summary>
        /// 子要素の HighlightPoint プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        void item_HighlightPointChanged(object sender, EventArgs e)
        {
            this.InvalidateMeasure();
            this.InvalidateVisual();
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
                    {
                        item.DataEnableChanged += item_LegendChanged;
                        item.LegendChanged += item_LegendChanged;
                        item.HighlightPointChanged += item_HighlightPointChanged;
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (LineGraphItem item in e.OldItems)
                    {
                        item.DataEnableChanged -= item_LegendChanged;
                        item.LegendChanged -= item_LegendChanged;
                        item.HighlightPointChanged -= item_HighlightPointChanged;
                    }
                    this.InvalidateMeasure();
                    this.InvalidateVisual();
                    break;

                case NotifyCollectionChangedAction.Replace:
                    break;

                case NotifyCollectionChangedAction.Reset:
                    foreach (LineGraphItem item in this.ItemsSource)
                    {
                        item.DataEnableChanged -= item_LegendChanged;
                        item.LegendChanged -= item_LegendChanged;
                        item.HighlightPointChanged -= item_HighlightPointChanged;
                    }
                    this.InvalidateMeasure();
                    this.InvalidateVisual();
                    break;

                // あり得ない
                default:
                    break;
            }
        }
        #endregion ItemsSource 依存関係プロパティ

        #region Left 依存関係プロパティ
        /// <summary>
        /// Left 依存関係プロパティ
        /// </summary>
        public static readonly DependencyProperty LeftProperty = DependencyProperty.Register("Left", typeof(double), typeof(LineGraphLegendPanel), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnLeftPropertyChanged));

        /// <summary>
        /// 左端からの表示位置を取得または設定します。
        /// </summary>
        public double Left
        {
            get { return (double)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        /// <summary>
        /// Left プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnLeftPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as LineGraphLegendPanel;
            if (control == null)
                return;

            Canvas.SetLeft(control, control.Left);
        }
        #endregion Left 依存関係プロパティ

        #region Top 依存関係プロパティ
        /// <summary>
        /// Top 依存関係プロパティ
        /// </summary>
        public static readonly DependencyProperty TopProperty = DependencyProperty.Register("Top", typeof(double), typeof(LineGraphLegendPanel), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTopPropertyChanged));

        /// <summary>
        /// 上端からの表示位置を取得または設定します。
        /// </summary>
        public double Top
        {
            get { return (double)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

        /// <summary>
        /// Top プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnTopPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as LineGraphLegendPanel;
            if (control == null)
                return;

            Canvas.SetTop(control, control.Top);
        }
        #endregion Top 依存関係プロパティ

        #region 描画関連オーバーライド
        /// <summary>
        /// サイズ計測をおこないます。
        /// </summary>
        /// <param name="constraint">サイズに対する制約</param>
        /// <returns>計測結果</returns>
        protected override Size MeasureOverride(Size constraint)
        {
            //return base.MeasureOverride(constraint);

            var typeface = new Typeface(this.FontFamily.FamilyNames.Select(x => x.Value).First());
            this._textDic.Clear();
            foreach (var item in this.LimeGraphItems)
            {
                var str = item.Legend;
                if (item.HighlightPoint != null)
                {
                    var xData = item.HighlightPoint.Value.X.ToString(!string.IsNullOrWhiteSpace(item.XStringFormat) ? item.XStringFormat : "#0");
                    var yData = item.HighlightPoint.Value.Y.ToString(!string.IsNullOrWhiteSpace(item.YStringFormat) ? item.YStringFormat : "#0");
                    str += " (" + xData + ", " + yData + ")";
                }
                var text = new FormattedText(str, CultureInfo.CurrentUICulture, this.FlowDirection, typeface, this.FontSize, this.Foreground);
                this._textDic.Add(text, item);
            }
            double width = 0.0;
            double height = 0.0;
            if (this._textDic.Count > 0)
            {
                width = this.BorderThickness.Left + _margin + _lineLength + _lineMargin + this._textDic.Max(x => x.Key.Width) + _margin + this.BorderThickness.Left;
                height = this.BorderThickness.Left + _margin + (this._textDic.Count - 1) * _dataMargin + this._textDic.Sum(x => x.Key.Height) + _margin + this.BorderThickness.Left;
            }

            return new Size(width, height);
        }

        /// <summary>
        /// 描画処理をおこないます。
        /// </summary>
        /// <param name="dc">描画するコンテキスト</param>
        protected override void OnRender(DrawingContext dc)
        {
            //base.OnRender(drawingContext);
            dc.DrawRoundedRectangle(this.Background, new Pen(this.BorderBrush, this.BorderThickness.Left), new Rect(0, 0, this.ActualWidth, this.ActualHeight), 4.0, 4.0);

            var ptLine0 = new Point(this.BorderThickness.Left + _margin, this.BorderThickness.Left + _margin);
            var ptLine1 = new Point(this.BorderThickness.Left + _margin + _lineLength, this.BorderThickness.Left + _margin);
            var ptText = new Point(this.BorderThickness.Left + _margin + _lineLength + _lineMargin, this.BorderThickness.Left + _margin);
            foreach (var pair in this._textDic)
            {
                var text = pair.Key;
                var item = pair.Value;

                var stroke = item.Color != null ? new SolidColorBrush(item.Color.Value) : item.Stroke;
                if (!stroke.IsFrozen)
                    stroke.Freeze();
                var fill = item.Color != null ? stroke : item.Fill;
                Pen pen = null;
                if (item.MarkerPen != null)
                {
                    pen = item.Color != null ? new Pen(stroke, item.MarkerPen.Thickness) : item.MarkerPen;
                }

                // 凡例データ線描画
                ptLine0.Offset(0, text.Height / 2.0);
                ptLine1.Offset(0, text.Height / 2.0);
                dc.DrawLine(new Pen(stroke, item.Thickness), ptLine0, ptLine1);
                switch (item.MarkerType)
                {
                    case LineGraphItem.MarkerTypes.Ellipse:
                        dc.DrawEllipse(fill, pen, new Point(ptLine0.X + _lineLength / 2.0, ptLine0.Y), item.MarkerSize.Width, item.MarkerSize.Height);
                        break;

                    case LineGraphItem.MarkerTypes.Rectangle:
                        dc.DrawRectangle(fill, pen, new Rect(new Point(ptLine0.X + _lineLength / 2.0 - item.MarkerSize.Width / 2.0, ptLine0.Y - item.MarkerSize.Height / 2.0), item.MarkerSize));
                        break;
                }
                ptLine0.Offset(0, text.Height / 2.0 + _dataMargin);
                ptLine1.Offset(0, text.Height / 2.0 + _dataMargin);

                // 凡例テキスト描画
                dc.DrawText(text, ptText);
                ptText.Offset(0, text.Height + _dataMargin);
            }
        }
        #endregion 描画関連オーバーライド

        #region ドラッグ操作
        /// <summary>
        /// 移動開始点
        /// </summary>
        private Point? _moveStartPoint;

        /// <summary>
        /// マウス左ボタン押下イベントハンドラ
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            System.Console.WriteLine("LineGraphLegendPanel の PreviewMouseLeftButtonDown");
            if (this.CaptureMouse())
            {
                _moveStartPoint = e.GetPosition(this);
                e.Handled = true;
            }
            else
            {
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
                this.ReleaseMouseCapture();
                _moveStartPoint = null;
                e.Handled = true;
            }
            else
            {
                base.OnPreviewMouseLeftButtonUp(e);
            }
        }

        /// <summary>
        /// マウス移動イベントハンドラ
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this.IsMouseCaptured && _moveStartPoint != null)
            {
                var pt = e.GetPosition(this);
                var dx = pt.X - _moveStartPoint.Value.X;
                var dy = pt.Y - _moveStartPoint.Value.Y;

                var left = Canvas.GetLeft(this);
                var top = Canvas.GetTop(this);
                this.Left = left + dx;
                this.Top = top + dy;

                e.Handled = true;
            }
            else
            {
                base.OnMouseMove(e);
            }
        }
        #endregion ドラッグ操作

        #region private プロパティ
        /// <summary>
        /// 凡例表示が有効なグラフデータ配列を取得します。
        /// </summary>
        private LineGraphItem[] LimeGraphItems
        {
            get { return this.ItemsSource != null ? this.ItemsSource.OfType<LineGraphItem>().Where(x => x.IsDataEnabled && x.Legend != null).ToArray() : new LineGraphItem[] { }; }
        }
        #endregion private プロパティ

        #region private const
        /// <summary>
        /// 外枠からの余白
        /// </summary>
        private const double _margin = 4.0;

        /// <summary>
        /// データ名同士の余白
        /// </summary>
        private const double _dataMargin = 2.0;

        /// <summary>
        /// データ線とデータ名の余白
        /// </summary>
        private const double _lineMargin = 6.0;

        /// <summary>
        /// 凡例中のデータ線長さ
        /// </summary>
        private const double _lineLength = 50.0;
        #endregion private const

        #region private フィールド
        /// <summary>
        /// FormattedText に変換したデータ名リスト
        /// </summary>
        private Dictionary<FormattedText, LineGraphItem> _textDic = new Dictionary<FormattedText, LineGraphItem>();
        #endregion private フィールド
    }
}
