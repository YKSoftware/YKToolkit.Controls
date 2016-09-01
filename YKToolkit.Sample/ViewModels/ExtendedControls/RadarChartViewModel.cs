namespace YKToolkit.Sample.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using YKToolkit.Bindings;

    public class RadarChartViewModel : ViewModelBase
    {
        public RadarChartViewModel()
        {
            this.Number = 7;
        }

        private double _graphMargin = 25.0;
        public double GraphMargin
        {
            get { return this._graphMargin; }
            set { SetProperty(ref this._graphMargin, value); }
        }

        private int _number;
        public int Number
        {
            get { return this._number; }
            set
            {
                if (SetProperty(ref this._number, value))
                {
                    RaisePropertyChanged("Labels");
                    RaisePropertyChanged("Values");
                    RaisePropertyChanged("Values2");
                }
            }
        }

        public IEnumerable<string> Labels { get { return Enumerable.Range(1, this.Number).Select(x => "項目" + x); } }

        private Random _rnd = new Random();

        public IEnumerable<double> Values
        {
            get
            {
                return Enumerable.Range(0, this.Number).Select(x => this._rnd.NextDouble()).ToArray();
            }
        }

        public IEnumerable<double> Values2
        {
            get
            {
                return Enumerable.Range(0, this.Number).Select(x => this._rnd.NextDouble()).ToArray();
            }
        }

        private DelegateCommand _randomDataCommand;
        public DelegateCommand RandomDataCommand
        {
            get
            {
                return this._randomDataCommand ?? (this._randomDataCommand = new DelegateCommand(_ =>
                {
                    var temp = this.Number;
                    this.Number = 0;
                    this.Number = temp;
                }));
            }
        }
    }
}
