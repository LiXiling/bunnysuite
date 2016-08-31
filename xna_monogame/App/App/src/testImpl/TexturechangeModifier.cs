using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using App.src.model;

namespace App.src.testImpl
{
    public class TexturechangeModifier : IBunnyModifier
    {
        public void ModifyBunny(IRenderable renderable, BenchmarkTest bt)
        {
                renderable.ChangeTexture(bt.random.Next(bt.bunnyTextures.Count), bt);           
        }
    }
}
