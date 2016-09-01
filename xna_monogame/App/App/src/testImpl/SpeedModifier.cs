using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using App.src.model;
using App.src.model.renderables;

namespace App.src.testImpl
{
    public class SpeedModifier : IBunnyModifier
    {
        public void ModifyBunny(IRenderable renderable, BenchmarkTest bt)
        {
            renderable.SetSpeed(
                (float)bt.random.NextDouble() * 5,
                (float)bt.random.NextDouble() * 5
            );
        }
    }
}
