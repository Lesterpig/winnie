using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    /// <summary>
    /// Flyweight design pattern to obtain TileType instances.
    /// </summary>
    public class TileTypeFactory
    {	
        /// <summary>
        /// Tile types instances cache.
        /// </summary>
		private static IDictionary<Identifier, TileType> types = new Dictionary<Identifier, TileType>();

        /// <summary>
        /// Identifiers.
        /// </summary>
        /// <remarks>
        /// Used for tile creation from C++.
        /// </remarks>
        public enum Identifier { WATER = 0, PLAIN = 1, FOREST = 2, MOUNTAIN = 3 }

        /// <summary>
        /// Get the specified type (Flyweight).
        /// </summary>
        /// <param name="type">Type.</param>
		public static TileType Get(Identifier type)
        {
			if (types.ContainsKey(type))
            {
				return types[type]; // use cache
			}

			TileType newType = null;

			switch (type) {
			case Identifier.WATER:
			    newType = new WaterTileType();
				break;
			case Identifier.PLAIN:
				newType = new PlainTileType();
				break;
            case Identifier.FOREST:
				newType = new ForestTileType();
				break;
			case Identifier.MOUNTAIN:
				newType = new MountainTileType();
				break;
			}

			return types[type] = newType;
        }
    }
}