using System;
using Core;
using Microsoft.Xna.Framework;

namespace MGUI
{
	public class MapShow
	{
		Game1 game;
		ForestGenerator fg;

		public MapShow (Game1 g)
		{
			game = g;
			fg = new ForestGenerator (game.SquareSize, game.Seed);
		}
			
		/// <summary>
		/// Each tile is represented by 9 64x64 texture block :
		/// Up Left, Up, Up Right, Right, Bottom Right, Bottom, Bottom Left, Left and Center
		/// By default, junction are done with grass
		/// </summary>
		private Rectangle TileToTexture(int x, int y, int dx, int dy) {
			Tile currentTile = game.GameModel.Map.getTile (x, y);
			string[] dyval = {"Up", "", "Bottom" };
			string[] dxval = {"Left", "", "Right" };

			string selectedTexture = "";
			selectedTexture += MapBinding.GetTileTextureName (currentTile);

			if (selectedTexture != "Grass") {
				bool border = false;

				//BOTTOM/UP
				Tile dyTile = currentTile.GetNeighbor (new Tile.Diff (0, dy-1));
				if (dyTile != null && dyTile.TileType != currentTile.TileType) {
					selectedTexture += dyval [dy];
					border = true;
				}

				//LEFT/RIGHT
				Tile dxTile = currentTile.GetNeighbor (new Tile.Diff (dx-1, 0));
				if (dxTile != null && dxTile.TileType != currentTile.TileType) {
					selectedTexture += dxval [dx];
					border = true;
				}

				//DIAGONAL
				if (!border && dx != 1 && dy != 1) {
					Tile ddTile = currentTile.GetNeighbor (new Tile.Diff (dx - 1, dy - 1));
					if (ddTile != null && ddTile.TileType != currentTile.TileType) {
						selectedTexture += "Diagonal" + dyval [dy] + dxval [dx];
						border = true;
					}
				}
		
				if (border) selectedTexture += "Grass";
			}
			return MapBinding.GetTexture (selectedTexture);
		}

		private void Initialize() {
			fg.ResetRandom ();
		}

		private void BlitTiles(int i, int j) {
			for (int dx = 0; dx < 3; dx++) {
				for (int dy = 0; dy < 3; dy++) {
					game.MapBatch.Draw (
						game.Map, 
						new Rectangle ((i * 3 + dx) * game.SquareSize, (j * 3 + dy) * game.SquareSize, game.SquareSize, game.SquareSize), 
						TileToTexture (i, j, dx, dy), 
						Color.White
					);
				}
			}
		}

		private void BlitForest(int i, int j) {
			if (game.GameModel.Map.getTile (i, j).TileType is ForestTileType) {
				fg.BlitRandomForest (game.MapBatch, game.Map, i, j);
			}
		}

		public void BlitMap() {
			Initialize ();

			for (int i = 0; i < game.GameModel.Map.SizeX; i++) {
				for (int j = 0; j < game.GameModel.Map.SizeY; j++) {
					BlitTiles (i,j);
					BlitForest (i,j);
				}
			}
		}
	}
}

