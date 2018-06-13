namespace YKToolkit.Sample.Models
{
    using System;
    using System.Windows;
    using YKToolkit.Controls;

    /// <summary>
    /// ダイアログに関する設定値を表します。
    /// </summary>
    public class DialogInfo : IDialogInfo
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public DialogInfo()
        {
            MessageBoxButton = MessageBoxButton.OK;
            MessageBoxImage = MessageBoxImage.None;
        }

        /// <summary>
        /// メッセージを取得または設定します。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// タイトルを取得または設定します。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 表示するボタンの種類を取得または設定します。
        /// </summary>
        public MessageBoxButton MessageBoxButton { get; set; }

        /// <summary>
        /// 表示するアイコンの種類を取得または設定します。
        /// </summary>
        public MessageBoxImage MessageBoxImage { get; set; }

        /// <summary>
        /// ダイアログ終了後のコールバックを取得または設定します。
        /// </summary>
        public Action<MessageBoxResult> Callback { get; set; }

        /// <summary>
        /// OK ボタンのキャプションを取得または設定します。
        /// </summary>
        public object OkButtonCaption { get; set; }

        /// <summary>
        /// Cancel ボタンのキャプションを取得または設定します。
        /// </summary>
        public object CancelButtonCaption { get; set; }

        /// <summary>
        /// Yes ボタンのキャプションを取得または設定します。
        /// </summary>
        public object YesButtonCaption { get; set; }

        /// <summary>
        /// No ボタンのキャプションを取得または設定します。
        /// </summary>
        public object NoButtonCaption { get; set; }
    }

}
