namespace YKToolkit.Controls
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    /// <summary>
    /// マウスフックをおこなうためのクラスを表します。
    /// </summary>
    public class MouseHook : IDisposable
    {
        #region MSLLHOOKSTRUCT 構造体

        /// <summary>
        /// 低レベルなマウスイベント情報を表します。
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            /// <summary>
            /// マウス座標を表します。
            /// </summary>
            public User32.POINT Point;

            /// <summary>
            /// If the message is WM_MOUSEWHEEL, the high-order word of this member is the wheel delta. The low-order word is reserved. A positive value indicates that the wheel was rotated forward, away from the user; a negative value indicates that the wheel was rotated backward, toward the user. One wheel click is defined as WHEEL_DELTA, which is 120.
            /// If the message is WM_XBUTTONDOWN, WM_XBUTTONUP, WM_XBUTTONDBLCLK, WM_NCXBUTTONDOWN, WM_NCXBUTTONUP, or WM_NCXBUTTONDBLCLK, the high-order word specifies which X button was pressed or released, and the low-order word is reserved. This value can be one or more of the following values. Otherwise, mouseData is not used. 
            /// 0x0001 : XBUTTON1, 0x0002 : XBUTTON2.
            /// </summary>
            public uint MouseData;

            /// <summary>
            /// The event-injected flags.
            /// </summary>
            public uint Flags;

            /// <summary>
            /// タイムスタンプを表します。
            /// </summary>
            public uint Time;

            /// <summary>
            /// メッセージに関連する付加情報を表します。
            /// </summary>
            public IntPtr dwExtraInfo;
        }

        #endregion MSLLHOOKSTRUCT 構造体

        [DllImport("User32.dll")]
        private static extern uint GetDoubleClickTime();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, MouseHookEventHandler lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private delegate IntPtr MouseHookEventHandler(int nCode, IntPtr wParam, IntPtr lParam);
        private MouseHookEventHandler _hookHandler;

        /// <summary>
        /// イベントハンドラのデリゲートを表します。
        /// </summary>
        /// <param name="e">イベント引数</param>
        public delegate void MouseHookCallback(MSLLHOOKSTRUCT e);

        #region イベント

        /// <summary>
        /// マウス左ボタンを押したときに発生します。
        /// </summary>
        public event MouseHookCallback MouseLeftButtonDown;

        /// <summary>
        /// マウス左ボタンを離したときに発生します。
        /// </summary>
        public event MouseHookCallback MouseLeftButtonUp;

        /// <summary>
        /// マウス右ボタンを押したときに発生します。
        /// </summary>
        public event MouseHookCallback MouseRightButtonDown;

        /// <summary>
        /// マウス右ボタンを離したときに発生します。
        /// </summary>
        public event MouseHookCallback MouseRightButtonUp;

        /// <summary>
        /// マウスを動かしたときに発生します。
        /// </summary>
        public event MouseHookCallback MouseMove;

        /// <summary>
        /// マウスホイールを動かしたときに発生します。
        /// </summary>
        public event MouseHookCallback MouseWheel;

        /// <summary>
        /// ダブルクリックしたときに発生します。
        /// </summary>
        public event MouseHookCallback DoubleClick;

        /// <summary>
        /// マウス中ボタンを押したときに発生します。
        /// </summary>
        public event MouseHookCallback MouseMiddleButtonDown;

        /// <summary>
        /// マウス中ボタンを離したときに発生します。
        /// </summary>
        public event MouseHookCallback MouseMiddleButtonUp;

        #endregion イベント

        /// <summary>
        /// ローレベルのマウスフック ID
        /// </summary>
        private IntPtr _hookID = IntPtr.Zero;

        /// <summary>
        /// マウスフックを開始します。
        /// </summary>
        public void Hook()
        {
            this._hookHandler = HookProc;
            this._hookID = SetHook(this._hookHandler);
        }

        /// <summary>
        /// マウスフックを終了します。
        /// </summary>
        public void UnHook()
        {
            if (this._hookID != IntPtr.Zero)
            {
                UnhookWindowsHookEx(this._hookID);
                this._hookID = IntPtr.Zero;
            }
        }

        /// <summary>
        /// グローバルフックを登録します。
        /// </summary>
        /// <param name="proc">コールバックメソッドを指定します。</param>
        /// <returns>SetWindowsHookEx 関数の戻り値を返します。</returns>
        private IntPtr SetHook(MouseHookEventHandler proc)
        {
            using (var module = Process.GetCurrentProcess().MainModule)
            {
                return SetWindowsHookEx((int)User32.WHs.WH_MOUSE_LL, proc, GetModuleHandle(module.ModuleName), 0);
            }
        }

        /// <summary>
        /// フック処理をおこないます。
        /// </summary>
        /// <param name="nCode">ウィンドウメッセージなどを受け取ります。</param>
        /// <param name="wParam">パラメータを受け取ります。</param>
        /// <param name="lParam">パラメータを受け取ります。</param>
        /// <returns>CallNextHookEx 関数の戻り値を返します。</returns>
        private IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                MouseHookCallback h = null;

                switch ((User32.WMs)wParam)
                {
                    case User32.WMs.WM_MOUSEMOVE:
                        h = this.MouseMove;
                        break;

                    case User32.WMs.WM_LBUTTONDOWN:
                        if (this._hasLeftButtonDown)
                        {
                            if ((uint)this._timer.MilliSeconds <= GetDoubleClickTime())
                            {
                                h = this.DoubleClick;
                                this._hasLeftButtonDown = false;
                            }
                            else
                            {
                                h = this.MouseLeftButtonDown;
                            }
                        }
                        else
                        {
                            h = this.MouseLeftButtonDown;
                            this._hasLeftButtonDown = true;
                        }
                        this._timer.Start();
                        break;

                    case User32.WMs.WM_LBUTTONUP:
                        h = this.MouseLeftButtonUp;
                        break;

                    case User32.WMs.WM_LBUTTONDBLCLK:
                        h = this.DoubleClick;
                        break;

                    case User32.WMs.WM_RBUTTONDOWN:
                        h = this.MouseRightButtonDown;
                        break;

                    case User32.WMs.WM_RBUTTONUP:
                        h = this.MouseRightButtonUp;
                        break;

                    case User32.WMs.WM_MBUTTONDOWN:
                        h = this.MouseMiddleButtonDown;
                        break;

                    case User32.WMs.WM_MBUTTONUP:
                        h = this.MouseMiddleButtonUp;
                        break;

                    case User32.WMs.WM_MOUSEWHEEL:
                        h = this.MouseWheel;
                        break;
                }

                if (h != null)
                    h((MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT)));
            }

            return CallNextHookEx(this._hookID, nCode, wParam, lParam);
        }

        /// <summary>
        /// リソースを破棄します。
        /// </summary>
        public void Dispose()
        {
            if (!this._isDisposed)
            {
                UnHook();
            }

            this._isDisposed = true;
        }

        private bool _isDisposed;

        private QpTimer _timer = new QpTimer();

        private bool _hasLeftButtonDown;
    }
}
