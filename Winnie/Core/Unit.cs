using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{   
    // TODO documentation
    public class Unit
    {   

        public Unit(Player player, Tile t)
        {
            this.Player = player;
            this.Tile = t;
        }
            
        public Player Player { get; private set; }
        public Race Race { get { return this.Player.Race; } }
        public Tile Tile { get; private set; }
        public int Life { get; set; }
        public bool Alive { get { return this.Life > 0; } }
        public double MovePoints { get; set; }

        public double LifeRatio
        {
            get { return (double)this.Life / this.Race.Life; }
        }

        public int AttackPoints
        {
            get { return this.Alive ? (int)Math.Ceiling(this.Race.Attack * LifeRatio) : 0; }
        }

        public int DefensePoints
        {
            get { return this.Alive ? (int)Math.Ceiling(this.Race.Armor * LifeRatio) : 0; }
        }

        public int VictoryPoints
        {
            get
            {
                return this.Alive ? this.Race.GetVictoryPoints(this.Tile.TileType) : 0;
            }
        }

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

                foreach (Tile t in map.Tiles)
                {
                    var score = dijkstra.getDistance(t.Point);

                    if (score > 0 && score <= this.MovePoints && t != this.Tile)
                    {
                        var steps = dijkstra.getPath(t.Point);
                        steps.Reverse();
                        var actions = new List<Move>();
                        for (int i = 1; i < steps.Count; i++)
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

        public void Move(Tile to, bool free = false)
        {   
            CheckActionAllowed(to, false, false, free);
            this.Tile.RemoveUnit(this);
            this.Tile = to;
            this.Tile.AddUnit(this);
        }

        public class AttackResult
        {
            public Unit Winner;
            public Unit Loser;
            public int Dmg;

            public AttackResult(Unit w, Unit l, int dmg) {
                this.Winner = w;
                this.Loser = l;
                this.Dmg = dmg;
            }
        }

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

        // Exceptions

        public class MovementNotAllowedException : Exception {}
        public class NotEnoughMovePointsException : Unit.MovementNotAllowedException {}
        public class EnnemiesRemainingException : Unit.MovementNotAllowedException {}
        public class SameRaceAttackException : Unit.MovementNotAllowedException {}
    }
}