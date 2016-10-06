namespace YKToolkit.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// メッセージを表示するダイアログを表します。
    /// </summary>
    [TemplatePart(Name = PART_OkButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_CancelButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_YesButton, Type = typeof(Button))]
    [TemplatePart(Name = PART_NoButton, Type = typeof(Button))]
    public class MessageBox : System.Windows.Window
    {
        #region TemplatePart
        private const string PART_OkButton = "PART_OkButton";
        private const string PART_CancelButton = "PART_CancelButton";
        private const string PART_YesButton = "PART_YesButton";
        private const string PART_NoButton = "PART_NoButton";

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

        private Button _yesButton;
        private Button YesButton
        {
            get { return _yesButton; }
            set
            {
                if (_yesButton != null)
                {
                    _yesButton.Click -= YesButton_Click;
                }
                _yesButton = value;
                if (_yesButton != null)
                {
                    _yesButton.Click += YesButton_Click;
                }
            }
        }

        private Button _noButton;
        private Button NoButton
        {
            get { return _noButton; }
            set
            {
                if (_noButton != null)
                {
                    _noButton.Click -= NoButton_Click;
                }
                _noButton = value;
                if (_noButton != null)
                {
                    _noButton.Click += NoButton_Click;
                }
            }
        }

        /// <summary>
        /// テンプレート適用時の処理
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.OkButton = this.Template.FindName(PART_OkButton, this) as Button;
            this.CancelButton = this.Template.FindName(PART_CancelButton, this) as Button;
            this.YesButton = this.Template.FindName(PART_YesButton, this) as Button;
            this.NoButton = this.Template.FindName(PART_NoButton, this) as Button;
        }
        #endregion TemplatePart

        #region コンストラクタ
        /// <summary>
        /// 静的なコンストラクタです。
        /// </summary>
        static MessageBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageBox), new FrameworkPropertyMetadata(typeof(MessageBox)));
        }
        #endregion コンストラクタ

        #region Message 依存関係プロパティ
        /// <summary>
        /// Message 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(MessageBox), new PropertyMetadata(null));

        /// <summary>
        /// 表示するメッセージを取得または設定します。
        /// </summary>
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        #endregion Message 依存関係プロパティ

        #region MessageBoxResult 依存関係プロパティ
        /// <summary>
        /// MessageBoxResult 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty MessageBoxResultProperty = DependencyProperty.Register("MessageBoxResult", typeof(MessageBoxResult), typeof(MessageBox), new PropertyMetadata(MessageBoxResult.None));

        /// <summary>
        /// ダイアログの戻り値を取得または設定します。
        /// </summary>
        public MessageBoxResult MessageBoxResult
        {
            get { return (MessageBoxResult)GetValue(MessageBoxResultProperty); }
            set { SetValue(MessageBoxResultProperty, value); }
        }
        #endregion MessageBoxResult 依存関係プロパティ

        #region MessageBoxButton 依存関係プロパティ
        /// <summary>
        /// MessageBoxButton 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty MessageBoxButtonProperty = DependencyProperty.Register("MessageBoxButton", typeof(MessageBoxButton), typeof(MessageBox), new PropertyMetadata(MessageBoxButton.OK));

        /// <summary>
        /// 表示するボタンを取得または設定します。
        /// </summary>
        public MessageBoxButton MessageBoxButton
        {
            get { return (MessageBoxButton)GetValue(MessageBoxButtonProperty); }
            set { SetValue(MessageBoxButtonProperty, value); }
        }
        #endregion MessageBoxButton 依存関係プロパティ

        #region MessageBoxImage 依存関係プロパティ
        /// <summary>
        /// MessageBoxImage 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty MessageBoxImageProperty = DependencyProperty.Register("MessageBoxImage", typeof(MessageBoxImage), typeof(MessageBox), new PropertyMetadata(MessageBoxImage.None));

        /// <summary>
        /// 表示するメッセージアイコンを取得または設定します。
        /// </summary>
        public MessageBoxImage MessageBoxImage
        {
            get { return (MessageBoxImage)GetValue(MessageBoxImageProperty); }
            set { SetValue(MessageBoxImageProperty, value); }
        }
        #endregion MessageBoxImage 依存関係プロパティ

        #region イベントハンドラ
        /// <summary>
        /// OkButton クリックイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.MessageBoxResult = MessageBoxResult.OK;
            this.DialogResult = true;
        }

        /// <summary>
        /// CancelButton クリックイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.MessageBoxResult = MessageBoxResult.Cancel;
            this.DialogResult = false;
        }

        /// <summary>
        /// YesButton クリックイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            this.MessageBoxResult = MessageBoxResult.Yes;
            this.DialogResult = true;
        }

        /// <summary>
        /// NoButton クリックイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.MessageBoxResult = MessageBoxResult.No;
            this.DialogResult = false;
        }
        #endregion イベントハンドラ

        #region 公開メソッド
        /// <summary>
        /// メッセージダイアログを表示します。
        /// </summary>
        /// <param name="message">表示するメッセージを指定します。</param>
        /// <returns>ダイアログの結果を返します。</returns>
        public static MessageBoxResult Show(string message)
        {
            return Show(null, message, null, MessageBoxButton.OK, MessageBoxImage.None);
        }

        /// <summary>
        /// メッセージダイアログを表示します。
        /// </summary>
        /// <param name="message">表示するメッセージを指定します。</param>
        /// <param name="title">ダイアログのタイトルを指定します。</param>
        /// <returns>ダイアログの結果を返します。</returns>
        public static MessageBoxResult Show(string message, string title)
        {
            return Show(null, message, title, MessageBoxButton.OK, MessageBoxImage.None);
        }

        /// <summary>
        /// メッセージダイアログを表示します。
        /// </summary>
        /// <param name="message">表示するメッセージを指定します。</param>
        /// <param name="title">ダイアログのタイトルを指定します。</param>
        /// <param name="button">表示するボタンの種類を指定します。</param>
        /// <returns>ダイアログの結果を返します。</returns>
        public static MessageBoxResult Show(string message, string title, MessageBoxButton button)
        {
            return Show(null, message, title, button, MessageBoxImage.None);
        }

        /// <summary>
        /// メッセージダイアログを表示します。
        /// </summary>
        /// <param name="message">表示するメッセージを指定します。</param>
        /// <param name="title">ダイアログのタイトルを指定します。</param>
        /// <param name="button">表示するボタンの種類を指定します。</param>
        /// <param name="image">表示するアイコンの種類を指定します。</param>
        /// <returns>ダイアログの結果を返します。</returns>
        public static MessageBoxResult Show(string message, string title, MessageBoxButton button, MessageBoxImage image)
        {
            return Show(null, message, title, button, image);
        }

        /// <summary>
        /// メッセージダイアログを表示します。
        /// </summary>
        /// <param name="owner">ダイアログに対するオーナーウィンドウを指定します。</param>
        /// <param name="message">表示するメッセージを指定します。</param>
        /// <returns>ダイアログの結果を返します。</returns>
        public static MessageBoxResult Show(System.Windows.Window owner, string message)
        {
            return Show(owner, message, null, MessageBoxButton.OK, MessageBoxImage.None);
        }

        /// <summary>
        /// メッセージダイアログを表示します。
        /// </summary>
        /// <param name="owner">ダイアログに対するオーナーウィンドウを指定します。</param>
        /// <param name="message">表示するメッセージを指定します。</param>
        /// <param name="title">ダイアログのタイトルを指定します。</param>
        /// <returns>ダイアログの結果を返します。</returns>
        public static MessageBoxResult Show(System.Windows.Window owner, string message, string title)
        {
            return Show(owner, message, title, MessageBoxButton.OK, MessageBoxImage.None);
        }

        /// <summary>
        /// メッセージダイアログを表示します。
        /// </summary>
        /// <param name="owner">ダイアログに対するオーナーウィンドウを指定します。</param>
        /// <param name="message">表示するメッセージを指定します。</param>
        /// <param name="title">ダイアログのタイトルを指定します。</param>
        /// <param name="button">表示するボタンの種類を指定します。</param>
        /// <returns>ダイアログの結果を返します。</returns>
        public static MessageBoxResult Show(System.Windows.Window owner, string message, string title, MessageBoxButton button)
        {
            return Show(owner, message, title, button, MessageBoxImage.None);
        }

        /// <summary>
        /// メッセージダイアログを表示します。
        /// </summary>
        /// <param name="owner">ダイアログに対するオーナーウィンドウを指定します。</param>
        /// <param name="message">表示するメッセージを指定します。</param>
        /// <param name="title">ダイアログのタイトルを指定します。</param>
        /// <param name="button">表示するボタンの種類を指定します。</param>
        /// <param name="image">表示するアイコンの種類を指定します。</param>
        /// <returns>ダイアログの結果を返します。</returns>
        public static MessageBoxResult Show(System.Windows.Window owner, string message, string title, MessageBoxButton button, MessageBoxImage image)
        {
            var dlg = new MessageBox();

            if (owner == null)
            {
                dlg.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            else
            {
                dlg.Owner = owner;
                dlg.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                dlg.Icon = owner.Icon;
            }
            dlg.Message = message;
            dlg.Title = title;
            dlg.MessageBoxButton = button;
            dlg.MessageBoxImage = image;

            dlg.PreviewKeyDown += (s, e) =>
            {
                if ((e.Key == Key.Escape) && (Keyboard.Modifiers == ModifierKeys.None))
                {
                    var control = s as MessageBox;
                    switch (control.MessageBoxButton)
                    {
                        case MessageBoxButton.OK: control.MessageBoxResult = MessageBoxResult.OK; break;
                        case MessageBoxButton.OKCancel:
                        case MessageBoxButton.YesNoCancel: control.MessageBoxResult = MessageBoxResult.Cancel; break;
                        case MessageBoxButton.YesNo: control.MessageBoxResult = MessageBoxResult.No; break;
                    }
                    control.DialogResult = false;
                }
            };

            dlg.ShowInTaskbar = false;
            dlg.ShowDialog();
            var result = dlg.MessageBoxResult;
            dlg = null;

            return result;
        }
        #endregion 公開メソッド
    }
}
