namespace System.Net.Sockets
{
    /// <summary>
    /// MC プロトコルの伝文を扱うクラスを提供します。
    /// </summary>
    internal class McMessage
    {
        /// <summary>
        /// 新しいインスタンスを生成します。
        /// </summary>
        public McMessage()
        {
            // 自局設定をデフォルトとする
            this.NetworkNumber = 0x00;
            this.PCNumber = 0xff;
            this.UnitIoNumber = 0x03ff;
            this.UnitStationNumber = 0x00;
        }

        /// <summary>
        /// 要求伝文を生成します。
        /// </summary>
        /// <param name="data">要求データを指定します。</param>
        /// <returns>生成した要求伝文の <c>byte</c> 配列を返します。</returns>
        public byte[] CreateRequestMessage(byte[] data)
        {
            // ヘッダは再構築が必要な時だけ作る
            if (this._isDirtyHeader)
            {
                this._header = new byte[]
                {
                    // サブヘッダ
                    0x50, 0x00,
                    // ネットワーク番号
                    this.NetworkNumber,
                    // PC 番号
                    this.PCNumber,
                    // 要求先ユニット I/O 番号
                    (byte)(this.UnitIoNumber & 0x00ff), (byte)((this.UnitIoNumber >> 8) & 0x00ff),
                    // 要求先ユニット局番号
                    this.UnitStationNumber,
                };
            }

            // 監視タイマ
            var mcTimeout = BitConverter.GetBytes((short)this.Timeout);

            // 要求データ長
            var lengthBytes = BitConverter.GetBytes((short)(mcTimeout.Length + data.Length));

            // 伝文生成
            var result = new byte[this._header.Length + lengthBytes.Length + mcTimeout.Length + data.Length];
            Array.Copy(this._header, 0, result, 0,                                                           this._header.Length);
            Array.Copy(lengthBytes,  0, result, this._header.Length,                                         lengthBytes.Length );
            Array.Copy(mcTimeout,    0, result, this._header.Length + lengthBytes.Length,                    mcTimeout.Length   );
            Array.Copy(data,         0, result, this._header.Length + lengthBytes.Length + mcTimeout.Length, data.Length        );
            return result;
        }

        private byte _networkNumber;
        /// <summary>
        /// ネットワーク番号を取得または設定します。
        /// </summary>
        public byte NetworkNumber
        {
            get { return this._networkNumber; }
            set
            {
                this._networkNumber = value;
                this._isDirtyHeader = true;
            }
        }

        private byte _pcNumber;
        /// <summary>
        /// PC 番号を取得または設定します。
        /// </summary>
        public byte PCNumber
        {
            get { return this._pcNumber; }
            set
            {
                this._pcNumber = value;
                this._isDirtyHeader = true;
            }
        }

        private short _unitIoNumber;
        /// <summary>
        /// 要求先ユニット I/O 番号を取得または設定します。
        /// </summary>
        public short UnitIoNumber
        {
            get { return this._unitIoNumber; }
            set
            {
                this._unitIoNumber = value;
                this._isDirtyHeader = true;
            }
        }

        private byte _unitStationNumber;
        /// <summary>
        /// 要求先ユニット局番号を取得または設定します。
        /// </summary>
        public byte UnitStationNumber
        {
            get { return this._unitStationNumber; }
            set
            {
                this._unitStationNumber = value;
                this._isDirtyHeader = true;
            }
        }

        /// <summary>
        /// 監視タイマを取得または設定します。
        /// </summary>
        public McTimeout Timeout { get; set; }

        /// <summary>
        /// ヘッダの再構築が必要な場合に true とします。
        /// </summary>
        private bool _isDirtyHeader;

        /// <summary>
        /// 要求伝文のヘッダ部をキャッシュとして保持します。
        /// </summary>
        private byte[] _header;
    }
}
