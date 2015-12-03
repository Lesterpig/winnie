using System;
using Core;

namespace Saver
{
    public class UnitData
    {
        public int Life;
        public double MovePoints;
        public int Position;

        public UnitData()
        {
        }

        public UnitData(Unit u)
        {
            this.Life = u.Life;
            this.MovePoints = u.MovePoints;
            this.Position = u.Tile.Position;
        }
    }
}

