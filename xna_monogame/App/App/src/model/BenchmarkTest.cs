using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using App.src.testImpl;
using App.src.model.renderables;
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
        public int avg;

        public bool noOutput = false;
        private List<RenderEnum> stateList = new List<RenderEnum>();
        private RenderEnum state = RenderEnum.Bunny;


        public BenchmarkTest(int minVal, int maxVal, int step, int avg)
        {
            this.minVal = minVal;
            this.maxVal = maxVal;
            this.step = step;
            this.avg = avg;

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
            if (frameCount == avg)
            {
                AddBunnies(step);
                if (bunnies.Count > maxVal)
                {
                    return true;
                }
                
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
                bunny.Draw(spriteBatch, drawBatch, this);
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
                IRenderable bunny = getNewSpawn();               
                
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

        public void addRenderState(RenderEnum newState)
        {
            stateList.Add(newState);
        }

        public Texture2D getRandomTexture()
        {
            return getTexture(random.Next(bunnyTextures.Count));
        }

        public Texture2D getTexture(int index)
        {
            return bunnyTextures.ElementAt(index);
        }

        private IRenderable getNewSpawn()
        {
            RenderEnum state;
            IRenderable bunny;

            if (stateList.Count == 0)
            {
                state = RenderEnum.Bunny;
            }
            else
            {
                state = stateList.ElementAt(random.Next(stateList.Count));
            }

            switch (state)
            {
                case RenderEnum.Bunny:
                    bunny = new Bunny(random.Next(bunnyTextures.Count), this);
                    break;
                case RenderEnum.Circle:
                    bunny = new Circle(random);
                    break;
                case RenderEnum.Line:
                    bunny = new Line(random);
                    break;
                case RenderEnum.Point:
                    bunny = new App.src.model.renderables.Point(random);
                    break;
                case RenderEnum.Rectangle:
                    bunny = new App.src.model.renderables.Rectangle(random);
                    break;
                case RenderEnum.Text:
                    bunny = new Text(random, content, maxX, maxY);
                    break;
                case RenderEnum.Triangle:
                    bunny = new Triangle(random);
                    break;
                default:
                    bunny = new Bunny(random.Next(bunnyTextures.Count), this);
                    break;
            }
            return bunny;
        }
    }
}
