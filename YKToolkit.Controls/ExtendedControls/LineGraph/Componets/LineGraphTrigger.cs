namespace YKToolkit.Controls
{
    /// <summary>
    /// 折れ線グラフ用の各種トリガを提供します。
    /// </summary>
    public class LineGraphTrigger
    {
        /// <summary>
        /// インスタンスを取得します。
        /// </summary>
        public static LineGraphTrigger Instance { get; private set; }

        /// <summary>
        /// 静的なコンストラクタ
        /// </summary>
        static LineGraphTrigger()
        {
            Instance = new LineGraphTrigger();
        }

        /// <summary>
        /// private なコンストラクタを定義することで外部からのインスタンス生成を抑止します。
        /// </summary>
        private LineGraphTrigger()
        {
        }

        /// <summary>
        /// DrawCommandReceived イベントハンドラのデリゲートを表します。
        /// </summary>
        /// <param name="key">キー名を指定します。</param>
        public delegate void OnDrawCommandReceived(string key);

        /// <summary>
        /// 描画を指示されたときに発生します。
        /// </summary>
        public event OnDrawCommandReceived DrawCommandReceived;

        /// <summary>
        /// 既定のキー "GraphControl" を持つ折れ線グラフコントロールに再描画を指示します。
        /// </summary>
        public static void ReDraw()
        {
            ReDraw("GraphControl");
        }

        /// <summary>
        /// 再描画を指示します。
        /// </summary>
        /// <param name="key">再描画するグラフに対するキー名を指定します。</param>
        public static void ReDraw(string key)
        {
            var h = Instance.DrawCommandReceived;
            if (h != null) h(key);
        }
    }
}
