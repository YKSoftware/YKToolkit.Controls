using System;
namespace YKToolkit.Controls
{
    /// <summary>
    /// HSV 値による色情報を表します。
    /// </summary>
    public struct HsvColor
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="h">H 値を指定します。</param>
        /// <param name="s">S 値を指定します。</param>
        /// <param name="v">V 値を指定します。</param>
        public HsvColor(double h, double s, double v)
        {
            this._h = h;
            this._s = s;
            this._v = v;
        }

        /// <summary>
        /// RGB 値を指定して新しいインスタンスを生成します。
        /// </summary>
        /// <param name="r">R 値を指定します。</param>
        /// <param name="g">G 値を指定します。</param>
        /// <param name="b">B 値を指定します。</param>
        /// <param name="isExchanged">必ず true を指定して下さい。</param>
        public HsvColor(byte r, byte g, byte b, bool isExchanged)
        {
            if (!isExchanged)
                throw new ArgumentException("isExchanged");

            this = HsvColorHelper.HsvColorFromRgb(r, g, b);
        }

        private double _h;
        /// <summary>
        /// H 値を取得または設定します。
        /// </summary>
        public double H
        {
            get { return _h; }
            set { _h = value; }
        }

        private double _s;
        /// <summary>
        /// S 値を取得または設定します。
        /// </summary>
        public double S
        {
            get { return _s; }
            set { _s = value; }
        }

        private double _v;
        /// <summary>
        /// V 値を取得または設定します。
        /// </summary>
        public double V
        {
            get { return _v; }
            set { _v = value; }
        }
    }
}
