using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace App.src.tests
{
    class TextureChangeTest : ITest
    {
        //Bunnies
        private List<Bunny> bunnies;
        private int bunnyCount = 0;

        private int step;

        private Random random;
        private ContentManager content;
        SpriteBatch spriteBatch;

        //Animation Constants
        private float gravity = 0.5f;
        private float maxX;
        private float minX;
        private float maxY;
        private float minY;

        public TextureChangeTest(ContentManager content, SpriteBatch spriteBatch)
        {
            this.content = content;
            this.spriteBatch = spriteBatch;
        }


        public int RunTest(GameTime gameTime)
        {
            AddBunnies(step);

            //bunnies movement
            for (int i = 0; i < bunnies.Count; i++)
            {
                Bunny bunny = bunnies[i];
                bunny.jump(random, gravity, minX, minY, maxX, maxY);
                bunny.changeTexture(random.Next(), content);
            }
            return bunnyCount;
        }

        public void Initialize(int min_val, float maxX, float maxY, int step)
        {
            bunnies = new List<Bunny>();
            random = new Random();

            this.minX = 0;
            this.minY = 0;
            this.maxX = maxX;
            this.maxY = maxY;
            this.step = step;

            AddBunnies(min_val);
        }

        public void LoadContent(ContentManager content, SpriteBatch spriteBatch)
        {
            this.content = content;
            this.spriteBatch = spriteBatch;
        }

        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < bunnies.Count; i++)
            {
                Bunny bunny = bunnies[i];
                spriteBatch.Draw(bunny.texture, new Vector2(bunny.X, bunny.Y), null, Color.White);
            }

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