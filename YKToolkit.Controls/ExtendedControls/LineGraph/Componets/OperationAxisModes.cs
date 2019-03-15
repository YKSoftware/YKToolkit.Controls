namespace YKToolkit.Controls
{
    /// <summary>
    /// 軸操作モードを表します。
    /// </summary>
    public enum OperationAxisModes
    {
        /// <summary>
        /// 操作しません。
        /// </summary>
        None = -1,

        /// <summary>
        /// 横軸移動操作を表します。
        /// </summary>
        MoveX = 0,

        /// <summary>
        /// 縦軸移動操作を表します。
        /// </summary>
        MoveY,

        /// <summary>
        /// 横-縦軸移動操作を表します。
        /// </summary>
        MoveXY,

        /// <summary>
        /// 第 2 主軸移動操作を表します。
        /// </summary>
        MoveY2,

        /// <summary>
        /// 横-第 2 主軸移動操作を表します。
        /// </summary>
        MoveXY2,

        /// <summary>
        /// 横軸拡大操作を表します。
        /// </summary>
        ZoomX,

        /// <summary>
        /// 縦軸拡大操作を表します。
        /// </summary>
        ZoomY,

        /// <summary>
        /// 横-縦軸拡大操作を表します。
        /// </summary>
        ZoomXY,

        /// <summary>
        /// 第 2 主軸拡大操作を表します。
        /// </summary>
        ZoomY2,

        /// <summary>
        /// 横-第 2 主軸拡大操作を表します。
        /// </summary>
        ZoomXY2,
    }
}
