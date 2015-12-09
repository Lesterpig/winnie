using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    // TODO documentation
    public class NaiveMap : MapGeneration
    {   
        public Map Generate(uint size, int seed = 0)
        {
            var tiles = new TileTypeFactory.Identifier[size * size];
            for (int i = 0; i < size * size; i++)
            {
                tiles[i] = (TileTypeFactory.Identifier)(i % 4);
            }
            return new Map(tiles, size, size);
        }

        public void PlacePlayers(Player p1, Player p2)
        {
            p1.InitialPosition = new Point(1, 0);
            p2.InitialPosition = new Point(3, 0);
        }
    }
}