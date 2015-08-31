﻿namespace YKToolkit.Controls
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    public static class User32
    {
        /// <summary>
        /// GetWindowLong 関数の導入
        /// </summary>
        /// <param name="hwnd">ウィンドウハンドル</param>
        /// <param name="index">インデックス</param>
        /// <returns>戻り値</returns>
        [DllImport("user32.dll")]
        public extern static int GetWindowLong(IntPtr hwnd, int index);

        /// <summary>
        /// SetWindowLong 関数の導入
        /// </summary>
        /// <param name="hwnd">ウィンドウハンドル</param>
        /// <param name="index">インデックス</param>
        /// <param name="value">設定値</param>
        /// <returns>戻り値</returns>
        [DllImport("user32.dll")]
        public extern static int SetWindowLong(IntPtr hwnd, int index, int value);

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
            /// WindowMessage - WM_SYSKEYDOWN
            /// </summary>
            WM_SYSKEYDOWN = 0x0104,

            /// <summary>
            /// WindowMessage - WM_SYSCOMMAND
            /// </summary>
            WM_SYSCOMMAND = 0x0112,

            /// <summary>
            /// WindowMessage - WM_HOTKEY
            /// </summary>
            WM_HOTKEY = 0x0312,
        }
        #endregion WindowMessage

        #region SystemCommand
        /// <summary>
        /// システムコマンドを表します。
        /// </summary>
        public enum SCs : int
        {
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
            /// BACKSPACE key
            /// </summary>
            VK_BACK = 0x08,

            /// <summary>
            /// TAB key
            /// </summary>
            VK_TAB = 0x09,

            /// <summary>
            /// CLEAR key
            /// </summary>
            VK_CLEAR = 0x0C,

            /// <summary>
            /// ENTER key
            /// </summary>
            VK_RETURN = 0x0D,

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
            /// NUM LOCK key
            /// </summary>
            VK_NUMLOCK = 0x90,

            /// <summary>
            /// SCROLL LOCK key
            /// </summary>
            VK_SCROLL = 0x91,

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
            /// Either the angle bracket key or the backslash key on the RT 102-key keyboard
            /// </summary>
            VK_OEM_102 = 0xE2,

            /// <summary>
            /// IME PROCESS key
            /// </summary>
            VK_PROCESSKEY = 0xE5,

            /// <summary>
            /// Used to pass Unicode characters as if they were keystrokes.
            /// The VK_PACKET key is the low word of a 32-bit Virtual Key value used for non-keyboard input methods.
            /// For more information, see Remark in KEYBDINPUT, SendInput, WM_KEYDOWN, and WM_KEYUP
            /// </summary>
            VK_PACKET = 0xE7,

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
            /// PA1 key
            /// </summary>
            VK_PA1 = 0xFD,

            /// <summary>
            /// Clear key
            /// </summary>
            VK_OEM_CLEAR = 0xFE,
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