using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TemplateMethodPattern.TestClasses
{
    public abstract class AbstractTest
    {
        protected int ResultValue;
        public void PerformTest()
        {
            StartTest();
            TestItself();
            TestResults();
        }

        protected abstract void StartTest();
        protected abstract void TestItself();
        protected abstract void TestResults();

    }
}
