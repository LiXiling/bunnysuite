﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LilyPath;

namespace App.src.model.renderables
{
    public class PathPrimitive : IRenderable
    {
        public float X;
        public float Y;

        private float SpeedX;
        private float SpeedY;

        private float growth = 0.1f;
        private double Rotation = 0.0;
        public Vector2 Scale = new Vector2(1f, 1f);

        private Vector2[] relVertex;
        public static int calls = 0;

        private Brush brush;

        public PathPrimitive(Random r)
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
            drawBatch.DrawPrimitivePath(new Pen(brush), makeAbsolute(relVertex));
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

        public virtual Vector2[] makeAbsolute(Vector2[] relCoords)
        {
            Vector2[] result = (Vector2[])relCoords.Clone();
            double s = Math.Sin(-Rotation * Math.PI / 180.0);
            double c = Math.Cos(-Rotation * Math.PI / 180.0);

            for (int i = 0; i < relVertex.Length; i++)
            {
                Vector2 p = result[i];
                p = Vector2.Multiply(p, Scale);                
                
                float xnew = (float)(p.X * c + p.Y * s);
                float ynew = (float)(-p.X * s + p.Y * c);


                result[i].X = xnew + this.X;
                result[i].Y = ynew + this.Y;                
            }

            return result;
        }

        public void Rotate()
        {

            Rotation += 1;
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
        public void setVertices(float originX, float originY, Vector2[] vertices)
        {
            X = originX;
            Y = originY;
            relVertex = vertices;
        }
        public void Teleport(Random random, float maxX, float maxY)
        {
            X = random.Next((int)maxX);
            Y = random.Next((int)maxY);
        }
    }
}
