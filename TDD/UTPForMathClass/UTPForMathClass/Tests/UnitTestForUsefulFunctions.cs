using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UTPForMathClass.Tests
{
    [TestClass]
    public class UnitTestForUsefulFunctions
    {
        [TestMethod]
        public void TestForFactorial()
        {
            Assert.AreEqual((int)UsefulFunctions.Factorial(5), 120);
        }

        [TestMethod]
        public void TestForPower()
        {
            Assert.AreEqual((int)UsefulFunctions.RaiseDoubleToDegree(5, 2), 25);
        }
    }
}
