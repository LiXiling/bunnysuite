using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace App.src.tests
{
    public interface ITest
    {
        void LoadContent(ContentManager content, SpriteBatch spriteBatch);
        int RunTest(GameTime gameTime);
        void Initialize(int min_val, float maxX, float maxY, int step);
        void Draw(GameTime gameTime);
    }
}
