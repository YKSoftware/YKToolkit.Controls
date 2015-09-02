namespace YKToolkit.Controls.Behaviors
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// フォーカス時にテキストを全選択するビヘイビアを表します。
    /// </summary>
    public class TextBoxAllSelectBehavior
    {
        #region IsEnabled 添付プロパティ
        /// <summary>
        /// IsEnabled 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(TextBoxAllSelectBehavior), new PropertyMetadata(false, OnIsEnabledPropertyChagned));

        /// <summary>
        /// IsEnabled 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <returns>取得した値を返します。</returns>
        public static bool GetIsEnabled(DependencyObject target)
        {
            return (bool)target.GetValue(IsEnabledProperty);
        }

        /// <summary>
        /// IsEnabled 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <param name="value">設定する値を指定します。</param>
        public static void SetIsEnabled(DependencyObject target, bool value)
        {
            target.SetValue(IsEnabledProperty, value);
        }

        /// <summary>
        /// IsEnabled 添付プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnIsEnabledPropertyChagned(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                var textbox = sender as TextBox;
                textbox.GotFocus -= TextBoxGotFocus;
                if ((bool)e.NewValue)
                {
                    textbox.GotFocus += TextBoxGotFocus;
                }
            }
            else if (sender is PasswordBox)
            {
                var passwordbox = sender as PasswordBox;
                passwordbox.GotFocus -= TextBoxGotFocus;
                if ((bool)e.NewValue)
                {
                    passwordbox.GotFocus += TextBoxGotFocus;
                }
            }
        }
        #endregion IsEnabled 添付プロパティ

        /// <summary>
        /// フォーカス取得イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        static void TextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox)
            {
                var textbox = sender as TextBox;
                textbox.Dispatcher.BeginInvoke((Action)(() => textbox.SelectAll()));
            }
            else if (sender is PasswordBox)
            {
                var passwordbox = sender as PasswordBox;
                passwordbox.Dispatcher.BeginInvoke((Action)(() => passwordbox.SelectAll()));
            }
        }
    }
}
