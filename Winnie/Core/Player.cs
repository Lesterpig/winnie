using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    /// <summary>
    /// Player.
    /// </summary>
    /// <remarks>
    /// A player is not linked to a game after creation.
    /// </remarks>
    public class Player
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Core.Player"/> class.
        /// </summary>
        /// <param name="name">Name.</param>
        /// <param name="race">Race.</param>
        public Player(string name, Race race)
        {
            this.Name = name;
            this.Race = race;
            this.Units = new HashSet<Unit>();
			this.InitialPosition = new Point ();
        }

        /// <summary>
        /// Gets or sets the initial position, where player units are going to be created by default.
        /// </summary>
        /// <value>The initial position.</value>
		public Point InitialPosition { get; set; }

        /// <summary>
        /// Gets the race.
        /// </summary>
        /// <value>The race.</value>
        public Race Race { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the units.
        /// </summary>
        /// <value>The units.</value>
        public ISet<Unit> Units { get; private set; }

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <value>The score.</value>
        public int Score
        {
            get
            {   
                return this.Units.Sum(u => u.VictoryPoints);
            }
        }

        /// <summary>
        /// Adds a unit to player's units.
        /// </summary>
        /// <param name="u">An unit.</param>
        public void AddUnit(Unit u)
        {
            this.Units.Add(u);
        }

        /// <summary>
        /// Starts the player's turn.
        /// </summary>
        public void StartTurn()
        {
            foreach (Unit unit in this.Units)
            {
                unit.MovePoints = 2;
            }
        }
    }
}