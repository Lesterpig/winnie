using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Core
{
	public class Perlin : IDisposable
	{
		bool disposed = false;
		IntPtr nativePerlin;

		public Perlin(int seed, int sx, int sy, int min, int max, int st, int oc, double pers)
		{
			nativePerlin = Perlin_new(seed,sx,sy,min,max,st,oc,pers);
		}

		~Perlin()
		{
			Dispose(false);
			Perlin_delete(nativePerlin);
		}

		public double coherentNoise2D(double x, double y) {
			return Perlin_coherentNoise2D (nativePerlin, x, y);
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
				Perlin_delete(nativePerlin);
			}
			disposed = true;
		}


		[DllImport("libAlgo.dll", CallingConvention= CallingConvention.Cdecl)]
		extern static double Perlin_coherentNoise2D(IntPtr perlin, double x, double y);

		[DllImport("libAlgo.dll", CallingConvention = CallingConvention.Cdecl)]
		extern static IntPtr Perlin_new(int seed, int sx, int sy, int min, int max, int st, int oc, double pers);

		[DllImport("libAlgo.dll", CallingConvention = CallingConvention.Cdecl)]
		extern static IntPtr Perlin_delete(IntPtr perlin);
	}
}

