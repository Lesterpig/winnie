using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI
{
	public class OverlayShow : Blittable
	{
		Game1 game;
		Color blueColor = new Color (20, 20, 250, 50);
		Color greenColor = new Color (20, 250, 20, 50);


		public OverlayShow (Game1 g)
		{
			game = g;
		}

		public void BlitMovement() {
			if (game.SelectedUnit == null)
				return;

			var AllPossibilities = game.SelectedUnit.MovePossibilites;

			for (int i = 0; i < game.GameModel.Map.SizeX; i++) {
				for (int j = 0; j < game.GameModel.Map.SizeY; j++) {

					foreach (Core.Tile t in AllPossibilities.Keys) {
						if (t.Point.x == i && t.Point.y == j) {
							game.OverlayBatch.Draw (
								game.MapOverlay,
								new Rectangle (i * 3 * game.SquareSize, j * 3 * game.SquareSize, game.SquareSize * 3, game.SquareSize * 3),
								new Rectangle (0, 0, 128, 128),
								blueColor
							);
						}
					}
				}
			}
		}

		public void BlitSelectedTile () {
			game.OverlayBatch.Draw (
				game.MapOverlay,
				new Rectangle (game.SelectedTile.Point.x * 3 * game.SquareSize, game.SelectedTile.Point.y * 3 * game.SquareSize, game.SquareSize * 3, game.SquareSize * 3),
				new Rectangle (0, 0, 128, 128),
				greenColor
			);
		}

		public void Blit()
		{
			BlitMovement ();
			BlitSelectedTile ();
		}
	}
}

