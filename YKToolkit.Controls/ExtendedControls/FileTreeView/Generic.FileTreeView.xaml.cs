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

            Initilization();
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
        ///// <summary>
        ///// SelectedPath 依存関係プロパティキーの定義
        ///// </summary>
        //private static readonly DependencyPropertyKey SelectedPathPropertyKey = DependencyProperty.RegisterReadOnly("SelectedPath", typeof(string), typeof(FileTreeView), new PropertyMetadata(null));

        /// <summary>
        /// SelectedPath 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty SelectedPathProperty = DependencyProperty.Register("SelectedPath", typeof(string), typeof(FileTreeView), new PropertyMetadata(null));
        //public static readonly DependencyProperty SelectedPathProperty = SelectedPathPropertyKey.DependencyProperty;

        /// <summary>
        /// 選択されているパスを取得または設定します。
        /// </summary>
        public string SelectedPath
        {
            get { return (string)GetValue(SelectedPathProperty); }
            set { SetValue(SelectedPathProperty, value); }
        }
        #endregion SelectedPath 依存関係プロパティ

        #region SearchPattern 依存関係プロパティ
        /// <summary>
        /// SearchPattern 依存関係プロパティ
        /// </summary>
        public static readonly DependencyProperty SearchPatternProperty = DependencyProperty.Register("SearchPattern", typeof(string), typeof(FileTreeView), new PropertyMetadata(null, OnSearchPatternPropertyChanged));

        /// <summary>
        /// ファイル検索のための検索パターンを取得または設定します。
        /// </summary>
        public string SearchPattern
        {
            get { return (string)GetValue(SearchPatternProperty); }
            set { SetValue(SearchPatternProperty, value); }
        }

        /// <summary>
        /// SearchPattern プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnSearchPatternPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as FileTreeView;
            if (control == null)
                return;

            control.Initilization();
        }
        #endregion SearchPattern 依存関係プロパティ

        #region IsFileEnabled 依存関係プロパティ
        /// <summary>
        /// IsFileEnabled 依存関係プロパティ
        /// </summary>
        public static readonly DependencyProperty IsFileEnabledProperty = DependencyProperty.Register("IsFileEnabled", typeof(bool), typeof(FileTreeView), new PropertyMetadata(false, OnIsFileEnabledPropertyChanged));

        /// <summary>
        /// ファイルを表示するかどうかを取得または設定します。
        /// </summary>
        public bool IsFileEnabled
        {
            get { return (bool)GetValue(IsFileEnabledProperty); }
            set { SetValue(IsFileEnabledProperty, value); }
        }

        /// <summary>
        /// IsFileEnabled プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnIsFileEnabledPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as FileTreeView;
            if (control == null)
                return;

            control.Initilization();
        }
        #endregion IsFileEnabled 依存関係プロパティ

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

        /// <summary>
        /// 初期化をおこないます。
        /// </summary>
        private void Initilization()
        {
            var w = Window.GetWindow(this);
            if (w == null)
                return;

            var handle = (new WindowInteropHelper(w)).Handle;

            #region マイコンピュータ
            _myComputer = new FileTreeViewItem("", this.SearchPattern, this.IsFileEnabled);
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
                    (_myComputer.Children as ObservableCollection<FileTreeViewItem>).Add(new FileTreeViewItem(info.RootDirectory.FullName, this.SearchPattern, this.IsFileEnabled));
                }
            }
            #endregion マイコンピュータ

            #region マイドキュメント
            var myDocumentPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var myDocument = new FileTreeViewItem(myDocumentPath, this.SearchPattern, this.IsFileEnabled);
            #endregion マイドキュメント

            #region デスクトップ
            var desktopPath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            var desktop = new FileTreeViewItem(desktopPath, this.SearchPattern, this.IsFileEnabled);
            desktop.Name = "デスクトップ";
            desktop.IsExpanded = true;

            (desktop.Children as Collection<FileTreeViewItem>).Insert(0, _myComputer);
            (desktop.Children as Collection<FileTreeViewItem>).Insert(1, myDocument);
            #endregion デスクトップ

            var rootCollection = new ObservableCollection<FileTreeViewItem>()
            {
                desktop,
            };
            this.MainTree.ItemsSource = rootCollection;
        }
    }
}
