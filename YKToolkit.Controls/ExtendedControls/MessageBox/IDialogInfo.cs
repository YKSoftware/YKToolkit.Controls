namespace YKToolkit.Controls
{
    using System.Windows;

    /// <summary>
    /// ダイアログに関する設定値を表すためのインターフェースを表します。
    /// </summary>
    public interface IDialogInfo
    {
        /// <summary>
        /// メッセージを取得します。
        /// </summary>
        string Message { get; }

        /// <summary>
        /// タイトルを取得します。
        /// </summary>
        string Title { get; }

        /// <summary>
        /// 表示するボタンの種類を取得します。
        /// </summary>
        MessageBoxButton MessageBoxButton { get; }

        /// <summary>
        /// 表示するアイコンの種類を取得します。
        /// </summary>
        MessageBoxImage MessageBoxImage { get; }
    }
}
