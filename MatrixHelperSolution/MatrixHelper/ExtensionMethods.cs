using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MatrixHelper
{
    public static class ExtensionMethods
    {
        public static IMatrix GetCopy(this IMatrix a)
        {
            if (a == null)
            {
                return null;
            }
            return new Matrix(a, default);
        }
        //public static IMatrix GetCopy(this IMatrix a, string newName, ILogger logger = null)
        //{
        //    if (a == null)
        //    {
        //        return null;
        //    }
        //    return new Matrix(a, newName, logger);
        //}
        // Rename to generic "SpecificLog()" to make use of it in ILoggable interface
        // and make it an instance in Matrix class (as ILoggable implementation)!
        public static IMatrix DumpToConsole(this IMatrix a, string title)
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
        public static List<float> ToList(this IMatrix a)
        {
            List<float> result = new List<float>(a.m * a.n);

            foreach (var item in a)
            {
                result.Add(item);
            }

            return result;
        }
        public static void Serialize(this IMatrix matrix, string fileName) 
        {
            using (Stream stream = File.Open(fileName, FileMode.Create))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(stream, matrix);
            }
        }
        public static IMatrix Deserialize(this IMatrix matrix, string fileName)
        {
            using (Stream stream = File.Open(fileName, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (IMatrix)bf.Deserialize(stream);
            }
        }

        #region returning one of the matrices involved in the formula and ignoring size checks

        public static IMatrix ForEach(this IMatrix result, Func<float, float> func)
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
        /// the funcs' return value becomes the new value of matrix a[j,k].
        /// </summary>
        public static IMatrix ForEach(this IMatrix result, IMatrix b, Func<float, float> func)
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
        //public static Matrix Add(this Matrix result, Matrix addend)
        //{
        //    for (int x = 0; x < result.n; x++)
        //    {
        //        for (int y = 0; y < result.m; y++)
        //        {
        //            result[y, x] = result[y, x] + addend[y, x];
        //        }
        //    }
        //    return result;
        //}
        //public static Matrix Subtract(this Matrix result, Matrix subtrahend)
        //{
        //    for (int x = 0; x < result.n; x++)
        //    {
        //        for (int y = 0; y < result.m; y++)
        //        {
        //            result[y, x] = result[y, x] - subtrahend[y, x];
        //        }
        //    }
        //    return result;
        //}
        //public static Matrix Multiplicate(this Matrix result, float factor)
        //{
        //    for (int x = 0; x < result.n; x++)
        //    {
        //        for (int y = 0; y < result.m; y++)
        //        {
        //            result[y, x] = result[y, x] * factor;
        //        }
        //    }
        //    return result;
        //}
        //public static Matrix Divide(this Matrix result, float divisor)
        //{
        //    for (int x = 0; x < result.n; x++)
        //    {
        //        for (int y = 0; y < result.m; y++)
        //        {
        //            result[y, x] = result[y, x] / divisor;
        //        }
        //    }
        //    return result;
        //}
        //public static Matrix GetHadamardProduct(this Matrix result, Matrix b)
        //{
        //    for (int x = 0; x < result.n; x++)
        //    {
        //        for (int y = 0; y < result.m; y++)
        //        {
        //            result[y, x] = result[y, x] * b[y, x];
        //        }
        //    }
        //    return result;
        //}

        #endregion

        #region returning a dedicated result matrix (independent from formula) and ignoring size checks

        //public static Matrix Add(this Matrix result, Matrix a, Matrix b)
        //{
        //    for (int x = 0; x < result.n; x++)
        //    {
        //        for (int y = 0; y < result.m; y++)
        //        {
        //            result[y, x] = a[y, x] + b[y, x];
        //        }
        //    }
        //    return result;
        //}
        //public static Matrix Subtract(this Matrix result, Matrix a, Matrix subtrahend)
        //{
        //    for (int x = 0; x < result.n; x++)
        //    {
        //        for (int y = 0; y < result.m; y++)
        //        {
        //            result[y, x] = a[y, x] - subtrahend[y, x];
        //        }
        //    }
        //    return result;
        //}
        //public static Matrix Multiplicate(this Matrix result, Matrix a, float factor)
        //{
        //    for (int x = 0; x < result.n; x++)
        //    {
        //        for (int y = 0; y < result.m; y++)
        //        {
        //            result[y, x] = a[y, x] * factor;
        //        }
        //    }
        //    return result;
        //}
        //public static Matrix Divide(this Matrix result, Matrix a, float divisor)
        //{
        //    for (int x = 0; x < result.n; x++)
        //    {
        //        for (int y = 0; y < result.m; y++)
        //        {
        //            result[y, x] = a[y, x] / divisor;
        //        }
        //    }
        //    return result;
        //}
        //public static void SetHadamardProduct(this Matrix result, Matrix a, Matrix b)
        //{
        //    for (int x = 0; x < result.n; x++)
        //    {
        //        for (int y = 0; y < result.m; y++)
        //        {
        //            result[y, x] = a[y, x] * b[y, x];
        //        }
        //    }
            
        //    //return result;
        //}
        //public static void SetScalarProduct(this Matrix result, Matrix a, Matrix b)
        //{
        //    // Make sure the result has only 0 - values
        //    // result.ForEach(x => 0);

        //    // For each row of matrix 'a'
        //    for (int y = 0; y < a.m; y++)
        //    {
        //        // you take each column of matrix 'b', 
        //        for (int x = 0; x < b.n; x++)
        //        {
        //            // iterate over each value of a's columns and b's rows (a.n=b.m)
        //            for (int z = 0; z < a.n; z++)
        //            {
        //                // make sure the result has only 0 - values
        //                //if (z == 0)
        //                //    result[y, x] = 0;

        //                // and compute their scalar product
        //                result[y, x] += a[y, z] * b[z, x];
        //            }
        //        }
        //    }

        //    //return result;
        //}

        #endregion
    }
}