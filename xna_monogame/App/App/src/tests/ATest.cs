using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace App.src.tests
{
    public abstract class ATest
    {
        //Bunnies
        public List<Bunny> bunnies;
        public int bunnyCount = 0;
        public int count = 0;

        public int step;

        public Random random;
        public ContentManager content;
        public SpriteBatch spriteBatch;

        //Animation Constants
        public float gravity = 0.5f;
        public float maxX;
        public float minX;
        public float maxY;
        public float minY;

        public void Initialize(int min_val, float maxX, float maxY, int step)
        {
            bunnies = new List<Bunny>();
            random = new Random();

            this.minX = 0;
            this.minY = 100;
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

        /// <summary>
        /// The single step procedure of the step. To be used in the Game Loop Update() step.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns>The current number of Drawn bunnies. Needed for exit criteria</returns>
        public abstract int RunTest(GameTime gameTime);

        /// <summary>
        /// Called inside a spriteBatch.Begin() Block! Draws all the Bunnies onto the screen
        /// </summary>
        /// <param name="gameTime"></param>
        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < bunnyCount; i++)
            {
                Bunny bunny = bunnies[i];
                spriteBatch.Draw(bunny.texture, new Vector2(bunny.X, bunny.Y), null, Color.White, (float)bunny.Rotation, new Vector2(bunny.originX, bunny.originY), (float) bunny.Scale, SpriteEffects.None, 0f);
            }

        }

        /// <summary>
        /// Add Bunnies to the Scenery
        /// </summary>
        /// <param name="count"> The Amount of Bunnies to be added</param>
        public abstract void AddBunnies(int count = 100);
    }
}
