#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
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
		public SpriteBatch HUDBatch { get; private set; }

		public Texture2D Map { get; private set;}
		public Texture2D Character { get; private set;}
		public Texture2D MapOverlay { get; private set; }
        public SpriteFont MainFont { get; private set; }
		public Core.Game GameModel { get; private set;}
		public int Seed { get; private set;}
		public int SquareSize { get; private set;}
		public int TileSize { get; private set;}


		private const int cameraAcceleration = 600;
		private const float controllerMinMovement = 0.2f;

		private List<SoundEffectInstance> soundtracks;
		private int selectedSong;

		Camera camera;

		MapShow mapShow;
		UnitShow unitShow;
		OverlayShow overlayShow;
        HudShow hudShow;

		public Core.Unit SelectedUnit { get; private set;}
		public Core.Tile SelectedTile { get; private set; }
		public IEnumerator<Core.Unit> EnumeratorUnit { get; private set;}
		public Core.Player CurrentPlayer { get; private set;}

		GraphicsDeviceManager graphics;
		KeyboardState oldKeyboardState;
		GamePadState oldGamepadState;

		public Game1 (Core.Game g, int seed)
		{
			GameModel = g;
			this.Seed = seed;
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";	            
			graphics.IsFullScreen = false;		
			Window.AllowUserResizing = true;
			graphics.PreferredBackBufferWidth = 1920;
			graphics.PreferredBackBufferHeight = 1080;
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
			TileSize = SquareSize * 3;

			this.IsMouseVisible = true;
			camera = new Camera(graphics.GraphicsDevice);
			camera.MaximumZoom = 2f;
			camera.MinimumZoom = 0.5f;

			mapShow = new MapShow (this);
			unitShow = new UnitShow (this);
			overlayShow = new OverlayShow (this);
            hudShow = new HudShow(this);

			SelectedUnit = null;
			SelectedTile = GameModel.Map.Tiles [0];
			CurrentPlayer = GameModel.Players [0];

			EnumeratorUnit = CurrentPlayer.Units.GetEnumerator ();

			// TODO: Add your initialization logic here
			base.Initialize ();
				
		}

		protected override void BeginRun()
		{
			soundtracks[selectedSong].IsLooped = true;
			soundtracks [selectedSong].Play ();
			oldKeyboardState = Keyboard.GetState ();
			oldGamepadState = GamePad.GetState (PlayerIndex.One);
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
            HUDBatch = new SpriteBatch(GraphicsDevice);

			Map = Content.Load<Texture2D> ("map");
			Character = Content.Load<Texture2D> ("character");
			MapOverlay = Content.Load<Texture2D> ("overlaytile");

            MainFont = Content.Load<SpriteFont>("kenpixel_mini_square");

			soundtracks = new List<SoundEffectInstance> ();
			soundtracks.Add(Content.Load<SoundEffect> ("soundtrack1").CreateInstance());
			soundtracks.Add(Content.Load<SoundEffect> ("soundtrack2").CreateInstance());
			soundtracks.Add(Content.Load<SoundEffect> ("soundtrack3").CreateInstance());

			Random rnd = new Random ();
			selectedSong = rnd.Next (0, 3);


			//TODO: use this.Content to load your game content here 
		}

        protected override void UnloadContent()
        {
            base.UnloadContent();

            foreach (SoundEffectInstance s in soundtracks)
            {
                s.Dispose();
            }

            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update (GameTime gameTime)
		{
			var deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;

			GamePadState currentGamepadState = GamePad.GetState (PlayerIndex.One);
			KeyboardState currentKeyboardState = Keyboard.GetState();

			// Camera Zoom
			if (currentKeyboardState.IsKeyDown (Keys.Q) || currentGamepadState.Buttons.X == ButtonState.Pressed)
				camera.ZoomIn(deltaTime * camera.Zoom);

			if (currentKeyboardState.IsKeyDown (Keys.Z) || currentGamepadState.Buttons.Y == ButtonState.Pressed)
				camera.ZoomOut(deltaTime * camera.Zoom);

			//SelectedTile
			Vector2 currentStick = currentGamepadState.ThumbSticks.Left;
			Vector2 oldStick = oldGamepadState.ThumbSticks.Left;

			if (oldKeyboardState.IsKeyUp (Keys.W) && currentKeyboardState.IsKeyDown (Keys.W) || oldStick.Y > -controllerMinMovement && currentStick.Y < -controllerMinMovement) {
				var NeighbourgTile = SelectedTile.GetNeighbor (new Tile.Diff (0, -1));
				TryToMoveSelectedTile (NeighbourgTile);
			}

			if (oldKeyboardState.IsKeyUp (Keys.S) && currentKeyboardState.IsKeyDown (Keys.S) || oldStick.Y < controllerMinMovement && currentStick.Y > controllerMinMovement) {
				var NeighbourgTile = SelectedTile.GetNeighbor (new Tile.Diff (0, 1));
				TryToMoveSelectedTile (NeighbourgTile);
			}

			if (oldKeyboardState.IsKeyUp (Keys.A) && currentKeyboardState.IsKeyDown (Keys.A) || oldStick.X > -controllerMinMovement && currentStick.X < -controllerMinMovement) {
				var NeighbourgTile = SelectedTile.GetNeighbor (new Tile.Diff (-1, 0));
				TryToMoveSelectedTile (NeighbourgTile);
			}

			if (oldKeyboardState.IsKeyUp (Keys.D) && currentKeyboardState.IsKeyDown (Keys.D) || oldStick.X < controllerMinMovement && currentStick.X > controllerMinMovement) {
				var NeighbourgTile = SelectedTile.GetNeighbor (new Tile.Diff (1, 0));
				TryToMoveSelectedTile (NeighbourgTile);
			}

			//Move unit

			//Unselect Unit
			if (currentKeyboardState.IsKeyDown (Keys.R) || currentGamepadState.Buttons.B == ButtonState.Pressed) {
				SelectedUnit = null;
			}

			//Action : Select or move unit
			if (currentKeyboardState.IsKeyDown (Keys.E) || currentGamepadState.Buttons.A == ButtonState.Pressed) {
				TryToMoveUnit ();
				TryToSelectUnit ();
			}

			if (oldKeyboardState.IsKeyDown(Keys.F) && currentKeyboardState.IsKeyUp(Keys.F) 
				|| oldGamepadState.Buttons.RightShoulder == ButtonState.Released && currentGamepadState.Buttons.RightShoulder == ButtonState.Pressed) {
				if (!EnumeratorUnit.MoveNext ()) {
					EnumeratorUnit.Reset ();
					EnumeratorUnit.MoveNext ();
				}
				SelectedUnit = EnumeratorUnit.Current;
				SelectedTile = SelectedUnit.Tile;
			}

            //Action: Next turn
            if (oldKeyboardState.IsKeyUp(Keys.Enter) && currentKeyboardState.IsKeyDown(Keys.Enter))
            {
                NextTurn();
            }

			Vector2 TriggerMove = new Vector2 (TileSize, TileSize);
			Vector2 StepMove = new Vector2 (TileSize, TileSize);
			camera.Adjust (mapShow.MapToScreen (SelectedTile.Point), TriggerMove, StepMove);

			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
				Keyboard.GetState ().IsKeyDown (Keys.Escape)) {
				Exit ();
			}
			#endif

			oldKeyboardState = currentKeyboardState;
			oldGamepadState = currentGamepadState;

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

            HUDBatch.Begin();
            hudShow.Blit();
            HUDBatch.End();

		}

		protected void TryToMoveUnit() {
			if (SelectedUnit != null) {
				List<Core.Move> moves;
				SelectedUnit.MovePossibilites.TryGetValue(SelectedTile, out moves);
				if (moves != null) {
					foreach (Move m in moves) {
						m.Execute ();
						GameModel.Actions.Push (m); // The stack will have to be managed by the GUI
					}
					SelectedTile = SelectedUnit.Tile;
				}
		}
		}

		protected void TryToSelectUnit() {
			if (SelectedTile.Units.Count > 0) {
				SelectedUnit = SelectedTile.Units.First();
			}
		}
		protected void TryToMoveSelectedTile(Core.Tile NeighbourgTile) {
			if (NeighbourgTile != null) {
				SelectedTile = NeighbourgTile;
			}
		}
        protected void NextTurn()
        {
            try
            {
                GameModel.NextTurn();
            }
            catch (Core.Game.EndOfGameException e)
            {
                // TODO handle end of game
                this.Exit();
            }
            hudShow.RefreshDataCache();
        }
    }
}

