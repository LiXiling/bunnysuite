using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App
{
    class Logger
    {
        private List<String> lines;

        public Logger()
        {
            lines = new List<string>();
        }

        public void addLine(String s)
        {
            lines.Add(s);
        }

        public void write()
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@".\Log.txt"))
            {
                foreach (string line in lines)
                {
                    file.WriteLine(line);
                }
                file.Close();
            }

        }
    }
}
