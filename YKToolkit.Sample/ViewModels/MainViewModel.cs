namespace YKToolkit.Sample.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Timers;
    using YKToolkit.Bindings;
    using YKToolkit.Sample.Models;

    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// アプリケーション名を取得します。
        /// </summary>
        public string Title
        {
            get { return ProductInfo.Instance.Title; }
        }

        #region 現在時刻
        /// <summary>
        /// 現在時刻更新用タイマー
        /// </summary>
        private Timer _timerByOneSecond;

        private DateTime _currentTime;
        /// <summary>
        /// 現在時刻を取得します。
        /// </summary>
        public DateTime CurrentTime
        {
            get
            {
                if (this._timerByOneSecond == null)
                {
                    _currentTime = DateTime.Now;
                    this._timerByOneSecond = new Timer(1000);
                    this._timerByOneSecond.Elapsed += (_, __) =>
                    {
                        CurrentTime = DateTime.Now;
                    };
                    this._timerByOneSecond.Start();
                }
                return _currentTime;
            }
            private set { SetProperty(ref _currentTime, value); }
        }
        #endregion 現在時刻

        #region テーマ変更
        /// <summary>
        /// テーマ名リストを取得します。
        /// </summary>
        public ReadOnlyCollection<string> Themes
        {
            get { return YKToolkit.Controls.ThemeManager.Instance.ThemeNameList; }
        }

        private string _currentTheme;
        /// <summary>
        /// 現在のテーマ名を取得または設定します。
        /// </summary>
        public string CurrentTheme
        {
            get
            {
                if (string.IsNullOrEmpty(_currentTheme))
                {
                    _currentTheme = YKToolkit.Controls.ThemeManager.Instance.CurrentTheme;
                    YKToolkit.Controls.ThemeManager.Instance.ThemeChanged += OnThemeChanged;
                }
                return _currentTheme;
            }
            set
            {
                if (SetProperty(ref _currentTheme, value))
                {
                    YKToolkit.Controls.ThemeManager.Instance.SetTheme(_currentTheme);
                }
            }
        }

        /// <summary>
        /// テーマ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private void OnThemeChanged(object sender, EventArgs e)
        {
            CurrentTheme = YKToolkit.Controls.ThemeManager.Instance.CurrentTheme;
        }

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
                    CurrentTheme = manager.ThemeNameList[index];
                }));
            }
        }
        #endregion テーマ変更

        #region ViewModel 選択
        private ReadOnlyCollection<ViewModelInfo> _viewModels;
        /// <summary>
        /// ViewModel リストを取得します。
        /// </summary>
        public ReadOnlyCollection<ViewModelInfo> ViewModels
        {
            get
            {
                if (_viewModels == null)
                {
                    _viewModels = new ReadOnlyCollection<ViewModelInfo>(new List<ViewModelInfo>()
                    {
                        new ViewModelInfo() { Name = "Debug", Instance = this._debugViewModel, },

                        new ViewModelInfo()
                        {
                            Name = "Standard Controls",
                            Children = new List<ViewModelInfo>()
                            {
                                new ViewModelInfo() { Name = "Button", Instance = this._buttonViewModel, },
                                new ViewModelInfo() { Name = "CheckBox", Instance = this._checkBoxViewModel, },
                                new ViewModelInfo() { Name = "ComboBox", Instance = this._comboBoxViewModel, },
                                new ViewModelInfo() { Name = "ContextMenu", Instance = this._contextMenuViewModel, },
                                new ViewModelInfo() { Name = "DataGrid", Instance = this._dataGridViewModel, },
                                new ViewModelInfo() { Name = "Expander", Instance = this._expanderViewModel, },
                                new ViewModelInfo() { Name = "Label", Instance = this._labelViewModel, },
                                new ViewModelInfo() { Name = "ListBox", Instance = this._listBoxViewModel, },
                                new ViewModelInfo() { Name = "Menu", Instance = this._menuViewModel, },
                                new ViewModelInfo() { Name = "PasswordBox", Instance = this._passwordBoxViewModel, },
                                new ViewModelInfo() { Name = "RadioButton", Instance = this._radioButtonViewModel, },
                                new ViewModelInfo() { Name = "RepeatButton", Instance = this._repeatButtonViewModel, },
                                new ViewModelInfo() { Name = "ScrollBar", Instance = this._scrollBarViewModel, },
                                new ViewModelInfo() { Name = "Slider", Instance = this._sliderViewModel, },
                                new ViewModelInfo() { Name = "StatusBar", Instance = this._statusBarViewModel, },
                                new ViewModelInfo() { Name = "TabControl", Instance = this._tabControlViewModel, },
                                new ViewModelInfo() { Name = "TextBox", Instance = this._textBoxViewModel, },
                                new ViewModelInfo() { Name = "ToggleButton", Instance = this._toggleButtonViewModel, },
                            },
                        },

                        new ViewModelInfo()
                        {
                            Name = "Extended Controls",
                            Children = new List<ViewModelInfo>()
                            {
                                new ViewModelInfo() { Name = "BusyIndicator", Instance = this._busyIndicatorViewModel, },
                                new ViewModelInfo() { Name = "ColorPicker", Instance = this._colorPickerViewModel, },
                                new ViewModelInfo() { Name = "DropDownButton", Instance = this._dropDownButtonViewModel, },
                                new ViewModelInfo() { Name = "FileTreeView", Instance = this._fileTreeViewViewModel, },
                                new ViewModelInfo() { Name = "LineGraph", Instance = this._lineGraphViewModel, },
                                new ViewModelInfo() { Name = "MessageBox", Instance = this._messageBoxViewModel, },
                                new ViewModelInfo() { Name = "SpinInput", Instance = this._spinInputViewModel, },
                                new ViewModelInfo() { Name = "SplitButton", Instance = this._splitButtonViewModel, },
                            },
                            IsExpanded = true,
                        },
                    });
                }
                return _viewModels;
            }
        }

        private DebugViewModel _debugViewModel = new DebugViewModel();

        private ButtonViewModel _buttonViewModel = new ButtonViewModel();
        private CheckBoxViewModel _checkBoxViewModel = new CheckBoxViewModel();
        private ComboBoxViewModel _comboBoxViewModel = new ComboBoxViewModel();
        private ContextMenuViewModel _contextMenuViewModel = new ContextMenuViewModel();
        private DataGridViewModel _dataGridViewModel = new DataGridViewModel();
        private ExpanderViewModel _expanderViewModel = new ExpanderViewModel();
        private LabelViewModel _labelViewModel = new LabelViewModel();
        private ListBoxViewModel _listBoxViewModel = new ListBoxViewModel();
        private MenuViewModel _menuViewModel = new MenuViewModel();
        private PasswordBoxViewModel _passwordBoxViewModel = new PasswordBoxViewModel();
        private RadioButtonViewModel _radioButtonViewModel = new RadioButtonViewModel();
        private RepeatButtonViewModel _repeatButtonViewModel = new RepeatButtonViewModel();
        private ScrollBarViewModel _scrollBarViewModel = new ScrollBarViewModel();
        private SliderViewModel _sliderViewModel = new SliderViewModel();
        private StatusBarViewModel _statusBarViewModel = new StatusBarViewModel();
        private TabControlViewModel _tabControlViewModel = new TabControlViewModel();
        private TextBoxViewModel _textBoxViewModel = new TextBoxViewModel();
        private ToggleButtonViewModel _toggleButtonViewModel = new ToggleButtonViewModel();

        private BusyIndicatorViewModel _busyIndicatorViewModel = new BusyIndicatorViewModel();
        private ColorPickerViewModel _colorPickerViewModel = new ColorPickerViewModel();
        private DropDownButtonViewModel _dropDownButtonViewModel = new DropDownButtonViewModel();
        private FileTreeViewViewModel _fileTreeViewViewModel = new FileTreeViewViewModel();
        private LineGraphViewModel _lineGraphViewModel = new LineGraphViewModel();
        private MessageBoxViewModel _messageBoxViewModel = new MessageBoxViewModel();
        private SpinInputViewModel _spinInputViewModel = new SpinInputViewModel();
        private SplitButtonViewModel _splitButtonViewModel = new SplitButtonViewModel();
        #endregion ViewModel 選択
    }
}
