using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMethodPattern.TestClasses
{
    class SmallTest : AbstractTest
    {
        protected override void StartTest()
        {
            Result.Append("Preparation for small test");
        }

        protected override void TestItself()
        {
            Result.Append("Performing small test");
        }

        protected override void TestResults()
        {
            Result.Append("Getting small test results");
        }
    }
}
