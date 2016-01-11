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
		public SpriteBatch HudBatch { get; private set; }

		public Texture2D Map { get; private set;}
		public Texture2D Character { get; private set;}
		public Texture2D MapOverlay { get; private set; }
        public Texture2D HudElements { get; private set; }
        public SpriteFont MainFont { get; private set; }
        public SpriteFont HugeFont { get; private set; }
		public Core.Game GameModel { get; private set;}
		public int Seed { get; private set;}
		public int SquareSize { get; private set;}
		public int TileSize { get; private set;}

		private const int cameraAcceleration = 600;
#if !OPENGL
        private const float controllerMinMovementY = -0.2f;
#else
        private const float controllerMinMovementY = 0.2f;
#endif
        private const float controllerMinMovementX = 0.2f;


        private List<SoundEffectInstance> soundtracks;
        private Dictionary<string, SoundEffectInstance> sfx;
		private int selectedSong;

#if !OPENGL
        private Dictionary<string, Keys> keyBinding = new Dictionary<string, Keys>() {
            { "ZOOM+", Keys.A },
            { "ZOOM-", Keys.W },
            { "UP",    Keys.Z },
            { "DOWN",  Keys.S },
            { "LEFT",  Keys.Q },
            { "RIGHT", Keys.D }
        };
#else
        private Dictionary<string, Keys> keyBinding = new Dictionary<string, Keys>() {
            { "ZOOM+", Keys.Q },
            { "ZOOM-", Keys.Z },
            { "UP",    Keys.W },
            { "DOWN",  Keys.S },
            { "LEFT",  Keys.A },
            { "RIGHT", Keys.D }
        };
