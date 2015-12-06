using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Core
{
	public class Dijkstra : IDisposable
	{
		bool disposed = false;
		IntPtr nativeDijkstra;

		public Dijkstra(double[] data, int sx, int sy, Point start)
		{
			nativeDijkstra = Dijkstra_new(data,sx,sy,start);
		}

		~Dijkstra()
		{
			Dispose(false);
			Dijkstra_delete(nativeDijkstra);
		}

		public double getDistance(Point goal) {
			return Dijkstra_getDistance (nativeDijkstra, goal);
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
				Dijkstra_delete(nativeDijkstra);
			}
			disposed = true;
		}


		[DllImport("libAlgo.dll", CallingConvention= CallingConvention.Cdecl)]
		extern static double Dijkstra_getDistance(IntPtr dijkstra, Point goal);

		[DllImport("libAlgo.dll", CallingConvention = CallingConvention.Cdecl)]
		extern static IntPtr Dijkstra_new(double[] data, int sx, int sy, Point start);

		[DllImport("libAlgo.dll", CallingConvention = CallingConvention.Cdecl)]
		extern static IntPtr Dijkstra_delete(IntPtr dijkstra);
	}
}

