namespace YKToolkit.Controls.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows;
    using System.Windows.Interop;

    /// <summary>
    /// 準備完了状態のドライブパスコレクションを取得するためのビヘイビアです。
    /// </summary>
    public class NotifyUsbDeviceChangedBehavior
    {
        #region EnabledDrives 添付プロパティ
        /// <summary>
        /// EnabledDrives 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty EnabledDrivesProperty = DependencyProperty.RegisterAttached("EnabledDrives", typeof(ICollection<string>), typeof(NotifyUsbDeviceChangedBehavior), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// EnabledDrives 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <returns>取得した値を返します。</returns>
        public static ICollection<string> GetEnabledDrives(DependencyObject target)
        {
            return (ICollection<string>)target.GetValue(EnabledDrivesProperty);
        }

        /// <summary>
        /// EnabledDrives 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <param name="value">設定する値を指定します。</param>
        public static void SetEnabledDrives(DependencyObject target, ICollection<string> value)
        {
            target.SetValue(EnabledDrivesProperty, value);
        }
        #endregion EnabledDrives 添付プロパティ

        #region IsEnabled 添付プロパティ
        /// <summary>
        /// IsEnabled 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(NotifyUsbDeviceChangedBehavior), new PropertyMetadata(false, OnIsEnabledPropertyChanged));

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

            if (window.IsLoaded)
            {
                RegiterShNotify(window);
            }
            else
            {
                window.SourceInitialized += (obj, args) =>
                {
                    var w = obj as Window;
                    if (w == null)
                        return;

                    RegiterShNotify(w);
                };
            }

            var drives = GetEnabledDrives(window);
            if (drives == null)
                drives = new ObservableCollection<string>();

            var infoArray = DriveInfo.GetDrives();
            foreach (var info in infoArray)
            {
                if (info.IsReady)
                {
                    drives.Add(info.Name.Substring(0, 1));
                }
            }
            SetEnabledDrives(window, drives);
        }

        private static void RegiterShNotify(Window window)
        {
            var handle = (new WindowInteropHelper(window)).Handle;
            var hwndSource = HwndSource.FromHwnd(handle);
            hwndSource.AddHook(WndProc);

            // WM_SHNOTIFY をハンドルするためのおまじない
            var notifyEntry = new Shell32.SHChangeNotifyEntry() { pIdl = IntPtr.Zero, Recursively = true };
            var notifyId = Shell32.SHChangeNotifyRegister(handle,
                                                    Shell32.Constant.SHCNF_TYPE | Shell32.Constant.SHCNF_IDLIST,
                                                    Shell32.Constant.SHCNE_MEDIAINSERTED | Shell32.Constant.SHCNE_MEDIAREMOVED,
                                                    (uint)Shell32.Constant.WM_SHNOTIFY,
                                                    1,
                                                    ref notifyEntry);
        }
        #endregion IsEnabled 添付プロパティ

        #region Callback 添付プロパティ
        /// <summary>
        /// Callback 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty CallbackProperty = DependencyProperty.RegisterAttached("Callback", typeof(Action<string, bool>), typeof(NotifyUsbDeviceChangedBehavior), new PropertyMetadata(null));

        /// <summary>
        /// Callback 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <returns>取得した値を返します。</returns>
        public static Action<string, bool> GetCallback(DependencyObject target)
        {
            return (Action<string, bool>)target.GetValue(CallbackProperty);
        }

        /// <summary>
        /// Callback 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <param name="value">設定する値を指定します。</param>
        public static void SetCallback(DependencyObject target, Action<string, bool> value)
        {
            target.SetValue(CallbackProperty, value);
        }
        #endregion Callback 添付プロパティ

        /// <summary>
        /// ウィンドウメッセージハンドラ
        /// </summary>
        /// <param name="hwnd">ウィンドウハンドラ</param>
        /// <param name="msg">ウィンドウメッセージ</param>
        /// <param name="wParam">パラメータ</param>
        /// <param name="lParam">サブパラメータ</param>
        /// <param name="handled">以降の処理をスキップしたい場合はコード内で true を指定します。</param>
        /// <returns>ゼロポインタを返します。</returns>
        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            var hwndSource = HwndSource.FromHwnd(hwnd);
            var target = hwndSource.RootVisual;
            if (target != null)
            {
                int item1 = 0;
                int devType = 0;
                string driveString;

                if (msg == Shell32.Constant.WM_SHNOTIFY)
                {
                    var driveRootPathBuffer = new StringBuilder("A:\\");
                    switch ((uint)lParam)
                    {
                        case Shell32.Constant.SHCNE_MEDIAINSERTED:
                            // 構造体の一部を読み取る
                            item1 = Marshal.ReadInt32(wParam);
                            // ドライブレターを確定する
                            Shell32.SHGetPathFromIDList((IntPtr)item1, driveRootPathBuffer);
                            // string 型に直す
                            driveString = driveRootPathBuffer.ToString().Substring(0, 1);
                            // コールバック処理をおこなう
                            handled = CheckDrive(target, driveString, true);
                            break;

                        case Shell32.Constant.SHCNE_MEDIAREMOVED:
                            // 構造体の一部を読み取る
                            item1 = Marshal.ReadInt32(wParam);
                            // ドライブレターを確定する
                            Shell32.SHGetPathFromIDList((IntPtr)item1, driveRootPathBuffer);
                            // string 型に直す
                            driveString = driveRootPathBuffer.ToString().Substring(0, 1);
                            // コールバック処理をおこなう
                            handled = CheckDrive(target, driveString, false);
                            break;
                    }
                }
                else if (msg == Shell32.Constant.WM_DEVICECHANGE)
                {
                    switch ((int)wParam)
                    {
                        case Shell32.Constant.DBT_CONFIGCHANGECANCELED:
                            break;

                        case Shell32.Constant.DBT_CONFIGCHANGED:
                            break;

                        case Shell32.Constant.DBT_CUSTOMEVENT:
                            break;

                        case Shell32.Constant.DBT_DEVICEARRIVAL:
                            devType = Marshal.ReadInt32(lParam, 4);
                            if (devType == Shell32.Constant.DBT_DEVTYP_VOLUME)
                            {
                                var vol = (Shell32.DEV_BROADCAST_VOLUME)Marshal.PtrToStructure(lParam, typeof(Shell32.DEV_BROADCAST_VOLUME));
                                driveString = Shell32.GetDriveString(vol.dbcv_unitmask);
                                // コールバック処理をおこなう
                                handled = CheckDrive(target, driveString, true);
                            }
                            break;

                        case Shell32.Constant.DBT_DEVICEQUERYREMOVE:
                            break;

                        case Shell32.Constant.DBT_DEVICEQUERYREMOVEFAILED:
                            break;

                        case Shell32.Constant.DBT_DEVICEREMOVECOMPLETE:
                            devType = Marshal.ReadInt32(lParam, 4);
                            if (devType == Shell32.Constant.DBT_DEVTYP_VOLUME)
                            {
                                var vol = (Shell32.DEV_BROADCAST_VOLUME)Marshal.PtrToStructure(lParam, typeof(Shell32.DEV_BROADCAST_VOLUME));
                                driveString = Shell32.GetDriveString(vol.dbcv_unitmask);
                                // コールバック処理をおこなう
                                handled = CheckDrive(target, driveString, false);
                            }
                            break;

                        case Shell32.Constant.DBT_DEVICEREMOVEPENDING:
                            break;

                        case Shell32.Constant.DBT_DEVICETYPESPECIFIC:
                            break;

                        case Shell32.Constant.DBT_DEVNODES_CHANGED:
                            break;

                        case Shell32.Constant.DBT_QUERYCHANGECONFIG:
                            break;

                        case Shell32.Constant.DBT_USERDEFINED:
                            break;
                    }
                }
            }

            return IntPtr.Zero;
        }

        /// <summary>
        /// 指定のドライブパスの状態を確認し、ドライブパスコレクションを更新します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <param name="driveString">ドライブパスを指定します。</param>
        /// <param name="checkState">確認する状態を指定します。</param>
        /// <returns></returns>
        private static bool CheckDrive(DependencyObject target, string driveString, bool checkState)
        {
            if (target == null)
                return false;

            var drives = GetEnabledDrives(target);
            if (drives == null)
                drives = new ObservableCollection<string>();

            var callback = GetCallback(target);

            // DriveInfo を取得
            var info = new DriveInfo(driveString);
            // 確認状態と一致することを確認する
            if (checkState)
            {
                // 追加の確認をおこなう
                if (info.IsReady == checkState)
                {
                    if (!drives.Contains(driveString))
                    {
                        drives.Add(driveString);
                        SetEnabledDrives(target, drives);
                        if (callback != null)
                            callback(driveString, checkState);
                        return true;
                    }
                }
            }
            else
            {
                // 削除の確認をおこなう
                if (info.IsReady == checkState)
                {
                    if (drives.Contains(driveString))
                    {
                        drives.Remove(driveString);
                        SetEnabledDrives(target, drives);
                        if (callback != null)
                            callback(driveString, checkState);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
