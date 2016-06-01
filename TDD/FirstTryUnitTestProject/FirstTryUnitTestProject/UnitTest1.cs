using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using FirstTryUnitTestProject.MyResources;

namespace RealFirstTryTestProject
{
    [TestClass]
    public class TestForStringBuilder
    {
        public StringBuilder TestStringBuilderObject;

        [TestInitialize]
        public void BeginningOfEachTest()
        {
            TestStringBuilderObject = new StringBuilder(StringsForTests.InitializingString);
        }

        [TestMethod]
        public void TestAppendMethod()
        {
            TestStringBuilderObject.Append(StringsForTests.ParameterForMethods);
            Assert.IsTrue(TestStringBuilderObject.ToString() == StringsForTests.CheckString, StringsForTests.TestAppendMethodFail);       
        }

        [TestMethod]
        public void TestAppendLineMethod()
        {
            string CurrentLineTerminator = Environment.NewLine;
            string CmpString = StringsForTests.CheckString + CurrentLineTerminator;
            TestStringBuilderObject.AppendLine(StringsForTests.ParameterForMethods);
            Assert.IsTrue(TestStringBuilderObject.ToString() == CmpString, StringsForTests.TestAppendLineMethodFail);
        }

        [TestMethod]
        public void TestClearMethod()
        {
            TestStringBuilderObject.Clear();
            Assert.IsTrue(TestStringBuilderObject.ToString() == string.Empty, StringsForTests.TestClearMethodFail);
        }
    }
}
