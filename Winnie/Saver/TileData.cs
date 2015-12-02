using System;
using Core;

namespace Saver
{
    public class TileData
    {
        public TileTypeFactory.Identifier Type;
        public int Position;

        public TileData(Tile t, int i)
        {
            this.Position = i;
            this.Type = t.TileType.Type;
        }
    }
}

