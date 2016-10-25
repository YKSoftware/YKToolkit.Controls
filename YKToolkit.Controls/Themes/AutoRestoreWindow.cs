namespace YKToolkit.Controls
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Interop;

    /// <summary>
    /// ウィンドウの位置とサイズを保存・復元できる YKToolkit.Controls のテーマに則った Window コントロールを表します。
    /// </summary>
    public class AutoRestoreWindow : Window
    {
        #region WindowSettings プロパティ
        /// <summary>
        /// WindowSettings 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty WindowSettingsProperty = DependencyProperty.Register("WindowSettings", typeof(IWindowSettings), typeof(Window), new UIPropertyMetadata(null));

        /// <summary>
        /// ウィンドウ設定を取得または設定します。
        /// </summary>
        public WindowSettings WindowSettings
        {
            get { return (WindowSettings)GetValue(WindowSettingsProperty); }
            set { SetValue(WindowSettingsProperty, value); }
        }
        #endregion WindowSettings プロパティ

        /// <summary>
        /// ウィンドウハンドルが確定してから最速のイベントハンドラ
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // 外部からウィンドウ設定の保存・復元クラスが与えられていない場合は既定実装を使用する
            if (this.WindowSettings == null)
            {
                this.WindowSettings = new WindowSettings(this);
            }

            this.WindowSettings.Reload();

            if (this.WindowSettings.Placement.HasValue)
            {
                var hwnd = new WindowInteropHelper(this).Handle;
                var placement = this.WindowSettings.Placement.Value;
                placement.length = Marshal.SizeOf(typeof(User32.WINDOWPLACEMENT));
                placement.flags = 0;
                placement.showCmd = (placement.showCmd == User32.SW.SHOWMINIMIZED) ? User32.SW.SHOWNORMAL : placement.showCmd;

                User32.SetWindowPlacement(hwnd, ref placement);
            }

            if (this.WindowSettings.ThemeName != null)
            {
                ThemeManager.Instance.SetTheme(this.WindowSettings.ThemeName);
            }

            this._isSourceInitialized = true;
        }

        /// <summary>
        /// OnSourceInitialized イベントが発行されたときに true になります。
        /// </summary>
        private bool _isSourceInitialized;

        /// <summary>
        /// ウィンドウを閉じるときの処理
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            // 一度も開いていないウィンドウについては処理しない
            if (!this._isSourceInitialized)
                return;

            base.OnClosing(e);

            if (!e.Cancel)
            {
                User32.WINDOWPLACEMENT placement;
                var hwnd = new WindowInteropHelper(this).Handle;
                User32.GetWindowPlacement(hwnd, out placement);

                this.WindowSettings.Placement = placement;
                this.WindowSettings.ThemeName = ThemeManager.Instance.CurrentTheme;
                this.WindowSettings.Save();
            }
        }
    }
}
