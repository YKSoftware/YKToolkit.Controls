namespace YKToolkit.Controls
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interop;

    /// <summary>
    /// ファイルツリーを表します。
    /// </summary>
    [TemplatePart(Name = PART_MainTree, Type = typeof(TreeView))]
    public class FileTreeView : Control
    {
        #region TemplatePart
        private const string PART_MainTree = "PART_MainTree";

        private TreeView _mainTree;
        private TreeView MainTree
        {
            get { return _mainTree; }
            set
            {
                if (_mainTree != null)
                {
                    _mainTree.ItemsSource = null;
                    _mainTree.SelectedItemChanged -= MainTree_SelectedItemChanged;
                }
                _mainTree = value;
                if (_mainTree != null)
                {
                    _mainTree.SelectedItemChanged += MainTree_SelectedItemChanged;
                }
            }
        }

        /// <summary>
        /// テンプレート適用時の処理
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.MainTree = this.Template.FindName(PART_MainTree, this) as TreeView;

            var w = Window.GetWindow(this);
            if (w == null)
                return;

            #region ツリーの初期状態を設定する

            var handle = (new WindowInteropHelper(w)).Handle;

            var desktopPath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var desktop = new FileTreeViewItem(desktopPath);
            desktop.IsExpanded = true;

            #region マイコンピュータ
            _myComputer = new FileTreeViewItem("");
            _myComputer.Name = "マイコンピュータ";
            _myComputer.IsExpanded = true;
            _myComputer.IsExpanded = false;
            _myComputer.BitmapByteArray = Shell32.ShellInfo.GetSpecialIconByByteArray(handle, Shell32.ShellInfo.FolderID.MyComputer);
            _myComputer.Children = new ObservableCollection<FileTreeViewItem>();

            var infoArray = DriveInfo.GetDrives();
            foreach (var info in infoArray)
            {
                if (info.IsReady)
                {
                    (_myComputer.Children as ObservableCollection<FileTreeViewItem>).Add(new FileTreeViewItem(info.RootDirectory.FullName));
                }
            }
            #endregion マイコンピュータ

            #region マイドキュメント
            var myDocumentPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var myDocument = new FileTreeViewItem(myDocumentPath);
            #endregion マイドキュメント

            (desktop.Children as Collection<FileTreeViewItem>).Insert(0, _myComputer);
            (desktop.Children as Collection<FileTreeViewItem>).Insert(1, myDocument);

            var rootCollection = new ObservableCollection<FileTreeViewItem>()
            {
                desktop,
            };
            this.MainTree.ItemsSource = rootCollection;

            #endregion ツリーの初期状態を設定する
        }
        #endregion TemplatePart

        #region コンストラクタ
        /// <summary>
        /// 静的なコンストラクタです。
        /// </summary>
        static FileTreeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FileTreeView), new FrameworkPropertyMetadata(typeof(FileTreeView)));
        }
        #endregion コンストラクタ

        #region SelectedPath 依存関係プロパティ
        /// <summary>
        /// SelectedPath 依存関係プロパティキーの定義
        /// </summary>
        public static readonly DependencyPropertyKey SelectedPathPropertyKey = DependencyProperty.RegisterReadOnly("SelectedPath", typeof(string), typeof(FileTreeView), new PropertyMetadata(null));

        /// <summary>
        /// SelectedPath 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty SelectedPathProperty = SelectedPathPropertyKey.DependencyProperty;

        /// <summary>
        /// 選択されているパスを取得します。
        /// </summary>
        public string SelectedPath
        {
            get { return (string)GetValue(SelectedPathProperty); }
            private set { SetValue(SelectedPathPropertyKey, value); }
        }
        #endregion SelectedPath 依存関係プロパティ

        #region イベントハンドラ
        /// <summary>
        /// MainTree SelectedItemChanged イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void MainTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = e.NewValue as FileTreeViewItem;
            this.SelectedPath = item != null ? item.FullPath : null;
        }
        #endregion イベントハンドラ

        #region private フィールド
        /// <summary>
        /// マイコンピュータを表すノード
        /// </summary>
        private FileTreeViewItem _myComputer;
        #endregion private フィールド
    }
}
