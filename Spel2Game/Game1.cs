using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace Spel2Game
{
    public class Game1 : Game
    {

        //viktiga saker med _ före
        private GraphicsDeviceManager _graphicsManager;
        private SpriteBatch _spriteBatch;
        private static Camera _camera;

        Background background;



        public static int ScreenHeight;
        public static int ScreenWidth;
        

  
        public Game1()
        {
            _camera = new Camera();

            _graphicsManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here


            ScreenHeight = _graphicsManager.PreferredBackBufferHeight;
            ScreenWidth = _graphicsManager.PreferredBackBufferWidth;
            GameElements.gameState = GameElements.State.Menu;
            GameElements.Initialize();


            //test
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            background = new Background(Content.Load<Texture2D>("background/background"), Window);

            GameElements.LoadContent(Content, Window);

            // TODO: use this.Content to load your game content here
       
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            _camera.cameraFollow(GameElements.player);
            switch (GameElements.gameState)
            {
                case GameElements.State.Run:
                    GameElements.gameState = GameElements.RunUpdate(Window, gameTime);
                    break;
                case GameElements.State.HighScore:
                    GameElements.HighScoreUpdate(gameTime, Window);
                    break;
                case GameElements.State.Quit:
                    this.Exit();
                    break;
                case GameElements.State.Win:
                    GameElements.HighScoreUpdate(gameTime, Window);

                    break;
                default: //menu
                    GameElements.MenuUpdate(Window, gameTime);
                    break;


            }


            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(transformMatrix: _camera.Transform);//camera ändring efter Camera.cs
            switch (GameElements.gameState)
            {
                case GameElements.State.Run:
                    GameElements.RunDraw(_spriteBatch);
                    break;
                case GameElements.State.HighScore:
                    GameElements.HighScoreDraw(_spriteBatch);
                    break;
                case GameElements.State.Quit:
                    this.Exit();
                    break;
                case GameElements.State.Win:
                    GameElements.HighScoreDraw(_spriteBatch);
                    break;
                default:
                    GameElements.MenuDraw(_spriteBatch, _graphicsManager.GraphicsDevice);
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);

        }
    }
}
