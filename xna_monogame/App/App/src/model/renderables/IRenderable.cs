﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LilyPath;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace App.src.model.renderables
{
    public interface IRenderable
    {
        void ChangeTexture(int index, BenchmarkTest bt);
        void ColorChange(Random r);
        void Draw(SpriteBatch spriteBatch, DrawBatch drawBatch, BenchmarkTest bt);
        void Grow();
        void Jump(Random random, float gravity, float minX, float minY, float maxX, float maxY);
        void Rotate();
        void SetScale(float xScale, float yScale);
        void SetSpeed(float xSpeed, float ySpeed);
        void Teleport(Random random, float maxX, float maxY);
    }
}
