using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixHelper
{
    public class Operations
    {
        /// <summary>
        /// Also called 'Dot Product' or 'Scalar Product'.
        /// Requirements: b = one dimensional (m x 1 Matrix)
        /// </summary>
        public static Matrix InnerProduct(Matrix a, Matrix b)
        {
            Matrix result = new Matrix(a.m, b.n);

            for (int y = 0; y < a.m; y++)
            {
                // result.content[y, 0] = default(float);

                for (int x = 0; x < a.n; x++)
                {
                    result[y, 0] += a[y, x] * b[x, 0];
                }
            }

            return result;
        }
        /// <summary>
        /// Also called 'Elementwise Product'. 
        /// Requirements: a.m = b.m and a.n = b.n 
        /// </summary>
        public static Matrix HadamardProduct(Matrix a, Matrix b)
        {
            if (a.m != b.m || a.n != b.n)
            {
                throw new ArgumentException(
                    "For the element-wise product of two matrices " +
                    "they need to have the same height m and the same width n!");
            }

            Matrix result = new Matrix(a.m, a.n);

            for (int y = 0; y < a.m; y++)
            {
                for (int x = 0; x < a.n; x++)
                {
                    result[y, x] = a[y, x] * b[y, x];
                }
            }

            return result;
        }
        /// <summary>
        /// Also called 'Kronecker Product'.
        /// </summary>
        public static Matrix OuterProduct(Matrix a, Matrix b)
        {
            throw new NotImplementedException();
        }
        public static Matrix Partial(Matrix a, Matrix b)
        {
            throw new NotImplementedException();
        }

        public static Func<Matrix, Matrix, Matrix, IMatrix> f = F();
        public static Func<Matrix, Matrix, Matrix, IMatrix> F()
        {
            return (a, w, b) => InnerProduct(w, a) + b;
        }
    }
}
