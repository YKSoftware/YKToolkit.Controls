namespace YKToolkit.Sample.ViewModels
{
    using System;
    using YKToolkit.Bindings;
    using YKToolkit.SampleForLineGraph.Models;

    public class LineGraphViewModel : ViewModelBase
    {
        private LineGraphSubViewModel _lineGraphSubViewModel = new LineGraphSubViewModel();
        /// <summary>
        /// LineGraph 用の ViewModel を取得します。
        /// </summary>
        public LineGraphSubViewModel LineGraphSubViewModel
        {
            get { return this._lineGraphSubViewModel; }
            private set { SetProperty(ref this._lineGraphSubViewModel, value); }
        }

        /// <summary>
        /// 設定ファイルのフルパス
        /// </summary>
        private const string _configFileName = @"C:\Users\yu-katou.ISPNET\Desktop\test.xml";

        #region グラフ状態の保存/読込
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
                    this.LineGraphSubViewModel = new LineGraphSubViewModel();
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
                    this.LineGraphSubViewModel.Serialize(_configFileName);
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
                    this.LineGraphSubViewModel = _configFileName.Deserialize() as LineGraphSubViewModel;
                }));
            }
        }
        #endregion グラフ状態の保存/読込

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
