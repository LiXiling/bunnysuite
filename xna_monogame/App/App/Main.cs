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

            if (debugText.getFps() >= 60)
            {
                AddBunnies(50);
            }

            //bunnies movement
            for (int i = 0; i < bunnies.Count; i++)
            {
                Bunny bunny = bunnies[i];
                bunny.X += bunny.SpeedX;
                bunny.Y += bunny.SpeedY;
                bunny.SpeedY += gravity;

                if (bunny.X > maxX)
                {
                    bunny.SpeedX *= -1;
                    bunny.X = maxX;
                }
                else if (bunny.X < minX)
                {
                    bunny.SpeedX *= -1;
                    bunny.X = minX;                   
                }

                if (bunny.Y > maxY)
                {
                    bunny.SpeedY *= -0.9f;
                    bunny.Y = maxY;
                    if ((float)random.NextDouble() > 0.5)
                    {
                        bunny.SpeedY -= (float)random.NextDouble();
                    }                    
                }
                else if (bunny.Y < minY)
                {
                    bunny.SpeedY = 0;
                    bunny.Y = minY;
                }
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
    }
}

