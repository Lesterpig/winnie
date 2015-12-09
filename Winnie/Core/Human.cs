using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    /// <summary>
    /// Human default values.
    /// </summary>
    public class Human : Race
    {
        /// <summary>
        /// The instance (singleton).
        /// </summary>
        public static Human Instance = new Human();

        private Human() {}

        /// <summary>
        /// Base armor points.
        /// </summary>
        /// <value>The starting number of armor points.</value>
        public int Armor
        {
            get
            {
                return 3;
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
                return 6;
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
                return 15;
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
				return 0;
			}
		}

        /// <summary>
        /// Determines whether this instance can move TO the specified tile type.
        /// </summary>
        /// <returns><c>true</c> if this instance can move to the specified tile type; otherwise, <c>false</c>.</returns>
        /// <param name="tileType">Tile type.</param>
        public bool CanMove(TileType tileType)
        {
            return true;
        }

        /// <summary>
        /// Gets the required move points.
        /// </summary>
        /// <returns>The required move points to move to the specified tile type.</returns>
        /// <param name="tileType">Tile type.</param>
        public double GetRequiredMovePoints(TileType tileType)
        {
            return 1;
        }

        /// <summary>
        /// Gets the victory points.
        /// </summary>
        /// <returns>The victory points in the specified tile type.</returns>
        /// <param name="tileType">Tile type.</param>
        public int GetVictoryPoints(TileType tileType)
        {
            if (tileType is WaterTileType) { return 0; }
            else if (tileType is PlainTileType) { return 2; }
            else { return 1; }
        }

        /// <summary>
        /// Determines whether this instance can do a ranged attack FROM the specified tile type.
        /// </summary>
        /// <returns><c>true</c> if this instance can do ranged attack from the specified tile type; otherwise, <c>false</c>.</returns>
        /// <param name="tileType">Tile type.</param>
        public bool CanDoRangedAttack(TileType tileType)
        {
            return false;
        }
    }
}