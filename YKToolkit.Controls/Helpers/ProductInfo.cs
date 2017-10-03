namespace YKToolkit.Helpers
{
    using System;
    using System.Reflection;

    /// <summary>
    /// アセンブリ情報を提供します。
    /// </summary>
    public class ProductInfo
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="isCurrentAssembly">呼び出し元のアセンブリ情報をソースとする場合に true を指定します。false を指定した場合は YKToolkit.Controls.dll に関する情報がソースとなります。</param>
        /// <param name="isDebugMode">デバッグモードかどうかを指定します。</param>
        /// <param name="isBetaMode">β 版モードかどうかを指定します。</param>
        public ProductInfo(bool isCurrentAssembly = false, bool isDebugMode = false, bool isBetaMode = false)
        {
            this._assembly = isCurrentAssembly ? Assembly.GetCallingAssembly() : Assembly.GetAssembly(typeof(ProductInfo));
            this.IsDebugMode = isCurrentAssembly ? isDebugMode :
#if DEBUG
                true;
#else
                false;
#endif
            this.IsBetaMode = isCurrentAssembly ? isBetaMode :
#if BETA
                true;
#else
            false;
#endif
        }

        /// <summary>
        ///自身のアセンブリ
        /// </summary>
        private Assembly _assembly;

        /// <summary>
        /// アセンブリに対する指定した型のカスタム属性を取得します。
        /// </summary>
        /// <typeparam name="T">カスタム属性を指定します。</typeparam>
        /// <returns>指定した型のカスタム属性の値を返します。</returns>
        private T GetAttribute<T>()
            where T : Attribute
        {
#if NET4
            return Attribute.GetCustomAttribute(this._assembly, typeof(T)) as T;
#else
            return this._assembly.GetCustomAttribute(typeof(T)) as T;
#endif
        }

        /// <summary>
        /// タイトルを取得します。
        /// </summary>
        public string Title { get { return GetAttribute<AssemblyTitleAttribute>().Title; } }

        /// <summary>
        /// 説明を取得します。
        /// </summary>
        public string Description { get { return GetAttribute<AssemblyDescriptionAttribute>().Description; } }

        /// <summary>
        /// 構成情報を取得します。
        /// </summary>
        public string Configuration { get { return GetAttribute<AssemblyConfigurationAttribute>().Configuration; } }

        /// <summary>
        /// 会社名を取得します。
        /// </summary>
        public string Company { get { return GetAttribute<AssemblyCompanyAttribute>().Company; } }

        /// <summary>
        /// 製品名を取得します。
        /// </summary>
        public string ProductName { get { return GetAttribute<AssemblyProductAttribute>().Product; } }

        /// <summary>
        /// 著作権を取得します。
        /// </summary>
        public string Copyright { get { return GetAttribute<AssemblyCopyrightAttribute>().Copyright; } }

        /// <summary>
        /// 商標を取得します。
        /// </summary>
        public string Trademark { get { return GetAttribute<AssemblyTrademarkAttribute>().Trademark; } }

        /// <summary>
        /// 属性付きアセンブリがサポートするカルチャを取得します。
        /// </summary>
        public string Culture { get { return GetAttribute<AssemblyCultureAttribute>().Culture; } }

        /// <summary>
        /// バージョンを取得します。
        /// </summary>
        public Version Version { get { return _assembly.GetName().Version; } }

        private string _versionString;
        /// <summary>
        /// バージョン表記を取得します。
        /// </summary>
        public string VersionString
        {
            get
            {
                return _versionString ?? (_versionString = string.Concat(new string[]
                    {
                        this.Version.ToString(3),
                        IsBetaMode ? " β" : "",
                        this.Version.Revision != 0 ? " rev." + this.Version.Revision.ToString() : "",
                        IsDebugMode ? " Debug Mode" : "",
                    }));
            }
        }

        /// <summary>
        /// デバッグモードかどうかを確認します。
        /// </summary>
        public bool IsDebugMode { get; private set; }

        /// <summary>
        /// β 版モードかどうかを確認します。
        /// </summary>
        public bool IsBetaMode { get; private set; }
    }
}
