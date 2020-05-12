namespace YKToolkit.Controls
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    /// <summary>
    /// キーボードフックをおこなうためのクラスを表します。
    /// </summary>
    public class KeyboardHook : IDisposable
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookEventHandler lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private delegate IntPtr KeyboardHookEventHandler(int nCode, IntPtr wParam, IntPtr lParam);
        private KeyboardHookEventHandler _hookHandler;

        /// <summary>
        /// イベントハンドラのデリゲートを表します。
        /// </summary>
        /// <param name="key">イベント引数</param>
        public delegate bool KeyboardHookCallback(User32.VKs key);

        #region イベント

        /// <summary>
        /// キーを押したときに発生します。
        /// </summary>
        public event KeyboardHookCallback KeyDown;

        /// <summary>
        /// キーを離したときに発生します。
        /// </summary>
        public event KeyboardHookCallback KeyUp;

        #endregion イベント

        /// <summary>
        /// ローレベルのキーボードフック ID
        /// </summary>
        private IntPtr _hookID = IntPtr.Zero;

        /// <summary>
        /// キーボードフックを開始します。
        /// </summary>
        public void Hook()
        {
            this._hookHandler = HookProc;
            this._hookID = SetHook(this._hookHandler);
        }

        /// <summary>
        /// キーボードフックを終了します。
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
        private IntPtr SetHook(KeyboardHookEventHandler proc)
        {
            using (var module = Process.GetCurrentProcess().MainModule)
            {
                return SetWindowsHookEx((int)User32.WHs.WH_KEYBOARD_LL, proc, GetModuleHandle(module.ModuleName), 0);
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
                KeyboardHookCallback h = null;

                switch ((User32.WMs)wParam)
                {
                    case User32.WMs.WM_KEYDOWN:
                        h = this.KeyDown;
                        break;

                    case User32.WMs.WM_SYSKEYDOWN:
                        h = this.KeyDown;
                        break;

                    case User32.WMs.WM_KEYUP:
                        h = this.KeyUp;
                        break;

                    case User32.WMs.WM_SYSKEYUP:
                        h = this.KeyUp;
                        break;
                }

                if (h != null)
                {
                    if (h((User32.VKs)Marshal.ReadInt32(lParam)))
                        return new IntPtr(1);
                }
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
    }
}
