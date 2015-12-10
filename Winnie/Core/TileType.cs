using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    /// <summary>
    /// Tile type.
    /// </summary>
    public abstract class TileType
    {
        /// <summary>
        /// Gets the type identifier.
        /// </summary>
        /// <remarks>
        /// Used for <see cref="TileTypeFactory.Get"/> flyweight desing pattern.
        /// </remarks>
        /// <value>The type identifier.</value>
        /// <seealso cref="TileTypeFactory"/>
        /// <seealso cref="PlainTileType"/>
        /// <seealso cref="WaterTileType"/>
        /// <seealso cref="ForestTileType"/>
        /// <seealso cref="MountainTileType"/>
		public abstract TileTypeFactory.Identifier Type { get; }
    }

    /// <summary>
    /// Plain tile type.
    /// </summary>
    public class PlainTileType : TileType
    {   
        /// <summary>
        /// Gets the type identifier.
        /// </summary>
        /// <value>The type identifier.</value>
        public override TileTypeFactory.Identifier Type { get { return TileTypeFactory.Identifier.PLAIN; } }
    }

    /// <summary>
    /// Water tile type.
    /// </summary>
    public class WaterTileType : TileType
    {
        /// <summary>
        /// Gets the type identifier.
        /// </summary>
        /// <value>The type identifier.</value>
        public override TileTypeFactory.Identifier Type { get { return TileTypeFactory.Identifier.WATER; } }
    }

    /// <summary>
    /// Forest tile type.
    /// </summary>
    public class ForestTileType : TileType
    {
        /// <summary>
        /// Gets the type identifier.
        /// </summary>
        /// <value>The type identifier.</value>
        public override TileTypeFactory.Identifier Type { get { return TileTypeFactory.Identifier.FOREST; } }
    }

    /// <summary>
    /// Mountain tile type.
    /// </summary>
    public class MountainTileType : TileType
    {
        /// <summary>
        /// Gets the type identifier.
        /// </summary>
        /// <value>The type identifier.</value>
        public override TileTypeFactory.Identifier Type { get { return TileTypeFactory.Identifier.MOUNTAIN; } }
    }
}