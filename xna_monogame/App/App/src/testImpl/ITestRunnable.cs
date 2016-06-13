using App.src.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.src.testImpl
{
    public interface ITestRunnable
    {
        void RunTest(BenchmarkTest bt);
    }
}
