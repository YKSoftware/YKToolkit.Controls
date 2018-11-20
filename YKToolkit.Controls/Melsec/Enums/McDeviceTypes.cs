namespace System.Net.Sockets
{
    /// <summary>
    /// PLC のビットデバイスの種類を表します。
    /// </summary>
    public enum McBitDeviceType : byte
    {
        /// <summary>
        /// 特殊リレー SM を表します。
        /// </summary>
        SM = McWordDeviceType.SM,

        /// <summary>
        /// 入力 X を表します。
        /// </summary>
        X = McWordDeviceType.X,

        /// <summary>
        /// 出力 Y を表します。
        /// </summary>
        Y = McWordDeviceType.Y,

        /// <summary>
        /// 内部リレー M を表します。
        /// </summary>
        M = McWordDeviceType.M,

        /// <summary>
        /// タイマー T の接点を表します。
        /// </summary>
        TS = McWordDeviceType.TS,
    }

    /// <summary>
    /// PLC のワードデバイスの種類を表します。
    /// </summary>
    public enum McWordDeviceType : byte
    {
        /// <summary>
        /// 特殊リレー SM を表します。
        /// </summary>
        SM = 0x91,

        /// <summary>
        /// 特殊レジスタ SD を表します。
        /// </summary>
        SD = 0xa9,

        /// <summary>
        /// 入力 X を表します。
        /// </summary>
        X = 0x9c,

        /// <summary>
        /// 出力 Y を表します。
        /// </summary>
        Y = 0x9d,

        /// <summary>
        /// 内部リレー M を表します。
        /// </summary>
        M = 0x90,

        /// <summary>
        /// データレジスタ D を表します。
        /// </summary>
        D = 0xa8,

        /// <summary>
        /// リンクレジスタ W を表します。
        /// </summary>
        W = 0xb4,

        /// <summary>
        /// タイマ T の接点を表します。
        /// </summary>
        TS = 0xc1,

        /// <summary>
        /// タイマ T の現在値を表します。
        /// </summary>
        TN = 0xc2,

        /// <summary>
        /// ファイルレジスタ ZR を表します。
        /// </summary>
        ZR = 0xb0,
    }
}
