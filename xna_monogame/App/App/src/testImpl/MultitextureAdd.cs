using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using App.src.model;

namespace App.src.testImpl
{
    public class MultitextureAdd : IBunnyAdder
    {
        public void AddBunnies(int count, BenchmarkTest bt)
        {
            for (int i = 0; i < count; i++)
            {
                Bunny bunny = new Bunny(bt.content);

                bunny.teleport(bt.random, bt.maxX, bt.maxY);
                bunny.changeTexture(bt.random.Next(), bt.content);
                
                bt.bunnies.Add(bunny);
            }
        }
    }
}
