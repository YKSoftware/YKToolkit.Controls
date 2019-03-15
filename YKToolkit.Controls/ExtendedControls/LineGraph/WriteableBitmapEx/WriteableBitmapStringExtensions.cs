namespace System.Windows.Media.Imaging
{
    using System.Windows.Controls;

    static partial class WriteableBitmapStringExtensions
    {
        public static WriteableBitmap GetTextWriteableBitmap(string text, Orientation orientation = Orientation.Horizontal)
        {
            var txtBlock = new TextBlock();
            txtBlock.Text = text;

            // 1) get current dpi
            PresentationSource pSource = PresentationSource.FromVisual(Application.Current.MainWindow);
            Matrix m = pSource.CompositionTarget.TransformToDevice;
            double _dpiX = m.M11 * 96;
            double _dpiY = m.M22 * 96;

            txtBlock.Measure(new Size(double.MaxValue, double.MaxValue));
            txtBlock.Arrange(new Rect(new Point(0, 0), new Size(double.MaxValue, double.MaxValue)));
            var sz = txtBlock.DesiredSize;
            // 2) create RenderTargetBitmap
            var elementBitmap = new RenderTargetBitmap((int)sz.Width, (int)sz.Height, _dpiX, _dpiY, PixelFormats.Default);

            // 3) undo element transformation
            var drawingVisual = new DrawingVisual();
            using (var dc = drawingVisual.RenderOpen())
            {
                var visualBrush = new VisualBrush(txtBlock);
                dc.DrawRectangle(visualBrush, null, new Rect(new Point(0, 0), sz));
            }
            // 4) draw element
            elementBitmap.Render(drawingVisual);

            var wb = new WriteableBitmap(elementBitmap);
            if (orientation == Orientation.Vertical) wb = wb.Rotate(270);
            return wb;
        }

        /// <summary>
        /// 残件：Typeface 型をもらって TextBlock に反映するようにしたほうが良い
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="orientation"></param>
        public static void DrawString(this WriteableBitmap bmp, string text, double x, double y, Orientation orientation = Orientation.Horizontal)
        {
            var textBmp = GetTextWriteableBitmap(text, orientation);
            bmp.Blit(new Point(x, y), textBmp, new Rect(0, 0, textBmp.PixelWidth, textBmp.PixelHeight), Colors.White,
            WriteableBitmapExtensions.BlendMode.Alpha);
        }
    }
}
