using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Solar_System_Warfare.Spites;
using System.Diagnostics;

namespace Solar_System_Warfare
{
    public class SolarSystemWarfareGame : Game
    {
        private ShipPool shipPool;
        private ScrollingBackground scrollingBackground;
        private Menu menu;
        private Song song;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public SolarSystemWarfareGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
                        
            // TODO: use this.Content to load your game content here
           
            _graphics.PreferredBackBufferHeight = 800;
            _graphics.PreferredBackBufferWidth = 600;
            _graphics.ApplyChanges();

            LoadImageAssets();

            shipPool = new ShipPool(InitEarth());
            scrollingBackground = new ScrollingBackground();
            menu = new Menu();
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            scrollingBackground.Scroll(gameTime);

            
            
            if (shipPool.EarthAlive)
            {
                var kState = Keyboard.GetState();
                shipPool.Update(gameTime, Content, kState, GamePad.GetState(PlayerIndex.One).ThumbSticks.Left);
            }
            else
            {
                /*if(kState.IsKeyDown(Keys.Space))
                {
                    shipPool = new ShipPool(InitEarth());
                }*/
                var buttonPressed = menu.Update(Mouse.GetState(), gameTime);
                switch(buttonPressed)
                {
                    case ButtonPressed.Start:
                        shipPool.Reset();
                        break;
                }

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            scrollingBackground.Draw(_spriteBatch);

            if (shipPool.EarthAlive)
            {
                shipPool.Draw(_spriteBatch);
                shipPool.Lives.Draw(_spriteBatch);
                shipPool.ScoreUpdater.Draw(_spriteBatch);
            }
            else
            {
                menu.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private Earth InitEarth()
        {
            return new Earth(
                new Vector2(300, 600),
                400,
                3);
        }

        private void LoadImageAssets()
        {
            Assets.Earth = Content.Load<Texture2D>("EarthBattleShip");
            Assets.EarthProjectile = Content.Load<Texture2D>("ProjectileEarth");
            Assets.Enemy = Content.Load<Texture2D>("EnemyShip");
            Assets.EnemyProjectile = Content.Load<Texture2D>("Projectile");
            Assets.Background = Content.Load<Texture2D>("spaceBackground");
            Assets.FullHeart = Content.Load<Texture2D>("fullHeart");
            Assets.EmptyHeart = Content.Load<Texture2D>("emptyHeart");
            Assets.Font = Content.Load<SpriteFont>("Score");
            Assets.BackButton = Content.Load<Texture2D>("backButton");
            Assets.CreditsButton = Content.Load<Texture2D>("creditsButton");
            Assets.StartButton = Content.Load<Texture2D>("startButton");
            song = Content.Load<Song>("TheLift");
        }
    }
}
