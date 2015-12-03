using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Map
    {
        public Tile[] Tiles { get; private set; }

        public Map(TileTypeFactory.Identifier[] tiles)
		{   
            int size = tiles.Length;
            this.Tiles = new Tile[size];

            for (int i = 0; i < size; i++)
            {
                this.Tiles[i] = new Tile(TileTypeFactory.Get(tiles[i]), i);
            }
		}

    }
}