namespace YKToolkit.Helpers
{
    public static partial class Extensions
    {
        /// <summary>
        /// 指定された数値の倍数に丸めます。
        /// </summary>
        /// <param name="value">丸められる値を表します。</param>
        /// <param name="r">基数を指定します。</param>
        /// <returns>丸められた結果を返します。</returns>
        public static double MRound(this double value, double r)
        {
            return (int)(value / r) * r;
        }
    }
}
