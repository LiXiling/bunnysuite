using App.src.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.src.testImpl
{
    public class FixedAdder : IBunnyModifier
    {
        public void ModifyBunny(Bunny bunny, BenchmarkTest bt)
        {
            bunny.jump(bt.random, bt.gravity, bt.minX, bt.minY, bt.maxX, bt.maxY);
        }
    }
}
