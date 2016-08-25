using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using LilyPath;

namespace App.src.model
{
    public class Bunny : IRenderable
    {
        //private const int numberOfTextures = 3;
        private static double fullCircle = Math.PI * 2;
        private float growth = 0.1f;

        public float X;
        public float Y;

        public float originX;
        public float originY;

        public float SpeedX;
        public float SpeedY;

        public float Rotation = 0;

        public Vector2 Scale = new Vector2(1f, 1f);
        public float initScale = 1;


        public Texture2D texture;
        //private Texture2D[] textureArray;

        public Bunny(Texture2D texture)
        {
            ChangeTexture(texture);
        }

        public void ChangeTexture(Texture2D texture)
        {
            this.texture = texture;

            initScale = (37f / texture.Height);

            originX = texture.Width / 2;
            originY = texture.Height / 2;
        }

        public void Draw(SpriteBatch spriteBatch, DrawBatch drawBatch)
        {            
            spriteBatch.Draw(
                this.texture,
                new Vector2(this.X, this.Y),
                null,
                Color.White,
                this.Rotation,
                new Vector2(this.originX, this.originY),
                Vector2.Multiply(this.Scale, this.initScale),
                SpriteEffects.None,
                0f
           );
            
        }

        public void Grow()
        {
            Scale.X += growth;
            Scale.Y += growth;
            if (Scale.X >= 5 || Scale.X <= 0.2)
            {
                growth *= -1;
            }
        }

        public void Jump(Random random, float gravity, float minX, float minY, float maxX, float maxY)
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
                this.SpeedY *= -0.8f;
                this.Y = maxY;
                if ((float)random.NextDouble() > 0.5)
                {
                    this.SpeedY -= 3f + (float)random.NextDouble() * 4f;
                }
            }
            else if (this.Y < minY)
            {
                this.SpeedY = 0;
                this.Y = minY;
            }
        }

        public void Rotate(Random random)
        {
            Rotation = (float) ((Rotation + 0.1) % fullCircle);
        }

        public void SetScale(float xScale, float yScale)
        {
            Scale.X = xScale;
            Scale.Y = yScale;
        }

        public void SetSpeed(float xSpeed, float ySpeed)
        {
            SpeedX = xSpeed;
            SpeedY = ySpeed;
        }

        public void Teleport(Random random, float maxX, float maxY)
        {
            X = random.Next((int)maxX);
            Y = random.Next((int)maxY);
        }
    }
}
