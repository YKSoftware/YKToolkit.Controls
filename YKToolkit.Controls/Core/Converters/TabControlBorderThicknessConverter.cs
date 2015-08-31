namespace YKToolkit.Controls.Converters
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    internal class TabControlBorderThicknessConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var thickness = (Thickness)value;
            var placement = parameter as string;

            switch (placement)
            {
                case "Left": thickness.Left = 0; break;
                case "Top": thickness.Top = 0; break;
                case "Right": thickness.Right = 0; break;
                case "Bottom": thickness.Bottom = 0; break;
            }

            return thickness;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
