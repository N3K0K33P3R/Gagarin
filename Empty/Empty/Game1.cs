using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using KeyboardInput = MonoFlash.Engine.KeyboardInput;

namespace Empty
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		private readonly GraphicsDeviceManager graphics;
		private          Main                  main;
		private          SpriteBatch           spriteBatch;

		public Game1()
		{
			graphics              = new GraphicsDeviceManager(this) { PreferredBackBufferWidth = 1600, PreferredBackBufferHeight = 800 };
			IsMouseVisible        = true;
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

			base.Initialize();
			KeyboardInput.Initialize(this, 500f, 20);

			main = new Main(GraphicsDevice);
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		public static SpriteFont fontForProperties;

		public static Texture2D stoneTexture;
		public static Texture2D woodTexture;
		public static Texture2D ironTexture;

		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			Assets.font = Content.Load<SpriteFont>("font");


            fontForProperties = Content.Load<SpriteFont>("fontForProperties");
            stoneTexture = Content.Load<Texture2D>("stone");
            woodTexture = Content.Load<Texture2D>("wood");
            ironTexture = Content.Load<Texture2D>("iron");
			Assets.textures["Human"] = Content.Load<Texture2D>("Pers/human");

            Assets.textures.Add("Grass", Content.Load<Texture2D>("Tile/Grass"));
            Assets.textures.Add("Sand", Content.Load<Texture2D>("Tile/Sand"));
            Assets.textures.Add("Stone", Content.Load<Texture2D>("Tile/Stone"));
            // TODO: use this.Content to load your game content here
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
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
				Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

			KeyboardInput.Update();
			main.Update(0);
			// TODO: Add your update logic here

			base.Update(gameTime);
		}


        Color sky = new Color(49, 47, 47);

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(sky);

			main.Draw(spriteBatch);
			// TODO: Add your drawing code here
			base.Draw(gameTime);
		}
	}
}