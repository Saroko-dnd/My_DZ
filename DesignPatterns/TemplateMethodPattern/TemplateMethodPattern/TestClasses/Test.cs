using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TemplateMethodPattern.TestClasses
{
    class Test : AbstractTest
    {
        protected override void StartTest()
        {
            MessageBox.Show("Test started");
        }

        protected override void TestItself()
        {
            MessageBox.Show("Test procssing");
            Random NewTestRandom = new Random();
            for (int Counter = 0; Counter < 10; ++Counter)
            {
                ResultValue = NewTestRandom.Next(1,10);
            }
        }
        protected override void TestResults()
        {
            if (ResultValue > 50)
            {
                MessageBox.Show("Success!");
            }
            else
            {
                MessageBox.Show("Failure!!");
            }

        } 
    }
}
