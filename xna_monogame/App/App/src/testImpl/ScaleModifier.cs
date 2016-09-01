using App.src.model;
using App.src.model.renderables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace App.src.testImpl
{
    public class ScaleModifier : IBunnyModifier
    {
        public void ModifyBunny(IRenderable renderable, BenchmarkTest bt)
        {
            renderable.SetScale(
                (float)(bt.random.NextDouble() * 4.8 + 0.2),
                (float)(bt.random.NextDouble() * 4.8 + 0.2)
            );
        }
    }
}
