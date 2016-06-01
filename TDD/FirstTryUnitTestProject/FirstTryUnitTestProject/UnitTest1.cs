using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using FirstTryUnitTestProject.MyResources;
using FirstTryUnitTestProject;

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
            string TestFailMessage = WrongStringMessage(StringsForTests.TestAppendMethodFail, StringsForTests.CheckString, TestStringBuilderObject.ToString());
            Assert.IsTrue(TestStringBuilderObject.ToString() == StringsForTests.CheckString, TestFailMessage);       
        }

        [TestMethod]
        public void TestAppendLineMethod()
        {
            string CurrentLineTerminator = Environment.NewLine;
            string CmpString = StringsForTests.CheckString + CurrentLineTerminator;
            TestStringBuilderObject.AppendLine(StringsForTests.ParameterForMethods);
            string TestFailMessage = WrongStringMessage(StringsForTests.TestAppendLineMethodFail, CmpString, TestStringBuilderObject.ToString());
            Assert.IsTrue(TestStringBuilderObject.ToString() == CmpString, TestFailMessage);
        }

        [TestMethod]
        public void TestClearMethod()
        {
            TestStringBuilderObject.Clear();
            string TestFailMessage = WrongStringMessage(StringsForTests.TestClearMethodFail, string.Empty, TestStringBuilderObject.ToString());
            Assert.IsTrue(TestStringBuilderObject.ToString() == string.Empty, TestFailMessage);
        }

        [TestMethod]
        public void TestAppendFormatMethod()
        {
            TestStringBuilderObject.AppendFormat(StringsForTests.ParameterForMethods + " {0} {1} {2}", 5, 4.9, 'F');
            string TestFailMessage = WrongStringMessage(StringsForTests.TestAppendFormatFail, StringsForTests.CheckStringForAppendFormat, TestStringBuilderObject.ToString());
            Assert.IsTrue(TestStringBuilderObject.ToString() == StringsForTests.CheckStringForAppendFormat, TestFailMessage);
        }

        private string WrongStringMessage(string TestFailDescription, string StringForComparising, string ResultString)
        {
            return TestFailDescription + " " + StringsForTests.ExpectedString + "\"" + StringForComparising + "\". " + StringsForTests.RetrievedString + "\"" + ResultString + "\".";
        }
    }

    /*[TestClass]
    public class TestClassForTestClass
    {
        [TestMethod]
        public void TestForTestMethod()
        {
            Assert.AreEqual(TestClass.TestMethod(), true);
        }
    }*/
}
