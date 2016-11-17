namespace YKToolkit.Sample.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using YKToolkit.Controls;
    using YKToolkit.Bindings;

    public class BinaryEditorViewModel : ViewModelBase
    {
        public BinaryEditorViewModel()
        {
            var source = Enumerable.Range(-1, 65536).Select(x => BitConverter.GetBytes((ulong)x).Reverse());
            //var source = new byte[][] { new byte[] { 0x7f, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff } };
            //var source = new byte[][] { new byte[] { 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, new byte[] { 0x00, 0x01 } };
            var data = new ObservableCollection<byte>();
            foreach (var bb in source)
            {
                foreach (var b in bb)
                {
                    data.Add(b);
                }
            }

            this.Data = data;
            this.Data.CollectionChanged += (s, e) =>
            {
                if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
                {
                    foreach (byte item in e.NewItems)
                    {
                        System.Diagnostics.Debug.WriteLine("0x" + item.ToString("X2"));
                    }
                }
            };
        }

        private ObservableCollection<byte> _data;
        /// <summary>
        /// サンプルデータを取得または設定します。
        /// </summary>
        public ObservableCollection<byte> Data
        {
            get { return this._data; }
            set { SetProperty(ref this._data, value); }
        }

        private double _fontSize = 16.0;
        /// <summary>
        /// フォントサイズを取得または設定します。
        /// </summary>
        public double FontSize
        {
            get { return this._fontSize; }
            set { SetProperty(ref this._fontSize, value); }
        }

        private int _topAddress = 0;
        /// <summary>
        /// 先頭アドレスを取得または設定します。
        /// </summary>
        public int TopAddress
        {
            get { return this._topAddress; }
            set { SetProperty(ref this._topAddress, value); }
        }

        private int _addressOffset = 0;
        /// <summary>
        /// アドレスオフセットを取得または設定します。
        /// </summary>
        public int AddressOffset
        {
            get { return this._addressOffset; }
            set { SetProperty(ref this._addressOffset, value); }
        }

        private int _visibleLines = 17;
        /// <summary>
        /// 表示行数を取得または設定します。
        /// </summary>
        public int VisibleLines
        {
            get { return this._visibleLines; }
            set { SetProperty(ref this._visibleLines, value); }
        }

        private DataStyles _dataStyle = DataStyles.Byte;
        /// <summary>
        /// データ表現を取得または設定します。
        /// </summary>
        public DataStyles DataStyle
        {
            get { return this._dataStyle; }
            set { SetProperty(ref this._dataStyle, value); }
        }

        private NumStyles _numStyle = NumStyles.Hexadecimal;
        /// <summary>
        /// 数値表現を取得または設定します。
        /// </summary>
        public NumStyles NumStyle
        {
            get { return this._numStyle; }
            set { SetProperty(ref this._numStyle, value); }
        }

        private int _selectedAddress;
        /// <summary>
        /// 選択中のアドレスを取得または設定します。
        /// </summary>
        public int SelectedAddress
        {
            get { return this._selectedAddress; }
            set { SetProperty(ref this._selectedAddress, value); }
        }

        private bool _isMonitoringMode;
        /// <summary>
        /// モニタリングモードかどうかを取得または設定します。
        /// </summary>
        public bool IsMonitoringMode
        {
            get { return this._isMonitoringMode; }
            set { SetProperty(ref this._isMonitoringMode, value); }
        }

        private DelegateCommand _changeCommand;
        public DelegateCommand ChangeCommand
        {
            get
            {
                return this._changeCommand ?? (this._changeCommand = new DelegateCommand(_ =>
                {
                    this.Data[1] ^= 1;
                }));
            }
        }
    }
}
