using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace L3ave.Drawing
{
    public class Window : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;

        private MusicPlayer musicPlayer;

        private SpriteBatch batch;

        private Drawer drawer;

        private GameLogic.Game game;

        public Window()
        {
            graphics = new GraphicsDeviceManager(this);

            base.Window.AllowUserResizing = true;
            base.Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            game = new GameLogic.Game();
            batch = new SpriteBatch(base.GraphicsDevice);
            drawer = new Drawer(base.Content, batch, game);

            base.Window.ClientSizeChanged += delegate
            {
                drawer.Resize(base.Window.ClientBounds);
            };

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();

            base.Initialize();

            drawer.Resize(base.Window.ClientBounds);
        }

        protected override void LoadContent()
        {
            musicPlayer = new MusicPlayer(base.Content);
            musicPlayer.PlayMusic("bgm");
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Escape) || game.Exited)
            {
                Exit();
            }
            if (state.IsKeyDown(Keys.W))
            {
                game.Jump();
            }
            if (state.IsKeyDown(Keys.A))
            {
                game.MoveLeft();
            }
            if (state.IsKeyDown(Keys.S))
            {
                game.MoveDown();
            }
            if (state.IsKeyDown(Keys.D))
            {
                game.MoveRight();
            }
            if (state.IsKeyDown(Keys.Space))
            {
                game.StartLightning();
            }
            if (state.IsKeyDown(Keys.N))
            {
                game.SwitchNoclip();
            }

            game.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.White);

            batch.Begin();
            drawer.Draw();
            batch.End();

            base.Draw(gameTime);
        }
    }
}
