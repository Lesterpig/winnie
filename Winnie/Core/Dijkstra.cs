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
		int sizeX, sizeY;

		public Dijkstra(double[] data, int sx, int sy, Point start)
		{
			sizeX = sx; sizeY = sy;
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

		public List<Point> getPath(Point goal) {
			int[] path = new int[sizeX*sizeY];
			int path_steps = Dijkstra_getPath (nativeDijkstra, goal, path);
			List<Point> ret = new List<Point>();
			for (int i = 0; i < path_steps; i++) {
				ret.Add (new Point(path[i] % sizeX, path[i] / sizeX));
			}
			return ret;
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

		[DllImport("libAlgo.dll", CallingConvention= CallingConvention.Cdecl)]
		extern static int Dijkstra_getPath(IntPtr dijkstra, Point goal, int[] path);

		[DllImport("libAlgo.dll", CallingConvention = CallingConvention.Cdecl)]
		extern static IntPtr Dijkstra_new(double[] data, int sx, int sy, Point start);

		[DllImport("libAlgo.dll", CallingConvention = CallingConvention.Cdecl)]
		extern static IntPtr Dijkstra_delete(IntPtr dijkstra);
	}
}

