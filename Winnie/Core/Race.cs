using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    /// <summary>
    /// Race definition.
    /// 
    /// Race classes are used to define default values for units.
    /// They should be instancied as singleton.
    /// </summary>
    /// <seealso cref="Human"/>
    /// <seealso cref="Elf"/>
    /// <seealso cref="Orc"/>
    public interface Race
    {
        /// <summary>
        /// Base life points.
        /// </summary>
        /// <value>The starting number of life points.</value>
        int Life
        {
            get;
        }

        /// <summary>
        /// Base armor points.
        /// </summary>
        /// <value>The starting number of armor points.</value>
        int Armor
        {
            get;
        }

        /// <summary>
        /// Base attack points.
        /// </summary>
        /// <value>The starting number of attack points.</value>
        int Attack
        {
            get;
        }

		/// <summary>
        /// Gets the identifier of the race.
        /// </summary>
        /// <remarks>
        /// Only used for C++ wrapper.
        /// </remarks>
        /// <value>The race identifier.</value>
		int Identifier {
			get;
		}

        /// <summary>
        /// Gets the required move points.
        /// </summary>
        /// <returns>The required move points to move to the specified tile type.</returns>
        /// <param name="tileType">Tile type.</param>
        double GetRequiredMovePoints(TileType tileType);

        /// <summary>
        /// Gets the victory points.
        /// </summary>
        /// <returns>The victory points in the specified tile type.</returns>
        /// <param name="tileType">Tile type.</param>
        int GetVictoryPoints(TileType tileType);

        /// <summary>
        /// Determines whether this instance can move TO the specified tile type.
        /// </summary>
        /// <returns><c>true</c> if this instance can move to the specified tile type; otherwise, <c>false</c>.</returns>
        /// <param name="tileType">Tile type.</param>
        bool CanMove(TileType tileType);

        /// <summary>
        /// Determines whether this instance can do a ranged attack FROM the specified tile type.
        /// </summary>
        /// <returns><c>true</c> if this instance can do ranged attack from the specified tile type; otherwise, <c>false</c>.</returns>
        /// <param name="tileType">Tile type.</param>
        bool CanDoRangedAttack(TileType tileType);
    }
}