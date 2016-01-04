using System;

namespace MGUI
{
	public class GameCreator
	{
		public static void New(Core.Game gameModel, int seed) {
			var game = new Game1 (gameModel, seed);
			game.Run ();
		}
	}
}

