namespace YKToolkit.Controls
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    /// <summary>
    /// ドロップダウン形式でコンテンツを表示するためのボタンを表します。
    /// </summary>
    [TemplatePart(Name = PART_DropDownPopup, Type = typeof(Popup))]
    public class DropDownButton : ContentControl
    {
        #region TemplatePart
        private const string PART_DropDownPopup = "PART_DropDownPopup";

        private Popup _dropDownPopup;
        private Popup DropDownPopup
        {
            get { return _dropDownPopup; }
            set { _dropDownPopup = value; }
        }

        /// <summary>
        /// テンプレート適用時の処理
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.DropDownPopup = this.Template.FindName(PART_DropDownPopup, this) as Popup;
        }
        #endregion TemplatePart

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
        public static readonly DependencyProperty IsDropDownOpenProperty = DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(DropDownButton), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsDropDownOpenPropertyChanged));

        /// <summary>
        /// ドロップダウンコンテンツが表示されているかどうかを取得または設定します。
        /// </summary>
        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        /// <summary>
        /// IsDropDownOpen 依存関係プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnIsDropDownOpenPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as DropDownButton;
            if (control == null)
                return;

            if (control.DropDownPopup == null)
                return;

            control.DropDownPopup.IsOpen = control.IsDropDownOpen;
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

        #region CloseTriggerObject 依存関係プロパティ
        /// <summary>
        /// CloseTriggerObject 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty CloseTriggerObjectProperty = DependencyProperty.Register("CloseTriggerObject", typeof(object), typeof(DropDownButton), new PropertyMetadata(null, OnCloseTriggerObjectPropertyChanged));

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
            var control = sender as DropDownButton;
            if (control != null)
            {
                control.IsDropDownOpen = false;
            }
        }
        #endregion CloseTriggerObject 依存関係プロパティ
    }
}
