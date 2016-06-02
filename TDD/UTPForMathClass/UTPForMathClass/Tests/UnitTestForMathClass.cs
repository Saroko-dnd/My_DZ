using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UTPForMathClass.Resources;

namespace UTPForMathClass
{
    [TestClass]
    public class UnitTestForMathClass
    {
        [TestMethod]
        [ExpectedException(typeof(FormatException), "Тест не пройден, так как метод Cos(string) 'съел' текст и не подавился, хотя должен был принимать только числа Double в текстовом виде.")]
        public void TestCosWrongStringFormat()
        {
            MathClass.Cos(TestStringValues.WrongStringFormatTestValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Тест не пройден, так как метод Cos(string) 'съел' null вместо строки и не подавился, хотя должен был принимать только числа Double в текстовом виде.")]
        public void TestCosStringIsNull()
        {
            MathClass.Cos(null);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException), "Тест не пройден, так как метод Cos(string) 'съел' строку содержацую число превышающее максимальное значение double.")]
        public void TestCosStringContainTooBigNumber()
        {
            MathClass.Cos(TestStringValues.StringContainTooBigNumberTestValue);
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException), "Тест не пройден, так как метод Cos(string) 'съел' строку содержацую число меньшее чем минимальное значение double.")]
        public void TestCosStringContainTooSmallNumber()
        {
            MathClass.Cos(TestStringValues.StringContainTooSmallNumberTestValue);
        }

        [TestMethod]
        public void TestResultOfCosMethod()
        {
            Assert.AreEqual(Math.Cos(13), MathClass.Cos("13"), 0.000001, FailMessages.AccuracyFailMessage);
            Assert.AreEqual(Math.Cos(0), MathClass.Cos("0"), 0.000001, FailMessages.AccuracyFailMessage);
            Assert.AreEqual(Math.Cos(-13), MathClass.Cos("-13"), 0.000001, FailMessages.AccuracyFailMessage);
        }
    }
}
