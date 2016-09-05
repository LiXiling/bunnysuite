using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LilyPath;
using Microsoft.Xna.Framework.Content;

namespace App.src.model.renderables
{
    public class Text : IRenderable
    {
        private static double fullCircle = Math.PI * 2;
        private static SpriteFont spriteFont;
        private String textString = "Hello World!";

        private float X = 200;
        private float Y = 200;

        private float maxX;
        private float maxY;

        private float SpeedX;
        private float SpeedY;

        private float Rotation = 0;

        private float growth = 0.1f;
        public int initScale = 1;
        public Vector2 Scale = new Vector2(1f, 1f);

        private Color color;

        public Text(Random r, ContentManager content, float maxX, float maxY)
        {
            ColorChange(r);

            if (spriteFont == null)
            {
                spriteFont = content.Load<SpriteFont>(@"default");
            }

            this.maxX = maxX;
            this.maxY = maxY;
        }

        public void ChangeTexture(int index, BenchmarkTest bt)
        {
            return;
        }

        public void ColorChange(Random r)
        {
            color =new Color(
                (byte)r.Next(0, 255),
                (byte)r.Next(0, 255),
                (byte)r.Next(0, 255)
            );
        }

        public void Draw(SpriteBatch spriteBatch, DrawBatch drawBatch, BenchmarkTest bt)
        {
            Vector2 pos = new Vector2(this.X,this.Y);
            spriteBatch.DrawString(
                spriteFont,
                textString,
                pos,
                color,
                Rotation,
                new Vector2(0,0),
                initScale * Scale,
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
        public void Rotate()
        {
            Rotation = (float)((Rotation + Math.PI / 180.0));
        }
        public void SetScale(float xScale, float yScale)
        {
            Scale.X = xScale;
            Scale.Y = yScale;
        }
        public void SetSpeed(float xSpeed, float ySpeed)
        {
            this.SpeedX = xSpeed;
            this.SpeedY = ySpeed;
        }
        public void Teleport(Random random, float maxX, float maxY)
        {
            X = random.Next((int)maxX);
            Y = random.Next((int)maxY);
        }
    }
}
