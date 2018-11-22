namespace System.Net.Sockets
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// 拡張メソッドを提供します。
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// ビットデバイスのデバイスコード一覧
        /// </summary>
        private static readonly IReadOnlyCollection<byte> BitDeviceTypes = Array.AsReadOnly(Enum.GetValues(typeof(McBitDeviceType)).Cast<byte>().ToArray());

        /// <summary>
        /// MC プロトコルの終了コードの詳細を取得します。
        /// </summary>
        /// <param name="endCode">MC プロトコルの終了コードを指定します。</param>
        /// <returns>詳細情報を返します。</returns>
        public static string GetDescription(this McEndCode endCode)
        {
            var fieldInfo = endCode.GetType().GetField(endCode.ToString());
            var attribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;
            return attribute != null ? attribute.Description : null;
        }

        /// <summary>
        /// ビットデバイスかどうかを確認します。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool IsBitDevice(this McWordDeviceType type)
        {
            return BitDeviceTypes.Any(x => (byte)x == (byte)type);
        }

        /// <summary>
        /// <c>byte</c> の各ビットを評価して <c>bool</c> 配列に変換します。
        /// </summary>
        /// <param name="b">変換する <c>byte</c> の値を指定します。</param>
        /// <returns>各ビットで 1 のものを <c>true</c> として <c>bool</c> 配列を返します。</returns>
        internal static bool[] ToBooleans(this byte b)
        {
            return new bool[] { (b & 0x01) != 0, (b & 0x02) != 0, (b & 0x04) != 0, (b & 0x08) != 0, (b & 0x10) != 0, (b & 0x20) != 0, (b & 0x40) != 0, (b & 0x80) != 0 };
        }

        /// <summary>
        /// <c>byte</c> の各ビットを評価して <c>int</c> 配列に変換します。
        /// </summary>
        /// <param name="b">変換する <c>byte</c> の値を指定します。</param>
        /// <returns>各ビットで 1 のものを 1 として <c>int</c> 配列を返します。</returns>
        internal static int[] ToInts(this byte b)
        {
            return new int[] { (b & 0x01), ((b >> 1) & 0x01), ((b >> 2) & 0x01), ((b >> 3) & 0x01), ((b >> 4) & 0x01), ((b >> 5) & 0x01), ((b >> 6) & 0x01), ((b >> 7) & 0x01) };
        }

        /// <summary>
        /// 指定された整数の倍数に丸めます。
        /// </summary>
        /// <param name="x">被演算子の整数を指定します。</param>
        /// <param name="a">丸める倍数の基数を指定します。</param>
        /// <returns>0 を含めた倍数を返します。</returns>
        internal static int MRound(this int x, int a)
        {
            return (x / a) * a;
        }
    }
}
