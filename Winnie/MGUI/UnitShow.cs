using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MGUI
{
	public class UnitShow
	{
		Game1 game;
		List<Unit> ulist;

		public UnitShow (Game1 g)
		{
			game = g;
			ulist = new List<Unit> ();
			ulist.Add (new ElfUnit(g.Seed));
			ulist.Add (new OrcUnit (g.Seed));
			ulist.Add (new HumanUnit (g.Seed));
		}

		public void Blit()
		{
			int posX = 0;
			foreach (Unit elem in ulist) {
				posX -= 128;
				elem.Blit (game.WorldBatch, game.Character, game.SquareSize, posX, 64);
			}
		}
	}
}

