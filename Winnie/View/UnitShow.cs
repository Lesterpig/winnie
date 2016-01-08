using System;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MGUI
{
	public class UnitShow : Blittable
	{
		Game1 game;
		public List<Unit> ListUnits { get; private set;}

		public UnitShow (Game1 g)
		{
			game = g;
			ListUnits = new List<Unit> ();
			Unit.GeneratorSeed = g.Seed;

			foreach (Core.Player p in game.GameModel.Players) {
				foreach (Core.Unit u in p.Units) {
					ListUnits.Add (Unit.New(u));
				}
			}
		}

		public void Blit()
		{

            Unit selected = null;

			foreach (Unit elem in ListUnits.Where(unit => unit.UnitModel.Alive)) {
                if (elem.UnitModel != game.SelectedUnit)
                    elem.Blit(game.CharacterBatch, game.Character, game.SquareSize);
                else
                    selected = elem;
            }

            // Draw selected unit over all other units
            if (selected != null)
                selected.Blit(game.CharacterBatch, game.Character, game.SquareSize);
        }
	}
}

