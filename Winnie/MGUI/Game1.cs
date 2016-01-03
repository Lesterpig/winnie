#region Using Statements
using System;

using Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

#endregion

namespace MGUI
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		public SpriteBatch MapBatch { get; private set; }
		public Texture2D Map { get; private set;}
		public Core.Game GameModel { get; private set;}
		public int Seed { get; private set;}
		public int SquareSize { get; private set;}

		private const int cameraAcceleration = 350;
		private const float controllerMinMovement = 0.2f;

		Camera2D camera;
		MapShow ms;
		GraphicsDeviceManager graphics;


		public Game1 ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";	            
			graphics.IsFullScreen = true;		
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			Seed = 1341;
			SquareSize = 64;

			var p1 = new Player("Player A", Human.Instance);
			var p2 = new Player("Player B", Elf.Instance);
			GameModel = GameBuilder.New<StandardGameType, PerlinMap>(p1, p2, true, Seed);
			this.IsMouseVisible = true;
			camera = new Camera2D(GraphicsDevice.Viewport);

			ms = new MapShow (this);

			// TODO: Add your initialization logic here
			base.Initialize ();
				
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			MapBatch = new SpriteBatch (GraphicsDevice);
			Map = Content.Load<Texture2D> ("map");

			//TODO: use this.Content to load your game content here 
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			var deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

			var gamepadState = GamePad.GetState (PlayerIndex.One);
			var keyboardState = Keyboard.GetState();

			// Camera Movement
			if (keyboardState.IsKeyDown (Keys.Up) || gamepadState.ThumbSticks.Right.X < -controllerMinMovement) {
				camera.Position -= new Vector2 (0, cameraAcceleration) * deltaTime;
			}

			if (keyboardState.IsKeyDown(Keys.Down) || gamepadState.ThumbSticks.Right.X > controllerMinMovement)
				camera.Position += new Vector2(0, cameraAcceleration) * deltaTime;

			if (keyboardState.IsKeyDown(Keys.Left) || gamepadState.ThumbSticks.Right.Y > controllerMinMovement)
				camera.Position -= new Vector2(cameraAcceleration, 0) * deltaTime;

			if (keyboardState.IsKeyDown(Keys.Right) || gamepadState.ThumbSticks.Right.Y < -controllerMinMovement)
				camera.Position += new Vector2(cameraAcceleration, 0) * deltaTime;

			// Camera Zoom
			if (keyboardState.IsKeyDown (Keys.W) || gamepadState.Buttons.LeftShoulder == ButtonState.Pressed)
				camera.Zoom += deltaTime;

			if (keyboardState.IsKeyDown (Keys.S) || gamepadState.Buttons.RightShoulder == ButtonState.Pressed)
				camera.Zoom -= deltaTime;

			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
				Keyboard.GetState ().IsKeyDown (Keys.Escape)) {
				Exit ();
			}
			#endif


			// TODO: Add your update logic here			
			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.Black);

			//TODO: Add your drawing code here
			MapBatch.Begin(transformMatrix: camera.GetViewMatrix());
			ms.BlitMap ();
			MapBatch.End();

			base.Draw (gameTime);
		}
	}
}

