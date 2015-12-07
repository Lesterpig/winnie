using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class UnitFactory
    {
        public static void Build(Player p, Tile t)
        {
            new Unit(p, t); // TODO upgrade this factory :(
        }
    }
}