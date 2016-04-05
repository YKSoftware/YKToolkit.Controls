namespace YKToolkit.Controls
{
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using YKToolkit.Bindings;

    /// <summary>
    /// FileTreeView コントロールのためのアイテムを表します。
    /// </summary>
    internal class FileTreeViewItem : NotificationObject
    {
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
                this.BitmapByteArray = Shell32.ShellInfo.GetSystemIconByByteArray(fullPath);

                var subDirs = dir.GetDirectories();
                var subFiles = dir.GetFiles(searchPattern != null ? searchPattern : string.Empty);
                if ((subDirs.Length > 0) || (isFileEnabled && (subFiles.Length > 0)))
                    this.Children = new object[] { null };   // ダミーデータを入れておく
            }
            else if (isFile && isFileEnabled)
            {
                var file = new FileInfo(fullPath);
                this.Name = file.Name;
                this.BitmapByteArray = Shell32.ShellInfo.GetSystemIconByByteArray(fullPath);
            }
        }

        private IEnumerable _children;
        /// <summary>
        /// 階層構造を取得または設定します。
        /// </summary>
        public IEnumerable Children
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
                        var children = new ObservableCollection<FileTreeViewItem>();
                        var currentDir = new DirectoryInfo(this.FullPath);
                        try
                        {
                            var dirsInfo = currentDir.GetDirectories();
                            foreach (var dirInfo in dirsInfo)
                            {
                                try
                                {
                                    dirInfo.GetAccessControl();
                                    children.Add(new FileTreeViewItem(dirInfo.FullName, _searchPattern, _isFileEnabled));
                                }
                                catch (Exception err)
                                {
                                    System.Diagnostics.Debug.WriteLine(err);
                                }
                            }
                            if (_isFileEnabled)
                            {
                                // ここはもっと効率良くして速度を向上させるべき
                                string[] patterns = null;
                                if (string.IsNullOrWhiteSpace(_searchPattern))
                                {
                                    patterns = new string[] { "*.*" };
                                }
                                else
                                {
                                    patterns = _searchPattern.Split(';').Select(x => x.Trim().ToLower()).ToArray();
                                }
                                foreach (var pattern in patterns)
                                {
                                    var filesInfo = currentDir.GetFiles(pattern);
                                    foreach (var fileInfo in filesInfo)
                                    {
                                        try
                                        {
                                            fileInfo.GetAccessControl();
                                            children.Add(new FileTreeViewItem(fileInfo.FullName, _searchPattern, _isFileEnabled));
                                        }
                                        catch (Exception err)
                                        {
                                            System.Diagnostics.Debug.WriteLine(err);
                                        }
                                    }
                                }
                            }
                            this.Children = children;
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
        /// ファイル検索のための検索パターン
        /// </summary>
        private static string _searchPattern;

        /// <summary>
        /// ファイルを表示するかどうか
        /// </summary>
        private static bool _isFileEnabled;
    }
}
