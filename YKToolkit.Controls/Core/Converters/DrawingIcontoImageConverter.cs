namespace YKToolkit.Controls.Converters
{
    using System.Drawing;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Interop;

    /// <summary>
    /// <c>System.Windows.MessageBoxImage</c> 列挙体で指定されたシステムアイコンを <c>System.Windows.Imaging.BitmapSource</c> クラスのインスタンスに変換するためのコンバータを表します。
    /// </summary>
    public class DrawingIcontoImageConverter : IValueConverter
    {
        /// <summary>
        /// 値を変換します。
        /// </summary>
        /// <param name="value">変換する値を指定します。</param>
        /// <param name="targetType">変換対象となっている値の型情報を指定します。</param>
        /// <param name="parameter">変換に使用するパラメータを指定します。</param>
        /// <param name="culture">変換に使用するカルチャ情報を指定します。</param>
        /// <returns>変換後の値を返します。</returns>
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var image = (MessageBoxImage)value;
            System.Drawing.Icon icon = null;
            switch (image)
            {
                case MessageBoxImage.None: break;

                case MessageBoxImage.Error: icon = SystemIcons.Error; break;
                //case MessageBoxImage.Hand: icon = SystemIcons.Hand; break;
                //case MessageBoxImage.Stop: icon = SystemIcons.Shield; break;

                case MessageBoxImage.Question: icon = SystemIcons.Question; break;

                case MessageBoxImage.Warning: icon = SystemIcons.Warning; break;
                //case MessageBoxImage.Exclamation: icon = SystemIcons.Exclamation; break;

                case MessageBoxImage.Information: icon = SystemIcons.Information; break;
                //case MessageBoxImage.Asterisk: icon = SystemIcons.Asterisk; break;
            }

            return icon != null ? Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, null) : null;
        }

        /// <summary>
        /// 値を逆変換します。
        /// </summary>
        /// <param name="value">変換する値を指定します。</param>
        /// <param name="targetType">変換対象となっている値の型情報を指定します。</param>
        /// <param name="parameter">変換に使用するパラメータを指定します。</param>
        /// <param name="culture">変換に使用するカルチャ情報を指定します。</param>
        /// <returns>変換後の値を返します。</returns>
        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
