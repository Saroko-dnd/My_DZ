using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace RealFirstTryTestProject
{
    [TestClass]
    public class TestForStringBuilder
    {
        public StringBuilder TestStringBuilderObject;
        [TestMethod]
        public void TestAppendMethod()
        {
            TestStringBuilderObject = new StringBuilder("Test string!");
            TestStringBuilderObject.Append("bullet");
            Assert.IsTrue( TestStringBuilderObject.ToString() == "Test string!bullet");           
        }

        [TestMethod]
        public void TestAppendLineMethod()
        {
            TestStringBuilderObject = new StringBuilder("Test string!");
            string CurrentLineTerminator = Environment.NewLine;
            string CmpString = "Test string!bullet" + CurrentLineTerminator;
            TestStringBuilderObject.AppendLine("bullet");
            Assert.IsTrue(TestStringBuilderObject.ToString() ==  CmpString);
        }

        [TestMethod]
        public void TestClearMethod()
        {
            TestStringBuilderObject = new StringBuilder("Test string!");
            TestStringBuilderObject.Clear();
            Assert.IsTrue(TestStringBuilderObject.ToString() == string.Empty);
        }
    }
}
