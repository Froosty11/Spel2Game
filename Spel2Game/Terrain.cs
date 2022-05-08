using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spel2Game
{
    class Terrain : PhysicalObject
    {
        public bool IsBreakable { get; }
        public Terrain(Texture2D texture, float x, float y, bool breakable) : base(texture, x, y, 0, 0) // egentligen behövs inte denna men kändes appropriate. gör så attt terrain inte rör sig vilket är skillnaden. 
        {
            IsBreakable = breakable;
        }

    }
}
