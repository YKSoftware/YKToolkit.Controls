namespace YKToolkit.Controls
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// FileTreeViewItem をマウスボタンで押したときのイベント引数を表します。
    /// </summary>
    public class FileTreeViewItemMouseEventArgs : EventArgs
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="fullPath">フルパスを指定します。</param>
        /// <param name="buttonState">マウスボタンの状態を指定します。</param>
        public FileTreeViewItemMouseEventArgs(string fullPath, MouseButtonState buttonState)
        {
            this.FullPath = fullPath;
            this.ButtonState = buttonState;
        }

        /// <summary>
        /// フルパスを取得します。
        /// </summary>
        public string FullPath { get; private set; }

        /// <summary>
        /// マウスボタンの状態を取得します。
        /// </summary>
        public MouseButtonState ButtonState { get; private set; }
    }
}
