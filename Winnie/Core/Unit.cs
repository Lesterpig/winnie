using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class Unit
    {   

        public Unit(Player player, Tile t)
        {
            this._player = player;
            this._player.AddUnit(this);
            this._tile = t;
            this._tile.AddUnit(this);
            this._life = this.Race.Life;
        }

        private Player _player;
        public Player Player
        {
            get { return this._player; }
        }

        public Race Race
        {
            get { return this._player.Race; }
        }

        private Tile _tile;
        public Tile Tile
        {
            get { return this._tile; }
        }

        private int _life;
        public int Life
        {
            get { return this._life; }
            set { this._life = value; }
        }

        public bool Alive
        {
            get { return this._life > 0; }
        }

        public double LifeRatio
        {
            get { return (double)this._life / this.Race.Life; }
        }

        public int AttackPoints
        {
            get { return this.Alive ? (int)Math.Ceiling(this.Race.Attack * LifeRatio) : 0; }
        }

        public int DefensePoints
        {
            get { return this.Alive ? (int)Math.Ceiling(this.Race.Armor * LifeRatio) : 0; }
        }

        public double MovePoints
        {
            get;
            set;
        }

        public int VictoryPoints
        {
            get
            {
                return this.Alive ? this.Race.GetVictoryPoints(this.Tile.TileType) : 0;
            }
        }

        public List<Action> Possibilities
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        public void Move(Tile to, bool reverse = false)
        {   
            CheckActionAllowed(to, false, reverse);
            this.Tile.RemoveUnit(this);
            this._tile = to;
            this.Tile.AddUnit(this);
        }

        public class AttackResult
        {
            public Unit Winner;
            public Unit Loser;
            public int Dmg;
            public bool Killed;

            public AttackResult(Unit w, Unit l, int dmg) {
                this.Winner = w;
                this.Loser = l;
                this.Dmg = dmg;
                this.Killed = l.Life <= 0;
            }
        }

        public AttackResult Attack(Unit target, bool ranged = false)
        {   
            if (this.Race == target.Race)
            {
                throw new SameRaceAttackException();
            }

            CheckActionAllowed(target.Tile, true, false, ranged);

            bool won = true;

            if (!ranged)
            {
                int chances = 100 * this.AttackPoints / (this.AttackPoints + target.AttackPoints);
                won = Game.Random.Next(100) < chances;
            }

            Unit winner = won ? this : target;
            Unit loser = won ? target : this;
            int dmg = Game.Random.Next(ranged ? 0 : 1, 6);

            loser.Life -= dmg;

            return new AttackResult(winner, loser, dmg);
        }

        private void CheckActionAllowed(Tile to, bool attack, bool reverse, bool ranged = false) {
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

            double requiredPoints = this.Race.GetRequiredMovePoints(to.TileType);

            if (!reverse)
            {
                if (requiredPoints > this.MovePoints)
                {
                    throw new Unit.NotEnoughMovePointsException();
                }
                this.MovePoints -= requiredPoints;
            }
            else
            {
                this.MovePoints += requiredPoints; // TODO check what to do during nextTurns actions for reverse operation
            }
        }

        // Exceptions

        public class MovementNotAllowedException : Exception {}
        public class NotEnoughMovePointsException : Unit.MovementNotAllowedException {}
        public class EnnemiesRemainingException : Unit.MovementNotAllowedException {}
        public class SameRaceAttackException : Unit.MovementNotAllowedException {}
    }
}