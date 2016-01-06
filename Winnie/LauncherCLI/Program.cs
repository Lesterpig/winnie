using System;
using MGUI;
using Core;
using System.Linq;

namespace Linux
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			int seed = 0;

			Console.WriteLine ("Launch MonoGame !");
			var p1 = new Player("Player A", Human.Instance);
			var p2 = new Player("Player B", Elf.Instance);
			var GameModel = GameBuilder.New<StandardGameType, PerlinMap>(p1, p2, true, seed);
			GameModel.Players[0].Units.ElementAt(0).Move(GameModel.Map.Tiles[5]);
			GameCreator.New (GameModel, seed);
		}
	}
}
