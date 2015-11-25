﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Human : Race
    {

        public static Human Instance = new Human();

        private Human() {}

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
                return 6;
            }
        }

        public int Life
        {
            get
            {
                return 15;
            }
        }

        public bool CanMove(TileType tileType)
        {
            return true;
        }

        public float GetRequiredMovePoints(TileType tileType)
        {
            return 1;
        }

        public int GetVictoryPoints(TileType tileType)
        {
            if (tileType is WaterTileType) { return 0; }
            else if (tileType is PlainTileType) { return 2; }
            else { return 1; }
        }
    }
}