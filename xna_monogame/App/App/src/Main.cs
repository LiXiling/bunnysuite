using System;
using System.Collections.Generic;
using App.src.tests;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace App
{

    public class Main : Microsoft.Xna.Framework.Game
    {
        //Test Parameter
        private ATest test;
        private String test_name;
        private int min_val;
        private int max_val;
        private int step;

        //Graphics IO
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Color bgColor;

        //Bunnies
        private int bunnyCount = 0;

        //Misc. Helpers
        private Logger logger;
        private DebugText debugText;



        public Main(ATest test, String test_name, int min_val, int max_val, int step)
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Data";

            this.test = test;
            this.test_name = test_name;
            this.min_val = min_val;
            this.max_val = max_val;
            this.step = step;

            logger = new Logger(test_name);
        }

        protected override void Initialize()
        {
            bgColor = new Color(21, 21, 21);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            debugText = new DebugText(Content);

            test.LoadContent(Content, spriteBatch);
            test.Initialize(min_val, graphics.PreferredBackBufferWidth - 26, graphics.PreferredBackBufferHeight - 37, step);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            bunnyCount = test.RunTest(gameTime);
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
            test.Draw(gameTime);
            debugText.Draw(spriteBatch, bunnyCount);
            spriteBatch.End();

            logger.addLog(gameTime, bunnyCount);
            base.Draw(gameTime);
        }
    }
}

