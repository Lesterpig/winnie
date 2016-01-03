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
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Texture2D map;
		Core.Game gameModel;

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
			var p1 = new Player("Player A", Human.Instance);
			var p2 = new Player("Player B", Elf.Instance);
			gameModel = GameBuilder.New<StandardGameType, PerlinMap>(p1, p2, true);

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
			spriteBatch = new SpriteBatch (GraphicsDevice);
			map = Content.Load<Texture2D> ("map");

			//TODO: use this.Content to load your game content here 
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
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
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);
			int zoom = 128;

		
			//TODO: Add your drawing code here
			spriteBatch.Begin();

			//RAW MAP
			for (int i = 0; i < gameModel.Map.SizeX; i++) {
				for (int j = 0; j < gameModel.Map.SizeY; j++) {
					Rectangle texture = MapBinding.Grass1;

					TileType t = gameModel.Map.getTile(i,j).TileType;
					if (t is WaterTileType)
						texture = MapBinding.Water;
					else if (t is MountainTileType)
						texture = MapBinding.Dirt;
					
					spriteBatch.Draw (map, new Rectangle (i*zoom, j*zoom, zoom, zoom), texture, Color.White);
				}
			}


			spriteBatch.End();

			base.Draw (gameTime);
		}
	}
}

