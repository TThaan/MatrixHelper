using System;

namespace MatrixHelper
{
    public static class ExtensionMethods
    {
        public static Matrix DumpToConsole(this Matrix a, string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                Console.WriteLine(title);
            }

            Console.Write(" ");
            for (int n = 0; n < a.n; n++)
            {
                Console.Write("----------------");
            }
            Console.WriteLine();

            for (int j = 0; j < a.m; j++)
            {
                if (a.n == 1)
                {
                    Console.Write(string.Format("|{0, 15}", a[j]));
                }
                else
                {
                    for (int k = 0; k < a.n; k++)
                    {
                        Console.Write(string.Format("|{0, 15}", a[j, k]));
                    }
                }
                Console.WriteLine("|");
            }

            Console.Write(" ");
            for (int n = 0; n < a.n; n++)
            {
                Console.Write("----------------");
            }

            return a;
        }
        public static Matrix ForEach_New(this Matrix a, Func<float, float> func)
        {
            Matrix result = new Matrix(a.m, a.n);

            for (int j = 0; j < a.m; j++)
            {
                for (int k = 0; k < a.n; k++)
                {
                    result[j,k]= func(a[j, k]);
                }
            }

            return result;
        }
        /// <summary>
        /// This func takes the value of matrix b[j,k] as parameter and 
        /// its' return value becomes the new value of matrix a[j,k].
        /// </summary>
        public static Matrix ForEach_New(this Matrix a, Matrix b, Func<float, float> func)
        {
            Matrix result = new Matrix(a.m, a.n);

            for (int j = 0; j < a.m; j++)
            {
                for (int k = 0; k < a.n; k++)
                {
                    result[j, k] = func(b[j, k]);
                }
            }

            return result;
        }

        #region returning the same matrix and ignoring size checks

        public static Matrix ForEach(this Matrix result, Func<float, float> func)
        {
            for (int j = 0; j < result.m; j++)
            {
                for (int k = 0; k < result.n; k++)
                {
                    result[j, k] = func(result[j, k]);
                }
            }

            return result;
        }
        /// <summary>
        /// This func takes the value of matrix b[j,k] as parameter and 
        /// its' return value becomes the new value of matrix a[j,k].
        /// </summary>
        public static Matrix ForEach(this Matrix result, Matrix b, Func<float, float> func)
        {
            for (int j = 0; j < result.m; j++)
            {
                for (int k = 0; k < result.n; k++)
                {
                    result[j, k] = func(b[j, k]);
                }
            }

            return result;
        }
        public static Matrix Add(this Matrix result, Matrix b)
        {
            for (int x = 0; x < b.n; x++)
            {
                for (int y = 0; y < b.m; y++)
                {
                    result[y, x] = result[y, x] + b[y, x];
                }
            }
            return result;
        }
        public static Matrix Subtract(this Matrix result, Matrix b)
        {
            for (int x = 0; x < b.n; x++)
            {
                for (int y = 0; y < b.m; y++)
                {
                    result[y, x] = result[y, x] - b[y, x];
                }
            }
            return result;
        }
        public static Matrix Multiplicate(this Matrix result, float f)
        {
            for (int x = 0; x < result.n; x++)
            {
                for (int y = 0; y < result.m; y++)
                {
                    result[y, x] = result[y, x] * f;
                }
            }
            return result;
        }
        public static Matrix Divide(this Matrix result, float f)
        {
            for (int x = 0; x < result.n; x++)
            {
                for (int y = 0; y < result.m; y++)
                {
                    result[y, x] = result[y, x] / f;
                }
            }
            return result;
        }
        public static Matrix MultiplicateLikeHadamard(this Matrix result, Matrix b)
        {
            for (int x = 0; x < result.n; x++)
            {
                for (int y = 0; y < result.m; y++)
                {
                    result[y, x] = result[y, x] * b[y, x];
                }
            }
            return result;
        }
        public static Matrix GetSacalarProduct(this Matrix result, Matrix a, Matrix b)
        {
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

        #endregion
    }
}