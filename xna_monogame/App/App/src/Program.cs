using System;

namespace App
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Main game = new Main())
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

