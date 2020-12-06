using MatrixHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MatrixHelperTest
{
    [TestClass]
    public class MatrixHelperTest
    {
        #region Example Data

        static readonly Matrix A14 = new Matrix(
            new float[,]
            {
                    {1},
                    {2},
                    {-3},
                    {4}
            });
        static readonly Matrix A41 = new Matrix(
            new float[,]
            {
                    { 1, 2, -3, 4 }
            });
        static readonly Matrix A32 = new Matrix(
                new float[,]
                {
                    {2, 9},
                    {-3, 18},
                    {7, 10}
                });
        static readonly Matrix A32_BackUp = new Matrix(
                new float[,]
                {
                    {2, 9},
                    {-3, 18},
                    {7, 10}
                });
        const float X = 2, Y = 10;

        #endregion

        #region Expected Results

        static readonly Matrix Expected_A32Transpose = new Matrix(
                new float[,]
                {
                    {2, -3, 7 },
                    {9, 18, 10}
                });
        static readonly Matrix Expected_AdditionOfA32AndA32 = new Matrix(
                new float[,]
                {
                    {4, 18},
                    {-6, 36},
                    {14, 20}
                });
        static readonly Matrix Expected_SubtractionOfA32AndA32 = new Matrix(
                new float[,]
                {
                    {0, 0},
                    {0, 0},
                    {0, 0}
                });
        static readonly Matrix Expected_MultiplicationOfA32AndX = new Matrix(
                new float[,]
                {
                    {4, 18},
                    {-6, 36},
                    {14, 20}
                });
        static readonly Matrix Expected_DivisionOfA32ByY = new Matrix(
                new float[,]
                {
                    {.2f, .9f},
                    {-.3f, 1.8f},
                    {.7f, 1}
                });
        static readonly Matrix Expected_ScalarProductOfA32WithA32Transpose = new Matrix(
                new float[,]
                {
                    {85f, 156f, 104f },
                    {156f, 333f, 159f },
                    {104f, 159f, 149f }
                });
        static readonly Matrix Expected_HadamarProductOfA32AndA32 = new Matrix(
                new float[,]
                {
                    {4, 81 },
                    {9, 324 },
                    {49,100 }
                });
        //static Matrix KroneckerProductOfA32AndA32Transpose = new Matrix(
        //        new float[,]
        //        {
        //            {0, 0},
        //            {0, 0},
        //            {0, 0}
        //        });

        #endregion

        #region Testing extension methods that return an 'involved matrix'.

        [TestMethod]
        public void Test_Transpose_ReturningInvolvedMatrix()
        {
            var expect = Expected_A32Transpose;
            var actual = A32.Transpose;

            Assert.AreEqual(expect, actual);
            A32.ForEach(A32_BackUp, x => x);
        }
        
        [TestMethod]
        public void Test_Addition_ReturningInvolvedMatrix()
        {
            var expect = Expected_AdditionOfA32AndA32;
            var actual = A32.Add(A32);

            Assert.AreEqual(expect, actual);
            A32.ForEach(A32_BackUp, x => x);
        }
        [TestMethod]
        public void Test_Subtraction_ReturningInvolvedMatrix()
        {
            var expect = Expected_SubtractionOfA32AndA32;
            var actual = A32.Subtract(A32);

            Assert.AreEqual(expect, actual);
            A32.ForEach(A32_BackUp, x => x);
        }
        [TestMethod]
        public void Test_Multiplication_ReturningInvolvedMatrix()
        {
            var expect = Expected_MultiplicationOfA32AndX;
            var actual = A32.Multiplicate(X);

            Assert.AreEqual(expect, actual);
            A32.ForEach(A32_BackUp, x => x);
        }
        [TestMethod]
        public void Test_Division_ReturningInvolvedMatrix()
        {
            var expect = Expected_DivisionOfA32ByY;
            var actual = A32.Divide(Y);

            Assert.AreEqual(expect, actual);
            A32.ForEach(A32_BackUp, x => x);
        }
        [TestMethod]
        public void Test_HadamardProduct_ReturningInvolvedMatrix()
        {
            var expect = Expected_HadamarProductOfA32AndA32;
            var actual = A32.GetHadamardProduct(A32);

            Assert.AreEqual(expect, actual);
            A32.ForEach(A32_BackUp, x => x);
        }

        #endregion

        #region Testing extension methods that return a dedicated 'result matrix'.

        [TestMethod]
        public void Test_Addition_ReturningResultMatrix()
        {
            var expect = Expected_AdditionOfA32AndA32;
            Matrix resultMatrix = new Matrix(A32.m, A32.n);
            resultMatrix = resultMatrix.Add(A32, A32);

            Assert.AreEqual(expect, resultMatrix);
            A32.ForEach(A32_BackUp, x => x);
        }
        [TestMethod]
        public void Test_Subtraction_ReturningResultMatrix()
        {
            var expect = Expected_SubtractionOfA32AndA32;
            Matrix resultMatrix = new Matrix(A32.m, A32.n);
            resultMatrix = resultMatrix.Subtract(A32, A32);

            Assert.AreEqual(expect, resultMatrix);
            A32.ForEach(A32_BackUp, x => x);
        }
        [TestMethod]
        public void Test_Multiplication_ReturningResultMatrix()
        {
            var expect = Expected_MultiplicationOfA32AndX;
            Matrix resultMatrix = new Matrix(A32.m, A32.n);
            resultMatrix = resultMatrix.Multiplicate(A32, X);

            Assert.AreEqual(expect, resultMatrix);
            A32.ForEach(A32_BackUp, x => x);
        }
        [TestMethod]
        public void Test_Division_ReturningResultMatrix()
        {
            var expect = Expected_DivisionOfA32ByY;
            Matrix resultMatrix = new Matrix(A32.m, A32.n);
            resultMatrix = resultMatrix.Divide(A32, Y);

            Assert.AreEqual(expect, resultMatrix);
            A32.ForEach(A32_BackUp, x => x);
        }
        [TestMethod]
        public void Test_ScalarProduct_ReturningResultMatrix()
        {
            var expect = Expected_ScalarProductOfA32WithA32Transpose;
            Matrix resultMatrix = new Matrix(A32.m, Expected_A32Transpose.n);
            resultMatrix = resultMatrix.GetScalarProduct(A32, Expected_A32Transpose);

            Assert.AreEqual(expect, resultMatrix);
            A32.ForEach(A32_BackUp, x => x);
        }
        [TestMethod]
        public void Test_HadamardProduct_ReturningResultMatrix()
        {
            var expect = Expected_HadamarProductOfA32AndA32;
            Matrix resultMatrix = new Matrix(A32.m, A32.n);
            resultMatrix = resultMatrix.GetHadamardProduct(A32, A32);

            Assert.AreEqual(expect, resultMatrix);
            A32.ForEach(A32_BackUp, x => x);
        }
        //[TestMethod]
        //public void Test_KroneckerProduct()
        //{
        //    var expect = KroneckerProductOfA32AndA32Transpose;
        //    var actual = Operations.KroneckerProduct(A32, A32Transpose);
        //    Assert.AreEqual(expect, actual);
        //    A32 = A32BackUp;
        //}

        #endregion
    }
}