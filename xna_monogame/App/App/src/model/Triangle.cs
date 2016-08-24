using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace App.src.model
{
    public class Triangle: IRenderable
    {
        private int originX = 0;
        private int originY = 0;
        private VertexPositionColor[] vertices = new VertexPositionColor[3];
     
        public Triangle()
        {            
            vertices[0].Position = new Vector3(0, 137, 0);
            vertices[0].Color = Color.Red;
            vertices[1].Position = new Vector3(126, 137, 0);
            vertices[1].Color = Color.Red;
            vertices[2].Position = new Vector3(113, 0, 0);
            vertices[2].Color = Color.Red;
        }

        public void ChangeTexture(Texture2D texture)
        {
            return;
        }
        public void Draw(SpriteBatch spriteBatch, GraphicsDevice device, BasicEffect basicEffect)
        {           
            device.DrawUserPrimitives<VertexPositionColor>(
                PrimitiveType.TriangleList,
                vertices,
                0,
                1,
                VertexPositionColor.VertexDeclaration
            );
        }
        public void Grow() 
        {
            return;
        }

        public void Jump(Random random, float gravity, float minX, float minY, float maxX, float maxY)
        {
            return;
        }
        public void Rotate(Random random)
        {
            return;
        }
        public void SetScale(float xScale, float yScale)
        {
            return;
        }
        public void SetSpeed(float xSpeed, float ySpeed)
        {
            return;
        }
        public void Teleport(Random random, float maxX, float maxY)
        {
            return;
        }
    }
}
