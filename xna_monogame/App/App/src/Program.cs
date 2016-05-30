using System;

namespace App
{
#if WINDOWS || XBOX
    static class Program
    {
        private static String test_name;
        private static String jsonStr;
        private static String ind_var;
        private static int min_val;
        private static int max_val;
        private static int step;

        static void Main(string[] args)
        {
            if (args.Length < 6)
            {
                // missing arguments?
                Console.WriteLine("Missing arguments. We assume some standard values for testing.");
                test_name = "standard";
                jsonStr = "{\"num_bunnies_normal\" : 0,\"num_bunnies_rotated\" : 0,\"file_bunny\" : \"C:/Users/Melvil/Documents/SG Praktikum/bunnysuite/orx/data/wabbit_alpha.png\"}";
                ind_var = "num_bunnies_normal";
                min_val = 0;
                max_val = 40000;
                step = 200;
            }
            else
            {
                // read the parameters
                test_name = args[0];
                jsonStr = args[1];
                ind_var = args[2];
                min_val = Int32.Parse(args[3]);
                max_val = Int32.Parse(args[4]);
                step = Int32.Parse(args[5]);
            }
            using (Main game = new Main(test_name,min_val,max_val,step))
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

