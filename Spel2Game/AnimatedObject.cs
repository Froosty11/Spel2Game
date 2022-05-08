using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Spel2Game
{   
    class AnimatedObject : PhysicalObject
    {

        Animation CurrentAnimation;
        protected bool turnedRight;

        public AnimatedObject(Texture2D baseTexture, Animation anim, float x, float y, float speedX, float speedy) : base(baseTexture, x, y, speedX, speedy)
        {
            turnedRight = false ;
            this.CurrentAnimation = anim;
            IsAlive = false;
        }
        public override void Update(GameWindow window, GameTime time)
        {
            CurrentAnimation.Update(time);
            this.Position += speed;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            CurrentAnimation.Draw(spriteBatch, Position, new Vector2(CurrentAnimation.animationWidth, CurrentAnimation.animationHeight), turnedRight);
        }
    }
}
