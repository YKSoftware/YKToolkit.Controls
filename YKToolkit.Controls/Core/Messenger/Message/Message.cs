namespace YKToolkit.Controls
{
    using System;

    /// <summary>
    /// メッセンジャーシステムのメッセージを表します。
    /// </summary>
    public class Message
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="messageKey">メッセージキーを指定します。</param>
        public Message(string messageKey)
        {
            this.MessageKey = messageKey;
        }

        /// <summary>
        /// メッセージキーを取得します。
        /// </summary>
        public string MessageKey { get; private set; }

        /// <summary>
        /// パラメータを持つコールバック処理を取得または設定します。
        /// </summary>
        public Action<object> Callback { get; set; }
    }
}
