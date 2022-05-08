using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Spel2Game
{
    class Minecraft : Enemy
    {
        Animation CurrentAnimation;
        float startX;
        public Rectangle swordHitbox;
        int swordOffset;
        int attackTimer;
        public Minecraft(Texture2D baseText, Animation currentAnimation, float x, float y, float speedX, float speedY, List<Animation> allAnimations) : base(baseText, currentAnimation, x, y, speedX, speedY, allAnimations) {
            CurrentAnimation = base.AllAnimations[1];
            startX = x;
            turnedRight = false;
        }

        public override void Update(GameWindow window, GameTime time, Rectangle playerBox)
        {
            CurrentAnimation.Update(time);
            if(attackTimer > 0)
            {
                CurrentAnimation = AllAnimations[2];
                attackTimer -= 1;
                if(attackTimer > 105 && attackTimer < 110)
                {
                    GameElements._dmgBoxes.Add(swordHitbox);
                }
            }
            if (attackTimer < 73)
                CurrentAnimation = AllAnimations[0];


            if (attackTimer <= 0)
            {
                WalkAndTurn();
                CurrentAnimation = AllAnimations[1];
            }


            
            if (!turnedRight)
                swordOffset = -(int)this.texWidth / 4;
            else
                swordOffset = (int)this.texWidth / 4 + (int)this.texWidth;
            swordHitbox = new Rectangle(new Point((int)this.Position.X + swordOffset, (int)this.Position.Y), new Point((int)this.texHeight, (int)this.texWidth));
            bool eyeIntersect = swordHitbox.Intersects(playerBox);
            if (eyeIntersect && attackTimer == 0)
            {
                attackTimer = 120; //60 fps/ dvs över lite mer än 2d sekunder. 
            }


            
        }

        private void WalkAndTurn()
        {
            Velocity.X = -speed.X;
            bool ifFinishedWalk = Position.X < startX - 96;
            if (ifFinishedWalk) //om han har gått hela vägen 2 blocks vänster vänder han till höger
                turnedRight = true;
            else if (Position.X > startX)
                turnedRight = false;


            if (turnedRight)
                Velocity.X *= -1;
            Position.X += Velocity.X;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            CurrentAnimation.Draw(spriteBatch, Position, new Vector2(CurrentAnimation.animationWidth, CurrentAnimation.animationHeight), turnedRight);
            //spriteBatch.DrawString(GameElements.font, "attackCD: " + attackTimer + "\n" + IsAlive, this.Position, Color.White);
        }


    }
}
