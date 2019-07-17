namespace YKToolkit.Controls
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    /// <summary>
    /// kernel32.dll を使用するための変数またはメソッドを提供します。
    /// </summary>
    public static class Kernel32
    {
        /// <summary>
        /// GetShortPathName 関数の導入
        /// </summary>
        /// <param name="lpszlongPath"></param>
        /// <param name="lpszShortPath"></param>
        /// <param name="cchBuffer"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        private static extern uint GetShortPathName([MarshalAs(UnmanagedType.LPTStr)]string lpszlongPath, [MarshalAs(UnmanagedType.LPTStr)]StringBuilder lpszShortPath, uint cchBuffer);

        /// <summary>
        /// 短いパスを取得します。
        /// </summary>
        /// <param name="path">フルパスを指定します。</param>
        /// <returns>短いパスを返します。</returns>
        public static string GetShortPath(string path)
        {
            sb.Clear();
            if (GetShortPathName(path, sb, (uint)sb.Capacity) == 0)
                throw new Exception("短いパスの取得に失敗しました。");
            return sb.ToString();
        }

        private static StringBuilder sb = new StringBuilder(1024);
    }
}
