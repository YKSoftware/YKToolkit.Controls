namespace YKToolkit.Controls
{
    using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shell;

    /// <summary>
    /// YKToolkit.Controls のテーマに則った Window コントロールを表します。
    /// </summary>
    [TemplatePart(Name = PART_IconButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_CloseButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_RestoreButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_MaximizeButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_MinimizeButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_TopmostButton, Type = typeof(Button))]
    public class Window : System.Windows.Window
    {
        #region TemplatePart
        private const string PART_IconButton = "PART_IconButton";
        private const string PART_CloseButton = "PART_CloseButton";
        private const string PART_RestoreButton = "PART_RestoreButton";
        private const string PART_MaximizeButton = "PART_MaximizeButton";
        private const string PART_MinimizeButton = "PART_MinimizeButton";
        private const string PART_TopmostButton = "PART_TopmostButton";

        private Button _iconButton;
        private Button IconButton
        {
            get { return _iconButton; }
            set
            {
                if (_iconButton != null)
                {
                    _iconButton.MouseDoubleClick -= IconButtonDoubleClick;
                }
                _iconButton = value;
                if (_iconButton != null)
                {
                    _iconButton.MouseDoubleClick += IconButtonDoubleClick;
                }
            }
        }

        private Button _closeButton;
        private Button CloseButton
        {
            get { return _closeButton; }
            set
            {
                if (_closeButton != null)
                {
                    _closeButton.Click -= CloseButtonClick;
                }
                _closeButton = value;
                if (_closeButton != null)
                {
                    _closeButton.Click += CloseButtonClick;
                }
            }
        }

        private Button _restoreButton;
        private Button RestoreButton
        {
            get { return _restoreButton; }
            set
            {
                if (_restoreButton != null)
                {
                    _restoreButton.Click -= RestoreButtonClick;
                }
                _restoreButton = value;
                if (_restoreButton != null)
                {
                    _restoreButton.Click += RestoreButtonClick;
                }
            }
        }

        private Button _maximizeButton;
        private Button MaximizeButton
        {
            get { return _maximizeButton; }
            set
            {
                if (_maximizeButton != null)
                {
                    _maximizeButton.Click -= MaximizeButtonClick;
                }
                _maximizeButton = value;
                if (_maximizeButton != null)
                {
                    _maximizeButton.Click += MaximizeButtonClick;
                }
            }
        }

        private Button _minimizeButton;
        private Button MinimizeButton
        {
            get { return _minimizeButton; }
            set
            {
                if (_minimizeButton != null)
                {
                    _minimizeButton.Click -= MinimizeButtonClick;
                }
                _minimizeButton = value;
                if (_minimizeButton != null)
                {
                    _minimizeButton.Click += MinimizeButtonClick;
                }
            }
        }

        private Button _topmostButton;
        private Button TopmostButton
        {
            get { return _topmostButton; }
            set
            {
                if (_topmostButton != null)
                {
                    _topmostButton.Click -= TopmostButtonClick;
                }
                _topmostButton = value;
                if (_topmostButton != null)
                {
                    _topmostButton.Click += TopmostButtonClick;
                }
            }
        }

        /// <summary>
        /// テンプレート適用時の処理
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.IconButton = this.Template.FindName(PART_IconButton, this) as Button;
            this.CloseButton = this.Template.FindName(PART_CloseButton, this) as Button;
            this.RestoreButton = this.Template.FindName(PART_RestoreButton, this) as Button;
            this.MaximizeButton = this.Template.FindName(PART_MaximizeButton, this) as Button;
            this.MinimizeButton = this.Template.FindName(PART_MinimizeButton, this) as Button;
            this.TopmostButton = this.Template.FindName(PART_TopmostButton, this) as Button;

            UpdateResizeBorderThickness();
        }
        #endregion TemplatePart

        #region CaptionLeftContent プロパティ
        /// <summary>
        /// CaptionLeftContent 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty CaptionLeftContentProperty = DependencyProperty.Register("CaptionLeftContent", typeof(object), typeof(Window), new PropertyMetadata(null));

        /// <summary>
        /// キャプションバーに表示するコンテンツを取得または設定します。
        /// </summary>
        public object CaptionLeftContent
        {
            get { return GetValue(CaptionLeftContentProperty); }
            set { SetValue(CaptionLeftContentProperty, value); }
        }
        #endregion CaptionLeftContent プロパティ

        #region CaptionRightContent プロパティ
        /// <summary>
        /// CaptionRightContent 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty CaptionRightContentProperty = DependencyProperty.Register("CaptionRightContent", typeof(object), typeof(Window), new PropertyMetadata(null));

        /// <summary>
        /// キャプションバーに表示するコンテンツを取得または設定します。
        /// </summary>
        public object CaptionRightContent
        {
            get { return GetValue(CaptionRightContentProperty); }
            set { SetValue(CaptionRightContentProperty, value); }
        }
        #endregion CaptionRightContent プロパティ

        #region SystemMenuContent プロパティ
        /// <summary>
        /// SystemMenuContent 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty SystemMenuContentProperty = DependencyProperty.Register("SystemMenuContent", typeof(object), typeof(Window), new PropertyMetadata(null));

        /// <summary>
        /// システムメニューとして表示するコンテンツを取得または設定します。
        /// </summary>
        public object SystemMenuContent
        {
            get { return GetValue(SystemMenuContentProperty); }
            set { SetValue(SystemMenuContentProperty, value); }
        }
        #endregion SystemMenuContent プロパティ

        #region IsClosingConfirmationEnabled プロパティ
        /// <summary>
        /// IsClosingConfirmationEnabled 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsClosingConfirmationEnabledProperty = DependencyProperty.Register("IsClosingConfirmationEnabled", typeof(bool), typeof(Window), new PropertyMetadata(true));

        /// <summary>
        /// ウィンドウを閉じることを確認するかどうかを取得または設定します。
        /// </summary>
        public bool IsClosingConfirmationEnabled
        {
            get { return (bool)GetValue(IsClosingConfirmationEnabledProperty); }
            set { SetValue(IsClosingConfirmationEnabledProperty, value); }
        }
        #endregion IsClosingConfirmationEnabled プロパティ

        #region ClosingConfirmationMessage プロパティ
        /// <summary>
        /// ClosingConfirmationMessage 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty ClosingConfirmationMessageProperty = DependencyProperty.Register("ClosingConfirmationMessage", typeof(string), typeof(Window), new PropertyMetadata("Do you really want to exit this application?"));

        /// <summary>
        /// ウィンドウを閉じることを確認するかどうかを取得または設定します。
        /// </summary>
        public string ClosingConfirmationMessage
        {
            get { return (string)GetValue(ClosingConfirmationMessageProperty); }
            set { SetValue(ClosingConfirmationMessageProperty, value); }
        }
        #endregion ClosingConfirmationMessage プロパティ

        #region ClosingConfirmationTitle プロパティ
        /// <summary>
        /// ClosingConfirmationTitle 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty ClosingConfirmationTitleProperty = DependencyProperty.Register("ClosingConfirmationTitle", typeof(string), typeof(Window), new PropertyMetadata("Confirmation"));

        /// <summary>
        /// ウィンドウを閉じることを確認するかどうかを取得または設定します。
        /// </summary>
        public string ClosingConfirmationTitle
        {
            get { return (string)GetValue(ClosingConfirmationTitleProperty); }
            set { SetValue(ClosingConfirmationTitleProperty, value); }
        }
        #endregion ClosingConfirmationTitle プロパティ

        #region IsCloseButtonEnabled プロパティ
        /// <summary>
        /// IsClosingConfirmationEnabled 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsCloseButtonEnabledProperty = DependencyProperty.Register("IsCloseButtonEnabled", typeof(bool), typeof(Window), new PropertyMetadata(true));

        /// <summary>
        /// 閉じるボタンが有効かどうかを取得または設定します。
        /// </summary>
        public bool IsCloseButtonEnabled
        {
            get { return (bool)GetValue(IsCloseButtonEnabledProperty); }
            set { SetValue(IsCloseButtonEnabledProperty, value); }
        }
        #endregion IsCloseButtonEnabled プロパティ

        #region IsRestoreButtonEnabled プロパティ
        /// <summary>
        /// IsRestoreButtonEnabled 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsRestoreButtonEnabledProperty = DependencyProperty.Register("IsRestoreButtonEnabled", typeof(bool), typeof(Window), new PropertyMetadata(true));

        /// <summary>
        /// 元に戻すボタンが有効かどうかを取得または設定します。
        /// </summary>
        public bool IsRestoreButtonEnabled
        {
            get { return (bool)GetValue(IsRestoreButtonEnabledProperty); }
            set { SetValue(IsRestoreButtonEnabledProperty, value); }
        }
        #endregion IsRestoreButtonEnabled プロパティ

        #region IsMaximizeButtonEnabled プロパティ
        /// <summary>
        /// IsMaximizeButtonEnabled 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsMaximizeButtonEnabledProperty = DependencyProperty.Register("IsMaximizeButtonEnabled", typeof(bool), typeof(Window), new PropertyMetadata(true));

        /// <summary>
        /// 最大化ボタンが有効かどうかを取得または設定します。
        /// </summary>
        public bool IsMaximizeButtonEnabled
        {
            get { return (bool)GetValue(IsMaximizeButtonEnabledProperty); }
            set { SetValue(IsMaximizeButtonEnabledProperty, value); }
        }
        #endregion IsMaximizeButtonEnabled プロパティ

        #region IsMinimizeButtonEnabled プロパティ
        /// <summary>
        /// IsMinimizeButtonEnabled 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsMinimizeButtonEnabledProperty = DependencyProperty.Register("IsMinimizeButtonEnabled", typeof(bool), typeof(Window), new PropertyMetadata(true));

        /// <summary>
        /// 最小化ボタンが有効かどうかを取得または設定します。
        /// </summary>
        public bool IsMinimizeButtonEnabled
        {
            get { return (bool)GetValue(IsMinimizeButtonEnabledProperty); }
            set { SetValue(IsMinimizeButtonEnabledProperty, value); }
        }
        #endregion IsMinimizeButtonEnabled プロパティ

        #region IsTopmostButtonEnabled プロパティ
        /// <summary>
        /// IsTopmostButtonEnabled 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsTopmostButtonEnabledProperty = DependencyProperty.Register("IsTopmostButtonEnabled", typeof(bool), typeof(Window), new PropertyMetadata(true));

        /// <summary>
        /// 常に手前に表示ボタンが有効かどうかを取得または設定します。
        /// </summary>
        public bool IsTopmostButtonEnabled
        {
            get { return (bool)GetValue(IsTopmostButtonEnabledProperty); }
            set { SetValue(IsTopmostButtonEnabledProperty, value); }
        }
        #endregion IsTopmostButtonEnabled プロパティ

        #region IconVisibility プロパティ
        /// <summary>
        /// IconVisibility 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IconVisibilityProperty = DependencyProperty.Register("IconVisibility", typeof(Visibility), typeof(Window), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// アイコンの視認性を取得または設定します。
        /// </summary>
        public Visibility IconVisibility
        {
            get { return (Visibility)GetValue(IconVisibilityProperty); }
            set { SetValue(IconVisibilityProperty, value); }
        }
        #endregion IconVisibility プロパティ

        #region CloseButtonVisibility プロパティ
        /// <summary>
        /// CloseButtonVisibility 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty CloseButtonVisibilityProperty = DependencyProperty.Register("CloseButtonVisibility", typeof(Visibility), typeof(Window), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// 閉じるボタンの視認性を取得または設定します。
        /// </summary>
        public Visibility CloseButtonVisibility
        {
            get { return (Visibility)GetValue(CloseButtonVisibilityProperty); }
            set { SetValue(CloseButtonVisibilityProperty, value); }
        }
        #endregion CloseButtonVisibility プロパティ

        #region RestoreButtonVisibility プロパティ
        /// <summary>
        /// RestoreButtonVisibility 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty RestoreButtonVisibilityProperty = DependencyProperty.Register("RestoreButtonVisibility", typeof(Visibility), typeof(Window), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// 元に戻すボタンの視認性を取得または設定します。
        /// </summary>
        public Visibility RestoreButtonVisibility
        {
            get { return (Visibility)GetValue(RestoreButtonVisibilityProperty); }
            set { SetValue(RestoreButtonVisibilityProperty, value); }
        }
        #endregion RestoreButtonVisibility プロパティ

        #region MaximizeButtonVisibility プロパティ
        /// <summary>
        /// MaximizeButtonVisibility 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty MaximizeButtonVisibilityProperty = DependencyProperty.Register("MaximizeButtonVisibility", typeof(Visibility), typeof(Window), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// 最大化ボタンの視認性を取得または設定します。
        /// </summary>
        public Visibility MaximizeButtonVisibility
        {
            get { return (Visibility)GetValue(MaximizeButtonVisibilityProperty); }
            set { SetValue(MaximizeButtonVisibilityProperty, value); }
        }
        #endregion MaximizeButtonVisibility プロパティ

        #region MinimizeButtonVisibility プロパティ
        /// <summary>
        /// MinimizeButtonVisibility 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty MinimizeButtonVisibilityProperty = DependencyProperty.Register("MinimizeButtonVisibility", typeof(Visibility), typeof(Window), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// 最小化ボタンの視認性を取得または設定します。
        /// </summary>
        public Visibility MinimizeButtonVisibility
        {
            get { return (Visibility)GetValue(MinimizeButtonVisibilityProperty); }
            set { SetValue(MinimizeButtonVisibilityProperty, value); }
        }
        #endregion MinimizeButtonVisibility プロパティ

        #region TopmostButtonVisibility プロパティ
        /// <summary>
        /// TopmostButtonVisibility 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty TopmostButtonVisibilityProperty = DependencyProperty.Register("TopmostButtonVisibility", typeof(Visibility), typeof(Window), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// 常に手前に表示ボタンの視認性を取得または設定します。
        /// </summary>
        public Visibility TopmostButtonVisibility
        {
            get { return (Visibility)GetValue(TopmostButtonVisibilityProperty); }
            set { SetValue(TopmostButtonVisibilityProperty, value); }
        }
        #endregion TopmostButtonVisibility プロパティ

        #region ResizeMode プロパティ
        /// <summary>
        /// ResizeMode 依存関係プロパティの定義
        /// </summary>
        public static readonly new DependencyProperty ResizeModeProperty = DependencyProperty.Register("ResizeMode", typeof(ResizeMode), typeof(Window), new PropertyMetadata(ResizeMode.CanResize, OnResizeModePropertyChanged));

        /// <summary>
        /// ウィンドウのリサイズモードを取得または設定します。
        /// </summary>
        public new ResizeMode ResizeMode
        {
            get { return (ResizeMode)GetValue(ResizeModeProperty); }
            set { SetValue(ResizeModeProperty, value); }
        }
        #endregion ResizeMode プロパティ

        #region ContentBackground プロパティ
        /// <summary>
        /// ContentBackground 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty ContentBackgroundProperty = DependencyProperty.Register("ContentBackground", typeof(Brush), typeof(Window), new PropertyMetadata(null));

        /// <summary>
        /// コンテンツ部の背景色を取得または設定します。
        /// </summary>
        public Brush ContentBackground
        {
            get { return (Brush)GetValue(ContentBackgroundProperty); }
            set { SetValue(ContentBackgroundProperty, value); }
        }
        #endregion ContentBackground プロパティ

        #region CaptionBorderThickness プロパティ
        /// <summary>
        /// CaptionBorderThickness 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty CaptionBorderThicknessProperty = DependencyProperty.Register("CaptionBorderThickness", typeof(double), typeof(Window), new PropertyMetadata(0.0));

        /// <summary>
        /// 非クライアント領域とクライアント領域の境界線の太さを取得または設定します。
        /// </summary>
        public double CaptionBorderThickness
        {
            get { return (double)GetValue(CaptionBorderThicknessProperty); }
            set { SetValue(CaptionBorderThicknessProperty, value); }
        }
        #endregion CaptionBorderThickness プロパティ

        /// <summary>
        /// 静的なコンストラクタ
        /// </summary>
        static Window()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata(typeof(Window)));

            // テーマを初期化する
            YKToolkit.Controls.ThemeManager.Instance.Initialize();

            // Popup の表示を右利き/左利き設定に依存させないようにする
            EnsureStandardPopupAlignment();
            SystemParameters.StaticPropertyChanged += SystemParameters_StaticPropertyChanged;
        }

        /// <summary>
        /// SystemParameters のプロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        static void SystemParameters_StaticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MenuDropAlignment")
                EnsureStandardPopupAlignment();
        }

        /// <summary>
        /// 右利き設定とする
        /// </summary>
        private static void EnsureStandardPopupAlignment()
        {
            var _menuDropAlignmentField = typeof(SystemParameters).GetField("_menuDropAlignment", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

            // MenuDropAlignment が true なら false に書き換える
            if (SystemParameters.MenuDropAlignment && (_menuDropAlignmentField != null))
            {
                _menuDropAlignmentField.SetValue(null, false);
            }
        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public Window()
        {
            this.Closing += OnWindowClosing;
            this.StateChanged += OnStateChanged;
        }

        /// <summary>
        /// ResizeMode プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnResizeModePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var w = sender as Window;
            w.UpdateResizeBorderThickness();
        }

        /// <summary>
        /// ResizeBorderThickness を更新します。
        /// </summary>
        private void UpdateResizeBorderThickness()
        {
            var chrome = WindowChrome.GetWindowChrome(this);
            if (chrome != null)
            {
                chrome.ResizeBorderThickness = this.ResizeMode == ResizeMode.NoResize ? new Thickness(0) : SystemParameters.WindowResizeBorderThickness;
            }
        }

        /// <summary>
        /// Window Closing イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            if (this.IsClosingConfirmationEnabled)
            {
                var result = MessageBox.Show(this, this.ClosingConfirmationMessage, this.ClosingConfirmationTitle, MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Cancel)
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// WindowState プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnStateChanged(object sender, EventArgs e)
        {
            if (this.MaximizeButtonVisibility == System.Windows.Visibility.Collapsed)
            {
                var w = sender as Window;
                if (w.WindowState == System.Windows.WindowState.Maximized)
                {
                    w.WindowState = System.Windows.WindowState.Normal;
                }
            }
        }

        /// <summary>
        /// IconButton ダブルクリックイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void IconButtonDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.IsCloseButtonEnabled)
            {
                this.Close();
            }
        }

        /// <summary>
        /// CloseButton クリックイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// RestoreButton クリックイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void RestoreButtonClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }

        /// <summary>
        /// MaximizeButton クリックイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void MaximizeButtonClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// MinimizeButton クリックイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void MinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// TopmostButton クリックイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void TopmostButtonClick(object sender, RoutedEventArgs e)
        {
            this.Topmost = !this.Topmost;
        }
    }
}
