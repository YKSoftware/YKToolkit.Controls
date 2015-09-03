namespace YKToolkit.SampleForLineGraph
{
    using System.Windows;
    using YKToolkit.SampleForLineGraph.ViewModels;
    using YKToolkit.SampleForLineGraph.Views;

    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var w = new MainView();
            var vm = new MainViewModel();

            w.DataContext = vm;
            w.Show();
        }
    }
}
