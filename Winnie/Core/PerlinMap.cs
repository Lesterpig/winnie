using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    /// <summary>
    /// A perlin map random generator. Uses a C++ library.
    /// </summary>
    public class PerlinMap : MapGeneration
    {   
        private Map _map;
        private Algo _algo;

        /// <summary>
        /// Initializes a new instance of the <see cref="Core.PerlinMap"/> class.
        /// </summary>
        public PerlinMap()
        {
            this._algo = new Algo();
        }

        /// <summary>
        /// Generates the map corresponding of the specified size and seed.
        /// </summary>
        /// <remarks>The resulting map should be a square of size * size tiles.</remarks>
        /// <param name="size">Size.</param>
        /// <param name="seed">Random seed. If zero, will generate a unpredicatable map.</param>
        public Map Generate(uint size, int seed = 0)
        {
            return this._map = new Map(this._algo.CreateMap(seed, (int) size, (int) size), size, size);
        }

        /// <summary>
        /// Setups initial positions for players.
        /// </summary>
        /// <param name="p1">Player 1.</param>
        /// <param name="p2">Player 2.</param>
        public void PlacePlayers(Player p1, Player p2)
        {
            this._algo.FindBestStartPosition(p1, p2, this._map);
        }
    }

}