using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
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
	}
}
