namespace YKToolkit.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// 拡張メソッドを提供します。
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// シーケンスを指定個数毎に分割します。
        /// </summary>
        /// <typeparam name="T">シーケンスの各要素の型を表します。</typeparam>
        /// <param name="source">元のシーケンスを表します。</param>
        /// <param name="size">分割個数を指定します。</param>
        /// <returns>指定個数で分割されたシーケンスを返します。</returns>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int size)
        {
            if (source == null) throw new ArgumentException("self");
            if (size < 1) throw new ArgumentException("size");

            using (var enumerator = source.GetEnumerator())
            {
                var list = new List<T>(size);
                while (enumerator.MoveNext())
                {
                    list.Add(enumerator.Current);
                    if (list.Count >= size)
                    {
                        yield return list;
                        list = new List<T>();
                    }
                }

                // 残りの部分
                if (list.Any())
                {
                    yield return list;
                }
            }
        }

        /// <summary>
        /// シーケンスを指定個数毎に分割します。このとき、セパレータとして存在する要素をスキップすることができます。
        /// </summary>
        /// <typeparam name="T">シーケンスの各要素の型を表します。</typeparam>
        /// <param name="source">元のシーケンスを表します。</param>
        /// <param name="size">分割個数を指定します。</param>
        /// <param name="skip">セパレータとしての要素数を指定します。</param>
        /// <returns>指定個数で分割されたシーケンスを返します。</returns>
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int size, int skip)
        {
            if (source == null) throw new ArgumentException("self");
            if (size < 1) throw new ArgumentException("size");

            var i = 0;

            using (var enumerator = source.GetEnumerator())
            {
                var list = new List<T>(size);
                while (enumerator.MoveNext())
                {
                    if (i >= 0)
                    {
                        list.Add(enumerator.Current);
                        if (list.Count >= size)
                        {
                            yield return list;
                            list = new List<T>();
                            i = -skip;
                        }
                    }
                    else
                    {
                        i++;
                    }
                }

                // 残りの部分
                if (list.Any())
                {
                    yield return list;
                }
            }
        }
    }
}
