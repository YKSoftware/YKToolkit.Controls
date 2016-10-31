namespace YKToolkit.Controls
{
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;

    /// <summary>
    /// ドロップダウン形式でコンテンツを表示するためのボタンを表します。
    /// </summary>
    [TemplatePart(Name = PART_ThemeColors1ListBox, Type = typeof(ListBox))]
    [TemplatePart(Name = PART_ThemeColors2ListBox, Type = typeof(ListBox))]
    [TemplatePart(Name = PART_StandardColorsListBox, Type = typeof(ListBox))]
    [TemplatePart(Name = PART_RecentColorsListBox, Type = typeof(ListBox))]
    [TemplatePart(Name = PART_TransparentButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_ColorShadingGrid, Type = typeof(Grid))]
    [TemplatePart(Name = PART_PalleteSolidColorBrush, Type = typeof(SolidColorBrush))]
    [TemplatePart(Name = PART_SelectorPoint1, Type = typeof(EllipseGeometry))]
    [TemplatePart(Name = PART_SelectorPoint2, Type = typeof(EllipseGeometry))]
    [TemplatePart(Name = PART_Slider_H, Type = typeof(Slider))]
    [TemplatePart(Name = PART_Slider_A, Type = typeof(Slider))]
    [TemplatePart(Name = PART_Slider_R, Type = typeof(Slider))]
    [TemplatePart(Name = PART_Slider_G, Type = typeof(Slider))]
    [TemplatePart(Name = PART_Slider_B, Type = typeof(Slider))]
    [TemplatePart(Name = PART_SampleSolidColorBrush, Type = typeof(SolidColorBrush))]
    [TemplatePart(Name = PART_PalleteColorTextBox, Type = typeof(TextBox))]
    [TemplatePart(Name = PART_OkButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_CancelButton, Type = typeof(Button))]
    public class ColorPicker : Control
    {
        #region TemplatePart
        private const string PART_ThemeColors1ListBox = "PART_ThemeColors1ListBox";
        private const string PART_ThemeColors2ListBox = "PART_ThemeColors2ListBox";
        private const string PART_StandardColorsListBox = "PART_StandardColorsListBox";
        private const string PART_RecentColorsListBox = "PART_RecentColorsListBox";
        private const string PART_TransparentButton = "PART_TransparentButton";

        private const string PART_ColorShadingGrid = "PART_ColorShadingGrid";
        private const string PART_PalleteSolidColorBrush = "PART_PalleteSolidColorBrush";
        private const string PART_SelectorPoint1 = "PART_SelectorPoint1";
        private const string PART_SelectorPoint2 = "PART_SelectorPoint2";
        private const string PART_Slider_H = "PART_Slider_H";
        private const string PART_Slider_A = "PART_Slider_A";
        private const string PART_Slider_R = "PART_Slider_R";
        private const string PART_Slider_G = "PART_Slider_G";
        private const string PART_Slider_B = "PART_Slider_B";
        private const string PART_SampleSolidColorBrush = "PART_SampleSolidColorBrush";
        private const string PART_PalleteColorTextBox = "PART_PalleteColorTextBox";
        private const string PART_OkButton = "PART_OkButton";
        private const string PART_CancelButton = "PART_CancelButton";

        private ListBox _themeColors1ListBox;
        private ListBox ThemeColors1ListBox
        {
            get { return _themeColors1ListBox; }
            set
            {
                if (_themeColors1ListBox != null)
                {
                    _themeColors1ListBox.SelectionChanged -= ThemeColors1ListBox_SelectionChanged;
                }
                _themeColors1ListBox = value;
                if (_themeColors1ListBox != null)
                {
                    _themeColors1ListBox.SelectionChanged += ThemeColors1ListBox_SelectionChanged;
                }
            }
        }

        private ListBox _themeColors2ListBox;
        private ListBox ThemeColors2ListBox
        {
            get { return _themeColors2ListBox; }
            set
            {
                if (_themeColors2ListBox != null)
                {
                    _themeColors2ListBox.SelectionChanged -= ThemeColors2ListBox_SelectionChanged;
                }
                _themeColors2ListBox = value;
                if (_themeColors2ListBox != null)
                {
                    _themeColors2ListBox.SelectionChanged += ThemeColors2ListBox_SelectionChanged;
                }
            }
        }

        private ListBox _standardColorsListBox;
        private ListBox StandardColorsListBox
        {
            get { return _standardColorsListBox; }
            set
            {
                if (_standardColorsListBox != null)
                {
                    _standardColorsListBox.SelectionChanged -= StandardColorsListBox_SelectionChanged;
                }
                _standardColorsListBox = value;
                if (_standardColorsListBox != null)
                {
                    _standardColorsListBox.SelectionChanged += StandardColorsListBox_SelectionChanged;
                }
            }
        }

        private ListBox _recentColorsListBox;
        private ListBox RecentColorsListBox
        {
            get { return _recentColorsListBox; }
            set
            {
                if (_recentColorsListBox != null)
                {
                    _recentColorsListBox.SelectionChanged -= RecentColorsListBox_SelectionChanged;
                }
                _recentColorsListBox = value;
                if (_recentColorsListBox != null)
                {
                    _recentColorsListBox.SelectionChanged += RecentColorsListBox_SelectionChanged;
                }
            }
        }

        private Button _transparentButton;
        private Button TransparentButton
        {
            get { return _transparentButton; }
            set
            {
                if (_transparentButton != null)
                {
                    _transparentButton.Click -= TransparentButton_Click;
                }
                _transparentButton = value;
                if (_transparentButton != null)
                {
                    _transparentButton.Click += TransparentButton_Click;
                }
            }
        }

        private Grid _colorShadingGrid;
        private Grid ColorShadingGrid
        {
            get { return _colorShadingGrid; }
            set
            {
                if (_colorShadingGrid != null)
                {
                }
                _colorShadingGrid = value;
                if (_colorShadingGrid != null)
                {
                    _colorShadingGrid.MouseLeftButtonDown += ColorShadingGrid_MouseLeftButtonDown;
                    _colorShadingGrid.MouseLeftButtonUp += ColorShadingGrid_MouseLeftButtonUp;
                    _colorShadingGrid.MouseMove += ColorShadingGrid_MouseMove;
                }
            }
        }

        private SolidColorBrush _palleteSolidColorBrush;
        private SolidColorBrush PalleteSolidColorBrush
        {
            get { return _palleteSolidColorBrush; }
            set { _palleteSolidColorBrush = value; }
        }

        private EllipseGeometry _selectorPoint1;
        private EllipseGeometry SelectorPoint1
        {
            get { return _selectorPoint1; }
            set { _selectorPoint1 = value; }
        }

        private EllipseGeometry _selectorPoint2;
        private EllipseGeometry SelectorPoint2
        {
            get { return _selectorPoint2; }
            set { _selectorPoint2 = value; }
        }

        private Slider _slider_H;
        private Slider Slider_H
        {
            get { return _slider_H; }
            set
            {
                if (_slider_H != null)
                {
                    _slider_H.ValueChanged -= Slider_H_ValueChanged;
                }
                _slider_H = value;
                if (_slider_H != null)
                {
                    _slider_H.ValueChanged += Slider_H_ValueChanged;
                }
            }
        }

        private Slider _slider_A;
        private Slider Slider_A
        {
            get { return _slider_A; }
            set
            {
                if (_slider_A != null)
                {
                    _slider_A.ValueChanged -= Slider_A_ValueChanged;
                }
                _slider_A = value;
                if (_slider_A != null)
                {
                    _slider_A.ValueChanged += Slider_A_ValueChanged;
                }
            }
        }

        private Slider _slider_R;
        private Slider Slider_R
        {
            get { return _slider_R; }
            set
            {
                if (_slider_R != null)
                {
                    _slider_R.ValueChanged -= Slider_R_ValueChanged;
                }
                _slider_R = value;
                if (_slider_R != null)
                {
                    _slider_R.ValueChanged += Slider_R_ValueChanged;
                }
            }
        }

        private Slider _slider_G;
        private Slider Slider_G
        {
            get { return _slider_G; }
            set
            {
                if (_slider_G != null)
                {
                    _slider_G.ValueChanged -= Slider_G_ValueChanged;
                }
                _slider_G = value;
                if (_slider_G != null)
                {
                    _slider_G.ValueChanged += Slider_G_ValueChanged;
                }
            }
        }

        private Slider _slider_B;
        private Slider Slider_B
        {
            get { return _slider_B; }
            set
            {
                if (_slider_B != null)
                {
                    _slider_B.ValueChanged -= Slider_B_ValueChanged;
                }
                _slider_B = value;
                if (_slider_B != null)
                {
                    _slider_B.ValueChanged += Slider_B_ValueChanged;
                }
            }
        }

        private SolidColorBrush _sampleSolidColorBrush;
        private SolidColorBrush SampleSolidColorBrush
        {
            get { return _sampleSolidColorBrush; }
            set { _sampleSolidColorBrush = value; }
        }

        private TextBox _palleteColorTextBox;
        private TextBox PalleteColorTextBox
        {
            get { return _palleteColorTextBox; }
            set
            {
                if (_palleteColorTextBox != null)
                {
                    _palleteColorTextBox.TextChanged -= PalleteColorTextBox_TextChanged;
                }
                _palleteColorTextBox = value;
                if (_palleteColorTextBox != null)
                {
                    _palleteColorTextBox.TextChanged += PalleteColorTextBox_TextChanged;
                }
            }
        }

        private Button _okButton;
        private Button OkButton
        {
            get { return _okButton; }
            set
            {
                if (_okButton != null)
                {
                    _okButton.Click -= OkButton_Click;
                }
                _okButton = value;
                if (_okButton != null)
                {
                    _okButton.Click += OkButton_Click;
                }
            }
        }

        private Button _cancelButton;
        private Button CancelButton
        {
            get { return _cancelButton; }
            set
            {
                if (_cancelButton != null)
                {
                    _cancelButton.Click -= CancelButton_Click;
                }
                _cancelButton = value;
                if (_cancelButton != null)
                {
                    _cancelButton.Click += CancelButton_Click;
                }
            }
        }

        /// <summary>
        /// テンプレート適用時の処理
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.ThemeColors1ListBox = this.Template.FindName(PART_ThemeColors1ListBox, this) as ListBox;
            this.ThemeColors2ListBox = this.Template.FindName(PART_ThemeColors2ListBox, this) as ListBox;
            this.StandardColorsListBox = this.Template.FindName(PART_StandardColorsListBox, this) as ListBox;
            this.RecentColorsListBox = this.Template.FindName(PART_RecentColorsListBox, this) as ListBox;
            this.TransparentButton = this.Template.FindName(PART_TransparentButton, this) as Button;

            this.ColorShadingGrid = this.Template.FindName(PART_ColorShadingGrid, this) as Grid;
            this.PalleteSolidColorBrush = this.Template.FindName(PART_PalleteSolidColorBrush, this) as SolidColorBrush;
            this.SelectorPoint1 = this.Template.FindName(PART_SelectorPoint1, this) as EllipseGeometry;
            this.SelectorPoint2 = this.Template.FindName(PART_SelectorPoint2, this) as EllipseGeometry;
            this.Slider_H = this.Template.FindName(PART_Slider_H, this) as Slider;
            this.Slider_A = this.Template.FindName(PART_Slider_A, this) as Slider;
            this.Slider_R = this.Template.FindName(PART_Slider_R, this) as Slider;
            this.Slider_G = this.Template.FindName(PART_Slider_G, this) as Slider;
            this.Slider_B = this.Template.FindName(PART_Slider_B, this) as Slider;
            this.SampleSolidColorBrush = this.Template.FindName(PART_SampleSolidColorBrush, this) as SolidColorBrush;
            this.PalleteColorTextBox = this.Template.FindName(PART_PalleteColorTextBox, this) as TextBox;
            this.OkButton = this.Template.FindName(PART_OkButton, this) as Button;
            this.CancelButton = this.Template.FindName(PART_CancelButton, this) as Button;

            this._isTemplateApplyed = true;
            this.Cancel();
        }
        #endregion TemplatePart

        #region コンストラクタ
        /// <summary>
        /// 静的なコンストラクタです。
        /// </summary>
        static ColorPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPicker), new FrameworkPropertyMetadata(typeof(ColorPicker)));

            #region テーマカラーの初期化
            ThemeColors1 = new Collection<ColorPickerItem>()
            {
                new ColorPickerItem("白", "FFFFFFFF"),
                new ColorPickerItem("黒", "FF000000"),
                new ColorPickerItem("ベージュ", "FFEEECC1"),
                new ColorPickerItem("濃い青", "FF1F497D"),
                new ColorPickerItem("青", "FF4F81BD"),
                new ColorPickerItem("赤", "FFC0504D"),
                new ColorPickerItem("オリーブ", "FF9BBB59"),
                new ColorPickerItem("紫", "FF8064A2"),
                new ColorPickerItem("アクア", "FF4BACC6"),
                new ColorPickerItem("オレンジ", "FFF79646"),
            };

            ThemeColors2 = new Collection<ColorPickerItem>()
            {
                new ColorPickerItem("白 + 黒 5%", "FFF2F2F2"),
                new ColorPickerItem("黒 + 白 50%", "FF7F7F7F"),
                new ColorPickerItem("ベージュ + 黒 10%", "FFDDD9C3"),
                new ColorPickerItem("濃い青 + 白 80%", "FFC6D9F0"),
                new ColorPickerItem("青 + 白 80%", "FFDBE5F1"),
                new ColorPickerItem("赤 + 白 80%", "FFF2DCDB"),
                new ColorPickerItem("オリーブ + 白 80%", "FFEBF1DD"),
                new ColorPickerItem("紫 + 白 80%", "FFE5E0EC"),
                new ColorPickerItem("アクア + 白 80%", "FFDBEEF3"),
                new ColorPickerItem("オレンジ + 白 80%", "FFFDEADA"),

                new ColorPickerItem("白 + 黒 15%", "FFD8D8D8"),
                new ColorPickerItem("黒 + 白 35%", "FF595959"),
                new ColorPickerItem("ベージュ + 黒 25%", "FFC4BD97"),
                new ColorPickerItem("濃い青 + 白 60%", "FF8DB3E2"),
                new ColorPickerItem("青 + 白 60%", "FFB8CCE4"),
                new ColorPickerItem("赤 + 白 60%", "FFE5B9B7"),
                new ColorPickerItem("オリーブ + 白 60%", "FFD7E3BC"),
                new ColorPickerItem("紫 + 白 60%", "FFCCC1D9"),
                new ColorPickerItem("アクア + 白 60%", "FFB7DDE8"),
                new ColorPickerItem("オレンジ + 白 60%", "FFFBD5B5"),

                new ColorPickerItem("白 + 黒 25%", "FFBFBFBF"),
                new ColorPickerItem("黒 + 白 25%", "FF3F3F3F"),
                new ColorPickerItem("ベージュ + 黒 50%", "FF938953"),
                new ColorPickerItem("濃い青 + 白 40%", "FF548DD4"),
                new ColorPickerItem("青 + 白 40%", "FF95B3D7"),
                new ColorPickerItem("赤 + 白 40%", "FFD99694"),
                new ColorPickerItem("オリーブ + 白 40%", "FFC3D69B"),
                new ColorPickerItem("紫 + 白 40%", "FFB2A2C7"),
                new ColorPickerItem("アクア + 白 40%", "FF92CDDC"),
                new ColorPickerItem("オレンジ + 白 40%", "FFFAC08F"),

                new ColorPickerItem("白 + 黒 35%", "FFA5A5A5"),
                new ColorPickerItem("黒 + 白 15%", "FF262626"),
                new ColorPickerItem("ベージュ + 黒 75%", "FF494429"),
                new ColorPickerItem("濃い青 + 黒 25%", "FF17365D"),
                new ColorPickerItem("青 + 黒 25%", "FF366092"),
                new ColorPickerItem("赤 + 黒 25%", "FF953734"),
                new ColorPickerItem("オリーブ + 黒 25%", "FF76923C"),
                new ColorPickerItem("紫 + 黒 25%", "FF5F497A"),
                new ColorPickerItem("アクア + 黒 25%", "FF31859B"),
                new ColorPickerItem("オレンジ + 黒 25%", "FFE36C09"),

                new ColorPickerItem("白 + 黒 50%", "FF7F7F7F"),
                new ColorPickerItem("黒 + 白 5%", "FF0C0C0C"),
                new ColorPickerItem("ベージュ + 黒 90%", "FF1D1B10"),
                new ColorPickerItem("濃い青 + 黒 50%", "FF0F243E"),
                new ColorPickerItem("青 + 黒 50%", "FF244061"),
                new ColorPickerItem("赤 + 黒 50%", "FF632423"),
                new ColorPickerItem("オリーブ + 黒 50%", "FF4F6128"),
                new ColorPickerItem("紫 + 黒 50%", "FF3F3151"),
                new ColorPickerItem("アクア + 黒 50%", "FF205867"),
                new ColorPickerItem("オレンジ + 黒 50%", "FF974806"),
            };
            #endregion テーマカラーの初期化

            #region 標準カラーの初期化
            StandardColors = new Collection<ColorPickerItem>()
            {
                new ColorPickerItem("濃い赤", "FFC00000"),
                new ColorPickerItem("赤", "FFFF0000"),
                new ColorPickerItem("オレンジ", "FFFFC000"),
                new ColorPickerItem("黄", "FFFFFF00"),
                new ColorPickerItem("薄い緑", "FF92D050"),
                new ColorPickerItem("緑", "FF00B050"),
                new ColorPickerItem("薄い青", "FF00B0F0"),
                new ColorPickerItem("青", "FF0070C0"),
                new ColorPickerItem("濃い青", "FF002060"),
                new ColorPickerItem("紫", "FF7030A0"),
            };
            #endregion 標準カラーの初期化

            RecentColors = new ObservableCollection<ColorPickerItem>();
        }
        #endregion コンストラクタ

        #region SelectedColor 依存関係プロパティ
        /// <summary>
        /// SelectedColor 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorPicker), new FrameworkPropertyMetadata(default(Color), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedColorPropertyChanged));

        /// <summary>
        /// 選択色を取得または設定します。
        /// </summary>
        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        /// <summary>
        /// SelectedColor プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnSelectedColorPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as ColorPicker;
            if (control == null)
                return;
            if (!control._isTemplateApplyed)
                return;

            var color = (Color)e.NewValue;
            ColorPickerItem item;
            if ((item = (ThemeColors1 as Collection<ColorPickerItem>).FirstOrDefault(x => x.Color == color)) != null)
            {
                control.SetCollectionItem(item, null, null, item);
            }
            else if ((item = (ThemeColors2 as Collection<ColorPickerItem>).FirstOrDefault(x => x.Color == color)) != null)
            {
                control.SetCollectionItem(null, item, null, item);
            }
            else if ((item = (StandardColors as Collection<ColorPickerItem>).FirstOrDefault(x => x.Color == color)) != null)
            {
                control.SetCollectionItem(null, null, item, item);
            }
            else if ((item = (RecentColors as Collection<ColorPickerItem>).FirstOrDefault(x => x.Color == color)) != null)
            {
                control.SetCollectionItem(null, null, null, item);
            }
            else
            {
                item = new ColorPickerItem(color);
                control.SetCollectionItem(null, null, null, item);
            }

            if (control._isSelectedColorChangedFromUI)
            {
                control._isRgbColorChangedFromUI = false;
                control._isSampleColorTextChangedFromUI = false;
                control._isHsvColorChangedFromUI = false;
                control.PalleteColor = color;
                control._isRgbColorChangedFromUI = true;
                control._isSampleColorTextChangedFromUI = true;
                control._isHsvColorChangedFromUI = true;
            }
        }
        #endregion SelectedColor 依存関係プロパティ

        #region RecentColorsVisibility 依存関係プロパティ
        /// <summary>
        /// RecentColorsVisibility 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty RecentColorsVisibilityProperty = DependencyProperty.Register("RecentColorsVisibility", typeof(Visibility), typeof(ColorPicker), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// 色選択履歴を表示するかどうかを取得または設定します。
        /// </summary>
        public Visibility RecentColorsVisibility
        {
            get { return (Visibility)GetValue(RecentColorsVisibilityProperty); }
            set { SetValue(RecentColorsVisibilityProperty, value); }
        }
        #endregion RecentColorsVisibility 依存関係プロパティ

        #region IsAdvancedModeEnabled 依存関係プロパティ
        /// <summary>
        /// IsAdvancedModeEnabled 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsAdvancedModeEnabledProperty = DependencyProperty.Register("IsAdvancedModeEnabled", typeof(bool), typeof(ColorPicker), new PropertyMetadata(true));

        /// <summary>
        /// Advanced モードが有効かどうかを取得または設定します。
        /// </summary>
        public bool IsAdvancedModeEnabled
        {
            get { return (bool)GetValue(IsAdvancedModeEnabledProperty); }
            set { SetValue(IsAdvancedModeEnabledProperty, value); }
        }
        #endregion IsAdvancedModeEnabled 依存関係プロパティ

        #region IsAlphaValueEnabled 依存関係プロパティ
        /// <summary>
        /// IsAdvancedModeEnabled 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsAlphaValueEnabledProperty = DependencyProperty.Register("IsAlphaValueEnabled", typeof(bool), typeof(ColorPicker), new FrameworkPropertyMetadata(true, OnIsAlphaValueEnabledPropertyChanged));

        /// <summary>
        /// Alpha 値が有効かどうかを取得または設定します。
        /// </summary>
        public bool IsAlphaValueEnabled
        {
            get { return (bool)GetValue(IsAlphaValueEnabledProperty); }
            set { SetValue(IsAlphaValueEnabledProperty, value); }
        }

        /// <summary>
        /// IsAlphaValueEnabled プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnIsAlphaValueEnabledPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as ColorPicker;

            if (control._isTemplateApplyed)
            {
                control._isSampleColorTextChangedFromUI = false;
                control._isHsvColorChangedFromUI = false;
                control.UpdatePalleteColor();
                control._isSampleColorTextChangedFromUI = true;
                control._isHsvColorChangedFromUI = true;
            }
        }
        #endregion IsAlphaValueEnabled 依存関係プロパティ

        #region 公開静的プロパティ
        /// <summary>
        /// テーマカラー 1 を取得します。
        /// </summary>
        public static IEnumerable ThemeColors1 { get; private set; }

        /// <summary>
        /// テーマカラー 2 を取得します。
        /// </summary>
        public static IEnumerable ThemeColors2 { get; private set; }

        /// <summary>
        /// 標準カラーを取得します。
        /// </summary>
        public static IEnumerable StandardColors { get; private set; }

        /// <summary>
        /// 標準カラーを取得します。
        /// </summary>
        public static IEnumerable RecentColors { get; private set; }
        #endregion 公開静的プロパティ

        #region イベントハンドラ

        #region Standard 色変更処理
        /// <summary>
        /// ThemeColors1ListBox 選択アイテム変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void ThemeColors1ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var item = e.AddedItems[0] as ColorPickerItem;
                if (item != null)
                {
                    SetCollectionItem(item, null, null, item);
                    this.SelectedColor = item.Color;
                }
            }
        }

        /// <summary>
        /// ThemeColors2ListBox 選択アイテム変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void ThemeColors2ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var item = e.AddedItems[0] as ColorPickerItem;
                if (item != null)
                {
                    SetCollectionItem(null, item, null, item);
                    this.SelectedColor = item.Color;
                }
            }
        }

        /// <summary>
        /// StandardColorsListBox 選択アイテム変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void StandardColorsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var item = e.AddedItems[0] as ColorPickerItem;
                if (item != null)
                {
                    SetCollectionItem(null, null, item, item);
                    this.SelectedColor = item.Color;
                }
            }
        }

        /// <summary>
        /// RecentColorsListBox 選択アイテム変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void RecentColorsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isRecentColorChangedFromUI)
                return;

            if (e.AddedItems.Count > 0)
            {
                var item = e.AddedItems[0] as ColorPickerItem;
                if (item != null)
                {
                    SetCollectionItem(null, null, null, item);
                    this.SelectedColor = item.Color;
                }
            }
        }

        /// <summary>
        /// TransparentButton クリックイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void TransparentButton_Click(object sender, RoutedEventArgs e)
        {
            this.SetCollectionItem(null, null, null, this._transparentItem);
            this.SelectedColor = this._transparentItem.Color;
        }
        #endregion Standard 色変更処理

        #region UI からの RgbColor 変更処理
        /// <summary>
        /// Slider_A 値変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void Slider_A_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this._isRgbColorChangedFromUI)
            {
                //var color = this.PalleteColor;
                //color.A = (byte)this.Slider_A.Value;
                //this.PalleteColor = color;

                this._isSampleColorTextChangedFromUI = false;
                this._isHsvColorChangedFromUI = false;
                UpdatePalleteColor();
                this._isSampleColorTextChangedFromUI = true;
                this._isHsvColorChangedFromUI = true;
            }
        }

        /// <summary>
        /// Slider_R 値変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void Slider_R_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this._isRgbColorChangedFromUI)
            {
                this._isSampleColorTextChangedFromUI = false;
                this._isHsvColorChangedFromUI = false;
                UpdatePalleteColor();
                this._isSampleColorTextChangedFromUI = true;
                this._isHsvColorChangedFromUI = true;
            }
        }

        /// <summary>
        /// Slider_G 値変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void Slider_G_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this._isRgbColorChangedFromUI)
            {
                this._isSampleColorTextChangedFromUI = false;
                this._isHsvColorChangedFromUI = false;
                UpdatePalleteColor();
                this._isSampleColorTextChangedFromUI = true;
                this._isHsvColorChangedFromUI = true;
            }
        }

        /// <summary>
        /// Slider_B 値変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void Slider_B_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this._isRgbColorChangedFromUI)
            {
                this._isSampleColorTextChangedFromUI = false;
                this._isHsvColorChangedFromUI = false;
                UpdatePalleteColor();
                this._isSampleColorTextChangedFromUI = true;
                this._isHsvColorChangedFromUI = true;
            }
        }

        /// <summary>
        /// SampleColorTextBox テキスト変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void PalleteColorTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_isSampleColorTextChangedFromUI)
                return;

            try
            {
                var str = this.PalleteColorTextBox.Text;
                if (!string.IsNullOrWhiteSpace(str))
                {
                    if (str[0] != '#')
                        throw new Exception();

                    var colorCode = str.Substring(1, str.Length - 1);

                    this._isRgbColorChangedFromUI = false;
                    this._isHsvColorChangedFromUI = false;
                    this.PalleteColor = ColorPickerItem.ColorFromCode(colorCode);
                    this._isRgbColorChangedFromUI = true;
                    this._isHsvColorChangedFromUI = true;

                    this.OkButton.IsEnabled = true;
                }
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err);
                this.OkButton.IsEnabled = false;
            }
        }
        #endregion UI からの RgbColor 変更処理

        #region UI からの HsvColor 変更処理
        /// <summary>
        /// 色選択パレットマウス左ボタン押下イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void ColorShadingGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.ColorShadingGrid.CaptureMouse();
            var pt = e.GetPosition(this.ColorShadingGrid);
            UpdateSelectorPointFromMouseEvent(pt);
        }

        /// <summary>
        /// 色選択パレットマウス左ボタンリリースイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void ColorShadingGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.ColorShadingGrid.ReleaseMouseCapture();
        }

        /// <summary>
        /// 色選択パレットマウス移動イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void ColorShadingGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.ColorShadingGrid.IsMouseCaptured)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    var pt = e.GetPosition(this.ColorShadingGrid);
                    UpdateSelectorPointFromMouseEvent(pt);
                }
            }
        }

        /// <summary>
        /// マウスイベントによるパレット選択位置の更新
        /// </summary>
        /// <param name="pt"></param>
        private void UpdateSelectorPointFromMouseEvent(Point pt)
        {
            if (pt.X < 0.0)
            {
                pt.X = 0.0;
            }
            else if (pt.X > this.ColorShadingGrid.ActualWidth)
            {
                pt.X = this.ColorShadingGrid.ActualWidth;
            }
            if (pt.Y < 0.0)
            {
                pt.Y = 0.0;
            }
            else if (pt.Y > this.ColorShadingGrid.ActualHeight)
            {
                pt.Y = this.ColorShadingGrid.ActualHeight;
            }
            this.SelectorPoint = pt;

            if (this._isHsvColorChangedFromUI)
            {
                this._isRgbColorChangedFromUI = false;
                this._isSampleColorTextChangedFromUI = false;
                UpdatePalleteColorFromHsvColor();
                this._isRgbColorChangedFromUI = true;
                this._isSampleColorTextChangedFromUI = true;
            }
        }

        /// <summary>
        /// Slider_H 値変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void Slider_H_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this._isHsvColorChangedFromUI)
            {
                this._isRgbColorChangedFromUI = false;
                this._isSampleColorTextChangedFromUI = false;
                UpdatePalleteColorFromHsvColor();
                this._isRgbColorChangedFromUI = true;
                this._isSampleColorTextChangedFromUI = true;
            }
        }
        #endregion UI からの HsvColor 変更処理

        /// <summary>
        /// OkButton クリックイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if ((this.SelectedColor == this.PalleteColor) && ((RecentColors as Collection<ColorPickerItem>).Count == 0))
            {
                var item = new ColorPickerItem(this.PalleteColor);
                this.SetCollectionItem(null, null, null, item);
            }
            else
            {
                this._isSelectedColorChangedFromUI = false;
                this.SelectedColor = this.PalleteColor;
                this._isSelectedColorChangedFromUI = true;
            }
        }

        /// <summary>
        /// CancelButton クリックイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Cancel();
        }

        /// <summary>
        /// キャンセル処理をおこないます。
        /// </summary>
        private void Cancel()
        {
            this._isRgbColorChangedFromUI = false;
            this._isHsvColorChangedFromUI = false;
            this._isSampleColorTextChangedFromUI = false;
            this.PalleteColor = this.SelectedColor;
            this._isRgbColorChangedFromUI = true;
            this._isHsvColorChangedFromUI = true;
            this._isSampleColorTextChangedFromUI = true;
        }

        #endregion イベントハンドラ

        #region ヘルパ
        /// <summary>
        /// 選択色変更ヘルパ
        /// </summary>
        /// <param name="item1">テーマカラー 1 の選択色を指定します。</param>
        /// <param name="item2">テーマカラー 2 の選択色を指定します。</param>
        /// <param name="item3">標準カラーの選択色を指定します。</param>
        /// <param name="item4">色選択履歴の選択色を指定します。</param>
        private void SetCollectionItem(ColorPickerItem item1, ColorPickerItem item2, ColorPickerItem item3, ColorPickerItem item4)
        {
            if (ThemeColors1ListBox != null) ThemeColors1ListBox.SelectedItem = item1;
            if (ThemeColors2ListBox != null) ThemeColors2ListBox.SelectedItem = item2;
            if (StandardColorsListBox != null) StandardColorsListBox.SelectedItem = item3;
            if (RecentColorsListBox != null)
            {
                if (item4 != null)
                {
                    var collection = RecentColors as Collection<ColorPickerItem>;
                    var index = collection.IndexOf(item4);
                    if (index < 0)
                    {
                        collection.Insert(0, item4);
                    }
                    else
                    {
                        var item = collection[index];
                        collection.Remove(item);
                        collection.Insert(0, item);
                    }
                    if (collection.Count > 10)
                    {
                        collection.RemoveAt(collection.Count - 1);
                    }

                    if ((item1 == null) && (item2 == null) && (item3 == null))
                    {
                        _isRecentColorChangedFromUI = false;
                        RecentColorsListBox.SelectedItem = item4;
                        _isRecentColorChangedFromUI = true;
                    }
                    else
                    {
                        _isRecentColorChangedFromUI = false;
                        RecentColorsListBox.SelectedItem = null;
                        _isRecentColorChangedFromUI = true;
                    }
                }
            }
        }

        /// <summary>
        /// パレット指定色変更
        /// </summary>
        private void UpdatePalleteColor()
        {
            PalleteColor = Color.FromArgb(this.IsAlphaValueEnabled ? (byte)this.Slider_A.Value : (byte)0xFF, (byte)this.Slider_R.Value, (byte)this.Slider_G.Value, (byte)this.Slider_B.Value);
        }

        /// <summary>
        /// パレット指定色変更
        /// </summary>
        private void UpdatePalleteColorFromHsvColor()
        {
            var pt = new Point(this.SelectorPoint.X / this.ColorShadingGrid.ActualWidth, this.SelectorPoint.Y / this.ColorShadingGrid.ActualHeight);
            var color = CalculateColor(pt);
            color.A = PalleteColor.A;

            PalleteColor = color;
        }

        /// <summary>
        /// HSV 指定値から RGB 値を算出する
        /// </summary>
        /// <param name="p">カラーパレット上の正規化現在位置</param>
        /// <returns>算出された色</returns>
        private Color CalculateColor(Point p)
        {
            double h = 360.0 - this.Slider_H.Value;
            double s = p.X;
            double v = 1.0 - p.Y;

            return new HsvColor(h, s, v).ColorFromHsv();
        }
        #endregion ヘルパ

        #region private フィールド
        /// <summary>
        /// 初期化されたかどうか
        /// </summary>
        private bool _isTemplateApplyed;

        /// <summary>
        /// 選択色が UI から変更されたかどうか
        /// </summary>
        private bool _isSelectedColorChangedFromUI = true;

        /// <summary>
        /// 色選択履歴の選択色が UI から変更されたかどうか
        /// </summary>
        private bool _isRecentColorChangedFromUI = true;

        /// <summary>
        /// Advanced モードで RGB 指定の色が UI から変更されたかどうか
        /// </summary>
        private bool _isRgbColorChangedFromUI = true;

        /// <summary>
        /// Advanced モードで HSV 指定の色が UI から変更されたかどうか
        /// </summary>
        private bool _isHsvColorChangedFromUI = true;

        /// <summary>
        /// SampleColorTextBox のテキストが UI から変更されたかどうか
        /// </summary>
        private bool _isSampleColorTextChangedFromUI = true;

        /// <summary>
        /// 透明色アイテム
        /// </summary>
        private ColorPickerItem _transparentItem = new ColorPickerItem("透明", "00000000");
        #endregion private フィールド

        #region private プロパティ
        private Color _palleteColor;
        /// <summary>
        /// パレット選択色
        /// </summary>
        private Color PalleteColor
        {
            get { return _palleteColor; }
            set
            {
                _palleteColor = value;

                // RGB 値変更
                if (!_isRgbColorChangedFromUI)
                {
                    this.Slider_A.Value = _palleteColor.A;
                    this.Slider_R.Value = _palleteColor.R;
                    this.Slider_G.Value = _palleteColor.G;
                    this.Slider_B.Value = _palleteColor.B;
                }

                // カラーコードのテキスト変更
                if (!this._isSampleColorTextChangedFromUI)
                {
                    var code = string.Format("#{0}{1:X2}{2:X2}{3:X2}", this.IsAlphaValueEnabled ? _palleteColor.A.ToString("X2") : "", _palleteColor.R, _palleteColor.G, _palleteColor.B);
                    PalleteColorTextBox.Text = code;
                }

                // HSV 値変更
                if (!_isHsvColorChangedFromUI)
                {
                    var hsvColor = _palleteColor.HsvColorFromRgb();

                    if (!(_palleteColor.R == _palleteColor.G && _palleteColor.R == _palleteColor.B))
                        this.Slider_H.Value = hsvColor.H;

                    var pt = new Point(hsvColor.S * this.ColorShadingGrid.Width, this.ColorShadingGrid.Height - hsvColor.V * this.ColorShadingGrid.Height);
                    this.SelectorPoint = pt;
                }

                this.SampleSolidColorBrush.Color = _palleteColor;
                var backgroundColor = new HsvColor(360.0 - this.Slider_H.Value, 1.0, 1.0).ColorFromHsv();
                this.PalleteSolidColorBrush.Color = backgroundColor;
            }
        }

        private Point _selectorPoint;
        /// <summary>
        /// パレット選択位置
        /// </summary>
        private Point SelectorPoint
        {
            get { return _selectorPoint; }
            set
            {
                _selectorPoint = value;
                this.SelectorPoint1.Center = _selectorPoint;
                this.SelectorPoint2.Center = _selectorPoint;
            }
        }
        #endregion private プロパティ
    }
}
