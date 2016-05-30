using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace App
{

    public class Main : Microsoft.Xna.Framework.Game
    {
        //Test Parameter
        private String test_name;
        private int min_val;
        private int max_val;
        private int step;

        //Graphics IO
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Color bgColor;

        //Bunnies
        private List<Bunny> bunnies;
        private int bunnyCount = 0;

        //Animation Constants
        private float gravity = 0.5f;
        private float maxX;
        private float minX;
        private float maxY;
        private float minY;

        //Misc. Helpers
        private Logger logger;
        private DebugText debugText;
        private Random random;



        public Main(String test_name, int min_val, int max_val, int step)
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Data";

            this.test_name = test_name;
            this.min_val = min_val;
            this.max_val = max_val;
            this.step = step;

            logger = new Logger(test_name);
        }

        protected override void Initialize()
        {
            bgColor = new Color(21, 21, 21);

            bunnies = new List<Bunny>();
            random = new Random();
            maxX = graphics.PreferredBackBufferWidth - 26;
            maxY = graphics.PreferredBackBufferHeight - 37;
            minX = 0;
            minY = 0;

            base.Initialize();
            AddBunnies(min_val);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            debugText = new DebugText(Content);
        }

        /// <summary>
        /// Add Bunnies to the Scenery
        /// </summary>
        /// <param name="count"> The Amount of Bunnies to be added</param>
        public void AddBunnies(int count = 100)
        {
            for (int i = 0; i < count; i++)
            {
                Bunny bunny = new Bunny(Content);
                bunny.SpeedX = (float)random.NextDouble() * 5;
                bunny.SpeedY = (float)random.NextDouble() * 5;

                bunnies.Add(bunny);
            }
            bunnyCount += count;
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            //Exit if enough Bunnies are drawn
            if (bunnyCount > max_val)
            {
                logger.write();
                this.Exit();
                return;
            }

            AddBunnies(step);

            //bunnies movement
            for (int i = 0; i < bunnies.Count; i++)
            {
                Bunny bunny = bunnies[i];
                bunny.jump(random, gravity, minX, minY, maxX, maxY);
                bunny.changeTexture(random.Next(), Content);
                bunny.rotate(random);
            }
            debugText.Update(gameTime);
            logger.addLog(gameTime, bunnyCount, debugText.getFps());
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(bgColor);

            spriteBatch.Begin();
            for (int i = 0; i < bunnies.Count; i++)
            {
                Bunny bunny = bunnies[i];
                spriteBatch.Draw(bunny.texture, new Vector2(bunny.X, bunny.Y), null, Color.White, (float)bunny.Rotation, new Vector2(bunny.originX, bunny.originY), 1f, SpriteEffects.None, 0f);
            }
            debugText.Draw(spriteBatch, bunnyCount);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

