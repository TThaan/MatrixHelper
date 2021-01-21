using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MatrixHelper
{
    public static class ExtensionMethods
    {
        public static IMatrix GetCopy(this IMatrix a, string newLoggableName = default)
        {
            if (a == null)
            {
                return null;
            }
            return new Matrix(a, string.IsNullOrEmpty(newLoggableName) ? a.LoggableName : newLoggableName);
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

        #endregion
    }
}