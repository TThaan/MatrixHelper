using CustomLogger;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MatrixHelper
{
    public interface IMatrixChanged
    {
        event MatrixChangedEventHandler MatrixChanged;
    }
    public interface IMatrix : IEnumerable<float>, IEquatable<IMatrix>, IMatrixChanged, ILoggable
    {
        float this[int y] { get; set; }
        float this[int y, int x] { get; set; }
        int Length { get; }
        int m { get; }
        int n { get; }
        float[][] Rows { get; }
        float[][] Columns { get; }

        bool IsEqualTo(IMatrix a);
        // bool IsIdenticalTo(IMatrix a);

        IMatrix GetTranspose();

        public static IMatrix operator +(IMatrix a, IMatrix b) => Operations.Addition(a, b);
        public static IMatrix operator -(IMatrix a, IMatrix b) => Operations.Subtraction(a, b);
        public static IMatrix operator *(IMatrix a, IMatrix b) => Operations.HadamardProduct(a, b);
        public static IMatrix operator *(IMatrix a, float b) => Operations.ProductWithAScalar(a, b);
        public static IMatrix operator *(float a, IMatrix b) => Operations.ProductWithAScalar(a, b);
        public static IMatrix operator /(IMatrix a, IMatrix b) => Operations.HadamardProduct(a, 1/b);
        public static IMatrix operator /(float a, IMatrix b) => Operations.DivisionOfScalarByMatrix(a, b);

        float[,] content { get; set; }
        float[] content_1d { get; set; }
    }

    [Serializable]
    /// <summary>
    /// wa: Base class Matrix plus child class StoringMatrix incl fields transpose, rows and columns?
    /// wa: ByteMatrix incl binary operations?
    /// </summary>
    public class Matrix : IMatrix, IEnumerable<float>
    {
        #region fields & ctors

        public float[,] content { get; set; }
        public float[] content_1d { get; set; }

        int _m, _n;

        public Matrix(string name)
        {
            if (!string.IsNullOrEmpty(name))
                LoggableName = name;
        }
        /// <summary>
        /// </summary>
        /// <param name="m">amount of rows</param>
        /// <param name="n">amount of columns</param>
        public Matrix(int m, int n, string name = default)
            :this(name)
        {
            this.m = m;
            this.n = n;

            content = new float[m, n];
            Length = m * n;
        }
        /// <summary>
        /// </summary>
        /// <param name="m">amount of rows</param>
        public Matrix(int m, string name = default)
            : this(name)
        {
            this.m = m;
            n = 1;

            content_1d = new float[m];
            Length = m;
        }
        /// <summary>
        /// first dimension = m = y = height
        /// </summary>
        /// <param name="array"></param>
        //public Matrix(float[] array, string name = default)
        //    : this(name)
        //{
        //    m = array.Length;
        //    n = 1;
        //    content = new float[m, n];

        //    for (int j = 0; j < m; j++)
        //    {
        //        content[j,0] = array[j];
        //    }
        //    Length = m * n;
        //}
        public Matrix(float[] array, string name = default)
            : this(name)
        {
            m = array.Length;
            n = 1;
            content_1d = new float[m];

            for (int j = 0; j < m; j++)
            {
                content_1d[j] = array[j];
            }
            Length = m;
        }
        /// <summary>
        /// First dimension = m = y = height!
        /// </summary>
        /// <param name="array"></param>
        public Matrix(float[,] array, string name = default)
            : this(name)
        {
            content = array;

            m = array.GetLength(0);
            n = array.GetLength(1);
            Length = m * n;
        }
        /// <summary>
        /// Copy Constructor
        /// </summary>
        public Matrix(IMatrix matrix, string name = default)
            : this(name)
        {
            m = matrix.m;
            n = matrix.n;

            if (n == 1)
            {
                content_1d = new float[matrix.m];

                for (int j = 0; j < matrix.m; j++)
                {
                    content_1d[j] = matrix[j];
                }
            }
            else
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
            
            Length = m * n;
        }

        #endregion

        #region Indexer

        public float this[int y, int x]
        {
            get
            {
                // if (n == 1 && x > 0)
                //     throw new ArgumentException("This matrix has no second dimension and cannot process 'x'.");
                if (n == 1)
                    return content_1d[y];
                else
                    return content[y, x];
            }
            set
            {
                // if (n == 1 && x > 0)
                //     throw new ArgumentException("This matrix has no second dimension and cannot process 'x'.");
                if (n == 1)
                    content_1d[y] = value;
                else
                    content[y, x] = value;
                // OnMatrixChanged();
            }
        }
        public float this[int y]
        {
            get
            {
                // if (n > 1)
                //     throw new ArgumentException("This matrix has a second dimension and needs it's value 'x'.");

                // return content[y, 0]; 
                return content_1d[y];
            }
            set
            {
                // if (n > 1)
                //     throw new ArgumentException("This matrix has a second dimension and needs it's value 'x'.");

                // content[y, 0] = value;
                content_1d[y] = value;
                // OnMatrixChanged();
            }
        }

        #endregion

        #region IMatrix

        public int Length { get; private set; }
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

        // Better 'GetRows()' as method to indicate it's an (effortful) action not a stored value?
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
        // Better 'GetColumns()' as method to indicate it's an (effortful) action not a stored value?
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
        /// 
        /// Currently content_1d is defined as row vector. 
        /// Thus its transpose cannot be a matrix (vector) with a content_1d but an m x n matrix!
        /// </summary>
        public IMatrix GetTranspose()
        {
            float[,] result = new float[n, m];
            for (int x = 0; x < m; x++)
            {
                for (int y = 0; y < n; y++)
                {
                    if (n == 1)
                    {
                        result[0, x] = content_1d[x];
                        break;
                    }

                    result[y, x] = content[x, y];
                }
            }
            return new Matrix(result);
        }

        #endregion

        #region Equality incl IEquatable<IMatrix>

        public bool Equals(IMatrix other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (m == other.m && n == other.n) return this
                    .Select((x, index) => x == other.ElementAt(index))
                    .All(x => x == true);
            else return false;
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals(obj as IMatrix);
        }

        public static bool operator ==(Matrix a, object b)
        {
            Matrix bAsMatrix = b as Matrix;

            if (bAsMatrix != null)
            {
                return a == bAsMatrix;
            }

            float[,] jagArr = b as float[,];
            if (jagArr != null)
            {
                bAsMatrix = new Matrix(jagArr);
            }

            float[] arr = b as float[];
            if (arr != null)
            {
                bAsMatrix = new Matrix(arr);
            }

            return a == bAsMatrix;
        }
        public static bool operator !=(Matrix a, object b)
        {
            Matrix bAsMatrix = b as Matrix;

            if (bAsMatrix != null)
            {
                return a != bAsMatrix;
            }

            float[,] jagArr = b as float[,];
            if (jagArr != null)
            {
                bAsMatrix = new Matrix(jagArr);
            }

            float[] arr = b as float[];
            if (arr != null)
            {
                bAsMatrix = new Matrix(arr);
            }

            return a != bAsMatrix;
        }
        public static bool operator ==(Matrix a, Matrix b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;
            else if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            if (a.m == b.m && a.n == b.n) return a
                    .Select((x, index) => x == b.ElementAt(index))
                    .All(x => x == true);
            else return false;
        }
        public static bool operator !=(Matrix a, Matrix b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return false;
            else if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return true;
            if (a.m == b.m && a.n == b.n) return a
                    .Select((x, index) => x == b.ElementAt(index))
                    .Any(x => x == false);
            else return true;
        }
        /// <summary>
        /// Used as substitute for ==. Redundant?
        /// </summary>
        public bool IsEqualTo(IMatrix a)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region other/unused operator overloads

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

        #region IEnumerable<float>

        public IEnumerator<float> GetEnumerator()
        {
            for (int y = 0; y < m; y++)
            {
                for (int x = 0; x < n; x++)
                {
                    if (n == 1)
                        yield return content_1d[y];
                    else
                        yield return content[y, x];
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region IMatrixChanged

        public event MatrixChangedEventHandler MatrixChanged;
        void OnMatrixChanged()
        {
            MatrixChanged?.Invoke(this, new MatrixChangedEventArgs(LoggableName));
        }

        #endregion

        #region ILoggable

        public string LoggableName { get; private set; } = "Matrix";

        public int Count => Length;

        // public bool IsReadOnly => throw new NotImplementedException();

        public string ToLog()
        {
            string log = "";

            if (!string.IsNullOrEmpty(LoggableName))
            {
                log += LoggableName + "\n";
            }

            log += "-";
            for (int i = 0; i < n; i++)
            {
                log += "----------------";
            }
            log += "\n";

            for (int j = 0; j < m; j++)
            {
                if (n == 1)
                {
                    log += string.Format("|{0, 15}", this[j]);
                }
                else
                {
                    for (int k = 0; k < n; k++)
                    {
                        log += string.Format("|{0, 15}", this[j, k]);
                    }
                }
                log += "|\n";
            }

            log += "-";
            for (int i = 0; i < n; i++)
            {
                log += "----------------";
            }

            return $"{log}\n";
        }

        #endregion
    }
}
