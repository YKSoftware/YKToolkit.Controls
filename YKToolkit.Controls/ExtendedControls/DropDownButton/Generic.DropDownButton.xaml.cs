namespace YKToolkit.Controls
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// ドロップダウン形式でコンテンツを表示するためのボタンを表します。
    /// </summary>
    public class DropDownButton : ContentControl
    {
        #region コンストラクタ
        /// <summary>
        /// 静的なコンストラクタです。
        /// </summary>
        static DropDownButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DropDownButton), new FrameworkPropertyMetadata(typeof(DropDownButton)));
        }
        #endregion コンストラクタ

        #region IsDropDownOpen 依存関係プロパティ
        /// <summary>
        /// IsDropDownOpen 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsDropDownOpenProperty = DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(DropDownButton), new PropertyMetadata(false));

        /// <summary>
        /// ドロップダウンコンテンツが表示されているかどうかを取得または設定します。
        /// </summary>
        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }
        #endregion IsDropDownOpen 依存関係プロパティ

        #region MaxDropDownHeight 依存関係プロパティ
        /// <summary>
        /// MaxDropDownHeight 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty MaxDropDownHeightProperty = DependencyProperty.Register("MaxDropDownHeight", typeof(double), typeof(DropDownButton), new PropertyMetadata(500.0));

        /// <summary>
        /// ドロップダウンコンテンツの最大高さを取得または設定します。
        /// </summary>
        public double MaxDropDownHeight
        {
            get { return (double)GetValue(MaxDropDownHeightProperty); }
            set { SetValue(MaxDropDownHeightProperty, value); }
        }
        #endregion MaxDropDownHeight 依存関係プロパティ

        #region DropDownContent 依存関係プロパティ
        /// <summary>
        /// DropDownContent 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty DropDownContentProperty = DependencyProperty.Register("DropDownContent", typeof(object), typeof(DropDownButton), new PropertyMetadata(null));

        /// <summary>
        /// ドロップダウンコンテンツを取得または設定します。
        /// </summary>
        public object DropDownContent
        {
            get { return GetValue(DropDownContentProperty); }
            set { SetValue(DropDownContentProperty, value); }
        }
        #endregion DropDownContent 依存関係プロパティ
    }
}
