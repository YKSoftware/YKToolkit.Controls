namespace System.Net.Sockets
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 拡張メソッドを提供します。
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// ビットデバイスのデバイスコード一覧
        /// </summary>
        private static readonly IReadOnlyCollection<byte> BitDeviceTypes = Array.AsReadOnly(Enum.GetValues(typeof(McBitDeviceType)).Cast<byte>().ToArray());

        /// <summary>
        /// ビットデバイスかどうかを確認します。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsBitDevice(this McWordDeviceType type)
        {
            return BitDeviceTypes.Any(x => (byte)x == (byte)type);
        }

        /// <summary>
        /// <c>byte</c> の各ビットを評価して <c>bool</c> 配列に変換します。
        /// </summary>
        /// <param name="b">変換する <c>byte</c> の値を指定します。</param>
        /// <returns>各ビットで 1 のものを <c>true</c> として <c>bool</c> 配列を返します。</returns>
        public static bool[] ToBooleans(this byte b)
        {
            return new bool[] { (b & 0x01) != 0, (b & 0x02) != 0, (b & 0x04) != 0, (b & 0x08) != 0, (b & 0x10) != 0, (b & 0x20) != 0, (b & 0x40) != 0, (b & 0x80) != 0 };
        }

        /// <summary>
        /// <c>byte</c> の各ビットを評価して <c>int</c> 配列に変換します。
        /// </summary>
        /// <param name="b">変換する <c>byte</c> の値を指定します。</param>
        /// <returns>各ビットで 1 のものを 1 として <c>int</c> 配列を返します。</returns>
        public static int[] ToInts(this byte b)
        {
            return new int[] { (b & 0x01), ((b >> 1) & 0x01), ((b >> 2) & 0x01), ((b >> 3) & 0x01), ((b >> 4) & 0x01), ((b >> 5) & 0x01), ((b >> 6) & 0x01), ((b >> 7) & 0x01) };
        }

        /// <summary>
        /// シーケンスを指定個数毎に分割します。
        /// </summary>
        /// <typeparam name="T">シーケンスの各要素の型を表します。</typeparam>
        /// <param name="source">元のシーケンスを表します。</param>
        /// <param name="size">分割個数を指定します。</param>
        /// <returns>指定個数で分割されたシーケンスを返します。</returns>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int size)
        {
            if (source == null) throw new ArgumentException("self");
            if (size < 1) throw new ArgumentException("size");

            using (var enumerator = source.GetEnumerator())
            {
                var list = new List<T>(size);
                while (enumerator.MoveNext())
                {
                    list.Add(enumerator.Current);
                    if (list.Count >= size)
                    {
                        yield return list;
                        list = new List<T>();
                    }
                }

                // 残りの部分
                if (list.Any())
                {
                    yield return list;
                }
            }
        }

        /// <summary>
        /// 指定された整数の倍数に丸めます。
        /// </summary>
        /// <param name="x">被演算子の整数を指定します。</param>
        /// <param name="a">丸める倍数の基数を指定します。</param>
        /// <returns>0 を含めた倍数を返します。</returns>
        public static int MRound(this int x, int a)
        {
            return (x / a) * a;
        }
    }
}
