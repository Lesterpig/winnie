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

        public TileTypeFactory.Identifier[] CreateMap(int nbTiles)
		{
            var tiles = new TileTypeFactory.Identifier[nbTiles];
			Algo_fillMap(nativeAlgo, tiles, nbTiles);
            return tiles;
		}

		public Algo()
		{
			nativeAlgo = Algo_new();
		}

		~Algo()
		{
			Dispose(false);
			Algo_delete(nativeAlgo);
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


		[DllImport("libAlgo.dll", CallingConvention= CallingConvention.Cdecl)]
        extern static void Algo_fillMap(IntPtr algo, TileTypeFactory.Identifier[] tiles, int nbTiles);

		[DllImport("libAlgo.dll", CallingConvention = CallingConvention.Cdecl)]
		extern static IntPtr Algo_new();

		[DllImport("libAlgo.dll", CallingConvention = CallingConvention.Cdecl)]
		extern static IntPtr Algo_delete(IntPtr algo);
	}
}
