namespace YKToolkit.Helpers
{
    using System;

    /// <summary>
    /// 拡張メソッドを提供します。
    /// </summary>
    public static partial class YKMath
    {
        #region 行列の分解
        /// <summary>
        /// 行列の LU 分解をおこないます。
        /// 対角要素を除く下三角の要素が行列 L (対角要素はすべて 1), 上三角の要素が行列 U を表す行列を返します。
        /// </summary>
        /// <param name="A">対象とする行列</param>
        /// <returns>対角要素を除く下三角の要素が行列 L (対角要素はすべて 1), 上三角の要素が行列 U を表す行列を返します。</returns>
        public static Matrix LUDecomposition(this Matrix A)
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
        public static Matrix LUPDecomposition(this Matrix A, out int[] perm, out int toggle)
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
            Matrix luMatrix = A.LUPDecomposition(out perm, out toggle);
            if (luMatrix.Rows == 0)
                throw new Exception("Unable to compute inverse");
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

        #region 連立方程式
        /// <summary>
        /// Ax = b という連立方程式を解きます。
        /// </summary>
        /// <param name="A">n 行 n 列の行列</param>
        /// <param name="b">n 行ベクトル</param>
        /// <returns>連立方程式の解</returns>
        public static double[] SolveSimultaneousEquations(this Matrix A, double[] b)
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
            var luMatrix = A.LUPDecomposition(out perm, out toggle);

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
        #endregion 連立方程式
    }
}
