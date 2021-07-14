namespace YKToolkit.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Windows.Threading;
    using YKToolkit.Bindings;

    /// <summary>
    /// FileTreeView コントロールのためのアイテムを表します。
    /// </summary>
    internal class FileTreeViewItem : NotificationObject
    {
        /// <summary>
        /// 静的なコンストラクタ
        /// </summary>
        static FileTreeViewItem()
        {
            // 特殊ディレクトリのパスを保持しておく
            SpecialFolders = new Dictionary<string, byte[]>(Enum.GetValues(typeof(Environment.SpecialFolder))
                                                                .Cast<Environment.SpecialFolder>()
                                                                .Select(x => Environment.GetFolderPath(x))
                                                                .Where(x => x != null)
                                                                .Distinct()
                                                                .Select(x => new KeyValuePair<string, byte[]>(x, Shell32.ShellInfo.GetSystemIconByByteArray(x)))
                                                                .ToDictionary(x => x.Key, x => x.Value));
        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="fullPath">フルパスを指定します。</param>
        /// <param name="searchPattern">ファイルの検索条件を指定します。</param>
        /// <param name="isFileEnabled">ファイルの読込を有効にする場合に true を指定します。</param>
        public FileTreeViewItem(string fullPath, string searchPattern, bool isFileEnabled = false)
        {
            _searchPattern = searchPattern;
            _isFileEnabled = isFileEnabled;

            var isDirectory = Directory.Exists(fullPath);
            var isFile = File.Exists(fullPath);
            if (!isDirectory && !isFile)
            {
                this.Name = fullPath + " (存在しません)";
                return;
            }

            this.FullPath = fullPath;
            var dir = new DirectoryInfo(fullPath);
            if (isDirectory)
            {
                this.Name = dir.Name;
                this.BitmapByteArray = GetSystemIconByByteArray(fullPath);

                var subDirs = dir.GetDirectories();
                var subFiles = dir.GetFiles(searchPattern != null ? searchPattern : string.Empty);
                if ((subDirs.Length > 0) || (isFileEnabled && (subFiles.Length > 0)))
                    this.Children = new ObservableCollection<FileTreeViewItem>() { null };   // ダミーデータを入れておく
            }
            else if (isFile && isFileEnabled)
            {
                var file = new FileInfo(fullPath);
                this.Name = file.Name;
                this.BitmapByteArray = GetSystemIconByByteArray(fullPath);
            }
        }

        private ObservableCollection<FileTreeViewItem> _children;
        /// <summary>
        /// 階層構造を取得または設定します。
        /// </summary>
        public ObservableCollection<FileTreeViewItem> Children
        {
            get { return _children; }
            set { SetProperty(ref _children, value); }
        }

        /// <summary>
        /// 名前を取得または設定します。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ディレクトリのフルパスを取得または設定します。
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// アイコン画像のバイト配列を取得または設定します。
        /// </summary>
        public byte[] BitmapByteArray { get; set; }

        private bool _hasGetChildren;

        private bool _isExpanded;
        /// <summary>
        /// 展開済みかどうかを取得または設定します。
        /// </summary>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                if (SetProperty(ref _isExpanded, value) && _isExpanded && !this._hasGetChildren)
                {
                    if (!string.IsNullOrWhiteSpace(this.FullPath))
                    {
                        // 展開されて初めて下層の実体を取得しにいく
                        this.Children = new ObservableCollection<FileTreeViewItem>();

                        var currentDir = new DirectoryInfo(this.FullPath);
                        try
                        {
                            currentDir.GetDirectories().ToList().ForEach(dirInfo =>
                            {
                                try
                                {
                                    dirInfo.GetAccessControl();
                                    AddChild(new FileTreeViewItem(dirInfo.FullName, _searchPattern, _isFileEnabled));
                                }
                                catch (Exception err)
                                {
                                    System.Diagnostics.Debug.WriteLine(err);
                                }
                            });

                            if (_isFileEnabled)
                            {
                                string[] patterns = string.IsNullOrWhiteSpace(_searchPattern) ?
                                                    new string[] { "*.*" } :
                                                    _searchPattern.Split(';').Select(x => x.Trim().ToLower()).ToArray();

                                patterns.SelectMany(pattern => currentDir.GetFiles(pattern))
                                        .Select(fileInfo =>
                                         {
                                             try
                                             {
                                                 return new FileTreeViewItem(fileInfo.FullName, _searchPattern, _isFileEnabled);
                                             }
                                             catch (Exception err)
                                             {
                                                 System.Diagnostics.Debug.WriteLine(err);
                                                 return null;
                                             }
                                         })
                                        .Where(x => x != null)
                                        .ToList()
                                        .ForEach(x => AddChild(x));
                            }
                        }
                        catch
                        {
                            // 例えばリムーバブルディスクなんかで外した後だったときは
                            // 展開せずに子要素なしとして終了
                            this.Children = null;
                        }
                    }

                    _hasGetChildren = true;
                }
            }
        }

        /// <summary>
        /// 指定されたパスのディレクトリまたはファイルのアイコン画像をバイト配列として取得します。
        /// </summary>
        /// <param name="path">フルパスを指定します。</param>
        /// <returns>アイコン画像のためのバイト配列を返します。</returns>
        private byte[] GetSystemIconByByteArray(string path)
        {
            // 特殊フォルダ
            if (SpecialFolders.ContainsKey(path))
            {
                // 保持してあるバイト配列を返す
                return SpecialFolders[path];
            }

            var extension = Path.GetExtension(path).ToLower();

            // 論理ドライブのパス、.ico ファイル、.exe ファイル、.lnk ファイルの場合は毎回読み込む
            if ((Directory.GetLogicalDrives().Any(x => x == path))
                || (extension == ".ico")
                || (extension == ".exe")
                || (extension == ".lnk"))
            {
                return Shell32.ShellInfo.GetSystemIconByByteArray(path);
            }

            // 通常のフォルダ
            if (Directory.Exists(path))
            {
                return DirectoryIcon ?? (DirectoryIcon = Shell32.ShellInfo.GetSystemIconByByteArray(path));
            }

            // コレクションに非同期でアクセスされるのでロックしておく
            lock (OtherFolders)
            {
                if (!OtherFolders.ContainsKey(extension))
                {
                    // 新たに読み込む拡張子を持つファイル
                    OtherFolders.Add(extension, Shell32.ShellInfo.GetSystemIconByByteArray(path));
                }
            }

            // 読み込み済みの拡張子を持つファイル
            return OtherFolders[extension];
        }

        /// <summary>
        /// 特殊ディレクトリに対するアイコン画像バイト配列をキャッシュ
        /// </summary>
        private readonly static Dictionary<string, byte[]> SpecialFolders;

        /// <summary>
        /// 読み込み済みの拡張子に対するアイコン画像バイト配列をキャッシュ
        /// </summary>
        private readonly static Dictionary<string, byte[]> OtherFolders = new Dictionary<string,byte[]>();

        /// <summary>
        /// 通常のディレクトリアイコン画像バイト配列をキャッシュ
        /// </summary>
        private static byte[] DirectoryIcon;

        private void AddChild(FileTreeViewItem item)
        {
            if (this._tempChildren == null) this._tempChildren = new Collection<FileTreeViewItem>();
            this._tempChildren.Add(item);

            // 非同期で何度もコールされるので
            // まとめて処理できるようにタイマーでちょっとだけ待つ
            if (this._timer == null)
            {
                this._timer = new DispatcherTimer()
                {
                    Interval = TimeSpan.FromMilliseconds(200),
                };
                this._timer.Tick += OnTimerTick;
            }
            this._timer.Stop();
            this._timer.Start();
        }

        private void OnTimerTick(object sender, EventArgs e)
        {
            this._timer.Stop();
            this._timer = null;

            var children = this.Children.Concat(this._tempChildren);
            this.Children = new ObservableCollection<FileTreeViewItem>(children.OrderBy(x => x, FileTreeViewItemComparer.Comparer));
            this._tempChildren.Clear();
        }

        private DispatcherTimer _timer;

        private Collection<FileTreeViewItem> _tempChildren;

        /// <summary>
        /// ファイル検索のための検索パターン
        /// </summary>
        private static string _searchPattern;

        /// <summary>
        /// ファイルを表示するかどうか
        /// </summary>
        private static bool _isFileEnabled;
    }

    internal class FileTreeViewItemComparer : IComparer<FileTreeViewItem>
    {
        public static readonly FileTreeViewItemComparer Comparer = new FileTreeViewItemComparer();

        private readonly List<string> SpecialPath = new List<string>()
        {
            null,
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        };

        public int Compare(FileTreeViewItem x, FileTreeViewItem y)
        {
            // 特殊ディレクトリが優先
            if (SpecialPath.Contains(x.FullPath))
            {
                if (!SpecialPath.Contains(y.FullPath))
                {
                    // x のみ特殊ディレクトリ
                    return -1;
                }
                else
                {
                    // 両方特殊ディレクトリの場合はリストのインデックス順とする
                    return SpecialPath.IndexOf(x.FullPath) - SpecialPath.IndexOf(y.FullPath);
                }
            }
            else if (SpecialPath.Contains(y.FullPath))
            {
                // y のみ特殊ディレクトリ
                return 1;
            }

            // ディレクトリが優先
            if (Directory.Exists(x.FullPath))
            {
                if (Directory.Exists(y.FullPath))
                {
                    // 両方ディレクトリの場合は単純に文字列でソート
                    return string.Compare(x.FullPath, y.FullPath);
                }
                else
                {
                    // x がディレクトリ、y がファイル
                    return -1;
                }
            }
            else if (Directory.Exists(y.FullPath))
            {
                // y のみディレクトリ
                return 1;
            }

            // 両方ファイルの場合は拡張子でソート
            var ret = string.Compare(Path.GetExtension(x.FullPath), Path.GetExtension(y.FullPath));
            if (ret != 0) return ret;
            else return string.Compare(x.FullPath, y.FullPath);
        }
    }
}
