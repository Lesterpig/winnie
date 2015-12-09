using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    /// <summary>
    /// Orc default values.
    /// </summary>
    public class Orc : Race
    {
        /// <summary>
        /// The instance (singleton).
        /// </summary>
        public static Orc Instance = new Orc();

        /// <summary>
        /// Initializes a new instance of the <see cref="Core.Orc"/> class.
        /// </summary>
        private Orc() {}

        /// <summary>
        /// Base armor points.
        /// </summary>
        /// <value>The starting number of armor points.</value>
        public int Armor
        {
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// Base attack points.
        /// </summary>
        /// <value>The starting number of attack points.</value>
        public int Attack
        {
            get
            {
                return 5;
            }
        }

        /// <summary>
        /// Base life points.
        /// </summary>
        /// <value>The starting number of life points.</value>
        public int Life
        {
            get
            {
                return 17;
            }
        }

        /// <summary>
        /// Gets the identifier of the race.
        /// </summary>
        /// <remarks>Only used for C++ wrapper.</remarks>
        /// <value>The race identifier.</value>
		public int Identifier
		{
			get 
			{
				return 2;
			}
		}

        /// <summary>
        /// Determines whether this instance can move TO the specified tile type.
        /// </summary>
        /// <returns><c>true</c> if this instance can move to the specified tile type; otherwise, <c>false</c>.</returns>
        /// <param name="tileType">Tile type.</param>
        public bool CanMove(TileType tileType)
        {
            return !(tileType is WaterTileType);
        }

        /// <summary>
        /// Gets the required move points.
        /// </summary>
        /// <returns>The required move points to move to the specified tile type.</returns>
        /// <param name="tileType">Tile type.</param>
        public double GetRequiredMovePoints(TileType tileType)
        {
            if (tileType is PlainTileType) { return (float) 0.5; }
            else { return 1; }
        }

        /// <summary>
        /// Gets the victory points.
        /// </summary>
        /// <returns>The victory points in the specified tile type.</returns>
        /// <param name="tileType">Tile type.</param>
        public int GetVictoryPoints(TileType tileType)
        {
            if (tileType is PlainTileType || tileType is ForestTileType) { return 1; }
            else if (tileType is MountainTileType) { return 2; }
            else { return 0; }
        }

        /// <summary>
        /// Determines whether this instance can do a ranged attack FROM the specified tile type.
        /// </summary>
        /// <returns><c>true</c> if this instance can do ranged attack from the specified tile type; otherwise, <c>false</c>.</returns>
        /// <param name="tileType">Tile type.</param>
        public bool CanDoRangedAttack(TileType tileType)
        {
            return tileType is MountainTileType;
        }
    }
}