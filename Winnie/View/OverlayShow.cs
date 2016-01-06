using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI
{
	public class OverlayShow : Blittable
	{
		Game1 game;
		public OverlayShow (Game1 g)
		{
			game = g;
		}

		public void Blit()
		{
			if (game.SelectedUnit == null)
				return;

			var AllPossibilities = game.SelectedUnit.UnitModel.MovePossibilites;

			for (int i = 0; i < game.GameModel.Map.SizeX; i++) {
				for (int j = 0; j < game.GameModel.Map.SizeY; j++) {

					foreach (Core.Tile t in AllPossibilities.Keys) {
						if (t.Point.x == i && t.Point.y == j) {
							game.OverlayBatch.Draw (
								game.MapOverlay,
								new Rectangle (i * 3 * game.SquareSize, j * 3 * game.SquareSize, game.SquareSize * 3, game.SquareSize * 3),
								new Rectangle (0, 0, 128, 128),
								new Color (20, 20, 255, 100)
							);
						}
					}
				}
			}
		}
	}
}

