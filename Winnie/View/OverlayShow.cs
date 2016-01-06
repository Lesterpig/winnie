using System;
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
			//game.OverlayBatch.Draw ();
			game.OverlayBatch.Draw (
				game.MapOverlay,
				new Rectangle (0 * 3 * game.SquareSize, 0 * 3 * game.SquareSize, game.SquareSize * 3, game.SquareSize * 3),
				new Rectangle (0, 0, 128, 128),
				new Color(20,20,255,100)
			);
		}
	}
}

