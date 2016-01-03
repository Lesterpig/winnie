using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Core;

namespace MGUI
{
	public class MapBinding
	{
		public static int Size = 128;
		private Map map;
		private static Dictionary<string, Vector2> binding = new Dictionary<string, Vector2>()
		{
			/* GRASS */
			{"Grass", new Vector2(1,1)},
			{"Grass2", new Vector2(3,2)},
			{"Grass3", new Vector2(4,2)},
			{"GrassTopLeftDirt", new Vector2(0,0)},
			{"GrassTopDirt", new Vector2(1,0)},
			{"GrassTopRightDirt", new Vector2(2,0)},
			{"GrassRightDirt", new Vector2(2,1)},
			{"GrassBottomRightDirt", new Vector2(2,2)},
			{"GrassBottomDirt", new Vector2(1,2)},
			{"GrassBottomLeftDirt", new Vector2(0,2)},
			{"GrassLeftDirt", new Vector2(0,1)},

			/* DIRT */
			{"Dirt", new Vector2(6,1)},
			{"DirtTopRightGrass", new Vector2(7,0)},
			{"DirtRightGrass", new Vector2(7,1)},
			{"DirtBottomRightGrass", new Vector2(7,2)},
			{"DirtBottomGrass", new Vector2(6,2)},
			{"DirtBottomLeftGrass", new Vector2(5,2)},
			{"DirtLeftGrass", new Vector2(5,1)},
			{"DirtTopLeftGrass", new Vector2(5,0)},
			{"DirtTopGrass", new Vector2(6,0)},

			/* WATER */
			{"Water", new Vector2(11,1)},
			{"WaterTopRightGrass", new Vector2(12,0)},
			{"WaterRightGrass", new Vector2(12,1)},
			{"WaterBottomRightGrass", new Vector2(12,2)},
			{"WaterBottomGrass", new Vector2(11,2)},
			{"WaterBottomLeftGrass", new Vector2(10,2)},
			{"WaterLeftGrass", new Vector2(10,1)},
			{"WaterTopLeftGrass", new Vector2(10,0)},
			{"WaterTopGrass", new Vector2(11,0)},

			/* TREES */
		};

		public MapBinding (Map m)
		{
			map = m;
		}

		public Rectangle GetTexture(int x, int y, int dx, int dy) {
			Tile currentTile = map.getTile (x, y);
			string[] dyval = {"Top", "", "Bottom" };
			string[] dxval = {"Left", "", "Right" };

			string selectedTexture = "";
			selectedTexture += GetTileTextureName (currentTile);

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
			return GetTexture (selectedTexture);
		}

		public string GetTileTextureName(Tile t) {
			if (t.TileType is WaterTileType) {
				return "Water";
			} else if (t.TileType is MountainTileType) {
				return "Dirt";
			}
			return "Grass";
		}

		public Rectangle GetTexture(string name) {
			Vector2 res;
			if (!binding.TryGetValue (name, out res))
				res = new Vector2 (8, 2);

			return new Rectangle ((int) (res.X * Size), (int)(res.Y * Size), Size, Size);

		}
	}
}

