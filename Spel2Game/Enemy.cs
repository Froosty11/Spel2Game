using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Spel2Game
{
    abstract class Enemy : AnimatedObject
    {
        protected List<Animation> AllAnimations;
        public Enemy(Texture2D baseText, Animation currentAnimation, float x, float y, float speedX, float speedY, List<Animation> allAnimations ) : base(baseText, currentAnimation, x, y, speedX, speedY)
        {
            turnedRight = false;
            AllAnimations = allAnimations;
            IsAlive = true;
        }
        abstract public void Update(GameWindow window, GameTime time, Rectangle playerRec);


    }
}
