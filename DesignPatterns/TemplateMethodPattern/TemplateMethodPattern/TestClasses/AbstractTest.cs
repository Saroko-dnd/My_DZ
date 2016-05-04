using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TemplateMethodPattern.TestClasses
{
    public abstract class AbstractTest
    {
        protected StringBuilder Result;

        public StringBuilder PerformTest()
        {
            Result = new StringBuilder();
            StartTest();
            TestItself();
            TestResults();
            return Result;
        }

        protected abstract void StartTest();
        protected abstract void TestItself();
        protected abstract void TestResults();

    }
}
