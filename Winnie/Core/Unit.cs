using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    /// <summary>
    /// Unit represents a player warrior.
    /// </summary>
    public class Unit
    {   
        /// <summary>
        /// Initializes a new instance of the <see cref="Core.Unit"/> class.
        /// </summary>
        /// <remarks>
        /// Please use <see cref="UnitFactory.Build"/> instead.
        /// </remarks>
        /// <param name="player">Player.</param>
        /// <param name="t">Tile.</param>
        public Unit(Player player, Tile t)
        {
            this.Player = player;
            this.Tile = t;
        }

        /// <summary>
        /// Gets the player.
        /// </summary>
        /// <value>The player.</value>
        public Player Player { get; private set; }

        /// <summary>
        /// Gets the race.
        /// </summary>
        /// <value>The race.</value>
        public Race Race { get { return this.Player.Race; } }

        /// <summary>
        /// Gets the tile.
        /// </summary>
        /// <value>The tile.</value>
        public Tile Tile { get; private set; }

        /// <summary>
        /// Gets or sets the life.
        /// </summary>
        /// <value>The life.</value>
        public int Life { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Core.Unit"/> is alive.
        /// </summary>
        /// <value><c>true</c> if alive; otherwise, <c>false</c>.</value>
        public bool Alive { get { return this.Life > 0; } }

        /// <summary>
        /// Gets or sets the move points.
        /// </summary>
        /// <value>The move points.</value>
        public double MovePoints { get; set; }

        /// <summary>
        /// Gets the life ratio.
        /// </summary>
        /// <value>The life ratio.</value>
        public double LifeRatio
        {
            get { return (double)this.Life / this.Race.Life; }
        }

        /// <summary>
        /// Gets the attack points.
        /// </summary>
        /// <value>The attack points.</value>
        public int AttackPoints
        {
            get { return this.Alive ? (int)Math.Ceiling(this.Race.Attack * LifeRatio) : 0; }
        }

        /// <summary>
        /// Gets the defense points.
        /// </summary>
        /// <value>The defense points.</value>
        public int DefensePoints
        {
            get { return this.Alive ? (int)Math.Ceiling(this.Race.Armor * LifeRatio) : 0; }
        }

        /// <summary>
        /// Gets the victory points.
        /// </summary>
        /// <value>The victory points.</value>
        public int VictoryPoints
        {
            get
            {
                return this.Alive ? this.Race.GetVictoryPoints(this.Tile.TileType) : 0;
            }
        }

        /// <summary>
        /// Gets the move possibilites.
        /// </summary>
        /// <value>
        /// Keys are tiles corresponding to allowed movements,
        /// values are associated list of movements to perform.
        /// </value>
        public IDictionary<Tile, List<Move>> MovePossibilites
        {
            get
            {   
                var possibilities = new Dictionary<Tile, List<Move>>();

                if (!this.Alive)
                {
                    return possibilities;
                }

                var map = this.Tile.Map;
                var dijkstra = new Dijkstra(map.GetMoveMap(this), (int) map.SizeX, (int) map.SizeY, this.Tile.Point);

                foreach (Tile t in map.Tiles) // Perform Dijkstra algorithme to every tile.
                {
                    var score = dijkstra.getDistance(t.Point);

                    if (score > 0 && score <= this.MovePoints && t != this.Tile) // The movement is possible.
                    {
                        var steps = dijkstra.getPath(t.Point);
                        steps.Reverse();
                        var actions = new List<Move>();
                        for (int i = 1; i < steps.Count; i++) // Create list of actions.
                        {   
                            var fr = steps[i-1];
                            var to = steps[i];
                            actions.Add(new Move(this, map.getTile(fr.x, fr.y), map.getTile(to.x, to.y)));
                        }
                        possibilities.Add(t, actions);
                    }
                }

                return possibilities;
            }
        }

        /// <summary>
        /// Gets the battle possibilities.
        /// </summary>
        /// <value>
        /// Keys are tiles corresponding to allowed battles,
        /// values are associated list of battle to execute.
        /// </value>
        public IDictionary<Tile, Battle> BattlePossibilities
        {
            get
            {
                var d = new Dictionary<Tile, Battle>();

                // Near attacks

                AddToBattlePossibilitiesOrNot(d, this.Tile.GetNeighbor(new Tile.Diff(0, -1)));
                AddToBattlePossibilitiesOrNot(d, this.Tile.GetNeighbor(new Tile.Diff(0, 1)));
                AddToBattlePossibilitiesOrNot(d, this.Tile.GetNeighbor(new Tile.Diff(-1, 0)));
                AddToBattlePossibilitiesOrNot(d, this.Tile.GetNeighbor(new Tile.Diff(1, 0)));

                // Ranged attack

                AddToBattlePossibilitiesOrNot(d, this.Tile.GetNeighbor(new Tile.Diff(0, -2)), true);
                AddToBattlePossibilitiesOrNot(d, this.Tile.GetNeighbor(new Tile.Diff(0, 2)), true);
                AddToBattlePossibilitiesOrNot(d, this.Tile.GetNeighbor(new Tile.Diff(-2, 0)), true);
                AddToBattlePossibilitiesOrNot(d, this.Tile.GetNeighbor(new Tile.Diff(2, 0)), true);


                return d;
            }
        }

        /// <summary>
        /// Adds a tile to battle possibilities or not, depending on race and tile specifications.
        /// </summary>
        /// <param name="d">Current dictionary of possibilities.</param>
        /// <param name="t">Current tile to check.</param>
        /// <param name="ranged">If set to <c>true</c> ranged.</param>
        private void AddToBattlePossibilitiesOrNot(IDictionary<Tile, Battle> d, Tile t, bool ranged = false)
        {
            if (t != null && this.Alive)
            {   
                if (!ranged || ranged && this.Race.CanDoRangedAttack(this.Tile.TileType))
                {
                    var required = ranged ? 1 : this.Race.GetRequiredMovePoints(t.TileType);
                    Unit strongest = t.StrongestUnit;
                    if (strongest != null && strongest.Race != this.Race && this.MovePoints >= required)
                    {
                        d.Add(t, new Battle(this, strongest, ranged));
                    }
                }
            }
        }

        /// <summary>
        /// Move the Unit to the specified tile.
        /// </summary>
        /// <param name="to">Destination.</param>
        /// <param name="free">If set to <c>true</c>, does not consume any movement point.</param>
        /// <exception cref="MovementNotAllowedException"></exception>
        public void Move(Tile to, bool free = false)
        {   
            CheckActionAllowed(to, false, false, free);
            this.Tile.RemoveUnit(this);
            this.Tile = to;
            this.Tile.AddUnit(this);
        }

        /// <summary>
        /// A utility class to save battle result.
        /// </summary>
        public class AttackResult
        {
            /// <summary>
            /// The winner.
            /// </summary>
            public Unit Winner;

            /// <summary>
            /// The loser.
            /// </summary>
            public Unit Loser;

            /// <summary>
            /// The damages performed to the loser.
            /// </summary>
            public int Dmg;

            /// <summary>
            /// Initializes a new instance of the <see cref="Core.Unit+AttackResult"/> class.
            /// </summary>
            /// <param name="w">The winer.</param>
            /// <param name="l">The loser.</param>
            /// <param name="dmg">The damages performed.</param>
            public AttackResult(Unit w, Unit l, int dmg) {
                this.Winner = w;
                this.Loser = l;
                this.Dmg = dmg;
            }
        }

        /// <summary>
        /// Attacks the specified target.
        /// </summary>
        /// <param name="target">Target.</param>
        /// <param name="ranged">If set to <c>true</c>, attack with a ranged attack.</param>
        /// <exception cref="MovementNotAllowedException"></exception>
        public AttackResult Attack(Unit target, bool ranged = false)
        {   
            if (this.Race == target.Race)
            {
                throw new SameRaceAttackException();
            }

            CheckActionAllowed(target.Tile, true, ranged, false);

            bool won = true;

            if (!ranged)
            {
                int chances = 100 * this.AttackPoints / (this.AttackPoints + target.AttackPoints);
                won = Game.Random.Next(0, 100) < chances;
            }

            Unit winner = won ? this : target;
            Unit loser = won ? target : this;
            int dmg = Game.Random.Next(ranged ? 0 : 1, 6);

            loser.Life -= dmg;

            return new AttackResult(winner, loser, dmg);
        }

        /// <summary>
        /// Checks if the asked action is allowed for the current unit.
        /// </summary>
        /// <param name="to">Destination</param>
        /// <param name="attack">If set to <c>true</c>, is an attack.</param>
        /// <param name="ranged">If set to <c>true</c>, is a ranged attack.</param>
        /// <param name="free">If set to <c>true</c>, does not consume any movement point.</param>
        private void CheckActionAllowed(Tile to, bool attack, bool ranged = false, bool free = false) {
            if (to == this.Tile)
            {
                return; // Dumb case, silently do nothing.
            }

            if (!this.Race.CanMove(to.TileType) && !ranged)
            {   
                throw new Unit.MovementNotAllowedException();
            }

            if (!attack && to.MasterRace != this.Race && to.MasterRace != null)
            {
                throw new Unit.EnnemiesRemainingException();
            }

            double requiredPoints = ranged ? 1 : this.Race.GetRequiredMovePoints(to.TileType);

            if (!free)
            {
                if (requiredPoints > this.MovePoints)
                {
                    throw new Unit.NotEnoughMovePointsException();
                }
                this.MovePoints -= requiredPoints;
            }
        }

        /// <summary>
        /// Movement not allowed exception.
        /// </summary>
        public class MovementNotAllowedException : Exception {}

        /// <summary>
        /// Not enough move points exception.
        /// </summary>
        public class NotEnoughMovePointsException : Unit.MovementNotAllowedException {}

        /// <summary>
        /// Ennemies remaining exception.
        /// </summary>
        public class EnnemiesRemainingException : Unit.MovementNotAllowedException {}

        /// <summary>
        /// Same race attack exception.
        /// </summary>
        public class SameRaceAttackException : Unit.MovementNotAllowedException {}
    }
}