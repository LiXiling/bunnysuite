using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using App.src.model;
using LilyPath;

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
        DrawBatch drawBatch;

        private Color bgColor;

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
            bt.Initialize(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            drawBatch = new DrawBatch(GraphicsDevice);

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

            Vector2 center = new Vector2(200, 200);
            Vector2[] relVertex = new Vector2[4];
            relVertex[0] = new Vector2(100, 137);
            relVertex[1] = new Vector2(113, 100);
            relVertex[2] = new Vector2(126, 137);
            relVertex[3] = new Vector2(125, 137);


            spriteBatch.Begin();
            drawBatch.Begin(DrawSortMode.Deferred, null, null, null, null, null, Matrix.Identity);
            
            //drawBatch.FillCircle(new SolidColorBrush(Color.SkyBlue), center, 175);
            //drawBatch.FillPath(new SolidColorBrush(Color.Red), relVertex);

            bt.Draw(spriteBatch, drawBatch);
            //debugText.Draw(spriteBatch, bunnyCount);

            drawBatch.End();
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

