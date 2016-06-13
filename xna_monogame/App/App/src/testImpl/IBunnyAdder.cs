using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using App.src.model;

namespace App.src.testImpl
{
    public interface IBunnyAdder
    {
        /// <summary>
        /// Adds Bunnies to the Scene
        /// </summary>
        /// <param name="count">The Amount of Bunnies to be added</param>
        /// <param name="bt">The BenchmarkTest calling this method</param>
        void AddBunnies(int count, BenchmarkTest bt);
    }
}
