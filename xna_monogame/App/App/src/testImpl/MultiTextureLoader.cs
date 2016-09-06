using App.src.model;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.src.testImpl
{
    public class MultiTextureLoader : ITextureLoader
    {
        public void LoadTexture(BenchmarkTest bt)
        {
            for (int i = 0; i < 3; i++ )
                bt.bunnyTextures.Add(bt.content.Load<Texture2D>(@"wabbit_" + i));
        }
    }
}
