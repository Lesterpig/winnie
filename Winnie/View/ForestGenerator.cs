using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI
{
	public class ForestGenerator
	{
		int squareSize;
		int seed;
		Random rnd;

		public ForestGenerator (int squareSize, int seed)
		{
			this.squareSize = squareSize;
			this.seed = seed;
		}

		public void ResetRandom() {
			rnd = new Random(seed);
		}

		private void BlitRandomTree(SpriteBatch spriteBatch, Texture2D map, int x, int y) {

			int selectedTree = rnd.Next (1, 7);
			spriteBatch.Draw (map, new Rectangle(x, y-squareSize, squareSize, squareSize), MapBinding.GetTexture("TreeUp"+selectedTree), Color.White);
			spriteBatch.Draw (map, new Rectangle(x, y, squareSize, squareSize), MapBinding.GetTexture("TreeBottom"+selectedTree), Color.White);
		}

		public void BlitRandomForest(SpriteBatch sb, Texture2D map, int x, int y) {
			for (int dx = 0; dx < 3; dx++) {
				for (int dy = 0; dy < 3; dy++) {
					BlitRandomTree (sb, map, (x * 3 + dx) * squareSize, (y * 3 + dy) * squareSize);
				}
			}
		}
	}
}

