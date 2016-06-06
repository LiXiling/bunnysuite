using System;
using App.src.tests;

namespace App
{
#if WINDOWS || XBOX
    static class Program
    {
        private static String test_name;
        private static String ind_var;
        private static int min_val;
        private static int max_val;
        private static int step;
        private static ATest test;

        static void Main(string[] args)
        {
            if (args.Length < 6)
            {
                // missing arguments?
                Console.WriteLine("Missing arguments. We assume some standard values for testing.");
                ind_var = "num_bunnies_normal";
                min_val = 0;
                max_val = 40000;
                step = 200;
            }
            else
            {
                // read the parameters
                test_name = args[0];
                ind_var = args[2];
                min_val = Int32.Parse(args[3]);
                max_val = Int32.Parse(args[4]);
                step = Int32.Parse(args[5]);
            }

            switch (test_name)
            {
                case "standard":
                    test = new StandardTest();
                    break;
                case "animation":
                    test = new AnimationTest();
                    break;
                case "textureChange":
                    test = new TextureChangeTest();
                    break;
                case "rotation":
                    test = new RotationTest();
                    break;
                default:
                    test = new AnimationTest();
                    break;
            }

            using (Main game = new Main(test,test_name,min_val,max_val,step))
            {
                game.Run();
            }
            Console.WriteLine();
            Console.WriteLine("Game is closed");
            Console.WriteLine();
        }
    }
#endif
}

