namespace YKToolkit.Controls.Behaviors
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// TreeView コントロールの自動水平スクロール機能を無効にするためのビヘイビアを表します。
    /// </summary>
    public class TreeViewAutoHorizontalScrollBehavior
    {
        #region IsEnabled 添付プロパティ
        /// <summary>
        /// IsEnabled 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(TreeViewAutoHorizontalScrollBehavior), new PropertyMetadata(true, OnIsEnabledPropertyChanged));

        /// <summary>
        /// IsEnabled 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <returns>取得した値を返します。</returns>
        public static bool GetIsEnabled(DependencyObject target)
        {
            return (bool)target.GetValue(IsEnabledProperty);
        }

        /// <summary>
        /// IsEnabled 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <param name="value">設定する値を指定します。</param>
        public static void SetIsEnabled(DependencyObject target, bool value)
        {
            target.SetValue(IsEnabledProperty, value);
        }

        /// <summary>
        /// IsEnabled 添付プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnIsEnabledPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as TreeViewItem;
            if (control == null)
                return;

            var isEnabled = GetIsEnabled(control);

            if (isEnabled)
            {
                control.RequestBringIntoView -= control_RequestBringIntoView;
            }
            else
            {
                control.RequestBringIntoView += control_RequestBringIntoView;
            }
        }
        #endregion IsEnabled 添付プロパティ

        /// <summary>
        /// RequestBringIntoView イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void control_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            var control = sender as TreeViewItem;
            e.Handled = control != null ? !GetIsEnabled(control) : false;
        }
    }
}
