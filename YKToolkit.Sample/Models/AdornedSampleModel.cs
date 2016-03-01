namespace YKToolkit.Sample.Models
{
    using YKToolkit.Bindings;

    public class AdornedSampleModel : NotificationObject
    {
        private string _name;
        public string Name
        {
            get { return this._name; }
            set { SetProperty(ref this._name, value); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return this._isSelected; }
            set { SetProperty(ref this._isSelected, value); }
        }
    }
}
