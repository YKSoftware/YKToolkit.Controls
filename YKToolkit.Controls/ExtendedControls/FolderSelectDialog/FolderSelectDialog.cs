namespace YKToolkit.Controls
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows.Interop;

    /// <summary>
    /// フォルダ選択ダイアログを表します。
    /// </summary>
    public class FolderSelectDialog
    {
        /// <summary>
        /// 選択されたフォルダのフルパスを取得または設定します。
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// ダイアログのキャプションを取得または設定します。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// フォルダ選択ダイアログを表示します。
        /// </summary>
        /// <returns>フォルダが選択された場合に true を返します。</returns>
        public bool? ShowDialog()
        {
            return ShowDialog(IntPtr.Zero);
        }

        /// <summary>
        /// フォルダ選択ダイアログを表示します。
        /// </summary>
        /// <param name="owner">オーナーウィンドウを指定します。</param>
        /// <returns>フォルダが選択された場合に true を返します。</returns>
        public bool? ShowDialog(System.Windows.Window owner)
        {
            var helper = new WindowInteropHelper(owner);
            return ShowDialog(helper.Handle);
        }

        /// <summary>
        /// フォルダ選択ダイアログを表示します。
        /// </summary>
        /// <param name="owner">オーナーウィンドウのハンドルを指定します。</param>
        /// <returns>フォルダが選択された場合に true を返します。</returns>
        public bool? ShowDialog(IntPtr owner)
        {
            var dlg = new FileOpenDialogInternal() as IFileOpenDialog;
            try
            {
                dlg.SetOptions(FOS.FOS_PICKFOLDERS | FOS.FOS_FORCEFILESYSTEM);

                IShellItem item;
                if (!string.IsNullOrEmpty(this.Path))
                {
                    IntPtr idl;
                    uint atts = 0;
                    if (NativeMethods.SHILCreateFromPath(this.Path, out idl, ref atts) == 0)
                    {
                        if (NativeMethods.SHCreateShellItem(IntPtr.Zero, IntPtr.Zero, idl, out item) == 0)
                        {
                            dlg.SetFolder(item);
                        }
                    }
                }

                if (!string.IsNullOrEmpty(this.Title))
                    dlg.SetTitle(this.Title);

                var hr = dlg.Show(owner);
                if (hr.Equals(NativeMethods.ERROR_CANCELLED))
                    return false;
                if (!hr.Equals(0))
                    return null;

                dlg.GetResult(out item);
                string outputPath;
                item.GetDisplayName(SIGDN.SIGDN_FILESYSPATH, out outputPath);
                this.Path = outputPath;

                return true;
            }
            finally
            {
                Marshal.FinalReleaseComObject(dlg);
            }
        }

        /// <summary>
        /// private なコンストラクタを定義することで外部からの生成を抑止します。
        /// </summary>
        [ComImport]
        [Guid("DC1C5A9C-E88A-4dde-A5A1-60F82A20AEF7")]
        private class FileOpenDialogInternal
        {
        }

        // not fully defined と記載された宣言は、支障ない範囲で端折ってあります。
        [ComImport]
        [Guid("42f85136-db7e-439c-85f1-e4075d135fc8")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IFileOpenDialog
        {
            [PreserveSig]
            UInt32 Show([In] IntPtr hwndParent);
            void SetFileTypes();     // not fully defined
            void SetFileTypeIndex();     // not fully defined
            void GetFileTypeIndex();     // not fully defined
            void Advise(); // not fully defined
            void Unadvise();
            void SetOptions([In] FOS fos);
            void GetOptions(); // not fully defined
            void SetDefaultFolder(); // not fully defined
            void SetFolder(IShellItem psi);
            void GetFolder(); // not fully defined
            void GetCurrentSelection(); // not fully defined
            void SetFileName();  // not fully defined
            void GetFileName();  // not fully defined
            void SetTitle([In, MarshalAs(UnmanagedType.LPWStr)] string pszTitle);
            void SetOkButtonLabel(); // not fully defined
            void SetFileNameLabel(); // not fully defined
            void GetResult(out IShellItem ppsi);
            void AddPlace(); // not fully defined
            void SetDefaultExtension(); // not fully defined
            void Close(); // not fully defined
            void SetClientGuid();  // not fully defined
            void ClearClientData();
            void SetFilter(); // not fully defined
            void GetResults(); // not fully defined
            void GetSelectedItems(); // not fully defined
        }

        [ComImport]
        [Guid("43826D1E-E718-42EE-BC55-A1E261C37BFE")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IShellItem
        {
            void BindToHandler(); // not fully defined
            void GetParent(); // not fully defined
            void GetDisplayName([In] SIGDN sigdnName, [MarshalAs(UnmanagedType.LPWStr)] out string ppszName);
            void GetAttributes();  // not fully defined
            void Compare();  // not fully defined
        }

        private enum SIGDN : uint // not fully defined
        {
            SIGDN_FILESYSPATH = 0x80058000,
        }

        [Flags]
        private enum FOS // not fully defined
        {
            FOS_FORCEFILESYSTEM = 0x40,
            FOS_PICKFOLDERS = 0x20,
        }

        private class NativeMethods
        {
            /// <summary>
            /// SHILCreateFromPath 関数の定義
            /// </summary>
            /// <param name="pszPath">A pointer to a null-terminated string of maximum length MAX_PATH containing the path to be converted.</param>
            /// <param name="ppIdl">The path in pszPath expressed as a PIDL.</param>
            /// <param name="rgflnOut">A pointer to a DWORD value that, on entry, indicates any attributes of the folder named in pszPath that the calling application would like to retrieve along with the PIDL. On exit, this value contains those requested attributes. For a list of possible attribute flags for this parameter, see IShellFolder::GetAttributesOf.</param>
            /// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
            [DllImport("shell32.dll")]
            public static extern int SHILCreateFromPath([MarshalAs(UnmanagedType.LPWStr)] string pszPath, out IntPtr ppIdl, ref uint rgflnOut);

            /// <summary>
            /// SHCreateShellItem 関数の定義
            /// </summary>
            /// <param name="pidlParent">A PIDL to the parent. This value can be NULL.</param>
            /// <param name="psfParent">A pointer to the parent IShellFolder. This value can be NULL.</param>
            /// <param name="pidl">A PIDL to the requested item. If parent information is not included in pidlParent or psfParent, this must be an absolute PIDL.</param>
            /// <param name="ppsi">When this method returns, contains the interface pointer to the new IShellItem.</param>
            /// <returns>If this function succeeds, it returns S_OK. Otherwise, it returns an HRESULT error code.</returns>
            [DllImport("shell32.dll")]
            public static extern int SHCreateShellItem(IntPtr pidlParent, IntPtr psfParent, IntPtr pidl, out IShellItem ppsi);

            /// <summary>
            /// この操作はユーザーによって取り消されました。
            /// </summary>
            public const uint ERROR_CANCELLED = 0x800704C7;
        }
    }
}
