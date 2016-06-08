using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.src.tests
{
    class MultiTextureTest : ATest
    {
        public MultiTextureTest() { }
        public override int RunTest()
        {
            if (count == 10)
            {
                AddBunnies(step);
                count = 0;
            }
            count++;

            return bunnyCount;
        }
        public override void AddBunnies(int count = 100)
        {
            for (int i = 0; i < count; i++)
            {
                Bunny bunny = new Bunny(content);

                bunny.teleport(random, maxX, maxY);
                bunny.changeTexture(random.Next(), content);
                bunnies.Add(bunny);
            }
            bunnyCount += count;
        }
    }
}
