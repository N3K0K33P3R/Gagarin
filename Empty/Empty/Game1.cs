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
			graphics              = new GraphicsDeviceManager(this);
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
			graphics.PreferredBackBufferWidth  = 1280;
			graphics.PreferredBackBufferHeight = 720;
			IsMouseVisible                     = true;
			KeyboardInput.Initialize(this, 500f, 20);

			main = new Main();
		}

        Planet planet;

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
            Assets.textures["sky0"] = Content.Load<Texture2D>("Skys/sky0");
            Assets.textures["sky1"] = Content.Load<Texture2D>("Skys/sky1");


            Assets.textures["desert"] = Content.Load<Texture2D>("Fons/desert");
            Assets.textures["fly"] = Content.Load<Texture2D>("Fons/fly");
            Assets.textures["mountincold"] = Content.Load<Texture2D>("Fons/mountincold");
            Assets.textures["mountin"] = Content.Load<Texture2D>("Fons/mountin");
            Assets.textures["fongrib"] = Content.Load<Texture2D>("Fons/fongrib");
            


            Assets.font = Content.Load<SpriteFont>("font");
            // TODO: use this.Content to load your game content here

            new Planet();
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
            Planet.CurrentPlanet.Update(gameTime);
			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			main.Draw(spriteBatch);
            // TODO: Add your drawing code here
            Planet.CurrentPlanet.Draw(spriteBatch);
			base.Draw(gameTime);
		}
	}
}