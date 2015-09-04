namespace YKToolkit.SampleForLineGraph.ViewModels
{
    using System;

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

        /// <summary>
        /// ファイルドロップ時のコールバックを取得します。
        /// </summary>
        public Action<string[]> FileDropCallback
        {
            get { return this.OnFileDrop; }
        }

        /// <summary>
        /// ファイルドロップ時のコールバック処理
        /// </summary>
        /// <param name="pathArray">フルパスの配列</param>
        private void OnFileDrop(string[] pathArray)
        {
            foreach (var path in pathArray)
            {
                System.Diagnostics.Debug.WriteLine(path);
            }
        }
    }
}
