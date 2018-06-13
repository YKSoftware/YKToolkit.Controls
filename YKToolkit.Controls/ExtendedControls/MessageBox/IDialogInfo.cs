namespace YKToolkit.Controls
{
    using System;
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

        /// <summary>
        /// ダイアログ終了後のコールバックを取得します。
        /// </summary>
        Action<MessageBoxResult> Callback { get; }

        /// <summary>
        /// OK ボタンのキャプションを取得または設定します。
        /// </summary>
        object OkButtonCaption { get; }

        /// <summary>
        /// Cancel ボタンのキャプションを取得または設定します。
        /// </summary>
        object CancelButtonCaption { get; }

        /// <summary>
        /// Yes ボタンのキャプションを取得または設定します。
        /// </summary>
        object YesButtonCaption { get; }

        /// <summary>
        /// No ボタンのキャプションを取得または設定します。
        /// </summary>
        object NoButtonCaption { get; }
    }
}
