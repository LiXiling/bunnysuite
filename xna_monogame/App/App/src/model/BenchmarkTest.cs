﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using App.src.testImpl;

namespace App.src.model
{
    public class BenchmarkTest
    {
        //Bunnies
        public List<Bunny> bunnies;

        //testImpl Interfaces
        private IBunnyAdder adder;
        private List<ITestRunnable> testRunnerList = new List<ITestRunnable>();

        //Misc. Helper
        public Random random;
        public ContentManager content;
        public SpriteBatch spriteBatch;
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

        public BenchmarkTest(int minVal, int maxVal, int step)
        {
            this.minVal = minVal;
            this.maxVal = maxVal;
            this.step = step;

            bunnies = new List<Bunny>();
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
            this.minY = 100;
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
            this.spriteBatch = spriteBatch;

            AddBunnies(minVal);
        }

        /// <summary>
        /// Runs the TestAnimation. Every 10 frames new Bunnies are added according to the step value
        /// </summary>
        /// <returns>The current Amount of drawn Bunnies</returns>
        public int RunTest()
        {
            if (frameCount == 10)
            {
                AddBunnies(step);
                frameCount = 0;
            }
            frameCount++;
            if (testRunnerList.Count == 0)
            {
                return bunnies.Count;
            }
            foreach (ITestRunnable testRunner in testRunnerList){
                testRunner.RunTest(this);
            }

            return bunnies.Count;
        }

        /// <summary>
        /// Called inside a spriteBatch.Begin() Block! Draws all the Bunnies onto the screen
        /// </summary>
        public void Draw()
        {
            for (int i = 0; i < bunnies.Count; i++)
            {
                Bunny bunny = bunnies[i];
                spriteBatch.Draw(bunny.texture, new Vector2(bunny.X, bunny.Y), null, Color.White, (float)bunny.Rotation, new Vector2(bunny.originX, bunny.originY), (float)bunny.Scale, SpriteEffects.None, 0f);
            }

        }

        /// <summary>
        /// Adds new Bunnies to the Scene
        /// </summary>
        /// <param name="count">Amount of Bunnies to be added</param>
        private void AddBunnies(int count)
        {
            adder.AddBunnies(count, this);
        }
        /// <summary>
        /// Setter for an IBunnyAdder Implementation
        /// </summary>
        /// <param name="adder"></param>
        public void setAdder(IBunnyAdder adder)
        {
            this.adder = adder;
        }

        /// <summary>
        /// Adds an ITestRunnable Implementation to the List of TestSteps
        /// </summary>
        /// <param name="runner"></param>
        public void addRunner(ITestRunnable runner)
        {
            testRunnerList.Add(runner);
        } 
    }
}