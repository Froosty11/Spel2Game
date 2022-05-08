using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


namespace Spel2Game
{
    public class Animation
    {        
        public int CurrentFrame { get; set; } // håller koll på nuvarande framen
        public int FrameCount { get; } // hur många frames som ska vara i en animation
        public int AnimHeight { get { return Sprites[0].Height; } } // hur hög den är. Tar den första framen och ger höjden

        public int AnimWidth { get { return Sprites[0].Width; } }

        public bool Repeating; // om animationen ska göras om och om igen eller bara stanna på den sista
        public float AnimationSpeed { get; set; }
        public List<Texture2D> Sprites { get; }
        public float timeSinceLastUpdate;


        public Animation(List<Texture2D> sprites, int frameCount, bool repeating, float animationSpeed) // KONSTRUKTOR FÖR EN NY ANIMATION
        {
            Sprites = sprites; // listan för alla sprites för en hel animation
            FrameCount = frameCount;
            Repeating = repeating;
            AnimationSpeed = animationSpeed;
            
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 position, Vector2 offset, bool turnedRight)
        {
            if (turnedRight)
            {
                spriteBatch.Draw(this.Sprites[this.CurrentFrame], position, Color.White);
            }
            else
            //relativt komplicerad. draw funktionen kräver massa skit som jag inte behöver, behöver endast flip horizontally vilket är en spriteeffect. Av någon anledning finns ingen "metod" för draw med spriteeffects utan allt extra information
            //första egenskapen handlar om vilken sprite, precis som "vanligt", andra är istället för en position en rektangel som spriten ska dras ut efter, vilket jag sätter som exakt samma proportioner som spriten som visas just då
            //En source rectangle vet jag inte riktigt vad det gör, men man kan sätta det som null. Färgen är vit, sen finns en offset vector och sen till slut spriteeffects. 

        
                spriteBatch.Draw(this.Sprites[this.CurrentFrame], new Rectangle(new Point((int)position.X, (int)position.Y),new Point((int)offset.X, (int)offset.Y)),null, Color.White,0,new Vector2(0,0), SpriteEffects.FlipHorizontally,1); // samma som skulle i vanliga fall rita spelaren
        }
        public void Update(GameTime gameTime)
        {
            
            timeSinceLastUpdate += (float)gameTime.ElapsedGameTime.TotalSeconds; // adderar tiden till en timer som resettas när den når animationspeed
            if(timeSinceLastUpdate > AnimationSpeed)
            {
                timeSinceLastUpdate = 0f; //resettar, och därmed blir det oxå en ny frame
                CurrentFrame++;

                if(CurrentFrame >= FrameCount)
                  //om mängden frames är fler än vad som ska finnas så resettas det till frame 0
                {
                    if (Repeating == true)
                        CurrentFrame = 0;
                    else
                        CurrentFrame = FrameCount-1;
                }
            }
        }
        public float animationWidth { get { return this.Sprites[this.CurrentFrame].Width;  } }
        public float animationHeight { get { return this.Sprites[this.CurrentFrame].Height; } }

        public Animation Clone() //deepclone av en animation
        { 
            return new Animation(this.Sprites, this.FrameCount, this.Repeating, this.AnimationSpeed);
        }

        
    }
    
}
