namespace YKToolkit.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// カラーマップコントロールを表します。
    /// このコントロールは WriteableBitmapEx を使用しています。(https://github.com/reneschulte/WriteableBitmapEx/blob/master/LICENSE)
    /// </summary>
    public class ColorMap : ContentControl
    {
        #region XData プロパティ

        /// <summary>
        /// XData 依存関係プロパティを IEnumerable&lt;double&gt; 型として定義します。
        /// </summary>
        public static readonly DependencyProperty XDataProperty = DependencyProperty.Register("XData", typeof(IEnumerable<double>), typeof(ColorMap), new UIPropertyMetadata(null, OnCollectionPropertyChanged));

        /// <summary>
        /// 横軸データを取得または設定します。
        /// </summary>
        public IEnumerable<double> XData
        {
            get { return (IEnumerable<double>)GetValue(XDataProperty); }
            set { SetValue(XDataProperty, value); }
        }

        #endregion XData プロパティ

        #region YData プロパティ

        /// <summary>
        /// YData 依存関係プロパティを IEnumerable&lt;double&gt; 型として定義します。
        /// </summary>
        public static readonly DependencyProperty YDataProperty = DependencyProperty.Register("YData", typeof(IEnumerable<double>), typeof(ColorMap), new UIPropertyMetadata(null, OnCollectionPropertyChanged));

        /// <summary>
        /// 縦軸データを取得または設定します。
        /// </summary>
        public IEnumerable<double> YData
        {
            get { return (IEnumerable<double>)GetValue(YDataProperty); }
            set { SetValue(YDataProperty, value); }
        }

        #endregion YData プロパティ

        #region ZData プロパティ

        /// <summary>
        /// ZData 依存関係プロパティを IEnumerable&lt;double&gt; 型として定義します。
        /// </summary>
        public static readonly DependencyProperty ZDataProperty = DependencyProperty.Register("ZData", typeof(IEnumerable<double>), typeof(ColorMap), new UIPropertyMetadata(null, OnCollectionPropertyChanged));

        /// <summary>
        /// 3 次元データを取得または設定します。
        /// </summary>
        public IEnumerable<double> ZData
        {
            get { return (IEnumerable<double>)GetValue(ZDataProperty); }
            set { SetValue(ZDataProperty, value); }
        }

        #endregion ZData プロパティ

        #region GraphTitle プロパティ

        /// <summary>
        /// グラフタイトルを string 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty GraphTitleProperty = DependencyProperty.Register("GraphTitle", typeof(string), typeof(ColorMap), new UIPropertyMetadata("Graph Title"));

        /// <summary>
        /// グラフタイトルを取得または設定します。
        /// </summary>
        public string GraphTitle
        {
            get { return (string)GetValue(GraphTitleProperty); }
            set { SetValue(GraphTitleProperty, value); }
        }

        #endregion GraphTitle プロパティ

        #region GraphTitleFontSize プロパティ

        /// <summary>
        /// グラフタイトルのフォントサイズを double 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty GraphTitleFontSizeProperty = DependencyProperty.Register("GraphTitleFontSize", typeof(double), typeof(ColorMap), new UIPropertyMetadata(16.0));

        /// <summary>
        /// グラフタイトルのフォントサイズを取得または設定します。
        /// </summary>
        public double GraphTitleFontSize
        {
            get { return (double)GetValue(GraphTitleFontSizeProperty); }
            set { SetValue(GraphTitleFontSizeProperty, value); }
        }

        #endregion GraphTitleFontSize プロパティ

        #region XAxisSettings プロパティ

        /// <summary>
        /// グラフの横軸目盛線に関する設定を AxisSettings 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty XAxisSettingsProperty = DependencyProperty.Register("XAxisSettings", typeof(AxisSettings), typeof(ColorMap), new UIPropertyMetadata(AxisSettings.CreateDefault(), OnAxisSettingsPropertyChanged));

        /// <summary>
        /// グラフの横軸目盛線に関する設定を取得または設定します。
        /// </summary>
        public AxisSettings XAxisSettings
        {
            get { return (AxisSettings)GetValue(XAxisSettingsProperty); }
            set { SetValue(XAxisSettingsProperty, value); }
        }

        #endregion XAxisSettings プロパティ

        #region YAxisSettings プロパティ

        /// <summary>
        /// グラフの縦軸目盛線に関する設定を AxisSettings 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty YAxisSettingsProperty = DependencyProperty.Register("YAxisSettings", typeof(AxisSettings), typeof(ColorMap), new UIPropertyMetadata(AxisSettings.CreateDefault(), OnAxisSettingsPropertyChanged));

        /// <summary>
        /// グラフの縦軸目盛線に関する設定を取得または設定します。
        /// </summary>
        public AxisSettings YAxisSettings
        {
            get { return (AxisSettings)GetValue(YAxisSettingsProperty); }
            set { SetValue(YAxisSettingsProperty, value); }
        }

        #endregion YAxisSettings プロパティ

        #region MajorGridColor プロパティ

        /// <summary>
        /// グラフの目盛線の色を Color 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty MajorGridColorProperty = DependencyProperty.Register("MajorGridColor", typeof(Color), typeof(ColorMap), new UIPropertyMetadata(Colors.DarkGray, OnMajorGridColorPropertyChanged));

        /// <summary>
        /// グラフの目盛線の色を取得または設定します。
        /// </summary>
        public Color MajorGridColor
        {
            get { return (Color)GetValue(MajorGridColorProperty); }
            set { SetValue(MajorGridColorProperty, value); }
        }

        /// <summary>
        /// MajorGridColor プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnMajorGridColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d as ColorMap).IsLoaded)
                (d as ColorMap).UpdateRendering_GridBitmap();
        }

        #endregion MajorGridColor プロパティ

        #region MinorGridColor プロパティ

        /// <summary>
        /// グラフの目盛補助線の色を Color 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty MinorGridColorProperty = DependencyProperty.Register("MinorGridColor", typeof(Color), typeof(ColorMap), new UIPropertyMetadata(Colors.Teal, OnMinorGridColorPropertyChanged));

        /// <summary>
        /// グラフの目盛補助線の色を取得または設定します。
        /// </summary>
        public Color MinorGridColor
        {
            get { return (Color)GetValue(MinorGridColorProperty); }
            set { SetValue(MinorGridColorProperty, value); }
        }

        /// <summary>
        /// MinorGridColor プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnMinorGridColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d as ColorMap).IsLoaded)
                (d as ColorMap).UpdateRendering_GridBitmap();
        }

        #endregion MinorGridColor プロパティ

        #region GraphAreaBackgroundColor プロパティ

        /// <summary>
        /// グラフ描画エリアの背景色を Color 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty GraphAreaBackgroundColorProperty = DependencyProperty.Register("GraphAreaBackgroundColor", typeof(Color), typeof(ColorMap), new UIPropertyMetadata(Colors.Transparent, OnGraphAreaBackgroundColorPropertyChanged));

        /// <summary>
        /// グラフ描画エリアの背景色を取得または設定します。
        /// </summary>
        public Color GraphAreaBackgroundColor
        {
            get { return (Color)GetValue(GraphAreaBackgroundColorProperty); }
            set { SetValue(GraphAreaBackgroundColorProperty, value); }
        }

        /// <summary>
        /// GraphAreaBackgroundColor プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnGraphAreaBackgroundColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d as ColorMap).IsLoaded)
                (d as ColorMap).UpdateRendering_GridBitmap();
        }

        #endregion GraphAreaBackgroundColor プロパティ

        #region AutoGraphAreaBackgroundColor プロパティ

        /// <summary>
        /// グラフ描画エリアの自動背景色を bool 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty AutoGraphAreaBackgroundColorProperty = DependencyProperty.Register("AutoGraphAreaBackgroundColor", typeof(bool), typeof(ColorMap), new UIPropertyMetadata(true, OnAutoGraphAreaBackgroundColorPropertyChanged));

        /// <summary>
        /// グラフ描画エリアの背景色を取得または設定します。
        /// </summary>
        public bool AutoGraphAreaBackgroundColor
        {
            get { return (bool)GetValue(AutoGraphAreaBackgroundColorProperty); }
            set { SetValue(AutoGraphAreaBackgroundColorProperty, value); }
        }

        /// <summary>
        /// AutoGraphAreaBackgroundColor プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnAutoGraphAreaBackgroundColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d as ColorMap).IsLoaded)
                (d as ColorMap).UpdateRendering_GridBitmap();
        }

        #endregion AutoGraphAreaBackgroundColor プロパティ

        #region GraphAreaBorderColor プロパティ

        /// <summary>
        /// グラフ描画エリアの枠線の色を Color 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty GraphAreaBorderColorProperty = DependencyProperty.Register("GraphAreaBorderColor", typeof(Color), typeof(ColorMap), new UIPropertyMetadata(Colors.DarkGray, OnGraphAreaBorderColorPropertyChanged));

        /// <summary>
        /// グラフ描画エリアの枠線の色を取得または設定します。
        /// </summary>
        public Color GraphAreaBorderColor
        {
            get { return (Color)GetValue(GraphAreaBorderColorProperty); }
            set { SetValue(GraphAreaBorderColorProperty, value); }
        }

        /// <summary>
        /// GraphAreaBorderColor プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnGraphAreaBorderColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d as ColorMap).IsLoaded)
                (d as ColorMap).UpdateRendering_GridBitmap();
        }

        #endregion GraphAreaBorderColor プロパティ

        #region GraphAreaBorderThickness プロパティ

        /// <summary>
        /// グラフ描画エリアの枠線の太さを Thickness 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty GraphAreaBorderThicknessProperty = DependencyProperty.Register("GraphAreaBorderThickness", typeof(Thickness), typeof(ColorMap), new UIPropertyMetadata(new Thickness(1), OnGraphAreaBorderThicknessPropertyChanged));

        /// <summary>
        /// グラフ描画エリアの枠線の太さを取得または設定します。
        /// </summary>
        public Thickness GraphAreaBorderThickness
        {
            get { return (Thickness)GetValue(GraphAreaBorderThicknessProperty); }
            set { SetValue(GraphAreaBorderThicknessProperty, value); }
        }

        /// <summary>
        /// GraphAreaBorderThickness プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnGraphAreaBorderThicknessPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d as ColorMap).IsLoaded)
                (d as ColorMap).UpdateRendering_GridBitmap();
        }

        #endregion GraphAreaBorderThickness プロパティ

        #region GraphAreaMargin プロパティ

        /// <summary>
        /// グラフ描画エリアの余白を Thickness 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty GraphAreaMarginProperty = DependencyProperty.Register("GraphAreaMargin", typeof(Thickness), typeof(ColorMap), new UIPropertyMetadata(new Thickness(60, 40, 20, 40), OnGraphAreaMarginPropertyChanged));

        /// <summary>
        /// グラフ描画エリアの余白を取得または設定します。
        /// </summary>
        public Thickness GraphAreaMargin
        {
            get { return (Thickness)GetValue(GraphAreaMarginProperty); }
            set { SetValue(GraphAreaMarginProperty, value); }
        }

        /// <summary>
        /// GraphAreaMargin プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnGraphAreaMarginPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //if ((d as ColorMap).IsLoaded)
            //    (d as ColorMap).InitWriteableBitmap();
        }

        #endregion GraphAreaMargin プロパティ

        /// <summary>
        /// コレクションの依存関係プロパティ変更イベントハンドラです。
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnCollectionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorMap = d as ColorMap;

            if (e.OldValue != null)
            {
                if (e.OldValue is INotifyCollectionChanged)
                {
#if NET4
                    (e.OldValue as INotifyCollectionChanged).CollectionChanged -= colorMap.OnCollectionChanged;
#else
                    CollectionChangedEventManager.RemoveHandler(e.OldValue as INotifyCollectionChanged, colorMap.OnCollectionChanged);
#endif
                }
            }
            if (e.NewValue != null)
            {
                if (e.NewValue is INotifyCollectionChanged)
                {
#if NET4
                    (e.OldValue as INotifyCollectionChanged).CollectionChanged += colorMap.OnCollectionChanged;
#else
                    CollectionChangedEventManager.AddHandler(e.NewValue as INotifyCollectionChanged, colorMap.OnCollectionChanged);
#endif
                }
            }

            colorMap.UpdateRendering_GraphBitmap();
        }

        public ColorMap()
        {
            #region メインコンテナの初期化

            // インスタンス化
            this._container = new Grid();

            // 子要素の事前準備
            var width = this.RenderSize.Width - this.GraphAreaMargin.Left - this.GraphAreaMargin.Right;
            var height = this.RenderSize.Height - this.GraphAreaMargin.Top - this.GraphAreaMargin.Bottom;
            if (width < 0) width = 0;
            if (height < 0) height = 0;
            this._graphArea = new Rect(this.GraphAreaMargin.Left, this.GraphAreaMargin.Top, width, height);

            for (var i = 0; i < AxisLabelObjectPoolCount; i++)
            {
                this._xAxisLabels[i] = new TextBlock() { TextAlignment = TextAlignment.Center, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
                this._container.Children.Add(this._xAxisLabels[i]);

                this._yAxisLabels[i] = new TextBlock() { TextAlignment = TextAlignment.Right, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
                this._container.Children.Add(this._yAxisLabels[i]);
            }

            // ルート要素として配置
            this.Content = this._container;

            #endregion メインコンテナの初期化

            #region デザインモードのときは実行しない

            if (DesignerProperties.GetIsInDesignMode(this) == false)
            {
                this.SizeChanged += OnSizeChanged;
            }

            #endregion デザインモードのときは実行しない


        }

        /// <summary>
        /// コレクション要素変更イベントハンドラです。
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateRendering_GraphBitmap();
        }

        /// <summary>
        /// 軸設定プロパティ値変更イベントハンドラです。
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnAxisSettingsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorMap = d as ColorMap;
            if (colorMap.IsLoaded)
            {
                colorMap.UpdateRendering_GridBitmap();
                colorMap.UpdateRendering_GraphBitmap();
            }
        }

        /// <summary>
        /// SizeChanged イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            InitWriteableBitmap();
        }

        /// <summary>
        /// WriteableBitmap 関連の初期化をおこないます。
        /// </summary>
        private void InitWriteableBitmap()
        {
            // GraphAreaMargin プロパティなどの変更によって
            // GraphImage コントロールのサイズが変更されたときは必ず呼び出すこと。

            var width = this.RenderSize.Width - this.GraphAreaMargin.Left - this.GraphAreaMargin.Right;
            var height = this.RenderSize.Height - this.GraphAreaMargin.Top - this.GraphAreaMargin.Bottom;
            if (width < 0) width = 0;
            if (height < 0) height = 0;
            this._graphArea = new Rect(this.GraphAreaMargin.Left, this.GraphAreaMargin.Top, width, height);

            // グラフ描画エリアの枠線と目盛線
            //this._gridBitmap = (width == 0) || (height == 0) ? null : new WriteableBitmap((int)(this._graphArea.Width * this._scale.DpiScaleX), (int)(this._graphArea.Height * this._scale.DpiScaleY), this._scale.PixelsPerInchX, this._scale.PixelsPerInchY, PixelFormats.Pbgra32, null);
            this._gridBitmap = (width == 0) || (height == 0) ? null : new WriteableBitmap((int)this._graphArea.Width, (int)this._graphArea.Height, 96, 96, PixelFormats.Pbgra32, null);
            this._gridImage.Source = this._gridBitmap;
            this._gridImage.Margin = this.GraphAreaMargin;
            //this._shouldUpdate_GridBitmap = true;

            // グラフタイトルと軸ラベル
            this._xAxisTitle.Margin = new Thickness(this.GraphAreaMargin.Left, 0, 0, 0);
            this._yAxisTitle.Margin = new Thickness(0, 0, 0, this.GraphAreaMargin.Bottom);

            // グラフデータ
            //this._graphBitmap = (width == 0) || (height == 0) ? null : new WriteableBitmap((int)(this._graphArea.Width * this._scale.DpiScaleX), (int)(this._graphArea.Height * this._scale.DpiScaleY), this._scale.PixelsPerInchX, this._scale.PixelsPerInchY, PixelFormats.Pbgra32, null);
            this._graphBitmap = (width == 0) || (height == 0) ? null : new WriteableBitmap((int)this._graphArea.Width, (int)this._graphArea.Height, 96, 96, PixelFormats.Pbgra32, null);
            this._graphImage.Source = this._graphBitmap;
            this._graphImage.Margin = this.GraphAreaMargin;
            //this._shouldUpdate_GraphBitmap = true;

            UpdateRendering();
        }

        private void UpdateRendering()
        {
            // デザインモードのときは実行しない
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            // 座標変換のための設定を更新する
            //this._graphBitmap.Lock();

            // グラフ描画エリアの枠線と目盛線
            //if (this._shouldUpdate_GridBitmap)
            {
                UpdateRendering_GridBitmap();
                //this._shouldUpdate_GridBitmap = false;
            }

            // グラフデータ
            //if (this._shouldUpdate_GraphBitmap)
            {
                UpdateRendering_GraphBitmap();
                //UpdateRendering_Cursor();
                //UpdateRendering_Legend();
                //this._shouldUpdate_GraphBitmap = false;
            }

            //this._graphBitmap.AddDirtyRect(new Int32Rect(0, 0, this._graphBitmap.PixelWidth, this._graphBitmap.PixelHeight));
            //this._graphBitmap.Unlock();
        }

        /// <summary>
        /// グラフの目盛軸を描画します。
        /// </summary>
        private void UpdateRendering_GridBitmap()
        {
            if (this._gridBitmap == null) return;
            this._gridBitmap.Clear();

            if (this.IsLoaded == false) return;


        }

        /// <summary>
        /// グラフを描画します。
        /// </summary>
        private void UpdateRendering_GraphBitmap()
        {
            if (this._graphBitmap == null) return;
            this._graphBitmap.Clear();

            //if (this.IsLoaded == false) return;
            if (this.XData == null) return;
            if (this.YData == null) return;

            // グラフタイトル、軸タイトル、軸ラベルのテキストすべてをここで確定する

            this._graphTitle.Text = this.GraphTitle;
            this._xAxisTitle.Text = this.XAxisSettings.Title;
            this._yAxisTitle.Text = this.YAxisSettings.Title;

            this._graphTitle.FontSize = this.GraphTitleFontSize;
            this._xAxisTitle.FontSize = this.XAxisSettings.TitleFontSize;
            this._yAxisTitle.FontSize = this.YAxisSettings.TitleFontSize;

            var ptGrid = new Point(this.XAxisSettings.Minimum, this.YAxisSettings.Minimum);
            var xAxisLavelMaxHeight = 0.0;
            var yAxisLavelMaxWidth = 0.0;
            double temp;
            for (var i = 0; i < AxisLabelObjectPoolCount; i++)
            {
                ptGrid.X = this.XAxisSettings.Minimum + i * this.XAxisSettings.MajorStep;
                ptGrid.Y = this.YAxisSettings.Minimum + i * this.YAxisSettings.MajorStep;
                this._xAxisLabels[i].FontSize = this.XAxisSettings.FontSize;
                this._yAxisLabels[i].FontSize = this.YAxisSettings.FontSize;
                this._xAxisLabels[i].Text = ptGrid.X.ToString(this.XAxisSettings.StringFormat);
                this._yAxisLabels[i].Text = ptGrid.Y.ToString(this.YAxisSettings.StringFormat);
                this._xAxisLabels[i].Arrange(new Rect(new Size(this.ActualWidth, this.ActualHeight)));
                this._yAxisLabels[i].Arrange(new Rect(new Size(this.ActualWidth, this.ActualHeight)));

                // ここで各軸ラベルの視認性を設定します。
                // 配置は後でします。

                // 横軸ラベルの視認性設定
                temp = SetXAxisLabelVisibility(this._xAxisLabels[i], ptGrid.X);
                if (xAxisLavelMaxHeight < temp) xAxisLavelMaxHeight = temp;

                // 縦軸ラベルの視認性設定
                // 配置は後でします。
                temp = SetYAxisLabelVisibility(this._yAxisLabels[i], ptGrid.Y);
                if (yAxisLavelMaxWidth < temp) yAxisLavelMaxWidth = temp;
            }

            // グラフ描画エリアを背景色でクリアする
            this._gridBitmap.Clear(Colors.Transparent);
            this._gridBitmap.FillRectangle(0, 0, (int)this._graphArea.Width, (int)this._graphArea.Height, this.AutoGraphAreaBackgroundColor ? (Color)this.FindResource("BoxBaseColor") : this.GraphAreaBackgroundColor);

            // 目盛補助線を先に描く
            var ptGridScreen = new Point();
            // 横軸目盛補助線は補助線が有効のときだけ表示する
            if (this.XAxisSettings.IsMajorGridEnabled && this.XAxisSettings.IsMinorGridEnabled && (this.XAxisSettings.MinorStep > 0))
            {
                ptGrid.X = this.XAxisSettings.Minimum + this.XAxisSettings.MinorStep;
                while (ptGrid.X < this.XAxisSettings.Maximum)
                {
                    ptGridScreen.X = XAxisToScreen(ptGrid.X);
                    this._gridBitmap.DrawLineAa((int)ptGridScreen.X, 0, (int)ptGridScreen.X, (int)this._graphArea.Height, this.MinorGridColor, (int)this.XAxisSettings.MinorGridThickness);
                    ptGrid.X += this.XAxisSettings.MinorStep;
                }
            }

            // 縦軸目盛補助線
            ptGrid.Y = this.YAxisSettings.Minimum + this.YAxisSettings.MinorStep;
            if (this.YAxisSettings.IsMajorGridEnabled && this.YAxisSettings.IsMinorGridEnabled)
            {
                ptGrid.Y = this.YAxisSettings.Minimum + this.YAxisSettings.MinorStep;
                while (ptGrid.Y < this.YAxisSettings.Maximum)
                {
                    ptGridScreen.Y = YAxisToScreen(ptGrid.Y);
                    this._gridBitmap.DrawLineAa(0, (int)ptGridScreen.Y, (int)this._graphArea.Width, (int)ptGridScreen.Y, this.MinorGridColor, (int)this.YAxisSettings.MinorGridThickness);
                    ptGrid.Y += this.YAxisSettings.MinorStep;
                }
            }

            // 横軸目盛線と軸ラベル
            if (this.XAxisSettings.IsMajorGridEnabled && (this.XAxisSettings.MajorStep > 0))
            {
                var labelIndex = 0;
                for (var i = 0; i < AxisLabelObjectPoolCount; i++)
                {
                    ptGrid.X = this.XAxisSettings.Minimum + i * this.XAxisSettings.MajorStep;
                    if (ptGrid.X > this.XAxisSettings.Maximum)
                        break;

                    ptGridScreen.X = XAxisToScreen(ptGrid.X);

                    // 目盛線
                    if ((this.XAxisSettings.Minimum < ptGrid.X) && (ptGrid.X < this.XAxisSettings.Maximum))
                    {
                        this._gridBitmap.DrawLineAa((int)ptGridScreen.X, 0, (int)ptGridScreen.X, (int)this._graphArea.Height, this.MajorGridColor, (int)this.XAxisSettings.MajorGridThickness);
                    }

                    // 軸ラベル
                    this._xAxisLabels[labelIndex].Margin = new Thickness(this._graphArea.Left + ptGridScreen.X - this._xAxisLabels[labelIndex].ActualWidth / 2.0, this._graphArea.Bottom, 0, 0);
                    labelIndex++;
                }
            }

            // 縦軸目盛線
            if (this.YAxisSettings.IsMajorGridEnabled && (this.YAxisSettings.MajorStep > 0))
            {
                var labelIndex = 0;
                for (var i = 0; i < AxisLabelObjectPoolCount; i++)
                {
                    ptGrid.Y = this.YAxisSettings.Minimum + i * this.YAxisSettings.MajorStep;
                    if (ptGrid.Y > this.YAxisSettings.Maximum)
                        break;

                    ptGridScreen.Y = YAxisToScreen(ptGrid.Y);

                    // 目盛線
                    if ((this.YAxisSettings.Minimum < ptGrid.Y) && (ptGrid.Y < this.YAxisSettings.Maximum))
                    {
                        this._gridBitmap.DrawLineAa(0, (int)ptGridScreen.Y, (int)this._graphArea.Width, (int)ptGridScreen.Y, this.MajorGridColor, (int)this.YAxisSettings.MajorGridThickness);
                    }

                    // 軸ラベル
                    this._yAxisLabels[labelIndex].Margin = new Thickness(this._graphArea.Left - this._yAxisLabels[labelIndex].RenderSize.Width - 4, this._graphArea.Top + ptGridScreen.Y - this._yAxisLabels[labelIndex].ActualHeight / 2.0, 0, 0);
                    labelIndex++;
                }
            }

            // グラフ描画エリアの枠線
            this._gridBitmap.DrawLineAa((int)(this.GraphAreaBorderThickness.Left / 2), 0, (int)(this.GraphAreaBorderThickness.Left / 2), (int)this._graphArea.Height, this.GraphAreaBorderColor, (int)this.GraphAreaBorderThickness.Left);
            this._gridBitmap.DrawLineAa(0, (int)(this.GraphAreaBorderThickness.Top / 2), (int)this._graphArea.Width, (int)(this.GraphAreaBorderThickness.Top / 2), this.GraphAreaBorderColor, (int)this.GraphAreaBorderThickness.Top);
            this._gridBitmap.DrawLineAa((int)(this._graphArea.Width - this.GraphAreaBorderThickness.Right / 2), 0, (int)(this._graphArea.Width - this.GraphAreaBorderThickness.Right / 2), (int)this._graphArea.Height, this.GraphAreaBorderColor, (int)this.GraphAreaBorderThickness.Right);
            this._gridBitmap.DrawLineAa(0, (int)(this._graphArea.Height - this.GraphAreaBorderThickness.Bottom / 2), (int)this._graphArea.Width, (int)(this._graphArea.Height - this.GraphAreaBorderThickness.Bottom / 2), this.GraphAreaBorderColor, (int)this.GraphAreaBorderThickness.Bottom);
        }

        private double SetXAxisLabelVisibility(TextBlock textBlock, double value)
        {
            var height = 0.0;
            if (this.XAxisSettings.IsMajorGridEnabled)
            {
                if ((this.XAxisSettings.GridLabelVisibility != Visibility.Collapsed) && (value <= this.XAxisSettings.Maximum))
                {
                    textBlock.Visibility = this.XAxisSettings.GridLabelVisibility;
                    // 最大高さ探索用に戻す
                    height = textBlock.RenderSize.Height;
                }
                else
                {
                    textBlock.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                textBlock.Visibility = Visibility.Collapsed;
            }
            return height;
        }

        private double SetYAxisLabelVisibility(TextBlock textBlock, double value)
        {
            var width = 0.0;
            if (this.YAxisSettings.IsMajorGridEnabled)
            {
                if ((this.YAxisSettings.GridLabelVisibility != Visibility.Collapsed) && (value <= this.YAxisSettings.Maximum))
                {
                    textBlock.Visibility = this.YAxisSettings.GridLabelVisibility;
                    // 最大幅探索用に戻す
                    width = textBlock.RenderSize.Width;
                }
                else
                {
                    textBlock.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                textBlock.Visibility = Visibility.Collapsed;
            }
            return width;
        }

        /// <summary>
        /// 横軸データをスクリーン座標に変換します。
        /// </summary>
        /// <param name="value">横軸データの値を指定します。</param>
        /// <returns>変換後の値を返します。</returns>
        private int XAxisToScreen(double value)
        {
            return (int)((value - this.XAxisSettings.Minimum) * this._graphArea.Width / this.XAxisSettings.Range);
        }

        /// <summary>
        /// 縦軸データをスクリーン座標に変換します。
        /// </summary>
        /// <param name="value">縦軸データの値を指定します。</param>
        /// <returns>変換後の値を返します。</returns>
        private int YAxisToScreen(double value)
        {
            return (int)(this._graphArea.Height - (value - this.YAxisSettings.Minimum) * this._graphArea.Height / this.YAxisSettings.Range);
        }

        private Grid _container;

        /// <summary>
        /// グラフ描画エリアの領域
        /// </summary>
        private Rect _graphArea;

        private Image _graphImage = new Image() { Stretch = Stretch.None };
        private WriteableBitmap _graphBitmap;

        private Image _gridImage = new Image() { Stretch = Stretch.None };
        private WriteableBitmap _gridBitmap;

        /// <summary>
        /// グラフタイトル用
        /// </summary>
        private TextBlock _graphTitle = new TextBlock() { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Top };

        /// <summary>
        /// 横軸タイトル用
        /// </summary>
        private TextBlock _xAxisTitle = new TextBlock() { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Bottom };

        /// <summary>
        /// 縦軸タイトル用
        /// </summary>
        private TextBlock _yAxisTitle = new TextBlock() { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Center, LayoutTransform = new RotateTransform(-90) };

        /// <summary>
        /// 横軸ラベル用オブジェクトプール
        /// </summary>
        private TextBlock[] _xAxisLabels = new TextBlock[AxisLabelObjectPoolCount];

        /// <summary>
        /// 縦軸ラベル用オブジェクトプール
        /// </summary>
        private TextBlock[] _yAxisLabels = new TextBlock[AxisLabelObjectPoolCount];

        /// <summary>
        /// 軸ラベルオブジェクトプールのバッファ数
        /// </summary>
        private const int AxisLabelObjectPoolCount = 30;
    }
}
