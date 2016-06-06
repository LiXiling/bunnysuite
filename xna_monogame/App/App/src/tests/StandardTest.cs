using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace App.src.tests
{
    class StandardTest : ATest
    {
        public StandardTest()
        {
        }

        public override int RunTest(GameTime gameTime)
        {
            int oldCount = bunnyCount;
            AddBunnies(step);

            return bunnyCount;
        }
        public override void AddBunnies(int count = 100)
        {
            for (int i = 0; i < count; i++)
            {
                Bunny bunny = new Bunny(content);

                bunny.jump(random, gravity, minX, minY, maxX, maxY);
                bunnies.Add(bunny);
            }
            bunnyCount += count;
        }
    }
}
