using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using App.src.model;

namespace App.src.testImpl
{
    public interface IBunnyModifier
    {
        /// <summary>
        /// Modifies a Bunny's status
        /// </summary>
        /// <param name="bunny">The Bunny to be modified</param>
        /// <param name="bt">The BenchmarkTest calling this method</param>
        void ModifyBunny(Bunny bunny, BenchmarkTest bt);
    }
}
