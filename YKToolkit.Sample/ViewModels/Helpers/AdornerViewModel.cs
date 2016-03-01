namespace YKToolkit.Sample.ViewModels
{
    using YKToolkit.Bindings;
    using YKToolkit.Sample.Models;

    public class AdornerViewModel : ViewModelBase
    {
        private AdornedSampleModel[] _templates;
        public AdornedSampleModel[] Templates
        {
            get
            {
                if (this._templates == null)
                {
                    this._templates = new AdornedSampleModel[]
                    {
                        new AdornedSampleModel() { Name = "template1" },
                        new AdornedSampleModel() { Name = "template2" },
                    };
                }
                return this._templates;
            }
        }
    }
}
