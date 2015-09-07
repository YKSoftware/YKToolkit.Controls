namespace YKToolkit.SampleForLineGraph.ViewModels
{
    using System;
    using YKToolkit.Bindings;
    using YKToolkit.SampleForLineGraph.Models;

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
        /// 設定ファイルのフルパス
        /// </summary>
        private const string _configFileName = @"C:\Users\yu-katou.ISPNET\Desktop\test.xml";

        private DelegateCommand _newCommand;
        /// <summary>
        /// 新規作成コマンドを取得します。
        /// </summary>
        public DelegateCommand NewCommand
        {
            get
            {
                return this._newCommand ?? (this._newCommand = new DelegateCommand(_ =>
                {
                    this.LineGraphViewModel = new LineGraphViewModel();
                }));
            }
        }

        private DelegateCommand _saveCommand;
        /// <summary>
        /// ファイル保存コマンドを取得します。
        /// </summary>
        public DelegateCommand SaveCommand
        {
            get
            {
                return this._saveCommand ?? (this._saveCommand = new DelegateCommand(_ =>
                {
                    this.LineGraphViewModel.Serialize(_configFileName);
                }));
            }
        }

        private DelegateCommand _loadCommand;
        /// <summary>
        /// ファイル読込コマンドを取得します。
        /// </summary>
        public DelegateCommand LoadCommand
        {
            get
            {
                return this._loadCommand ?? (this._loadCommand = new DelegateCommand(_ =>
                {
                    this.LineGraphViewModel = _configFileName.Deserialize() as LineGraphViewModel;
                }));
            }
        }

        private DelegateCommand _changeThemeCommand;
        /// <summary>
        /// テーマ切替コマンドを取得します。
        /// </summary>
        public DelegateCommand ChangeThemeCommand
        {
            get
            {
                return this._changeThemeCommand ?? (this._changeThemeCommand = new DelegateCommand(_ =>
                {
                    var manager = YKToolkit.Controls.ThemeManager.Instance;
                    manager.SetTheme(manager.CurrentTheme == "Dark" ? "Light" : "Dark");
                }));
            }
        }

        #region 画像保存
        private DelegateCommand _saveImageCommand;
        /// <summary>
        /// 画像保存コマンドを取得します。
        /// </summary>
        public DelegateCommand SaveImageCommand
        {
            get
            {
                return this._saveImageCommand ?? (this._saveImageCommand = new DelegateCommand(_ =>
                {
                    SaveImageCommandCallback = this.OnSaveImage;
                }));
            }
        }

        private Action<object, bool?> _saveImageCommandCallback;
        /// <summary>
        /// 画像保存コールバック処理を取得または設定します。
        /// </summary>
        public Action<object, bool?> SaveImageCommandCallback
        {
            get { return this._saveImageCommandCallback; }
            set { SetProperty(ref this._saveImageCommandCallback, value); }
        }

        /// <summary>
        /// 画像保存のための処理をおこないます。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="result"></param>
        private void OnSaveImage(object obj, bool? result)
        {
            SaveImageCommandCallback = null;
            var filePath = obj as string;
            if (!string.IsNullOrWhiteSpace(filePath) && result.HasValue && result.Value)
            {
                BitmapFilePath = filePath;
            }
        }

        private string _bitmapFilePath;
        /// <summary>
        /// 保存する画像のフルパスを取得または設定します。
        /// </summary>
        public string BitmapFilePath
        {
            get { return this._bitmapFilePath; }
            set { SetProperty(ref this._bitmapFilePath, value); }
        }
        #endregion 画像保存
    }
}
