using App.src.model;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.src.testImpl
{
    public class StandardTextureLoader : ITextureLoader
    {
        public void LoadTexture(BenchmarkTest bt)
        {
            bt.bunnyTextures.Add(bt.content.Load<Texture2D>(@"wabbit_alpha" + 0));
        }
    }
}
