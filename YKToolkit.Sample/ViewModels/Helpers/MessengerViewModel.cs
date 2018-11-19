namespace YKToolkit.Sample.ViewModels
{
    using System.Windows;
    using YKToolkit.Bindings;
    using YKToolkit.Controls;

    internal class MessengerViewModel : ViewModelBase
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public MessengerViewModel()
        {
            this._dialogMessage = new DialogMessage("MessageTest", obj =>
            {
                var r = (MessageBoxResult)obj;
                this.MessageResult = r.ToString();
            })
            {
                Message = "テストメッセージです。",
                Caption = "メッセンジャーテスト",
                ButtonCaptions = new string[] { "オッケー", "中止", "はい", "いいえ" },
                DialogButton = MessageBoxButton.OKCancel,
                DialogImage = MessageBoxImage.Warning,
                Location = WindowStartupLocation.CenterOwner,
            };
        }

        private string _messageResult;
        /// <summary>
        /// メッセージ結果を取得します。
        /// </summary>
        public string MessageResult
        {
            get { return this._messageResult; }
            private set { SetProperty(ref this._messageResult, value); }
        }

        private DelegateCommand _messengerTestCommand;
        public DelegateCommand MessengerTestCommand
        {
            get
            {
                return this._messengerTestCommand ?? (this._messengerTestCommand = new DelegateCommand(_ =>
                {
                    InteractionMessenger.Send(this._dialogMessage);
                }));
            }
        }

        private DialogMessage _dialogMessage;
    }
}
