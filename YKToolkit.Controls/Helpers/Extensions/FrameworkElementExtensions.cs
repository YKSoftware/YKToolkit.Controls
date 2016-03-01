namespace YKToolkit.Helpers
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public static partial class Extensions
    {
        /// <summary>
        /// ビジュアルツリーの親要素を探索します。
        /// </summary>
        /// <param name="element">探索を開始する子要素を指定します。</param>
        /// <param name="type">探索する親要素の型を指定します。</param>
        /// <returns>指定された型の親要素を返します。存在しない場合は null を返します。</returns>
        public static FrameworkElement FindAncestor(this FrameworkElement element, Type type)
        {
            do
            {
                element = VisualTreeHelper.GetParent(element) as FrameworkElement;
                if (element.GetType() == type)
                    return element;
            } while (element != null);
            return null;
        }
    }
}
