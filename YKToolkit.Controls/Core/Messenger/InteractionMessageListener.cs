namespace YKToolkit.Controls
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// メッセンジャーシステムのメッセージリスナーを表します。
    /// </summary>
    public static class InteractionMessageListener
    {
        /// <summary>
        /// 静的なコンストラクタ
        /// </summary>
        static InteractionMessageListener()
        {
            InteractionMessenger.InteractionMessageRaised += OnInteractionMessageRaised;
        }

        /// <summary>
        /// InteractionMessageRaised イベントハンドラ
        /// </summary>
        /// <param name="message">送信されたメッセージ</param>
        private static void OnInteractionMessageRaised(Message message)
        {
            Func<Message, object> func = null;
            if (InteractionMessageListener._dic.TryGetValue(message.MessageKey, out func))
            {
                var obj = func(message);
                if (message.Callback != null) message.Callback(obj);
            }
        }

        /// <summary>
        /// メッセンジャーシステムにメッセージを登録します。
        /// </summary>
        /// <param name="messageKey">登録するメッセージキーを指定します。</param>
        /// <param name="func">メッセージキーに対する処理を指定します。</param>
        public static void Register(string messageKey, Func<Message, object> func)
        {
            if (!InteractionMessageListener._dic.ContainsKey(messageKey))
                InteractionMessageListener._dic.Add(messageKey, func);
        }

        /// <summary>
        /// メッセンジャーシステムからメッセージを登録解除します。
        /// </summary>
        /// <param name="messageKey">登録解除するメッセージキーを指定します。</param>
        public static void Unregister(string messageKey)
        {
            if (!InteractionMessageListener._dic.ContainsKey(messageKey))
                InteractionMessageListener._dic.Remove(messageKey);
        }

        /// <summary>
        /// メッセージキーとメッセージ処理を紐付けるためのディクショナリ
        /// </summary>
        private static Dictionary<string, Func<Message, object>> _dic = new Dictionary<string, Func<Message, object>>();
    }
}
