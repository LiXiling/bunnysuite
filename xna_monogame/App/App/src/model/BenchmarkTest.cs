﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using App.src.testImpl;
using LilyPath;

namespace App.src.model
{
    public class BenchmarkTest
    {
        //Bunnies
        public List<IRenderable> bunnies;
        public List<Texture2D> bunnyTextures;

        //testImpl Interfaces
        private List<IBunnyModifier> modifierList = new List<IBunnyModifier>();
        private List<IBunnyModifier> testProcedureList = new List<IBunnyModifier>();
        private List<ITextureLoader> texLoaderList = new List<ITextureLoader>();

        //Misc. Helper
        public Random random;
        public ContentManager content;
        private int frameCount = 0;

        //Animation Constants
        public float gravity = 0.5f;
        public float maxX;
        public float minX;
        public float maxY;
        public float minY;

        //Test Constants
        public int minVal;
        public int maxVal;
        public int step;

        public bool noOutput = false;
        private RenderEnum state = RenderEnum.Bunny;


        public BenchmarkTest(int minVal, int maxVal, int step)
        {
            this.minVal = minVal;
            this.maxVal = maxVal;
            this.step = step;

            bunnies = new List<IRenderable>();
            bunnyTextures = new List<Texture2D>();
            random = new Random();
        }
        /// <summary>
        /// Initializes the Benchmark with its GraphicDevice Constants
        /// </summary>
        /// <param name="maxX">max Value in X Dimension</param>
        /// <param name="maxY">max Value in Y Dimension</param>
        public void Initialize(float maxX, float maxY)
        {
            this.minX = 0;
            this.minY = 0;
            this.maxX = maxX;
            this.maxY = maxY;
        }
        /// <summary>
        /// Loads Content
        /// </summary>
        /// <param name="content">ContentManager</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        public void LoadContent(ContentManager content, SpriteBatch spriteBatch)
        {
            this.content = content;

            LoadTextures();

            AddBunnies(minVal);
        }

        private void LoadTextures()
        {
            foreach (ITextureLoader loader in texLoaderList)
            {
                loader.LoadTexture(this);
            }

        }

        public Texture2D getTexture()
        {
            return bunnyTextures.ElementAt(random.Next(bunnyTextures.Count));
        }

        public int getBunnyCount()
        {
            return bunnies.Count;
        }

        /// <summary>
        /// Runs the TestAnimation. Every 10 frames new Bunnies are added according to the step value
        /// </summary>
        /// <returns>True, if the max number of Bunnies has been reached</returns>
        public bool RunTest()
        {
            //Every 10 frames make a new step
            if (frameCount == 10)
            {
                if (bunnies.Count >= maxVal)
                {
                    return true;
                }
                AddBunnies(step);
                frameCount = 0;
            }
            frameCount++;

            //return if no additional TestProcedure is given
            if (testProcedureList.Count == 0)
            {
                return false;
            }

            foreach (IRenderable bunny in bunnies)
            {
                foreach (IBunnyModifier testProcedure in testProcedureList)
                {
                    testProcedure.ModifyBunny(bunny, this);
                }
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch, DrawBatch drawBatch)
        {
            if (noOutput || bunnies.Count == 0)
            {
                return;
            }

            int drawCalls = 0;

            foreach (IRenderable bunny in bunnies)
            {
                drawCalls++;
                bunny.Draw(spriteBatch, drawBatch);
            }
        }

        /// <summary>
        /// Adds new Bunnies to the Scene
        /// </summary>
        /// <param name="count">Amount of Bunnies to be added</param>
        private void AddBunnies(int count)
        {

            for (int i = 0; i < count; i++)
            {
                IRenderable bunny;

                switch (state)
                {
                    case RenderEnum.Bunny:
                        bunny = new Bunny(this.getTexture());
                        break;
                    case RenderEnum.Triangle:
                        bunny = new Triangle(random);
                        break;
                    default:
                        bunny = new Bunny(this.getTexture());
                        break;
                }
                //Bunny bunny = new Bunny(this.getTexture());
                //Triangle bunny = new Triangle(random);
                foreach (IBunnyModifier modifier in modifierList)
                {
                    modifier.ModifyBunny(bunny, this);
                }
                bunnies.Add(bunny);
            }
        }

        public void addTextureLoader(ITextureLoader loader)
        {
            texLoaderList.Add(loader);
        }

        /// <summary>
        /// Adds an IBunnyAdder Implementation to the Object Spawn Modifiers
        /// </summary>
        /// <param name="adder"></param>
        public void addSpawnModifier(IBunnyModifier modifier)
        {
            modifierList.Add(modifier);
        }

        /// <summary>
        /// Adds an IBunnyModifier Implementation to the Procedure Steps
        /// </summary>
        /// <param name="runner"></param>
        public void addUpdateModifier(IBunnyModifier runner)
        {
            testProcedureList.Add(runner);
        }

        public void setRenderState(RenderEnum newState)
        {
            state = newState;
        }
    }
}
