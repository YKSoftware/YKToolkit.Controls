namespace YKToolkit.Controls
{
    using System.Windows.Media;

    /// <summary>
    /// ColorPicker コントロールのアイテムひとつを表します。
    /// </summary>
    internal class ColorPickerItem
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="color">色を指定します。</param>
        public ColorPickerItem(Color color)
        {
            this.Color = color;
            this.Name = string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="name">アイテム名を指定します。null の場合はカラーコードがセットされます。</param>
        /// <param name="colorCode">カラーコードを指定します。例：黒色の場合は "FF000000"。半透明の赤の場合は "80FF0000"</param>
        public ColorPickerItem(string name, string colorCode)
        {
            this.Color = ColorFromCode(colorCode);
            this.Name = string.IsNullOrWhiteSpace(name) ? "#" + colorCode : name;
        }

        /// <summary>
        /// アイテム名を取得または設定します。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 色情報を取得または設定します。
        /// </summary>
        public Color Color { get; set; }

        public static Color ColorFromCode(string colorCode)
        {
            var colorValue = int.Parse(colorCode, System.Globalization.NumberStyles.HexNumber);
            var a = (byte)((colorValue >> 24) & 0x00ff);
            var r = (byte)((colorValue >> 16) & 0x00ff);
            var g = (byte)((colorValue >> 8) & 0x00ff);
            var b = (byte)(colorValue & 0x00ff);

            return Color.FromArgb(a, r, g, b);
        }
    }
}
