using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class TileTypeFactory
    {	

		private static Dictionary<Identifier, TileType> types = new Dictionary<Identifier, TileType>();

        // Used for tile creation from C++ wrapper
        public enum Identifier { WATER = 0, PLAIN = 1, FOREST = 2, MOUNTAIN = 3 }

		public static TileType Get(Identifier type)
        {
			if (types.ContainsKey(type))
            {
				return types[type];
			}

			TileType newType;

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
			default:
				newType = null;
				break;
			}

			return types[type] = newType;
        }
    }
}