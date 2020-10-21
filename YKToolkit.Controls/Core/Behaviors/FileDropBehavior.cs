namespace YKToolkit.Controls.Behaviors
{
    using System;
    using System.Windows;
    using System.Windows.Threading;

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

            System.Diagnostics.Debug.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId);
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
            System.Diagnostics.Debug.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId);
            var element = sender as UIElement;
            if (element == null)
                return;

            var callback = GetCallback(element);
            if (callback == null)
                return;

            var files = e.Data.GetData(DataFormats.FileDrop);
            // ドラッグ＆ドロップの処理は OLE に渡される。
            // OLE はターゲットとソースの両方を扱うので別スレッドで動作する。
            // このため、ドロップ処理で発生する例外は OLE の管轄になるが、
            // 発生した例外を元のアプリケーションに渡す方法がない。
            // よって Drop イベントハンドラで発生する例外は握りつぶされてしまう。
            // これを防ぐため、例外が発生した場合は UI スレッドに非同期で渡すようにする。
            // ちなみに Invoke() メソッドだと例外情報は渡せない模様。謎。
            // とにかく Drop イベントハンドラから抜けないとダメらしい。
            if (files != null)
            {
                Dispatcher.CurrentDispatcher.BeginInvoke((Action<string[]>)(p =>
                {
                    callback(p);
                }), files);
                e.Handled = true;
            }
        }
    }
}
