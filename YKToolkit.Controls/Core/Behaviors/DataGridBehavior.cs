namespace YKToolkit.Controls.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    /// <summary>
    /// <c>System.Windows.Controls.DataGrid</c> コントロールの行ヘッダにインデックスを付加する添付ビヘイビア
    /// </summary>
    public class DataGridBehavior
    {
        #region DisplayRowNumber
        /// <summary>
        /// DisplayRowNumber 添付プロパティの定義
        /// </summary>
        public static DependencyProperty DisplayRowNumberProperty = DependencyProperty.RegisterAttached("DisplayRowNumber", typeof(int?), typeof(DataGridBehavior), new FrameworkPropertyMetadata(null, OnDisplayRowNumberChanged));
        /// <summary>
        /// DisplayRowNumber 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">添付プロパティの取得先</param>
        /// <returns>取得結果</returns>
        public static int? GetDisplayRowNumber(DependencyObject target)
        {
            return (int?)target.GetValue(DisplayRowNumberProperty);
        }
        /// <summary>
        /// DisplayRowNumber 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">添付プロパティの設定対象</param>
        /// <param name="value">設定値を指定します。</param>
        public static void SetDisplayRowNumber(DependencyObject target, int? value)
        {
            target.SetValue(DisplayRowNumberProperty, value);
        }

        private static void OnDisplayRowNumberChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = target as DataGrid;
            if (e.NewValue != null)
            {
                var displayNumber = GetDisplayRowNumber(dataGrid);

                EventHandler<DataGridRowEventArgs> loadedRowHandler = null;
                loadedRowHandler = (object sender, DataGridRowEventArgs ea) =>
                {
                    if (displayNumber == null)
                    {
                        dataGrid.LoadingRow -= loadedRowHandler;
                        return;
                    }
                    ea.Row.Header = ea.Row.GetIndex() + displayNumber;
                };
                dataGrid.LoadingRow += loadedRowHandler;

                ItemsChangedEventHandler itemsChangedHandler = null;
                itemsChangedHandler = (object sender, ItemsChangedEventArgs ea) =>
                {
                    if (displayNumber == null)
                    {
                        dataGrid.ItemContainerGenerator.ItemsChanged -= itemsChangedHandler;
                        return;
                    }
                    GetVisualChildCollection<DataGridRow>(dataGrid).
                        ForEach(d => d.Header = d.GetIndex() + displayNumber);
                };
                dataGrid.ItemContainerGenerator.ItemsChanged += itemsChangedHandler;
            }
        }

        #endregion  // DisplayRowNumber

        #region Get Visuals

        private static List<T> GetVisualChildCollection<T>(object parent)
            where T : Visual
        {
            var visualCollection = new List<T>();
            GetVisualChildCollection(parent as DependencyObject, visualCollection);
            return visualCollection;
        }

        private static void GetVisualChildCollection<T>(DependencyObject parent, List<T> visualCollection)
            where T : Visual
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    visualCollection.Add(child as T);
                }
                if (child != null)
                {
                    GetVisualChildCollection(child, visualCollection);
                }
            }
        }

        #endregion  // Get Visuals
    }
}
