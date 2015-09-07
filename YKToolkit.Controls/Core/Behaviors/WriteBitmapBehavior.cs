namespace YKToolkit.Controls.Behaviors
{
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// 指定されたファイルパスに自身を画像として保存するための添付ビヘイビア
    /// </summary>
    public class WriteBitmapBehavior
    {
        #region FilePath 添付プロパティ
        /// <summary>
        /// FilePath 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty FilePathProperty = DependencyProperty.RegisterAttached("FilePath", typeof(string), typeof(WriteBitmapBehavior), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnFilePathPropertyChanged));

        /// <summary>
        /// FilePath 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <returns>取得した値を返します。</returns>
        public static string GetFilePath(DependencyObject target)
        {
            return (string)target.GetValue(FilePathProperty);
        }

        /// <summary>
        /// FilePath 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">対象とする DependencyObject を指定します。</param>
        /// <param name="value">設定する値を指定します。</param>
        public static void SetFilePath(DependencyObject target, string value)
        {
            target.SetValue(FilePathProperty, value);
        }

        /// <summary>
        /// FilePath 添付プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnFilePathPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var element = sender as FrameworkElement;
            if (element == null)
                return;

            var path = GetFilePath(element);
            if (string.IsNullOrWhiteSpace(path))
                return;
            SetFilePath(element, null);

            // Margin の設定の分ずれる
            // 親パネルが Grid だと Grid.Row="0" Grid.Column="0" が基準となるため、親パネルは Border 等にする
            var bmp = new RenderTargetBitmap((int)element.ActualWidth, (int)element.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(element);

            var extension = Path.GetExtension(path).ToLower();
            BitmapEncoder encoder = null;
            switch (extension)
            {
                case ".bmp": encoder = new BmpBitmapEncoder(); break;
                default:
                case ".png": encoder = new PngBitmapEncoder(); break;
            }

            // 画像保存
            if (encoder != null)
            {
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                using (FileStream fs = File.Open(path, System.IO.FileMode.Create))
                {
                    encoder.Save(fs);
                }
            }

        }
        #endregion FilePath 添付プロパティ
    }
}
