using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using App.src.model;

namespace App.src.testImpl
{
    public class AnimationTest : IBunnyModifier, ITestRunnable
    {
        public void RunTest(BenchmarkTest bt)
        {
            //bunnies movement
            for (int i = 0; i < bt.bunnies.Count; i++)
            {
                bt.bunnies[i].jump(bt.random, bt.gravity, bt.minX, bt.minY, bt.maxX, bt.maxY);
            }
        }

        public void ModifyBunny(Bunny bunny, BenchmarkTest bt)
        {
            bunny.SpeedX = (float)bt.random.NextDouble() * 5;
            bunny.SpeedY = (float)bt.random.NextDouble() * 5;
        }
    }
}
