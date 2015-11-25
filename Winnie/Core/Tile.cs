using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Tile
    {   

        private TileType _tiletype;
        public Tile(TileType t)
        {
            this._tiletype = t;
        }

        public TileType TileType
        {
            get { return this._tiletype; }
        }

        public List<Tile> Neighbours
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public List<Unit> Units
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Unit GetStrongestUnit()
        {
            throw new System.NotImplementedException();
        }
    }
}