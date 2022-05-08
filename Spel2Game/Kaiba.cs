using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Spel2Game
{
    class Kaiba : Enemy
    {
        Animation CurrentAnimation;
        List<Projectile> cards;
        Texture2D cardTexture;
        double attackCooldown;

        public List<Projectile> Cards { get { return cards; } }

        public Kaiba(Texture2D baseText, Animation currentAnimation, float x, float y, float speedX, float speedY, List<Animation> allAnimations, Texture2D cardtexture) : base(baseText, currentAnimation, x, y , speedX, speedY, allAnimations)
        {
            CurrentAnimation = base.AllAnimations[0];
            cardTexture = cardtexture;
            cards = new List<Projectile>();
        }
        public override void Update(GameWindow window, GameTime time, Rectangle playerRec)
        {
            CurrentAnimation.Update(time);
            if (playerRec.Center.X > this.Rectangle.Center.X)
                turnedRight = true;
            else
                turnedRight = false;

            Rectangle temp = new Rectangle(playerRec.X, playerRec.Y, playerRec.Width, playerRec.Height * 2);
            if (temp.Y + temp.Height >= this.Y && temp.Y < this.Y + this.texHeight && attackCooldown <= 0)
            {
                //attack
                CurrentAnimation = AllAnimations[1];
                if (CurrentAnimation.CurrentFrame == 3)
                {
                    if(turnedRight)
                        cards.Add(new Projectile(cardTexture, this.X, this.Y+20, 3.0f, 0f));
                    else
                        cards.Add(new Projectile(cardTexture, this.X, this.Y+20, -3.0f, 0f));

                    attackCooldown += 1.6f; // countar ner från 1.6s o skjuter igen om han är på sin "kast" frame. 
                }
            }
            else if (attackCooldown > 0)
            {
                attackCooldown -= time.ElapsedGameTime.TotalSeconds;
            }
            else
                CurrentAnimation = AllAnimations[0];


            foreach (Projectile p in cards)
            {
                p.Update();
                GameElements._dmgBoxes.Add(p.Rectangle);
                
            }


        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            CurrentAnimation.Draw(spriteBatch, Position, new Vector2(CurrentAnimation.animationWidth, CurrentAnimation.animationHeight), !turnedRight);
            foreach(Projectile p in cards)
            {
                p.Draw(spriteBatch);
            }
        }
    }
}
