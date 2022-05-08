using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Content;

namespace Spel2Game
{
    class Menu
    {
        List<MenuItem> menuItems;
        public Menu(List<MenuItem> menuItems)
        {
            this.menuItems = menuItems;
        }
        public void Update(GameTime time)
        {
            foreach(MenuItem m in menuItems)
            {
                m.Update(time);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (MenuItem m in menuItems)
            {
                m.Draw(spriteBatch);
            }
        }
    }

    class MenuItem : GameObject
    {
        GameElements.State buttonState;
        MouseState oldMouseState;
        MouseState mouseState;
        bool rörKnapp;
        Rectangle rec;

        public MenuItem(Texture2D texture, Vector2 position, GameElements.State buttonState) : base(texture, position.X, position.Y) {
            this.buttonState = buttonState;
            rec = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            oldMouseState = mouseState;
            mouseState = Mouse.GetState();

            var mouseRectangle = new Rectangle(mouseState.X, mouseState.Y-20, 1, 5);

            rörKnapp = false;
            if (mouseRectangle.Intersects(rec))
            {
                rörKnapp = true;

                if (mouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed)
                {
                    GameElements.gameState = buttonState;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            if (rörKnapp)
                colour = Color.DarkGray;

            spriteBatch.Draw(texture, rec, colour);

        }
    }
}
