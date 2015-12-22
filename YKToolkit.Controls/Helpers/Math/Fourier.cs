namespace YKToolkit.Helpers
{
    using System;
    using System.Linq;
    using System.Windows;

    /// <summary>
    /// 拡張メソッドを提供します。
    /// </summary>
    public static partial class YKMath
    {
        #region FFT 関連

        #region 公開メソッド
        /// <summary>
        /// FFT 演算をおこないます。
        /// </summary>
        /// <param name="source">フーリエ変換をおこなうデータ配列 (要素数は 2 の累乗であること)</param>
        /// <returns>フーリエ変換後のデータ配列</returns>
        public static Complex[] FFT(this double[] source)
        {
            return FFT(source.Select(i => new Complex(i)).ToArray(), WindowFunctions.Rectangle);
        }

        /// <summary>
        /// FFT 演算をおこないます。
        /// </summary>
        /// <param name="source">フーリエ変換をおこなうデータ配列 (要素数は 2 の累乗であること)</param>
        /// <param name="windowFunction">窓関数を選択する</param>
        /// <returns>フーリエ変換後のデータ配列</returns>
        public static Complex[] FFT(this double[] source, WindowFunctions windowFunction)
        {
            return FFT(source.Select(i => new Complex(i)).ToArray(), windowFunction);
        }

        /// <summary>
        /// FFT 演算をおこないます。
        /// </summary>
        /// <param name="source">フーリエ変換をおこなうデータ配列 (要素数は 2 の累乗であること)</param>
        /// <returns>フーリエ変換後のデータ配列</returns>
        public static Complex[] FFT(this Complex[] source)
        {
            return FFT(source, WindowFunctions.Rectangle);
        }

        /// <summary>
        /// FFT 演算をおこないます。
        /// </summary>
        /// <param name="source">フーリエ変換をおこなうデータ配列 (要素数は 2 の累乗であること)</param>
        /// <param name="windowFunction">窓関数を指定する (デフォルトは矩形窓)</param>
        /// <returns>フーリエ変換後のデータ配列</returns>
        public static Complex[] FFT(this Complex[] source, WindowFunctions windowFunction)
        {
            int length = source != null ? source.Length : 0;
            if (length <= 0)
                return null;

            // 2 の累乗かどうかを確認する
            if ((length & (length - 1)) != 0)
                return null;

            var data = source.Clone() as Complex[];
            var win = GetWindowFunction(windowFunction, length);
            for (int i = 0; i < length; i++)
            {
                data[i] *= win[i];
            }

            return FFTCalculation(data, false);
        }

        /// <summary>
        /// IFFT 演算をおこないます。
        /// </summary>
        /// <param name="source">フーリエ逆変換をおこなうデータ配列 (要素数は 2 の累乗であること)</param>
        /// <returns>フーリエ逆変換後のデータ配列</returns>
        public static Complex[] IFFT(this Complex[] source)
        {
            int length = source != null ? source.Length : 0;
            if (length <= 0)
                return null;

            // 2 の累乗かどうかを確認する
            if ((length & (length - 1)) != 0)
                return null;

            return FFTCalculation(source, true);
        }
        #endregion  公開メソッド

        #region private
        /// <summary>
        /// バタフライ演算によるフーリエ変換をおこないます。
        /// </summary>
        /// <param name="source">フーリエ変換対象データ配列 (要素数は必ず 2 の累乗であること)</param>
        /// <param name="isInverse">フーリエ逆変換をおこなう場合は true にする</param>
        /// <returns>フーリエ変換後のデータ配列</returns>
        private static Complex[] FFTCalculation(Complex[] source, bool isInverse)
        {
            var data = source.Clone() as Complex[];

            try
            {
                var length = data.Length;
                var sign = 1.0;
                int i, j, k;

                if (!isInverse)
                {
                    for (i = 0; i < data.Length; i++)
                    {
                        data[i].Real /= length;
                        data[i].Imaginary /= length;
                    }
                    sign = -1.0;
                }

                // 回転因子の計算
                var twiddleFactor = new Complex[length / 2];
                for (i = 0; i < length / 2; i++)
                {
                    twiddleFactor[i] = new Complex(Math.Cos(2.0 * i * Math.PI / length), sign * Math.Sin(2.0 * i * Math.PI / length));
                }

                // ビット反転
                j = 0;
                for (i = 0; i <= length - 2; i++)
                {
                    if (i < j)
                    {
                        // データのビット反転
                        var tmp = data[j].Clone();
                        data[j] = data[i].Clone();
                        data[i] = tmp.Clone();
                    }

                    // 0x000, 0x100, 0x010, 0x110, 0x001... のように上位からビットが進むようにする
                    k = length / 2;
                    while (k <= j)
                    {
                        j -= k;
                        k /= 2;
                    }
                    j += k;
                }

                // バタフライ演算
                var power = (int)(Math.Log(length, 2) + 0.5);
                for (i = 1; i <= power; i++)
                {
                    var m = (int)(1 << i);
                    var h = m / 2;

                    for (j = 0; j < h; j++)
                    {
                        for (k = j; k < length; k += m)
                        {
                            var kp = k + h;
                            var vtmp = twiddleFactor[j * length / m] * data[kp];

                            // ビット反転
                            data[kp] = data[k] - vtmp;
                            data[k] += vtmp;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err.Message);
            }

            return data;
        }
        #endregion  private

        #endregion  FFT 関連

        #region 窓関数関連
        /// <summary>
        /// 窓関数を取得します。
        /// </summary>
        /// <param name="windowFunction">窓関数を選択する</param>
        /// <param name="length">データ数を指定する</param>
        /// <returns>窓関数値配列</returns>
        public static double[] GetWindowFunction(WindowFunctions windowFunction, int length)
        {
            return GetWindowFunction(windowFunction, length, 0);
        }

        /// <summary>
        /// 窓関数を取得します。
        /// </summary>
        /// <param name="windowFunction">窓関数を選択する</param>
        /// <param name="length">データ数を指定する</param>
        /// <param name="attenuation">(カイザー窓関数選択時のみ)減衰量を指定する</param>
        /// <returns>窓関数値配列</returns>
        public static double[] GetWindowFunction(WindowFunctions windowFunction, int length, double attenuation)
        {
            double[] win = null;

            switch (windowFunction)
            {
                default:
                case WindowFunctions.Rectangle:
                    win = RectangleWindowFunction(length);
                    break;

                case WindowFunctions.Hanning:
                    win = HanningWindowFunction(length, true);
                    break;

                case WindowFunctions.Kaiser:
                    win = KaiserWindowFunction(length, attenuation);
                    break;
            }

            return win;
        }

        /// <summary>
        /// 矩形窓関数を取得します。
        /// </summary>
        /// <param name="length">窓関数の長さ (1 より大きい整数)</param>
        /// <returns>窓関数値配列</returns>
        public static double[] RectangleWindowFunction(int length)
        {
            return Enumerable.Range(0, length).Select(i => 1.0).ToArray();
        }

        /// <summary>
        /// ハニング窓関数を取得します。
        /// isPeriodic に true を指定した場合、指定した長さより 1 長い窓関数を生成して、
        /// 先頭から長さ length の値を返します。
        /// </summary>
        /// <param name="length">窓関数の長さ (1 より大きい整数)</param>
        /// <param name="isPeriodic">周期的な関数として生成する場合は true を指定します。false の場合は対称的な関数となります。<br/>FFT などに用いる場合は true、フィルタ設計などに用いる場合は false にすることを推奨します。</param>
        /// <returns>窓関数値配列</returns>
        public static double[] HanningWindowFunction(int length, bool isPeriodic)
        {
            var len = isPeriodic ? length + 1 : length;
            return len > 1 ? Enumerable.Range(0, len).Select(i => 0.5 - 0.5 * Math.Cos(2.0 * Math.PI * i / (len - 1))).Take(length).ToArray() : null;
        }

        /// <summary>
        /// カイザー窓関数を取得します。
        /// </summary>
        /// <param name="length">窓関数の長さ (1 より大きい整数)</param>
        /// <param name="attenuation">減衰値</param>
        /// <returns>窓関数値配列</returns>
        public static double[] KaiserWindowFunction(int length, double attenuation)
        {
            var alpha = 0.5842 * System.Math.Pow(attenuation - 21.0, 0.4) + 0.07886 * (attenuation - 21.0);
            if (attenuation >= 50.0)
                alpha = 0.1102 * (attenuation - 8.7);
            else if (attenuation < 21.0)
                alpha = 0.0;

            var w = new double[length];

            for (int i = 0; i < length; i++)
            {
                var k = i > (length - 1) / 2 ? length - i : i;
                var a = System.Math.PI * alpha;
                var b = a * System.Math.Sqrt(1.0 - System.Math.Pow((2.0 * k / (double)(length - 1)), 2.0));
                w[i] = CalcBessel(b) / CalcBessel(a);
            }

            return w;
        }

        /// <summary>
        /// ベッセル関数値を算出します。
        /// </summary>
        /// <param name="x">ベッセル関数の次数</param>
        /// <returns>ベッセル関数値</returns>
        public static double CalcBessel(double x)
        {
            var x_half = x / 2.0;
            double y = 1.0;
            double y_old = 1.0;
            double pow = 1.0;
            double factorial = 1.0;
            const int LargeValue = 100;

            for (int i = 1; i < LargeValue; i++)
            {
                pow *= x_half;
                factorial *= i;
                y += pow / factorial;
                if ((y - y_old) < 1e-14)
                    break;

                y_old = y;
            }

            return y;
        }
        #endregion  窓関数関連

        #region 2 次補間
        /// <summary>
        /// <para>3 点データの 2 次補間によるピーク値のときの x 座標値を算出します。</para>
        /// <para>ピーク座標の前後を含む 3 点の座標を用いたピーク値補間メソッドです。</para>
        /// </summary>
        /// <param name="x">ピーク値のひとつ手前の座標</param>
        /// <param name="y">ピーク値の座標</param>
        /// <param name="z">ピーク値のひとつ後の座標</param>
        /// <returns>ピーク値のときの x 座標値</returns>
        public static double QuadraticInterpolation(Point x, Point y, Point z)
        {
            var x1_x2_y0 = (y.X - z.X) * x.Y;
            var x2_x0_y1 = (z.X - x.X) * y.Y;
            var x0_x1_y2 = (x.X - y.X) * z.Y;
            var x1_x2 = y.X + z.X;
            var x2_x0 = z.X + x.X;
            var x0_x1 = x.X + y.X;

            return (x1_x2 * x1_x2_y0 + x2_x0 * x2_x0_y1 + x0_x1 * x0_x1_y2) / (x1_x2_y0 + x2_x0_y1 + x0_x1_y2) / 2.0;

        }
        #endregion  2 次補間
    }
}
