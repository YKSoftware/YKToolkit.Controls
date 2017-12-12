namespace YKToolkit.Bindings
{
    using System;
    using System.ComponentModel;
#if !NET4
    using System.Runtime.CompilerServices;
#endif

    /// <summary>
    /// <c>System.ComponentModel.INotifyPropertyChanged</c> インターフェースを実装した抽象クラスを表します。
    /// </summary>
    [Serializable]
    public abstract class NotificationObject : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged のメンバ
        /// <summary>
        /// プロパティ変更時に発生します。
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion INotifyPropertyChanged のメンバ

        /// <summary>
        /// プロパティ変更イベント PropertyChanged を発行します。
        /// </summary>
        /// <param name="propertyName">プロパティ名を指定します。</param>
#if NET4
        protected void RaisePropertyChanged(string propertyName = null)
#else
        protected void RaisePropertyChanged([CallerMemberName]string propertyName = null)
#endif
        {
            var h = this.PropertyChanged;
            if (h != null) h(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 全プロパティを対象としたプロパティ変更イベント PropertyChanged を発行します。
        /// </summary>
        protected void RaiseAllPropertyChanged()
        {
            var h = this.PropertyChanged;
            if (h != null) h(this, new PropertyChangedEventArgs(null));
        }

        /// <summary>
        /// プロパティ変更をおこない、プロパティ変更イベント PropertyChanged を発行します。
        /// </summary>
        /// <typeparam name="T">プロパティの型</typeparam>
        /// <param name="target">変更するプロパティの実体を参照渡しとして指定します。</param>
        /// <param name="value">変更後の値を指定します。</param>
        /// <param name="propertyName">プロパティ名を指定します。</param>
        /// <returns>プロパティ値に変更があった場合に true を返します。</returns>
#if NET4
        protected bool SetProperty<T>(ref T target, T value, string propertyName = null)
#else
        protected bool SetProperty<T>(ref T target, T value, [CallerMemberName]string propertyName = null)
#endif
        {
            if (object.Equals(target, value))
                return false;
            target = value;
            RaisePropertyChanged(propertyName);
            return true;
        }
    }
}
