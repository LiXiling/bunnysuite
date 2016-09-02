using App.src.model;
using App.src.model.renderables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.src.testImpl
{
    public class RotationModifier : IBunnyModifier
    {
        public void ModifyBunny(IRenderable renderable, BenchmarkTest bt)
        {
            renderable.Rotate();
        }
    }
}
