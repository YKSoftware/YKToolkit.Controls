namespace YKToolkit.Sample
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using YKToolkit.Sample.ViewModels;
    using YKToolkit.Sample.Views;

    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // View-ViewModel マップを指定して下さい。
            _windowMap = new Dictionary<string, KeyValuePair<Type, Type>>()
            {
                { "Main", new KeyValuePair<Type, Type>(typeof(MainView), typeof(MainViewModel)) },
                { "Version", new KeyValuePair<Type, Type>(typeof(VersionView), typeof(VersionViewModel)) },
            };

            // テーマの初期化をおこないます。
            //YKToolkit.Controls.ThemeManager.Initialize("Light");
        }

        #region static 公開プロパティ
        /// <summary>
        /// アプリケーションのインスタンスを取得します。
        /// </summary>
        public static App Instance
        {
            get { return Application.Current as App; }
        }
        #endregion static 公開プロパティ

        #region private フィールド
        /// <summary>
        /// インスタンス化された View を保持するためのリスト
        /// </summary>
        private List<System.Windows.Window> views = new List<System.Windows.Window>();

        /// <summary>
        /// ウィンドウ名と View-ViewModel の Type 情報を紐付けるためのディクショナリ
        /// </summary>
        private readonly Dictionary<string, KeyValuePair<Type, Type>> _windowMap;

        /// <summary>
        /// アプリケーションのメインとなる View
        /// </summary>
        private System.Windows.Window rootView;
        #endregion private フィールド

        #region override
        /// <summary>
        /// 起動時イベントハンドラ
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if ((_windowMap == null) || (_windowMap.Count == 0))
                throw new InvalidOperationException("ウィンドウ名および View-ViewModel の関係性を示すディクショナリが初期化されていません。");

            rootView = ShowWindowCore(_windowMap.ToArray()[0].Key);
        }
        #endregion override

        #region 公開メソッド
        /// <summary>
        /// ウィンドウを表示します。
        /// </summary>
        /// <param name="windowName">表示するウィンドウの名前を指定します。</param>
        public void ShowWindow(string windowName)
        {
            ShowWindowCore(windowName);
        }

        /// <summary>
        /// ウィンドウを閉じます。
        /// </summary>
        /// <param name="windowName">閉じるウィンドウに対する ViewModel の Type 情報を指定します。</param>
        public void CloseWindow(Type type, bool isClear = true)
        {
            var vw = views.LastOrDefault(view => view.DataContext.GetType() == type);
            if (vw != null)
            {
                vw.Close();
                // ClosingWindow() イベントハンドラのほうで処理されるから不要
                //views.Remove(vw);

            }
        }

        /// <summary>
        /// ウィンドウをダイアログとして表示します。
        /// </summary>
        /// <param name="type">表示するダイアログウィンドウに対する DataContext の Type 情報を指定します。</param>
        public void ShowDialog(string windowName)
        {
            if (windowName == null)
                return;

            System.Windows.Window vw = null;
            KeyValuePair<Type, Type> pair;
            if (_windowMap.TryGetValue(windowName, out pair))
            {
                var vwType = pair.Key;
                var vmType = pair.Value;

                vw = CrateWindowInstance(vwType, vmType);

                if (vw != null)
                {
                    views.Add(vw);
                    vw.ShowDialog();
                    views.Remove(vw);
                }
            }
        }

        /// <summary>
        /// メッセージダイアログを表示します。
        /// </summary>
        /// <param name="message">表示するメッセージを指定します。</param>
        /// <param name="title">ダイアログのタイトルを指定します。</param>
        /// <param name="button">ダイアログに表示するボタンのパターンを指定します。</param>
        /// <param name="image">ダイアログに表示するアイコンの種別を指定します。</param>
        /// <returns>ダイアログの結果を返します。</returns>
        //public YKToolkit.Controls.MessageBoxResult ShowMessage(string message, string title, YKToolkit.Controls.MessageBoxButton button, YKToolkit.Controls.MessageBoxImage image)
        //{
        //    return YKToolkit.Controls.MessageBox.Show(message, title, button, image);
        //}
        #endregion 公開メソッド

        #region private メソッド
        /// <summary>
        /// ウィンドウを表示します。
        /// </summary>
        /// <param name="windowName">表示するウィンドウの名前を指定します。</param>
        /// <returns>表示するウィンドウを返します。</returns>
        private System.Windows.Window ShowWindowCore(string windowName)
        {
            if (windowName == null)
                return null;

            System.Windows.Window vw = null;
            KeyValuePair<Type, Type> pair;
            if (_windowMap.TryGetValue(windowName, out pair))
            {
                var vwType = pair.Key;
                var vmType = pair.Value;

                vw = views.FirstOrDefault(view => view.GetType() == vwType);
                if (vw == null)
                {
                    vw = CrateWindowInstance(vwType, vmType);

                    if (vw != null)
                    {
                        views.Add(vw);
                        vw.Show();
                    }
                }
                else
                {
                    if (vw.WindowState == WindowState.Minimized)
                        vw.WindowState = WindowState.Normal;
                    vw.Activate();
                }
            }

            return vw;
        }

        /// <summary>
        /// ウィンドウの新しいインスタンスを生成します。
        /// </summary>
        /// <param name="vwType">生成するウィンドウの Type 情報を指定します。</param>
        /// <param name="vmType">生成するウィンドウに対する DataContext の Type 情報を指定します。</param>
        /// <returns>新しいウィンドウのインスタンスを返します。</returns>
        private System.Windows.Window CrateWindowInstance(Type vwType, Type vmType)
        {
            object obj;
            obj = vwType.InvokeMember(null, System.Reflection.BindingFlags.CreateInstance, null, null, null);
            var vw = obj != null ? obj as System.Windows.Window : null;
            if (vw != null)
            {
                obj = vmType.InvokeMember(null, System.Reflection.BindingFlags.CreateInstance, null, null, null);
                vw.DataContext = obj;

                if (rootView != null)
                {
                    vw.Owner = rootView;
                    vw.WindowStartupLocation = WindowStartupLocation.CenterOwner;

                    vw.Closing += ClosingWindow;
                }
            }

            return vw;
        }

        /// <summary>
        /// ウィンドウの Closing イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void ClosingWindow(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var view = sender as System.Windows.Window;
            if (view != null)
            {
                view.Closing -= ClosingWindow;
                views.Remove(view);
            }
        }
        #endregion private メソッド
    }
}
