using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using App.src.model;

namespace App.src.testImpl
{
    public interface IBunnyAdder
    {
        void AddBunnies(int count, BenchmarkTest bt);
    }
}
