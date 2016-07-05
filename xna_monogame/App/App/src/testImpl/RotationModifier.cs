﻿using App.src.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.src.testImpl
{
    public class RotationModifier : IBunnyModifier
    {
        public void ModifyBunny(Bunny bunny, BenchmarkTest bt)
        {
            bunny.rotate(bt.random);
        }
    }
}