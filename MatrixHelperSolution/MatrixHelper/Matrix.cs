using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MatrixHelper
{
    /// <summary>
    /// wa: Base class Matrix plus child class StoringMatrix incl fields transpose, rows and columns?
    /// wa: ByteMatrix incl binary operations?
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
        /// </summary>
        /// <param name="m">amount of rows</param>
        public Matrix(int m)
        {
            this.m = m;
            n = 1;

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

            for (int j = 0; j < m; j++)
            {
                content[j,0] = array[j];
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
        /// <summary>
        /// Copy Constructor
        /// </summary>
        public Matrix(Matrix matrix)
        {
            content = new float[matrix.m, matrix.n];            

            for (int j = 0; j < matrix.m; j++)
            {
                for (int k = 0; k < matrix.n; k++)
                {
                    content[j, k] = matrix[j, k];
                }
            }
        }

        #endregion

        #region Indexer

        public float this[int y, int x]
        {
            get
            {
                if (n == 1 && x > 0)
                    throw new ArgumentException("This matrix has no second dimension and cannot process 'x'.");

                return content[y, x];
            }
            set
            {
                if (n == 1 && x > 0)
                    throw new ArgumentException("This matrix has no second dimension and cannot process 'x'.");

                content[y, x] = value;
            }
        }
        public float this[int y]
        {
            get
            {
                if (n > 1)
                    throw new ArgumentException("This matrix has a second dimension and needs it's value 'x'.");

                return content[y, 0]; 
            }
            set
            {
                if (n > 1)
                    throw new ArgumentException("This matrix has a second dimension and needs it's value 'x'.");

                content[y, 0] = value;
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

        public static Matrix operator +(Matrix a, Matrix b) => Operations.Addition(a, b);
        public static Matrix operator -(Matrix a, Matrix b)=> Operations.Subtraction(a, b);
        public static Matrix operator *(float a, Matrix b) => Operations.ProductWithAScalar(a, b);
        public static Matrix operator *(Matrix b, float a) => Operations.ProductWithAScalar(a, b);
        public static Matrix operator /(float a, Matrix b) => Operations.DivisionWithAScalar(a, b);
        public static Matrix operator /(Matrix b, float a) => Operations.DivisionWithAScalar(a, b);
        public static Matrix operator *(Matrix a, Matrix b) => Operations.ScalarProduct(a, b);
        public static bool operator ==(Matrix a, Matrix b)
        {
            if (ReferenceEquals(a, null) &&
                ReferenceEquals(b, null))
                return true;
            else if (ReferenceEquals(a, null) ||
                ReferenceEquals(b, null))
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
        public override bool Equals(object obj)
        {
            // debugging/testing
            return this == obj as Matrix;
            // return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            throw new NotImplementedException();
            // return base.GetHashCode();
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
