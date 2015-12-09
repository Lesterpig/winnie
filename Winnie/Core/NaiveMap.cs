using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    /// <summary>
    /// A naive map generator (Mock).
    /// It will generate a map with always the same sequence of tiles : 
    /// Water, then Plain, then Forest, then Mountain, then Water ...
    /// 
    /// Player 1 will be on (1,0) and Player 2 on (3,0).
    /// </summary>
    /// <remarks>
    /// Only used for test purposes.
    /// </remarks>
    public class NaiveMap : MapGeneration
    {   
        /// <summary>
        /// Generates the map corresponding of the specified size and seed.
        /// </summary>
        /// <remarks>The resulting map should be a square of size * size tiles.</remarks>
        /// <param name="size">Size (should be at least 4).</param>
        /// <param name="seed">Random seed.</param>
        public Map Generate(uint size, int seed = 0)
        {
            var tiles = new TileTypeFactory.Identifier[size * size];
            for (int i = 0; i < size * size; i++)
            {
                tiles[i] = (TileTypeFactory.Identifier)(i % 4);
            }
            return new Map(tiles, size, size);
        }

        /// <summary>
        /// Setups initial positions for players.
        /// </summary>
        /// <param name="p1">Player 1.</param>
        /// <param name="p2">Player 2.</param>
        public void PlacePlayers(Player p1, Player p2)
        {
            p1.InitialPosition = new Point(1, 0);
            p2.InitialPosition = new Point(3, 0);
        }
    }
}