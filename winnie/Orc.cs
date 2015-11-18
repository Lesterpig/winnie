using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace winnie
{
    public class Orc : Race
    {

        public static Orc Instance = new Orc();

        public int Armor
        {
            get
            {
                return 2;
            }
        }

        public int Attack
        {
            get
            {
                return 5;
            }
        }

        public int Life
        {
            get
            {
                return 17;
            }
        }

        public bool CanMove(TileType tileType)
        {
            return !(tileType is WaterTileType);
        }

        public float GetRequiredMovePoints(TileType tileType)
        {
            if (tileType is PlainTileType) { return (float) 0.5; }
            else { return 1; }
        }

        public float GetVictoryPoints(TileType tileType)
        {
            if (tileType is PlainTileType || tileType is ForestTileType) { return 1; }
            else if (tileType is MountainTileType) { return 2; }
            else { return 0; }
        }
    }
}