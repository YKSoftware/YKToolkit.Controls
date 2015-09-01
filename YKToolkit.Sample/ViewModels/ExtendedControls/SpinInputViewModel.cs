namespace YKToolkit.Sample.ViewModels
{
    public class SpinInputViewModel : ViewModelBase
    {
        private double _value = 1000.0;
        /// <summary>
        /// 数値を取得または設定します。
        /// </summary>
        public double Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }

        private string _stringFormat = "#,0.0 [m]";
        /// <summary>
        /// 数値の表示形式を取得または設定します。
        /// </summary>
        public string StringFormat
        {
            get { return _stringFormat; }
            set { SetProperty(ref _stringFormat, value); }
        }

        private double _minimum = -10000.0;
        /// <summary>
        /// 最小値を取得または設定します。
        /// </summary>
        public double Minimum
        {
            get { return _minimum; }
            set { SetProperty(ref _minimum, value); }
        }

        private double _maximum = 10000.0;
        /// <summary>
        /// 最大値を取得または設定します。
        /// </summary>
        public double Maximum
        {
            get { return _maximum; }
            set { SetProperty(ref _maximum, value); }
        }

        private double _tick = 100.0;
        /// <summary>
        /// 値の増減値を取得または設定します。
        /// </summary>
        public double Tick
        {
            get { return _tick; }
            set { SetProperty(ref _tick, value); }
        }

        private int _delay = 200;
        /// <summary>
        /// 繰り返し始めるまでの遅延時間 [ms] を取得または設定します。
        /// </summary>
        public int Delay
        {
            get { return _delay; }
            set { SetProperty(ref _delay, value); }
        }

        private int _interval = 50;
        /// <summary>
        /// 繰り返す間隔 [ms] を取得または設定します。
        /// </summary>
        public int Interval
        {
            get { return _interval; }
            set { SetProperty(ref _interval, value); }
        }
    }
}
