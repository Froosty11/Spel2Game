using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spel2Game
{
    abstract class MovingObject : GameObject
    {
        protected Vector2 Velocity; //nuvarande hastigheten
        protected Vector2 speed;
        
        public MovingObject(Texture2D texture, float x, float y, float speedX, float speedY) : base(texture, x, y)
        {
            this.speed.X = speedX;
            this.speed.Y = speedY;
        }

    }
}
