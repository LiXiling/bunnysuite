using App.src.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.src.testImpl
{
    public class ScaledTest : ITestRunnable
    {
        public void RunTest(BenchmarkTest bt)
        {
            for (int i = 0; i < bt.bunnies.Count; i++)
            {
                bt.bunnies[i].grow();
            }
        }
    }
}
