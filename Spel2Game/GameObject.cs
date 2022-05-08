using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spel2Game
{
    class GameObject
    {
        protected Texture2D texture; // objektets textur
        protected Vector2 Position; //objektets position
        public GameObject(Texture2D texture, float X, float Y) //konstruktor
        {
            this.texture = texture;
            this.Position.X = X;
            this.Position.Y = Y;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
        public float X { get { return Position.X; } } //getter för x koordinat
        public float Y { get { return Position.Y; } } //getter för y koordinat
        public float texWidth { get { return texture.Width; } }
        public float texHeight { get { return texture.Height; } }


    }
    class PhysicalObject : MovingObject
        {
        protected bool isAlive = true;
            public PhysicalObject(Texture2D texture, float x, float y, float speedX, float speedY) : base(texture, x, y, speedX, speedY)
            {
                //ingen kod här än- konstruktorn är allt samma som  movingobject
            }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            }
        }
        protected bool IsTouchingLeft(PhysicalObject otherObject)
        {
            return this.Rectangle.Right + this.Velocity.X > otherObject.Rectangle.Left &&
              this.Rectangle.Left < otherObject.Rectangle.Left &&
              this.Rectangle.Bottom > otherObject.Rectangle.Top &&
              this.Rectangle.Top < otherObject.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(PhysicalObject otherObject)
        {
            return this.Rectangle.Left + this.Velocity.X < otherObject.Rectangle.Right &&
              this.Rectangle.Right > otherObject.Rectangle.Right &&
              this.Rectangle.Bottom > otherObject.Rectangle.Top &&
              this.Rectangle.Top < otherObject.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(PhysicalObject otherObject)
        {
            return this.Rectangle.Bottom + this.Velocity.Y >= otherObject.Rectangle.Top &&
              this.Rectangle.Top < otherObject.Rectangle.Top &&
              this.Rectangle.Right > otherObject.Rectangle.Left &&
              this.Rectangle.Left < otherObject.Rectangle.Right;
        }

        protected bool IsTouchingBottom(PhysicalObject otherObject)
        {
            return this.Rectangle.Top + this.Velocity.Y <= otherObject.Rectangle.Bottom &&
              this.Rectangle.Bottom > otherObject.Rectangle.Bottom &&
              this.Rectangle.Right > otherObject.Rectangle.Left &&
              this.Rectangle.Left < otherObject.Rectangle.Right;
        }
        public bool IsAlive { get { return isAlive; } set { isAlive = value; } }
        public virtual void Update(GameWindow window, GameTime time) { }
        }
    }
