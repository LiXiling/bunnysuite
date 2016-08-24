using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace App.src.model
{
    public interface IRenderable
    {
        void ChangeTexture(Texture2D texture);
        void Draw(SpriteBatch spriteBatch, GraphicsDevice device, BasicEffect basicEffect);
        void Grow();
        void Jump(Random random, float gravity, float minX, float minY, float maxX, float maxY);
        void Rotate(Random random);
        void SetScale(float xScale, float yScale);
        void SetSpeed(float xSpeed, float ySpeed);
        void Teleport(Random random, float maxX, float maxY);
    }
}
