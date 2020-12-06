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
        public static Matrix ProductWithAScalar(float a, Matrix b)
        {
            Matrix result = new Matrix(b.m, b.n);

            for (int x = 0; x < b.n; x++)
            {
                for (int y = 0; y < b.m; y++)
                {
                    result[y, x] = a * b[y, x];
                }
            }

            return result;
        }
        public static Matrix DivisionWithAScalar(float a, Matrix b)
        {
            Matrix result = new Matrix(b.m, b.n);

            for (int x = 0; x < b.n; x++)
            {
                for (int y = 0; y < b.m; y++)
                {
                    result[y, x] = a / b[y, x];
                }
            }

            return result;
        }
        /// <summary>
        /// Condition: a.m = b.m & a.n = b.n.
        /// </summary>
        /// <returns></returns>
        public static Matrix Addition(Matrix a, Matrix b)
        {
            if (a.m != b.m || a.n != b.n)
                throw new ArgumentException("a.m must equal b.m and a.n must equal b.n");

            Matrix result = new Matrix(b.m, b.n);

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
        public static Matrix Subtraction(Matrix a, Matrix b)
        {
            if (a.m != b.m || a.n != b.n)
                throw new ArgumentException("a.m must equal b.m and a.n must equal b.n");

            Matrix result = new Matrix(b.m, b.n);

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
        public static Matrix ScalarProduct(Matrix a, Matrix b)
        {
            if (a.n != b.m)
                throw new ArgumentException("a.n must equal b.m");

            Matrix result = new Matrix(a.m, b.n);

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
        public static Matrix KroneckerProduct(Matrix a, Matrix b)
        {
            int m = a.m * b.m;
            int n = a.n * b.n;

            Matrix result = new Matrix(m, n);

            for (int y = 0; y < a.m; y++)
            {
                for (int x = 0; x < a.n; x++)
                {
                    Matrix tmpResult = a[y, x] * b;
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
        public static Matrix HadamardProduct(Matrix a, Matrix b)
        {
            if (a.m != b.m || a.n != b.n)
                throw new ArgumentException("a.m must equal b.m and a.n must equal b.n");

            Matrix result = new Matrix(b.m, b.n);

            for (int x = 0; x < b.n; x++)
            {
                for (int y = 0; y < b.m; y++)
                {
                    result[y, x] = a[y, x] * b[y, x];
                }
            }

            return result;
        }
        /// <summary>
        /// Identic derivation for each dScalar/db_jk.
        /// </summary>
        
        public static float TotalSum(Matrix a)
        {
            return a.Sum();
        }
        public static Matrix FlattenToOneColumn(Matrix a)
        {
            Matrix result = new Matrix((int)a.LongCount());

            int i = 0;
            foreach (var item in a)
            {
                result[i++] = item;
            }

            return result;
        }

        // Still contemplating about the utility of Partials here..

        //public static Matrix Partial(float scalar, Matrix b, Func<float, float, float> derivation)
        //{
        //    var result = new Matrix(b.n, b.m);

        //    for (int j = 0; j < b.m; j++)
        //    {
        //        for (int k = 0; k < b.n; k++)
        //        {
        //            result[k, j] = derivation(scalar, b[k, j]);
        //        }
        //    }

        //    return result;
        //}
        ///// <summary>
        ///// ...
        ///// </summary>
        //public static Matrix Partial(float scalar, Matrix b, Func<float, float, float>[,] derivations)
        //{
        //    var result = new Matrix(b.n, b.m);

        //    // "Goto" next column of matrix b
        //    for (int k = 0; k < b.n; k++)
        //    {
        //        // and iterate over this column to get it's derivation and
        //        // put it into (the transposed) result.
        //        for (int j = 0; j < b.m; j++)
        //        {
        //            result[k, j] = derivations[j, k](scalar, b[j, k]);
        //        }
        //    }

        //    return result;
        //}
        ///// <summary>
        ///// The first F(=f1) gets derived with respect to each x(=x1, x2, etc),
        ///// one after the other, into the first row of the result.
        ///// </summary>
        //public static Matrix Partial(Matrix a, float scalar, Func<float, float, float>[,] derivation)
        //{
        //    throw new NotImplementedException();
        //}
        ///// <summary>
        ///// Identic derivation for each dScalar/db_jk.
        ///// </summary>
        //public static Matrix Partial(Matrix a, Matrix b, Func<float, float, float> derivation)
        //{
        //    Matrix result = new Matrix(a.m, b.m);

        //    if (a.n == 1 && b.n == 1)
        //    {
        //        for (int j = 0; j < a.m; j++)
        //        {
        //            for (int k = 0; k < b.m; k++)
        //            {
        //                result[j, k] = derivation(a[j, 1], b[k, 1]);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        throw new NotImplementedException("This function is only implemented for two (vertical) one-dimensional matrices so far.");
        //    }
        //    return result;
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //public static Matrix Partial(Matrix a, Matrix b, Func<float, float, float>[,] derivations)
        //{
        //    Matrix result = new Matrix(a.m, b.m);

        //    if (a.n == 1 && b.n == 1)
        //    {
        //        for (int j = 0; j < a.m; j++)
        //        {
        //            for (int k = 0; k < b.m; k++)
        //            {
        //                result[j, k] = derivations[j, k](a[j, 1], b[k, 1]);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        throw new NotImplementedException("This function is only implemented for two (vertical) one-dimensional matrices so far.");
        //    }
        //    return result;
        //}



        //public static Func<Matrix, Matrix, Matrix, Matrix> f = F();
        //public static Func<Matrix, Matrix, Matrix, Matrix> F()
        //{
        //    return (a, w, b) => InnerProduct(w, a) + b;
        //}

        //public static Matrix FunctionOf(Matrix a, Func)
        //{
        //    Matrix result = new Matrix((int)a.LongCount());

        //    int i = 0;
        //    foreach (var item in a)
        //    {
        //        result[i++] = item;
        //    }

        //    return result;
        //}
    }
}
