using System;
using System.IO;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace App
{
    public class Logger
    {
        private List<String> lines;
        private List<Tuple<int, int>> loggedTuples;
        private String name;
        private float elapsedTime = 0.0f;

        public Logger(String name)
        {
            lines = new List<string>();
            loggedTuples = new List<Tuple<int, int>>();
            this.name = name;
        }
        /// <summary>
        /// Add a new Entry to the Log if half a second in GameTime has elapsed
        /// </summary>
        /// <param name="gameTime">The current Gametime</param>
        /// <param name="bunnyCount">The current amount of drawn bunnies</param>
        /// <param name="fps">The currently achieved fps</param>
        public void addLog(GameTime gameTime, int bunnyCount, int fps)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime >= 500.0f)
            {
                addLog(bunnyCount, fps);
            }
        }

        /// <summary>
        /// Add a new Entry to the Log
        /// </summary>
        /// <param name="bunnyCount">The current count of drawn bunnies</param>
        /// <param name="fps">The current reached fps</param>
        public void addLog(int bunnyCount, int fps)
        {
            loggedTuples.Add(new Tuple<int, int>(bunnyCount, fps));
        }
        /// <summary>
        /// Writes the Log into a logFile
        /// </summary>
        public void write()
        {
            String path = @".\log\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path + name + ".log"))
            {
                foreach (String line in lines)
                {
                    file.WriteLine(line);
                }

                foreach (Tuple<int, int> tuple in loggedTuples)
                {
                    file.WriteLine(tuple.Item1.ToString() + "\t" + tuple.Item2.ToString());
                }
                file.Close();
            }

        }
        /// <summary>
        /// add the String to the Log
        /// </summary>
        /// <param name="s">the logString</param>
        public void addLine(String s)
        {
            lines.Add(s);
        }
    }
}
