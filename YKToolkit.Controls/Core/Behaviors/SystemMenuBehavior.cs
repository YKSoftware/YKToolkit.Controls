namespace YKToolkit.Controls.Behaviors
{
    using System;
    using System.Windows;
    using System.Windows.Interop;

    /// <summary>
    /// Window クラスのシステムメニューの有効/無効を設定するためのビヘイビアを提供します。
    /// </summary>
    public class SystemMenuBehavior
    {
        #region IsEnabled 添付プロパティ
        /// <summary>
        /// IsEnabled 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(SystemMenuBehavior), new PropertyMetadata(true, OnIsEnabledPropertyChanged));

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
            var window = sender as Window;
            if (window == null)
                return;

            window.SourceInitialized += (obj, args) =>
            {
                var w = obj as Window;
                if (w == null)
                    return;

                var handle = (new WindowInteropHelper(w)).Handle;
                var original = User32.GetWindowLong(handle, (int)User32.GWLs.GWL_STYLE);
                var current = GetWindowStyle(w, original);
                if (original != current)
                {
                    User32.SetWindowLong(handle, (int)User32.GWLs.GWL_STYLE, current);
                }

                // メッセージ処理をフック
                var hwndSource = HwndSource.FromHwnd(handle);
                hwndSource.AddHook(WndProc);
            };
        }

        private static int GetWindowStyle(DependencyObject obj, int windowStyle)
        {
            var style = (int)User32.WSs.WS_SYSMENU;
            var isEnabled = GetIsEnabled(obj);
            if (isEnabled)
            {
                windowStyle |= style;
            }
            else
            {
                windowStyle &= ~style;
            }

            return windowStyle;
        }

        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if ((msg == (int)User32.WMs.WM_SYSKEYDOWN) && (wParam.ToInt32() == (int)User32.VKs.VK_F4))
            {
                handled = true;
            }
            else if (msg == (int)User32.WMs.WM_NCLBUTTONDBLCLK)
            {
                // 非クライアント領域ダブルクリックによる最大化を無効にする
                //handled = true;
            }

            return IntPtr.Zero;
        }
        #endregion IsEnabled 添付プロパティ
    }
}
