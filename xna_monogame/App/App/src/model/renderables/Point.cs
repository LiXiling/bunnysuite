using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LilyPath;

namespace App.src.model.renderables
{
    public class Point : PathPrimitive
    {
        public Point(Random r)
            : base(r)
        {
            float X = 13;
            float Y = 18;
            Vector2[] relVertex = new Vector2[2];

            relVertex[0] = new Vector2(0, 0);
            relVertex[1] = new Vector2(1, 0);

            setVertices(X, Y, relVertex);
        }

        public override Vector2[] makeAbsolute(Vector2[] relCoords)
        {
            Vector2[] result = (Vector2[])relCoords.Clone();

            for (int i = 0; i < result.Length; i++)
            {             
                result[i].X += this.X;
                result[i].Y += this.Y;
            }
            return result;
        }
    }
}
