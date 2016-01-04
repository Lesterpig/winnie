using System;
using MGUI;
using Core;

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
			GameCreator.New (GameModel, seed);
		}
	}
}
