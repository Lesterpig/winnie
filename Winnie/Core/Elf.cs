using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Elf : Race
    {

        public static Elf Instance = new Elf();

        public int Armor
        {
            get
            {
                return 3;
            }
        }

        public int Attack
        {
            get
            {
                return 4;
            }
        }

        public int Life
        {
            get
            {
                return 12;
            }
        }

        public bool CanMove(TileType tileType)
        {
            return !(tileType is WaterTileType);
        }

        public double GetRequiredMovePoints(TileType tileType)
        {
            if(tileType is MountainTileType) { return 2; }
            else { return 1; }
        }

        public int GetVictoryPoints(TileType tileType)
        {
            if (tileType is PlainTileType) { return 1; }
            else if (tileType is ForestTileType) { return 3; }
            else { return 0; }
        }
    }
}