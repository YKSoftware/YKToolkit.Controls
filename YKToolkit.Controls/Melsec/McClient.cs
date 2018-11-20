namespace System.Net.Sockets
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// MC プロトコルを用いた通信をおこなうクライアント機能を提供します。
    /// </summary>
    public class McClient : IDisposable
    {
        #region Connected イベント

        /// <summary>
        /// Connected イベントハンドラのデリゲートを表します。
        /// </summary>
        public delegate void OnConnected(McClient client);

        /// <summary>
        /// 接続したときに発生します。
        /// </summary>
        public event OnConnected Connected;

        /// <summary>
        /// Connected イベントを発行します。
        /// </summary>
        private void RaiseConnected()
        {
            var h = this.Connected;
            if (h != null) h(this);
        }

        #endregion Connected イベント

        #region Closed イベント

        /// <summary>
        /// Closed イベントハンドラのデリゲートを表します。
        /// </summary>
        public delegate void OnClosed(McClient client);

        /// <summary>
        /// 切断したときに発生します。
        /// </summary>
        public event OnClosed Closed;

        /// <summary>
        /// Closed イベントを発行します。
        /// </summary>
        private void RaiseClosed()
        {
            var h = this.Closed;
            if (h != null) h(this);
        }

        #endregion Closed イベント

        /// <summary>
        /// 非同期で指定のホストへの接続を開始します。
        /// </summary>
        /// <param name="hostName">接続先のホスト名を指定します。</param>
        /// <param name="port">接続先のポート番号を指定します。</param>
        /// <returns>接続処理をおこなう非同期タスク。</returns>
        public Task StartAsync(string hostName, int port)
        {
            return Task.Run(() =>
            {
                Start(hostName, port);
            });
        }

        /// <summary>
        /// 指定のホストへの接続を開始します。
        /// </summary>
        /// <param name="hostName">接続先のホスト名を指定します。</param>
        /// <param name="port">接続先のポート番号を指定します。</param>
        public void Start(string hostName, int port)
        {
            this._connection.Connected += _ => RaiseConnected();
            this._connection.Closed += () => RaiseClosed();

            this._connection.Start(hostName, port);
            this.HostName = hostName;
            this.Port = port;
            System.Diagnostics.Debug.WriteLine("接続処理を終了しました。");
        }

        /// <summary>
        /// 非同期でデバイスを一括読出しします。デバイス範囲によっては何回かに分割して一括読み出し処理をおこないます。
        /// </summary>
        /// <param name="devices">値を読み出すデバイス群を指定します。</param>
        /// <returns>一括読出しをおこなう非同期タスク。</returns>
        public Task ReadDeviceAsync(IEnumerable<IMcDevice> devices)
        {
            return Task.Run(() => ReadDevice(devices));
        }

        /// <summary>
        /// デバイスを一括読出しします。デバイス範囲によっては何回かに分割して一括読出し処理をおこないます。
        /// </summary>
        /// <param name="devices">値を読み出すデバイス群を指定します。</param>
        /// <returns>終了コードを返します。</returns>
        public McEndCode ReadDevice(IEnumerable<IMcDevice> devices)
        {
            if (this.IsDisposed)
                throw new ObjectDisposedException(ObjectDisposedExceptionMessage, null as Exception);

            // デバイス点数が少ない場合はワード単位のランダム読出し 1 回の通信で完結させる
            var endCode = ReadRandomDevice(devices);
            if (endCode == McEndCode.UnKnown)
            {
                // デバイス点数が多すぎる場合は各デバイスで一括読出しをおこなう

                var bitDevices = devices.OfType<IMcBitDevice>().ToArray();
                var wordDevices = devices.OfType<IMcWordDevice>().ToArray();
                var doubleWordDevices = devices.OfType<IMcDoubleWordDevice>().ToArray();

                // ビットデバイスの読み出し
                endCode = ReadDevice(bitDevices);

                // ワードデバイスの読み出し
                if (endCode == McEndCode.Success)
                    ReadDevice(wordDevices);

                // ダブルワードデバイスの読み出し
                if (endCode == McEndCode.Success)
                    ReadDevice(doubleWordDevices);

                return endCode;
            }

            return endCode;
        }

        /// <summary>
        /// ワード単位のランダム読み出し 1 回の通信をおこないます。
        /// </summary>
        /// <param name="devices"></param>
        /// <returns>終了コードを返します。データ点数が 1 回の通信で収まらない場合は <c>McEndCode.Unknown</c> を返します。</returns>
        public McEndCode ReadRandomDevice(IEnumerable<IMcDevice> devices)
        {
            if (this.IsDisposed)
                throw new ObjectDisposedException(ObjectDisposedExceptionMessage, null as Exception);

            var bitDevices = devices.OfType<IMcBitDevice>().ToArray();
            var wordDevices = devices.OfType<IMcWordDevice>().ToArray();
            var doubleWordDevices = devices.OfType<IMcDoubleWordDevice>().ToArray();
            var pair = this._connection.ReadDevice(bitDevices, wordDevices, doubleWordDevices);
            if (pair.Key == McEndCode.Success)
            {
                var wordLength = bitDevices.Length + wordDevices.Length;
                var length = wordLength + doubleWordDevices.Length;
                for (var i = 0; i < wordLength; i++)
                {
                    if (i < bitDevices.Length)
                    {
                        bitDevices[i].Value = pair.Value[i] != 0;
                    }
                    else //if (i < wordLength)
                    {
                        wordDevices[i - bitDevices.Length].Value = pair.Value[i];
                    }
                }

                var leaveValues = pair.Value.Skip(wordLength).Chunk(2).Select(x => BitConverter.ToInt32(x.SelectMany(y => BitConverter.GetBytes(y)).ToArray(), 0)).ToArray();
                for (var i = 0; i < doubleWordDevices.Length; i++)
                {
                    doubleWordDevices[i].Value = leaveValues[i];
                }
            }

            return pair.Key;
        }

        #region レジスタ記号毎に一括読出しを、場合によっていは分割して通信処理する公開メソッド

        /// <summary>
        /// ダブルワードデバイスを一括読出しします。デバイス範囲によっては何回かに分割して一括読出し処理をおこないます。
        /// </summary>
        /// <param name="doubleWordDevices">値を読み出すダブルワードデバイス群を指定します。</param>
        /// <returns>終了コードを返します。</returns>
        public McEndCode ReadDevice(IEnumerable<IMcDoubleWordDevice> doubleWordDevices)
        {
            if (this.IsDisposed)
                throw new ObjectDisposedException(ObjectDisposedExceptionMessage, null as Exception);

            // レジスタ記号とアドレスを使用して昇順で並べ替え、
            // McBitDeviceType 列挙型をキーとしてグルーピングすることで、レジスタ毎に読み出し処理できるようにする
            //     TSource : ビットデバイス群
            //     TKey    : レジスタ記号
            var groupingDevices = doubleWordDevices.OrderBy(x => x, IMcDeviceComparer.DefaultComparer).GroupBy(x => x.DeviceType);
            foreach (var group in groupingDevices)
            {
                // アドレスを 400 番毎に分類し、それぞれを一括読出しする
                //     TSource : 特定のレジスタ記号にグルーピングされたビットデバイス群
                //     TKey    : 0、400、800 …
                var orderedDevices = group.GroupBy(x => x.Address.MRound(400)).ToArray();
                foreach (var devices in orderedDevices)
                {
                    // 一括読出しする
                    var maxAddress = devices.Max(x => x.Address);
                    var minAddress = devices.Min(x => x.Address);
                    var dataNumbers = maxAddress - minAddress + 2;
                    var pair = ReadDoubleWordDevice(group.Key, minAddress, dataNumbers);
                    if (pair.Key != McEndCode.Success)
                    {
                        // 異常なときは後続処理をあきらめてエラーコードを返す
                        return pair.Key;
                    }

                    // 受信データをキャッシュする
                    foreach (var device in devices)
                    {
                        var i = (device.Address - minAddress) / 2;
                        device.Value = pair.Value[i];
                    }
                }
            }

            return McEndCode.Success;
        }

        /// <summary>
        /// ワードデバイスを一括読出しします。デバイス範囲によっては何回かに分割して一括読出し処理をおこないます。
        /// </summary>
        /// <param name="wordDevices">値を読み出すワードデバイス群を指定します。</param>
        /// <returns>終了コードを返します。</returns>
        public McEndCode ReadDevice(IEnumerable<IMcWordDevice> wordDevices)
        {
            if (this.IsDisposed)
                throw new ObjectDisposedException(ObjectDisposedExceptionMessage, null as Exception);

            // レジスタ記号とアドレスを使用して昇順で並べ替え、
            // McBitDeviceType 列挙型をキーとしてグルーピングすることで、レジスタ毎に読み出し処理できるようにする
            //     TSource : ビットデバイス群
            //     TKey    : レジスタ記号
            var groupingDevices = wordDevices.OrderBy(x => x, IMcDeviceComparer.DefaultComparer).GroupBy(x => x.DeviceType);
            foreach (var group in groupingDevices)
            {
                // アドレスを 900 番毎に分類し、それぞれを一括読出しする
                //     TSource : 特定のレジスタ記号にグルーピングされたビットデバイス群
                //     TKey    : 0、900、1,800 …
                var orderedDevices = group.GroupBy(x => x.Address.MRound(900)).ToArray();
                foreach (var devices in orderedDevices)
                {
                    // 一括読出しする
                    var maxAddress = devices.Max(x => x.Address);
                    var minAddress = devices.Min(x => x.Address);
                    var dataNumbers = maxAddress - minAddress + 1;
                    var pair = ReadWordDevice(group.Key, minAddress, dataNumbers);
                    if (pair.Key != McEndCode.Success)
                    {
                        // 異常なときは後続処理をあきらめてエラーコードを返す
                        return pair.Key;
                    }

                    // 受信データをキャッシュする
                    foreach (var device in devices)
                    {
                        var i = device.Address - minAddress;
                        device.Value = pair.Value[i];
                    }
                }
            }

            return McEndCode.Success;
        }

        /// <summary>
        /// ビットデバイスを一括読出しします。デバイス範囲によっては何回かに分割して一括読出し処理をおこないます。
        /// </summary>
        /// <param name="bitDevices">値を読み出すビットデバイス群を指定します。</param>
        /// <returns>終了コードを返します。</returns>
        public McEndCode ReadDevice(IEnumerable<IMcBitDevice> bitDevices)
        {
            if (this.IsDisposed)
                throw new ObjectDisposedException(ObjectDisposedExceptionMessage, null as Exception);

            // レジスタ記号とアドレスを使用して昇順で並べ替え、
            // McBitDeviceType 列挙型をキーとしてグルーピングすることで、レジスタ毎に読み出し処理できるようにする
            //     TSource : ビットデバイス群
            //     TKey    : レジスタ記号
            var groupingDevices = bitDevices.OrderBy(x => x, IMcDeviceComparer.DefaultComparer).GroupBy(x => x.DeviceType);
            foreach (var group in groupingDevices)
            {
                // アドレスを 10,000 番毎に分類し、それぞれを一括読出しする
                //     TSource : 特定のレジスタ記号にグルーピングされたビットデバイス群
                //     TKey    : 0、10,000、20,000 …
                var orderedDevices = group.GroupBy(x => x.Address.MRound(10000)).ToArray();
                foreach (var devices in orderedDevices)
                {
                    // 一括読出しする
                    var maxAddress = devices.Max(x => x.Address);
                    var minAddress = devices.Min(x => x.Address);
                    var dataNumbers = maxAddress - minAddress + 1;
                    var pair = ReadBitDevice(group.Key, minAddress, dataNumbers);
                    if (pair.Key != McEndCode.Success)
                    {
                        // 異常なときは後続処理をあきらめてエラーコードを返す
                        return pair.Key;
                    }

                    // 正常なときは受信データをキャッシュする
                    foreach (var device in devices)
                    {
                        var i = device.Address - minAddress;
                        device.Value = pair.Value[i];
                    }
                }
            }

            return McEndCode.Success;
        }

        #endregion レジスタ記号毎に一括読出しを、場合によっていは分割して通信処理する公開メソッド

        #region IMcDevice インターフェースを必要としない公開メソッド

        /// <summary>
        /// ビットデバイスの一括読出しをおこないます。ワードデバイスを読み出す場合は <see cref="ReadWordDevice"/>、ダブルワードデバイスを読み出す場合は <see cref="ReadDoubleWordDevice"/> を使用してください。
        /// </summary>
        /// <param name="type">デバイス種別を指定します。</param>
        /// <param name="topAddress">読み出すデバイスの先頭アドレスを指定します。</param>
        /// <param name="dataNumbers">読み出すデバイスのワード数を指定します。</param>
        /// <returns>終了コードと受信処理結果のペアを返します。</returns>
        public KeyValuePair<McEndCode, bool[]> ReadBitDevice(McBitDeviceType type, int topAddress, int dataNumbers)
        {
            if (this.IsDisposed)
                throw new ObjectDisposedException(ObjectDisposedExceptionMessage, null as Exception);

            return this._connection.ReadDevice((McWordDeviceType)type, topAddress, dataNumbers, McConnection.BitDeviceReader);
        }

        /// <summary>
        /// ワードデバイスの一括読出しをおこないます。ビットデバイスを読み出す場合は <see cref="ReadBitDevice"/>()、ダブルワードデバイスを読み出す場合は <see cref="ReadDoubleWordDevice"/>() を使用してください。
        /// </summary>
        /// <param name="type">デバイス種別を指定します。</param>
        /// <param name="topAddress">読み出すデバイスの先頭アドレスを指定します。</param>
        /// <param name="dataNumbers">読み出すデバイスのワード数を指定します。</param>
        /// <returns>終了コードと受信処理結果のペアを返します。</returns>
        public KeyValuePair<McEndCode, short[]> ReadWordDevice(McWordDeviceType type, int topAddress, int dataNumbers)
        {
            if (this.IsDisposed)
                throw new ObjectDisposedException(ObjectDisposedExceptionMessage, null as Exception);
            if (type.IsBitDevice())
                throw new ArgumentException("ワードデバイスを指定してください。", "type");

            return this._connection.ReadDevice(type, topAddress, dataNumbers, McConnection.WordDeviceReader);
        }

        /// <summary>
        /// ダブルワードデバイスの一括読出しをおこないます。ビットデバイスを読み出す場合は <see cref="ReadBitDevice"/>()、ワードデバイスを読み出す場合は <see cref="ReadWordDevice"/>() を使用してください。
        /// </summary>
        /// <param name="type">デバイス種別を指定します。</param>
        /// <param name="topAddress">読み出すデバイスの先頭アドレスを指定します。</param>
        /// <param name="dataNumbers">読み出すデバイスのワード数を指定します。読み出したワード数分をダブルワードとして扱います。ダブルワード 1 点につきワード数 2 個として指定してください。</param>
        /// <returns>終了コードと受信処理結果のペアを返します。</returns>
        public KeyValuePair<McEndCode, Int32[]> ReadDoubleWordDevice(McWordDeviceType type, int topAddress, int dataNumbers)
        {
            if (this.IsDisposed)
                throw new ObjectDisposedException(ObjectDisposedExceptionMessage, null as Exception);
            if (type.IsBitDevice())
                throw new ArgumentException("ワードデバイスを指定してください。", "type");

            var pair = this._connection.ReadDevice(type, topAddress, dataNumbers, McConnection.WordDeviceReader);
            Int32[] values = null;
            if (pair.Key == McEndCode.Success)
            {
                // ワード単位で読み出した結果をダブルワード単位に変換する
                values = pair.Value.Chunk(2).Select(x => BitConverter.ToInt32(x.SelectMany(y => BitConverter.GetBytes(y)).ToArray(), 0)).ToArray();
            }
            return new KeyValuePair<McEndCode, Int32[]>(pair.Key, values);
        }

        #endregion IMcDevice インターフェースを必要としない公開メソッド

        /// <summary>
        /// 接続先のホスト名を取得します。
        /// </summary>
        public string HostName { get; private set; }

        /// <summary>
        /// 接続先のポート番号を取得します。
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// TCP 通信による MC プロトコル操作用オブジェクト
        /// </summary>
        private readonly McConnection _connection = new McConnection();

        /// <summary>
        /// オブジェクト破棄済み例外メッセージ
        /// </summary>
        private const string ObjectDisposedExceptionMessage = "接続は既にクローズされています。新しいインスタンスを用意してください。";

        /// <summary>
        /// リソースを破棄します。
        /// </summary>
        public void Dispose()
        {
            if (!this.IsDisposed)
            {
                this._connection.Close();
                this.IsDisposed = true;
            }
        }

        /// <summary>
        /// オブジェクト破棄済みかどうかを取得します。
        /// </summary>
        public bool IsDisposed { get; private set; }
    }
}
