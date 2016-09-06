using System;
using App.src.model;

namespace App
{
    static class Program
    {
        private static String testnameList;
        private static int min_val;
        private static int max_val;
        private static int step;

        private static int xRes = 800;
        private static int yRes = 600;
        private static int avg = 10;

        static void Main(string[] args)
        {
            BenchmarkFactory bf = new BenchmarkFactory();
            if (args.Length < 4)
            {
                // missing arguments?
                Console.WriteLine("Missing arguments. We assume some standard values for testing.");
                testnameList = "texts,animation,rotation, scaled";
                min_val = 1;
                max_val = 20000;
                step = 00;
                xRes = 800;
                yRes = 600;
            }
            else
            {
                // read the parameters
                testnameList = args[0];
                min_val = Int32.Parse(args[1]);
                max_val = Int32.Parse(args[2]);
                step = Int32.Parse(args[3]);
            }

            //Read Resolution Width
            if (args.Length >= 5)
            {
                xRes = Int32.Parse(args[4]);
            }
            
            //Read Resolution Height
            if (args.Length >= 6)
            {
                yRes = Int32.Parse(args[5]);
            }

            if (args.Length >= 7)
            {
                avg = Int32.Parse(args[6]);
            }
            
            BenchmarkTest bt = bf.ConstructBenchmark(testnameList, min_val, max_val, step, avg);

            using (BunnyMark game = new BunnyMark(bt, testnameList, max_val, xRes, yRes, avg))
            {
                game.Run();
            }
        }
    }
}
