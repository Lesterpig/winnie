using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Map
    {
        public Tile[,] Tiles { get; private set; }

        public Map(TileTypeFactory.Identifier[] tiles)
		{   
            int size = (int) Math.Sqrt(tiles.Length);
            this.Tiles = new Tile[size,size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    this.Tiles[i,j] = new Tile(TileTypeFactory.Get(tiles[i*size+j]), i*size+j);
                }
            }
		}

    }
}