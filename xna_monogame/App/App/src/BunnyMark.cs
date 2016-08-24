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
        BasicEffect basicEffect;

        //Test Values
        private int bunnyCount = 0;
        private bool btFinished = false;

        //Misc. Helpers
        private Logger logger;
        private DebugText debugText;

        public BunnyMark(BenchmarkTest bt, String testnameList, int maxVal, int xRes, int yRes)
        {
            this.Window.Title = "BunnySuite - XNA";
            test_name = testnameList;
            max_val = maxVal;

            Console.WriteLine(xRes + " " + yRes);

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = xRes;
            graphics.PreferredBackBufferHeight = yRes;
            graphics.ApplyChanges();
            Content.RootDirectory = "Data";

            this.bt = bt;

            logger = new Logger(test_name);

            this.IsFixedTimeStep = false;
            //graphics.SynchronizeWithVerticalRetrace = false;
        }

        protected override void Initialize()
        {
            bgColor = new Color(21, 21, 21);
            bt.Initialize(graphics.PreferredBackBufferWidth - 26, graphics.PreferredBackBufferHeight - 37);
            base.Initialize();

            basicEffect = new BasicEffect(graphics.GraphicsDevice);
            basicEffect.VertexColorEnabled = true;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter(
                0, graphics.GraphicsDevice.Viewport.Width,      // left, right
                graphics.GraphicsDevice.Viewport.Height, 0,     // bottom, top
                0, 1                                            // near, far plane
            );                                         
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
            btFinished = bt.RunTest();

            debugText.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            bunnyCount = bt.getBunnyCount();

            GraphicsDevice.Clear(bgColor);
            basicEffect.CurrentTechnique.Passes[0].Apply();

            spriteBatch.Begin();
            bt.Draw(spriteBatch, graphics.GraphicsDevice, basicEffect);                       
            debugText.Draw(spriteBatch, bunnyCount);
            spriteBatch.End();

            logger.addLog(gameTime, bunnyCount);

            //Exit if enough Bunnies are drawn
            if (btFinished && bunnyCount == max_val)
            {
                logger.write();
                this.Exit();
                return;
            }

            base.Draw(gameTime);

        }
    }
}

