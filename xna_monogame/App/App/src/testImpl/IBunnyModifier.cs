using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using App.src.model;
using App.src.model.renderables;

namespace App.src.testImpl
{
    public interface IBunnyModifier
    {
        /// <summary>
        /// Modifies a Bunny's status
        /// </summary>
        /// <param name="renderable">The RenderObject to be modified</param>
        /// <param name="bt">The BenchmarkTest calling this method</param>
        void ModifyBunny(IRenderable renderable, BenchmarkTest bt);
    }
}
