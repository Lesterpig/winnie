using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    // TODO tests
    public class Tile
    {   

        public class Neighborhood // JavaScript style.
        {
            public Tile N;
            public Tile S;
            public Tile E;
            public Tile W;
        }
            
        public Tile(TileType t)
        {
            this._tiletype = t;
            this._neighbors = new Neighborhood();
            this._units = new HashSet<Unit>();
        }

        private TileType _tiletype;
        public TileType TileType
        {
            get { return this._tiletype; }
        }
            
        private Neighborhood _neighbors;
        public Neighborhood Neighbors
        {
            get { return this._neighbors; }
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
    }
}