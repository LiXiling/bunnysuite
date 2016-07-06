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
        public BenchmarkTest ConstructBenchmark(String testnameList, int minVal, int maxVal, int step)
        {
            BenchmarkTest bt = new BenchmarkTest(minVal, maxVal, step);
            String[] testnames = testnameList.split(",");

            foreach(String testname : testnames){

                switch (testname)
                {
                    case "standard":
                        bt.addSpawnModifier(new FixedPositionModifier());
                        break;
                    case "animation":
                        bt.addSpawnModifier(new SpeedModifier());
                        bt.addUpdateModifier(new AnimationModifier());
                        break;
                    case "texturechange":
                        bt.addSpawnModifier(new RandomPositionModifier());
                        bt.addUpdateModifier(new TexturechangeModifier());
                        break;
                    case "rotation":
                        bt.addSpawnModifier(new RandomPositionModifier());
                        bt.addUpdateModifier(new RotationModifier());
                        break;
                    case "random":
                        RandomPositionModifier rt = new RandomPositionModifier();
                        bt.addSpawnModifier(rt);
                        bt.addUpdateModifier(rt);
                        break;
                    case "scaled":
                        bt.addSpawnModifier(new RandomPositionModifier());
                        bt.addUpdateModifier(new ScaleModifier());
                        break;
                    case "multitexture":
                        bt.addSpawnModifier(new RandomPositionModifier());
                        bt.addSpawnModifier(new TexturechangeModifier());
                        break;
                    default:
                        bt.addSpawnModifier(new SpeedModifier());
                        bt.addUpdateModifier(new AnimationModifier());
                        break;
                    }
            }

            return bt;
        }
    }
}
