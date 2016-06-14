using App.src.testImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.src.model
{
    public class BenchmarkFactory
    {
        public BenchmarkFactory(){}

        /// <summary>
        /// Constructs a BenchmarkTest with the given Parameters
        /// </summary>
        /// <param name="testname">The testname, defining the behaviour</param>
        /// <param name="minVal">Starting Value for the Test</param>
        /// <param name="maxVal">End Value for the Test</param>
        /// <param name="step">Step increment for the Test</param>
        /// <returns></returns>
        public BenchmarkTest ConstructBenchmark(String testname, int minVal, int maxVal, int step)
        {
            BenchmarkTest bt = new BenchmarkTest(minVal, maxVal, step);

            switch (testname)
            {
                case "standard":
                    bt.addModifier(new FixedAdder());
                    break;
                case "animation":
                    AnimationTest at = new AnimationTest();
                    bt.addModifier(at);
                    bt.addRunner(at);
                    break;
                case "texturechange":
                    bt.addModifier(new RandomTest());
                    bt.addRunner(new TexturechangeTest());
                    break;
                case "rotation":
                    bt.addModifier(new RandomTest());
                    bt.addRunner(new RotationTest());
                    break;
                case "random":
                    RandomTest rt = new RandomTest();
                    bt.addModifier(rt);
                    bt.addRunner(rt);
                    break;
                case "scaled":
                    bt.addModifier(new RandomTest());
                    bt.addRunner(new ScaledTest());
                    break;
                case "multitexture":
                    bt.addModifier(new RandomTest());
                    bt.addModifier(new MultitextureAdd());
                    break;
                default:
                    at = new AnimationTest();
                    bt.addModifier(at);
                    bt.addRunner(at);
                    break;
            }

            return bt;
        }
    }
}
