namespace YKToolkit.Sample.ViewModels
{
    using YKToolkit.Bindings;

    public class SplitButtonViewModel : ViewModelBase
    {
        private string _innerText = "内側のテキスト";
        /// <summary>
        /// SplitButton コントロールの内側にあるテキストとバインディングしています。
        /// </summary>
        public string InnerText
        {
            get { return _innerText; }
            set { SetProperty(ref _innerText, value); }
        }

        private string _outerText = "外側のテキスト";
        /// <summary>
        /// SplitButton コントロールの外側にあるテキストとバインディングしています。
        /// </summary>
        public string OuterText
        {
            get { return _outerText; }
            set { SetProperty(ref _outerText, value); }
        }

        private DelegateCommand _splitButtonCommand;
        /// <summary>
        /// 内側のテキストを外側のテキストへコピーするコマンドを取得します。
        /// </summary>
        public DelegateCommand SplitButtonCommand
        {
            get
            {
                return _splitButtonCommand ?? (_splitButtonCommand = new DelegateCommand(
                _ =>
                {
                    OuterText = InnerText;
                },
                _ => !string.IsNullOrWhiteSpace(InnerText)));
            }
        }
    }
}
