namespace System.Net.Sockets
{
    /// <summary>
    /// 監視タイマー設定を表します。
    /// </summary>
    public enum McTimeout : short
    {
        /// <summary>
        /// 処理が完了するまで待ち続けます。
        /// </summary>
        Forever = 0,

        /// <summary>
        /// 250[ms] 待ちます。
        /// </summary>
        Wait250ms = 0x0001,

        /// <summary>
        /// 500[ms] 待ちます。
        /// </summary>
        Wait500ms = 0x0002,

        /// <summary>
        /// 1[s] 待ちます。
        /// </summary>
        Wait1s = 0x0004,

        /// <summary>
        /// 2[s] 待ちます。
        /// </summary>
        Wait2s = 0x0008,

        /// <summary>
        /// 4[s] 待ちます。
        /// </summary>
        Wait4s = 0x0010,

        /// <summary>
        /// 10[s] 待ちます。
        /// </summary>
        Wait10s = 0x0028,

        /// <summary>
        /// 20[s] 待ちます。
        /// </summary>
        Wait20s = 0x0050,

        /// <summary>
        /// 30[s] 待ちます。
        /// </summary>
        Wait30s = 0x0078,

        /// <summary>
        /// 60[s] 待ちます。
        /// </summary>
        Wait60s = 0x00f0,
    }
}
