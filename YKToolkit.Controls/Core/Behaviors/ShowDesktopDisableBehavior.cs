namespace YKToolkit.Controls.Behaviors
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows;

    internal static class NativeMethods
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, ShowDesktopDisableBehavior.WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        [DllImport("user32.dll")]
        internal static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        [DllImport("user32.dll")]
        internal static extern int GetClassName(IntPtr hwnd, StringBuilder name, int count);
    }

    /// <summary>
    /// 「デスクトップを表示」ボタンを押したとき、ウィンドウを最小化するかどうかを制御するためのビヘイビアを表します。
    /// </summary>
    public class ShowDesktopDisableBehavior
    {
        #region IsEnabled 添付プロパティ
        /// <summary>
        /// IsEnabled 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(ShowDesktopDisableBehavior), new PropertyMetadata(OnIsEnabledPropertyChanged));

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
                w.SourceInitialized += OnSourceInitialized;
            }
            else
            {
                RemoveHook(w);
                w.SourceInitialized -= OnSourceInitialized;
            }
        }
        #endregion IsEnabled 添付プロパティ

        /// <summary>
        /// Window クラスのウィンドウハンドルが決定する最速のイベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnSourceInitialized(object sender, EventArgs e)
        {
            var w = sender as Window;
            AddHook(w);
        }

        private const uint WINEVENT_OUTOFCONTEXT = 0u;
        private const uint EVENT_SYSTEM_FOREGROUND = 3u;

        private const string WORKERW = "WorkerW";
        private const string PROGMAN = "Progman";

        /// <summary>
        /// フックを開始します。
        /// </summary>
        /// <param name="window">対象とするウィンドウを指定します。</param>
        public static void AddHook(Window window)
        {
            if (IsHooked)
            {
                return;
            }

            IsHooked = true;

            _window = window;
            _delegate = new WinEventDelegate(WinEventHook);
            _hookIntPtr = NativeMethods.SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, _delegate, 0, 0, WINEVENT_OUTOFCONTEXT);
        }

        /// <summary>
        /// フックを解除します。
        /// </summary>
        /// <param name="window">対象とするウィンドウを指定します。</param>
        public static void RemoveHook(Window window)
        {
            if (!IsHooked)
            {
                return;
            }

            IsHooked = false;

            NativeMethods.UnhookWinEvent(_hookIntPtr.Value);

            _delegate = null;
            _hookIntPtr = null;
            _window = null;
        }

        private static string GetWindowClass(IntPtr hwnd)
        {
            StringBuilder _sb = new StringBuilder(32);
            NativeMethods.GetClassName(hwnd, _sb, _sb.Capacity);
            return _sb.ToString();
        }

        internal delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        private static void WinEventHook(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (eventType == EVENT_SYSTEM_FOREGROUND)
            {
                string _class = GetWindowClass(hwnd);

                if (string.Equals(_class, WORKERW, StringComparison.Ordinal) /*|| string.Equals(_class, PROGMAN, StringComparison.Ordinal)*/ )
                {
                    _window.Topmost = true;
                }
                else
                {
                    _window.Topmost = false;
                }
            }
        }

        /// <summary>
        /// フック済みかどうかを取得します。
        /// </summary>
        public static bool IsHooked { get; private set; }

        private static IntPtr? _hookIntPtr { get; set; }

        private static WinEventDelegate _delegate { get; set; }

        private static Window _window { get; set; }
    }
}