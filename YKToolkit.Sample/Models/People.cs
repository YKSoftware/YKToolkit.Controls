namespace YKToolkit.Sample.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class People : ObservableCollection<Person>
    {
        public People()
        {
        }

        public People(IEnumerable<Person> collection)
            : base(collection)
        {
        }
    }
}
