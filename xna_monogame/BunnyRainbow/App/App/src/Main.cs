using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace App
{

    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MouseState lastMouseState;
        MouseState currentMouseState;

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
        private Logger logger = new Logger();
        private bool started = false;

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            //mouse input
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                AddBunnies(1000);
            }
            /*
            if (debugText.getFps() >= 60)
            {
                started = true;
                AddBunnies(50);
            }
            else if (started)
            {
                logger.addLine("Bunnies_Simple: " + debugText.getFps() + " fps and " + bunnies.Count + " Bunnies");
                logger.write();
                Exit();
                return;
            }
            */
            AddBunnies(100);
            //bunnies movement
            for (int i = 0; i < bunnies.Count; i++)
            {
                Bunny bunny = bunnies[i];
                bunny.jump(random, gravity, minX, minY, maxX, maxY);
                bunny.changeTexture(random.Next(),Content);
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
                spriteBatch.Draw(bunnies[i].texture, new Vector2(bunnies[i].X, bunnies[i].Y), Color.White);
            }
            debugText.Draw(spriteBatch, bunniesCount);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        void Game1_Exiting(object sender, EventArgs e)
        {
            // Add any code that must execute before the game ends.

        }
    }
}

