using System;
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
        }

        public void Initialize(float maxX, float maxY)
        {
            bunnies = new List<Bunny>();
            random = new Random();

            this.minX = 0;
            this.minY = 100;
            this.maxX = maxX;
            this.maxY = maxY;     
        }

        public void LoadContent(ContentManager content, SpriteBatch spriteBatch)
        {
            this.content = content;
            this.spriteBatch = spriteBatch;

            AddBunnies(minVal);
        }

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

        private void AddBunnies(int count)
        {
            adder.AddBunnies(count, this);
        }

        public void setAdder(IBunnyAdder adder)
        {
            this.adder = adder;
        }

        public void addRunner(ITestRunnable runner)
        {
            testRunnerList.Add(runner);
        } 
    }
}
