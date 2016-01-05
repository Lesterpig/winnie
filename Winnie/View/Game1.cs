#region Using Statements
using System;
using System.Collections.Generic;
using Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

#endregion

namespace MGUI
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		public SpriteBatch WorldBatch { get; private set; }
		public Texture2D Map { get; private set;}
		public Texture2D Character { get; private set;}
		public Core.Game GameModel { get; private set;}
		public int Seed { get; private set;}
		public int SquareSize { get; private set;}

		private const int cameraAcceleration = 600;
		private const float controllerMinMovement = 0.2f;

		Camera camera;
		List<Blittable> WorldBlit;
		GraphicsDeviceManager graphics;


		public Game1 (Core.Game g, int seed)
		{
			GameModel = g;
			this.Seed = seed;
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
			SquareSize = 64;

			this.IsMouseVisible = true;
			camera = new Camera(graphics.GraphicsDevice);
			camera.MaximumZoom = 2f;
			camera.MinimumZoom = 0.5f;

			WorldBlit = new List<Blittable> ();
			WorldBlit.Add(new MapShow (this));
			WorldBlit.Add(new UnitShow (this));

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
			WorldBatch = new SpriteBatch (GraphicsDevice);
			Map = Content.Load<Texture2D> ("map");
			Character = Content.Load<Texture2D> ("character");

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
			if (keyboardState.IsKeyDown (Keys.Up) || gamepadState.ThumbSticks.Right.X < -controllerMinMovement)
				camera.MoveUp (deltaTime);

			if (keyboardState.IsKeyDown (Keys.Down) || gamepadState.ThumbSticks.Right.X > controllerMinMovement)
				camera.MoveDown (deltaTime);

			if (keyboardState.IsKeyDown (Keys.Left) || gamepadState.ThumbSticks.Right.Y > controllerMinMovement)
				camera.MoveLeft (deltaTime);

			if (keyboardState.IsKeyDown (Keys.Right) || gamepadState.ThumbSticks.Right.Y < -controllerMinMovement)
				camera.MoveRight (deltaTime);

			// Camera Zoom
			if (keyboardState.IsKeyDown (Keys.W) || gamepadState.DPad.Up == ButtonState.Pressed)
				camera.ZoomIn(deltaTime * camera.Zoom);

			if (keyboardState.IsKeyDown (Keys.S) || gamepadState.DPad.Down == ButtonState.Pressed)
				camera.ZoomOut(deltaTime * camera.Zoom);
				
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
			WorldBatch.Begin(transformMatrix: camera.GetViewMatrix());
			foreach(Blittable b in WorldBlit) {
				b.Blit ();
			}
			WorldBatch.End();

			base.Draw (gameTime);
		}
	}
}

