namespace YKToolkit.Controls
{
    using System;
    using System.Windows;

    /// <summary>
    /// ファイル書き込みコモンダイアログ表示用のメッセージを表します。
    /// </summary>
    public class SaveFileDialogMessage : Message
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="messageKey">メッセージキーを指定します。</param>
        /// <param name="callback">メッセージに紐付ける処理を指定します。</param>
        public SaveFileDialogMessage(string messageKey, Action<object> callback)
            : base(messageKey)
        {
            this.Callback = callback;
        }

        /// <summary>
        /// ダイアログキャプションを取得または設定します。
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// 既定のファイル名を取得または設定します。
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// ファイルフィルターを取得または設定します。
        /// </summary>
        public string FileFilter { get; set; }
    }
}
