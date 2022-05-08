using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
namespace Spel2Game
{
    class Camera
    {
        public Matrix Transform { get; set; }  // bara get/setter för att _spriteBatch.Begin() behöver den som en variabel
        public void cameraFollow(Player player) // metod som får kameran att följa spelaren. 
        {
            Matrix position = Matrix.CreateTranslation(-(int) 0, // följer spelaren i både y led. Halva spelarens rectangle använder jag för att få kameran i mittpunkten på spelaren. eftersom det är en vertikal platformer gjorde jag bara y led
                -(int)GameElements.player.Y - GameElements.player.Rectangle.Height / 2, 0);

            Matrix screenOffset = Matrix.CreateTranslation(0, Game1.ScreenHeight / 2, 0); //screenoffsetten är till för att kameran ska vara "offsettad" ifrån spelaren så att inte spelaren är högst upp i högra hörnet

            Transform = position * screenOffset;

        }
    }
}
