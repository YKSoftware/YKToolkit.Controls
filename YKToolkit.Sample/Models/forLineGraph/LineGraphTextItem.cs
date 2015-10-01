namespace YKToolkit.Models.SampleForLineGraph
{
    using System;
    using YKToolkit.Bindings;

    /// <summary>
    /// 折れ線グラフのテキスト設定を表します。
    /// </summary>
    public class LineGraphTextItem : NotificationObject
    {
        #region コンストラクタ
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public LineGraphTextItem()
            : this(null)
        {
        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="text">テキストを指定します。</param>
        public LineGraphTextItem(string text)
            : this(text, 14.0)
        {
        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="text">テキストを指定します。</param>
        /// <param name="fontSize">フォントサイズを指定します。</param>
        public LineGraphTextItem(string text, double fontSize)
        {
            if (fontSize < 0.0)
                throw new ArgumentException("フォントサイズは正の値である必要があります。");

            this.Text = text;
            this.FontSize = fontSize;
        }
        #endregion コンストラクタ

        private string _text;
        /// <summary>
        /// テキストを取得または設定します。
        /// </summary>
        public string Text
        {
            get { return this._text; }
            set { SetProperty(ref this._text, value); }
        }

        private double _fontSize;
        /// <summary>
        /// フォントサイズを取得または設定します。
        /// </summary>
        public double FontSize
        {
            get { return this._fontSize; }
            set { SetProperty(ref this._fontSize, value); }
        }
    }
}
