using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    /// <summary>
    /// An interface for map generator (Strategy design pattern).
    /// </summary>
    public interface MapGeneration
    {   
        /// <summary>
        /// Generates the map corresponding of the specified size and seed.
        /// </summary>
        /// <remarks>
        /// The resulting map should be a square of size * size tiles.
        /// </remarks>
        /// <param name="size">Size.</param>
        /// <param name="seed">Random seed.</param>
        Map Generate(uint size, int seed = 0);

        /// <summary>
        /// Setups initial positions for players.
        /// </summary>
        /// <param name="p1">Player 1.</param>
        /// <param name="p2">Player 2.</param>
        void PlacePlayers(Player p1, Player p2);
    }
}