namespace YKToolkit.Sample.Views
{
    using System.Windows;
    using System.Windows.Controls;
    using YKToolkit.Sample.Models;

    internal class AdornerTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return (container as FrameworkElement).FindResource((item as AdornedSampleModel).Name) as DataTemplate;
        }
    }
}
