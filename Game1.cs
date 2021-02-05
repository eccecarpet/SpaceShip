using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Spaceship
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D ship_Sprite;
        Texture2D asteroid_Sprite;
        Texture2D space_Sprite;

        SpriteFont gameFont;
        SpriteFont timerFont;

        Ship player = new Ship();
       

        Controller gameController = new Controller();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ship_Sprite = Content.Load<Texture2D>("ship");
            asteroid_Sprite = Content.Load<Texture2D>("asteroid");
            space_Sprite = Content.Load<Texture2D>("space");

            gameFont = Content.Load<SpriteFont>("spaceFont");
            timerFont = Content.Load<SpriteFont>("TimerFont");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.shipUpdate(gameTime, gameController);
            gameController.conUpdate(gameTime);

            for (int i = 0; i < gameController.asteroids.Count; i++)
            {
                gameController.asteroids[i].asteroidUpdate(gameTime);

                if (gameController.asteroids[i].position.X < (0 - gameController.asteroids[i].radius))
                {
                    gameController.asteroids[i].offScreen = true;
                }


                int sum = gameController.asteroids[i].radius + 30;  // radius of asteroidsbox + radius of shipbox
                
                if (Vector2.Distance(gameController.asteroids[i].position, player.position) < sum)
                {
                    gameController.inGame = false;
                    player.position = Ship.defaultPosition;
                    i = gameController.asteroids.Count; // exit loop
                    gameController.asteroids.Clear(); //clear asteroids when you crash and back to menu
                }
            }

            gameController.asteroids.RemoveAll(a => a.offScreen);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(space_Sprite, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(ship_Sprite, new Vector2 (player.position.X -34, player.position.Y-50), Color.White);

            if(gameController.inGame == false)
            {
                string menuMessage = "Press Enter to Begin!";
                Vector2 sizeOfText = gameFont.MeasureString(menuMessage); //size menu
                _spriteBatch.DrawString(gameFont, menuMessage, new Vector2(640 - (sizeOfText.X / 2), 200), Color.White);
            }

            for (int i = 0; i < gameController.asteroids.Count; i++)
            {
                Vector2 temPos = gameController.asteroids[i].position;
                int tempRadius = gameController.asteroids[i].radius;
                _spriteBatch.Draw(asteroid_Sprite, new Vector2(temPos.X - tempRadius, temPos.Y - tempRadius), Color.White);
            }


            _spriteBatch.DrawString(timerFont, "Time :  " + Math.Floor(gameController.totalTime).ToString(), new Vector2(3,3), Color.White);
                _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
