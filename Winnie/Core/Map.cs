using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Map
    {
        public Tile[] Tiles { get; private set; }
        public TileTypeFactory.Identifier[] RawTiles { get; private set; } //Only used for c++
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }

		public Map(TileTypeFactory.Identifier[] tiles, int sx, int sy)
		{   
			this.RawTiles = tiles;
			this.SizeX = sx;
			this.SizeY = sy;

            int size = tiles.Length;
            this.Tiles = new Tile[size];

            for (int i = 0; i < size; i++)
            {
                this.Tiles[i] = new Tile(TileTypeFactory.Get(tiles[i]), i);
            }
		}

		public Tile getTile(int x, int y) {
			return Tiles [x + SizeX * y];
		}

    }
}
