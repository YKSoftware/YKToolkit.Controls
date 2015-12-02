namespace YKToolkit.Controls
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using System.Windows.Threading;
    using YKToolkit.Controls.Behaviors;

    /// <summary>
    /// バイナリエディタを表します。
    /// </summary>
    [TemplatePart(Name = PART_BinaryTable, Type = typeof(BinaryEditorTable))]
    [TemplatePart(Name = PART_VerticalScrollBar, Type = typeof(ScrollBar))]
    [TemplatePart(Name = PART_CursorRectangle, Type = typeof(Rectangle))]
    [TemplatePart(Name = PART_InputTextBox, Type = typeof(TextBox))]
    public class BinaryEditor : Control, IInputElement
    {
        #region TemplatePart
        private const string PART_BinaryTable = "PART_BinaryTable";
        private const string PART_VerticalScrollBar = "PART_VerticalScrollBar";
        private const string PART_CursorRectangle = "PART_CursorRectangle";
        private const string PART_InputTextBox = "PART_InputTextBox";

        private BinaryEditorTable _binaryTable;
        private BinaryEditorTable BinaryTable
        {
            get { return this._binaryTable; }
            set { this._binaryTable = value; }
        }

        private ScrollBar _verticalScrollBar;
        private ScrollBar VerticalScrollBar
        {
            get { return this._verticalScrollBar; }
            set
            {
                if (this._verticalScrollBar != null)
                {
                    this._verticalScrollBar.ValueChanged -= OnVerticalScrollBarValueChanged;
                }
                this._verticalScrollBar = value;
                if (this._verticalScrollBar != null)
                {
                    this._verticalScrollBar.ValueChanged += OnVerticalScrollBarValueChanged;
                }
            }
        }

        private Rectangle _cursorRectangle;
        private Rectangle CursorRectangle
        {
            get { return this._cursorRectangle; }
            set
            {
                this._cursorRectangle = value;
                if (this._cursorRectangle != null)
                {
                    this._cursorRectangle.Fill = new SolidColorBrush(Colors.Transparent);
                }
            }
        }

        private TextBox _inputTextBox;
        private TextBox InputTextBox
        {
            get { return this._inputTextBox; }
            set
            {
                if (this._inputTextBox != null)
                {
                    this._inputTextBox.KeyDown -= OnInputTextBoxKeyDown;
                    this._inputTextBox.TextChanged -= OnInputTextBoxTextChanged;
                }
                this._inputTextBox = value;
                if (this._inputTextBox != null)
                {
                    this._inputTextBox.TextAlignment = TextAlignment.Right;
                    this._inputTextBox.Visibility = Visibility.Collapsed;
                    TextBoxAllSelectBehavior.SetIsEnabled(this._inputTextBox, false);

                    this._inputTextBox.KeyDown += OnInputTextBoxKeyDown;
                    this._inputTextBox.TextChanged += OnInputTextBoxTextChanged;
                }
            }
        }

        /// <summary>
        /// テンプレート適用時の処理
        /// </summary>
        public override void OnApplyTemplate()
        {
            this.BinaryTable = this.Template.FindName(PART_BinaryTable, this) as BinaryEditorTable;
            this.VerticalScrollBar = this.Template.FindName(PART_VerticalScrollBar, this) as ScrollBar;
            this.CursorRectangle = this.Template.FindName(PART_CursorRectangle, this) as Rectangle;
            this.InputTextBox = this.Template.FindName(PART_InputTextBox, this) as TextBox;
        }
        #endregion TemplatePart

        #region コンストラクタ
        /// <summary>
        /// 静的なコンストラクタです。
        /// </summary>
        static BinaryEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BinaryEditor), new FrameworkPropertyMetadata(typeof(BinaryEditor)));
        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public BinaryEditor()
        {
            this._timer = new DispatcherTimer(TimeSpan.FromMilliseconds(500), DispatcherPriority.Normal, OnCursorTimerTick, this.Dispatcher);
            this._timer.Start();

            this._updateTimer = new DispatcherTimer(TimeSpan.FromMilliseconds(10), DispatcherPriority.Normal, OnStartLazyUpdateCallback, this.Dispatcher);

            this.MouseLeftButtonUp += OnMouseLeftButtonUp;
            this.GotFocus += OnGotFocus;
            this.LostFocus += OnLostFocus;
        }
        #endregion コンストラクタ

        #region Data 依存関係プロパティ
        /// <summary>
        /// Data 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(ObservableCollection<byte>), typeof(BinaryEditor), new FrameworkPropertyMetadata(null, OnDataPropertyChanged));

        /// <summary>
        /// データを取得または設定します。
        /// </summary>
        public ObservableCollection<byte> Data
        {
            get { return (ObservableCollection<byte>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        /// <summary>
        /// Data プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnDataPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as BinaryEditor;
            if (control == null)
                return;

            control.OnDataPropertyChanged(e.OldValue as ObservableCollection<byte>, e.NewValue as ObservableCollection<byte>);
        }

        /// <summary>
        /// Data プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private void OnDataPropertyChanged(ObservableCollection<byte> oldValue, ObservableCollection<byte> newValue)
        {
            UpdateScrollBarInfo();
        }
        #endregion Data 依存関係プロパティ

        #region TopAddress 依存関係プロパティ
        /// <summary>
        /// TopAddress 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty TopAddressProperty = DependencyProperty.Register("TopAddress", typeof(int), typeof(BinaryEditor), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTopAddressPropertyChanged));

        /// <summary>
        /// 先頭アドレスを取得または設定します。
        /// </summary>
        public int TopAddress
        {
            get { return (int)GetValue(TopAddressProperty); }
            set { SetValue(TopAddressProperty, value); }
        }

        /// <summary>
        /// TopAddress プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnTopAddressPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as BinaryEditor;
            if (control == null)
                return;

            control.OnTopAddressPropertyChanged((int)e.OldValue, (int)e.NewValue);
        }

        /// <summary>
        /// TopAddress プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private void OnTopAddressPropertyChanged(int oldValue, int newValue)
        {
            ClearInputTextBox();
        }
        #endregion TopAddress 依存関係プロパティ

        #region AddressOffset 依存関係プロパティ
        /// <summary>
        /// AddressOffset 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty AddressOffsetProperty = DependencyProperty.Register("AddressOffset", typeof(int), typeof(BinaryEditor), new FrameworkPropertyMetadata(0, OnAddressOffsetPropertyChanged));

        /// <summary>
        /// アドレスオフセットを取得または設定します。
        /// </summary>
        public int AddressOffset
        {
            get { return (int)GetValue(AddressOffsetProperty); }
            set { SetValue(AddressOffsetProperty, value); }
        }

        /// <summary>
        /// AddressOffset プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnAddressOffsetPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as BinaryEditor;
            if (control == null)
                return;

            control.OnAddressOffsetPropertyChanged((int)e.OldValue, (int)e.NewValue);
        }

        /// <summary>
        /// AddressOffset プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private void OnAddressOffsetPropertyChanged(int oldValue, int newValue)
        {
            ClearInputTextBox();
        }
        #endregion AddressOffset 依存関係プロパティ

        #region VisibleLines 依存関係プロパティ
        /// <summary>
        /// VisibleLines 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty VisibleLinesProperty = DependencyProperty.Register("VisibleLines", typeof(int), typeof(BinaryEditor), new FrameworkPropertyMetadata(16, OnVisibleLinesPropertyChanged));

        /// <summary>
        /// 表示行数を取得または設定します。
        /// </summary>
        public int VisibleLines
        {
            get { return (int)GetValue(VisibleLinesProperty); }
            set { SetValue(VisibleLinesProperty, value); }
        }

        /// <summary>
        /// VisibleLines プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnVisibleLinesPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as BinaryEditor;
            if (control == null)
                return;

            control.OnVisibleLinesPropertyChanged((int)e.OldValue, (int)e.NewValue);
        }

        /// <summary>
        /// VisibleLines プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private void OnVisibleLinesPropertyChanged(int oldValue, int newValue)
        {
            ClearInputTextBox();
            UpdateScrollBarInfo();
        }
        #endregion VisibleLines 依存関係プロパティ

        #region DataStyle 依存関係プロパティ
        /// <summary>
        /// DataStyle 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty DataStyleProperty = DependencyProperty.Register("DataStyle", typeof(DataStyles), typeof(BinaryEditor), new FrameworkPropertyMetadata(DataStyles.Byte, OnDataStylePropertyChanged));

        /// <summary>
        /// データ表現を取得または設定します。
        /// </summary>
        public DataStyles DataStyle
        {
            get { return (DataStyles)GetValue(DataStyleProperty); }
            set { SetValue(DataStyleProperty, value); }
        }

        /// <summary>
        /// DataStyle プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnDataStylePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as BinaryEditor;
            if (control == null)
                return;

            control.OnDataStylePropertyChanged((DataStyles)e.OldValue, (DataStyles)e.NewValue);
        }

        /// <summary>
        /// DataStyle プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private void OnDataStylePropertyChanged(DataStyles oldValue, DataStyles newValue)
        {
            ClearInputTextBox();
        }
        #endregion DataStyle 依存関係プロパティ

        #region NumStyle 依存関係プロパティ
        /// <summary>
        /// NumStyle 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty NumStyleProperty = DependencyProperty.Register("NumStyle", typeof(NumStyles), typeof(BinaryEditor), new FrameworkPropertyMetadata(NumStyles.Hexadecimal, OnNumStylePropertyChanged));

        /// <summary>
        /// 数値表現を取得または設定します。
        /// </summary>
        public NumStyles NumStyle
        {
            get { return (NumStyles)GetValue(NumStyleProperty); }
            set { SetValue(NumStyleProperty, value); }
        }

        /// <summary>
        /// NumStyle プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnNumStylePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as BinaryEditor;
            if (control == null)
                return;

            control.OnNumStylePropertyChanged((NumStyles)e.OldValue, (NumStyles)e.NewValue);
        }

        /// <summary>
        /// NumStyle プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private void OnNumStylePropertyChanged(NumStyles oldValue, NumStyles newValue)
        {
            ClearInputTextBox();
        }
        #endregion NumStyle 依存関係プロパティ

        #region SelectedAddress 依存関係プロパティ
        /// <summary>
        /// SelectedAddress 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty SelectedAddressProperty = DependencyProperty.Register("SelectedAddress", typeof(int), typeof(BinaryEditor), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.None, OnSelectedAddressPropertyChanged, OnSelectedAddressPropertyCoerceCallback));

        /// <summary>
        /// 選択しているアドレスを取得または設定します。
        /// </summary>
        public int SelectedAddress
        {
            get { return (int)GetValue(SelectedAddressProperty); }
            set { SetValue(SelectedAddressProperty, value); }
        }

        /// <summary>
        /// SelectedAddress プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnSelectedAddressPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as BinaryEditor;
            if (control == null)
                return;

            control.OnSelectedAddressPropertyChanged((int)e.OldValue, (int)e.NewValue);
        }

        /// <summary>
        /// SelectedAddress プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private void OnSelectedAddressPropertyChanged(int oldValue, int newValue)
        {
            ClearInputTextBox();
        }

        /// <summary>
        /// SelectedAddress プロパティ値検証イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="baseValue">検証値</param>
        /// <returns>検証後の値</returns>
        private static object OnSelectedAddressPropertyCoerceCallback(DependencyObject sender, object baseValue)
        {
            var control = sender as BinaryEditor;
            if (control == null)
                return baseValue;

            return control.OnSelectedAddressPropertyCoerceCallback((int)baseValue);
        }

        /// <summary>
        /// SelectedAddress プロパティ値検証イベントハンドラ
        /// </summary>
        /// <param name="value">検証値</param>
        /// <returns>検証後の値</returns>
        private int OnSelectedAddressPropertyCoerceCallback(int value)
        {
            if (value < 0) value = 0;
            if ((this.Data != null) && (value > this.Data.Count)) value = this.Data.Count;
            return ContainAddress(value) ? value : this.TopAddress;
        }
        #endregion SelectedAddress 依存関係プロパティ

        #region イベントハンドラ

        /// <summary>
        /// VerticalScrollBar の Value プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnVerticalScrollBarValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.TopAddress = (int)(this.VerticalScrollBar.Value / 16) * 16;
        }

        /// <summary>
        /// カーソル点滅用タイマー Tick イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnCursorTimerTick(object sender, EventArgs e)
        {
            if (this.CursorRectangle == null)
                return;
            if (!(this.CursorRectangle.Fill is SolidColorBrush))
                return;

            if (Keyboard.FocusedElement == this)
            {
                if ((this.CursorRectangle.Fill as SolidColorBrush).Color != Colors.Transparent)
                {
                    ClearCusorColor();
                }
                else
                {
                    SetCusorColor();
                }
            }
            else
            {
                ClearCusorColor();
            }
        }

        /// <summary>
        /// カーソル位置更新用タイマー Tick イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnStartLazyUpdateCallback(object sender, EventArgs e)
        {
            var origin = this.BinaryTable.DataOrigin;
            var size = this.BinaryTable.DataUnitSize;

            var offset = this.SelectedAddress - this.TopAddress;
            var offset_y = (int)(offset / 16);
            var offset_x = (offset - (offset_y * 16)) / (int)this.DataStyle;

            this.CursorRectangle.Width = size.Width;
            this.CursorRectangle.Height = size.Height;
            Canvas.SetLeft(this.CursorRectangle, origin.X + offset_x * size.Width);
            Canvas.SetTop(this.CursorRectangle, origin.Y + offset_y * size.Height);

            this.InputTextBox.Width = size.Width + 8;
            this.InputTextBox.Height = size.Height + 8;
            Canvas.SetLeft(this.InputTextBox, origin.X + offset_x * size.Width - 4);
            Canvas.SetTop(this.InputTextBox, origin.Y + offset_y * size.Height - 4);

            this._updateTimer.Stop();
        }

        /// <summary>
        /// MouseLeftButtonUp イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Keyboard.Focus(this);
        }

        /// <summary>
        /// フォーカス取得イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnGotFocus(object sender, RoutedEventArgs e)
        {
            this._updateTimer.Start();
        }

        /// <summary>
        /// フォーカス消失イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            ClearCusorColor();
            this._updateTimer.Stop();
        }

        /// <summary>
        /// OnKeyDown イベントハンドラのオーバーライド
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (this.InputTextBox.IsFocused)
                return;

            int address;

            switch (e.Key)
            {
                case Key.Left:
                    if (Keyboard.Modifiers == ModifierKeys.None)
                    {
                        MoveCursor(-1 * (int)this.DataStyle);
                        e.Handled = true;
                        return;
                    }
                    break;

                case Key.Up:
                    if (Keyboard.Modifiers == ModifierKeys.None)
                    {
                        MoveCursor(-16);
                        e.Handled = true;
                        return;
                    }
                    break;

                case Key.Right:
                    if (Keyboard.Modifiers == ModifierKeys.None)
                    {
                        MoveCursor(1 * (int)this.DataStyle);
                        e.Handled = true;
                        return;
                    }
                    break;

                case Key.Down:
                    if (Keyboard.Modifiers == ModifierKeys.None)
                    {
                        MoveCursor(16);
                        e.Handled = true;
                        return;
                    }
                    break;

                case Key.PageDown:
                    if (Keyboard.Modifiers == ModifierKeys.None)
                    {
                        MoveCursor(16 * this.VisibleLines);
                        e.Handled = true;
                        return;
                    }
                    break;

                case Key.PageUp:
                    if (Keyboard.Modifiers == ModifierKeys.None)
                    {
                        MoveCursor(-16 * this.VisibleLines);
                        e.Handled = true;
                        return;
                    }
                    break;

                case Key.Home:
                    if (Keyboard.Modifiers == ModifierKeys.Control)
                    {
                        JumpCursor(0);
                    }
                    else
                    {
                        address = this.SelectedAddress & 0x0f;
                        MoveCursor(-address);
                    }
                    e.Handled = true;
                    return;

                case Key.End:
                    if (Keyboard.Modifiers == ModifierKeys.Control)
                    {
                        JumpCursor(this.Data.Count);
                    }
                    else
                    {
                        address = this.SelectedAddress | 0x0f;
                        JumpCursor(address);
                    }
                    e.Handled = true;
                    return;

                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                    BeginInputNumber((e.Key - Key.D0).ToString());
                    e.Handled = true;
                    return;

                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                    BeginInputNumber((e.Key - Key.NumPad0).ToString());
                    e.Handled = true;
                    return;

                case Key.A:
                case Key.B:
                case Key.C:
                case Key.D:
                case Key.E:
                case Key.F:
                    if (this.NumStyle == NumStyles.Hexadecimal)
                    {
                        BeginInputNumber((e.Key - Key.A + 10).ToString("X"));
                        e.Handled = true;
                        return;
                    }
                    break;

                case Key.OemMinus:
                case Key.Subtract:
                    if (this.NumStyle == NumStyles.SignedDecimal)
                    {
                        BeginInputNumber("-");
                        e.Handled = true;
                        return;
                    }
                    break;

                case Key.F2:
                    BeginInputNumber(GetSelectedAddressDataString());
                    this.InputTextBox.Select(0, this.InputTextBox.Text.Length);
                    e.Handled = true;
                    return;

                default:
                    break;
            }
        }

        /// <summary>
        /// データ入力用の TextBox の KeyDown イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnInputTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    this.InputTextBox.Text = "";
                    this.InputTextBox.Visibility = Visibility.Collapsed;
                    this.BinaryTable.Focus();
                    e.Handled = true;
                    return;

                case Key.Enter:
                    UpdateSelectedAddressData(this.InputTextBox.Text);
                    this.InputTextBox.Text = "";
                    this.InputTextBox.Visibility = Visibility.Collapsed;
                    this.BinaryTable.Focus();
                    e.Handled = true;
                    return;

                default:
                    break;
            }
        }

        /// <summary>
        /// データ入力用の TextBox の TextChanged イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnInputTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            var control = sender as TextBox;
            if (control == null)
                return;

            this.InputTextBox.Text = CoerceNumStyle(control.Text);
            this.InputTextBox.Select(this.InputTextBox.Text.Length, 0);
        }

        /// <summary>
        /// データ入力用の TextBox に入力させる文字列を校正する処理
        /// </summary>
        /// <param name="text">入力された文字列</param>
        /// <returns>校正した文字列</returns>
        private string CoerceNumStyle(string text)
        {
            var str = text.Trim().ToUpper();

            if (this.NumStyle == NumStyles.Hexadecimal)
            {
                var num = 2 * (int)this.DataStyle;
                if (str.Length > num)
                    str = str.Substring(0, num);
            }

            for (var i = 0; i < str.Length; i++)
            {
                var isOk = false;
                var b = System.Text.Encoding.ASCII.GetBytes(str.Substring(i, 1))[0];
                if ((0x30 <= b) && (b <= 0x39))
                {
                    isOk = true;
                }
                else if (b == 0x2d)
                {
                    if ((i == 0) && (this.NumStyle == NumStyles.SignedDecimal))
                    {
                        isOk = true;
                    }
                }
                else if ((0x41 <= b) && (b <= 0x46))
                {
                    if (this.NumStyle == NumStyles.Hexadecimal)
                    {
                        isOk = true;
                    }
                }

                if (!isOk)
                {
                    str = str.Remove(i, 1);
                    if (str.Length <= 0)
                        break;
                    i--;
                }
            }

            return str;
        }

        #endregion イベントハンドラ

        #region private フィールド

        /// <summary>
        /// カーソル点滅用タイマー
        /// </summary>
        private DispatcherTimer _timer;

        /// <summary>
        /// カーソル位置更新用タイマー
        /// </summary>
        private DispatcherTimer _updateTimer;

        #endregion private フィールド

        #region ヘルパ

        /// <summary>
        /// スクロールバーの設定を更新します。
        /// </summary>
        private void UpdateScrollBarInfo()
        {
            this.VerticalScrollBar.Minimum = 0;
            this.VerticalScrollBar.Maximum = this.Data != null ? this.Data.Count : 0;
            this.VerticalScrollBar.SmallChange = 16;
            this.VerticalScrollBar.LargeChange = 16 * this.VisibleLines;

            if (this.TopAddress > this.VerticalScrollBar.Maximum)
                this.TopAddress = 0;
        }

        /// <summary>
        /// アドレスが表示領域内にあることを確認します。
        /// </summary>
        /// <returns>表示領域内にある場合に true を返します。</returns>
        private bool ContainAddress(int address)
        {
            var top = this.TopAddress;
            var end = top + this.VisibleLines * 16 - 1;
            return (top <= address) && (address <= end);
        }

        #region カーソル関係

        /// <summary>
        /// カーソルの色を選択色にします。
        /// </summary>
        private void SetCusorColor()
        {
            if (this.CursorRectangle == null)
                return;
            if (!(this.CursorRectangle.Fill is SolidColorBrush))
                return;

            var color = SystemColors.HighlightColor;
            //if (!DesignerProperties.GetIsInDesignMode(this))
            {
                var colorResource = this.FindResource("SelectedColor");
                if ((colorResource != null) && (colorResource.GetType() == typeof(Color)))
                    color = (Color)Convert.ChangeType(colorResource, typeof(Color));
            }
            (this.CursorRectangle.Fill as SolidColorBrush).Color = color;
        }

        /// <summary>
        /// カーソルの色を透明色にします。
        /// </summary>
        private void ClearCusorColor()
        {
            if (this.CursorRectangle == null)
                return;
            if (!(this.CursorRectangle.Fill is SolidColorBrush))
                return;

            (this.CursorRectangle.Fill as SolidColorBrush).Color = Colors.Transparent;
        }

        /// <summary>
        /// カーソルをジャンプします。
        /// </summary>
        /// <param name="address">移動先のアドレスを指定します。</param>
        private void JumpCursor(int address)
        {
            if (this.Data == null)
                return;
            if (address > this.Data.Count)
                return;

            if (!ContainAddress(address))
            {
                this.TopAddress = address - (address & 0x0f);
            }

            this.SelectedAddress = address;
            this._timer.Stop();
            this._updateTimer.Start();
            SetCusorColor();
            this._timer.Start();
        }

        /// <summary>
        /// カーソルを移動します。
        /// </summary>
        /// <param name="offset">移動量を指定します。</param>
        private void MoveCursor(int offset)
        {
            if (offset == 0)
                return;
            if (this.Data == null)
                return;

            var address = this.SelectedAddress + offset;
            if (!ContainAddress(address))
            {
                if (offset > 0)
                {
                    var top = address - (address & 0x0f) - 16 * (this.VisibleLines - 1);
                    if (top > this.Data.Count)
                        return;
                    this.TopAddress = top;
                }
                else
                {
                    var top = address - (address & 0x0f);
                    if (top < 0)
                        return;
                    this.TopAddress = top;
                }
            }
            this.SelectedAddress += offset;
            this._timer.Stop();
            this._updateTimer.Start();
            SetCusorColor();
            this._timer.Start();
        }

        #endregion カーソル関係

        /// <summary>
        /// データ入力を開始します。
        /// </summary>
        /// <param name="text">入力開始時のテキストを指定します。</param>
        private void BeginInputNumber(string text)
        {
            this.InputTextBox.FontFamily = this.BinaryTable.Typeface.FontFamily;
            this.InputTextBox.FontSize = this.FontSize;
            this.InputTextBox.Text = text;
            if (this.InputTextBox.Visibility == Visibility.Collapsed)
                this.InputTextBox.Visibility = Visibility.Visible;
            this.InputTextBox.Focus();
            this.InputTextBox.Select(this.InputTextBox.Text.Length, 0);
        }

        /// <summary>
        /// データ入力を確定します。
        /// </summary>
        /// <param name="text">入力した値</param>
        private void UpdateSelectedAddressData(string text)
        {
            object value;
            byte[] bytes = new byte[0];

            switch (this.DataStyle)
            {
                case DataStyles.Byte:
                    try
                    {
                        // 最後に byte でアンボックスするのですべて byte でボックス化するようにする
                        if (this.NumStyle == NumStyles.Hexadecimal)
                        {
                            value = byte.Parse(text, NumberStyles.HexNumber);
                        }
                        else if (this.NumStyle == NumStyles.Decimal)
                        {
                            value = byte.Parse(text);
                        }
                        else
                        {
                            value = (byte)sbyte.Parse(text);
                        }
                        bytes = new byte[1] { (byte)value };
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                    }
                    break;

                case DataStyles.Word:
                    try
                    {
                        // 最後に ushort でアンボックスするのですべて ushort でボックス化するようにする
                        if (this.NumStyle == NumStyles.Hexadecimal)
                        {
                            value = ushort.Parse(text, NumberStyles.HexNumber);
                        }
                        else if (this.NumStyle == NumStyles.Decimal)
                        {
                            value = ushort.Parse(text);
                        }
                        else
                        {
                            value = (ushort)short.Parse(text);
                        }
                        bytes = BitConverter.GetBytes((ushort)value).Reverse().ToArray();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                    }
                    break;

                case DataStyles.DoubleWord:
                    try
                    {
                        // 最後に uint でアンボックスするのですべて uint でボックス化するようにする
                        if (this.NumStyle == NumStyles.Hexadecimal)
                        {
                            value = uint.Parse(text, NumberStyles.HexNumber);
                        }
                        else if (this.NumStyle == NumStyles.Decimal)
                        {
                            value = uint.Parse(text);
                        }
                        else
                        {
                            value = (uint)int.Parse(text);
                        }
                        bytes = BitConverter.GetBytes((uint)value).Reverse().ToArray();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                    }
                    break;

                case DataStyles.QuadWord:
                    try
                    {
                        // 最後に ulong でアンボックスするのですべて ulong でボックス化するようにする
                        if (this.NumStyle == NumStyles.Hexadecimal)
                        {
                            value = ulong.Parse(text, NumberStyles.HexNumber);
                        }
                        else if (this.NumStyle == NumStyles.Decimal)
                        {
                            value = ulong.Parse(text);
                        }
                        else
                        {
                            value = (ulong)long.Parse(text);
                        }
                        bytes = BitConverter.GetBytes((ulong)value).Reverse().ToArray();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                    }
                    break;

                default:
                    return;
            }

            var address = this.SelectedAddress;
            foreach (var b in bytes)
            {
                this.Data[address] = b;
                address++;
            }
        }

        /// <summary>
        /// 選択アドレスのデータを文字列として取得します。
        /// </summary>
        /// <returns></returns>
        private string GetSelectedAddressDataString()
        {
            byte[] bytes = this.Data.Skip(this.SelectedAddress).Take((int)this.DataStyle).Reverse().ToArray();
            var str = "";

            if (bytes.Length > 0)
            {
                switch (this.DataStyle)
                {
                    case DataStyles.Byte:
                        if (this.NumStyle == NumStyles.Hexadecimal)
                        {
                            str = bytes[0].ToString("X2");
                        }
                        else if (this.NumStyle == NumStyles.Decimal)
                        {
                            str = bytes[0].ToString();
                        }
                        else
                        {
                            str = ((sbyte)bytes[0]).ToString();
                        }
                        break;

                    case DataStyles.Word:
                        if (this.NumStyle == NumStyles.Hexadecimal)
                        {
                            str = BitConverter.ToUInt16(bytes, 0).ToString("X4");
                        }
                        else if (this.NumStyle == NumStyles.Decimal)
                        {
                            str = BitConverter.ToUInt16(bytes, 0).ToString();
                        }
                        else
                        {
                            str = BitConverter.ToInt16(bytes, 0).ToString();
                        }
                        break;

                    case DataStyles.DoubleWord:
                        if (this.NumStyle == NumStyles.Hexadecimal)
                        {
                            str = BitConverter.ToUInt32(bytes, 0).ToString("X8");
                        }
                        else if (this.NumStyle == NumStyles.Decimal)
                        {
                            str = BitConverter.ToUInt32(bytes, 0).ToString();
                        }
                        else
                        {
                            str = BitConverter.ToInt32(bytes, 0).ToString();
                        }
                        break;

                    case DataStyles.QuadWord:
                        if (this.NumStyle == NumStyles.Hexadecimal)
                        {
                            str = BitConverter.ToUInt64(bytes, 0).ToString("X16");
                        }
                        else if (this.NumStyle == NumStyles.Decimal)
                        {
                            str = BitConverter.ToUInt64(bytes, 0).ToString();
                        }
                        else
                        {
                            str = BitConverter.ToInt64(bytes, 0).ToString();
                        }
                        break;
                }
            }

            return str;
        }

        /// <summary>
        /// 入力用の TextBox をクリアします。
        /// </summary>
        private void ClearInputTextBox()
        {
            this.InputTextBox.Text = "";
            this.InputTextBox.Visibility = Visibility.Collapsed;
        }

        #endregion ヘルパ

    }
}
