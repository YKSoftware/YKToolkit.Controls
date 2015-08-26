using System.Collections.ObjectModel;
using YKToolkit.Bindings;
namespace YKToolkit.Sample.ViewModels
{
    public class ButtonViewModel : ViewModelBase
    {
        #region テーマ変更
        private DelegateCommand _changeThemeCommand;
        /// <summary>
        /// テーマを変更します。
        /// </summary>
        public DelegateCommand ChageThemeCommand
        {
            get
            {
                return _changeThemeCommand ?? (_changeThemeCommand = new DelegateCommand(_ =>
                {
                    var manager = YKToolkit.Controls.ThemeManager.Instance;
                    var index = manager.ThemeNameList.IndexOf(manager.CurrentTheme) > 0 ? 0 : 1;
                    manager.SetTheme(manager.ThemeNameList[index]);
                }));
            }
        }
        #endregion テーマ変更
    }
}
