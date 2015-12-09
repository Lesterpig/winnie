using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    /// <summary>
    /// Map represents the map of a game, containing some Tiles.
    /// </summary>
    public class Map
    {   
        /// <summary>
        /// Gets the tiles.
        /// </summary>
        /// <value>The tiles.</value>
        public Tile[] Tiles { get; private set; }

        /// <summary>
        /// Gets the raw tiles.
        /// </summary>
        /// <remarks>
        /// Only used for C++ wrapper
        /// </remarks>
        /// <value>The raw tiles.</value>
        public TileTypeFactory.Identifier[] RawTiles { get; private set; }

        /// <summary>
        /// Gets the size x.
        /// </summary>
        /// <value>The size x.</value>
        public uint SizeX { get; private set; }

        /// <summary>
        /// Gets the size y.
        /// </summary>
        /// <value>The size y.</value>
        public uint SizeY { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Core.Map"/> class.
        /// </summary>
        /// <param name="tiles">Tiles identifiers.</param>
        /// <param name="sx">Size x.</param>
        /// <param name="sy">Size y.</param>
		public Map(TileTypeFactory.Identifier[] tiles, uint sx, uint sy)
		{   
			this.RawTiles = tiles;
			this.SizeX = sx;
			this.SizeY = sy;

            int size = tiles.Length;
            this.Tiles = new Tile[size];

            for (int i = 0; i < size; i++)
            {
                this.Tiles[i] = new Tile(TileTypeFactory.Get(tiles[i]), i);
                this.Tiles[i].Map = this;
            }
		}

        /// <summary>
        /// Transforms the map to a c++-ready map for movement computation.
        /// </summary>
        /// <remarks>
        /// The unit is provided to compute required move points for all tiles.
        /// </remarks>
        /// <returns>The move map.</returns>
        /// <param name="u">The unit to move.</param>
        /// <seealso cref="Dijkstra"/>
        public double[] GetMoveMap(Unit u)
        {
            double[] map = new double[this.Tiles.Length];
            for (int i = 0; i < this.Tiles.Length; i++)
            {   
                Race master = this.Tiles[i].MasterRace;
                if (u.Race.CanMove(this.Tiles[i].TileType) && (master == null || master == u.Race))
                {
                    map[i] = u.Race.GetRequiredMovePoints(this.Tiles[i].TileType);
                }
                else
                {
                    map[i] = -1; // Unable to move on this tile
                }
            }
            return map;
        }

        /// <summary>
        /// Gets the tile at the specified position.
        /// </summary>
        /// <returns>The tile.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
		public Tile getTile(int x, int y) {
            if (x < 0 || x >= this.SizeX || y < 0 || y >= this.SizeY)
            {
                return null;
            }
			return Tiles [x + SizeX * y];
		}
            

    }
}
