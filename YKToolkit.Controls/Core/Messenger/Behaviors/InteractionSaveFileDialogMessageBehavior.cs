namespace YKToolkit.Controls.Behaviors
{
    using Microsoft.Win32;
    using System;
    using System.Windows;

    /// <summary>
    /// メッセンジャーシステムによるファイル書き込みコモンダイアログ表示のビヘイビアを提供します。
    /// </summary>
    public class InteractionSaveFileDialogMessageBehavior
    {
        /// <summary>
        /// string 型の MessageKey 添付プロパティを定義します。
        /// </summary>
        public static readonly DependencyProperty MessageKeyProperty = DependencyProperty.RegisterAttached("MessageKey", typeof(string), typeof(InteractionSaveFileDialogMessageBehavior), new PropertyMetadata(null, OnMessageKeyPropertyChanged));

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
                    var m = message as SaveFileDialogMessage;
                    if (m == null)
                        throw new Exception("メッセージが " + message.GetType().FullName + " クラスです。YKToolkit.Controls.Behaviors.InteractionSaveFileDialogMessageBehavior クラスで扱えるメッセージは YKToolkit.Controls.SaveFileDialogMessage クラスまたはその派生クラスです。");
                    var dlg = new SaveFileDialog();
                    dlg.Title = m.Caption;
                    dlg.FileName = m.FileName;
                    dlg.Filter = m.FileFilter;
                    var result = dlg.ShowDialog(w);
                    return (result.HasValue && result.Value) ? dlg.FileName : null;
                });
            }
        }
    }
}
