using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    /// <summary>
    /// A tile represents a cell on a map.
    /// </summary>
    public class Tile
    {   
        /// <summary>
        /// Initializes a new instance of the <see cref="Core.Tile"/> class.
        /// </summary>
        /// <param name="t">The tile type.</param>
        /// <param name="position">The position in the map, defaults to -1 if not specified. Used to determine neighbors efficiently.</param>
        public Tile(TileType t, int position = -1)
        {
            this.TileType = t;
            this.Neighbors = new Dictionary<Diff, Tile>();
            this.Units = new HashSet<Unit>();
            this.Position = position;
        }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>The position in the map. Used to determine neighbors efficiently</value>
        public int Position { get; private set; }

        /// <summary>
        /// Gets the type of the tile.
        /// </summary>
        /// <value>The type of the tile.</value>
        public TileType TileType { get; private set; }

        /// <summary>
        /// Gets the units.
        /// </summary>
        /// <value>The units.</value>
        public ISet<Unit> Units { get; private set; }

        /// <summary>
        /// Gets the point.
        /// </summary>
        /// <value>The point.</value>
        public Point Point { get; private set; }

        private Map _map;

        /// <summary>
        /// Gets or sets the map.
        /// </summary>
        /// <remarks>
        /// Should be set after creation if position is set.
        /// </remarks>
        /// <value>The map.</value>
        public Map Map
        {
            get
            {
                return this._map;
            }
            set
            {   
                this._map = value;
                if (this._map != null && this.Position >= 0)
                {
                    this.Point = new Point((int) (this.Position % this.Map.SizeX), (int) ((int)this.Position / this.Map.SizeX));
                }
                else
                {
                    this.Point = null;
                }
            }
        }

        /// <summary>
        /// Adds an unit to the tile.
        /// </summary>
        /// <remarks>
        /// This is handled by <see cref="UnitFactory.Build"/>.
        /// </remarks>
        /// <param name="u">An unit to add</param>
        public void AddUnit(Unit u) {
            this.Units.Add(u);
        }

        /// <summary>
        /// Removes an unit from the tile.
        /// </summary>
        /// <param name="u">An unit to remove</param>
        public void RemoveUnit(Unit u) {
            this.Units.Remove(u);
        }

        /// <summary>
        /// Gets the strongest unit on the tile.
        /// </summary>
        /// <value>The strongest unit.</value>
        public Unit StrongestUnit
        {
            get
            { 
                return this.Units.Where(u => u.Alive).OrderByDescending(u => u.DefensePoints).FirstOrDefault();
            }  
        }

        /// <summary>
        /// Gets the master race.
        /// </summary>
        /// <value>The master race on the tile, or null if no alive unit is present.</value>
        public Race MasterRace
        {
            get
            {
                var firstAlive = this.Units.FirstOrDefault(u => u.Alive);
                return firstAlive == null ? null : firstAlive.Race;
            }
        }
            
        /*
         * Neighbor management
         * 
         * For fast access to relative tiles, we use a "Diff" class and a cache (dictionnary).
         */

        /// <summary>
        /// An utility class to deals with intervals.
        /// </summary>
        public class Diff {

            /// <summary>
            /// Gets or sets the dx.
            /// </summary>
            /// <value>The dx.</value>
            public int Dx { get; set; }

            /// <summary>
            /// Gets or sets the dy.
            /// </summary>
            /// <value>The dy.</value>
            public int Dy { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Core.Tile+Diff"/> class.
            /// </summary>
            /// <param name="dx">Dx.</param>
            /// <param name="dy">Dy.</param>
            public Diff(int dx, int dy)
            {
                this.Dx = dx;
                this.Dy = dy;
            }

            /// <summary>
            /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="Core.Tile+Diff"/>.
            /// </summary>
            /// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="Core.Tile+Diff"/>.</param>
            /// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
            /// <see cref="Core.Tile+Diff"/>; otherwise, <c>false</c>.</returns>
            public override bool Equals(Object obj)
            {   
                if (obj == null || GetType() != obj.GetType()) 
                    return false;

                Diff b = (Diff)obj;
                return this.Dx == b.Dx && this.Dy == b.Dy;
            }

            /// <summary>
            /// Serves as a hash function for a <see cref="Core.Tile+Diff"/> object.
            /// </summary>
            /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as
            /// a hash table.</returns>
            public override int GetHashCode() 
            {
                return this.Dx ^ this.Dy;
            }
        }

        /// <summary>
        /// The neighbors cache dictionary.
        /// </summary>
        /// <remarks>
        /// We are supposing tiles are not moving around the map.
        /// </remarks
        private IDictionary<Diff, Tile> Neighbors;

        /// <summary>
        /// Gets the neighbor for asked diff.
        /// </summary>
        /// <remarks>
        /// Results are cached, complexity is near O(1).
        /// It needs <c>Map</c> to be set.
        /// </remarks>
        /// <example>
        /// Get the tile two tiles away from my left:
        /// 
        /// <code>
        /// tile.GetNeighbor(new Tile.Diff(-2, 0))
        /// </code>
        /// </example>
        /// <returns>The neighbor.</returns>
        /// <param name="diff">Diff.</param>
        public Tile GetNeighbor(Diff diff)
        {
            if (!this.Neighbors.ContainsKey(diff))
            {   
                // Cache result
                this.Neighbors[diff] = this.Map.getTile(this.Point.x + diff.Dx, this.Point.y + diff.Dy);
            }
            return this.Neighbors[diff];
        }
    }
}