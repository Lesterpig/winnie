using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    /// <summary>
    /// Unit factory.
    /// </summary>
    public class UnitFactory
    {
        /// <summary>
        /// Builds the specified Unit for specified player and tile.
        /// It also adds the unit to the player and tile own lists, setting the default Life points value.
        /// </summary>
        /// <param name="p">Owner.</param>
        /// <param name="t">Position.</param>
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