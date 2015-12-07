using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Map
    {
        public Tile[] Tiles { get; private set; }
        public TileTypeFactory.Identifier[] RawTiles { get; private set; } //Only used for c++
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }

		public Map(TileTypeFactory.Identifier[] tiles, int sx, int sy)
		{   
			this.RawTiles = tiles;
			this.SizeX = sx;
			this.SizeY = sy;

            int size = tiles.Length;
            this.Tiles = new Tile[size];

            for (int i = 0; i < size; i++)
            {
                this.Tiles[i] = new Tile(TileTypeFactory.Get(tiles[i]), i);
                this.Tiles[i].Map = this;
            }
		}

        public double[] GetMoveMap(Unit u)
        {
            double[] map = new double[this.Tiles.Length];
            for (int i = 0; i < this.Tiles.Length; i++)
            {   
                Race master = this.Tiles[i].MasterRace;
                if (u.Race.CanMove(this.Tiles[i].TileType) && (master == null || master == u.Race))
                {
                    map[i] = u.Race.GetRequiredMovePoints(this.Tiles[i].TileType);
                }
                else
                {
                    map[i] = 100;
                }
                System.Console.Write(map[i] + " ");
            }
            return map;
        }

		public Tile getTile(int x, int y) {
            if (x < 0 || x >= this.SizeX || y < 0 || y >= this.SizeY)
            {
                return null;
            }
			return Tiles [x + SizeX * y];
		}
            

    }
}
