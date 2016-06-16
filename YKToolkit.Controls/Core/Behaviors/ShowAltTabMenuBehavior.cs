namespace YKToolkit.Controls.Behaviors
{
    using System;
    using System.Windows;
    using System.Windows.Interop;

    /// <summary>
    /// Alt+Tab メニューにウィンドウを表示させないようにするためのビヘイビアを表します。
    /// </summary>
    public class ShowAltTabMenuBehavior
    {
        #region IsEnabled 添付プロパティ
        /// <summary>
        /// IsEnabled 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(ShowAltTabMenuBehavior), new PropertyMetadata(true, OnIsEnabledPropertyChanged));

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
        private static void OnIsEnabledPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var w = sender as Window;
            if (w == null)
                return;

            var isEnabled = (bool)e.NewValue;
            if (isEnabled)
            {
                w.SourceInitialized -= OnSourceInitialized;
            }
            else
            {
                w.SourceInitialized += OnSourceInitialized;
            }
        }
        #endregion IsEnabled 添付プロパティ

        /// <summary>
        /// Window のウィンドウハンドルが決定される最速のイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnSourceInitialized(object sender, EventArgs e)
        {
            var w = sender as Window;
            var helper = new WindowInteropHelper(w);
            var exStyle = User32.GetWindowLongPtr(helper.Handle, (int)User32.GWLs.GWL_EXSTYLE).ToInt32();
            exStyle |= (int)User32.WSs.WS_EX_TOOLWINDOW;
            User32.SetWindowLongPtr(helper.Handle, (int)User32.GWLs.GWL_EXSTYLE, new IntPtr(exStyle));
        }
    }
}
