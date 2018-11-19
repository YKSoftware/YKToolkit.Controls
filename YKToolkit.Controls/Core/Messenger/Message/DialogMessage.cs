namespace YKToolkit.Controls
{
    using System;
using System.Windows;

    /// <summary>
    /// ダイアログ表示用のメッセージを表します。
    /// </summary>
    public class DialogMessage : Message
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="messageKey">メッセージキーを指定します。</param>
        /// <param name="callback">メッセージに紐付ける処理を指定します。</param>
        public DialogMessage(string messageKey, Action<object> callback)
            : base(messageKey)
        {
            this.Callback = callback;
        }

        /// <summary>
        /// ダイアログ上のボタンに表示するキャプションを取得または設定します。
        /// OK、Cancel、Yes、No の順に配列で指定します。
        /// 例えば OK と Cancel に対するキャプションのみ指定する場合は要素数 2 の配列を指定しても構いません。
        /// </summary>
        public string[] ButtonCaptions { get; set; }

        /// <summary>
        /// ダイアログのキャプションを取得または設定します。
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// ダイアログのメッセージを取得または設定します。
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// ダイアログ上に表示するボタンを取得または設定します。
        /// </summary>
        public MessageBoxButton DialogButton { get; set; }

        /// <summary>
        /// ダイアログ上に表示するアイコンを取得または設定します。
        /// </summary>
        public MessageBoxImage DialogImage { get; set; }

        /// <summary>
        /// ダイアログを表示する場所を指定します。
        /// </summary>
        public WindowStartupLocation Location { get; set; }
    }
}
