namespace YKToolkit.Controls
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// shell32.dll を使用するための変数またはメソッドを提供します。
    /// </summary>
    public static class Shell32
    {
        /// <summary>
        /// SHGetPathFromIDList 関数の定義
        /// </summary>
        /// <param name="pidl">The address of an item identifier list that specifies a file or directory location relative to the root of the namespace (the desktop).</param>
        /// <param name="Path">The address of a buffer to receive the file system path. This buffer must be at least MAX_PATH characters in size.</param>
        /// <returns></returns>
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int SHGetPathFromIDList(IntPtr pidl, StringBuilder Path);

        /// <summary>
        /// SHChangeNotifyRegister 関数の定義
        /// </summary>
        /// <param name="hWnd">A handle to the window that receives the change or notification messages.</param>
        /// <param name="fSources">One or more of the following values that indicate the type of events for which to receive notifications. </param>
        /// <param name="fEvents">Change notification events for which to receive notification. See the SHCNE flags listed in SHChangeNotify for possible values.</param>
        /// <param name="wMsg">Message to be posted to the window procedure.</param>
        /// <param name="cEntries">Number of entries in the pshcne array.</param>
        /// <param name="pFsne">Array of SHChangeNotifyEntry structures that contain the notifications. This array should always be set to one when calling SHChangeNotifyRegister or SHChangeNotifyDeregister will not work properly.</param>
        /// <returns></returns>
        [DllImport("shell32.dll", EntryPoint = "#2", CharSet = CharSet.Auto)]
        public static extern uint SHChangeNotifyRegister(IntPtr hWnd, int fSources, int fEvents, uint wMsg, int cEntries, ref SHChangeNotifyEntry pFsne);

        /// <summary>
        /// 定数を表すための静的クラス
        /// </summary>
        public static class Constant
        {
            #region SHCNF_***
            /// <summary>
            /// SHChangeNotifyRegister - SHCNF_IDLIST
            /// </summary>
            public const int SHCNF_IDLIST = 0x0000;

            /// <summary>
            /// SHChangeNotifyRegister - SHCNF_PATHA
            /// </summary>
            public const int SHCNF_PATHA = 0x0001;

            /// <summary>
            /// SHChangeNotifyRegister - SHCNF_PRINTERA
            /// </summary>
            public const int SHCNF_PRINTERA = 0x0002;

            /// <summary>
            /// SHChangeNotifyRegister - SHCNF_DWORD
            /// </summary>
            public const int SHCNF_DWORD = 0x0003;

            /// <summary>
            /// SHChangeNotifyRegister - SHCNF_PATHW
            /// </summary>
            public const int SHCNF_PATHW = 0x0005;

            /// <summary>
            /// SHChangeNotifyRegister - SHCNF_PRINTERW
            /// </summary>
            public const int SHCNF_PRINTERW = 0x0006;

            /// <summary>
            /// SHChangeNotifyRegister - SHCNF_TYPE
            /// </summary>
            public const int SHCNF_TYPE = 0x00FF;

            /// <summary>
            /// SHChangeNotifyRegister - SHCNF_FLUSH
            /// </summary>
            public const int SHCNF_FLUSH = 0x1000;

            /// <summary>
            /// SHChangeNotifyRegister - SHCNF_FLUSHNOWAIT
            /// </summary>
            public const int SHCNF_FLUSHNOWAIT = 0x2000;
            #endregion SHCNF_***

            #region SHCNE_***
            /// <summary>
            /// SHChangeNotifyRegister - SHCNE_MEDIAINSERTED
            /// </summary>
            public const int SHCNE_MEDIAINSERTED = 0x00000020;

            /// <summary>
            /// SHChangeNotifyRegister - SHCNE_MEDIAREMOVED
            /// </summary>
            public const int SHCNE_MEDIAREMOVED = 0x00000040;
            #endregion SHCNE_***

            #region WM_***
            /// <summary>
            /// WindowMessage - WM_SHNOTIFY
            /// </summary>
            public const int WM_SHNOTIFY = 0x00000401;

            /// <summary>
            /// Notifies an application of a change to the hardware configuration of a device or the computer.
            /// </summary>
            public const int WM_DEVICECHANGE = 0x00000219;
            #endregion WM_***

            #region DBT_***
            /// <summary>
            /// A request to change the current configuration (dock or undock) has been canceled.
            /// </summary>
            public const int DBT_CONFIGCHANGECANCELED = 0x00000019;

            /// <summary>
            /// The current configuration has changed, due to a dock or undock.
            /// </summary>
            public const int DBT_CONFIGCHANGED = 0x00000018;

            /// <summary>
            /// A custom event has occurred.
            /// </summary>
            public const int DBT_CUSTOMEVENT = 0x00008006;

            /// <summary>
            /// A device or piece of media has been inserted and is now available.
            /// </summary>
            public const int DBT_DEVICEARRIVAL = 0x00008000;

            /// <summary>
            /// Permission is requested to remove a device or piece of media. Any application can deny this request and cancel the removal.
            /// </summary>
            public const int DBT_DEVICEQUERYREMOVE = 0x00008001;

            /// <summary>
            /// A request to remove a device or piece of media has been canceled.
            /// </summary>
            public const int DBT_DEVICEQUERYREMOVEFAILED = 0x00008002;

            /// <summary>
            /// A device or piece of media has been removed.
            /// </summary>
            public const int DBT_DEVICEREMOVECOMPLETE = 0x00008004;

            /// <summary>
            /// A device or piece of media is about to be removed. Cannot be denied.
            /// </summary>
            public const int DBT_DEVICEREMOVEPENDING = 0x00008003;

            /// <summary>
            /// A device-specific event has occurred.
            /// </summary>
            public const int DBT_DEVICETYPESPECIFIC = 0x00008005;

            /// <summary>
            /// A device has been added to or removed from the system.
            /// </summary>
            public const int DBT_DEVNODES_CHANGED = 0x00000007;

            /// <summary>
            /// Permission is requested to change the current configuration (dock or undock).
            /// </summary>
            public const int DBT_QUERYCHANGECONFIG = 0x00000017;

            /// <summary>
            /// The meaning of this message is user-defined.
            /// </summary>
            public const int DBT_USERDEFINED = 0x0000FFFF;

            /// <summary>
            /// Logical volume. This structure is a DEV_BROADCAST_VOLUME structure.
            /// </summary>
            public const int DBT_DEVTYP_VOLUME = 0x00000002;

            /// <summary>
            /// File system handle. This structure is a DEV_BROADCAST_HANDLE structure.
            /// </summary>
            public const int DBT_DEVTYP_HANDLE = 0x00000006;
            #endregion DBT_***

            #region DBTF_***
            /// <summary>
            /// Change affects media in drive. If not set, change affects physical device or drive.
            /// </summary>
            public const int DBTF_MEDIA = 0x0001;

            /// <summary>
            /// Indicated logical volume is a network volume.
            /// </summary>
            public const int DBTF_NET = 0x0002;
            #endregion DBTF_***
        }

        /// <summary>
        /// SHChangeNotifyRegister 関数の戻り値としても使用する構造体の定義
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct SHChangeNotifyEntry
        {
            /// <summary>
            /// PIDL for which to receive notifications.
            /// </summary>
            public IntPtr pIdl;

            /// <summary>
            /// A flag indicating whether to post notifications for children of this PIDL. For example, if the PIDL points to a folder, then file notifications would come from the folder's children if this flag was TRUE.
            /// </summary>
            public bool Recursively;
        }

        /// <summary>
        /// Struct for parameters of the WM_DEVICECHANGE message
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DEV_BROADCAST_VOLUME
        {
            /// <summary>
            /// The size of this structure, in bytes. 
            /// </summary>
            public int dbcv_size;

            /// <summary>
            /// The device type, which determines the event-specific information that follows the first three members.
            /// Set to DBT_DEVTYP_VOLUME (2).
            /// </summary>
            public int dbcv_devicetype;

            /// <summary>
            /// do not use.
            /// </summary>
            public int dbcv_reserved;

            /// <summary>
            /// The logical unit mask identifying one or more logical units. Each bit in the mask corresponds to one logical drive. Bit 0 represents drive A, bit 1 represents drive B, and so on.
            /// </summary>
            public int dbcv_unitmask;

            /// <summary>
            /// This parameter can be one of the following values.
            /// DBTF_MEDIA : Change affects media in drive. If not set, change affects physical device or drive.
            /// DBTF_NET : Indicated logical volume is a network volume.
            /// </summary>
            public int dbcv_flags;
        }

        /// <summary>
        /// DEV_BROADCAST_VOLUME 構造体のマスク設定からドライブレターを算出します。
        /// </summary>
        /// <param name="mask">DEV_BROADCAST_VOLUME 構造体のマスク設定を指定します。</param>
        /// <returns>ドライブレターを返します。</returns>
        public static string GetDriveString(int mask)
        {
            var c = 'A';
            if (mask <= 0x2000000)
            {
                while (0 != (mask /= 2))
                {
                    c++;
                }
            }
            else
            {
                c = '?';
            }

            return c.ToString();
        }

        /// <summary>
        /// Win32 における SHFILEINFO 構造体を取得するためのヘルパ
        /// </summary>
        public static class ShellInfo
        {
            #region アイコン取得関連
            [DllImport("shell32.dll")]
            public static extern int SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, int cbSizeFileInfo, uint uFlags);

            [DllImport("shell32.dll")]
            public static extern IntPtr SHGetFileInfo(IntPtr pIDL, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

            [DllImport("shell32.dll")]
            public static extern int SHGetSpecialFolderLocation(IntPtr hwndOwner, int nFolder, out IntPtr ppidl);

            // pidlのfree用   
            [InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("00000002-0000-0000-C000-000000000046")]
            public interface IMalloc
            {
                [PreserveSig]
                IntPtr Alloc([In] int cb);
                [PreserveSig]
                IntPtr Realloc([In] IntPtr pv, [In] int cb);
                [PreserveSig]
                void Free([In] IntPtr pv);
                [PreserveSig]
                int GetSize([In] IntPtr pv);
                [PreserveSig]
                int DidAlloc(IntPtr pv);
                [PreserveSig]
                void HeapMinimize();
            }

            [DllImport("Shell32.DLL")]
            public static extern int SHGetMalloc(out IMalloc ppMalloc);

            //===================  
            // SHGetSpecialFolderLocationに使用するFolderのIDの定義。  
            // ここをかえるといろいろな特殊フォルダpidlが取得できる。  
            //===================
            /// <summary>
            /// SHGetSpecialFolderLocation に使用する Folder ID の定義。
            /// </summary>
            public enum FolderID
            {
                Desktop = 0x0000,
                Printers = 0x0004,
                MyDocuments = 0x0005,
                Favorites = 0x0006,
                Recent = 0x0008,
                SendTo = 0x0009,
                StartMenu = 0x000b,
                MyComputer = 0x0011,
                NetworkNeighborhood = 0x0012,
                Templates = 0x0015,
                MyPictures = 0x0027,
                NetAndDialUpConnections = 0x0031,
            }

            private const uint SHGFI_ICON = 0x100;
            private const uint SHGFI_LARGEICON = 0x0;
            private const uint SHGFI_SMALLICON = 0x1;
            private const uint SHGFI_PIDL = 0x8;

            [StructLayout(LayoutKind.Sequential)]
            public struct SHFILEINFO
            {
                public IntPtr hIcon;
                public IntPtr iIcon;
                public uint dwAttributes;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
                public string szDisplayName;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
                public string szTypeName;
            }

            /// <summary>
            /// 特殊フォルダのアイコン画像を取得します。
            /// </summary>
            /// <param name="handle">ウィンドウハンドルを指定します。</param>
            /// <param name="id">フォルダ ID を指定します。</param>
            /// <param name="isLarge">大きいアイコンを取得したい場合に true を指定します。</param>
            /// <returns>取得したアイコン画像を返します。</returns>
            public static BitmapSource GetSpecialIcon(IntPtr handle, FolderID id, bool isLarge = false)
            {
                BitmapSource icon = null;

                IntPtr hImg; //the handle to the system image list 
                SHFILEINFO psf = new SHFILEINFO();

                var startLocation = id;
                IntPtr pidlRoot = IntPtr.Zero;

                SHGetSpecialFolderLocation(handle, (int)startLocation, out pidlRoot);

                var uFlags = isLarge ? SHGFI_LARGEICON : SHGFI_SMALLICON;
                uFlags |= SHGFI_ICON | SHGFI_PIDL;
                hImg = SHGetFileInfo(pidlRoot, 0, ref psf, (uint)Marshal.SizeOf(psf), uFlags);

                //The icon is returned in the hIcon member of the shinfo struct 
                //System.Drawing.Icon myIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon);
                if (psf.hIcon != IntPtr.Zero)
                    icon = Imaging.CreateBitmapSourceFromHIcon(psf.hIcon, Int32Rect.Empty, null);

                // pidlを解放する。
                if (pidlRoot != IntPtr.Zero)
                {
                    IMalloc malloc;
                    int nret = SHGetMalloc(out malloc);
                    malloc.Free(pidlRoot);
                }

                return icon;
            }

            /// <summary>
            /// 特殊フォルダのアイコン画像をバイト配列として取得します。
            /// </summary>
            /// <param name="handle">ウィンドウハンドルを指定します。</param>
            /// <param name="id">フォルダ ID を指定します。</param>
            /// <param name="isLarge">大きいアイコンを取得したい場合に true を指定します。</param>
            /// <returns>取得したアイコン画像を返します。</returns>
            public static byte[] GetSpecialIconByByteArray(IntPtr handle, FolderID id, bool isLarge = false)
            {
                var icon = GetSpecialIcon(handle, id, isLarge);
                return BitmapSourceToByteArray(icon);
            }

            /// <summary>
            /// アイコン画像を取得します。
            /// </summary>
            /// <param name="path">ファイルのフルパスを指定します。</param>
            /// <returns>取得結果</returns>
            public static BitmapSource GetSystemIcon(string path)
            {
                BitmapSource icon = null;

                SHFILEINFO psf = new SHFILEINFO();
                SHGetFileInfo(path, 0, ref psf, Marshal.SizeOf(psf), SHGFI_ICON | SHGFI_SMALLICON);

                if (psf.hIcon != IntPtr.Zero)
                    icon = Imaging.CreateBitmapSourceFromHIcon(psf.hIcon, Int32Rect.Empty, null);

                return icon;
            }

            /// <summary>
            /// アイコン画像をバイト配列として取得します。
            /// </summary>
            /// <param name="path">ファイルのフルパスを指定します。</param>
            /// <returns>取得結果</returns>
            public static byte[] GetSystemIconByByteArray(string path)
            {
                var icon = GetSystemIcon(path);
                return BitmapSourceToByteArray(icon);
            }
            #endregion  // アイコン取得関連
        }

        /// <summary>
        /// BitmapSource をバイト配列に変換します。
        /// </summary>
        /// <param name="bitmap">変換する BitmapSource を指定します。</param>
        /// <returns>変換したバイト配列を返します。</returns>
        private static byte[] BitmapSourceToByteArray(BitmapSource bitmap)
        {
            byte[] bytes = null;
            using (var stream = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.Save(stream);
                bytes = stream.GetBuffer();
            }
            return bytes;
        }
    }
}
