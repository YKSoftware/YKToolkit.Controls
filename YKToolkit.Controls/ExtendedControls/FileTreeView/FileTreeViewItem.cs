namespace YKToolkit.Controls
{
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.IO;
    using YKToolkit.Bindings;

    /// <summary>
    /// FileTreeView コントロールのためのアイテムを表します。
    /// </summary>
    internal class FileTreeViewItem : NotificationObject
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public FileTreeViewItem(string fullPath, bool isAutoGenerate = true)
        {
            var isDirectory = Directory.Exists(fullPath);
            if (!isDirectory)
            {
                this.Name = fullPath + " (存在しません)";
                return;
            }

            this.FullPath = fullPath;
            if (isDirectory)
            {
                var dir = new DirectoryInfo(fullPath);
                this.Name = dir.Name;
                this.BitmapByteArray = Shell32.ShellInfo.GetSystemIconByByteArray(fullPath);

                if (isAutoGenerate)
                {
                    var subDirs = dir.GetDirectories();
                    if (subDirs.Length > 0)
                        Children = new object[] { null };   // ダミーデータを入れておく
                }
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
                                    children.Add(new FileTreeViewItem(dirInfo.FullName));
                                }
                                catch
                                {
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
    }
}
