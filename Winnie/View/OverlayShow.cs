using System;
using System.Linq;
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
		Color redColor = new Color (250, 20, 20, 50);
        Color purpleColor = Color.Purple * 0.3f;

        public bool MoveSuggestionEnabled { get; set; }

		public OverlayShow (Game1 g)
		{
			game = g;
            MoveSuggestionEnabled = false;
		}

		void showTileSquare(ICollection<Core.Tile> tiles, int i, int j, Color c) {
			foreach (Core.Tile t in tiles) {
				if (t.Point.x == i && t.Point.y == j) {
					game.OverlayBatch.Draw (
						game.MapOverlay,
						new Rectangle (i * 3 * game.SquareSize, j * 3 * game.SquareSize, game.SquareSize * 3, game.SquareSize * 3),
						new Rectangle (0, 0, 128, 128),
						c
					);
				}
			}
		}

		void BlitMovement() {
			if (game.SelectedUnit == null || game.SelectedUnit.Player != game.GameModel.CurrentPlayer)
				return;

			var AllPossibilities = game.SelectedUnit.MovePossibilites;
			var AllBattle = game.SelectedUnit.BattlePossibilities;

			for (int i = 0; i < game.GameModel.Map.SizeX; i++) {
				for (int j = 0; j < game.GameModel.Map.SizeY; j++) {
					showTileSquare (AllPossibilities.Keys, i, j, blueColor);
					showTileSquare (AllBattle.Keys, i, j, redColor);
				}
			}
		}

		void BlitSelectedTile () {
			game.OverlayBatch.Draw (
				game.MapOverlay,
				new Rectangle (game.SelectedTile.Point.x * 3 * game.SquareSize, game.SelectedTile.Point.y * 3 * game.SquareSize, game.SquareSize * 3, game.SquareSize * 3),
				new Rectangle (0, 0, 128, 128),
				greenColor
			);
		}

        private int glowingCmp = 0;
		public void BlitRecommandedTile() {
            if (game.SelectedUnit == null)
                return;
            var gp = game.GameModel.FindBestActions().Where(a => a.start.Equals(game.SelectedUnit.Tile.Point) && !a.start.Equals(a.goal));
			foreach (Core.Proposition prop in gp) {
				game.OverlayBatch.Draw (
					game.MapOverlay,
					new Rectangle (prop.goal.x * 3 * game.SquareSize, prop.goal.y * 3 * game.SquareSize, game.SquareSize * 3, game.SquareSize * 3),
					new Rectangle (0, 0, 128, 128),
                    purpleColor * (float) (0.5f + 0.5f * (Math.Cos(glowingCmp / 10)))
				);
			}
            glowingCmp = (glowingCmp + 1) % 1000000; // Quick and dirty...
        }

		public void Blit()
		{
			BlitMovement ();
			BlitSelectedTile ();
            if (MoveSuggestionEnabled)
			    BlitRecommandedTile ();
		}
	}
}

