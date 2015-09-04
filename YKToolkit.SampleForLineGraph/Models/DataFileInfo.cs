namespace YKToolkit.SampleForLineGraph.Models
{
    using YKToolkit.Bindings;

    public class DataFileInfo : NotificationObject
    {
        private string _name;
        /// <summary>
        /// 名前を取得または設定します。
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { SetProperty(ref this._name, value); }
        }

        private string _fullPath;
        /// <summary>
        /// フルパスを取得または設定します。
        /// </summary>
        public string FullPath
        {
            get { return this._fullPath; }
            set { SetProperty(ref this._fullPath, value); }
        }

        private bool _isSelected;
        /// <summary>
        /// 選択されているかどうかを取得または設定します。
        /// </summary>
        public bool IsSelected
        {
            get { return this._isSelected; }
            set { SetProperty(ref this._isSelected, value); }
        }
    }
}
