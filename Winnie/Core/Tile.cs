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
            this.TileType = t;
            this.Neighbors = new Dictionary<Diff, Tile>();
            this.Units = new HashSet<Unit>();
            this.Position = position;
        }

        public int Position { get; private set; }
        public Map Map { get; set; }
        public TileType TileType { get; private set; }
        public ISet<Unit> Units { get; private set; }

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