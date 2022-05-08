using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spel2Game
{

    //Playerklassen används för att skapa en ny player. består av en sprite, en nuvarande position, och en vector för vilket håll den åker
    class Player : PhysicalObject
    {
        List<Animation> AllAnimations;
        Animation CurrentAnimation;
        bool turnedRight;
        public bool jumping;
        public bool inAir;
        public enum InAnimation { none, dash, pound, damage};
        public InAnimation animationState;
        public float dmgCooldown;
        private SpecialAttack atkDash;
        private SpecialAttack atkPound;

        protected Healthbar hpbar;

        double timeInSeconds;


        public Player(List<Animation> allAnimations, Texture2D basetexture, Texture2D heartTexture, float x, float y, float speedX, float speedY) : base(basetexture, x, y , speedX, speedY) //konstruktor, gör en ny spelare
        {
            this.AllAnimations = allAnimations;
            this.CurrentAnimation = allAnimations[0];
            this.turnedRight = true;
            this.inAir = true;
            this.atkDash = new SpecialAttack(new Vector2(240, 0), AllAnimations[3], 0.5f, 1.5f);
            this.animationState = InAnimation.none;
            this.atkPound = new SpecialAttack(new Vector2(0, 220), AllAnimations[2], 0.5f, 0.0f);
            this.hpbar = new Healthbar(heartTexture, 5);
        }
        public void Update(GameTime gameTime, List<Terrain> collisionObjects, List<Animation> animationsList, List<Rectangle> dmgboxes, List<Enemy> enemies)
        {
            CurrentAnimation.Update(gameTime);
            CurrentAnimation = AllAnimations[0];

            Physics(collisionObjects);
            if (!inAir && animationState == InAnimation.damage)
                animationState = InAnimation.none;
            atkDash.timeSinceLastPlay += (float)gameTime.ElapsedGameTime.TotalSeconds;
            dmgCooldown += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!(dmgboxes == null))
            {
                CheckHitboxes(gameTime, dmgboxes);
            }
            timeInSeconds += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            CheckAttacks(gameTime, enemies, collisionObjects);

            Move(collisionObjects, gameTime); //Metod som adderar spelarens speed till sin velocity beroende på vilka tangenter som trycks. Förutom om man är i väggar
                                              //04/23 la nu också in special attacker här. just nu bara en dash
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                GameElements.gameState = GameElements.State.Menu;
                GameElements.Reset(0);
            }

            if (!inAir)
            {
                atkPound.currentPlaytime = 0;
                AllAnimations[1].CurrentFrame = 0; // resettar hoppanimationen eftersom den inte är repeating. 
                                                   //animera spelarens sprite
                AllAnimations[6].CurrentFrame = 0;
            }


            Animate();
            // adderar den uträknade velocityn till spelarens vector
            if (!(Velocity.Y == 0))
            {
                inAir = true;
            }



            Position += Velocity;
            if (animationState == InAnimation.none)
                Velocity.X = 0; // "resettar" velocity och därmed gör iordning för nästa update. Hade oxå gått att göra velocity.X = 0; och velocity.Y = 0;


            hpbar.Update(this.Rectangle);

            if (hpbar.currentHealth<= 0)
            {
                isAlive = false;
            }


        }

        private void CheckAttacks(GameTime gameTime, List<Enemy> enemies, List<Terrain> terrain)
        {
            foreach (Enemy e in enemies)
            {
                if (e.Rectangle.Intersects(this.Rectangle))
                {
                    bool isAttacking = animationState == InAnimation.dash || animationState == InAnimation.pound;
                    if (isAttacking)
                        e.IsAlive = false;

                    else if (this.IsTouchingTop(e) && animationState != InAnimation.damage){
                        e.IsAlive = false;
                        Velocity.Y *= -1;
                    }
                    else
                        TakeDamage(gameTime);
                }
            }
        }

        private void Move(List<Terrain> collisionObjects, GameTime gameTime)
        {

            bool notTakingDamage = !(animationState == InAnimation.damage);
            if (notTakingDamage) 
            {


                bool walkLeft = Keyboard.GetState().IsKeyDown(Keys.D) && !Keyboard.GetState().IsKeyDown(Keys.A);
                bool walkRight = Keyboard.GetState().IsKeyDown(Keys.A) && !Keyboard.GetState().IsKeyDown(Keys.D);
                if (walkRight)
                {
                    Velocity.X -= speed.X;
                    turnedRight = false;
                }

                else if (walkLeft)
                {
                    Velocity.X += speed.X;
                    turnedRight = true;
                }

                bool canJump = Keyboard.GetState().IsKeyDown(Keys.W) && inAir == false;
                if (canJump)
                {
                    jumping = true;
                    inAir = true;
                }


                if ((Keyboard.GetState().IsKeyDown(Keys.A) || Keyboard.GetState().IsKeyDown(Keys.D)) && Keyboard.GetState().IsKeyDown(Keys.LeftShift) || atkDash.currentPlaytime > 0)
                {
                    Vector2 temp = atkDash.AttackUpdate(gameTime, Position, turnedRight);

                    if (temp != Vector2.Zero)
                    {
                        Velocity = temp;
                        animationState = InAnimation.dash;
                    }
                    else
                        animationState = InAnimation.none;
                }
                else
                    animationState = InAnimation.none;

                if ((Keyboard.GetState().IsKeyDown(Keys.S) || atkPound.currentPlaytime > 0) && this.inAir )
                    {
                    Vector2 temp = atkPound.AttackUpdate(gameTime, Position, turnedRight);

                    if (temp != Vector2.Zero)
                    {
                        Velocity = temp;
                        animationState = InAnimation.pound;
                    }
                    else
                        animationState = InAnimation.none;
                }


                if (Keyboard.GetState().IsKeyDown(Keys.K))
                {
                    TakeDamage(gameTime);

                }
            }



            foreach (var physicalObject in collisionObjects)
            {

                bool MovingRight = this.Velocity.X > 0;
                bool MovingLeft = this.Velocity.X < 0;

                bool xCollision = MovingRight && this.IsTouchingLeft(physicalObject) ||
                                    (MovingLeft && this.IsTouchingRight(physicalObject));
                if (xCollision)
                {
                    if (MovingRight) //Om velocity just nu är påväg höger- dvs positiv velocity och vi vet att den kommer träffa något i x ledet
                        this.Velocity.X = this.Rectangle.Right - physicalObject.Rectangle.Left; //Räkna ut skillnaden mellan objekt och spelare- och därefter sätt velocity till allt som behövs.
                    else if (MovingLeft) // samma som ovan fast istället är det påväg vänster
                        this.Velocity.X = this.Rectangle.Left - physicalObject.Rectangle.Right;


                    if (animationState == InAnimation.dash)
                    {
                        if (physicalObject.IsBreakable)
                        {
                            physicalObject.IsAlive = false;
                        }
                        else
                        {
                            TakeDamage(gameTime);
                            atkDash.currentPlaytime = 0;
                            atkDash.timeSinceLastPlay = 0;
                        }


                    }
                        
                }
                



                bool MovingDown = this.Velocity.Y > 0;
                bool MovingUp = this.Velocity.Y < 0;
                bool yCollision = MovingDown && this.IsTouchingTop(physicalObject) || // checkar om den kommer ovanifrån och har en positiv y-velocity ELLER om den kommer underifrån med negativ velocity.
                                    MovingUp && this.IsTouchingBottom(physicalObject);
                if (yCollision)
                {
                    if (MovingDown) //om du kommer ihåg hade jag problem med att den studsade lite innan landning- löste. hade fel ordning på physObject och this i raden under och därmed fick -3 istället för +3 i velocity
                    {

                        if(animationState == InAnimation.pound && physicalObject.IsBreakable)
                        {
                            physicalObject.IsAlive = false;
                        }
                        if(physicalObject is Spike)
                        {
                            TakeDamage(gameTime);
                            this.inAir = false;
                            break;
                        }

                        this.Velocity.Y =  physicalObject.Rectangle.Top - this.Rectangle.Bottom; // samma som x-led fast bytt
                        this.inAir = false;
                    }

                    else if (MovingUp) // om spelaren är  påväg uppåt
                    {
                        this.Velocity.Y = physicalObject.Rectangle.Bottom - this.Rectangle.Top;
                    }
                }
            }
        }

        private void Animate()
        {
                if (inAir)
                    CurrentAnimation = AllAnimations[1];
                else if (Velocity.X > 0 || Velocity.X < 0)
                    CurrentAnimation = AllAnimations[2];
                else
                    CurrentAnimation = AllAnimations[0];

                if(animationState == InAnimation.dash)
                CurrentAnimation = AllAnimations[3];
            if (animationState == InAnimation.damage)
                CurrentAnimation = AllAnimations[4];
            if (animationState == InAnimation.pound)
                CurrentAnimation = AllAnimations[6];


        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            CurrentAnimation.Draw(spriteBatch, Position, new Vector2(CurrentAnimation.animationWidth,CurrentAnimation.animationHeight), turnedRight);
            hpbar.DrawHearts(spriteBatch);
        }


        private void Physics(List<Terrain> collisionObjects)
        {
            if(Velocity.Y < 8f)
                Velocity.Y += 0.25f;

            if(jumping == true)
            {
                Velocity.Y +=    (float) -4.0f;
                jumping = false;
            }


        }

        public void TakeDamage(GameTime gameTime)
        {
            if (dmgCooldown > 0.5f) 
            {
                AllAnimations[4].CurrentFrame = 0;
                animationState = InAnimation.damage;
                hpbar.currentHealth -= 1;
                dmgCooldown = 0;
                if (turnedRight)
                    Velocity = new Vector2(-4, -6);
                else
                    Velocity = new Vector2(4, -6);
            }

        }

        public void CheckHitboxes(GameTime gametime, List<Rectangle> dmgboxes)
        {
            foreach(Rectangle r in dmgboxes)
            {
                if (r.Intersects(this.Rectangle))
                {
                    TakeDamage(gametime);
                }
            }
        }

        public void Reset()
        {
            Position = new Vector2(120, 180);
            Velocity = Vector2.Zero;
            isAlive = true;
            hpbar.currentHealth = 5;
            animationState = InAnimation.none;
        }


        public float GetYVelocity { get { return Velocity.Y; } }
        public float SetY { set { Position.Y = value; } } //behöver dem här för att GameElements reset funktion ska kunna ladda in spelaren på valfri position beroende på level :) 
        public float SetX { set { Position.X = value; } }



        public double playTime { get { return timeInSeconds; } set { timeInSeconds = value; } }


    }
}
