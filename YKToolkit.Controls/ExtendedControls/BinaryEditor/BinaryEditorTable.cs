/////////////////////////////////////////////////////////////////////////////////////
// ■ 1 バイト表示
//          1         2         3         4         5         6         7         8         9         A         
// 1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789
//              +00  +01  +02  +03  +04  +05  +06  +07  +08  +09  +0A  +0B  +0C  +0D  +0E  +0F       ASCII      
// 0x12345678  -128 -128 -128 -128 -128 -128 -128 -128 -128 -128 -128 -128 -128 -128 -128 -128  ................
// 
// 1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789
//             +00 +01 +02 +03 +04 +05 +06 +07 +08 +09 +0A +0B +0C +0D +0E +0F       ASCII      
// 0x12345678   FF  FF  FF  FF  FF  FF  FF  FF  FF  FF  FF  FF  FF  FF  FF  FF  ................
// 
// ■ 2 バイト表示
// 1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789
//                +00     +02     +04     +06     +08     +0A     +0C     +0E        ASCII      
// 0x12345678  -32,768 -32,768 -32,768 -32,768 -32,768 -32,768 -32,768 -32,768  ................
// 
// 1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789
//              +00  +02  +04  +06  +08  +0A  +0C  +0E       ASCII      
// 0x12345678  FFFF FFFF FFFF FFFF FFFF FFFF FFFF FFFF  ................
// 
// ■ 4 バイト表示
// 1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789
//                    +00            +04            +08            +0C           ASCII      
// 0x12345678  -2,147,483,648 -2,147,483,648 -2,147,483,648 -2,147,483,648  ................
// 
// 1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789
//                +00      +04      +08      +0C         ASCII      
// 0x12345678  FFFFFFFF FFFFFFFF FFFFFFFF FFFFFFFF  ................
// 
// ■ 8 バイト表示
// 1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789
//                         +00                        +08                  ASCII      
// 0x12345678  -9,223,372,036,854,775,808 -9,223,372,036,854,775,808  ................
// 
// 1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789
//                   +00              +08              ASCII      
// 0x12345678  FFFFFFFFFFFFFFFF FFFFFFFFFFFFFFFF  ................
/////////////////////////////////////////////////////////////////////////////////////

