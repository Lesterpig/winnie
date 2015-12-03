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

        public Unit Rebuild(Player player, Map map)
        {
            var unit = new Unit(player, map.Tiles[this.Position]);
            unit.Life = this.Life;
            unit.MovePoints = this.MovePoints;
            return unit;
        }

    }
}

