using System;
using Gtk;
using Cairo;

public partial class MainWindow: Gtk.Window
{

	public Core.Map map;

	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		SetDefaultSize(420, 420);
		SetPosition(WindowPosition.Center);
		DeleteEvent += delegate { Application.Quit(); };

		DrawingArea darea = new DrawingArea();
		darea.ExposeEvent += OnExpose;

		Add(darea);

		ShowAll();
		Build ();
	}
	void OnExpose(object sender, ExposeEventArgs args)
	{
		DrawingArea area = (DrawingArea) sender;
		Cairo.Context cr =  Gdk.CairoHelper.Create(area.GdkWindow);

		int w = 0, p = 0, f = 0, m = 0;

		for (int i = 0; i < map.SizeX; i++) {
			for (int j = 0; j < map.SizeY; j++) {
				cr.Rectangle(30*i, 30*j, 30*(i+1), 30*(j+1));
				cr.SetSourceRGB(1, 1, 1);
				cr.StrokePreserve();

				Core.Tile t = map.getTile (i, j);
				if (t.TileType is Core.WaterTileType) {
					cr.SetSourceRGB (0.25, 0.68, 0.82); w++;
				} else if (t.TileType is Core.PlainTileType) {
					cr.SetSourceRGB (0.73, 0.93, 0.55); p++;
				} else if (t.TileType is Core.ForestTileType) {
					cr.SetSourceRGB (0.24, 0.5, 0.05); f++;
				} else {
					cr.SetSourceRGB (0.39, 0.36, 0.31); m++;
				}
				 
				cr.Fill();
			}
		}

		Console.WriteLine ("water: "+ w + ", plain: "+ p + ", forest: "+ f + ", mountain: "+m);


		((IDisposable) cr.GetTarget()).Dispose();                                      
		((IDisposable) cr).Dispose();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
