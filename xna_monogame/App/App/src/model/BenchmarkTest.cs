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
        private List<IBunnyModifier> modifierList = new List<IBunnyModifier>();
        private List<IBunnyModifier> testProcedureList = new List<IBunnyModifier>();

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
            //Every 10 frames make a new step
            if (frameCount == 10)
            {
                AddBunnies(step);
                frameCount = 0;
            }
            frameCount++;

            //return if no additional TestProcedure is given
            if (testProcedureList.Count == 0)
            {
                return bunnies.Count;
            }

            foreach (Bunny bunny in bunnies)
            {
                foreach (IBunnyModifier testProcedure in testProcedureList)
                {
                    testProcedure.ModifyBunny(bunny, this);
                }
            }
            return bunnies.Count;
        }

        /// <summary>
        /// Called inside a spriteBatch.Begin() Block! Draws all the Bunnies onto the screen
        /// </summary>
        public void Draw()
        {
            foreach (Bunny bunny in bunnies)
            {
                spriteBatch.Draw(bunny.texture, new Vector2(bunny.X, bunny.Y), null, Color.White, (float)bunny.Rotation, new Vector2(bunny.originX, bunny.originY), (float)bunny.Scale, SpriteEffects.None, 0f);
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
                Bunny bunny = new Bunny(content);
                foreach (IBunnyModifier modifier in modifierList)
                {
                    modifier.ModifyBunny(bunny, this);
                }
                bunnies.Add(bunny);
            }
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
    }
}
