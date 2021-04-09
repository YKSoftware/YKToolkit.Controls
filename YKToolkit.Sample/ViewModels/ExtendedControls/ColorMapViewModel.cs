namespace YKToolkit.Sample.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ColorMapViewModel : ViewModelBase
    {
        public XYZ[] Data { get; } = Enumerable.Range(0, 101).SelectMany(x => Enumerable.Range(0, 101 - x).Select(y => new XYZ()
        {
            X = (double)(10 * x),
            Y = (double)(10 * y),
            Z = (double)_rand.Next(0, 501),
        })).ToArray();

        public IEnumerable<double> XData { get { return this.Data.Select(x => x.X); } }
        public IEnumerable<double> YData { get { return this.Data.Select(x => x.Y); } }
        public IEnumerable<double> ZData { get { return this.Data.Select(x => x.Z); } }

        private static readonly Random _rand = new Random();

        public struct XYZ
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
        }
    }
}
