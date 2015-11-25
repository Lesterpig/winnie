using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class TileTypeFactory
    {	

		private static Dictionary<string, TileType> types = new Dictionary<string, TileType>();

		public static TileType Get(string type)
        {
			if (types.ContainsKey(type)) {
				return types[type];
			}

			TileType newType;

			switch (type) {
			case "Water":
				newType = new WaterTileType();
				break;
			case "Plain":
				newType = new PlainTileType();
				break;
			case "Forest":
				newType = new ForestTileType();
				break;
			case "Mountain":
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