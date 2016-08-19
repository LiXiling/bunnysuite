using System;
using System.IO;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Globalization;

namespace App
{
    public class Logger
    {
        private List<String> lines;
        private List<Tuple<int, float>> loggedTuples;
        private String name;
        private float elapsedTime = 0.0f;
        private int count = 0;

        public Logger(String name)
        {
            lines = new List<string>();
            loggedTuples = new List<Tuple<int, float>>();
            this.name = name;
        }
        /// <summary>
        /// Add a new Entry to the Log if half a second in GameTime has elapsed
        /// </summary>
        /// <param name="gameTime">The current Gametime</param>
        /// <param name="bunnyCount">The current amount of drawn bunnies</param>
        /// <param name="fps">The currently achieved fps</param>
        public void addLog(GameTime gameTime, int bunnyCount)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            

            if (count == 10)
            {
                addLog(bunnyCount, elapsedTime / 10.0f);
                elapsedTime = 0;
                count = 0;
            }
            count++;
        }

        /// <summary>
        /// Add a new Entry to the Log
        /// </summary>
        /// <param name="bunnyCount">The current count of drawn bunnies</param>
        /// <param name="deltatime">The current deltatime in s</param>
        public void addLog(int bunnyCount, float deltatime)
        {
            loggedTuples.Add(new Tuple<int, float>(bunnyCount, deltatime));
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

                foreach (Tuple<int, float> tuple in loggedTuples)
                {
                    file.WriteLine(tuple.Item1.ToString() + "\t" + tuple.Item2.ToString(CultureInfo.InvariantCulture));
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
