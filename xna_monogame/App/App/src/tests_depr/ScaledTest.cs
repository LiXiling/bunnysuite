using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace App.src.tests
{
    class ScaledTest : ATest
    {
        public ScaledTest()
        {
        }
        public override int RunTest()
        {
            if (count == 10)
            {
                AddBunnies(step);
                count = 0;
            }
            count++;

            //bunnies movement
            for (int i = 0; i < bunnyCount; i++)
            {
                Bunny bunny = bunnies[i];
                bunny.grow();
            }
            return bunnyCount;
        }

        public override void AddBunnies(int count = 100)
        {
            for (int i = 0; i < count; i++)
            {
                Bunny bunny = new Bunny(content);

                bunny.teleport(random, maxX, maxY);
                bunnies.Add(bunny);
            }
            bunnyCount += count;
        }
    }
}
