namespace YKToolkit.Bindings
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class NotificationObject : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged のメンバ
        /// <summary>
        /// プロパティ変更時に発生します。
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion INotifyPropertyChanged のメンバ

        /// <summary>
        /// プロパティ変更イベント PropertyChanged を発行します。
        /// </summary>
        /// <param name="propertyName">プロパティ名を指定します。</param>
        protected virtual void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            var h = this.PropertyChanged;
            if (h != null) h(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// プロパティ変更をおこない、プロパティ変更イベント PropertyChanged を発行します。
        /// </summary>
        /// <typeparam name="T">プロパティの型</typeparam>
        /// <param name="target">変更するプロパティの実体を参照渡しとして指定します。</param>
        /// <param name="value">変更後の値を指定します。</param>
        /// <param name="propertyName">プロパティ名を指定します。</param>
        /// <returns>プロパティ値に変更があった場合に true を返します。</returns>
        protected virtual bool SetProperty<T>(ref T target, T value, [CallerMemberName]string propertyName = null)
        {
            if (object.Equals(target, value))
                return false;
            target = value;
            RaisePropertyChanged(propertyName);
            return true;
        }
    }
}
