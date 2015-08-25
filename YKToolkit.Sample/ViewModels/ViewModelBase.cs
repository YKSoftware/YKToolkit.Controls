namespace YKToolkit.Sample.ViewModels
{
    using YKToolkit.Bindings;

    public class ViewModelBase : NotificationObject
    {
        #region ウィンドウ生成コマンド
        private DelegateCommand createWindowCommand;
        /// <summary>
        /// ウィンドウ生成コマンドを取得します。
        /// </summary>
        public DelegateCommand CreateWindowCommand
        {
            get
            {
                if (createWindowCommand == null)
                    createWindowCommand = new DelegateCommand(p =>
                    {
                        App.Instance.ShowWindow(p as string);
                    });
                return createWindowCommand;
            }
        }
        #endregion ウィンドウ生成コマンド

        #region ダイアログ生成コマンド
        private DelegateCommand createDialogCommand;
        /// <summary>
        /// ダイアログ生成コマンドを取得します。
        /// </summary>
        public DelegateCommand CreateDialogCommand
        {
            get
            {
                if (createDialogCommand == null)
                    createDialogCommand = new DelegateCommand(p =>
                    {
                        App.Instance.ShowDialog(p as string);
                    });
                return createDialogCommand;
            }
        }
        #endregion ダイアログ生成コマンド

        #region 閉じるコマンド
        private DelegateCommand closeWindowCommand;
        /// <summary>
        /// ウィンドウを閉じるコマンドを取得します。
        /// </summary>
        public DelegateCommand CloseWindowCommand
        {
            get
            {
                if (closeWindowCommand == null)
                    closeWindowCommand = new DelegateCommand(_ =>
                    {
                        App.Instance.CloseWindow(this.GetType());
                    });
                return closeWindowCommand;
            }
        }
        #endregion 閉じるコマンド
    }
}
