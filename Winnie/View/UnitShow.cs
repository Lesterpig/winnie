using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MGUI
{
	public class UnitShow : Blittable
	{
		Game1 game;
		List<Unit> listUnits;

		public UnitShow (Game1 g)
		{
			game = g;
			listUnits = new List<Unit> ();
			Unit.GeneratorSeed = g.Seed;

			foreach (Core.Player p in game.GameModel.Players) {
				foreach (Core.Unit u in p.Units) {
					listUnits.Add (Unit.New(u));
				}
			}

		}

		public void Blit()
		{
			foreach (Unit elem in listUnits) {
				elem.Blit (game.CharacterBatch, game.Character, game.SquareSize);
			}
		}
	}
}

