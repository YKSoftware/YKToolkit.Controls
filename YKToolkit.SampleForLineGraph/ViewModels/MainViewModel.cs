namespace YKToolkit.SampleForLineGraph.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private LineGraphViewModel _linegraphViewModel = new LineGraphViewModel();
        /// <summary>
        /// LineGraph 用の ViewModel を取得します。
        /// </summary>
        public LineGraphViewModel LineGraphViewModel
        {
            get { return this._linegraphViewModel; }
            private set { SetProperty(ref this._linegraphViewModel, value); }
        }
    }
}
