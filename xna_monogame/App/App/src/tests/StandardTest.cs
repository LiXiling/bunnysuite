using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace App.src.tests
{
    class StandardTest : ITest
    {
        //Bunnies
        private List<Bunny> bunnies;
        private int bunnyCount = 0;

        private int step;

        private Random random;
        private ContentManager content;
        SpriteBatch spriteBatch;

        public StandardTest()
        {
        }

        public void Initialize(int min_val, float maxX, float maxY, int step)
        {
            bunnies = new List<Bunny>();
            random = new Random();

            this.step = step;

            AddBunnies(min_val);
        }

        public void LoadContent(ContentManager content, SpriteBatch spriteBatch)
        {
            this.content = content;
            this.spriteBatch = spriteBatch;
        }


        public int RunTest(GameTime gameTime)
        {
            AddBunnies(step);

            return bunnyCount;
        }



        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            for (int i = 0; i < bunnies.Count; i++)
            {
                Bunny bunny = bunnies[i];
                spriteBatch.Draw(bunny.texture, new Vector2(0, 0), null, Color.White);
            }
            spriteBatch.End();

        }

        /// <summary>
        /// Add Bunnies to the Scenery
        /// </summary>
        /// <param name="count"> The Amount of Bunnies to be added</param>
        public void AddBunnies(int count = 100)
        {
            for (int i = 0; i < count; i++)
            {
                Bunny bunny = new Bunny(content);

                bunnies.Add(bunny);
            }
            bunnyCount += count;
        }
    }
}
