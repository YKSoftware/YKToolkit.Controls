namespace YKToolkit.Sample.ViewModels
{
    using System.Windows.Media;
    using YKToolkit.Controls;

    /// <summary>
    /// HsvColor サンプル用 View に対する ViewModel を表します。
    /// </summary>
    public class HsvColorViewModel : ViewModelBase
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public HsvColorViewModel()
        {
            this.SelectedColor = Colors.Red;
        }

        #region 公開プロパティ

        private Color _selectedColor;
        /// <summary>
        /// 選択中の色情報を取得または設定します。
        /// </summary>
        public Color SelectedColor
        {
            get { return this._selectedColor; }
            set
            {
                if (SetProperty(ref this._selectedColor, value))
                {
                    if (!this._isHsvChangedFromUI)
                    {
                        this._isChangedFromUI = false;
                        var hsvColor = HsvColor.FromRgb(this._selectedColor);
                        this.H = (int)hsvColor.H;
                        this.S = (int)(hsvColor.S * 255);
                        this.V = (int)(hsvColor.V * 255);
                        this._isChangedFromUI = true;
                    }
                }
            }
        }

        private int _h;
        /// <summary>
        /// 色相の値を取得または設定します。
        /// </summary>
        public int H
        {
            get { return this._h; }
            set
            {
                if (SetProperty(ref this._h, value))
                {
                    this._isHsvChangedFromUI = true;
                    this.HsvColor = new HsvColor(this.H, this.S / 255.0, this.V / 255.0);
                    this._isHsvChangedFromUI = false;
                }
            }
        }

        private int _s;
        /// <summary>
        /// 彩度の値を取得または設定します。
        /// </summary>
        public int S
        {
            get { return this._s; }
            set
            {
                if (SetProperty(ref this._s, value))
                {
                    this._isHsvChangedFromUI = true;
                    this.HsvColor = new HsvColor(this.H, this.S / 255.0, this.V / 255.0);
                    this._isHsvChangedFromUI = false;
                }
            }
        }

        private int _v;
        /// <summary>
        /// 明度の値を取得または設定します。
        /// </summary>
        public int V
        {
            get { return this._v; }
            set
            {
                if (SetProperty(ref this._v, value))
                {
                    this._isHsvChangedFromUI = true;
                    this.HsvColor = new HsvColor(this.H, this.S / 255.0, this.V / 255.0);
                    this._isHsvChangedFromUI = false;
                }
            }
        }

        #endregion 公開プロパティ

        private HsvColor _hsvColor;
        /// <summary>
        /// HSV 表現による色情報を取得または設定します。
        /// </summary>
        private HsvColor HsvColor
        {
            get { return this._hsvColor; }
            set
            {
                this._hsvColor = value;

                if (this._isChangedFromUI)
                {
                    this.SelectedColor = this._hsvColor.ColorFromHsv();
                }
            }
        }

        /// <summary>
        /// 選択色が UI から変更されたかどうかを保持します。
        /// </summary>
        private bool _isChangedFromUI = true;

        /// <summary>
        /// HSV 情報が UI から変更されたかどうかを保持します。
        /// </summary>
        private bool _isHsvChangedFromUI = false;
    }
}
