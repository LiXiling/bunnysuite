using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LilyPath;

namespace App.src.model.renderables
{
    public class Line : PathPrimitive
    {         
        public Line(Random r): base(r)
        {
            float X = 13;
            float Y = 18;   
            Vector2[] relVertex = new Vector2[2];

            relVertex[0] = new Vector2(-13, -18);
            relVertex[1] = new Vector2(13, 19);

            setVertices(X, Y, relVertex);
        }
    }
}
