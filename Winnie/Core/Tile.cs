using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Tile
    {
        public TileType TileType
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
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