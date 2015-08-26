namespace YKToolkit.Sample.ViewModels
{
    using System.Linq;
    using YKToolkit.Sample.Models;

    public class DataGridViewModel : ViewModelBase
    {
        private People _people;
        /// <summary>
        /// 人物データを取得します。
        /// </summary>
        public People People
        {
            get
            {
                if (_people == null)
                {
                    _people = new People(Enumerable
                        .Range(0, 100000)
                        .Select(i =>
                            {
                                var person = new Person()
                                {
                                    ID = i,
                                    Name = "田中 " + i + "太郎",
                                    Age = i % 90,
                                    Gender = i % 3 != 0 ? Gender.Male : Gender.Female,
                                    Height = 100.0 + 2.34 * (i % 23),
                                    Weight = 30.0 + 3.14 * (i % 27),
                                };
                                person.IsValid = ((person.Gender == Gender.Male) && (person.Weight > 50.0))
                                                 || ((person.Gender == Gender.Female) && (person.Weight < 40.0));
                                return person;
                            }));
                }
                return _people;
            }
        }
    }
}
