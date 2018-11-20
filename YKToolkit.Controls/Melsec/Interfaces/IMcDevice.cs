namespace System.Net.Sockets
{
    using System.Collections.Generic;

    /// <summary>
    /// PLC のデバイスを表すためのインターフェースを表します。
    /// </summary>
    public interface IMcDevice
    {
        /// <summary>
        /// デバイスのアドレスを取得します。
        /// </summary>
        int Address { get; }

        /// <summary>
        /// プロトコル上のバイト配列を取得します。
        /// </summary>
        byte[] Bytes { get; }

        /// <summary>
        /// デバイス名を取得します。
        /// </summary>
        string DeviceName { get; }

        /// <summary>
        /// アドレスを含めたデバイス名を取得します。
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// PLC のビットデバイスを表すためのインターフェースを表します。
    /// </summary>
    public interface IMcBitDevice : IMcDevice
    {
        /// <summary>
        /// PLC デバイスの種類を取得します。
        /// </summary>
        McBitDeviceType DeviceType { get; }

        /// <summary>
        /// デバイスの値が更新されたときの経過時間 [ms] を取得または設定します。
        /// </summary>
        double ElapsedMilliseconds { get; set; }

        /// <summary>
        /// デバイスの値を取得します。
        /// </summary>
        bool Value { get; set; }
    }

    /// <summary>
    /// PLC のワードデバイスを表すためのインターフェースを表します。
    /// </summary>
    public interface IMcWordDevice : IMcDevice
    {
        /// <summary>
        /// PLC デバイスの種類を取得します。
        /// </summary>
        McWordDeviceType DeviceType { get; }

        /// <summary>
        /// デバイスの値を取得します。
        /// </summary>
        short Value { get; set; }
    }

    /// <summary>
    /// PLC のダブルワードデバイスを表すためのインターフェースを表します。
    /// </summary>
    public interface IMcDoubleWordDevice : IMcDevice
    {
        /// <summary>
        /// PLC デバイスの種類を取得します。
        /// </summary>
        McWordDeviceType DeviceType { get; }

        /// <summary>
        /// デバイスの値を取得します。
        /// </summary>
        Int32 Value { get; set; }
    }

    /// <summary>
    /// IMcDevice インターフェースに対する比較演算子を提供します。
    /// </summary>
    internal class IMcDeviceComparer : IComparer<IMcDevice>
    {
        /// <summary>
        /// IMcDevice.DeviceName を優先し、IMcDevice.Address で昇順に並べ替える比較演算子を提供します。
        /// </summary>
        public static readonly IMcDeviceComparer DefaultComparer = new IMcDeviceComparer();

        /// <summary>
        /// 大小比較をおこないます。
        /// </summary>
        /// <param name="x">比較されるオブジェクトを指定します。</param>
        /// <param name="y">比較するオブジェクトを指定します。</param>
        /// <returns>比較結果を返します。</returns>
        public int Compare(IMcDevice x, IMcDevice y)
        {
            if (x.DeviceName == y.DeviceName)
                return x.Address - y.Address;

            return x.DeviceName.GetHashCode() - y.DeviceName.GetHashCode();
        }
    }
}
