using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spel2Game
{
    class SpecialAttack
    {
        private float TimeUsed;
        private Animation AnimUsed;
        private Vector2 TravelVector;
        public float currentPlaytime;
        private float Cooldown;
        public float timeSinceLastPlay;
        public SpecialAttack(Vector2 travel, Animation anim, float playtime, float cooldown)
        {
            this.TimeUsed = playtime;
            this.AnimUsed = anim;
            this.TravelVector = travel;
            this.Cooldown = cooldown;
        }

        public Vector2 AttackUpdate(GameTime gameTime, Vector2 pos, bool turnedRight)
        {

            if(timeSinceLastPlay >= Cooldown)
            {
                float temp2 = (float)gameTime.ElapsedGameTime.TotalSeconds;
                currentPlaytime += temp2; // adderar tiden till en timer som tas när den når animationspeed
                if (currentPlaytime < TimeUsed)
                {
                    this.AnimUsed.Update(gameTime);
                    Vector2 temp;
                    if (turnedRight)
                        temp = new Vector2(TravelVector.X / TimeUsed * temp2 , TravelVector.Y / TimeUsed *temp2);
                    else
                        temp = new Vector2(TravelVector.X / TimeUsed * -1 * temp2, TravelVector.Y / TimeUsed * temp2);

                    return temp;
                }
                else
                {
                    currentPlaytime = 0;
                    timeSinceLastPlay = 0;
                    return Vector2.Zero;
                }
            }
            return
                Vector2.Zero;



        }
        
    }
}
