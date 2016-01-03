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

		private Rectangle TileToTexture(int x, int y, int dx, int dy) {
			Tile currentTile = game.GameModel.Map.getTile (x, y);
			string[] dyval = {"Up", "", "Bottom" };
			string[] dxval = {"Left", "", "Right" };

			string selectedTexture = "";
			selectedTexture += MapBinding.GetTileTextureName (currentTile);

			if (selectedTexture != "Grass") {
				bool append = false;

				Tile dyTile = currentTile.GetNeighbor (new Tile.Diff (0, dy-1));
				if (dyTile != null && dyTile.TileType != currentTile.TileType) {
					selectedTexture += dyval [dy];
					append = true;
				}

				Tile dxTile = currentTile.GetNeighbor (new Tile.Diff (dx-1, 0));
				if (dxTile != null && dxTile.TileType != currentTile.TileType) {
					selectedTexture += dxval [dx];
					append = true;
				}

				if (append) selectedTexture += "Grass";
			}
			return MapBinding.GetTexture (selectedTexture);
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
			for (int i = 0; i < game.GameModel.Map.SizeX; i++) {
				for (int j = 0; j < game.GameModel.Map.SizeY; j++) {
					BlitTiles (i,j);
					BlitForest (i,j);
				}
			}
		}
	}
}

