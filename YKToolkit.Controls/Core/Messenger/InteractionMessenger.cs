namespace YKToolkit.Controls
{
    using System;

    /// <summary>
    /// メッセンジャーシステムにメッセージを送信する機能を提供します。
    /// </summary>
    public static class InteractionMessenger
    {
        #region InteractionMessageRaised

        /// <summary>
        /// InteractionMessageRaised イベントハンドラのデリゲートを表します。
        /// </summary>
        /// <param name="message">メッセージを指定します。</param>
        public delegate void OnInteractionMessageRaise(Message message);

        /// <summary>
        /// メッセンジャーシステムにメッセージが送信されたときに発生します。
        /// </summary>
        public static event OnInteractionMessageRaise InteractionMessageRaised;

        /// <summary>
        /// InteractionMessageRaised イベントを発行します。
        /// </summary>
        /// <param name="message"></param>
        private static void RaiseInteractionMessageRaised(Message message)
        {
            var h = InteractionMessenger.InteractionMessageRaised;
            if (h != null) h(message);
        }

        #endregion InteractionMessageRaised

        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="message">メッセージを指定します。</param>
        public static void Send(Message message)
        {
            RaiseInteractionMessageRaised(message);
        }
    }
}
