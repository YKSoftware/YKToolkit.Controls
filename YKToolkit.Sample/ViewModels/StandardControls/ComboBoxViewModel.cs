namespace YKToolkit.Sample.ViewModels
{
    using System.Linq;
    using YKToolkit.Sample.Models;

    public class ComboBoxViewModel : ViewModelBase
    {
        #region ComboBox 用サンプルデータ
        private People _people;
        /// <summary>
        /// 人物データコレクションを取得または設定します。
        /// </summary>
        public People People
        {
            get
            {
                if (_people == null)
                {
                    _people = new People(Enumerable
                        .Range(0, 10)
                        .Select(i => new Person()
                        {
                            Name = "田中 " + i + "太郎",
                            Age = 20 - i,
                            Gender = i % 2 == 0 ? Gender.Male : Gender.Female,
                        }));
                }
                return _people;
            }
            set { SetProperty(ref _people, value); }
        }
        #endregion ComboBox 用サンプルデータ
    }
}
