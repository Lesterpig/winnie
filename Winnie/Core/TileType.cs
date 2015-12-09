using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    // TODO documentation
    public abstract class TileType
    {
		public abstract int Height { get; }
		public abstract TileTypeFactory.Identifier Type { get; }
    }

    public class PlainTileType : TileType
    {
		public override int Height { get { return 1; } }
        public override TileTypeFactory.Identifier Type { get { return TileTypeFactory.Identifier.PLAIN; } }
    }

    public class WaterTileType : TileType
    {
		public override int Height { get { return 0; } }
        public override TileTypeFactory.Identifier Type { get { return TileTypeFactory.Identifier.WATER; } }
    }

    public class ForestTileType : TileType
    {
		public override int Height { get { return 2; } }
        public override TileTypeFactory.Identifier Type { get { return TileTypeFactory.Identifier.FOREST; } }
    }

    public class MountainTileType : TileType
    {
		public override int Height { get { return 3; } }
        public override TileTypeFactory.Identifier Type { get { return TileTypeFactory.Identifier.MOUNTAIN; } }
    }
}