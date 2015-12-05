using System;
using System.Runtime.InteropServices;

namespace Core
{
	[StructLayout(LayoutKind.Sequential)]
	public class Point {
		public int x;
		public int y;

		public Point() {
			x = -1;
			y = -1;
		}
	}  
}

