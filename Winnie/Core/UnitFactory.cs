using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class UnitFactory
    {
        public static Unit Build(Player p, Tile t)
        {
            Unit u = new Unit(p, t);
            p.AddUnit(u);
            t.AddUnit(u);
            u.Life = p.Race.Life;
            return u;
        }
    }
}