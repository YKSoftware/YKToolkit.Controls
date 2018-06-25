namespace YKToolkit.Controls
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    /// <summary>
    /// user32.dll を使用するための変数またはメソッドを提供します。
    /// </summary>
    public static class User32
    {
        /// <summary>
        /// GetWindowLong(x86) 関数の導入
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル</param>
        /// <param name="nIndex">インデックス</param>
        /// <returns>戻り値</returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern IntPtr GetWindowLongPtr32(IntPtr hWnd, int nIndex);

        /// <summary>
        /// GetWindowLong(x64) 関数の導入
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル</param>
        /// <param name="nIndex">インデックス</param>
        /// <returns>戻り値</returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        private static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

        /// <summary>
        /// GetWindowLong 関数
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル</param>
        /// <param name="nIndex">インデックス</param>
        /// <returns>戻り値</returns>
        public static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
        {
            return IntPtr.Size == 8 ? GetWindowLongPtr64(hWnd, nIndex) : GetWindowLongPtr32(hWnd, nIndex);
        }

        /// <summary>
        /// SetWindowLong(x86) 関数の導入
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル</param>
        /// <param name="nIndex">インデックス</param>
        /// <param name="dwNewLong">設定値</param>
        /// <returns>戻り値</returns>
        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLongPtr32(IntPtr hWnd, int nIndex, int dwNewLong);

        /// <summary>
        /// SetWindowLong(x64) 関数の導入
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル</param>
        /// <param name="nIndex">インデックス</param>
        /// <param name="dwNewLong">設定値</param>
        /// <returns>戻り値</returns>
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        /// <summary>
        /// SetWindowLong 関数
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル</param>
        /// <param name="nIndex">インデックス</param>
        /// <param name="dwNewLong">設定値</param>
        /// <returns>戻り値</returns>
        public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            return IntPtr.Size == 8 ? SetWindowLongPtr64(hWnd, nIndex, dwNewLong) :
                new IntPtr(SetWindowLongPtr32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        /// <summary>
        /// SetWindowPlacement 関数
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル</param>
        /// <param name="lpwndpl">ウィンドウの位置やサイズなどの情報を含む構造体</param>
        /// <returns>戻り値</returns>
        [DllImport("user32.dll")]
        public static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WINDOWPLACEMENT lpwndpl);

        /// <summary>
        /// GetWindowPlacement 関数
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル</param>
        /// <param name="lpwndpl">ウィンドウの位置やサイズなどの情報を含む構造体</param>
        /// <returns>戻り値</returns>
        [DllImport("user32.dll")]
        public static extern bool GetWindowPlacement(IntPtr hWnd, out WINDOWPLACEMENT lpwndpl);

        /// <summary>
        /// SendMessage 関数の導入
        /// </summary>
        /// <param name="hwnd">ウィンドウハンドル</param>
        /// <param name="msg">ウィンドウメッセージ</param>
        /// <param name="wParam">パラメータ</param>
        /// <param name="lParam">パラメータ</param>
        /// <returns>戻り値</returns>
        [DllImport("user32.dll")]
        public extern static int SendMessage(IntPtr hwnd, uint msg, IntPtr wParam, ref IntPtr lParam);

        /// <summary>
        /// RegisterHotKey 関数の定義
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル</param>
        /// <param name="id">固有識別子</param>
        /// <param name="MOD_KEY">ホットキーに対する修飾キー</param>
        /// <param name="VK">登録するホットキー</param>
        /// <returns>0:失敗 (既に他が登録済み)/0以外:成功</returns>
        [DllImport("user32.dll")]
        public extern static int RegisterHotKey(IntPtr hWnd, int id, int MOD_KEY, int VK);

        /// <summary>
        /// UnregisterHotKey 関数の定義
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル</param>
        /// <param name="id">固有識別子</param>
        /// <returns>0:失敗/0以外:成功</returns>
        [DllImport("user32.dll")]
        public extern static int UnregisterHotKey(IntPtr hWnd, int id);

        #region GetWindowLong
        /// <summary>
        /// GetWindowLong に関するパラメータを表します。
        /// </summary>
        public enum GWLs : int
        {
            /// <summary>
            /// GetWindowLong 関数に関する Style
            /// </summary>
            GWL_STYLE = -16,

            /// <summary>
            /// GetWindowLong 関数に関する StyleEx
            /// </summary>
            GWL_EXSTYLE = -20,
        }
        #endregion GetWindowLong

        #region WindowStyle
        /// <summary>
        /// WindowStyle に関するパラメータを表します。
        /// </summary>
        public enum WSs : int
        {
            /// <summary>
            /// WindowStyle - WS_NONE
            /// </summary>
            WS_NONE = 0x00000000,

            /// <summary>
            /// WindowStyle - WS_EX_TOOLWINDOW
            /// </summary>
            WS_EX_TOOLWINDOW = 0x00000080,

            /// <summary>
            /// WindowStyle - WS_EX_CONTEXTHELP
            /// </summary>
            WS_EX_CONTEXTHELP = 0x00400,

            /// <summary>
            /// WindowStyle - WS_MAXIMIZEBOX
            /// </summary>
            WS_MAXIMIZEBOX = 0x10000,

            /// <summary>
            /// WindowStyle - WS_MINIMIZEBOX
            /// </summary>
            WS_MINIMIZEBOX = 0x20000,

            /// <summary>
            /// WindowStyle - WS_SYSMENU
            /// </summary>
            WS_SYSMENU = 0x80000,
        }
        #endregion WindowStyle

        #region WindowPlacement
        /// <summary>
        /// ウィンドウのサイズや位置などの情報を含む構造体を表します。
        /// </summary>
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {
            /// <summary>
            /// この構造体の長さをバイト数で表します。
            /// </summary>
            public int length;

            /// <summary>
            /// ウィンドウを元に戻すメソッドおよび最小化されたウィンドウの位置を制御するフラグを表します。
            /// </summary>
            public int flags;

            /// <summary>
            /// ウィンドウの状態を表します。
            /// </summary>
            public SW showCmd;

            /// <summary>
            /// ウィンドウを最小化する直前のウィンドウの左上隅点座標を表します。
            /// </summary>
            public POINT minPosition;

            /// <summary>
            /// ウィンドウを最大化する直前のウィンドウの左上隅点座標を表します。
            /// </summary>
            public POINT maxPosition;

            /// <summary>
            /// ウィンドウが正常に復元された位置にあるときの座標を表します。
            /// </summary>
            public RECT normalPosition;
        }

        /// <summary>
        /// 2 次元座標を表します。
        /// </summary>
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            /// <summary>
            /// X 座標を表します。
            /// </summary>
            public int X;

            /// <summary>
            /// Y 座標を表します。
            /// </summary>
            public int Y;

            /// <summary>
            /// 新しいインスタンスを生成します。
            /// </summary>
            /// <param name="x">X 座標を指定します。</param>
            /// <param name="y">Y 座標を指定します。</param>
            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            /// <summary>
            /// 文字列変換します。
            /// </summary>
            /// <returns>変換した文字列を返します。</returns>
            public override string ToString()
            {
                return string.Format("({0}, {1})", this.X, this.Y);
            }
        }

        /// <summary>
        /// 2 次元座標の領域を表します。
        /// </summary>
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            /// <summary>
            /// 領域の左端座標を表します。
            /// </summary>
            public int Left;

            /// <summary>
            /// 領域の上端座標を表します。
            /// </summary>
            public int Top;

            /// <summary>
            /// 領域の右端座標を表します。
            /// </summary>
            public int Right;

            /// <summary>
            /// 領域の下端座標を表します。
            /// </summary>
            public int Bottom;

            /// <summary>
            /// 新しいインスタンスを生成します。
            /// </summary>
            /// <param name="left">領域の左端座標を指定します。</param>
            /// <param name="top">領域の上端座標を指定します。</param>
            /// <param name="right">領域の右端座標を指定します。</param>
            /// <param name="bottom">領域の下端座標を指定します。</param>
            public RECT(int left, int top, int right, int bottom)
            {
                this.Left = left;
                this.Top = top;
                this.Right = right;
                this.Bottom = bottom;
            }
        }

        /// <summary>
        /// ウィンドウの状態を表します。
        /// </summary>
        public enum SW
        {
            /// <summary>
            /// 非表示を表します。
            /// </summary>
            HIDE = 0,

            /// <summary>
            /// 通常のウィンドウ状態を表します。
            /// </summary>
            SHOWNORMAL = 1,

            /// <summary>
            /// 最小化されたウィンドウを表します。
            /// </summary>
            SHOWMINIMIZED = 2,

            /// <summary>
            /// 最大化されたウィンドウを表します。
            /// </summary>
            SHOWMAXIMIZED = 3,

            /// <summary>
            /// アクティブ化されたウィンドウを表します。
            /// </summary>
            SHOWACTIVATE = 4,

            /// <summary>
            /// アクティブ化されたウィンドウを表します。
            /// </summary>
            SHOW = 5,

            /// <summary>
            /// 最小化状態を表します。
            /// </summary>
            MINIMIZE = 6,

            /// <summary>
            /// 最小化状態を表します。
            /// </summary>
            SHOWMINNOACTIVE = 7,

            /// <summary>
            /// 通常の状態を表します。
            /// </summary>
            SHOWNA = 8,

            /// <summary>
            /// 元のサイズを表します。
            /// </summary>
            RESTORE = 9,

            /// <summary>
            /// 既定値を表します。
            /// </summary>
            SHOWDEFAULT = 10,
        }
        #endregion WindowPlacement

        #region WindowMessage
        /// <summary>
        /// ウィンドウメッセージを表します。
        /// </summary>
        public enum WMs : int
        {
            /// <summary>
            /// WindowMessage - WM_NCLBUTTONDBLCLK
            /// </summary>
            WM_NCLBUTTONDBLCLK = 0x00A3,

            /// <summary>
            /// WindowMessage - WM_KEYDOWN
            /// </summary>
            WM_KEYDOWN = 0x0100,

            /// <summary>
            /// WindowMessage - WM_KEYUP
            /// </summary>
            WM_KEYUP = 0x0101,

            /// <summary>
            /// WindowMessage - WM_CHAR
            /// </summary>
            WM_CHAR = 0x0102,

            /// <summary>
            /// WindowMessage - WM_SYSKEYDOWN
            /// </summary>
            WM_SYSKEYDOWN = 0x0104,

            /// <summary>
            /// WindowMessage - WM_SYSKEYUP
            /// </summary>
            WM_SYSKEYUP = 0x0105,

            /// <summary>
            /// WindowMessage - WM_SYSCOMMAND
            /// </summary>
            WM_SYSCOMMAND = 0x0112,

            /// <summary>
            /// WindowMessage - WM_HOTKEY
            /// </summary>
            WM_HOTKEY = 0x0312,

            /// <summary>
            /// WindowMessage - WM_MOUSEMOVE
            /// </summary>
            WM_MOUSEMOVE = 0x0200,

            /// <summary>
            /// WindowMessage - WM_LBUTTONDOWN
            /// </summary>
            WM_LBUTTONDOWN = 0x0201,

            /// <summary>
            /// WindowMessage - WM_LBUTTONUP
            /// </summary>
            WM_LBUTTONUP = 0x0202,

            /// <summary>
            /// WindowMessage - WM_LBUTTONDBLCLK
            /// </summary>
            WM_LBUTTONDBLCLK = 0x0203,

            /// <summary>
            /// WindowMessage - WM_RBUTTONDOWN
            /// </summary>
            WM_RBUTTONDOWN = 0x0204,

            /// <summary>
            /// WindowMessage - WM_RBUTTONUP
            /// </summary>
            WM_RBUTTONUP = 0x0205,

            /// <summary>
            /// WindowMessage - WM_MBUTTONDOWN
            /// </summary>
            WM_MBUTTONDOWN = 0x0207,

            /// <summary>
            /// WindowMessage - WM_MBUTTONUP
            /// </summary>
            WM_MBUTTONUP = 0x0208,

            /// <summary>
            /// WindowMessage - WM_MOUSEWHEEL
            /// </summary>
            WM_MOUSEWHEEL = 0x020A,
        }
        #endregion WindowMessage

        #region WindowsHook
        /// <summary>
        /// Windows Hook を表します。
        /// </summary>
        public enum WHs : int
        {
            /// <summary>
            /// WindowsHook - WH_KEYBOARD_LL
            /// </summary>
            WH_KEYBOARD_LL = 13,

            /// <summary>
            /// WindowsHook - WH_MOUSE_LL
            /// </summary>
            WH_MOUSE_LL = 14,
        }
        #endregion WindowsHook

        #region SystemCommand
        /// <summary>
        /// システムコマンドを表します。
        /// </summary>
        public enum SCs : int
        {
            /// <summary>
            /// SystemCommand - SC_SIZE
            /// </summary>
            SC_SIZE = 0xF000,

            /// <summary>
            /// SystemCommand - SC_MOVE
            /// </summary>
            SC_MOVE = 0xF010,

            /// <summary>
            /// SystemCommand - SC_MINIMIZE
            /// </summary>
            SC_MINIMIZE = 0xF020,

            /// <summary>
            /// SystemCommand - SC_MAXIMIZE
            /// </summary>
            SC_MAXIMIZE = 0xF030,

            /// <summary>
            /// SystemCommand - SC_NEXTWINDOW
            /// </summary>
            SC_NEXTWINDOW = 0xF040,

            /// <summary>
            /// SystemCommand - SC_PREVWINDOW
            /// </summary>
            SC_PREVWINDOW = 0xF050,

            /// <summary>
            /// SystemCommand - SC_CLOSE
            /// </summary>
            SC_CLOSE = 0xF060,

            /// <summary>
            /// SystemCommand - SC_VSCROLL
            /// </summary>
            SC_VSCROLL = 0xF070,

            /// <summary>
            /// SystemCommand - SC_HSCROLL
            /// </summary>
            SC_HSCROLL = 0xF080,

            /// <summary>
            /// SystemCommand - SC_MOUSEMENU
            /// </summary>
            SC_MOUSEMENU = 0xF090,

            /// <summary>
            /// SystemCommand - SC_KEYMENU
            /// </summary>
            SC_KEYMENU = 0xF100,

            /// <summary>
            /// SystemCommand - SC_RESTORE
            /// </summary>
            SC_RESTORE = 0xF120,

            /// <summary>
            /// SystemCommand - SC_TASKLIST
            /// </summary>
            SC_TASKLIST = 0xF130,

            /// <summary>
            /// SystemCommand - SC_SCREENSAVE
            /// </summary>
            SC_SCREENSAVE = 0xF140,

            /// <summary>
            /// SystemCommand - SC_HOTKEY
            /// </summary>
            SC_HOTKEY = 0xF150,

            /// <summary>
            /// SystemCommand - SC_DEFAULT
            /// </summary>
            SC_DEFAULT = 0xF160,

            /// <summary>
            /// SystemCommand - SC_MONITORPOWER
            /// </summary>
            SC_MONITORPOWER = 0xF170,

            /// <summary>
            /// SystemCommand - SC_CONTEXTHELP
            /// </summary>
            SC_CONTEXTHELP = 0xF180,
        }
        #endregion SystemCommand

        #region VirtualKeyboard
        /// <summary>
        /// VirtualKeyboard を表します。
        /// </summary>
        public enum VKs : int
        {
            /// <summary>
            /// Undefined
            /// </summary>
            VK_0x00 = 0x00,

            /// <summary>
            /// Left mouse button
            /// </summary>
            VK_LBUTTON = 0x01,

            /// <summary>
            /// Right mouse button
            /// </summary>
            VK_RBUTTON = 0x02,

            /// <summary>
            /// Control-break processing
            /// </summary>
            VK_CANCEL = 0x03,

            /// <summary>
            /// Middle mouse button (three-button mouse)
            /// </summary>
            VK_MBUTTON = 0x04,

            /// <summary>
            /// X1 mouse button
            /// </summary>
            VK_XBUTTON1 = 0x05,

            /// <summary>
            /// X2 mouse button
            /// </summary>
            VK_XBUTTON2 = 0x06,

            /// <summary>
            /// Undefined
            /// </summary>
            VK_0x07 = 0x07,

            /// <summary>
            /// BACKSPACE key
            /// </summary>
            VK_BACK = 0x08,

            /// <summary>
            /// TAB key
            /// </summary>
            VK_TAB = 0x09,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0x0A = 0x0A,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0x0B = 0x0B,

            /// <summary>
            /// CLEAR key
            /// </summary>
            VK_CLEAR = 0x0C,

            /// <summary>
            /// ENTER key
            /// </summary>
            VK_RETURN = 0x0D,

            /// <summary>
            /// Undefined
            /// </summary>
            VK_0x0E = 0x0E,

            /// <summary>
            /// Undefined
            /// </summary>
            VK_0x0F = 0x0F,

            /// <summary>
            /// SHIFT key
            /// </summary>
            VK_SHIFT = 0x10,

            /// <summary>
            /// CTRL key
            /// </summary>
            VK_CONTROL = 0x11,

            /// <summary>
            /// ALT key
            /// </summary>
            VK_MENU = 0x12,

            /// <summary>
            /// PAUSE key
            /// </summary>
            VK_PAUSE = 0x13,

            /// <summary>
            /// CAPS LOCK key
            /// </summary>
            VK_CAPITAL = 0x14,

            /// <summary>
            /// IME Kana mode
            /// </summary>
            VK_KANA = 0x15,

            /// <summary>
            /// IME Hanguel mode (maintained for compatibility; use VK_HANGUL)
            /// </summary>
            VK_HANGUEL = 0x15,

            /// <summary>
            /// IME Hangul mode
            /// </summary>
            VK_HANGUL = 0x15,

            /// <summary>
            /// Undefined
            /// </summary>
            VK_0x16 = 0x16,

            /// <summary>
            /// IME Junja mode
            /// </summary>
            VK_JUNJA = 0x17,

            /// <summary>
            /// IME final mode
            /// </summary>
            VK_FINAL = 0x18,

            /// <summary>
            /// IME Hanja mode
            /// </summary>
            VK_HANJA = 0x19,

            /// <summary>
            /// IME Kanji mode
            /// </summary>
            VK_KANJI = 0x19,

            /// <summary>
            /// Undefined
            /// </summary>
            VK_0x1A = 0x1A,

            /// <summary>
            /// ESC key
            /// </summary>
            VK_ESCAPE = 0x1B,

            /// <summary>
            /// IME convert
            /// </summary>
            VK_CONVERT = 0x1C,

            /// <summary>
            /// IME nonconvert
            /// </summary>
            VK_NONCONVERT = 0x1D,

            /// <summary>
            /// IME accept
            /// </summary>
            VK_ACCEPT = 0x1E,

            /// <summary>
            /// IME mode change request
            /// </summary>
            VK_MODECHANGE = 0x1F,

            /// <summary>
            /// SPACEBAR
            /// </summary>
            VK_SPACE = 0x20,

            /// <summary>
            /// PAGE UP key
            /// </summary>
            VK_PRIOR = 0x21,

            /// <summary>
            /// PAGE DOWN key
            /// </summary>
            VK_NEXT = 0x22,

            /// <summary>
            /// END key
            /// </summary>
            VK_END = 0x23,

            /// <summary>
            /// HOME key
            /// </summary>
            VK_HOME = 0x24,

            /// <summary>
            /// LEFT ARROW key
            /// </summary>
            VK_LEFT = 0x25,

            /// <summary>
            /// UP ARROW key
            /// </summary>
            VK_UP = 0x26,

            /// <summary>
            /// RIGHT ARROW key
            /// </summary>
            VK_RIGHT = 0x27,

            /// <summary>
            /// DOWN ARROW key
            /// </summary>
            VK_DOWN = 0x28,

            /// <summary>
            /// SELECT key
            /// </summary>
            VK_SELECT = 0x29,

            /// <summary>
            /// PRINT key
            /// </summary>
            VK_PRINT = 0x2A,

            /// <summary>
            /// EXECUTE key
            /// </summary>
            VK_EXECUTE = 0x2B,

            /// <summary>
            /// PRINT SCREEN key
            /// </summary>
            VK_SNAPSHOT = 0x2C,

            /// <summary>
            /// INS key
            /// </summary>
            VK_INSERT = 0x2D,

            /// <summary>
            /// DEL key
            /// </summary>
            VK_DELETE = 0x2E,

            /// <summary>
            /// HELP key
            /// </summary>
            VK_HELP = 0x2F,

            /// <summary>
            /// 0 key
            /// </summary>
            K_0 = 0x30,

            /// <summary>
            /// 1 key
            /// </summary>
            K_1 = 0x31,

            /// <summary>
            /// 2 key
            /// </summary>
            K_2 = 0x32,

            /// <summary>
            /// 3 key
            /// </summary>
            K_3 = 0x33,

            /// <summary>
            /// 4 key
            /// </summary>
            K_4 = 0x34,

            /// <summary>
            /// 5 key
            /// </summary>
            K_5 = 0x35,

            /// <summary>
            /// 6 key
            /// </summary>
            K_6 = 0x36,

            /// <summary>
            /// 7 key
            /// </summary>
            K_7 = 0x37,

            /// <summary>
            /// 8 key
            /// </summary>
            K_8 = 0x38,

            /// <summary>
            /// 9 key
            /// </summary>
            K_9 = 0x39,

            /// <summary>
            /// Undefined
            /// </summary>
            VK_0x3A = 0x3A,

            /// <summary>
            /// Undefined
            /// </summary>
            VK_0x3B = 0x3B,

            /// <summary>
            /// Undefined
            /// </summary>
            VK_0x3C = 0x3C,

            /// <summary>
            /// Undefined
            /// </summary>
            VK_0x3D = 0x3D,

            /// <summary>
            /// Undefined
            /// </summary>
            VK_0x3E = 0x3E,

            /// <summary>
            /// Undefined
            /// </summary>
            VK_0x3F = 0x3F,

            /// <summary>
            /// Undefined
            /// </summary>
            VK_0x40 = 0x40,

            /// <summary>
            /// A key
            /// </summary>
            K_A = 0x41,

            /// <summary>
            /// B key
            /// </summary>
            K_B = 0x42,

            /// <summary>
            /// C key
            /// </summary>
            K_C = 0x43,

            /// <summary>
            /// D key
            /// </summary>
            K_D = 0x44,

            /// <summary>
            /// E key
            /// </summary>
            K_E = 0x45,

            /// <summary>
            /// F key
            /// </summary>
            K_F = 0x46,

            /// <summary>
            /// G key
            /// </summary>
            K_G = 0x47,

            /// <summary>
            /// H key
            /// </summary>
            K_H = 0x48,

            /// <summary>
            /// I key
            /// </summary>
            K_I = 0x49,

            /// <summary>
            /// J key
            /// </summary>
            K_J = 0x4A,

            /// <summary>
            /// K key
            /// </summary>
            K_K = 0x4B,

            /// <summary>
            /// L key
            /// </summary>
            K_L = 0x4C,

            /// <summary>
            /// M key
            /// </summary>
            K_M = 0x4D,

            /// <summary>
            /// N key
            /// </summary>
            K_N = 0x4E,

            /// <summary>
            /// O key
            /// </summary>
            K_O = 0x4F,

            /// <summary>
            /// P key
            /// </summary>
            K_P = 0x50,

            /// <summary>
            /// Q key
            /// </summary>
            K_Q = 0x51,

            /// <summary>
            /// R key
            /// </summary>
            K_R = 0x52,

            /// <summary>
            /// S key
            /// </summary>
            K_S = 0x53,

            /// <summary>
            /// T key
            /// </summary>
            K_T = 0x54,

            /// <summary>
            /// U key
            /// </summary>
            K_U = 0x55,

            /// <summary>
            /// V key
            /// </summary>
            K_V = 0x56,

            /// <summary>
            /// W key
            /// </summary>
            K_W = 0x57,

            /// <summary>
            /// X key
            /// </summary>
            K_X = 0x58,

            /// <summary>
            /// Y key
            /// </summary>
            K_Y = 0x59,

            /// <summary>
            /// Z key
            /// </summary>
            K_Z = 0x5A,

            /// <summary>
            /// Left Windows key (Natural keyboard)
            /// </summary>
            VK_LWIN = 0x5B,

            /// <summary>
            /// Right Windows key (Natural keyboard)
            /// </summary>
            VK_RWIN = 0x5C,

            /// <summary>
            /// Applications key (Natural keyboard)
            /// </summary>
            VK_APPS = 0x5D,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0x5E = 0x5E,

            /// <summary>
            /// Computer Sleep key
            /// </summary>
            VK_SLEEP = 0x5F,

            /// <summary>
            /// Numeric keypad 0 key
            /// </summary>
            VK_NUMPAD0 = 0x60,

            /// <summary>
            /// Numeric keypad 1 key
            /// </summary>
            VK_NUMPAD1 = 0x61,

            /// <summary>
            /// Numeric keypad 2 key
            /// </summary>
            VK_NUMPAD2 = 0x62,

            /// <summary>
            /// Numeric keypad 3 key
            /// </summary>
            VK_NUMPAD3 = 0x63,

            /// <summary>
            /// Numeric keypad 4 key
            /// </summary>
            VK_NUMPAD4 = 0x64,

            /// <summary>
            /// Numeric keypad 5 key
            /// </summary>
            VK_NUMPAD5 = 0x65,

            /// <summary>
            /// Numeric keypad 6 key
            /// </summary>
            VK_NUMPAD6 = 0x66,

            /// <summary>
            /// Numeric keypad 7 key
            /// </summary>
            VK_NUMPAD7 = 0x67,

            /// <summary>
            /// Numeric keypad 8 key
            /// </summary>
            VK_NUMPAD8 = 0x68,

            /// <summary>
            /// Numeric keypad 9 key
            /// </summary>
            VK_NUMPAD9 = 0x69,

            /// <summary>
            /// Multiply key
            /// </summary>
            VK_MULTIPLY = 0x6A,

            /// <summary>
            /// Add key
            /// </summary>
            VK_ADD = 0x6B,

            /// <summary>
            /// Separator key
            /// </summary>
            VK_SEPARATOR = 0x6C,

            /// <summary>
            /// Subtract key
            /// </summary>
            VK_SUBTRACT = 0x6D,

            /// <summary>
            /// Decimal key
            /// </summary>
            VK_DECIMAL = 0x6E,

            /// <summary>
            /// Divide key
            /// </summary>
            VK_DIVIDE = 0x6F,

            /// <summary>
            /// F1 key
            /// </summary>
            VK_F1 = 0x70,

            /// <summary>
            /// F2 key
            /// </summary>
            VK_F2 = 0x71,

            /// <summary>
            /// F3 key
            /// </summary>
            VK_F3 = 0x72,

            /// <summary>
            /// F4 key
            /// </summary>
            VK_F4 = 0x73,

            /// <summary>
            /// F5 key
            /// </summary>
            VK_F5 = 0x74,

            /// <summary>
            /// F6 key
            /// </summary>
            VK_F6 = 0x75,

            /// <summary>
            /// F7 key
            /// </summary>
            VK_F7 = 0x76,

            /// <summary>
            /// F8 key
            /// </summary>
            VK_F8 = 0x77,

            /// <summary>
            /// F9 key
            /// </summary>
            VK_F9 = 0x78,

            /// <summary>
            /// F10 key
            /// </summary>
            VK_F10 = 0x79,

            /// <summary>
            /// F11 key
            /// </summary>
            VK_F11 = 0x7A,

            /// <summary>
            /// F12 key
            /// </summary>
            VK_F12 = 0x7B,

            /// <summary>
            /// F13 key
            /// </summary>
            VK_F13 = 0x7C,

            /// <summary>
            /// F14 key
            /// </summary>
            VK_F14 = 0x7D,

            /// <summary>
            /// F15 key
            /// </summary>
            VK_F15 = 0x7E,

            /// <summary>
            /// F16 key
            /// </summary>
            VK_F16 = 0x7F,

            /// <summary>
            /// F17 key
            /// </summary>
            VK_F17 = 0x80,

            /// <summary>
            /// F18 key
            /// </summary>
            VK_F18 = 0x81,

            /// <summary>
            /// F19 key
            /// </summary>
            VK_F19 = 0x82,

            /// <summary>
            /// F20 key
            /// </summary>
            VK_F20 = 0x83,

            /// <summary>
            /// F21 key
            /// </summary>
            VK_F21 = 0x84,

            /// <summary>
            /// F22 key
            /// </summary>
            VK_F22 = 0x85,

            /// <summary>
            /// F23 key
            /// </summary>
            VK_F23 = 0x86,

            /// <summary>
            /// F24 key
            /// </summary>
            VK_F24 = 0x87,

            /// <summary>
            /// Unassigned
            /// </summary>
            VK_0x88 = 0x88,

            /// <summary>
            /// Unassigned
            /// </summary>
            VK_0x89 = 0x89,

            /// <summary>
            /// Unassigned
            /// </summary>
            VK_0x8A = 0x8A,

            /// <summary>
            /// Unassigned
            /// </summary>
            VK_0x8B = 0x8B,

            /// <summary>
            /// Unassigned
            /// </summary>
            VK_0x8C = 0x8C,

            /// <summary>
            /// Unassigned
            /// </summary>
            VK_0x8D = 0x8D,

            /// <summary>
            /// Unassigned
            /// </summary>
            VK_0x8E = 0x8E,

            /// <summary>
            /// Unassigned
            /// </summary>
            VK_0x8F = 0x8F,

            /// <summary>
            /// NUM LOCK key
            /// </summary>
            VK_NUMLOCK = 0x90,

            /// <summary>
            /// SCROLL LOCK key
            /// </summary>
            VK_SCROLL = 0x91,

            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0x92 = 0x92,

            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0x93 = 0x93,

            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0x94 = 0x94,

            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0x95 = 0x95,

            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0x96 = 0x96,

            /// <summary>
            /// Unassigned
            /// </summary>
            VK_0x97 = 0x97,

            /// <summary>
            /// Unassigned
            /// </summary>
            VK_0x98 = 0x98,

            /// <summary>
            /// Unassigned
            /// </summary>
            VK_0x99 = 0x99,

            /// <summary>
            /// Unassigned
            /// </summary>
            VK_0x9A = 0x9A,

            /// <summary>
            /// Unassigned
            /// </summary>
            VK_0x9B = 0x9B,

            /// <summary>
            /// Unassigned
            /// </summary>
            VK_0x9C = 0x9C,

            /// <summary>
            /// Unassigned
            /// </summary>
            VK_0x9D = 0x9D,

            /// <summary>
            /// Unassigned
            /// </summary>
            VK_0x9E = 0x9E,

            /// <summary>
            /// Unassigned
            /// </summary>
            VK_0x9F = 0x9F,

            /// <summary>
            /// Left SHIFT key
            /// </summary>
            VK_LSHIFT = 0xA0,

            /// <summary>
            /// Right SHIFT key
            /// </summary>
            VK_RSHIFT = 0xA1,

            /// <summary>
            /// Left CONTROL key
            /// </summary>
            VK_LCONTROL = 0xA2,

            /// <summary>
            /// Right CONTROL key
            /// </summary>
            VK_RCONTROL = 0xA3,

            /// <summary>
            /// Left MENU key
            /// </summary>
            VK_LMENU = 0xA4,

            /// <summary>
            /// Right MENU key
            /// </summary>
            VK_RMENU = 0xA5,

            /// <summary>
            /// Browser Back key
            /// </summary>
            VK_BROWSER_BACK = 0xA6,

            /// <summary>
            /// Browser Forward key
            /// </summary>
            VK_BROWSER_FORWARD = 0xA7,

            /// <summary>
            /// Browser Refresh key
            /// </summary>
            VK_BROWSER_REFRESH = 0xA8,

            /// <summary>
            /// Browser Stop key
            /// </summary>
            VK_BROWSER_STOP = 0xA9,

            /// <summary>
            /// Browser Search key
            /// </summary>
            VK_BROWSER_SEARCH = 0xAA,

            /// <summary>
            /// Browser Favorites key
            /// </summary>
            VK_BROWSER_FAVORITES = 0xAB,

            /// <summary>
            /// Browser Start and Home key
            /// </summary>
            VK_BROWSER_HOME = 0xAC,

            /// <summary>
            /// Volume Mute key
            /// </summary>
            VK_VOLUME_MUTE = 0xAD,

            /// <summary>
            /// Volume Down key
            /// </summary>
            VK_VOLUME_DOWN = 0xAE,

            /// <summary>
            /// Volume Up key
            /// </summary>
            VK_VOLUME_UP = 0xAF,

            /// <summary>
            /// Next Track key
            /// </summary>
            VK_MEDIA_NEXT_TRACK = 0xB0,

            /// <summary>
            /// Previous Track key
            /// </summary>
            VK_MEDIA_PREV_TRACK = 0xB1,

            /// <summary>
            /// Stop Media key
            /// </summary>
            VK_MEDIA_STOP = 0xB2,

            /// <summary>
            /// Play/Pause Media key
            /// </summary>
            VK_MEDIA_PLAY_PAUSE = 0xB3,

            /// <summary>
            /// Start Mail key
            /// </summary>
            VK_LAUNCH_MAIL = 0xB4,

            /// <summary>
            /// Select Media key
            /// </summary>
            VK_LAUNCH_MEDIA_SELECT = 0xB5,

            /// <summary>
            /// Start Application 1 key
            /// </summary>
            VK_LAUNCH_APP1 = 0xB6,

            /// <summary>
            /// Start Application 2 key
            /// </summary>
            VK_LAUNCH_APP2 = 0xB7,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xB8 = 0xB8,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xB9 = 0xB9,

            /// <summary>
            /// Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the ';:' key
            /// </summary>
            VK_OEM_1 = 0xBA,

            /// <summary>
            /// For any country/region, the '+' key
            /// </summary>
            VK_OEM_PLUS = 0xBB,

            /// <summary>
            /// For any country/region, the ',' key
            /// </summary>
            VK_OEM_COMMA = 0xBC,

            /// <summary>
            /// For any country/region, the '-' key
            /// </summary>
            VK_OEM_MINUS = 0xBD,

            /// <summary>
            /// For any country/region, the '.' key
            /// </summary>
            VK_OEM_PERIOD = 0xBE,

            /// <summary>
            /// Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '/?' key
            /// </summary>
            VK_OEM_2 = 0xBF,

            /// <summary>
            /// Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '`~' key
            /// </summary>
            VK_OEM_3 = 0xC0,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xC1 = 0xC1,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xC2 = 0xC2,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xC3 = 0xC3,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xC4 = 0xC4,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xC5 = 0xC5,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xC6 = 0xC6,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xC7 = 0xC7,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xC8 = 0xC8,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xC9 = 0xC9,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xCA = 0xCA,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xCB = 0xCB,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xCC = 0xCC,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xCD = 0xCD,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xCE = 0xCE,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xCF = 0xCF,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xD0 = 0xD0,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xD1 = 0xD1,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xD2 = 0xD2,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xD3 = 0xD3,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xD4 = 0xD4,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xD5 = 0xD5,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xD6 = 0xD6,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xD7 = 0xD7,

            /// <summary>
            /// Unassigned
            /// </summary>
            VK_0xD8 = 0xD8,

            /// <summary>
            /// Unassigned
            /// </summary>
            VK_0xD9 = 0xD9,

            /// <summary>
            /// Unassigned
            /// </summary>
            VK_0xDA = 0xDA,

            /// <summary>
            /// Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '[{' key
            /// </summary>
            VK_OEM_4 = 0xDB,

            /// <summary>
            /// Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '\\|' key
            /// </summary>
            VK_OEM_5 = 0xDC,

            /// <summary>
            /// Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the ']}' key
            /// </summary>
            VK_OEM_6 = 0xDD,

            /// <summary>
            /// Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the 'single-quote/double-quote' key
            /// </summary>
            VK_OEM_7 = 0xDE,

            /// <summary>
            /// Used for miscellaneous characters; it can vary by keyboard.
            /// </summary>
            VK_OEM_8 = 0xDF,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_0xE0 = 0xE0,

            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0xE1 = 0xE1,

            /// <summary>
            /// Either the angle bracket key or the backslash key on the RT 102-key keyboard
            /// </summary>
            VK_OEM_102 = 0xE2,


            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0xE3 = 0xE3,

            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0xE4 = 0xE4,

            /// <summary>
            /// IME PROCESS key
            /// </summary>
            VK_PROCESSKEY = 0xE5,

            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0xE6 = 0xE6,

            /// <summary>
            /// Used to pass Unicode characters as if they were keystrokes.
            /// The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods.
            /// For more information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN, and WM_KEYUP
            /// </summary>
            VK_PACKET = 0xE7,

            /// <summary>
            /// Unassigned
            /// </summary>
            VK_0xE8 = 0xE8,

            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0xE9 = 0xE9,

            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0xEA = 0xEA,

            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0xEB = 0xEB,

            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0xEC = 0xEC,

            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0xED = 0xED,

            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0xEE = 0xEE,

            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0xEF = 0xEF,

            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0xF0 = 0xF0,

            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0xF1 = 0xF1,

            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0xF2 = 0xF2,

            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0xF3 = 0xF3,

            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0xF4 = 0xF4,

            /// <summary>
            /// OEM specific
            /// </summary>
            VK_0xF5 = 0xF5,

            /// <summary>
            /// Attn key
            /// </summary>
            VK_ATTN = 0xF6,

            /// <summary>
            /// CrSel key
            /// </summary>
            VK_CRSEL = 0xF7,

            /// <summary>
            /// ExSel key
            /// </summary>
            VK_EXSEL = 0xF8,

            /// <summary>
            /// Erase EOF key
            /// </summary>
            VK_EREOF = 0xF9,

            /// <summary>
            /// Play key
            /// </summary>
            VK_PLAY = 0xFA,

            /// <summary>
            /// Zoom key
            /// </summary>
            VK_ZOOM = 0xFB,

            /// <summary>
            /// Reserved
            /// </summary>
            VK_NONAME = 0xFC,

            /// <summary>
            /// PA1 key
            /// </summary>
            VK_PA1 = 0xFD,

            /// <summary>
            /// Clear key
            /// </summary>
            VK_OEM_CLEAR = 0xFE,

            /// <summary>
            /// Undefined
            /// </summary>
            VK_0xFF = 0xFF,
        }
        #endregion VirtualKeyboard

        #region HotKey 登録用 ID
        /// <summary>
        /// HotKey登録時に指定する ID を表します。
        /// 0x0000～0xbfff で指定してください。
        /// 0xc000～0xffff は DLL 用に予約済みのため使用できません。
        /// </summary>
        public enum HOTKEYs : int
        {
            /// <summary>
            /// HotKey 登録用 ID 1
            /// </summary>
            HOTKEY_ID01 = 0x0001,

            /// <summary>
            /// HotKey 登録用 ID 2
            /// </summary>
            HOTKEY_ID02 = 0x0002,

            /// <summary>
            /// HotKey 登録用 ID 3
            /// </summary>
            HOTKEY_ID03 = 0x0003,

            /// <summary>
            /// HotKey 登録用 ID 4
            /// </summary>
            HOTKEY_ID04 = 0x0004,

            /// <summary>
            /// HotKey 登録用 ID 5
            /// </summary>
            HOTKEY_ID05 = 0x0005,

            /// <summary>
            /// HotKey 登録用 ID 6
            /// </summary>
            HOTKEY_ID06 = 0x0006,

            /// <summary>
            /// HotKey 登録用 ID 7
            /// </summary>
            HOTKEY_ID07 = 0x0007,

            /// <summary>
            /// HotKey 登録用 ID 8
            /// </summary>
            HOTKEY_ID08 = 0x0008,

            /// <summary>
            /// HotKey 登録用 ID 9
            /// </summary>
            HOTKEY_ID09 = 0x0009,

            /// <summary>
            /// HotKey 登録用 ID 10
            /// </summary>
            HOTKEY_ID10 = 0x000A,

            /// <summary>
            /// HotKey 登録用 ID 11
            /// </summary>
            HOTKEY_ID11 = 0x000B,

            /// <summary>
            /// HotKey 登録用 ID 12
            /// </summary>
            HOTKEY_ID12 = 0x000C,

            /// <summary>
            /// HotKey 登録用 ID 13
            /// </summary>
            HOTKEY_ID13 = 0x000D,

            /// <summary>
            /// HotKey 登録用 ID 14
            /// </summary>
            HOTKEY_ID14 = 0x000E,

            /// <summary>
            /// HotKey 登録用 ID 15
            /// </summary>
            HOTKEY_ID15 = 0x000F,

            /// <summary>
            /// HotKey 登録用 ID 16
            /// </summary>
            HOTKEY_ID16 = 0x0010,
        }
        #endregion HotKey 登録用 ID
    }
}
