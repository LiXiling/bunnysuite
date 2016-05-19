using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace App
{
    public class Bunny
    {
        private const int numberOfTextures = 3;
        public float X;
        public float Y;
        public float SpeedX;
        public float SpeedY;
        public Texture2D texture;
        private int i = 0;

        public Bunny(ContentManager content)
        {
            loadTexture(content);
        }
        private void loadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(@"wabbit_alpha"+i);

        }

        public void changeTexture(int j, ContentManager content) {
            i = j % numberOfTextures;
            loadTexture(content);
        }

        public void jump(Random random, float gravity, float minX, float minY, float maxX, float maxY)
        {
            
            this.X += this.SpeedX;
            this.Y += this.SpeedY;
            this.SpeedY += gravity;

            if (this.X > maxX)
            {
                this.SpeedX *= -1;
                this.X = maxX;
            }
            else if (this.X < minX)
            {
                this.SpeedX *= -1;
                this.X = minX;
            }

            if (this.Y > maxY)
            {
                this.SpeedY *= -0.9f;
                this.Y = maxY;
                if ((float)random.NextDouble() > 0.5)
                {
                    this.SpeedY -= (float)random.NextDouble();
                }
            }
            else if (this.Y < minY)
            {
                this.SpeedY = 0;
                this.Y = minY;
            }
        }
    }
}
