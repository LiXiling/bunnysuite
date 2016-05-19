using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App
{
    public class Logger
    {
        private List<String> lines;
        private List<Tuple<int, int>> loggedTuples;

        public Logger()
        {
            lines = new List<string>();
            loggedTuples = new List<Tuple<int, int>>();
        }

        public void addLine(String s)
        {
            lines.Add(s);
        }

        public void addLog(int bunnyCount, int fps)
        {
            loggedTuples.Add(new Tuple<int, int>(bunnyCount, fps));
        }

        public void write()
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@".\Log.txt"))
            {
                foreach (String line in lines)
                {
                    file.WriteLine(line);
                }

                foreach (Tuple<int, int> tuple in loggedTuples)
                {
                    file.WriteLine(tuple.Item1.ToString() + " " + tuple.Item2.ToString());
                }
                file.Close();
            }

        }
    }
}
