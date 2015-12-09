using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    // TODO documentation
    public class PerlinMap : MapGeneration
    {   
        private Map _map;
        private Algo _algo;

        public PerlinMap()
        {
            this._algo = new Algo();
        }

        public Map Generate(uint size, int seed = 0)
        {
            return this._map = new Map(this._algo.CreateMap(seed, (int) size, (int) size), size, size);
        }

        public void PlacePlayers(Player p1, Player p2)
        {
            this._algo.FindBestStartPosition(p1, p2, this._map);
        }
    }

}