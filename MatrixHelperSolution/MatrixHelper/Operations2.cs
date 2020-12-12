using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixHelper
{
    public class Operations2
    {
        #region Just setting a dedicated result matrix without return ing it (independent from formula) and ignoring size checks

        public static void Add(Matrix a, Matrix b, Matrix result)
        {
            for (int x = 0; x < result.n; x++)
            {
                for (int y = 0; y < result.m; y++)
                {
                    result[y, x] = a[y, x] + b[y, x];
                }
            }
        }
        public static void Subtract(Matrix a, Matrix subtrahend, Matrix result)
        {
            for (int x = 0; x < result.n; x++)
            {
                for (int y = 0; y < result.m; y++)
                {
                    result[y, x] = a[y, x] - subtrahend[y, x];
                }
            }
        }
        public static void Multiplicate(Matrix a, float factor, Matrix result)
        {
            for (int x = 0; x < result.n; x++)
            {
                for (int y = 0; y < result.m; y++)
                {
                    result[y, x] = a[y, x] * factor;
                }
            }
        }
        public static void Divide(Matrix a, float divisor, Matrix result)
        {
            for (int x = 0; x < result.n; x++)
            {
                for (int y = 0; y < result.m; y++)
                {
                    result[y, x] = a[y, x] / divisor;
                }
            }
        }
        public static void SetHadamardProduct(Matrix a, Matrix b, Matrix result)
        {
            for (int x = 0; x < result.n; x++)
            {
                for (int y = 0; y < result.m; y++)
                {
                    result[y, x] = a[y, x] * b[y, x];
                }
            }
        }
        public static void SetScalarProduct(Matrix a, Matrix b, Matrix result)
        {
            // Make sure the result has only 0 - values
            result.ForEach(x => 0);

            // For each row of matrix 'a'
            for (int y = 0; y < a.m; y++)
            {
                // you take each column of matrix 'b', 
                for (int x = 0; x < b.n; x++)
                {
                    // iterate over each value of a's columns and b's rows (a.n=b.m)
                    for (int z = 0; z < a.n; z++)
                    {
                        // make sure the result has only 0 - values
                        //if (z == 0)
                        //    result[y, x] = 0;

                        // and compute their scalar product
                        result[y, x] += a[y, z] * b[z, x];
                    }
                }
            }
        }

        #endregion
    }
}
