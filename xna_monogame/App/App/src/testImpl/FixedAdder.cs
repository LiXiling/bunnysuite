using App.src.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.src.testImpl
{
    public class FixedAdder : IBunnyAdder
    {
        public void AddBunnies(int count, BenchmarkTest bt){

        
        for (int i = 0; i < count; i++)
            {
                Bunny bunny = new Bunny(bt.content);

                bunny.jump(bt.random, bt.gravity, bt.minX, bt.minY, bt.maxX, bt.maxY);
                
                bt.bunnies.Add(bunny);
            }
        }
    }
}
