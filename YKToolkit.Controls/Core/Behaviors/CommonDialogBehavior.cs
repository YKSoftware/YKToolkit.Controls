namespace YKToolkit.Controls.Behaviors
{
    using Microsoft.Win32;
    using System;
    using System.Windows;

    /// <summary>
    /// コモンダイアログを表示するための添付ビヘイビア
    /// </summary>
    public class CommonDialogBehavior
    {
        /// <summary>
        /// ダイアログの種類を示す列挙体
        /// </summary>
        public enum DialogTypes : int
        {
            /// <summary>
            /// ファイル保存ダイアログ
            /// </summary>
            SaveFile = 0,

            /// <summary>
            /// ファイル読込ダイアログ
            /// </summary>
            OpenFile,

            /// <summary>
            /// フォルダ選択ダイアログ
            /// </summary>
            FolderSelect,
        }

        /// <summary>
        /// DialogType 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty DialogTypeProperty = DependencyProperty.RegisterAttached("DialogType", typeof(DialogTypes), typeof(CommonDialogBehavior), new FrameworkPropertyMetadata(DialogTypes.OpenFile));
        /// <summary>
        /// DialogType 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">添付プロパティの取得先</param>
        /// <returns>取得結果</returns>
        public static DialogTypes GetDialogType(DependencyObject target)
        {
            return (DialogTypes)target.GetValue(DialogTypeProperty);
        }
        /// <summary>
        /// DialogType 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">添付プロパティの設定対象</param>
        /// <param name="value">設定値を指定します。</param>
        public static void SetDialogType(DependencyObject target, DialogTypes value)
        {
            target.SetValue(DialogTypeProperty, value);
        }

        /// <summary>
        /// Title 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.RegisterAttached("Title", typeof(string), typeof(CommonDialogBehavior), new FrameworkPropertyMetadata(string.Empty));
        /// <summary>
        /// Title 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">添付プロパティの取得先</param>
        /// <returns>取得結果</returns>
        public static string GetTitle(DependencyObject target)
        {
            return (string)target.GetValue(TitleProperty);
        }
        /// <summary>
        /// Title 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">添付プロパティの設定対象</param>
        /// <param name="value">設定値を指定します。</param>
        public static void SetTitle(DependencyObject target, string value)
        {
            target.SetValue(TitleProperty, value);
        }

        /// <summary>
        /// FileName 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty FileNameProperty = DependencyProperty.RegisterAttached("FileName", typeof(string), typeof(CommonDialogBehavior), new FrameworkPropertyMetadata(string.Empty));
        /// <summary>
        /// FileName 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">添付プロパティの取得先</param>
        /// <returns>取得結果</returns>
        public static string GetFileName(DependencyObject target)
        {
            return (string)target.GetValue(FileNameProperty);
        }
        /// <summary>
        /// FileName 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">添付プロパティの設定対象</param>
        /// <param name="value">設定値を指定します。</param>
        public static void SetFileName(DependencyObject target, string value)
        {
            target.SetValue(FileNameProperty, value);
        }

        /// <summary>
        /// FileFilter 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty FileFilterProperty = DependencyProperty.RegisterAttached("FileFilter", typeof(string), typeof(CommonDialogBehavior), new FrameworkPropertyMetadata(string.Empty));
        /// <summary>
        /// FileFilter 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">添付プロパティの取得先</param>
        /// <returns>取得結果</returns>
        public static string GetFileFilter(DependencyObject target)
        {
            return (string)target.GetValue(FileFilterProperty);
        }
        /// <summary>
        /// FileFilter 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">添付プロパティの設定対象</param>
        /// <param name="value">設定値を指定します。</param>
        public static void SetFileFilter(DependencyObject target, string value)
        {
            target.SetValue(FileFilterProperty, value);
        }

        /// <summary>
        /// IsMultiSelect 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsMultiSelectProperty = DependencyProperty.RegisterAttached("IsMultiSelect", typeof(bool), typeof(CommonDialogBehavior), new FrameworkPropertyMetadata(false));
        /// <summary>
        /// IsMultiSelect 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">添付プロパティの取得先</param>
        /// <returns>取得結果</returns>
        public static bool GetIsMultiSelect(DependencyObject target)
        {
            return (bool)target.GetValue(IsMultiSelectProperty);
        }
        /// <summary>
        /// IsMultiSelect 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">添付プロパティの設定対象</param>
        /// <param name="value">設定値を指定します。</param>
        public static void SetIsMultiSelect(DependencyObject target, bool value)
        {
            target.SetValue(IsMultiSelectProperty, value);
        }

        /// <summary>
        /// Callback 添付プロパティの定義
        /// </summary>
        public static readonly DependencyProperty CallbackProperty = DependencyProperty.RegisterAttached("Callback", typeof(Action<object, bool?>), typeof(CommonDialogBehavior), new FrameworkPropertyMetadata(null, OnCallbackChanged));
        /// <summary>
        /// Callback 添付プロパティを取得します。
        /// </summary>
        /// <param name="target">添付プロパティの取得先</param>
        /// <returns>取得結果</returns>
        public static Action<object, bool?> GetCallback(DependencyObject target)
        {
            return (Action<object, bool?>)target.GetValue(CallbackProperty);
        }
        /// <summary>
        /// Callback 添付プロパティを設定します。
        /// </summary>
        /// <param name="target">添付プロパティの設定対象</param>
        /// <param name="value">設定値を指定します。</param>
        public static void SetCallback(DependencyObject target, Action<object, bool?> value)
        {
            target.SetValue(CallbackProperty, value);
        }

        private static void OnCallbackChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var element = target as UIElement;
            if ((e.NewValue as Action<object, bool?>) != null)
            {
                var callback = e.NewValue as Action<object, bool?>;
                if (callback != null)
                {
                    var owner = Window.GetWindow(target);
                    var type = GetDialogType(element);
                    var title = GetTitle(element);
                    var filename = GetFileName(element);
                    var filter = GetFileFilter(element);
                    var isMultiSelect = GetIsMultiSelect(element);
                    bool? result = null;

                    switch (type)
                    {
                        case DialogTypes.SaveFile:
                            var saveDialog = new SaveFileDialog();
                            saveDialog.Title = title;
                            saveDialog.FileName = filename;
                            saveDialog.Filter = filter;
                            result = saveDialog.ShowDialog(Window.GetWindow(element));
                            callback(saveDialog.FileName, result);
                            break;

                        case DialogTypes.OpenFile:
                            var openDialog = new OpenFileDialog();
                            openDialog.Title = title;
                            openDialog.FileName = filename;
                            openDialog.Filter = filter;
                            openDialog.Multiselect = isMultiSelect;
                            result = openDialog.ShowDialog(Window.GetWindow(element));
                            callback(openDialog.FileNames, result);
                            break;

                        case DialogTypes.FolderSelect:
                            var selectDialog = new FolderSelectDialog();
                            selectDialog.Title = title;
                            selectDialog.Path = filename;
                            result = selectDialog.ShowDialog(owner);
                            callback(selectDialog.Path, result);
                            break;
                    }
                }
            }
        }
    }
}
