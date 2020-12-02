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
        public static Matrix ForEach(this Matrix a, Func<float, float> func)
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
        public static Matrix ForEach(this Matrix a, Matrix b, Func<float, float> func)
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
    }
}
