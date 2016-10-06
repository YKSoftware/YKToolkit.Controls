namespace YKToolkit.Controls
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using System.Windows;

    /// <summary>
    /// YKToolkit.Controls テーマ管理をおこないます。
    /// </summary>
    public class ThemeManager
    {
        #region const
        /// <summary>
        /// 名前空間名
        /// </summary>
        private const string namespaceString = "YKToolkit.Controls";

        /// <summary>
        /// テーマ名を含むタグ名
        /// </summary>
        private const string ThemeNameKey = "ThemeName";
        #endregion const

        #region Singleton
        /// <summary>
        /// インスタンスを取得します。
        /// </summary>
        public static ThemeManager Instance { get; private set; }

        /// <summary>
        /// 静的なコンストラクタ
        /// </summary>
        static ThemeManager()
        {
            Instance = new ThemeManager();
        }

        /// <summary>
        /// プライベートなコンストラクタを定義することで
        /// 外部からのインスタンス生成を防止します。
        /// </summary>
        private ThemeManager()
        {
        }
        #endregion Singleton

        #region 公開プロパティ
        /// <summary>
        /// 現在のテーマ名を取得します。
        /// </summary>
        public string CurrentTheme { get; private set; }

        /// <summary>
        /// テーマ名リストを取得します。
        /// </summary>
        public ReadOnlyCollection<string> ThemeNameList { get; private set; }

        /// <summary>
        /// バージョン番号を取得します。
        /// </summary>
        public static Version Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version; }
        }

        /// <summary>
        /// バージョン番号の文字列を取得します。
        /// </summary>
        public static string VersionString
        {
            get
            {
                var version = Version;
                return string.Format("{0}{1}{2}{3}", version.ToString(3), IsBeta ? " β" : "", version.Revision == 0 ? "" : " rev." + version.Revision, IsDebugMode ? " Debug Mode" : "");
            }
        }

        /// <summary>
        /// デバッグモードかどうかを取得します。
        /// </summary>
        public static bool IsDebugMode
        {
#if DEBUG
            get { return true; }
#else
            get { return false; }
#endif
        }

        /// <summary>
        /// ベータ版かどうかを取得します。
        /// </summary>
        public static bool IsBeta
        {
#if BETA
            get { return true; }
#else
            get { return false; }
#endif
        }
        #endregion 公開プロパティ

        #region 公開メソッド
        /// <summary>
        /// 初期化済みフラグ
        /// </summary>
        private static bool _isInitialized;

        /// <summary>
        /// 初期化を行います。
        /// </summary>
        /// <param name="theme">設定するテーマ名を指定します。</param>
        public void Initialize(string theme = "Dark")
        {
            if (!_isInitialized)
            {
                _isInitialized = true;

                LoadThemes();
                var resourceDictionary = (ResourceDictionary)Application.LoadComponent(new Uri("/" + namespaceString + ";component/Themes/Generic.xaml", UriKind.Relative));
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);

                if (!this._themeDictionary.ContainsKey(theme))
                    theme = this._themeDictionary.Keys.First();
                SetTheme(theme);
            }
        }

        /// <summary>
        /// テーマを設定します。
        /// </summary>
        /// <param name="theme">設定するテーマ名を指定します。</param>
        public void SetTheme(string theme)
        {
            if (this.CurrentTheme != theme)
            {
                if (this._themeDictionary.ContainsKey(theme))
                {
                    Application.Current.Resources.MergedDictionaries.Add(this._themeDictionary[theme]);
                    this.CurrentTheme = theme;
                    this.RaiseThemeChanged();
                }
            }
        }
        #endregion 公開メソッド

        #region イベント
        /// <summary>
        /// テーマ変更後に発生します。
        /// </summary>
        public event EventHandler<EventArgs> ThemeChanged;

        /// <summary>
        /// ThemeChanged イベントを発行します。
        /// </summary>
        private void RaiseThemeChanged()
        {
            var h = this.ThemeChanged;
            if (h != null) h(this, EventArgs.Empty);
        }
        #endregion イベント

        #region ヘルパ
        /// <summary>
        /// テーマ名とリソースを紐付けるためのディクショナリ
        /// </summary>
        private Dictionary<string, ResourceDictionary> _themeDictionary;

        /// <summary>
        /// リソース内のテーマを読込ます。
        /// </summary>
        private void LoadThemes()
        {
            this._themeDictionary = new Dictionary<string, ResourceDictionary>();

            var themeList = GetThemeNameList();
            foreach (var theme in themeList)
            {
                var filePath = string.Format(@"{0};component/Themes/Colors/{1}.xaml", namespaceString, theme);
                var dictionary = (ResourceDictionary)Application.LoadComponent(new Uri(filePath, UriKind.Relative));
                var name = dictionary.Contains(ThemeNameKey) ? dictionary[ThemeNameKey] as string : theme;
                this._themeDictionary.Add(name, dictionary);
            }
            this.ThemeNameList = new ReadOnlyCollection<string>(new List<string>(this._themeDictionary.Select(x => x.Key)));
        }

        /// <summary>
        /// リソース内のテーマ用ファイル名を取得します。
        /// </summary>
        /// <returns>テーマ用ファイル名リストを返します。</returns>
        private string[] GetThemeNameList()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceFileName = assembly.GetName().Name + ".g.resources";
            using (var stream = assembly.GetManifestResourceStream(resourceFileName))
            {
                using (var reader = new ResourceReader(stream))
                {
                    return reader.Cast<DictionaryEntry>()
                        .Where(entry => entry.Key.ToString().StartsWith("themes/colors/"))
                        .Select(entry => entry.Key.ToString().Replace(".baml", "").Replace("themes/colors/", ""))
                        .OrderBy(entry => entry.ToString())
                        .ToArray();
                    //return new List<string>(reader.Cast<DictionaryEntry>()
                    //    .Where(entry => entry.Key.ToString().StartsWith("themes/colors/"))
                    //    .Select(entry => entry.Key.ToString().Replace(".baml", "").Replace("themes/colors/", ""))
                    //    .OrderBy(entry => entry.ToString().ToArray()));
                }
            }
        }
        #endregion ヘルパ
    }
}
