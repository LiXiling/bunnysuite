using System;
using App.src.model;

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

        static void Main(string[] args)
        {
            BenchmarkFactory bf = new BenchmarkFactory();
            if (args.Length < 4)
            {
                // missing arguments?
                Console.WriteLine("Missing arguments. We assume some standard values for testing.");
                test_name = "multitexture";
                min_val = 10;
                max_val = 40000;
                step = 200;
            }
            else
            {
                // read the parameters
                test_name = args[0];
                min_val = Int32.Parse(args[1]);
                max_val = Int32.Parse(args[2]);
                step = Int32.Parse(args[3]);
            }

            BenchmarkTest bt = bf.ConstructBenchmark(test_name, min_val, max_val, step);

            using (BunnyMark game = new BunnyMark(bt, test_name, max_val))
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

