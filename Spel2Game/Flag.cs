using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spel2Game
{
    class Flag : AnimatedObject
    {
        public Flag(Texture2D baseTexture, Animation anim, float x, float y) : base(baseTexture, anim, x, y, 0, 0)
        {

        }
    }
}
