using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Spel2Game
{
    class BackgroundSprite : GameObject
    {
        public BackgroundSprite(Texture2D texture, float x, float y) : base(texture, x, y)
        {

        }

        public void Update(GameWindow window, int nrBackgroundY, float test)
        {
            if (Position.Y-texture.Height > test + Game1.ScreenHeight / 2)
            {
                Position.Y -= nrBackgroundY * texture.Height;
            }
            else if (Position.Y+texture.Height < test - Game1.ScreenHeight / 2)
                Position.Y +=  nrBackgroundY * texture.Height;
        }
    }
}

