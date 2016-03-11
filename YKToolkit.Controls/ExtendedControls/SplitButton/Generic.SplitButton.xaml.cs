namespace YKToolkit.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// ドロップダウン形式でコンテンツを表示したり直接コマンドを実行したりするボタンを表します。
    /// </summary>
    public class SplitButton : ContentControl
    {
        #region コンストラクタ
        /// <summary>
        /// 静的なコンストラクタです。
        /// </summary>
        static SplitButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SplitButton), new FrameworkPropertyMetadata(typeof(SplitButton)));
        }
        #endregion コンストラクタ

        #region IsDropDownOpen 依存関係プロパティ
        /// <summary>
        /// IsDropDownOpen 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsDropDownOpenProperty = DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(SplitButton), new PropertyMetadata(false));

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
        public static readonly DependencyProperty MaxDropDownHeightProperty = DependencyProperty.Register("MaxDropDownHeight", typeof(double), typeof(SplitButton), new PropertyMetadata(500.0));

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
        public static readonly DependencyProperty DropDownContentProperty = DependencyProperty.Register("DropDownContent", typeof(object), typeof(SplitButton), new PropertyMetadata(null));

        /// <summary>
        /// ドロップダウンコンテンツを取得または設定します。
        /// </summary>
        public object DropDownContent
        {
            get { return GetValue(DropDownContentProperty); }
            set { SetValue(DropDownContentProperty, value); }
        }
        #endregion DropDownContent 依存関係プロパティ

        #region CloseTriggerObject 依存関係プロパティ
        /// <summary>
        /// CloseTriggerObject 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty CloseTriggerObjectProperty = DependencyProperty.Register("CloseTriggerObject", typeof(object), typeof(SplitButton), new PropertyMetadata(null, OnCloseTriggerObjectPropertyChanged));

        /// <summary>
        /// ドロップダウンコンテンツを閉じるトリガオブジェクトを取得または設定します。
        /// </summary>
        public object CloseTriggerObject
        {
            get { return GetValue(CloseTriggerObjectProperty); }
            set { SetValue(CloseTriggerObjectProperty, value); }
        }

        /// <summary>
        /// CloseTriggerObject プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnCloseTriggerObjectPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as SplitButton;
            if (control != null)
            {
                control.IsDropDownOpen = false;
            }
        }
        #endregion CloseTriggerObject 依存関係プロパティ

        #region Command 依存関係プロパティ
        /// <summary>
        /// Command 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(SplitButton), new PropertyMetadata(null));

        /// <summary>
        /// コマンドを取得または設定します。
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        #endregion Command 依存関係プロパティ

        #region CommandParameter 依存関係プロパティ
        /// <summary>
        /// CommandParameter 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(SplitButton), new PropertyMetadata(null));

        /// <summary>
        /// コマンドパラメーターを取得または設定します。
        /// </summary>
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        #endregion CommandParameter 依存関係プロパティ
    }
}
