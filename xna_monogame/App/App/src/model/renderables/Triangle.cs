using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LilyPath;

namespace App.src.model.renderables
{
    public class Triangle : PathPrimitive
    {

        public Triangle(Random r) : base(r)
        {
            float X = 13;
            float Y = 25;
            Vector2[] relVertex = new Vector2[4];
            relVertex[0] = new Vector2(-13, 12);
            relVertex[1] = new Vector2(0, -25);
            relVertex[2] = new Vector2(13, 12);
            relVertex[3] = relVertex[0];

            setVertices(X,Y,relVertex);
        }
    }
}