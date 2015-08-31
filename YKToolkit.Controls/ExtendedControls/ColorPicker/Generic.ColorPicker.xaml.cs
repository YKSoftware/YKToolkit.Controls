namespace YKToolkit.Controls
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// ドロップダウン形式でコンテンツを表示するためのボタンを表します。
    /// </summary>
    public class ColorPicker : Control
    {
        #region コンストラクタ
        /// <summary>
        /// 静的なコンストラクタです。
        /// </summary>
        static ColorPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPicker), new FrameworkPropertyMetadata(typeof(ColorPicker)));
        }
        #endregion コンストラクタ
    }
}
