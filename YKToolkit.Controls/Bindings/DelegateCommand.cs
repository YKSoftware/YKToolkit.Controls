namespace YKToolkit.Bindings
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// <c>System.Windows.Input.ICommand</c> インターフェースを実装したコマンドを表します。
    /// </summary>
    public class DelegateCommand : ICommand
    {
        /// <summary>
        /// コマンドの実体
        /// </summary>
        private Action<object> _execute;

        /// <summary>
        /// コマンドの実行可能性判別の実体
        /// </summary>
        private Func<object, bool> _canExecute;

        #region コンストラクタ
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="execute">コマンドの実体を指定します。</param>
        public DelegateCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="execute">コマンドの実体を指定します。</param>
        /// <param name="canExecute">コマンドの実行可能判別の実体を指定します。</param>
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }
        #endregion コンストラクタ

        #region ICommand のメンバ
        /// <summary>
        /// コマンドの実行可能性を判別します。
        /// </summary>
        /// <param name="parameter">判別するためのパラメータを指定します。</param>
        /// <returns>コマンドが実行可能である場合に true を返します。</returns>
        public bool CanExecute(object parameter)
        {
            return this._canExecute != null ? this._canExecute(parameter) : true;
        }

        /// <summary>
        /// 実行可能判別条件が変更したときに発生しいます。
        /// </summary>
        public event System.EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// コマンドを実行します。
        /// </summary>
        /// <param name="parameter">コマンド実行のためのパラメータを指定します。</param>
        public void Execute(object parameter)
        {
            if (this._execute != null)
                this._execute(parameter);
        }
        #endregion ICommand のメンバ
    }
}
