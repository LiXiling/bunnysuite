using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace App.src.tests
{
    class AnimationTest : ATest
    {
        public AnimationTest()
        {
        }

        public override int RunTest(GameTime gameTime)
        {
            AddBunnies(step);

            //bunnies movement
            for (int i = 0; i < bunnies.Count; i++)
            {
                Bunny bunny = bunnies[i];
                bunny.jump(random, gravity, minX, minY, maxX, maxY);
            }
            return bunnyCount;
        }


        public override void AddBunnies(int count = 100)
        {
            for (int i = 0; i < count; i++)
            {
                Bunny bunny = new Bunny(content);

                bunny.SpeedX = (float)random.NextDouble() * 5;
                bunny.SpeedY = (float)random.NextDouble() * 5;
                bunnies.Add(bunny);
            }
            bunnyCount += count;
        }
    }
}
