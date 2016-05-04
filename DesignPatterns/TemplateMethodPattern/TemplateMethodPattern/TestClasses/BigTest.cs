using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMethodPattern.TestClasses
{
    class BigTest : AbstractTest
    {
        protected override void StartTest()
        {
            Result.Append("Preparation for big test\r\n");
        }

        protected override void TestItself()
        {
            Result.Append("Performing big test\r\n");
        }

        protected override void TestResults()
        {
            Result.Append("Getting big test results\r\n\r\n");
        }
    }
}
