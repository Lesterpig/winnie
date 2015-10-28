using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace winnie
{
    public abstract class TileType
    {
        private int Height;
        private string Type;
    }

    public class PlainTileType : TileType
    {
    }

    public class WaterTileType : TileType
    {
    }

    public class ForestTileType : TileType
    {
    }

    public class MountainTileType : TileType
    {
    }
}