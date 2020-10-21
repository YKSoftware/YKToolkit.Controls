namespace YKToolkit.Controls
{
    using YKToolkit.Controls;

    /// <summary>
    /// LinegraphMenu.xaml の相互作用ロジック
    /// </summary>
    public partial class LinegraphMenu : Window
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public LinegraphMenu()
        {
            InitializeComponent();

            this.Closing += (_, e) => e.Cancel = this.HasClosed;
            this.Closed += (_, __) => this.HasClosed = true;
        }

        /// <summary>
        /// 既に閉じているかどうかを取得します。
        /// </summary>
        public bool HasClosed { get; private set; }
    }
}
