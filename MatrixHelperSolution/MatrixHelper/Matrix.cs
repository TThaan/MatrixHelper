using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MatrixHelper
{
    /// <summary>
    /// wa: Base class Matrix plus child class StoringMatrix incl fields transpose, rows and columns?
    /// </summary>
    public class Matrix : IEnumerable<float>    // IMatrix, 
    {
        #region fields

        float[,] content;
        int _m, _n;
        Matrix transpose;

        #endregion

        #region ctor

        /// <summary>
        /// </summary>
        /// <param name="m">amount of rows</param>
        /// <param name="n">amount of columns</param>
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
        public Matrix Transpose => transpose ?? (transpose = GetTranspose());
        /// <summary>
        /// amount of rows
        /// </summary>
        public int m 
        { 
            get { return _m; } 
            private set { _m = value; } 
        }
        /// <summary>
        /// amount of columns
        /// </summary>
        public int n 
        {
            get { return _n; }
            private set { _n = value; }
        }
        // Better GetRows as method to indicate it's an (effortful) action not a stored value?
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
        // Better GetColumns as method to indicate it's an (effortful) action not a stored value?
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
        public Matrix GetTranspose()
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

        public static Matrix operator +(Matrix a, Matrix b)
        {
            return Operations.Addition(a, b);
        }
        public static Matrix operator -(Matrix a, Matrix b)
        {
            return Operations.Subtraction(a, b);
        }
        public static Matrix operator *(float a, Matrix b)
        {
            return Operations.ProductWithAScalar(a, b);
        }
        public static Matrix operator *(Matrix b, float a)
        {
            return Operations.ProductWithAScalar(a, b);
        }
        public static Matrix operator /(float a, Matrix b)
        {
            return Operations.DivisionWithAScalar(a, b);
        }
        public static Matrix operator /(Matrix b, float a)
        {
            return Operations.DivisionWithAScalar(a, b);
        }
        public static Matrix operator *(Matrix a, Matrix b)
        {
            return Operations.ScalarProduct(a, b);
        }
        public static bool operator ==(Matrix a, Matrix b)
        {
            if (object.ReferenceEquals(a, null) &&
                object.ReferenceEquals(b, null))
                return true;
            else if (object.ReferenceEquals(a, null) ||
                object.ReferenceEquals(b, null))
                return false;

            if (a.m == b.m && a.n == b.n)
                return a
                    .Select((x, index) => x == b.ElementAt(index))
                    .All(x => x == true);
            else 
                return false;
        }
        public static bool operator !=(Matrix a, Matrix b)
        {
            if (object.ReferenceEquals(a, null) &&
                object.ReferenceEquals(b, null))
                return false;
            else if (object.ReferenceEquals(a, null) ||
                object.ReferenceEquals(b, null))
                return true;

            if (a.m == b.m && a.n == b.n)
                return a
                    .Select((x, index) => x == b.ElementAt(index))
                    .Any(x => x == false);
            else
                return true;
        }

        // Don't!?
        public static Matrix operator %(Matrix a, Matrix b)
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
