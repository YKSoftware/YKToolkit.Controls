namespace YKToolkit.Sample.Views.Debug
{
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public class ItemDragBehavior
    {
        #region IsEnabled 添付プロパティ
        /// <summary>
        /// IsEnabled 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(ItemDragBehavior), new PropertyMetadata(false, OnIsEnabledPropertyChanged));

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
            var element = sender as UIElement;
            if (element == null)
                return;

            var isEnabled = GetIsEnabled(element);
            if (isEnabled)
            {
                //element.DragStarted += element_DragStarted;
                element.PreviewMouseLeftButtonDown += element_PreviewMouseLeftButtonDown;
            }
            else
            {
            }
        }

        static void element_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("PreviewMouseLeftButtonDown");

            var element = sender as FrameworkElement;
            //var parent = FindAncestor<System.Windows.Controls.ItemsControl>(element);
            //var container = parent.ContainerFromElement(element) as FrameworkElement;
            var layer = System.Windows.Documents.AdornerLayer.GetAdornerLayer(element);
            System.Diagnostics.Debug.WriteLine(layer == null ? "NULL" : layer.ToString());
        }

        static void element_DragStarted(object sender, DragStartedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("DragStarted");
        }
        #endregion IsEnabled 添付プロパティ

        private static T FindAncestor<T>(FrameworkElement element)
            where T : FrameworkElement
        {
            do
            {
                element = System.Windows.Media.VisualTreeHelper.GetParent(element) as FrameworkElement;
                if (element is T)
                    return element as T;
            } while (element != null);
            return null;
        }
    }
}
