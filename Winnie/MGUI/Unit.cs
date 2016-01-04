using System;
using Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI
{
	public abstract class Unit
	{
		protected Random rnd;
		protected string body;
		protected string pant;
		protected string shoes;
		protected string shirt;
		protected string hair;
		protected string hat;
		protected string shield;
		protected string weapon;

		public Unit(int seed) {
			this.rnd = new Random(seed);
		}

		public void Blit(SpriteBatch spriteBatch, Texture2D character, int squareSize, int x, int y) {
			spriteBatch.Draw(character, new Rectangle(x,y,squareSize,squareSize), UnitBinding.GetTexture(body), Color.White);

			int pantSizeX = 24 * squareSize / 128;
			int pantSizeY = 104 * squareSize / 128;
			int pantDx = 80 * squareSize / 128;
			int pantDy = 24 * squareSize / 128;
			spriteBatch.Draw(character, new Rectangle(x+pantSizeX, y+pantSizeY, pantDx, pantDy), UnitBinding.GetTexture(pant), Color.White);
		}
	}
}

