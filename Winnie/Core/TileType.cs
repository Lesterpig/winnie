using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public abstract class TileType
    {
		public abstract int Height { get; }
		public abstract string Type { get; }
    }

    public class PlainTileType : TileType
    {
		public override int Height { get { return 1; } }
		public override string Type { get { return "Plain"; } }
    }

    public class WaterTileType : TileType
    {
		public override int Height { get { return 0; } }
		public override string Type { get { return "Water"; } }
    }

    public class ForestTileType : TileType
    {
		public override int Height { get { return 2; } }
		public override string Type { get { return "Forest"; } }
    }

    public class MountainTileType : TileType
    {
		public override int Height { get { return 3; } }
		public override string Type { get { return "Mountain"; } }
    }
}