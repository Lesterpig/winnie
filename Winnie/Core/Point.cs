using System;
using System.Runtime.InteropServices;

namespace Core
{   
    // TODO documentation
	[StructLayout(LayoutKind.Sequential)]
	public class Point {
		public int x;
		public int y;

		public Point() {
			x = -1;
			y = -1;
		}

		public Point(int x, int y) {
			this.x = x;
			this.y = y;
		}
	}  
}

