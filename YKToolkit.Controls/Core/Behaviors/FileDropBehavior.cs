namespace YKToolkit.Controls.Behaviors
{
    using System;
    using System.Windows;

    /// <summary>
    /// ファイルドロップ時にフルパスをコールバックに渡すためのビヘイビアを表します。
    /// </summary>
    public class FileDropBehavior
    {
        #region Callback 添付プロパティ
        /// <summary>
        /// Callback 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty CallbackProperty = DependencyProperty.RegisterAttached("Callback", typeof(Action<string[]>), typeof(FileDropBehavior), new PropertyMetadata(null, OnCallbackPropertyChanged));

        /// <summary>
        /// Callback 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <returns>取得した値を返します。</returns>
        public static Action<string[]> GetCallback(DependencyObject target)
        {
            return (Action<string[]>)target.GetValue(CallbackProperty);
        }

        /// <summary>
        /// Callback 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <param name="value">設定する値を指定します。</param>
        public static void SetCallback(DependencyObject target, Action<string[]> value)
        {
            target.SetValue(CallbackProperty, value);
        }

        /// <summary>
        /// Callback 添付プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnCallbackPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as UIElement;
            if (control == null)
                return;

            var callback = GetCallback(control);
            if (callback != null)
            {
                control.AllowDrop = true;
                control.PreviewDragOver += OnPreviewDragOver;
                control.Drop += OnDrop;
            }
            else
            {
                control.PreviewDragOver -= OnPreviewDragOver;
                control.Drop -= OnDrop;
            }
        }
        #endregion Callback 添付プロパティ

        /// <summary>
        /// PreviewDragOver イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnPreviewDragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
            e.Handled = e.Data.GetDataPresent(DataFormats.FileDrop);
        }

        /// <summary>
        /// Drop イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnDrop(object sender, DragEventArgs e)
        {
            var element = sender as UIElement;
            if (element == null)
                return;

            var callback = GetCallback(element);
            if (callback == null)
                return;

            callback(e.Data.GetData(DataFormats.FileDrop) as string[]);
            e.Handled = true;
        }
    }
}