namespace YKToolkit.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// バイナリエディタ用テーブルを表します。
    /// </summary>
    internal class BinaryEditorTable : FrameworkElement
    {
        #region コンストラクタ
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public BinaryEditorTable()
        {
            ThemeManager.Instance.ThemeChanged += OnThemeChanged;
            this.Typeface = new Typeface("consolas");
        }
        #endregion コンストラクタ

        #region Data 依存関係プロパティ
        /// <summary>
        /// Data 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty DataProperty = BinaryEditor.DataProperty.AddOwner(typeof(BinaryEditorTable), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDataPropertyChanged));

        /// <summary>
        /// データを取得または設定します。
        /// </summary>
        public ObservableCollection<byte> Data
        {
            get { return (ObservableCollection<byte>)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        /// <summary>
        /// Data プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnDataPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var control = sender as BinaryEditorTable;
            if (control == null)
                return;

            control.OnDataPropertyChanged(e.OldValue as ObservableCollection<byte>, e.NewValue as ObservableCollection<byte>);
        }

        /// <summary>
        /// Data プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldValue">変更前の値</param>
        /// <param name="newValue">変更後の値</param>
        private void OnDataPropertyChanged(ObservableCollection<byte> oldValue, ObservableCollection<byte> newValue)
        {
            if (oldValue != null)
            {
                oldValue.CollectionChanged -= OnDataCollectionChanged;
            }
            if (newValue != null)
            {
                newValue.CollectionChanged += OnDataCollectionChanged;
            }

            this.InvalidateVisual();
        }
        #endregion Data 依存関係プロパティ

        #region TopAddress 依存関係プロパティ
        /// <summary>
        /// TopAddress 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty TopAddressProperty = BinaryEditor.TopAddressProperty.AddOwner(typeof(BinaryEditorTable), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// 先頭アドレスを取得または設定します。
        /// </summary>
        public int TopAddress
        {
            get { return (int)GetValue(TopAddressProperty); }
            set { SetValue(TopAddressProperty, value); }
        }
        #endregion TopAddress 依存関係プロパティ

        #region AddressOffset 依存関係プロパティ
        /// <summary>
        /// AddressOffset 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty AddressOffsetProperty = BinaryEditor.AddressOffsetProperty.AddOwner(typeof(BinaryEditorTable), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// アドレスオフセットを取得または設定します。
        /// </summary>
        public int AddressOffset
        {
            get { return (int)GetValue(AddressOffsetProperty); }
            set { SetValue(AddressOffsetProperty, value); }
        }
        #endregion AddressOffset 依存関係プロパティ

        #region VisibleLines 依存関係プロパティ
        /// <summary>
        /// VisibleLines 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty VisibleLinesProperty = BinaryEditor.VisibleLinesProperty.AddOwner(typeof(BinaryEditorTable), new FrameworkPropertyMetadata(16, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// 表示行数を取得または設定します。
        /// </summary>
        public int VisibleLines
        {
            get { return (int)GetValue(VisibleLinesProperty); }
            set { SetValue(VisibleLinesProperty, value); }
        }
        #endregion VisibleLines 依存関係プロパティ

        #region FontSize 依存関係プロパティ
        /// <summary>
        /// FontSize 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty FontSizeProperty = TextBlock.FontSizeProperty.AddOwner(typeof(BinaryEditorTable), new FrameworkPropertyMetadata(16.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// フォントサイズを取得または設定します。
        /// </summary>
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }
        #endregion FontSize 依存関係プロパティ

        #region DataStyle 依存関係プロパティ
        /// <summary>
        /// DataStyle 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty DataStyleProperty = BinaryEditor.DataStyleProperty.AddOwner(typeof(BinaryEditorTable), new FrameworkPropertyMetadata(DataStyles.Byte, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// データ表現を取得または設定します。
        /// </summary>
        public DataStyles DataStyle
        {
            get { return (DataStyles)GetValue(DataStyleProperty); }
            set { SetValue(DataStyleProperty, value); }
        }
        #endregion DataStyle 依存関係プロパティ

        #region NumStyle 依存関係プロパティ
        /// <summary>
        /// NumStyle 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty NumStyleProperty = BinaryEditor.NumStyleProperty.AddOwner(typeof(BinaryEditorTable), new FrameworkPropertyMetadata(NumStyles.Hexadecimal, FrameworkPropertyMetadataOptions.AffectsMeasure));

        /// <summary>
        /// 数値表現を取得または設定します。
        /// </summary>
        public NumStyles NumStyle
        {
            get { return (NumStyles)GetValue(NumStyleProperty); }
            set { SetValue(NumStyleProperty, value); }
        }
        #endregion NumStyle 依存関係プロパティ

        #region IsMonitoringMode 依存関係プロパティ
        /// <summary>
        /// IsMonitoringMode 依存関係プロパティの定義
        /// </summary>
        public static readonly DependencyProperty IsMonitoringModeProperty = DependencyProperty.Register("IsMonitoringMode", typeof(bool), typeof(BinaryEditorTable), new PropertyMetadata(false));

        /// <summary>
        /// モニタモードかどうかを取得または設定します。
        /// </summary>
        public bool IsMonitoringMode
        {
            get { return (bool)GetValue(IsMonitoringModeProperty); }
            set { SetValue(IsMonitoringModeProperty, value); }
        }
        #endregion IsMonitoringMode 依存関係プロパティ

        #region 描画関連オーバーライド

        /// <summary>
        /// サイズを計測する処理のオーバーライド
        /// </summary>
        /// <param name="availableSize">親要素から与えられるサイズ</param>
        /// <returns>必要なサイズ</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            var temp = new FormattedText("F", CultureInfo.CurrentUICulture, FlowDirection.LeftToRight, this.Typeface, this.FontSize, null);
            this._charSize = new Size(temp.Width, temp.Height);

            //var line = double.IsInfinity(availableSize.Height) ? 16 : (int)(availableSize.Height / this._charSize.Height);
            //this.VisibleLines = line > 1 ? line : 1;

            // 1 バイト表示の幅と高さに合わせてサイズを計測する
            var charNumber = 0;
            switch (this.DataStyle)
            {
                default:
                case DataStyles.Byte: charNumber = this.NumStyle == NumStyles.Hexadecimal ? 93 : 109; break;
                case DataStyles.Word: charNumber = this.NumStyle == NumStyles.Hexadecimal ? 69 : 93; break;
                case DataStyles.DoubleWord: charNumber = this.NumStyle == NumStyles.Hexadecimal ? 65 : 89; break;
                case DataStyles.QuadWord: charNumber = this.NumStyle == NumStyles.Hexadecimal ? 63 : 83; break;
            }
            var width = double.IsNaN(this.Width) ? charNumber * this._charSize.Width : this.Width;
            this._extent = new Size(width, (this.VisibleLines + 1) * this._charSize.Height);

            return this._extent;
        }

        /// <summary>
        /// 子要素を配置する処理のオーバーライド
        /// </summary>
        /// <param name="finalSize">親要素から与えられるサイズ</param>
        /// <returns>子要素の配置に必要なサイズ</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            return finalSize;
        }

        /// <summary>
        /// 描画処理のオーバーライド
        /// </summary>
        /// <param name="drawingContext">描画先のコンテキストが与えられます。</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            //base.OnRender(drawingContext);

            var data = this.Data != null ? this.Data : new ObservableCollection<byte>();
            var extent = this._extent;

            var color = SystemColors.ControlTextColor;
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                var colorResource = this.FindResource("ForegroundColor");
                if ((colorResource != null) && (colorResource.GetType() == typeof(Color)))
                    color = (Color)Convert.ChangeType(colorResource, typeof(Color));
            }
            var foreground = new SolidColorBrush(color);
            foreground.Freeze();

            var dataStyle = this.DataStyle;
            if (data.Count % (int)this.DataStyle != 0)
                dataStyle = DataStyles.Byte;

            // 何バイト表示かによってデータの最大文字数が変化する
            var w_max = this._charSize.Width;
            switch (dataStyle)
            {
                case DataStyles.Byte:
                    if (this.NumStyle == NumStyles.Hexadecimal)
                        w_max *= 3;
                    else
                        w_max *= 4;
                    break;
                case DataStyles.Word:
                    if (this.NumStyle == NumStyles.Hexadecimal)
                        w_max *= 4;
                    else
                        w_max *= 7;
                    break;
                case DataStyles.DoubleWord:
                    if (this.NumStyle == NumStyles.Hexadecimal)
                        w_max *= 8;
                    else
                        w_max *= 14;
                    break;
                default:
                case DataStyles.QuadWord:
                    if (this.NumStyle == NumStyles.Hexadecimal)
                        w_max *= 16;
                    else
                        w_max *= 26;
                    break;
            }
            var w = this._charSize.Width;
            var h = this._charSize.Height;
            Point pt;

            #region カーソル表示のための設定を更新する
            // 1 データ分の領域のサイズ
            this.DataUnitSize = new Size(w + w_max, h);
            // データ表示領域の原点
            this.DataOrigin = new Point(11 * w, h);
            #endregion カーソル表示のための設定を更新する

            #region ヘッダ部を描画
            pt = new Point(12 * w + w_max / 2, 0);
            foreach (var x in Enumerable.Range(0, 16 / (int)dataStyle).Select(i => (int)dataStyle * i))
            {
                var header = new FormattedText("+" + x.ToString("X02"), CultureInfo.CurrentUICulture, FlowDirection.LeftToRight, this.Typeface, this.FontSize, foreground);
                header.TextAlignment = TextAlignment.Center;
                drawingContext.DrawText(header, pt);
                pt.Offset(w + w_max, 0);
            }
