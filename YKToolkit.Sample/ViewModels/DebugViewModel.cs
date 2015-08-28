namespace YKToolkit.Sample.ViewModels
{
    using YKToolkit.Bindings;

    public class DebugViewModel : ViewModelBase
    {
        private DelegateCommand _clickActionCommand;
        public DelegateCommand ClickActionCommand
        {
            get
            {
                return _clickActionCommand ?? (_clickActionCommand = new DelegateCommand(p =>
                {
                    System.Console.WriteLine(p.GetType().ToString());
                }));
            }
        }

        private DelegateCommand _dragDeltaCommand;
        public DelegateCommand DragDeltaCommand
        {
            get
            {
                return _dragDeltaCommand ?? (_dragDeltaCommand = new DelegateCommand(p =>
                {
                    var e = p as System.Windows.Controls.Primitives.DragDeltaEventArgs;
                }));
            }
        }
    }
}
