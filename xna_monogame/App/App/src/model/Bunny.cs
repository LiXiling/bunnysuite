﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace App
{
    public class Bunny
    {
        private const int numberOfTextures = 3;
        private static double fullCircle = Math.PI * 2;
        private double growth = 0.1;

        public float X;
        public float Y;

        public float originX;
        public float originY;

        public float SpeedX;
        public float SpeedY;

        public double Rotation = 0;
        public double Scale = 1;


        public Texture2D texture;
        private Texture2D[] textureArray;

        public Bunny(ContentManager content)
        {
            textureArray = new Texture2D[numberOfTextures];
            loadArray(content);
        }

        private void loadArray(ContentManager content)
        {
            for (int i = 0; i < textureArray.Length; i++)
            {
                textureArray[i] = content.Load<Texture2D>(@"wabbit_alpha" + i);
            }
            texture = textureArray[0];
        }

        public void changeTexture(int j, ContentManager content)
        {
            int i = j % numberOfTextures;
            texture = textureArray[i];
            originX = texture.Width / 2;
            originY = texture.Height / 2;
        }

        public void jump(Random random, float gravity, float minX, float minY, float maxX, float maxY)
        {

            this.X += this.SpeedX;
            this.Y += this.SpeedY;
            this.SpeedY += gravity;
            //this.SpeedX += (float) random.NextDouble();

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

        public void teleport(Random random, float maxX, float maxY)
        {
            X = random.Next((int)maxX);
            Y = random.Next((int)maxY);
        }

        public void grow()
        {
            Scale += growth;
            if (Scale >= 5 || Scale <= 0.2)
            {
                growth *= -1;
            }
        }

        public void rotate(Random random)
        {
            Rotation = (Rotation + random.NextDouble() * 0.1) % fullCircle;
        }
    }
}