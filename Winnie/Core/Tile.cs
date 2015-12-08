using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    public class Tile
    {   

        public Tile(TileType t, int position = -1)
        {
            this.TileType = t;
            this.Neighbors = new Dictionary<Diff, Tile>();
            this.Units = new HashSet<Unit>();
            this.Position = position;
        }

        public int Position { get; private set; }
        public TileType TileType { get; private set; }
        public ISet<Unit> Units { get; private set; }
        public Point Point { get; private set; }

        private Map _map;
        public Map Map
        {
            get
            {
                return this._map;
            }
            set
            {   
                this._map = value;
                if (this._map != null && this.Position >= 0)
                {
                    this.Point = new Point((int) (this.Position % this.Map.SizeX), (int) ((int)this.Position / this.Map.SizeX));
                }
                else
                {
                    this.Point = null;
                }
            }
        }

        public void AddUnit(Unit u) {
            this.Units.Add(u);
        }

        public void RemoveUnit(Unit u) {
            this.Units.Remove(u);
        }

        public Unit StrongestUnit
        {
            get
            { 
                return this.Units.Where(u => u.Alive).OrderByDescending(u => u.DefensePoints).FirstOrDefault();
            }  
        }

        public Race MasterRace
        {
            get
            {
                var firstAlive = this.Units.FirstOrDefault(u => u.Alive);
                return firstAlive == null ? null : firstAlive.Race;
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

            public override bool Equals(Object obj)
            {   
                if (obj == null || GetType() != obj.GetType()) 
                    return false;

                Diff b = (Diff)obj;
                return this.Dx == b.Dx && this.Dy == b.Dy;
            }

            public override int GetHashCode() 
            {
                return this.Dx ^ this.Dy;
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