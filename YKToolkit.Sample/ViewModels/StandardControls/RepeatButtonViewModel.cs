namespace YKToolkit.Sample.ViewModels
{
    using YKToolkit.Bindings;

    public class RepeatButtonViewModel : ViewModelBase
    {
        private int _count;
        /// <summary>
        /// カウント値を取得または設定します。
        /// </summary>
        public int Count
        {
            get { return _count; }
            set { SetProperty(ref _count, value); }
        }

        private DelegateCommand _increaseCommand;
        /// <summary>
        /// カウント加算コマンドを取得します。
        /// </summary>
        public DelegateCommand IncreaseCommand
        {
            get
            {
                return _increaseCommand ?? (_increaseCommand = new DelegateCommand(_ =>
                {
                    Count++;
                }));
            }
        }

        private DelegateCommand _resetCommand;
        /// <summary>
        /// カウントリセットコマンドを取得します。
        /// </summary>
        public DelegateCommand ResetCommand
        {
            get
            {
                return _resetCommand ?? (_resetCommand = new DelegateCommand(_ =>
                {
                    Count = 0;
                }));
            }
        }
    }
}
