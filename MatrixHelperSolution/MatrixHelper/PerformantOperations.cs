namespace MatrixHelper
{
    public class PerformantOperations
    {
        #region Set values of an injected 'result'-matrix without returning it and ignoring size checks

        public static void Add(IMatrix a, IMatrix b, IMatrix result)
        {
            for (int x = 0; x < result.n; x++)
            {
                for (int y = 0; y < result.m; y++)
                {
                    result[y, x] = a[y, x] + b[y, x];
                }
            }
        }
        public static void Subtract(IMatrix a, IMatrix subtrahend, IMatrix result)
        {
            for (int x = 0; x < result.n; x++)
            {
                for (int y = 0; y < result.m; y++)
                {
                    result[y, x] = a[y, x] - subtrahend[y, x];
                }
            }
        }
        public static void Multiplicate(IMatrix a, float factor, IMatrix result)
        {
            for (int x = 0; x < result.n; x++)
            {
                for (int y = 0; y < result.m; y++)
                {
                    result[y, x] = a[y, x] * factor;
                }
            }
        }
        public static void Divide(IMatrix a, float divisor, IMatrix result)
        {
            for (int x = 0; x < result.n; x++)
            {
                for (int y = 0; y < result.m; y++)
                {
                    result[y, x] = a[y, x] / divisor;
                }
            }
        }
        public static void SetHadamardProduct(IMatrix a, IMatrix b, IMatrix result)
        {
            for (int x = 0; x < result.n; x++)
            {
                for (int y = 0; y < result.m; y++)
                {
                    result[y, x] = a[y, x] * b[y, x];
                }
            }
        }
        public static void SetScalarProduct(IMatrix a, IMatrix b, IMatrix result)
        {
            // Make sure the result has only 0 - values
            // result.ForEach(x => 0);

            // For each row of matrix 'a'
            for (int y = 0; y < a.m; y++)
            {
                // you take each column of matrix 'b', 
                for (int x = 0; x < b.n; x++)
                {
                    // iterate over each value of a's columns and b's rows (a.n=b.m)
                    for (int z = 0; z < a.n; z++)
                    {
                        // (Make sure the result starts with each cell = 0.)
                        if (z == 0)
                            result[y, x] = 0;

                        // and compute their scalar product.
                        result[y, x] += a[y, z] * b[z, x];
                    }
                }
            }
        }

        #endregion
    }
}
