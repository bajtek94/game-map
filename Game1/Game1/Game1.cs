using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.ViewportAdapters;


namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Sprite playerSprite;

        private ViewportAdapter viewportAdapter;
        private TiledMap map;
        private TiledMapRenderer mapRenderer;
        private Camera2D camera;
        private Vector2 position;
        public int screenWidth { get; set; }
        public int screenHeight { get; set; }

        public Game1()
        {
            screenWidth = 1024;
            screenHeight = 768;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, screenWidth*2, screenHeight*2);
            mapRenderer = new TiledMapRenderer(GraphicsDevice);
            camera = new Camera2D(viewportAdapter);
            position = new Vector2(1200,900);
            base.Initialize();


        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            playerSprite = new Sprite(Content.Load<Texture2D>("enemy"), new Vector2((int)(screenWidth*0.5), (int)(screenHeight*0.5)), new Rectangle(0, 0, 100, 250), 0.4f);

            map = Content.Load<TiledMap>("map");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            playerSprite.Update();
            var state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.A))
            {
                position.X -= 400 * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }
            if (state.IsKeyDown(Keys.D))
            {
                position.X += 400 * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }
            if (state.IsKeyDown(Keys.S))
            {
                position.Y += 400 * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }
            if (state.IsKeyDown(Keys.W))
            {
                position.Y -= 400 * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }

            camera.LookAt(position);
            mapRenderer.Update(map, gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //////////////////////////////////////////////////////////


            /////////////////////////////////////////////////////////
            // TODO: Add your drawing code here
            spriteBatch.Begin();
            var viewMatrix = camera.GetViewMatrix();
            var projectionMatrix = Matrix.CreateOrthographicOffCenter(0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0, 0f, -1f);
            mapRenderer.Draw(map, viewMatrix, projectionMatrix, null);
            playerSprite.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
