namespace YKToolkit.Helpers
{
    /// <summary>
    /// 複素数を扱う構造体を表します。
    /// </summary>
    public struct Complex
    {
        #region コンストラクタ
        /// <summary>
        /// 新しいインスタンスを作成します。
        /// </summary>
        /// <param name="real">実部</param>
        public Complex(double real)
            : this(real, 0)
        {
        }

        /// <summary>
        /// 新しいインスタンスを作成します。
        /// </summary>
        /// <param name="real">実部</param>
        /// <param name="imaginary">虚部</param>
        public Complex(double real, double imaginary)
        {
            this.real = real;
            this.imaginary = imaginary;
        }
        #endregion  コンストラクタ

        /// <summary>
        /// クローンを生成します。
        /// </summary>
        /// <returns>生成されたクローン</returns>
        public Complex Clone()
        {
            return (Complex)MemberwiseClone();
        }

        #region 公開プロパティ
        private double real;
        /// <summary>
        /// 実部を取得または設定します。
        /// </summary>
        public double Real
        {
            get { return real; }
            set { real = value; }
        }

        private double imaginary;
        /// <summary>
        /// 虚部を取得または設定します。
        /// </summary>
        public double Imaginary
        {
            get { return imaginary; }
            set { imaginary = value; }
        }

        /// <summary>
        /// 絶対値を取得します。
        /// </summary>
        public double Abs
        {
            get { return System.Math.Sqrt(Real * Real + Imaginary * Imaginary); }
        }

        /// <summary>
        /// 絶対値をデシベル単位で取得します。
        /// </summary>
        public double Decibel
        {
            get { return 20.0 * System.Math.Log10(Abs); }
        }
        #endregion  公開プロパティ

        #region オーバーロード演算子
        /// <summary>
        /// <c>Complex</c> 構造体と <c>double</c> 型の + 演算子を定義します。
        /// </summary>
        /// <param name="x">足される値</param>
        /// <param name="y">足す値</param>
        /// <returns>演算結果</returns>
        public static Complex operator +(Complex x, double y)
        {
            return new Complex(x.Real + y, x.Imaginary);
        }

        /// <summary>
        /// <c>Complex</c> 構造体と <c>double</c> 型の + 演算子を定義します。
        /// </summary>
        /// <param name="x">足される値</param>
        /// <param name="y">足す値</param>
        /// <returns>演算結果</returns>
        public static Complex operator +(double x, Complex y)
        {
            return new Complex(x + y.Real, y.Imaginary);
        }

        /// <summary>
        /// <c>Complex</c> 構造体と <c>Complex</c> 構造体の + 演算子を定義します。
        /// </summary>
        /// <param name="x">足される値</param>
        /// <param name="y">足す値</param>
        /// <returns>演算結果</returns>
        public static Complex operator +(Complex x, Complex y)
        {
            return new Complex(x.Real + y.Real, x.Imaginary + y.Imaginary);
        }

        /// <summary>
        /// <c>Complex</c> 構造体と <c>double</c> 型の - 演算子を定義します。
        /// </summary>
        /// <param name="x">引かれる値</param>
        /// <param name="y">引く値</param>
        /// <returns>演算結果</returns>
        public static Complex operator -(Complex x, double y)
        {
            return new Complex(x.Real - y, x.Imaginary);
        }

        /// <summary>
        /// <c>Complex</c> 構造体と <c>double</c> 型の - 演算子を定義します。
        /// </summary>
        /// <param name="x">引かれる値</param>
        /// <param name="y">引く値</param>
        /// <returns>演算結果</returns>
        public static Complex operator -(double x, Complex y)
        {
            return new Complex(x - y.Real, -y.Imaginary);
        }

        /// <summary>
        /// <c>Complex</c> 構造体と <c>Complex</c> 構造体の - 演算子を定義します。
        /// </summary>
        /// <param name="x">引かれる値</param>
        /// <param name="y">引く値</param>
        /// <returns>演算結果</returns>
        public static Complex operator -(Complex x, Complex y)
        {
            return new Complex(x.Real - y.Real, x.Imaginary - y.Imaginary);
        }

        /// <summary>
        /// <c>Complex</c> 構造体と <c>double</c> 型の * 演算子を定義します。
        /// </summary>
        /// <param name="x">掛けられる値</param>
        /// <param name="y">掛ける値</param>
        /// <returns>演算結果</returns>
        public static Complex operator *(Complex x, double y)
        {
            return new Complex(y * x.Real, y * x.Imaginary);
        }

        /// <summary>
        /// <c>Complex</c> 構造体と <c>double</c> 型の * 演算子を定義します。
        /// </summary>
        /// <param name="x">掛けられる値</param>
        /// <param name="y">掛ける値</param>
        /// <returns>演算結果</returns>
        public static Complex operator *(double x, Complex y)
        {
            return new Complex(x * y.Real, x * y.Imaginary);
        }

        /// <summary>
        /// <c>Complex</c> 構造体と <c>Complex</c> 構造体の * 演算子を定義します。
        /// </summary>
        /// <param name="x">掛けられる値</param>
        /// <param name="y">掛ける値</param>
        /// <returns>演算結果</returns>
        public static Complex operator *(Complex x, Complex y)
        {
            return new Complex(x.Real * y.Real - x.Imaginary * y.Imaginary, x.Imaginary * y.Real + x.Real * y.Imaginary);
        }

        /// <summary>
        /// <c>Complex</c> 構造体と <c>double</c> 型の / 演算子を定義します。
        /// </summary>
        /// <param name="x">割られる値</param>
        /// <param name="y">割る値</param>
        /// <returns>演算結果</returns>
        public static Complex operator /(Complex x, double y)
        {
            return new Complex(x.Real / y, x.Imaginary / y);
        }

        /// <summary>
        /// <c>Complex</c> 構造体と <c>double</c> 型の / 演算子を定義します。
        /// </summary>
        /// <param name="x">割られる値</param>
        /// <param name="y">割る値</param>
        /// <returns>演算結果</returns>
        public static Complex operator /(double x, Complex y)
        {
            var temp = new Complex(x, 0);
            return temp / y;
        }

        /// <summary>
        /// <c>Complex</c> 構造体と <c>Complex</c> 構造体の / 演算子を定義します。
        /// </summary>
        /// <param name="x">割られる値</param>
        /// <param name="y">割る値</param>
        /// <returns>演算結果</returns>
        public static Complex operator /(Complex x, Complex y)
        {
            var yy = y.Clone();
            yy.Imaginary *= -1.0;
            var ret = x * yy;
            var temp = y.Real * y.Real + y.Imaginary * y.Imaginary;
            ret.Real /= temp;
            ret.Imaginary /= temp;

            return ret;
        }
        #endregion  オーバーロード演算子

        /// <summary>
        /// Complex 構造体を文字列として表現します。
        /// </summary>
        /// <returns>変換された文字列</returns>
        public override string ToString()
        {
            //return string.Format("Real={0}, Imaginary={1}, ABS={2}", Real.ToString("f4").PadLeft(10), Imaginary.ToString("f4").PadLeft(10), Abs.ToString("f4").PadLeft(10));
            return string.Format("({0},{1})", Real, Imaginary);
        }
    }
}
