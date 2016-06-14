﻿using App.src.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.src.testImpl
{
    public class RandomTest: IBunnyModifier, ITestRunnable
    {
        public void RunTest(BenchmarkTest bt)
        {
            //bunnies movement
            for (int i = 0; i < bt.bunnies.Count; i++)
            {                
                bt.bunnies[i].teleport(bt.random, bt.maxX, bt.maxY);
            }
        }

        public void ModifyBunny(Bunny bunny, BenchmarkTest bt)
        {
                bunny.teleport(bt.random, bt.maxX, bt.maxY);
        }
    }
}
