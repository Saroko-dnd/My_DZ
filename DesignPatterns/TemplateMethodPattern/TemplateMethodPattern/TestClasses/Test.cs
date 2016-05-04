﻿using System;
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
            Result.Append("Preparation for the test");
        }

        protected override void TestItself()
        {
            Result.Append("Performing test");
        }

        protected override void TestResults()
        {
            Result.Append("Getting test results");
        } 
    }
}
