using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI
{
	public class ForestGenerator
	{
		SpriteBatch spriteBatch;
		Texture2D map;
		int squareSize;
		Random rnd;

		public ForestGenerator (SpriteBatch sb, Texture2D map, int squareSize, int seed)
		{
			spriteBatch = sb;
			this.map = map;
			this.squareSize = squareSize;
			rnd = new Random(seed);
		}

		private void BlitRandomTree(int x, int y) {
			int selectedTree = rnd.Next (1, 7);
			spriteBatch.Draw (map, new Rectangle(x, y-squareSize, squareSize, squareSize), MapBinding.GetTexture("TreeUp"+selectedTree), Color.White);
			spriteBatch.Draw (map, new Rectangle(x, y, squareSize, squareSize), MapBinding.GetTexture("TreeBottom"+selectedTree), Color.White);
		}

		public void BlitRandomForest(int x, int y) {
			for (int dx = 0; dx < 3; dx++) {
				for (int dy = 0; dy < 3; dy++) {
					BlitRandomTree ((x * 3 + dx) * squareSize, (y * 3 + dy) * squareSize);
				}
			}
		}
	}
}

