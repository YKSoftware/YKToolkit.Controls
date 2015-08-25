namespace YKToolkit.Sample.ViewModels
{
    using YKToolkit.Sample.Models;

    public class VersionViewModel : ViewModelBase
    {
        /// <summary>
        /// バージョンを取得します。
        /// </summary>
        public string Version
        {
            get { return ProductInfo.Instance.VersionString; }
        }

        /// <summary>
        /// アプリケーションタイトルを取得します。
        /// </summary>
        public string Title
        {
            get { return ProductInfo.Instance.Title; }
        }

        /// <summary>
        /// プロダクト名を取得します。
        /// </summary>
        public string ProductName
        {
            get { return ProductInfo.Instance.Product; }
        }

        /// <summary>
        /// アプリケーションの説明を取得します。
        /// </summary>
        public string Description
        {
            get { return ProductInfo.Instance.Description; }
        }

        /// <summary>
        /// コピーライトを取得します。
        /// </summary>
        public string Copyright
        {
            get { return ProductInfo.Instance.Copyright; }
        }

        /// <summary>
        /// YKToolkit.Controls.dll のバージョンを取得します。
        /// </summary>
        public string YKToolkitControlsVersion
        {
            get { return YKToolkit.Controls.ThemeManager.VersionString; }
        }
    }
}
