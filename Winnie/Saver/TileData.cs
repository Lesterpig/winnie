using System;
using Core;

namespace Saver
{
    public class TileData
    {
        public TileTypeFactory.Identifier Type;

        public TileData()
        {
        }

        public TileData(Tile t)
        {
            this.Type = t.TileType.Type;
        }
    }
}