//            if (this.DataStyle == DataStyles.Byte)
            {
                pt.Offset(9 * w - w_max / 2.0, 0);
                var text = new FormattedText("ASCII", CultureInfo.CurrentUICulture, FlowDirection.LeftToRight, this.Typeface, this.FontSize, foreground);
                text.TextAlignment = TextAlignment.Center;
                drawingContext.DrawText(text, pt);
            }
            #endregion ヘッダ部を描画

            #region データ部を描画
            if (data.Count > 0)
            {
                pt = new Point(0, h);
                var address = this.TopAddress;
                while (pt.Y < this._extent.Height)
                {
                    // アドレス部
                    var str = "0x" + (address + this.AddressOffset).ToString("X08");
                    var text = new FormattedText(str, CultureInfo.CurrentUICulture, FlowDirection.LeftToRight, this.Typeface, this.FontSize, foreground);
                    drawingContext.DrawText(text, pt);

                    #region 16 個のデータを描画
                    var pt2 = new Point(12 * w + w_max, pt.Y);
                    var line = data.Count - address < 16 ? data.Skip(address).ToArray() : data.Skip(address).Take(16).ToArray();

                    // ASCII
                    for (var i = 0; i < line.Length; i += (int)DataStyles.Byte)
                    {
                        if ((0x20 <= line[i]) && (line[i] <= 0x7f))
                            str = System.Text.Encoding.ASCII.GetString(new byte[] { line[i] });
                        else
                            str = ".";
                        text = new FormattedText(str, CultureInfo.CurrentUICulture, FlowDirection.LeftToRight, this.Typeface, this.FontSize, foreground);
                        drawingContext.DrawText(text, new Point((13 + i) * this._charSize.Width + (this._charSize.Width + w_max) * 16 / (int)this.DataStyle, pt2.Y));
                    }

                    // バイナリデータ部
                    for (var i = 0; i < line.Length; i += (int)dataStyle)
                    {
                        var bytes = line.Skip(i).Take((int)dataStyle).Reverse().ToArray();
                        switch (bytes.Length)
                        {
                            case 1:
                                if (this.NumStyle == NumStyles.Hexadecimal)
                                {
                                    str = line[i].ToString("X2");
                                }
                                else if (this.NumStyle == NumStyles.SignedDecimal)
                                {
                                    str = ((sbyte)line[i]).ToString("N0");
                                }
                                else
                                {
                                    str = line[i].ToString("N0");
                                }
                                break;
                            case 2:
                                if (this.NumStyle == NumStyles.Hexadecimal)
                                {
                                    str = string.Concat(bytes.Reverse().Select(x => x.ToString("X2")).ToArray());
                                }
                                else if (this.NumStyle == NumStyles.SignedDecimal)
                                {
                                    str = BitConverter.ToInt16(bytes, 0).ToString("N0");
                                }
                                else
                                {
                                    str = BitConverter.ToUInt16(bytes, 0).ToString("N0");
                                }
                                break;
                            case 4:
                                if (this.NumStyle == NumStyles.Hexadecimal)
                                {
                                    str = string.Concat(bytes.Reverse().Select(x => x.ToString("X2")).ToArray());
                                }
                                else if (this.NumStyle == NumStyles.SignedDecimal)
                                {
                                    str = BitConverter.ToInt32(bytes, 0).ToString("N0");
                                }
                                else
                                {
                                    str = BitConverter.ToUInt32(bytes, 0).ToString("N0");
                                }
                                break;
                            case 8:
                                if (this.NumStyle == NumStyles.Hexadecimal)
                                {
                                    str = string.Concat(bytes.Reverse().Select(x => x.ToString("X2")).ToArray());
                                }
                                else if (this.NumStyle == NumStyles.SignedDecimal)
                                {
                                    str = BitConverter.ToInt64(bytes, 0).ToString("N0");
                                }
                                else
                                {
                                    str = BitConverter.ToUInt64(bytes, 0).ToString("N0");
                                }
                                break;
                            default:
                                break;
                        }   // end of switch

                        text = new FormattedText(str, CultureInfo.CurrentUICulture, FlowDirection.LeftToRight, this.Typeface, this.FontSize, foreground);
                        text.TextAlignment = TextAlignment.Right;
                        drawingContext.DrawText(text, pt2);
                        pt2.Offset(w + w_max, 0);
                    }
                    #endregion 16 個のデータを描画

                    #region 次の行へいくための前処理
                    pt.Offset(0, h);

                    address += 0x10;
                    if (address > data.Count - (this.IsMonitoringMode ? 1 : 0))
                        break;

                    if (double.IsPositiveInfinity(extent.Height))
                    {
                        if (address > this.TopAddress + 0x0100)
                            break;
                    }
                    #endregion 次の行へいくための前処理
                }   // end of while
            }
            #endregion データ部を描画
        }

        #endregion 描画関連オーバーライド

        #region イベントハンドラ

        private void OnThemeChanged(object sender, EventArgs e)
        {
            this.InvalidateVisual();
        }

        /// <summary>
        /// データコレクション要素変更イベントハンドラ
        /// </summary>
        /// <param name="sender">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        void OnDataCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.InvalidateVisual();
        }

        #endregion イベントハンドラ

        #region internal プロパティ

        private Point _dataOrigin;
        /// <summary>
        /// データ表示領域の左上座標を取得します。
        /// </summary>
        internal Point DataOrigin
        {
            get { return this._dataOrigin; }
            private set { this._dataOrigin = value; }
        }

        private Size _dataUnitSize;
        /// <summary>
        /// 1 データ分の領域のサイズを取得します。
        /// </summary>
        internal Size DataUnitSize
        {
            get { return this._dataUnitSize; }
            private set { this._dataUnitSize = value; }
        }

        private Typeface _typeface;
        /// <summary>
        /// 文字列の書式
        /// </summary>
        internal Typeface Typeface
        {
            get { return this._typeface; }
            private set { this._typeface = value; }
        }

        #endregion internal プロパティ

        #region private フィールド

        /// <summary>
        /// 表示領域
        /// </summary>
        private Size _extent;

        /// <summary>
        /// 1 文字のサイズ
        /// </summary>
        private Size _charSize;

        /// <summary>
        /// 書き込みデータとアドレスを紐付けるディクショナリ
        /// キー : 書き込み先インデックス
        /// 値   : 書き込むデータ
        /// </summary>
        private Dictionary<int, byte> _writeDataDictionary = new Dictionary<int, byte>();

        #endregion private フィールド

        #region ヘルパ

        #endregion ヘルパ
    }
}
