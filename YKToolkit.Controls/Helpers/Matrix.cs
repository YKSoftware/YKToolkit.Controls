namespace YKToolkit.Helpers
{
    using System;

    /// <summary>
    /// 行列を表現するための構造体
    /// </summary>
    public class Matrix
    {
        #region コンストラクタ
        /// <summary>
        /// 正方行列を生成します。
        /// </summary>
        /// <param name="rows">行数</param>
        public Matrix(int rows)
            : this(rows, rows)
        {
        }

        /// <summary>
        /// 指定サイズの行列を生成します。
        /// </summary>
        /// <param name="rows">行数</param>
        /// <param name="columns">列数</param>
        public Matrix(int rows, int columns)
        {
            this._rows = rows;
            this._columns = columns;

            this._array = new double[rows][];
            for (int i = 0; i < rows; i++)
                this._array[i] = new double[columns];
        }
        #endregion  // コンストラクタ

        /// <summary>
        /// 各要素の値
        /// </summary>
        private double[][] _array;

        /// <summary>
        /// クローンを生成します。
        /// </summary>
        /// <returns></returns>
        public Matrix Clone()
        {
            var mat = new Matrix(this.Rows, this.Columns);
            for (int i = 0; i < this.Rows; i++)
                this._array[i].CopyTo(mat[i], 0);

            return mat;
        }

        #region インデクサ
        /// <summary>
        /// 行列の各要素を取得または設定します。
        /// </summary>
        /// <param name="row">行番号 (0 始まり)</param>
        /// <param name="column">列番号 (0 始まり)</param>
        /// <returns>指定されたインデックスの要素の値</returns>
        public double this[int row, int column]
        {
            get
            {
                if (!IsValid(row, column))
                    return double.NaN;

                return this._array[row][column];
            }
            set
            {
                if (IsValid(row, column))
                    this._array[row][column] = value;
            }
        }

        /// <summary>
        /// 行列の各行を取得または設定します。
        /// </summary>
        /// <param name="row">行番号 (0 始まり)</param>
        /// <returns>指定されたインデックスの行ベクトル</returns>
        public double[] this[int row]
        {
            get
            {
                if (!IsValid(row))
                    return null;

                return this._array[row];
            }
            set
            {
                if (value.Length == this.Columns)
                {
                    if (IsValid(row))
                        this._array[row] = value;
                }
            }
        }
        #endregion  // インデクサ

        #region 公開プロパティ
        private int _rows;
        /// <summary>
        /// 行数を取得します。
        /// </summary>
        public int Rows
        {
            get { return this._rows; }
        }

        private int _columns;
        /// <summary>
        /// 列数を取得します。
        /// </summary>
        public int Columns
        {
            get { return this._columns; }
        }
        #endregion  // 公開プロパティ

        #region ヘルパ
        /// <summary>
        /// 行番号が範囲内かどうか確認します。
        /// </summary>
        /// <param name="row">行番号 (0 始まり)</param>
        /// <returns>指定されたインデックスが範囲外の場合に <c>false</c> を返します。</returns>
        private bool IsValid(int row)
        {
            if (row > this.Rows)
                return false;

            return true;
        }

        /// <summary>
        /// 行番号および列番号が範囲内かどうか確認します。
        /// </summary>
        /// <param name="row">行番号 (0 始まり)</param>
        /// <param name="column">列番号 (0 始まり)</param>
        /// <returns>指定されたインデックスが範囲外の場合に <c>false</c> を返します。</returns>
        private bool IsValid(int row, int column)
        {
            if (row > this.Rows)
                return false;
            if (column > this.Columns)
                return false;

            return true;
        }
        #endregion  // ヘルパ

        #region オーバーロード演算子
        /// <summary>
        /// <c>Matrix</c> 構造体と <c>Matrix</c> 構造体の + 演算子を定義します。
        /// </summary>
        /// <param name="m">左側の行列</param>
        /// <param name="n">右側の行列</param>
        /// <returns><c>m + n</c> の演算結果</returns>
        public static Matrix operator +(Matrix m, Matrix n)
        {
            if ((m.Rows != n.Rows) || (m.Columns != n.Columns))
                throw new ArgumentException("行列の次元が一致しません。");

            var mat = new Matrix(m.Rows, m.Columns);
            for (int i = 0; i < m.Rows; i++)
            {
                for (int j = 0; j < m.Columns; j++)
                {
                    mat[i, j] = m[i, j] + n[i, j];
                }
            }

            return mat;
        }

        /// <summary>
        /// <c>Matrix</c> 構造体と <c>Matrix</c> 構造体の - 演算子を定義します。
        /// </summary>
        /// <param name="m">左側の行列</param>
        /// <param name="n">右側の行列</param>
        /// <returns><c>m - n</c> の演算結果</returns>
        public static Matrix operator -(Matrix m, Matrix n)
        {
            if ((m.Rows != n.Rows) || (m.Columns != n.Columns))
                throw new ArgumentException("行列の次元が一致しません。");

            var mat = new Matrix(m.Rows, m.Columns);
            for (int i = 0; i < m.Rows; i++)
            {
                for (int j = 0; j < m.Columns; j++)
                {
                    mat[i, j] = m[i, j] - n[i, j];
                }
            }

            return mat;
        }

        /// <summary>
        /// <c>Matrix</c> 構造体と <c>Matrix</c> 構造体の * 演算子を定義します。
        /// </summary>
        /// <param name="m">左側の行列</param>
        /// <param name="n">右側の行列</param>
        /// <returns><c>m * n</c> の演算結果</returns>
        public static Matrix operator *(Matrix m, Matrix n)
        {
            if (m.Columns != n.Rows)
                throw new ArgumentException("行列の次元が一致しません。");

            var mat = new Matrix(m.Rows, n.Columns);

            //for (int i = 0; i < m.Rows; i++)
            //{
            //    for (int j = 0; j < n.Columns; j++)
            //    {
            //        for (int k = 0; k < m.Columns; k++)
            //        {
            //            mat[i, j] += m[i, k] * n[k, j];
            //        }
            //    }
            //}

            System.Threading.Tasks.Parallel.For(0, m.Rows, i =>
            {
                for (int j = 0; j < n.Columns; j++)
                {
                    for (int k = 0; k < m.Columns; k++)
                    {
                        mat[i, j] += m[i, k] * n[k, j];
                    }
                }
            });

            return mat;
        }

        /// <summary>
        /// <c>Matrix</c> 構造体と <c>double</c> 型の * 演算子を定義します。
        /// </summary>
        /// <param name="m">左側の行列</param>
        /// <param name="n">ベクトル</param>
        /// <returns><c>m * n</c> の演算結果</returns>
        public static Matrix operator *(Matrix m, double[] n)
        {
            if (n == null)
                throw new NullReferenceException("ベクトルを指定してください。");
            if (m.Columns != n.Length)
                throw new ArgumentException("行列とベクトルの次元が一致しません。");

            var mat = new Matrix(m.Rows, 1);
            for (int i = 0; i < m.Rows; i++)
            {
                for (int j = 0; j < m.Columns; j++)
                {
                    mat[i, 0] += m[i, j] * n[j];
                }
            }

            return mat;
        }
        #endregion  // オーバーロード演算子

        /// <summary>
        /// <c>Matrix</c> 構造体を文字列に変換します。
        /// </summary>
        /// <returns>変換された文字列</returns>
        public override string ToString()
        {
            return ToString(5);
        }

        /// <summary>
        /// <c>Matrix</c> 構造体を文字列に変換します。
        /// </summary>
        /// <returns>変換された文字列</returns>
        public string ToString(int keta)
        {
            if ((Rows == 0) || (Columns == 0))
                return "NULL";

            string format = string.Format("f{0}", keta);
            string s = "";
            for (int i = 0; i < Rows; ++i)
            {
                for (int j = 0; j < Columns; ++j)
                    s += _array[i][j].ToString(format).PadLeft(keta + 5) + " ";
                if (i != Rows - 1)
                    s += System.Environment.NewLine;
            }
            return s;
        }

        #region static な公開メソッド
        /// <summary>
        /// 単位行列を生成します。
        /// </summary>
        /// <param name="rows">行列のサイズを指定する</param>
        /// <returns>指定サイズの単位行列</returns>
        public static Matrix Identity(int rows)
        {
            var mat = new Matrix(rows);
            for (int i = 0; i < rows; i++)
                mat[i, i] = 1.0;
            return mat;
        }

        /// <summary>
        /// 行置換行列を生成します。
        /// </summary>
        /// <param name="perm">行置換情報を保持する列ベクトル</param>
        /// <returns>置換後の行列</returns>
        public static Matrix PermArrayToMatrix(int[] perm)
        {
            // Doolittle perm array to corresponding matrix
            int n = perm.Length;
            var mat = new Matrix(n);
            for (int i = 0; i < n; ++i)
                mat[i, perm[i]] = 1.0;
            return mat;
        }
        #endregion  // static な公開メソッド
    }
}
