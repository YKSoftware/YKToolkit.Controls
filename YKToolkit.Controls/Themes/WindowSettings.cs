namespace YKToolkit.Controls
{
    using System.Configuration;

    /// <summary>
    /// ウィンドウのサイズと位置を保存・復元するためのインターフェースを表します。
    /// </summary>
    public interface IWindowSettings
    {
        /// <summary>
        /// ウィンドウのサイズと位置を取得または設定します。
        /// </summary>
        User32.WINDOWPLACEMENT? Placement { get; set; }

        /// <summary>
        /// テーマ名を取得または設定します。
        /// </summary>
        string ThemeName { get; set; }

        /// <summary>
        /// 設定値を読み込みます。
        /// </summary>
        void Reload();

        /// <summary>
        /// 設定値を保存します。
        /// </summary>
        void Save();
    }

    /// <summary>
    /// IWindowSettings インターフェースを実装したクラスを表します。
    /// </summary>
    public class WindowSettings : ApplicationSettingsBase, IWindowSettings
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="window">Application Settings オブジェクトのオーナーを指定します。</param>
        public WindowSettings(System.Windows.Window window)
            : base(window.GetType().FullName)
        {
        }

        /// <summary>
        /// ウィンドウのサイズと位置を取得または設定します。
        /// </summary>
        [UserScopedSetting]
        public User32.WINDOWPLACEMENT? Placement
        {
            get { return this["Placement"] != null ? (User32.WINDOWPLACEMENT?)(User32.WINDOWPLACEMENT)this["Placement"] : null; }
            set { this["Placement"] = value; }
        }

        /// <summary>
        /// テーマ名を取得または設定します。
        /// </summary>
        [UserScopedSetting]
        public string ThemeName
        {
            get { return this["ThemeName"] != null ? this["ThemeName"] as string : null; }
            set { this["ThemeName"] = value; }
        }
    }
}
