using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Core
{   
    // TODO documentation
	[StructLayout(LayoutKind.Sequential)]
	public class Proposition {
		public Point start;
		public Point goal;
		public int   bonus;
	}

	public class Algo : IDisposable
	{
		bool disposed = false;
		IntPtr nativeAlgo;

		public Algo()
		{
			nativeAlgo = Algo_new();
		}

		~Algo()
		{
			Dispose(false);
			Algo_delete(nativeAlgo);
		}

		public TileTypeFactory.Identifier[] CreateMap(int seed, int sizeX, int sizeY)
		{
			var tiles = new TileTypeFactory.Identifier[sizeX * sizeY];
			Algo_fillMap(nativeAlgo, tiles, seed, sizeX, sizeY);

			return tiles;
		}

		public void FindBestStartPosition(Player p1, Player p2, Map m)
		{
            Algo_findBestStartPosition (nativeAlgo, m.RawTiles, (int)m.SizeX, (int)m.SizeY, p1.Race.Identifier, p2.Race.Identifier, p1.InitialPosition, p2.InitialPosition);
		}

		public List<Proposition> FindBestActions(Player me, Player opponent, Map m) {
			List<Proposition> props = new List<Proposition>();

			int i = 0;
			int[] myUnits = new int[me.Units.Count];
			foreach (Unit u in me.Units) {
				myUnits[i++] = u.Tile.Position;
			}

			i = 0;
			int[] opponentUnits = new int[opponent.Units.Count];
			foreach (Unit u in opponent.Units) {
				opponentUnits[i++] = u.Tile.Position;
			}
			Proposition a1 = new Proposition ();
			Proposition a2 = new Proposition ();
			Proposition a3 = new Proposition ();
			Algo_findBestActions (nativeAlgo, m.RawTiles, (int)m.SizeX, (int)m.SizeY, myUnits, myUnits.Length, opponentUnits, opponentUnits.Length, me.Race.Identifier,a1,a2,a3);
			props.Add (a1);
			props.Add (a2);
			props.Add (a3);
			return props;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposed)
				return; 
			if (disposing)
			{
				Algo_delete(nativeAlgo);
			}
			disposed = true;
		}

		[DllImport("libAlgo.dll", CallingConvention = CallingConvention.Cdecl)]
		extern static IntPtr Algo_new();

		[DllImport("libAlgo.dll", CallingConvention = CallingConvention.Cdecl)]
		extern static IntPtr Algo_delete(IntPtr algo);

		[DllImport("libAlgo.dll", CallingConvention= CallingConvention.Cdecl)]
		extern static void Algo_fillMap(IntPtr algo, TileTypeFactory.Identifier[] tiles, int seed, int sizeX, int sizeY);

		[DllImport("libAlgo.dll", CallingConvention= CallingConvention.Cdecl)]
		extern static void Algo_findBestStartPosition(IntPtr algo, TileTypeFactory.Identifier[] tiles, int sizeX, int sizeY, int pl1, int pl2, Point p1, Point p2);

		[DllImport("libAlgo.dll", CallingConvention= CallingConvention.Cdecl)]
		extern static void Algo_findBestActions(IntPtr algo, TileTypeFactory.Identifier[] map, int sx, int sy, int[] allies, int nallies, int[] ennemies, int nennemies, int pl, [Out]Proposition a1, [Out]Proposition a2, [Out]Proposition a3);

	}
}
