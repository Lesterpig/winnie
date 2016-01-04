using System;

namespace MGUI
{
	public class GameCreator
	{
		public static void New() {
			var game = new Game1 ();
			game.Run ();
		}
	}
}

