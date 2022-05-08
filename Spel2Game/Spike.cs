using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spel2Game
{
    class Spike : Terrain
    {
        public Spike(Texture2D texture, float x, float y) : base(texture, x, y, false)
        {

        }
    }
}
