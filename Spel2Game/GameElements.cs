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
    static class GameElements
    {
        static int _lvl; //vilken nivå man är på
        static int _amountOfLevels;

        public enum State{ Menu, Run, HighScore, Quit, Win};
        public static State gameState;
        static List<Texture2D> brickTextures;
        static List<Texture2D> woodTextures;
        static List<Texture2D> idleTextures;
        static List<Texture2D> poundTextures;
        static List<Texture2D> walkTextures;
        static List<Texture2D> jumpTextures;
        static List<Texture2D> dashTextures;
        static List<Texture2D> torchTextures;
        static List<Texture2D> steveIdleTextures;
        static List<Texture2D> steveWalkTextures;
        static List<Texture2D> stunTextures;
        static List<Texture2D> takeDmgTextures;
        static List<Texture2D> steveStrikeTextures;
        static List<Texture2D> kaibaIdleTextures;
        static List<Texture2D> kaibaAttackTextures;
        static List<Texture2D> flagTextures;
        static List<Animation> playerAnimations;
        static List<Animation> minecraftAnimations;
        static List<Animation> kaibaAnimations;

        //meny
        static List<Texture2D> mmTextures;
        static List<MenuItem> mMenuItems;
        static Menu mainMenu;

        static List<TimerScore> timeScores; //hade tyvärr nt tid, men var påväg att göra en level class som hanterade konstruktor och därmed också var bättre. istället blir det en lista

        static Random rnd;

        static Animation animWalk;
        static Animation animDash;
        static Animation animIdle;
        static Animation animJump;
        static Animation animPound;
        static Animation animStun;
        static Animation animDmg;
        static Animation animTorch;
        static Animation minecraftIdle;
        static Animation minecraftWalk;
        static Animation minecraftStrike;
        static Animation kaibaAttack;
        static Animation kaibaIdle;
        static Animation animFlag;

        static Texture2D spikeSprite;
        static Background background;

        static Texture2D menuSprite;

        //spelare
        public static Player player;


        //viktigt skit som typ kamera class, lista av objekt osv
        public static SpriteFont font;
        static List<Terrain> _collisionObjects; //collisionobjects är physicalObjects och kolliderar. Har implementerat att dem kan vara animerade med polyformism update och draw funktione
        static List<AnimatedObject> _animationObjects; //animationObjects för bara objekt som ska animeras men inte kollideras med
        static List<Enemy> _enemies;
        public static List<Rectangle> _dmgBoxes;
        private static List<Flag> _winFlags;
        static StreamReader laddaFil;

        public static void Initialize()
        {
            _collisionObjects = new List<Terrain>();
            _animationObjects = new List<AnimatedObject>();
            _enemies = new List<Enemy>();
            _dmgBoxes = new List<Rectangle>();
            _winFlags = new List<Flag>();


            _amountOfLevels = 2;
            laddaFil = new StreamReader("terräng1.txt");

            //spelares listor av textures- behövs en för varje animation
            idleTextures = new List<Texture2D>();
            walkTextures = new List<Texture2D>();
            jumpTextures = new List<Texture2D>();
            dashTextures = new List<Texture2D>();
            poundTextures = new List<Texture2D>();
            torchTextures = new List<Texture2D>();
            flagTextures = new List<Texture2D>();
            takeDmgTextures = new List<Texture2D>();
            stunTextures = new List<Texture2D>();
            //steves lista av texture
            steveIdleTextures = new List<Texture2D>();
            steveWalkTextures = new List<Texture2D>();
            steveStrikeTextures = new List<Texture2D>();
            //kaibas
            kaibaAttackTextures = new List<Texture2D>();
            kaibaIdleTextures = new List<Texture2D>();
            //variation i bricks/wood
            brickTextures = new List<Texture2D>();
            woodTextures = new List<Texture2D>();
            //lista av animationer för att importera i enemyklassen. 
            playerAnimations = new List<Animation>();
            minecraftAnimations = new List<Animation>();
            kaibaAnimations = new List<Animation>();

            _lvl = 0;

            rnd = new Random();

            timeScores = new List<TimerScore>() ;
            for(int i = 0; i < _amountOfLevels; i++)
            {
                TimerScore temp = new TimerScore(5);
                timeScores.Add(temp);
            }

            mmTextures = new List<Texture2D>();
            mMenuItems = new List<MenuItem>();
            mainMenu = new Menu(mMenuItems);


        }

        public static void LoadContent(ContentManager Content, GameWindow window)
        {
            timeScores[0].LoadFromFile("highscore0.txt");
            timeScores[1].LoadFromFile("highscore1.txt");
            idleTextures.Add(Content.Load<Texture2D>("player/movement/idle1"));
            idleTextures.Add(Content.Load<Texture2D>("player/movement/idle2"));
            idleTextures.Add(Content.Load<Texture2D>("player/movement/idle3"));
            idleTextures.Add(Content.Load<Texture2D>("player/movement/idle4"));
            idleTextures.Add(Content.Load<Texture2D>("player/movement/idle5"));

            jumpTextures.Add(Content.Load<Texture2D>("player/movement/jump1"));
            jumpTextures.Add(Content.Load<Texture2D>("player/movement/jump2"));
            jumpTextures.Add(Content.Load<Texture2D>("player/movement/jump3"));
            jumpTextures.Add(Content.Load<Texture2D>("player/movement/jump4"));

            walkTextures.Add(Content.Load<Texture2D>("player/movement/walk1"));
            walkTextures.Add(Content.Load<Texture2D>("player/movement/walk2"));
            walkTextures.Add(Content.Load<Texture2D>("player/movement/walk3"));
            walkTextures.Add(Content.Load<Texture2D>("player/movement/walk4"));
            walkTextures.Add(Content.Load<Texture2D>("player/movement/walk5"));
            walkTextures.Add(Content.Load<Texture2D>("player/movement/walk6"));
            walkTextures.Add(Content.Load<Texture2D>("player/movement/walk7"));

            dashTextures.Add(Content.Load<Texture2D>("player/specials/dash1"));
            dashTextures.Add(Content.Load<Texture2D>("player/specials/dash2"));
            dashTextures.Add(Content.Load<Texture2D>("player/specials/dash3"));
            dashTextures.Add(Content.Load<Texture2D>("player/specials/dash4"));
            dashTextures.Add(Content.Load<Texture2D>("player/specials/dash5"));
            dashTextures.Add(Content.Load<Texture2D>("player/specials/dash6"));
            dashTextures.Add(Content.Load<Texture2D>("player/specials/dash7"));
            dashTextures.Add(Content.Load<Texture2D>("player/specials/dash8"));
            dashTextures.Add(Content.Load<Texture2D>("player/specials/dash9"));

            stunTextures.Add(Content.Load<Texture2D>("player/stun/stun1"));
            stunTextures.Add(Content.Load<Texture2D>("player/stun/stun2"));
            stunTextures.Add(Content.Load<Texture2D>("player/stun/stun3"));

            takeDmgTextures.Add(Content.Load<Texture2D>("player/takedamage/takedmg1"));
            takeDmgTextures.Add(Content.Load<Texture2D>("player/takedamage/takedmg2"));
            takeDmgTextures.Add(Content.Load<Texture2D>("player/takedamage/takedmg3"));
            takeDmgTextures.Add(Content.Load<Texture2D>("player/takedamage/takedmg4"));
            takeDmgTextures.Add(Content.Load<Texture2D>("player/takedamage/takedmg5"));
            takeDmgTextures.Add(Content.Load<Texture2D>("player/takedamage/takedmg6"));
            takeDmgTextures.Add(Content.Load<Texture2D>("player/takedamage/takedmg7"));
            takeDmgTextures.Add(Content.Load<Texture2D>("player/takedamage/takedmg8"));

            poundTextures.Add(Content.Load<Texture2D>("player/pound/pound1"));
            poundTextures.Add(Content.Load<Texture2D>("player/pound/pound2"));
            poundTextures.Add(Content.Load<Texture2D>("player/pound/pound3"));

            torchTextures.Add(Content.Load<Texture2D>("terrain/torchSprites/torch"));
            torchTextures.Add(Content.Load<Texture2D>("terrain/torchSprites/torch2"));
            torchTextures.Add(Content.Load<Texture2D>("terrain/torchSprites/torch3"));
            torchTextures.Add(Content.Load<Texture2D>("terrain/torchSprites/torch4"));
            torchTextures.Add(Content.Load<Texture2D>("terrain/torchSprites/torch5"));

            flagTextures.Add(Content.Load<Texture2D>("terrain/flag1"));
            flagTextures.Add(Content.Load<Texture2D>("terrain/flag2"));

            steveIdleTextures.Add(Content.Load<Texture2D>("enemies/minecraft/idle1"));
            steveIdleTextures.Add(Content.Load<Texture2D>("enemies/minecraft/idle2"));
            steveIdleTextures.Add(Content.Load<Texture2D>("enemies/minecraft/idle3"));
            steveIdleTextures.Add(Content.Load<Texture2D>("enemies/minecraft/idle4"));

            steveWalkTextures.Add(Content.Load<Texture2D>("enemies/minecraft/walk1"));
            steveWalkTextures.Add(Content.Load<Texture2D>("enemies/minecraft/walk2"));
            steveWalkTextures.Add(Content.Load<Texture2D>("enemies/minecraft/walk3"));
            steveWalkTextures.Add(Content.Load<Texture2D>("enemies/minecraft/walk4"));
            steveWalkTextures.Add(Content.Load<Texture2D>("enemies/minecraft/walk5"));
            steveWalkTextures.Add(Content.Load<Texture2D>("enemies/minecraft/walk6"));
            steveWalkTextures.Add(Content.Load<Texture2D>("enemies/minecraft/walk7"));

            steveStrikeTextures.Add(Content.Load<Texture2D>("enemies/minecraft/attack/strike2"));
            steveStrikeTextures.Add(Content.Load<Texture2D>("enemies/minecraft/attack/strike3"));
            steveStrikeTextures.Add(Content.Load<Texture2D>("enemies/minecraft/attack/strike4"));
            steveStrikeTextures.Add(Content.Load<Texture2D>("enemies/minecraft/attack/strike5"));

            kaibaAttackTextures.Add(Content.Load<Texture2D>("enemies/kaiba/attack1"));
            kaibaAttackTextures.Add(Content.Load<Texture2D>("enemies/kaiba/attack2"));
            kaibaAttackTextures.Add(Content.Load<Texture2D>("enemies/kaiba/attack3"));
            kaibaAttackTextures.Add(Content.Load<Texture2D>("enemies/kaiba/attack4"));
            kaibaAttackTextures.Add(Content.Load<Texture2D>("enemies/kaiba/card"));

            kaibaIdleTextures.Add(Content.Load<Texture2D>("enemies/kaiba/idle1"));
            kaibaIdleTextures.Add(Content.Load<Texture2D>("enemies/kaiba/idle2"));



            animIdle = new Animation(idleTextures, 5, true, 0.4f);
            playerAnimations.Add(animIdle);

            animJump = new Animation(jumpTextures, 4, false, 0.2f);
            playerAnimations.Add(animJump);

            animWalk = new Animation(walkTextures, 7, true, 0.2f);
            playerAnimations.Add(animWalk);

            animDash = new Animation(dashTextures, 9, true, 0.2f);
            playerAnimations.Add(animDash);

            animDmg = new Animation(takeDmgTextures, 8, false, 0.3f);
            playerAnimations.Add(animDmg);

            animStun = new Animation(stunTextures, 3, true, 0.4f);
            playerAnimations.Add(animStun);

            animPound = new Animation(poundTextures, 3, false, 0.2f);
            playerAnimations.Add(animPound);

            animTorch = new Animation(torchTextures, 5, true, 0.15f);

            animFlag = new Animation(flagTextures, 2, true, 0.15f);

            //mMenu grejor

            mmTextures.Add(Content.Load<Texture2D>("menu/start"));
            mmTextures.Add(Content.Load<Texture2D>("menu/highscore"));
            mmTextures.Add(Content.Load<Texture2D>("menu/exit"));

            float halfWidth = Game1.ScreenWidth / 2;
            mMenuItems.Add(new MenuItem(mmTextures[0], new Vector2(halfWidth - 193, 210f), State.Run));
            mMenuItems.Add(new MenuItem(mmTextures[1], new Vector2(halfWidth - 193, 280f), State.HighScore));
            mMenuItems.Add(new MenuItem(mmTextures[2], new Vector2(halfWidth - 193, 350f), State.Quit));


            /*Fienders animationer!
             * minecraft
             * 
             */

            minecraftIdle = new Animation(steveIdleTextures, 4, true, 0.2f);
            minecraftAnimations.Add(minecraftIdle);
            minecraftWalk = new Animation(steveWalkTextures, 7, true, 0.2f);
            minecraftAnimations.Add(minecraftWalk);
            minecraftStrike = new Animation(steveStrikeTextures, 4, true, 0.2f);
            minecraftAnimations.Add(minecraftStrike);


            //kaiba

            kaibaIdle = new Animation(kaibaIdleTextures, 2, true, 0.5f);
            kaibaAnimations.Add(kaibaIdle);
            kaibaAttack = new Animation(kaibaAttackTextures, 4, true, 0.4f);
            kaibaAnimations.Add(kaibaAttack);


            //terrängtextures/animationer
            spikeSprite = Content.Load<Texture2D>("terrain/spike");

            menuSprite = Content.Load<Texture2D>("menu/startscreen");

            brickTextures.Add(Content.Load<Texture2D>("terrain/brick/brick1"));
            brickTextures.Add(Content.Load<Texture2D>("terrain/brick/brick2"));
            brickTextures.Add(Content.Load<Texture2D>("terrain/brick/brick3"));
            brickTextures.Add(Content.Load<Texture2D>("terrain/brick/brick4"));
            brickTextures.Add(Content.Load<Texture2D>("terrain/brick/bricktop")); //index 4 för top-bricken. 

            woodTextures.Add(Content.Load<Texture2D>("terrain/wood/wood1"));
            woodTextures.Add(Content.Load<Texture2D>("terrain/wood/wood2"));
            woodTextures.Add(Content.Load<Texture2D>("terrain/wood/wood3"));



            font = Content.Load<SpriteFont>("ArcadeClassic");

            background = new Background(Content.Load<Texture2D>("background/background"), window);

            player = new Player(playerAnimations, Content.Load<Texture2D>("player/idle"), Content.Load<Texture2D>("player/heart"), 120, 200, 4.0f, 4.0f);
            Reset(_lvl); //settar upp för spelet

        }


        public static State RunUpdate(GameWindow window, GameTime gameTime)
        {
            _dmgBoxes.Clear();



            foreach (AnimatedObject t in _animationObjects)
            {
                t.Update(window, gameTime);
            }

            foreach(Flag f in _winFlags.ToList())
            {
                if (f.Rectangle.Intersects(player.Rectangle)){
                    gameState = State.Win;


                }
            }

            foreach (Terrain t in _collisionObjects.ToList())
            {
                if (t.IsAlive == false)
                {
                    _collisionObjects.Remove(t);
                }
                t.Update(window, gameTime);
            }

            foreach (Enemy e in _enemies.ToList())
            {
                if (e.IsAlive == false)
                {
                    _enemies.Remove(e);
                }
                e.Update(window, gameTime, player.Rectangle);
            }

            player.Update(gameTime, _collisionObjects, playerAnimations, _dmgBoxes, _enemies);
            background.Update(window, player.Y);


            if (player.IsAlive == false)
            {
                _lvl = 0;
                Reset(_lvl);
               
                return
                    State.Menu;

            }
            else
                return
                    gameState;
        }
        public static void RunDraw(SpriteBatch _spriteBatch)
        {
            background.Draw(_spriteBatch);

            foreach (PhysicalObject t in _collisionObjects)
                t.Draw(_spriteBatch);
            foreach (AnimatedObject t in _animationObjects)
                t.Draw(_spriteBatch);
            foreach (Enemy e in _enemies.ToList())
                e.Draw(_spriteBatch);
            player.Draw(_spriteBatch);
        }


        public static void HighScoreUpdate(GameTime time, GameWindow window)
        {
            background.Update(window, player.Y);

            if (gameState == State.Win)
            {
                if (timeScores[_lvl].EnterUpdate(time, player.playTime / 1000))
                {
                    timeScores[_lvl].SaveToFile("highscore" + _lvl + ".txt");

                    Reset(++_lvl);
                    if (_lvl > 1)
                        _lvl = 0;
                    gameState = State.Run;

                }
            }
            else gameState = State.HighScore;

            KeyboardState kbstate = Keyboard.GetState();
            if (kbstate.IsKeyDown(Keys.Escape)) gameState = State.Menu;

        }
        public static void HighScoreDraw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);

            if (gameState == State.Win) {
                Vector2 position = new Vector2(100, player.Y - 160);
                timeScores[_lvl].EnterDraw(spriteBatch, font, position);


            }
            else
            {
                int j = 0;
                foreach(TimerScore t in timeScores)
                {
                    Vector2 position = new Vector2(50 + 400 * j++, player.Y-160);
                    t.PrintDraw(spriteBatch, font, position, timeScores.IndexOf(t));

                }
            }
        }

        public static void MenuDraw(SpriteBatch spriteBatch, GraphicsDevice graphics)
        {
            background.Draw(spriteBatch);
            spriteBatch.Draw(menuSprite,graphics.Viewport.Bounds, Color.White);

            mainMenu.Draw(spriteBatch);

        }
        public static void MenuUpdate(GameWindow window, GameTime time)
        {
            background.Update(window, 100);
            mainMenu.Update(time);
        }

        public static void Reset(int level)
        {
            switch (level)
            {
                case 0:
                    laddaFil = new StreamReader("terräng0.txt");

                    break;

                case 1:
                    laddaFil = new StreamReader("terräng1.txt");
                    break;
                default:
                    laddaFil = new StreamReader("terräng1.txt");
                    break;
            }


            string lvlString;
            string[,] listAvObjekt = new string[1000, 20];
            string[] listaAvStrings;
            int yKoord = 0; // y koordinat för loading
            int xKoord = 0; // x koordinat för loading

            player.Reset();

                _animationObjects.Clear();
            _winFlags.Clear();
                _collisionObjects.Clear();
                _enemies.Clear();
            while ((lvlString = laddaFil.ReadLine()) != null)
            {



                listaAvStrings = lvlString.Split(',');

                yKoord++;
                xKoord = 0;
                foreach (string s in listaAvStrings)
                {

                    listAvObjekt[yKoord, xKoord] = s; //sparar en lista som kan jämföras med senare
                    bool luftOvanför = !(listAvObjekt[yKoord - 1, xKoord] == "1");
                    switch (s[0])
                    {
                        case '0':

                            break;

                        case 'P'://spelarens startposition
                            player.SetX = 48 * xKoord;
                            player.SetY = 48 * yKoord;
                            break;
                        case '1'://brick block
                            int rndBrick = rnd.Next(0, 3); //gör en random brick texture utav 4 möjliga
                            _collisionObjects.Add(new Terrain(brickTextures[rndBrick], 48 * xKoord, 48 * yKoord, false));
                            if (luftOvanför)
                                _collisionObjects.Add(new Terrain(brickTextures[4], 48 * xKoord, 48 * yKoord, false)); //lägger till en överdel ifall den ser att det inte är en brick ovanför
                            break;

                        case '2':
                            int rndWood = rnd.Next(0, 2); //gör en random brick texture utav 4 möjliga
                            _collisionObjects.Add(new Terrain(woodTextures[rndWood], 48 * xKoord, 48 * yKoord, true));
                            break;
                        case '3'://spik terräng
                            _collisionObjects.Add(new Spike(spikeSprite, 48 * xKoord, 48 * yKoord + 16)); //+16 är eftersom spikarna är kortare än ett vanligt block
                            break;
                        case '4': //animerad fackla
                            _animationObjects.Add(new AnimatedObject(torchTextures[0], animTorch.Clone(), 48 * xKoord + 16, 48 * yKoord, 0, 0)); ;//offset eftersom att facklan är mycket smalare än blocks
                            break;
                        case '5': // fiende 1: Minecraft/Steve
                            Minecraft mc = new Minecraft(steveIdleTextures[0], minecraftIdle, 48 * xKoord + 8, 48 * yKoord - 20, 0.7f, 0f, minecraftAnimations.ConvertAll(anim => anim.Clone()));
                            _enemies.Add(mc);
                            break;

                        case '6': // fiende 2: Kaiba

                            Kaiba kb = new Kaiba(kaibaIdleTextures[0], kaibaIdle, 48 * xKoord + 12, 48 * yKoord - 4, 0f, 0f, kaibaAnimations.ConvertAll(anim => anim.Clone()), kaibaAttackTextures[4]);
                            _enemies.Add(kb);
                            break;
                        case '7':
                            Flag fg = new Flag(flagTextures[0], animFlag, 48 * xKoord-10, 48 * yKoord);
                            _animationObjects.Add(fg);
                            _winFlags.Add(fg);
                            break;
                    }
                    xKoord++;

                }
            }


            laddaFil.Close();
        }
    }
}
