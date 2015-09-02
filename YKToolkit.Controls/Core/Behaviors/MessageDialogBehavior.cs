namespace YKToolkit.Controls.Behaviors
{
    using System.Windows;

    /// <summary>
    /// MessageBox コントロールを表示するためのビヘイビアを表します。
    /// </summary>
    public class MessageDialogBehavior
    {
        #region DialogInfo 添付プロパティ
        /// <summary>
        /// DialogInfo 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty DialogInfoProperty = DependencyProperty.RegisterAttached("DialogInfo", typeof(IDialogInfo), typeof(MessageDialogBehavior), new FrameworkPropertyMetadata(null, OnDialogInfoPropertyChanged));

        /// <summary>
        /// DialogInfo 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <returns>取得した値を返します。</returns>
        public static IDialogInfo GetDialogInfo(DependencyObject target)
        {
            return (IDialogInfo)target.GetValue(DialogInfoProperty);
        }

        /// <summary>
        /// DialogInfo 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <param name="value">設定する値を指定します。</param>
        public static void SetDialogInfo(DependencyObject target, IDialogInfo value)
        {
            target.SetValue(DialogInfoProperty, value);
        }

        /// <summary>
        /// DialogInfo 添付プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnDialogInfoPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var element = sender as UIElement;
            if (element == null)
                return;

            var info = GetDialogInfo(element);
            if (info == null)
                return;

            var result = YKToolkit.Controls.MessageBox.Show(Window.GetWindow(element), info.Message, info.Title, info.MessageBoxButton, info.MessageBoxImage);
            SetMessageBoxResult(element, result);
        }
        #endregion DialogInfo 添付プロパティ

        #region MessageBoxResult 添付プロパティ
        /// <summary>
        /// MessageBoxResult 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty MessageBoxResultProperty = DependencyProperty.RegisterAttached("MessageBoxResult", typeof(MessageBoxResult), typeof(MessageDialogBehavior), new FrameworkPropertyMetadata(MessageBoxResult.OK, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// MessageBoxResult 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <returns>取得した値を返します。</returns>
        public static MessageBoxResult GetMessageBoxResult(DependencyObject target)
        {
            return (MessageBoxResult)target.GetValue(MessageBoxResultProperty);
        }

        /// <summary>
        /// MessageBoxResult 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <param name="value">設定する値を指定します。</param>
        public static void SetMessageBoxResult(DependencyObject target, MessageBoxResult value)
        {
            target.SetValue(MessageBoxResultProperty, value);
        }
        #endregion MessageBoxResult 添付プロパティ
    }
}
