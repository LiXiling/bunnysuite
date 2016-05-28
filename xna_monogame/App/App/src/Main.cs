using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace App
{

    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<Bunny> bunnies;
        private float gravity = 0.5f;
        private float maxX;
        private float minX;
        private float maxY;
        private float minY;
        private Random random;
        private Color bgColor;
        private int bunniesCount = 0;
        private DebugText debugText;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Data";
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
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            debugText = new DebugText(Content);
        }

        public void AddBunnies(int count = 100)
        {
            for (int i = 0; i < count; i++)
            {
                Bunny bunny = new Bunny(Content);
                bunny.SpeedX = (float)random.NextDouble() * 5;
                bunny.SpeedY = (float)random.NextDouble() * 5;

                bunnies.Add(bunny);
            }
            bunniesCount += count;
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            AddBunnies(100);
            
            if (bunniesCount > 30000)
            {
                debugText.getLogger().write();
                this.Exit();
                return;
            }
            
            //bunnies movement
            for (int i = 0; i < bunnies.Count; i++)
            {
                Bunny bunny = bunnies[i];
                bunny.jump(random, gravity, minX, minY, maxX, maxY);
                bunny.changeTexture(random.Next(), Content);
                bunny.rotate(random);
            }

            debugText.Update(gameTime);
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
            debugText.Draw(spriteBatch, bunniesCount);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

