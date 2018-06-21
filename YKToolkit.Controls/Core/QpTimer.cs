namespace YKToolkit.Controls.Core
{
    using System;
    using System.Runtime.InteropServices;

    /// <summary>
    /// QueryPerformanceCounter による時間計測タイマーを表します。
    /// </summary>
    public class QpTimer
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);
        [DllImport("Kernel32.DLL")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        /// <summary>
        /// 静的なコンストラクタ
        /// </summary>
        static QpTimer()
        {
            if (!QueryPerformanceFrequency(out _frequency))
                throw new Exception("高解像力パフォーマンスカウンタがサポートされていません。");
        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public QpTimer()
        {
            Start();
        }

        /// <summary>
        /// 時間計測を開始します。
        /// </summary>
        public void Start()
        {
            QueryPerformanceCounter(out this._start);
        }

        private static long _frequency;
        /// <summary>
        /// 1 秒あたりのカウント数を取得します。
        /// </summary>
        public static long Frequency
        {
            get { return _frequency; }
            private set { _frequency = value; }
        }

        /// <summary>
        /// 経過時間をミリ秒単位で取得します。
        /// </summary>
        public double MilliSeconds
        {
            get
            {
                var end = 0L;
                QueryPerformanceCounter(out end);
                return (end - this._start) * 1000.0 / Frequency;
            }
        }

        /// <summary>
        /// 経過時間を秒単位で取得します。
        /// </summary>
        public double Seconds
        {
            get
            {
                return this.MilliSeconds / 1000.0;
            }
        }

        /// <summary>
        /// 時間計測開始時のカウント数
        /// </summary>
        private long _start;
    }
}
