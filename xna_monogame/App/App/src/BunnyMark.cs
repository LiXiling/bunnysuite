using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using App.src.model;

namespace App
{

    public class BunnyMark : Microsoft.Xna.Framework.Game
    {
        //Test Parameter
        private String test_name;
        private int max_val;
        
        private BenchmarkTest bt;

        //Graphics IO
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Color bgColor;

        //Bunnies
        private int bunnyCount = 0;

        //Misc. Helpers
        private Logger logger;
        private DebugText debugText;

        public BunnyMark(BenchmarkTest bt, String testnameList, int maxVal, int xRes, int yRes)
        {
            this.Window.Title = "BunnySuite - XNA";
            test_name = testnameList;
            max_val = maxVal;

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = xRes;
            graphics.PreferredBackBufferHeight = yRes;
            Content.RootDirectory = "Data";

            this.bt = bt;

            logger = new Logger(test_name);
        }

        protected override void Initialize()
        {
            bgColor = new Color(21, 21, 21);
            bt.Initialize(graphics.PreferredBackBufferWidth - 26, graphics.PreferredBackBufferHeight - 37);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            debugText = new DebugText(Content);

            bt.LoadContent(Content, spriteBatch);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            bunnyCount = bt.RunTest();
            //Exit if enough Bunnies are drawn
            if (bunnyCount > max_val)
            {
                logger.write();
                this.Exit();
                return;
            }

            debugText.Update(gameTime);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(bgColor);
            
            spriteBatch.Begin();
            
            bt.Draw();
            debugText.Draw(spriteBatch, bunnyCount);
            
            spriteBatch.End();

            logger.addLog(gameTime, bunnyCount);
            base.Draw(gameTime);
        }
    }
}

