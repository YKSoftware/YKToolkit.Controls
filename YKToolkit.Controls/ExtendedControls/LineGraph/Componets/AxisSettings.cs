namespace YKToolkit.Controls
{
    using System.Windows;
    using YKToolkit.Bindings;

    /// <summary>
    /// 軸設定を表します。
    /// </summary>
    public class AxisSettings : NotificationObject
    {
        private string _title = "Label";
        /// <summary>
        /// 軸ラベルを取得または設定します。
        /// </summary>
        public string Title
        {
            get { return this._title; }
            set { SetProperty(ref this._title, value); }
        }

        private double _minimum = 0;
        /// <summary>
        /// 最小値を取得または設定します。
        /// </summary>
        public double Minimum
        {
            get { return this._minimum; }
            set
            {
                if (SetProperty(ref this._minimum, value))
                {
                    RaisePropertyChanged("Range");
                }
            }
        }

        private double _maximum = 1000;
        /// <summary>
        /// 最大値を取得または設定します。
        /// </summary>
        public double Maximum
        {
            get { return this._maximum; }
            set
            {
                if (SetProperty(ref this._maximum, value))
                {
                    RaisePropertyChanged("Range");
                }
            }
        }

        private double _majorStep = 100;
        /// <summary>
        /// 目盛間隔を取得または設定します。
        /// </summary>
        public double MajorStep
        {
            get { return this._majorStep; }
            set { SetProperty(ref this._majorStep, value); }
        }

        private double _minorStep = 50;
        /// <summary>
        /// 補助目盛間隔を取得または設定します。
        /// </summary>
        public double MinorStep
        {
            get { return this._minorStep; }
            set { SetProperty(ref this._minorStep, value); }
        }

        private double _majorGridThickness = 1;
        /// <summary>
        /// 目盛線幅を取得または設定します。
        /// </summary>
        public double MajorGridThickness
        {
            get { return this._majorGridThickness; }
            set { SetProperty(ref this._majorGridThickness, value); }
        }

        private double _minorGridThickness = 1;
        /// <summary>
        /// 補助目盛線幅を取得または設定します。
        /// </summary>
        public double MinorGridThickness
        {
            get { return this._minorGridThickness; }
            set { SetProperty(ref this._minorGridThickness, value); }
        }

        private bool _isMajorGridEnabled = true;
        /// <summary>
        /// 目盛線が有効かどうかを取得または設定します。
        /// </summary>
        public bool IsMajorGridEnabled
        {
            get { return this._isMajorGridEnabled; }
            set { SetProperty(ref this._isMajorGridEnabled, value); }
        }

        private bool _isMinorGridEnabled = false;
        /// <summary>
        /// 補助目盛線が有効化どうかを取得または設定します。
        /// </summary>
        public bool IsMinorGridEnabled
        {
            get { return this._isMinorGridEnabled; }
            set { SetProperty(ref this._isMinorGridEnabled, value); }
        }

        private Visibility _gridLabelVisibility = Visibility.Visible;
        /// <summary>
        /// 軸ラベルの視認性を取得または設定します。
        /// </summary>
        public Visibility GridLabelVisibility
        {
            get { return this._gridLabelVisibility; }
            set { SetProperty(ref this._gridLabelVisibility, value); }
        }

        private string _stringFormat = "#,##0.0";
        /// <summary>
        /// 軸ラベルの表示形式を取得または設定します。
        /// </summary>
        public string StringFormat
        {
            get { return this._stringFormat; }
            set { SetProperty(ref this._stringFormat, value); }
        }

        /// <summary>
        /// 軸の設定範囲を取得します。
        /// </summary>
        public double Range { get { return this.Maximum - this.Minimum; } }

        /// <summary>
        /// 目盛線の既定値
        /// </summary>
        public const int DefaultGridLines = 9;

        /// <summary>
        /// 軸設定の既定値を持つインスタンスを生成します。
        /// </summary>
        /// <returns></returns>
        public static AxisSettings CreateDefault()
        {
            return new AxisSettings();
        }

        /// <summary>
        /// オブジェクトのコピーを生成します。
        /// </summary>
        /// <returns></returns>
        public AxisSettings Clone()
        {
            return new AxisSettings()
            {
                Title = this.Title,
                Minimum = this.Minimum,
                Maximum = this.Maximum,
                MajorStep = this.MajorStep,
                MinorStep = this.MinorStep,
                MajorGridThickness = this.MajorGridThickness,
                MinorGridThickness = this.MinorGridThickness,
                IsMajorGridEnabled = this.IsMajorGridEnabled,
                IsMinorGridEnabled = this.IsMinorGridEnabled,
                GridLabelVisibility = this.GridLabelVisibility,
                StringFormat = this.StringFormat,
            };
        }
    }
}
