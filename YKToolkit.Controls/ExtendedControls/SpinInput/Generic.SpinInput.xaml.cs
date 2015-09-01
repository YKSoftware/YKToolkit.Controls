namespace YKToolkit.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// スピンボタンによる数値入力コントロールを表します。
    /// </summary>
    public class SpinInput : Control
    {
        #region コンストラクタ
        /// <summary>
        /// 静的なコンストラクタです。
        /// </summary>
        static SpinInput()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SpinInput), new FrameworkPropertyMetadata(typeof(SpinInput)));
        }
        #endregion コンストラクタ
    }
}
