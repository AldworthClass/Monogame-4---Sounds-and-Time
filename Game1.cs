using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Monogame_4___Sounds_and_Time
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        MouseState mouseState;

        SpriteFont timeFont;
        
        Texture2D bombTexture;
        Rectangle bombRect;

        SoundEffect explode;

        float seconds;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "Lesson 4 - Sound and Time";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 800;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 500;   // set this value to the desired height of your window
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            bombRect = new Rectangle(50, 50, 700, 400);
            //startTime = 0;
            seconds = 0;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bombTexture = Content.Load<Texture2D>("bomb");
            timeFont = Content.Load<SpriteFont>("Time");
            explode = Content.Load<SoundEffect>("explosion");
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Calculates elapsed time since the last timestamp
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Resets bomb timer when the left mouse button is clicked
            if (mouseState.LeftButton == ButtonState.Pressed)
                seconds = 0f; 

            // Plays explosion if bomb has not been reset before 10 seconds is up
            if (seconds >= 10)
            {
                explode.Play();
                seconds = 0f;  // Resets the timer
            }
                
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(bombTexture, bombRect, Color.White);
            // (10 - seconds) will display the timer as counting down from 10
            _spriteBatch.DrawString(timeFont, (10 - seconds).ToString("00.0"), new Vector2(270, 200), Color.Black); 
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
