namespace YKToolkit.Sample.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// ViewModel に関する情報を表します。
    /// </summary>
    public class ViewModelInfo
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public ViewModelInfo()
        {
        }

        public string Name { get; set; }
        public object Instance { get; set; }
        public List<ViewModelInfo> Children { get; set; }
        public bool IsExpanded { get; set; }
    }
}
