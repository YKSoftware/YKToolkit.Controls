namespace YKToolkit.Controls.Converters
{
    using System.Windows.Data;

    /// <summary>
    /// 値を反転させるためのコンバーターを表します。
    /// </summary>
    public class InverseConverter : IValueConverter
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
            if (value is bool)
                return !(bool)value;
            if (value is int)
                return -(int)value;
            if (value is long)
                return -(long)value;
            if (value is double)
                return -(double)value;

            return null;
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
            if (value is bool)
                return !(bool)value;
            if (value is int)
                return -(int)value;
            if (value is long)
                return -(long)value;
            if (value is double)
                return -(double)value;

            return null;
        }
    }
}
