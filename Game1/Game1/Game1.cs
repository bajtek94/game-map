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

        Sprite fire, fire1, fire2, fire3;
        Sprite stone;

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

            fire = new Sprite(Content.Load<Texture2D>("Asset01"), new Vector2(300, 500), new Rectangle(500, 0, 200, 300), 0.4f);
            fire1 = new Sprite(Content.Load<Texture2D>("Asset01"), new Vector2(400, 500), new Rectangle(500, 0, 200, 300), 0.4f);
            fire2 = new Sprite(Content.Load<Texture2D>("Asset01"), new Vector2(300, 400), new Rectangle(500, 0, 200, 300), 0.4f);
            fire3 = new Sprite(Content.Load<Texture2D>("Asset01"), new Vector2(400, 400), new Rectangle(500, 0, 200, 300), 0.4f);
            stone = new Sprite(Content.Load<Texture2D>("Asset01"), new Vector2(400, 100), new Rectangle(500, 300, 200, 100), 2f);

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
            fire.Update();
            var state = Keyboard.GetState();
            int speed = 200;
            var viewMatrix = camera.GetViewMatrix();
            int speedForSprites = (int)(speed * viewMatrix.Scale.X);

            if (state.IsKeyDown(Keys.A))
            {
                position.X -= speed * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }
            if (state.IsKeyDown(Keys.D))
            {
                position.X += speed * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }
            if (state.IsKeyDown(Keys.S))
            {
                position.Y += speed * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }
            if (state.IsKeyDown(Keys.W))
            {
                position.Y -= speed * gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            }

            fire.MoveUpdate(gameTime, speedForSprites);
            fire1.MoveUpdate(gameTime, speedForSprites);
            fire2.MoveUpdate(gameTime, speedForSprites);
            fire3.MoveUpdate(gameTime, speedForSprites);
            stone.MoveUpdate(gameTime, speedForSprites);

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

            //fire.Draw(spriteBatch);
            //fire1.Draw(spriteBatch);
            //fire2.Draw(spriteBatch);
            //fire3.Draw(spriteBatch);
            //stone.Draw(spriteBatch);

            playerSprite.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
            

        }

    }
}
