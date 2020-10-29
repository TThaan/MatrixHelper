using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MatrixHelper
{
    public class Matrix : IMatrix, IEnumerable<float>
    {
        #region fields

        float[,] content;
        int _m, _n;
        IMatrix transpose;

        #endregion

        #region ctor

        /// <summary>
        /// </summary>
        /// <param name="m">amount of rows (= y-axis)</param>
        /// <param name="n">amount of columns (= x-axis)</param>
        public Matrix(int m, int n)
        {
            this.m = m;
            this.n = n;

            content = new float[m, n];
        }
        /// <summary>
        /// first dimension = m = y = height
        /// </summary>
        /// <param name="array"></param>
        public Matrix(float[] array)
        {
            m = array.Length;
            n = 1;

            content = new float[m, n];
            for (int y = 0; y < m; y++)
            {
                content[y, 0] = array[y];
            }
        }
        /// <summary>
        /// First dimension = m = y = height!
        /// </summary>
        /// <param name="array"></param>
        public Matrix(float[,] array)
        {
            content = array;

            m = array.GetLength(0);
            n = array.GetLength(1);
        }

        #endregion

        #region Indexer

        public float this[int y, int x]
        {
            get => content[y, x];
            set
            {
                content[y, x] = value;
            }
        }

        #endregion

        #region IMatrix

        /// <summary>
        /// Get the matrix's transpose once and store it for later reuse.
        /// </summary>
        public IMatrix Transpose => transpose ?? (transpose = GetTranspose());
        /// <summary>
        /// number of rows
        /// </summary>
        public int m 
        { 
            get { return _m; } 
            private set { _m = value; } 
        }
        /// <summary>
        /// number of columns
        /// </summary>
        public int n 
        {
            get { return _n; }
            private set { _n = value; }
        }
        public float[][] Rows
        {
            get
            {
                float[][] result = new float[m][];
                for (int y = 0; y < m; y++)
                {
                    float[] row = new float[n];

                    for (int x = 0; x < n; x++)
                    {
                        row[x] = content[y, x];
                    }

                    result[y] = row;
                }

                return result;
            }
        }
        public float[][] Columns
        {
            get
            {
                float[][] result = new float[n][];
                for (int x = 0; x < n; x++)
                {
                    float[] column = new float[m];

                    for (int y = 0; y < m; y++)
                    {
                        column[y] = content[y, x];
                    }

                    result[x] = column;
                }

                return result;
            }
        }

        /// <summary>
        /// Get the matrix's transpose without storing it.
        /// </summary>
        public IMatrix GetTranspose()
        {
            float[,] result = new float[n, m];
            for (int x = 0; x < m; x++)
            {
                for (int y = 0; y < n; y++)
                {
                    result[y, x] = content[x, y];
                }
            }
            return new Matrix(result);
        }

        #endregion

        #region IEnumerable<float>

        public IEnumerator<float> GetEnumerator()
        {
            for (int y = 0; y < m; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    yield return content[y, x];
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region operator overloads

        /// <summary>
        /// a.m = b.m and 
        /// a.n = b.n = 1
        /// </summary>
        public static IMatrix operator +(Matrix a, Matrix b)    // Forget about interface or operator overloading!
        {
            if (a.m != b.m || a.n != 1 || b.n != 1)
            {
                throw new ArgumentException(
                    "a.m of a must equal b.m and" +
                    "a.n and b.n must equal 1.");
            }

            return new Matrix(Enumerable.Range(0, a.m)
                .Select((x, index) => a[index, 0] + b[index, 0])
                .ToArray()
                );
        }
        /// <summary>
        /// a.m = b.m and 
        /// a.n = b.n = 1
        /// </summary>
        public static IMatrix operator -(Matrix a, Matrix b)
        {
            if (a.m != b.m || a.n != 1 || b.n != 1)
            {
                throw new ArgumentException(
                    "a.m of a must equal b.m and" +
                    "a.n and b.n must equal 1.");
            }

            return new Matrix(Enumerable.Range(0, a.m)
                .Select((x, index) => a[index, 0] - b[index, 0])
                .ToArray()
                );
        }

        /// <summary>
        /// 'Simple' Scalar Product
        /// </summary>
        public static IMatrix operator *(float a, Matrix b)
        {
            float[,] arr = new float[b.m, b.n];

            for (int x = 0; x < b.n; x++)
            {
                for (int y = 0; y < b.m; y++)
                {
                    arr[y, x] = a * b[y, x];
                }
            }
            //float[,] arr = Enumerable.Range(0, b.m)
            //    .Select(x =>
            //        Enumerable.Range(0, b.n).Select(y => a * b[x, y])
            //    ).ToArray();
            var result = new Matrix(arr);
            return result;
        }
        /// <summary>
        /// Elementwise Product = Hadamard Product (or 
        /// Outer/Kronecker Product if a & b have diff dimensions?).
        /// </summary>
        public static IMatrix operator *(Matrix a, Matrix b)
        {
            if (a.m != b.m || a.n != 1 || b.n != 1)
            {
                throw new ArgumentException(
                    "a.m of a must equal b.m and" +
                    "a.n and b.n must equal 1.");
            }

            return new Matrix(Enumerable.Range(0, a.m)
                .Select((x, index) => a[index, 0] * b[index, 0])
                .ToArray()
                );
        }

        #endregion
    }
}
