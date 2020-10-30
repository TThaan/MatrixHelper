using System;

namespace MatrixHelper
{
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
        /// Also called 'Elementwise Product'. 
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

        public static Matrix Partial(Matrix a, Matrix b)
        {
            throw new NotImplementedException();
        }

        //public static Func<Matrix, Matrix, Matrix, Matrix> f = F();
        //public static Func<Matrix, Matrix, Matrix, Matrix> F()
        //{
        //    return (a, w, b) => InnerProduct(w, a) + b;
        //}
    }
}
