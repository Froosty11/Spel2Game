using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Spel2Game
{
    class Healthbar
    {
        public int currentHealth { get; set; }
        int maxHealth;
        Texture2D hptexture;
        List<GameObject> allHearts;
        public Healthbar(Texture2D texture, int maximumHealth)//behöver inte en hel spelare eller objekt, eftersom det bara är för centrerings skull
        {
            currentHealth = maximumHealth;
            maxHealth = maximumHealth;
            hptexture = texture;
            allHearts = new List<GameObject>();
        }                                                                                                                                                                                                                                                                                                                
        public void Update(Rectangle playerRec)
        {
            allHearts.Clear();
            for(int i = 1; i <= currentHealth; i++)
            {
                double x = playerRec.Center.X - (i) * (hptexture.Width+1) + (2.5*hptexture.Width); // i mitten av spelaren- splittat hälften åt höger hälften åt vänster. *1.1 på hptexture är för att dem inte ska sitta precis brevid varann
                allHearts.Add(new GameObject(hptexture, (int)x, playerRec.Center.Y - hptexture.Height - playerRec.Height/2 - 30));
            }
        }
        public void DrawHearts(SpriteBatch spriteBatch)
        {
            foreach(GameObject h in allHearts)
            {
                h.Draw(spriteBatch);
            }
        }
    }
}
