namespace YKToolkit.Sample.ViewModels
{
    using System.Windows.Media;

    public class BusyIndicatorViewModel : ViewModelBase
    {
        private bool _isDropDownOpen;
        public bool IsDropDownOpen
        {
            get { return _isDropDownOpen; }
            set { SetProperty(ref _isDropDownOpen, value); }
        }

        private Color _selectedColor = Colors.Red;
        public Color SelectedColor
        {
            get { return _selectedColor; }
            set
            {
                if (SetProperty(ref _selectedColor, value))
                {
                    this.IsDropDownOpen = false;
                }
            }
        }
    }
}
