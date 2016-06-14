﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using App.src.model;

namespace App.src.testImpl
{
    public class MultitextureAdd : IBunnyModifier
    {
        public void ModifyBunny(Bunny bunny, BenchmarkTest bt){
                bunny.changeTexture(bt.random.Next(), bt.content);           
        }
    }
}
