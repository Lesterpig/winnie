﻿#region Using Statements
using System;
using System.Collections.Generic;
using Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
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
		public SpriteBatch MapBatch { get; private set; }
		public SpriteBatch OverlayBatch { get; private set; }
		public SpriteBatch CharacterBatch { get; private set; }
		public SpriteBatch UIBatch { get; private set; }

		public Texture2D Map { get; private set;}
		public Texture2D Character { get; private set;}
		public Texture2D MapOverlay { get; private set; }
		public Core.Game GameModel { get; private set;}
		public int Seed { get; private set;}
		public int SquareSize { get; private set;}

		private const int cameraAcceleration = 600;
		private const float controllerMinMovement = 0.2f;

		private List<SoundEffectInstance> soundtracks;
		private int selectedSong;

		Camera camera;

		Blittable mapShow;
		Blittable unitShow;
		Blittable overlayShow;

		GraphicsDeviceManager graphics;


		public Game1 (Core.Game g, int seed)
		{
			GameModel = g;
			this.Seed = seed;
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";	            
			graphics.IsFullScreen = false;		
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

			mapShow = new MapShow (this);
			unitShow = new UnitShow (this);
			overlayShow = new OverlayShow (this);

			// TODO: Add your initialization logic here
			base.Initialize ();
				
		}

		protected override void BeginRun()
		{
			soundtracks[selectedSong].IsLooped = true;
			soundtracks [selectedSong].Play ();
			base.BeginRun();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			MapBatch = new SpriteBatch (GraphicsDevice);
			OverlayBatch = new SpriteBatch (GraphicsDevice);
			CharacterBatch = new SpriteBatch (GraphicsDevice);

			Map = Content.Load<Texture2D> ("map");
			Character = Content.Load<Texture2D> ("character");
			MapOverlay = Content.Load<Texture2D> ("overlaytile");

			soundtracks = new List<SoundEffectInstance> ();
			soundtracks.Add(Content.Load<SoundEffect> ("soundtrack1").CreateInstance());
			soundtracks.Add(Content.Load<SoundEffect> ("soundtrack2").CreateInstance());
			soundtracks.Add(Content.Load<SoundEffect> ("soundtrack3").CreateInstance());

			Random rnd = new Random ();
			selectedSong = rnd.Next (0, 3);


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
			DrawGame ();

			base.Draw (gameTime);
		}

		protected void DrawGame() 
		{
			graphics.GraphicsDevice.Clear (Color.Black);

			MapBatch.Begin(transformMatrix: camera.GetViewMatrix());
			mapShow.Blit ();
			MapBatch.End ();

			OverlayBatch.Begin (transformMatrix: camera.GetViewMatrix ());
			overlayShow.Blit ();
			OverlayBatch.End ();

			CharacterBatch.Begin (transformMatrix: camera.GetViewMatrix ());
			unitShow.Blit ();
			CharacterBatch.End ();

		}
	}
}
