using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    // TODO tests
    public class Tile
    {   

        public Tile(TileType t, int position = -1)
        {
            this._tiletype = t;
            this.Neighbors = new Dictionary<Diff, Tile>();
            this._units = new HashSet<Unit>();
            this.Position = position;
        }

        public int Position { get; private set; }
        public Map Map { get; set; }

        private TileType _tiletype;
        public TileType TileType
        {
            get { return this._tiletype; }
        }

        private ISet<Unit> _units;
        public ISet<Unit> Units
        {
            get { return this._units; }
        }

        public void AddUnit(Unit u) {
            this._units.Add(u);
        }

        public void RemoveUnit(Unit u) {
            this._units.Remove(u);
        }

        public Unit StrongestUnit
        {
            get
            { 
                Unit best = null;
                foreach (Unit u in this._units) // TODO improve code style?
                {
                    if (u.Alive && (best == null || best.DefensePoints < u.DefensePoints))
                    {
                        best = u;
                    }
                }
                return best;
            }  
        }

        public Race MasterRace
        {
            get
            {
                foreach (Unit u in this._units) // TODO improve code style?
                {
                    if (u.Alive)
                    {
                        return u.Race;
                    }
                }
                return null;
            }
        }

        public Point Point // TODO set during construction
        {
            get
            {
                return new Point(this.Position % this.Map.SizeX, (int)this.Position / this.Map.SizeX);
            }
        }


        /*
         * Neighbor management
         * 
         * For fast access to relative tiles, we use a "Diff" class and a cache (dictionnary).
         */

        public class Diff {
            public int Dx { get; set; }
            public int Dy { get; set; }

            public Diff(int dx, int dy)
            {
                this.Dx = dx;
                this.Dy = dy;
            }

            public bool Equals(Diff b)
            {
                return this.Dx == b.Dx && this.Dy == b.Dy;
            }
        }

        private IDictionary<Diff, Tile> Neighbors;

        public Tile GetNeighbor(Diff diff)
        {
            if (!this.Neighbors.ContainsKey(diff))
            {
                this.Neighbors[diff] = this.Map.getTile(this.Point.x + diff.Dx, this.Point.y + diff.Dy);
            }
            return this.Neighbors[diff];
        }
    }
}