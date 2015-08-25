namespace YKToolkit.Sample.Models
{
    using System;
    using System.Reflection;

    /// <summary>
    /// プロダクト情報を取得するためのクラス
    /// </summary>
    public class ProductInfo
    {
        #region Singlton クラス
        /// <summary>
        /// インスタンスを取得します。
        /// </summary>
        public static readonly ProductInfo Instance = new ProductInfo();

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        private ProductInfo()
        {
        }
        #endregion Singlton クラス

        /// <summary>
        /// 自分のアセンブリ
        /// </summary>
        private Assembly assembly = Assembly.GetExecutingAssembly();

        private string title;
        /// <summary>
        /// アプリケーションの名前を取得します。
        /// </summary>
        public string Title
        {
            get { return title ?? (title = ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyTitleAttribute))).Title); }
        }

        private string description;
        /// <summary>
        /// アプリケーションの詳細を取得します。
        /// </summary>
        public string Description
        {
            get { return description ?? (description = ((AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyDescriptionAttribute))).Description); }
        }

        private string company;
        /// <summary>
        /// アプリケーション開発元を取得します。
        /// </summary>
        public string Company
        {
            get { return company ?? (company = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCompanyAttribute))).Company); }
        }

        private string product;
        /// <summary>
        /// アプリケーションのプロダクト名を取得します。
        /// </summary>
        public string Product
        {
            get { return product ?? (product = ((AssemblyProductAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyProductAttribute))).Product); }
        }

        private string copyright;
        /// <summary>
        /// アプリケーションのコピーライトを取得します。
        /// </summary>
        public string Copyright
        {
            get { return copyright ?? (copyright = ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCopyrightAttribute))).Copyright); }
        }

        private string trademark;
        /// <summary>
        /// アプリケーションのトレードマークを取得します。
        /// </summary>
        public string Trademark
        {
            get { return trademark ?? (trademark = ((AssemblyTrademarkAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyTrademarkAttribute))).Trademark); }
        }

        private Version version;
        /// <summary>
        /// アプリケーションのバージョンを取得します。
        /// </summary>
        public Version Version
        {
            get { return version ?? (version = assembly.GetName().Version); }
        }

        private string versionString;
        /// <summary>
        /// アプリケーションのバージョン文字列を取得します。
        /// </summary>
        public string VersionString
        {
            get { return versionString ?? (versionString = string.Format("{0}{1}{2}{3}", Version.ToString(3), IsBetaMode ? " β" : "", Version.Revision == 0 ? "" : " rev." + Version.Revision, IsDebugMode ? " Debug Mode" : "")); }
        }

        /// <summary>
        /// デバッグモードかどうか確認します。
        /// </summary>
        public bool IsDebugMode
        {
#if DEBUG
            get { return true; }
#else
            get { return false; }
#endif
        }

        /// <summary>
        /// ベータ版かどうか確認します。
        /// </summary>
        public bool IsBetaMode
        {
#if BETA
            get { return true; }
#else
            get { return false; }
#endif
        }
    }
}
