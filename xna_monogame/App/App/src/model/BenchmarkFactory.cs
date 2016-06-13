﻿using App.src.testImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.src.model
{
    public class BenchmarkFactory
    {
        public BenchmarkFactory(){}
        public BenchmarkTest ConstructBenchmark(String testname, int minVal, int maxVal, int step)
        {
            BenchmarkTest bt = new BenchmarkTest(minVal, maxVal, step);

            switch (testname)
            {
                case "standard":
                    bt.setAdder(new FixedAdder());
                    break;
                case "animation":
                    AnimationTest at = new AnimationTest();
                    bt.setAdder(at);
                    bt.addRunner(at);
                    break;
                case "texturechange":
                    bt.setAdder(new RandomTest());
                    bt.addRunner(new TexturechangeTest());
                    break;
                case "rotation":
                    bt.setAdder(new RandomTest());
                    bt.addRunner(new RotationTest());
                    break;
                case "random":
                    RandomTest rt = new RandomTest();
                    bt.setAdder(rt);
                    bt.addRunner(rt);
                    break;
                case "scaled":
                    bt.setAdder(new RandomTest());
                    bt.addRunner(new ScaledTest());
                    break;
                case "multitexture":
                    bt.setAdder(new MultitextureAdd());
                    break;
                default:
                    at = new AnimationTest();
                    bt.setAdder(at);
                    bt.addRunner(at);
                    break;
            }

            return bt;
        }
    }
}
