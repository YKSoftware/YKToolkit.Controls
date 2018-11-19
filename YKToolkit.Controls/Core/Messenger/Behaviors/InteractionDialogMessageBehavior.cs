namespace YKToolkit.Controls.Behaviors
{
    using System;
    using System.Windows;

    /// <summary>
    /// メッセンジャーシステムによるダイアログ表示のビヘイビアを提供します。
    /// </summary>
    public class InteractionDialogMessageBehavior
    {
        /// <summary>
        /// string 型の MessageKey 添付プロパティを定義します。
        /// </summary>
        public static readonly DependencyProperty MessageKeyProperty = DependencyProperty.RegisterAttached("MessageKey", typeof(string), typeof(InteractionDialogMessageBehavior), new PropertyMetadata(null, OnMessageKeyPropertyChanged));

        /// <summary>
        /// MessageKey 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <returns>取得した値を返します。</returns>
        public static string GetMessageKey(DependencyObject target)
        {
            return (string)target.GetValue(MessageKeyProperty);
        }

        /// <summary>
        /// MessageKey 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <param name="value">設定する値を指定します。</param>
        public static void SetMessageKey(DependencyObject target, string value)
        {
            target.SetValue(MessageKeyProperty, value);
        }

        /// <summary>
        /// MessageKey 添付プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnMessageKeyPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var w = sender as Window ?? Window.GetWindow(sender);
            var key = GetMessageKey(sender);
            var oldKey = e.OldValue as string;
            var newKey = e.NewValue as string;
            if (!string.IsNullOrEmpty(oldKey))
            {
                // メッセージ登録解除
                InteractionMessageListener.Unregister(oldKey);
            }
            if (!string.IsNullOrEmpty(newKey))
            {
                // メッセージ登録
                InteractionMessageListener.Register(newKey, message =>
                {
                    var m = message as DialogMessage;
                    if (m == null)
                        throw new Exception("メッセージが " + message.GetType().FullName + " クラスです。YKToolkit.Controls.Behaviors.InteractionDialogMessageBehavior クラスで扱えるメッセージは YKToolkit.Controls.DialogMessage クラスまたはその派生クラスです。");
                    return YKToolkit.Controls.MessageBox.Show(m.Location == WindowStartupLocation.CenterOwner ? w : null, m.Message, m.Caption, m.DialogButton, m.DialogImage, m.ButtonCaptions);
                });
            }
        }
    }
}
