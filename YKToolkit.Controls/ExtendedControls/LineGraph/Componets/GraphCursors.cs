namespace YKToolkit.Controls
{
    using System;

    /// <summary>
    /// グラフカーソルを表します。
    /// </summary>
    [Flags]
    public enum GraphCursors
    {
        /// <summary>
        /// 該当しません。
        /// </summary>
        None = 0,

        /// <summary>
        /// 横軸カーソル 1 を表します。
        /// </summary>
        XCursor1 = 0x0001,

        /// <summary>
        /// 横軸カーソル 2 を表します。
        /// </summary>
        XCursor2 = 0x0002,
    }
}
