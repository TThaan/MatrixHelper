using System;
using System.Collections;
using System.Collections.Generic;

namespace MatrixHelper
{
    /// <summary>
    /// wa: Base class Matrix plus child class StoringMatrix incl fields transpose, rows and columns?
    /// </summary>
    //public class Matrix_FuncOfFloat : IEnumerable<Func<float, float, float>>    // IMatrix, 
    //{
    //    #region fields

    //    Func<float, float, float>[,] content;
    //    int _m, _n;
    //    Matrix_FuncOfFloat transpose;

    //    #endregion

    //    #region ctor

    //    /// <summary>
    //    /// </summary>
    //    /// <param name="m">amount of rows</param>
    //    /// <param name="n">amount of columns</param>
    //    public Matrix_FuncOfFloat(int m, int n)
    //    {
    //        this.m = m;
    //        this.n = n;

    //        content = new Func<float, float, float>[m, n];
    //    }
    //    /// <summary>
    //    /// first dimension = m = y = height
    //    /// </summary>
    //    /// <param name="array"></param>
    //    public Matrix_FuncOfFloat(Func<float, float, float>[] array)
    //    {
    //        m = array.Length;
    //        n = 1;

    //        content = new Func<float, float, float>[m, n];
    //        for (int y = 0; y < m; y++)
    //        {
    //            content[y, 0] = array[y];
    //        }
    //    }
    //    /// <summary>
    //    /// First dimension = m = y = height!
    //    /// </summary>
    //    /// <param name="array"></param>
    //    public Matrix_FuncOfFloat(Func<float, float, float>[,] array)
    //    {
    //        content = array;

    //        m = array.GetLength(0);
    //        n = array.GetLength(1);
    //    }

    //    #endregion

    //    #region Indexer

    //    public Func<float, float, float> this[int y, int x]
    //    {
    //        get => content[y, x];
    //        set
    //        {
    //            content[y, x] = value;
    //        }
    //    }

    //    #endregion

    //    #region IMatrix

    //    /// <summary>
    //    /// Get the matrix's transpose once and store it for later reuse.
    //    /// </summary>
    //    public Matrix_FuncOfFloat Transpose => transpose ?? (transpose = GetTranspose());
    //    /// <summary>
    //    /// amount of rows
    //    /// </summary>
    //    public int m
    //    {
    //        get { return _m; }
    //        private set { _m = value; }
    //    }
    //    /// <summary>
    //    /// amount of columns
    //    /// </summary>
    //    public int n
    //    {
    //        get { return _n; }
    //        private set { _n = value; }
    //    }
    //    // Better GetRows as method to indicate it's an (effortful) action not a stored value?
    //    public Func<float, float, float>[][] Rows
    //    {
    //        get
    //        {
    //            Func<float, float, float>[][] result = new Func<float, float, float>[m][];
    //            for (int y = 0; y < m; y++)
    //            {
    //                Func<float, float, float>[] row = new Func<float, float, float>[n];

    //                for (int x = 0; x < n; x++)
    //                {
    //                    row[x] = content[y, x];
    //                }

    //                result[y] = row;
    //            }

    //            return result;
    //        }
    //    }
    //    // Better GetColumns as method to indicate it's an (effortful) action not a stored value?
    //    public Func<float, float, float>[][] Columns
    //    {
    //        get
    //        {
    //            Func<float, float, float>[][] result = new Func<float, float, float>[n][];
    //            for (int x = 0; x < n; x++)
    //            {
    //                Func<float, float, float>[] column = new Func<float, float, float>[m];

    //                for (int y = 0; y < m; y++)
    //                {
    //                    column[y] = content[y, x];
    //                }

    //                result[x] = column;
    //            }

    //            return result;
    //        }
    //    }

    //    /// <summary>
    //    /// Get the matrix's transpose without storing it.
    //    /// </summary>
    //    public Matrix_FuncOfFloat GetTranspose()
    //    {
    //        Func<float, float, float>[,] result = new Func<float, float, float>[n, m];
    //        for (int x = 0; x < m; x++)
    //        {
    //            for (int y = 0; y < n; y++)
    //            {
    //                result[y, x] = content[x, y];
    //            }
    //        }
    //        return new Matrix_FuncOfFloat(result);
    //    }

    //    #endregion

    //    #region IEnumerable<float>

    //    public IEnumerator<Func<float, float, float>> GetEnumerator()
    //    {
    //        for (int y = 0; y < m; y++)
    //        {
    //            for (int x = 0; x < n; x++)
    //            {
    //                yield return content[y, x];
    //            }
    //        }
    //    }
    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }

    //    #endregion
    //}
}