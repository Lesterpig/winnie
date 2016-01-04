using System;
using Microsoft.Xna.Framework;

namespace MGUI
{
	public class UnitShow
	{
		Game1 game;

		public UnitShow (Game1 g)
		{
			game = g;
		}

		public void Blit()
		{
			var e = new ElfUnit (game.Seed);
			e.Blit (game.WorldBatch, game.Character, game.SquareSize, 64, 64);
		}
	}
}

