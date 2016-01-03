using System;
using Microsoft.Xna.Framework;


namespace MGUI
{
	public class MapBinding
	{
		public static int Size = 128;
		/**
		 * GRASS
		 */
		public static Rectangle Grass1 = new Rectangle(Size * 1, Size * 1, Size, Size);
		public static Rectangle Grass2 = new Rectangle(Size * 3, Size * 2, Size, Size);
		public static Rectangle Grass3 = new Rectangle(Size * 4, Size * 2, Size, Size);

		/**
		 * GRASS - DIRT FRONTIER
		 */
		public static Rectangle GrassTopLeftDirt = new Rectangle(Size * 0, Size * 0, Size, Size);
		public static Rectangle GrassTopDirt = new Rectangle(Size * 1, Size * 0, Size, Size);
		public static Rectangle GrassTopRightDirt = new Rectangle(Size * 2, Size * 0, Size, Size);
		public static Rectangle GrassRightDirt = new Rectangle(Size * 2, Size * 1, Size, Size);
		public static Rectangle GrassBottomRightDirt = new Rectangle(Size * 2, Size * 2, Size, Size); 

		/**
		 * DIRT
		 */
		public static Rectangle Dirt = new Rectangle(Size * 6, Size * 1, Size, Size);

		/**
		 * WATER
		 */
		public static Rectangle Water = new Rectangle(Size * 11, Size * 1, Size, Size);

		/**
		 * TREES
		 */

		private MapBinding ()
		{
		}
	}
}

