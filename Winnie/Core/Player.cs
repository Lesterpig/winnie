using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Player
    {
    
        public Player(string name, Race race)
        {
            this._name = name;
            this._race = race;
            this._units = new HashSet<Unit>();
			this.InitialPosition = new Point ();
        }

		public Point InitialPosition { get; set; }

        private Race _race;
        public Race Race
        {
            get { return _race; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
        }

        public int Score
        {
            get
            {   
                int score = 0;
                foreach (Unit unit in this.Units) {
                    score += unit.VictoryPoints;
                }
                return score;
            }
        }
            
        private ISet<Unit> _units;
        public ISet<Unit> Units
        {
            get { return _units; }
        }

        public void AddUnit(Unit u)
        {
            this.Units.Add(u);
        }

        public void StartTurn()
        {
            foreach (Unit unit in this.Units)
            {
                unit.MovePoints = 2;
            }
        }
    }
}