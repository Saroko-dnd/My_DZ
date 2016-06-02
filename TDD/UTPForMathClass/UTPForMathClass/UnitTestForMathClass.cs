using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UTPForMathClass.Resources;

namespace UTPForMathClass
{
    [TestClass]
    public class UnitTestForMathClass
    {
        [TestMethod]
        [ExpectedException(typeof(FormatException), "Тест не пройден, так как метод Cos(string) съел текст и не подавился, хотя должен был принимать только числа Double в текстовом виде")]
        public void TextCosWrongStringFormat()
        {
            MathClass.Cos("rel");
        }

        [TestMethod]
        public void TestResultOfCosMethod()
        {
            Assert.AreEqual(Math.Cos(13), MathClass.Cos("13"), 0.000001);
            Assert.AreEqual(Math.Cos(0), MathClass.Cos("0"), 0.000001);
            Assert.AreEqual(Math.Cos(-13), MathClass.Cos("-13"), 0.000001);
        }

        [TestMethod]
        public void TestForFactorial()
        {
            Assert.AreEqual((int)MathClass.Factorial(5), 120);
        }

        [TestMethod]
        public void TestForPower()
        {
            Assert.AreEqual((int)MathClass.RaisedToThePower(5,2), 25);
        }
    }
}
