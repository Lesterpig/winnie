using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Map
    {
		//@TODO REFACTOR, this is a hotfix in order to test C++ implementation
		//We should not use TileTypes directly but instead use a Tile.
        //private Core.Tile[][] Tiles;
		public TileType[] Tiles { get; private set; }
		//@ENDTODO

		public Map(TileType[] tiles)
		{
			Tiles = tiles;
		}

    }
}