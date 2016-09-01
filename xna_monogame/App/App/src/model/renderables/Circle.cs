using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LilyPath;

namespace App.src.model.renderables
{
    public class Circle : IRenderable
    {
        private float X = 13;
        private float Y = 13;

        private float SpeedX;
        private float SpeedY;

        private float growth = 0.1f;
        private float radius = 13;
        private float scale = 1;

        private Brush brush;

        public Circle(Random r)
        {
            ColorChange(r);
        }

        public void ChangeTexture(int index, BenchmarkTest bt)
        {
            return;
        }
        public void ColorChange(Random r)
        {
            brush = new SolidColorBrush(new Color(
                (byte)r.Next(0, 255),
                (byte)r.Next(0, 255),
                (byte)r.Next(0, 255)
            ));
        }
        public void Draw(SpriteBatch spriteBatch, DrawBatch drawBatch, BenchmarkTest bt)
        {
            drawBatch.DrawPrimitiveCircle(new Pen(brush), new Vector2(X, Y), radius * scale);
        }
        public void Grow()
        {
            scale += growth;
            if (scale >= 5 || scale <= 0.2)
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
            return;
        }
        public void SetScale(float xScale, float yScale)
        {
            scale = xScale;
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
