using System;
using System.Runtime.InteropServices;

namespace Core
{   
    /// <summary>
    /// A utility class to deal with points.
    /// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public class Point {

        /// <summary>
        /// The x coordinate.
        /// </summary>
		public int x;

        /// <summary>
        /// The y coordinate.
        /// </summary>
		public int y;

        /// <summary>
        /// Initializes a new instance of the <see cref="Core.Point"/> class with default values (-1, -1).
        /// </summary>
		public Point() {
			x = -1;
			y = -1;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="Core.Point"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
		public Point(int x, int y) {
			this.x = x;
			this.y = y;
		}
	}  
}

