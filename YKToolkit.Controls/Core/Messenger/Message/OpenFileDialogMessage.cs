namespace YKToolkit.Controls
{
    using System;
    using System.Windows;

    /// <summary>
    /// ファイル読み込みコモンダイアログ表示用のメッセージを表します。
    /// </summary>
    public class OpenFileDialogMessage : Message
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="messageKey">メッセージキーを指定します。</param>
        /// <param name="callback">メッセージに紐付ける処理を指定します。</param>
        public OpenFileDialogMessage(string messageKey, Action<object> callback)
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

        /// <summary>
        /// ファイル複数選択を許可するかどうかを取得または設定します。
        /// </summary>
        public bool IsMultiSelect { get; set; }
    }
}
