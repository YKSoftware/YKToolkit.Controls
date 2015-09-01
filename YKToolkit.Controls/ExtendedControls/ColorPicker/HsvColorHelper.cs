namespace YKToolkit.Controls
{
    using System;
    using System.Windows.Media;

    /// <summary>
    /// HSV 値による色表現に関するヘルパを提供します。
    /// </summary>
    internal static class HsvColorHelper
    {
        /// <summary>
        /// Color の情報から HsvColor の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="color">Color のインスタンスを指定します。</param>
        /// <returns>HsvColor の新しいインスタンスを返します。</returns>
        public static HsvColor HsvColorFromRgb(this Color color)
        {
            return HsvColorFromRgb(color.R, color.G, color.B);
        }

        /// <summary>
        /// HsvColor の情報から Color の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="hsvColor">HsvColor のインスタンスを指定します。</param>
        /// <returns>Color の新しいインスタンスを返します。</returns>
        public static Color ColorFromHsv(this HsvColor hsvColor)
        {
            return ColorFromHsv(hsvColor.H, hsvColor.S, hsvColor.V);
        }

        /// <summary>
        /// RGB 値を指定して HsvColor の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="r">R 値を指定します。</param>
        /// <param name="g">G 値を指定します。</param>
        /// <param name="b">B 値を指定します。</param>
        /// <returns>HsvColor の新しいインスタンスを返します。</returns>
        public static HsvColor HsvColorFromRgb(int r, int g, int b)
        {
            double delta, min;
            double h = 0, s, v;

            min = Math.Min(Math.Min(r, b), g);
            v = Math.Max(Math.Max(r, b), g);
            delta = v - min;

            if (v == 0.0)
            {
                s = 0;
            }
            else
            {
                s = delta / v;
            }

            if (s == 0)
            {
                h = 0.0;
            }
            else
            {
                if (r == v)
                    h = (b - g) / delta;
                else if (b == v)
                    h = 2 + (g - r) / delta;
                else if (g == v)
                    h = 4 + (r - b) / delta;

                h *= 60;
                if (h < 0.0)
                    h = h + 360;
            }

            v /= 255;

            return new HsvColor(h, s, v);
        }

        /// <summary>
        /// HSV 値を指定して Color の新しいインスタンスを生成します。
        /// </summary>
        /// <param name="h">H 値を指定します。</param>
        /// <param name="s">S 値を指定します。</param>
        /// <param name="v"> 値を指定します。</param>
        /// <returns>Color の新しいインスタンスを返します。</returns>
        public static Color ColorFromHsv(double h, double s, double v)
        {
            double r = 0, g = 0, b = 0;

            if (s == 0)
            {
                r = v;
                g = v;
                b = v;
            }
            else
            {
                int i;
                double f, p, q, t;

                if (h == 360)
                    h = 0;
                else
                    h = h / 60;

                i = (int)Math.Truncate(h);
                f = h - i;

                p = v * (1.0 - s);
                q = v * (1.0 - (s * f));
                t = v * (1.0 - (s * (1.0 - f)));

                switch (i)
                {
                    case 0:
                        {
                            r = v;
                            g = t;
                            b = p;
                            break;
                        }
                    case 1:
                        {
                            r = q;
                            g = v;
                            b = p;
                            break;
                        }
                    case 2:
                        {
                            r = p;
                            g = v;
                            b = t;
                            break;
                        }
                    case 3:
                        {
                            r = p;
                            g = q;
                            b = v;
                            break;
                        }
                    case 4:
                        {
                            r = t;
                            g = p;
                            b = v;
                            break;
                        }
                    default:
                        {
                            r = v;
                            g = p;
                            b = q;
                            break;
                        }
                }

            }

            return Color.FromArgb(255, (byte)(Math.Round(r * 255)), (byte)(Math.Round(g * 255)), (byte)(Math.Round(b * 255)));
        }
    }
}
