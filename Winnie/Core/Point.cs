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

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Core.Point"/>.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Core.Point"/>.</param>
        /// <returns>><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
        /// <see cref="Core.Tile+Diff"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Point b = (Point)obj;
            return this.x == b.x && this.y == b.y;
        }
    }  
}

