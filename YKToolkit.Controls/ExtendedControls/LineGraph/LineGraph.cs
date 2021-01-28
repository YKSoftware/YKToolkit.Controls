namespace YKToolkit.Controls
{
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;
    using YKToolkit.Bindings;
    using YKToolkit.Helpers;

    /// <summary>
    /// 折れ線グラフコントロールを表します。
    /// このコントロールは WriteableBitmapEx を使用しています。(https://github.com/reneschulte/WriteableBitmapEx/blob/master/LICENSE)
    /// </summary>
    public class LineGraph : ContentControl
    {
        #region GraphAreaBackgroundColor プロパティ

        /// <summary>
        /// グラフ描画エリアの背景色を Color 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty GraphAreaBackgroundColorProperty = DependencyProperty.Register("GraphAreaBackgroundColor", typeof(Color), typeof(LineGraph), new UIPropertyMetadata(Colors.Transparent, OnGraphAreaBackgroundColorPropertyChanged));

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
            if ((d as LineGraph).IsLoaded)
                (d as LineGraph).UpdateRendering_GridBitmap();
        }

        #endregion GraphAreaBackgroundColor プロパティ

        #region AutoGraphAreaBackgroundColor プロパティ

        /// <summary>
        /// グラフ描画エリアの自動背景色を bool 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty AutoGraphAreaBackgroundColorProperty = DependencyProperty.Register("AutoGraphAreaBackgroundColor", typeof(bool), typeof(LineGraph), new UIPropertyMetadata(true, OnAutoGraphAreaBackgroundColorPropertyChanged));

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
            if ((d as LineGraph).IsLoaded)
                (d as LineGraph).UpdateRendering_GridBitmap();
        }

        #endregion AutoGraphAreaBackgroundColor プロパティ

        #region GraphAreaBorderColor プロパティ

        /// <summary>
        /// グラフ描画エリアの枠線の色を Color 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty GraphAreaBorderColorProperty = DependencyProperty.Register("GraphAreaBorderColor", typeof(Color), typeof(LineGraph), new UIPropertyMetadata(Colors.DarkGray, OnGraphAreaBorderColorPropertyChanged));

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
            if ((d as LineGraph).IsLoaded)
                (d as LineGraph).UpdateRendering_GridBitmap();
        }

        #endregion GraphAreaBorderColor プロパティ

        #region GraphAreaBorderThickness プロパティ

        /// <summary>
        /// グラフ描画エリアの枠線の太さを Thickness 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty GraphAreaBorderThicknessProperty = DependencyProperty.Register("GraphAreaBorderThickness", typeof(Thickness), typeof(LineGraph), new UIPropertyMetadata(new Thickness(1), OnGraphAreaBorderThicknessPropertyChanged));

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
            if ((d as LineGraph).IsLoaded)
                (d as LineGraph).UpdateRendering_GridBitmap();
        }

        #endregion GraphAreaBorderThickness プロパティ

        #region GraphAreaMargin プロパティ

        /// <summary>
        /// グラフ描画エリアの余白を Thickness 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty GraphAreaMarginProperty = DependencyProperty.Register("GraphAreaMargin", typeof(Thickness), typeof(LineGraph), new UIPropertyMetadata(new Thickness(60,40,20,40), OnGraphAreaMarginPropertyChanged));

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
            if ((d as LineGraph).IsLoaded)
                (d as LineGraph).InitWriteableBitmap();
        }

        #endregion GraphAreaMargin プロパティ

        #region MajorGridColor プロパティ

        /// <summary>
        /// グラフの目盛線の色を Color 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty MajorGridColorProperty = DependencyProperty.Register("MajorGridColor", typeof(Color), typeof(LineGraph), new UIPropertyMetadata(Colors.DarkGray, OnMajorGridColorPropertyChanged));

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
            if ((d as LineGraph).IsLoaded)
                (d as LineGraph).UpdateRendering_GridBitmap();
        }

        #endregion MajorGridColor プロパティ

        #region MinorGridColor プロパティ

        /// <summary>
        /// グラフの目盛補助線の色を Color 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty MinorGridColorProperty = DependencyProperty.Register("MinorGridColor", typeof(Color), typeof(LineGraph), new UIPropertyMetadata(Colors.Teal, OnMinorGridColorPropertyChanged));

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
            if ((d as LineGraph).IsLoaded)
                (d as LineGraph).UpdateRendering_GridBitmap();
        }

        #endregion MinorGridColor プロパティ

        #region GraphTitle プロパティ

        /// <summary>
        /// グラフタイトルを string 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty GraphTitleProperty = DependencyProperty.Register("GraphTitle", typeof(string), typeof(LineGraph), new UIPropertyMetadata("Graph Title"));

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
        public static readonly DependencyProperty GraphTitleFontSizeProperty = DependencyProperty.Register("GraphTitleFontSize", typeof(double), typeof(LineGraph), new UIPropertyMetadata(16.0));

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
        public static readonly DependencyProperty XAxisSettingsProperty = DependencyProperty.Register("XAxisSettings", typeof(AxisSettings), typeof(LineGraph), new UIPropertyMetadata(AxisSettings.CreateDefault(), OnXAxisSettingsPropertyChanged));

        /// <summary>
        /// グラフの横軸目盛線に関する設定を取得または設定します。
        /// </summary>
        public AxisSettings XAxisSettings
        {
            get { return (AxisSettings)GetValue(XAxisSettingsProperty); }
            set { SetValue(XAxisSettingsProperty, value); }
        }

        /// <summary>
        /// XAxisSettings プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnXAxisSettingsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d as LineGraph).IsLoaded)
                (d as LineGraph).UpdateRendering_GridBitmap();
        }

        #endregion XAxisSettings プロパティ

        #region YAxisSettings プロパティ

        /// <summary>
        /// グラフの縦軸目盛線に関する設定を AxisSettings 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty YAxisSettingsProperty = DependencyProperty.Register("YAxisSettings", typeof(AxisSettings), typeof(LineGraph), new UIPropertyMetadata(AxisSettings.CreateDefault(), OnYAxisSettingsPropertyChanged));

        /// <summary>
        /// グラフの縦軸目盛線に関する設定を取得または設定します。
        /// </summary>
        public AxisSettings YAxisSettings
        {
            get { return (AxisSettings)GetValue(YAxisSettingsProperty); }
            set { SetValue(YAxisSettingsProperty, value); }
        }

        /// <summary>
        /// YAxisSettings プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnYAxisSettingsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d as LineGraph).IsLoaded)
                (d as LineGraph).UpdateRendering_GridBitmap();
        }

        #endregion YAxisSettings プロパティ

        #region Y2AxisSettings プロパティ

        /// <summary>
        /// グラフの第 2 主軸目盛線に関する設定を AxisSettings 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty Y2AxisSettingsProperty = DependencyProperty.Register("Y2AxisSettings", typeof(AxisSettings), typeof(LineGraph), new UIPropertyMetadata(AxisSettings.CreateDefault(), OnYAxisSettingsPropertyChanged));

        /// <summary>
        /// グラフの第 2 主軸目盛線に関する設定を取得または設定します。
        /// </summary>
        public AxisSettings Y2AxisSettings
        {
            get { return (AxisSettings)GetValue(Y2AxisSettingsProperty); }
            set { SetValue(Y2AxisSettingsProperty, value); }
        }

        /// <summary>
        /// Y2AxisSettings プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnY2AxisSettingsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d as LineGraph).IsLoaded)
                (d as LineGraph).UpdateRendering_GridBitmap();
        }

        #endregion Y2AxisSettings プロパティ

        #region IsLegendEnabled プロパティ

        /// <summary>
        /// グラフの凡例の表示を bool 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty IsLegendEnabledProperty = DependencyProperty.Register("IsLegendEnabled", typeof(bool), typeof(LineGraph), new UIPropertyMetadata(true, OnIsLegendEnabledPropertyChanged));

        /// <summary>
        /// グラフの凡例の表示位置を取得または設定します。
        /// </summary>
        public bool IsLegendEnabled
        {
            get { return (bool)GetValue(IsLegendEnabledProperty); }
            set { SetValue(IsLegendEnabledProperty, value); }
        }

        /// <summary>
        /// IsLegendEnabled プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnIsLegendEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d as LineGraph).IsLoaded)
                (d as LineGraph).UpdateRendering_Legend();
        }

        #endregion IsLegendEnabled プロパティ

        #region IsContextMenuEnabled プロパティ

        /// <summary>
        /// グラフのコンテキストメニューの表示を bool 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty IsContextMenuEnabledProperty = DependencyProperty.Register("IsContextMenuEnabled", typeof(bool), typeof(LineGraph), new UIPropertyMetadata(true, OnIsContextMenuEnabledPropertyChanged));

        /// <summary>
        /// グラフのコンテキストメニューの表示を取得または設定します。
        /// </summary>
        public bool IsContextMenuEnabled
        {
            get { return (bool)GetValue(IsContextMenuEnabledProperty); }
            set { SetValue(IsContextMenuEnabledProperty, value); }
        }

        /// <summary>
        /// IsContextMenuEnabled プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnIsContextMenuEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var graph = d as LineGraph;
            graph._container.ContextMenu = graph.IsContextMenuEnabled ? graph._contextMenu : null;
        }

        #endregion IsContextMenuEnabled プロパティ

        #region LegendPosition プロパティ

        /// <summary>
        /// グラフの凡例の表示位置を LegendPositions 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty LegendPositionProperty = DependencyProperty.Register("LegendPosition", typeof(LegendPositions), typeof(LineGraph), new UIPropertyMetadata(LegendPositions.TopLeft, OnLegendPositionsPropertyChanged));

        /// <summary>
        /// グラフの凡例の表示位置を取得または設定します。
        /// </summary>
        public LegendPositions LegendPosition
        {
            get { return (LegendPositions)GetValue(LegendPositionProperty); }
            set { SetValue(LegendPositionProperty, value); }
        }

        /// <summary>
        /// LegendPosition プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnLegendPositionsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d as LineGraph).IsLoaded)
                (d as LineGraph).UpdateLegendPosition();
        }

        #endregion LegendPosition プロパティ

        #region LegendMargin プロパティ

        /// <summary>
        /// グラフの凡例の表示位置を LegendPositions 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty LegendMarginProperty = DependencyProperty.Register("LegendMargin", typeof(Thickness), typeof(LineGraph), new UIPropertyMetadata(new Thickness(16), OnLegendMarginPropertyChanged));

        /// <summary>
        /// グラフの凡例の表示位置を取得または設定します。
        /// </summary>
        public Thickness LegendMargin
        {
            get { return (Thickness)GetValue(LegendMarginProperty); }
            set { SetValue(LegendMarginProperty, value); }
        }

        /// <summary>
        /// LegendMargin プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnLegendMarginPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d as LineGraph).IsLoaded)
                (d as LineGraph).UpdateLegendPosition();
        }

        #endregion LegendMargin プロパティ

        #region IsCursorEnabled プロパティ

        /// <summary>
        /// グラフカーソルの表示を bool 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty IsCursorEnabledProperty = DependencyProperty.Register("IsCursorEnabled", typeof(bool), typeof(LineGraph), new UIPropertyMetadata(false, OnIsCursorEnabledPropertyChanged));

        /// <summary>
        /// グラフカーソルの表示位置を取得または設定します。
        /// </summary>
        public bool IsCursorEnabled
        {
            get { return (bool)GetValue(IsCursorEnabledProperty); }
            set { SetValue(IsCursorEnabledProperty, value); }
        }

        /// <summary>
        /// IsCursorEnabled プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnIsCursorEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d as LineGraph).IsLoaded)
            {
                (d as LineGraph).UpdateRendering_Cursor();
                (d as LineGraph).UpdateRendering_Legend();
            }
        }

        #endregion IsCursorEnabled プロパティ

        #region GraphCursor プロパティ

        /// <summary>
        /// グラフカーソルの種類を GraphCursors 型の依存関係プロパティとして定義します。
        /// </summary>
        public static readonly DependencyProperty GraphCursorProperty = DependencyProperty.Register("GraphCursor", typeof(GraphCursors), typeof(LineGraph), new UIPropertyMetadata(GraphCursors.XCursor1 | GraphCursors.XCursor2, OnGraphCursorPropertyChanged));

        /// <summary>
        /// グラフカーソルの表示位置を取得または設定します。
        /// </summary>
        public GraphCursors GraphCursor
        {
            get { return (GraphCursors)GetValue(GraphCursorProperty); }
            set { SetValue(GraphCursorProperty, value); }
        }

        /// <summary>
        /// GraphCursor プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnGraphCursorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d as LineGraph).IsLoaded)
            {
                (d as LineGraph).UpdateRendering_Cursor();
                (d as LineGraph).UpdateRendering_Legend();
            }
        }

        #endregion GraphCursor プロパティ

        #region GraphDataCollection プロパティ

        /// <summary>
        /// グラフデータのコレクションを IEnumerable&lt;GraphData&gt; 型の依存関係プロパティとして定義します。
        /// </summary>
        private static readonly DependencyProperty GraphDataCollectionProperty = DependencyProperty.Register("GraphDataCollection", typeof(IEnumerable<LineGraphData>), typeof(LineGraph), new UIPropertyMetadata(new List<LineGraphData>()));

        /// <summary>
        /// グラフデータのコレクションを取得または設定します。
        /// </summary>
        public IEnumerable<LineGraphData> GraphDataCollection
        {
            get { return (IEnumerable<LineGraphData>)GetValue(GraphDataCollectionProperty); }
            set { SetValue(GraphDataCollectionProperty, value); }
        }

        #endregion GraphDataCollection プロパティ

        #region DrawKey プロパティ

        /// <summary>
        /// グラフ描画のキーを string 型の依存関係プロパティとして定義します。
        /// </summary>
        private static readonly DependencyProperty DrawKeyProperty = DependencyProperty.Register("DrawKey", typeof(string), typeof(LineGraph), new UIPropertyMetadata("GraphControl"));

        /// <summary>
        /// グラフ描画のキーを取得または設定します。
        /// </summary>
        public string DrawKey
        {
            get { return (string)GetValue(DrawKeyProperty); }
            set { SetValue(DrawKeyProperty, value); }
        }

        #endregion DrawKey プロパティ

        #region HorizontalSelectionArea プロパティ

        /// <summary>
        /// グラフの横軸に対する選択領域を Point 型の依存関係プロパティとして定義します。
        /// </summary>
        private static readonly DependencyProperty HorizontalSelectionAreaProperty = DependencyProperty.Register("HorizontalSelectionArea", typeof(Point), typeof(LineGraph), new UIPropertyMetadata(new Point(double.NaN, double.NaN), OnHorizontalSelectionAreaPropertyChanged));

        /// <summary>
        /// グラフの横軸に対する選択領域を取得または設定します。
        /// </summary>
        public Point HorizontalSelectionArea
        {
            get { return (Point)GetValue(HorizontalSelectionAreaProperty); }
            set { SetValue(HorizontalSelectionAreaProperty, value); }
        }

        /// <summary>
        /// HorizontalSelectionArea プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnHorizontalSelectionAreaPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var graph = d as LineGraph;
            if (graph.IsLoaded)
            {
                graph._shouldUpdate_GridBitmap = true;
                graph.UpdateRendering();
            }
        }

        #endregion HorizontalSelectionArea プロパティ

        #region 初期化

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public LineGraph()
        {
            //this._scale = VisualTreeHelper.GetDpi(this);

            this._zoomRectangleGeometry = new RectangleGeometry();
            this._zoomRectanglePath = new Path() { Data = this._zoomRectangleGeometry, Fill = this._zoomRectangleBrush, Visibility = Visibility.Collapsed, Stretch = Stretch.None };
            this._axisOperationModeText = new TextBlock();

            this._lineGraphMenu = new LinegraphMenu() { DataContext = this };
            this._container = new Grid()
            {
                Background = new SolidColorBrush((Color)this.FindResource("WindowColor")),
                Children =
                {
                    this._gridImage,
                    this._graphImage,
                    this._cursorPointerImage,
                    this._zoomRectanglePath,
                    this._graphTitle,
                    this._xAxisTitle,
                    this._yAxisTitle,
                    this._y2AxisTitle,
                    this._axisOperationModeText,
                },
                Focusable = true,
            };

            #region 軸移動および拡大メニュー

            this._moveXMenu = new MenuItem() { Header = "横軸"           , IsCheckable = true, Command = this.ChangeOperationAxisModeCommand, CommandParameter = OperationAxisModes.MoveX };
            this._moveYMenu = new MenuItem() { Header = "縦軸"           , IsCheckable = true, Command = this.ChangeOperationAxisModeCommand, CommandParameter = OperationAxisModes.MoveY };
            this._moveXYMenu = new MenuItem() { Header = "横軸-縦軸"     , IsCheckable = true, Command = this.ChangeOperationAxisModeCommand, CommandParameter = OperationAxisModes.MoveXY };
            this._moveY2Menu = new MenuItem() { Header = "第 2 主軸"     , IsCheckable = true, Command = this.ChangeOperationAxisModeCommand, CommandParameter = OperationAxisModes.MoveY2 };
            this._moveXY2Menu = new MenuItem() { Header = "横軸-第 2 主軸", IsCheckable = true, Command = this.ChangeOperationAxisModeCommand, CommandParameter = OperationAxisModes.MoveXY2 };
            this._moveMenu = new MenuItem()
            {
                Header = "移動",
                ItemsSource = new MenuItem[]
                {
                    this._moveXMenu,
                    this._moveYMenu,
                    this._moveXYMenu,
                    this._moveY2Menu,
                    this._moveXY2Menu,
                },
            };

            this._zoomXMenu = new MenuItem() { Header = "横軸"           , IsCheckable = true, Command = this.ChangeOperationAxisModeCommand, CommandParameter = OperationAxisModes.ZoomX };
            this._zoomYMenu = new MenuItem() { Header = "縦軸"           , IsCheckable = true, Command = this.ChangeOperationAxisModeCommand, CommandParameter = OperationAxisModes.ZoomY };
            this._zoomXYMenu = new MenuItem() { Header = "横軸-縦軸"     , IsCheckable = true, Command = this.ChangeOperationAxisModeCommand, CommandParameter = OperationAxisModes.ZoomXY };
            this._zoomY2Menu = new MenuItem() { Header = "第 2 主軸"     , IsCheckable = true, Command = this.ChangeOperationAxisModeCommand, CommandParameter = OperationAxisModes.ZoomY2 };
            this._zoomXY2Menu = new MenuItem() { Header = "横軸-第 2 主軸", IsCheckable = true, Command = this.ChangeOperationAxisModeCommand, CommandParameter = OperationAxisModes.ZoomXY2 };
            this._zoomMenu = new MenuItem()
            {
                Header = "拡大",
                ItemsSource = new MenuItem[]
                {
                    this._zoomXMenu,
                    this._zoomYMenu,
                    this._zoomXYMenu,
                    this._zoomY2Menu,
                    this._zoomXY2Menu,
                },
            };

            // コードから操作するために軸操作関連の MenuItem をひとつの配列に集約する
            this._operationAxisMenu = new MenuItem[]
            {
                    this._moveXMenu,
                    this._moveYMenu,
                    this._moveXYMenu,
                    this._moveY2Menu,
                    this._moveXY2Menu,
                    this._zoomXMenu,
                    this._zoomYMenu,
                    this._zoomXYMenu,
                    this._zoomY2Menu,
                    this._zoomXY2Menu,
            };

            #endregion 軸移動および拡大メニュー

            #region 凡例メニュー

            this._legendEnabledMenu = new MenuItem()     { Header = "凡例を表示する",             IsCheckable = true, Command = this.ChangeLegendEnabledCommand, IsChecked = true, StaysOpenOnClick = true };
            this._legendTopLeftMenu = new MenuItem()     { Header = "凡例を左上に表示する",       IsCheckable = true, Command = this.ChangeLegendPositionCommand, CommandParameter = LegendPositions.TopLeft, IsChecked = true };
            this._legendTopRightMenu = new MenuItem()    { Header = "凡例を右上に表示する",       IsCheckable = true, Command = this.ChangeLegendPositionCommand, CommandParameter = LegendPositions.TopRight };
            this._legendBottomRightMenu = new MenuItem() { Header = "凡例を右下に表示する",       IsCheckable = true, Command = this.ChangeLegendPositionCommand, CommandParameter = LegendPositions.BottomRight };
            this._legendBottomLeftMenu = new MenuItem()  { Header = "凡例を左下に表示する",       IsCheckable = true, Command = this.ChangeLegendPositionCommand, CommandParameter = LegendPositions.BottomLeft };
            this._legendArbitraryMenu = new MenuItem()   { Header = "凡例の表示位置を固定しない", IsCheckable = true, Command = this.ChangeLegendPositionCommand, CommandParameter = LegendPositions.Arbitrary };
            this._legendMenu = new MenuItem()
            {
                Header = "凡例",
                // ItemsSource だと Separator が入れられない
                Items =
                {
                    this._legendEnabledMenu,
                    new Separator(),
                    this._legendTopLeftMenu,
                    this._legendTopRightMenu,
                    this._legendBottomRightMenu,
                    this._legendBottomLeftMenu,
                    this._legendArbitraryMenu,
                },
            };

            // コードから操作するために凡例関連の MenuItem をひとつの配列に集約する
            _operationLegendMenu = new MenuItem[]
            {
                    this._legendArbitraryMenu,
                    this._legendTopLeftMenu,
                    this._legendTopRightMenu,
                    this._legendBottomRightMenu,
                    this._legendBottomLeftMenu,
            };

            #endregion 凡例メニュー

            #region グラフカーソルメニュー

            this._cursorEnabledMenu = new MenuItem() { Header = "グラフカーソルを表示する", IsCheckable = true, Command = this.ChangeCursorEnabledCommand, StaysOpenOnClick = true };
            this._xCursor1Menu = new MenuItem() { Header = "横軸 1", IsCheckable = true, Command = this.ChangeCursorCommand, IsChecked = true, StaysOpenOnClick = true };
            this._xCursor2Menu = new MenuItem() { Header = "横軸 2", IsCheckable = true, Command = this.ChangeCursorCommand, IsChecked = true, StaysOpenOnClick = true };
            this._resetXCursorMenu = new MenuItem() { Header = "カーソル位置をリセットする", Command = this.ResetCursorCommand };

            this._cursorMenu = new MenuItem()
            {
                Header = "グラフカーソル",
                Items =
                {
                    this._cursorEnabledMenu,
                    new Separator(),
                    this._xCursor1Menu,
                    this._xCursor2Menu,
                    this._resetXCursorMenu,
                },
            };

            #endregion グラフカーソルメニュー

            this._allAutoScalingMenu = new MenuItem() { Header = "全自動", Command = AutoScalingCommand };
            this._onlyYAxisScalingMenu = new MenuItem() { Header = "縦軸のみ", Command = OnlyYAxisScalingCommand };
            this._yStepFixedAutoScalingMenu = new MenuItem() { Header = "縦軸目盛間隔固定", Command = YStepFixedAutoScalingCommand };
            this._autoScalingMenu = new MenuItem()
            {
                Header = "表示範囲自動設定",
                ItemsSource = new MenuItem[]
                {
                    this._allAutoScalingMenu,
                    this._onlyYAxisScalingMenu,
                    this._yStepFixedAutoScalingMenu,
                },
            };

            this._saveImageMenu = new MenuItem() { Header = "画像保存", Command = WriteBitmapCommand };
            this._configMenu = new MenuItem() { Header = "詳細設定", Command = OpenConfigMenuCommand };

            this._contextMenu = new ContextMenu()
            {
                // ItemsSource だと Separator が入れられない
                Items =
                {
                    this._moveMenu,
                    this._zoomMenu,
                    this._legendMenu,
                    this._cursorMenu,
                    new Separator(),
                    this._autoScalingMenu,
                    this._saveImageMenu,
                    this._configMenu,
                },
            };
            this._container.ContextMenu = this._contextMenu;

            this.Content = this._container;

            for (var i = 0; i < AxisLabelObjectPoolCount; i++)
            {
                this._xAxisLabels[i] = new TextBlock() { TextAlignment = TextAlignment.Center, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
                this._container.Children.Add(this._xAxisLabels[i]);

                this._yAxisLabels[i] = new TextBlock() { TextAlignment = TextAlignment.Right, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
                this._container.Children.Add(this._yAxisLabels[i]);

                this._y2AxisLabels[i] = new TextBlock() { TextAlignment = TextAlignment.Left, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top };
                this._container.Children.Add(this._y2AxisLabels[i]);
            }

            #region グラフカーソル関連

            this._xCursor1Rectangle = new Rectangle() { Fill = new SolidColorBrush(), Width = 2, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Focusable = true, Cursor = Cursors.SizeWE };
            this._xCursor1TextBlock1 = new TextBlock() { Background = new SolidColorBrush(), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, TextAlignment = TextAlignment.Center, Padding = new Thickness(8, 2, 8, 2), Text = "X1" };
            this._xCursor1TextBlock2 = new TextBlock() { Background = new SolidColorBrush(), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, TextAlignment = TextAlignment.Center, Padding = new Thickness(8, 2, 8, 2) };
            this._container.Children.Add(this._xCursor1Rectangle);
            this._container.Children.Add(this._xCursor1TextBlock1);
            this._container.Children.Add(this._xCursor1TextBlock2);

            this._xCursor2Rectangle = new Rectangle() { Fill = new SolidColorBrush(), Width = 2, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Focusable = true, Cursor = Cursors.SizeWE };
            this._xCursor2TextBlock1 = new TextBlock() { Background = new SolidColorBrush(), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, TextAlignment = TextAlignment.Center, Padding = new Thickness(8, 2, 8, 2), Text = "X2" };
            this._xCursor2TextBlock2 = new TextBlock() { Background = new SolidColorBrush(), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, TextAlignment = TextAlignment.Center, Padding = new Thickness(8, 2, 8, 2) };
            this._container.Children.Add(this._xCursor2Rectangle);
            this._container.Children.Add(this._xCursor2TextBlock1);
            this._container.Children.Add(this._xCursor2TextBlock2);

            this._xCursor1Rectangle.MouseLeftButtonDown += OnXCursor1MouseLeftButtonDown;
            this._xCursor1Rectangle.MouseLeftButtonUp += OnXCursor1MouseLeftButtonUp;
            this._xCursor1Rectangle.MouseMove += OnXCursor1MouseMove;
            this._xCursor1Rectangle.KeyDown += OnXCursor1KeyDown;
            this._xCursor1TextBlock1.MouseLeftButtonDown += OnXCursor1MouseLeftButtonDown;
            this._xCursor1TextBlock1.MouseLeftButtonUp += OnXCursor1MouseLeftButtonUp;
            this._xCursor1TextBlock1.MouseMove += OnXCursor1MouseMove;
            this._xCursor1TextBlock1.KeyDown += OnXCursor1KeyDown;
            this._xCursor1TextBlock2.MouseLeftButtonDown += OnXCursor1MouseLeftButtonDown;
            this._xCursor1TextBlock2.MouseLeftButtonUp += OnXCursor1MouseLeftButtonUp;
            this._xCursor1TextBlock2.MouseMove += OnXCursor1MouseMove;
            this._xCursor1TextBlock2.KeyDown += OnXCursor1KeyDown;

            this._xCursor2Rectangle.MouseLeftButtonDown += OnXCursor2MouseLeftButtonDown;
            this._xCursor2Rectangle.MouseLeftButtonUp += OnXCursor2MouseLeftButtonUp;
            this._xCursor2Rectangle.MouseMove += OnXCursor2MouseMove;
            this._xCursor2Rectangle.KeyDown += OnXCursor1KeyDown;
            this._xCursor2TextBlock1.MouseLeftButtonDown += OnXCursor2MouseLeftButtonDown;
            this._xCursor2TextBlock1.MouseLeftButtonUp += OnXCursor2MouseLeftButtonUp;
            this._xCursor2TextBlock1.MouseMove += OnXCursor2MouseMove;
            this._xCursor2TextBlock1.KeyDown += OnXCursor1KeyDown;
            this._xCursor2TextBlock2.MouseLeftButtonDown += OnXCursor2MouseLeftButtonDown;
            this._xCursor2TextBlock2.MouseLeftButtonUp += OnXCursor2MouseLeftButtonUp;
            this._xCursor2TextBlock2.MouseMove += OnXCursor2MouseMove;
            this._xCursor2TextBlock2.KeyDown += OnXCursor1KeyDown;

            #endregion グラフカーソル関連

            #region グラフに対する選択領域関連

            this._horizontalSelectionRectangle = new Rectangle() { Fill = new SolidColorBrush(), Width = 2, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Focusable = true };
            this._horizontalSelectionRectangleLeft = new Rectangle() { Fill = new SolidColorBrush(), Width = 2, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Focusable = true, Cursor = Cursors.SizeWE };
            this._horizontalSelectionRectangleRight = new Rectangle() { Fill = new SolidColorBrush(), Width = 2, HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Focusable = true, Cursor = Cursors.SizeWE };
            this._container.Children.Add(this._horizontalSelectionRectangle);
            this._container.Children.Add(this._horizontalSelectionRectangleLeft);
            this._container.Children.Add(this._horizontalSelectionRectangleRight);

            #endregion グラフに対する選択領域関連

            #region 凡例関連

            this._legendCanvas = new Canvas();
            this._legends = new List<TextBlock>(10);
            this._legendLines = new List<Rectangle>(10);
            for (var i = 0; i < 10; i++)
            {
                var textblock = new TextBlock() { HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Visibility = Visibility.Collapsed };
                this._legends.Add(textblock);
                this._legendCanvas.Children.Add(textblock);

                var rectangle = new Rectangle() { Width = 20, Fill = new SolidColorBrush(), Visibility = Visibility.Collapsed };
                this._legendLines.Add(rectangle);
                this._legendCanvas.Children.Add(rectangle);
            }
            this._legendBorder = new Border() { Child = this._legendCanvas, Padding = new Thickness(4), HorizontalAlignment = HorizontalAlignment.Left, VerticalAlignment = VerticalAlignment.Top, Background = new SolidColorBrush(), BorderBrush = new SolidColorBrush(), BorderThickness = new Thickness(1), Focusable = true };
            this._container.Children.Add(this._legendBorder);
            this._legendBorder.MouseLeftButtonDown += OnLegendBorderMouseLeftButtonDown;
            this._legendBorder.MouseLeftButtonUp += OnLegendBorderMouseLeftButtonUp;
            this._legendBorder.MouseMove += OnLegendBorderMouseMove;
            this._legendBorder.KeyDown += OnLegendBorderKeyDown;

            #endregion 凡例関連

            this.Y2AxisSettings.GridLabelVisibility = Visibility.Collapsed;

            // デザインモードのときは実行しない
            if (DesignerProperties.GetIsInDesignMode(this) == false)
            {
                // 再描画コマンド受付
                LineGraphTrigger.Instance.DrawCommandReceived += OnDrawCommandReceived;

                this.Loaded += OnLoaded;
                this.SizeChanged += OnSizeChanged;
                this._container.MouseLeftButtonDown += OnMouseLeftButtonDown;
                this._container.MouseLeftButtonUp += OnMouseLeftButtonUp;
                this._container.MouseMove += OnMouseMove;
                this._container.KeyDown += OnContainerKeyDown;

                YKToolkit.Controls.ThemeManager.Instance.ThemeChanged += OnThemeChanged;
            }
        }

        /// <summary>
        /// Loaded イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= OnLoaded;

            // デザインモードのときは実行しない
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            var w = Window.GetWindow(this);
            w.Closed += (_, __) => this._lineGraphMenu.Close();
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
        /// テーマ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnThemeChanged(object sender, EventArgs e)
        {
            if (this._gridBitmap == null)
                return;

            var brush = new SolidColorBrush((Color)this.FindResource("ForegroundColor"));
            if (brush.CanFreeze) brush.Freeze();
            TextBlock.SetForeground(this, brush);
            (this._container.Background as SolidColorBrush).Color = (Color)this.FindResource("WindowColor");

            // ここは時間かかってもいいから初期化してしまえ
            InitWriteableBitmap();
            //this._shouldUpdate_GraphBitmap = true;
            //this._shouldUpdate_GridBitmap = true;
            //UpdateRendering();
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
            this._shouldUpdate_GridBitmap = true;

            // グラフタイトルと軸ラベル
            this._xAxisTitle.Margin = new Thickness(this.GraphAreaMargin.Left, 0, 0, 0);
            this._yAxisTitle.Margin = new Thickness(0, 0, 0, this.GraphAreaMargin.Bottom);
            this._y2AxisTitle.Margin = new Thickness(0, 0, 0, this.GraphAreaMargin.Bottom);

            // 軸操作モード用テキスト
            this._axisOperationModeText.Margin = this.GraphAreaMargin;

            // グラフデータ
            //this._graphBitmap = (width == 0) || (height == 0) ? null : new WriteableBitmap((int)(this._graphArea.Width * this._scale.DpiScaleX), (int)(this._graphArea.Height * this._scale.DpiScaleY), this._scale.PixelsPerInchX, this._scale.PixelsPerInchY, PixelFormats.Pbgra32, null);
            this._graphBitmap = (width == 0) || (height == 0) ? null : new WriteableBitmap((int)this._graphArea.Width, (int)this._graphArea.Height, 96, 96, PixelFormats.Pbgra32, null);
            this._graphImage.Source = this._graphBitmap;
            this._graphImage.Margin = this.GraphAreaMargin;
            this._shouldUpdate_GraphBitmap = true;

            // グラフカーソルによるポインタ
            this._cursorPointerBitmap = (width == 0) || (height == 0) ? null : new WriteableBitmap((int)this._graphArea.Width, (int)this._graphArea.Height, 96, 96, PixelFormats.Pbgra32, null);
            this._cursorPointerImage.Source = this._cursorPointerBitmap;
            this._cursorPointerImage.Margin = this.GraphAreaMargin;

            UpdateRendering();
        }

        #endregion 初期化

        private void UpdateRendering()
        {
            // デザインモードのときは実行しない
            if (DesignerProperties.GetIsInDesignMode(this))
                return;

            // 座標変換のための設定を更新する
            //this._graphBitmap.Lock();

            // グラフ描画エリアの枠線と目盛線
            if (this._shouldUpdate_GridBitmap)
            {
                UpdateRendering_GridBitmap();
                UpdateRendering_HorizontalSelectionArea();
                this._shouldUpdate_GridBitmap = false;
            }

            // グラフデータ
            if (this._shouldUpdate_GraphBitmap)
            {
                UpdateRendering_GraphBitmap();
                UpdateRendering_Cursor();
                UpdateRendering_Legend();
                this._shouldUpdate_GraphBitmap = false;
            }

            //this._graphBitmap.AddDirtyRect(new Int32Rect(0, 0, this._graphBitmap.PixelWidth, this._graphBitmap.PixelHeight));
            //this._graphBitmap.Unlock();
        }

        /// <summary>
        /// LineGraphTrigger.DrawCommandReceived イベントハンドラ
        /// </summary>
        /// <param name="key">イベント引数</param>
        private void OnDrawCommandReceived(string key)
        {
            if (key == this.DrawKey)
            {
#if BETA
                InitWriteableBitmap();
#else
                this._shouldUpdate_GridBitmap = true;
                this._shouldUpdate_GraphBitmap = true;
                UpdateRendering();
#endif
            }
        }

        /// <summary>
        /// リソースを破棄します。
        /// </summary>
        public void Dispose()
        {
            this._lineGraphMenu.Close();
        }

        #region 座標変換ヘルパ

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
        /// <param name="isY2">第２種軸データの場合に true を指定します。</param>
        /// <returns>変換後の値を返します。</returns>
        private int YAxisToScreen(double value, bool isY2 = false)
        {
            if (isY2)
                return (int)(this._graphArea.Height - (value - this.Y2AxisSettings.Minimum) * this._graphArea.Height / this.Y2AxisSettings.Range);
            else
                return (int)(this._graphArea.Height - (value - this.YAxisSettings.Minimum) * this._graphArea.Height / this.YAxisSettings.Range);
        }

        /// <summary>
        /// スクリーン座標における位置を横軸データに変換します。
        /// </summary>
        /// <param name="value">スクリーン座標を指定します。</param>
        /// <returns>変換後の値を返します。</returns>
        private double ScreenToXAxis(double value)
        {
            return value * this.XAxisSettings.Range / this._graphArea.Width + this.XAxisSettings.Minimum;
        }

        /// <summary>
        /// スクリーン座標における位置を縦軸データに変換します。
        /// </summary>
        /// <param name="value">スクリーン座標を指定します。</param>
        /// <returns>変換後の値を返します。</returns>
        private double ScreenToYAxis(double value)
        {
            return this.YAxisSettings.Maximum - value * this.YAxisSettings.Range / this._graphArea.Height;
        }

        /// <summary>
        /// スクリーン座標における位置を第 2 主軸データに変換します。
        /// </summary>
        /// <param name="value">スクリーン座標を指定します。</param>
        /// <returns>変換後の値を返します。</returns>
        private double ScreenToY2Axis(double value)
        {
            return this.Y2AxisSettings.Maximum - value * this.Y2AxisSettings.Range / this._graphArea.Height;
        }

        /// <summary>
        /// スクリーン座標における差分を横軸データの差分に変換します。
        /// </summary>
        /// <param name="distance">スクリーン座標における差分を指定します。</param>
        /// <returns>変換後の値を返します。</returns>
        private double ScreenDistanceToXAxisDistance(double distance)
        {
            return (double)distance / this._graphArea.Width * this.XAxisSettings.Range;
        }

        /// <summary>
        /// スクリーン座標における差分を縦軸データの差分に変換します。
        /// </summary>
        /// <param name="distance">スクリーン座標における差分を指定します。</param>
        /// <returns>変換後の値を返します。</returns>
        private double ScreenDistanceToYAxisDistance(double distance)
        {
            return (double)distance / this._graphArea.Height * this.YAxisSettings.Range;
        }

        /// <summary>
        /// スクリーン座標における差分を第 2 主軸データの差分に変換します。
        /// </summary>
        /// <param name="distance">スクリーン座標における差分を指定します。</param>
        /// <returns>変換後の値を返します。</returns>
        private double ScreenDistanceToY2AxisDistance(double distance)
        {
            return (double)distance / this._graphArea.Height * this.Y2AxisSettings.Range;
        }

        #endregion 座標変換ヘルパ

        #region グラフ描画エリアの枠線と目盛線

        /// <summary>
        /// グラフ描画エリアの枠線と目盛線用の WriteableBitmap を入れるためのコントロール
        /// </summary>
        private Image _gridImage = new Image() { Stretch = Stretch.None };

        /// <summary>
        /// グラフ描画エリアの枠線と目盛線用の WriteableBitmap オブジェクト
        /// </summary>
        private WriteableBitmap _gridBitmap;

        /// <summary>
        /// グラフ描画エリアの枠線と目盛線を再描画すべきかどうか
        /// </summary>
        private bool _shouldUpdate_GridBitmap;

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
        /// 第 2 主軸タイトル用
        /// </summary>
        private TextBlock _y2AxisTitle = new TextBlock() { HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Center, LayoutTransform = new RotateTransform(90) };

        /// <summary>
        /// 横軸ラベル用オブジェクトプール
        /// </summary>
        private TextBlock[] _xAxisLabels = new TextBlock[AxisLabelObjectPoolCount];

        /// <summary>
        /// 縦軸ラベル用オブジェクトプール
        /// </summary>
        private TextBlock[] _yAxisLabels = new TextBlock[AxisLabelObjectPoolCount];

        /// <summary>
        /// 第 2 主軸ラベル用オブジェクトプール
        /// </summary>
        private TextBlock[] _y2AxisLabels = new TextBlock[AxisLabelObjectPoolCount];

        /// <summary>
        /// 軸ラベルオブジェクトプールのバッファ数
        /// </summary>
        private const int AxisLabelObjectPoolCount = 30;

        /// <summary>
        /// グラフ描画エリアの枠線と目盛線の描画をおこないます。
        /// </summary>
        private void UpdateRendering_GridBitmap()
        {
#if DEBUG2
            this._count_GridBitmap++;
            System.Diagnostics.Debug.WriteLine(string.Format("[{0}] UpdateRendering_GridBitmap()", this._count_GridBitmap));
#endif

            if (this._gridImage == null)
                return;
            if (this._gridBitmap == null)
                return;

            // グラフタイトル、軸タイトル、軸ラベルのテキストすべてをここで確定する

            this._graphTitle.Text = this.GraphTitle;
            this._xAxisTitle.Text = this.XAxisSettings.Title;
            this._yAxisTitle.Text = this.YAxisSettings.Title;
            this._y2AxisTitle.Text = this.Y2AxisSettings.Title;

            this._graphTitle.FontSize = this.GraphTitleFontSize;
            this._xAxisTitle.FontSize = this.XAxisSettings.TitleFontSize;
            this._yAxisTitle.FontSize = this.YAxisSettings.TitleFontSize;
            this._y2AxisTitle.FontSize = this.Y2AxisSettings.TitleFontSize;

            var ptGrid = new Point(this.XAxisSettings.Minimum, this.YAxisSettings.Minimum);
            var pt2Grid = new Point(this.XAxisSettings.Minimum, this.Y2AxisSettings.Minimum);
            var xAxisLavelMaxHeight = 0.0;
            var yAxisLavelMaxWidth = 0.0;
            var y2AxisLavelMaxWidth = 0.0;
            double temp;
            for (var i = 0; i < AxisLabelObjectPoolCount; i++)
            {
                ptGrid.X = this.XAxisSettings.Minimum + i * this.XAxisSettings.MajorStep;
                ptGrid.Y = this.YAxisSettings.Minimum + i * this.YAxisSettings.MajorStep;
                pt2Grid.Y = this.Y2AxisSettings.Minimum + i * this.Y2AxisSettings.MajorStep;
                this._xAxisLabels[i].FontSize = this.XAxisSettings.FontSize;
                this._yAxisLabels[i].FontSize = this.YAxisSettings.FontSize;
                this._y2AxisLabels[i].FontSize = this.Y2AxisSettings.FontSize;
                this._xAxisLabels[i].Text = ptGrid.X.ToString(this.XAxisSettings.StringFormat);
                this._yAxisLabels[i].Text = ptGrid.Y.ToString(this.YAxisSettings.StringFormat);
                this._y2AxisLabels[i].Text = pt2Grid.Y.ToString(this.Y2AxisSettings.StringFormat);
                this._xAxisLabels[i].Arrange(new Rect(new Size(this.ActualWidth, this.ActualHeight)));
                this._yAxisLabels[i].Arrange(new Rect(new Size(this.ActualWidth, this.ActualHeight)));
                this._y2AxisLabels[i].Arrange(new Rect(new Size(this.ActualWidth, this.ActualHeight)));

                // ここで各軸ラベルの視認性を設定します。
                // 配置は後でします。

                // 横軸ラベルの視認性設定
                temp = SetXAxisLabelVisibility(this._xAxisLabels[i], ptGrid.X);
                if (xAxisLavelMaxHeight < temp) xAxisLavelMaxHeight = temp;

                // 縦軸ラベルの視認性設定
                // 配置は後でします。
                temp = SetYAxisLabelVisibility(this._yAxisLabels[i], ptGrid.Y);
                if (yAxisLavelMaxWidth < temp) yAxisLavelMaxWidth = temp;

                // 第 2 主軸ラベルの視認性設定
                // 配置は後でします。
                temp = SetY2AxisLabelVisibility(this._y2AxisLabels[i], pt2Grid.Y);
                if (y2AxisLavelMaxWidth < temp) y2AxisLavelMaxWidth = temp;
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

            // 第 2 主軸目盛補助線
            ptGrid.Y = this.Y2AxisSettings.Minimum + this.Y2AxisSettings.MinorStep;
            if (this.Y2AxisSettings.IsMajorGridEnabled && this.Y2AxisSettings.IsMinorGridEnabled)
            {
                ptGrid.Y = this.Y2AxisSettings.Minimum + this.Y2AxisSettings.MinorStep;
                while (ptGrid.Y < this.Y2AxisSettings.Maximum)
                {
                    ptGridScreen.Y = YAxisToScreen(ptGrid.Y, true);
                    this._gridBitmap.DrawLineAa(0, (int)ptGridScreen.Y, (int)this._graphArea.Width, (int)ptGridScreen.Y, this.MinorGridColor, (int)this.Y2AxisSettings.MinorGridThickness);
                    ptGrid.Y += this.Y2AxisSettings.MinorStep;
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

            // 第 2 主軸目盛線
            if (this.Y2AxisSettings.IsMajorGridEnabled && (this.Y2AxisSettings.MajorStep > 0))
            {
                var labelIndex = 0;
                for (var i = 0; i < AxisLabelObjectPoolCount; i++)
                {
                    ptGrid.Y = this.Y2AxisSettings.Minimum + i * this.Y2AxisSettings.MajorStep;
                    if (ptGrid.Y > this.Y2AxisSettings.Maximum)
                        break;

                    ptGridScreen.Y = YAxisToScreen(ptGrid.Y, true);

                    // 目盛線
                    if ((this.Y2AxisSettings.Minimum < ptGrid.Y) && (ptGrid.Y < this.Y2AxisSettings.Maximum))
                    {
                        this._gridBitmap.DrawLineAa(0, (int)ptGridScreen.Y, (int)this._graphArea.Width, (int)ptGridScreen.Y, this.MajorGridColor, (int)this.Y2AxisSettings.MajorGridThickness);
                    }

                    // 軸ラベル
                    this._y2AxisLabels[labelIndex].Margin = new Thickness(this._graphArea.Right + 4, this._graphArea.Top + ptGridScreen.Y - this._y2AxisLabels[labelIndex].ActualHeight / 2.0, 0, 0);
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

        private double SetY2AxisLabelVisibility(TextBlock textBlock, double value)
        {
            var width = 0.0;
            if (this.Y2AxisSettings.IsMajorGridEnabled)
            {
                if ((this.Y2AxisSettings.GridLabelVisibility != Visibility.Collapsed) && (value <= this.Y2AxisSettings.Maximum))
                {
                    textBlock.Visibility = this.Y2AxisSettings.GridLabelVisibility;
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

        #endregion グラフ描画エリアの枠線と目盛線

        #region グラフに対する選択領域

        private Rectangle _horizontalSelectionRectangle;
        private Rectangle _horizontalSelectionRectangleLeft;
        private Rectangle _horizontalSelectionRectangleRight;

        /// <summary>
        /// グラフの横軸に対する選択領域の描画をおこないます。
        /// </summary>
        private void UpdateRendering_HorizontalSelectionArea()
        {
            this._horizontalSelectionRectangle.Visibility = Visibility.Collapsed;
            this._horizontalSelectionRectangleLeft.Visibility = Visibility.Collapsed;
            this._horizontalSelectionRectangleRight.Visibility = Visibility.Collapsed;

            var pt = this.HorizontalSelectionArea;
            if (double.IsNaN(pt.X) || double.IsNaN(pt.Y))
                return;

            var color = (Color)this.FindResource("WindowBorderColor");
            color.A = 0x80;
            (this._horizontalSelectionRectangle.Fill as SolidColorBrush).Color = color;
            color.A = 0xff;
            (this._horizontalSelectionRectangleLeft.Fill as SolidColorBrush).Color = color;
            (this._horizontalSelectionRectangleRight.Fill as SolidColorBrush).Color = color;

            var isLeftEnabled = (this.XAxisSettings.Minimum <= pt.X) && (pt.X <= this.XAxisSettings.Maximum);
            var isRightEnabled = (this.XAxisSettings.Minimum <= pt.Y) && (pt.Y <= this.XAxisSettings.Maximum);
            var topLeft = new Point(this._graphArea.Left + (isLeftEnabled ? XAxisToScreen(pt.X) : 0), this._graphArea.Top + this.GraphAreaBorderThickness.Top);
            var bottomRight = new Point(isRightEnabled ? this._graphArea.Left + XAxisToScreen(pt.Y) : this._graphArea.Right, this._graphArea.Bottom - this.GraphAreaBorderThickness.Bottom);
            var height = this._graphArea.Height - this.GraphAreaBorderThickness.Top - this.GraphAreaBorderThickness.Bottom;
            if (height < 0) height = 0;

            if (isLeftEnabled || isRightEnabled)
            {
                this._horizontalSelectionRectangle.Width = bottomRight.X - topLeft.X;
                this._horizontalSelectionRectangle.Height = height;
                this._horizontalSelectionRectangle.Margin = new Thickness(topLeft.X, topLeft.Y, 0, 0);
                this._horizontalSelectionRectangle.Visibility = Visibility.Visible;
            }

            if (isLeftEnabled)
            {
                this._horizontalSelectionRectangleLeft.Height = height;
                this._horizontalSelectionRectangleLeft.Margin = new Thickness(topLeft.X, topLeft.Y, 0, 0);
                this._horizontalSelectionRectangleLeft.Visibility = Visibility.Visible;
            }

            if (isRightEnabled)
            {
                this._horizontalSelectionRectangleRight.Height = height;
                this._horizontalSelectionRectangleRight.Margin = new Thickness(bottomRight.X, topLeft.Y, 0, 0);
                this._horizontalSelectionRectangleRight.Visibility = Visibility.Visible;
            }
        }

        #endregion グラフに対する選択領域

        #region グラフデータ

        /// <summary>
        /// グラフデータ用の WriteableBitmap を入れるためのコントロール
        /// </summary>
        private Image _graphImage = new Image() { Stretch = Stretch.None };

        /// <summary>
        /// グラフデータ用の WriteableBitmap オブジェクト
        /// </summary>
        private WriteableBitmap _graphBitmap;

        /// <summary>
        /// グラフデータを再描画すべきかどうか
        /// </summary>
        private bool _shouldUpdate_GraphBitmap;

        /// <summary>
        /// グラフデータの描画をおこないます。
        /// </summary>
        private void UpdateRendering_GraphBitmap()
        {
#if DEBUG2
            this._count_GraphBitmap++;
            System.Diagnostics.Debug.WriteLine(string.Format("[{0}] UpdateRendering_GraphBitmap()", this._count_GraphBitmap));
#endif

            if (this._graphImage == null)
                return;
            if (this._graphBitmap == null)
                return;

            // グラフ描画エリアをクリアする
            this._graphBitmap.Clear(Colors.Transparent);

            if (this.GraphDataCollection == null)
                return;

            var index = -1;
            Color color;
            foreach (var graphData in this.GraphDataCollection)
            {
                index++;

                if (graphData.Visibility != Visibility.Visible) continue;
                if (graphData.DataContext == null) graphData.DataContext = this.DataContext;
                if ((graphData.XData == null) || (graphData.YData == null))
                    continue;
                var length = Math.Min(graphData.XData.Length, graphData.YData.Length);
                if (length == 0)
                    continue;
                color = graphData.AutoStroke ? GetColor(index) : graphData.Stroke;

                var xMin = this.XAxisSettings.Minimum;
                var xMax = this.XAxisSettings.Maximum;
                var yMin = graphData.IsY2 ? this.Y2AxisSettings.Minimum : this.YAxisSettings.Minimum;
                var yMax = graphData.IsY2 ? this.Y2AxisSettings.Maximum : this.YAxisSettings.Maximum;
                var pt0 = new Point();
                var ptLine0 = new Point();
                var ptLine1 = new Point();
                var isPtLine0 = false;
                var isPtLine1 = false;
                var isFirstPoint = true;        // 最初のデータ点を描画したら落とす
                var endIndex = 0;
                //var isLineFirst = true;
                int i;
                for (i = 0; i < length; i++)
                {
                    #region 線の描画
                    if (!isFirstPoint)
                    {

                        if ((pt0.Y > yMax) && (graphData.YData[i] > yMax))
                        {
                            // 絶対に線の描画があり得ないパターン
                        }
                        else if ((pt0.Y < yMin) && (graphData.YData[i] < yMin))
                        {
                            // 絶対に線の描画があり得ないパターン
                        }
                        else
                        {
                            // 以前の点が右端より左側にあって
                            // 今回の点が左端より右側にある場合のみ
                            // 線を描画する可能性がある
                            if ((pt0.X < xMax) && (graphData.XData[i] > xMin))
                            {
                                // 以前の点が範囲内ならこれを左端の点とする
                                if ((xMin <= pt0.X) && (pt0.X <= xMax) && (yMin <= pt0.Y) && (pt0.Y <= yMax))
                                {
                                    ptLine0.X = XAxisToScreen(pt0.X);
                                    ptLine0.Y = YAxisToScreen(pt0.Y, graphData.IsY2);
                                    isPtLine0 = true;
                                }
                                // 今回の点が範囲内ならこれを右端の点とする
                                if ((xMin <= graphData.XData[i]) && (graphData.XData[i] <= xMax) && (yMin <= graphData.YData[i]) && (graphData.YData[i] <= yMax))
                                {
                                    ptLine1.X = XAxisToScreen(graphData.XData[i]);
                                    ptLine1.Y = YAxisToScreen(graphData.YData[i], graphData.IsY2);
                                    isPtLine1 = true;
                                }

                                // 左端または右端の点が確定していない場合はグラフ表示範囲の境界線との交点を調べる
                                if (!isPtLine0 || !isPtLine1)
                                {
                                    if (graphData.XData[i] != pt0.X)
                                    {
                                        // y = ax + b
                                        // 傾き
                                        double a = (graphData.YData[i] - pt0.Y) / (graphData.XData[i] - pt0.X);
                                        // 切片
                                        double b = pt0.Y - a * pt0.X;

                                        // 左端縦軸との交点
                                        var yLeft = a * xMin + b;
                                        // 右端縦軸との交点
                                        var yRight = a * xMax + b;
                                        // 上端横軸との交点
                                        var xTop = (yMax - b) / a;
                                        // 下端横軸との交点
                                        var xBottom = (yMin - b) / a;

                                        #region 左端の点を確定する
                                        if (!isPtLine0)
                                        {
                                            // 左端縦軸交点の確認
                                            if ((yMin <= yLeft) && (yLeft <= yMax))
                                            {
                                                ptLine0.X = XAxisToScreen(xMin);
                                                ptLine0.Y = YAxisToScreen(yLeft, graphData.IsY2);
                                                isPtLine0 = true;
                                            }
                                            else if (yLeft < yMin)
                                            {
                                                // 交わり得るのは下端横軸交点
                                                if ((xMin <= xBottom) && (xBottom <= xMax))
                                                {
                                                    ptLine0.X = XAxisToScreen(xBottom);
                                                    ptLine0.Y = YAxisToScreen(yMin, graphData.IsY2);
                                                    isPtLine0 = true;
                                                }
                                            }
                                            else // if (yLeft > yMax)
                                            {
                                                // 交わり得るのは上端横軸交点
                                                if ((xMin <= xTop) && (xTop <= xMax))
                                                {
                                                    ptLine0.X = XAxisToScreen(xTop);
                                                    ptLine0.Y = YAxisToScreen(yMax, graphData.IsY2);
                                                    isPtLine0 = true;
                                                }
                                            }

                                            //if (isPtLine0)
                                            //{
                                            //    pathfigure.Segments.Add(new LineSegment(ptLine0, false));
                                            //}
                                        }
                                        #endregion 左端の点を確定する

                                        #region 右端の点を確定する
                                        if (!isPtLine1)
                                        {
                                            // 右端縦軸交点の確認
                                            if ((yMin <= yRight) && (yRight <= yMax))
                                            {
                                                ptLine1.X = XAxisToScreen(xMax);
                                                ptLine1.Y = YAxisToScreen(yRight, graphData.IsY2);
                                                isPtLine1 = true;
                                            }
                                            else
                                            {
                                                // 下から上への線の場合
                                                if (pt0.Y < graphData.YData[i])
                                                {
                                                    // 交わり得るのは上端横軸交点
                                                    if ((xMin <= xTop) && (xTop <= xMax))
                                                    {
                                                        ptLine1.X = XAxisToScreen(xTop);
                                                        ptLine1.Y = YAxisToScreen(yMax, graphData.IsY2);
                                                        isPtLine1 = true;
                                                    }
                                                }
                                                else
                                                {
                                                    // 上から下への線の場合は
                                                    // 交わり得るのは下端横軸交点
                                                    if ((xMin <= xBottom) && (xBottom <= xMax))
                                                    {
                                                        ptLine1.X = XAxisToScreen(xBottom);
                                                        ptLine1.Y = YAxisToScreen(yMin, graphData.IsY2);
                                                        isPtLine1 = true;
                                                    }
                                                }
                                            }
                                        }
                                        #endregion 右端の点を確定する
                                    }
                                }

                                // 線の開始点を確定する
                                //if (isLineFirst && isPtLine0)
                                //{
                                //    pathfigure.StartPoint = ptLine0;
                                //    isLineFirst = false;
                                //}

                                // 右端の点が確定したら線を結ぶ
                                if (isPtLine0 && isPtLine1)
                                {
                                    //pathfigure.Segments.Add(new LineSegment(ptLine1, true));
                                    this._graphBitmap.DrawLineAa((int)ptLine0.X, (int)ptLine0.Y, (int)ptLine1.X, (int)ptLine1.Y, color, graphData.StrokeThickness);
                                }
                            }
                        }
                        isPtLine0 = false;
                        isPtLine1 = false;
                    }
                    #endregion 線の描画

                    if (graphData.IsMarkerEnabled)
                    {
                        #region データ点の描画
                        if (!isFirstPoint)
                        {
                            // 線の上にデータ点を描画するために
                            // ひとつ前のデータ点を描画する
                            this._graphBitmap.FillEllipseCentered(XAxisToScreen(pt0.X), YAxisToScreen(pt0.Y, graphData.IsY2), 3, 3, color);
                        }
                        if (i == length - 1)
                        {
                            // ひとつ前のデータ点を描画していたので
                            // 最後のデータ点をここで描画する
                            this._graphBitmap.FillEllipseCentered(XAxisToScreen(graphData.XData[i]), YAxisToScreen(graphData.YData[i], graphData.IsY2), 3, 3, color);
                        }
                        #endregion データ点の描画
                    }

                    // 以前の点として保持
                    pt0.X = graphData.XData[i];
                    pt0.Y = graphData.YData[i];

                    endIndex = i;
                    if (graphData.XData[i] > this.XAxisSettings.Maximum + this.XAxisSettings.MajorStep)
                    {
                        break;
                    }
                    if (isFirstPoint)
                    {
                        if (graphData.XData[i] >= this.XAxisSettings.Minimum - this.XAxisSettings.MajorStep)
                        {
                            isFirstPoint = false;
                            graphData.StartIndex = i;
                        }
                    }
                }
                graphData.EndIndex = endIndex;
                // graphData.StartIndex/EndIndex はグラフカーソル表示のために使用しています
            }   // end of foreach (var graphData in this.GraphDataCollection)
        }

        private Color GetColor(int index)
        {
            switch (index % 4)
            {
                default:
                case 0: return Colors.Orange;
                case 1: return Colors.SkyBlue;
                case 2: return Colors.Crimson;
                case 3: return Colors.MediumSeaGreen;
            }
        }

        #endregion グラフデータ

        #region 凡例

        /// <summary>
        /// 凡例用コンテナ
        /// </summary>
        private Border _legendBorder;

        /// <summary>
        /// 凡例描画用キャンバス
        /// </summary>
        private Canvas _legendCanvas;

        /// <summary>
        /// 凡例用のテキストオブジェクトプール
        /// </summary>
        private List<TextBlock> _legends;

        /// <summary>
        /// 凡例用の線オブジェクトプール
        /// </summary>
        private List<Rectangle> _legendLines;

        /// <summary>
        /// 凡例移動開始位置
        /// </summary>
        private Point? _legendMovePoint;

        /// <summary>
        /// 凡例の左上位置
        /// </summary>
        private Thickness _legendPosition;

        /// <summary>
        /// 凡例の描画をおこないます。
        /// </summary>
        private void UpdateRendering_Legend()
        {
            if (!this.IsLegendEnabled || !this.GraphDataCollection.Any(x => x.Visibility == Visibility.Visible))
            {
                this._legendBorder.Visibility = Visibility.Collapsed;
                return;
            }
            this._legendBorder.Visibility = Visibility.Visible;

            (this._legendBorder.Background as SolidColorBrush).Color = (Color)this.FindResource("BoxBaseColor");
            (this._legendBorder.BorderBrush as SolidColorBrush).Color = (Color)this.FindResource("WindowBorderColor");
            for (var i = 0; i < this._legends.Count; i++)
            {
                this._legends[i].Visibility = Visibility.Collapsed;
                this._legendLines[i].Visibility = Visibility.Collapsed;
            }

            // 全体のサイズを測定して this._legendCanvas のサイズを確定させてから配置する

            // サイズ計測
            var margin = 2.0;
            var height = margin;
            var width = 0.0;
            var graphDataCollection = this.GraphDataCollection.ToArray();
            for (var i = 0; i < graphDataCollection.Length; i++)
            {
                if (graphDataCollection[i].Visibility == Visibility.Collapsed)
                    continue;

                if (i >= this._legends.Count)
                {
                    this._legends.Add(new TextBlock());
                    this._legendLines.Add(new Rectangle() { Width = 20, Fill = new SolidColorBrush() });
                }

                this._legends[i].Visibility = graphDataCollection[i].Visibility;
                this._legendLines[i].Visibility = graphDataCollection[i].Visibility;
                if (graphDataCollection[i].Visibility == Visibility.Visible)
                {
                    this._legends[i].Text = graphDataCollection[i].Legend;
                    if (this.IsCursorEnabled && (graphDataCollection[i].XData.Length > 0) && (graphDataCollection[i].YData.Length > 0))
                    {
                        Point? x = null;
                        Point? y = null;
                        Vector? z = null;
                        if (this.GraphCursor.HasFlag(GraphCursors.XCursor2)) x = CalcCursorPosition(graphDataCollection[i], this._xCursor2);
                        if (this.GraphCursor.HasFlag(GraphCursors.XCursor1)) y = CalcCursorPosition(graphDataCollection[i], this._xCursor1);
                        if ((x != null) && (y != null)) z = x.Value - y.Value;

                        if ((x != null) || (y != null))
                        {
                            this._legends[i].Text += string.Concat(new string[]
                            {
                                x == null ? "" : " (" + x.Value.X.ToString(this.XAxisSettings.StringFormat) + ", " + (graphDataCollection[i].IsY2 ? x.Value.Y.ToString(this.Y2AxisSettings.StringFormat) : x.Value.Y.ToString(this.YAxisSettings.StringFormat)) + ")",
                                z == null ? "" : " -",
                                y == null ? "" : " (" + y.Value.X.ToString(this.XAxisSettings.StringFormat) + ", " + (graphDataCollection[i].IsY2 ? y.Value.Y.ToString(this.Y2AxisSettings.StringFormat) : y.Value.Y.ToString(this.YAxisSettings.StringFormat)) + ")",
                                z == null ? "" : " = (" + z.Value.X.ToString(this.XAxisSettings.StringFormat) + ", " + (graphDataCollection[i].IsY2 ? y.Value.Y.ToString(this.Y2AxisSettings.StringFormat) : z.Value.Y.ToString(this.YAxisSettings.StringFormat)) + ")",
                            });
                        }
                    }
                    (this._legendLines[i].Fill as SolidColorBrush).Color = graphDataCollection[i].AutoStroke ? GetColor(i) : graphDataCollection[i].Stroke;
                    this._legendLines[i].Height = graphDataCollection[i].StrokeThickness;
                }
                else
                {
                    this._legends[i].Text = "";
                    (this._legendLines[i].Fill as SolidColorBrush).Color = Colors.Transparent;
                }
                this._legends[i].Arrange(new Rect(new Size(this.ActualWidth, this.ActualHeight)));
                height += this._legends[i].ActualHeight + margin;
                if (width < this._legends[i].ActualWidth) width = this._legends[i].ActualWidth;
            }
            width += 20 + 3 * margin;

            // サイズの確定
            this._legendCanvas.Width = width;
            this._legendCanvas.Height = height;

            // 配置
            height = 0.0;
            for (var i = 0; i < graphDataCollection.Length; i++)
            {
                Canvas.SetLeft(this._legends[i], margin + this._legendLines[i].Width + margin);
                Canvas.SetTop(this._legends[i], height + margin);

                Canvas.SetLeft(this._legendLines[i], margin);
                Canvas.SetTop(this._legendLines[i], height + margin + this._legends[i].ActualHeight / 2.0 - this._legendLines[i].Height / 2.0);

                height += this._legends[i].ActualHeight;
            }

            UpdateLegendPosition();
        }

        /// <summary>
        /// カーソル位置からグラフデータの座標を算出します。
        /// </summary>
        /// <param name="graphData">グラフデータを指定します。</param>
        /// <param name="cursor">カーソル位置を指定します。</param>
        /// <returns>算出された座標を返します。</returns>
        private Point CalcCursorPosition(LineGraphData graphData, double cursor)
        {
            var index = 0;
            var x = (double)int.MaxValue;
            var y = 0.0;
            for (var i = graphData.StartIndex; i <= graphData.EndIndex; i++)
            {
                y = Math.Abs(cursor - graphData.XData[i]);
                if (x > y)
                {
                    index = i;
                    x = y;
                }
            }
            if (graphData.YData.Length <= index) index = graphData.YData.Length - 1;
            return new Point(graphData.XData[index], graphData.YData[index]);
        }

        /// <summary>
        /// 凡例の表示位置を更新します。
        /// </summary>
        private void UpdateLegendPosition()
        {
            switch (this.LegendPosition)
            {
                case LegendPositions.TopLeft:
                    this._legendBorder.Margin = new Thickness(this._graphArea.Left + this.LegendMargin.Left, this._graphArea.Top + this.LegendMargin.Top, 0, 0);
                    break;

                case LegendPositions.TopRight:
                    this._legendBorder.Margin = new Thickness(this._graphArea.Right - this._legendBorder.ActualWidth - this.LegendMargin.Right, this._graphArea.Top + this.LegendMargin.Top, 0, 0);
                    break;

                case LegendPositions.BottomRight:
                    this._legendBorder.Margin = new Thickness(this._graphArea.Right - this._legendBorder.ActualWidth - this.LegendMargin.Right, this._graphArea.Bottom - this._legendBorder.ActualHeight - this.LegendMargin.Bottom, 0, 0);
                    break;

                case LegendPositions.BottomLeft:
                    this._legendBorder.Margin = new Thickness(this._graphArea.Left + this.LegendMargin.Left, this._graphArea.Bottom - this._legendBorder.ActualHeight - this.LegendMargin.Bottom, 0, 0);
                    break;

                case LegendPositions.Arbitrary:
                default:
                    break;
            }
        }

        private DelegateCommand _changeLegendEnabledCommand;
        /// <summary>
        /// 凡例表示切替コマンドを取得します。
        /// </summary>
        public DelegateCommand ChangeLegendEnabledCommand
        {
            get
            {
                return this._changeLegendEnabledCommand ?? (this._changeLegendEnabledCommand = new DelegateCommand(_ =>
                {
                    this.IsLegendEnabled = this._legendEnabledMenu.IsChecked;
                }));
            }
        }

        private DelegateCommand _changeLegendPositionCommand;
        /// <summary>
        /// 凡例表示位置切替コマンドを取得します。
        /// </summary>
        public DelegateCommand ChangeLegendPositionCommand
        {
            get
            {
                return this._changeLegendPositionCommand ?? (this._changeLegendPositionCommand = new DelegateCommand(
                p =>
                {
                    UpdateLegendPosition((LegendPositions)p);
                },
                _ => this.IsLegendEnabled));
            }
        }

        /// <summary>
        /// 凡例表示を更新します。
        /// </summary>
        private void UpdateLegendPosition(LegendPositions position)
        {
            if (this.LegendPosition == position)
            {
                UpdateRendering_Legend();
                //return;
            }

            var previousPosition = this.LegendPosition;
            this.LegendPosition = position;

            this._operationLegendMenu[(int)previousPosition].IsChecked = false;
            var index = (int)this._operationAxisMode;
            this._operationLegendMenu[(int)this.LegendPosition].IsChecked = true;
        }

        /// <summary>
        /// MouseLeftButtonDown イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnLegendBorderMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            border.CaptureMouse();
            border.Focus();

            this._legendPosition = this._legendBorder.Margin;
            this._legendMovePoint = e.GetPosition(this);
            e.Handled = true;
        }

        /// <summary>
        /// MouseLeftButtonUp イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnLegendBorderMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            border.ReleaseMouseCapture();

            this._legendMovePoint = null;
            e.Handled = true;
        }

        /// <summary>
        /// MouseMove イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnLegendBorderMouseMove(object sender, MouseEventArgs e)
        {
            if (this._legendMovePoint != null)
            {
                var offset = e.GetPosition(this) - this._legendMovePoint.Value;
                this._legendBorder.Margin = new Thickness(this._legendPosition.Left + offset.X, this._legendPosition.Top + offset.Y, 0, 0);
                e.Handled = true;
            }
        }

        /// <summary>
        /// KeyDown イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnLegendBorderKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (this._legendMovePoint != null)
                {
                    var border = sender as Border;
                    border.ReleaseMouseCapture();
                    this._legendBorder.Margin = this._legendPosition;
                    this._legendMovePoint = null;
                    e.Handled = true;
                }
            }
        }

        #endregion 凡例

        #region グラフカーソル

        /// <summary>
        /// グラフカーソルによるポインタ用の WriteableBitmap を入れるためのコントロール
        /// </summary>
        private Image _cursorPointerImage = new Image() { Stretch = Stretch.None };

        /// <summary>
        /// グラフカーソルによるポインタ用の WriteableBitmap オブジェクト
        /// </summary>
        private WriteableBitmap _cursorPointerBitmap;

        /// <summary>
        /// グラフカーソルとグラフデータの交点を表示するときのグラフデータ数制限
        /// </summary>
        private const int ShowCursorPointerLimit = 5000;

        private double _xCursor1;
        private double _xCursor1Old;
        private Rectangle _xCursor1Rectangle;
        private TextBlock _xCursor1TextBlock1;
        private TextBlock _xCursor1TextBlock2;

        private double _xCursor2;
        private double _xCursor2Old;
        private Rectangle _xCursor2Rectangle;
        private TextBlock _xCursor2TextBlock1;
        private TextBlock _xCursor2TextBlock2;

        private Point? _cursorDragStartPoint;

        /// <summary>
        /// グラフカーソルの描画をおこないます。
        /// </summary>
        private void UpdateRendering_Cursor()
        {
            this._xCursor1Rectangle.Visibility = Visibility.Collapsed;
            this._xCursor1TextBlock1.Visibility = Visibility.Collapsed;
            this._xCursor1TextBlock2.Visibility = Visibility.Collapsed;

            this._xCursor2Rectangle.Visibility = Visibility.Collapsed;
            this._xCursor2TextBlock1.Visibility = Visibility.Collapsed;
            this._xCursor2TextBlock2.Visibility = Visibility.Collapsed;

            if (!this.IsCursorEnabled)
                return;
            if (this._cursorPointerImage == null)
                return;
            if (this._cursorPointerBitmap == null)
                return;

            // グラフカーソルのポインタ描画エリアをクリアする
            this._cursorPointerBitmap.Clear(Colors.Transparent);

            var color = (Color)this.FindResource("WindowBorderColor");

            if (this.GraphCursor.HasFlag(GraphCursors.XCursor1))
            {
                this._xCursor1Rectangle.Visibility = Visibility.Visible;
                this._xCursor1TextBlock1.Visibility = Visibility.Visible;
                this._xCursor1TextBlock2.Visibility = Visibility.Visible;

                (this._xCursor1Rectangle.Fill as SolidColorBrush).Color = color;
                (this._xCursor1TextBlock1.Background as SolidColorBrush).Color = color;
                (this._xCursor1TextBlock2.Background as SolidColorBrush).Color = color;

                this._xCursor1Rectangle.Height = this._graphArea.Height;
                this._xCursor1Rectangle.Margin = new Thickness(this._graphArea.Left + XAxisToScreen(this._xCursor1), this._graphArea.Top, 0, 0);

                this._xCursor1TextBlock2.Text = this._xCursor1.ToString(this.XAxisSettings.StringFormat);
                this._xCursor1TextBlock1.Margin = new Thickness(this._graphArea.Left + XAxisToScreen(this._xCursor1) - this._xCursor1TextBlock1.RenderSize.Width / 2.0, this._graphArea.Top, 0, 0);
                this._xCursor1TextBlock2.Margin = new Thickness(this._graphArea.Left + XAxisToScreen(this._xCursor1) - this._xCursor1TextBlock2.RenderSize.Width / 2.0, this._graphArea.Bottom - this._xCursor1TextBlock2.RenderSize.Height, 0, 0);

                // グラフカーソルとグラフデータの交点にポインタを表示する
                Point pt1;
                Point pt2 = new Point();
                int index = 0;
                Color cursorColor;
                foreach (var graphData in this.GraphDataCollection)
                {
                    if (graphData.EndIndex - graphData.StartIndex >= ShowCursorPointerLimit)
                        continue;

                    if ((graphData.XData.Length > 0) && (graphData.YData.Length > 0))
                    {
                        pt1 = CalcCursorPosition(graphData, this._xCursor1);
                        pt2.X = XAxisToScreen(pt1.X);
                        pt2.Y = YAxisToScreen(pt1.Y, graphData.IsY2);
                        cursorColor = graphData.AutoStroke ? GetColor(index) : graphData.Stroke;
                        this._cursorPointerBitmap.FillEllipseCentered((int)pt2.X, (int)pt2.Y, 6, 6, cursorColor);
                    }
                    index++;
                }
            }

            if (this.GraphCursor.HasFlag(GraphCursors.XCursor2))
            {
                this._xCursor2Rectangle.Visibility = Visibility.Visible;
                this._xCursor2TextBlock1.Visibility = Visibility.Visible;
                this._xCursor2TextBlock2.Visibility = Visibility.Visible;

                (this._xCursor2Rectangle.Fill as SolidColorBrush).Color = color;
                (this._xCursor2TextBlock1.Background as SolidColorBrush).Color = color;
                (this._xCursor2TextBlock2.Background as SolidColorBrush).Color = color;

                this._xCursor2Rectangle.Height = this._graphArea.Height;
                this._xCursor2Rectangle.Margin = new Thickness(this._graphArea.Left + XAxisToScreen(this._xCursor2), this._graphArea.Top, 0, 0);

                this._xCursor2TextBlock2.Text = this._xCursor2.ToString(this.XAxisSettings.StringFormat);
                this._xCursor2TextBlock1.Margin = new Thickness(this._graphArea.Left + XAxisToScreen(this._xCursor2) - this._xCursor2TextBlock1.RenderSize.Width / 2.0, this._graphArea.Top, 0, 0);
                this._xCursor2TextBlock2.Margin = new Thickness(this._graphArea.Left + XAxisToScreen(this._xCursor2) - this._xCursor2TextBlock2.RenderSize.Width / 2.0, this._graphArea.Bottom - this._xCursor2TextBlock2.RenderSize.Height, 0, 0);

                // グラフカーソルとグラフデータの交点にポインタを表示する
                Point pt1;
                Point pt2 = new Point();
                int index = 0;
                Color cursorColor;
                foreach (var graphData in this.GraphDataCollection)
                {
                    if (graphData.EndIndex - graphData.StartIndex >= ShowCursorPointerLimit)
                        continue;

                    if ((graphData.XData.Length > 0) && (graphData.YData.Length > 0))
                    {
                        pt1 = CalcCursorPosition(graphData, this._xCursor2);
                        pt2.X = XAxisToScreen(pt1.X);
                        pt2.Y = YAxisToScreen(pt1.Y, graphData.IsY2);
                        cursorColor = graphData.AutoStroke ? GetColor(index) : graphData.Stroke;
                        this._cursorPointerBitmap.FillEllipseCentered((int)pt2.X, (int)pt2.Y, 6, 6, cursorColor);
                    }
                    index++;
                }
            }
        }

        private DelegateCommand _changeCursorEnabledCommand;
        /// <summary>
        /// グラフコントロール表示切替コマンドを取得します。
        /// </summary>
        public DelegateCommand ChangeCursorEnabledCommand
        {
            get
            {
                return this._changeCursorEnabledCommand ?? (this._changeCursorEnabledCommand = new DelegateCommand(_ =>
                {
                    this.IsCursorEnabled = this._cursorEnabledMenu.IsChecked;
                }));
            }
        }

        private DelegateCommand _changeCursorCommand;
        /// <summary>
        /// グラフカーソル表示種別切替コマンドを取得します。
        /// </summary>
        public DelegateCommand ChangeCursorCommand
        {
            get
            {
                return this._changeCursorCommand ?? (this._changeCursorCommand = new DelegateCommand(
                _ =>
                {
                    var cursors = GraphCursors.None;
                    if (this._xCursor1Menu.IsChecked) cursors |= GraphCursors.XCursor1;
                    if (this._xCursor2Menu.IsChecked) cursors |= GraphCursors.XCursor2;
                    this.GraphCursor = cursors;
                },
                _ => this.IsCursorEnabled));
            }
        }

        private DelegateCommand _resetCursorCommand;
        /// <summary>
        /// カーソル位置リセットコマンドを取得します。
        /// </summary>
        public DelegateCommand ResetCursorCommand
        {
            get
            {
                return this._resetCursorCommand ?? (this._resetCursorCommand = new DelegateCommand(
                _ =>
                {
                    this._xCursor1 = this.XAxisSettings.Minimum;
                    this._xCursor2 = this.XAxisSettings.Maximum;
                    UpdateRendering_Cursor();
                },
                _ => this.IsCursorEnabled));
            }
        }

        /// <summary>
        /// MouseLeftButtonDown イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnXCursor1MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnXCursor1MouseLeftButtonDown");
            var element = sender as UIElement;
            element.CaptureMouse();
            element.Focus();
            this._xCursor1Old = this._xCursor1;
            this._cursorDragStartPoint = e.GetPosition(this);
            e.Handled = true;
        }

        /// <summary>
        /// MouseLeftButtonDown イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnXCursor2MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnXCursor2MouseLeftButtonDown");
            var element = sender as UIElement;
            element.CaptureMouse();
            element.Focus();
            this._xCursor2Old = this._xCursor2;
            this._cursorDragStartPoint = e.GetPosition(this);
            e.Handled = true;
        }

        /// <summary>
        /// MouseLeftButtonUp イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnXCursor1MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var element = sender as UIElement;
            element.ReleaseMouseCapture();

            this._cursorDragStartPoint = null;
            e.Handled = true;
        }

        /// <summary>
        /// MouseLeftButtonUp イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnXCursor2MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var element = sender as UIElement;
            element.ReleaseMouseCapture();

            this._cursorDragStartPoint = null;
            e.Handled = true;
        }

        /// <summary>
        /// MouseMove イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnXCursor1MouseMove(object sender, MouseEventArgs e)
        {
            if (this._cursorDragStartPoint != null)
            {
                var pt = e.GetPosition(this);
                var xCursor = this._xCursor1 + ScreenDistanceToXAxisDistance((pt - this._cursorDragStartPoint.Value).X);
                if (xCursor < this.XAxisSettings.Minimum) xCursor = this.XAxisSettings.Minimum;
                else if (xCursor > this.XAxisSettings.Maximum) xCursor = this.XAxisSettings.Maximum;
                this._xCursor1 = xCursor;
                if (this._xCursor1 < this.XAxisSettings.Minimum) this._xCursor1 = this.XAxisSettings.Minimum;
                else if (this._xCursor1 > this.XAxisSettings.Maximum) this._xCursor1 = this.XAxisSettings.Maximum;
                UpdateRendering_Legend();
                UpdateRendering_Cursor();

                this._cursorDragStartPoint = pt;
                e.Handled = true;
            }
        }

        /// <summary>
        /// MouseMove イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnXCursor2MouseMove(object sender, MouseEventArgs e)
        {
            if (this._cursorDragStartPoint != null)
            {
                var pt = e.GetPosition(this);
                var xCursor = this._xCursor2 + ScreenDistanceToXAxisDistance((pt - this._cursorDragStartPoint.Value).X);
                if (xCursor < this.XAxisSettings.Minimum) xCursor = this.XAxisSettings.Minimum;
                else if (xCursor > this.XAxisSettings.Maximum) xCursor = this.XAxisSettings.Maximum;
                this._xCursor2 = xCursor;
                if (this._xCursor2 < this.XAxisSettings.Minimum) this._xCursor2 = this.XAxisSettings.Minimum;
                else if (this._xCursor2 > this.XAxisSettings.Maximum) this._xCursor2 = this.XAxisSettings.Maximum;
                UpdateRendering_Legend();
                UpdateRendering_Cursor();

                this._cursorDragStartPoint = pt;
                e.Handled = true;
            }
        }

        /// <summary>
        /// KeyDown イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnXCursor1KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (this._cursorDragStartPoint != null)
                {
                    (sender as UIElement).ReleaseMouseCapture();
                    this._xCursor1 = this._xCursor1Old;
                    this._cursorDragStartPoint = null;
                    UpdateRendering_Legend();
                    UpdateRendering_Cursor();
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// KeyDown イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnXCursor2KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (this._cursorDragStartPoint != null)
                {
                    (sender as UIElement).ReleaseMouseCapture();
                    this._xCursor2 = this._xCursor2Old;
                    this._cursorDragStartPoint = null;
                    UpdateRendering_Legend();
                    UpdateRendering_Cursor();
                    e.Handled = true;
                }
            }
        }

        #endregion グラフカーソル

        // /// <summary>
        // /// 現在の DPI
        // /// </summary>
        //private DpiScale _scale;

        /// <summary>
        /// 全体のコンテンツを含むコンテナ用オブジェクト
        /// </summary>
        private Grid _container;

        /// <summary>
        /// グラフ描画エリアの領域
        /// </summary>
        private Rect _graphArea;

        #region コンテキストメニュー

        /// <summary>
        /// グラフのコンテキストメニュー
        /// </summary>
        private ContextMenu _contextMenu;

        /// <summary>
        /// 移動メニュー
        /// </summary>
        private MenuItem _moveMenu;

        private MenuItem _moveXMenu;
        private MenuItem _moveYMenu;
        private MenuItem _moveXYMenu;
        private MenuItem _moveY2Menu;
        private MenuItem _moveXY2Menu;

        /// <summary>
        /// 拡大メニュー
        /// </summary>
        private MenuItem _zoomMenu;

        private MenuItem _zoomXMenu;
        private MenuItem _zoomYMenu;
        private MenuItem _zoomXYMenu;
        private MenuItem _zoomY2Menu;
        private MenuItem _zoomXY2Menu;

        /// <summary>
        /// 凡例メニュー
        /// </summary>
        private MenuItem _legendMenu;

        private MenuItem _legendEnabledMenu;
        private MenuItem _legendTopLeftMenu;
        private MenuItem _legendTopRightMenu;
        private MenuItem _legendBottomRightMenu;
        private MenuItem _legendBottomLeftMenu;
        private MenuItem _legendArbitraryMenu;

        /// <summary>
        /// カーソルメニュー
        /// </summary>
        private MenuItem _cursorMenu;

        private MenuItem _cursorEnabledMenu;
        private MenuItem _xCursor1Menu;
        private MenuItem _xCursor2Menu;
        private MenuItem _resetXCursorMenu;

        /// <summary>
        /// 表示範囲自動設定メニュー
        /// </summary>
        private MenuItem _autoScalingMenu;

        private MenuItem _allAutoScalingMenu;
        private MenuItem _onlyYAxisScalingMenu;
        private MenuItem _yStepFixedAutoScalingMenu;

        /// <summary>
        /// 画像保存メニュー
        /// </summary>
        private MenuItem _saveImageMenu;

        /// <summary>
        /// 詳細設定メニュー
        /// </summary>
        private MenuItem _configMenu;

        /// <summary>
        /// コードから操作するためにメニューを集約する配列データ
        /// UI として使用しているわけではありません。
        /// </summary>
        private MenuItem[] _operationAxisMenu;

        /// <summary>
        /// コードから操作するためにメニューを集約する配列データ
        /// UI として使用しているわけではありません。
        /// </summary>
        private MenuItem[] _operationLegendMenu;

        #endregion コンテキストメニュー

        #region 移動拡大モード

        /// <summary>
        /// 軸操作モード表示用 TextBlock
        /// </summary>
        private TextBlock _axisOperationModeText;

        /// <summary>
        /// 拡大領域表示用オブジェクト
        /// </summary>
        private Path _zoomRectanglePath;

        /// <summary>
        /// 拡大領域表示用オブジェクトのためのジオメトリ
        /// </summary>
        private RectangleGeometry _zoomRectangleGeometry;

        /// <summary>
        /// 拡大領域表示用オブジェクトの背景色
        /// </summary>
        private SolidColorBrush _zoomRectangleBrush = new SolidColorBrush();

        /// <summary>
        /// 軸操作モード
        /// </summary>
        private OperationAxisModes _operationAxisMode = OperationAxisModes.None;

        /// <summary>
        /// 軸操作開始点
        /// </summary>
        private Point? _startPoint = null;

        private AxisSettings _startXAxisSettings;
        private AxisSettings _startYAxisSettings;
        private AxisSettings _startY2AxisSettings;

        private DelegateCommand _changeOperationAxisModeCommand;
        /// <summary>
        /// 軸操作モード変更コマンドを取得します。
        /// </summary>
        private DelegateCommand ChangeOperationAxisModeCommand
        {
            get
            {
                return this._changeOperationAxisModeCommand ?? (this._changeOperationAxisModeCommand = new DelegateCommand(
                p =>
                {
                    UpdateOperationAxisMode((OperationAxisModes)p);
                }));
            }
        }

        /// <summary>
        /// 軸操作モードを更新します。
        /// </summary>
        private void UpdateOperationAxisMode(OperationAxisModes mode)
        {
            var previousMode = this._operationAxisMode;
            if (this._operationAxisMode == mode)
            {
                this._operationAxisMode = OperationAxisModes.None;
            }
            else
            {
                this._operationAxisMode = mode;
            }

            if (this._operationAxisMode == OperationAxisModes.None)
            {
                foreach (var menu in this._operationAxisMenu)
                {
                    menu.IsChecked = false;
                }
            }
            else
            {
                if (previousMode != OperationAxisModes.None)
                {
                    this._operationAxisMenu[(int)previousMode].IsChecked = false;
                }
                var index = (int)this._operationAxisMode;
                this._operationAxisMenu[index].IsChecked = true;
                // KeyDown イベントを拾うためにフォーカスする
                this._container.Focus();
            }

            this._axisOperationModeText.Text = AxisOperationString(this._operationAxisMode);
        }

        private string AxisOperationString(OperationAxisModes mode)
        {
            switch (mode)
            {
                case OperationAxisModes.None: return "";
                case OperationAxisModes.MoveX: return "横軸移動";
                case OperationAxisModes.MoveY: return "縦軸移動";
                case OperationAxisModes.MoveXY: return "横-縦軸移動";
                case OperationAxisModes.MoveY2: return "第 2 主軸移動";
                case OperationAxisModes.MoveXY2: return "横-第 2 主軸移動";
                case OperationAxisModes.ZoomX: return "横軸拡大";
                case OperationAxisModes.ZoomY: return "縦軸拡大";
                case OperationAxisModes.ZoomXY: return "横-縦軸拡大";
                case OperationAxisModes.ZoomY2: return "第 2 主軸拡大";
                case OperationAxisModes.ZoomXY2: return "横-第 2 主軸拡大";
                default:
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// MouseLeftButtonDown イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this._operationAxisMode != OperationAxisModes.None)
            {
                this._startPoint = e.GetPosition(this._graphImage);
            }

            if (this._operationAxisMode >= OperationAxisModes.ZoomX)
            {
                // 拡大モードの場合

                // ダブルクリックしたときは縮小する
                if (e.ClickCount == 2)
                {
                    ZoomOutAxis();
                }
                else
                {
                    this._container.CaptureMouse();
                }
            }
            else if (this._operationAxisMode >= OperationAxisModes.MoveX)
            {
                // 移動モードの場合
                this._startXAxisSettings = this.XAxisSettings.Clone();
                this._startYAxisSettings = this.YAxisSettings.Clone();
                this._startY2AxisSettings = this.Y2AxisSettings.Clone();
                this._container.CaptureMouse();
            }
        }

        /// <summary>
        /// 移動モードのマウスカーソルに変更します。
        /// </summary>
        private void ChangeMoveMouseCursor()
        {
            switch (this._operationAxisMode)
            {
                case OperationAxisModes.MoveX:
                    this.Cursor = Cursors.ScrollWE;
                    break;

                case OperationAxisModes.MoveY:
                case OperationAxisModes.MoveY2:
                    this.Cursor = Cursors.ScrollNS;
                    break;

                case OperationAxisModes.MoveXY:
                case OperationAxisModes.MoveXY2:
                    this.Cursor = Cursors.ScrollAll;
                    break;

                default:
                    this.Cursor = null;
                    break;
            }
        }

        /// <summary>
        /// MouseLeftButtonUp イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this._container.ReleaseMouseCapture();

            if ((this._operationAxisMode >= OperationAxisModes.ZoomX) && (this._startPoint != null))
            {
                // 拡大モードの場合
                if (this._zoomRectanglePath.Visibility != Visibility.Collapsed)
                {
                    var endPt = e.GetPosition(this._graphImage);
                    ZoomAxis(ScreenToXAxis(this._zoomRectangleGeometry.Rect.Left), ScreenToXAxis(this._zoomRectangleGeometry.Rect.Right),
                             ScreenToYAxis(this._zoomRectangleGeometry.Rect.Top), ScreenToYAxis(this._zoomRectangleGeometry.Rect.Bottom),
                             ScreenToY2Axis(this._zoomRectangleGeometry.Rect.Top), ScreenToY2Axis(this._zoomRectangleGeometry.Rect.Bottom));
                    this._zoomRectanglePath.Visibility = Visibility.Collapsed;
                }
                this._startPoint = null;
            }
            else if (this._operationAxisMode >= OperationAxisModes.MoveX)
            {
                // 移動モードの場合
                this._startPoint = null;
            }
        }

        private void ZoomAxis(double x_1, double x_2, double y1_1, double y1_2, double y2_1, double y2_2)
        {
            var xMin = x_1; System.Diagnostics.Debug.WriteLine("ZoomAxis()");


            var xMax = x_2;
            if (xMin > xMax)
            {
                xMin = x_2;
                xMax = x_1;
            }

            var yMin = y1_1;
            var yMax = y1_2;
            if (yMin > yMax)
            {
                yMin = y1_2;
                yMax = y1_1;
            }

            var y2Min = y2_1;
            var y2Max = y2_2;
            if (y2Min > y2Max)
            {
                y2Min = y2_2;
                y2Max = y2_1;
            }

            if ((this._operationAxisMode == OperationAxisModes.ZoomX) ||
                (this._operationAxisMode == OperationAxisModes.ZoomXY) ||
                (this._operationAxisMode == OperationAxisModes.ZoomXY2))
            {
                this.XAxisSettings.Minimum = xMin;
                this.XAxisSettings.Maximum = xMax;
                this.XAxisSettings.MajorStep = this.XAxisSettings.Range / 10.0;
                this.XAxisSettings.MinorStep = this.XAxisSettings.Range / 20.0;
            }

            if ((this._operationAxisMode == OperationAxisModes.ZoomY) ||
                (this._operationAxisMode == OperationAxisModes.ZoomXY))
            {
                this.YAxisSettings.Minimum = yMin;
                this.YAxisSettings.Maximum = yMax;
                this.YAxisSettings.MajorStep = this.YAxisSettings.Range / 10.0;
                this.YAxisSettings.MinorStep = this.YAxisSettings.Range / 20.0;
            }

            if ((this._operationAxisMode == OperationAxisModes.ZoomY2) ||
                (this._operationAxisMode == OperationAxisModes.ZoomXY2))
            {
                this.Y2AxisSettings.Minimum = y2Min;
                this.Y2AxisSettings.Maximum = y2Max;
                this.Y2AxisSettings.MajorStep = this.Y2AxisSettings.Range / 10.0;
                this.Y2AxisSettings.MinorStep = this.Y2AxisSettings.Range / 20.0;
            }

#if BETA
            InitWriteableBitmap();
#else
            this._shouldUpdate_GridBitmap = true;
            this._shouldUpdate_GraphBitmap = true;
            UpdateRendering();
#endif
        }

        private void ZoomOutAxis()
        {
            System.Diagnostics.Debug.WriteLine("ZoomOutAxis()");

            if ((this._operationAxisMode == OperationAxisModes.ZoomX) ||
                (this._operationAxisMode == OperationAxisModes.ZoomXY) ||
                (this._operationAxisMode == OperationAxisModes.ZoomXY2))
            {
                this.XAxisSettings.Minimum -= 2 * this.XAxisSettings.MajorStep;
                this.XAxisSettings.Maximum += 2 * this.XAxisSettings.MajorStep;
                this.XAxisSettings.MajorStep = this.XAxisSettings.Range / 10.0;
                this.XAxisSettings.MinorStep = this.XAxisSettings.Range / 20.0;
            }

            if ((this._operationAxisMode == OperationAxisModes.ZoomY) ||
                (this._operationAxisMode == OperationAxisModes.ZoomXY))
            {
                this.YAxisSettings.Minimum -= 2 * this.YAxisSettings.MajorStep;
                this.YAxisSettings.Maximum += 2 * this.YAxisSettings.MajorStep;
                this.YAxisSettings.MajorStep = this.YAxisSettings.Range / 10.0;
                this.YAxisSettings.MinorStep = this.YAxisSettings.Range / 20.0;
            }

            if ((this._operationAxisMode == OperationAxisModes.ZoomY2) ||
                (this._operationAxisMode == OperationAxisModes.ZoomXY2))
            {
                this.Y2AxisSettings.Minimum -= 2 * this.Y2AxisSettings.MajorStep;
                this.Y2AxisSettings.Maximum += 2 * this.Y2AxisSettings.MajorStep;
                this.Y2AxisSettings.MajorStep = this.Y2AxisSettings.Range / 10.0;
                this.Y2AxisSettings.MinorStep = this.Y2AxisSettings.Range / 20.0;
            }

#if BETA
            InitWriteableBitmap();
#else
            this._shouldUpdate_GridBitmap = true;
            this._shouldUpdate_GraphBitmap = true;
            UpdateRendering();
#endif
        }

        /// <summary>
        /// MouseMove イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            // メニュー選択後は必ず MouseMove イベントが発生するので
            // マウスカーソル変更はこちらにのみ実装
            // MouseLeftButtonDown イベントには実装しない
            ChangeMoveMouseCursor();

            if ((e.LeftButton == MouseButtonState.Pressed) && (this._startPoint != null))
            {
                if (this._operationAxisMode >= OperationAxisModes.ZoomX)
                {
                    // 拡大モードの場合
                    // 拡大領域を示すオブジェクトを表示する
                    var screenPt = e.GetPosition(this._graphImage);
                    ZoomAxisEffect(screenPt.X, screenPt.Y);
                    e.Handled = true;
                }
                else if (this._operationAxisMode >= OperationAxisModes.MoveX)
                {
                    // 移動モードの場合
                    // ドラッグ操作で軸を移動する
                    var offsetPt = e.GetPosition(this._graphImage) - this._startPoint.Value;    // スクリーン座標での差分
                    MoveAxis(-offsetPt.X, offsetPt.Y);
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// 軸操作モードが拡大モードのときに指定した範囲に拡大領域を表示します。
        /// </summary>
        /// <param name="screenX"></param>
        /// <param name="screenY"></param>
        private void ZoomAxisEffect(double screenX, double screenY)
        {
            if (this._operationAxisMode < OperationAxisModes.ZoomX)
            {
                this._zoomRectanglePath.Visibility = Visibility.Collapsed;
                return;
            }

            var left = screenX;
            var top = screenY;
            var right = this._startPoint.Value.X;
            var bottom = this._startPoint.Value.Y;

            if (left > right)
            {
                left = right;
                right = screenX;
            }
            if (top > bottom)
            {
                top = bottom;
                bottom = screenY;
            }
            if (left < 0) left = 0;
            if (top < 0) top = 0;
            if (right > this._graphArea.Width) right = this._graphArea.Width;
            if (bottom > this._graphArea.Height) bottom = this._graphArea.Height;

            var width = right - left;
            var height = bottom - top;

            var isXEnabled = width >= SystemParameters.MinimumHorizontalDragDistance;
            var isYEnabled = height >= SystemParameters.MinimumVerticalDragDistance;

            if ((this._operationAxisMode == OperationAxisModes.ZoomX) && !isXEnabled )
            {
                this._zoomRectanglePath.Visibility = Visibility.Collapsed;
                return;
            }

            if (((this._operationAxisMode == OperationAxisModes.ZoomY) || (this._operationAxisMode == OperationAxisModes.ZoomY2)) && !isYEnabled)
            {
                this._zoomRectanglePath.Visibility = Visibility.Collapsed;
                return;
            }

            if (((this._operationAxisMode == OperationAxisModes.ZoomXY) || (this._operationAxisMode == OperationAxisModes.ZoomXY2)) && !isXEnabled && !isYEnabled)
            {
                this._zoomRectanglePath.Visibility = Visibility.Collapsed;
                return;
            }

            if (this._operationAxisMode == OperationAxisModes.ZoomX)
            {
                top = 0;
                height = this._graphArea.Height;
            }
            else if ((this._operationAxisMode == OperationAxisModes.ZoomY) || (this._operationAxisMode == OperationAxisModes.ZoomY2))
            {
                left = 0;
                width = this._graphArea.Width;
            }

            if (left < 0) left = 0;
            if (top < 0) top = 0;
            if (width < 0) width = 0;
            if (height < 0) height = 0;

            this._zoomRectanglePath.Visibility = Visibility.Visible;
            this._zoomRectanglePath.Margin = this.GraphAreaMargin;
            var color = (Color)this.FindResource("WindowBorderColor");
            color.A = 0xa0;
            this._zoomRectangleBrush.Color = color;
            this._zoomRectangleGeometry.Rect = new Rect(left, top, width, height);
        }

        /// <summary>
        /// 軸操作モードが移動モードのときに指定した分だけ目盛設定を移動します。
        /// </summary>
        /// <param name="screenX">横方向に対するスクリーン座標のオフセットを指定します。</param>
        /// <param name="screenY">縦方向に対するスクリーン座標のオフセットを指定します。</param>
        private void MoveAxis(double screenX, double screenY)
        {
            if (this._operationAxisMode == OperationAxisModes.None)
                return;
            if (this._operationAxisMode >= OperationAxisModes.ZoomX)
                return;

            var offsetX = Math.Round(ScreenDistanceToXAxisDistance(screenX) / this.XAxisSettings.MinorStep, MidpointRounding.AwayFromZero) * this.XAxisSettings.MinorStep;
            var offsetY = Math.Round(ScreenDistanceToYAxisDistance(screenY) / this.YAxisSettings.MinorStep, MidpointRounding.AwayFromZero) * this.YAxisSettings.MinorStep;
            var offsetY2 = Math.Round(ScreenDistanceToY2AxisDistance(screenY) / this.Y2AxisSettings.MinorStep, MidpointRounding.AwayFromZero) * this.Y2AxisSettings.MinorStep;

            if ((this._operationAxisMode == OperationAxisModes.MoveX) ||
                (this._operationAxisMode == OperationAxisModes.MoveXY) ||
                (this._operationAxisMode == OperationAxisModes.MoveXY2))
            {
                this.XAxisSettings.Minimum = this._startXAxisSettings.Minimum + offsetX;
                this.XAxisSettings.Maximum = this._startXAxisSettings.Maximum + offsetX;
            }

            if ((this._operationAxisMode == OperationAxisModes.MoveY) ||
                (this._operationAxisMode == OperationAxisModes.MoveXY))
            {
                this.YAxisSettings.Minimum = this._startYAxisSettings.Minimum + offsetY;
                this.YAxisSettings.Maximum = this._startYAxisSettings.Maximum + offsetY;
            }

            if ((this._operationAxisMode == OperationAxisModes.MoveY2) ||
                (this._operationAxisMode == OperationAxisModes.MoveXY2))
            {
                this.Y2AxisSettings.Minimum = this._startY2AxisSettings.Minimum + offsetY2;
                this.Y2AxisSettings.Maximum = this._startY2AxisSettings.Maximum + offsetY2;
            }

#if BETA
            InitWriteableBitmap();
#else
            this._shouldUpdate_GridBitmap = true;
            this._shouldUpdate_GraphBitmap = true;
            UpdateRendering();
#endif
        }

        /// <summary>
        /// KeyDown イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnContainerKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (this._startPoint == null)
                {
                    UpdateOperationAxisMode(OperationAxisModes.None);
                }
                else
                {
                    if ((OperationAxisModes.MoveX <= this._operationAxisMode) && (this._operationAxisMode <= OperationAxisModes.MoveXY2))
                    {
                        this.XAxisSettings = this._startXAxisSettings.Clone();
                        this.YAxisSettings = this._startYAxisSettings.Clone();
                        this.Y2AxisSettings = this._startY2AxisSettings.Clone();
                        this._startPoint = null;

#if BETA
                        InitWriteableBitmap();
#else
                        this._shouldUpdate_GridBitmap = true;
                        this._shouldUpdate_GraphBitmap = true;
                        UpdateRendering();
#endif
                    }
                    else if (this._operationAxisMode >= OperationAxisModes.ZoomX)
                    {
                        this._startPoint = null;
                        this._zoomRectanglePath.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        #endregion 移動拡大モード

        #region 詳細設定メニュー

        /// <summary>
        /// 詳細設定ダイアログ
        /// </summary>
        private LinegraphMenu _lineGraphMenu;

        private DelegateCommand _openConfigMenuCommand;
        /// <summary>
        /// 詳細設定メニューオープンコマンドを取得します。
        /// </summary>
        internal DelegateCommand OpenConfigMenuCommand
        {
            get
            {
                return this._openConfigMenuCommand ?? (this._openConfigMenuCommand = new DelegateCommand(_ =>
                {
                    this._lineGraphMenu.Show();
                }));
            }
        }

        private DelegateCommand _configOkCommand;
        /// <summary>
        /// 詳細設定メニュー確定コマンドを取得します。
        /// </summary>
        public DelegateCommand ConfigOkCommand
        {
            get
            {
                return this._configOkCommand ?? (this._configOkCommand = new DelegateCommand(_ =>
                {
                    this._lineGraphMenu.Hide();

#if BETA
                    InitWriteableBitmap();
#else
                    this._shouldUpdate_GridBitmap = true;
                    this._shouldUpdate_GraphBitmap = true;
                    UpdateRendering();
#endif
                }));
            }
        }

        private DelegateCommand _configApplyCommand;
        /// <summary>
        /// 詳細設定メニュー適用コマンドを取得します。
        /// </summary>
        public DelegateCommand ConfigApplyCommand
        {
            get
            {
                return this._configApplyCommand ?? (this._configApplyCommand = new DelegateCommand(_ =>
                {
#if BETA
                    InitWriteableBitmap();
#else
                    this._shouldUpdate_GridBitmap = true;
                    this._shouldUpdate_GraphBitmap = true;
                    UpdateRendering();
#endif
                }));
            }
        }

        private DelegateCommand _cancelCommand;
        /// <summary>
        /// 詳細設定メニューキャンセルコマンドを取得します。
        /// </summary>
        public DelegateCommand CancelCommand
        {
            get
            {
                return this._cancelCommand ?? (this._cancelCommand = new DelegateCommand(_ =>
                {
                    this._lineGraphMenu.Hide();
                }));
            }
        }

        /// <summary>
        /// グラフ描画エリアの左余白を取得または設定します。
        /// </summary>
        public double GraphAreaMarginLeft
        {
            get { return this.GraphAreaMargin.Left; }
            set { this.GraphAreaMargin = new Thickness(value, this.GraphAreaMargin.Top, this.GraphAreaMargin.Right, this.GraphAreaMargin.Bottom); }
        }

        /// <summary>
        /// グラフ描画エリアの上余白を取得または設定します。
        /// </summary>
        public double GraphAreaMarginTop
        {
            get { return this.GraphAreaMargin.Top; }
            set { this.GraphAreaMargin = new Thickness(this.GraphAreaMargin.Left, value, this.GraphAreaMargin.Right, this.GraphAreaMargin.Bottom); }
        }

        /// <summary>
        /// グラフ描画エリアの右余白を取得または設定します。
        /// </summary>
        public double GraphAreaMarginRight
        {
            get { return this.GraphAreaMargin.Right; }
            set { this.GraphAreaMargin = new Thickness(this.GraphAreaMargin.Left, this.GraphAreaMargin.Top, value, this.GraphAreaMargin.Bottom); }
        }

        /// <summary>
        /// グラフ描画エリアの下余白を取得または設定します。
        /// </summary>
        public double GraphAreaMarginBottom
        {
            get { return this.GraphAreaMargin.Bottom; }
            set { this.GraphAreaMargin = new Thickness(this.GraphAreaMargin.Left, this.GraphAreaMargin.Top, this.GraphAreaMargin.Right, value); }
        }

        /// <summary>
        /// グラフ描画エリアの左枠線の太さを取得または設定します。
        /// </summary>
        public double GraphAreaBorderThicknessLeft
        {
            get { return this.GraphAreaBorderThickness.Left; }
            set { this.GraphAreaBorderThickness = new Thickness(value, this.GraphAreaBorderThickness.Top, this.GraphAreaBorderThickness.Right, this.GraphAreaBorderThickness.Bottom); }
        }

        /// <summary>
        /// グラフ描画エリアの上枠線の太さを取得または設定します。
        /// </summary>
        public double GraphAreaBorderThicknessTop
        {
            get { return this.GraphAreaBorderThickness.Top; }
            set { this.GraphAreaBorderThickness = new Thickness(this.GraphAreaBorderThickness.Left, value, this.GraphAreaBorderThickness.Right, this.GraphAreaBorderThickness.Bottom); }
        }

        /// <summary>
        /// グラフ描画エリアの右枠線の太さを取得または設定します。
        /// </summary>
        public double GraphAreaBorderThicknessRight
        {
            get { return this.GraphAreaBorderThickness.Right; }
            set { this.GraphAreaBorderThickness = new Thickness(this.GraphAreaBorderThickness.Left, this.GraphAreaBorderThickness.Top, value, this.GraphAreaBorderThickness.Bottom); }
        }

        /// <summary>
        /// グラフ描画エリアの下枠線の太さを取得または設定します。
        /// </summary>
        public double GraphAreaBorderThicknessBottom
        {
            get { return this.GraphAreaBorderThickness.Bottom; }
            set { this.GraphAreaBorderThickness = new Thickness(this.GraphAreaBorderThickness.Left, this.GraphAreaBorderThickness.Top, this.GraphAreaBorderThickness.Right, value); }
        }

        #endregion 詳細設定メニュー

        #region 表示範囲自動設定メニュー

        private DelegateCommand _autoScalingCommand;
        /// <summary>
        /// 表示範囲自動設定コマンドを取得します。
        /// </summary>
        public DelegateCommand AutoScalingCommand
        {
            get
            {
                return this._autoScalingCommand ?? (this._autoScalingCommand = new DelegateCommand(_ =>
                {
                    var count = this.GraphDataCollection.Count();
                    if (count < 1) return;

                    // 横軸設定
                    if (this.GraphDataCollection.Any(x => x.Visibility == Visibility.Visible))
                    {
                        var xData = this.GraphDataCollection.Where(x => x.Visibility == Visibility.Visible).Select(x => x.XData);
                        if (!xData.Any(x => (x != null) && (x.Length > 0))) return;
                        xData = xData.Where(x => x.Length > 0);
                        this.XAxisSettings.Minimum = xData.Min(x => x.Min());
                        this.XAxisSettings.Maximum = xData.Max(x => x.Max());
                        this.XAxisSettings.MajorStep = (this.XAxisSettings.Maximum - this.XAxisSettings.Minimum) / (AxisSettings.DefaultGridLines + 1);
                        this.XAxisSettings.MinorStep = this.XAxisSettings.MajorStep / 2;
                    }

                    // 縦軸設定
                    if (this.GraphDataCollection.Any(x => (x.Visibility == Visibility.Visible) && !x.IsY2))
                    {
                        var yData = this.GraphDataCollection.Where(x => (x.Visibility == Visibility.Visible) && !x.IsY2).Select(x => x.YData);
                        if (yData.Any(x => (x != null) && (x.Length > 0)))
                        {
                            yData = yData.Where(x => x.Length > 0);
                            var ymin = yData.Min(x => x.Min());
                            var ymax = yData.Max(x => x.Max());
                            this.YAxisSettings.MajorStep = (ymax - ymin) / (AxisSettings.DefaultGridLines + 1);
                            this.YAxisSettings.MinorStep = this.YAxisSettings.MajorStep / 2;
                            this.YAxisSettings.Minimum = ymin.MRound(this.YAxisSettings.MajorStep);
                            this.YAxisSettings.Maximum = ymax.MRound(this.YAxisSettings.MajorStep) + this.YAxisSettings.MajorStep;
                        }
                    }

                    // 第2主軸設定
                    if (this.GraphDataCollection.Any(x => (x.Visibility == Visibility.Visible) && x.IsY2))
                    {
                        var yData = this.GraphDataCollection.Where(x => (x.Visibility == Visibility.Visible) && x.IsY2).Select(x => x.YData);
                        if (yData.Any(x => (x != null) && (x.Length > 0)))
                        {
                            yData = yData.Where(x => x.Length > 0);
                            var ymin = yData.Min(x => x.Min());
                            var ymax = yData.Max(x => x.Max());
                            this.Y2AxisSettings.MajorStep = (ymax - ymin) / (AxisSettings.DefaultGridLines + 1);
                            this.Y2AxisSettings.MinorStep = this.Y2AxisSettings.MajorStep / 2;
                            this.Y2AxisSettings.Minimum = ymin.MRound(this.Y2AxisSettings.MajorStep);
                            this.Y2AxisSettings.Maximum = ymax.MRound(this.Y2AxisSettings.MajorStep) + this.Y2AxisSettings.MajorStep;
                        }
                    }

#if BETA
                    InitWriteableBitmap();
#else
                    this._shouldUpdate_GridBitmap = true;
                    this._shouldUpdate_GraphBitmap = true;
                    UpdateRendering();
#endif
                }));
            }
        }

        private DelegateCommand _onlyYAxisScalingCommand;
        /// <summary>
        /// 縦軸のみ表示範囲自動設定コマンドを取得します。
        /// </summary>
        public DelegateCommand OnlyYAxisScalingCommand
        {
            get
            {
                return this._onlyYAxisScalingCommand ?? (this._onlyYAxisScalingCommand = new DelegateCommand(_ =>
                {
                    var count = this.GraphDataCollection.Count();
                    if (count < 1) return;

                    // 横軸設定をそのままにしながら縦軸設定のみを自動設定します。
                    // 現在表示されている横軸の範囲内のデータのみを対象とするようにします。

                    // 縦軸設定
                    if (this.GraphDataCollection.Any(x => (x.Visibility == Visibility.Visible) && !x.IsY2 && x.XData.Any(y => (this.XAxisSettings.Minimum <= y) && (y <= this.XAxisSettings.Maximum))))
                    {
                        var yData = this.GraphDataCollection.Where(x => (x.Visibility == Visibility.Visible) && !x.IsY2).SelectMany(x => x.XData.Zip(x.YData, (xx, yy) => new double[] { xx, yy })).Where(x => (this.XAxisSettings.Minimum <= x[0]) && (x[0] <= this.XAxisSettings.Maximum)).Select(x => x[1]).ToArray();
                        if (yData.Length > 0)
                        {
                            var ymin = yData.Min();
                            var ymax = yData.Max();
                            this.YAxisSettings.MajorStep = (ymax - ymin) / (AxisSettings.DefaultGridLines + 1);
                            this.YAxisSettings.MinorStep = this.YAxisSettings.MajorStep / 2;
                            this.YAxisSettings.Minimum = ymin.MRound(this.YAxisSettings.MajorStep);
                            this.YAxisSettings.Maximum = ymax.MRound(this.YAxisSettings.MajorStep) + this.YAxisSettings.MajorStep;
                        }
                    }

                    // 第2主軸設定
                    if (this.GraphDataCollection.Any(x => (x.Visibility == Visibility.Visible) && x.IsY2 && x.XData.Any(y => (this.XAxisSettings.Minimum <= y) && (y <= this.XAxisSettings.Maximum))))
                    {
                        var yData = this.GraphDataCollection.Where(x => (x.Visibility == Visibility.Visible) && x.IsY2).SelectMany(x => x.XData.Zip(x.YData, (xx, yy) => new double[] { xx, yy })).Where(x => (this.XAxisSettings.Minimum <= x[0]) && (x[0] <= this.XAxisSettings.Maximum)).Select(x => x[1]).ToArray();
                        if (yData.Length > 0)
                        {
                            var ymin = yData.Min();
                            var ymax = yData.Max();
                            this.Y2AxisSettings.MajorStep = (ymax - ymin) / (AxisSettings.DefaultGridLines + 1);
                            this.Y2AxisSettings.MinorStep = this.Y2AxisSettings.MajorStep / 2;
                            this.Y2AxisSettings.Minimum = ymin.MRound(this.Y2AxisSettings.MajorStep);
                            this.Y2AxisSettings.Maximum = ymax.MRound(this.Y2AxisSettings.MajorStep) + this.Y2AxisSettings.MajorStep;
                        }
                    }

#if BETA
                    InitWriteableBitmap();
#else
                    this._shouldUpdate_GridBitmap = true;
                    this._shouldUpdate_GraphBitmap = true;
                    UpdateRendering();
#endif
                }));
            }
        }

        private DelegateCommand _yStepFixedAutoScalingCommand;
        /// <summary>
        /// 縦軸目盛間隔を固定した表示範囲自動設定コマンドを取得します。
        /// </summary>
        public DelegateCommand YStepFixedAutoScalingCommand
        {
            get
            {
                return this._yStepFixedAutoScalingCommand ?? (this._yStepFixedAutoScalingCommand = new DelegateCommand(_ =>
                {
                    var count = this.GraphDataCollection.Count();
                    if (count < 1) return;

                    // 横軸設定
                    if (this.GraphDataCollection.Any(x => x.Visibility == Visibility.Visible))
                    {
                        var xData = this.GraphDataCollection.Select(x => x.XData);
                        if (!xData.Any(x => (x != null) && (x.Length > 0))) return;
                        xData = xData.Where(x => x.Length > 0);
                        this.XAxisSettings.Minimum = xData.Min(x => x.Min());
                        this.XAxisSettings.Maximum = xData.Max(x => x.Max());
                        this.XAxisSettings.MajorStep = (this.XAxisSettings.Maximum - this.XAxisSettings.Minimum) / (AxisSettings.DefaultGridLines + 1);
                        this.XAxisSettings.MinorStep = this.XAxisSettings.MajorStep / 2;
                    }

                    // 縦軸設定
                    if (this.GraphDataCollection.Any(x => (x.Visibility == Visibility.Visible) && !x.IsY2))
                    {
                        var yData = this.GraphDataCollection.Where(x => (x.Visibility == Visibility.Visible) && !x.IsY2).Select(x => x.YData);
                        if (yData.Any(x => (x != null) && (x.Length > 0)))
                        {
                            yData = yData.Where(x => x.Length > 0);
                            var ymin = yData.Min(x => x.Min());
                            var ymax = yData.Max(x => x.Max());
                            var yMiddle = ((ymax + ymin) / 2).MRound(this.YAxisSettings.MajorStep);
                            this.YAxisSettings.Minimum = yMiddle - 5 * this.YAxisSettings.MajorStep;
                            this.YAxisSettings.Maximum = yMiddle + 5 * this.YAxisSettings.MajorStep;
                        }
                    }

                    // 第2主軸設定
                    if (this.GraphDataCollection.Any(x => (x.Visibility == Visibility.Visible) && x.IsY2))
                    {
                        var yData = this.GraphDataCollection.Where(x => (x.Visibility == Visibility.Visible) && x.IsY2).Select(x => x.YData);
                        if (yData.Any(x => (x != null) && (x.Length > 0)))
                        {
                            yData = yData.Where(x => x.Length > 0);
                            var ymin = yData.Min(x => x.Min());
                            var ymax = yData.Max(x => x.Max());
                            var yMiddle = ((ymax + ymin) / 2).MRound(this.Y2AxisSettings.MajorStep);
                            this.Y2AxisSettings.Minimum = yMiddle - 5 * this.Y2AxisSettings.MajorStep;
                            this.Y2AxisSettings.Maximum = yMiddle + 5 * this.Y2AxisSettings.MajorStep;
                        }
                    }

#if BETA
                    InitWriteableBitmap();
#else
                    this._shouldUpdate_GridBitmap = true;
                    this._shouldUpdate_GraphBitmap = true;
                    UpdateRendering();
#endif
                }));
            }
        }

        #endregion 表示範囲自動設定メニュー

        #region 画像保存メニュー

        private SaveFileDialog _saveDialog = new SaveFileDialog() { Title = "画像保存", Filter = "PNG 画像 (*.png)|*.png|ビットマップ画像 (*.bmp)|*.bmp" };

        private DelegateCommand _writeBitmapCommand;
        /// <summary>
        /// 画像保存コマンドを取得します。
        /// </summary>
        public DelegateCommand WriteBitmapCommand
        {
            get
            {
                return this._writeBitmapCommand ?? (this._writeBitmapCommand = new DelegateCommand(_ =>
                {
                    this._saveDialog.FileName = "NewImage.png";
                    var result = this._saveDialog.ShowDialog(Window.GetWindow(this));
                    if (result.HasValue && result.Value)
                    {
                        var path = this._saveDialog.FileName;
                        var bmp = new RenderTargetBitmap((int)this._container.ActualWidth, (int)this._container.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                        bmp.Render(this._container);
                        var extension = System.IO.Path.GetExtension(path).ToLower();
                        BitmapEncoder encoder = null;
                        switch (extension)
                        {
                            case ".bmp": encoder = new BmpBitmapEncoder(); break;
                            default:
                            case ".png": encoder = new PngBitmapEncoder(); break;
                        }
                        if (encoder != null)
                        {
                            encoder.Frames.Add(BitmapFrame.Create(bmp));
                            using (System.IO.FileStream fs = System.IO.File.Open(path, System.IO.FileMode.Create))
                            {
                                encoder.Save(fs);
                            }
                        }
                    }
                }));
            }
        }

        #endregion 画像保存メニュー

#if DEBUG2
        private int _count_GridBitmap;
        private int _count_GraphBitmap;
#endif
    }
}
