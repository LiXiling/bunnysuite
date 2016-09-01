using App.src.testImpl;
using App.src.model.renderables;
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
        public BenchmarkTest ConstructBenchmark(String testnameList, int minVal, int maxVal, int step, int avg)
        {
            BenchmarkTest bt = new BenchmarkTest(minVal, maxVal, step, avg);
            String[] testnames = testnameList.Split(',');

            Boolean textureAdded = false;

            foreach(String testname in testnames){

                switch (testname)
                {
                    case "alpha":
                        bt.addTextureLoader(new GhostTextureLoader());                        
                        textureAdded = true;
                        break;
                    case "animation":
                        bt.addSpawnModifier(new SpeedModifier());
                        bt.addUpdateModifier(new AnimationModifier());
                        break;      
                    case "bunnies":
                        bt.addRenderState(RenderEnum.Bunny);
                        break;
                    case "circles":
                        bt.addRenderState(RenderEnum.Circle);
                        break;
                    case "colorchange":
                        bt.addUpdateModifier(new ColorChangeModifier());
                        break;
                    case "hdtexture":
                        bt.addTextureLoader(new HDTextureLoader());                        
                        textureAdded = true;
                        break;
                    case "lines":
                        bt.addRenderState(RenderEnum.Line);
                        break;
                    case "no_output":
                        bt.noOutput = true;
                        break;
                    case "multitexture":
                        bt.addSpawnModifier(new TexturechangeModifier());
                        bt.addTextureLoader(new MultiTextureLoader());
                        textureAdded = true;
                        break;
                    case "pulsation":
                        bt.addUpdateModifier(new PulseModifier());
                        break;
                    case "random":
                        bt.addSpawnModifier(new RandomPositionModifier());                  
                        break;
                    case "rectangles":
                        bt.addRenderState(RenderEnum.Rectangle);
                        break;
                    case "rotation":
                        bt.addSpawnModifier(new RandomPositionModifier());
                        bt.addUpdateModifier(new RotationModifier());
                        break;      
                    case "scaled":
                        bt.addSpawnModifier(new ScaleModifier());
                        break;
                    case "standard":
                        bt.addSpawnModifier(new FixedPositionModifier());
                        bt.addTextureLoader(new StandardTextureLoader());
                        textureAdded = true;
                        break;                    
                    case "teleport":
                        bt.addUpdateModifier(new RandomPositionModifier());
                        break;
                    case "texts":
                        bt.addRenderState(RenderEnum.Text);
                        break;
                    case "texturechange":
                        bt.addUpdateModifier(new TexturechangeModifier());                      
                        break;
                    case "thin":
                        bt.addTextureLoader(new ThinTextureLoader());
                        textureAdded = true;
                        break;
                    case "tinted":
                        bt.addSpawnModifier(new ColorChangeModifier());
                        break;
                    case "triangles":
                        bt.addRenderState(RenderEnum.Triangle);
                        break;
                    default:
                        bt.addSpawnModifier(new SpeedModifier());
                        bt.addUpdateModifier(new AnimationModifier());
                        bt.addTextureLoader(new StandardTextureLoader());
                        textureAdded = true;
                        break;
                    }
            }

            Console.WriteLine(textureAdded);

            if (!textureAdded)
            {
                bt.addTextureLoader(new StandardTextureLoader());
            }
            return bt;
        }
    }
}
