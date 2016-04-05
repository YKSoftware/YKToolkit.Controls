namespace YKToolkit.Controls
{
    using System;

    /// <summary>
    /// FileTreeViewItem をダブルクリックしたときのイベント引数を表します。
    /// </summary>
    public class FileTreeViewItemDoubleClickEventArgs : EventArgs
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        /// <param name="fullPath">フルパスを指定します。</param>
        public FileTreeViewItemDoubleClickEventArgs(string fullPath)
        {
            this.FullPath = fullPath;
        }

        /// <summary>
        /// フルパスを取得します。
        /// </summary>
        public string FullPath { get; private set; }
    }
}
