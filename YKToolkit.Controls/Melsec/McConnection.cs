namespace System.Net.Sockets
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class McConnection : IDisposable
    {
        #region Connected イベント

        /// <summary>
        /// Connected イベントハンドラのデリゲートを表します。
        /// </summary>
        public delegate void OnConnected(McConnection connection);

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
        public delegate void OnClosed();

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
            if (h != null) h();
        }

        #endregion Closed イベント

        #region ErrorOccurred イベント

        /// <summary>
        /// ErrorOccurred イベントハンドラのデリゲートを表します。
        /// </summary>
        /// <param name="errorCode">ソケットエラーのエラーコードを指定します。</param>
        /// <param name="message">エラーメッセージを指定します。</param>
        public delegate void OnErrorOccurred(SocketError errorCode, string message);

        /// <summary>
        /// ソケットエラーをハンドリングしたときに発生します。
        /// </summary>
        public event OnErrorOccurred ErrorOccurred;

        /// <summary>
        /// ErrorOccurred イベントを発行します。
        /// </summary>
        /// <param name="errorCode">ソケットエラーのエラーコードを指定します。</param>
        /// <param name="message">エラーメッセージを指定します。</param>
        private void RaiseErrorOccurred(SocketError errorCode, string message)
        {
            var h = this.ErrorOccurred;
            if (h != null) h(errorCode, message);
        }

        #endregion ErrorOccurred イベント

        #region 接続と切断

        /// <summary>
        /// 通信接続のリトライまたは通信開始までにかける遅延時間 [ms]
        /// </summary>
        private const int RetryDelay = 1000;

        /// <summary>
        /// 指定したホストに接続し、通信処理を開始します。
        /// </summary>
        /// <param name="hostName">接続先のホスト名を指定します。</param>
        /// <param name="port">接続先のポート番号を指定します。</param>
        public async void Start(string hostName, int port)
        {
            if (this._isDisposed)
                throw new ObjectDisposedException("一度使用したオブジェクトは再利用できません。", null as Exception);
            if (this._isClosed)
                throw new InvalidOperationException("接続は既にクローズしています。");

            this.HostName = hostName;
            this.Port = port;

            // 接続を確立するまで接続を試みる
            await StartConnection();
            this.IsConnected = true;
            RaiseConnected();

            // 非同期で接続状態をポーリングする
            await EndConnection();
            this.IsConnected = false;

            // 切断状態を検出したら終了する
            Close();
        }

        /// <summary>
        /// 接続を確立するまで接続を試みます。
        /// </summary>
        /// <returns>非同期処理の接続操作を表すタスク。</returns>
        private Task StartConnection()
        {
            return Task.Run(async () =>
            {
                this._client = new TcpClient();
                while (this._client != null)
                {
                    try
                    {
                        if (this._client.Connected)
                            break;
                        this._client.Connect(this.HostName, this.Port);
                        this._stream = this._client.GetStream();

                        System.Diagnostics.Debug.WriteLine("接続しました。");
                    }
                    catch (SocketException ex)
                    {
                        RaiseErrorOccurred(ex.SocketErrorCode, ex.Message);
                    }
                    catch (Exception ex)
                    {
                        RaiseErrorOccurred(SocketError.SocketError, ex.Message);
                    }

                    // 接続のリトライまたは通信開始に 1 秒の間隔を空ける
                    await Task.Delay(McConnection.RetryDelay);
                }
            });
        }

        /// <summary>
        /// 接続が切断されるまで接続状態を監視します。
        /// </summary>
        /// <returns>非同期処理の切断操作を表すタスク。</returns>
        private Task EndConnection()
        {
            return Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        if (this._client.Client.Poll(1000, SelectMode.SelectRead) && (this._client.Available == 0))
                        {
                            System.Diagnostics.Debug.WriteLine("切断しました。");
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("切断しました。 - " + ex.Message);
                        break;
                    }
                }
            });
        }

        /// <summary>
        /// 接続を切断し、リソースを破棄します。
        /// </summary>
        public void Close()
        {
            if (!this._isClosed)
            {
                this._isClosed = true;
                if (this._client != null)
                {
                    this._client.Close();
                    this._client = null;
                }

                RaiseClosed();
            }
        }

        /// <summary>
        /// オブジェクトを破棄します。
        /// </summary>
        public void Dispose()
        {
            if (!this._isDisposed)
            {
                Close();
                this._isDisposed = true;
            }
        }

        /// <summary>
        /// 切断済みかどうか
        /// </summary>
        private bool _isClosed;

        /// <summary>
        /// オブジェクト破棄済みかどうか
        /// </summary>
        private bool _isDisposed;

        /// <summary>
        /// 接続中の TCP クライアント
        /// </summary>
        private TcpClient _client;

        /// <summary>
        /// 送受信用のストリーム
        /// </summary>
        private NetworkStream _stream;

        #endregion 接続と切断

        /// <summary>
        /// ワード単位のランダム読出しをおこないます。
        /// </summary>
        /// <param name="bitDevices">読み出すビットデバイス群を指定します。</param>
        /// <param name="wordDevices">読み出すワードデバイス群を指定します。</param>
        /// <param name="doubleWordDevices">読み出すダブルワードデバイス群を指定します。</param>
        /// <returns>終了コードと受信処理結果のペアを返します。データ数が 1 回の通信に収まらない場合は未定義のエラーコード <c>McEndCode.Unknown</c> を返します。</returns>
        public KeyValuePair<McEndCode, short[]> ReadDevice(IEnumerable<IMcBitDevice> bitDevices, IEnumerable<IMcWordDevice> wordDevices, IEnumerable<IMcDoubleWordDevice> doubleWordDevices)
        {
            if (this._isDisposed)
                throw new ObjectDisposedException("Dispose されたものは使用できません。", null as Exception);
            if (!this.IsConnected)
                throw new Exception("接続していません。");

            // データ数が 1 回の通信に収まらない場合は未定義のエラーコードを返す
            var bitDeviceLength = bitDevices.Count();
            var wordDeviceLength = wordDevices.Count();
            var doubleWordDeviceLength = doubleWordDevices.Count();
            var dataNumbers = bitDeviceLength + wordDeviceLength + 2 * doubleWordDeviceLength;
            if (dataNumbers > McConnection.MaxRandomReadLength)
                return new KeyValuePair<McEndCode, short[]>(McEndCode.UnKnown, null);

            // 要求データ作成
            var command = new byte[] { 0x03, 0x04 };
            var subCommand = new byte[] { 0x00, 0x00 };
            var wordLength = new byte[] { (byte)(bitDeviceLength + wordDeviceLength), (byte)doubleWordDeviceLength };
            var deviceBytes = bitDevices.SelectMany(x => x.Bytes).Concat(wordDevices.SelectMany(x => x.Bytes)).Concat(doubleWordDevices.SelectMany(x => x.Bytes)).ToArray();

            var request = new byte[command.Length + subCommand.Length + wordLength.Length + deviceBytes.Length];
            Array.Copy(command,     0, request,              0,                                         command.Length);
            Array.Copy(subCommand,  0, request, command.Length,                                         subCommand.Length);
            Array.Copy(wordLength,  0, request, command.Length + subCommand.Length,                     wordLength.Length);
            Array.Copy(deviceBytes, 0, request, command.Length + subCommand.Length + wordLength.Length, deviceBytes.Length);

            // 要求伝文生成
            var bytes = this._3eFrame.CreateRequestMessage(request);

            // 伝文送信
            this._stream.Write(bytes, 0, bytes.Length);

            // 応答受信
            var n = this._stream.Read(this._buffer, 0, this._buffer.Length);

            // サブヘッダ取得
            //ushort subHeader = 0;
            //if (n >= 2) subHeader = BitConverter.ToUInt16(this._buffer, 0);

            // アクセス経路取得
            //if (n >= 7) ...

            // 応答データ長
            ushort length = 0;
            if (n >= 9) length = BitConverter.ToUInt16(this._buffer, 7);

            // 終了コード
            McEndCode endCode = McEndCode.UnKnown;
            if (n >= 11) endCode = (McEndCode)BitConverter.ToUInt16(this._buffer, 9);

            // データ部取得
            short[] data = null;
            if (endCode == McEndCode.Success)
            {
                var dataLength = length - 2;    // 終了コード 2 バイト分を除いた値がデータ部の長さ
                if ((dataLength > 0) && (n == 11 + dataLength))
                {
                    data = WordDeviceReader(this._buffer, dataNumbers);
                }
            }

            return new KeyValuePair<McEndCode, short[]>(endCode, data);
        }

        /// <summary>
        /// ワード単位の一括読出しをおこないます。
        /// </summary>
        /// <typeparam name="T">受信処理結果の型を表します。</typeparam>
        /// <param name="type">デバイス種別を指定します。</param>
        /// <param name="topAddress">読み出すデバイスの先頭アドレスを指定します。</param>
        /// <param name="dataNumbers">読み出すデバイスのワード数を指定します。</param>
        /// <param name="func">受信処理を指定します。</param>
        /// <returns>終了コードと受信処理結果のペアを返します。</returns>
        public KeyValuePair<McEndCode, T> ReadDevice<T>(McWordDeviceType type, int topAddress, int dataNumbers, Func<IEnumerable<byte>, int, T> func)
        {
            if (this._isDisposed)
                throw new ObjectDisposedException("Dispose されたものは使用できません。", null as Exception);
            if (!this.IsConnected)
                throw new Exception("接続していません。");

            // 要求データ作成
            var command = new byte[] { 0x01, 0x04 };
            var subCommand = new byte[] { 0x00, 0x00 };
            var address = BitConverter.GetBytes(topAddress).Take(3).ToArray();
            var deviceCode = new byte[] { (byte)type };
            var dataNumbersBytes = BitConverter.GetBytes((ushort)dataNumbers);

            var request = new byte[command.Length + subCommand.Length + address.Length + deviceCode.Length + dataNumbersBytes.Length];
            Array.Copy(command,          0, request, 0,                                                                       command.Length);
            Array.Copy(subCommand,       0, request, command.Length,                                                          subCommand.Length);
            Array.Copy(address,          0, request, command.Length + subCommand.Length,                                      address.Length);
            Array.Copy(deviceCode,       0, request, command.Length + subCommand.Length + address.Length,                     deviceCode.Length);
            Array.Copy(dataNumbersBytes, 0, request, command.Length + subCommand.Length + address.Length + deviceCode.Length, dataNumbersBytes.Length);

            // 要求伝文生成
            var bytes = this._3eFrame.CreateRequestMessage(request);

            // 伝文送信
            this._stream.Write(bytes, 0, bytes.Length);

            // 応答受信
            var n = this._stream.Read(this._buffer, 0, this._buffer.Length);

            // サブヘッダ取得
            //ushort subHeader = 0;
            //if (n >= 2) subHeader = BitConverter.ToUInt16(this._buffer, 0);

            // アクセス経路取得
            //if (n >= 7) ...

            // 応答データ長
            ushort length = 0;
            if (n >= 9) length = BitConverter.ToUInt16(this._buffer, 7);

            // 終了コード
            McEndCode endCode = McEndCode.UnKnown;
            if (n >= 11) endCode = (McEndCode)BitConverter.ToUInt16(this._buffer, 9);

            // データ部取得
            T data = default(T);
            if (endCode == McEndCode.Success)
            {
                var dataLength = length - 2;    // 終了コード 2 バイト分を除いた値がデータ部の長さ
                if ((dataLength > 0) && (n == 11 + dataLength))
                {
                    data = func(this._buffer, dataNumbers);
                }
            }

            return new KeyValuePair<McEndCode, T>(endCode, data);
        }

        /// <summary>
        /// ランダム読出しの最大ワード数
        /// </summary>
        private const int MaxRandomReadLength = 192;

        /// <summary>
        /// ビットデバイス用受信処理
        /// </summary>
        public static readonly Func<IEnumerable<byte>, int, bool[]> BitDeviceReader = (x, dataNumbers) => x.Skip(11).SelectMany(y => y.ToBooleans()).Take(dataNumbers).ToArray();

        /// <summary>
        /// ワードデバイス用受信処理
        /// </summary>
        public static readonly Func<IEnumerable<byte>, int, short[]> WordDeviceReader = (x, dataNumbers) => x.Skip(11).Chunk(2).Select(y => BitConverter.ToInt16(y.ToArray(), 0)).Take(dataNumbers).ToArray();

        #region 公開プロパティ

        /// <summary>
        /// 接続先のホスト名を取得します。
        /// </summary>
        public string HostName { get; private set; }

        /// <summary>
        /// 接続先のポート番号を取得します。
        /// </summary>
        public int Port { get; private set; }

        /// <summary>
        /// 接続済みかどうかを取得します。
        /// </summary>
        public bool IsConnected { get; private set; }

        /// <summary>
        /// 通信フレーム種別を取得または設定します。
        /// </summary>
        public McFrame McFrame { get; set; }

        /// <summary>
        /// 監視タイマーを取得または設定します。
        /// </summary>
        public McTimeout Timeout { get; set; }

        #endregion 公開プロパティ

        /// <summary>
        /// 3E フレーム用
        /// </summary>
        private McMessage _3eFrame = new McMessage();

        /// <summary>
        /// 受信処理用一時バッファ
        /// </summary>
        private byte[] _buffer = new byte[8192];
    }
}
