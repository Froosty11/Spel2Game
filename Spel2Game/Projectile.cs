using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Spel2Game
{
    class Projectile : PhysicalObject
    {
        public Projectile(Texture2D texture, float x, float y, float speedX, float speedY) : base(texture, x, y, speedX, speedY)
        {

        }
        public void Update()
        {
            Position += speed;
        }
    }
}