#endif

        Camera camera;

		MapShow mapShow;
		UnitShow unitShow;
		OverlayShow overlayShow;
        HudShow hudShow;

		public Core.Unit SelectedUnit { get; private set;}
		public Core.Tile SelectedTile { get; private set; }
		public IEnumerator<Core.Unit> EnumeratorUnit { get; private set;}

		GraphicsDeviceManager graphics;
		KeyboardState oldKeyboardState;
		GamePadState oldGamepadState;
        bool controlsFrozen = false;

		public Game1 (Core.Game g, int seed)
		{
			GameModel = g;
			this.Seed = seed;
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";	            
			graphics.IsFullScreen = false;		
			Window.AllowUserResizing = true;
            Window.Title = "Small World";
            Window.Position = new Microsoft.Xna.Framework.Point(0,0);
            Window.IsBorderless = true;

            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics.ApplyChanges();
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

			SelectedTile = GameModel.Map.Tiles [0];
			EnumeratorUnit = GameModel.CurrentPlayer.Units.GetEnumerator ();
            SelectNextUnit();

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
            HudBatch = new SpriteBatch(GraphicsDevice);

			Map = Content.Load<Texture2D> ("map");
			Character = Content.Load<Texture2D> ("character");
			MapOverlay = Content.Load<Texture2D> ("overlaytile");
            HudElements = Content.Load<Texture2D>("hud_elements");

            MainFont = Content.Load<SpriteFont>("kenpixel_mini_square");
            HugeFont = Content.Load<SpriteFont>("kenpixel_mini_square_huge");

			soundtracks = new List<SoundEffectInstance> ();
			soundtracks.Add(Content.Load<SoundEffect> ("soundtrack1").CreateInstance());
			soundtracks.Add(Content.Load<SoundEffect> ("soundtrack2").CreateInstance());
			soundtracks.Add(Content.Load<SoundEffect> ("soundtrack3").CreateInstance());

            sfx = new Dictionary<string, SoundEffectInstance>();
            sfx.Add("drums", Content.Load<SoundEffect>("drums").CreateInstance());
            sfx.Add("sword", Content.Load<SoundEffect>("sword").CreateInstance());
            sfx.Add("arrow", Content.Load<SoundEffect>("arrow").CreateInstance());
            sfx.Add("turn", Content.Load<SoundEffect>("turn").CreateInstance());

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
            hudShow.UpdateTime(deltaTime);

			GamePadState currentGamepadState = GamePad.GetState (PlayerIndex.One);
			KeyboardState currentKeyboardState = Keyboard.GetState();

            // Camera Zoom
            if (currentKeyboardState.IsKeyDown (keyBinding["ZOOM+"]) || currentGamepadState.Buttons.X == ButtonState.Pressed)
				camera.ZoomIn(deltaTime * camera.Zoom);

			if (currentKeyboardState.IsKeyDown (keyBinding["ZOOM-"]) || currentGamepadState.Buttons.Y == ButtonState.Pressed)
				camera.ZoomOut(deltaTime * camera.Zoom);

            //SelectedTile
            Vector2 currentStick = currentGamepadState.ThumbSticks.Left;
			Vector2 oldStick = oldGamepadState.ThumbSticks.Left;

			if (oldKeyboardState.IsKeyUp (keyBinding["UP"]) && currentKeyboardState.IsKeyDown (keyBinding["UP"]) || oldStick.Y > -controllerMinMovementY && currentStick.Y < -controllerMinMovementY) {
				var NeighbourgTile = SelectedTile.GetNeighbor (new Tile.Diff (0, -1));
				TryToMoveSelectedTile (NeighbourgTile);
			}

			if (oldKeyboardState.IsKeyUp (keyBinding["DOWN"]) && currentKeyboardState.IsKeyDown (keyBinding["DOWN"]) || oldStick.Y < controllerMinMovementY && currentStick.Y > controllerMinMovementY) {
				var NeighbourgTile = SelectedTile.GetNeighbor (new Tile.Diff (0, 1));
				TryToMoveSelectedTile (NeighbourgTile);
			}

			if (oldKeyboardState.IsKeyUp (keyBinding["LEFT"]) && currentKeyboardState.IsKeyDown (keyBinding["LEFT"]) || oldStick.X > -controllerMinMovementX && currentStick.X < -controllerMinMovementX) {
				var NeighbourgTile = SelectedTile.GetNeighbor (new Tile.Diff (-1, 0));
				TryToMoveSelectedTile (NeighbourgTile);
			}

			if (oldKeyboardState.IsKeyUp (keyBinding["RIGHT"]) && currentKeyboardState.IsKeyDown (keyBinding["RIGHT"]) || oldStick.X < controllerMinMovementX && currentStick.X > controllerMinMovementX) {
				var NeighbourgTile = SelectedTile.GetNeighbor (new Tile.Diff (1, 0));
				TryToMoveSelectedTile (NeighbourgTile);
			}

			if (!controlsFrozen)
			{

				//Unselect Unit
				if (oldKeyboardState.IsKeyUp(Keys.R) && currentKeyboardState.IsKeyDown (Keys.R) || oldGamepadState.Buttons.B == ButtonState.Released && currentGamepadState.Buttons.B == ButtonState.Pressed) {
					SelectedUnit = null;
				}

				//Action : Select or move unit
				if (oldKeyboardState.IsKeyUp(Keys.E) && currentKeyboardState.IsKeyDown (Keys.E) || oldGamepadState.Buttons.A == ButtonState.Released && currentGamepadState.Buttons.A == ButtonState.Pressed) {
					TryToMoveUnit ();
					TryToAttackUnit ();
					TryToSelectUnit ();
				}

				if (oldKeyboardState.IsKeyDown(Keys.F) && currentKeyboardState.IsKeyUp(Keys.F) 
					|| oldGamepadState.Buttons.RightShoulder == ButtonState.Released && currentGamepadState.Buttons.RightShoulder == ButtonState.Pressed) {
                    SelectNextUnit();
				}

                //Action: Undo
                if (oldKeyboardState.IsKeyUp(Keys.Delete) && currentKeyboardState.IsKeyDown(Keys.Delete) || oldGamepadState.Buttons.Back == ButtonState.Released && currentGamepadState.Buttons.Back == ButtonState.Pressed)
                {
                    if (GameModel.CheatMode) Undo();
                }

                //Action: Next turn
                if (oldKeyboardState.IsKeyUp(Keys.Enter) && currentKeyboardState.IsKeyDown(Keys.Enter) || oldGamepadState.Buttons.LeftShoulder == ButtonState.Released && currentGamepadState.Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    NextTurn();
                }

                //Action: Save game
                if (oldKeyboardState.IsKeyUp(Keys.F5) && currentKeyboardState.IsKeyDown(Keys.F5))
                {
                    string fileName = Saver.SaveGame(GameModel);
                    if (fileName != null)
                        hudShow.Notification = "Saved game as " + fileName;
                    else
                        hudShow.Notification = "Unable to save file!";
                }
            }

            Vector2 TriggerMove = new Vector2 (TileSize, TileSize);
			Vector2 StepMove = new Vector2 (TileSize, TileSize);
			camera.Adjust (mapShow.MapToScreen (SelectedTile.Point), TriggerMove, StepMove);

			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
#if !__IOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
				Keyboard.GetState ().IsKeyDown (Keys.Escape)) {
                Exit();
                //this.Window.Handle;
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

            HudBatch.Begin();
            hudShow.Blit();
            HudBatch.End();

		}

		protected void TryToMoveUnit() {
			if (SelectedUnit == null)
				return;

			List<Core.Move> moves;
			SelectedUnit.MovePossibilites.TryGetValue(SelectedTile, out moves);
			if (moves != null) {
				foreach (Move m in moves) {
					m.Execute ();
					GameModel.Actions.Push (m);
				}
				SelectedTile = SelectedUnit.Tile;
				SelectedUnit = null;
			}
		}

		protected void TryToAttackUnit() {
			if (SelectedUnit == null)
				return;

			Battle battle;
			SelectedUnit.BattlePossibilities.TryGetValue (SelectedTile, out battle);

			if (battle == null)
				return;

			battle.Execute ();
            GameModel.Actions.Push(battle);

            sfx[battle.Ranged ? "arrow" : "sword"].Play();
            hudShow.Notification = (battle.Result.Winner.Player == SelectedUnit.Player
                                    ? "Successful attack" : "Failed attack")
                                    + " with " + battle.Result.Dmg + " dmg";

			SelectedTile = SelectedUnit.Tile;
		}

		protected void TryToSelectUnit() {
            var alive = SelectedTile.Units.Where(unit => unit.Alive && unit.Player == GameModel.CurrentPlayer);
            if (alive.Count() <= 0)
				return;
			SelectedUnit = alive.First();
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
				EnumeratorUnit = GameModel.CurrentPlayer.Units.GetEnumerator ();
                GameModel.Actions.Clear();
                sfx["turn"].Play();
                hudShow.Event = GameModel.CurrentPlayer.Name + "'s turn!";
                SelectNextUnit();
            }
            catch (Core.Game.EndOfGameException)
            {
                sfx["drums"].Play();
                controlsFrozen = true;
            }
            hudShow.RefreshDataCache();
        }
        protected void Undo()
        {
            if (GameModel.Actions.Count == 0)
            {
                hudShow.Notification = "No action to undo";
            }
            else
            {
                GameModel.Actions.Pop().ReverseExecute();
                hudShow.Notification = "Undid action!";
            }
        }
        protected void SelectNextUnit()
        {
            do
            {
                if (!EnumeratorUnit.MoveNext())
                {
                    EnumeratorUnit.Reset();
                    EnumeratorUnit.MoveNext();
                }
            } while (!EnumeratorUnit.Current.Alive);
            SelectedUnit = EnumeratorUnit.Current;
            SelectedTile = SelectedUnit.Tile;
        }
    }
}

