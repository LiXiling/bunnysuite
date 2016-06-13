using App.src.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.src.testImpl
{
    public interface ITestRunnable
    {
        /// <summary>
        /// The Test Animation Behaviour
        /// </summary>
        /// <param name="bt">The BenchmarkTest calling the method</param>
        void RunTest(BenchmarkTest bt);
    }
}
