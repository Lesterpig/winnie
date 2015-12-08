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
            this.Name = name;
            this.Race = race;
            this.Units = new HashSet<Unit>();
			this.InitialPosition = new Point ();
        }

		public Point InitialPosition { get; set; }
        public Race Race { get; private set; }
        public string Name { get; private set; }
        public ISet<Unit> Units { get; set; }

        public int Score
        {
            get
            {   
                return this.Units.Sum(u => u.VictoryPoints);
            }
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