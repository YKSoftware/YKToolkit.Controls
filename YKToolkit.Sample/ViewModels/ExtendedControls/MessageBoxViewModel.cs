namespace YKToolkit.Sample.ViewModels
{
    using System.Windows;
    using YKToolkit.Bindings;
    using YKToolkit.Sample.Models;

    public class MessageBoxViewModel : ViewModelBase
    {
        private string _message = "ダイアログに表示するメッセージです。";
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private string _title = "ダイアログのキャプションです。";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _messageBoxButtonString;
        public string MessageBoxButtonString
        {
            get { return _messageBoxButtonString; }
            set
            {
                if (SetProperty(ref _messageBoxButtonString, value))
                {
                    switch (_messageBoxButtonString)
                    {
                        default:
                        case "OK": this._messageBoxButton = MessageBoxButton.OK; break;
                        case "OKCancel": this._messageBoxButton = MessageBoxButton.OKCancel; break;
                        case "YesNo": this._messageBoxButton = MessageBoxButton.YesNo; break;
                        case "YesNoCancel": this._messageBoxButton = MessageBoxButton.YesNoCancel; break;
                    }
                }
            }
        }

        private string _messageBoxImageString;
        public string MessageBoxImageString
        {
            get { return _messageBoxImageString; }
            set
            {
                if (SetProperty(ref _messageBoxImageString, value))
                {
                    switch (_messageBoxImageString)
                    {
                        default:
                        case "None": this._messageBoxImage = MessageBoxImage.None; break;
                        case "Error": this._messageBoxImage = MessageBoxImage.Error; break;
                        case "Question": this._messageBoxImage = MessageBoxImage.Question; break;
                        case "Warning": this._messageBoxImage = MessageBoxImage.Warning; break;
                        case "Information": this._messageBoxImage = MessageBoxImage.Information; break;
                    }
                }
            }
        }

        private string _okButtonCaption = "OK";
        public string OkButtonCaption
        {
            get { return this._okButtonCaption; }
            set { SetProperty(ref this._okButtonCaption, value); }
        }

        private string _cancelButtonCaption = "Cancel";
        public string CancelButtonCaption
        {
            get { return this._cancelButtonCaption; }
            set { SetProperty(ref this._cancelButtonCaption, value); }
        }

        private string _yesButtonCaption = "Yes";
        public string YesButtonCaption
        {
            get { return this._yesButtonCaption; }
            set { SetProperty(ref this._yesButtonCaption, value); }
        }

        private string _noButtonCaption = "No";
        public string NoButtonCaption
        {
            get { return this._noButtonCaption; }
            set { SetProperty(ref this._noButtonCaption, value); }
        }

        private string _result;
        public string Result
        {
            get { return _result; }
            set { SetProperty(ref _result, value); }
        }

        private MessageBoxButton _messageBoxButton;
        private MessageBoxImage _messageBoxImage;

        private DialogInfo _dialogInfo;
        public DialogInfo DialogInfo
        {
            get { return _dialogInfo; }
            set { SetProperty(ref _dialogInfo, value); }
        }

        private void OnDialog(MessageBoxResult result)
        {
            switch (result)
            {
                case MessageBoxResult.None: break;
                case MessageBoxResult.OK: this.Result = "OK になりました。"; break;
                case MessageBoxResult.Cancel: this.Result = "Cancel になりました。"; break;
                case MessageBoxResult.Yes: this.Result = "Yes になりました。"; break;
                case MessageBoxResult.No: this.Result = "No になりました。"; break;
            }
            DialogInfo = null;
        }

        private DelegateCommand _showDialogCommand;
        public DelegateCommand ShowDialogCommand
        {
            get
            {
                return _showDialogCommand ?? (_showDialogCommand = new DelegateCommand(_ =>
                {
                    DialogInfo = new DialogInfo()
                    {
                        Message = this.Message,
                        Title = this.Title,
                        MessageBoxButton = this._messageBoxButton,
                        MessageBoxImage = this._messageBoxImage,
                        Callback = OnDialog,
                        OkButtonCaption = this.OkButtonCaption,
                        CancelButtonCaption = this.CancelButtonCaption,
                        YesButtonCaption = this.YesButtonCaption,
                        NoButtonCaption = this.NoButtonCaption,
                    };
                }));
            }
        }
    }
}
