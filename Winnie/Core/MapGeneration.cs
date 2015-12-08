using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public interface MapGeneration
    {
        Map Generate(int size, int seed = 0);
        void PlacePlayers(Player p1, Player p2);
    }
}