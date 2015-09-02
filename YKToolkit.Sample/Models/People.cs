namespace YKToolkit.Sample.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// 人物データのコレクションを表します。
    /// </summary>
    public class People : ObservableCollection<Person>
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public People()
        {
        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="collection">コレクションの初期値を指定します。</param>
        public People(IEnumerable<Person> collection)
            : base(collection)
        {
        }
    }
}
