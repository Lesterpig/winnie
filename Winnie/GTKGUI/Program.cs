using System;
using Gtk;

namespace GTKGUI
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			int seed = 0, mapX = 30, mapY = 30;
			Core.Algo a = new Core.Algo ();
			Core.Map m = new Core.Map (a.CreateMap (seed, mapX, mapY), mapX, mapY);

			Application.Init ();
			MainWindow win = new MainWindow ();
			win.map = m;
			win.Show ();
			Application.Run ();
		}
	}
}
