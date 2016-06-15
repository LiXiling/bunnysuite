using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using App.src.model;

namespace App.src.testImpl
{
    public class SpeedModifier : IBunnyModifier
    {
        public void ModifyBunny(Bunny bunny, BenchmarkTest bt)
        {
            bunny.SpeedX = (float)bt.random.NextDouble() * 5;
            bunny.SpeedY = (float)bt.random.NextDouble() * 5;
        }
    }
}
