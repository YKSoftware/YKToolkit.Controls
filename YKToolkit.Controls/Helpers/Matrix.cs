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
            return ToString(3);
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

        #region 行列の分解
        /// <summary>
        /// 行列の LU 分解をおこないます。
        /// 対角要素を除く下三角の要素が行列 L (対角要素はすべて 1), 上三角の要素が行列 U を表す行列を返します。
        /// </summary>
        /// <param name="A">対象とする行列</param>
        /// <returns>対角要素を除く下三角の要素が行列 L (対角要素はすべて 1), 上三角の要素が行列 U を表す行列を返します。</returns>
        public static Matrix LUDecomposition(Matrix A)
        {
            if ((A.Rows == 0) || (A.Columns == 0) || (A.Rows != A.Columns))
                throw new Exception("LU 分解できません。");

            var mat = A.Clone();
            var n = mat.Rows;

            for (int k = 0; k < n; k++)
            {
                var temp = 1.0 / mat[k, k];
                for (int i = k + 1; i < n; i++)
                {
                    mat[i, k] = mat[i, k] * temp;
                }

                for (int j = k + 1; j < n; j++)
                {
                    var Akj = mat[k, j];
                    for (int i = k + 1; i < n; i++)
                    {
                        mat[i, j] -= mat[i, k] * Akj;
                    }
                }
            }

            return mat;
        }

        /// <summary>
        /// 行列の LUP 分解をおこないます。
        /// </summary>
        /// <param name="A">対象とする行列</param>
        /// <param name="perm">行置換配列</param>
        /// <param name="toggle">+1:交換した行数が偶数 / -1:奇数</param>
        /// <returns>対角要素を除く下三角の要素が行列 L (対角要素はすべて 1), 上三角の要素が行列 U を表す行列</returns>
        public static Matrix LUPDecomposition(Matrix A, out int[] perm, out int toggle)
        {
            // Doolittle LUP decomposition.
            // assumes matrix is square.

            var n = A.Rows; // convenience
            var result = A.Clone();
            perm = new int[n];
            for (int i = 0; i < n; ++i) { perm[i] = i; }
            toggle = 1;
            for (int j = 0; j < n - 1; ++j) // each column
            {
                double colMax = Math.Abs(result[j, j]); // largest val in col j
                int pRow = j;
                for (int i = j + 1; i < n; ++i)
                {
                    if (result[i, j] > colMax)
                    {
                        colMax = result[i, j];
                        pRow = i;
                    }
                }
                if (pRow != j) // swap rows
                {
                    double[] rowPtr = result[pRow];
                    result[pRow] = result[j];
                    result[j] = rowPtr;
                    int tmp = perm[pRow]; // and swap perm info
                    perm[pRow] = perm[j];
                    perm[j] = tmp;
                    toggle = -toggle; // row-swap toggle
                }
                if (Math.Abs(result[j, j]) < 1.0E-20)
                    throw new Exception("正則条件が成立していない？");

                for (int i = j + 1; i < n; ++i)
                {
                    result[i, j] /= result[j, j];
                    for (int k = j + 1; k < n; ++k)
                        result[i, k] -= result[i, j] * result[j, k];
                }
            } // main j column loop
            return result;
        }
        #endregion  // 行列の分解

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

        #region 逆行列
        /// <summary>
        /// 掃き出し法で逆行列を算出します。
        /// </summary>
        /// <param name="A">逆行列を算出する行列</param>
        /// <returns>算出された逆行列</returns>
        public static Matrix Inverse(Matrix A)
        {
            var data = A.Clone();
            var mat = Matrix.Identity(data.Rows);
            double buf;         // 一時的なデータを蓄える
            int i, j, k;        // カウンタ
            int n = data.Rows;     // 配列の次数

            // 掃き出し法
            for (i = 0; i < n; i++)
            {
                buf = 1 / data[i][i];
                for (j = 0; j < n; j++)
                {
                    data[i][j] *= buf;
                    mat[i][j] *= buf;
                }

                for (j = 0; j < n; j++)
                {

                    if (i != j)
                    {
                        buf = data[j][i];
                        for (k = 0; k < n; k++)
                        {
                            data[j][k] -= data[i][k] * buf;
                            mat[j][k] -= mat[i][k] * buf;
                        }
                    }
                }
            }

            return mat;
        }

        /// <summary>
        /// <obsolete>使わないでください</obsolete> LUP 分解を用いて逆行列を算出します。
        /// </summary>
        /// <param name="A">対象とする正方行列</param>
        /// <returns>対象とする行列の逆行列</returns>
        public static Matrix DecompositionInverse(Matrix A)
        {
            if (A.Rows != A.Columns)
                throw new ArgumentException("正方行列を指定してください。");

            var n = A.Rows;
            var mat = A.Clone();
            int[] perm;
            int toggle;
            Matrix luMatrix = LUPDecomposition(A, out perm, out toggle);
            if (luMatrix.Rows == 0)
                throw new System.Exception("Unable to compute inverse");
            var b = new double[n];
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    if (i == perm[j])
                        b[j] = 1.0;
                    else
                        b[j] = 0.0;
                }
                var x = SolveSimultaneousEquations(luMatrix, b);
                for (int j = 0; j < n; ++j)
                    mat[j, i] = x[j];
            }

            return mat;
        }
        #endregion  // 逆行列

        /// <summary>
        /// Ax = b という連立方程式を解きます。
        /// </summary>
        /// <param name="A">n 行 n 列の行列</param>
        /// <param name="b">n 行ベクトル</param>
        /// <returns>連立方程式の解</returns>
        public static double[] SolveSimultaneousEquations(Matrix A, double[] b)
        {
            if (b == null)
                return null;
            if ((A.Rows == 0) || (A.Columns == 0) || (A.Rows != A.Columns) || (A.Rows != b.Length))
                return null;

            var n = A.Rows;

            // LU 分解
            //var luMatrix = Matrix.LUDecomposition(A);
            // LUP 分解
            var perm = new int[n];
            int toggle;
            var luMatrix = Matrix.LUPDecomposition(A, out perm, out toggle);

            var x = new double[n];
            b.CopyTo(x, 0);
            for (int i = 1; i < n; ++i)
            {
                double sum = x[i];
                for (int j = 0; j < i; ++j)
                {
                    sum -= luMatrix[i, j] * x[j];
                }
                x[i] = sum;
            }
            x[n - 1] /= luMatrix[n - 1, n - 1];
            for (int i = n - 2; i >= 0; --i)
            {
                double sum = x[i];
                for (int j = i + 1; j < n; ++j)
                {
                    sum -= luMatrix[i, j] * x[j];
                }
                x[i] = sum / luMatrix[i, i];
            }

            return x;
        }

        #endregion  // static な公開メソッド
    }
}
