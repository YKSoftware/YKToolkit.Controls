namespace YKToolkit.Controls.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    /// <summary>
    /// <c>System.Windows.Controls.DataGrid</c> コントロールの行ヘッダにインデックスを付加するビヘイビアを表します。
    /// </summary>
    public class DataGridBehavior
    {
        #region DisplayRowNumber 添付プロパティ
        /// <summary>
        /// DisplayRowNumber 添付プロパティの定義
        /// </summary>
        public static DependencyProperty DisplayRowNumberProperty = DependencyProperty.RegisterAttached("DisplayRowNumber", typeof(int?), typeof(DataGridBehavior), new FrameworkPropertyMetadata(null, OnDisplayRowNumberChanged));
        /// <summary>
        /// DisplayRowNumber 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <returns>取得した値を返します。</returns>
        public static int? GetDisplayRowNumber(DependencyObject target)
        {
            return (int?)target.GetValue(DisplayRowNumberProperty);
        }
        /// <summary>
        /// DisplayRowNumber 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <param name="value">設定値を指定します。</param>
        public static void SetDisplayRowNumber(DependencyObject target, int? value)
        {
            target.SetValue(DisplayRowNumberProperty, value);
        }

        /// <summary>
        /// DisplayRowNumber 添付プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnDisplayRowNumberChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid == null)
                return;

            if (e.NewValue != null)
            {
                var displayNumber = GetDisplayRowNumber(dataGrid);

                EventHandler<DataGridRowEventArgs> loadedRowHandler = null;
                loadedRowHandler = (object _, DataGridRowEventArgs ea) =>
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
                itemsChangedHandler = (object _, ItemsChangedEventArgs ea) =>
                {
                    if (displayNumber == null)
                    {
                        dataGrid.ItemContainerGenerator.ItemsChanged -= itemsChangedHandler;
                        return;
                    }
                    // 子要素の DataGridRow クラスに対してのみヘッダ情報を書き換える
                    GetVisualChildCollection<DataGridRow>(dataGrid).
                        ForEach(d => d.Header = d.GetIndex() + displayNumber);
                };
                dataGrid.ItemContainerGenerator.ItemsChanged += itemsChangedHandler;
            }
        }
        #endregion DisplayRowNumber 添付プロパティ

        /// <summary>
        /// 指定された型の子要素をリストとして取得します。
        /// </summary>
        /// <typeparam name="T">リストアップする型を指定します。</typeparam>
        /// <param name="parent">子要素を持つ親を指定します。</param>
        /// <returns>指定された型の子要素のみを集めたリストを返します。</returns>
        private static List<T> GetVisualChildCollection<T>(object parent)
            where T : Visual
        {
            var visualCollection = new List<T>();
            GetVisualChildCollection(parent as DependencyObject, visualCollection);
            return visualCollection;
        }

        /// <summary>
        /// 指定された型の子要素を与えられたリストに追加します。
        /// </summary>
        /// <typeparam name="T">リストアップする型を指定します。</typeparam>
        /// <param name="parent">子要素を持つ親を指定します。</param>
        /// <param name="visualCollection">リストアップするためのリストを指定します。</param>
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
    }
}
