namespace YKToolkit.Controls
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    /// <summary>
    /// スピンボタンによる数値入力コントロールを表します。
    /// </summary>
    [TemplatePart(Name = PART_InputTextBox, Type = typeof(TextBox))]
    [TemplatePart(Name = PART_UpButton, Type = typeof(RepeatButton))]
    [TemplatePart(Name = PART_DownButton, Type = typeof(RepeatButton))]
    public class SpinInput : Control
    {
        #region TemplatePart
        private const string PART_InputTextBox = "PART_InputTextBox";
        private const string PART_UpButton = "PART_UpButton";
        private const string PART_DownButton = "PART_DownButton";

        private TextBox _inputTextBox;
        private TextBox InputTextBox
        {
            get { return _inputTextBox; }
            set
            {
                if (_inputTextBox != null)
                {
                }
                _inputTextBox = value;
                if (_inputTextBox != null)
                {
                    _inputTextBox.KeyDown += InputTextBox_KeyDown;
                }
            }
        }

        private RepeatButton _upButton;
        private RepeatButton UpButton
        {
            get { return _upButton; }
            set
            {
                if (_upButton != null)
                {
                    _upButton.Click -= UpButton_Click;
                }
                _upButton = value;
                if (_upButton != null)
                {
                    _upButton.Click += UpButton_Click;
                }
            }
        }

        private RepeatButton _downButton;
        private RepeatButton DownButton
        {
            get { return _downButton; }
            set
            {
                if (_downButton != null)
                {
                    _downButton.Click -= DownButton_Click;
                }
                _downButton = value;
                if (_downButton != null)
                {
                    _downButton.Click += DownButton_Click;
                }
            }
        }

        /// <summary>
        /// テンプレート適用時の処理
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.InputTextBox = this.Template.FindName(PART_InputTextBox, this) as TextBox;
            this.UpButton = this.Template.FindName(PART_UpButton, this) as RepeatButton;
            this.DownButton = this.Template.FindName(PART_DownButton, this) as RepeatButton;
        }
        #endregion TemplatePart

        #region コンストラクタ
        /// <summary>
        /// 静的なコンストラクタです。
        /// </summary>
        static SpinInput()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SpinInput), new FrameworkPropertyMetadata(typeof(SpinInput)));
        }
        #endregion コンストラクタ

        #region Value 依存関係プロパティ
        /// <summary>
        /// Value 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(SpinInput), new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValuePropertyChanged));

        /// <summary>
        /// 数値を取得または設定します。
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Value プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnValuePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as SpinInput;
            if (control == null)
                return;

            control.CoerceValue();

            if (control._newText == null)
            {
                var c = string.IsNullOrWhiteSpace(control.StringFormat) ? "" : control.StringFormat.Substring(0, 1).ToLower();
                if (c == "d")
                {
                    control.Text = ((int)control.Value).ToString(control.StringFormat);
                }
                else if (c == "x")
                {
                    control.Text = "0x" + ((int)control.Value).ToString(control.StringFormat);
                }
                else
                {
                    control.Text = control.Value.ToString(control.StringFormat);
                }
            }
            control._newText = null;
        }
        #endregion Value 依存関係プロパティ

        #region Text 依存関係プロパティ
        /// <summary>
        /// Text 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(SpinInput), new PropertyMetadata(null, OnTextPropertyChanged));

        /// <summary>
        /// 表示されているテキストを取得または設定します。
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Text プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnTextPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as SpinInput;
            if (control == null)
                return;

            if (control._newText != null)
                control.UpdateValueFromText(control.Text, control.NumberStyle);
        }
        #endregion Text 依存関係プロパティ

        #region StringFormat 依存関係プロパティ
        /// <summary>
        /// StringFormat 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty StringFormatProperty = DependencyProperty.Register("StringFormat", typeof(string), typeof(SpinInput), new PropertyMetadata(null, OnStringFormatPropertyChanged));

        /// <summary>
        /// 表示するテキストの書式を取得または設定します。
        /// </summary>
        public string StringFormat
        {
            get { return (string)GetValue(StringFormatProperty); }
            set { SetValue(StringFormatProperty, value); }
        }

        /// <summary>
        /// StringFormat プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnStringFormatPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as SpinInput;
            if (control == null)
                return;

            var c = string.IsNullOrWhiteSpace(control.StringFormat) ? "" : control.StringFormat.Substring(0, 1).ToLower();
            if (c == "d")
            {
                control.Text = ((int)control.Value).ToString(control.StringFormat);
            }
            else if (c == "x")
            {
                control.Text = "0x" + ((int)control.Value).ToString(control.StringFormat);
            }
            else
            {
                control.Text = control.Value.ToString(control.StringFormat);
            }
        }
        #endregion StringFormat 依存関係プロパティ

        #region NumberStyle 依存関係プロパティ
        /// <summary>
        /// NumberStyle 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty NumberStyleProperty = DependencyProperty.Register("NumberStyle", typeof(NumberStyles), typeof(SpinInput), new PropertyMetadata(NumberStyles.Any));

        /// <summary>
        /// 表示されているテキストを取得または設定します。
        /// </summary>
        public NumberStyles NumberStyle
        {
            get { return (NumberStyles)GetValue(NumberStyleProperty); }
            set { SetValue(NumberStyleProperty, value); }
        }
        #endregion NumberStyle 依存関係プロパティ

        #region Delay 依存関係プロパティ
        /// <summary>
        /// Delay 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty DelayProperty = DependencyProperty.Register("Delay", typeof(int), typeof(SpinInput), new PropertyMetadata(200));

        /// <summary>
        /// ボタンのクリックイベントを繰り返し始めるまでの遅延時間 [ms] を取得または設定します。
        /// </summary>
        public int Delay
        {
            get { return (int)GetValue(DelayProperty); }
            set { SetValue(DelayProperty, value); }
        }
        #endregion Delay 依存関係プロパティ

        #region Interval 依存関係プロパティ
        /// <summary>
        /// Interval 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IntervalProperty = DependencyProperty.Register("Interval", typeof(int), typeof(SpinInput), new PropertyMetadata(50));

        /// <summary>
        /// ボタンのクリックイベントを繰り返す間隔 [ms] を取得または設定します。
        /// </summary>
        public int Interval
        {
            get { return (int)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }
        #endregion Interval 依存関係プロパティ

        #region Tick 依存関係プロパティ
        /// <summary>
        /// Tick 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty TickProperty = DependencyProperty.Register("Tick", typeof(double), typeof(SpinInput), new PropertyMetadata(1.0));

        /// <summary>
        /// 値の増減値を取得または設定します。
        /// </summary>
        public double Tick
        {
            get { return (double)GetValue(TickProperty); }
            set { SetValue(TickProperty, value); }
        }
        #endregion Tick 依存関係プロパティ

        #region Minimum 依存関係プロパティ
        /// <summary>
        /// Minimum 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register("Minimum", typeof(double), typeof(SpinInput), new PropertyMetadata(double.MinValue, OnMinimumPropertyChanged));

        /// <summary>
        /// 最小値を取得または設定します。
        /// </summary>
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// Minimum プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnMinimumPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as SpinInput;
            if (control == null)
                return;

            control.CoerceValue();
        }
        #endregion Minimum 依存関係プロパティ

        #region Maximum 依存関係プロパティ
        /// <summary>
        /// Maximum 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register("Maximum", typeof(double), typeof(SpinInput), new PropertyMetadata(double.MaxValue, OnMaximumPropertyChanged));

        /// <summary>
        /// 最小値を取得または設定します。
        /// </summary>
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// Maximum プロパティ値変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnMaximumPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as SpinInput;
            if (control == null)
                return;

            control.CoerceValue();
        }
        #endregion Maximum 依存関係プロパティ

        #region IsReadOnly 依存関係プロパティ
        /// <summary>
        /// IsReadOnly 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty = DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(SpinInput), new PropertyMetadata(false));

        /// <summary>
        /// 読み取り専用かどうかを取得または設定します。
        /// </summary>
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }
        #endregion IsReadOnly 依存関係プロパティ

        #region イベントハンドラ
        /// <summary>
        /// InputTextBox キー押下イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void InputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var textbox = sender as TextBox;
            if (textbox == null)
                return;

            if (e.Key == Key.Enter)
            {
                _newText = textbox.Text;
                UpdateValueFromText(textbox.Text, this.NumberStyle);

                //var targetProperty = TextBox.TextProperty;
                //var bindingExpression = BindingOperations.GetBindingExpression(textbox, targetProperty);
                //if (bindingExpression != null)
                //{
                //    // データバインディングのソースを更新する
                //    bindingExpression.UpdateSource();
                //}
            }
        }

        /// <summary>
        /// UpButton クリックイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Value + this.Tick <= this.Maximum)
                this.Value += this.Tick;
        }

        /// <summary>
        /// DownButton クリックイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Value - this.Tick >= this.Minimum)
                this.Value -= this.Tick;
        }
        #endregion イベントハンドラ

        #region ヘルパ
        /// <summary>
        /// テキストから Value プロパティを更新します。
        /// </summary>
        /// <param name="text">テキストを指定します。</param>
        /// <param name="style">パースするための読取形式を指定します。</param>
        private void UpdateValueFromText(string text, NumberStyles style)
        {
            double value = 0.0;
            if (double.TryParse(text, style, CultureInfo.CurrentUICulture, out value))
            {
                this.Value = value;
            }
        }

        /// <summary>
        /// Value プロパティに制限を加えます。
        /// </summary>
        private void CoerceValue()
        {
            if (this.Minimum > this.Maximum)
                return;

            if (this.Value > this.Maximum)
                this.Value = this.Maximum;
            else if (this.Value < this.Minimum)
                this.Value = this.Minimum;
        }
        #endregion ヘルパ

        #region private フィールド
        /// <summary>
        /// UI から変更されたテキスト
        /// </summary>
        private string _newText;
        #endregion private フィールド
    }
}
