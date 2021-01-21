using System;
using System.Linq;

namespace MatrixHelper
{
    /// <summary>
    /// Operations return a newly created matrix keeping the involved ones (the parameters) intact.
    /// They also check for the correct sizes of the passed in matrices.
    /// They are slow.
    /// </summary>
    public class Operations
    {
        public static IMatrix ProductWithAScalar(float a, IMatrix b)
        {
            IMatrix result = new Matrix(b.m, b.n);

            for (int x = 0; x < b.n; x++)
            {
                for (int y = 0; y < b.m; y++)
                {
                    result[y, x] = a * b[y, x];
                }
            }

            return result;
        }
        public static IMatrix ProductWithAScalar(IMatrix a, float b)
        {
            IMatrix result = new Matrix(a.m, a.n);

            for (int x = 0; x < a.n; x++)
            {
                for (int y = 0; y < a.m; y++)
                {
                    result[y, x] = a[y, x] * b;
                }
            }

            return result;
        }
        public static IMatrix DivisionOfScalarByMatrix(float a, IMatrix b)
        {
            IMatrix result = new Matrix(b.m, b.n);

            for (int x = 0; x < b.n; x++)
            {
                for (int y = 0; y < b.m; y++)
                {
                    result[y, x] = a / b[y, x];
                }
            }

            return result;
        }
        public static IMatrix DivisionOfMatrixByScalar(IMatrix a, float b)
        {
            IMatrix result = new Matrix(a.m, a.n);

            for (int x = 0; x < a.n; x++)
            {
                for (int y = 0; y < a.m; y++)
                {
                    result[y, x] = a[y, x] / b;
                }
            }

            return result;
        }
        /// <summary>
        /// Condition: a.m = b.m & a.n = b.n.
        /// </summary>
        /// <returns></returns>
        public static IMatrix Addition(IMatrix a, IMatrix b)
        {
            if (a.m != b.m || a.n != b.n)
                throw new ArgumentException("a.m must equal b.m and a.n must equal b.n");

            IMatrix result = new Matrix(b.m, b.n);

            for (int x = 0; x < b.n; x++)
            {
                for (int y = 0; y < b.m; y++)
                {
                    result[y, x] = a[y, x] + b[y, x];
                }
            }

            return result;
        }
        /// <summary>
        /// Condition: a.m = b.m & a.n = b.n.
        /// </summary>
        public static IMatrix Subtraction(IMatrix a, IMatrix b)
        {
            if (a.m != b.m || a.n != b.n)
                throw new ArgumentException("a.m must equal b.m and a.n must equal b.n");

            IMatrix result = new Matrix(b.m, b.n);

            for (int x = 0; x < b.n; x++)
            {
                for (int y = 0; y < b.m; y++)
                {
                    result[y, x] = a[y, x] - b[y, x];
                }
            }

            return result;
        }
        /// <summary>
        /// Also called 'Dot Product' or 'Inner Product'.
        /// Condition: a.n = b.m
        /// </summary>
        
        public static IMatrix ScalarProduct(IMatrix a, IMatrix b)
        {
            if (a.n != b.m)
                throw new ArgumentException("a.n must equal b.m");

            IMatrix result = new Matrix(a.m, b.n);

            // For each row of matrix 'a'
            for (int y = 0; y < a.m; y++)
            {
                // you take each column of matrix 'b', 
                for (int x = 0; x < b.n; x++)
                {
                    // iterate over each value of a's columns and b's rows (a.n=b.m)
                    for (int z = 0; z < a.n; z++)
                    {
                        // and compute their scalar product
                        result[y, x] += a[y, z] * b[z, x];
                    }
                }
            }

            return result;
        }
        /// <summary>
        /// Also called 'Outer Product'.
        /// </summary>
        public static IMatrix KroneckerProduct(IMatrix a, IMatrix b)
        {
            int m = a.m * b.m;
            int n = a.n * b.n;

            IMatrix result = new Matrix(m, n);

            for (int y = 0; y < a.m; y++)
            {
                for (int x = 0; x < a.n; x++)
                {
                    IMatrix tmpResult = a[y, x] * b;
                    for (int yy = 0; yy < b.m; yy++)
                    {
                        for (int xx = 0; xx < b.n; xx++)
                        {
                            int yResult = (y * b.m) + yy;
                            int xResult = (x * b.n) + xx;
                            result[yResult, xResult] = tmpResult[yy, xx];
                        }
                    }
                    
                }
            }

            return result;
        }
        /// <summary>
        /// Also called 'Elementwise Product' or 'Schur Product'. 
        /// Condition: a.m = b.m & a.n = b.n 
        /// </summary>
        public static IMatrix HadamardProduct(IMatrix a, IMatrix b)
        {
            if (a.m != b.m || a.n != b.n)
                throw new ArgumentException("a.m must equal b.m and a.n must equal b.n");

            IMatrix result = new Matrix(b.m, b.n);

            for (int x = 0; x < b.n; x++)
            {
                for (int y = 0; y < b.m; y++)
                {
                    result[y, x] = a[y, x] * b[y, x];
                }
            }

            return result;
        }
        public static IMatrix FlattenToOneColumn(IMatrix a)
        {
            IMatrix result = new Matrix((int)a.LongCount());

            int i = 0;
            foreach (var item in a)
            {
                result[i++] = item;
            }

            return result;
        }
    }
}
