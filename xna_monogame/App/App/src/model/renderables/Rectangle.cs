using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LilyPath;

namespace App.src.model.renderables
{
    public class Rectangle : PathPrimitive
    {            

        public Rectangle(Random r): base(r)
        {            
            float X = 13;
            float Y = 18;

            Vector2[] relVertex = new Vector2[5];

            relVertex[0] = new Vector2(-13, -18);
            relVertex[1] = new Vector2(-13, 19);
            relVertex[2] = new Vector2(13, 19);
            relVertex[3] = new Vector2(13, -18);
            relVertex[4] = relVertex[0];

            setVertices(X, Y, relVertex);
        }
    }
}
